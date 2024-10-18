// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCommands
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitCommands : MonoBehaviour
  {
    [HideInInspector]
    private List<GameObject> mAbilityButtons = new List<GameObject>();
    public UnitCommands.CommandEvent OnCommandSelect;
    public UnitCommands.YesNoEvent OnYesNoSelect;
    public UnitCommands.MapExitEvent OnMapExitSelect;
    public UnitCommands.UnitChgEvent OnUnitChgSelect;
    public GameObject MoveButton;
    public GameObject AttackButton;
    public GameObject RenkeiButton;
    public GameObject ItemButton;
    public GameObject MapButton;
    public GameObject ExitMapButton;
    public GameObject EndButton;
    public GameObject AbilityButton;
    public GameObject GridEventButton;
    public GameObject OKButton;
    public GameObject CancelButton;
    public GameObject UnitChgButton;
    public Donuts DonutsBG;
    public float DonutsAnglePerItem;
    public float DonutsAngleStart;
    public string OtherSkillName;
    public string OtherSkillIconName;
    public string AbilityImageBG;
    public string AbilityImageIcon;
    public string AbilityName;
    public Color AbilityDisableColor;
    private bool mIsEnableUnitChgButton;
    private bool mIsActiveUnitChgButton;

    public UnitCommands.ButtonTypes VisibleButtons
    {
      set
      {
        bool is_enable = (value & UnitCommands.ButtonTypes.Action) != (UnitCommands.ButtonTypes) 0;
        bool flag1 = (value & UnitCommands.ButtonTypes.GridEvent) != (UnitCommands.ButtonTypes) 0;
        bool flag2 = (value & UnitCommands.ButtonTypes.Misc) != (UnitCommands.ButtonTypes) 0;
        bool flag3 = (value & UnitCommands.ButtonTypes.IsRenkei) != (UnitCommands.ButtonTypes) 0;
        bool flag4 = (value & UnitCommands.ButtonTypes.Map) != (UnitCommands.ButtonTypes) 0;
        int num = 0;
        if ((UnityEngine.Object) this.AttackButton != (UnityEngine.Object) null)
        {
          this.AttackButton.SetActive(is_enable && (value & UnitCommands.ButtonTypes.Attack) != (UnitCommands.ButtonTypes) 0 && (!flag3 || (UnityEngine.Object) this.RenkeiButton == (UnityEngine.Object) null));
          if (this.AttackButton.activeSelf)
            ++num;
        }
        if ((UnityEngine.Object) this.RenkeiButton != (UnityEngine.Object) null)
        {
          this.RenkeiButton.SetActive(is_enable && (value & UnitCommands.ButtonTypes.Attack) != (UnitCommands.ButtonTypes) 0 && (flag3 && (UnityEngine.Object) this.RenkeiButton != (UnityEngine.Object) null));
          if (this.RenkeiButton.activeSelf)
            ++num;
        }
        if ((UnityEngine.Object) this.ItemButton != (UnityEngine.Object) null)
        {
          this.ItemButton.SetActive(is_enable && (value & UnitCommands.ButtonTypes.Item) != (UnitCommands.ButtonTypes) 0);
          if (this.ItemButton.activeSelf)
            ++num;
        }
        for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        {
          this.mAbilityButtons[index].SetActive(is_enable && (value & UnitCommands.ButtonTypes.Skill) != (UnitCommands.ButtonTypes) 0);
          if (this.mAbilityButtons[index].activeSelf)
            ++num;
        }
        if ((UnityEngine.Object) this.GridEventButton != (UnityEngine.Object) null)
        {
          this.GridEventButton.SetActive(flag1);
          if (this.GridEventButton.activeSelf)
            ++num;
        }
        if ((UnityEngine.Object) this.EndButton != (UnityEngine.Object) null)
        {
          this.EndButton.SetActive(flag2);
          if (this.EndButton.activeSelf)
            ++num;
        }
        if ((UnityEngine.Object) this.MapButton != (UnityEngine.Object) null)
          this.MapButton.SetActive(flag4);
        if ((UnityEngine.Object) this.UnitChgButton != (UnityEngine.Object) null && this.mIsEnableUnitChgButton)
          this.EnableUnitChgButton(is_enable, false);
        if (!((UnityEngine.Object) this.DonutsBG != (UnityEngine.Object) null))
          return;
        if (num >= 2)
          this.DonutsBG.SetRange(this.DonutsAngleStart, this.DonutsAngleStart + (float) (num - 1) * this.DonutsAnglePerItem);
        else
          this.DonutsBG.SetRange(0.0f, 0.0f);
      }
    }

    private void OnDestroy()
    {
    }

    private void SetButtonEvent(GameObject go, UnitCommands.ClickEvent callback)
    {
      Button component = go.GetComponent<Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.onClick.AddListener(new UnityAction(callback.Invoke));
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.MoveButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.MoveButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Move, (object) 0);
        }));
      if ((UnityEngine.Object) this.AttackButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.AttackButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null || MonoSingleton<GameManager>.Instance.IsTutorial() && !((UnityEngine.Object) SGHighlightObject.Instance() == (UnityEngine.Object) null))
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Attack, (object) 0);
        }));
      if ((UnityEngine.Object) this.RenkeiButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.RenkeiButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Attack, (object) 0);
        }));
      if ((UnityEngine.Object) this.ItemButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.ItemButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Item, (object) 0);
        }));
      if ((UnityEngine.Object) this.MapButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.MapButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Map, (object) 0);
        }));
      if ((UnityEngine.Object) this.EndButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.EndButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.End, (object) 0);
        }));
      if ((UnityEngine.Object) this.AbilityButton != (UnityEngine.Object) null)
        this.AbilityButton.SetActive(false);
      if ((UnityEngine.Object) this.GridEventButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.GridEventButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Gimmick, (object) 0);
        }));
      if ((UnityEngine.Object) this.OKButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.OKButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnYesNoSelect == null)
            return;
          this.OnYesNoSelect(true);
        }));
      if ((UnityEngine.Object) this.CancelButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.CancelButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnYesNoSelect == null)
            return;
          this.OnYesNoSelect(false);
        }));
      if ((UnityEngine.Object) this.ExitMapButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.ExitMapButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnMapExitSelect == null)
            return;
          this.OnMapExitSelect();
        }));
      if ((UnityEngine.Object) this.UnitChgButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.UnitChgButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnUnitChgSelect == null)
            return;
          this.OnUnitChgSelect();
        }));
      this.EnableUnitChgButton(false, false);
    }

    public void SetAbilities(AbilityData[] abilities, Unit unit)
    {
      for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mAbilityButtons[index]);
      this.mAbilityButtons.Clear();
      Transform parent = this.AbilityButton.transform.parent;
      for (int index1 = 0; index1 < abilities.Length; ++index1)
      {
        if (abilities[index1].AbilityType != EAbilityType.Passive)
        {
          bool flag = false;
          int mp = (int) unit.CurrentStatus.param.mp;
          AbilityData ability = abilities[index1];
          List<SkillData> skills = ability.Skills;
          GameObject go = UnityEngine.Object.Instantiate<GameObject>(this.AbilityButton);
          go.transform.SetParent(parent, false);
          DataSource.Bind<AbilityData>(go, ability);
          go.SetActive(true);
          for (int index2 = 0; index2 < skills.Count; ++index2)
          {
            SkillData skill = skills[index2];
            int skillUseCount = (int) unit.GetSkillUseCount(skill);
            if (unit.GetSkillUsedCost(skill) <= mp && skillUseCount > 0)
            {
              flag = true;
              break;
            }
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityImageBG))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.transform, this.AbilityImageBG);
            if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
              childRecursively.GetComponent<Image>().color = this.AbilityDisableColor;
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityImageIcon))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.transform, this.AbilityImageIcon);
            if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
              childRecursively.GetComponent<RawImage_Transparent>().color = this.AbilityDisableColor;
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityName))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.transform, this.AbilityName);
            if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
              childRecursively.GetComponent<LText>().color = this.AbilityDisableColor;
          }
          this.SetButtonEvent(go, (UnitCommands.ClickEvent) (() => this.OnSelectAbility(ability)));
          this.mAbilityButtons.Add(go);
        }
      }
      this.SortButtons();
    }

    private void OnSelectAbility(AbilityData ability)
    {
      if (this.OnCommandSelect == null)
        return;
      if (MonoSingleton<GameManager>.Instance.IsTutorial() && (UnityEngine.Object) SGHighlightObject.Instance() != (UnityEngine.Object) null)
      {
        string nextTutorialStep = MonoSingleton<GameManager>.Instance.GetNextTutorialStep();
        if (nextTutorialStep == "ShowAbilityCommand" && ability.AbilityID != "AB_SEI_LOWER" || nextTutorialStep == "ShowMACommand" && ability.AbilityID != "AB_SEI_SHINING_CROSS_TUTORIAL")
          return;
      }
      this.OnCommandSelect(UnitCommands.CommandTypes.Ability, (object) ability);
    }

    private void SortButtons()
    {
      int num = 0;
      if ((UnityEngine.Object) this.AbilityButton != (UnityEngine.Object) null)
        num = this.AbilityButton.transform.GetSiblingIndex();
      for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        this.mAbilityButtons[index].transform.SetSiblingIndex(num + index + 1);
    }

    public bool IsEnableUnitChgButton
    {
      get
      {
        return this.mIsEnableUnitChgButton;
      }
    }

    public bool IsActiveUnitChgButton
    {
      get
      {
        return this.mIsActiveUnitChgButton;
      }
    }

    public void EnableUnitChgButton(bool is_enable, bool is_active = false)
    {
      this.mIsEnableUnitChgButton = is_enable;
      this.mIsActiveUnitChgButton = is_active;
      if (!((UnityEngine.Object) this.UnitChgButton != (UnityEngine.Object) null))
        return;
      this.UnitChgButton.SetActive(is_enable);
      if (!is_enable)
        return;
      Selectable[] componentsInChildren = this.UnitChgButton.GetComponentsInChildren<Selectable>(true);
      if (componentsInChildren == null)
        return;
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        componentsInChildren[index].interactable = is_active;
    }

    public enum CommandTypes
    {
      None,
      Move,
      Attack,
      Ability,
      Item,
      Gimmick,
      Map,
      End,
    }

    [Flags]
    public enum ButtonTypes
    {
      Move = 1,
      Action = 2,
      GridEvent = 4,
      Misc = 8,
      Attack = 16, // 0x00000010
      Skill = 32, // 0x00000020
      Item = 64, // 0x00000040
      IsRenkei = 128, // 0x00000080
      Map = 256, // 0x00000100
    }

    private delegate void ClickEvent();

    public delegate void CommandEvent(UnitCommands.CommandTypes command, object data);

    public delegate void YesNoEvent(bool yes);

    public delegate void MapExitEvent();

    public delegate void UnitChgEvent();
  }
}
