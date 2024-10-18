// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class TrophyState
  {
    public int[] Count = new int[0];
    public string iname;
    public bool IsEnded;
    public int StartYMD;
    public DateTime RewardedAt;
    public bool IsDirty;
    public TrophyParam Param;
    public bool IsSending;

    public bool IsCompleted
    {
      get
      {
        if (this.Param == null || this.Count.Length < this.Param.Objectives.Length)
          return false;
        for (int index = 0; index < this.Param.Objectives.Length && index < this.Count.Length; ++index)
        {
          if (this.Count[index] < this.Param.Objectives[index].RequiredCount)
            return false;
        }
        return true;
      }
    }

    public override string ToString()
    {
      return "iname: " + this.iname + (object) '\n' + "IsEnded: " + (object) this.IsEnded + (object) '\n' + "Count: " + ((IList<int>) this.Count).ListToString<int>(false, false, false, string.Empty, string.Empty, string.Empty) + (object) '\n' + "StartYMD: " + (object) this.StartYMD + (object) '\n' + "IsDirty: " + (object) this.IsDirty + (object) '\n' + "IsCompleted: " + (object) this.IsCompleted + (object) '\n' + "IsSending: " + (object) this.IsSending + (object) '\n' + "RewardedAt: " + (object) this.RewardedAt + (object) '\n' + "Param.DispType: " + (object) this.Param.DispType + (object) '\n' + "Param.Category: " + this.Param.Category;
    }
  }
}
