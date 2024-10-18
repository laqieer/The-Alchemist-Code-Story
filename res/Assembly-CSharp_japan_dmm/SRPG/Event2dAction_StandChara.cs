// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("立ち絵2/配置(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    public string CharaID;
    public bool Flip;
    public EventStandCharaController2.PositionTypes Position;
    public GameObject StandTemplate;
    public string Emotion;
    private string DummyID = "dummyID";
    private GameObject mStandObject;
    private EventStandChara2 mEventStandChara;
    private EventStandCharaController2 mEVCharaController;
    private LoadRequest mStandCharaResource;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__Iterator0 assetsCIterator0 = new Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__Iterator0();
      return (IEnumerator) assetsCIterator0;
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandObject, (Object) null))
        return;
      string id = this.DummyID;
      if (!string.IsNullOrEmpty(this.CharaID))
        id = this.CharaID;
      if (Object.op_Inequality((Object) EventStandCharaController2.FindInstances(id), (Object) null))
      {
        this.mEVCharaController = EventStandCharaController2.FindInstances(id);
        this.mStandObject = ((Component) this.mEVCharaController).gameObject;
      }
      if (!Object.op_Equality((Object) this.mStandObject, (Object) null) || !Object.op_Inequality((Object) this.StandTemplate, (Object) null))
        return;
      this.mStandObject = Object.Instantiate<GameObject>(this.StandTemplate);
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
      RectTransform rectTransform = component;
      Vector2 vector2_2 = vector2_1;
      component.anchorMax = vector2_2;
      Vector2 vector2_3 = vector2_2;
      rectTransform.anchorMin = vector2_3;
      this.mStandObject.transform.SetParent((Transform) this.EventRootTransform, false);
      this.mStandObject.transform.SetAsLastSibling();
      this.mEVCharaController.CharaID = this.CharaID;
      this.mStandObject.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mStandObject, (Object) null) && !this.mStandObject.gameObject.activeInHierarchy)
        this.mStandObject.gameObject.SetActive(true);
      if (!Object.op_Inequality((Object) this.mStandObject, (Object) null))
        return;
      this.mEVCharaController.Emotion = this.Emotion;
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      if (Object.op_Equality((Object) ((Component) component).transform.parent, (Object) null))
      {
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
        RectTransform rectTransform = component;
        Vector2 vector2_2 = vector2_1;
        component.anchorMax = vector2_2;
        Vector2 vector2_3 = vector2_2;
        rectTransform.anchorMin = vector2_3;
        Vector3 vector3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(1f, 1f, 1f);
        ((Transform) component).localScale = vector3;
        this.mStandObject.transform.SetParent((Transform) this.EventRootTransform, false);
        this.mStandObject.transform.SetAsLastSibling();
      }
      if (this.Flip)
        ((Transform) component).Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mEVCharaController.Open();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mStandObject, (Object) null))
        return;
      Object.Destroy((Object) this.mStandObject.gameObject);
    }

    public override void Update()
    {
      if (!this.enabled)
        return;
      if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeIn)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Active;
        this.ActivateNext();
      }
      else if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeOut)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Inactive;
        this.enabled = false;
        ((Component) this.mEVCharaController).gameObject.SetActive(false);
        ((Component) this.mEVCharaController).transform.parent = (Transform) null;
      }
      else
      {
        if (this.mEVCharaController.Fading)
          return;
        this.ActivateNext();
      }
    }
  }
}
