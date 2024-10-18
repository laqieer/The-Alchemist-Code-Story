// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEntryTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class UnitEntryTrigger
  {
    public string unit = string.Empty;
    public string skill = string.Empty;
    public int type;
    public int value;
    public int x;
    public int y;
    [NonSerialized]
    public bool on;

    public void Clear()
    {
      this.unit = string.Empty;
      this.skill = string.Empty;
      this.value = 0;
      this.x = 0;
      this.y = 0;
    }
  }
}
