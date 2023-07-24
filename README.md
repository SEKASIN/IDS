# IDS
EXILED plugin that randomly causes generator faults at random rooms.

## Installation
Download IDS.dll from [Releases](https://github.com/SEKASIN/IDS/blob/master/Releases)\
Move IDS.dll to .config/EXILED/Plugins and restart server.

## Configuration
Edit values in .config/EXILED/Configs/PORT-config.yml\
Example config with default values:
```
i_d_s:
# Is the Plugin enabled.
  is_enabled: true
  # Debug mode.
  debug: true
  # How often a room should be blacked out, time in seconds.
  blackout_frequency: 60
  # How many seconds a room should stay blackouted for.
  blackout_duration: 10
  # What is the change that the doors will be locked with the blackout. Give a value between 0 and 1
  door_lock_down_chance: 0.5
```
- is_enabled
> A boolean; Controls if IDS is enabled or not.
- debug
> A boolean; Enables some extra logging.
- blackout_frequency
> An int; How many seconds between blackouts.
- blackout_duration
> An int; How long a room will stay blackouted for.
- door_lock_down_chance
> A float; What is the probability that the doors will also be closed in a blackouted room.
