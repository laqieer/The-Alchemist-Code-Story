﻿// Decompiled with JetBrains decompiler
// Type: SRPG.KeyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

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
  }
}