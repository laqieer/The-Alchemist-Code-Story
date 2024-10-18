// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MultiPlayVersusEdit : MonoBehaviour
  {
    private void Start() => this.Set();

    public void Set()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int maxUnit;
      string str;
      if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.RankMatch)
      {
        maxUnit = player.Partys[10].MAX_UNIT;
        str = PlayerPrefsUtility.RANKMATCH_ID_KEY;
      }
      else
      {
        maxUnit = player.Partys[7].MAX_UNIT;
        str = PlayerPrefsUtility.VERSUS_ID_KEY;
      }
      for (int idx = 0; idx < maxUnit; ++idx)
      {
        if (!PlayerPrefsUtility.HasKey(str + (object) idx))
          player.SetVersusPlacement(str + (object) idx, idx);
      }
      PlayerPrefsUtility.Save();
    }
  }
}
