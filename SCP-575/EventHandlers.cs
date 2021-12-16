using Exiled.API.Features;
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

		public void OnWarheadDetonated()
        {
			if (!plugin.Config.Warhead) return;

			Timing.KillCoroutines(plugin.BlackoutCoroutine, plugin.KeterCoroutine);
			float num = (AlphaWarheadController.Host.timeToDetonation <= 0f) ? 3.5f : 1f;
			Cassie.GlitchyMessage("SCP 5 7 5 SUCCESSFULLY TERMINATED BY ALPHA WARHEAD", UnityEngine.Random.Range(0.1f, 0.14f) * num, UnityEngine.Random.Range(0.07f, 0.08f) * num);
        }
	}
}
