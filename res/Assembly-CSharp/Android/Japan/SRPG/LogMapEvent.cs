// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class LogMapEvent : BattleLog
  {
    public BuffBit buff = new BuffBit();
    public BuffBit debuff = new BuffBit();
    public Unit self;
    public Unit target;
    public EEventType type;
    public EEventGimmick gimmick;
    public int heal;

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
