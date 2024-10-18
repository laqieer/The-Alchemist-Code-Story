// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRewardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GvGLeagueRewardList : MonoBehaviour
  {
    [SerializeField]
    private GameObject mRewardAreaRoot;
    [SerializeField]
    private GameObject mRewardLeagueRoot;
    [SerializeField]
    private GameObject mRewardAreaTemplate;
    [SerializeField]
    private GameObject mRewardLeagueTemplate;
    [SerializeField]
    private GameObject mAreaTab;
    [SerializeField]
    private GameObject mLeagueTab;
    private GameObject mNowTab;

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GvGLeagueParam selectLeagueParam = GuildManager.Instance.SelectLeagueParam;
      if (Object.op_Equality((Object) instance, (Object) null) || selectLeagueParam == null)
        return;
      GvGRewardRankingParam rewardRanking = GvGRewardRankingParam.GetRewardRanking(selectLeagueParam.Reward);
      if (rewardRanking == null || rewardRanking.RewardDetail == null)
        return;
      if (Object.op_Inequality((Object) this.mRewardAreaTemplate, (Object) null))
      {
        GameUtility.SetGameObjectActive(this.mRewardAreaTemplate, false);
        GvGPeriodParam gvGnearPeriod = GvGPeriodParam.GetGvGNearPeriod();
        if (gvGnearPeriod != null)
        {
          List<GvGNodeParam> gvGnodeList = GvGNodeParam.GetGvGNodeList(gvGnearPeriod.Id);
          for (int index = 0; index < gvGnodeList.Count; ++index)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mRewardAreaTemplate, this.mRewardAreaTemplate.transform.parent);
            if (!Object.op_Equality((Object) gameObject, (Object) null))
            {
              DataSource.Bind<GvGNodeParam>(gameObject, gvGnodeList[index]);
              bool flag = gameObject.GetComponent<GvGLeagueRewardListContent>().SetUpArea(selectLeagueParam.Id);
              gameObject.SetActive(flag);
            }
          }
        }
      }
      if (Object.op_Inequality((Object) this.mRewardLeagueTemplate, (Object) null))
      {
        GameUtility.SetGameObjectActive(this.mRewardLeagueTemplate, false);
        for (int index = 0; index < rewardRanking.RewardDetail.Count; ++index)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.mRewardLeagueTemplate, this.mRewardLeagueTemplate.transform.parent);
          if (!Object.op_Equality((Object) gameObject, (Object) null))
          {
            DataSource.Bind<GvGRewardRankingDetailParam>(gameObject, rewardRanking.RewardDetail[index]);
            bool flag = gameObject.GetComponent<GvGLeagueRewardListContent>().SetUpLeague();
            gameObject.SetActive(flag);
          }
        }
      }
      this.SetTab(this.mAreaTab);
    }

    public void SetTab(GameObject obj)
    {
      if (Object.op_Inequality((Object) this.mNowTab, (Object) obj))
        this.mNowTab = obj;
      GameUtility.SetGameObjectActive(this.mRewardAreaRoot, Object.op_Equality((Object) obj, (Object) this.mAreaTab));
      GameUtility.SetGameObjectActive(this.mRewardLeagueRoot, Object.op_Equality((Object) obj, (Object) this.mLeagueTab));
    }
  }
}
