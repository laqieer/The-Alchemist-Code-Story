// Decompiled with JetBrains decompiler
// Type: SpriteSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SpriteSheet : ScriptableObject
{
  [NonSerialized]
  public bool Dirty = true;
  public SpriteSheet.Item[] Items;

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
