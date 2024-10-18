// Decompiled with JetBrains decompiler
// Type: SRPG.WebviewWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class WebviewWindow : MonoBehaviour
  {
    private string termsOfUseURL = "http://gumi.sg/terms/";
    private string privacyPolicyURL = "https://gumi.sg/privacy-policy/";
    private string helpCenterURL = "https://alchemistww.zendesk.com/hc/";
    private string rankMatchURL = "/sections/360001976814-PvP";
    public WebviewWindow.URLType URL_Type;
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    public Button CloseButton;

    private void Start()
    {
      DebugUtility.Log("[WebviewWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if ((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null)
          this.CloseButton.interactable = true;
        DebugUtility.Log("[WebviewWindow]WebView not Enabled");
      }
      else
      {
        string str = string.Empty;
        switch (this.URL_Type)
        {
          case WebviewWindow.URLType.TermsOfUse:
            str = this.termsOfUseURL;
            break;
          case WebviewWindow.URLType.PrivacyPolicy:
            str = this.privacyPolicyURL;
            break;
          case WebviewWindow.URLType.HelpCenter:
            str = this.GetHelpCenterURL();
            break;
          case WebviewWindow.URLType.RankMatchInfo:
            str = this.GetHelpCenterURL() + this.rankMatchURL;
            break;
        }
        if (this.URL_Type == WebviewWindow.URLType.TermsOfUse || this.URL_Type == WebviewWindow.URLType.PrivacyPolicy)
        {
          if (GameUtility.Config_Language == "french")
            str += "?lang=fr";
          if (GameUtility.Config_Language == "german")
            str += "?lang=de";
          if (GameUtility.Config_Language == "spanish")
            str += "?lang=es";
        }
        DebugUtility.Log("[WebviewWindow]WebView opening " + str);
        this.mWebView = this.GetComponent<UniWebView>();
        if (!((UnityEngine.Object) this.WebViewContainer != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mWebView == (UnityEngine.Object) null))
          return;
        Rect rect = this.WebViewContainer.rect;
        this.mWebView = new GameObject("UniWebView").AddComponent<UniWebView>();
        this.mWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
        this.mWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
        this.mWebView.transform.SetParent((Transform) this.WebViewContainer, false);
        this.mWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
        this.mWebView.url = str;
        this.mWebView.Load();
      }
    }

    private string GetHelpCenterURL()
    {
      string helpCenterUrl = this.helpCenterURL;
      string configLanguage = GameUtility.Config_Language;
      if (configLanguage != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (WebviewWindow.\u003C\u003Ef__switch\u0024mapA == null)
        {
          // ISSUE: reference to a compiler-generated field
          WebviewWindow.\u003C\u003Ef__switch\u0024mapA = new Dictionary<string, int>(3)
          {
            {
              "french",
              0
            },
            {
              "german",
              1
            },
            {
              "spanish",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (WebviewWindow.\u003C\u003Ef__switch\u0024mapA.TryGetValue(configLanguage, out num))
        {
          switch (num)
          {
            case 0:
              return this.helpCenterURL + "fr";
            case 1:
              return this.helpCenterURL + "de";
            case 2:
              return this.helpCenterURL + "es";
          }
        }
      }
      return this.helpCenterURL + "en-us";
    }

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
      Vector3[] fourCornersArray = new Vector3[4];
      this.WebViewContainer.GetWorldCorners(fourCornersArray);
      float num1 = (float) ScreenUtility.DefaultScreenWidth / (float) Screen.width;
      float num2 = (float) ScreenUtility.DefaultScreenHeight / (float) Screen.height;
      int aLeft = (int) ((double) fourCornersArray[0].x * (double) num1);
      int aRight = (int) (((double) Screen.width - (double) fourCornersArray[2].x) * (double) num1);
      int aTop = (int) (((double) Screen.height - (double) fourCornersArray[1].y) * (double) num2);
      int aBottom = (int) ((double) fourCornersArray[0].y * (double) num2);
      return new UniWebViewEdgeInsets(aTop, aLeft, aBottom, aRight);
    }

    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
      if (success)
        this.mWebView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      else
        DebugUtility.LogError("Something wrong in webview loading: " + errorMessage);
      if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
        return;
      this.CloseButton.interactable = true;
    }

    public enum URLType
    {
      TermsOfUse,
      PrivacyPolicy,
      HelpCenter,
      RankMatchInfo,
    }
  }
}
