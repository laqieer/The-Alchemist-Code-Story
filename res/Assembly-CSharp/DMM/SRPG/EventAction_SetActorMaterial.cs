// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetActorMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/マテリアル変更", "マテリアルプロパティーを変更", 5592405, 4473992)]
  public class EventAction_SetActorMaterial : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    [HideInInspector]
    public bool allMaterials = true;
    [HideInInspector]
    public string materialName;
    [HideInInspector]
    public bool changeTexture;
    [HideInInspector]
    public Texture2D texture;
    [HideInInspector]
    public EventAction_SetActorMaterial.ColorModes mode;
    [HideInInspector]
    public Color blendColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (Object.op_Inequality((Object) byUniqueName, (Object) null))
      {
        Renderer[] componentsInChildren = ((Component) byUniqueName).gameObject.GetComponentsInChildren<Renderer>(true);
        for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        {
          Material material = componentsInChildren[index].material;
          if ((!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false))) && (this.allMaterials || ((Object) material).name == this.materialName + " (Instance)"))
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
