// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class UnlockParam
  {
    public string iname;
    public UnlockTargets UnlockTarget;
    public int PlayerLevel;
    public int VipRank;

    public bool Deserialize(JSON_UnlockParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      try
      {
        this.UnlockTarget = (UnlockTargets) Enum.Parse(typeof (UnlockTargets), json.iname);
      }
      catch (Exception ex)
      {
        return false;
      }
      this.PlayerLevel = json.lv;
      this.VipRank = json.vip;
      return true;
    }

    public override string ToString()
    {
      return string.Format("[UnlockParam]iname: {0},\tUnlockTarget: {1},\tPlayerLevel: {2},\t VipRank: {3}", new object[4]{ (object) this.iname, (object) this.UnlockTarget, (object) this.PlayerLevel, (object) this.VipRank });
    }
  }
}
