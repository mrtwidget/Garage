using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Steamworks;

namespace NEXIS.Garage
{
    public class Garages
    {
        public string SteamID { get; set; }
        public ushort VehicleID { get; set; }

        public void Load()
        {
            // create file if not exist
            if (!File.Exists(Garage.Instance.Configuration.Instance.DataDirectory + "Garages.json"))
            {
                File.Create(Garage.Instance.Configuration.Instance.DataDirectory + "Garages.json").Dispose();
                File.WriteAllText(Garage.Instance.Configuration.Instance.DataDirectory + "Garages.json", "[]");
            }

            string file = File.ReadAllText(Garage.Instance.Configuration.Instance.DataDirectory + "Garages.json");
            JArray json = JArray.Parse(file);
            Garage.Instance.GarageList = json.ToObject<List<Garages>>();

            if (Garage.Instance.Configuration.Instance.Debug)
                Console.WriteLine("Player Garages Loaded: " + Garage.Instance.GarageList.Count);
        }

        public void Update()
        {
            // save to file
            string json = JsonConvert.SerializeObject(Garage.Instance.GarageList, Formatting.Indented);
            File.WriteAllText(Garage.Instance.Configuration.Instance.DataDirectory + "Garages.json", json);
        }
    }
}