// Decompiled with JetBrains decompiler
// Type: UniWebViewLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class UniWebViewLogger
{
  private static UniWebViewLogger instance;
  private UniWebViewLogger.Level level;

  private UniWebViewLogger(UniWebViewLogger.Level level) => this.level = level;

  public UniWebViewLogger.Level LogLevel
  {
    get => this.level;
    set
    {
      this.Log(UniWebViewLogger.Level.Off, "Setting UniWebView logger level to: " + (object) value);
      this.level = value;
      UniWebViewInterface.SetLogLevel((int) value);
    }
  }

  public static UniWebViewLogger Instance
  {
    get
    {
      if (UniWebViewLogger.instance == null)
        UniWebViewLogger.instance = new UniWebViewLogger(UniWebViewLogger.Level.Critical);
      return UniWebViewLogger.instance;
    }
  }

  public void Verbose(string message) => this.Log(UniWebViewLogger.Level.Verbose, message);

  public void Debug(string message) => this.Log(UniWebViewLogger.Level.Debug, message);

  public void Info(string message) => this.Log(UniWebViewLogger.Level.Info, message);

  public void Critical(string message) => this.Log(UniWebViewLogger.Level.Critical, message);

  private void Log(UniWebViewLogger.Level level, string message)
  {
    if (level < this.LogLevel)
      return;
    string str = "<UniWebView> " + message;
    if (level == UniWebViewLogger.Level.Critical)
      UnityEngine.Debug.LogError((object) str);
    else
      UnityEngine.Debug.Log((object) str);
  }

  public enum Level
  {
    Verbose = 0,
    Debug = 10, // 0x0000000A
    Info = 20, // 0x00000014
    Critical = 80, // 0x00000050
    Off = 99, // 0x00000063
  }
}
