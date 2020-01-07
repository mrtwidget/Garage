using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace NEXIS.Garage
{
    public class GarageConfiguration : IRocketPluginConfiguration
    {
        public bool Debug;
        public string DataDirectory;
        public int MaxVehicles;
        public bool RemoveVehicleWhenLoaded;

        public void LoadDefaults()
        {
            Debug = true;
            DataDirectory = "Plugins/Garage/";
            MaxVehicles = 6;
            RemoveVehicleWhenLoaded = true;
        }
    }
}