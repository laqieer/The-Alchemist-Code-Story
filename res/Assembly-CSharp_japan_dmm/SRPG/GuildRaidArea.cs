// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidArea : MonoBehaviour
  {
    [SerializeField]
    private GuildRaidAreaListAssets mGuildRaidAreaList;
    [SerializeField]
    private GameObject mStageGuildRaidIcon;
    [SerializeField]
    private GameObject mStageGuildRaidBossIcon;
    [SerializeField]
    private Transform mGuildRaidAreaBGParent;
    private List<GuildRaidBossIcon> mGuildRaidBossIconList;
    private GuildRaidStagePoint mGuildRaidStagePoint;
    private LoadRequest mResourceRequest;
    private GameObject mGuildRaidAreaBG;
    private bool mFinishDownload;
    private bool mIsAreaCleared;

    public bool FinishDownload => this.mFinishDownload;

    public bool IsAreaCleared => this.mIsAreaCleared;

    public void Initialize(int roundIndex)
    {
      if (Object.op_Equality((Object) this.mGuildRaidStagePoint, (Object) null))
      {
        if (this.mGuildRaidAreaList.GuildRaidStagePointList.Count < GuildRaidManager.Instance.AreaBossCount)
          GuildRaidManager.Instance.SetAreaBossOverflow(this.mGuildRaidAreaList.GuildRaidStagePointList.Count);
        if (GuildRaidManager.Instance.AreaBossCount >= 1 && this.mGuildRaidAreaList.GuildRaidStagePointList.Count >= GuildRaidManager.Instance.AreaBossCount)
        {
          GuildRaidStagePoint guildRaidStagePoint = this.mGuildRaidAreaList.GuildRaidStagePointList[GuildRaidManager.Instance.AreaBossCount - 1];
          if (Object.op_Inequality((Object) guildRaidStagePoint, (Object) null))
          {
            this.mGuildRaidStagePoint = Object.Instantiate<GuildRaidStagePoint>(guildRaidStagePoint, ((Component) this).transform);
            this.mGuildRaidStagePoint.SetClearEffect(GuildRaidManager.Instance.CurrentAreaNo - 1);
          }
        }
      }
      if (Object.op_Equality((Object) this.mGuildRaidAreaBG, (Object) null))
        this.mResourceRequest = AssetManager.LoadAsync<GameObject>(this.mGuildRaidAreaList.GuildRaidAreaBG);
      if (Object.op_Equality((Object) this.mStageGuildRaidIcon, (Object) null) || Object.op_Equality((Object) this.mGuildRaidStagePoint, (Object) null) || this.mGuildRaidStagePoint.StageList == null)
        return;
      if (this.mGuildRaidBossIconList != null)
      {
        for (int index = 0; index < this.mGuildRaidBossIconList.Count; ++index)
          Object.Destroy((Object) ((Component) this.mGuildRaidBossIconList[index]).gameObject);
        this.mGuildRaidBossIconList.Clear();
      }
      this.mGuildRaidBossIconList = new List<GuildRaidBossIcon>();
      for (int index = 0; index < this.mGuildRaidStagePoint.StageList.Count; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.mStageGuildRaidIcon);
        gameObject.transform.SetParent(this.mGuildRaidStagePoint.StageList[index], false);
        GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GuildRaidManager.Instance.PeriodId, index + 1);
        GuildRaidBossIcon component = gameObject.GetComponent<GuildRaidBossIcon>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.Setup(guildRaidBossParam, roundIndex + 1, index + 1);
          this.mGuildRaidBossIconList.Add(component);
        }
        if (guildRaidBossParam != null)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
          if (unitParam != null)
          {
            AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, (string) null));
            AssetManager.PrepareAssets(AssetPath.UnitImage(unitParam, (string) null));
          }
        }
      }
      this.StartCoroutine(this.DownloadBGImage());
      this.mFinishDownload = false;
      this.StartCoroutine(this.DownloadUnitImage());
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildRaidArea.\u003CDownloadUnitImage\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadBGImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildRaidArea.\u003CDownloadBGImage\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }
  }
}
