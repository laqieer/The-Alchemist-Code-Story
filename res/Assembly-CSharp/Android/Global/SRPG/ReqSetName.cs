﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqSetName : WebAPI
  {
    public ReqSetName(string username, Network.ResponseCallback response)
    {
      username = WebAPI.EscapeString(username);
      this.name = "setname";
      this.body = WebAPI.GetRequestString("\"name\":\"" + username + "\"");
      this.callback = response;
    }
  }
}