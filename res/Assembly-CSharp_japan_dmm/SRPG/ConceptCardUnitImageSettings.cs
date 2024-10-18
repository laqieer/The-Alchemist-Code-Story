// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardUnitImageSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardUnitImageSettings : ScriptableObject
  {
    private const string ASSET_NAME = "ConceptCardSettings/ConceptCardUnitImageSettings";
    public UnitImageSetting UnitImageSetting;
    private static ConceptCardUnitImageSettings mInstance;

    public static ConceptCardUnitImageSettings Instance
    {
      get
      {
        if (Object.op_Equality((Object) ConceptCardUnitImageSettings.mInstance, (Object) null))
          ConceptCardUnitImageSettings.mInstance = AssetManager.Load<ConceptCardUnitImageSettings>("ConceptCardSettings/ConceptCardUnitImageSettings");
        return ConceptCardUnitImageSettings.mInstance;
      }
    }

    public static void ComposeUnitConceptCardImage(
      ConceptCardParam param,
      RawImage bgImage,
      GameObject imageTemplate,
      GameObject msg,
      Text msgText)
    {
      string path1 = string.IsNullOrEmpty(param.bg_image) ? AssetPath.ConceptCard(param.icon) : AssetPath.ConceptCard(param.bg_image);
      if (Object.op_Inequality((Object) bgImage, (Object) null))
      {
        string fileName = Path.GetFileName(path1);
        if (((Object) ((Graphic) bgImage).mainTexture).name != fileName)
        {
          bgImage.texture = (Texture) null;
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(bgImage, path1);
        }
      }
      if (param.unit_images != null && param.unit_images.Length > 0)
      {
        Dictionary<string, UnitImageSetting.Vector2AndFloat> table = ConceptCardUnitImageSettings.Instance.UnitImageSetting.GetTable();
        foreach (string unitImage in param.unit_images)
        {
          if (!Object.op_Equality((Object) imageTemplate, (Object) null))
          {
            GameObject gameObject = Object.Instantiate<GameObject>(imageTemplate);
            RawImage component = gameObject.GetComponent<RawImage>();
            string path2 = AssetPath.UnitImage(unitImage);
            component.texture = (Texture) null;
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(component, path2);
            gameObject.transform.SetParent(imageTemplate.transform.parent, false);
            gameObject.SetActive(true);
            RectTransform transform = gameObject.transform as RectTransform;
            transform.pivot = new Vector2(0.0f, 1f);
            UnitImageSetting.Vector2AndFloat vector2AndFloat;
            if (table != null && table.TryGetValue(unitImage, out vector2AndFloat))
            {
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(transform.sizeDelta.x * vector2AndFloat.Offset.x, transform.sizeDelta.y * vector2AndFloat.Offset.y);
              RectTransform rectTransform = transform;
              rectTransform.anchoredPosition = Vector2.op_Addition(rectTransform.anchoredPosition, vector2);
              ((Transform) transform).localScale = new Vector3(vector2AndFloat.Scale, vector2AndFloat.Scale, vector2AndFloat.Scale);
            }
          }
        }
        if (Object.op_Inequality((Object) msg, (Object) null))
          msg.SetActive(true);
        if (!Object.op_Inequality((Object) msgText, (Object) null))
          return;
        msgText.text = param.GetLocalizedTextMessage();
      }
      else
      {
        if (Object.op_Inequality((Object) msg, (Object) null))
          msg.SetActive(false);
        if (!Object.op_Inequality((Object) msgText, (Object) null))
          return;
        msgText.text = (string) null;
      }
    }
  }
}
