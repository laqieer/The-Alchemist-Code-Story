// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilitySkillList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitAbilitySkillList : MonoBehaviour
  {
    public ListItemEvents ItemTemplate;
    public ScrollRect ScrollViewRect;
    public UnitAbilitySkillList.SelectSkillEvent OnSelectSkill;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private Unit mUnit;

    public void Start()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      ((Component) this.ItemTemplate).gameObject.SetActive(false);
    }

    public void Refresh(Unit self)
    {
      this.mUnit = self;
      this.Refresh();
    }

    public void Refresh()
    {
      this.DestroyItems();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
      {
        Debug.LogError((object) "ItemTemplate が未設定です。");
      }
      else
      {
        AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(((Component) this).gameObject, (AbilityData) null);
        if (dataOfClass == null)
        {
          Debug.LogWarning((object) "AbilityData を参照できません。");
        }
        else
        {
          this.ScrollViewRect.normalizedPosition = new Vector2(0.5f, 1f);
          GameParameter.UpdateAll(((Component) this).gameObject);
          Transform parent = ((Component) this.ItemTemplate).transform.parent;
          for (int index = 0; index < dataOfClass.Skills.Count; ++index)
          {
            ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.ItemTemplate);
            ((Component) listItemEvents).transform.SetParent(parent, false);
            this.mItems.Add(listItemEvents);
            SkillData skill = dataOfClass.Skills[index];
            DataSource.Bind<SkillData>(((Component) listItemEvents).gameObject, skill);
            DataSource.Bind<Unit>(((Component) listItemEvents).gameObject, this.mUnit);
            ((Component) listItemEvents).gameObject.SetActive(true);
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go => this.SelectSkill(DataSource.FindDataOfClass<SkillData>(go, (SkillData) null)));
            Selectable selectable = ((Component) listItemEvents).GetComponentInChildren<Selectable>();
            if (Object.op_Equality((Object) selectable, (Object) null))
              selectable = ((Component) listItemEvents).GetComponent<Selectable>();
            if (Object.op_Inequality((Object) selectable, (Object) null))
            {
              selectable.interactable = this.mUnit.CheckEnableUseSkill(skill);
              if (selectable.interactable)
                selectable.interactable = this.mUnit.IsUseSkillCollabo(skill, true);
              ((Behaviour) selectable).enabled = !((Behaviour) selectable).enabled;
              ((Behaviour) selectable).enabled = !((Behaviour) selectable).enabled;
            }
            UnitAbilitySkillListItem component = ((Component) listItemEvents).gameObject.GetComponent<UnitAbilitySkillListItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              bool noLimit = !this.mUnit.CheckEnableSkillUseCount(skill);
              component.SetSkillCount((int) this.mUnit.GetSkillUseCount(skill), (int) this.mUnit.GetSkillUseCountMax(skill), noLimit);
              component.SetCastSpeed(skill.CastSpeed);
            }
          }
        }
      }
    }

    private void SelectSkill(SkillData skill)
    {
      if (skill == null)
        return;
      this.OnSelectSkill(skill);
    }

    private void DestroyItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).gameObject);
      this.mItems.Clear();
    }

    public delegate void SelectSkillEvent(SkillData skill);
  }
}
