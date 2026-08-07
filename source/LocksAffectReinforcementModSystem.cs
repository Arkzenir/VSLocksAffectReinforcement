using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

[assembly: ModInfo("LocksAffectReinforcement", "locksaffectreinforcement")]

namespace LocksAffectReinforcement;

public class LocksAffectReinforcementModSystem : ModSystem
{
    const string ConfigFileName = "locksaffectreinforcement.json";
    const string HarmonyId = "locksaffectreinforcement.trylock";

    public static Dictionary<string, float> LockModifiers = new();

    Harmony? harmony;

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.Logger.Debug("[LocksAffectReinforcement] Mod loaded.");
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        LoadConfig(api);

        harmony = new Harmony(HarmonyId);
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public override void Dispose()
    {
        harmony?.UnpatchAll(HarmonyId);
        base.Dispose();
    }

    void LoadConfig(ICoreServerAPI api)
    {
        LockModifierConfig config = api.LoadModConfig<LockModifierConfig>(ConfigFileName);
        if (config == null)
        {
            config = new LockModifierConfig();
            api.StoreModConfig(config, ConfigFileName);
            api.Logger.Notification($"[LocksAffectReinforcement] No config found, wrote defaults to {ConfigFileName}.");
        }

        LockModifiers = config.LockModifiers;
    }
}
