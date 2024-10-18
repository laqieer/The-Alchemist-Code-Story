﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitAwake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqUnitAwake : WebAPI
  {
    public ReqUnitAwake(long iid, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null, int awake_lv = 0)
    {
      this.name = "unit/plus/add";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":" + (object) iid);
      if (awake_lv > 0)
      {
        stringBuilder.Append(",");
        stringBuilder.Append("\"target_plus\":" + (object) awake_lv);
      }
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
