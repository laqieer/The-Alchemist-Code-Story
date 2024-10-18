// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UsageRateRanking : MonoBehaviour, IFlowInterface
  {
    public static readonly string[] ViewInfo = new string[3]{ "quest", "arena", "tower_match" };
    private List<UsageRateRankingItem> Items = new List<UsageRateRankingItem>();
    public GameObject ItemBase;
    public GameObject Parent;
    public GameObject Aggregating;
    public Scrollbar ItemScrollBar;
    private UsageRateRanking.ViewInfoType mNowViewInfoType;
    public Toggle[] RankingToggle;

    public byte NowViewInfoIndex
    {
      get
      {
        return (byte) this.mNowViewInfoType;
      }
    }

    public string NowViewInfo
    {
      get
      {
        return UsageRateRanking.ViewInfo[(int) this.NowViewInfoIndex];
      }
    }

    public void Start()
    {
      if (this.RankingToggle == null)
        return;
      for (int index1 = 0; index1 < this.RankingToggle.Length; ++index1)
      {
        if (!((UnityEngine.Object) this.RankingToggle[index1] == (UnityEngine.Object) null))
        {
          int index = index1;
          this.RankingToggle[index1].onValueChanged.AddListener((UnityAction<bool>) (value =>
          {
            if (!value)
              return;
            this.OnChangedToggle(index);
          }));
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
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemBase);
            gameObject.transform.SetParent(this.Parent.transform, false);
            UsageRateRankingItem component = gameObject.GetComponent<UsageRateRankingItem>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              component.gameObject.SetActive(true);
              this.Items.Add(component);
            }
          }
          this.Items[index].Refresh(index + 1, rankingData.ranking[index]);
        }
        GameParameter.UpdateAll(this.gameObject);
        if (rankingData.ranking.Length >= this.Items.Count)
          return;
        for (int length = rankingData.ranking.Length; length < this.Items.Count; ++length)
          this.Items[length].gameObject.SetActive(false);
      }
    }

    private void OnChangedToggle(int index)
    {
      this.OnChangedToggle((UsageRateRanking.ViewInfoType) index);
    }

    public void OnChangedToggle(UsageRateRanking.ViewInfoType index)
    {
      this.mNowViewInfoType = index;
      if ((UnityEngine.Object) this.ItemScrollBar != (UnityEngine.Object) null)
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
