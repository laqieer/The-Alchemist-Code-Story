// Decompiled with JetBrains decompiler
// Type: SRPG.AppealBallonSprites
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AppealBallonSprites : ScriptableObject
  {
    public AppealBallonSprites.Item[] Items = new AppealBallonSprites.Item[7];
    private bool Dirty = true;

    public Sprite GetSprite(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
          return this.Items[index].CharSprite;
      }
      return (Sprite) null;
    }

    public Sprite GetSpriteTextL(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
          return this.Items[index].TextLSprite;
      }
      return (Sprite) null;
    }

    public Sprite GetSpriteTextR(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
          return this.Items[index].TextRSprite;
      }
      return (Sprite) null;
    }

    public Sprite[] GetSprites(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      Sprite[] sprites = new Sprite[3];
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
        {
          sprites[0] = this.Items[index].CharSprite;
          sprites[1] = this.Items[index].TextLSprite;
          sprites[1] = this.Items[index].TextRSprite;
          break;
        }
      }
      return sprites;
    }

    private void RecalcHashCodes()
    {
      for (int index = 0; index < this.Items.Length; ++index)
        this.Items[index].hash = this.Items[index].ID.GetHashCode();
    }

    [Serializable]
    public struct Item
    {
      public string ID;
      public Sprite CharSprite;
      public Sprite TextLSprite;
      public Sprite TextRSprite;
      [NonSerialized]
      public int hash;
    }
  }
}
