// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class MultiPlayVersusRanking : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 0;
    [SerializeField]
    private Text RankText;
    [SerializeField]
    private Text BestRankText;
    [SerializeField]
    private Transform IconHolder;
    [SerializeField]
    private GameObject ItemIcon;
    [SerializeField]
    private GameObject CoinIcon;
    [SerializeField]
    private GameObject GoldIcon;
    [SerializeField]
    private GameObject ArenaCoinIcon;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemIcon, (Object) null))
        this.ItemIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.CoinIcon, (Object) null))
        this.CoinIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldIcon, (Object) null))
        this.GoldIcon.SetActive(false);
      if (!Object.op_Inequality((Object) this.ArenaCoinIcon, (Object) null))
        return;
      this.ArenaCoinIcon.SetActive(false);
    }

    private void Refresh()
    {
      if (GlobalVars.ArenaAward == null)
        return;
      Json_ArenaAward arenaAward = GlobalVars.ArenaAward;
      if (Object.op_Inequality((Object) this.RankText, (Object) null))
        this.RankText.text = arenaAward.rank.ToString();
      if (Object.op_Inequality((Object) this.BestRankText, (Object) null))
        this.BestRankText.text = arenaAward.rank_best.ToString();
      if (GlobalVars.ArenaAward.reward == null || GlobalVars.ArenaAward.reward.rank == null)
        return;
      Json_ArenaRewardInfo rank = GlobalVars.ArenaAward.reward.rank;
      if (rank.coin > 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.CoinIcon);
        DataSource.Bind<int>(gameObject, rank.coin);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (rank.gold > 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.GoldIcon);
        DataSource.Bind<int>(gameObject, rank.gold);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (rank.arenacoin > 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ArenaCoinIcon);
        DataSource.Bind<int>(gameObject, rank.arenacoin);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (rank.items != null && rank.items.Length > 0)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        foreach (Json_Item jsonItem in rank.items)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemIcon);
          ItemData data = new ItemData();
          data.Setup(0L, jsonItem.iname, jsonItem.num);
          DataSource.Bind<ItemData>(gameObject, data);
          gameObject.transform.SetParent(this.IconHolder, false);
          gameObject.SetActive(true);
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
