using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using System;
using System.Collections.Generic;

namespace SCP_575
{
    public class Extensions
    {
        public Plugin plugin;
        public Extensions(Plugin plugin) => this.plugin = plugin;

        private Random rnd = new Random();

        public IEnumerator<float> RunBlackoutTimer()
        {
            yield return Timing.WaitForSeconds(plugin.Config.InitialDelay);

            while(true)
            {
                Cassie.Message(plugin.Config.CassieMessageStart, false, true);
                if (plugin.Config.DisableTeslas)
                    plugin.TeslasDisabled = true;
                yield return Timing.WaitForSeconds(8.7f);

                int blackoutDur = plugin.Config.DurationMax;
                int delay = plugin.Config.InitialDelay;

                if(plugin.Config.RandomEvents)
                {
                    blackoutDur = rnd.Next(plugin.Config.DurationMin, plugin.Config.DurationMax);
                    delay = rnd.Next(plugin.Config.DelayMin, plugin.Config.DelayMax);
                }

                if (plugin.Config.EnableKeter)
                    plugin.KeterCoroutine = Timing.RunCoroutine(RunKeter(blackoutDur));

                Map.TurnOffAllLights(blackoutDur, plugin.Config.AffectedZones);

                if (plugin.Config.Voice)
                    Cassie.Message(plugin.Config.CassieKeter, false, false);
                yield return Timing.WaitForSeconds(blackoutDur - 8.7f);
                Cassie.Message(plugin.Config.CassieMessageEnd, false, true);
                yield return Timing.WaitForSeconds(8.7f);
                Timing.KillCoroutines(plugin.KeterCoroutine);
                plugin.TeslasDisabled = false;
                yield return Timing.WaitForSeconds(delay);
            }
        }

        public IEnumerator<float> RunKeter(int dur)
        {
            do
            {
                foreach (Player player in Player.List)
                {
                    if (!player.CurrentRoom.LightsOn && !player.HasFlashlightModuleEnabled && !(player.CurrentItem is Flashlight flashlight && flashlight.Active) && player.IsHuman)
                    {
                        player.Hurt(plugin.Config.KeterDamage, plugin.Config.KilledBy);
                        if (plugin.Config.DamageHintDuration > 0)
                            player.ShowHint(plugin.Config.DamageHint, plugin.Config.DamageHintDuration);
                    }

                    yield return Timing.WaitForSeconds(5f);
                }
            } while ((dur -= 5) > 5);
        }
    }
}
