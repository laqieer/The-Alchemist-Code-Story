// Decompiled with JetBrains decompiler
// Type: SRPG.InspSkillTriggerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class InspSkillTriggerParam
  {
    private string iname;
    private List<InspSkillTriggerParam.TriggerData> triggers;

    public string Iname => this.iname;

    public List<InspSkillTriggerParam.TriggerData> Triggers => this.triggers;

    private bool Deserialize(JSON_InspSkillTriggerParam json)
    {
      if (json == null || json.triggers == null || json.triggers.Length <= 0)
        return false;
      this.iname = json.iname;
      this.triggers = new List<InspSkillTriggerParam.TriggerData>();
      for (int index = 0; index < json.triggers.Length; ++index)
      {
        JSON_InspSkillTriggerParam.JSON_TriggerData trigger = json.triggers[index];
        InspSkillTriggerParam.TriggerData triggerData = new InspSkillTriggerParam.TriggerData();
        triggerData.type = (eInspSkillTriggerType) trigger.type;
        triggerData.val = trigger.val;
        if (triggerData.type == eInspSkillTriggerType.TARGET_UNIT_ELEMENT || triggerData.type == eInspSkillTriggerType.UNIT_ELEMENT)
        {
          triggerData.element = EElement.None;
          try
          {
            triggerData.element = (EElement) Enum.Parse(typeof (EElement), triggerData.val, true);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + triggerData.val));
          }
        }
        if (triggerData.type == eInspSkillTriggerType.TARGET_UNIT_LEVEL || triggerData.type == eInspSkillTriggerType.UNIT_LEVEL)
        {
          triggerData.val_int = 0;
          if (!int.TryParse(triggerData.val, out triggerData.val_int) && GameUtility.IsDebugBuild)
            Debug.LogError((object) ("Unknown numeric type: " + triggerData.val));
        }
        this.triggers.Add(triggerData);
      }
      return true;
    }

    public static bool Deserialize(
      JSON_InspSkillTriggerParam[] jsons,
      ref Dictionary<string, InspSkillTriggerParam> insp_skill_trigger_dic)
    {
      if (jsons == null || jsons.Length <= 0)
        return false;
      insp_skill_trigger_dic = new Dictionary<string, InspSkillTriggerParam>();
      for (int index = 0; index < jsons.Length; ++index)
      {
        JSON_InspSkillTriggerParam json = jsons[index];
        InspSkillTriggerParam skillTriggerParam = new InspSkillTriggerParam();
        if (skillTriggerParam.Deserialize(json) && !insp_skill_trigger_dic.ContainsKey(json.iname))
          insp_skill_trigger_dic.Add(json.iname, skillTriggerParam);
      }
      return true;
    }

    public bool Check(UnitData unit, List<UnitData> targets, SkillData use)
    {
      if (this.Triggers == null || this.Triggers.Count <= 0)
        return false;
      foreach (InspSkillTriggerParam.TriggerData trigger in this.Triggers)
      {
        if (!this.CheckCondition(trigger, unit, targets, use))
          return false;
      }
      return true;
    }

    private bool CheckCondition(
      InspSkillTriggerParam.TriggerData trigger,
      UnitData unit,
      List<UnitData> targets,
      SkillData use)
    {
      switch (trigger.type)
      {
        case eInspSkillTriggerType.UNIT:
          if (unit.UnitID == trigger.val)
            return true;
          break;
        case eInspSkillTriggerType.UNIT_ELEMENT:
          if (unit.Element == trigger.element)
            return true;
          break;
        case eInspSkillTriggerType.UNIT_TYPE:
          return false;
        case eInspSkillTriggerType.UNIT_JOB:
          if (unit.CurrentJob != null && unit.CurrentJob.JobID == trigger.val)
            return true;
          break;
        case eInspSkillTriggerType.UNIT_BIRTH:
          if ((string) unit.UnitParam.birth == trigger.val)
            return true;
          break;
        case eInspSkillTriggerType.USE_SKILL:
          if (use.SkillID == trigger.val)
            return true;
          break;
        case eInspSkillTriggerType.TARGET_UNIT:
        case eInspSkillTriggerType.TARGET_UNIT_ELEMENT:
        case eInspSkillTriggerType.TARGET_UNIT_TYPE:
        case eInspSkillTriggerType.TARGET_UNIT_JOB:
        case eInspSkillTriggerType.TARGET_UNIT_BIRTH:
        case eInspSkillTriggerType.TARGET_UNIT_LEVEL:
          if (this.CheckTargets(trigger, targets))
            return true;
          break;
        case eInspSkillTriggerType.UNIT_LEVEL:
          if (unit.Lv >= trigger.val_int)
            return true;
          break;
      }
      return false;
    }

    private bool CheckTargets(InspSkillTriggerParam.TriggerData trigger, List<UnitData> targets)
    {
      foreach (UnitData target in targets)
      {
        switch (trigger.type)
        {
          case eInspSkillTriggerType.TARGET_UNIT:
            if (target.UnitID == trigger.val)
              return true;
            continue;
          case eInspSkillTriggerType.TARGET_UNIT_ELEMENT:
            if (target.UnitParam.element == trigger.element)
              return true;
            continue;
          case eInspSkillTriggerType.TARGET_UNIT_JOB:
            if (target.CurrentJob.JobID == trigger.val)
              return true;
            continue;
          case eInspSkillTriggerType.TARGET_UNIT_BIRTH:
            if ((string) target.UnitParam.birth == trigger.val)
              return true;
            continue;
          case eInspSkillTriggerType.TARGET_UNIT_LEVEL:
            if (target.Lv >= trigger.val_int)
              return true;
            continue;
          default:
            continue;
        }
      }
      return false;
    }

    [MessagePackObject(true)]
    public class TriggerData
    {
      public eInspSkillTriggerType type;
      public string val;
      public int val_int;
      public EElement element;
    }
  }
}
