﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemCompositAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemCompositAll : WebAPI
  {
    public ReqItemCompositAll(string iname, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "item/gouseiall";
      int num = !is_cmn ? 0 : 1;
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\",\"is_cmn\":" + (object) num);
      this.callback = response;
    }
  }
}
