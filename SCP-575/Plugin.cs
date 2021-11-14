using System;
using MEC;

using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace SCP_575
{
	public class Plugin : Exiled.API.Features.Plugin<Config>
	{
        public override string Name => "SCP-575";
        public override string Author => "Original by Joker119, Continued by Marco15453";
        public override string Prefix => "575";
        public override Version Version => new Version(4, 2, 0);
        public override Version RequiredExiledVersion => new Version(3, 6, 2);

        public Random Gen = new Random();
		public bool TeslasDisabled = false;
		
		public EventHandlers EventHandlers;
		public Extensions Extensions;

		public CoroutineHandle BlackoutCoroutine;
		public CoroutineHandle KeterCoroutine;
		
		public override void OnEnabled()
		{				
			registerEvents();
			base.OnEnabled();
		}

		public override void OnDisabled()
		{
			unregisterEvents();
			base.OnDisabled();
        }

        private void registerEvents()
        {
			EventHandlers = new EventHandlers(this);
			Extensions = new Extensions(this);

			// Server
			Server.RoundStarted += EventHandlers.OnRoundStart;
			Server.RoundEnded += EventHandlers.OnRoundEnd;

			// Player
			Player.TriggeringTesla += EventHandlers.OnTriggerTesla;
		}

        private void unregisterEvents()
        {
			// Server
			Server.RoundStarted -= EventHandlers.OnRoundStart;
			Server.RoundEnded -= EventHandlers.OnRoundEnd;

			// Player
			Player.TriggeringTesla -= EventHandlers.OnTriggerTesla;

			EventHandlers = null;
			Extensions = null;
		}
	}
}