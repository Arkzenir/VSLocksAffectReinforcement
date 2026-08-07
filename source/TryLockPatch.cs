using System;
using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace LocksAffectReinforcement;

[HarmonyPatch(typeof(ModSystemBlockReinforcement), nameof(ModSystemBlockReinforcement.TryLock))]
public static class TryLockPatch
{
    // ModSystemBlockReinforcement.api is private; it's the only way to check
    // which side this instance is running on before touching persisted state.
    static readonly FieldInfo? ApiField =
        typeof(ModSystemBlockReinforcement).GetField("api", BindingFlags.NonPublic | BindingFlags.Instance);

    public static void Postfix(ModSystemBlockReinforcement __instance, BlockPos pos, string itemCode, bool __result)
    {
        if (!__result) return;

        ICoreAPI? api = ApiField?.GetValue(__instance) as ICoreAPI;
        if (api == null || api.Side != EnumAppSide.Server) return;

        if (!LocksAffectReinforcementModSystem.LockModifiers.TryGetValue(itemCode, out float modifier)) return;

        BlockReinforcement bre = __instance.GetReinforcment(pos);
        if (bre == null || bre.Strength <= 0) return;

        int newStrength = Math.Max((int)Math.Round(bre.Strength * modifier), 1);
        int delta = bre.Strength - newStrength;
        if (delta == 0) return;

        // ConsumeStrength persists and re-syncs the reinforcement to clients;
        // a negative delta raises the strength instead of lowering it.
        __instance.ConsumeStrength(pos, delta);
    }
}
