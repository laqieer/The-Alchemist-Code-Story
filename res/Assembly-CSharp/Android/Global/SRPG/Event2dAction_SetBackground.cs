// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("背景/配置(2D)", "背景を配置します", 5592405, 4473992)]
  public class Event2dAction_SetBackground : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/EventBackGround";
    [HideInInspector]
    public Texture2D Background;
    [HideInInspector]
    public EventBackGround mBackGround;
    private LoadRequest mBackGroundResource;

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
      return (IEnumerator) new Event2dAction_SetBackground.\u003CPreloadAssets\u003Ec__IteratorBC() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mBackGround == (UnityEngine.Object) null))
        return;
      this.mBackGround = EventBackGround.Find();
      if (!((UnityEngine.Object) this.mBackGround == (UnityEngine.Object) null) || this.mBackGroundResource == null)
        return;
      this.mBackGround = UnityEngine.Object.Instantiate(this.mBackGroundResource.asset) as EventBackGround;
      this.mBackGround.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mBackGround.transform.SetAsFirstSibling();
      this.mBackGround.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mBackGround != (UnityEngine.Object) null && !this.mBackGround.gameObject.activeInHierarchy)
        this.mBackGround.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.mBackGround != (UnityEngine.Object) null || (UnityEngine.Object) this.mBackGround.gameObject.GetComponent<RawImage>().texture != (UnityEngine.Object) this.Background)
        this.mBackGround.gameObject.GetComponent<RawImage>().texture = (Texture) this.Background;
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mBackGround != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mBackGround.gameObject);
    }
  }
}
