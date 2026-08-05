using Vintagestory.API.Common;

[assembly: ModInfo("LocksAffectReinforcement", "locksaffectreinforcement")]

namespace LocksAffectReinforcement;

public class LocksAffectReinforcementModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.Logger.Debug("[LocksAffectReinforcement] Mod loaded.");
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
    }
}
