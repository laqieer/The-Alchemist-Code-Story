﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUpdateSelectAward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqUpdateSelectAward : WebAPI
  {
    public ReqUpdateSelectAward(string iname, Network.ResponseCallback response)
    {
      this.name = "award/select";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"selected_award\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
