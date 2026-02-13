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
    public class Two : MonoBehaviour {
        public static float twoSpawnDistance = 300f;
        public static float twoSpawnHeight = 500f;

        // THIS IS THE TWO.
        public static void SpawnTwo() {
            string videoPath = Path.Combine(Bib.GetCurrentAssemblyPath(), "2.mp4");
            if (!File.Exists(videoPath)) {
                LogHelper.LogError("2.mp4 not found");
                return;
            }
            float twoSpawnDistance = 300f;
            float twoSpawnHeight = 500f;

            GameObject billboard = GameObject.CreatePrimitive(PrimitiveType.Quad);
            billboard.name = "FrankenTwo";
            billboard.transform.localScale = new Vector3(60, 60, 60);

            RenderTexture videoTexture = new RenderTexture(374, 210, 16);
            videoTexture.Create();
            
            VideoPlayer video = billboard.AddComponent<VideoPlayer>();
            video.url = videoPath;
            video.targetTexture = videoTexture;
            video.isLooping = true;
            video.playOnAwake = true;
            
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.color = new Color(1, 1, 1, 1);
            mat.mainTexture = videoTexture;
            MeshRenderer renderer = billboard.GetComponent<MeshRenderer>();
            renderer.material = mat;

            AlwaysLookAtCamera looker = billboard.AddComponent<AlwaysLookAtCamera>();
            looker.rotationOffset = new Vector3(180, 0, 180);
            UnityObject.Destroy(billboard.GetComponent<MeshCollider>());
            billboard.transform.position = GetPointOnCircle(NewMovement.Instance.transform.position, Two.twoSpawnDistance, Two.twoSpawnHeight);

            billboard.AddComponent<TwoFaller>();
        }

        public static Vector3 GetPointOnCircle(Vector3 center, float radius, float heightOffset) {
            float angleDeg = UnityEngine.Random.Range(0, 360);
            float angleRad = angleDeg * Mathf.Deg2Rad;
            return new Vector3(
                center.x + radius * Mathf.Cos(angleRad),
                center.y + heightOffset,
                center.z + radius * Mathf.Sin(angleRad)
            );
        }
    }

    public class TwoSpawner : MonoBehaviour {
        public float time = 1.25f;
        public float currentTime = 0f;

        void Update() {
            currentTime += Time.deltaTime;
            if (currentTime >= time) {
                currentTime = 0f;
                Two.SpawnTwo();
            }
        }
    }

    public class TwoFaller : MonoBehaviour {
        public float speed;
        public float fallenDistance = 0f;
        public float maxFallenDistance = 1500f;

        void Awake() {
            speed = UnityEngine.Random.Range(30, 90);
        }

        void Update() {
            float fallDistance = speed * Time.deltaTime;
            Vector3 pos = this.transform.position;
            this.transform.position = new Vector3(pos.x, pos.y - fallDistance, pos.z);
            fallenDistance += fallDistance;
            if (fallenDistance >= maxFallenDistance) {
                UnityObject.Destroy(this.gameObject);
            }
        }
    }
}