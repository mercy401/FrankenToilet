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
    // THIS IS THE BIB.
    public class Bib : MonoBehaviour {
        public static Bib Instance = null;

        public static Texture2D ConfettiTexture;
        public static Dictionary<string, AudioClip> Voices = new Dictionary<string, AudioClip>();

        public static string GetCurrentAssemblyPath() {
            string dllPath = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(dllPath);
        }


        public static Texture2D LoadEmbeddedTexture(string resourceName) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) {
                LogHelper.LogError($"Embedded resource '{resourceName}' not found");
                return null;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            Texture2D tex = new Texture2D(2, 2);
            if (tex.LoadImage(buffer)) {
                return tex;
            } else {
                LogHelper.LogError("Failed to load embedded texture");
                return null;
            }
        }

        public void ObtainVoice(string path) {
            StartCoroutine(LoadAudioFromFile(path));
        }

        public static IEnumerator LoadAudioFromFile(string path) {
            string fullPath = Path.Combine(Paths.PluginPath, "FrankenToilet/" + path);
            string url = "file://" + fullPath.Replace("\\", "/");
            
            using (var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS)) {
                yield return request.SendWebRequest();
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                string clipName = path.Split("/")[^1].Split(".")[0];
                clip.name = clipName;
                Bib.Voices[clipName] = clip;
            }
        }

        public void Start() {
            MegaAss();
        }
        
        public void MegaAss() {
            ConfettiTexture = LoadEmbeddedTexture("FrankenToilet.somebilly.CONFETTI.png");
            ObtainVoice("ENEMY_VOICE/CONFETTI_SOUND.ogg");

            // MACHINES
            ObtainVoice("ENEMY_VOICE/V1.ogg");
            ObtainVoice("ENEMY_VOICE/SWORDSMACHINE.ogg");
            ObtainVoice("ENEMY_VOICE/SWORDSMACHINE_AGONY.ogg");
            ObtainVoice("ENEMY_VOICE/SWORDSMACHINE_TUNDRA.ogg");
            ObtainVoice("ENEMY_VOICE/DRONE.ogg");
            ObtainVoice("ENEMY_VOICE/STREETCLEANER.ogg");
            ObtainVoice("ENEMY_VOICE/V2.ogg");
            ObtainVoice("ENEMY_VOICE/V2_2.ogg");
            ObtainVoice("ENEMY_VOICE/MINDFLAYER.ogg");
            ObtainVoice("ENEMY_VOICE/MINDFLAYER_IDOLED.ogg");
            ObtainVoice("ENEMY_VOICE/SENTRY.ogg");
            ObtainVoice("ENEMY_VOICE/GUTTERMAN.ogg");
            ObtainVoice("ENEMY_VOICE/GUTTERTANK.ogg");
            ObtainVoice("ENEMY_VOICE/LANDMINE.ogg");
            ObtainVoice("ENEMY_VOICE/EARTHMOVER.ogg");
            ObtainVoice("ENEMY_VOICE/EARTHMOVER_BRAIN.ogg");
            ObtainVoice("ENEMY_VOICE/ROCKET_LAUNCHER.ogg");
            ObtainVoice("ENEMY_VOICE/MORTAR.ogg");
            ObtainVoice("ENEMY_VOICE/TOWER.ogg");

            // HUSKS
            ObtainVoice("ENEMY_VOICE/FILTH.ogg");
            ObtainVoice("ENEMY_VOICE/STRAY.ogg");
            ObtainVoice("ENEMY_VOICE/SCHISM.ogg");
            ObtainVoice("ENEMY_VOICE/SOLDIER.ogg");
            ObtainVoice("ENEMY_VOICE/MINOS_CORPSE.ogg");
            ObtainVoice("ENEMY_VOICE/MINOS_CORPSE_HAND.ogg");
            ObtainVoice("ENEMY_VOICE/MINOS_CORPSE_LONG.ogg");
            ObtainVoice("ENEMY_VOICE/MINOS_CORPSE_PARASITE.ogg");
            ObtainVoice("ENEMY_VOICE/STALKER.ogg");
            ObtainVoice("ENEMY_VOICE/SISYPHEAN_INSURRECTIONIST.ogg");
            ObtainVoice("ENEMY_VOICE/SISYPHEAN_INSURRECTIONIST_ANGRY.ogg");
            ObtainVoice("ENEMY_VOICE/SISYPHEAN_INSURRECTIONIST_RUDE.ogg");
            ObtainVoice("ENEMY_VOICE/FERRYMAN.ogg");

            // DEMONS
            ObtainVoice("ENEMY_VOICE/MALICIOUS_FACE.ogg");
            ObtainVoice("ENEMY_VOICE/CERBERUS.ogg");
            ObtainVoice("ENEMY_VOICE/CERBERUS_BIG.ogg");
            ObtainVoice("ENEMY_VOICE/HIDEOUS_MASS.ogg");
            ObtainVoice("ENEMY_VOICE/IDOL.ogg");
            ObtainVoice("ENEMY_VOICE/LEVIATHAN_5-3.ogg");
            ObtainVoice("ENEMY_VOICE/LEVIATHAN_INTRO.ogg");
            ObtainVoice("ENEMY_VOICE/LEVIATHAN.ogg");
            ObtainVoice("ENEMY_VOICE/MANNEQUIN.ogg");
            ObtainVoice("ENEMY_VOICE/MINOTAUR.ogg");

            // ANGELS
            ObtainVoice("ENEMY_VOICE/GABRIEL.ogg");
            ObtainVoice("ENEMY_VOICE/GAGABRIEL.ogg");
            ObtainVoice("ENEMY_VOICE/VIRTUE.ogg");

            // OTHER
            ObtainVoice("ENEMY_VOICE/SOMETHING_WICKED.ogg");
            ObtainVoice("ENEMY_VOICE/CANCEROUS_RODENT.ogg");
            ObtainVoice("ENEMY_VOICE/VERY_CANCEROUS_RODENT.ogg");
            ObtainVoice("ENEMY_VOICE/MYSTERIOUS_DRUID_KNIGHT_AND_OWL.ogg");
            ObtainVoice("ENEMY_VOICE/BIG_JOHNINATOR.ogg");
            ObtainVoice("ENEMY_VOICE/FLESH_PRISON.ogg");
            ObtainVoice("ENEMY_VOICE/MINOS_PRIME.ogg");
            ObtainVoice("ENEMY_VOICE/FLESH_PANOPTICON.ogg");
            ObtainVoice("ENEMY_VOICE/SISYPHUS_PRIME.ogg");
            ObtainVoice("ENEMY_VOICE/EYE.ogg");
            ObtainVoice("ENEMY_VOICE/BIG_EYE.ogg");
            ObtainVoice("ENEMY_VOICE/MINI_MAURICE.ogg");
            ObtainVoice("ENEMY_VOICE/PUPPET.ogg");
            ObtainVoice("ENEMY_VOICE/MIRAGE.ogg");
            ObtainVoice("ENEMY_VOICE/GIANNI_MATRAGRANO.ogg");
        }

        // minotaur is added twice because
        public void AddAndPlayVoice(GameObject obj, AudioClip voice, float volume = 0.65f) {
            if (voice == null) {
                return;
            }

            if (Bib.CheckForVoiceObject(obj, voice.name)) {
                return;
            }

            GameObject voiceObject = new GameObject("FrankenVoiceObject");
            voiceObject.transform.SetParent(obj.transform);
            AudioSource audio = voiceObject.AddComponent<AudioSource>();
            audio.clip = voice;
            audio.volume = volume;
            audio.PlayDelayed(UnityEngine.Random.Range(0.0f, 0.2f));

            StartCoroutine(WaitAndMute(audio));
        }

        public static bool CheckForVoiceObject(GameObject obj, string voiceName) {
            Transform tryFind = obj.transform.Find("FrankenVoiceObject");
            if (tryFind == null) {
                return false;
            }
            AudioSource tryAudio = tryFind.GetComponent<AudioSource>();
            if (tryAudio == null) {
                return false;
            }
            if (tryAudio.clip.name == voiceName) {
                return true;
            }
            return false;
        }

        public static IEnumerator WaitAndMute(AudioSource audio) {
            while (audio && audio.isPlaying) {
                yield return null;
            }
            if (audio) {
                audio.mute = true;
            }
        }
    }
}