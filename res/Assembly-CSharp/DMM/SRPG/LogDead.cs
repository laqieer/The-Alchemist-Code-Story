// Decompiled with JetBrains decompiler
// Type: SRPG.LogDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class LogDead : BattleLog
  {
    public List<LogDead.Param> list_normal = new List<LogDead.Param>();
    public List<LogDead.Param> list_sentence = new List<LogDead.Param>();

    public void Add(Unit unit, DeadTypes type)
    {
      LogDead.Param obj = new LogDead.Param();
      obj.self = unit;
      obj.type = type;
      if (type == DeadTypes.DeathSentence)
        this.list_sentence.Add(obj);
      else
        this.list_normal.Add(obj);
    }

    public struct Param
    {
      public Unit self;
      public DeadTypes type;
    }
  }
}
