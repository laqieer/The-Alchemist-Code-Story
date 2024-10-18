﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RankingQuestInfo : MonoBehaviour
  {
    [SerializeField]
    private Text m_UserName;
    [SerializeField]
    private Text m_MainScore;
    [SerializeField]
    private Text m_SubScore;
    [SerializeField]
    private RankingQuestInfo.RankViewObject[] m_SpecialRankObject;
    [SerializeField]
    private RankingQuestInfo.RankViewObject m_CommonRankObject;

    public void UpdateValue()
    {
      RankingQuestUserData dataOfClass = DataSource.FindDataOfClass<RankingQuestUserData>(((Component) this).gameObject, (RankingQuestUserData) null);
      if (dataOfClass == null)
        return;
      int num = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UserName, (UnityEngine.Object) null))
        this.m_UserName.text = dataOfClass.m_PlayerName;
      if (this.m_SpecialRankObject != null)
      {
        num = this.m_SpecialRankObject.Length;
        for (int index = 0; index < this.m_SpecialRankObject.Length; ++index)
        {
          if (dataOfClass.m_Rank - 1 == index)
          {
            this.m_SpecialRankObject[index].SetActive(true);
            this.m_SpecialRankObject[index].SetRank(dataOfClass.m_Rank.ToString());
          }
          else
            this.m_SpecialRankObject[index].SetActive(false);
        }
      }
      if (this.m_CommonRankObject != null)
      {
        if (num < dataOfClass.m_Rank)
        {
          this.m_CommonRankObject.SetActive(true);
          this.m_CommonRankObject.SetRank(LocalizedText.Get("sys.RANKING_QUEST_WND_RANK", (object) dataOfClass.m_Rank));
        }
        else
          this.m_CommonRankObject.SetActive(false);
      }
      if (dataOfClass.IsActionCountRanking)
        this.Refrection_ActionCountRanking(dataOfClass);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Refrection_ActionCountRanking(RankingQuestUserData data)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_MainScore, (UnityEngine.Object) null))
        this.m_MainScore.text = data.m_MainScore.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SubScore, (UnityEngine.Object) null))
        return;
      this.m_SubScore.text = data.m_SubScore.ToString();
    }

    [Serializable]
    public class RankViewObject
    {
      [SerializeField]
      public GameObject m_RootObject;
      [SerializeField]
      public Text m_TextObject;

      public void SetActive(bool value)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_RootObject, (UnityEngine.Object) null))
          return;
        this.m_RootObject.SetActive(value);
      }

      public void SetRank(string value)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TextObject, (UnityEngine.Object) null))
          return;
        this.m_TextObject.text = value;
      }
    }
  }
}
