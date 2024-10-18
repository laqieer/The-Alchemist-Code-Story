// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FlipStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/反転(2D)", "立ち絵2を反転します", 5592405, 4473992)]
  public class Event2dAction_FlipStandChara : EventAction
  {
    public static List<EventStandCharaController2> InstancesForFlip = new List<EventStandCharaController2>();
    public float Time = 1f;
    private List<RawImage> fadeInList = new List<RawImage>();
    private List<RawImage> fadeOutList = new List<RawImage>();
    public string CharaID;
    public bool async;
    private GameObject mStandObjectFlip;
    private EventStandCharaController2 mEVCharaController;
    private EventStandCharaController2 mEVCharaFlipController;
    private float offset;
    private Color InColor;
    private Color OutColor;

    public override void PreStart()
    {
      if (string.IsNullOrEmpty(this.CharaID))
        return;
      this.mEVCharaController = EventStandCharaController2.FindInstances(this.CharaID);
      string str = this.CharaID + "_Flip";
      for (int index = 0; index < Event2dAction_FlipStandChara.InstancesForFlip.Count; ++index)
      {
        if (Event2dAction_FlipStandChara.InstancesForFlip[index].CharaID == str)
        {
          this.mEVCharaFlipController = Event2dAction_FlipStandChara.InstancesForFlip[index];
          this.mStandObjectFlip = this.mEVCharaFlipController.gameObject;
          break;
        }
      }
      if (!((UnityEngine.Object) this.mEVCharaFlipController == (UnityEngine.Object) null) || !((UnityEngine.Object) this.mEVCharaController != (UnityEngine.Object) null))
        return;
      this.mStandObjectFlip = UnityEngine.Object.Instantiate<GameObject>(this.mEVCharaController.gameObject);
      this.mEVCharaFlipController = this.mStandObjectFlip.GetComponent<EventStandCharaController2>();
      this.mEVCharaFlipController.CharaID = str;
      Event2dAction_FlipStandChara.InstancesForFlip.Add(this.mEVCharaFlipController);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandObjectFlip == (UnityEngine.Object) null)
      {
        this.ActivateNext();
      }
      else
      {
        if (!this.mStandObjectFlip.gameObject.activeInHierarchy)
          this.mStandObjectFlip.gameObject.SetActive(true);
        this.mEVCharaFlipController.Emotion = this.mEVCharaController.Emotion;
        RectTransform transform1 = this.mEVCharaController.gameObject.transform as RectTransform;
        RectTransform transform2 = this.mStandObjectFlip.transform as RectTransform;
        transform2.SetParent(transform1.parent);
        transform2.localScale = transform1.localScale;
        transform2.anchorMax = transform1.anchorMax;
        transform2.anchorMin = transform1.anchorMin;
        transform2.anchoredPosition = transform1.anchoredPosition;
        transform2.localRotation = transform1.localRotation * Quaternion.Euler(0.0f, 180f, 0.0f);
        this.mEVCharaFlipController.Open(0.0f);
        for (int index = 0; index < this.mEVCharaController.StandCharaList.Length; ++index)
        {
          GameObject bodyObject = this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>().BodyObject;
          if ((UnityEngine.Object) bodyObject != (UnityEngine.Object) null)
            this.fadeOutList.Add(bodyObject.GetComponent<RawImage>());
          GameObject faceObject = this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>().FaceObject;
          if ((UnityEngine.Object) faceObject != (UnityEngine.Object) null)
            this.fadeOutList.Add(faceObject.GetComponent<RawImage>());
        }
        for (int index = 0; index < this.mEVCharaFlipController.StandCharaList.Length; ++index)
        {
          GameObject bodyObject = this.mEVCharaFlipController.StandCharaList[index].GetComponent<EventStandChara2>().BodyObject;
          if ((UnityEngine.Object) bodyObject != (UnityEngine.Object) null)
            this.fadeInList.Add(bodyObject.GetComponent<RawImage>());
          GameObject faceObject = this.mEVCharaFlipController.StandCharaList[index].GetComponent<EventStandChara2>().FaceObject;
          if ((UnityEngine.Object) faceObject != (UnityEngine.Object) null)
            this.fadeInList.Add(faceObject.GetComponent<RawImage>());
        }
        for (int index = 0; index < this.fadeOutList.Count; ++index)
        {
          if (this.fadeOutList[index].isActiveAndEnabled)
          {
            this.InColor = this.fadeOutList[index].color;
            break;
          }
        }
        this.OutColor = this.InColor;
        this.OutColor.a = 0.0f;
        for (int index = 0; index < this.fadeInList.Count; ++index)
          this.fadeInList[index].color = this.InColor;
        this.offset = (double) this.Time > 0.0 ? 0.0f : 1f;
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if ((double) this.offset >= 1.0)
      {
        this.mEVCharaFlipController.Close(0.0f);
        this.mEVCharaFlipController.gameObject.SetActive(false);
        this.mEVCharaController.gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0.0f, 180f, 0.0f));
        for (int index = 0; index < this.fadeOutList.Count; ++index)
          this.fadeOutList[index].color = this.InColor;
        if (this.async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        Color color1 = Color.Lerp(this.OutColor, this.InColor, this.offset);
        for (int index = 0; index < this.fadeInList.Count; ++index)
          this.fadeInList[index].color = color1;
        Color color2 = Color.Lerp(this.InColor, this.OutColor, this.offset);
        for (int index = 0; index < this.fadeOutList.Count; ++index)
          this.fadeOutList[index].color = color2;
        this.offset += UnityEngine.Time.deltaTime / this.Time;
        this.offset = Mathf.Clamp01(this.offset);
      }
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mStandObjectFlip != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandObjectFlip.gameObject);
    }
  }
}
