// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenuButton_Unitlist
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SortMenuButton_Unitlist : SortMenuButton
  {
    public Image FilterButton;
    public Sprite Active;
    public Sprite NonActive;
    public Text Msg;
    private Coroutine mCoroutine;
    private bool mRequest;
    private bool mFlag;

    protected override void Start() => base.Start();

    protected override void OnEnable()
    {
    }

    protected override void UpdateFilterState(bool active)
    {
      this.mFlag = active;
      this.mRequest = true;
    }

    private void Update()
    {
      if (!this.mRequest || this.mCoroutine != null)
        return;
      this.mCoroutine = ((MonoBehaviour) this).StartCoroutine(this.UpdateState(this.mFlag));
      this.mRequest = false;
    }

    [DebuggerHidden]
    private IEnumerator UpdateState(bool active)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SortMenuButton_Unitlist.\u003CUpdateState\u003Ec__Iterator0()
      {
        active = active,
        \u0024this = this
      };
    }
  }
}
