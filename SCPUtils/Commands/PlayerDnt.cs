﻿using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class PlayerDnt : ICommand
    {

        public string Command { get; } = "scputils_player_dnt";

        public string[] Aliases { get; } = new[] { "pdnt", "dnt", "su_pdnt", "su_playerdnt", "scpu_pdnt", "scpu_playerdnt" };

        public string Description { get; } = "Use this command to forcefully refuse dnt requests!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string target;
            if (!sender.CheckPermission("scputils.dnt"))
            {
                response = "<color=red>您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else
            {
                if (arguments.Count < 1)
                {
                    response = $"<color=yellow>Usage: {Command} <player name/id></color>";
                    return false;
                }
                else
                {
                    target = arguments.Array[1].ToString();
                }
            }
            Player databasePlayer = target.GetDatabasePlayer();

            if (databasePlayer == null)
            {
                response = $"<color=yellow>Player not found on Database or Player is loading data!</color>";
                return false;
            }

            if (databasePlayer.IgnoreDNT == false)
            {
                databasePlayer.IgnoreDNT = true;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "Success, ignore DNT has been enabled!";
            }
            else
            {
                databasePlayer.IgnoreDNT = false;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "Success, ignore DNT has been disabled!";
            }


            return true;
        }
    }
}
