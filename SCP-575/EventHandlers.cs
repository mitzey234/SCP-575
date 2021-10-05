using System.Collections.Generic;
using Exiled.Events.EventArgs;
using MEC;

namespace SCP_575
{
	public class EventHandlers
	{
		public Plugin plugin;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart()
		{
			Timing.KillCoroutines(plugin.BlackoutCoroutine, plugin.KeterCoroutine);
			plugin.TeslasDisabled = false;
			if (plugin.Gen.Next(100) < plugin.Config.SpawnChance)
				plugin.BlackoutCoroutine = Timing.RunCoroutine(plugin.Extensions.RunBlackoutTimer());
		}

		public void OnRoundEnd(RoundEndedEventArgs ev)
		{
			Timing.KillCoroutines(plugin.BlackoutCoroutine, plugin.KeterCoroutine);
		}

		public void OnTriggerTesla(TriggeringTeslaEventArgs ev)
		{
			if (plugin.TeslasDisabled)
				ev.IsTriggerable = false;
		}
	}
}
