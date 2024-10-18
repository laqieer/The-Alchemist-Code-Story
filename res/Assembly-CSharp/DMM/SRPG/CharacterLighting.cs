// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLighting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CharacterLighting : MonoBehaviour
  {
    private void Start() => this.Update();

    private void Update()
    {
      Vector3 position = ((Component) this).transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if (Object.op_Equality((Object) volume, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      MeshRenderer[] componentsInChildren1 = ((Component) this).GetComponentsInChildren<MeshRenderer>();
      for (int index = 0; index < componentsInChildren1.Length; ++index)
      {
        ((Renderer) componentsInChildren1[index]).material.SetColor("_directLitColor", directLit);
        ((Renderer) componentsInChildren1[index]).material.SetColor("_indirectLitColor", indirectLit);
      }
      SkinnedMeshRenderer[] componentsInChildren2 = ((Component) this).GetComponentsInChildren<SkinnedMeshRenderer>();
      for (int index = 0; index < componentsInChildren2.Length; ++index)
      {
        ((Renderer) componentsInChildren2[index]).material.SetColor("_directLitColor", directLit);
        ((Renderer) componentsInChildren2[index]).material.SetColor("_indirectLitColor", indirectLit);
      }
    }
  }
}
