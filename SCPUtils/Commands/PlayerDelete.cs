﻿using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class PlayerDelete : ICommand
    {

        public string Command { get; } = "scputils_player_delete";

        public string[] Aliases { get; } = new[] { "pdelete", "su_pdelete", "su_playerdelete", "scpu_pdelete", "scpu_playerdelete" };

        public string Description { get; } = "Delete a player (and all the player data) from the database, action is irreversible!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (!sender.CheckPermission("scputils.playerdelete"))
            {
                response = "<color=red> 您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else if (arguments.Count < 1)
            {
                response = $"<color=red>Usage: {Command} <player name/id> (You will delete the player from the database)</color>";
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
                Database.LiteDatabase.GetCollection<Player>().Delete(databasePlayer.Id);
                response = $"{target} 已从数据库中删除!";

                return true;
            }
        }
    }
}

