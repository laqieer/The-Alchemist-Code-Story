// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlComEnd : WebAPI
  {
    public ReqBtlComEnd(
      string req_fuid,
      int opp_rank,
      int my_rank,
      BtlResultTypes result,
      int[] beats,
      int[] itemSteals,
      int[] goldSteals,
      int[] missions,
      int[] missions_log,
      string[] fuid,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BtlEndTypes apiType,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "btl/colo/exec";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"fuid\":\"");
      stringBuilder.Append(req_fuid);
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"opp_rank\":");
      stringBuilder.Append(opp_rank);
      stringBuilder.Append(",\"my_rank\":");
      stringBuilder.Append(my_rank);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString() + "," + this.makeBody(true, 0L, 0, result, beats, itemSteals, goldSteals, missions, missions_log, fuid, usedItems, response, apiType, trophyprog, bingoprog));
      this.callback = response;
    }

    public ReqBtlComEnd(
      long btlid,
      int time,
      BtlResultTypes result,
      int[] beats,
      int[] itemSteals,
      int[] goldSteals,
      int[] missions,
      int[] missions_log,
      string[] fuid,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BtlEndTypes apiType,
      string trophyprog = null,
      string bingoprog = null,
      int elem = 0,
      string rankingQuestEndParam = null,
      bool is_rehash = false,
      bool? is_skip = null,
      BattleCore.Record record = null,
      int time_per_lap = 0)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("btl/");
      stringBuilder.Append(apiType.ToString());
      stringBuilder.Append("/end");
      this.name = stringBuilder.ToString();
      this.body = WebAPI.GetRequestString(this.makeBody(false, btlid, time, result, beats, itemSteals, goldSteals, missions, missions_log, fuid, usedItems, response, apiType, trophyprog, bingoprog, elem, rankingQuestEndParam, is_rehash, is_skip, record, time_per_lap));
      this.callback = response;
    }

    private string makeBody(
      bool is_arena,
      long btlid,
      int time,
      BtlResultTypes result,
      int[] beats,
      int[] itemSteals,
      int[] goldSteals,
      int[] missions,
      int[] missions_log,
      string[] fuid,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BtlEndTypes apiType,
      string trophyprog,
      string bingoprog,
      int elem = 0,
      string rankingQuestEndParam = null,
      bool is_rehash = false,
      bool? is_skip = null,
      BattleCore.Record record = null,
      int time_per_lap = 0)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Length = 0;
      if (!is_arena)
      {
        stringBuilder.Append("\"btlid\":");
        stringBuilder.Append(btlid);
        stringBuilder.Append(',');
      }
      stringBuilder.Append("\"btlendparam\":{");
      stringBuilder.Append("\"time\":");
      stringBuilder.Append(time);
      stringBuilder.Append(',');
      stringBuilder.Append("\"result\":\"");
      switch (result)
      {
        case BtlResultTypes.Win:
          stringBuilder.Append("win");
          break;
        case BtlResultTypes.Lose:
          stringBuilder.Append("lose");
          break;
        case BtlResultTypes.Retire:
          stringBuilder.Append("retire");
          break;
        case BtlResultTypes.Cancel:
          stringBuilder.Append("cancel");
          break;
      }
      if (result == BtlResultTypes.Win)
      {
        if (beats == null)
          beats = new int[0];
        if (itemSteals == null)
          itemSteals = new int[0];
        if (goldSteals == null)
          goldSteals = new int[0];
        if (missions == null)
          missions = new int[0];
        if (missions_log == null)
          missions_log = new int[0];
      }
      if (result != BtlResultTypes.Cancel && usedItems == null)
        usedItems = new Dictionary<OString, OInt>();
      stringBuilder.Append("\",");
      if (beats != null)
      {
        stringBuilder.Append("\"beats\":[");
        for (int index = 0; index < beats.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(beats[index].ToString());
        }
        stringBuilder.Append("],");
      }
      if (itemSteals != null || goldSteals != null)
      {
        stringBuilder.Append("\"steals\":{");
        if (itemSteals != null)
        {
          stringBuilder.Append("\"items\":[");
          for (int index = 0; index < itemSteals.Length; ++index)
          {
            stringBuilder.Append(itemSteals[index].ToString());
            if (index != beats.Length - 1)
              stringBuilder.Append(',');
          }
          stringBuilder.Append("]");
        }
        if (goldSteals != null)
        {
          if (itemSteals != null)
            stringBuilder.Append(',');
          stringBuilder.Append("\"golds\":[");
          for (int index = 0; index < goldSteals.Length; ++index)
          {
            stringBuilder.Append(goldSteals[index].ToString());
            if (index != beats.Length - 1)
              stringBuilder.Append(",");
          }
          stringBuilder.Append("]");
        }
        stringBuilder.Append("},");
      }
      if (missions != null)
      {
        stringBuilder.Append("\"missions\":[");
        for (int index = 0; index < missions.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(missions[index].ToString());
        }
        stringBuilder.Append("],");
      }
      if (missions_log != null)
      {
        stringBuilder.Append("\"missions_log\":[");
        for (int index = 0; index < missions_log.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(missions_log[index].ToString());
        }
        stringBuilder.Append("],");
      }
      if (usedItems != null)
      {
        stringBuilder.Append("\"inputs\":[");
        int num = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in usedItems)
        {
          if (num > 0)
            stringBuilder.Append(',');
          stringBuilder.Append("{");
          stringBuilder.Append("\"use\":\"");
          stringBuilder.Append((string) usedItem.Key);
          stringBuilder.Append("\",");
          stringBuilder.Append("\"n\":");
          stringBuilder.Append((int) usedItem.Value);
          stringBuilder.Append("}");
          ++num;
        }
        stringBuilder.Append("],");
      }
      if (apiType == BtlEndTypes.multi)
      {
        stringBuilder.Append("\"token\":\"");
        stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
        stringBuilder.Append("\",");
      }
      if (!string.IsNullOrEmpty(rankingQuestEndParam))
      {
        stringBuilder.Append(rankingQuestEndParam);
        stringBuilder.Append(",");
      }
      if (result == BtlResultTypes.Cancel && is_rehash)
      {
        stringBuilder.Append("\"is_rehash\":");
        stringBuilder.Append(1);
        stringBuilder.Append(",");
      }
      if (is_skip.HasValue)
      {
        stringBuilder.Append("\"is_skip\":");
        stringBuilder.Append(!is_skip.Value ? 0 : 1);
        stringBuilder.Append(",");
      }
      if (result == BtlResultTypes.Win && record != null)
      {
        if (record.mInspSkillInsList.Count != 0)
        {
          stringBuilder.Append("\"sins\":[");
          for (int index1 = 0; index1 < record.mInspSkillInsList.Count; ++index1)
          {
            BattleCore.Record.InspSkill mInspSkillIns = record.mInspSkillInsList[index1];
            if (index1 != 0)
              stringBuilder.Append(",");
            stringBuilder.Append("{");
            stringBuilder.Append("\"uiid\":");
            stringBuilder.Append(mInspSkillIns.mUnitData.UniqueID);
            stringBuilder.Append(",");
            stringBuilder.Append("\"artifact\":[");
            for (int index2 = 0; index2 < mInspSkillIns.mUnitInspList.Count; ++index2)
            {
              Unit.UnitInsp mUnitInsp = mInspSkillIns.mUnitInspList[index2];
              if (index2 != 0)
                stringBuilder.Append(",");
              stringBuilder.Append("{");
              stringBuilder.Append("\"iid\":");
              stringBuilder.Append((long) mUnitInsp.mArtifact.UniqueID);
              stringBuilder.Append(",");
              stringBuilder.Append("\"slot\":");
              stringBuilder.Append((int) mUnitInsp.mSlotNo);
              stringBuilder.Append("}");
            }
            stringBuilder.Append("]}");
          }
          stringBuilder.Append("],");
        }
        if (record.mInspSkillUseList.Count != 0)
        {
          stringBuilder.Append("\"use_sins\":[");
          for (int index3 = 0; index3 < record.mInspSkillUseList.Count; ++index3)
          {
            BattleCore.Record.InspSkill mInspSkillUse = record.mInspSkillUseList[index3];
            if (index3 != 0)
              stringBuilder.Append(",");
            stringBuilder.Append("{");
            stringBuilder.Append("\"uiid\":");
            stringBuilder.Append(mInspSkillUse.mUnitData.UniqueID);
            stringBuilder.Append(",");
            stringBuilder.Append("\"artifact\":[");
            for (int index4 = 0; index4 < mInspSkillUse.mUnitInspList.Count; ++index4)
            {
              Unit.UnitInsp mUnitInsp = mInspSkillUse.mUnitInspList[index4];
              if (index4 != 0)
                stringBuilder.Append(",");
              stringBuilder.Append("{");
              stringBuilder.Append("\"iid\":");
              stringBuilder.Append((long) mUnitInsp.mArtifact.UniqueID);
              stringBuilder.Append("}");
            }
            stringBuilder.Append("]}");
          }
          stringBuilder.Append("],");
        }
      }
      string empty = string.Empty;
      string str = record == null || !record.mIsUseAutoPlayMode ? (record == null || record.mIsUseAutoPlayMode ? "none" : "off") : "on";
      stringBuilder.Append("\"auto_status\":");
      stringBuilder.Append("\"" + str + "\"");
      stringBuilder.Append(",");
      if (time_per_lap > 0)
      {
        stringBuilder.Append("\"time_per_lap\":");
        stringBuilder.Append(time_per_lap);
        stringBuilder.Append(",");
      }
      if (stringBuilder[stringBuilder.Length - 1] == ',')
        --stringBuilder.Length;
      stringBuilder.Append('}');
      if (apiType == BtlEndTypes.multi && fuid != null)
      {
        stringBuilder.Append(",\"fuids\":[");
        for (int index = 0; index < fuid.Length; ++index)
        {
          if (fuid[index] != null)
          {
            if (index != 0)
              stringBuilder.Append(", ");
            stringBuilder.Append("\"");
            stringBuilder.Append(fuid[index]);
            stringBuilder.Append("\"");
          }
        }
        stringBuilder.Append("]");
      }
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      if (elem != 0)
      {
        stringBuilder.Append(",");
        stringBuilder.Append("\"support_elem\":\"");
        stringBuilder.Append(elem);
        stringBuilder.Append("\"");
      }
      return stringBuilder.ToString();
    }

    public static string CreateRankingQuestEndParam(int main_score, int sub_score)
    {
      StringBuilder stringBuilder = new StringBuilder(128);
      stringBuilder.Append("\"score\":{");
      stringBuilder.Append("\"main_score\":");
      stringBuilder.Append(main_score);
      stringBuilder.Append(",");
      stringBuilder.Append("\"sub_score\":");
      stringBuilder.Append(sub_score);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
