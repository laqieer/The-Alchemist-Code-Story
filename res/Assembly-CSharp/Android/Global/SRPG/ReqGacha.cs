﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGacha : WebAPI
  {
    public ReqGacha(Network.ResponseCallback response)
    {
      this.name = "gacha";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
