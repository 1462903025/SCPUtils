using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using DamageTypes = Exiled.API.Enums.DamageType;
using Log = Exiled.API.Features.Log;
using ZoneType = Exiled.API.Enums.ZoneType;

namespace SCPUtils
{
    public class Configs : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("scp自杀或退出应该被警告吗？")]
        public bool EnableSCPSuicideAutoWarn { get; private set; } = true;

        [Description("如果只剩下一个玩家，是否应该自动重新启动回合？")]
        public bool EnableRoundRestartCheck { get; private set; } = true;

        [Description("作为SCP的退出是否被视为应警告的违规行为？")]
        public bool QuitEqualsSuicide { get; private set; } = true;

        [Description("SCP在达到一定阈值后是否应该因退出或自杀而被踢？")]
        public bool AutoKickOnSCPSuicide { get; private set; } = true;

        [Description("SCP在退出或自杀达到一定阈值后是否应该被封禁？")]
        public bool EnableSCPSuicideAutoBan { get; private set; } = false;

        [Description("是否应该禁止SCP在某个阈值后在X轮再次玩SCP（049-2除外）？如果这设置为true，则必须将EnableSCPsuidicateAutoban设置为false")]
        public bool EnableSCPSuicideSoftBan { get; private set; } = true;

        [Description("作为SCP，每次退出/自杀封禁后，封禁持续时间是否应该增加？")]
        public bool MultiplyBanDurationEachBan { get; private set; } = true;

        [Description("badge过期后是否应重置玩家首选项？")]
        public bool ResetPreferencedOnBadgeExpire { get; private set; } = true;

        [Description("被封禁的玩家被自动踢吗？")]
        public bool AutoKickBannedNames { get; private set; } = true;

        [Description("教程人员是否应视为SCP？如果是真的，如果启用，他们将收到自杀/退出警告")]
        public bool AreTutorialsSCP { get; private set; } = false;

        [Description("scp在退出或自杀被ban，自动踢出时管理是否收到广播")]
        public bool BroadcastSanctions { get; private set; } = true;

        [Description("SCP自杀或退出，管理是否收到广播提醒？")]
        public bool BroadcastWarns { get; private set; } = false;

        [Description("如果加入时的用户没有执行命令的权限，是否应该重置昵称？您可以使用 scputils_preference_persist 命令绕过它")]
        public bool KeepNameWithoutPermission { get; private set; } = false;

        [Description("如果加入时的用户没有执行命令的权限，是否应该重置颜色？您可以使用 scputils_preference_persist 命令绕过它")]
        public bool KeepColorWithoutPermission { get; private set; } = false;

        [Description("如果加入时的用户没有执行命令的权限，是否应该重置徽章可见性？您可以使用 scputils_preference_persist 命令绕过它")]
        public bool KeepBadgeVisibilityWithoutPermission { get; private set; } = false;

        [Description("096的通知目标功能是否已启用？")]
        public bool Scp096TargetNotifyEnabled { get; private set; } = true;

        [Description("通知最后一个活着的玩家？")]
        public bool NotifyLastPlayerAlive { get; private set; } = true;

        [Description("是否忽略DNT请求？")]
        public bool IgnoreDntRequests { get; private set; } = false;

        [Description("是否启用自动重启模块？")]
        public bool EnableAutoRestart { get; private set; } = false;

        [Description("Automute player if evasion is detected?")]
        public bool AutoMute { get; private set; } = false;

        [Description("如果是多帐户，是否应显示广播？")]
        public bool MultiAccountBroadcast { get; private set; } = false;

        [Description("作为SCP自杀的自动警告消息")]
        public Exiled.API.Features.Broadcast SuicideWarnMessage { get; private set; } = new Exiled.API.Features.Broadcast("<color=red>WARN:\nAs per server rules SCP's suicide is an offence, doing it too much will result in a ban!</color>", 30, true, Broadcast.BroadcastFlags.Normal);

        [Description("欢迎消息（如果启用）")]
        public Exiled.API.Features.Broadcast WelcomeMessage { get; private set; } = new Exiled.API.Features.Broadcast("<color=green>Welcome to the server %player%!</color>", 12, true, Broadcast.BroadcastFlags.Normal);

        [Description("净化信息（如果启用）")]
        public Exiled.API.Features.Broadcast DecontaminationMessage { get; private set; } = new Exiled.API.Features.Broadcast("<color=yellow>Decontamination has started</color>", 12, false, Broadcast.BroadcastFlags.Normal);

        [Description("自杀自动踢出原因（如果启用）")]
        public string SuicideKickMessage { get; private set; } = "Suicide as SCP";

        [Description("使用受限昵称的自动提示消息")]
        public string AutoKickBannedNameMessage { get; private set; } = "您正在使用受限昵称或与受限昵称太相似，请更改它";

        [Description("自杀自动封禁原因（如果启用）")]
        public string AutoBanMessage { get; private set; } = "Exceeded SCP suicide limit";

        [Description("玩家未被授权使用此命令时的消息")]
        public string UnauthorizedNickNameChange { get; private set; } = "<color=red>权限被拒绝</color>";

        [Description("玩家未被授权使用此命令时的消息")]
        public string UnauthorizedColorChange { get; private set; } = "<color=red>权限被拒绝</color>";

        [Description("玩家未被授权使用此命令时的消息")]
        public string UnauthorizedBadgeChangeVisibility { get; private set; } = "<color=red>权限被拒绝</color>";

        [Description("如果玩家尝试将其昵称更改为受限昵称，则显示消息")]
        public string InvalidNicknameText { get; private set; } = "此昵称已被服主限制，请使用其他昵称！";

        [Description("数据库名称，仅在运行多个服务器时更改")]
        public string DatabaseName { get; private set; } = "SCPUtils";

        [Description("应将数据库存储在哪个文件夹中？")]
        public string DatabaseFolder { get; private set; } = "EXILED";

        [Description("Discord webhook url for mute evasion reports")]
        public string WebhookUrl { get; private set; } = "None";

        [Description("Discord webhook bot nickname")]
        public string WebhookNickname { get; private set; } = "The Frontman";

        [Description("SCP死亡时应显示哪个广播？")]
        public Exiled.API.Features.Broadcast ScpDeathMessage { get; private set; } = new Exiled.API.Features.Broadcast("<color=blue>SCP %playername% (%scpname%) was killed by %killername%. Cause of death: %reason%</color>", 12, true, Broadcast.BroadcastFlags.Normal);

        [Description("SCP死亡时应显示哪个广播？")]
        public Exiled.API.Features.Broadcast ScpSuicideMessage { get; private set; } = new Exiled.API.Features.Broadcast("<color=blue>SCP %playername% (%scpname%) has killed by themselves. Cause of death: %reason%</color>", 12, true, Broadcast.BroadcastFlags.Normal);

        [Description("如果服务器中只有一个玩家，则自动重启时间（如果启用）")]
        public ushort AutoRestartTime { get; private set; } = 15;

        [Description("SCP-096目标消息持续时间（如果启用）")]
        public ushort Scp096TargetMessageDuration { get; private set; } = 12;

        [Description("最后一个玩家活着消息持续时间（如果启用）")]
        public ushort LastPlayerAliveMessageDuration { get; private set; } = 12;

        [Description("通过 scputils_broadcast 命令发送的广播的默认持续时间")]
        public ushort BroadcastDuration { get; private set; } = 12;

        [Description("通过 scputils_broadcast 命令发送提示的默认持续时间")]
        public ushort HintsDuration { get; private set; } = 12;

        [Description("在忽略SCP自杀/退出百分比的情况下，玩家可能不会收到任何踢出或封禁之前，自杀的最小数量是多少？（如果启用）")]
        public int ScpSuicideTollerance { get; private set; } = 5;

        [Description("SCP自杀/退出基本封禁持续时间（如果启用）")]
        public int AutoBanDuration { get; private set; } = 15;

        [Description("SCP自杀/退出自动封禁轮次的基数（如果启用）")]
        public int AutoBanRoundsCount { get; private set; } = 2;

        [Description("使用“更改名称”命令时，昵称的最大长度是多少？")]
        public int NicknameMaxLength { get; private set; } = 32;

        [Description("如果079触发电网多少秒，玩家就不会收到自杀警告？（对于大多数服务器，2个就足够了）")]
        public int Scp079TeslaEventWait { get; private set; } = 2;

        [Description("玩家在被禁赛前需要多少SCP的退出/自杀百分比？（您可以在设置中添加tollerence）")]
        public float AutoBanThreshold { get; private set; } = 30.5f;

        [Description("Which quit / suicide percentage as SCP a player require before getting kicked? (You can add tollerence in settings)")]
        public float AutoKickThreshold { get; private set; } = 15.5f;

        [Description("哪些颜色受限制 .scputils_change_color color 命令？")]
        public List<string> RestrictedRoleColors { get; private set; } = new List<string>() { "Color1", "Color2" };

        [Description("限制哪些昵称 .scputils_change_nickname 昵称命令？")]
        public List<string> BannedNickNames { get; private set; } = new List<string>() { "@everyone", "@here", "Admin" };

        [Description("哪些ASN应被列入黑名单？从列入黑名单的ASN连接的玩家应通过 scputils_whitelist_asn  ASN命令被列入白名单（50889是geforce now ASN）")]
        public List<string> ASNBlacklist { get; private set; } = new List<string>() { "50889" };

        [Description("在多帐户检测器中应忽略哪些ASN？（50889是geforce现在的ASN）")]
        public List<string> ASNWhiteslistMultiAccount { get; private set; } = new List<string>() { "50889" };

        [Description("从列入黑名单的ASN连接时，未列入白名单的玩家应收到哪些消息")]
        public string AsnKickMessage { get; private set; } = "您连接的ASN已从此服务器列入黑名单，请与服务器工作人员联系以请求被列入白名单";

        [Description("应向成为SCP-096目标的人员显示哪些信息？")]
        public string Scp096TargetNotifyText { get; private set; } = "<color=red>注意:</color>\n<color=purple>你成了SCP-096的目标！</color>";

        [Description("哪一条信息应该显示给最后一个活着的玩家？")]
        public string LastPlayerAliveNotificationText { get; private set; } = "<color=red>注意:</color>\n<color=purple>你是队伍最后一个活着的玩家！</color>";

        [Description("当玩家重新加入时，应该为掉线警告显示哪条消息？")]
        public Exiled.API.Features.Broadcast OfflineWarnNotification { get; private set; } = new Exiled.API.Features.Broadcast("<color=red>发布警告通知:</color>\n<color=yellow>您最近被警告退出游戏中的SCP，继续这种行为可能会导致封禁！</color>", 30, true, Broadcast.BroadcastFlags.Normal);

        [Description("当玩家被禁止玩SCP时，应该向他显示哪些信息？")]
        public Exiled.API.Features.Broadcast RoundBanNotification { get; private set; } = new Exiled.API.Features.Broadcast("<color=red>您已被禁止警告:</color>\n<color=yellow><size=27>您已被禁止玩SCP。由于您过去的违规行为，您被排除在SCP %roundnumber% 回合的比赛之外！</size></color>", 30, true, Broadcast.BroadcastFlags.Normal);

        [Description("当一个玩家被禁止并被另一个玩家替换时，当他以SCP身份生成时，应该向他显示哪条消息？")]
        public Exiled.API.Features.Broadcast RoundBanSpawnNotification { get; private set; } = new Exiled.API.Features.Broadcast("<color=red>您被SCP禁止：</color>\n<color=yellow><size=27>您已被删除为SCP，因为您当前被SCP禁止！您必须更换其他 %roundnumber% 次才能再次玩SCP！</size></color>", 30, true, Broadcast.BroadcastFlags.Normal);

        [Description("服务器应该在一天中的哪个时间自动重新启动？")]
        public string AutoRestartTimeTask { get; private set; } = "1:35:0";


        [Description("哪些文本应显示在discord嵌入之外？")]
        public string ExtraText { get; private set; } = "@everyone";

        [Description("插件应该忽略哪个组的DNT标志？")]
        public List<string> DntIgnoreList { get; private set; } = new List<string>() { "testusergroup1", "testusergroup2" };

        [Description("允许哪一类查看MTF和下次重生信息")]
        public List<Team> AllowedMtfInfoTeam { get; private set; } = new List<Team>() { Team.MTF, Team.RSC, Team.RIP };

        [Description("允许哪一类查看混沌信息和下次重生信息")]
        public List<Team> AllowedChaosInfoTeam { get; private set; } = new List<Team>() { Team.CDP, Team.CHI, Team.RIP };

        /*

          public Dictionary<Team, ImmunityPlayers> ImmunityPlayers { get; private set; } = new Dictionary<Team, ImmunityPlayers>()
          {
              //Class-D example dictionary config
              {
                Team.CDP, //key
                new ImmunityPlayers
                {                  
                   Attacker = new List<Team>() { Team.RSC, Team.MTF }, //Attacker List
                   ShouldBeCuffed = true, //Cuffed
                   ImmunityZones = new List<ZoneType>() { ZoneType.Entrance, ZoneType.Surface } //Zone List                  
                }
              },

              //Rip example dictionary config
              {
                Team.RIP, //key
                new ImmunityPlayers
                {
                    Attacker = new List<Team>() { Team.TUT }, //Attacker List
                    ShouldBeCuffed = false, //Cuffed
                    ImmunityZones = new List<ZoneType>() { ZoneType.Entrance, ZoneType.Surface, ZoneType.LightContainment, ZoneType.HeavyContainment, ZoneType.Unspecified } //Zone list
                }
              }
          };

          */

        [Description("您必须将要保护的团队作为关键团队添加到目标中，将敌人团队作为价值添加到列表中，在github文档中您可以看到所有团队。")]

        public Dictionary<Team, List<Team>> CuffedImmunityPlayers { get; private set; } = new Dictionary<Team, List<Team>>();


        [Description("指示是否应将受保护的团队铐起来以获得保护，如果不添加团队，则无论如何都将获得保护")]

        public List<Team> CuffedProtectedTeams { get; private set; } = new List<Team>();

        [Description("指示受保护团队受保护的区域，区域列表：Surface, Entrance, HeavyContainment, LightContainment, Unspecified")]

        public Dictionary<Team, List<ZoneType>> CuffedSafeZones { get; private set; } = new Dictionary<Team, List<ZoneType>>();

        [Description("哪些SCP用户可以在没有任何 badge 的情况下使用V与人交谈（删除不允许的SCP）？（这将绕过权限检查，因此无论级别如何，每个人都可以与这些SCP对话）")]

        public List<RoleType> AllowedScps { get; private set; } = new List<RoleType>() { RoleType.Scp049, RoleType.Scp0492, RoleType.Scp079, RoleType.Scp096, RoleType.Scp106, RoleType.Scp173, RoleType.Scp93953, RoleType.Scp93989 };

        [Description("损坏类型的翻译")]
        public Dictionary<string, string> DamageTypesTranslations { get; private set; } = new Dictionary<string, string>() { { DamageTypes.Explosion.ToString().ToUpper(), DamageTypes.Explosion.ToString().ToUpper() }, { DamageTypes.Asphyxiation.ToString().ToUpper(), DamageTypes.Asphyxiation.ToString().ToUpper() }, { DamageTypes.Bleeding.ToString().ToUpper(), DamageTypes.Bleeding.ToString().ToUpper() },
           { DamageTypes.Crushed.ToString().ToUpper(), DamageTypes.Crushed.ToString().ToUpper() }, { DamageTypes.Decontamination.ToString().ToUpper(), DamageTypes.Decontamination.ToString().ToUpper() }, { DamageTypes.Falldown.ToString().ToUpper(), DamageTypes.Falldown.ToString().ToUpper() }, { DamageTypes.FemurBreaker.ToString().ToUpper(), DamageTypes.FemurBreaker.ToString().ToUpper() }, { DamageTypes.FriendlyFireDetector.ToString().ToUpper(), DamageTypes.FriendlyFireDetector.ToString().ToUpper()},
           { DamageTypes.MicroHid.ToString().ToUpper(), DamageTypes.MicroHid.ToString().ToUpper() }, { DamageTypes.PocketDimension.ToString().ToUpper(), DamageTypes.PocketDimension.ToString().ToUpper() }, { DamageTypes.Poison.ToString().ToUpper(), DamageTypes.Poison.ToString().ToUpper() },
           { DamageTypes.Recontainment.ToString().ToUpper(), DamageTypes.Recontainment.ToString().ToUpper() }, { DamageTypes.Scp.ToString().ToUpper(), DamageTypes.Scp.ToString().ToUpper() }, { DamageTypes.Scp018.ToString().ToUpper(), DamageTypes.Scp018.ToString().ToUpper() },
           { DamageTypes.Scp207.ToString().ToUpper(), DamageTypes.Scp207.ToString().ToUpper() }, { DamageTypes.SeveredHands.ToString().ToUpper(), DamageTypes.SeveredHands.ToString().ToUpper() }, { DamageTypes.Tesla.ToString().ToUpper(), DamageTypes.Tesla.ToString().ToUpper() }, { DamageTypes.Unknown.ToString().ToUpper(), DamageTypes.Unknown.ToString().ToUpper() },
           { DamageTypes.Warhead.ToString().ToUpper(), DamageTypes.Warhead.ToString().ToUpper() }, { DamageTypes.Firearm.ToString().ToUpper(), DamageTypes.Firearm.ToString().ToUpper() } };


        [Description("unwarn 命令的命令名")]
        public string UnwarnCommand { get; set; } = "scputils_player_unwarn";

        [Description("unwarn 命令的别名")]
        public string[] UnwarnCommandAliases { get; set; } = new[] { "unwarn", "sunwarn", "su_player_unw", "su_punw", "su_puw", "scpu_player_unw", "scpu_punw", "scpu_puw" };
        [Description("当玩家以大于1个帐户进入时，广播发送给所有在线工作人员")]

        public Exiled.API.Features.Broadcast AlertStaffBroadcastMultiAccount { get; private set; } = new Exiled.API.Features.Broadcast(
            "<size=40><color=red>Alert</color></size>\n<size=35>Player <color=yellow>{player}</color> has entered with <color=yellow>{accountNumber}</color> accounts</size>\n<size=30>Check console pressing <color=yellow>ò</color></size>",
            10);

        [Description("Broadcast to send to all online staff when player change IP")]
        public Exiled.API.Features.Broadcast AlertStaffBroadcastChangeIP { get; private set; } = new Exiled.API.Features.Broadcast(
            "<size=40><color=red>Alert</color></size>\n<size=35>Player <color=yellow>{player}</color> has changed IP. <color=yellow>{oldIP}</color> to <color=yellow>{newIP}</color></size>\n<size=35>Check console pressing <color=yellow>ò</color></size>",
            10);


        public void ConfigValidator()
        {
            if (ScpSuicideTollerance < 0)
            {
                Log.Warn("Invalid config scputils_scp_suicide_tollerance, loading dafault one!");
                ScpSuicideTollerance = 5;

            }
            if (AutoKickThreshold >= AutoBanThreshold)
            {
                Log.Warn("Invalid config scputils_auto_kick_threshold OR scputils_auto_ban_threshold, loading dafault one!");
                AutoBanThreshold = 30.5f;

            }
            if (AutoRestartTime < 0)
            {
                Log.Warn("Invalid config scputils_auto_restart_time, loading dafault one!");
                AutoRestartTime = 15;
            }
            if (Scp079TeslaEventWait < 0)
            {
                Log.Warn("Invalid config scputils_scp_079_tesla_event_wait, loading dafault one!");
                Scp079TeslaEventWait = 2;
            }
            if (IgnoreDntRequests)
            {
                Log.Warn("You have set in server configs to ignore Do Not Track requests but that's a violation on Verified Server Rules (if your server is verified) and could cause punishement such as delist [Rule 8.11]");
            }

            if (EnableSCPSuicideSoftBan)
            {
                EnableSCPSuicideAutoBan = false;
            }

            if (!IsEnabled)
            {
                Log.Warn("You disabled the plugin in server configs!");
            }
        }
    }
}
