// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EnableQuitSG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("Forced termination / permission (3D)", "Allow forced termination", 5592405, 4473992)]
  public class EventAction_EnableQuitSG : EventAction
  {
    private static readonly string AssetPath = "UI/BtnSkip_movie";
    public bool Special;
    protected static EventQuit mQuit;
    private LoadRequest mResource;
    public AnalyticsManager.TrackingType AnalyticsTypeToTrackOnQuit;

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
      return (IEnumerator) new EventAction_EnableQuitSG.\u003CPreloadAssets\u003Ec__Iterator3F() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) EventAction_EnableQuitSG.mQuit || this.mResource == null)
        return;
      EventAction_EnableQuitSG.mQuit = UnityEngine.Object.Instantiate(this.mResource.asset) as EventQuit;
      EventAction_EnableQuitSG.mQuit.transform.SetParent(this.ActiveCanvas.transform, false);
      EventAction_EnableQuitSG.mQuit.transform.SetAsLastSibling();
      EventAction_EnableQuitSG.mQuit.OnClick = (UnityAction) (() =>
      {
        if (!((UnityEngine.Object) this.Sequence != (UnityEngine.Object) null))
          return;
        UnityEngine.Debug.LogWarning((object) ("AnalyticsEnableQuit: " + this.Sequence.name));
        this.SkipButtonAction(this.Special, this.Sequence, EventAction_EnableQuitSG.mQuit.gameObject);
        AnalyticsManager.TrackTutorialEventGeneric(this.AnalyticsTypeToTrackOnQuit);
      });
      EventAction_EnableQuitSG.mQuit.gameObject.SetActive(false);
    }

    private void SkipButtonAction(bool inIsSpecial, EventScript.Sequence inEventScriptSequence, GameObject inSkipButtonGameObject)
    {
      if (!inIsSpecial)
        GameUtility.FadeOut(2f);
      else
        inEventScriptSequence.GoToEndState();
      inEventScriptSequence.OnQuitImmediate();
      inSkipButtonGameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) EventAction_EnableQuitSG.mQuit)
        return;
      EventAction_EnableQuitSG.mQuit.gameObject.SetActive(true);
      EventAction_EnableQuitSG.mQuit.transform.SetAsLastSibling();
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      EventAction_EnableQuitSG.mQuit = (EventQuit) null;
    }
  }
}
