using UnityEngine;

namespace BH.UI
{
    public delegate void NoArgDelegate();

    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = (T)this;

            if (!_animatedElementOverlay)
                _animatedElementOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        protected static void Open()
        {
            if (Instance == null)
                MenuManager.Instance.CreateInstance<T>();
            else
                Instance.gameObject.SetActive(true);

            MenuManager.Instance.OpenMenu(Instance);
        }

        protected static void Close(NoArgDelegate callback = null)
        {
            if (Instance == null)
            {
                Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
                return;
            }

            MenuManager.Instance.CloseMenu(Instance, callback);
        }

        public override void OnBackPressed()
        {
            Close();
        }
    }

    public abstract class Menu : MonoBehaviour
    {
        public UIAnimatedElementOverlay _animatedElementOverlay;

        [Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
        public bool DestroyWhenClosed = true;

        [Tooltip("Disable menus that are under this one in the stack")]
        public bool DisableMenusUnderneath = true;

        public abstract void OnBackPressed();

        protected virtual void OnValidate()
        {
            if (!_animatedElementOverlay)
                _animatedElementOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }
    }
}
