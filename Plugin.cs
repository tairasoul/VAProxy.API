using BepInEx;
using BepInEx.Logging;

namespace VAP_API
{
    [BepInPlugin("va.proxy.api", "VAPI", "1.1.0")]
    public sealed class Plugin: BaseUnityPlugin
    {
        // its currently just a bundle loader/file loader lmao
        // ill add more features eventually
        /// <summary>
        /// True after BundleAPI has loaded all bundles.
        /// </summary>
        public static bool isReady { get; private set; } = false;
        internal static ManualLogSource Log;
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

        internal void Init()
        {
            if (!isReady)
            {
                BundleLoader.Load();
                isReady = true;
            }
        }
    }
}
