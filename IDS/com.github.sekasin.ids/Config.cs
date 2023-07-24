using System.ComponentModel;
using Exiled.API.Interfaces;

namespace IDS.com.github.sekasin.ids
{
    public class Config: IConfig
    {
        [Description("Is the Plugin enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug mode.")]
        public bool Debug { get; set; } = true;

        [Description("How often a room should be blacked out, time in seconds.")]
        public int BlackoutFrequency { get; set; } = 60;
        
        [Description("How many seconds a room should stay blackouted for.")]
        public int BlackoutDuration { get; set; } = 10;

        [Description(
            "What is the change that the doors will be locked with the blackout. Give a value between 0 and 1")]
        public double DoorLockDownChance { get; set; } = 0.5;
    }
}