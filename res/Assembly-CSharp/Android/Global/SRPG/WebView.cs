// Decompiled with JetBrains decompiler
// Type: SRPG.WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class WebView : MonoBehaviour
  {
    public Text Text_Title;
    public Button Btn_Close;
    public RawImage ClientArea;
    public UIUtility.DialogResultEvent OnClose;
    private UniWebView uniWebView;

    public void BeginClose()
    {
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.gameObject);
    }

    private void OnClickButton(GameObject obj)
    {
      if ((UnityEngine.Object) obj == (UnityEngine.Object) this.Btn_Close.gameObject && (UnityEngine.Object) this.Btn_Close != (UnityEngine.Object) null)
        this.OnClose(this.gameObject);
      this.BeginClose();
    }

    public void SetTitleText(string text)
    {
      if (!((UnityEngine.Object) this.Text_Title != (UnityEngine.Object) null))
        return;
      this.Text_Title.text = text;
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.Btn_Close != (UnityEngine.Object) null))
        return;
      UIUtility.AddEventListener(this.Btn_Close.gameObject, (UnityEvent) this.Btn_Close.onClick, new UIUtility.EventListener(this.OnClickButton));
    }

    public void SetHeaderField(string key, string value)
    {
      this.uniWebView = this.gameObject.GetComponent<UniWebView>();
      if ((UnityEngine.Object) this.uniWebView == (UnityEngine.Object) null)
        this.uniWebView = this.gameObject.AddComponent<UniWebView>();
      this.uniWebView.SetHeaderField(key, value);
    }

    public void OpenURL(string url)
    {
      if (GameUtility.Config_Language == "french")
        url += "?lang=fr";
      if (GameUtility.Config_Language == "german")
        url += "?lang=de";
      if (GameUtility.Config_Language == "spanish")
        url += "?lang=es";
      this.uniWebView = this.gameObject.GetComponent<UniWebView>();
      if ((UnityEngine.Object) this.uniWebView == (UnityEngine.Object) null)
        this.uniWebView = this.gameObject.AddComponent<UniWebView>();
      this.uniWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
      this.uniWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
      this.uniWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      this.uniWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
      this.uniWebView.SetShowSpinnerWhenLoading(true);
      this.uniWebView.url = url;
      this.uniWebView.Load();
    }

    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
      if (success)
        webView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      else
        Debug.Log((object) ("Something wrong in webview loading: " + errorMessage));
    }

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
      if (!(message.scheme == "uniwebview") || !string.Equals(message.path, "browser") || !(message.args.ContainsKey("protocol") | message.args.ContainsKey("url")))
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

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
      Vector3[] fourCornersArray = new Vector3[4];
      this.ClientArea.GetComponent<RectTransform>().GetWorldCorners(fourCornersArray);
      float num1 = (float) ScreenUtility.DefaultScreenWidth / (float) Screen.width;
      float num2 = (float) ScreenUtility.DefaultScreenHeight / (float) Screen.height;
      int aLeft = (int) ((double) fourCornersArray[0].x * (double) num1);
      int aRight = (int) (((double) Screen.width - (double) fourCornersArray[2].x) * (double) num1);
      int aTop = (int) (((double) Screen.height - (double) fourCornersArray[1].y) * (double) num2);
      int aBottom = (int) ((double) fourCornersArray[0].y * (double) num2);
      return new UniWebViewEdgeInsets(aTop, aLeft, aBottom, aRight);
    }
  }
}
