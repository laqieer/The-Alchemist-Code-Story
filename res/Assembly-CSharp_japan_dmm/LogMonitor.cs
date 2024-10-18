// Decompiled with JetBrains decompiler
// Type: LogMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

#nullable disable
[AddComponentMenu("")]
public class LogMonitor : MonoBehaviour
{
  public static LogMonitor mInstnace;
  private List<LogMonitor.Log> mLogs = new List<LogMonitor.Log>();
  private int mLogCount;
  private GUIStyle mBackgroundStyle;
  private GUIStyle mErrorStyle;
  private GUIStyle mExceptionStyle;
  private GUIStyle mStackTraceStyle;
  private string mStackTrace;
  private Texture2D mErrorStyle_BackgroundNormal;
  private Texture2D mErrorStyle_BackgroundHover;
  private Texture2D mStackTraceStyle_BackgroundNormal;
  private Texture2D mExceptionStyle_BackgroundNormal;
  private Texture2D mExceptionStyle_BackgroundHover;
  private Texture2D mBackgroundStyle_BackgroundNormal;
  private LogMonitor.GUICallback mCallback;
  private bool mDisp;
  private bool mSending;

  public static void Start()
  {
    if (!GameUtility.IsDebugBuild || !UnityEngine.Object.op_Equality((UnityEngine.Object) LogMonitor.mInstnace, (UnityEngine.Object) null))
      return;
    LogMonitor.mInstnace = new GameObject(nameof (LogMonitor)).AddComponent<LogMonitor>();
  }

  public static LogMonitor Instance => LogMonitor.mInstnace;

  public bool isDisp => this.mDisp;

  public bool IsSend => this.mSending;

