﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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

    public string Platform
    {
      get
      {
        return "dmmgamesstore";
      }
    }

    public bool initialized
    {
      get
      {
        return true;
      }
    }

    public bool hasError { get; private set; }

    public int ViewerId { get; set; }

    public string OnetimeToken { get; set; }
  }
}
