// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EnableQuit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("強制終了/許可", "スクリプトの強制終了を有効にします。", 5592405, 4473992)]
  public class EventAction_EnableQuit : EventAction
  {
    private static readonly string AssetPath = "UI/BtnSkip_movie";
    protected static EventQuit mQuit;
    private LoadRequest mResource;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_EnableQuit.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
    }

    public override void PreStart()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) EventAction_EnableQuit.mQuit || this.mResource == null)
        return;
      EventQuit eventQuit = EventQuit.Find();
      EventAction_EnableQuit.mQuit = !((UnityEngine.Object) eventQuit == (UnityEngine.Object) null) ? eventQuit : UnityEngine.Object.Instantiate(this.mResource.asset) as EventQuit;
      EventAction_EnableQuit.mQuit.transform.SetParent(this.ActiveCanvas.transform, false);
      EventAction_EnableQuit.mQuit.transform.SetAsLastSibling();
      EventAction_EnableQuit.mQuit.OnClick = (UnityAction) (() =>
      {
        if (!((UnityEngine.Object) this.Sequence != (UnityEngine.Object) null))
          return;
        this.SkipButtonAction(this.Sequence, EventAction_EnableQuit.mQuit.gameObject);
      });
      EventAction_EnableQuit.mQuit.gameObject.SetActive(false);
    }

    private void SkipButtonAction(EventScript.Sequence inEventScriptSequence, GameObject inSkipButtonGameObject)
    {
      GlobalVars.IsSkipQuestDemo = true;
      inEventScriptSequence.GoToEndState();
      inSkipButtonGameObject.SetActive(false);
      EventScript.ActiveButtons(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) EventAction_EnableQuit.mQuit)
        return;
      EventAction_EnableQuit.mQuit.gameObject.SetActive(true);
      EventAction_EnableQuit.mQuit.transform.SetAsLastSibling();
      EventScript.ActiveButtons(true);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      EventAction_EnableQuit.mQuit = (EventQuit) null;
    }
  }
}
