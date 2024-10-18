// Decompiled with JetBrains decompiler
// Type: SRPG.ReqEquipExpAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class ReqEquipExpAdd : WebAPI
  {
    public ReqEquipExpAdd(long iid, int slot, Dictionary<string, int> usedItems, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/enforce";
      this.body = "\"iid\":" + (object) iid + ",";
      ReqEquipExpAdd reqEquipExpAdd = this;
      reqEquipExpAdd.body = reqEquipExpAdd.body + "\"id_equip\":" + (object) slot + ",";
      this.body += "\"mats\":[";
      string str = string.Empty;
      foreach (KeyValuePair<string, int> usedItem in usedItems)
      {
        str += "{";
        str = str + "\"iname\":\"" + usedItem.Key + "\",";
        str = str + "\"num\":" + (object) usedItem.Value;
        str += "},";
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      this.body += str;
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
