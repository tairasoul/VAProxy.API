using BepInEx;
using BepInEx.Logging;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace VAP_API
{
    [BepInPlugin("va.proxy.api", "VAPI", "1.3.0")]
    public sealed class Plugin: BaseUnityPlugin
    {
        // its currently just a bundle loader/file loader lmao
        // ill add more features eventually
        /// <summary>
        /// True after BundleAPI has loaded all bundles.
        /// </summary>
        public static bool isReady { get; private set; } = false;
        internal static ManualLogSource Log;
        public static Texture2D Spritemap;
        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo("VAPI awake.");
        }

        internal void Start()
        {
            Init();
        }

        internal void OnDestroy()
        {
            Init();
        }
        private static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        }

        internal void Init()
        {
            if (!isReady)
            {
                BundleLoader.Load();
                isReady = true;
                using Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyName() + ".ui.textures.bundle");
                BundleLoader.LoadFromStream(resource);
                BundleLoader.LoadComplete += () =>
                {
                    Spritemap = BundleLoader.GetLoadedAsset<Texture2D>("assets/ComponentAssets/Spritemap.png");
                };
            }
        }
    }
}
