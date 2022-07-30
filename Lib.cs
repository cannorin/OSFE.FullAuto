using HarmonyLib;
using Rewired;
using UnityEngine;

namespace Patches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Update")]
    class Player_Update
    {
        private static float nextTime = 0.0f;
        private static float interval = 0.1f;

        static void Postfix(Player __instance)
        {
            var p = __instance;
            var rewiredPlayer = p.rewiredPlayer;
            if (p.controlBlocks.Count < 1 && rewiredPlayer != null && !ControllerDisconnectCtrl.controllerDisconnectInProgress && !UIInputFieldVirtualKeyboard.inputCaptureInProgress)
            {
                var time = Time.time;
                if (rewiredPlayer.GetButtonTimedPress("FireOne", 0.5f) && time > nextTime)
                {
                    nextTime = time + interval;
                    p.QueueAction(InputAction.FireOne);
                }
                else if (rewiredPlayer.GetButtonTimedPress("FireTwo", 0.5f) && Time.time > nextTime)
                {
                    nextTime = time + interval;
                    p.QueueAction(InputAction.FireTwo);
                }
            }
        }
    }
}