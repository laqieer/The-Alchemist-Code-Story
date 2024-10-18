// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilitySkillList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
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
    private bool isCommandTutorial;

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
          this.isCommandTutorial = false;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if ((instance.Player.TutorialFlags & 1L) == 0L && (instance.GetNextTutorialStep() == "ShowAbilityCommand" || instance.GetNextTutorialStep() == "ShowMACommand"))
          {
            instance.CompleteTutorialStep();
            this.isCommandTutorial = true;
          }
          this.ScrollViewRect.normalizedPosition = new Vector2(0.5f, 1f);
          GameParameter.UpdateAll(this.gameObject);
          Transform parent = this.ItemTemplate.transform.parent;
          for (int index = 0; index < dataOfClass.Skills.Count; ++index)
          {
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemTemplate);
            listItemEvents.transform.SetParent(parent, false);
            this.mItems.Add(listItemEvents);
            SkillData skill = dataOfClass.Skills[index];
            DataSource.Bind<SkillData>(listItemEvents.gameObject, skill);
            DataSource.Bind<Unit>(listItemEvents.gameObject, this.mUnit);
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
            if (this.isCommandTutorial)
            {
              if (skill.SkillID == "SK_SEI_SWORD_CRASH")
              {
                SGHighlightObject.Instance().highlightedObject = listItemEvents.gameObject;
                SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_0.005", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.TopLeft, false, false, false);
              }
              else if (skill.SkillID == "SK_SEI_SHINING_CROSS_TUTORIAL")
              {
                SGHighlightObject.Instance().highlightedObject = listItemEvents.gameObject;
                SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_0.009", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, false, false, false);
              }
            }
          }
        }
      }
    }

    private void SelectSkill(SkillData skill)
    {
      if (skill != null)
        this.OnSelectSkill(skill);
      if (!this.isCommandTutorial)
        return;
      MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
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
