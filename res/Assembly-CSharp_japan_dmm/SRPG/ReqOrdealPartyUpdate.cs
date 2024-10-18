// Decompiled with JetBrains decompiler
// Type: SRPG.ReqOrdealPartyUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqOrdealPartyUpdate : WebAPI
  {
    public ReqOrdealPartyUpdate(Network.ResponseCallback response, List<PartyEditData> parties)
    {
      PartyData party1 = MonoSingleton<GameManager>.Instance.Player.Partys[9];
      this.name = "party2/ordeal/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"parties\":[");
      int num = 0;
      stringBuilder.Append("{\"units\":[");
      foreach (PartyEditData party2 in parties)
      {
        if (num > 0)
          stringBuilder.Append(',');
        stringBuilder.Append('[');
        for (int index = 0; index < party1.MAX_UNIT && index < party2.Units.Length && party2.Units[index] != null; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(party2.Units[index].UniqueID);
        }
        stringBuilder.Append(']');
        ++num;
      }
      stringBuilder.Append(']');
      string stringFromPartyType = PartyData.GetStringFromPartyType(PlayerPartyTypes.Ordeal);
      stringBuilder.Append(",\"ptype\":\"");
      stringBuilder.Append(stringFromPartyType);
      stringBuilder.Append('"');
      stringBuilder.Append('}');
      stringBuilder.Append(']');
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
