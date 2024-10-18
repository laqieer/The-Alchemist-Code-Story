// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      {
        rankingQuestParam = SceneBattle.Instance.Battle.GetRankingQuestParam();
        DataSource.Bind<UnitData>(this.gameObject, SceneBattle.Instance.Battle.Leader.UnitData, false);
      }
      else
        rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      if (rankingQuestParam == null)
        return;
      this.m_HigherRankText.gameObject.SetActive(false);
      this.m_MiddleRankText.gameObject.SetActive(false);
      this.m_LowerRankText.gameObject.SetActive(false);
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
      text.gameObject.SetActive(true);
      text.text = SceneBattle.Instance.RankingQuestNewRank.ToString();
      if (rankingQuestParam.type == RankingQuestType.ActionCount)
      {
        if ((UnityEngine.Object) this.m_MainScoreText != (UnityEngine.Object) null)
          this.m_MainScoreText.text = LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION");
        if ((UnityEngine.Object) this.m_MainScoreValue != (UnityEngine.Object) null)
          this.m_MainScoreValue.text = SceneBattle.Instance.Battle.ActionCountTotal.ToString();
      }
      if (!((UnityEngine.Object) this.m_SubScoreValue != (UnityEngine.Object) null))
        return;
      this.m_SubScoreValue.text = SceneBattle.Instance.Battle.CalcPlayerUnitsTotalParameter().ToString();
    }
  }
}
