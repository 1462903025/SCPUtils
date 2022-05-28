using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Text;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class PermissionsView : ICommand
    {
        public string Command { get; } = "scputils_permissions_view";

        public string[] Aliases { get; } = new[] { "pmv", "permissionsview", "su_pmv", "scpu_pmview", "scpu_permissionsview" };

        public string Description { get; } = "Show your scputils permissions";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            StringBuilder message = new StringBuilder($"Your permissions (Granted):\n");
            if (sender.CheckPermission("scputils.help")) message.AppendLine("您可能会看到SCPUtils Admin命令！");
            if (sender.CheckPermission("scputils.playerinfo")) message.AppendLine("您可以看到其他玩家的玩家信息！");
            if (sender.CheckPermission("scputils.playerlist")) message.AppendLine("您可以看到玩家列表！");
            if (sender.CheckPermission("scputils.playerreset")) message.AppendLine("您可以重置玩家！");
            if (sender.CheckPermission("scputils.playerresetpreferences")) message.AppendLine("您可以重置玩家首选项！");
            if (sender.CheckPermission("scputils.playersetcolor")) message.AppendLine("您可以设置其他玩家的 badge 颜色！");
            if (sender.CheckPermission("scputils.playersetname")) message.AppendLine("您可以设置其他玩家的昵称！");
            if (sender.CheckPermission("scputils.handlebadges")) message.AppendLine("您可以临时添加或删除 badge ！");
            if (sender.CheckPermission("scputils.playtime")) message.AppendLine("您可能会看到其他玩家的游戏时间！");
            if (sender.CheckPermission("scputils.whitelist")) message.AppendLine("您可以从ASN白名单中添加/删除玩家！");
            if (sender.CheckPermission("scputils.stafflist")) message.AppendLine("您可以在线查看员工列表！");
            if (sender.CheckPermission("scputils.warnmanagement")) message.AppendLine("您可以启用或禁用自动警告！");
            if (sender.CheckPermission("scputils.globaledit")) message.AppendLine("您可以在全部范围内编辑玩家！");
            if (sender.CheckPermission("scputils.playeredit")) message.AppendLine("您可以编辑单人游戏！");
            if (sender.CheckPermission("scputils.playerdelete")) message.AppendLine("您可以从数据库中删除玩家！");
            if (sender.CheckPermission("scputils.keep")) message.AppendLine("您可以强制插件保留用户首选项，即使没有权限！");
            if (sender.CheckPermission("scputils.moderatecommands")) message.AppendLine("您可以调节SCPUtils命令！");
            if (sender.CheckPermission("scputils.dnt")) message.AppendLine("您可以强制忽略特定玩家的DNT请求");
            if (sender.CheckPermission("scputils.showwarns")) message.AppendLine("您可能会看到详细的自动警告信息！");
            if (sender.CheckPermission("scputils.unwarn")) message.AppendLine("你可以移除玩家的自动制裁！");
            if (sender.CheckPermission("scputils.broadcast")) message.AppendLine("您可以使用SCPUtils广播功能！");
            if (sender.CheckPermission("scputils.changenickname")) message.AppendLine("您可以更改自己的昵称！");
            if (sender.CheckPermission("scputils.changecolor")) message.AppendLine("你可以改变自己的颜色！");
            if (sender.CheckPermission("scputils.badgevisibility")) message.AppendLine("您可以更改自己的 badge 可见性！");
            if (sender.CheckPermission("scputils.ownplaytime")) message.AppendLine("你可以看到自己的游戏时间！");
            if (sender.CheckPermission("scputils.bypassnickrestriction")) message.AppendLine("您可以绕过昵称限制！");
            if (sender.CheckPermission("scputils.roundinfo.execute")) message.AppendLine("您可以执行roundinfo命令！");
            if (sender.CheckPermission("scputils.roundinfo.roundtime")) message.AppendLine("你可以看到roundtime！");
            if (sender.CheckPermission("scputils.roundinfo.tickets")) message.AppendLine("你可以看到所有球队的门票！");
            if (sender.CheckPermission("scputils.roundinfo.nextrespawnteam")) message.AppendLine("您可能会看到下一个重生信息！");
            if (sender.CheckPermission("scputils.roundinfo.respawncount")) message.AppendLine("您可能会看到上次混沌/mtf重生信息！");
            if (sender.CheckPermission("scputils.roundinfo.lastrespawn")) message.AppendLine("你可以看到混沌/mtf何时重生！");
            if (sender.CheckPermission("scputils.onlinelist.basic")) message.AppendLine("您可以执行在线信息命令！");
            if (sender.CheckPermission("scputils.onlinelist.userid")) message.AppendLine("您可能会看到在线信息用户ID！");
            if (sender.CheckPermission("scputils.onlinelist.badge")) message.AppendLine("您可以看到在线信息徽章！");
            if (sender.CheckPermission("scputils.onlinelist.role")) message.AppendLine("您可能会看到在线信息角色！");
            if (sender.CheckPermission("scputils.onlinelist.health")) message.AppendLine("您可能会看到联机信息健康！");
            if (sender.CheckPermission("scputils.onlinelist.flags")) message.AppendLine("您可能会看到联机信息标志！");
            if (sender.CheckPermission("scputils_speak.scp049") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp049)) message.AppendLine("You MAY speak with SCP-049!");
            if (sender.CheckPermission("scputils_speak.scp0492") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp0492)) message.AppendLine("You MAY speak with SCP-0492!");
            if (sender.CheckPermission("scputils_speak.scp079") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp079)) message.AppendLine("You MAY speak with SCP-079!");
            if (sender.CheckPermission("scputils_speak.scp096") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp096)) message.AppendLine("You MAY speak with SCP-096!");
            if (sender.CheckPermission("scputils_speak.scp106") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp106)) message.AppendLine("You MAY speak with SCP-106!");
            if (sender.CheckPermission("scputils_speak.scp173") || ScpUtils.StaticInstance.Config.AllowedScps.Contains(RoleType.Scp173)) message.AppendLine("You MAY speak with SCP-173!");
            message.AppendLine("Permissions not listed here means they are denied, more info about permissions on github.com/terminator-97/SCPUtils");


            response = message.ToString();
            return true;
        }

    }
}
