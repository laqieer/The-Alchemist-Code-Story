﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.Register
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class Register : GAuthResponse<Register>
  {
    public Register(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.DeviceId = document.Root["device_id"].ToString();
    }

    public string DeviceId { get; private set; }
  }
}
