// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaRankingInfo : MonoBehaviour
  {
    [Space(10f)]
    public Text Ranking;
    public Text PlayerName;
    public Text PlayerKOs;
    public ImageArray ranking_image;

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(this.gameObject, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.ArenaRank <= 3)
      {
        this.ranking_image.ImageIndex = dataOfClass.ArenaRank;
        this.Ranking.gameObject.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.ranking_image != (UnityEngine.Object) null)
          this.ranking_image.ImageIndex = 0;
        this.Ranking.gameObject.SetActive(true);
        this.Ranking.text = string.Format(LocalizedText.Get("sys.RANKING_RANK"), (object) dataOfClass.ArenaRank.ToString());
      }
      if (!string.IsNullOrEmpty(dataOfClass.PlayerName))
        this.PlayerName.text = dataOfClass.PlayerName.ToString();
      if (!(dataOfClass.battle_at > DateTime.MinValue))
        return;
      this.PlayerKOs.text = dataOfClass.battle_at.ToString(GameUtility.Localized_TimePattern_Short);
    }
  }
}
