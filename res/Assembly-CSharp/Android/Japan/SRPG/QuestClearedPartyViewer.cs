// Decompiled with JetBrains decompiler
// Type: SRPG.QuestClearedPartyViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestClearedPartyViewer : MonoBehaviour
  {
    private List<PartySlotData> mSlotData = new List<PartySlotData>();
    [SerializeField]
    private GenericSlot UnitSlotTemplate;
    [SerializeField]
    private GenericSlot NpcSlotTemplate;
    [SerializeField]
    private Transform MainMemberHolder;
    [SerializeField]
    private Transform SubMemberHolder;
    [SerializeField]
    private GameObject[] ConditionItems;
    [SerializeField]
    private GameObject[] ConditionStars;
    [SerializeField]
    private GenericSlot[] ItemSlots;
    [SerializeField]
    private Text TotalAtk;
    [SerializeField]
    private GenericSlot LeaderSkill;
    [SerializeField]
    private GenericSlot SupportSkill;
    [SerializeField]
    private GameObject[] EnableUploadObjects;
    private QuestParam mCurrentQuest;
    private UnitData[] mCurrentParty;
    private SupportData mCurrentSupport;
    private List<UnitData> mGuestUnits;
    private GenericSlot[] UnitSlots;
    private GenericSlot[] SubUnitSlots;
    private GenericSlot GuestUnitSlot;
    private GenericSlot FriendSlot;
    private bool mIsHeloOnly;
    private UnitData[] mUserSelectionParty;
    private int[] mUserSelectionAchievement;
    private ItemData[] mUsedItems;
    private bool mIsUserOwnUnits;

    private void Start()
    {
      this.mIsUserOwnUnits = GlobalVars.UserSelectionPartyDataInfo == null;
      if (this.mIsUserOwnUnits)
      {
        this.mCurrentSupport = MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID));
        if (this.mCurrentSupport == null)
          this.mCurrentSupport = GlobalVars.SelectedSupport.Get();
      }
      else
      {
        this.mUserSelectionParty = GlobalVars.UserSelectionPartyDataInfo.unitData;
        this.mCurrentSupport = GlobalVars.UserSelectionPartyDataInfo.supportData;
        this.mUserSelectionAchievement = GlobalVars.UserSelectionPartyDataInfo.achievements;
        this.mUsedItems = GlobalVars.UserSelectionPartyDataInfo.usedItems;
      }
      foreach (GameObject enableUploadObject in this.EnableUploadObjects)
        enableUploadObject.SetActive(this.mIsUserOwnUnits);
      GameUtility.SetGameObjectActive((Component) this.UnitSlotTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.NpcSlotTemplate, false);
      this.LoadParty();
      this.CreateSlots();
      this.UpdateLeaderSkills();
      this.CalcTotalAttack();
      this.LoadInventory();
      this.LoadConditions();
    }

    private void OnDestroy()
    {
      GlobalVars.UserSelectionPartyDataInfo = (GlobalVars.UserSelectionPartyData) null;
    }

    private void LoadInventory()
    {
      if (this.mIsUserOwnUnits)
      {
        int index = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in SceneBattle.Instance.Battle.GetQuestRecord().used_items)
        {
          if (index >= this.ItemSlots.Length)
            break;
          string key = (string) usedItem.Key;
          int num = (int) usedItem.Value;
          ItemData data = new ItemData();
          data.Setup(0L, key, num);
          this.ItemSlots[index].SetSlotData<ItemData>(data);
          ++index;
        }
      }
      else
      {
        if (this.mUsedItems == null)
          return;
        for (int index = 0; index < this.ItemSlots.Length && index < this.mUsedItems.Length; ++index)
          this.ItemSlots[index].SetSlotData<ItemData>(this.mUsedItems[index]);
      }
    }

    private void LoadConditions()
    {
      if (this.ConditionItems == null || this.ConditionStars == null)
        return;
      if (this.mIsUserOwnUnits)
        this.LoadConditionStarsFromBattle();
      else
        this.LoadConditionStarsFromUserSelection();
      if (this.mCurrentQuest.type != QuestTypes.Tower || this.ConditionItems.Length < 3)
        return;
      this.ConditionItems[2].SetActive(false);
    }

    private void LoadConditionStarsFromBattle()
    {
      SceneBattle instance = SceneBattle.Instance;
      for (int index1 = 0; index1 < this.ConditionStars.Length; ++index1)
      {
        bool flag = false;
        switch (index1)
        {
          case 0:
            flag = !instance.Battle.PlayByManually;
            break;
          case 1:
            flag = instance.Battle.IsAllAlive();
            break;
          case 2:
            BattleCore.Record questRecord = instance.Battle.GetQuestRecord();
            flag = true;
            for (int index2 = 0; index2 < questRecord.bonusCount; ++index2)
            {
              if ((questRecord.allBonusFlags & 1 << index2) == 0)
              {
                flag = false;
                break;
              }
            }
            break;
        }
        this.ConditionStars[index1].SetActive(flag);
      }
    }

    private void LoadConditionStarsFromUserSelection()
    {
      for (int index = 0; index < this.ConditionStars.Length && index < this.mUserSelectionAchievement.Length; ++index)
      {
        bool flag = this.mUserSelectionAchievement[index] != 0;
        this.ConditionStars[index].SetActive(flag);
      }
    }

    private void LoadParty()
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mIsHeloOnly = PartyUtility.IsSoloStoryParty(this.mCurrentQuest);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyWindow2.EditPartyTypes type = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
      if (type == PartyWindow2.EditPartyTypes.Auto)
        type = PartyWindow2.EditPartyTypes.Normal;
      PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type.ToPlayerPartyType());
      this.mCurrentParty = new UnitData[partyOfType.MAX_UNIT];
      if (this.mIsUserOwnUnits)
      {
        for (int index = 0; index < partyOfType.MAX_UNIT; ++index)
        {
          long unitUniqueId = partyOfType.GetUnitUniqueID(index);
          if (unitUniqueId > 0L)
            this.mCurrentParty[index] = player.FindUnitDataByUniqueID(unitUniqueId);
        }
      }
      else
      {
        for (int index = 0; index < partyOfType.MAX_UNIT && index < this.mUserSelectionParty.Length; ++index)
          this.mCurrentParty[index] = this.mUserSelectionParty[index];
      }
    }

    private GenericSlot CreateSlotObject(PartySlotData slotData, GenericSlot template, Transform parent)
    {
      GenericSlot component = UnityEngine.Object.Instantiate<GameObject>(template.gameObject).GetComponent<GenericSlot>();
      component.transform.SetParent(parent, false);
      component.gameObject.SetActive(true);
      component.SetSlotData<PartySlotData>(slotData);
      return component;
    }

    private void DestroyPartySlotObjects()
    {
      if (this.UnitSlots != null)
        GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
      GameUtility.DestroyGameObject((Component) this.FriendSlot);
      this.mSlotData.Clear();
    }

    private void CreateSlots()
    {
      this.DestroyPartySlotObjects();
      List<PartySlotData> mainSlotData = new List<PartySlotData>();
      List<PartySlotData> subSlotData = new List<PartySlotData>();
      PartySlotData supportSlotData = (PartySlotData) null;
      PartyUtility.CreatePartySlotData(this.mCurrentQuest, out mainSlotData, out subSlotData, out supportSlotData);
      List<GenericSlot> genericSlotList = new List<GenericSlot>();
      if ((UnityEngine.Object) this.MainMemberHolder != (UnityEngine.Object) null && mainSlotData.Count > 0)
      {
        foreach (PartySlotData slotData in mainSlotData)
        {
          GenericSlot genericSlot = slotData.Type == PartySlotType.Npc || slotData.Type == PartySlotType.NpcHero ? this.CreateSlotObject(slotData, this.NpcSlotTemplate, this.MainMemberHolder) : this.CreateSlotObject(slotData, this.UnitSlotTemplate, this.MainMemberHolder);
          genericSlotList.Add(genericSlot);
        }
        if (supportSlotData != null)
          this.FriendSlot = this.CreateSlotObject(supportSlotData, this.UnitSlotTemplate, this.MainMemberHolder);
      }
      if ((UnityEngine.Object) this.SubMemberHolder != (UnityEngine.Object) null && subSlotData.Count > 0)
      {
        foreach (PartySlotData slotData in subSlotData)
        {
          GenericSlot genericSlot = slotData.Type == PartySlotType.Npc || slotData.Type == PartySlotType.NpcHero ? this.CreateSlotObject(slotData, this.NpcSlotTemplate, this.SubMemberHolder) : this.CreateSlotObject(slotData, this.UnitSlotTemplate, this.SubMemberHolder);
          genericSlotList.Add(genericSlot);
        }
      }
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) mainSlotData);
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) subSlotData);
      this.UnitSlots = genericSlotList.ToArray();
      if ((UnityEngine.Object) this.FriendSlot != (UnityEngine.Object) null && this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tower)
      {
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
        if (towerFloor != null)
        {
          this.FriendSlot.gameObject.SetActive(towerFloor.can_help);
          this.SupportSkill.gameObject.SetActive(towerFloor.can_help);
        }
      }
      this.mGuestUnits = new List<UnitData>();
      PartyUtility.MergePartySlotWithPartyUnits(this.mCurrentQuest, this.mSlotData, this.mCurrentParty, this.mGuestUnits, this.mIsUserOwnUnits);
      this.ReflectionUnitSlot();
    }

    private void ReflectionUnitSlot()
    {
      int index1 = 0;
      for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.mCurrentParty.Length && index2 < this.mSlotData.Count; ++index2)
      {
        if ((UnityEngine.Object) this.UnitSlots[index2] != (UnityEngine.Object) null)
        {
          this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
          PartySlotData partySlotData = this.mSlotData[index2];
          if (partySlotData.Type == PartySlotType.ForcedHero)
          {
            if (this.mGuestUnits != null && index1 < this.mGuestUnits.Count)
              this.UnitSlots[index2].SetSlotData<UnitData>(this.mGuestUnits[index1]);
            ++index1;
          }
          else if (partySlotData.Type == PartySlotType.Npc || partySlotData.Type == PartySlotType.NpcHero)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
            this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
          }
          else
            this.UnitSlots[index2].SetSlotData<UnitData>(this.mCurrentParty[index2]);
        }
      }
      if (!((UnityEngine.Object) this.FriendSlot != (UnityEngine.Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
      if (this.mIsUserOwnUnits)
      {
        this.mCurrentSupport = MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID)) ?? GlobalVars.SelectedSupport.Get();
        this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport != null ? this.mCurrentSupport.Unit : (UnitData) null);
      }
      else
      {
        if (this.mCurrentSupport == null)
          return;
        this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport.Unit);
      }
    }

    private void CalcTotalAttack()
    {
      int num = 0;
      for (int index = 0; index < this.mCurrentParty.Length; ++index)
      {
        UnitData unitData = this.mCurrentParty[index];
        if (unitData != null)
          num = num + (int) unitData.Status.param.atk + (int) unitData.Status.param.mag;
      }
      if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null)
        num = num + (int) this.mCurrentSupport.Unit.Status.param.atk + (int) this.mCurrentSupport.Unit.Status.param.mag;
      if (this.mGuestUnits != null)
      {
        foreach (UnitData mGuestUnit in this.mGuestUnits)
        {
          num += (int) mGuestUnit.Status.param.atk;
          num += (int) mGuestUnit.Status.param.mag;
        }
      }
      if (!((UnityEngine.Object) this.TotalAtk != (UnityEngine.Object) null))
        return;
      this.TotalAtk.text = num.ToString();
    }

    private void UpdateLeaderSkills()
    {
      if ((UnityEngine.Object) this.LeaderSkill != (UnityEngine.Object) null)
      {
        SkillParam data = (SkillParam) null;
        if (this.mIsHeloOnly)
        {
          if (this.mGuestUnits != null && this.mGuestUnits.Count > 0 && this.mGuestUnits[0].LeaderSkill != null)
            data = this.mGuestUnits[0].LeaderSkill.SkillParam;
        }
        else if (this.mCurrentParty[0] != null)
        {
          if (this.mCurrentParty[0].LeaderSkill != null)
            data = this.mCurrentParty[0].LeaderSkill.SkillParam;
        }
        else if (this.mSlotData[0].Type == PartySlotType.Npc || this.mSlotData[0].Type == PartySlotType.NpcHero)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mSlotData[0].UnitName);
          if (unitParam != null && unitParam.leader_skills != null && unitParam.leader_skills.Length >= 4)
          {
            string leaderSkill = unitParam.leader_skills[4];
            if (!string.IsNullOrEmpty(leaderSkill))
              data = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(leaderSkill);
          }
        }
        else if (this.mGuestUnits != null && this.mGuestUnits.Count > 0 && this.mGuestUnits[0].LeaderSkill != null)
          data = this.mGuestUnits[0].LeaderSkill.SkillParam;
        this.LeaderSkill.SetSlotData<SkillParam>(data);
      }
      if (!((UnityEngine.Object) this.SupportSkill != (UnityEngine.Object) null))
        return;
      SupportData supportData = !this.mIsUserOwnUnits ? this.mCurrentSupport : MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID)) ?? GlobalVars.SelectedSupport.Get();
      SkillParam data1 = (SkillParam) null;
      if (supportData != null && supportData.Unit.LeaderSkill != null && supportData.IsFriend())
        data1 = supportData.Unit.LeaderSkill.SkillParam;
      this.SupportSkill.SetSlotData<SkillParam>(data1);
    }
  }
}
