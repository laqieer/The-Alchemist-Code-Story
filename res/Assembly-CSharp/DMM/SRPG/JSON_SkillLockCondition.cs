// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SkillLockCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_SkillLockCondition
  {
    public int type;
    public int value;
    public int[] x = new int[1]{ -1 };
    public int[] y = new int[1]{ -1 };

    public void CopyTo(SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = new List<int>((IEnumerable<int>) this.x);
      dsc.y = new List<int>((IEnumerable<int>) this.y);
    }

    public void CopyTo(JSON_SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = this.x;
      dsc.y = this.y;
    }
  }
}
