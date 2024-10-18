// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.App.NetworkHelper;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("System/WebView", 32741)]
  [FlowNode.Pin(100, "Create", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Destroy", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "Preload", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Created", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Destroyed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_WebView : FlowNode
  {
    private const int PIN_ID_CREATE = 100;
    private const int PIN_ID_DESTROY = 101;
    private const int PIN_ID_PRELOAD = 102;
    private const int PIN_ID_CREATED = 1;
    private const int PIN_ID_DESTROYED = 2;
    private const string PREFAB_PATH = "UI/WebView";
    public string Title;
    public string URL;
    public bool usegAuth = true;
    public bool useVariable;
    public bool resetVariable;
    private WebView webView;
    private string originalURL;
    public FlowNode_WebView.URL_Mode URLMode;

    private void Create()
    {
      GameObject gameObject = AssetManager.Load<GameObject>("UI/WebView");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        this.webView = UnityEngine.Object.Instantiate<GameObject>(gameObject).GetComponent<WebView>();
        if (this.useVariable)
        {
          if (this.resetVariable)
            this.originalURL = this.URL;
          string str1 = FlowNode_Variable.Get(this.URL);
          if (!string.IsNullOrEmpty(str1))
            this.URL = str1;
          string str2 = FlowNode_Variable.Get(this.Title);
          if (!string.IsNullOrEmpty(str2))
            this.Title = str2;
        }
        this.webView.OnClose = new UIUtility.DialogResultEvent(this.OnClose);
        this.webView.Text_Title.text = LocalizedText.Get(this.Title);
        if (this.usegAuth)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
          foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            this.webView.SetHeaderField(keyValuePair.Key, keyValuePair.Value);
        }
        if (this.URL.StartsWith("http://") || this.URL.StartsWith("https://"))
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(this.URL);
          this.webView.OpenURL(this.URL);
        }
        else if (this.URLMode == FlowNode_WebView.URL_Mode.APIHost)
          this.webView.OpenURL(Network.Host + this.URL);
        else if (this.URLMode == FlowNode_WebView.URL_Mode.SiteHost)
          this.webView.OpenURL(Network.SiteHost + this.URL);
        else if (this.URLMode == FlowNode_WebView.URL_Mode.NewsHost)
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(Network.NewsHost + this.URL);
          this.webView.OpenURL(Network.NewsHost + this.URL);
        }
        if (!this.useVariable || !this.resetVariable)
          return;
        this.URL = this.originalURL;
      }
      else
        Debug.Log((object) "Prefab not Found");
    }

    public void OnClose(GameObject go) => this.ActivateOutputLinks(2);

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.Create();
          this.ActivateOutputLinks(1);
          break;
        case 101:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.webView, (UnityEngine.Object) null))
            this.webView.BeginClose();
          this.ActivateOutputLinks(2);
          break;
      }
    }

    public enum URL_Mode
    {
      APIHost,
      SiteHost,
      NewsHost,
    }
  }
}
