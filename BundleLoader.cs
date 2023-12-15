using BepInEx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VAP_API
{
    public class BundleLoader
    {
        public static bool hasLoadedAssets = false;

        public static Dictionary<string, UnityEngine.Object> assets = new Dictionary<string, UnityEngine.Object>();

        public static event Action LoadComplete = completed;
        internal static void Load()
        {
            Plugin.Log.LogMessage("[BundleLoader] Loading bundles."); 
            string[] invalidEndings = { ".dll", ".json", ".png", ".md", ".old", ".txt", ".exe", ".lem" };
            string bundleDir = Path.Combine(Paths.BepInExRootPath, "plugins");
            string[] bundles = Directory.GetFiles(bundleDir, "*", SearchOption.AllDirectories).Where(file => !invalidEndings.Any(ending => file.EndsWith(ending, StringComparison.CurrentCultureIgnoreCase))).ToArray();

            byte[] bundleStart = Encoding.ASCII.GetBytes("UnityFS");

            List<string> properBundles = new List<string>();

            foreach (string path in bundles)
            {
                byte[] buffer = new byte[bundleStart.Length];

                using (FileStream fs = File.Open(path, FileMode.Open))
                {
                    fs.Read(buffer, 0, buffer.Length);
                }

                if (buffer.SequenceEqual(bundleStart))
                {
                    properBundles.Add(path);
                }
            }

            bundles = properBundles.ToArray();

            if (bundles.Length == 0)
            {
                Plugin.Log.LogMessage("[BundleLoader] No bundles to load from BepInEx/plugins.");
            }
            else
            {
                LoadFromDir(bundles);
            }
            LoadFromDir(bundles);
            LoadComplete.Invoke();
        }

        private static void LoadFromDir(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    SaveAsset(array[i]);
                }
                catch (Exception e)
                {
                    Plugin.Log.LogError($"[BundleLoader] Failed to load AssetBundle {array[i]}");
                }
            }
        }

        private static void SaveAsset(string path)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            try
            {
                string[] assetPaths = bundle.GetAllAssetNames();
                foreach (string assetPath in assetPaths)
                {
                    Plugin.Log.LogInfo("[BundleLoader] Got asset " + assetPath);
                    UnityEngine.Object loadedAsset = bundle.LoadAsset(assetPath);
                    if (loadedAsset == null)
                    {
                        Plugin.Log.LogWarning($"[BundleLoader] Failed to load asset {assetPath} from bundle {bundle.name}");
                        continue;
                    }

                    if (assets.ContainsKey(assetPath))
                    {
                        Plugin.Log.LogError($"[BundleLoader] found duplicate asset {assetPath}");
                        return;
                    }
                    assets.Add(assetPath, loadedAsset);
                    Plugin.Log.LogInfo($"[BundleLoader] Loaded asset {loadedAsset.name}");
                }
            }
            finally
            {
                bundle?.Unload(false);
            }
        }

        /// <summary>
        /// Get an asset from loaded assets.
        /// Return could be null if asset doesn't exist.
        /// </summary>

        public static  TAsset GetLoadedAsset<TAsset>(string path) where TAsset : UnityEngine.Object
        {
            assets.TryGetValue(path.ToLower(), out UnityEngine.Object asset);
            if (asset == null)
            {
                return null;
            }
            return (TAsset)asset;
        }

        public static void completed()
        {
            Plugin.Log.LogInfo("Completed loading of assets.");
        }
    }
}
