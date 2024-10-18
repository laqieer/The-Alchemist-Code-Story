// Decompiled with JetBrains decompiler
// Type: SRPG.ReqOrdealPartyUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;

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
