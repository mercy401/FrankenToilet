#pragma warning disable CS8618
using FrankenToilet;
using FrankenToilet.Core;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Video;
using TMPro;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace FrankenToilet.somebilly {
    [EntryPoint]
    // THIS IS THE PLUGIN.
    public class Plugin {
        public static Bib bib;

        [EntryPoint]
        public static void Initialize() {
            LogHelper.LogInfo("SOMETHING MALICIOUS IS BREWING...");
            SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(OnSceneLoaded);
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode lsm) {
            if (Bib.Instance != null) {
                return;
            }
            AddThings();
        }
        public static void AddThings() {
            GameObject theStuff = new GameObject("frankenstuff");
            Bib.Instance = theStuff.AddComponent<Bib>();
            UnityObject.DontDestroyOnLoad(theStuff);
        }
    }
}