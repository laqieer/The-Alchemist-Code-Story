// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TowerRankItem : MonoBehaviour
  {
    [SerializeField]
    private Text m_UserName;
    [SerializeField]
    private Text m_UserLv;
    [SerializeField]
    private Text m_Rank;
    [SerializeField]
    private Text m_Score;

    public void Setup(TowerResuponse.TowerRankParam rankData)
    {
      if (rankData == null)
        return;
      this.SetText(this.m_UserName, rankData.name);
      this.SetText(this.m_UserLv, rankData.lv);
      this.SetText(this.m_Rank, rankData.rank);
      this.SetText(this.m_Score, rankData.score);
    }

    private void SetText(Text text, int value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = value.ToString();
    }

    private void SetText(Text text, string value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = value;
    }
  }
}
