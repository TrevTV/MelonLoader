using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using MelonLoader.Utils;

namespace MelonLoader
{
    public static class MelonHandler
    {
        internal static void Setup()
        {
            if (!Directory.Exists(MelonEnvironment.PluginsDirectory))
                Directory.CreateDirectory(MelonEnvironment.PluginsDirectory);
            
            if (!Directory.Exists(MelonEnvironment.ModsDirectory))
                Directory.CreateDirectory(MelonEnvironment.ModsDirectory);
        }

        private static bool firstSpacer = false;
        public static void LoadMelonsFromDirectory<T>(string path) where T : MelonTypeBase<T>
        {
            path = Path.GetFullPath(path);

            var loadingMsg = $"Loading {MelonTypeBase<T>.TypeName}s from '{path}'...";
            MelonLogger.WriteSpacer();
            MelonLogger.Msg(loadingMsg);

            bool hasWroteLine = false;

            var files = Directory.GetFiles(path, "*.dll");
            var melonAssemblies = new List<MelonAssembly>();
            foreach (var f in files)
            {
                if (!hasWroteLine)
                {
                    hasWroteLine = true;
                    MelonLogger.WriteLine(Color.Magenta);
                }

                var asm = MelonAssembly.LoadMelonAssembly(f, false);
                if (asm == null)
                    continue;

                melonAssemblies.Add(asm);
            }

            var melons = new List<T>();
            foreach (var asm in melonAssemblies)
            {
                asm.LoadMelons();
                foreach (var m in asm.LoadedMelons)
                {
                    if (m is T t)
                    {
                        melons.Add(t);
                    }
                    else
                    {
                        MelonLogger.Warning($"Failed to load Melon '{m.Info.Name}' from '{path}': The given Melon is a {m.MelonTypeName} and cannot be loaded as a {MelonTypeBase<T>.TypeName}. Make sure it's in the right folder.");
                        continue;
                    }
                }
            }

            if (hasWroteLine)
                MelonLogger.WriteSpacer();

            MelonBase.RegisterSorted(melons);

            if (hasWroteLine)
                MelonLogger.WriteLine(Color.Magenta);

            var count = MelonTypeBase<T>._registeredMelons.Count;
            MelonLogger.Msg($"{count} {MelonTypeBase<T>.TypeName.MakePlural(count)} loaded.");
            if (firstSpacer || (typeof(T) ==  typeof(MelonMod)))
                MelonLogger.WriteSpacer();
            firstSpacer = true;
        }
        
        public static void LoadUserlibs(string path)
        {
            path = Path.GetFullPath(path);

            var loadingMsg = $"Loading UserLibs from '{path}'...";
            MelonLogger.WriteSpacer();
            MelonLogger.Msg(loadingMsg);

            bool hasWroteLine = false;

            var files = Directory.GetFiles(path, "*.dll");
            var melonAssemblies = new List<MelonAssembly>();
            foreach (var f in files)
            {
                if (!hasWroteLine)
                {
                    hasWroteLine = true;
                    MelonLogger.WriteLine(Color.Magenta);
                }

                var asm = MelonAssembly.LoadMelonAssembly(f, false);
                if (asm == null)
                    continue;

                melonAssemblies.Add(asm);
            }
        }
    }
}
