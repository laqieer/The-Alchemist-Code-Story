// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaHistoryInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaHistoryInfo : MonoBehaviour
  {
    [Space(10f)]
    public Text Ranking;
    public Text created_at;
    public Text PlayerName;
    public Text PlayerLevel;
    public GameObject unit_icon;
    public ImageArray result_image;
    public ImageArray ranking_delta;
    public ImageArray history_type;
    public Image NewImage;

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayerHistory dataOfClass = DataSource.FindDataOfClass<ArenaPlayerHistory>(this.gameObject, (ArenaPlayerHistory) null);
      if (dataOfClass == null)
        return;
      this.PlayerLevel.text = dataOfClass.enemy.PlayerLevel.ToString();
      this.result_image.ImageIndex = !dataOfClass.IsWin() ? 1 : 0;
      this.NewImage.gameObject.SetActive(dataOfClass.IsNew());
      if (dataOfClass.IsNew())
        this.created_at.color = new Color((float) byte.MaxValue, (float) byte.MaxValue, 0.0f, 1f);
      this.history_type.ImageIndex = !dataOfClass.IsAttack() ? 1 : 0;
      this.Ranking.text = dataOfClass.ranking.up.ToString();
      this.Ranking.gameObject.SetActive(dataOfClass.ranking.up != 0);
      if (dataOfClass.ranking.up > 0)
        this.ranking_delta.ImageIndex = 0;
      else if (dataOfClass.ranking.up < 0)
      {
        this.ranking_delta.ImageIndex = 1;
        this.Ranking.color = new Color((float) byte.MaxValue, 0.0f, 0.0f, 1f);
      }
      else
        this.ranking_delta.gameObject.SetActive(false);
      this.PlayerName.text = dataOfClass.enemy.PlayerName.ToString();
      this.created_at.text = dataOfClass.battle_at.ToString("MM/dd HH:mm");
    }
  }
}
