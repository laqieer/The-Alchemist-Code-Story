// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
  public class Logger
  {
    private static bool warningLogEnabled = true;
    private static bool debugLogEnabled;

    public static bool DebugLogEnabled
    {
      get
      {
        return Logger.debugLogEnabled;
      }
      set
      {
        Logger.debugLogEnabled = value;
      }
    }

    public static bool WarningLogEnabled
    {
      get
      {
        return Logger.warningLogEnabled;
      }
      set
      {
        Logger.warningLogEnabled = value;
      }
    }

    public static void d(string msg)
    {
      if (!Logger.debugLogEnabled)
        return;
      PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.Log((object) Logger.ToLogMessage(string.Empty, "DEBUG", msg))));
    }

    public static void w(string msg)
    {
      if (!Logger.warningLogEnabled)
        return;
      PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.LogWarning((object) Logger.ToLogMessage("!!!", "WARNING", msg))));
    }

    public static void e(string msg)
    {
      if (!Logger.warningLogEnabled)
        return;
      PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.LogWarning((object) Logger.ToLogMessage("***", "ERROR", msg))));
    }

    public static string describe(byte[] b)
    {
      if (b == null)
        return "(null)";
      return "byte[" + (object) b.Length + "]";
    }

    private static string ToLogMessage(string prefix, string logType, string msg)
    {
      return string.Format("{0} [Play Games Plugin DLL] {1} {2}: {3}", (object) prefix, (object) DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"), (object) logType, (object) msg);
    }
  }
}
