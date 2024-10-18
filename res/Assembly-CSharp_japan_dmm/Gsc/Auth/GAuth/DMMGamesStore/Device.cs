// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Gsc.Auth.GAuth.DMMGamesStore
{
  public class Device : IDevice
  {
    public static Device Instance;

    public Device()
    {
      Device.Instance = this;
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      for (int index = 1; index < commandLineArgs.Length; ++index)
      {
        string str = commandLineArgs[index];
        if (str.StartsWith("/viewer_id="))
          this.ViewerId = int.Parse(str.Split('=')[1]);
        else if (str.StartsWith("/onetime_token="))
          this.OnetimeToken = str.Split('=')[1];
      }
      this.hasError = this.ViewerId == 0 || string.IsNullOrEmpty(this.OnetimeToken);
    }

    public string Platform => "dmmgamesstore";

    public bool initialized => true;

    public bool hasError { get; private set; }

    public int ViewerId { get; set; }

    public string OnetimeToken { get; set; }
  }
}
