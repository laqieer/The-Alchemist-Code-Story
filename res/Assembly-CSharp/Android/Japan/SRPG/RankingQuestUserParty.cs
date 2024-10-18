// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestUserParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      this.m_PartyWindow.enabled = true;
      this.Refresh();
    }

    private void Refresh()
    {
      RankingQuestParam rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      RankingQuestUserData dataOfClass = DataSource.FindDataOfClass<RankingQuestUserData>(this.gameObject, (RankingQuestUserData) null);
      if ((UnityEngine.Object) this.m_MainScoreText != (UnityEngine.Object) null && rankingQuestParam != null && rankingQuestParam.type == RankingQuestType.ActionCount)
        this.m_MainScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION");
      if ((UnityEngine.Object) this.m_MainScoreValueSuffix != (UnityEngine.Object) null && rankingQuestParam != null)
      {
        if (rankingQuestParam.type == RankingQuestType.ActionCount)
        {
          this.m_MainScoreValueSuffix.gameObject.SetActive(true);
          this.m_MainScoreValueSuffix.text = LocalizedText.Get("sys.RANKING_QUEST_PARTY_ACTION_SUFFIX");
        }
        else
          this.m_MainScoreValueSuffix.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.m_SubScoreText != (UnityEngine.Object) null)
        this.m_SubScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_UNIT_TOTAL");
      if ((UnityEngine.Object) this.m_MainScoreValue != (UnityEngine.Object) null)
        this.m_MainScoreValue.text = dataOfClass.m_MainScore.ToString();
      if (!((UnityEngine.Object) this.m_SubScoreValue != (UnityEngine.Object) null))
        return;
      this.m_SubScoreValue.text = dataOfClass.m_SubScore.ToString();
    }
  }
}
