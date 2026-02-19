using FrankenToilet.Core;
using HarmonyLib;

namespace FrankenToilet.mercy.Patches;

[PatchOnEntry]
[HarmonyPatch(typeof(Guttertank))]
public static class GuttertankSpeed
{
    public static bool Active = false;
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    public static void Start(Guttertank __instance)
    {
        if (Active) __instance.anim.speed *= 2;
    }

    public static void Activate() => Active = true;
}