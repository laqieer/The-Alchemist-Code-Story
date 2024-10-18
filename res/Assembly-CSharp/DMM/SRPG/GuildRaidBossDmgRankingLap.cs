// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossDmgRankingLap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "画面更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "ダメージランキング読み込み", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "リスト追加読み込み", FlowNode.PinTypes.Output, 101)]
  public class GuildRaidBossDmgRankingLap : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_REFRESH = 10;
    private const int PIN_OUTPUT_DETAIL = 100;
    private const int PIN_OUTPUT_REQLAPLIST = 101;
    [SerializeField]
    private GameObject mBossTemplate;
    [SerializeField]
    private GameObject mLapTemplate;
    private GameObject mSelectTab;
    [SerializeField]
    private SRPG_ScrollRect Scroll;
    [SerializeField]
    private RectTransform ScrollContent;
    private bool IsLoading;
    private List<GameObject> mCreateList = new List<GameObject>();

    private void Awake()
    {
      this.Scroll.verticalNormalizedPosition = 1f;
      this.Refresh();
    }

    private void Update()
    {
      if (this.IsLoading || GuildRaidManager.Instance.RankingDamageSummaryPage >= GuildRaidManager.Instance.RankingDamageSummaryPageTotal || !Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) this.ScrollContent, (Object) null) || (double) this.Scroll.verticalNormalizedPosition * (double) this.ScrollContent.sizeDelta.y >= 10.0)
        return;
      this.IsLoading = true;
      ++GuildRaidManager.Instance.RankingDamageSummaryPage;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      int num = -1;
      GuildRaidManager instance = GuildRaidManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.RankingDamageSummaryList == null || Object.op_Equality((Object) this.mBossTemplate, (Object) null) || Object.op_Equality((Object) this.mLapTemplate, (Object) null))
        return;
      this.mBossTemplate.SetActive(false);
      this.mLapTemplate.SetActive(false);
      for (int index = 0; index < this.mCreateList.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mCreateList[index], (Object) null))
          Object.Destroy((Object) this.mCreateList[index]);
      }
      this.mCreateList.Clear();
      for (int index = 0; index < instance.RankingDamageSummaryList.Count; ++index)
      {
        if (instance.RankingDamageSummaryList[index] != null)
        {
          if (num > instance.RankingDamageSummaryList[index].Round || num == -1)
          {
            num = instance.RankingDamageSummaryList[index].Round;
            GameObject gameObject = Object.Instantiate<GameObject>(this.mLapTemplate, this.mLapTemplate.transform.parent);
            gameObject.SetActive(true);
            this.mCreateList.Add(gameObject);
            GuildRaidBossDmgRankingLapItem component = gameObject.GetComponent<GuildRaidBossDmgRankingLapItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              Text mLapText = component.mLapText;
              if (Object.op_Inequality((Object) mLapText, (Object) null))
                mLapText.text = num.ToString();
            }
          }
          GameObject gameObject1 = Object.Instantiate<GameObject>(this.mBossTemplate, this.mBossTemplate.transform.parent);
          this.mCreateList.Add(gameObject1);
          DataSource.Bind<GuildRaidRankingDamage>(gameObject1, instance.RankingDamageSummaryList[index]);
          gameObject1.SetActive(true);
        }
      }
      this.IsLoading = false;
    }

    public void OnDetail(GameObject go)
    {
      GuildRaidRankingDamage dataOfClass = DataSource.FindDataOfClass<GuildRaidRankingDamage>(go, (GuildRaidRankingDamage) null);
      if (dataOfClass == null)
        return;
      GuildRaidManager.Instance.RankingDamageRoundBossId = dataOfClass.BossId;
      GuildRaidManager.Instance.RankingDamageRoundRound = dataOfClass.Round;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
