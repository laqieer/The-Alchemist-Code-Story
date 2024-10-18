// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_AIAction
  {
    public string skill;
    public int type;
    public int turn;
    public int notBlock;
    public int noExecAct;
    public int nextActIdx;
    public int nextTurnAct;
    public int turnActIdx;
    public JSON_SkillLockCondition cond;
  }
}
