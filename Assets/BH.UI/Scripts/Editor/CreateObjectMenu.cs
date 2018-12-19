using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

namespace BH.UI
{
    public static class CreateObjectMenu
    {
        [MenuItem("GameObject/UI/BH.UI - UIImage")]
        static void CreateUIImage(MenuCommand menuCommand)
        {
            // Check if there is a Canvas in the scene.
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                // Create new Canvas since none exists in the scene.
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                // Add a Canvas Scaler Component.
                canvas.gameObject.AddComponent<CanvasScaler>();

                // Add a Graphic Raycaster Component.
                canvas.gameObject.AddComponent<GraphicRaycaster>();

                Undo.RegisterCreatedObjectUndo(canvasObject, "Create " + canvasObject.name);
            }

            GameObject image = new GameObject("BH.UI - UIImage");
            image.AddComponent<RectTransform>();
            Selection.activeObject = image;
            UIImage imageUIImage = image.AddComponent(typeof(UIImage)) as UIImage;

            Undo.RegisterCreatedObjectUndo(image, "Create " + image.name);

            GameObject imageImage = new GameObject("AnimatedImage");
            imageImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(imageImage, image);
            imageImage.AddComponent(typeof(Image));
            imageUIImage._imageAnimator = imageImage.AddComponent(typeof(UIImageAnimator)) as UIImageAnimator;
            imageUIImage._rectTransformAnimator = imageUIImage._imageAnimator;

            Undo.RegisterCreatedObjectUndo(imageImage, "Create " + imageImage.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(image, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(image, contextObject);

            // Check if an event system already exists in the scene.
            if (!Object.FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }

        [MenuItem("GameObject/UI/BH.UI - UITMProText")]
        static void CreateUIText(MenuCommand menuCommand)
        {
            // Check if there is a Canvas in the scene.
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                // Create new Canvas since none exists in the scene.
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                // Add a Canvas Scaler Component.
                canvas.gameObject.AddComponent<CanvasScaler>();

                // Add a Graphic Raycaster Component.
                canvas.gameObject.AddComponent<GraphicRaycaster>();

                Undo.RegisterCreatedObjectUndo(canvasObject, "Create " + canvasObject.name);
            }

            GameObject text = new GameObject("BH.UI - UITMProText");
            text.AddComponent<RectTransform>();
            Selection.activeObject = text;
            UITMProText textUIText = text.AddComponent(typeof(UITMProText)) as UITMProText;

            Undo.RegisterCreatedObjectUndo(text, "Create " + text.name);

            GameObject textText = new GameObject("AnimatedText");
            textText.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(textText, text);
            textText.AddComponent(typeof(TextMeshProUGUI));
            textUIText._textAnimator = textText.AddComponent(typeof(UITMProTextAnimator)) as UITMProTextAnimator;
            textUIText._rectTransformAnimator = textUIText._textAnimator;

            Undo.RegisterCreatedObjectUndo(textText, "Create " + textText.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(text, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(text, contextObject);

            // Check if an event system already exists in the scene.
            if (!Object.FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }


        [MenuItem("GameObject/UI/BH.UI - UIButton")]
        static void CreateUIButton(MenuCommand menuCommand)
        {
            // Check if there is a Canvas in the scene.
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                // Create new Canvas since none exists in the scene.
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                // Add a Canvas Scaler Component.
                canvas.gameObject.AddComponent<CanvasScaler>();

                // Add a Graphic Raycaster Component.
                canvas.gameObject.AddComponent<GraphicRaycaster>();

                Undo.RegisterCreatedObjectUndo(canvasObject, "Create " + canvasObject.name);
            }

            GameObject button = new GameObject("BH.UI - UIButton");
            button.AddComponent<RectTransform>();
            Selection.activeObject = button;
            UIButton buttonUIButton = button.AddComponent(typeof(UIButton)) as UIButton;

            Undo.RegisterCreatedObjectUndo(button, "Create " + button.name);

            GameObject raycastImage = new GameObject("RaycastImage");
            raycastImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(raycastImage, button);
            Image raycastImageImage = raycastImage.AddComponent(typeof(Image)) as Image;
            raycastImageImage.color = Color.clear;

            Undo.RegisterCreatedObjectUndo(raycastImage, "Create " + raycastImage.name);

            GameObject animatedRectTransform = new GameObject("AnimatedRectTransform");
            animatedRectTransform.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedRectTransform, button);
            buttonUIButton._buttonRectTransformAnimator = animatedRectTransform.AddComponent(typeof(UIRectTransformAnimator)) as UIRectTransformAnimator;

            Undo.RegisterCreatedObjectUndo(animatedRectTransform, "Create " + animatedRectTransform.name);

            GameObject animatedImage = new GameObject("AnimatedImage");
            animatedImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedImage, animatedRectTransform);
            Image animatedImageImage = animatedImage.AddComponent(typeof(Image)) as Image;
            animatedImageImage.raycastTarget = false;
            buttonUIButton._buttonImageAnimator = animatedImage.AddComponent(typeof(UIImageAnimator)) as UIImageAnimator;

            Undo.RegisterCreatedObjectUndo(animatedImage, "Create " + animatedImage.name);

            GameObject animatedText = new GameObject("AnimatedText");
            animatedText.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedText, animatedRectTransform);
            TextMeshProUGUI animatedTextText = animatedText.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            animatedTextText.raycastTarget = false;
            buttonUIButton._buttonTextAnimator = animatedText.AddComponent(typeof(UITMProTextAnimator)) as UITMProTextAnimator;

            Undo.RegisterCreatedObjectUndo(animatedText, "Create " + animatedText.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(button, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(button, contextObject);

            // Check if an event system already exists in the scene.
            if (!Object.FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }
    }
}