  private void Awake()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) this).gameObject);
    ((UnityEngine.Object) this).hideFlags = (HideFlags) 61;
    this.SetDisp(true);
  }

  public void SetDisp(bool value) => this.mDisp = value;

  private void OnEnable()
  {
    GameUtility.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
  }

  private void OnDisable()
  {
    GameUtility.UnregisterLogCallback(new Application.LogCallback(this.HandleLog));
  }

  private void OnGUICallback()
  {
    if (this.mLogs.Count <= 0 || !this.mDisp)
      return;
    if (this.mErrorStyle == null)
    {
      this.mErrorStyle_BackgroundNormal = new Texture2D(1, 1);
      this.mErrorStyle_BackgroundHover = new Texture2D(1, 1);
      this.mErrorStyle = new GUIStyle(GUI.skin.label);
      this.mErrorStyle.contentOffset = Vector2.op_Multiply(Vector2.right, 5f);
      this.mErrorStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mErrorStyle.fontSize = 10;
      this.mErrorStyle.normal = new GUIStyleState();
      this.mErrorStyle.normal.textColor = Color.white;
      this.mErrorStyle.normal.background = this.mErrorStyle_BackgroundNormal;
      this.mErrorStyle.normal.background.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f));
      this.mErrorStyle.normal.background.Apply();
      this.mErrorStyle.hover = new GUIStyleState();
      this.mErrorStyle.hover.textColor = Color.white;
      this.mErrorStyle.hover.background = this.mErrorStyle_BackgroundHover;
      this.mErrorStyle.hover.background.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
      this.mErrorStyle.hover.background.Apply();
    }
    if (this.mStackTraceStyle == null)
    {
      this.mStackTraceStyle_BackgroundNormal = new Texture2D(1, 1);
      this.mStackTraceStyle = new GUIStyle(GUI.skin.label);
      this.mStackTraceStyle.contentOffset = Vector2.op_Multiply(Vector2.right, 5f);
      this.mStackTraceStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mStackTraceStyle.fontSize = 10;
      this.mStackTraceStyle.normal = new GUIStyleState();
      this.mStackTraceStyle.normal.textColor = Color.white;
      this.mStackTraceStyle.normal.background = this.mStackTraceStyle_BackgroundNormal;
      this.mStackTraceStyle.normal.background.SetPixel(0, 0, new Color(0.0f, 0.0f, 1f));
      this.mStackTraceStyle.normal.background.Apply();
    }
    if (this.mExceptionStyle == null)
    {
      this.mExceptionStyle_BackgroundNormal = new Texture2D(1, 1);
      this.mExceptionStyle_BackgroundHover = new Texture2D(1, 1);
      this.mExceptionStyle = new GUIStyle(GUI.skin.label);
      this.mExceptionStyle.contentOffset = Vector2.op_Multiply(Vector2.right, 5f);
      this.mExceptionStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mExceptionStyle.fontSize = 10;
      this.mExceptionStyle.normal = new GUIStyleState();
      this.mExceptionStyle.normal.textColor = Color.yellow;
      this.mExceptionStyle.normal.background = this.mExceptionStyle_BackgroundNormal;
      this.mExceptionStyle.normal.background.SetPixel(0, 0, new Color(0.5f, 0.0f, 0.0f));
      this.mExceptionStyle.normal.background.Apply();
      this.mExceptionStyle.hover = new GUIStyleState();
      this.mExceptionStyle.hover.textColor = Color.yellow;
      this.mExceptionStyle.hover.background = this.mExceptionStyle_BackgroundHover;
      this.mExceptionStyle.hover.background.SetPixel(0, 0, new Color(0.6f, 0.0f, 0.0f));
      this.mExceptionStyle.hover.background.Apply();
    }
    if (this.mBackgroundStyle == null)
    {
      this.mBackgroundStyle_BackgroundNormal = new Texture2D(1, 1);
      this.mBackgroundStyle = new GUIStyle(GUI.skin.label);
      this.mBackgroundStyle.stretchWidth = true;
      this.mBackgroundStyle.stretchHeight = true;
      this.mBackgroundStyle.normal.background = this.mBackgroundStyle_BackgroundNormal;
      this.mBackgroundStyle.normal.background.SetPixel(0, 0, Color.black);
      this.mBackgroundStyle.normal.background.Apply();
      this.mBackgroundStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mBackgroundStyle.padding = this.mBackgroundStyle.margin;
    }
    float width = (float) Screen.width;
    float num = 0.0f;
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) ((Rect) ref safeArea).width < (double) width)
    {
      width = ((Rect) ref safeArea).width;
      num = ((Rect) ref safeArea).x;
    }
    GUI.Box(new Rect(num, 0.0f, width, 30f), string.Empty, this.mBackgroundStyle);
    if (GUI.Button(new Rect((float) ((double) num + (double) width - 30.0), 0.0f, 30f, 30f), "X"))
    {
      this.Clear();
    }
    else
    {
      GUILayout.BeginArea(new Rect(num, 0.0f, width - 30f, (float) Screen.height));
      GUILayout.BeginVertical(new GUILayoutOption[0]);
      for (int index = 0; index < this.mLogs.Count; ++index)
      {
        if (GUILayout.Button("#" + (object) this.mLogs[index].index + " " + this.mLogs[index].logString, this.mLogs[index].type != null ? this.mExceptionStyle : this.mErrorStyle, new GUILayoutOption[1]
        {
          GUILayout.Width(width - 30f)
        }))
        {
          this.mStackTrace = this.mLogs[index].stackTrace;
          this.mSending = false;
        }
      }
      if (!string.IsNullOrEmpty(this.mStackTrace))
      {
        GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
        {
          GUILayout.Height(4f)
        });
        GUILayout.Label(this.mStackTrace, this.mStackTraceStyle, new GUILayoutOption[1]
        {
          GUILayout.Width(width - 30f)
        });
      }
      GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
      {
        GUILayout.Height(8f)
      });
      GUILayout.BeginHorizontal(new GUILayoutOption[0]);
      for (int index = 0; index < this.mLogs.Count; ++index)
      {
        if (GUILayout.Button("#" + (object) this.mLogs[index].index, new GUILayoutOption[2]
        {
          GUILayout.Height(40f),
          GUILayout.Width(40f)
        }))
          this.mStackTrace = this.mLogs[index].stackTrace;
      }
      if (!string.IsNullOrEmpty(this.mStackTrace))
      {
        GUI.enabled = !this.mSending;
        if (GUILayout.Button("Slackに\nスクリーンショット送信", new GUILayoutOption[2]
        {
          GUILayout.Width(160f),
          GUILayout.Height(40f)
        }))
          this.SendSlack(LogMonitor.SendType.SCREENSHOT, (string) null, (Action) null, (Action) null);
        GUI.enabled = true;
      }
      GUILayout.EndHorizontal();
      GUILayout.EndVertical();
      GUILayout.EndArea();
    }
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != 4 && type != null || type == null && logString.StartsWith("Asynchronous Background loading is only supported in Unity Pro."))
      return;
    LogMonitor.Log log;
    if (this.mLogs.Count > 5)
    {
      log = this.mLogs[0];
      this.mLogs.RemoveAt(0);
    }
    else
      log = new LogMonitor.Log();
    log.type = type;
    log.logString = logString;
    log.stackTrace = stackTrace;
    log.index = ++this.mLogCount;
    this.mLogs.Add(log);
    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCallback, (UnityEngine.Object) null))
      return;
    this.mCallback = new GameObject("callback", new System.Type[2]
    {
      typeof (GameObject),
      typeof (LogMonitor.GUICallback)
    }).GetComponent<LogMonitor.GUICallback>();
    this.mCallback.OnGUIListener = new LogMonitor.GUICallback.GUIEvent(this.OnGUICallback);
    ((Component) this.mCallback).transform.SetParent(((Component) this).transform, false);
  }

  public void Clear()
  {
    this.mLogs.Clear();
    this.mStackTrace = (string) null;
    UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mCallback).gameObject);
    this.mCallback = (LogMonitor.GUICallback) null;
  }

  public bool SendSlack(
    LogMonitor.SendType send_type,
    string send_message,
    Action start_callback,
    Action end_callback,
    string screenRecordingFilename = null)
  {
    if (this.mSending)
      return false;
    if (string.IsNullOrEmpty(send_message))
      send_message = string.Empty;
    DateTime localTime = DateTime.UtcNow.ToLocalTime();
    string empty = string.Empty;
    string sendFileName = string.Empty;
    switch (send_type)
    {
      case LogMonitor.SendType.SCREENSHOT:
        sendFileName = empty + AppPath.persistentDataPath + "/" + (object) localTime.Year + "-" + (object) localTime.Month + "-" + (object) localTime.Day + "-" + (object) localTime.Hour + "-" + (object) localTime.Minute + "-" + (object) localTime.Second + "-" + (object) localTime.Millisecond + ".PNG";
        break;
      case LogMonitor.SendType.MOVIE:
        sendFileName = screenRecordingFilename;
        break;
      case LogMonitor.SendType.TWITTER_SS:
        sendFileName = empty + AppPath.persistentDataPath + "/" + SNSController.SCREENSHOT_IMAGE_SAVE_DIR + "/" + SNSController.SCREENSHOT_IMAGE_NAME;
        break;
    }
    this.StartCoroutine(this.PostSlack(send_type, sendFileName, send_message, start_callback, end_callback));
    return true;
  }

  [DebuggerHidden]
  private IEnumerator PostSlack(
    LogMonitor.SendType send_type,
    string sendFileName,
    string sendMessage,
    Action start_callback,
    Action end_callback)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LogMonitor.\u003CPostSlack\u003Ec__Iterator0 postSlackCIterator0 = new LogMonitor.\u003CPostSlack\u003Ec__Iterator0();
    return (IEnumerator) postSlackCIterator0;
  }

  private void SaveScreeShot(string filename)
  {
    Texture2D texture2D = new Texture2D(Screen.width, Screen.height, (TextureFormat) 3, false);
    texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height), 0, 0);
    texture2D.Apply();
    byte[] png = ImageConversion.EncodeToPNG(texture2D);
    UnityEngine.Object.Destroy((UnityEngine.Object) texture2D);
    File.WriteAllBytes(filename, png);
  }

  public void DirectLog(string logString) => this.HandleLog(logString, (string) null, (LogType) 0);

  public enum SendType
  {
    MESSAGE,
    SCREENSHOT,
    MOVIE,
    TWITTER_SS,
  }

  private class Log
  {
    public int index;
    public string logString;
    public string stackTrace;
    public LogType type;
  }

  [Serializable]
  public class ErrorInfo
  {
    public string productname;
    public string assets;
    public string appver;
    public string dlcver;
    public string netver;
    public string host;
    public string code;
    public string uid;
    public string name;
    public string use_info;
  }

  [Serializable]
  public class NotificationMessage
  {
    public string message;
  }

  [AddComponentMenu("")]
  public class GUICallback : MonoBehaviour
  {
    public LogMonitor.GUICallback.GUIEvent OnGUIListener = (LogMonitor.GUICallback.GUIEvent) (() => { });

    private void OnGUI() => this.OnGUIListener();

    public delegate void GUIEvent();
  }
}
