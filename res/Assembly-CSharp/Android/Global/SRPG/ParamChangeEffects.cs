// Decompiled with JetBrains decompiler
// Type: SRPG.ParamChangeEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

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
        {
          if (isDebuff)
            return this.Effects[index].OnDebuff;
          return this.Effects[index].OnBuff;
        }
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
