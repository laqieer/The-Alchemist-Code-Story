// Decompiled with JetBrains decompiler
// Type: SRPG.BattleLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public abstract class BattleLog
  {
    public virtual void Serialize(StringBuilder dst)
    {
    }

    public virtual void Deserialize(string log)
    {
    }
  }
}
