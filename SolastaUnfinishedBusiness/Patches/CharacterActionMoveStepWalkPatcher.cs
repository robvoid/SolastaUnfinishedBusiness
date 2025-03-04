﻿using HarmonyLib;
using JetBrains.Annotations;
using SolastaUnfinishedBusiness.Api.GameExtensions;
using SolastaUnfinishedBusiness.Interfaces;

namespace SolastaUnfinishedBusiness.Patches;

[UsedImplicitly]
public static class CharacterActionMoveStepWalkPatcher
{
    internal static bool IsCharacterActionMoveStepWalk;

    //PATCH: support for `IMoveStepFinished`
    [HarmonyPatch(typeof(CharacterActionMoveStepWalk),
        nameof(CharacterActionMoveStepWalk.ChangeEndProneStatusIfNecessary))]
    [UsedImplicitly]
    public static class ChangeEndProneStatusIfNecessary_Patch
    {
        [UsedImplicitly]
        public static void Prefix(CharacterActionMoveStepWalk __instance)
        {
            IsCharacterActionMoveStepWalk = true;

            var mover = __instance.ActingCharacter;

            foreach (var moveStepFinished in mover.RulesetCharacter.GetSubFeaturesByType<IMoveStepFinished>())
            {
                moveStepFinished.MoveStepFinished(mover);
            }
        }
    }
}
