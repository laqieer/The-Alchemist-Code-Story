// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisLapBossReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GenesisLapBossReward : MonoBehaviour
  {
    [SerializeField]
    private Text mItemName;
    [SerializeField]
    private Text mItemNum;
    [SerializeField]
    private Transform mTrIconParent;
    [SerializeField]
    private GenesisRewardIcon mRewardIcon;
    private int mIndex;
    private GenesisRewardDataParam mReward;

    public int Index => this.mIndex;

    public GenesisRewardDataParam Reward => this.mReward;

    public void SetItem(int index, GenesisRewardDataParam reward)
    {
      if (index < 0 || reward == null)
        return;
      this.mIndex = index;
      this.mReward = reward;
      string str = string.Empty;
      switch (reward.ItemType)
      {
        case 0:
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward.ItemIname);
          if (itemParam != null)
          {
            str = itemParam.name;
            break;
          }
          break;
        case 1:
          str = LocalizedText.Get("sys.GOLD");
          break;
        case 2:
          str = LocalizedText.Get("sys.COIN");
          break;
        case 3:
          AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(reward.ItemIname);
          if (awardParam != null)
          {
            str = awardParam.name;
            break;
          }
          break;
        case 4:
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(reward.ItemIname);
          if (unitParam != null)
          {
            str = unitParam.name;
            break;
          }
          break;
        case 5:
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(reward.ItemIname);
          if (conceptCardParam != null)
          {
            str = conceptCardParam.name;
            break;
          }
          break;
        case 6:
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.ItemIname);
          if (artifactParam != null)
          {
            str = artifactParam.name;
            break;
          }
          break;
      }
      if (Object.op_Implicit((Object) this.mItemName))
        this.mItemName.text = str;
      if (Object.op_Implicit((Object) this.mItemNum))
        this.mItemNum.text = reward.ItemType != 1 ? reward.ItemNum.ToString() : string.Format("{0:#,0}", (object) reward.ItemNum);
      if (!Object.op_Implicit((Object) this.mTrIconParent) || !Object.op_Inequality((Object) this.mRewardIcon, (Object) null))
        return;
      Object.Instantiate<GenesisRewardIcon>(this.mRewardIcon, this.mTrIconParent).Initialize(reward);
    }
  }
}
