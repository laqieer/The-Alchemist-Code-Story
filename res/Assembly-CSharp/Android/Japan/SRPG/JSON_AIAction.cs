// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
