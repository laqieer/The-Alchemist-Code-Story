// Decompiled with JetBrains decompiler
// Type: SRPG.LogAutoHeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class LogAutoHeal : BattleLog
  {
    public Unit self;
    public LogAutoHeal.HealType type;
    public int value;
    public int beforeHp;
    public int beforeMp;

    public enum HealType
    {
      Hp,
      Jewel,
    }
  }
}
