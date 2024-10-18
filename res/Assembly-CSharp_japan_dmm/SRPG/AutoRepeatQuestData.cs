// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestData
  {
    private string mQuestIname;
    private long[] mUnits;
    private int mTimePerLap;
    private int mLapMax;
    private int mLapMaxEx;
    private bool mIsBoxFull;
    private AutoRepeatQuestData.EndType mStopReason;
    private int mCurrentLap;
    private int mGold;
    private AutoRepeatQuestData.DropItem[][] mDrops;
    private DateTime mStartTime;
    private bool mIsInitialized;
    private bool mIsMeasuring;

    public string QuestIname => this.mQuestIname;

    public long[] Units => this.mUnits;

    public int TimePerLap => this.mTimePerLap;

    public int LapMax => this.mLapMax;

    public int CurrentLap => this.mCurrentLap;

    public bool IsBoxFull => this.mIsBoxFull;

    public int Gold => this.mGold;

    public AutoRepeatQuestData.DropItem[][] Drops => this.mDrops;

    public DateTime StartTime => this.mStartTime;

    public bool IsInitialized => this.mIsInitialized;

    public bool IsMeasuring => this.mIsMeasuring;

    public bool IsExistRecord => this.State != AutoRepeatQuestData.eState.IDLE;

    public AutoRepeatQuestData.EndType StopReason => this.mStopReason;

    public static void SetLocalNotification(AutoRepeatQuestData progress)
    {
      if (progress == null)
        return;
      string empty = string.Empty;
      string message;
      switch (progress.StopReason)
      {
        case AutoRepeatQuestData.EndType.COMPLETE:
          message = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_NOTIFICATION_COMPLETE");
          break;
        case AutoRepeatQuestData.EndType.AP_LACK:
          message = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_NOTIFICATION_AP");
          break;
        case AutoRepeatQuestData.EndType.BOX_FULL:
          message = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_NOTIFICATION_BOX");
          break;
        default:
          message = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_NOTIFICATION_DEFAULT");
          break;
      }
      MyLocalNotification.SetAutoRepeatQuest(message, progress.EndTime);
    }

    public DateTime EndTime
    {
      get
      {
        return this.mStartTime.AddSeconds((double) (this.mTimePerLap * this.mLapMax + this.GetCoolTime(this.mLapMax)));
      }
    }

    public DateTime EndTimeEx
    {
      get
      {
        return this.mStartTime.AddSeconds((double) (this.mTimePerLap * this.mLapMaxEx + this.GetCoolTime(this.mLapMaxEx)));
      }
    }

    public AutoRepeatQuestData.eState State
    {
      get
      {
        if (this.mLapMax <= 0)
          return AutoRepeatQuestData.eState.IDLE;
        return this.EndTime > TimeManager.ServerTime ? AutoRepeatQuestData.eState.AUTO_REPEAT_NOW : AutoRepeatQuestData.eState.AUTO_REPEAT_END;
      }
    }

    public int ElapsedTime => (int) (TimeManager.ServerTime - this.mStartTime).TotalSeconds;

    public void Deserialize(Json_AutoRepeatQuestData json, bool override_drop = true)
    {
      if (json != null)
      {
        this.mQuestIname = json.qid;
        if (json.units != null)
        {
          this.mUnits = new long[json.units.Length];
          for (int index = 0; index < json.units.Length; ++index)
            this.mUnits[index] = (long) json.units[index];
        }
        this.mTimePerLap = json.time_per_lap;
        this.mLapMax = json.lap_num;
        this.mLapMaxEx = json.lap_num_not_box;
        this.mIsBoxFull = json.is_full_box > 0;
        this.mCurrentLap = json.current_lap_num;
        if (!string.IsNullOrEmpty(json.start_time))
          this.mStartTime = DateTime.Parse(json.start_time);
        this.mIsMeasuring = json.auto_repeat_check == 1;
        this.mStopReason = (AutoRepeatQuestData.EndType) json.stop_reason;
        AutoRepeatQuestData.DropItem[][] b = (AutoRepeatQuestData.DropItem[][]) null;
        if (json.drops != null)
        {
          b = new AutoRepeatQuestData.DropItem[json.drops.Length][];
          for (int index1 = 0; index1 < json.drops.Length; ++index1)
          {
            List<AutoRepeatQuestData.DropItem> dropItemList = new List<AutoRepeatQuestData.DropItem>();
            if (json.drops[index1] != null)
            {
              for (int index2 = 0; index2 < json.drops[index1].Length; ++index2)
              {
                if (json.drops[index1][index2] != null)
                {
                  AutoRepeatQuestData.DropItem dropItem = new AutoRepeatQuestData.DropItem(json.drops[index1][index2]);
                  dropItemList.Add(dropItem);
                }
              }
            }
            b[index1] = dropItemList.ToArray();
          }
        }
        this.mDrops = !override_drop ? this.AppendDropItems(this.mDrops, b) : b;
        this.mGold = json.gold;
      }
      this.mIsInitialized = true;
    }

    public void Reset()
    {
      this.mQuestIname = string.Empty;
      this.mUnits = (long[]) null;
      this.mTimePerLap = 0;
      this.mLapMax = 0;
      this.mLapMaxEx = 0;
      this.mCurrentLap = 0;
      this.mGold = 0;
      this.mStartTime = DateTime.MinValue;
      this.mDrops = (AutoRepeatQuestData.DropItem[][]) null;
      this.mIsMeasuring = false;
    }

    public void SetLapMax(int new_lap_max) => this.mLapMax = new_lap_max;

    private int GetCoolTime(int lap_max)
    {
      return Mathf.Max(0, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AutoRepeatCoolTime * (lap_max - 1));
    }

    public List<Unit.DropItem> GetDropItem()
    {
      List<Unit.DropItem> dropItem1 = new List<Unit.DropItem>();
      if (this.mDrops != null)
      {
        for (int index1 = 0; index1 < this.mDrops.Length; ++index1)
        {
          if (this.mDrops[index1] != null)
          {
            for (int index2 = 0; index2 < this.mDrops[index1].Length; ++index2)
            {
              if (this.mDrops[index1][index2] != null)
              {
                Unit.DropItem dropItem2 = new Unit.DropItem();
                switch (this.mDrops[index1][index2].type)
                {
                  case EBattleRewardType.None:
                    continue;
                  case EBattleRewardType.Item:
                    ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mDrops[index1][index2].iname);
                    if (itemParam == null)
                    {
                      DebugUtility.LogError("MasterParamに存在しないアイテムがドロップデータに含まれています => " + this.mDrops[index1][index2].iname);
                      continue;
                    }
                    dropItem2.itemParam = itemParam;
                    break;
                  case EBattleRewardType.ConceptCard:
                    ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.mDrops[index1][index2].iname);
                    if (conceptCardParam == null)
                    {
                      DebugUtility.LogError("MasterParamに存在しない真理念装がドロップデータに含まれています => " + this.mDrops[index1][index2].iname);
                      continue;
                    }
                    dropItem2.conceptCardParam = conceptCardParam;
                    break;
                  default:
                    DebugUtility.LogError("EBattleRewardType に定義されていないアイテム種別がドロップしました => " + this.mDrops[index1][index2].iname);
                    continue;
                }
                dropItem2.num = (OInt) this.mDrops[index1][index2].num;
                dropItem2.is_secret = (OBool) (this.mDrops[index1][index2].secret != 0);
                dropItem1.Add(dropItem2);
              }
            }
          }
        }
      }
      return dropItem1;
    }

    public List<QuestResult.DropItemData> GetDropItemData()
    {
      List<QuestResult.DropItemData> dropItemData1 = new List<QuestResult.DropItemData>();
      List<Unit.DropItem> dropItem = this.GetDropItem();
      for (int index = 0; index < dropItem.Count; ++index)
      {
        QuestResult.DropItemData dropItemData2 = new QuestResult.DropItemData();
        dropItemData2.SetupDropItemData(dropItem[index].BattleRewardType, (long) index, dropItem[index].Iname, (int) dropItem[index].num);
        dropItemData1.Add(dropItemData2);
      }
      return dropItemData1;
    }

    public List<UnitData> GetUnitDatas()
    {
      List<UnitData> unitDatas = new List<UnitData>();
      for (int index = 0; index < this.mUnits.Length; ++index)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnits[index]);
        if (unitDataByUniqueId != null)
          unitDatas.Add(unitDataByUniqueId);
      }
      return unitDatas;
    }

    public List<QuestParam> GetCharacterQuestParams()
    {
      List<QuestParam> characterQuestParams = new List<QuestParam>();
      List<UnitData> unitDatas = this.GetUnitDatas();
      for (int index = 0; index < unitDatas.Count; ++index)
      {
        UnitData.CharacterQuestParam charaEpisodeData = unitDatas[index].GetCurrentCharaEpisodeData();
        if (charaEpisodeData != null)
          characterQuestParams.Add(charaEpisodeData.Param);
      }
      return characterQuestParams;
    }

    private AutoRepeatQuestData.DropItem[][] AppendDropItems(
      AutoRepeatQuestData.DropItem[][] a,
      AutoRepeatQuestData.DropItem[][] b)
    {
      if (a == null && b == null)
        return (AutoRepeatQuestData.DropItem[][]) null;
      if (a == null)
        return b;
      if (b == null)
        return a;
      AutoRepeatQuestData.DropItem[][] destinationArray = new AutoRepeatQuestData.DropItem[a.Length + b.Length][];
      Array.Copy((Array) a, (Array) destinationArray, a.Length);
      Array.Copy((Array) b, 0, (Array) destinationArray, a.Length, b.Length);
      return destinationArray;
    }

    public enum eState
    {
      IDLE,
      AUTO_REPEAT_NOW,
      AUTO_REPEAT_END,
    }

    public enum EndType
    {
      COMPLETE,
      AP_LACK,
      BOX_FULL,
      SUSPEND,
      EXPIRED,
      CHALLENGE_LIMIT,
      LAP_LIMIT,
    }

    [Serializable]
    public class DropItem
    {
      public string iname;
      public EBattleRewardType type;
      public int num;
      public int secret;

      public DropItem(BattleCore.Json_BtlDrop json)
      {
        this.iname = json.iname;
        this.type = json.dropItemType;
        this.num = json.num;
        this.secret = json.secret;
      }
    }
  }
}
