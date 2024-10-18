// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptLeaderSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSetConceptLeaderSkill : WebAPI
  {
    public ReqSetConceptLeaderSkill(long unit_iid, bool set, Network.ResponseCallback response)
    {
      this.name = "unit/concept/leaderskill/set";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"unit_iid\":");
      stringBuilder.Append(unit_iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"is_set\":");
      stringBuilder.Append(!set ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
