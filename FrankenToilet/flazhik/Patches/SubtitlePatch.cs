using FrankenToilet.Core;
using FrankenToilet.flazhik.Assets;
using FrankenToilet.flazhik.Components;
using HarmonyLib;
using TMPro;
using UnityEngine;
using static UnityEngine.Object;

namespace FrankenToilet.flazhik.Patches;

[PatchOnEntry]
[HarmonyPatch(typeof(Subtitle))]
public class SubtitlePatch
{
    [ExternalAsset("Assets/UltrakillSanAndreas/Prefabs/SubtitleFontPreset.prefab", typeof(GameObject))]
    private static GameObject fontPreset;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Subtitle), "Awake")]
    public static void Subtitle_Awake_Postfix(Subtitle __instance)
    {
        var text = fontPreset.GetComponent<TMP_Text>();
        __instance.uiText.font = text.font;
    }
}