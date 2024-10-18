// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Info", 32741)]
  [FlowNode.Pin(201, "TimeOut", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Already Beat", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "Rescue Damage Zero", FlowNode.PinTypes.Output, 203)]
  public class FlowNode_ReqRaidInfo : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      int area_id = 0;
      int boss_id = 0;
      int round = 0;
      string uid = string.Empty;
      switch (RaidManager.Instance.SelectedRaidOwnerType)
      {
        case RaidManager.RaidOwnerType.Self:
          area_id = RaidManager.Instance.CurrentRaidAreaId;
          boss_id = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.BossId;
          round = RaidManager.Instance.CurrentRound;
          break;
        case RaidManager.RaidOwnerType.Rescue:
          area_id = RaidManager.Instance.RescueRaidBossData.AreaId;
          boss_id = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.BossId;
          round = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.Round;
          uid = RaidManager.Instance.RescueRaidBossData.OwnerUID;
          break;
        case RaidManager.RaidOwnerType.Rescue_Temp:
          area_id = RaidManager.Instance.SelectedRaidRescueMember.AreaId;
          boss_id = RaidManager.Instance.SelectedRaidRescueMember.BossId;
          round = RaidManager.Instance.SelectedRaidRescueMember.Round;
          uid = RaidManager.Instance.SelectedRaidRescueMember.UID;
          break;
        case RaidManager.RaidOwnerType.Self_Cleared:
          area_id = RaidManager.Instance.CurrentRaidAreaId;
          boss_id = RaidManager.Instance.SelectedClearedRaidBossInfo.BossId;
          round = RaidManager.Instance.CurrentRound;
          break;
      }
      return (WebAPI) new ReqRaidInfo(area_id, boss_id, round, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.ResetError();
        UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_BATTLE_ERR_BEAT_RESCUE_TITLE"), LocalizedText.Get("sys.RAID_BATTLE_ERR_BEAT_RESCUE_MSG"), (UIUtility.DialogResultEvent) (gameObject => this.ActivateOutputLinks(202)), (GameObject) null, false, -1);
        return false;
      }
      WebAPI.JSON_BodyResponse<ReqRaidInfo.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidInfo.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/info");
        if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null)
          throw new Exception("RaidManager not exists : /raidboss/info");
        RaidManager.Instance.Setup(jsonObject.body);
        if (jsonObject.body.raidboss.boss_info.is_timeover != 0)
        {
          int num = 1;
          string str = string.Empty;
          RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(jsonObject.body.raidboss.area_id);
          if (raidArea != null)
            num = raidArea.Order;
          RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(jsonObject.body.raidboss.boss_info.boss_id);
          if (raidBoss != null)
            str = raidBoss.Name;
          UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_BATTLE_TIME_OVER_TITLE"), string.Format(LocalizedText.Get("sys.RAID_BATTLE_TIME_OVER_MSG"), (object) num, (object) str), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(201)), (GameObject) null, false, -1);
          return false;
        }
        if (jsonObject.body.raidboss.boss_info.is_rescue_damage_zero != 0)
        {
          UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_BATTLE_RESCUE_DAMAGE_ZERO_TITLE"), LocalizedText.Get("sys.RAID_BATTLE_RESCUE_DAMAGE_ZERO_MSG"), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(203)), (GameObject) null, false, -1);
          return false;
        }
        if (jsonObject.body.raidboss.boss_info.is_beat_resucue != 0)
        {
          string str = string.Empty;
          RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(jsonObject.body.raidboss.boss_info.boss_id);
          if (raidBoss != null)
            str = raidBoss.Name;
          UIUtility.SystemMessage(string.Format(LocalizedText.Get("sys.RAID_BATTLE_BEAT_RESCUE_TITLE"), (object) str), string.Format(LocalizedText.Get("sys.RAID_BATTLE_BEAT_RESCUE_MSG"), (object) str), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        DownloadUtility.PrepareRaidBossAsset(MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(jsonObject.body.raidboss.boss_info.boss_id));
        this.StartCoroutine(this.DownloadUnitImage());
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return false;
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqRaidInfo.\u003CDownloadUnitImage\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
