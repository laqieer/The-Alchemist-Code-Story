// Decompiled with JetBrains decompiler
// Type: SRPG.KeyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class KeyItem
  {
    public string iname;
    public int num;

    public bool IsHasItem()
    {
      return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.iname) >= this.num;
    }

    public bool IsHas() => MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.iname) > 0;
  }
}
