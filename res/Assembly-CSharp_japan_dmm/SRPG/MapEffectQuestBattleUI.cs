// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuestBattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MapEffectQuestBattleUI : MonoBehaviour
  {
    public SRPG_Button ButtonMapEffect;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffect;

    private void Start()
    {
      if (!Object.op_Implicit((Object) this.ButtonMapEffect))
        return;
      this.ButtonMapEffect.AddListener((SRPG_Button.ButtonClickEvent) (button => this.ReqOpenMapEffect()));
    }

    private void ReqOpenMapEffect() => this.StartCoroutine(this.OpenMapEffect());

    [DebuggerHidden]
    private IEnumerator OpenMapEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MapEffectQuestBattleUI.\u003COpenMapEffect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
