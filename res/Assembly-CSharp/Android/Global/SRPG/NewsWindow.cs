// Decompiled with JetBrains decompiler
// Type: SRPG.NewsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NewsWindow : MonoBehaviour
  {
    private string uriWhatsnew = "html/whatsnew/";
    private string uriWhatsnewUrgency = "/html/whatsnew/emergencey.html";
    private readonly string offical_url = Network.OfficialUrl;
    private readonly string serialcode_url = "serial";
    private readonly string invitation_url = "invitation";
    private string[] allow_change_scenes = new string[13]{ "MENU_ARENA", "MENU_MULTI", "MENU_INN", "MENU_PARTY", "MENU_SHOP", "MENU_SHOP_TABI", "MENU_SHOP_KIMAGURE", "MENU_SHOP_MONOZUKI", "MENU_UNITLIST", "MENU_QUEST", "MENU_DAILY", "MENU_GACHA", "MENU_REPLAY" };
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    private string siteHost;
    public Button CloseButton;
    public int testCounter;

    private void Start()
    {
      this.siteHost = Network.SiteHost;
      Debug.Log((object) "[NewsWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if ((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null)
          this.CloseButton.interactable = true;
        Debug.Log((object) "[NewsWindow]Not WebView Enable");
      }
      else
      {
        Debug.Log((object) "[NewsWindow]WebView Enable");
        string str = Network.NewsHost + this.uriWhatsnew;
        if (NewsUtility.getNewsTypes() == NewsUtility.NewsTypes.Urgency)
        {
          str = Network.NewsHost + this.uriWhatsnewUrgency;
          NewsUtility.clearNewsType();
        }
        if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") != null)
        {
          if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "1")
          {
            FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
            str = this.siteHost + this.serialcode_url + "?fuid=" + MonoSingleton<GameManager>.Instance.Player.FUID;
          }
          else
          {
            if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "2")
            {
              FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
              Application.OpenURL(this.offical_url);
              return;
            }
            if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "3")
            {
              FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
              str = this.siteHost + this.invitation_url + "?fuid=" + MonoSingleton<GameManager>.Instance.Player.FUID;
            }
          }
        }
        this.mWebView = this.GetComponent<UniWebView>();
        if ((UnityEngine.Object) this.WebViewContainer != (UnityEngine.Object) null && (UnityEngine.Object) this.mWebView == (UnityEngine.Object) null)
        {
          Rect rect = this.WebViewContainer.rect;
          this.mWebView = new GameObject("UniWebView").AddComponent<UniWebView>();
          this.mWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
          this.mWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
          this.mWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
          this.mWebView.transform.SetParent((Transform) this.WebViewContainer, false);
          this.mWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
          if (GameUtility.Config_Language == "french")
            str += "?lang=fr";
          if (GameUtility.Config_Language == "german")
            str += "?lang=de";
          if (GameUtility.Config_Language == "spanish")
            str += "?lang=es";
          this.mWebView.url = str;
          this.mWebView.Load();
        }
        if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
          return;
        this.CloseButton.interactable = false;
      }
    }

    private void StartSceneChange(string new_scene)
    {
      foreach (string allowChangeScene in this.allow_change_scenes)
      {
        if (allowChangeScene == new_scene)
        {
          GameObject gameObject = GameObject.Find("Config_Home(Clone)");
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
          GlobalEvent.Invoke(new_scene, (object) this);
          break;
        }
      }
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
        Debug.LogError((object) ("Something wrong in webview loading: " + errorMessage));
      if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
        return;
      this.CloseButton.interactable = true;
    }

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
      if (string.Equals(message.path, "scene"))
      {
        this.StartSceneChange(message.args["name"]);
      }
      else
      {
        if (!string.Equals(message.path, "browser") || !(message.args.ContainsKey("protocol") | message.args.ContainsKey("url")))
          return;
        string str1 = message.rawMessage.Substring("uniwebview://".Length);
        string str2 = message.args["protocol"];
        int num = str1.IndexOf("url=");
        string str3 = str1.Substring(num + "url=".Length);
        string str4 = str2 + "://" + str3;
        Uri result;
        if (Uri.TryCreate(str4, UriKind.RelativeOrAbsolute, out result))
          Application.OpenURL(str4);
        else
          Debug.Log((object) ("This is not valid as URL: " + str1));
      }
    }
  }
}
