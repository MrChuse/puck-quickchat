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