#nullable disable
using AutoCollect;
using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace AutoCollect.Framework;

[HarmonyPatch(typeof(Board), "Update")]
public class BoardUpdatePatch
{
    private static void Postfix(Board __instance)
    {
        var board = __instance;
        if (board.mChallenge?.mChallengeState > ChallengeState.Normal)
        {
            return;
        }

        var flag6 = board.mLevelComplete || board.mLevelAwardSpawned || board.mBoardFadeOutCounter > 0;
        if (flag6) return;

        DataArray<Coin> coins = board.m_coins;
        if (coins == null) return;

        for (var i = 0; i < coins.Count; i++)
        {
            var coin = coins[i];
            if (coin is not { mIsBeingCollected: false, mDead: false }) continue;
            if (coin.mType is CoinType.UsableSeedPacket or CoinType.PresentPlant)
            {
                return;
            }

            // 原版写死 Collect(0) = 玩家1；这里改为可切换的目标玩家
            coin.Collect(ModEntry.TargetPlayer);
        }
    }
}
