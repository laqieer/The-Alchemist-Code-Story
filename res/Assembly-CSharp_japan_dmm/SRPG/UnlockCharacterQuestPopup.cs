// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockCharacterQuestPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mCurrentUnit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
