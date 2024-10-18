// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵2/配置(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    private string DummyID = "dummyID";
    [SRPG.CharaID]
    public string CharaID;
    public bool Flip;
    public EventStandCharaController2.PositionTypes Position;
    [ObjectIsStandTemplate]
    public GameObject StandTemplate;
    public string Emotion;
    private GameObject mStandObject;
    private EventStandChara2 mEventStandChara;
    private EventStandCharaController2 mEVCharaController;
    private LoadRequest mStandCharaResource;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__IteratorC2 assetsCIteratorC2 = new Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__IteratorC2();
      return (IEnumerator) assetsCIteratorC2;
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
      if (!((UnityEngine.Object) this.mStandObject == (UnityEngine.Object) null) || !((UnityEngine.Object) this.StandTemplate != (UnityEngine.Object) null))
        return;
      this.mStandObject = UnityEngine.Object.Instantiate<GameObject>(this.StandTemplate);
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
      Vector2 vector2_1 = new Vector2(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
      RectTransform rectTransform = component;
      Vector2 vector2_2 = vector2_1;
      component.anchorMax = vector2_2;
      Vector2 vector2_3 = vector2_2;
      rectTransform.anchorMin = vector2_3;
      this.mStandObject.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mStandObject.transform.SetAsLastSibling();
      this.mEVCharaController.CharaID = this.CharaID;
      this.mStandObject.gameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null && !this.mStandObject.gameObject.activeInHierarchy)
        this.mStandObject.gameObject.SetActive(true);
      if (!((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null))
        return;
      this.mEVCharaController.Emotion = this.Emotion;
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      if ((UnityEngine.Object) component.transform.parent == (UnityEngine.Object) null)
      {
        Vector2 vector2_1 = new Vector2(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
        RectTransform rectTransform = component;
        Vector2 vector2_2 = vector2_1;
        component.anchorMax = vector2_2;
        Vector2 vector2_3 = vector2_2;
        rectTransform.anchorMin = vector2_3;
        Vector3 vector3 = new Vector3(1f, 1f, 1f);
        component.localScale = vector3;
        this.mStandObject.transform.SetParent(this.ActiveCanvas.transform, false);
        this.mStandObject.transform.SetAsLastSibling();
      }
      if (this.Flip)
        component.Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mEVCharaController.Open(0.5f);
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
        this.ActivateNext();
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
        this.ActivateNext();
      }
    }
  }
}
