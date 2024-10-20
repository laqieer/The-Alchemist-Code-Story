﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetBackground
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
  [EventActionInfo("背景/配置(2D)", "背景を配置します", 5592405, 4473992)]
  public class Event2dAction_SetBackground : EventAction
  {
    [HideInInspector]
    public Texture2D Background;
    [HideInInspector]
    public EventBackGround mBackGround;
    private LoadRequest mBackGroundResource;
    private static readonly string AssetPath = "Event2dAssets/EventBackGround";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SetBackground.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBackGround, (Object) null))
        return;
      this.mBackGround = EventBackGround.Find();
      if (!Object.op_Equality((Object) this.mBackGround, (Object) null) || this.mBackGroundResource == null)
        return;
      this.mBackGround = Object.Instantiate(this.mBackGroundResource.asset) as EventBackGround;
      ((Component) this.mBackGround).transform.SetParent((Transform) this.EventRootTransform, false);
      ((Component) this.mBackGround).transform.SetAsFirstSibling();
      ((Component) this.mBackGround).gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBackGround, (Object) null) && !((Component) this.mBackGround).gameObject.activeInHierarchy)
        ((Component) this.mBackGround).gameObject.SetActive(true);
      if (Object.op_Inequality((Object) this.mBackGround, (Object) null) || Object.op_Inequality((Object) ((Component) this.mBackGround).gameObject.GetComponent<RawImage>().texture, (Object) this.Background))
        ((Component) this.mBackGround).gameObject.GetComponent<RawImage>().texture = (Texture) this.Background;
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mBackGround, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mBackGround).gameObject);
    }
  }
}