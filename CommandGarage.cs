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

        public string Syntax => "/garage";

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
                            Garages g = Garage.Instance.GarageList.Find(x => x.SteamID == player.CSteamID.ToString());
                            Console.WriteLine("got player");
                            if (g.Cars.Count > Garage.Instance.Configuration.Instance.MaxVehicles)
                            {
                                UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_max_vehicles_reached"), Color.red);
                                return;
                            }

                            Console.WriteLine("trying to add vehicle...");
                            g.Cars.Add(player.CurrentVehicle.id);
                            Console.WriteLine("added vehicle");
                            UnturnedChat.Say(caller, Garage.Instance.Translations.Instance.Translate("garage_vehicle_saved"), Color.green);
                        }
                        break;
                    case "load":
                        //Garages g = Garage.Instance.GarageList.Find(x => x.Player == player.CSteamID);

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