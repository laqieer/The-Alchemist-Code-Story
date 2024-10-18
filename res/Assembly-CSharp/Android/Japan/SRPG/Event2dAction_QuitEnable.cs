// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("強制終了/許可(2D)", "強制終了を許可します", 5592405, 4473992)]
  public class Event2dAction_QuitEnable : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/BtnSkip";
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
      return (IEnumerator) new Event2dAction_QuitEnable.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
    }

    public override void PreStart()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) Event2dAction_QuitEnable.mQuit || this.mResource == null)
        return;
      Event2dAction_QuitEnable.mQuit = UnityEngine.Object.Instantiate(this.mResource.asset) as EventQuit;
      Event2dAction_QuitEnable.mQuit.transform.SetParent(this.ActiveCanvas.transform, false);
      Event2dAction_QuitEnable.mQuit.transform.SetAsLastSibling();
      Event2dAction_QuitEnable.mQuit.OnClick = (UnityAction) (() => this.Sequence.OnQuit());
      Event2dAction_QuitEnable.mQuit.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) Event2dAction_QuitEnable.mQuit)
        return;
      Event2dAction_QuitEnable.mQuit.gameObject.SetActive(true);
      EventScript.ActiveButtons(true);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      Event2dAction_QuitEnable.mQuit = (EventQuit) null;
    }
  }
}
