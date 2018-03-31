using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using System.Net;
using System.IO;
using System.Runtime.Caching;
using System.Threading;

namespace iws
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {

        WebRequest request;
        Cache cache = new Cache();
        

        public List<string> GetCities()
        {
            List<string> villes = new List<string>();
            
            villes.Add("toulouse");

            return villes;
        }

        public List<string> GetInfo(string stationName, string cityName)
        {
            List<string> res = cache.getInfo(cityName, stationName);
            if (res == null)
            {
                res = new List<string>();
                res.Add("Station non trouvée");
            }

            return res;

        }

        /*public List<string> GetInfoAsync(string stationName, string cityName)
        {
            List<string> res = cache.getInfo(cityName, stationName);
            if (res == null)
            {
                res = new List<string>();
                res.Add("Station non trouvée");
            }

            return res;
        }*/

        /* public IAsyncResult BeginGetInfo(string stationName, string cityName, AsyncCallback callback, object asyncState)
         {
             StringBuilder res = new StringBuilder();
             request = WebRequest.Create(
             "https://api.jcdecaux.com/vls/v1/stations?contract=" + cityName + "&apiKey=debad70e5907a4c4ff2ccdc05ab06dec199793db");

             stationName = stationName.ToUpper();

             return request.BeginGetRequestStream(callback, asyncState);
         }

         public string EndGetInfo(IAsyncResult r)
         {
             using (WebResponse wr = request.EndGetResponse(r))
             {
                 using (StreamReader sr = new StreamReader(wr.GetResponseStream()))
                 {
                     return sr.ReadToEnd();
                 }
             }
         }*/

        public List<string> GetStations(string cityName)
        {
            return cache.getStations(cityName).Keys.ToList();
        }
    }

    public class Cache
    {
        private MemoryCache cache = MemoryCache.Default;

        private void fillCache(string city)
        {
            if (!cache.Contains(city))
            {
                Dictionary<string, List<string>> stations = new Dictionary<string, List<string>>();
                WebRequest request = WebRequest.Create(
                "https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=debad70e5907a4c4ff2ccdc05ab06dec199793db");

                try
                {
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    var stationString = JArray.Parse(responseFromServer);

                    foreach (JObject item in stationString)
                    {
                        var name = (string)item.SelectToken("name");
                        List<string> infos = new List<string>();

                        infos.Add(item.SelectToken("bike_stands") + " emplacement(s) au total");
                        infos.Add(item.SelectToken("available_bikes") + " vélo(s) disponibles");
                        infos.Add(item.SelectToken("available_bike_stands") + " emplacement(s) disponibles");

                        stations.Add(name, infos);
                    }

                    reader.Close();
                    response.Close();

                    var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, 5, 0) };
                    cache.Add(city, stations, policy);
                }
                catch (WebException wex)
                {

                }
            }
        }


        public Dictionary<string, List<string>> getStations(string city)
        {
            fillCache(city);
            return (Dictionary<string, List<string>>)cache.Get(city);
        }

        public List<string> getInfo(string city, string station)
        {
            fillCache(city);
            Dictionary<string, List<string>> cacheDic = (Dictionary<string, List<string>>)cache.Get(city);

            return cacheDic.FirstOrDefault(t => t.Key.Contains(station.ToUpper())).Value;
        }
    }

}