﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqJobAbility : WebAPI
  {
    public ReqJobAbility(long iid_job, long[] iid_abils, Network.ResponseCallback response)
    {
      this.name = "unit/job/abil/set";
      this.body = "\"iid\":" + (object) iid_job + ",";
      this.body += "\"iid_abils\":";
      this.body += "[";
      for (int index = 0; index < iid_abils.Length; ++index)
      {
        this.body += iid_abils[index].ToString();
        if (index != iid_abils.Length - 1)
          this.body += ",";
      }
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
