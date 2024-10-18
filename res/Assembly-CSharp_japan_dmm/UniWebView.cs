// Decompiled with JetBrains decompiler
// Type: UniWebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UniWebView : MonoBehaviour
{
  private string id = Guid.NewGuid().ToString();
  private UniWebViewNativeListener listener;
  private bool isPortrait;
  [SerializeField]
  private string urlOnStart;
  [SerializeField]
  private bool showOnStart;
  [SerializeField]
  private bool fullScreen;
  [SerializeField]
  private bool useToolbar;
  [SerializeField]
  private UniWebViewToolbarPosition toolbarPosition;
  private Dictionary<string, Action> actions = new Dictionary<string, Action>();
  private Dictionary<string, Action<UniWebViewNativeResultPayload>> payloadActions = new Dictionary<string, Action<UniWebViewNativeResultPayload>>();
  [SerializeField]
  private Rect frame;
  [SerializeField]
  private RectTransform referenceRectTransform;
  private bool started;
  private Color backgroundColor = Color.white;

  public event UniWebView.PageStartedDelegate OnPageStarted;

  public event UniWebView.PageFinishedDelegate OnPageFinished;

  public event UniWebView.PageErrorReceivedDelegate OnPageErrorReceived;

  public event UniWebView.MessageReceivedDelegate OnMessageReceived;

  public event UniWebView.ShouldCloseDelegate OnShouldClose;

  public event UniWebView.KeyCodeReceivedDelegate OnKeyCodeReceived;

  public event UniWebView.OrientationChangedDelegate OnOrientationChanged;

  public event UniWebView.OnWebContentProcessTerminatedDelegate OnWebContentProcessTerminated;

  public Rect Frame
  {
    get => this.frame;
    set
    {
      this.frame = value;
      this.UpdateFrame();
    }
  }

  public RectTransform ReferenceRectTransform
  {
    get => this.referenceRectTransform;
    set
    {
      this.referenceRectTransform = value;
      this.UpdateFrame();
    }
  }

  public string Url => UniWebViewInterface.GetUrl(this.listener.Name);

  public void UpdateFrame()
  {
    Rect rect = this.NextFrameRect();
    UniWebViewInterface.SetFrame(this.listener.Name, (int) ((Rect) ref rect).x, (int) ((Rect) ref rect).y, (int) ((Rect) ref rect).width, (int) ((Rect) ref rect).height);
  }

  private Rect NextFrameRect()
  {
    if (Object.op_Equality((Object) this.referenceRectTransform, (Object) null))
    {
      UniWebViewLogger.Instance.Info("Using Frame setting to determine web view frame.");
      return this.frame;
    }
    UniWebViewLogger.Instance.Info("Using reference RectTransform to determine web view frame.");
    Vector3[] vector3Array = new Vector3[4];
    this.referenceRectTransform.GetWorldCorners(vector3Array);
    Vector3 vector3_1 = vector3Array[0];
    Vector3 screenPoint1 = vector3Array[1];
    Vector3 vector3_2 = vector3Array[2];
    Vector3 screenPoint2 = vector3Array[3];
    Canvas componentInParent = ((Component) this.referenceRectTransform).GetComponentInParent<Canvas>();
    if (Object.op_Equality((Object) componentInParent, (Object) null))
      return this.frame;
    RenderMode renderMode = componentInParent.renderMode;
    if (renderMode != null && (renderMode == 1 || renderMode == 2))
    {
      Camera worldCamera = componentInParent.worldCamera;
      if (Object.op_Equality((Object) worldCamera, (Object) null))
      {
        UniWebViewLogger.Instance.Critical("You need a render camera \n                        or event camera to use RectTransform to determine correct \n                        frame for UniWebView.");
        UniWebViewLogger.Instance.Info("No camera found. Fall back to ScreenSpaceOverlay mode.");
      }
      else
      {
        worldCamera.WorldToScreenPoint(vector3_1);
        screenPoint1 = worldCamera.WorldToScreenPoint(screenPoint1);
        worldCamera.WorldToScreenPoint(vector3_2);
        screenPoint2 = worldCamera.WorldToScreenPoint(screenPoint2);
      }
    }
    return new Rect(screenPoint1.x, (float) Screen.height - screenPoint1.y, screenPoint2.x - screenPoint1.x, screenPoint1.y - screenPoint2.y);
  }

  private void Awake()
  {
    GameObject gameObject = new GameObject(this.id);
    this.listener = gameObject.AddComponent<UniWebViewNativeListener>();
    gameObject.transform.parent = ((Component) this).transform;
    this.listener.webView = this;
    UniWebViewNativeListener.AddListener(this.listener);
    Rect rect;
    if (this.fullScreen)
    {
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
    }
    else
      rect = this.NextFrameRect();
    UniWebViewInterface.Init(this.listener.Name, (int) ((Rect) ref rect).x, (int) ((Rect) ref rect).y, (int) ((Rect) ref rect).width, (int) ((Rect) ref rect).height);
    this.isPortrait = Screen.height >= Screen.width;
  }

  private void Start()
  {
    if (this.showOnStart)
      this.Show();
    if (!string.IsNullOrEmpty(this.urlOnStart))
      this.Load(this.urlOnStart);
    this.started = true;
    if (!Object.op_Inequality((Object) this.referenceRectTransform, (Object) null))
      return;
    this.UpdateFrame();
  }

  private void Update()
  {
    bool flag = Screen.height >= Screen.width;
    if (this.isPortrait == flag)
      return;
    this.isPortrait = flag;
    if (this.OnOrientationChanged != null)
      this.OnOrientationChanged(this, !this.isPortrait ? (ScreenOrientation) 3 : (ScreenOrientation) 1);
    if (!Object.op_Inequality((Object) this.referenceRectTransform, (Object) null))
      return;
    this.UpdateFrame();
  }

  private void OnEnable()
  {
    if (!this.started)
      return;
    this.Show();
  }

  private void OnDisable()
  {
    if (!this.started)
      return;
    this.Hide();
  }

  public void Load(string url, bool skipEncoding = false, string readAccessURL = null)
  {
    UniWebViewInterface.Load(this.listener.Name, url, skipEncoding, readAccessURL);
  }

  public void LoadHTMLString(string htmlString, string baseUrl, bool skipEncoding = false)
  {
    UniWebViewInterface.LoadHTMLString(this.listener.Name, htmlString, baseUrl, skipEncoding);
  }

  public void Reload() => UniWebViewInterface.Reload(this.listener.Name);

  public void Stop() => UniWebViewInterface.Stop(this.listener.Name);

  public bool CanGoBack => UniWebViewInterface.CanGoBack(this.listener.Name);

  public bool CanGoForward => UniWebViewInterface.CanGoForward(this.listener.Name);

  public void GoBack() => UniWebViewInterface.GoBack(this.listener.Name);

  public void GoForward() => UniWebViewInterface.GoForward(this.listener.Name);

  public void SetOpenLinksInExternalBrowser(bool flag)
  {
    UniWebViewInterface.SetOpenLinksInExternalBrowser(this.listener.Name, flag);
  }

  public bool Show(
    bool fade = false,
    UniWebViewTransitionEdge edge = UniWebViewTransitionEdge.None,
    float duration = 0.4f,
    Action completionHandler = null)
  {
    string str = Guid.NewGuid().ToString();
    bool flag = UniWebViewInterface.Show(this.listener.Name, fade, (int) edge, duration, str);
    if (flag && completionHandler != null)
    {
      if (fade || edge != UniWebViewTransitionEdge.None)
        this.actions.Add(str, completionHandler);
      else
        completionHandler();
    }
    if (flag && this.useToolbar)
      this.SetShowToolbar(true, onTop: this.toolbarPosition == UniWebViewToolbarPosition.Top, adjustInset: this.fullScreen);
    return flag;
  }

  public bool Hide(
    bool fade = false,
    UniWebViewTransitionEdge edge = UniWebViewTransitionEdge.None,
    float duration = 0.4f,
    Action completionHandler = null)
  {
    string str = Guid.NewGuid().ToString();
    bool flag = UniWebViewInterface.Hide(this.listener.Name, fade, (int) edge, duration, str);
    if (flag && completionHandler != null)
    {
      if (fade || edge != UniWebViewTransitionEdge.None)
        this.actions.Add(str, completionHandler);
      else
        completionHandler();
    }
    if (flag && this.useToolbar)
      this.SetShowToolbar(false, onTop: this.toolbarPosition == UniWebViewToolbarPosition.Top, adjustInset: this.fullScreen);
    return flag;
  }

  public bool AnimateTo(Rect frame, float duration, float delay = 0.0f, Action completionHandler = null)
  {
    string str = Guid.NewGuid().ToString();
    bool flag = UniWebViewInterface.AnimateTo(this.listener.Name, (int) ((Rect) ref frame).x, (int) ((Rect) ref frame).y, (int) ((Rect) ref frame).width, (int) ((Rect) ref frame).height, duration, delay, str);
    if (flag)
    {
      this.frame = frame;
      if (completionHandler != null)
        this.actions.Add(str, completionHandler);
    }
    return flag;
  }

  public void AddJavaScript(
    string jsString,
    Action<UniWebViewNativeResultPayload> completionHandler = null)
  {
    string str = Guid.NewGuid().ToString();
    UniWebViewInterface.AddJavaScript(this.listener.Name, jsString, str);
    if (completionHandler == null)
      return;
    this.payloadActions.Add(str, completionHandler);
  }

  public void EvaluateJavaScript(
    string jsString,
    Action<UniWebViewNativeResultPayload> completionHandler = null)
  {
    string str = Guid.NewGuid().ToString();
    UniWebViewInterface.EvaluateJavaScript(this.listener.Name, jsString, str);
    if (completionHandler == null)
      return;
    this.payloadActions.Add(str, completionHandler);
  }

  public void AddUrlScheme(string scheme)
  {
    if (scheme == null)
      UniWebViewLogger.Instance.Critical("The scheme should not be null.");
    else if (scheme.Contains("://"))
      UniWebViewLogger.Instance.Critical("The scheme should not include invalid characters '://'");
    else
      UniWebViewInterface.AddUrlScheme(this.listener.Name, scheme);
  }

  public void RemoveUrlScheme(string scheme)
  {
    if (scheme == null)
      UniWebViewLogger.Instance.Critical("The scheme should not be null.");
    else if (scheme.Contains("://"))
      UniWebViewLogger.Instance.Critical("The scheme should not include invalid characters '://'");
    else
      UniWebViewInterface.RemoveUrlScheme(this.listener.Name, scheme);
  }

  public void AddSslExceptionDomain(string domain)
  {
    if (domain == null)
      UniWebViewLogger.Instance.Critical("The domain should not be null.");
    else if (domain.Contains("://"))
      UniWebViewLogger.Instance.Critical("The domain should not include invalid characters '://'");
    else
      UniWebViewInterface.AddSslExceptionDomain(this.listener.Name, domain);
  }

  public void RemoveSslExceptionDomain(string domain)
  {
    if (domain == null)
      UniWebViewLogger.Instance.Critical("The domain should not be null.");
    else if (domain.Contains("://"))
      UniWebViewLogger.Instance.Critical("The domain should not include invalid characters '://'");
    else
      UniWebViewInterface.RemoveSslExceptionDomain(this.listener.Name, domain);
  }

  public void SetHeaderField(string key, string value)
  {
    if (key == null)
      UniWebViewLogger.Instance.Critical("Header key should not be null.");
    else
      UniWebViewInterface.SetHeaderField(this.listener.Name, key, value);
  }

  public void SetUserAgent(string agent)
  {
    UniWebViewInterface.SetUserAgent(this.listener.Name, agent);
  }

  public string GetUserAgent() => UniWebViewInterface.GetUserAgent(this.listener.Name);

  public void SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior behavior)
  {
  }

  public static void SetAllowAutoPlay(bool flag) => UniWebViewInterface.SetAllowAutoPlay(flag);

  public static void SetAllowInlinePlay(bool flag)
  {
  }

  public static void SetJavaScriptEnabled(bool enabled)
  {
    UniWebViewInterface.SetJavaScriptEnabled(enabled);
  }

  public static void SetAllowJavaScriptOpenWindow(bool flag)
  {
    UniWebViewInterface.SetAllowJavaScriptOpenWindow(flag);
  }

  public void CleanCache() => UniWebViewInterface.CleanCache(this.listener.Name);

  public static void ClearCookies() => UniWebViewInterface.ClearCookies();

  public static void SetCookie(string url, string cookie, bool skipEncoding = false)
  {
    UniWebViewInterface.SetCookie(url, cookie, skipEncoding);
  }

  public static string GetCookie(string url, string key, bool skipEncoding = false)
  {
    return UniWebViewInterface.GetCookie(url, key, skipEncoding);
  }

  public static void ClearHttpAuthUsernamePassword(string host, string realm)
  {
    UniWebViewInterface.ClearHttpAuthUsernamePassword(host, realm);
  }

  public Color BackgroundColor
  {
    get => this.backgroundColor;
    set
    {
      this.backgroundColor = value;
      UniWebViewInterface.SetBackgroundColor(this.listener.Name, value.r, value.g, value.b, value.a);
    }
  }

  public float Alpha
  {
    get => UniWebViewInterface.GetWebViewAlpha(this.listener.Name);
    set => UniWebViewInterface.SetWebViewAlpha(this.listener.Name, value);
  }

  public void SetShowSpinnerWhileLoading(bool flag)
  {
    UniWebViewInterface.SetShowSpinnerWhileLoading(this.listener.Name, flag);
  }

  public void SetSpinnerText(string text)
  {
    UniWebViewInterface.SetSpinnerText(this.listener.Name, text);
  }

  public void SetHorizontalScrollBarEnabled(bool enabled)
  {
    UniWebViewInterface.SetHorizontalScrollBarEnabled(this.listener.Name, enabled);
  }

  public void SetVerticalScrollBarEnabled(bool enabled)
  {
    UniWebViewInterface.SetVerticalScrollBarEnabled(this.listener.Name, enabled);
  }

  public void SetBouncesEnabled(bool enabled)
  {
    UniWebViewInterface.SetBouncesEnabled(this.listener.Name, enabled);
  }

  public void SetZoomEnabled(bool enabled)
  {
    UniWebViewInterface.SetZoomEnabled(this.listener.Name, enabled);
  }

  public void AddPermissionTrustDomain(string domain)
  {
  }

  public void RemovePermissionTrustDomain(string domain)
  {
  }

  public void SetBackButtonEnabled(bool enabled)
  {
  }

  public void SetUseWideViewPort(bool flag)
  {
  }

  public void SetLoadWithOverviewMode(bool flag)
  {
  }

  public void SetImmersiveModeEnabled(bool enabled)
  {
  }

  public void SetShowToolbar(bool show, bool animated = false, bool onTop = true, bool adjustInset = false)
  {
  }

  public void SetToolbarDoneButtonText(string text)
  {
  }

  public static void SetWebContentsDebuggingEnabled(bool enabled)
  {
    UniWebViewInterface.SetWebContentsDebuggingEnabled(enabled);
  }

  public void SetWindowUserResizeEnabled(bool enabled)
  {
  }

  public void GetHTMLContent(Action<string> handler)
  {
    this.EvaluateJavaScript("document.documentElement.outerHTML", (Action<UniWebViewNativeResultPayload>) (payload =>
    {
      if (handler == null)
        return;
      handler(payload.data);
    }));
  }

  public void SetAllowFileAccessFromFileURLs(bool flag)
  {
  }

  public void SetAllowHTTPAuthPopUpWindow(bool flag)
  {
    UniWebViewInterface.SetAllowHTTPAuthPopUpWindow(this.listener.Name, flag);
  }

  public void SetCalloutEnabled(bool enabled)
  {
    UniWebViewInterface.SetCalloutEnabled(this.listener.Name, enabled);
  }

  public void SetSupportMultipleWindows(bool enabled)
  {
    UniWebViewInterface.SetSupportMultipleWindows(this.listener.Name, enabled);
  }

  public void SetDragInteractionEnabled(bool enabled)
  {
  }

  public void Print() => UniWebViewInterface.Print(this.listener.Name);

  public void ScrollTo(int x, int y, bool animated)
  {
    UniWebViewInterface.ScrollTo(this.listener.Name, x, y, animated);
  }

  private void OnDestroy()
  {
    UniWebViewNativeListener.RemoveListener(this.listener.Name);
    UniWebViewInterface.Destroy(this.listener.Name);
    Object.Destroy((Object) ((Component) this.listener).gameObject);
  }

  private void OnApplicationPause(bool pause)
  {
  }

  internal void InternalOnShowTransitionFinished(string identifier)
  {
    Action action;
    if (!this.actions.TryGetValue(identifier, out action))
      return;
    action();
    this.actions.Remove(identifier);
  }

  internal void InternalOnHideTransitionFinished(string identifier)
  {
    Action action;
    if (!this.actions.TryGetValue(identifier, out action))
      return;
    action();
    this.actions.Remove(identifier);
  }

  internal void InternalOnAnimateToFinished(string identifier)
  {
    Action action;
    if (!this.actions.TryGetValue(identifier, out action))
      return;
    action();
    this.actions.Remove(identifier);
  }

  internal void InternalOnAddJavaScriptFinished(UniWebViewNativeResultPayload payload)
  {
    string identifier = payload.identifier;
    Action<UniWebViewNativeResultPayload> action;
    if (!this.payloadActions.TryGetValue(identifier, out action))
      return;
    action(payload);
    this.payloadActions.Remove(identifier);
  }

  internal void InternalOnEvalJavaScriptFinished(UniWebViewNativeResultPayload payload)
  {
    string identifier = payload.identifier;
    Action<UniWebViewNativeResultPayload> action;
    if (!this.payloadActions.TryGetValue(identifier, out action))
      return;
    action(payload);
    this.payloadActions.Remove(identifier);
  }

  internal void InternalOnPageFinished(UniWebViewNativeResultPayload payload)
  {
    if (this.OnPageFinished == null)
      return;
    int result = -1;
    if (int.TryParse(payload.resultCode, out result))
      this.OnPageFinished(this, result, payload.data);
    else
      UniWebViewLogger.Instance.Critical("Invalid status code received: " + payload.resultCode);
  }

  internal void InternalOnPageStarted(string url)
  {
    if (this.OnPageStarted == null)
      return;
    this.OnPageStarted(this, url);
  }

  internal void InternalOnPageErrorReceived(UniWebViewNativeResultPayload payload)
  {
    if (this.OnPageErrorReceived == null)
      return;
    int result = -1;
    if (int.TryParse(payload.resultCode, out result))
      this.OnPageErrorReceived(this, result, payload.data);
    else
      UniWebViewLogger.Instance.Critical("Invalid error code received: " + payload.resultCode);
  }

  internal void InternalOnMessageReceived(string result)
  {
    UniWebViewMessage message = new UniWebViewMessage(result);
    if (this.OnMessageReceived == null)
      return;
    this.OnMessageReceived(this, message);
  }

  internal void InternalOnWebViewKeyDown(int keyCode)
  {
    if (this.OnKeyCodeReceived == null)
      return;
    this.OnKeyCodeReceived(this, keyCode);
  }

  internal void InternalOnShouldClose()
  {
    if (this.OnShouldClose != null)
    {
      if (!this.OnShouldClose(this))
        return;
      Object.Destroy((Object) this);
    }
    else
      Object.Destroy((Object) this);
  }

  internal void InternalWebContentProcessDidTerminate()
  {
    if (this.OnWebContentProcessTerminated == null)
      return;
    this.OnWebContentProcessTerminated(this);
  }

  [Obsolete("OnOreintationChanged is a typo and deprecated. Use `OnOrientationChanged` instead.", true)]
  public event UniWebView.OrientationChangedDelegate OnOreintationChanged;

  public delegate void PageStartedDelegate(UniWebView webView, string url);

  public delegate void PageFinishedDelegate(UniWebView webView, int statusCode, string url);

  public delegate void PageErrorReceivedDelegate(
    UniWebView webView,
    int errorCode,
    string errorMessage);

  public delegate void MessageReceivedDelegate(UniWebView webView, UniWebViewMessage message);

  public delegate bool ShouldCloseDelegate(UniWebView webView);

  public delegate void KeyCodeReceivedDelegate(UniWebView webView, int keyCode);

  public delegate void OrientationChangedDelegate(UniWebView webView, ScreenOrientation orientation);

  public delegate void OnWebContentProcessTerminatedDelegate(UniWebView webView);

  [Obsolete("OreintationChangedDelegate is a typo and deprecated. Use `OrientationChangedDelegate` instead.", true)]
  public delegate void OreintationChangedDelegate(UniWebView webView, ScreenOrientation orientation);
}
