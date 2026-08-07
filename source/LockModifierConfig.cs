using System.Collections.Generic;

namespace LocksAffectReinforcement;

public class LockModifierConfig
{
    // Key: fully domain-prefixed lock item code (e.g. "game:padlock-steel").
    // Value: multiplier applied to a block's reinforcement strength when that
    // lock item is applied. Add an entry here to support a lock from any mod.
    public Dictionary<string, float> LockModifiers = new()
    {
        { "game:padlock-tinbronze", 0.8f },
        { "game:padlock-blackbronze", 0.8f },
        { "game:padlock-bismuthbronze", 0.8f },
        { "game:padlock-iron", 1.0f },
        { "game:padlock-meteoriciron", 1.25f },
        { "game:padlock-steel", 1.5f },
    };
}
