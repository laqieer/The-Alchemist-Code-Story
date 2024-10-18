// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TowerScore : MonoBehaviour
  {
    [HeaderBar("▼「スコア内訳」のテキスト")]
    [SerializeField]
    private Text m_ScoreTitle;
    [HeaderBar("▼スコア")]
    [SerializeField]
    private Text m_TurnNum;
    [SerializeField]
    private Text m_DiedNum;
    [SerializeField]
    private Text m_RetireNum;
    [SerializeField]
    private Text m_RecoveryNum;
    [SerializeField]
    private Text m_FloorResetNum;
    [SerializeField]
    private Text m_ChallengeNum;
    [SerializeField]
    private Text m_LoseNum;
    [HeaderBar("▼ランキング")]
    [SerializeField]
    private TowerRankItem m_SpdRankTop;
    [SerializeField]
    private TowerRankItem m_TecRankTop;
    [SerializeField]
    private GameObject m_SpdRankHeader;
    [SerializeField]
    private GameObject m_TecRankHeader;
    [SerializeField]
    private GameObject m_NoRankData;
    private string NotSocreText = "-";

    public void SetScoreTitleText(string floorName)
    {
      TowerScore.SetText(this.m_ScoreTitle, LocalizedText.Get("sys.TOWER_BRAGDOWN_SCORE_FLOOR", (object) floorName));
    }

    public void Setup(
      TowerScore.ViewParam param,
      TowerResuponse.TowerRankParam[] spdRankData,
      TowerResuponse.TowerRankParam[] tecRankData)
    {
      if (param.IsNoData)
      {
        TowerScore.SetText(this.m_TurnNum, this.NotSocreText);
        TowerScore.SetText(this.m_DiedNum, this.NotSocreText);
        TowerScore.SetText(this.m_RetireNum, this.NotSocreText);
        TowerScore.SetText(this.m_RecoveryNum, this.NotSocreText);
        TowerScore.SetText(this.m_FloorResetNum, this.NotSocreText);
        TowerScore.SetText(this.m_ChallengeNum, this.NotSocreText);
        TowerScore.SetText(this.m_LoseNum, this.NotSocreText);
      }
      else
      {
        TowerScore.SetText(this.m_TurnNum, param.TurnNum);
        TowerScore.SetText(this.m_DiedNum, param.DiedNum);
        TowerScore.SetText(this.m_RetireNum, param.RetireNum);
        TowerScore.SetText(this.m_RecoveryNum, param.RecoveryNum);
        TowerScore.SetText(this.m_FloorResetNum, param.FloorResetNum);
        TowerScore.SetText(this.m_ChallengeNum, param.ChallengeNum);
        TowerScore.SetText(this.m_LoseNum, param.LoseNum);
      }
      if (((((false ? 1 : 0) | (spdRankData == null ? 1 : (spdRankData.Length < 1 ? 1 : 0))) != 0 ? 1 : 0) | (tecRankData == null ? 1 : (tecRankData.Length < 1 ? 1 : 0))) != 0)
      {
        GameUtility.SetGameObjectActive(this.m_NoRankData, true);
        GameUtility.SetGameObjectActive((Component) this.m_SpdRankTop, false);
        GameUtility.SetGameObjectActive((Component) this.m_TecRankTop, false);
        GameUtility.SetGameObjectActive(this.m_SpdRankHeader, false);
        GameUtility.SetGameObjectActive(this.m_TecRankHeader, false);
      }
      else
      {
        this.SetRankData(this.m_SpdRankTop, spdRankData);
        this.SetRankData(this.m_TecRankTop, tecRankData);
        GameUtility.SetGameObjectActive(this.m_NoRankData, false);
        GameUtility.SetGameObjectActive((Component) this.m_SpdRankTop, true);
        GameUtility.SetGameObjectActive((Component) this.m_TecRankTop, true);
        GameUtility.SetGameObjectActive(this.m_SpdRankHeader, true);
        GameUtility.SetGameObjectActive(this.m_TecRankHeader, true);
      }
    }

    public void SetRankData(TowerRankItem rankItem, TowerResuponse.TowerRankParam[] rankData)
    {
      if (rankData == null || rankData.Length < 1)
      {
        GameUtility.SetGameObjectActive((Component) rankItem, false);
      }
      else
      {
        TowerResuponse.TowerRankParam rankData1 = rankData[0];
        if (!Object.op_Inequality((Object) rankItem, (Object) null))
          return;
        DataSource.Bind<UnitData>(((Component) rankItem).gameObject, rankData1.unit);
        rankItem.Setup(rankData1);
        GameParameter.UpdateAll(((Component) rankItem).gameObject);
      }
    }

    private static void SetText(Text text, int value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = value.ToString();
    }

    private static void SetText(Text text, string value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = value;
    }

    public class ViewParam
    {
      private int m_TurnNum = -1;
      private int m_DiedNum = -1;
      private int m_RetireNum = -1;
      private int m_RecoveryNum = -1;
      private int m_FloorResetNum = -1;
      private int m_ChallengeNum = -1;
      private int m_LoseNum = -1;

      public int TurnNum
      {
        get => this.m_TurnNum;
        set => this.m_TurnNum = value;
      }

      public int DiedNum
      {
        get => this.m_DiedNum;
        set => this.m_DiedNum = value;
      }

      public int RetireNum
      {
        get => this.m_RetireNum;
        set => this.m_RetireNum = value;
      }

      public int RecoveryNum
      {
        get => this.m_RecoveryNum;
        set => this.m_RecoveryNum = value;
      }

      public int FloorResetNum
      {
        get => this.m_FloorResetNum;
        set => this.m_FloorResetNum = value;
      }

      public int ChallengeNum
      {
        get => this.m_ChallengeNum;
        set => this.m_ChallengeNum = value;
      }

      public int LoseNum
      {
        get => this.m_LoseNum;
        set => this.m_LoseNum = value;
      }

      public bool IsNoData
      {
        get
        {
          return this.m_TurnNum == -1 || this.m_DiedNum == -1 || this.m_RetireNum == -1 || this.m_RecoveryNum == -1 || this.m_FloorResetNum == -1 || this.m_ChallengeNum == -1 || this.m_LoseNum == -1;
        }
      }
    }
  }
}
