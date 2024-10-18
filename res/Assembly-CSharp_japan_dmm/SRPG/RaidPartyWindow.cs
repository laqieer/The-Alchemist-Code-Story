// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1010, "レイド挑戦回数を全回復", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1000, "ラストアッタクパーティに固定", FlowNode.PinTypes.Input, 1000)]
  public class RaidPartyWindow : PartyWindow2
  {
    private const int PIN_INPUT_GUILDRAID_FIXPARTY = 1000;
    private const int PIN_OUTPUT_RAIDBOSS_BP_RECOVER = 1010;
    private GameObject mConfirmBox;

    protected override void OverrideLoadTeam()
    {
      if (!GuildRaidManager.Instance.IsForcedDeck)
      {
        DebugUtility.LogWarning("< color = yellow > GuildRaidのForcedDeckの指定がないのに設定を行おうとしている</ color > ");
      }
      else
      {
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
          throw new InvalidPartyTypeException();
        this.mGuestUnit.Clear();
        this.mMaxTeamCount = this.mCurrentPartyType.GetMaxTeamCount();
        PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
        this.mTeams.Clear();
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        if (GuildRaidManager.Instance.ForcedDeck.Count > this.mSlotData.Count)
          DebugUtility.LogWarning("< color = yellow > ForcedDeckがスロットより多い</ color > ");
        PartyEditData partyEditData = new PartyEditData(string.Empty, partyOfType);
        partyEditData.SetUnitsForce(GuildRaidManager.Instance.ForcedDeck.ToArray());
        this.mTeams.Add(partyEditData);
        this.SetCurrentParty(0);
        this.Refresh(true);
      }
    }

    protected override bool CheckMember(int numMainUnits)
    {
      if (numMainUnits <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => { }));
        return false;
      }
      if (this.CurrentParty.Units[0] == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEADERNOTSET"), (UIUtility.DialogResultEvent) (dialog => { }));
        return false;
      }
      string empty = string.Empty;
      if (this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.CurrentParty.Units, ref empty))
        return true;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => { }));
      return false;
    }

    protected override int AvailableMainMemberSlots
    {
      get
      {
        return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Raid || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid ? 5 : base.AvailableMainMemberSlots;
      }
    }

    protected override void RegistPartyMember(
      List<UnitData> allUnits,
      bool heroesAvailable,
      bool selectedSlotIsEmpty,
      int numMainMembers)
    {
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || allUnits[index] != this.CurrentParty.Units[0] || !selectedSlotIsEmpty || numMainMembers > 1) && (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower || allUnits[index].Lv >= this.mCurrentQuest.EntryCondition.ulvmin))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }

    protected override void PostForwardPressed()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Raid)
      {
        if (Object.op_Equality((Object) RaidManager.Instance, (Object) null))
        {
          DebugUtility.LogError("RaidManager is NULL : RaidPartyWindow.PostForwardPressed");
          return;
        }
        if (!this.mCurrentQuest.IsRaid && !this.mCurrentQuest.IsGuildRaid)
        {
          DebugUtility.LogError("target quest is not raid : RaidPartyWindow.PostForwardPressed");
          return;
        }
        RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(RaidManager.Instance.RaidPeriodId);
        if (raidPeriod == null)
        {
          DebugUtility.LogError("not found period param : RaidPartyWindow.PostForwardPressed");
          return;
        }
        if (RaidManager.Instance.RaidBp <= 0 && Object.op_Equality((Object) this.mConfirmBox, (Object) null) && RaidManager.Instance.SelectedRaidOwnerType != RaidManager.RaidOwnerType.Rescue_Temp)
        {
          if (raidPeriod.BpByCoin * raidPeriod.MaxBp <= MonoSingleton<GameManager>.Instance.Player.PaidCoin + (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin))
          {
            this.mConfirmBox = UIUtility.ConfirmBox(LocalizedText.Get("sys.RAIDBOSS_BP_BUY", (object) (raidPeriod.BpByCoin * raidPeriod.MaxBp), (object) MonoSingleton<GameManager>.Instance.Player.PaidCoin, (object) (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin)), new UIUtility.DialogResultEvent(this.OnBuy), (UIUtility.DialogResultEvent) null);
            return;
          }
          UIUtility.SystemMessage(LocalizedText.Get("sys.OUTOFCOIN"), (UIUtility.DialogResultEvent) null);
          return;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid)
      {
        if (Object.op_Equality((Object) GuildRaidManager.Instance, (Object) null))
        {
          DebugUtility.LogError("RaidManager is NULL : RaidPartyWindow.PostForwardPressed");
          return;
        }
        if (!this.mCurrentQuest.IsRaid && !this.mCurrentQuest.IsGuildRaid)
        {
          DebugUtility.LogError("target quest is not raid : RaidPartyWindow.PostForwardPressed");
          return;
        }
        if (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildRaidManager.Instance.PeriodId) == null)
        {
          DebugUtility.LogError("not found period param : RaidPartyWindow.PostForwardPressed");
          return;
        }
        if (GuildRaidManager.Instance.CurrentBp <= 0 && GuildRaidManager.Instance.BattleType == GuildRaidBattleType.Main && Object.op_Equality((Object) this.mConfirmBox, (Object) null) && !GuildRaidManager.Instance.IsForcedDeck)
        {
          if (GuildRaidManager.Instance.ChallengedBp == GuildRaidManager.Instance.MaxBp)
          {
            UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDRAID_BP_EMPTY"), (UIUtility.DialogResultEvent) null);
            return;
          }
          UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDRAID_CHALLENGEBP_EMPTY"), (UIUtility.DialogResultEvent) null);
          return;
        }
        if (GuildRaidManager.Instance.BattleType == GuildRaidBattleType.Main && MonoSingleton<GameManager>.Instance.IsGuildRaidBossHpWarning(GuildRaidManager.Instance.CurrentBossInfo.BossId, GuildRaidManager.Instance.CurrentBossInfo.CurrentHP))
        {
          UIUtility.ConfirmBox(LocalizedText.Get("sys.GUILDRAID_HP_MESSAGE"), new UIUtility.DialogResultEvent(this.OnBattleGo), (UIUtility.DialogResultEvent) null);
          return;
        }
      }
      else
      {
        DebugUtility.LogError("Miss Match mCurrentPartyType : RaidPartyWindow.PostForwardPressed");
        return;
      }
      base.PostForwardPressed();
    }

    private void OnBuy(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void OnBattleGo(GameObject go) => base.PostForwardPressed();

    public override void Activated(int pinID)
    {
      if (pinID == 1000)
      {
        if (Object.op_Inequality((Object) GuildRaidManager.Instance, (Object) null) && GuildRaidManager.Instance.BattleType == GuildRaidBattleType.Main && GuildRaidManager.Instance.IsForcedDeck)
          this.mIsLockCurrentParty = true;
        else
          this.mIsLockCurrentParty = false;
        this.Refresh();
      }
      else
        base.Activated(pinID);
    }
  }
}
