using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace NEXIS.Garage
{
    public class GarageConfiguration : IRocketPluginConfiguration
    {
        public bool Debug;

        public void LoadDefaults()
        {
            Debug = true;
        }
    }
}