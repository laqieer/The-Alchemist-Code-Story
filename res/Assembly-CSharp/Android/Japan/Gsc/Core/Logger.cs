// Decompiled with JetBrains decompiler
// Type: Gsc.Core.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      if (logType != LogType.Assert && logType != LogType.Exception && logType != LogType.Error)
        return;
      UnityErrorLogSender.Instance.Send(logMessage, stackTrace, logType);
    }

    private static void _HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      Logger.HandleLog(logMessage, stackTrace, logType);
      // ISSUE: reference to a compiler-generated field
      if (Logger.Callback == null)
        return;
      // ISSUE: reference to a compiler-generated field
      Logger.Callback(logMessage, stackTrace, logType);
    }
  }
}
