// Decompiled with JetBrains decompiler
// Type: SpriteSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
public class SpriteSheet : ScriptableObject
{
  public SpriteSheet.Item[] Items;
  [NonSerialized]
  public bool Dirty = true;

  public Sprite GetSprite(string name)
  {
    int hashCode = name.GetHashCode();
    if (this.Dirty)
    {
      this.RecalcHashCodes();
      this.Dirty = false;
    }
    for (int index = 0; index < this.Items.Length; ++index)
    {
      if (hashCode == this.Items[index].h && name == this.Items[index].n)
        return this.Items[index].s;
    }
    return (Sprite) null;
  }

  private void RecalcHashCodes()
  {
    for (int index = 0; index < this.Items.Length; ++index)
      this.Items[index].h = this.Items[index].n.GetHashCode();
  }

  [Serializable]
  public struct Item
  {
    public string n;
    public Sprite s;
    [NonSerialized]
    public int h;
  }
}
