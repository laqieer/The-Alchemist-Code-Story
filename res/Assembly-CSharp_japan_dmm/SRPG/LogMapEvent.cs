// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class LogMapEvent : BattleLog
  {
    public Unit self;
    public Unit target;
    public EEventType type;
    public EEventGimmick gimmick;
    public int heal;
    public BuffBit buff = new BuffBit();
    public BuffBit debuff = new BuffBit();

    public bool IsBuffEffect()
    {
      for (int index = 0; index < this.buff.bits.Length; ++index)
      {
        if (this.buff.bits[index] != 0)
          return true;
      }
      for (int index = 0; index < this.debuff.bits.Length; ++index)
      {
        if (this.debuff.bits[index] != 0)
          return true;
      }
      return false;
    }
  }
}
