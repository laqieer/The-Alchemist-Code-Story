// Decompiled with JetBrains decompiler
// Type: LogMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

[AddComponentMenu("")]
public class LogMonitor : MonoBehaviour
{
  private List<LogMonitor.Log> mLogs = new List<LogMonitor.Log>();
  public static LogMonitor mInstnace;
  private int mLogCount;
  private GUIStyle mBackgroundStyle;
  private GUIStyle mErrorStyle;
  private GUIStyle mExceptionStyle;
  private GUIStyle mStackTraceStyle;
  private string mStackTrace;
  private LogMonitor.GUICallback mCallback;
  private bool mDisp;
  private bool mSending;

  public static void Start()
  {
    if (!GameUtility.IsDebugBuild || !((UnityEngine.Object) LogMonitor.mInstnace == (UnityEngine.Object) null))
      return;
    LogMonitor.mInstnace = new GameObject(nameof (LogMonitor)).AddComponent<LogMonitor>();
  }

  public static LogMonitor Instance
  {
    get
    {
      return LogMonitor.mInstnace;
    }
  }

  public bool isDisp
  {
    get
    {
      return this.mDisp;
    }
  }

  public bool IsSend
  {
    get
    {
      return this.mSending;
    }
  }

  private void Awake()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.hideFlags = HideFlags.HideAndDontSave;
    this.SetDisp(true);
  }

  public void SetDisp(bool value)
  {
    this.mDisp = value;
  }

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
      this.mErrorStyle = new GUIStyle(GUI.skin.label);
      this.mErrorStyle.contentOffset = Vector2.right * 5f;
      this.mErrorStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mErrorStyle.fontSize = 10;
      this.mErrorStyle.normal = new GUIStyleState();
      this.mErrorStyle.normal.textColor = Color.white;
      this.mErrorStyle.normal.background = new Texture2D(1, 1);
      this.mErrorStyle.normal.background.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f));
      this.mErrorStyle.normal.background.Apply();
      this.mErrorStyle.hover = new GUIStyleState();
      this.mErrorStyle.hover.textColor = Color.white;
      this.mErrorStyle.hover.background = new Texture2D(1, 1);
      this.mErrorStyle.hover.background.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
      this.mErrorStyle.hover.background.Apply();
    }
    if (this.mStackTraceStyle == null)
    {
      this.mStackTraceStyle = new GUIStyle(GUI.skin.label);
      this.mStackTraceStyle.contentOffset = Vector2.right * 5f;
      this.mStackTraceStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mStackTraceStyle.fontSize = 10;
      this.mStackTraceStyle.normal = new GUIStyleState();
      this.mStackTraceStyle.normal.textColor = Color.white;
      this.mStackTraceStyle.normal.background = new Texture2D(1, 1);
      this.mStackTraceStyle.normal.background.SetPixel(0, 0, new Color(0.0f, 0.0f, 1f));
      this.mStackTraceStyle.normal.background.Apply();
    }
    if (this.mExceptionStyle == null)
    {
      this.mExceptionStyle = new GUIStyle(GUI.skin.label);
      this.mExceptionStyle.contentOffset = Vector2.right * 5f;
      this.mExceptionStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mExceptionStyle.fontSize = 10;
      this.mExceptionStyle.normal = new GUIStyleState();
      this.mExceptionStyle.normal.textColor = Color.yellow;
      this.mExceptionStyle.normal.background = new Texture2D(1, 1);
      this.mExceptionStyle.normal.background.SetPixel(0, 0, new Color(0.5f, 0.0f, 0.0f));
      this.mExceptionStyle.normal.background.Apply();
      this.mExceptionStyle.hover = new GUIStyleState();
      this.mExceptionStyle.hover.textColor = Color.yellow;
      this.mExceptionStyle.hover.background = new Texture2D(1, 1);
      this.mExceptionStyle.hover.background.SetPixel(0, 0, new Color(0.6f, 0.0f, 0.0f));
      this.mExceptionStyle.hover.background.Apply();
    }
    if (this.mBackgroundStyle == null)
    {
      this.mBackgroundStyle = new GUIStyle(GUI.skin.label);
      this.mBackgroundStyle.stretchWidth = true;
      this.mBackgroundStyle.stretchHeight = true;
      this.mBackgroundStyle.normal.background = new Texture2D(1, 1);
      this.mBackgroundStyle.normal.background.SetPixel(0, 0, Color.black);
      this.mBackgroundStyle.normal.background.Apply();
      this.mBackgroundStyle.margin = new RectOffset(0, 0, 0, 0);
      this.mBackgroundStyle.padding = this.mBackgroundStyle.margin;
    }
    GUI.Box(new Rect(0.0f, 0.0f, (float) Screen.width, 30f), string.Empty, this.mBackgroundStyle);
    if (GUI.Button(new Rect((float) (Screen.width - 30), 0.0f, 30f, 30f), "X"))
    {
      this.Clear();
    }
    else
    {
      GUILayout.BeginVertical();
      for (int index = 0; index < this.mLogs.Count; ++index)
      {
        if (GUILayout.Button("#" + (object) this.mLogs[index].index + " " + this.mLogs[index].logString, this.mLogs[index].type != LogType.Error ? this.mExceptionStyle : this.mErrorStyle, new GUILayoutOption[1]
        {
          GUILayout.Width((float) (Screen.width - 30))
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
          GUILayout.Width((float) (Screen.width - 30))
        });
      }
      GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
      {
        GUILayout.Height(8f)
      });
      GUILayout.BeginHorizontal();
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
        if (GUILayout.Button("HipChatに\nスクリーンショット送信", new GUILayoutOption[2]
        {
          GUILayout.Width(160f),
          GUILayout.Height(40f)
        }))
          this.SendHipChat((Action) null, (Action) null);
        GUI.enabled = true;
      }
      GUILayout.EndHorizontal();
      GUILayout.EndVertical();
    }
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != LogType.Exception && type != LogType.Error || type == LogType.Error && logString.StartsWith("Asynchronous Background loading is only supported in Unity Pro."))
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
    if (!((UnityEngine.Object) this.mCallback == (UnityEngine.Object) null))
      return;
    this.mCallback = new GameObject("callback", new System.Type[2]
    {
      typeof (GameObject),
      typeof (LogMonitor.GUICallback)
    }).GetComponent<LogMonitor.GUICallback>();
    this.mCallback.OnGUIListener = new LogMonitor.GUICallback.GUIEvent(this.OnGUICallback);
    this.mCallback.transform.SetParent(this.transform, false);
  }

  public void Clear()
  {
    this.mLogs.Clear();
    this.mStackTrace = (string) null;
    UnityEngine.Object.Destroy((UnityEngine.Object) this.mCallback.gameObject);
    this.mCallback = (LogMonitor.GUICallback) null;
  }

  public bool SendHipChat(Action start_callback, Action end_callback)
  {
    if (this.mSending)
      return false;
    DateTime localTime = DateTime.UtcNow.ToLocalTime();
    this.StartCoroutine(this.PostHipchat(string.Empty + AppPath.persistentDataPath + "/" + (object) localTime.Year + "-" + (object) localTime.Month + "-" + (object) localTime.Day + "-" + (object) localTime.Hour + "-" + (object) localTime.Minute + "-" + (object) localTime.Second + "-" + (object) localTime.Millisecond + ".PNG", start_callback, end_callback));
    return true;
  }

  [DebuggerHidden]
  private IEnumerator PostHipchat(string filename, Action start_callback, Action end_callback)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LogMonitor.\u003CPostHipchat\u003Ec__Iterator0 hipchatCIterator0 = new LogMonitor.\u003CPostHipchat\u003Ec__Iterator0();
    return (IEnumerator) hipchatCIterator0;
  }

  private void SaveScreeShot(string filename)
  {
    Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height), 0, 0);
    texture2D.Apply();
    byte[] png = texture2D.EncodeToPNG();
    UnityEngine.Object.Destroy((UnityEngine.Object) texture2D);
    File.WriteAllBytes(filename, png);
  }

  public void DirectLog(string logString)
  {
    this.HandleLog(logString, (string) null, LogType.Error);
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
    public LogMonitor.GUICallback.GUIEvent OnGUIListener = (LogMonitor.GUICallback.GUIEvent) (() => {});

    private void OnGUI()
    {
      this.OnGUIListener();
    }

    public delegate void GUIEvent();
  }
}
