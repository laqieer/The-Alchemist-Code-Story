﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LogDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

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
