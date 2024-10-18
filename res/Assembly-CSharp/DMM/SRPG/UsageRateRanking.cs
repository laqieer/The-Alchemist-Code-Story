// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UsageRateRanking : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemBase;
    public GameObject Parent;
    public GameObject Aggregating;
    private List<UsageRateRankingItem> Items = new List<UsageRateRankingItem>();
    public Scrollbar ItemScrollBar;
    private UsageRateRanking.ViewInfoType mNowViewInfoType;
    public Toggle[] RankingToggle;
    public static readonly string[] ViewInfo = new string[3]
    {
      "quest",
      "arena",
      "tower_match"
    };

    public byte NowViewInfoIndex => (byte) this.mNowViewInfoType;

    public string NowViewInfo => UsageRateRanking.ViewInfo[(int) this.NowViewInfoIndex];

    public void Start()
    {
      if (this.RankingToggle == null)
        return;
      for (int index = 0; index < this.RankingToggle.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UsageRateRanking.\u003CStart\u003Ec__AnonStorey0 startCAnonStorey0 = new UsageRateRanking.\u003CStart\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey0.\u0024this = this;
        if (!Object.op_Equality((Object) this.RankingToggle[index], (Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey0.index = index;
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.RankingToggle[index].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey0, __methodptr(\u003C\u003Em__0)));
        }
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      Dictionary<string, RankingData> unitRanking = MonoSingleton<GameManager>.Instance.UnitRanking;
      if (!unitRanking.ContainsKey(this.NowViewInfo))
      {
        this.Aggregating.SetActive(true);
      }
      else
      {
        RankingData rankingData = unitRanking[this.NowViewInfo];
        this.Aggregating.SetActive(rankingData.isReady != 1);
        if (rankingData.isReady != 1)
          return;
        for (int index = 0; index < rankingData.ranking.Length; ++index)
        {
          if (this.Items.Count <= index)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.ItemBase);
            gameObject.transform.SetParent(this.Parent.transform, false);
            UsageRateRankingItem component = gameObject.GetComponent<UsageRateRankingItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              ((Component) component).gameObject.SetActive(true);
              this.Items.Add(component);
            }
          }
          this.Items[index].Refresh(index + 1, rankingData.ranking[index]);
        }
        GameParameter.UpdateAll(((Component) this).gameObject);
        if (rankingData.ranking.Length >= this.Items.Count)
          return;
        for (int length = rankingData.ranking.Length; length < this.Items.Count; ++length)
          ((Component) this.Items[length]).gameObject.SetActive(false);
      }
    }

    private void OnChangedToggle(int index)
    {
      this.OnChangedToggle((UsageRateRanking.ViewInfoType) index);
    }

    public void OnChangedToggle(UsageRateRanking.ViewInfoType index)
    {
      this.mNowViewInfoType = index;
      if (Object.op_Inequality((Object) this.ItemScrollBar, (Object) null))
        this.ItemScrollBar.value = 1f;
      for (int index1 = 0; index1 < this.RankingToggle.Length; ++index1)
        this.RankingToggle[index1].isOn = (int) this.NowViewInfoIndex == index1;
      this.Refresh();
    }

    public enum ViewInfoType : byte
    {
      Quest,
      Arena,
      TowerMatch,
      Num,
    }
  }
}
