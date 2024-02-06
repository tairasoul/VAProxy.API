using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Object = UnityEngine.Object;
using VAP_API.Extensions;

namespace VAP_API.UIUtils
{
    public class ComponentUtils
    {
        /// <summary>
        /// Get a font within the game.
        /// </summary>
        /// <param name="name">Name of font as shown in UnityExplorer.</param>
        public static Font GetFont(string name)
        {
            Object[] fonts = Object.FindObjectsOfTypeAll(typeof(Font));
            foreach (Font font in fonts.Cast<Font>())
            {
                if (font.name == name) return font;
            }
            return null;
        }

        /// <summary>
        /// Create a toggle element.
        /// </summary>
        /// <param name="display">The text displayed to the user.</param>
        /// <param name="id">The name to give the toggle object.</param>
        public static GameObject CreateToggle(string display, string id)
        {
            GameObject Toggle = new(id);
            Toggle.AddComponent<RectTransform>();
            Toggle.transform.localScale = new Vector3(2.1268f, 2.1268f, 2.1268f);
            Toggle toggle = Toggle.AddComponent<Toggle>();
            GameObject Label = Toggle.AddObject("Label");
            Label.AddComponent<CanvasRenderer>();
            Label.AddComponent<RectTransform>();
            Label.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            Text text = Label.AddComponent<Text>();
            text.alignment = TextAnchor.MiddleLeft;
            text.fontSize = 100;
            //GameObject original = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Resolution/VSync/Background");
            text.font = GetFont("Orbitron-Bold");
            text.text = display;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.color = new Color(0.9843f, 0.6902f, 0.2314f);
            GameObject background = Toggle.AddObject("Background");
            background.AddComponent<RectTransform>().anchoredPosition = new Vector2(10, 0);
            background.AddComponent<CanvasRenderer>();
            background.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            Image image = background.AddComponent<Image>();
            Sprite sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/ComponentAssets/Nuke Panel Sprite.png"), new Rect(0, 0, 32, 32), new Vector2(16, 16));
            Plugin.Log.LogInfo(sprite);
            image.sprite = sprite;//original.GetComponent<Image>().sprite;
            GameObject checkmark = background.AddObject("Checkmark");
            checkmark.AddComponent<RectTransform>();
            checkmark.AddComponent<CanvasRenderer>();
            Image checkmarkImage = checkmark.AddComponent<Image>();
            Sprite checkmarkSprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/ComponentAssets/Checkmark.png"), new Rect(0, 0, 32, 32), new Vector2(16, 16));
            checkmarkImage.enabled = false;
            toggle.image = checkmarkImage;
            checkmarkImage.sprite = checkmarkSprite;
            toggle.onValueChanged.AddListener((bool active) =>
            {
                checkmarkImage.enabled = active;
            });
            return Toggle;
        }

        /// <summary>
        /// Create a button element.
        /// </summary>
        /// <param name="display">The text displayed to the user.</param>
        /// <param name="id">The name to give the button object.</param>

        public static GameObject CreateButton(string display, string id)
        {
            GameObject Button = new(id);
            Button.AddComponent<RectTransform>();
            Button.AddComponent<CanvasRenderer>();
            Image img = Button.AddComponent<Image>();
            img.type = Image.Type.Simple;
            img.color = new Color(1, 1, 1, 0.0588f);
            Button button = Button.AddComponent<Button>();
            button.image = img;
            button.transition = Selectable.Transition.ColorTint;
            GameObject name = Button.AddObject("ItemName");
            RectTransform transform = name.AddComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(65, 0);
            name.AddComponent<CanvasRenderer>();
            Text text = name.AddComponent<Text>();
            text.text = display;
            text.alignment = TextAnchor.MiddleLeft;
            text.font = GetFont("Orbitron-Regular");
            text.fontSize = 20;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.resizeTextMaxSize = 40;
            text.resizeTextMinSize = 2;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            /*GameObject Settings = GameObject.Find("MAINMENU/Canvas/Pages/Setting");
            GameObject Container = Settings.Find("Content/GameObject");
            GameObject Audio = Container.Find("audio");
            GameObject Button = Audio.Instantiate();
            Button.name = id;
            GameObject ItemName = Button.Find("ItemName");
            Text text = ItemName.GetComponent<Text>();
            text.text = display;*/
            return Button;
        }

        /// <summary>
        /// Create a slider element.
        /// </summary>
        /// <param name="display">The text displayed to the user.</param>
        /// <param name="id">The name to give the slider object.</param>

        public static GameObject CreateSlider(string display, string id)
        {
            /*GameObject Settings = GameObject.Find("MAINMENU/Canvas/Pages/Setting");
            GameObject Sensitivity = Settings.Find("AUDIO/SFX");
            GameObject Slider = Sensitivity.Instantiate();
            Slider.name = id;
            Slider.GetComponent<Text>().text = display;*/
            GameObject Slider = new(id);
            Slider.AddComponent<RectTransform>();
            Slider.AddComponent<CanvasRenderer>();
            Text text = Slider.AddComponent<Text>();
            text.alignment = TextAnchor.MiddleLeft;
            text.font = GetFont("Orbitron-Medium");
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.fontSize = 20;
            text.text = display;
            text.resizeTextMaxSize = 40;
            text.resizeTextMinSize = 1;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            GameObject SliderObj = Slider.AddObject("Slider");
            SliderObj.AddComponent<RectTransform>().anchoredPosition = new Vector2(-146.4058f, -36.39f);
            GameObject background = SliderObj.AddObject("Background");
            background.AddComponent<RectTransform>();
            background.AddComponent<CanvasRenderer>();
            Image bg = background.AddComponent<Image>();
            bg.sprite = Sprite.Create(Plugin.Spritemap, new Rect(310, 939, 68, 68), new Vector2(34, 34));
            bg.type = Image.Type.Sliced;
            GameObject FillArea = Slider.AddObject("Fill Area");
            FillArea.AddComponent<RectTransform>().anchoredPosition = new Vector2(-5, 0);
            GameObject Fill = FillArea.AddObject("Fill");
            Fill.AddComponent<RectTransform>();
            Fill.AddComponent<CanvasRenderer>();
            Image fill = Fill.AddComponent<Image>();
            fill.sprite = Sprite.Create(Plugin.Spritemap, new Rect(310, 939, 68, 68), new Vector2(34, 34));
            GameObject HandleSlide = Slider.AddObject("Handle Slide Area");
            HandleSlide.AddComponent<RectTransform>();
            GameObject Handle = HandleSlide.AddObject("Handle");
            Handle.AddComponent<RectTransform>();
            Handle.AddComponent<CanvasRenderer>();
            Image handle = Handle.AddComponent<Image>();
            handle.sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/ComponentAssets/Seperator_bar.png"), new Rect(0, 0, 17, 20), new Vector2(8.5f, 10));
            Slider slider = SliderObj.AddComponent<Slider>();
            slider.direction = UnityEngine.UI.Slider.Direction.LeftToRight;
            slider.fillRect = Fill.GetComponent<RectTransform>();
            slider.handleRect = Handle.GetComponent<RectTransform>();
            slider.transition = Selectable.Transition.ColorTint;
            return Slider;
        }
    }
}
