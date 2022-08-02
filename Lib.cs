using HarmonyLib;
using Rewired;
using UnityEngine;
using System;

namespace Patches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Update")]
    class Player_Update
    {
        private static DateTime nextTime = DateTime.Now;
        private static TimeSpan interval = TimeSpan.FromMilliseconds(100);

        static void Postfix(Player __instance)
        {
            var p = __instance;
            var rewiredPlayer = p.rewiredPlayer;
            if (p.controlBlocks.Count < 1 && rewiredPlayer != null && !ControllerDisconnectCtrl.controllerDisconnectInProgress && !UIInputFieldVirtualKeyboard.inputCaptureInProgress)
            {
                var time = DateTime.Now;
                if (rewiredPlayer.GetButtonTimedPress("FireOne", 0.5f) && time > nextTime)
                {
                    nextTime = time + interval;
                    p.QueueAction(InputAction.FireOne);
                }
                else if (rewiredPlayer.GetButtonTimedPress("FireTwo", 0.5f) && time > nextTime)
                {
                    nextTime = time + interval;
                    p.QueueAction(InputAction.FireTwo);
                }
            }
        }
    }
}