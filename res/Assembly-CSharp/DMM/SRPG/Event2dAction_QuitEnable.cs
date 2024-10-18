// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [EventActionInfo("強制終了/許可(2D)", "強制終了を許可します", 5592405, 4473992)]
  public class Event2dAction_QuitEnable : EventAction
  {
    protected static EventQuit mQuit;
    private LoadRequest mResource;
    private static readonly string AssetPath = "Event2dAssets/BtnSkip";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_QuitEnable.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) null, (Object) Event2dAction_QuitEnable.mQuit) || this.mResource == null)
        return;
      Event2dAction_QuitEnable.mQuit = Object.Instantiate(this.mResource.asset) as EventQuit;
      ((Component) Event2dAction_QuitEnable.mQuit).transform.SetParent((Transform) this.EventRootTransform, false);
      ((Component) Event2dAction_QuitEnable.mQuit).transform.SetAsLastSibling();
      // ISSUE: method pointer
      Event2dAction_QuitEnable.mQuit.OnClick = new UnityAction((object) this, __methodptr(\u003CPreStart\u003Em__0));
      ((Component) Event2dAction_QuitEnable.mQuit).gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) null, (Object) Event2dAction_QuitEnable.mQuit))
        return;
      ((Component) Event2dAction_QuitEnable.mQuit).gameObject.SetActive(true);
      EventScript.ActiveButtons(true);
      this.ActivateNext();
    }

    protected override void OnDestroy() => Event2dAction_QuitEnable.mQuit = (EventQuit) null;
  }
}
