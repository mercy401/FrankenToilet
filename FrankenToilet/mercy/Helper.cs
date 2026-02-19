using System.IO;
using System.Reflection;
using FrankenToilet.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrankenToilet.mercy;

public static class Helper {
    public const string assetBundleName = "FrankenToilet.mercy.assets.bundle";
    public static AssetBundle? GetBundle()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Stream? stream = assembly.GetManifestResourceStream(assetBundleName);
        if (stream == null) return null;
        return AssetBundle.LoadFromStream(stream);
    }
    public static Sprite[]? LoadFrames(AssetBundle? assetBundle, string path, int frames)
    {
        AssetBundle? bundle = Plugin.assetBundle;
        if (bundle == null)
        {
            LogHelper.LogError("NO ASSET BUNDLE");
            return null;
        }
        // ADDS ALL OF THE FRAMES TO MAKE AN ANIMATED IMAGE, WAHOO
        Sprite[] sprites = new Sprite[frames];
        for (int i = 1; i <= frames; i++)
        {
            string filePath = $"Assets/Features/{path}/{i.ToString()}.png";
            LogHelper.LogInfo(filePath);
            Sprite? currentSprite = bundle.LoadAsset<Sprite>(filePath);
            if (currentSprite == null)
            {
                LogHelper.LogError($"FRAME {i} COULDN'T BE LOADED");
                return null;
            }
            sprites[i-1] = currentSprite;
        } return sprites;
    }

    public static GameObject? GetRootCanvas()
    {
        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gameObjects) 
            if (go.GetComponent<Canvas>() != null) return go;
        return null;
    }
}