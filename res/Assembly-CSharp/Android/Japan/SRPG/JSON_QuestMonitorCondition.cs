// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestMonitorCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class JSON_QuestMonitorCondition
  {
    public UnitMonitorCondition[] actions;
    public UnitMonitorCondition[] goals;
    public UnitMonitorCondition[] withdraw;

    public void CopyTo(QuestMonitorCondition dst)
    {
      dst.Clear();
      if (this.actions != null && this.actions.Length > 0)
        dst.actions = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.actions);
      if (this.goals != null && this.goals.Length > 0)
        dst.goals = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.goals);
      if (this.withdraw == null || this.withdraw.Length <= 0)
        return;
      dst.withdraw = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.withdraw);
    }
  }
}
