// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArenaPartyWindow : PartyWindow2
  {
    [SerializeField]
    private GameObject mMapInfo;
    [SerializeField]
    private GameObject mRestTimeRoot;
    [SerializeField]
    private Text mRestTimeText;
    [SerializeField]
    private Text mPartyDescText;

    protected override void Init()
    {
      base.Init();
      DataSource.Bind<QuestParam>(this.mMapInfo, MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID));
      GameParameter.UpdateAll(this.mMapInfo);
      bool is_display = false;
      bool is_need_refresh = false;
      string end_at_text = string.Empty;
      ArenaWindow.GetMapInfoViewData(out is_display, out end_at_text, out is_need_refresh);
      GameUtility.SetGameObjectActive(this.mRestTimeRoot, is_display);
      if (Object.op_Implicit((Object) this.mRestTimeText) && this.mRestTimeText.text != end_at_text)
        this.mRestTimeText.text = end_at_text;
      int num = 0;
      switch (this.mCurrentPartyType)
      {
        case PartyWindow2.EditPartyTypes.Arena:
          num = 0;
          break;
        case PartyWindow2.EditPartyTypes.ArenaDef:
          num = 1;
          break;
      }
      for (int index = 0; index < this.TeamTabs.Length; ++index)
      {
        bool flag = num == index;
        GameUtility.SetToggle(this.TeamTabs[index], flag);
      }
      this.Refresh_DescText(this.mCurrentPartyType);
      this.Refresh_ToggleInteractable();
    }

    protected override void OnDestroy()
    {
      if (Object.op_Inequality((Object) ArenaWindow.Instance, (Object) null))
        ArenaWindow.Instance.SyncArenaGlobalVars();
      base.OnDestroy();
    }

    protected override void ChangeEnabledTeamTabs(int index)
    {
    }

    public void OnTeamTabChange_Arena()
    {
      switch ((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("tab_index"))
      {
        case 0:
          this.mCurrentPartyType = PartyWindow2.EditPartyTypes.Arena;
          break;
        case 1:
          this.mCurrentPartyType = PartyWindow2.EditPartyTypes.ArenaDef;
          break;
      }
      eOverWritePartyType overWritePartyType = UnitOverWriteUtility.EditPartyType2OverWritePartyType(this.mCurrentPartyType);
      GlobalVars.OverWritePartyType.Set(overWritePartyType);
      this.LoadTeam(this.mCurrentPartyType);
      this.TeamChangeImpl(this.mTeams[0]);
      this.Refresh_DescText(this.mCurrentPartyType);
      this.Refresh_ToggleInteractable();
    }

    private void Refresh_ToggleInteractable()
    {
      for (int index = 0; index < this.TeamTabs.Length; ++index)
        ((Selectable) this.TeamTabs[index]).interactable = !this.TeamTabs[index].isOn;
    }

    private void Refresh_DescText(PartyWindow2.EditPartyTypes party_type)
    {
      string key = string.Empty;
      switch (party_type)
      {
        case PartyWindow2.EditPartyTypes.Arena:
          key = "sys.ARENA_TAB_ATTACE_TEAM_TITLE";
          break;
        case PartyWindow2.EditPartyTypes.ArenaDef:
          key = "sys.ARENA_TAB_DEFENSE_TEAM_TITLE";
          break;
      }
      if (!Object.op_Inequality((Object) this.mPartyDescText, (Object) null) || string.IsNullOrEmpty(key))
        return;
      this.mPartyDescText.text = LocalizedText.Get(key);
    }

    protected override void UnitList_OnSelect_RefreshOverWrite(UnitData selected_unit)
    {
      if (!UnitOverWriteUtility.IsNeedOverWrite(this.mCurrentPartyType))
        return;
      this.SaveParty((PartyWindow2.Callback) (() =>
      {
        this.Refresh(true);
        this.LockWindow(false);
        this.mIsSaving = false;
        this.DuplicateNotify(selected_unit);
      }), (PartyWindow2.Callback) (() => UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ILLEGAL_PARTY"), (UIUtility.DialogResultEvent) null)));
    }

    private void DuplicateNotify(UnitData unit)
    {
      if (unit == null)
        return;
      bool flag1 = false;
      foreach (long artifact in unit.CurrentJob.Artifacts)
      {
        long equipedUnitId = UnitOverWriteUtility.GetEquipedUnitId(MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(artifact), (eOverWritePartyType) GlobalVars.OverWritePartyType);
        if (equipedUnitId >= 0L && equipedUnitId != unit.UniqueID)
        {
          flag1 = true;
          break;
        }
      }
      bool flag2 = false;
      if (unit.ConceptCards != null)
      {
        for (int index = 0; index < unit.ConceptCards.Length; ++index)
        {
          if (unit.ConceptCards[index] != null)
          {
            long equipedUnitId = UnitOverWriteUtility.GetEquipedUnitId(unit.ConceptCards[index], (eOverWritePartyType) GlobalVars.OverWritePartyType);
            if (equipedUnitId >= 0L && equipedUnitId != unit.UniqueID)
            {
              flag2 = true;
              break;
            }
          }
        }
      }
      string empty = string.Empty;
      if (flag1 && flag2)
        empty = LocalizedText.Get("sys.OVER_WRITE_PARTY_SET_UNIT_NOTIFY1");
      else if (flag1)
        empty = LocalizedText.Get("sys.OVER_WRITE_PARTY_SET_UNIT_NOTIFY2");
      else if (flag2)
        empty = LocalizedText.Get("sys.OVER_WRITE_PARTY_SET_UNIT_NOTIFY3");
      if (string.IsNullOrEmpty(empty))
        return;
      UIUtility.SystemMessage(empty, (UIUtility.DialogResultEvent) null);
    }
  }
}
