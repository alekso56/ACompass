using System;
using BepInEx;
using System.IO;
using GameNetcodeStuff;
using HarmonyLib;
using LC_API.BundleAPI;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Properties.TypeUtility;
using static UnityEngine.GraphicsBuffer;
using Object = UnityEngine.Object;

namespace Compass.Patches
{
    [HarmonyPatch]
    internal class Patches
    {
        static CompassUpdater updater;
        [HarmonyPatch(typeof(HUDManager), "Awake")]
        [HarmonyPostfix]
        public static void AddCompass(HUDManager __instance)
        {
            Transform main = __instance.HUDContainer.transform;
            Debug.Log("Attaching compass to :"+main);
            GameObject obj = BundleLoader.GetLoadedAsset<GameObject>("assets/compass/mask.prefab");
            if (obj == null)
            {
                string dir1 = Path.Combine(Paths.PluginPath, "alekso56-ACompass", "compass");
                LoadedAssetBundle bundle = BundleLoader.LoadAssetBundle(dir1, false);
                obj = bundle.GetAsset<GameObject>("assets/compass/mask.prefab");
            }
            obj = Object.Instantiate<GameObject>(obj, main);
            obj.transform.SetParent(main.transform, false);
            Debug.Log("Compass attached, setting target.");
            updater = obj.AddComponent<CompassUpdater>();
            Debug.Log("Updater loaded");
            updater.CompassImage = obj.transform.GetChild(0).GetComponentInChildren<RawImage>();
        }
    }
}
