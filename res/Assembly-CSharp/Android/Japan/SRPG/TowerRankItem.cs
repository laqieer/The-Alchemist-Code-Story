// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = value.ToString();
    }

    private void SetText(Text text, string value)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = value;
    }
  }
}
