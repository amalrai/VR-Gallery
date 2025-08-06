using UnityEngine;
using System.Collections;

public class BrowserOpener : UnitySingleton<BrowserOpener>
{
    // check readme file to find out how to change title, colors etc.
    public void OnButtonClicked(string url, string title)
    {
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.displayURLAsPageTitle = false;
        options.pageTitle = title;
        Debug.Log(url);
        Debug.Log(title);
        InAppBrowser.OpenURL(url, options);
    }

    public void OnClearCacheClicked()
    {
        InAppBrowser.ClearCache();
    }
}
