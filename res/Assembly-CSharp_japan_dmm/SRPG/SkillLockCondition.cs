// Decompiled with JetBrains decompiler
// Type: SRPG.SkillLockCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class SkillLockCondition
  {
    public int type;
    public int value;
    public List<int> x = new List<int>();
    public List<int> y = new List<int>();
    [NonSerialized]
    public bool unlock;

    public void Clear()
    {
      this.value = 0;
      this.x.Clear();
      this.y.Clear();
    }

    public void CopyTo(SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = this.x;
      dsc.y = this.y;
      dsc.unlock = this.unlock;
    }

    public void CopyTo(JSON_SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = this.x.ToArray();
      dsc.y = this.y.ToArray();
    }
  }
}
