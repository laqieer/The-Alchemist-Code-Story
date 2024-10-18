// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetActorMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/マテリアル変更", "マテリアルプロパティーを変更", 5592405, 4473992)]
  public class EventAction_SetActorMaterial : EventAction
  {
    [HideInInspector]
    public bool allMaterials = true;
    [HideInInspector]
    public Color blendColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    [StringIsActorList]
    public string ActorID;
    [HideInInspector]
    public string materialName;
    [HideInInspector]
    public bool changeTexture;
    [HideInInspector]
    public Texture2D texture;
    [HideInInspector]
    public EventAction_SetActorMaterial.ColorModes mode;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if ((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null)
      {
        Renderer[] componentsInChildren = byUniqueName.gameObject.GetComponentsInChildren<Renderer>(true);
        for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        {
          Material material = componentsInChildren[index].material;
          if ((!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false))) && (this.allMaterials || material.name == this.materialName + " (Instance)"))
          {
            if (this.changeTexture)
              material.SetTexture("_MainTex", (Texture) this.texture);
            material.EnableKeyword("MONOCHROME_OFF");
            material.DisableKeyword("MONOCHROME_ON");
            material.EnableKeyword("COLORBLEND_OFF");
            material.DisableKeyword("COLORBLEND_ON");
            switch (this.mode)
            {
              case EventAction_SetActorMaterial.ColorModes.Monochrome:
                material.EnableKeyword("MONOCHROME_ON");
                material.DisableKeyword("MONOCHROME_OFF");
                continue;
              case EventAction_SetActorMaterial.ColorModes.Blend:
                Color blendColor = this.blendColor;
                material.EnableKeyword("COLORBLEND_ON");
                material.DisableKeyword("COLORBLEND_OFF");
                material.SetColor("_blendColor", blendColor);
                continue;
              default:
                continue;
            }
          }
        }
      }
      this.ActivateNext();
    }

    public enum ColorModes
    {
      None,
      Monochrome,
      Blend,
    }
  }
}
