// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRewardListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArenaRewardListItem : ListItemEvents
  {
    public GameObject RankImage;
    public GameObject RankText;
    public GameObject RankObjectSingle;
    public Text RankTextSingle;
    public GameObject RankObjectMulti;
    public Text RankTextMultiFrom;
    public Text RankTextMultiTo;
    public GameObject RankObjectMultiTo;
    public GameObject RewardCoin;
    public GameObject RewardArenaCoin;
    public GameObject RewardGold;
    public GameObject RewardItem;

    public void Initialize(ArenaRewardParam param, bool end)
    {
      DataSource.Bind<ArenaRewardParam>(this.RankImage, param);
      if (param.Coin > 0 && Object.op_Inequality((Object) this.RewardCoin, (Object) null))
        this.RewardCoin.SetActive(this.SetAmount(this.RewardCoin, param.Coin));
      if (param.Gold > 0 && Object.op_Inequality((Object) this.RewardGold, (Object) null))
        this.RewardGold.SetActive(this.SetAmount(this.RewardGold, param.Gold));
      if (param.ArenaCoin > 0 && Object.op_Inequality((Object) this.RewardArenaCoin, (Object) null))
        this.RewardArenaCoin.SetActive(this.SetAmount(this.RewardArenaCoin, param.ArenaCoin));
      if (param.Items != null && param.Items.Count > 0)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        foreach (ArenaRewardParam.RewardItem rewardItem in param.Items)
        {
          ItemParam itemParam = instance.GetItemParam(rewardItem.iname);
          if (itemParam != null)
          {
            GameObject go = Object.Instantiate<GameObject>(this.RewardItem);
            DataSource.Bind<ItemParam>(go, itemParam);
            go.transform.SetParent(this.RewardItem.transform.parent, false);
            go.SetActive(this.SetAmount(go, rewardItem.num));
          }
        }
      }
      this.SetRankingText(param, end);
      ((Component) this).gameObject.SetActive(true);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private bool SetAmount(GameObject go, int num)
    {
      Transform transform = go.transform.Find("amount/Text_amount");
      if (Object.op_Equality((Object) transform, (Object) null))
        return false;
      Text component = ((Component) transform).GetComponent<Text>();
      if (Object.op_Equality((Object) component, (Object) null))
        return false;
      component.text = num.ToString();
      return true;
    }

    private void SetRankingText(ArenaRewardParam param, bool end)
    {
      if (param.Rank <= 3)
      {
        this.RankText.SetActive(false);
      }
      else
      {
        this.RankText.SetActive(true);
        if (param.Rank != param.FromRank)
        {
          this.RankObjectSingle.SetActive(false);
          this.RankObjectMulti.SetActive(true);
          this.RankTextMultiFrom.text = param.FromRank.ToString();
          this.RankTextMultiTo.text = param.Rank.ToString();
          if (!end)
            return;
          this.RankObjectMultiTo.SetActive(false);
        }
        else
        {
          this.RankObjectSingle.SetActive(true);
          this.RankObjectMulti.SetActive(false);
          this.RankTextSingle.text = param.Rank.ToString();
        }
      }
    }
  }
}
