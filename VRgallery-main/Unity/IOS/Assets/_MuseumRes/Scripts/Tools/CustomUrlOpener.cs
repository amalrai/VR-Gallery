using UnityEngine;
using System.Runtime.InteropServices;

public static class CustomUrlOpener
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void CustomOpenUrl(string url);
#endif

    public static void Open(string url)
    {
#if UNITY_IOS && !UNITY_EDITOR
        CustomOpenUrl(url);
#else
        Application.OpenURL(url);
#endif
    }
}
