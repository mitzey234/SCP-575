using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using Exiled.Loader;

namespace SCP_575
{
    public class Config : IConfig
    {
        [Description("Whether or not randomly timed events should occur. If false, all events will be at the same interval apart.")]
        public bool RandomEvents { get; set; } = true;
        [Description("Whether or not tesla gates should be disabled during blackouts.")]
        public bool DisableTeslas { get; set; } = true;
        [Description("The delay before the first event of each round, in seconds.")]
        public int InitialDelay { get; set; } = 300;
        [Description("The minimum number of seconds a blackout event can last.")]
        public int DurationMin { get; set; } = 30;
        [Description("The maximum number of seconds a blackout event can last. If RandomEvents is disabled, this will be the duration for every event.")]
        public int DurationMax { get; set; } = 90;
        [Description("The minimum amount of seconds between each event.")]
        public int DelayMin { get; set; } = 180;
        [Description("The maximum amount of seconds between each event. If RandomEvents is disabled, this will be the delay between every event.")]
        public int DelayMax { get; set; } = 500;
        [Description("The percentage chance that SCP-575 events will occur in any particular round.")]
        public int SpawnChance { get; set; } = 45;
        [Description("Whether or not people in dark rooms should take damage if they have no light source in their hand.")]
        public bool EnableKeter { get; set; } = true;
        [Description("Blackout Affected Zones")]
        public HashSet<ZoneType> AffectedZones { get; set; } = new HashSet<ZoneType>
        {
            ZoneType.Surface,
            ZoneType.Entrance,
            ZoneType.HeavyContainment,
            ZoneType.LightContainment
        };
        [Description("Whether or not SCP-575's \"roar\" should happen after a blackout starts.")]
        public bool Voice { get; set; } = true;
        [Description("How much damage per 5 seconds should be inflicted if EnableKeter is set to true.")]
        public float KeterDamage { get; set; } = 10f;
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Name displayed in player's death information.")]
        public string KilledBy { get; set; } = "SCP-575";
        [Description("Message said by Cassie.")]
        public string CassieMessageStart { get; set; } = "facility power system failure in 3 . 2 . 1 .";
        public string CassieKeter { get; set; } = "pitch_0.15 .g7";
        public string CassieMessageEnd { get; set; } = "facility power system now operational";
        [Description("Hint shown when a player is damaged by SCP-575.")]
        public string DamageHint { get; set; } = "<color=blue>You were damaged by</color> <color=red>SCP-575</color><color=blue>! Equip a flashlight!</color>";
        public ushort DamageHintDuration { get; set; } = 5;

    }
}