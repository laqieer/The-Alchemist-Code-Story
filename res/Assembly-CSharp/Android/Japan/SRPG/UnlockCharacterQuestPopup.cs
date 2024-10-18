// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockCharacterQuestPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnlockCharacterQuestPopup : MonoBehaviour
  {
    private UnitData mCurrentUnit;
    public Text EpisodeTitle;
    public Text EpisodeNumber;

    private void Start()
    {
    }

    public void Setup(UnitData unit, int episodeNumber, string episodeTitle)
    {
      this.mCurrentUnit = unit;
      DataSource.Bind<UnitData>(this.gameObject, this.mCurrentUnit, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
