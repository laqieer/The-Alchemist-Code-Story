// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuestBattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

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
      if (!(bool) ((UnityEngine.Object) this.ButtonMapEffect))
        return;
      this.ButtonMapEffect.AddListener((SRPG_Button.ButtonClickEvent) (button => this.ReqOpenMapEffect()));
    }

    private void ReqOpenMapEffect()
    {
      this.StartCoroutine(this.OpenMapEffect());
    }

    [DebuggerHidden]
    private IEnumerator OpenMapEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MapEffectQuestBattleUI.\u003COpenMapEffect\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
