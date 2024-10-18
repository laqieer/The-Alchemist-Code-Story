// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FadeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("フェード(2D)", "画面をフェードイン・フェードアウトさせます", 5592405, 4473992)]
  public class Event2dAction_FadeScreen : EventAction
  {
    public bool FadeOut;
    public bool ChangeColor;
    public bool Async;
    public float FadeTime = 1f;
    private Event2dFade mEvent2dFade;
    private LoadRequest mResource;
    private static readonly string AssetPath = "Event2dAssets/Event2dFade";
    private Color FadeInColorWhite = new Color(1f, 1f, 1f, 0.0f);
    private Color FadeInColorBlack = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_FadeScreen.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) this.mEvent2dFade, (Object) null))
        return;
      this.mEvent2dFade = Event2dFade.Find();
      if (Object.op_Inequality((Object) this.mEvent2dFade, (Object) null))
        return;
      this.mEvent2dFade = Object.Instantiate(this.mResource.asset) as Event2dFade;
      ((Component) this.mEvent2dFade).transform.SetParent((Transform) this.EventRootTransform, false);
      ((Component) this.mEvent2dFade).transform.SetAsLastSibling();
      ((Component) this.mEvent2dFade).gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mEvent2dFade, (Object) null))
        return;
      ((Component) this.mEvent2dFade).gameObject.SetActive(true);
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
