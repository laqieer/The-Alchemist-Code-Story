// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRewardListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      DataSource.Bind<ArenaRewardParam>(this.RankImage, param, false);
      if (param.Coin > 0 && (UnityEngine.Object) this.RewardCoin != (UnityEngine.Object) null)
        this.RewardCoin.SetActive(this.SetAmount(this.RewardCoin, param.Coin));
      if (param.Gold > 0 && (UnityEngine.Object) this.RewardGold != (UnityEngine.Object) null)
        this.RewardGold.SetActive(this.SetAmount(this.RewardGold, param.Gold));
      if (param.ArenaCoin > 0 && (UnityEngine.Object) this.RewardArenaCoin != (UnityEngine.Object) null)
        this.RewardArenaCoin.SetActive(this.SetAmount(this.RewardArenaCoin, param.ArenaCoin));
      if (param.Items != null && param.Items.Count > 0)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        foreach (ArenaRewardParam.RewardItem rewardItem in param.Items)
        {
          ItemParam itemParam = instance.GetItemParam(rewardItem.iname);
          if (itemParam != null)
          {
            GameObject go = UnityEngine.Object.Instantiate<GameObject>(this.RewardItem);
            DataSource.Bind<ItemParam>(go, itemParam, false);
            go.transform.SetParent(this.RewardItem.transform.parent, false);
            go.SetActive(this.SetAmount(go, rewardItem.num));
          }
        }
      }
      this.SetRankingText(param, end);
      this.gameObject.SetActive(true);
      GameParameter.UpdateAll(this.gameObject);
    }

    private bool SetAmount(GameObject go, int num)
    {
      Transform transform = go.transform.Find("amount/Text_amount");
      if ((UnityEngine.Object) transform == (UnityEngine.Object) null)
        return false;
      Text component = transform.GetComponent<Text>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
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
