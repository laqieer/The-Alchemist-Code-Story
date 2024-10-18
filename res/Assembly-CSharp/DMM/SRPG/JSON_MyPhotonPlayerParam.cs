// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MyPhotonPlayerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_MyPhotonPlayerParam
  {
    public int playerID;
    public int playerIndex;
    public string playerName = string.Empty;
    public int playerLevel;
    public string FUID = string.Empty;
    public string UID = string.Empty;
    public int totalAtk;
    public int totalStatus;
    public int rankpoint;
    public string award = string.Empty;
    public int rankmatch_score;
    public string support_unit = string.Empty;
    public int draft_id;
    public int guild_id;
    public string guild_name = string.Empty;
    public int state;
    public JSON_MyPhotonPlayerParam.UnitDataElem[] units;
    public int leaderID;
    public int mtChallengeFloor;
    public int mtClearedFloor;
    public int isAuto;
    public int isSupportAI;
    public int isAutoTreasure;
    public int isAutoNoSkill;

    public static JSON_MyPhotonPlayerParam Parse(string json)
    {
      if (string.IsNullOrEmpty(json))
        return new JSON_MyPhotonPlayerParam();
      JSON_MyPhotonPlayerParam jsonObject = JSONParser.parseJSONObject<JSON_MyPhotonPlayerParam>(json);
      jsonObject.SetupUnits();
      return jsonObject;
    }

    public void SetupUnits()
    {
      if (this.units == null)
        return;
      for (int index = 0; index < this.units.Length; ++index)
      {
        if (this.units[index].unitJson != null)
        {
          UnitData unitData = new UnitData();
          unitData.Deserialize(this.units[index].unitJson);
          this.units[index].unit = unitData;
        }
      }
    }

    public string Serialize()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{");
      stringBuilder.Append("\"playerID\":");
      stringBuilder.Append(this.playerID);
      stringBuilder.Append(",\"playerIndex\":");
      stringBuilder.Append(this.playerIndex);
      stringBuilder.Append(",\"playerName\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.playerName));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"playerLevel\":");
      stringBuilder.Append(this.playerLevel);
      stringBuilder.Append(",\"FUID\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.FUID));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"UID\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.UID));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"state\":");
      stringBuilder.Append(this.state);
      stringBuilder.Append(",\"leaderID\":");
      stringBuilder.Append(this.leaderID);
      stringBuilder.Append(",\"totalAtk\":");
      stringBuilder.Append(this.totalAtk);
      stringBuilder.Append(",\"totalStatus\":");
      stringBuilder.Append(this.totalStatus);
      stringBuilder.Append(",\"rankpoint\":");
      stringBuilder.Append(this.rankpoint);
      stringBuilder.Append(",\"mtChallengeFloor\":");
      stringBuilder.Append(this.mtChallengeFloor);
      stringBuilder.Append(",\"mtClearedFloor\":");
      stringBuilder.Append(this.mtClearedFloor);
      stringBuilder.Append(",\"isSupportAI\":");
      stringBuilder.Append(this.isSupportAI);
      stringBuilder.Append(",\"award\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.award));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"rankmatch_score\":");
      stringBuilder.Append(this.rankmatch_score);
      if (GlobalVars.IsVersusDraftMode)
      {
        stringBuilder.Append(",\"support_unit\":");
        stringBuilder.Append("\"");
        stringBuilder.Append(JsonEscape.Escape(this.support_unit));
        stringBuilder.Append("\"");
      }
      stringBuilder.Append(",\"draft_id\":");
      stringBuilder.Append(this.draft_id);
      stringBuilder.Append(",\"guild_id\":");
      stringBuilder.Append(this.guild_id);
      stringBuilder.Append(",\"guild_name\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(JsonEscape.Escape(this.guild_name));
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"isAuto\":");
      stringBuilder.Append(this.isAuto);
      stringBuilder.Append(",\"isAutoTreasure\":");
      stringBuilder.Append(this.isAutoTreasure);
      stringBuilder.Append(",\"isAutoNoSkill\":");
      stringBuilder.Append(this.isAutoNoSkill);
      stringBuilder.Append(",\"units\":[");
      if (this.units != null)
      {
        for (int index = 0; index < this.units.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(this.units[index].Serialize());
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    public SupportData CreateSupportData()
    {
      if (this.units == null || this.units.Length <= 0)
        return (SupportData) null;
      if (this.units[0] == null || this.units[0].unit == null)
        return (SupportData) null;
      return new SupportData()
      {
        FUID = this.FUID,
        Unit = this.units[0].unit,
        PlayerName = this.playerName,
        PlayerLevel = this.playerLevel,
        UnitID = this.units[0].unit.UnitID,
        UnitLevel = this.units[0].unit.Lv,
        UnitRarity = this.units[0].unit.Rarity,
        JobID = this.units[0].unit.CurrentJob.JobID,
        LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(this.units[0].unit.Rarity, this.units[0].unit.AwakeLv)
      };
    }

    public void CreateJsonUnitData()
    {
      if (this.units == null)
        return;
      for (int index = 0; index < this.units.Length; ++index)
      {
        if (this.units[index].unit != null)
        {
          string str = this.units[index].unit.Serialize();
          this.units[index].unitJson = JsonUtility.FromJson<Json_Unit>(str);
        }
      }
    }

    public static JSON_MyPhotonPlayerParam Create(int playerID = 0, int playerIndex = 0)
    {
      JSON_MyPhotonPlayerParam photonPlayerParam = new JSON_MyPhotonPlayerParam();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) PunMonoSingleton<MyPhoton>.Instance, (UnityEngine.Object) null))
        return (JSON_MyPhotonPlayerParam) null;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      photonPlayerParam.playerID = playerID;
      photonPlayerParam.playerIndex = playerIndex;
      photonPlayerParam.playerName = player.Name;
      photonPlayerParam.playerLevel = player.Lv;
      photonPlayerParam.FUID = player.FUID;
      photonPlayerParam.UID = MonoSingleton<GameManager>.Instance.DeviceId;
      photonPlayerParam.award = player.SelectedAward;
      photonPlayerParam.guild_id = player.PlayerGuild == null || !player.PlayerGuild.IsJoined ? 0 : player.PlayerGuild.Gid;
      photonPlayerParam.guild_name = player.PlayerGuild == null || !player.PlayerGuild.IsJoined ? string.Empty : player.PlayerGuild.Name;
      photonPlayerParam.isAuto = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONFIG_USE_AUTO_PLAY_MULTI);
      photonPlayerParam.isAutoTreasure = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_TREASURE_MULTI);
      photonPlayerParam.isAutoNoSkill = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_DISABLE_SKILL_MULTI);
      PlayerPartyTypes playerPartyTypes;
      switch (GlobalVars.SelectedMultiPlayRoomType)
      {
        case JSON_MyPhotonRoomParam.EType.RAID:
          playerPartyTypes = PlayerPartyTypes.Multiplay;
          break;
        case JSON_MyPhotonRoomParam.EType.VERSUS:
          playerPartyTypes = PlayerPartyTypes.Versus;
          break;
        case JSON_MyPhotonRoomParam.EType.TOWER:
          playerPartyTypes = PlayerPartyTypes.MultiTower;
          break;
        case JSON_MyPhotonRoomParam.EType.RANKMATCH:
          playerPartyTypes = PlayerPartyTypes.RankMatch;
          break;
        default:
          playerPartyTypes = PlayerPartyTypes.Multiplay;
          break;
      }
      QuestParam quest = (QuestParam) null;
      PartyData party;
      int length;
      int lastSelectionIndex;
      List<PartyEditData> teams;
      switch (playerPartyTypes)
      {
        case PlayerPartyTypes.Versus:
        case PlayerPartyTypes.RankMatch:
          party = player.Partys[(int) playerPartyTypes];
          length = party.MAX_MAINMEMBER;
          teams = PartyUtility.LoadTeamPresets(playerPartyTypes, out lastSelectionIndex);
          if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            break;
          }
          break;
        case PlayerPartyTypes.MultiTower:
          party = player.Partys[(int) playerPartyTypes];
          length = party.MAX_UNIT;
          teams = PartyUtility.LoadTeamPresets(playerPartyTypes, out lastSelectionIndex);
          break;
        default:
          party = player.Partys[(int) playerPartyTypes];
          length = party.MAX_UNIT;
          if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (quest != null)
              length = (int) quest.unitNum;
          }
          teams = new List<PartyEditData>()
          {
            new PartyEditData(string.Empty, party)
          };
          break;
      }
      UnitData[] unitDataArray = new UnitData[length];
      if (teams != null && teams.Count > 0)
      {
        PartyUtility.ResetToDefaultTeamIfNeeded(playerPartyTypes, quest, teams);
        UnitData[] units = teams[0].Units;
        for (int index = 0; index < unitDataArray.Length && index < units.Length; ++index)
          unitDataArray[index] = units[index];
      }
      else
      {
        for (int index = 0; index < unitDataArray.Length; ++index)
        {
          long unitUniqueId = party.GetUnitUniqueID(index);
          unitDataArray[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitUniqueId);
        }
      }
      int num1 = 0;
      int num2 = 0;
      photonPlayerParam.leaderID = party.LeaderIndex;
      List<JSON_MyPhotonPlayerParam.UnitDataElem> source = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
      for (int index = 0; index < unitDataArray.Length; ++index)
      {
        UnitData unitData = unitDataArray[index];
        if (unitData != null)
        {
          JSON_MyPhotonPlayerParam.UnitDataElem unitDataElem = new JSON_MyPhotonPlayerParam.UnitDataElem()
          {
            slotID = num1
          };
          ++num1;
          unitDataElem.place = GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER ? (playerPartyTypes != PlayerPartyTypes.RankMatch ? player.GetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index) : player.GetVersusPlacement(PlayerPrefsUtility.RANKMATCH_ID_KEY + (object) index)) : -1;
          unitDataElem.sub = index < party.MAX_MAINMEMBER || party.MAX_SUBMEMBER <= 0 ? 0 : 1;
          unitDataElem.unit = unitData;
          source.Add(unitDataElem);
          num2 = num2 + (int) unitData.Status.param.atk + (int) unitData.Status.param.mag;
        }
      }
      photonPlayerParam.units = source.ToArray();
      photonPlayerParam.totalAtk = num2;
      photonPlayerParam.totalStatus = PartyUtility.CalcTotalCombatPower(source.Select<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>) (unit_elem => unit_elem.unit)));
      photonPlayerParam.rankpoint = player.VERSUS_POINT;
      photonPlayerParam.mtChallengeFloor = MonoSingleton<GameManager>.Instance.GetMTChallengeFloor();
      photonPlayerParam.mtClearedFloor = MonoSingleton<GameManager>.Instance.GetMTClearedMaxFloor();
      photonPlayerParam.rankmatch_score = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS)
      {
        Json_Unit jsonObject = JSONParser.parseJSONObject<Json_Unit>(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID).Serialize());
        jsonObject.concept_cards = (JSON_ConceptCard[]) null;
        jsonObject.quest_clear_unlocks = (string[]) null;
        jsonObject.abil = (Json_MasterAbility) null;
        jsonObject.c_abil = (Json_CollaboAbility) null;
        jsonObject.door_abils = (Json_Ability[]) null;
        jsonObject.runes = (Json_RuneData[]) null;
        jsonObject.jobs[0].artis = (Json_Artifact[]) null;
        jsonObject.jobs[0].abils = (Json_Ability[]) null;
        jsonObject.jobs[0].select = (Json_JobSelectable) null;
        jsonObject.jobs = new Json_Job[1]
        {
          jsonObject.jobs[0]
        };
        UnitData unitData = new UnitData();
        unitData.Deserialize(jsonObject);
        photonPlayerParam.support_unit = unitData.Serialize();
      }
      photonPlayerParam.draft_id = VersusDraftList.DraftID;
      return photonPlayerParam;
    }

    public static JSON_MyPhotonPlayerParam CreateSupportAI(
      MultiSupportData support,
      int playerIndex,
      int playerID)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) PunMonoSingleton<MyPhoton>.Instance, (UnityEngine.Object) null))
        return (JSON_MyPhotonPlayerParam) null;
      JSON_MyPhotonPlayerParam supportAi = new JSON_MyPhotonPlayerParam();
      supportAi.playerID = 1000 + playerID;
      supportAi.playerIndex = playerIndex;
      supportAi.playerName = support.Name;
      supportAi.playerLevel = support.Lv;
      supportAi.FUID = support.FUID;
      supportAi.UID = support.UID;
      supportAi.guild_id = support.Guild == null ? 0 : support.Guild.id;
      supportAi.guild_name = support.Guild == null ? string.Empty : support.Guild.name;
      supportAi.leaderID = 0;
      supportAi.state = 1;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = 0;
      int num2 = 0;
      List<JSON_MyPhotonPlayerParam.UnitDataElem> source = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
      for (int index = 0; index < support.Units.Count; ++index)
      {
        UnitData unit = support.Units[index];
        if (unit != null)
        {
          JSON_MyPhotonPlayerParam.UnitDataElem unitDataElem = new JSON_MyPhotonPlayerParam.UnitDataElem()
          {
            slotID = num1,
            place = index,
            sub = 0,
            unit = unit
          };
          ++num1;
          source.Add(unitDataElem);
          num2 = num2 + (int) unit.Status.param.atk + (int) unit.Status.param.mag;
        }
      }
      supportAi.units = source.ToArray();
      supportAi.totalAtk = num2;
      supportAi.totalStatus = PartyUtility.CalcTotalCombatPower(source.Select<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>) (unit_elem => unit_elem.unit)));
      supportAi.rankpoint = 0;
      supportAi.mtChallengeFloor = 0;
      supportAi.mtClearedFloor = 0;
      supportAi.rankmatch_score = 0;
      supportAi.draft_id = 0;
      supportAi.isSupportAI = 1;
      return supportAi;
    }

    public void UpdateMultiTowerPlacement(bool isDefault)
    {
      if (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER)
        return;
      if (isDefault)
      {
        for (int index = 0; index < this.units.Length; ++index)
        {
          if (this.units[index] != null)
          {
            this.units[index].place = (this.playerIndex - 1) * 2 + index;
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index, this.units[index].place, true);
          }
        }
      }
      else
      {
        for (int index = 0; index < this.units.Length; ++index)
        {
          if (this.units[index] != null)
            this.units[index].place = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index);
        }
      }
    }

    [Serializable]
    public class UnitDataElem
    {
      public int slotID;
      public int place;
      public int sub;
      public Json_Unit unitJson;
      public UnitData unit;

      public string Serialize()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("{");
        stringBuilder.Append("\"slotID\":");
        stringBuilder.Append(this.slotID);
        stringBuilder.Append(",\"place\":");
        stringBuilder.Append(this.place);
        stringBuilder.Append(",\"sub\":");
        stringBuilder.Append(this.sub);
        if (this.unit != null)
        {
          stringBuilder.Append(",\"unitJson\":");
          stringBuilder.Append(this.unit.Serialize2());
        }
        else if (this.unitJson != null)
        {
          this.unit = new UnitData();
          this.unit.Deserialize(this.unitJson);
          stringBuilder.Append(",\"unitJson\":");
          stringBuilder.Append(this.unit.Serialize2());
        }
        stringBuilder.Append("}");
        return stringBuilder.ToString();
      }
    }

    public enum EState
    {
      NOP,
      READY,
      START,
      START_CONFIRM,
      EDIT,
      FLOOR_SELECT,
      NUM,
    }
  }
}
