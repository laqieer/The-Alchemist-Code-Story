// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1010, "レイド挑戦回数を全回復", FlowNode.PinTypes.Output, 1010)]
  public class RaidPartyWindow : PartyWindow2
  {
    private const int PIN_OUTPUT_RAIDBOSS_BP_RECOVER = 1010;
    private GameObject mConfirmBox;

    protected override bool CheckMember(int numMainUnits)
    {
      if (numMainUnits <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
      if (this.mCurrentParty.Units[0] == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEADERNOTSET"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
      string empty = string.Empty;
      if (this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mCurrentParty.Units, ref empty))
        return true;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
      return false;
    }

    protected override int AvailableMainMemberSlots
    {
      get
      {
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Raid)
          return 5;
        return base.AvailableMainMemberSlots;
      }
    }

    protected override void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
    {
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1) && (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower || allUnits[index].Lv >= this.mCurrentQuest.EntryCondition.ulvmin))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }

    protected override void PostForwardPressed()
    {
      if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null)
        DebugUtility.LogError("RaidManager is NULL : RaidPartyWindow.PostForwardPressed");
      else if (!this.mCurrentQuest.IsRaid)
      {
        DebugUtility.LogError("target quest is not raid : RaidPartyWindow.PostForwardPressed");
      }
      else
      {
        RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(RaidManager.Instance.RaidPeriodId);
        if (raidPeriod == null)
          DebugUtility.LogError("not found period param : RaidPartyWindow.PostForwardPressed");
        else if (RaidManager.Instance.RaidBp <= 0 && (UnityEngine.Object) this.mConfirmBox == (UnityEngine.Object) null && RaidManager.Instance.SelectedRaidOwnerType != RaidManager.RaidOwnerType.Rescue_Temp)
        {
          if (raidPeriod.BpByCoin * raidPeriod.MaxBp <= MonoSingleton<GameManager>.Instance.Player.PaidCoin + (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin))
            this.mConfirmBox = UIUtility.ConfirmBox(LocalizedText.Get("sys.RAIDBOSS_BP_BUY", (object) (raidPeriod.BpByCoin * raidPeriod.MaxBp), (object) MonoSingleton<GameManager>.Instance.Player.PaidCoin, (object) (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin)), new UIUtility.DialogResultEvent(this.OnBuy), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
          else
            UIUtility.SystemMessage(LocalizedText.Get("sys.OUTOFCOIN"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
          base.PostForwardPressed();
      }
    }

    private void OnBuy(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }
  }
}
