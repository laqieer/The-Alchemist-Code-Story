// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.AdjustLogLevelExtension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace com.adjust.sdk
{
  public static class AdjustLogLevelExtension
  {
    public static string ToLowercaseString(this AdjustLogLevel AdjustLogLevel)
    {
      switch (AdjustLogLevel)
      {
        case AdjustLogLevel.Verbose:
          return "verbose";
        case AdjustLogLevel.Debug:
          return "debug";
        case AdjustLogLevel.Info:
          return "info";
        case AdjustLogLevel.Warn:
          return "warn";
        case AdjustLogLevel.Error:
          return "error";
        case AdjustLogLevel.Assert:
          return "assert";
        case AdjustLogLevel.Suppress:
          return "suppress";
        default:
          return "unknown";
      }
    }

    public static string ToUppercaseString(this AdjustLogLevel AdjustLogLevel)
    {
      switch (AdjustLogLevel)
      {
        case AdjustLogLevel.Verbose:
          return "VERBOSE";
        case AdjustLogLevel.Debug:
          return "DEBUG";
        case AdjustLogLevel.Info:
          return "INFO";
        case AdjustLogLevel.Warn:
          return "WARN";
        case AdjustLogLevel.Error:
          return "ERROR";
        case AdjustLogLevel.Assert:
          return "ASSERT";
        case AdjustLogLevel.Suppress:
          return "SUPPRESS";
        default:
          return "UNKNOWN";
      }
    }
  }
}
