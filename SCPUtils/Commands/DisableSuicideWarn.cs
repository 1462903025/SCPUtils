using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class DisableSuicideWarn : ICommand
    {
        public string Command { get; } = "scputils_disable_suicide_warns";
        public string[] Aliases { get; } = new[] { "dsw", "disable_suicide_warns", "su_dsw", "scpu_dsw" };

        public string Description { get; } = "Temporarily disable SCP suicide/quit detector for the duration of the round";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.warnmanagement"))
            {
                response = "<color=red>您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else if (SCPUtils.EventHandlers.TemporarilyDisabledWarns)
            {
                response = "警告已禁用";
                return false;
            }
            else if (!ScpUtils.StaticInstance.Config.EnableSCPSuicideAutoWarn)
            {
                response = "服务器配置已禁用自杀/退出警告！";
                return false;
            }
            EventHandlers.TemporarilyDisabledWarns = true;
            response = "自杀和退出警告在这一轮中被禁用！";
            return true;
        }
    }
}
