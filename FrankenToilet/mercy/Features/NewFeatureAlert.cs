using System.Diagnostics;
using FrankenToilet.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FrankenToilet.mercy.Features;

public class NewFeatureAlert : MonoBehaviour
{
    public Sprite[]? frames;
    public Stopwatch deathTimer = new();
    public Stopwatch frameTimer = new();
    public Image image;
    public int index;
    public const int FRAMECOUNT = 11;

    private void Awake()
    {
        frames = Helper.LoadFrames(Plugin.assetBundle, "NewFeatureAlert", FRAMECOUNT);
        deathTimer.Start();
        frameTimer.Start();
        image = gameObject.AddComponent<Image>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 40);
    }

    private void Update()
    {
        if (deathTimer.Elapsed.TotalSeconds >= 2) Destroy(gameObject);
        if (frames == null) return;
        // ANIMATION
        if (frameTimer.Elapsed.TotalSeconds >= 0.05)
        {
            image.sprite = frames[index];
            LogHelper.LogInfo(index);
            if (index < FRAMECOUNT-1) ++index;
            else index = 0;
            frameTimer.Restart();
        }
    }
}