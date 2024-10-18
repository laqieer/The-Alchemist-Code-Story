// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEntryTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class UnitEntryTrigger
  {
    public int type;
    public string unit = string.Empty;
    public string skill = string.Empty;
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
