// Decompiled with JetBrains decompiler
// Type: LogMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class LogMonitor : MonoBehaviour
{
  private List<LogMonitor.Log> mLogs = new List<LogMonitor.Log>();
  private static LogMonitor mInstance;
  private LogMonitor.GUICallback mCallback;
  private int mLogCount;
  private GUIStyle mBackgroundStyle;
  private GUIStyle mErrorStyle;
  private GUIStyle mExceptionStyle;
  private GUIStyle mStackTraceStyle;
  private string mStackTrace;
  public bool isDisp;

  public static LogMonitor Instance
  {
    get
    {
      if ((UnityEngine.Object) LogMonitor.mInstance == (UnityEngine.Object) null)
      {
        LogMonitor.mInstance = new GameObject(nameof (LogMonitor)).AddComponent<LogMonitor>();
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) LogMonitor.mInstance.gameObject);
        LogMonitor.mInstance.hideFlags = HideFlags.HideAndDontSave;
        LogMonitor.mInstance.isDisp = true;
      }
      return LogMonitor.mInstance;
    }
  }

  public static void Start()
  {
    if (!GameUtility.IsDebugBuild)
      return;
    LogMonitor.Instance.InitializeGUICallback();
  }

  private void InitializeGUICallback()
  {
    this.mCallback = new GameObject("callback").AddComponent<LogMonitor.GUICallback>();
    this.mCallback.OnGUIListener = new LogMonitor.GUICallback.GUIEvent(this.OnGUICallback);
    this.mCallback.gameObject.transform.SetParent(this.transform, false);
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
    if (this.mLogs.Count <= 0 || !this.isDisp)
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
    GUI.Box(new Rect(0.0f, 0.0f, (float) Screen.width, 25f), string.Empty, this.mBackgroundStyle);
    if (GUI.Button(new Rect((float) (Screen.width - 25), 0.0f, 25f, 25f), "X"))
    {
      this.Clear();
    }
    else
    {
      GUILayout.Space(25f);
      GUILayout.BeginVertical();
      for (int index = 0; index < this.mLogs.Count; ++index)
      {
        if (GUILayout.Button("#" + (object) this.mLogs[index].index + " " + this.mLogs[index].logString, this.mLogs[index].type == LogType.Exception ? this.mExceptionStyle : this.mErrorStyle, new GUILayoutOption[1]
        {
          GUILayout.Width((float) Screen.width)
        }))
          this.mStackTrace = this.mLogs[index].stackTrace;
      }
      GUILayout.EndVertical();
      if (!string.IsNullOrEmpty(this.mStackTrace))
      {
        GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
        {
          GUILayout.Height(4f)
        });
        GUILayout.Label(this.mStackTrace, this.mStackTraceStyle, new GUILayoutOption[1]
        {
          GUILayout.Width((float) Screen.width)
        });
      }
      GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
      {
        GUILayout.Height(8f)
      });
    }
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != LogType.Exception && type != LogType.Error)
      return;
    this.SGLogging(logString, stackTrace, type);
  }

  private void JPLogging(string logString, string stackTrace, LogType type)
  {
    switch (type)
    {
      case LogType.Error:
        DebugMenu.LogError("Error", logString, stackTrace);
        break;
      case LogType.Assert:
        DebugMenu.LogError("Assert", logString, stackTrace);
        break;
      case LogType.Warning:
        DebugMenu.LogWarning((string) null, logString, stackTrace);
        break;
      case LogType.Log:
        DebugMenu.Log((string) null, logString, stackTrace);
        break;
      case LogType.Exception:
        DebugMenu.LogError("Exception", logString, stackTrace);
        break;
    }
  }

  private void SGLogging(string logString, string stackTrace, LogType type)
  {
    if (type == LogType.Error && logString.StartsWith("Asynchronous Background loading is only supported in Unity Pro."))
      return;
    if (!this.mCallback.isActiveAndEnabled)
      this.mCallback.gameObject.SetActive(true);
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
  }

  public void Clear()
  {
    this.mLogs.Clear();
    this.mStackTrace = string.Empty;
    if (!this.mCallback.isActiveAndEnabled)
      return;
    this.mCallback.gameObject.SetActive(false);
  }

  private class GUICallback : MonoBehaviour
  {
    public LogMonitor.GUICallback.GUIEvent OnGUIListener = (LogMonitor.GUICallback.GUIEvent) (() => {});

    private void OnGUI()
    {
      this.OnGUIListener();
    }

    public delegate void GUIEvent();
  }

  private class Log
  {
    public int index;
    public string logString;
    public string stackTrace;
    public LogType type;
  }
}
