// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeHelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeHelpWindow : MonoBehaviour
  {
    private readonly string url = "https://alchemistww.zendesk.com/hc/en-us/articles/115001377133-What-are-Challenges-";
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    public Button CloseButton;

    private void Start()
    {
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
          return;
        this.CloseButton.interactable = true;
      }
      else
      {
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
          this.mWebView.url = this.url;
          this.mWebView.Load();
        }
        if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
          return;
        this.CloseButton.interactable = false;
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
    }
  }
}
