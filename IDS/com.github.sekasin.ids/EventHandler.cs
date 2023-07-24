using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using Random = System.Random;
using Server = Exiled.Events.Handlers.Server;

namespace IDS.com.github.sekasin.ids;

public class EventHandler {
        private readonly Plugin<Config> _main;
        private readonly bool _debugMode;
        private readonly int BlackoutDuration;
        private readonly int BlackoutFrequency;
        private readonly double DoorLockDownChance;
        CoroutineHandle timer;
        private Random rnd;

        public EventHandler(Plugin<Config> plugin)
        {
            _main = plugin;
            _debugMode = plugin.Config.Debug;
            if (_debugMode)
            {
                Log.Info("Loading EventHandler");
            }

            BlackoutFrequency = plugin.Config.BlackoutFrequency;
            BlackoutDuration = plugin.Config.BlackoutDuration;
            DoorLockDownChance = plugin.Config.DoorLockDownChance;
            rnd = new Random();
            
            if (_debugMode) {
                Log.Debug("A room will be randomly in lockdown every " + BlackoutFrequency + " seconds.");
            }

            Server.RoundStarted += StartIDSTimer;
            Server.RestartingRound += StopIDSTimer;
            Server.RoundEnded += StopIDSTimer;
        }

        private void StartIDSTimer() {
            timer = Timing.CallDelayed(BlackoutFrequency, TurnOffRandomRoomLight);
        }
        private void StopIDSTimer() {
            Timing.KillCoroutines(timer);
            if (_debugMode) {
                Log.Debug("Stopped IDS timers.");
            }
        }
        
        private void StopIDSTimer(RoundEndedEventArgs ev) {
            Timing.KillCoroutines(timer);
        }

        private void TurnOffRandomRoomLight()
        {
            int index = rnd.Next(RoomLightController.Instances.Count);

            RoomLightController.Instances.RemoveAll(item => Room.Get(item.Room).Zone == ZoneType.Other || Room.Get(item.Room).Zone == ZoneType.Unspecified);
            RoomLightController chosen = RoomLightController.Instances[index];

            Room room = Room.Get(chosen.Room);

            room.TurnOffLights(BlackoutDuration);
            
            if (_debugMode) {
                Log.Debug(chosen.Room + " in blackout");
            }

            if (rnd.NextDouble() < DoorLockDownChance)
            {
                room.LockDown(BlackoutDuration);
                if (_debugMode) {
                    Log.Debug(chosen.Room + " in lockdown");
                }
            }

            Timing.KillCoroutines(timer);
            StartIDSTimer();
        }
        public void UnregisterEvents()
        {
            Server.RoundStarted -= StartIDSTimer;
            Server.RestartingRound -= StopIDSTimer;
            Server.RoundEnded -= StopIDSTimer;
        }
}