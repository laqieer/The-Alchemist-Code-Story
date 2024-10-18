﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestUserParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "データ反映", FlowNode.PinTypes.Input, 100)]
  public class RankingQuestUserParty : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_REFRECTION_DATA = 100;
    [SerializeField]
    private QuestClearedPartyViewer m_PartyWindow;
    [SerializeField]
    private Text m_MainScoreText;
    [SerializeField]
    private Text m_MainScoreValue;
    [SerializeField]
    private Text m_MainScoreValueSuffix;
    [SerializeField]
    private Text m_SubScoreText;
    [SerializeField]
    private Text m_SubScoreValue;

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      ((Behaviour) this.m_PartyWindow).enabled = true;
      this.Refresh();
    }

    private void Refresh()
    {
      RankingQuestParam rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      RankingQuestUserData dataOfClass = DataSource.FindDataOfClass<RankingQuestUserData>(((Component) this).gameObject, (RankingQuestUserData) null);
      if (Object.op_Inequality((Object) this.m_MainScoreText, (Object) null) && rankingQuestParam != null && rankingQuestParam.type == RankingQuestType.ActionCount)
        this.m_MainScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION");
      if (Object.op_Inequality((Object) this.m_MainScoreValueSuffix, (Object) null) && rankingQuestParam != null)
      {
        if (rankingQuestParam.type == RankingQuestType.ActionCount)
        {
          ((Component) this.m_MainScoreValueSuffix).gameObject.SetActive(true);
          this.m_MainScoreValueSuffix.text = LocalizedText.Get("sys.RANKING_QUEST_PARTY_ACTION_SUFFIX");
        }
        else
          ((Component) this.m_MainScoreValueSuffix).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.m_SubScoreText, (Object) null))
        this.m_SubScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_UNIT_TOTAL");
      if (Object.op_Inequality((Object) this.m_MainScoreValue, (Object) null))
        this.m_MainScoreValue.text = dataOfClass.m_MainScore.ToString();
      if (!Object.op_Inequality((Object) this.m_SubScoreValue, (Object) null))
        return;
      this.m_SubScoreValue.text = dataOfClass.m_SubScore.ToString();
    }
  }
}
