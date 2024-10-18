// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FadeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("フェード(2D)", "画面をフェードイン・フェードアウトさせます", 5592405, 4473992)]
  public class Event2dAction_FadeScreen : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dFade";
    public float FadeTime = 1f;
    private Color FadeInColorWhite = new Color(1f, 1f, 1f, 0.0f);
    private Color FadeInColorBlack = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public bool FadeOut;
    public bool ChangeColor;
    public bool Async;
    private Event2dFade mEvent2dFade;
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
      return (IEnumerator) new Event2dAction_FadeScreen.\u003CPreloadAssets\u003Ec__IteratorBA() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if ((UnityEngine.Object) this.mEvent2dFade != (UnityEngine.Object) null)
        return;
      this.mEvent2dFade = Event2dFade.Find();
      if ((UnityEngine.Object) this.mEvent2dFade != (UnityEngine.Object) null)
        return;
      this.mEvent2dFade = UnityEngine.Object.Instantiate(this.mResource.asset) as Event2dFade;
      this.mEvent2dFade.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mEvent2dFade.transform.SetAsLastSibling();
      this.mEvent2dFade.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mEvent2dFade == (UnityEngine.Object) null)
        return;
      this.mEvent2dFade.gameObject.SetActive(true);
      this.StartFade();
    }

    private void StartFade()
    {
      if (this.FadeOut)
      {
        if (this.ChangeColor)
          this.mEvent2dFade.FadeTo(Color.white, this.FadeTime);
        else
          this.mEvent2dFade.FadeTo(Color.black, this.FadeTime);
      }
      else if (this.ChangeColor)
        this.mEvent2dFade.FadeTo(this.FadeInColorWhite, this.FadeTime);
      else
        this.mEvent2dFade.FadeTo(this.FadeInColorBlack, this.FadeTime);
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    public override void Update()
    {
      if (this.mEvent2dFade.IsFading)
        return;
      this.ActivateNext();
    }
  }
}
