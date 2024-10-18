// Decompiled with JetBrains decompiler
// Type: UniWebViewInterface
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class UniWebViewInterface
{
  private static bool alreadyLoggedWarning;

  public static void SetLogLevel(int level) => UniWebViewInterface.CheckPlatform();

  public static void Init(string name, int x, int y, int width, int height)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void Destroy(string name) => UniWebViewInterface.CheckPlatform();

  public static void Load(string name, string url, bool skipEncoding, string readAccessURL)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void LoadHTMLString(string name, string html, string baseUrl, bool skipEncoding)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void Reload(string name) => UniWebViewInterface.CheckPlatform();

  public static void Stop(string name) => UniWebViewInterface.CheckPlatform();

  public static string GetUrl(string name)
  {
    UniWebViewInterface.CheckPlatform();
    return string.Empty;
  }

  public static void SetFrame(string name, int x, int y, int width, int height)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetPosition(string name, int x, int y) => UniWebViewInterface.CheckPlatform();

  public static void SetSize(string name, int width, int height)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static bool Show(string name, bool fade, int edge, float duration, string identifier)
  {
    UniWebViewInterface.CheckPlatform();
    return false;
  }

  public static bool Hide(string name, bool fade, int edge, float duration, string identifier)
  {
    UniWebViewInterface.CheckPlatform();
    return false;
  }

  public static bool AnimateTo(
    string name,
    int x,
    int y,
    int width,
    int height,
    float duration,
    float delay,
    string identifier)
  {
    UniWebViewInterface.CheckPlatform();
    return false;
  }

  public static void AddJavaScript(string name, string jsString, string identifier)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void EvaluateJavaScript(string name, string jsString, string identifier)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void AddUrlScheme(string name, string scheme)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void RemoveUrlScheme(string name, string scheme)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void AddSslExceptionDomain(string name, string domain)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void RemoveSslExceptionDomain(string name, string domain)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetHeaderField(string name, string key, string value)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetUserAgent(string name, string userAgent)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static string GetUserAgent(string name)
  {
    UniWebViewInterface.CheckPlatform();
    return string.Empty;
  }

  public static void SetAllowAutoPlay(bool flag) => UniWebViewInterface.CheckPlatform();

  public static void SetAllowInlinePlay(bool flag) => UniWebViewInterface.CheckPlatform();

  public static void SetAllowJavaScriptOpenWindow(bool flag) => UniWebViewInterface.CheckPlatform();

  public static void SetJavaScriptEnabled(bool flag) => UniWebViewInterface.CheckPlatform();

  public static void CleanCache(string name) => UniWebViewInterface.CheckPlatform();

  public static void ClearCookies() => UniWebViewInterface.CheckPlatform();

  public static void SetCookie(string url, string cookie, bool skipEncoding)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static string GetCookie(string url, string key, bool skipEncoding)
  {
    UniWebViewInterface.CheckPlatform();
    return string.Empty;
  }

  public static void ClearHttpAuthUsernamePassword(string host, string realm)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetBackgroundColor(string name, float r, float g, float b, float a)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetWebViewAlpha(string name, float alpha)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static float GetWebViewAlpha(string name)
  {
    UniWebViewInterface.CheckPlatform();
    return 0.0f;
  }

  public static void SetShowSpinnerWhileLoading(string name, bool show)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetSpinnerText(string name, string text)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static bool CanGoBack(string name)
  {
    UniWebViewInterface.CheckPlatform();
    return false;
  }

  public static bool CanGoForward(string name)
  {
    UniWebViewInterface.CheckPlatform();
    return false;
  }

  public static void GoBack(string name) => UniWebViewInterface.CheckPlatform();

  public static void GoForward(string name) => UniWebViewInterface.CheckPlatform();

  public static void SetOpenLinksInExternalBrowser(string name, bool flag)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetHorizontalScrollBarEnabled(string name, bool enabled)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetVerticalScrollBarEnabled(string name, bool enabled)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetBouncesEnabled(string name, bool enabled)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetZoomEnabled(string name, bool enabled)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetShowToolbar(
    string name,
    bool show,
    bool animated,
    bool onTop,
    bool adjustInset)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetToolbarDoneButtonText(string name, string text)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetWebContentsDebuggingEnabled(bool enabled)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetAllowHTTPAuthPopUpWindow(string name, bool flag)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void Print(string name) => UniWebViewInterface.CheckPlatform();

  public static void SetCalloutEnabled(string name, bool flag)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetSupportMultipleWindows(string name, bool flag)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void SetDragInteractionEnabled(string name, bool flag)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void ScrollTo(string name, int x, int y, bool animated)
  {
    UniWebViewInterface.CheckPlatform();
  }

  public static void CheckPlatform()
  {
    if (UniWebViewInterface.alreadyLoggedWarning)
      return;
    UniWebViewInterface.alreadyLoggedWarning = true;
    Debug.LogWarning((object) ("UniWebView only supports iOS/Android/macOS Editor. You current platform " + (object) Application.platform + " is not supported."));
  }
}
