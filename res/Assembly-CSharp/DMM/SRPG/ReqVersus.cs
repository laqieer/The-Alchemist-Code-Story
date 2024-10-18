// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqVersus : WebAPI
  {
    public ReqVersus(
      string iname,
      int plid,
      int seat,
      string uid,
      VersusStatusData param,
      int num,
      Network.ResponseCallback response,
      VERSUS_TYPE type,
      int draft_id = 0,
      int enemy_draft_id = 0)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "vs/" + type.ToString().ToLower() + "match/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(JsonEscape.Escape(iname));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"token\":\"");
      stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"plid\":\"");
      stringBuilder.Append(plid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"seat\":\"");
      stringBuilder.Append(seat);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"uid\":\"");
      stringBuilder.Append(uid);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"status\":{");
      stringBuilder.Append("\"hp\":" + (object) param.Hp + ",");
      stringBuilder.Append("\"atk\":" + (object) param.Atk + ",");
      stringBuilder.Append("\"def\":" + (object) param.Def + ",");
      stringBuilder.Append("\"matk\":" + (object) param.Matk + ",");
      stringBuilder.Append("\"mdef\":" + (object) param.Mdef + ",");
      stringBuilder.Append("\"dex\":" + (object) param.Dex + ",");
      stringBuilder.Append("\"spd\":" + (object) param.Spd + ",");
      stringBuilder.Append("\"cri\":" + (object) param.Cri + ",");
      stringBuilder.Append("\"luck\":" + (object) param.Luck + ",");
      stringBuilder.Append("\"cmb\":" + (object) param.Cmb + ",");
      stringBuilder.Append("\"move\":" + (object) param.Move + ",");
      stringBuilder.Append("\"jmp\":" + (object) param.Jmp);
      stringBuilder.Append("}");
      stringBuilder.Append(",\"member_count\":" + (object) num);
      if (draft_id > 0)
      {
        stringBuilder.Append(",\"draft_id\":" + (object) draft_id);
        stringBuilder.Append(",\"enemy_draft_id\":" + (object) enemy_draft_id);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
