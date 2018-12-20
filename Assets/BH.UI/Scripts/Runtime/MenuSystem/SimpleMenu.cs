namespace BH.UI
{
    /// <summary>
    /// A base menu class that implements parameterless Show and Hide methods
    /// </summary>
    public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T>
    {
        public static void Show(bool animate = true, NoArgDelegate callback = null)
        {
            Open(animate, callback);
        }

        public static bool Hide(bool animate = true, NoArgDelegate callback = null)
        {
            return Close(animate, callback);
        }
    }
}
