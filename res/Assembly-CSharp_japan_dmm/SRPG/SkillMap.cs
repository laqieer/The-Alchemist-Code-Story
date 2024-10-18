// Decompiled with JetBrains decompiler
// Type: SRPG.SkillMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class SkillMap
  {
    private Unit m_Owner;
    private uint[] m_SkillSeed;
    private List<SkillMap.Data> m_List;
    private BattleCore.SkillResult m_UseSkill;
    private AIAction m_Action;
    private bool m_IsNoExecActionSkill;
    private List<SkillData> m_AllSkills;
    private int m_AttackHeight;
    private int m_UseSkillNum;
    private List<List<SkillData>> m_UseSkillLists;
    private List<SkillData> m_ForceSkillList;
    private List<SkillData> m_HealSkills;
    private List<SkillData> m_DamageSkills;
    private List<SkillData> m_SupportSkills;
    private List<SkillData> m_CureConditionSkills;
    private List<SkillData> m_FailConditionSkills;
    private List<SkillData> m_DisableConditionSkills;
    private List<SkillData> m_TransformSkills;
    private List<SkillData> m_ProtectSkills;
    private List<SkillData> m_ExeSkills;

    public SkillMap()
    {
      this.m_Owner = (Unit) null;
      this.m_SkillSeed = new uint[4];
      this.m_List = new List<SkillMap.Data>();
      this.m_AllSkills = new List<SkillData>();
      this.m_UseSkillLists = new List<List<SkillData>>();
      this.m_ForceSkillList = new List<SkillData>();
      this.m_HealSkills = new List<SkillData>(5);
      this.m_DamageSkills = new List<SkillData>(5);
      this.m_SupportSkills = new List<SkillData>(5);
      this.m_CureConditionSkills = new List<SkillData>(5);
      this.m_FailConditionSkills = new List<SkillData>(5);
      this.m_DisableConditionSkills = new List<SkillData>(5);
      this.m_TransformSkills = new List<SkillData>(5);
      this.m_ProtectSkills = new List<SkillData>(5);
      this.m_ExeSkills = new List<SkillData>(5);
    }

    public Unit owner
    {
      set => this.m_Owner = value;
      get => this.m_Owner;
    }

    public uint[] skillSeed
    {
      set
      {
        for (int index = 0; index < value.Length; ++index)
        {
          if (index < this.m_SkillSeed.Length)
            this.m_SkillSeed[index] = value[index];
        }
      }
      get => this.m_SkillSeed;
    }

    public List<SkillMap.Data> list => this.m_List;

    public bool isNoExecActionSkill
    {
      set => this.m_IsNoExecActionSkill = value;
      get => this.m_IsNoExecActionSkill;
    }

    public List<SkillData> allSkills => this.m_AllSkills;

    public int attackHeight
    {
      set => this.m_AttackHeight = value;
      get => this.m_AttackHeight;
    }

    public int useSkillNum
    {
      set => this.m_UseSkillNum = value;
      get => this.m_UseSkillNum;
    }

    public List<List<SkillData>> useSkillLists => this.m_UseSkillLists;

    public List<SkillData> forceSkillList => this.m_ForceSkillList;

    public List<SkillData> healSkills => this.m_HealSkills;

    public List<SkillData> damageSkills => this.m_DamageSkills;

    public List<SkillData> supportSkills => this.m_SupportSkills;

    public List<SkillData> cureConditionSkills => this.m_CureConditionSkills;

    public List<SkillData> failConditionSkills => this.m_FailConditionSkills;

    public List<SkillData> disableConditionSkills => this.m_DisableConditionSkills;

    public List<SkillData> transformSkills => this.m_TransformSkills;

    public List<SkillData> protectSkills => this.m_ProtectSkills;

    public List<SkillData> exeSkills => this.m_ExeSkills;

    public void Clear()
    {
      this.m_AllSkills.Clear();
      this.m_UseSkill = (BattleCore.SkillResult) null;
      this.m_Action = (AIAction) null;
      this.m_UseSkillNum = 0;
      this.m_UseSkillLists.Clear();
      this.m_ForceSkillList.Clear();
      this.m_HealSkills.Clear();
      this.m_DamageSkills.Clear();
      this.m_SupportSkills.Clear();
      this.m_CureConditionSkills.Clear();
      this.m_FailConditionSkills.Clear();
      this.m_DisableConditionSkills.Clear();
      this.m_TransformSkills.Clear();
      this.m_ProtectSkills.Clear();
      this.m_ExeSkills.Clear();
      this.Reset();
      this.m_IsNoExecActionSkill = false;
    }

    public void Reset()
    {
      this.m_List.Clear();
      this.m_AttackHeight = 0;
    }

    public SkillMap.Data[] ToArray() => this.m_List.ToArray();

    public void Add(SkillMap.Data data) => this.m_List.Add(data);

    public SkillMap.Data Get(SkillData skill)
    {
      return skill != null ? this.m_List.Find((Predicate<SkillMap.Data>) (prop => prop.skill != null && prop.skill.SkillID == skill.SkillID)) : (SkillMap.Data) null;
    }

    public void SetUseSkill(BattleCore.SkillResult result) => this.m_UseSkill = result;

    public BattleCore.SkillResult GetUseSkill() => this.m_UseSkill;

    public bool HasUseSkill() => this.m_UseSkill != null;

    public List<SkillData> GetSkillList(int n)
    {
      if (n < this.m_UseSkillLists.Count)
      {
        if (this.m_UseSkillLists[n] != null)
          return this.m_UseSkillLists[n];
      }
      else if (n == this.m_UseSkillLists.Count)
        return this.m_ForceSkillList;
      return (List<SkillData>) null;
    }

    public List<SkillData> GetSkillList() => this.GetSkillList(this.m_UseSkillNum);

    public void NextSkillList() => ++this.m_UseSkillNum;

    public void SetAction(AIAction action) => this.m_Action = action;

    public AIAction GetAction() => this.m_Action;

    public void NextAction(bool isMove, BattleCore.SkillResult result)
    {
      if (this.m_Action == null)
        return;
      bool flag = (bool) this.m_Action.notBlock;
      if (isMove)
        flag = (int) this.m_Action.type == 2 || flag;
      else if (result != null && result.skill != null)
      {
        if (!string.IsNullOrEmpty((string) this.m_Action.skill))
          flag = (string) this.m_Action.skill == result.skill.SkillID || flag;
        else if ((int) this.m_Action.type == 1)
        {
          SkillData attackSkill = this.owner.GetAttackSkill();
          if (attackSkill != null)
            flag = attackSkill.SkillID == result.skill.SkillID || flag;
        }
        else if ((int) this.m_Action.type == 2)
          flag = true;
      }
      else
        flag = (int) this.m_Action.type == 0 || flag;
      if (this.m_Action.nextTurnAct != eAIActionNextTurnAct.NONE && this.m_IsNoExecActionSkill)
      {
        flag = this.m_Action.nextTurnAct == eAIActionNextTurnAct.NEXT_ACTION;
        if (this.m_Action.nextTurnAct == eAIActionNextTurnAct.SPECIFIED_ACTION && this.m_Action.turnActIdx > 0 && this.m_Owner.SetAIAction(this.m_Action.turnActIdx - 1) != null)
        {
          this.m_Action = (AIAction) null;
          return;
        }
      }
      if (!flag)
        return;
      this.m_Action = (AIAction) null;
      this.m_Owner.NextAIAction();
    }

    public void NextAction(bool isMove) => this.NextAction(isMove, (BattleCore.SkillResult) null);

    public void NextAction(BattleCore.SkillResult result) => this.NextAction(false, result);

    public AIAction SetAction(int index)
    {
      if (this.m_Owner == null)
        return (AIAction) null;
      AIAction aiAction = this.m_Owner.SetAIAction(index);
      if (aiAction != null)
        this.m_Action = aiAction;
      return aiAction;
    }

    public static int GetHash(int x, int y) => y * 1024 + x;

    public static int GetHash(IntVector2 pos) => SkillMap.GetHash(pos.x, pos.y);

    public class Score
    {
      public int priority;
      public IntVector2 pos;
      public SkillRange range;
      public LogSkill log;

      public Score(int _x, int _y, int _w, int _h)
      {
        this.priority = 0;
        this.pos.x = _x;
        this.pos.y = _y;
        this.range = new SkillRange(_w, _h);
      }

      public bool IsAttackRange(int x, int y) => this.range.Get(x, y);
    }

    public class Target
    {
      public IntVector2 pos;
      public Dictionary<int, SkillMap.Score> scores;

      public void Add(SkillMap.Score score)
      {
        this.scores[SkillMap.GetHash(score.pos.x, score.pos.y)] = score;
      }

      public SkillMap.Score Get(int x, int y)
      {
        SkillMap.Score score = (SkillMap.Score) null;
        this.scores.TryGetValue(SkillMap.GetHash(x, y), out score);
        return score;
      }

      public bool IsRange(int x, int y) => this.scores.ContainsKey(SkillMap.GetHash(x, y));

      public bool IsAttackRange(int x, int y)
      {
        foreach (KeyValuePair<int, SkillMap.Score> score in this.scores)
        {
          if (score.Value.IsAttackRange(x, y))
            return true;
        }
        return false;
      }
    }

    public class Data
    {
      public SkillData skill;
      public Dictionary<int, SkillMap.Target> targets;

      public Data(SkillData _skill)
      {
        this.skill = _skill;
        this.targets = new Dictionary<int, SkillMap.Target>();
      }

      public void Add(SkillMap.Target range) => this.targets[SkillMap.GetHash(range.pos)] = range;

      public SkillMap.Target Get(int x, int y)
      {
        SkillMap.Target target = (SkillMap.Target) null;
        this.targets.TryGetValue(SkillMap.GetHash(x, y), out target);
        return target;
      }

      public SkillMap.Score[] GetScores(int x, int y)
      {
        List<SkillMap.Score> scoreList = new List<SkillMap.Score>();
        foreach (KeyValuePair<int, SkillMap.Target> target in this.targets)
        {
          SkillMap.Score score = target.Value.Get(x, y);
          if (score != null)
            scoreList.Add(score);
        }
        return scoreList.ToArray();
      }

      public bool IsRange(int x, int y)
      {
        foreach (KeyValuePair<int, SkillMap.Target> target in this.targets)
        {
          if (target.Value.IsRange(x, y))
            return true;
        }
        return false;
      }

      public bool IsAttackRange(int x, int y)
      {
        foreach (KeyValuePair<int, SkillMap.Target> target in this.targets)
        {
          if (target.Value.IsAttackRange(x, y))
            return true;
        }
        return false;
      }
    }
  }
}
