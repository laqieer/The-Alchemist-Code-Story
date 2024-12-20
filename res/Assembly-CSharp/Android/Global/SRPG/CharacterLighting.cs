﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLighting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CharacterLighting : MonoBehaviour
  {
    private void Start()
    {
      this.Update();
    }

    private void Update()
    {
      Vector3 position = this.transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if ((UnityEngine.Object) volume == (UnityEngine.Object) null)
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      MeshRenderer[] componentsInChildren1 = this.GetComponentsInChildren<MeshRenderer>();
      for (int index = 0; index < componentsInChildren1.Length; ++index)
      {
        componentsInChildren1[index].material.SetColor("_directLitColor", directLit);
        componentsInChildren1[index].material.SetColor("_indirectLitColor", indirectLit);
      }
      SkinnedMeshRenderer[] componentsInChildren2 = this.GetComponentsInChildren<SkinnedMeshRenderer>();
      for (int index = 0; index < componentsInChildren2.Length; ++index)
      {
        componentsInChildren2[index].material.SetColor("_directLitColor", directLit);
        componentsInChildren2[index].material.SetColor("_indirectLitColor", indirectLit);
      }
    }
  }
}
