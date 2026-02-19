using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using FrankenToilet.Core;
using FrankenToilet.mercy.Features;
using FrankenToilet.mercy.Patches;
using UnityEngine;

namespace FrankenToilet.mercy;

public class ActivateFeatures : MonoBehaviour
{
    public static Stopwatch timer = new();
    public static long time = 0;
    // i am so sorry
    public static List<Action> features = [
        () => GuttertankSpeed.Activate()
    ];
    public static GameObject newFeatureAlert;

    public void Awake()
    {
        timer.Start();
        time = Plugin.rand.Next(10, 20);
    }
    public void Update()
    {
        if (timer.Elapsed.TotalSeconds > time && features.Count > 0)
        {
            // WE ACTIVATE NEW FEATURES!!
            int featureIndex;
            if (features.Count - 1 > 0) featureIndex = Plugin.rand.Next(0, features.Count - 1);
            else featureIndex = 0;
            LogHelper.LogInfo(features.Count);
            LogHelper.LogInfo(featureIndex);
            Action activate = features[featureIndex];
            activate();
            features.Remove(activate);
            GameObject? canvas = Helper.GetRootCanvas();
            if (canvas != null)
            {
                newFeatureAlert = Instantiate(new GameObject("New Feature Alert"), canvas.transform);
                newFeatureAlert.AddComponent<NewFeatureAlert>();
                LogHelper.LogInfo("CREATED THE OBJECT");
            } else LogHelper.LogError("COULDN'T CREATE THE OBJECT");
            if (features.Count > 0)
            {
                time = Plugin.rand.Next(10, 20);
                timer.Restart();
            } else timer.Reset();
        }
    }

    private void OnDestroy() => timer.Reset();
}