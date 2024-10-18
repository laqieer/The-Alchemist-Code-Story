// Decompiled with JetBrains decompiler
// Type: SRPG.LogAutoHeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
