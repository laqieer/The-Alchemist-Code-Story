﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SwapUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/Swap", 32741)]
  [FlowNode.Pin(1, "Swap In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Swap Out", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SwapUI : FlowNode
  {
    [FlowNode.ShowInInfo]
    public GameObject Target;
    public bool Deactivate;
    private GameObject mDummy;
    private DestroyEventListener mTargetDestroyEvent;
    private DestroyEventListener mDummyDestroyEvent;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
          return;
        this.SwapOut();
        this.ActivateOutputLinks(10);
      }
      else
      {
        this.SwapIn();
        this.ActivateOutputLinks(10);
      }
    }

    private void SwapIn()
    {
      if ((UnityEngine.Object) this.mDummy == (UnityEngine.Object) null || (UnityEngine.Object) this.Target == (UnityEngine.Object) null)
        return;
      Transform transform1 = this.Target.transform;
      Transform transform2 = this.mDummy.transform;
      transform1.SetParent(transform2.parent, false);
      transform1.SetSiblingIndex(transform2.GetSiblingIndex());
      this.mDummy.GetComponent<DestroyEventListener>().Listeners = (DestroyEventListener.DestroyEvent) null;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mDummy.gameObject);
      this.mDummy = (GameObject) null;
      if (!this.Deactivate)
        return;
      this.Target.SetActive(true);
    }

    private void SwapOut()
    {
      if ((UnityEngine.Object) this.mDummy != (UnityEngine.Object) null || (UnityEngine.Object) this.Target == (UnityEngine.Object) null)
        return;
      Transform transform1 = this.Target.transform;
      this.mDummy = new GameObject(this.Target.name + "(Dummy)", new System.Type[1]
      {
        typeof (DestroyEventListener)
      });
      Transform transform2 = this.mDummy.transform;
      transform2.SetParent(transform1.parent, false);
      transform2.SetSiblingIndex(transform1.GetSiblingIndex());
      this.mDummyDestroyEvent = this.mDummy.GetComponent<DestroyEventListener>();
      this.mTargetDestroyEvent = this.Target.gameObject.AddComponent<DestroyEventListener>();
      this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) (go =>
      {
        this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Target);
      });
      this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) (go =>
      {
        this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mDummy);
      });
      transform1.SetParent((Transform) UIUtility.Pool, false);
      if (!this.Deactivate)
        return;
      this.Target.SetActive(false);
    }
  }
}
