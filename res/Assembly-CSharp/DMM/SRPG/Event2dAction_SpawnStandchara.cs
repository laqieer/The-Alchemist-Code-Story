// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnStandchara
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
  [EventActionInfo("立ち絵/配置(2D)", "立ち絵を配置します", 5592405, 4473992)]
  public class Event2dAction_SpawnStandchara : EventAction
  {
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
    private static readonly string AssetPath = "Event2dAssets/EventStandChara";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SpawnStandchara.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = Object.Instantiate(this.mStandCharaResource.asset) as EventStandChara;
      RectTransform component = ((Component) this.mStandChara).GetComponent<RectTransform>();
      this.mStandChara.InitPositionX(this.EventRootTransform);
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
      ((Transform) component).position = vector3;
      ((Component) this.mStandChara).transform.SetParent((Transform) this.EventRootTransform, false);
      ((Component) this.mStandChara).transform.SetAsFirstSibling();
      this.mStandChara.CharaID = this.CharaID;
      ((Component) this.mStandChara).gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mStandChara, (Object) null) && !((Component) this.mStandChara).gameObject.activeInHierarchy)
        ((Component) this.mStandChara).gameObject.SetActive(true);
      if (!Object.op_Inequality((Object) this.mStandChara, (Object) null))
        return;
      ((Graphic) ((Component) this.mStandChara).gameObject.GetComponent<RawImage>()).color = new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.0f);
      ((Component) this.mStandChara).gameObject.GetComponent<RawImage>().texture = (Texture) this.StandcharaImage;
      RectTransform component = ((Component) this.mStandChara).GetComponent<RectTransform>();
      if (Object.op_Equality((Object) ((Component) this.mStandChara).transform.parent, (Object) null))
      {
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
        ((Transform) component).position = vector3_1;
        Vector3 vector3_2;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_2).\u002Ector(1f, 1f, 1f);
        ((Transform) component).localScale = vector3_2;
        ((Component) this.mStandChara).transform.SetParent((Transform) this.EventRootTransform, false);
        ((Component) this.mStandChara).transform.SetAsFirstSibling();
        ((Component) this.mStandChara).transform.SetSiblingIndex(((Component) EventDialogBubbleCustom.FindHead()).transform.GetSiblingIndex() - 1);
      }
      if (this.Flip)
        ((Transform) component).Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mStandChara.Open();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mStandChara, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mStandChara).gameObject);
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
        ((Component) this.mStandChara).gameObject.SetActive(false);
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
