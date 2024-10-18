// Decompiled with JetBrains decompiler
// Type: UniWebViewNativeListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UniWebViewNativeListener : MonoBehaviour
{
  private static Dictionary<string, UniWebViewNativeListener> listeners = new Dictionary<string, UniWebViewNativeListener>();
  [HideInInspector]
  public UniWebView webView;

  public static void AddListener(UniWebViewNativeListener target)
  {
    UniWebViewNativeListener.listeners.Add(target.Name, target);
  }

  public static void RemoveListener(string name) => UniWebViewNativeListener.listeners.Remove(name);

  public static UniWebViewNativeListener GetListener(string name)
  {
    UniWebViewNativeListener viewNativeListener = (UniWebViewNativeListener) null;
    return UniWebViewNativeListener.listeners.TryGetValue(name, out viewNativeListener) ? viewNativeListener : (UniWebViewNativeListener) null;
  }

  public string Name => ((Object) ((Component) this).gameObject).name;

  public void PageStarted(string url)
  {
    UniWebViewLogger.Instance.Info("Page Started Event. Url: " + url);
    this.webView.InternalOnPageStarted(url);
  }

  public void PageFinished(string result)
  {
    UniWebViewLogger.Instance.Info("Page Finished Event. Url: " + result);
    this.webView.InternalOnPageFinished(JsonUtility.FromJson<UniWebViewNativeResultPayload>(result));
  }

  public void PageErrorReceived(string result)
  {
    UniWebViewLogger.Instance.Info("Page Error Received Event. Result: " + result);
    this.webView.InternalOnPageErrorReceived(JsonUtility.FromJson<UniWebViewNativeResultPayload>(result));
  }

  public void ShowTransitionFinished(string identifer)
  {
    UniWebViewLogger.Instance.Info("Show Transition Finished Event. Identifier: " + identifer);
    this.webView.InternalOnShowTransitionFinished(identifer);
  }

  public void HideTransitionFinished(string identifer)
  {
    UniWebViewLogger.Instance.Info("Hide Transition Finished Event. Identifier: " + identifer);
    this.webView.InternalOnHideTransitionFinished(identifer);
  }

  public void AnimateToFinished(string identifer)
  {
    UniWebViewLogger.Instance.Info("Animate To Finished Event. Identifier: " + identifer);
    this.webView.InternalOnAnimateToFinished(identifer);
  }

  public void AddJavaScriptFinished(string result)
  {
    UniWebViewLogger.Instance.Info("Add JavaScript Finished Event. Result: " + result);
    this.webView.InternalOnAddJavaScriptFinished(JsonUtility.FromJson<UniWebViewNativeResultPayload>(result));
  }

  public void EvalJavaScriptFinished(string result)
  {
    UniWebViewLogger.Instance.Info("Eval JavaScript Finished Event. Result: " + result);
    this.webView.InternalOnEvalJavaScriptFinished(JsonUtility.FromJson<UniWebViewNativeResultPayload>(result));
  }

  public void MessageReceived(string result)
  {
    UniWebViewLogger.Instance.Info("Message Received Event. Result: " + result);
    this.webView.InternalOnMessageReceived(result);
  }

  public void WebViewKeyDown(string keyCode)
  {
    UniWebViewLogger.Instance.Info("Web View Key Down: " + keyCode);
    int result;
    if (int.TryParse(keyCode, out result))
      this.webView.InternalOnWebViewKeyDown(result);
    else
      UniWebViewLogger.Instance.Critical("Failed in converting key code: " + keyCode);
  }

  public void WebViewDone(string param)
  {
    UniWebViewLogger.Instance.Info("Web View Done Event.");
    this.webView.InternalOnShouldClose();
  }

  public void WebContentProcessDidTerminate(string param)
  {
    UniWebViewLogger.Instance.Info("Web Content Process Terminate Event.");
    this.webView.InternalWebContentProcessDidTerminate();
  }
}
