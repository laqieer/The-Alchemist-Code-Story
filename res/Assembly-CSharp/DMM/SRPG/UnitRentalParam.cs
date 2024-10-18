// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalParam
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
  public class UnitRentalParam
  {
    public string Iname;
    public string UnitId;
    public DateTime BeginAt;
    public DateTime EndAt;
    public OInt PtMax;
    public OInt PtUpLv;
    public OInt PtUpEvol;
    public OInt PtUpAwake;
    public OInt PtUpJobLv;
    public OInt PtUpAbilityLv;
    public OInt PtUpQuestMain;
    public OInt PtUpQuestSub;
    public string NotificationId;
    public List<RentalQuestInfo> UnitQuestInfo = new List<RentalQuestInfo>();
    private UnitParam mUnit;
    private UnitRentalNotificationParam mNotification;
    private static UnitData mGetUnitData;

    public UnitParam Unit
    {
      get
      {
        if (this.mUnit == null && !string.IsNullOrEmpty(this.UnitId) && UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
          this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitId);
        return this.mUnit;
      }
    }

    private UnitRentalNotificationParam Notification
    {
      get
      {
        if (this.mNotification == null && !string.IsNullOrEmpty(this.NotificationId))
          this.mNotification = UnitRentalNotificationParam.GetParam(this.NotificationId);
        return this.mNotification;
      }
    }

    public void Deserialize(JSON_UnitRentalParam json)
    {
      this.Iname = json.iname;
      this.UnitId = json.unit;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.BeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.EndAt = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("開始日時、終了日時の設定が正しくありません!! id : " + json.iname);
      }
      this.PtMax = (OInt) json.pt_max;
      this.PtUpLv = (OInt) json.ptup_lv;
      this.PtUpEvol = (OInt) json.ptup_evol;
      this.PtUpAwake = (OInt) json.ptup_awake;
      this.PtUpJobLv = (OInt) json.ptup_job_lv;
      this.PtUpAbilityLv = (OInt) json.ptup_ability_lv;
      this.PtUpQuestMain = (OInt) json.ptup_quest_main;
      this.PtUpQuestSub = (OInt) json.ptup_quest_sub;
      this.NotificationId = json.notification;
      if (json.quest_infos == null || json.quest_infos.Length <= 0)
        return;
      this.UnitQuestInfo.Clear();
      foreach (JSON_UnitRentalParam.QuestInfo questInfo in json.quest_infos)
        this.UnitQuestInfo.Add(new RentalQuestInfo(questInfo.quest_id, questInfo.point));
    }

    public bool IsWithinPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.BeginAt <= serverTime && serverTime <= this.EndAt;
    }

    public List<QuestParam> GetLiberationQuests(int point)
    {
      List<QuestParam> liberationQuests = new List<QuestParam>();
      for (int index = 0; index < this.UnitQuestInfo.Count; ++index)
      {
        RentalQuestInfo rentalQuestInfo = this.UnitQuestInfo[index];
        if (point >= (int) rentalQuestInfo.Point)
          liberationQuests.Add(rentalQuestInfo.Quest);
      }
      return liberationQuests;
    }

    public List<QuestParam> GetNewLiberationQuests(int old_point, int new_point)
    {
      List<QuestParam> liberationQuests = new List<QuestParam>();
      for (int index = 0; index < this.UnitQuestInfo.Count; ++index)
      {
        RentalQuestInfo rentalQuestInfo = this.UnitQuestInfo[index];
        if (old_point < (int) rentalQuestInfo.Point && (int) rentalQuestInfo.Point <= new_point)
          liberationQuests.Add(rentalQuestInfo.Quest);
      }
      return liberationQuests;
    }

    public static void Deserialize(
      JSON_UnitRentalParam[] jsons,
      ref Dictionary<string, UnitRentalParam> refUnitRentalParams)
    {
      if (jsons == null || jsons.Length <= 0)
        return;
      refUnitRentalParams = new Dictionary<string, UnitRentalParam>();
      foreach (JSON_UnitRentalParam json in jsons)
      {
        UnitRentalParam unitRentalParam = new UnitRentalParam();
        unitRentalParam.Deserialize(json);
        if (!refUnitRentalParams.ContainsKey(json.iname))
          refUnitRentalParams.Add(json.iname, unitRentalParam);
      }
    }

    public static UnitRentalParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (UnitRentalParam) null;
      Dictionary<string, UnitRentalParam> unitRentalParams = MonoSingleton<GameManager>.Instance.MasterParam.UnitRentalParams;
      if (unitRentalParams == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>UnitRentalParam/GetParam no data!</color>"));
        return (UnitRentalParam) null;
      }
      try
      {
        return unitRentalParams[key];
      }
      catch (Exception ex)
      {
        Debug.LogError((object) (ex.Message + " ( Key : " + key + " ) "));
      }
      return (UnitRentalParam) null;
    }

    public static UnitRentalParam GetActiveUnitRentalParam()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (UnitRentalParam) null;
      Dictionary<string, UnitRentalParam> unitRentalParams = MonoSingleton<GameManager>.Instance.MasterParam.UnitRentalParams;
      if (unitRentalParams == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>UnitRentalParam/GetActiveUnitRentalParam no data!</color>"));
        return (UnitRentalParam) null;
      }
      foreach (UnitRentalParam activeUnitRentalParam in unitRentalParams.Values)
      {
        if (activeUnitRentalParam.IsWithinPeriod())
          return activeUnitRentalParam;
      }
      return (UnitRentalParam) null;
    }

    public static bool SendNotificationIsNeed(string unit_iname, int old_point, int new_point)
    {
      if (old_point >= new_point)
        return false;
      UnitRentalParam activeUnitRentalParam = UnitRentalParam.GetActiveUnitRentalParam();
      if (activeUnitRentalParam == null || unit_iname != activeUnitRentalParam.UnitId || (int) activeUnitRentalParam.PtMax <= 0)
        return false;
      int num1 = old_point * 100 / (int) activeUnitRentalParam.PtMax;
      int num2 = new_point * 100 / (int) activeUnitRentalParam.PtMax;
      bool flag = false;
      UnitRentalNotificationParam notification = activeUnitRentalParam.Notification;
      if (notification != null)
      {
        for (int index1 = 0; index1 < notification.NotiList.Count; ++index1)
        {
          UnitRentalNotificationDataParam noti = notification.NotiList[index1];
          for (int index2 = 0; index2 < noti.PerList.Count; ++index2)
          {
            int per = noti.PerList[index2];
            if (num1 < per && per <= num2)
            {
              NotifyList.Push(string.Format(LocalizedText.Get("sys." + noti.SysId), (object) per));
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    public static UnitData GetUnitData
    {
      get => UnitRentalParam.mGetUnitData;
      set => UnitRentalParam.mGetUnitData = value;
    }

    public static void EntryLeaveReturnItems(Json_Item[] return_items)
    {
      UnitRentalParam.ClearLeaveReturnItems();
      if (return_items == null || return_items.Length == 0)
        return;
      List<ItemData> itemDataList = new List<ItemData>(return_items.Length);
      for (int index = 0; index < return_items.Length; ++index)
      {
        ItemData itemData = new ItemData();
        itemData.Deserialize(return_items[index]);
        itemDataList.Add(itemData);
      }
      GlobalVars.ReturnItems = itemDataList;
    }

    public static void ClearLeaveReturnItems() => GlobalVars.ReturnItems = (List<ItemData>) null;
  }
}
