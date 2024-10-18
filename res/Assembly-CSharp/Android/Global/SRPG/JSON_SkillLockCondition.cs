// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SkillLockCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class JSON_SkillLockCondition
  {
    public int[] x = new int[1]{ -1 };
    public int[] y = new int[1]{ -1 };
    public int type;
    public int value;

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
