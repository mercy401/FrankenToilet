using System;
using System.Diagnostics;
using FrankenToilet.Core;
using FrankenToilet.mercy.Patches;
using plog.Models;
using UnityEngine;
using Random = System.Random;

namespace FrankenToilet.mercy;

[EntryPoint]
public static class Plugin
{
    public static AssetBundle? assetBundle;
    public static Random rand = new();
    public static GameObject? manager;
    [EntryPoint]
    public static void Entry()
    {
        LogHelper.LogInfo("MERCY HERE!");
        // WE GET THE ASSET BUNDLE
        assetBundle = Helper.GetBundle();
        if (assetBundle == null) LogHelper.LogError("SORRY BRAH BUT THE ASSET BUNDLE IS MISSING :((((");
        else LogHelper.LogInfo($"LOADED {Helper.assetBundleName}");
    }
}