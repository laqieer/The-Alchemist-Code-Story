// Decompiled with JetBrains decompiler
// Type: ExceptionMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
[AddComponentMenu("")]
public class ExceptionMonitor : MonoBehaviour
{
  private static bool mExceptionStop;
  private static ExceptionMonitor mInstnace;
  private bool bMessageDraw;
  private Coroutine m_OutputRoutine;

  public static bool IsExceptionStop => ExceptionMonitor.mExceptionStop;

  private static string CrashLogTextPath => AppPath.crashLogPath + "/crash.txt";

  public static void Start()
  {
    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ExceptionMonitor.mInstnace, (UnityEngine.Object) null))
      return;
    ExceptionMonitor.mInstnace = new GameObject(nameof (ExceptionMonitor)).AddComponent<ExceptionMonitor>();
  }

  private void Awake()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) this).gameObject);
    ((UnityEngine.Object) this).hideFlags = (HideFlags) 61;
    Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != 4 || this.bMessageDraw)
      return;
    this.SendLogMessage(logString, stackTrace);
    ExceptionMonitor.mExceptionStop = true;
    if (logString.IndexOf("DllNotFoundException:") >= 0)
    {
      if (this.m_OutputRoutine != null)
        return;
      this.m_OutputRoutine = this.StartCoroutine(this.OutputCrashInfo(logString, stackTrace, new Action(this.MessageBoxDLL)));
    }
    else
    {
      if (this.m_OutputRoutine != null)
        return;
      this.m_OutputRoutine = this.StartCoroutine(this.OutputCrashInfo(logString, stackTrace, new Action(this.MessageBoxDefault)));
    }
  }

  private void SendLogMessage(string logString, string stackTrace)
  {
    FlowNode_SendLogMessage.SendLogGenerator dict = new FlowNode_SendLogMessage.SendLogGenerator();
    dict.AddCommon(true, false, false, true);
    dict.Add("err", logString);
    dict.Add("trace", stackTrace);
    FlowNode_SendLogMessage.SendLogMessage(dict, "Exception");
  }

  private void MessageBoxDefault()
  {
    this.bMessageDraw = true;
    ((Behaviour) EventSystem.current.currentInputModule).enabled = true;
    EmbedSystemMessage.Create(LocalizedText.Get("embed.APP_EXCEPTION"), (EmbedSystemMessage.SystemMessageEvent) (yes =>
    {
      this.bMessageDraw = false;
      Application.Quit();
      ExceptionMonitor.mExceptionStop = false;
    }));
  }

  private void MessageBoxDLL()
  {
    this.bMessageDraw = true;
    EmbedSystemMessageEx embedSystemMessageEx = EmbedSystemMessageEx.Create(LocalizedText.Get("embed.APP_DLL_EXCEPTION"));
    embedSystemMessageEx.AddButton(LocalizedText.Get("embed.APP_DLL_SUPPORT"), false, (EmbedSystemMessageEx.SystemMessageEvent) (ok => Application.OpenURL("https://al.fg-games.co.jp/news/206/")));
    embedSystemMessageEx.AddButton(LocalizedText.Get("embed.APP_DLL_QUIT"), true, (EmbedSystemMessageEx.SystemMessageEvent) (ok =>
    {
      this.bMessageDraw = false;
      Application.Quit();
      ExceptionMonitor.mExceptionStop = false;
    }));
  }

  [DebuggerHidden]
  private IEnumerator OutputCrashInfo(string logString, string stackTrace, Action onComplete)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new ExceptionMonitor.\u003COutputCrashInfo\u003Ec__Iterator0()
    {
      logString = logString,
      stackTrace = stackTrace,
      onComplete = onComplete,
      \u0024this = this
    };
  }
}
