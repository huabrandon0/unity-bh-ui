namespace BH.UI
{
    /// <summary>
    /// A base menu class that implements parameterless Show and Hide methods
    /// </summary>
    public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T>
    {
        public static void Show(NoArgDelegate callback = null)
        {
            Open(callback);
        }

        public static void Hide(NoArgDelegate callback = null)
        {
            Close(callback);
        }
    }
}
