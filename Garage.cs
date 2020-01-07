using System;
using System.Collections.Generic;
using Rocket.Unturned;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Rocket.API;
using Rocket.API.Collections;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using Steamworks;

namespace NEXIS.Garage
{
    public class Garage : RocketPlugin<GarageConfiguration>
    {
        #region Fields

        public static Garage Instance;
        public Garages Garages;
        public List<Garages> GarageList;

        #endregion

        #region Overrides

        protected override void Load()
        {
            Instance = this;
            Garages = new Garages();
            Garages.Load();

            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;

            Logger.Log("Loaded!", ConsoleColor.DarkGreen);
        }

        protected override void Unload()
        {
            Garages.Update();

            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;

            Logger.Log("Unloaded!", ConsoleColor.Yellow);
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList() {
                    {"garage_disabled", "Garage is currently unavailable"},
                    {"garage_invalid_command", "Invalid command! Type /garage to view correct syntax!"},
                    {"garage_vehicle_saved", "Your vehicle has been saved to your garage!"},
                    {"garage_max_vehicles_reached", "You cannot save any more vehicles to your garage!"}
                };
            }
        }

        #endregion

        #region Events

        public void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            Garages g = GarageList.Find(x => x.SteamID == player.CSteamID.ToString());
            if (g == null)
            {
                var newGarage = new Garages();
                newGarage.SteamID = player.CSteamID.ToString();
                GarageList.Add(newGarage);
            }
        }

        public void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {

        }

        #endregion

        public void FixedUpdate()
        {
            if (Instance.State != PluginState.Loaded) return;
        }
    }
}