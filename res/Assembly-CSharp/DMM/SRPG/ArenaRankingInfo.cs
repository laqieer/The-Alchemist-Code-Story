// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArenaRankingInfo : MonoBehaviour
  {
    [Space(10f)]
    public Text Ranking;
    public Text PlayerName;
    public Text PlayerKOs;
    public ImageArray ranking_image;

    private void OnEnable() => this.UpdateValue();

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).gameObject, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.ArenaRank <= 3)
      {
        this.ranking_image.ImageIndex = dataOfClass.ArenaRank;
        ((Component) this.Ranking).gameObject.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ranking_image, (UnityEngine.Object) null))
          this.ranking_image.ImageIndex = 0;
        ((Component) this.Ranking).gameObject.SetActive(true);
        this.Ranking.text = dataOfClass.ArenaRank.ToString() + LocalizedText.Get("sys.ARENA_LBL_RANK");
      }
      if (!string.IsNullOrEmpty(dataOfClass.PlayerName))
        this.PlayerName.text = dataOfClass.PlayerName.ToString();
      if (!(dataOfClass.battle_at > DateTime.MinValue))
        return;
      this.PlayerKOs.text = dataOfClass.battle_at.ToString("MM/dd HH:mm");
    }
  }
}
