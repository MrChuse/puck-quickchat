using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine.InputSystem;

namespace QuickChat;

[BepInPlugin("plugins.quickchat", "QuickChat", "0.1.0.0")]
public class Plugin : BasePlugin
{
    // the "configurable" things
    private readonly Harmony _harmony = new Harmony("plugins.quickchat");
    
    // plugin managers
    public static new ManualLogSource Log;
    public static UIChat chat;

    public static InputAction alpha1Action;
    public static InputAction alpha2Action;
    public static InputAction alpha3Action;
    public static InputAction alpha4Action;
    public static ConfigEntry<string>[,] chat_table;

    static String[][] chat_table_default = [
            ["I got it!", "Centering!", "Take the shot!", "Defending..."],
            ["Nice one!", "Great pass!", "Thanks!", "What a save!"],
            ["Holy cow!", "Noooo!", "Wow!", "Close one!"],
            ["@#$%!", "No problem.", "This is Rocket League!", "Sorry!"]
        ];
    
    public override void Load()
    {
        HarmonyFileLog.Enabled = true;

        alpha1Action = new InputAction(binding: "<keyboard>/1");
        alpha1Action.Enable();
        alpha2Action = new InputAction(binding: "<keyboard>/2");
        alpha2Action.Enable();
        alpha3Action = new InputAction(binding: "<keyboard>/3");
        alpha3Action.Enable();
        alpha4Action = new InputAction(binding: "<keyboard>/4");
        alpha4Action.Enable();

        chat_table = new ConfigEntry<string>[4,4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++){
                chat_table[i,j] = Config.Bind("QuickChat", // The section under which the option is shown
                    $"quickchat{i+1}{j+1}",                // The key of the configuration option in the configuration file
                    chat_table_default[i][j],              // The default value
                    $"quickchat{i+1}{j+1}");               // Description of the option to show in the config file
        }

        // Plugin startup logic
        Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded! Patching methods...");
        _harmony.PatchAll();
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is all patched! Patched methods:");
        
        var originalMethods = Harmony.GetAllPatchedMethods();
        foreach (var method in originalMethods)
        {
            Log.LogInfo($" - {method.DeclaringType.FullName}.{method.Name}");
        }
    }
}