// Decompiled with JetBrains decompiler
// Type: SRPG.AIActionTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
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
