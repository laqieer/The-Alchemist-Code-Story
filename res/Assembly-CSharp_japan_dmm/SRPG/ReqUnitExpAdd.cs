// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitExpAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqUnitExpAdd : WebAPI
  {
    public ReqUnitExpAdd(
      long iid,
      Dictionary<string, int> usedItems,
      Network.ResponseCallback response)
    {
      this.name = "unit/exp/add";
      this.body = "\"iid\":" + (object) iid + ",";
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
