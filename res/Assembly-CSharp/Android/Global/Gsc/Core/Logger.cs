// Decompiled with JetBrains decompiler
// Type: Gsc.Core.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using UnityEngine;

namespace Gsc.Core
{
  public class Logger
  {
    private static bool initialized;

    public static event Application.LogCallback Callback;

    public static void Init()
    {
      if (Logger.initialized)
        return;
      Application.logMessageReceived -= new Application.LogCallback(Logger._HandleLog);
      Application.logMessageReceived += new Application.LogCallback(Logger._HandleLog);
      Logger.initialized = true;
    }

    public static void HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      switch (logType)
      {
        case LogType.Error:
        case LogType.Assert:
        case LogType.Exception:
          UnityErrorLogSender.Instance.Send(logMessage, stackTrace, logType);
          break;
      }
    }

    private static void _HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      Logger.HandleLog(logMessage, stackTrace, logType);
      if (Logger.Callback == null)
        return;
      Logger.Callback(logMessage, stackTrace, logType);
    }
  }
}
