﻿using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class GlobalEdit : ICommand
    {

        public string Command { get; } = "scputils_global_edit";

        public string[] Aliases { get; } = new[] { "gedit", "su_gedit", "su_globaledit", "su_ge", "scpu_gedit", "scpu_globaledit", "scpu_ge" };

        public string Description { get; } = "Remove specified amount of scp games / warns / kick / bans from each player present in DB!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.globaledit"))
            {
                response = "<color=red>您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else
            {
                if (arguments.Count < 4)
                {
                    response = $"<color=yellow>Usage: {Command} <SCPGames to remove> <Suicides to remove> <Kicks to remove> <Bans to remove></color>";
                    return false;
                }
            }

            if (int.TryParse(arguments.Array[1].ToString(), out int scpGamesToRemove) && int.TryParse(arguments.Array[2].ToString(), out int suicidesToRemove) && int.TryParse(arguments.Array[3].ToString(), out int kicksToRemove) && int.TryParse(arguments.Array[4].ToString(), out int bansToRemove))
            {
                foreach (Player databasePlayer in Database.LiteDatabase.GetCollection<Player>().Find(x => x.ScpSuicideCount >= 1))
                {
                    if (databasePlayer.TotalScpGamesPlayed >= scpGamesToRemove)
                    {
                        databasePlayer.TotalScpGamesPlayed -= scpGamesToRemove;
                    }
                    else
                    {
                        databasePlayer.TotalScpGamesPlayed = 0;
                    }

                    if (databasePlayer.ScpSuicideCount >= suicidesToRemove)
                    {
                        databasePlayer.ScpSuicideCount -= suicidesToRemove;
                    }
                    else
                    {
                        databasePlayer.ScpSuicideCount = 0;
                    }

                    if (databasePlayer.TotalScpSuicideKicks >= kicksToRemove)
                    {
                        databasePlayer.TotalScpSuicideKicks -= kicksToRemove;
                    }
                    else
                    {
                        databasePlayer.TotalScpSuicideKicks = 0;
                    }

                    if (databasePlayer.TotalScpSuicideBans >= bansToRemove)
                    {
                        databasePlayer.TotalScpSuicideBans -= bansToRemove;
                    }
                    else
                    {
                        databasePlayer.TotalScpSuicideBans = 0;
                    }

                    Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                }
            }

            else
            {
                response = $"一个或多个参数不是整数，命令用法示例: {Command} 4 2 1 3";
                return false;
            }

            response = $"Success, the following edits have been made globally: Removed: {scpGamesToRemove} SCP Game(s), {suicidesToRemove} Suicide(s), {kicksToRemove} Kick(s), {bansToRemove} Ban(s) to each player!";
            return true;
        }
    }
}
