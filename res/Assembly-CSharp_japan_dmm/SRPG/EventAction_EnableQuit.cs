// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EnableQuit
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
  [EventActionInfo("強制終了/許可", "スクリプトの強制終了を有効にします。", 5592405, 4473992)]
  public class EventAction_EnableQuit : EventAction
  {
    protected static EventQuit mQuit;
    private LoadRequest mResource;
    private static readonly string AssetPath = "UI/BtnSkip_movie";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_EnableQuit.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) null, (Object) EventAction_EnableQuit.mQuit) || this.mResource == null)
        return;
      EventQuit eventQuit = EventQuit.Find();
      EventAction_EnableQuit.mQuit = !Object.op_Equality((Object) eventQuit, (Object) null) ? eventQuit : Object.Instantiate(this.mResource.asset) as EventQuit;
      ((Component) EventAction_EnableQuit.mQuit).transform.SetParent((Transform) this.EventRootTransform, false);
      ((Component) EventAction_EnableQuit.mQuit).transform.SetAsLastSibling();
      // ISSUE: method pointer
      EventAction_EnableQuit.mQuit.OnClick = new UnityAction((object) this, __methodptr(\u003CPreStart\u003Em__0));
      ((Component) EventAction_EnableQuit.mQuit).gameObject.SetActive(false);
    }

    private void SkipButtonAction(
      EventScript.Sequence inEventScriptSequence,
      GameObject inSkipButtonGameObject)
    {
      GlobalVars.IsSkipQuestDemo = true;
      inEventScriptSequence.GoToEndState();
      inSkipButtonGameObject.SetActive(false);
      EventScript.ActiveButtons(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) null, (Object) EventAction_EnableQuit.mQuit))
        return;
      ((Component) EventAction_EnableQuit.mQuit).gameObject.SetActive(true);
      ((Component) EventAction_EnableQuit.mQuit).transform.SetAsLastSibling();
      EventScript.ActiveButtons(true);
      this.ActivateNext();
    }

    protected override void OnDestroy() => EventAction_EnableQuit.mQuit = (EventQuit) null;
  }
}
