﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AIActionTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class AIActionTable
  {
    public List<AIAction> actions = new List<AIAction>();
    public int looped;

    public void Clear()
    {
      this.actions.Clear();
      this.looped = 0;
    }

    public void CopyTo(AIActionTable result)
    {
      if (result == null)
        return;
      result.actions.Clear();
      for (int index = 0; index < this.actions.Count; ++index)
      {
        AIAction aiAction = new AIAction();
        aiAction.skill = this.actions[index].skill;
        aiAction.type = this.actions[index].type;
        aiAction.turn = this.actions[index].turn;
        aiAction.notBlock = this.actions[index].notBlock;
        aiAction.noExecAct = this.actions[index].noExecAct;
        aiAction.nextActIdx = this.actions[index].nextActIdx;
        aiAction.nextTurnAct = this.actions[index].nextTurnAct;
        aiAction.turnActIdx = this.actions[index].turnActIdx;
        if (this.actions[index].cond != null)
        {
          aiAction.cond = new SkillLockCondition();
          this.actions[index].cond.CopyTo(aiAction.cond);
        }
        result.actions.Add(aiAction);
      }
      result.looped = this.looped;
    }
  }
}
