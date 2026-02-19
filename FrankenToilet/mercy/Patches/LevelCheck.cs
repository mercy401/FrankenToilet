using System.Linq;
using FrankenToilet.Core;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace FrankenToilet.mercy.Patches;

[PatchOnEntry]
[HarmonyPatch(typeof(SceneHelper))]
public static class LevelCheck
{
    public static string[] blacklistedScenes = ["Bootstrap", "Intro", "Main Menu"];
    [HarmonyPatch("OnSceneLoaded")]
    [HarmonyPostfix]
    public static void SceneCheck() {
        Plugin.canvas = Helper.GetRootCanvas();
        // ACTIVATE OUR FEATURES,,,
        if (!blacklistedScenes.Contains(SceneHelper.CurrentScene))
        {
            Plugin.manager = new GameObject("MANAGER");
            Plugin.manager.AddComponent<ActivateFeatures>();
        }
    }
}