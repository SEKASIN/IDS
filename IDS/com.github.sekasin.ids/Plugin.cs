using System;
using Exiled.API.Features;
using IDS.com.github.sekasin.ids;
using EventHandler = IDS.com.github.sekasin.ids.EventHandler;

namespace IDSSuperScan.com.github.sekasin.ids {
    public class SuperScan : Plugin<Config> {
        public override string Name => "IDS";
        public override string Author => "Ugi0";
        public override Version Version => new Version(2, 0, 0);
        public EventHandler eventHandler;

        public override void OnEnabled() {
            Log.Info("IDS loading...");
            if (!Config.IsEnabled) {
                Log.Warn("IDS disabled from config, unloading...");
                OnDisabled();
                return;
            }
            eventHandler = new EventHandler(this);
            Log.Info("IDS loaded.");
        }

        public override void OnDisabled()
        {
            eventHandler.UnregisterEvents();
            Log.Info("IDS unloaded.");
        }
    }
}