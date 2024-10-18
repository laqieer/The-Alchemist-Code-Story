// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(2001, "キャッシュを削除", FlowNode.PinTypes.Input, 2001)]
  [FlowNode.Pin(2010, "GvGチーム名変更完了", FlowNode.PinTypes.Input, 2010)]
  public class GvGPartyWindow : PartyWindow2
  {
    private const int GVG_DELETE_CACHE = 2001;
    private const int GVG_PRESET_NAMECHANGE = 2010;
    [SerializeField]
    private Text NodeName;
    [SerializeField]
    private Text GoText;
    [SerializeField]
    private string HPBarObjectName = "gauge_hp";
    [SerializeField]
    private string DeadObjectName = "dead";
    [SerializeField]
    private bool SetPartyUnitsClear = true;
    private List<UnitData> Units;
    [SerializeField]
    private FixedScrollablePulldown GvGTeamPulldown;
    [SerializeField]
    private Text GvGTeamPulldownText;
    [SerializeField]
    private List<Button> ConsecutiveWinsButton;
    [SerializeField]
    private Slider StaminaSlider;
    [SerializeField]
    private ImageArray StaminaImageArray;
    [SerializeField]
    private GameObject UnitLockObject;
    private GvGNodeData CurrentNode;

    private void Awake()
    {
      this.mIsLockCurrentParty = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NodeName, (UnityEngine.Object) null))
      {
        this.CurrentNode = GvGManager.Instance.NodeDataList.Find((Predicate<GvGNodeData>) (n => n.NodeId == GvGManager.Instance.SelectNodeId));
        if (this.CurrentNode != null)
          this.NodeName.text = this.CurrentNode.NodeParam.Name;
      }
      this.ReloadPartyUnits();
    }

    public override void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2001:
          this.ReloadPartyUnits();
          return;
        case 2010:
          if (!string.IsNullOrEmpty(GlobalVars.TeamName))
          {
            List<PartyEditData> teams = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, out int _);
            if (this.mSelectedRenameTeamIndex >= 0)
            {
              if (teams.Count > this.mSelectedRenameTeamIndex)
              {
                teams[this.mSelectedRenameTeamIndex].Name = GlobalVars.TeamName;
                this.mSelectedRenameTeamIndex = -1;
              }
            }
            else
              this.CurrentParty.Name = GlobalVars.TeamName;
            PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, teams, this.ContainsNotFree(), this.mSlotData);
            this.ResetTeamPulldown(teams, this.mCurrentPartyType.GetMaxTeamCount(), this.mCurrentQuest, this.GvGTeamPulldown);
            break;
          }
          break;
      }
      base.Activated(pinID);
    }

    private void ReloadPartyUnits()
    {
      this.Units = new List<UnitData>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null) || GvGBattleTop.Instance.SelfParty == null)
        return;
      GvGParty selfParty = GvGBattleTop.Instance.SelfParty;
      if (selfParty == null)
      {
        DebugUtility.LogError("< color = red > GvGのパーティ情報がない</ color > ");
      }
      else
      {
        selfParty.Units.ForEach((Action<GvGPartyUnit>) (u => this.Units.Add((UnitData) u)));
        bool flag = GvGBattleTop.Instance.SelfParty.WinNum > 0;
        this.GvGTeamPulldown.interactable = !flag;
        if (this.ConsecutiveWinsButton == null)
          return;
        for (int index = 0; index < this.ConsecutiveWinsButton.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConsecutiveWinsButton[index], (UnityEngine.Object) null))
            ((Selectable) this.ConsecutiveWinsButton[index]).interactable = !flag;
        }
      }
    }

    protected override void SetItemSlot(int slotIndex, ItemData item)
    {
    }

    protected override void SaveTeamPresets()
    {
    }

    protected override void BeforeSetPartyUnit()
    {
      if (this.CurrentParty == null || this.CurrentParty.Units == null)
        return;
      if (this.SetPartyUnitsClear)
        this.Units.Clear();
      GameUtility.SetGameObjectActive(this.UnitLockObject, false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null))
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoText, (UnityEngine.Object) null))
          this.GoText.text = LocalizedText.Get("sys.GVG_BTN_OFFENSEGO");
        if (GvGBattleTop.Instance.SelfParty != null && GvGBattleTop.Instance.SelfParty.WinNum > 0)
        {
          GameUtility.SetGameObjectActive(this.UnitLockObject, true);
          GvGManager.Instance.GetStaminaImageIndex(this.CurrentNode, this.StaminaSlider, this.StaminaImageArray, GvGBattleTop.Instance.SelfParty.WinNum);
        }
      }
      for (int i = 0; i < this.CurrentParty.Units.Length && i < this.UnitSlots.Length; ++i)
      {
        if (this.CurrentParty.Units[i] != null)
        {
          if (this.Units.Count < i + 1)
            this.Units.Add(this.CurrentParty.Units[i]);
          else
            this.Units[i] = this.CurrentParty.Units[i];
        }
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnitSlots[i], (UnityEngine.Object) null))
        {
          SerializeValueBehaviour component1 = ((Component) this.UnitSlots[i]).GetComponent<SerializeValueBehaviour>();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          {
            GameObject gameObject1 = component1.list.GetGameObject(this.HPBarObjectName);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            {
              gameObject1.SetActive(this.CurrentParty.Units[i] != null);
              GameObject gameObject2 = component1.list.GetGameObject(this.DeadObjectName);
              GameUtility.SetGameObjectActive(gameObject2, false);
              if (this.CurrentParty.Units[i] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null) && GvGBattleTop.Instance.SelfParty != null && GvGBattleTop.Instance.SelfParty.WinNum > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
              {
                GvGPartyUnit gvGpartyUnit = GvGBattleTop.Instance.SelfParty.Units.Find((Predicate<GvGPartyUnit>) (u => u.UniqueID == this.CurrentParty.Units[i].UniqueID));
                if (gvGpartyUnit != null)
                {
                  Slider component2 = gameObject1.GetComponent<Slider>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                  {
                    float num = 0.0f;
                    if (this.CurrentParty.Units[i] != null)
                    {
                      gameObject1.SetActive(true);
                      if (gvGpartyUnit.HP == -1)
                      {
                        num = 1f;
                      }
                      else
                      {
                        num = (float) gvGpartyUnit.HP / (float) (int) gvGpartyUnit.Status.param.hp;
                        GameUtility.SetGameObjectActive(gameObject2, gvGpartyUnit.HP == 0);
                      }
                    }
                    component2.value = num * 100f;
                  }
                }
              }
            }
          }
        }
      }
    }

    protected override void OnForwardOrBackButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.BackButton))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      else if (!this.CheckUsedUnitCount(false))
      {
        UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_RULE_USED_UNITCOUNT_OVER"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        long[] numArray = new long[3];
        for (int index = 0; index < this.CurrentParty.Units.Length; ++index)
        {
          if (this.CurrentParty.Units[index] != null)
            numArray[index] = this.CurrentParty.Units[index].UniqueID;
        }
        if (GvGManager.Instance.GvGPeriod != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGDefenseSettings.Instance, (UnityEngine.Object) null))
        {
          long[] all = Array.FindAll<long>(numArray, (Predicate<long>) (unit_uid => unit_uid != 0L));
          if (all == null || all.Length < GvGManager.Instance.GvGPeriod.DefenseUnitMin)
          {
            UIUtility.SystemMessage(string.Format(LocalizedText.Get("sys.GVG_DEFENSE_UNITCOUNT_NOT_ENOUGH"), (object) GvGManager.Instance.GvGPeriod.DefenseUnitMin), (UIUtility.DialogResultEvent) null);
            return;
          }
        }
        if (!Array.Exists<long>(numArray, (Predicate<long>) (unit_uid => unit_uid != 0L)))
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
        else if (this.CheckUsedUnit())
        {
          UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_USEDUNIT_MESSAGE"), (UIUtility.DialogResultEvent) null);
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null))
            GvGBattleTop.Instance.SetEditParty(numArray);
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGDefenseSettings.Instance, (UnityEngine.Object) null))
            GvGDefenseSettings.Instance.SetEditParty(numArray);
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGNodeInfo.Instance, (UnityEngine.Object) null))
            GvGNodeInfo.Instance.SetEditParty(numArray);
          this.Units.Clear();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
        }
      }
    }

    protected override void OverrideLoadTeam()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
        throw new InvalidPartyTypeException();
      this.mGuestUnit.Clear();
      this.mMaxTeamCount = this.mCurrentPartyType.GetMaxTeamCount();
      PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
      this.mTeams.Clear();
      PartyData partyOfType1 = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
      if (this.Units.Count > this.mSlotData.Count)
        DebugUtility.LogError("< color = red > ForcedDeckがスロットより多い</ color > ");
      PartyEditData partyEditData = new PartyEditData(string.Empty, partyOfType1);
      partyEditData.SetUnitsForce(this.Units.ToArray());
      this.mTeams.Add(partyEditData);
      this.SetCurrentParty(0);
      this.Refresh(true);
      this.BeforeSetPartyUnit();
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, out int _);
      if (teams == null || teams != null && teams.Count < this.mMaxTeamCount)
      {
        teams = new List<PartyEditData>();
        PartyData partyOfType2 = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        for (int index = 0; index < this.mMaxTeamCount; ++index)
        {
          teams.Add(new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(index), partyOfType2));
          PartyUtility.SaveTeamPresets(this.mCurrentPartyType, index, teams, this.ContainsNotFree(), this.mSlotData);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null) && GvGBattleTop.Instance.SelfParty != null && GvGBattleTop.Instance.SelfParty.WinNum > 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
          ((Selectable) this.NextButton).interactable = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
          ((Selectable) this.PrevButton).interactable = false;
        if (GvGBattleTop.Instance.IsConfirmParty && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
          ((Selectable) this.ForwardButton).interactable = false;
      }
      else
        this.ChangeEnabledArrowButtons(this.mCurrentTeamIndex, this.mMaxTeamCount);
      this.ResetTeamPulldown(teams, this.mCurrentPartyType.GetMaxTeamCount(), this.mCurrentQuest, this.GvGTeamPulldown);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GvGTeamPulldown, (UnityEngine.Object) null))
      {
        this.GvGTeamPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnGvGPresetTeamChange);
        this.GvGTeamPulldown.ClearSelection();
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GvGTeamPulldownText, (UnityEngine.Object) null))
        return;
      this.GvGTeamPulldownText.text = LocalizedText.Get("sys.GVG_PULLDOWN_FIRST_TEXT");
    }

    protected override void OnNextTeamChange()
    {
      this.LoadPresetTeam();
      this.mCurrentTeamIndex = this.GvGTeamPulldown.Selection;
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex + 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GvGTeamPulldown, (UnityEngine.Object) null))
        return;
      this.GvGTeamPulldown.PrevSelection = this.mCurrentTeamIndex;
      this.GvGTeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    protected override void OnPrevTeamChange()
    {
      this.LoadPresetTeam();
      this.mCurrentTeamIndex = this.GvGTeamPulldown.Selection;
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex - 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GvGTeamPulldown, (UnityEngine.Object) null))
        return;
      this.GvGTeamPulldown.PrevSelection = this.GvGTeamPulldown.Selection;
      this.GvGTeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    private void LoadPresetTeam()
    {
      if (this.mTeams != null && this.mTeams.Count >= this.mMaxTeamCount)
        return;
      this.mTeams = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, out int _);
    }

    private void OnGvGPresetTeamChange(int index)
    {
      this.LoadPresetTeam();
      this.mCurrentTeamIndex = -1;
      this.OnTeamChangeImpl(index);
    }

    protected override void PostForwardPressed()
    {
      if (!this.CheckUsedUnitCount(false))
        UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_RULE_USED_UNITCOUNT_OVER"), (UIUtility.DialogResultEvent) null);
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGDefenseSettings.Instance, (UnityEngine.Object) null))
        UIUtility.ConfirmBox("sys.GVG_DEFENSE_CONFIRM", (UIUtility.DialogResultEvent) (go => base.PostForwardPressed()), (UIUtility.DialogResultEvent) null);
      else if (this.CheckUsedUnit())
        UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_USEDUNIT_MESSAGE"), (UIUtility.DialogResultEvent) null);
      else
        base.PostForwardPressed();
    }

    protected override void OnUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (DataSource.FindDataOfClass<UnitData>(((Component) slot).gameObject, (UnitData) null) == null && !this.CheckUsedUnitCount(true))
        UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_RULE_USED_UNITCOUNT_OVER"), (UIUtility.DialogResultEvent) null);
      else
        base.OnUnitSlotClick(slot, interactable);
    }

    public override void Reopen(bool farceRefresh = false)
    {
      base.Reopen(farceRefresh);
      this.Units.Clear();
      this.ReloadPartyUnits();
      this.OverrideLoadTeam();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private bool CheckUsedUnit()
    {
      GvGBattleTop instance1 = GvGBattleTop.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance1, (UnityEngine.Object) null) && instance1.SelfParty != null && instance1.SelfParty.WinNum > 0)
        return false;
      GvGManager instance2 = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance2, (UnityEngine.Object) null) || instance2.UsedUnitList == null || this.CurrentParty == null || this.CurrentParty.Units == null)
        return false;
      for (int index = 0; index < this.CurrentParty.Units.Length; ++index)
      {
        if (this.CurrentParty.Units[index] != null && instance2.UsedUnitList.Contains(this.CurrentParty.Units[index].UniqueID))
          return true;
      }
      return false;
    }

    private bool CheckUsedUnitCount(bool is_check_slot)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) || GvGManager.Instance.CurrentRule == null)
        return false;
      int currentRuleUnitCount = GvGManager.Instance.CurrentRuleUnitCount;
      if (currentRuleUnitCount <= 0)
        return true;
      List<long> longList = new List<long>((IEnumerable<long>) GvGManager.Instance.UsedUnitList);
      int usedUnitTodayCount = GvGManager.Instance.UsedUnitTodayCount;
      for (int index = 0; index < this.CurrentParty.Units.Length; ++index)
      {
        if (this.CurrentParty.Units[index] != null && !longList.Contains(this.CurrentParty.Units[index].UniqueID))
        {
          longList.Add(this.CurrentParty.Units[index].UniqueID);
          ++usedUnitTodayCount;
        }
      }
      return !is_check_slot ? currentRuleUnitCount >= usedUnitTodayCount : currentRuleUnitCount > usedUnitTodayCount;
    }
  }
}
