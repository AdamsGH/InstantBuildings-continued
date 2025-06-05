using System;
using StardewModdingAPI;
using System.Diagnostics;

namespace BitwiseJonMods.Common
{
    /// <summary>The API for Generic Mod Config Menu.</summary>
    public interface IGenericModConfigMenuApi
    {
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);
        void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);
        void AddKeybind(IManifest mod, Func<SButton> getValue, Action<SButton> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);
    }

    public static class Utility
    {
        private static IMonitor _monitor;

        public static void InitLogging(IMonitor monitor)
        {
            _monitor = monitor;
        }

        [ConditionalAttribute("DEBUG")]
        public static void Log(string msg, LogLevel level = LogLevel.Debug)
        {
            _monitor.Log(msg, level);
        }
    }
}
