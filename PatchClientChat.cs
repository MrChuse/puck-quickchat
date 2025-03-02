using System;
using HarmonyLib;
using UnityEngine;

namespace QuickChat;

public class PatchClientChat
{
    [HarmonyPatch(typeof(UIChat), nameof(UIChat.Start))]
    class PatchUIChatStart
    {
        static void Postfix(UIChat __instance)
        {
            Plugin.Log.LogInfo($"Patch: UIChat.Start (Postfix) was called.");
            Plugin.chat = __instance;
        }
    }

    [HarmonyPatch(typeof(PlayerInput), nameof(PlayerInput.Update))]
    class PatchPlayerInputUpdate
    {
        static int btn1 = -1;
        static int btn2 = -1;
        static float time_of_last_press=0;

        static void Postfix()
        {
            if (Plugin.chat.isFocused) return;

            if (Plugin.slashAction.WasPressedThisFrame()){
                Plugin.chat.FocusChatInput();
            }

            if (Plugin.alpha1Action.WasPressedThisFrame()){
                if (btn1 == -1 && Time.time - time_of_last_press > 0.05){
                    btn1 = 1;
                    time_of_last_press = Time.time;
                }
                else if (Time.time - time_of_last_press > 0.05){
                    btn2 = 1;
                    time_of_last_press = Time.time;
                }
            }
            if (Plugin.alpha2Action.WasPressedThisFrame()){
                if (btn1 == -1 && Time.time - time_of_last_press > 0.05){
                    btn1 = 2;
                    time_of_last_press = Time.time;
                }
                else if (Time.time - time_of_last_press > 0.05){
                    btn2 = 2;
                    time_of_last_press = Time.time;
                }
            }
            if (Plugin.alpha3Action.WasPressedThisFrame()){
                if (btn1 == -1 && Time.time - time_of_last_press > 0.05){
                    btn1 = 3;
                    time_of_last_press = Time.time;
                }
                else if (Time.time - time_of_last_press > 0.05){
                    btn2 = 3;
                    time_of_last_press = Time.time;
                }
            }
            if (Plugin.alpha4Action.WasPressedThisFrame()){
                if (btn1 == -1 && Time.time - time_of_last_press > 0.05){
                    btn1 = 4;
                    time_of_last_press = Time.time;
                }
                else if (Time.time - time_of_last_press > 0.05){
                    btn2 = 4;
                    time_of_last_press = Time.time;
                }
            }
            if (Time.time - time_of_last_press > 3){
                btn1 = -1;
                btn2 = -1;
            }
            if (btn1 != -1 && btn2 != -1){
                Plugin.chat.Client_SendClientChatMessage($"{Plugin.chat_table[btn1-1,btn2-1].BoxedValue}");
                btn1 = -1;
                btn2 = -1;
            }
        }
    }
}