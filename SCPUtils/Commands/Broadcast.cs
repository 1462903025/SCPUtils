using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class Broadcast : ICommand
    {
        public string Command { get; } = "scputils_broadcast";

        public string[] Aliases { get; } = new[] { "sbc", "su_bc", "scpu_bc" };

        public string Description { get; } = "Allows to send custom broadcastes";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.broadcast"))
            {
                response = "<color=red>您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }

            else if (arguments.Count < 2)
            {
                response = $"<color=yellow>Usage: {Command} <hint(h)/broadcast(bc)> <id> <duration (optional, if empty will be used the default one set for this broadcast)></color>";
                return false;
            }
            else
            {
                var databaseBroadcast = GetBroadcast.FindBroadcast(arguments.Array[2]);

                if (databaseBroadcast == null)
                {
                    response = "无效的广播ID！";
                    return false;
                }
                int duration = databaseBroadcast.Seconds;
                if (arguments.Count == 3)
                {
                    if (int.TryParse(arguments.Array[3].ToString(), out duration)) { }
                    else
                    {
                        response = "广播持续时间必须为整数";
                        return false;
                    }
                }

                switch (arguments.Array[1].ToString())
                {
                    case "broadcast":
                    case "bc":
                        Map.Broadcast((ushort)duration, databaseBroadcast.Text);
                        response = "向所有玩家发送广播！";
                        break;
                    case "hint":
                    case "h":
                        Map.ShowHint(databaseBroadcast.Text, duration);
                        response = "向所有玩家发送提示！";
                        break;
                    default:
                        response = "Invalid argument, you should use broadcast/bc or hint/h.";
                        break;
                }
            }

            return true;
        }
    }
}
