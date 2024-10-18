// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/配置2(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara2 : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    private string DummyID = "dummyID";
    public string CharaID;
    public bool Flip;
    public EventStandCharaController2.PositionTypes Position;
    public GameObject StandTemplate;
    public string Emotion;
    public bool Async;
    public bool Fade;
    [HideInInspector]
    public float FadeTime;
    private GameObject mStandObject;
    private EventStandChara2 mEventStandChara;
    private EventStandCharaController2 mEVCharaController;
    private LoadRequest mStandCharaResource;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Event2dAction_StandChara2.\u003CPreloadAssets\u003Ec__Iterator0 assetsCIterator0 = new Event2dAction_StandChara2.\u003CPreloadAssets\u003Ec__Iterator0();
      return (IEnumerator) assetsCIterator0;
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mStandObject == (UnityEngine.Object) null))
        return;
      string id = this.DummyID;
      if (!string.IsNullOrEmpty(this.CharaID))
        id = this.CharaID;
      if ((UnityEngine.Object) EventStandCharaController2.FindInstances(id) != (UnityEngine.Object) null)
      {
        this.mEVCharaController = EventStandCharaController2.FindInstances(id);
        this.mStandObject = this.mEVCharaController.gameObject;
      }
      if ((UnityEngine.Object) this.mStandObject == (UnityEngine.Object) null && (UnityEngine.Object) this.StandTemplate != (UnityEngine.Object) null)
      {
        this.mStandObject = UnityEngine.Object.Instantiate<GameObject>(this.StandTemplate);
        this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
        this.mEVCharaController.CharaID = this.CharaID;
      }
      if (!((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null))
        return;
      this.mStandObject.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mStandObject.transform.SetAsLastSibling();
      this.mStandObject.gameObject.SetActive(false);
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      Vector2 vector2_1 = new Vector2(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
      RectTransform rectTransform = component;
      Vector2 vector2_2 = vector2_1;
      component.anchorMax = vector2_2;
      Vector2 vector2_3 = vector2_2;
      rectTransform.anchorMin = vector2_3;
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null && !this.mStandObject.gameObject.activeInHierarchy)
        this.mStandObject.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null)
      {
        this.mEVCharaController.Emotion = this.Emotion;
        RectTransform component = this.mStandObject.GetComponent<RectTransform>();
        Vector2 vector2_1 = new Vector2(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
        RectTransform rectTransform = component;
        Vector2 vector2_2 = vector2_1;
        component.anchorMax = vector2_2;
        Vector2 vector2_3 = vector2_2;
        rectTransform.anchorMin = vector2_3;
        Vector3 vector3 = new Vector3(1f, 1f, 1f);
        component.localScale = vector3;
        if ((UnityEngine.Object) component.transform.parent == (UnityEngine.Object) null)
        {
          this.mStandObject.transform.SetParent(this.ActiveCanvas.transform, false);
          this.mStandObject.transform.SetAsLastSibling();
        }
        if (this.Flip)
          component.Rotate(new Vector3(0.0f, 180f, 0.0f));
        if (this.Fade)
          this.mEVCharaController.Open(this.FadeTime);
        else
          this.mEVCharaController.Open(0.0f);
      }
      if (!this.Async)
        return;
      this.ActivateNext(true);
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandObject.gameObject);
    }

    public override void Update()
    {
      if (!this.enabled)
        return;
      if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeIn)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Active;
        if (!this.Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
      else if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeOut)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Inactive;
        this.enabled = false;
        this.mEVCharaController.gameObject.SetActive(false);
        this.mEVCharaController.transform.parent = (Transform) null;
      }
      else
      {
        if (this.mEVCharaController.Fading)
          return;
        if (!this.Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
    }
  }
}
