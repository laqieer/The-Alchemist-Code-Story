// Decompiled with JetBrains decompiler
// Type: SRPG.RaidArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RaidArea : MonoBehaviour
  {
    [SerializeField]
    private RaidAreaListAssets mRaidAreaList;
    [SerializeField]
    private GameObject mStageRaidIcon;
    [SerializeField]
    private GameObject mStageRaidBossIcon;
    [SerializeField]
    private Text mRaidAreaTitleText;
    [SerializeField]
    private Transform mRaidAreaBGParent;
    private List<RaidBossIcon> mRaidBossIconList;
    private RaidStagePoint mRaidStagePoint;
    private LoadRequest mResourceRequest;
    private GameObject mRaidAreaBG;
    private bool mFinishDownload;
    private bool mIsAreaCleared;
    private bool mSelectingRandomRaid;

    public bool FinishDownload => this.mFinishDownload;

    public bool IsAreaCleared => this.mIsAreaCleared;

    public void FinishSelectingRandomRaid() => this.mSelectingRandomRaid = false;

    public void Initialize(int areaIndex = -1)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidStagePoint, (UnityEngine.Object) null) && areaIndex >= 0)
      {
        RaidAreaParam raidAreaByOrder = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidAreaByOrder(RaidManager.Instance.RaidPeriodId, areaIndex + 1);
        if (raidAreaByOrder != null && this.mRaidAreaList.RaidStagePointList.Count > raidAreaByOrder.BossCount)
        {
          RaidStagePoint raidStagePoint = this.mRaidAreaList.RaidStagePointList[raidAreaByOrder.BossCount];
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) raidStagePoint, (UnityEngine.Object) null))
            this.mRaidStagePoint = UnityEngine.Object.Instantiate<RaidStagePoint>(raidStagePoint, ((Component) this).transform);
        }
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidAreaBG, (UnityEngine.Object) null) && areaIndex >= 0 && this.mRaidAreaList.RaidAreaBG.Length > areaIndex)
        this.mResourceRequest = AssetManager.LoadAsync<GameObject>(this.mRaidAreaList.RaidAreaBG[areaIndex]);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStageRaidIcon, (UnityEngine.Object) null) || this.mRaidStagePoint.StageList == null)
        return;
      if (this.mRaidBossIconList != null)
      {
        for (int index = 0; index < this.mRaidBossIconList.Count; ++index)
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mRaidBossIconList[index]).gameObject);
        this.mRaidBossIconList.Clear();
      }
      this.mRaidBossIconList = new List<RaidBossIcon>();
      for (int i = 0; i < this.mRaidStagePoint.StageList.Count; ++i)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mStageRaidIcon);
        gameObject.transform.SetParent(this.mRaidStagePoint.StageList[i], false);
        RaidBossInfo raidBossInfo = RaidManager.Instance.BeatedRaidBossList.Find((Predicate<RaidBossInfo>) (brb => brb.No == i + 1));
        RaidBossIcon component = gameObject.GetComponent<RaidBossIcon>();
        component.Setup(raidBossInfo, i + 1);
        this.mRaidBossIconList.Add(component);
        if (RaidManager.Instance.CurrentRaidBossData != null && RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.No == i + 1)
          raidBossInfo = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo;
        if (raidBossInfo != null)
        {
          RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidBossInfo.BossId);
          if (raidBoss != null)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss.UnitIName);
            if (unitParam != null)
            {
              AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, (string) null));
              AssetManager.PrepareAssets(AssetPath.UnitImage(unitParam, (string) null));
            }
          }
        }
      }
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.mStageRaidBossIcon);
      gameObject1.transform.SetParent(this.mRaidStagePoint.BossStage, false);
      RaidBossInfo info = RaidManager.Instance.BeatedRaidBossList.Find((Predicate<RaidBossInfo>) (brb => brb.No == 0));
      if (info != null)
        this.mIsAreaCleared = true;
      RaidBossIcon component1 = gameObject1.GetComponent<RaidBossIcon>();
      component1.Setup(info, 0);
      this.mRaidBossIconList.Add(component1);
      RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(RaidManager.Instance.CurrentRaidAreaId);
      if (raidArea != null)
      {
        RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidArea.AreaBossId);
        if (raidBoss != null)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss.UnitIName);
          if (unitParam != null)
          {
            AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, (string) null));
            AssetManager.PrepareAssets(AssetPath.UnitImage(unitParam, (string) null));
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRaidAreaTitleText, (UnityEngine.Object) null))
        {
          this.mRaidAreaTitleText.text = string.Format(LocalizedText.Get("sys.RAID_AREANAME"), (object) raidArea.Order);
          this.StartCoroutine(this.DownloadBGImage());
        }
      }
      this.mFinishDownload = false;
      this.StartCoroutine(this.DownloadUnitImage());
    }

    public void StartSelectRaid()
    {
      this.mSelectingRandomRaid = true;
      this.StartCoroutine(this._StartSelectRaid());
    }

    [DebuggerHidden]
    private IEnumerator _StartSelectRaid()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidArea.\u003C_StartSelectRaid\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidArea.\u003CDownloadUnitImage\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadBGImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidArea.\u003CDownloadBGImage\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }
  }
}
