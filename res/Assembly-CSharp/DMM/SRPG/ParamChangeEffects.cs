// Decompiled with JetBrains decompiler
// Type: SRPG.ParamChangeEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ParamChangeEffects : ScriptableObject
  {
    [HideInInspector]
    public ParamChangeEffects.EffectData[] Effects = new ParamChangeEffects.EffectData[0];

    public Sprite FindSprite(string type, bool isDebuff)
    {
      for (int index = 0; index < this.Effects.Length; ++index)
      {
        if (this.Effects[index].TypeName == type)
          return isDebuff ? this.Effects[index].OnDebuff : this.Effects[index].OnBuff;
      }
      return (Sprite) null;
    }

    [Serializable]
    public struct EffectData
    {
      public string TypeName;
      public Sprite OnBuff;
      public Sprite OnDebuff;
    }
  }
}
