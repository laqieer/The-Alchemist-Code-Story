// Decompiled with JetBrains decompiler
// Type: UniWebDemo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UniWebDemo : MonoBehaviour
{
  public GameObject cubePrefab;
  public TextMesh tipTextMesh;
  private UniWebView _webView;
  private string _errorMessage;
  private GameObject _cube;
  private Vector3 _moveVector;

  private void Start()
  {
    this._cube = UnityEngine.Object.Instantiate<GameObject>(this.cubePrefab);
    this._cube.GetComponent<UniWebViewCube>().webViewDemo = this;
    this._moveVector = Vector3.zero;
  }

  private void Update()
  {
    if (!((UnityEngine.Object) this._cube != (UnityEngine.Object) null))
      return;
    if ((double) this._cube.transform.position.y < -5.0)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this._cube);
      this._cube = (GameObject) null;
    }
    else
      this._cube.transform.Translate(this._moveVector * Time.deltaTime);
  }

  private void OnGUI()
  {
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height - 150), 150f, 80f), "Open"))
    {
      this._webView = this.GetComponent<UniWebView>();
      if ((UnityEngine.Object) this._webView == (UnityEngine.Object) null)
      {
        this._webView = this.gameObject.AddComponent<UniWebView>();
        this._webView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
        this._webView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
        this._webView.OnWebViewShouldClose += new UniWebView.WebViewShouldCloseDelegate(this.OnWebViewShouldClose);
        this._webView.OnEvalJavaScriptFinished += new UniWebView.EvalJavaScriptFinishedDelegate(this.OnEvalJavaScriptFinished);
        this._webView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      }
      this._webView.url = "http://uniwebview.onevcat.com/demo/index1-1.html";
      this._webView.Load();
      this._errorMessage = (string) null;
    }
    if ((UnityEngine.Object) this._webView != (UnityEngine.Object) null && GUI.Button(new Rect(150f, (float) (Screen.height - 150), 150f, 80f), "Back"))
      this._webView.GoBack();
    if ((UnityEngine.Object) this._webView != (UnityEngine.Object) null && GUI.Button(new Rect(300f, (float) (Screen.height - 150), 150f, 80f), "ToolBar"))
    {
      if (this._webView.toolBarShow)
        this._webView.HideToolBar(true);
      else
        this._webView.ShowToolBar(true);
    }
    if (this._errorMessage == null)
      return;
    GUI.Label(new Rect(0.0f, 0.0f, (float) Screen.width, 80f), this._errorMessage);
  }

  private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
  {
    if (success)
    {
      webView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
    }
    else
    {
      Debug.Log((object) ("Something wrong in webview loading: " + errorMessage));
      this._errorMessage = errorMessage;
    }
  }

  private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
  {
    Debug.Log((object) "Received a message from native");
    Debug.Log((object) message.rawMessage);
    if (string.Equals(message.path, "move"))
    {
      Vector3 vector3 = Vector3.zero;
      if (string.Equals(message.args["direction"], "up"))
        vector3 = new Vector3(0.0f, 0.0f, 1f);
      else if (string.Equals(message.args["direction"], "down"))
        vector3 = new Vector3(0.0f, 0.0f, -1f);
      else if (string.Equals(message.args["direction"], "left"))
        vector3 = new Vector3(-1f, 0.0f, 0.0f);
      else if (string.Equals(message.args["direction"], "right"))
        vector3 = new Vector3(1f, 0.0f, 0.0f);
      int result = 0;
      if (int.TryParse(message.args["distance"], out result))
        vector3 *= (float) result;
      this._moveVector = vector3;
    }
    else if (string.Equals(message.path, "add"))
    {
      if ((UnityEngine.Object) this._cube != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this._cube);
      this._cube = UnityEngine.Object.Instantiate<GameObject>(this.cubePrefab);
      this._cube.GetComponent<UniWebViewCube>().webViewDemo = this;
      this._moveVector = Vector3.zero;
    }
    else
    {
      if (!string.Equals(message.path, "close"))
        return;
      webView.Hide(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      UnityEngine.Object.Destroy((UnityEngine.Object) webView);
      webView.OnReceivedMessage -= new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
      webView.OnLoadComplete -= new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
      webView.OnWebViewShouldClose -= new UniWebView.WebViewShouldCloseDelegate(this.OnWebViewShouldClose);
      webView.OnEvalJavaScriptFinished -= new UniWebView.EvalJavaScriptFinishedDelegate(this.OnEvalJavaScriptFinished);
      webView.InsetsForScreenOreitation -= new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      this._webView = (UniWebView) null;
    }
  }

  public void ShowAlertInWebview(float time, bool first)
  {
    this._moveVector = Vector3.zero;
    if (!first)
      return;
    this._webView.EvaluatingJavaScript("sample(" + (object) time + ")");
  }

  private void OnEvalJavaScriptFinished(UniWebView webView, string result)
  {
    Debug.Log((object) ("js result: " + result));
    this.tipTextMesh.text = "<color=#000000>" + result + "</color>";
  }

  private bool OnWebViewShouldClose(UniWebView webView)
  {
    if (!((UnityEngine.Object) webView == (UnityEngine.Object) this._webView))
      return false;
    this._webView = (UniWebView) null;
    return true;
  }

  private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
  {
    int aBottom = (int) ((double) UniWebViewHelper.screenHeight * 0.5);
    if (orientation == UniWebViewOrientation.Portrait)
      return new UniWebViewEdgeInsets(5, 5, aBottom, 5);
    return new UniWebViewEdgeInsets(5, 5, aBottom, 5);
  }
}
