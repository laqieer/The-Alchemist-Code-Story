// Decompiled with JetBrains decompiler
// Type: SRPG.AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
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
