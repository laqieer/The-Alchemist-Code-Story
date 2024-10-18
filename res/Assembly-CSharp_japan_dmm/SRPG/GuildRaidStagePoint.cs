// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidStagePoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidStagePoint : MonoBehaviour
  {
    [SerializeField]
    private List<Transform> mStageList;
    [SerializeField]
    private List<GameObject> mStageEffect;

    public List<Transform> StageList => this.mStageList;

    public void SetClearEffect(int target)
    {
      if (this.mStageEffect == null || this.mStageEffect.Count <= target)
        return;
      for (int index = 0; index < this.mStageEffect.Count; ++index)
        GameUtility.SetGameObjectActive(this.mStageEffect[index], false);
      for (int index = 0; index < this.mStageEffect.Count && index < target; ++index)
        GameUtility.SetGameObjectActive(this.mStageEffect[index], true);
    }
  }
}
