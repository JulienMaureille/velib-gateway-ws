# Velib Gateway WebService
A C# .NET project that provides an intermediary web service between a client and the JCDecaux API (find a bicycle station, ask for informations : number of bicycles availables, number of free slots ...) for the city of Toulouse, France.

There are an IWS + 2 clients : one in console, one with a user interface (WPF).

## Extensions
* Graphical User Interface for the client
* Add a cache in the IWS

## How to...
Get real-time informations about a Velib station :

* **With the command line client**
1. Type 
```
stations
```
To get the list of all stations.

2. Then, type 
```
info
```
And type the name of the station you want some informations (number of bicycles availables, number of free slots)

3. You get the requested information in your terminal.

4. You can retry or quit with 
```
quit
```

* **With the UI Client**
1. Select the city of Toulouse in the left panel
2. Select a station in the right panel
3. You get the requested information in the bottom panel.



