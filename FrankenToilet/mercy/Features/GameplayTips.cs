using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrankenToilet.mercy.Features;

public class GameplayTips : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fontSize = 26;
    public float additionFactor = 0.05f;
    public string[] gameplayTips =
    [
        "Use accumulator for\nAOE Accumulator Damage",
        "Always switch to\ndefault piercer",
        "Play 4-3!",
        "Emu Otori is the\nmain character\nof ULTRAKILL!",
        "Also try CrossCode!"
    ];
    [MercyFeature]
    public static void Activate()
    {
        GameObject gameObject = Instantiate(new GameObject("Gameplay Tips"), Plugin.canvas.transform);
        gameObject.AddComponent<GameplayTips>();
        int x = Plugin.rand.Next(0, Screen.width);
        int y = Plugin.rand.Next(0, Screen.height);
        gameObject.transform.position = new Vector3(x, y);
    }

    private void Awake()
    {
        text = gameObject.AddComponent<TextMeshProUGUI>();
        int index = Plugin.rand.Next(0, gameplayTips.Length-1);
        string gameplayTip = gameplayTips[index];
        text.text = $"GAMEPLAY TIP:\n{gameplayTip}";
        text.fontSize = fontSize;
        text.color = Color.yellow;
        text.alignment = TextAlignmentOptions.Center;
        text.enableWordWrapping = false;
    }

    private void Update()
    {
        if (fontSize >= 28 || fontSize <= 24) additionFactor = -additionFactor;
        fontSize += additionFactor;
        text.fontSize = fontSize;
    }
}