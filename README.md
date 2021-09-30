# SCP-575 (non-playable)
SCP-575 is a sentient (non-playable) entity that will wreak havoc around the facility.

When a round starts there is a configurable chance that SCP-575 will be present that round.
If he is, after a configurable initial delay (default 5mins), he will cause a power blackout within the facility. During this blackout, lights in HCZ, LCZ and Entrance will be off, causing players to need flashlights or NV scopes to find their way around. (SCP's are unaffected by the blackouts, and can see normally).
If SCP-575's 'keter' option is enabled (which it is by default), he will damage players every 5 sec who are in the dark without a light source (such as a flashlight, gun flashlight attachment, or a gun NV scope attackment) for a configurable amount of damage (default 10).
During blackouts, automatic tesla gates are disabled, though 079 can still manually trigger them.
After a configured period of time (default 90s) the blackout will end.

After the initial blackout, it will repeat itself throughout the round, with a configurable interval (default 8min).

There is also some randomness embeded within all of these above mentioned timers, meaning, unless you disable the randomness, the timers will be randomly shorter than their default 'max' duration. Meaning a blackout could only last 20s instead of 90, and they could repeat 3min after eachother instead of 8, etc.

# Config
Name | Type | Description | Default
---- | ---- | ----------- | -------
random_events | bool | Whether or not randomly timed events should occur. If false, all events will be at the same interval apart. | true
disable_teslas | bool | Whether or not tesla gates should be disabled during blackouts. | true
initial_delay | float | The delay before the first event of each round, in seconds. | 300
duration_min | float | The minimum number of seconds a blackout event can last. | 30
duration_max | float | The maximum number of seconds a blackout event can last. If RandomEvents is disabled, this will be the duration for every event. | 90
delay_min | int | The minimum amount of seconds between each event. | 180
delay_max | int | The maximum amount of seconds between each event. If RandomEvents is disabled, this will be the delay between every event. | 500
spawn_chance | int | The percentage change that SCP-575 events will occur in any particular round. | 45
enable_keter | bool | Whether or not people in dark rooms should take damage if they have no light source in their hand. | true
affected_zones | HashSet | Blackout Affected Zones | Surface, Entrance, HeavyContainment, LightContainment
voice | bool | Whether or not SCP-575's "roar" should happen after a blackout starts. | true
keter_damage | float | How much damage per 5 seconds should be inflicted if EnableKeter is set to true. | 10
is_enabled | bool | Whether or not the plugin is enabled. | true
killed_by | string | Name displayed in player's death information | SCP-575
cassie_message_start | string | Message said by Cassie | facility power system failure in 3 . 2 . 1 .
cassie_keter | string | -/- | pitch_0.15 .g7
cassie_message_end | string | -/- | facility power system now operational
damage_broadcast | string | Broadcast shown when a player is damaged by SCP-575. | You were damaged by SCP-575! Equip a flashlight!
damage_broadcast_duration | ushort | -/- | 5

# Default Config
```yml
575:
  # Whether or not randomly timed events should occur. If false, all events will be at the same interval apart.
  random_events: true
  # Whether or not tesla gates should be disabled during blackouts.
  disable_teslas: true
  # The delay before the first event of each round, in seconds.
  initial_delay: 300
  # The minimum number of seconds a blackout event can last.
  duration_min: 30
  # The maximum number of seconds a blackout event can last. If RandomEvents is disabled, this will be the duration for every event.
  duration_max: 90
  # The minimum amount of seconds between each event.
  delay_min: 180
  # The maximum amount of seconds between each event. If RandomEvents is disabled, this will be the delay between every event.
  delay_max: 500
  # The percentage change that SCP-575 events will occur in any particular round.
  spawn_chance: 45
  # Whether or not people in dark rooms should take damage if they have no light source in their hand.
  enable_keter: true
  # Blackout Affected Zones
  affected_zones:
  - Surface
  - Entrance
  - HeavyContainment
  - LightContainment
  # Whether or not SCP-575's "roar" should happen after a blackout starts.
  voice: true
  # How much damage per 5 seconds should be inflicted if EnableKeter is set to true.
  keter_damage: 10
  # Whether or not the plugin is enabled.
  is_enabled: true
  # Name displayed in player's death information.
  killed_by: SCP-575
  # Message said by Cassie.
  cassie_message_start: facility power system failure in 3 . 2 . 1 .
  cassie_keter: pitch_0.15 .g7
  cassie_message_end: facility power system now operational
  # Broadcast shown when a player is damaged by SCP-575.
  damage_broadcast: You were damaged by SCP-575! Equip a flashlight!
  damage_broadcast_duration: 5
```
