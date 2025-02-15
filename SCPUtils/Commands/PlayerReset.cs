﻿using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Reset : ICommand
    {

        public string Command { get; } = "scputils_player_reset";

        public string[] Aliases { get; } = new[] { "pr", "su_pr", "su_playerreset", "su_playereset", "scpu_pr", "scpu_playerreset", "scpu_playereset" };

        public string Description { get; } = "Reset player data (Quits,Ban,Kicks,Nickname,Badge etc, everything)!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (!sender.CheckPermission("scputils.playerreset"))
            {
                response = "<color=red> 您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else if (arguments.Count < 1)
            {
                response = $"<color=red>Usage: {Command} <player name/id></color>";
                return false;
            }
            else
            {
                string target = arguments.Array[1].ToString();

                Player databasePlayer = target.GetDatabasePlayer();

                if (databasePlayer == null)
                {
                    response = "<color=yellow>Player not found on Database or Player is loading data!</color>";
                    return false;
                }

                databasePlayer.Reset();
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "Player has been reset!";

                return true;
            }
        }
    }
}

