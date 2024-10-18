// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MyPhotonRoomParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.App;
using System.Text;

#nullable disable
namespace SRPG
{
  public class JSON_MyPhotonRoomParam
  {
    public static readonly int LINE_PARAM_ENCODE_KEY = 6789;
    public string creatorName = string.Empty;
    public int creatorLV = 1;
    public string creatorFUID = string.Empty;
    public string comment = string.Empty;
    public string passCode = string.Empty;
    public int btlSpd = 1;
    public int autoAllowed;
    public string btlver = string.Empty;
    public int type;
    public int isLINE;
    public string iname = string.Empty;
    public int started;
    public int roomid;
    public int audience;
    public int audienceNum;
    public int unitlv;
    public int challegedMTFloor;
    public int vsmode;
    public int draft_type;
    public int draft_deck_id;
    public JSON_MyPhotonPlayerParam[] players;
    public JSON_MyPhotonPlayerParam[] support;

    public static string GetCreatorFUID()
    {
      return Network.Mode == Network.EConnectMode.Offline ? MonoSingleton<GameManager>.Instance.UdId : MonoSingleton<GameManager>.Instance.Player.FUID;
    }

    public JSON_MyPhotonPlayerParam GetOwner()
    {
      if (this.players == null || this.players.Length <= 0)
        return (JSON_MyPhotonPlayerParam) null;
      JSON_MyPhotonPlayerParam owner = this.players[0];
      foreach (JSON_MyPhotonPlayerParam player in this.players)
      {
        if (player.playerIndex > 0 && player.playerIndex < owner.playerIndex)
          owner = player;
      }
      return owner;
    }

    public string GetOwnerName()
    {
      JSON_MyPhotonPlayerParam owner = this.GetOwner();
      return owner == null ? this.creatorName : owner.playerName;
    }

    public int GetOwnerLV()
    {
      JSON_MyPhotonPlayerParam owner = this.GetOwner();
      return owner == null ? this.creatorLV : owner.playerLevel;
    }

    public static JSON_MyPhotonRoomParam Parse(string json)
    {
      return json == null || json.Length <= 0 ? new JSON_MyPhotonRoomParam() : JSONParser.parseJSONObject<JSON_MyPhotonRoomParam>(json);
    }

    public string Serialize()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{");
      stringBuilder.Append("\"creatorName\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.creatorName));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"creatorLV\":");
      stringBuilder.Append(this.creatorLV);
      stringBuilder.Append(",\"creatorFUID\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.creatorFUID));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"comment\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.comment));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"passCode\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.passCode));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"btlSpd\":");
      stringBuilder.Append(this.btlSpd);
      stringBuilder.Append(",\"autoAllowed\":");
      stringBuilder.Append(this.autoAllowed);
      if (!Network.GetEnvironment.IsEnvironmentFlag(Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF))
      {
        stringBuilder.Append(",\"btlver\":");
        stringBuilder.Append("\"");
        stringBuilder.Append(JsonEscape.Escape(this.btlver));
        stringBuilder.Append("\"");
      }
      stringBuilder.Append(",\"iname\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.iname));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"type\":");
      stringBuilder.Append(this.type);
      stringBuilder.Append(",\"isLINE\":");
      stringBuilder.Append(this.isLINE);
      stringBuilder.Append(",\"started\":");
      stringBuilder.Append(this.started);
      stringBuilder.Append(",\"roomid\":");
      stringBuilder.Append(this.roomid);
      stringBuilder.Append(",\"audience\":");
      stringBuilder.Append(this.audience);
      stringBuilder.Append(",\"audienceNum\":");
      stringBuilder.Append(this.audienceNum);
      stringBuilder.Append(",\"unitlv\":");
      stringBuilder.Append(this.unitlv);
      stringBuilder.Append(",\"challegedMTFloor\":");
      stringBuilder.Append(GlobalVars.SelectedMultiTowerFloor);
      stringBuilder.Append(",\"vsmode\":");
      stringBuilder.Append(this.vsmode);
      stringBuilder.Append(",\"draft_type\":");
      stringBuilder.Append(this.draft_type);
      stringBuilder.Append(",\"draft_deck_id\":");
      stringBuilder.Append(this.draft_deck_id);
      stringBuilder.Append(",\"players\":[");
      if (this.players != null)
      {
        for (int index = 0; index < this.players.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(this.players[index].Serialize());
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append(",\"support\":[");
      if (this.support != null)
      {
        for (int index = 0; index < this.support.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(this.support[index].Serialize());
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    public static int GetTotalUnitNum(QuestParam param)
    {
      return param == null ? 0 : (int) param.unitNum * (int) param.playerNum;
    }

    public int GetUnitSlotNum()
    {
      return this.GetUnitSlotNum(PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex);
    }

    public int GetUnitSlotNum(int playerIndex)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.iname);
      return quest == null ? 0 : (int) quest.unitNum;
    }

    public enum EType
    {
      RAID,
      VERSUS,
      TOWER,
      RANKMATCH,
      NUM,
    }
  }
}
