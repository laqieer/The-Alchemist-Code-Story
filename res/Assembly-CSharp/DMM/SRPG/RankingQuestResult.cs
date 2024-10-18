// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RankingQuestResult : MonoBehaviour
  {
    private const int HIGHER_RANK_TEXT_INDEX = 4;
    private const int MIDDLE_RANK_TEXT_INDEX = 10;
    [SerializeField]
    private Text m_HigherRankText;
    [SerializeField]
    private Text m_MiddleRankText;
    [SerializeField]
    private Text m_LowerRankText;
    [SerializeField]
    private GameObject m_RankDecoration;
    [SerializeField]
    private GameObject m_RankDecorationEffect;
    [SerializeField]
    private Text m_MainScoreText;
    [SerializeField]
    private Text m_MainScoreValue;
    [SerializeField]
    private Text m_SubScoreValue;

    private void Start()
    {
      RankingQuestParam rankingQuestParam;
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        rankingQuestParam = SceneBattle.Instance.Battle.GetRankingQuestParam();
        DataSource.Bind<UnitData>(((Component) this).gameObject, SceneBattle.Instance.Battle.Leader.UnitData);
      }
      else
        rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      if (rankingQuestParam == null)
        return;
      ((Component) this.m_HigherRankText).gameObject.SetActive(false);
      ((Component) this.m_MiddleRankText).gameObject.SetActive(false);
      ((Component) this.m_LowerRankText).gameObject.SetActive(false);
      this.m_RankDecoration.SetActive(false);
      this.m_RankDecorationEffect.SetActive(false);
      Text text = this.m_LowerRankText;
      if (SceneBattle.Instance.RankingQuestNewRank <= 4)
      {
        text = this.m_HigherRankText;
        this.m_RankDecoration.SetActive(true);
        this.m_RankDecorationEffect.SetActive(true);
      }
      else if (SceneBattle.Instance.RankingQuestNewRank <= 10)
      {
        text = this.m_MiddleRankText;
        this.m_RankDecoration.SetActive(true);
        this.m_RankDecorationEffect.SetActive(true);
      }
      ((Component) text).gameObject.SetActive(true);
      text.text = SceneBattle.Instance.RankingQuestNewRank.ToString();
      if (rankingQuestParam.type == RankingQuestType.ActionCount)
      {
        if (Object.op_Inequality((Object) this.m_MainScoreText, (Object) null))
          this.m_MainScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION");
        if (Object.op_Inequality((Object) this.m_MainScoreValue, (Object) null))
          this.m_MainScoreValue.text = SceneBattle.Instance.Battle.ActionCountTotal.ToString();
      }
      if (!Object.op_Inequality((Object) this.m_SubScoreValue, (Object) null))
        return;
      this.m_SubScoreValue.text = SceneBattle.Instance.Battle.CalcPlayerUnitsTotalParameter().ToString();
    }
  }
}
