using System;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace NEXIS.Garage
{
    public class CommandGarage : IRocketCommand
    {
        #region Properties

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public bool AllowFromConsole => false;

        public string Name => "garage";

        public string Help => "Save or load a vehicle in your garage. To save. you must be in a vehicle.";

        public List<string> Aliases => new List<string>() { "g" };

        public string Syntax => "/garage [save|load]";

        public List<string> Permissions => new List<string>() { "garage" };

        #endregion

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length > 0)
            {
                switch(command[0])
                {
                    case "save":
                        if (player.IsInVehicle)
                        {
                            Garages plrGarage = Garage.Instance.GarageList.Find(x => x.SteamID == player.CSteamID.ToString());
                            if (plrGarage == null)
                            {
                                UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_error"), Color.red);
                                Console.WriteLine("ERROR! Cannot find garage for online player!");
                                return;
                            }
                            plrGarage.VehicleID = player.CurrentVehicle.id;
                            UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_vehicle_saved"), Color.green);
                        }
                        else
                        {
                            UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_not_in_vehicle"), Color.red);
                        }
                        break;
                    case "load":
                        Garages plrVehicle = Garage.Instance.GarageList.Find(x => x.SteamID == player.CSteamID.ToString());
                        if (plrVehicle == null)
                        {
                            UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_error"), Color.red);
                            Console.WriteLine("ERROR! Cannot find garage for online player!");
                            return;
                        }

                        if (plrVehicle.VehicleID == 0)
                        {
                            UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_no_vehicle"), Color.red);
                            return;
                        }

                        player.GiveVehicle(plrVehicle.VehicleID);

                        if (Garage.Instance.Configuration.Instance.RemoveVehicleWhenLoaded)
                            plrVehicle.VehicleID = 0;

                        UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_vehicle_loaded"), Color.green);
                        break;
                    default:
                        UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_invalid_command"), Color.red);
                        break;
                }
            }
            else
            {
                UnturnedChat.Say(caller, Help, Color.white);
                UnturnedChat.Say(caller, "Syntax: " + Syntax, Color.yellow);
            }
        }
    }
}