using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class AsnUnWhitelist : ICommand
    {
        public string Command { get; } = "scputils_unwhitelist_asn";

        public string[] Aliases { get; } = new[] { "asnuw", "su_uwl_asn", "scpu_uwl_asn" };

        public string Description { get; } = "Un-Whitelist a player to disallow him access the server even if ASN is blacklisted!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.whitelist"))
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

                databasePlayer.ASNWhitelisted = false;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = "玩家已从白名单中删除！";
                return true;
            }
        }
    }
}
