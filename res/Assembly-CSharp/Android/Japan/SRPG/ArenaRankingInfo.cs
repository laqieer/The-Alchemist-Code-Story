// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
