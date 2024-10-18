// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusCpuList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqVersusCpuList : WebAPI
  {
    public ReqVersusCpuList(VersusStatusData param, int num, string quest_iname, Network.ResponseCallback response)
    {
      this.name = "vs/com";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
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
      stringBuilder.Append(",\"iname\":\"");
      stringBuilder.Append(JsonEscape.Escape(quest_iname));
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
