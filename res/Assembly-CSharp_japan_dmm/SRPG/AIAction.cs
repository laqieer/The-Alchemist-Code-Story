// Decompiled with JetBrains decompiler
// Type: SRPG.AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class AIAction
  {
    public OString skill;
    public OInt type;
    public OInt turn;
    public OBool notBlock;
    public eAIActionNoExecAct noExecAct;
    public int nextActIdx;
    public eAIActionNextTurnAct nextTurnAct;
    public int turnActIdx;
    public SkillLockCondition cond;
  }
}
