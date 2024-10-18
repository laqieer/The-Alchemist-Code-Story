// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilitySkillList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitAbilitySkillList : MonoBehaviour
  {
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public ListItemEvents ItemTemplate;
    public ScrollRect ScrollViewRect;
    public UnitAbilitySkillList.SelectSkillEvent OnSelectSkill;
    private Unit mUnit;

    public void Start()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null))
        return;
      this.ItemTemplate.gameObject.SetActive(false);
    }

    public void Refresh(Unit self)
    {
      this.mUnit = self;
      this.Refresh();
    }

    public void Refresh()
    {
      this.DestroyItems();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "ItemTemplate が未設定です。");
      }
      else
      {
        AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(this.gameObject, (AbilityData) null);
        if (dataOfClass == null)
        {
          Debug.LogWarning((object) "AbilityData を参照できません。");
        }
        else
        {
          this.ScrollViewRect.normalizedPosition = new Vector2(0.5f, 1f);
          GameParameter.UpdateAll(this.gameObject);
          Transform parent = this.ItemTemplate.transform.parent;
          for (int index = 0; index < dataOfClass.Skills.Count; ++index)
          {
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemTemplate);
            listItemEvents.transform.SetParent(parent, false);
            this.mItems.Add(listItemEvents);
            SkillData skill = dataOfClass.Skills[index];
            DataSource.Bind<SkillData>(listItemEvents.gameObject, skill, false);
            DataSource.Bind<Unit>(listItemEvents.gameObject, this.mUnit, false);
            listItemEvents.gameObject.SetActive(true);
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go => this.SelectSkill(DataSource.FindDataOfClass<SkillData>(go, (SkillData) null)));
            Selectable selectable = listItemEvents.GetComponentInChildren<Selectable>();
            if ((UnityEngine.Object) selectable == (UnityEngine.Object) null)
              selectable = listItemEvents.GetComponent<Selectable>();
            if ((UnityEngine.Object) selectable != (UnityEngine.Object) null)
            {
              selectable.interactable = this.mUnit.CheckEnableUseSkill(skill, false);
              if (selectable.interactable)
                selectable.interactable = this.mUnit.IsUseSkillCollabo(skill, true);
              selectable.enabled = !selectable.enabled;
              selectable.enabled = !selectable.enabled;
            }
            UnitAbilitySkillListItem component = listItemEvents.gameObject.GetComponent<UnitAbilitySkillListItem>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject);
      this.mItems.Clear();
    }

    public delegate void SelectSkillEvent(SkillData skill);
  }
}
