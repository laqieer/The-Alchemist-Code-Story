﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquipV2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqJobEquipV2 : WebAPI
  {
    public ReqJobEquipV2(long iid_unit, string iname_jobset, long slot, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/set2";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"iname\":\"");
      stringBuilder.Append(iname_jobset);
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"slot\":");
      stringBuilder.Append(slot);
      stringBuilder.Append(",\"is_cmn\":");
      stringBuilder.Append(!is_cmn ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
