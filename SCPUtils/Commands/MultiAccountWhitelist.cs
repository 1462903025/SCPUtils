using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class MultiAccountWhitelist : ICommand
    {
        public string Command { get; } = "scputils_multiaccount_whitelist";

        public string[] Aliases { get; } = new[] { "mawl" };

        public string Description { get; } = "Whitelist/Unwhitelist a player to make him being ignored/detected by multiaccount system!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.whitelistma"))
            {
                response = "<color=red>您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else if (arguments.Count < 1)
            {
                response = $"<color=yellow>Usage: {Command} <player name/id></color>";
                return false;
            }

            else
            {
                string target = arguments.Array[1];
                Player databasePlayer = target.GetDatabasePlayer();

                if (databasePlayer == null)
                {
                    response = "<color=yellow>在数据库中找不到玩家，或者玩家正在加载数据！</color>";
                    return false;
                }

                databasePlayer.MultiAccountWhiteList = !databasePlayer.MultiAccountWhiteList;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "成功";
                return true;
            }
        }
    }
}
