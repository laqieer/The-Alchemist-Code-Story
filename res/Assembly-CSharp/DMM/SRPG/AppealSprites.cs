// Decompiled with JetBrains decompiler
// Type: SRPG.AppealSprites
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AppealSprites : ScriptableObject
  {
    public AppealSprites.Item[] Items = new AppealSprites.Item[7];
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
          return this.Items[index].Sprite;
      }
      return (Sprite) null;
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
      public Sprite Sprite;
      [NonSerialized]
      public int hash;
    }
  }
}
