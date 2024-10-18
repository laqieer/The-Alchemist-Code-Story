// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquipAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqJobEquipAll : WebAPI
  {
    public ReqJobEquipAll(
      long iid,
      string iname_jobset,
      bool[] iid_equips,
      Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/slots";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":");
      stringBuilder.Append(iid);
      stringBuilder.Append(",\"jobset\":\"");
      stringBuilder.Append(iname_jobset);
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"slot\":{");
      for (int index = 0; index < iid_equips.Length; ++index)
      {
        stringBuilder.Append(string.Format("\"slot{0}\":", (object) index));
        if (iid_equips[index])
          stringBuilder.Append("1");
        else
          stringBuilder.Append("0");
        if (index != iid_equips.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
