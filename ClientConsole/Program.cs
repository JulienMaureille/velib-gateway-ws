using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iws;


namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue sur le WebService des Velibs de Toulouse !");
            Console.WriteLine("(help pour obtenir de l'aide)");
            while (true)
            {
                Service1 iws = new Service1();
                string choix = Console.ReadLine();

                if(choix == "help")
                {
                    Console.WriteLine("Liste des commandes :");
                    Console.WriteLine("- stations : obtenir la liste des stations");
                    Console.WriteLine("- info : obtenir les informations d'une station");
                    Console.WriteLine("- quit / exit : quitter le programme");
                }
                else if( choix == "quit" || choix == "exit")
                {
                    Environment.Exit(0);
                }
                else if (choix == "info")
                {
                    Console.Write("Nom de la station : ");
                    string station = Console.ReadLine();
                    
                    Console.WriteLine("-------------------");
                    foreach (string s in iws.GetInfo(station, "toulouse"))
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("-------------------");
                }
                else if (choix == "stations")
                {
                    Console.WriteLine("Liste des stations : ");
                    Console.WriteLine("-------------------");
                    foreach(string s in iws.GetStations("toulouse"))
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("-------------------");
                }
                else
                {
                    Console.WriteLine("Commande inconnue");
                }


               
            }
        }
    }
}
