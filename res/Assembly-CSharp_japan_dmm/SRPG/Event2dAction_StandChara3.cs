// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/立ち絵2/配置3(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara3 : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    public string CharaID;
    public GameObject StandTemplate;
    private string DummyID = "dummyID";
    public bool NewMaterial = true;
    private GameObject mStandObject;
    private EventStandCharaController2 mEVCharaController;
    private static readonly Vector2 START_POSITION = new Vector2(-1f, 0.0f);
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
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      string id = this.DummyID;
      if (!string.IsNullOrEmpty(this.CharaID))
        id = this.CharaID;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventStandCharaController2.FindInstances(id), (UnityEngine.Object) null))
      {
        this.mEVCharaController = EventStandCharaController2.FindInstances(id);
        this.mStandObject = ((Component) this.mEVCharaController).gameObject;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StandTemplate, (UnityEngine.Object) null))
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
              Vector2 vector2_1;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_1).\u002Ector(component2.sizeDelta.x * ((Transform) component2).localScale.x, component2.sizeDelta.y * ((Transform) component2).localScale.y);
              Vector2 vector2_2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_2).\u002Ector(component3.sizeDelta.x * ((Transform) component3).localScale.x, component3.sizeDelta.y * ((Transform) component3).localScale.y);
              Vector2 vector2_3;
              Vector2 vector2_4;
              if (Mathf.Approximately(vector2_2.x, 0.0f) || Mathf.Approximately(vector2_2.y, 0.0f))
              {
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_3).\u002Ector(0.0f, 0.0f);
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_4).\u002Ector(2f, 2f);
              }
              else
              {
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_3).\u002Ector(vector2_1.x / vector2_2.x, vector2_1.y / vector2_2.y);
                Vector2 vector2_5;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_5).\u002Ector(((Transform) component3).localPosition.x - component3.pivot.x * vector2_2.x, ((Transform) component3).localPosition.y - component3.pivot.y * vector2_2.y);
                Vector2 vector2_6;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_6).\u002Ector(((Transform) component2).localPosition.x - component2.pivot.x * vector2_1.x, ((Transform) component2).localPosition.y - component2.pivot.y * vector2_1.y);
                Vector2 vector2_7 = Vector2.op_Subtraction(vector2_5, vector2_6);
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_4).\u002Ector(-1f * vector2_7.x / vector2_2.x, -1f * vector2_7.y / vector2_2.y);
              }
              Material material = new Material(Shader.Find("UI/Custom/EventStandChara"));
              Texture mainTexture = ((Graphic) component1.FaceObject.GetComponent<RawImage>()).mainTexture;
              this.SetMaterialProperty(material, "_FaceTex", mainTexture);
              this.SetMaterialProperty(material, "_ScaleX", vector2_3.x);
              this.SetMaterialProperty(material, "_ScaleY", vector2_3.y);
              this.SetMaterialProperty(material, "_OffsetX", vector2_4.x);
              this.SetMaterialProperty(material, "_OffsetY", vector2_4.y);
              ((Graphic) component1.BodyObject.GetComponent<RawImage>()).material = material;
              GameUtility.SetGameObjectActive(component1.FaceObject, false);
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      this.mStandObject.transform.SetParent((Transform) this.EventRootTransform, false);
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null) && !this.mStandObject.gameObject.activeInHierarchy)
        this.mStandObject.gameObject.SetActive(true);
      this.mEVCharaController.Open(0.0f);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandObject.gameObject);
    }
  }
}
