using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using Respawning;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;
using Handlers = Exiled.Events.Handlers;
using Exiled.API.Features.Items;

namespace SCP_575
{
	public class Plugin : Exiled.API.Features.Plugin<Config>
	{
        public override string Name => "SCP-575";
        public override string Author => "Original by Galaxy119, Continued by Marco15453";
        public override string Prefix => "575";
        public override Version Version => new Version(3, 9, 2);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        public Random Gen = new Random();
		
		public EventHandlers EventHandlers;
		public static bool TimerOn;

		public override void OnEnabled()
		{				
			EventHandlers = new EventHandlers(this);

			Handlers.Server.RoundStarted += EventHandlers.OnRoundStart;
			Handlers.Server.RoundEnded += EventHandlers.OnRoundEnd;
			Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
			Handlers.Player.TriggeringTesla += EventHandlers.OnTriggerTesla;

			base.OnEnabled();
		}

		public override void OnDisabled()
		{
			foreach (CoroutineHandle handle in EventHandlers.Coroutines)
				Timing.KillCoroutines(handle);
			EventHandlers.Coroutines.Clear();
			Handlers.Server.RoundStarted -= EventHandlers.OnRoundStart;
			Handlers.Server.RoundEnded -= EventHandlers.OnRoundEnd;
			Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
			Handlers.Player.TriggeringTesla -= EventHandlers.OnTriggerTesla;
			EventHandlers = null;

			base.OnDisabled();
		}

		public IEnumerator<float> RunBlackoutTimer()
		{
			yield return Timing.WaitForSeconds(Config.InitialDelay);

			for (;;)
			{
				RespawnEffectsController.PlayCassieAnnouncement(Config.CassieMessageStart, false, true);

				if (Config.DisableTeslas)
					EventHandlers.TeslasDisabled = true;
				TimerOn = true;
				yield return Timing.WaitForSeconds(8.7f);
			
				float blackoutDur = Config.DurationMax;
				if (Config.RandomEvents)
					blackoutDur = (float)Gen.NextDouble() * (Config.DurationMax - Config.DurationMin) + Config.DurationMin;
				if (Config.EnableKeter)
					EventHandlers.Coroutines.Add(Timing.RunCoroutine(Keter(blackoutDur), "keter"));

				foreach (ZoneType type in Config.AffectedZones) 
					Map.TurnOffAllLights(blackoutDur, type);

				if (Config.Voice)
					RespawnEffectsController.PlayCassieAnnouncement(Config.CassieKeter, false, false);
				yield return Timing.WaitForSeconds(blackoutDur - 8.7f);
				RespawnEffectsController.PlayCassieAnnouncement(Config.CassieMessageEnd, false, true);
				yield return Timing.WaitForSeconds(8.7f);
				Timing.KillCoroutines("keter");
				EventHandlers.TeslasDisabled = false;
				TimerOn = false;
				if (Config.RandomEvents)
					yield return Timing.WaitForSeconds(Gen.Next(Config.DelayMin, Config.DelayMax));
				else
					yield return Timing.WaitForSeconds(Config.InitialDelay);
			}
		}

		public IEnumerator<float> Keter(float dur)
		{
			do
			{
				foreach (Player player in Player.List)
				{
					if (player.CurrentRoom.LightsOff && !player.HasFlashlightModuleEnabled && !(player.CurrentItem is Flashlight flashlight && flashlight.Active) && player.ReferenceHub.characterClassManager.IsHuman())
						player.Hurt(Config.KeterDamage, DamageTypes.Bleeding, Config.KilledBy);

					yield return Timing.WaitForSeconds(5f);
				}
			} while ((dur -= 5f) > 5f);
		}
	}
}