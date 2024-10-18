﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMonitorCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class QuestMonitorCondition
  {
    public List<UnitMonitorCondition> actions = new List<UnitMonitorCondition>();
    public List<UnitMonitorCondition> goals = new List<UnitMonitorCondition>();
    public List<UnitMonitorCondition> withdraw = new List<UnitMonitorCondition>();

    public void Clear()
    {
      this.actions.Clear();
      this.goals.Clear();
      this.withdraw.Clear();
    }

    public void CopyTo(QuestMonitorCondition dst)
    {
      this.Clear();
      for (int index = 0; index < this.actions.Count; ++index)
        dst.actions.Add(this.actions[index]);
      for (int index = 0; index < this.goals.Count; ++index)
        dst.goals.Add(this.goals[index]);
      for (int index = 0; index < this.goals.Count; ++index)
        dst.goals.Add(this.withdraw[index]);
    }
  }
}
