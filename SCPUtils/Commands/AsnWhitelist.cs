using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class AsnWhitelist : ICommand
    {
        public string Command { get; } = "scputils_whitelist_asn";

        public string[] Aliases { get; } = new[] { "asnw", "su_wl_asn", "scpu_wl_asn" };

        public string Description { get; } = "Whitelist a player to make him access the server even if ASN is blacklisted!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.whitelist"))
            {
                response = "<color=red> 您需要更高的管理级别才能使用此命令！</color>";
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

                databasePlayer.ASNWhitelisted = true;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "玩家已被列入白名单！";
                return true;
            }
        }
    }
}
