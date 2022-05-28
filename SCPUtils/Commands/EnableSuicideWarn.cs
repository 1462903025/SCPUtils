using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class EnableSuicideWarn : ICommand
    {
        public string Command { get; } = "scputils_enable_suicide_warns";

        public string[] Aliases { get; } = new[] { "esw", "enable_suicide_warns", "su_esw", "scpu_esw" };

        public string Description { get; } = "Enables again SCP suicide/quit detector";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scputils.warnmanagement"))
            {
                response = "<color=red> 您需要更高的管理级别才能使用此命令！</color>";
                return false;
            }
            else if (!SCPUtils.EventHandlers.TemporarilyDisabledWarns)
            {
                response = "警告已启用";
                return false;
            }
            else if (!ScpUtils.StaticInstance.Config.EnableSCPSuicideAutoWarn)
            {
                response = "自杀/退出警告被服务器配置禁用，如果这是一个错误，请联系服务器所有者！";
                return false;
            }
            EventHandlers.TemporarilyDisabledWarns = false;
            response = "自杀和退出警告已重新启用！";
            return true;
        }
    }
}
