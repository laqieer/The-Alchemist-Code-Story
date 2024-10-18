// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("立ち絵/配置(2D)", "立ち絵を配置します", 5592405, 4473992)]
  public class Event2dAction_SpawnStandchara : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/EventStandChara";
    public string CharaID;
    public bool Flip;
    public EventStandChara.PositionTypes Position;
    [HideInInspector]
    public Texture2D StandcharaImage;
    [HideInInspector]
    public EventStandChara mStandChara;
    [HideInInspector]
    public IEnumerator mEnumerator;
    private LoadRequest mStandCharaResource;

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
      return (IEnumerator) new Event2dAction_SpawnStandchara.\u003CPreloadAssets\u003Ec__IteratorC1() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
      if (!((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null))
        return;
      this.mStandChara = UnityEngine.Object.Instantiate(this.mStandCharaResource.asset) as EventStandChara;
      RectTransform component = this.mStandChara.GetComponent<RectTransform>();
      this.mStandChara.InitPositionX(this.ActiveCanvas.transform as RectTransform);
      Vector3 vector3 = new Vector3(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
      component.position = vector3;
      this.mStandChara.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mStandChara.transform.SetAsFirstSibling();
      this.mStandChara.CharaID = this.CharaID;
      this.mStandChara.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandChara != (UnityEngine.Object) null && !this.mStandChara.gameObject.activeInHierarchy)
        this.mStandChara.gameObject.SetActive(true);
      if (!((UnityEngine.Object) this.mStandChara != (UnityEngine.Object) null))
        return;
      this.mStandChara.gameObject.GetComponent<RawImage>().color = new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.0f);
      this.mStandChara.gameObject.GetComponent<RawImage>().texture = (Texture) this.StandcharaImage;
      RectTransform component = this.mStandChara.GetComponent<RectTransform>();
      if ((UnityEngine.Object) this.mStandChara.transform.parent == (UnityEngine.Object) null)
      {
        Vector3 vector3_1 = new Vector3(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
        component.position = vector3_1;
        Vector3 vector3_2 = new Vector3(1f, 1f, 1f);
        component.localScale = vector3_2;
        this.mStandChara.transform.SetParent(this.ActiveCanvas.transform, false);
        this.mStandChara.transform.SetAsFirstSibling();
        this.mStandChara.transform.SetSiblingIndex(EventDialogBubbleCustom.FindHead().transform.GetSiblingIndex() - 1);
      }
      if (this.Flip)
        component.Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mStandChara.Open(0.3f);
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mStandChara != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandChara.gameObject);
    }

    public override void Update()
    {
      if (!this.enabled)
        return;
      if (!this.mStandChara.Fading && this.mStandChara.State == EventStandChara.StateTypes.FadeIn)
      {
        this.mStandChara.State = EventStandChara.StateTypes.Active;
        this.ActivateNext(true);
      }
      else
      {
        if (this.mStandChara.Fading || this.mStandChara.State != EventStandChara.StateTypes.FadeOut)
          return;
        this.mStandChara.State = EventStandChara.StateTypes.Inactive;
        this.enabled = false;
        this.mStandChara.gameObject.SetActive(false);
      }
    }

    public enum StateTypes
    {
      Initialized,
      StartFadeIn,
      FadeIn,
      EndFadeIn,
      StartFadeOut,
      FadeOut,
      EndFadeOut,
      Active,
      Inactive,
    }
  }
}
