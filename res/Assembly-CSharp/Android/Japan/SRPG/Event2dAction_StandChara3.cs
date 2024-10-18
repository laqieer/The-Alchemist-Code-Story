// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/配置3(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara3 : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    private static readonly Vector2 START_POSITION = new Vector2(-1f, 0.0f);
    private string DummyID = "dummyID";
    public bool NewMaterial = true;
    public string CharaID;
    public GameObject StandTemplate;
    private GameObject mStandObject;
    private EventStandCharaController2 mEVCharaController;
    private const string SHADER_NAME = "UI/Custom/EventStandChara";
    private const string PROPERTYNAME_SCALE_X = "_ScaleX";
    private const string PROPERTYNAME_SCALE_Y = "_ScaleY";
    private const string PROPERTYNAME_OFFSET_X = "_OffsetX";
    private const string PROPERTYNAME_OFFSET_Y = "_OffsetY";
    private const string PROPERTYNAME_FACE_TEX = "_FaceTex";

    public override void PreStart()
    {
      if (this.NewMaterial)
      {
        Shader.DisableKeyword("EVENT_MONOCHROME_ON");
        Shader.DisableKeyword("EVENT_SEPIA_ON");
      }
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
        if (this.NewMaterial)
        {
          for (int index = 0; index < this.mEVCharaController.StandCharaList.Length; ++index)
          {
            try
            {
              EventStandChara2 component1 = this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>();
              RectTransform component2 = component1.BodyObject.GetComponent<RectTransform>();
              RectTransform component3 = component1.FaceObject.GetComponent<RectTransform>();
              Vector2 vector2_1 = new Vector2(component2.sizeDelta.x * component2.localScale.x, component2.sizeDelta.y * component2.localScale.y);
              Vector2 vector2_2 = new Vector2(component3.sizeDelta.x * component3.localScale.x, component3.sizeDelta.y * component3.localScale.y);
              Vector2 vector2_3;
              Vector2 vector2_4;
              if (Mathf.Approximately(vector2_2.x, 0.0f) || Mathf.Approximately(vector2_2.y, 0.0f))
              {
                vector2_3 = new Vector2(0.0f, 0.0f);
                vector2_4 = new Vector2(2f, 2f);
              }
              else
              {
                vector2_3 = new Vector2(vector2_1.x / vector2_2.x, vector2_1.y / vector2_2.y);
                Vector2 vector2_5 = new Vector2(component3.localPosition.x - component3.pivot.x * vector2_2.x, component3.localPosition.y - component3.pivot.y * vector2_2.y) - new Vector2(component2.localPosition.x - component2.pivot.x * vector2_1.x, component2.localPosition.y - component2.pivot.y * vector2_1.y);
                vector2_4 = new Vector2(-1f * vector2_5.x / vector2_2.x, -1f * vector2_5.y / vector2_2.y);
              }
              Material material = new Material(Shader.Find("UI/Custom/EventStandChara"));
              Texture mainTexture = component1.FaceObject.GetComponent<RawImage>().mainTexture;
              this.SetMaterialProperty(material, "_FaceTex", mainTexture);
              this.SetMaterialProperty(material, "_ScaleX", vector2_3.x);
              this.SetMaterialProperty(material, "_ScaleY", vector2_3.y);
              this.SetMaterialProperty(material, "_OffsetX", vector2_4.x);
              this.SetMaterialProperty(material, "_OffsetY", vector2_4.y);
              component1.BodyObject.GetComponent<RawImage>().material = material;
              GameUtility.SetGameObjectActive(component1.FaceObject, false);
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      if (!((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null))
        return;
      this.mStandObject.transform.SetParent(this.ActiveCanvas.transform, false);
      this.mStandObject.transform.SetAsLastSibling();
      this.mStandObject.gameObject.SetActive(false);
      RectTransform component = this.mStandObject.GetComponent<RectTransform>();
      RectTransform rectTransform = component;
      Vector2 startPosition = Event2dAction_StandChara3.START_POSITION;
      component.anchorMax = startPosition;
      Vector2 vector2 = startPosition;
      rectTransform.anchorMin = vector2;
    }

    private bool SetMaterialProperty(Material material, string name, float val)
    {
      if (!material.HasProperty(name))
        return false;
      material.SetFloat(name, val);
      return true;
    }

    private bool SetMaterialProperty(Material material, string name, Texture val)
    {
      if (!material.HasProperty(name))
        return false;
      material.SetTexture(name, val);
      return true;
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null && !this.mStandObject.gameObject.activeInHierarchy)
        this.mStandObject.gameObject.SetActive(true);
      this.mEVCharaController.Open(0.0f);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mStandObject != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandObject.gameObject);
    }
  }
}
