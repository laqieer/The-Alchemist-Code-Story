// Decompiled with JetBrains decompiler
// Type: SRPG.AppealSprites
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

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
