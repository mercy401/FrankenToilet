using FrankenToilet.Core;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;


namespace FrankenToilet.greycsont;

[PatchOnEntry]
[HarmonyPatch(typeof(PauseMenu))]
public static class PauseMenuPatch
{
    public static string farinthebluesky = "farinthebluesky";
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PauseMenu), nameof(PauseMenu.OnEnable))]
    public static void Postfix(PauseMenu __instance)
    {
        if (__instance.transform.Find(farinthebluesky) != null) return;
        var imgObj = new GameObject(farinthebluesky);
        
        imgObj.transform.SetParent(__instance.transform, false);
        
        imgObj.transform.SetAsLastSibling();
        
        
        var img = imgObj.AddComponent<Image>();
        var animator = imgObj.AddComponent<SpriteSheetAnimator>();
        animator.frames = AssetBundleController.farInTheBlueSky;
        img.SetNativeSize();
        
        
        
        var rect = imgObj.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        
        rect.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        
        rect.anchoredPosition = new Vector2(-400f, 0f);
    }
}


public class SpriteSheetAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 25f; // That's the fps for far in the blue sky only
    
    private Image _image;
    private int _frameIndex;
    private float _timer;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;
        
        _timer += Time.unscaledDeltaTime;

        if (_timer >= (1f / fps))
        {
            _timer = 0;
            _frameIndex = (_frameIndex + 1) % frames.Length;
            _image.sprite = frames[_frameIndex];
        }
    }
}