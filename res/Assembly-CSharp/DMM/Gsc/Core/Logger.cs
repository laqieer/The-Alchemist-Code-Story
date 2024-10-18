// Decompiled with JetBrains decompiler
// Type: Gsc.Core.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using UnityEngine;

#nullable disable
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
      // ISSUE: reference to a compiler-generated field
      if (Logger.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Logger.\u003C\u003Ef__mg\u0024cache0 = new Application.LogCallback(Logger._HandleLog);
      }
      // ISSUE: reference to a compiler-generated field
      Application.logMessageReceived -= Logger.\u003C\u003Ef__mg\u0024cache0;
      // ISSUE: reference to a compiler-generated field
      if (Logger.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Logger.\u003C\u003Ef__mg\u0024cache1 = new Application.LogCallback(Logger._HandleLog);
      }
      // ISSUE: reference to a compiler-generated field
      Application.logMessageReceived += Logger.\u003C\u003Ef__mg\u0024cache1;
      Logger.initialized = true;
    }

    public static void HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      if (logType != 1 && logType != 4 && logType != null)
        return;
      UnityErrorLogSender.Instance.Send(logMessage, stackTrace, logType);
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
