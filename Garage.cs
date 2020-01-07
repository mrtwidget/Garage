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
                    {"garage_vehicle_saved", "This vehicle has been parked in your garage!"},
                    {"garage_error", "Something went wrong with your Garage! Report this to the admins!"},
                    {"garage_vehicle_loaded", "Spawned a vehicle from your garage!"},
                    {"garage_no_vehicle", "You don't have a vehicle parked in your garage"},
                    {"garage_not_in_vehicle", "You must be in a vehicle to park it in your garage!"}
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
                newGarage.VehicleID = 0;
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