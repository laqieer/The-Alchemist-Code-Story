// Decompiled with JetBrains decompiler
// Type: SRPG.GameParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/Game Parameter")]
  public class GameParameter : MonoBehaviour, IGameParameter
  {
    private const int PARAMETER_CATEGORY_SIZE = 100;
    public static List<GameParameter> Instances = new List<GameParameter>();
    public GameParameter.ParameterTypes ParameterType = GameParameter.ParameterTypes.None;
    public int InstanceType;
    public int ViewType;
    public int Index;
    public GameObject[] SerializeGameObject;
    private Slider mSlider;
    private UnityEngine.UI.Text mText;
    private InputField mInputField;
    private Animator mAnimator;
    private RawImage mImage;
    private ImageArray mImageArray;
    private Coroutine mUpdateCoroutine;
    private float mNextUpdateTime;
    private string mDefaultValue;
    private Vector2 mDefaultRangeValue;
    private Texture mDefaultImage;
    private Sprite mDefaultSprite;
    private bool mUpdate;
    private bool mIsEmptyGO;
    private bool mStarted;

    private bool IsParameterType_Rune
    {
      get
      {
        return GameParameter.ParameterTypes.RUNE_ST < this.ParameterType && this.ParameterType <= GameParameter.ParameterTypes.RUNE_ED;
      }
    }

    private bool IsParameterType_Ranking
    {
      get
      {
        return GameParameter.ParameterTypes.RANKING_ST < this.ParameterType && this.ParameterType <= GameParameter.ParameterTypes.RANKING_ED;
      }
    }

    private bool IsParameterType_CombatPower
    {
      get
      {
        return GameParameter.ParameterTypes.COMBATPOWER_ST < this.ParameterType && this.ParameterType <= GameParameter.ParameterTypes.COMBATPOWER_ED;
      }
    }

    private SupportData GetSupportData()
    {
      return DataSource.FindDataOfClass<SupportData>(((Component) this).gameObject, (SupportData) null) ?? (SupportData) GlobalVars.SelectedSupport;
    }

    private FriendData GetFriendData()
    {
      return DataSource.FindDataOfClass<FriendData>(((Component) this).gameObject, (FriendData) null);
    }

    private AbilityParam GetAbilityParam()
    {
      AbilityParam dataOfClass = DataSource.FindDataOfClass<AbilityParam>(((Component) this).gameObject, (AbilityParam) null);
      if (dataOfClass == null)
      {
        AbilityData abilityData = this.GetAbilityData();
        if (abilityData != null)
          dataOfClass = abilityData.Param;
      }
      return dataOfClass;
    }

    private AbilityData GetAbilityData()
    {
      return DataSource.FindDataOfClass<AbilityData>(((Component) this).gameObject, (AbilityData) null);
    }

    private ArenaPlayer GetArenaPlayer()
    {
      switch ((GameParameter.ArenaPlayerInstanceTypes) this.InstanceType)
      {
        case GameParameter.ArenaPlayerInstanceTypes.Enemy:
          return (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        default:
          return DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).gameObject, (ArenaPlayer) null);
      }
    }

    private ArtifactParam GetArtifactParam()
    {
      switch ((GameParameter.ArtifactInstanceTypes) this.InstanceType)
      {
        case GameParameter.ArtifactInstanceTypes.Any:
          ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
          return dataOfClass1 != null ? dataOfClass1.ArtifactParam : DataSource.FindDataOfClass<ArtifactParam>(((Component) this).gameObject, (ArtifactParam) null);
        case GameParameter.ArtifactInstanceTypes.QuestReward:
          QuestParam questParamAuto = this.GetQuestParamAuto();
          if (questParamAuto != null && 0 <= this.Index && questParamAuto.bonusObjective != null && this.Index < questParamAuto.bonusObjective.Length)
            return MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(questParamAuto.bonusObjective[this.Index].item);
          break;
        case GameParameter.ArtifactInstanceTypes.Trophy:
          ArtifactRewardData dataOfClass2 = DataSource.FindDataOfClass<ArtifactRewardData>(((Component) this).gameObject, (ArtifactRewardData) null);
          if (dataOfClass2 != null)
          {
            ArtifactParam artifactParam = dataOfClass2.ArtifactParam;
            if (artifactParam != null)
              return artifactParam;
          }
          this.ResetToDefault();
          break;
      }
      return (ArtifactParam) null;
    }

    private ArtifactData GetArtifactData()
    {
      return DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
    }

    private void SetArtifactFrame(ArtifactParam param)
    {
      if (param == null)
        return;
      Image component = ((Component) this).GetComponent<Image>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      int rareini = param.rareini;
      GameSettings instance = GameSettings.Instance;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) || rareini >= instance.ArtifactIcon_Frames.Length)
        return;
      component.sprite = instance.ArtifactIcon_Frames[rareini];
    }

    private Unit GetUnit()
    {
      switch ((GameParameter.UnitInstanceTypes) this.InstanceType)
      {
        case GameParameter.UnitInstanceTypes.CurrentTurn:
          return UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle.CurrentUnit != null ? SceneBattle.Instance.Battle.CurrentUnit : (Unit) null;
        default:
          return DataSource.FindDataOfClass<Unit>(((Component) this).gameObject, (Unit) null);
      }
    }

    private UnitParam GetUnitParam()
    {
      UnitParam dataOfClass = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
      if (dataOfClass != null)
        return dataOfClass;
      return this.GetUnitData()?.UnitParam;
    }

    private UnitData GetUnitData()
    {
      UnitData unitData = (UnitData) null;
      switch (this.InstanceType)
      {
        case 0:
          unitData = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
          if (unitData == null)
          {
            Unit unit = this.GetUnit();
            if (unit != null)
            {
              unitData = unit.UnitData;
              break;
            }
            break;
          }
          break;
        case 3:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle.CurrentUnit != null)
            return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
          break;
        case 4:
        case 5:
        case 6:
          ArenaPlayer dataOfClass1 = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).gameObject, (ArenaPlayer) null);
          if (dataOfClass1 != null)
          {
            int index = this.InstanceType - 4;
            unitData = dataOfClass1.Unit[index];
            break;
          }
          break;
        case 7:
        case 8:
        case 9:
          ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
          if (selectedArenaPlayer != null)
          {
            int index = this.InstanceType - 7;
            unitData = selectedArenaPlayer.Unit[index];
            break;
          }
          break;
        case 10:
        case 11:
        case 12:
          PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
          int index1 = this.InstanceType - 10;
          long unitUniqueId1 = player1.Partys[(int) GlobalVars.SelectedPartyIndex].GetUnitUniqueID(index1);
          unitData = player1.FindUnitDataByUniqueID(unitUniqueId1);
          break;
        case 13:
          PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
          long unitUniqueId2 = player2.Partys[7].GetUnitUniqueID(0);
          unitData = player2.FindUnitDataByUniqueID(unitUniqueId2);
          break;
        case 16:
        case 17:
        case 18:
          VersusCpuData dataOfClass2 = DataSource.FindDataOfClass<VersusCpuData>(((Component) this).gameObject, (VersusCpuData) null);
          if (dataOfClass2 != null)
          {
            int index2 = this.InstanceType - 16;
            if (dataOfClass2.Units.Length > index2)
            {
              unitData = dataOfClass2.Units[index2];
              break;
            }
            break;
          }
          break;
        case 19:
          PlayerData player3 = MonoSingleton<GameManager>.Instance.Player;
          long unitUniqueId3 = player3.Partys[10].GetUnitUniqueID(0);
          unitData = player3.FindUnitDataByUniqueID(unitUniqueId3);
          break;
        case 20:
          unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
          break;
        case 21:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
        case 27:
        case 28:
        case 29:
        case 30:
        case 31:
        case 32:
          List<UnitData> unitDataList;
          int index3;
          if (this.InstanceType < 27)
          {
            unitDataList = VersusDraftList.VersusDraftUnitDataListPlayer;
            index3 = this.InstanceType - 21;
          }
          else
          {
            unitDataList = VersusDraftList.VersusDraftUnitDataListEnemy;
            index3 = this.InstanceType - 27;
          }
          if (unitDataList != null && unitDataList.Count > index3)
          {
            unitData = unitDataList[index3];
            break;
          }
          break;
      }
      return unitData;
    }

    private JSON_MyPhotonPlayerParam.UnitDataElem GetMultiPlayerUnitData(int index)
    {
      JSON_MyPhotonPlayerParam roomPlayerParam = this.GetRoomPlayerParam();
      return roomPlayerParam == null || roomPlayerParam.units == null ? (JSON_MyPhotonPlayerParam.UnitDataElem) null : Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(roomPlayerParam.units, (Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>) (e => e.slotID == index));
    }

    private JSON_MyPhotonPlayerParam GetVersusPlayerParam(
      JSON_MyPhotonPlayerParam[] players,
      int cnt)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      JSON_MyPhotonPlayerParam versusPlayerParam = (JSON_MyPhotonPlayerParam) null;
      if (players.Length > cnt)
      {
        for (int index = 0; index < players.Length; ++index)
        {
          JSON_MyPhotonPlayerParam player = players[index];
          if (player != null && player.playerID != instance.GetMyPlayer().playerID)
          {
            versusPlayerParam = player;
            break;
          }
        }
      }
      return versusPlayerParam;
    }

    private JSON_MyPhotonPlayerParam GetVersusPlayerParam()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      JSON_MyPhotonPlayerParam versusPlayerParam = (JSON_MyPhotonPlayerParam) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
      {
        if (this.InstanceType == 0)
        {
          versusPlayerParam = JSON_MyPhotonPlayerParam.Create();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom != null)
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
            if (myPhotonRoomParam != null)
            {
              JSON_MyPhotonPlayerParam[] players = myPhotonRoomParam.players;
              if (players != null && players.Length > 1)
                versusPlayerParam = this.GetVersusPlayerParam(players, 1);
            }
            if (versusPlayerParam == null)
            {
              string roomParam = instance.GetRoomParam("started");
              if (roomParam != null)
              {
                FlowNode_StartMultiPlay.PlayerList jsonObject = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam);
                if (jsonObject != null)
                  versusPlayerParam = this.GetVersusPlayerParam(jsonObject.players, 1);
              }
            }
          }
        }
      }
      return versusPlayerParam;
    }

    private PartyData GetPartyData()
    {
      return DataSource.FindDataOfClass<PartyData>(((Component) this).gameObject, (PartyData) null);
    }

    private SkillParam GetLeaderSkill(PartyData party)
    {
      UnitData unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(party.GetUnitUniqueID(party.LeaderIndex));
      if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        unit = UnitOverWriteUtility.Apply(unit, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      return unit != null && unit.CurrentLeaderSkill != null ? unit.CurrentLeaderSkill.SkillParam : (SkillParam) null;
    }

    private ItemParam GetItemParam()
    {
      switch (this.InstanceType)
      {
        case 0:
          ItemData dataOfClass1 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
          return dataOfClass1 != null ? dataOfClass1.Param : DataSource.FindDataOfClass<ItemParam>(((Component) this).gameObject, (ItemParam) null);
        case 1:
          PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
          if (0 <= this.Index && this.Index < player1.Inventory.Length)
            return player1.Inventory[this.Index].Param;
          break;
        case 2:
          QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
          if (questParam == null && UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) ?? DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
          if (questParam != null && questParam.type == QuestTypes.Tower)
          {
            TowerRewardItem towerRewardItem = this.GetTowerRewardItem();
            if (towerRewardItem == null)
              return (ItemParam) null;
            return towerRewardItem.type != TowerRewardItem.RewardType.Item ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam(towerRewardItem.iname);
          }
          if (questParam != null && questParam.IsVersus)
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            PlayerData player2 = instance.Player;
            VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(player2.VersusTowerFloor + 1);
            if (versusTowerParam == null)
              return (ItemParam) null;
            if (versusTowerParam.ArrivalItemType != VERSUS_ITEM_TYPE.item)
              return (ItemParam) null;
            string arrivalIteminame = (string) versusTowerParam.ArrivalIteminame;
            return instance.GetItemParam(arrivalIteminame);
          }
          if (questParam != null && 0 <= this.Index && questParam.bonusObjective != null && this.Index < questParam.bonusObjective.Length)
            return MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[this.Index].item);
          break;
        case 3:
          EquipData dataOfClass2 = DataSource.FindDataOfClass<EquipData>(((Component) this).gameObject, (EquipData) null);
          if (dataOfClass2 != null)
            return dataOfClass2.ItemParam;
          break;
        case 4:
          EnhanceMaterial dataOfClass3 = DataSource.FindDataOfClass<EnhanceMaterial>(((Component) this).gameObject, (EnhanceMaterial) null);
          if (dataOfClass3 != null && dataOfClass3.item != null)
            return dataOfClass3.item.Param;
          break;
        case 5:
          EnhanceEquipData dataOfClass4 = DataSource.FindDataOfClass<EnhanceEquipData>(((Component) this).gameObject, (EnhanceEquipData) null);
          if (dataOfClass4 != null && dataOfClass4.equip != null)
            return dataOfClass4.equip.ItemParam;
          break;
        case 6:
          SellItem dataOfClass5 = DataSource.FindDataOfClass<SellItem>(((Component) this).gameObject, (SellItem) null);
          if (dataOfClass5 != null && dataOfClass5.item != null)
            return dataOfClass5.item.Param;
          break;
        case 7:
          ConsumeItemData dataOfClass6 = DataSource.FindDataOfClass<ConsumeItemData>(((Component) this).gameObject, (ConsumeItemData) null);
          if (dataOfClass6 != null)
            return dataOfClass6.param;
          break;
      }
      return (ItemParam) null;
    }

    private ItemData GetInventoryItemData()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      return 0 <= this.Index && this.Index < player.Inventory.Length ? player.Inventory[this.Index] : (ItemData) null;
    }

    private PlayerLevelUpInfo GetLevelUpInfo()
    {
      return DataSource.FindDataOfClass<PlayerLevelUpInfo>(((Component) this).gameObject, (PlayerLevelUpInfo) null);
    }

    private ItemParam GetInventoryItemParam() => this.GetInventoryItemData()?.Param;

    private SkillData GetSkillData()
    {
      return DataSource.FindDataOfClass<SkillData>(((Component) this).gameObject, (SkillData) null);
    }

    private SkillParam GetSkillParam()
    {
      return DataSource.FindDataOfClass<SkillParam>(((Component) this).gameObject, (SkillParam) null);
    }

    private JobParam GetJobParam()
    {
      JobParam dataOfClass = DataSource.FindDataOfClass<JobParam>(((Component) this).gameObject, (JobParam) null);
      if (dataOfClass != null)
        return dataOfClass;
      return DataSource.FindDataOfClass<JobData>(((Component) this).gameObject, (JobData) null)?.Param;
    }

    private EquipData GetUnitEquipData()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      return dataOfClass != null && 0 <= this.Index && this.Index < dataOfClass.CurrentEquips.Length ? dataOfClass.CurrentEquips[this.Index] : (EquipData) null;
    }

    private EquipData GetEquipData()
    {
      return DataSource.FindDataOfClass<EquipData>(((Component) this).gameObject, (EquipData) null);
    }

    private QuestParam GetQuestParamAuto()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) ? MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) : DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
    }

    private TowerRewardItem GetTowerRewardItem()
    {
      QuestParam questParamAuto = this.GetQuestParamAuto();
      if (questParamAuto == null)
        return (TowerRewardItem) null;
      TowerRewardParam towerReward = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(questParamAuto.iname).reward_id);
      if (towerReward == null)
        return (TowerRewardItem) null;
      List<TowerRewardItem> towerRewardItem = towerReward.GetTowerRewardItem();
      return towerRewardItem == null || towerRewardItem.Count < this.Index ? (TowerRewardItem) null : towerRewardItem[this.Index];
    }

    private QuestParam GetQuestParam()
    {
      QuestParam questParam = (QuestParam) null;
      switch ((GameParameter.QuestInstanceTypes) this.InstanceType)
      {
        case GameParameter.QuestInstanceTypes.Any:
          questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
          break;
        case GameParameter.QuestInstanceTypes.Playing:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
          {
            questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
            break;
          }
          break;
        case GameParameter.QuestInstanceTypes.Selected:
          questParam = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          break;
      }
      return questParam;
    }

    private JSON_MyPhotonPlayerParam GetRoomPlayerParam()
    {
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).gameObject, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass == null)
        return (JSON_MyPhotonPlayerParam) null;
      return dataOfClass.playerIndex <= 0 ? (JSON_MyPhotonPlayerParam) null : dataOfClass;
    }

    private MultiPlayAPIRoom GetRoom()
    {
      return DataSource.FindDataOfClass<MultiPlayAPIRoom>(((Component) this).gameObject, (MultiPlayAPIRoom) null);
    }

    private JSON_MyPhotonRoomParam GetRoomParam()
    {
      JSON_MyPhotonRoomParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonRoomParam>(((Component) this).gameObject, (JSON_MyPhotonRoomParam) null);
      if (dataOfClass != null)
        return dataOfClass;
      MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
      return currentRoom == null ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
    }

    private bool LoadItemIcon(string iconName)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
      if (string.IsNullOrEmpty(iconName))
        return false;
      iconLoader.ResourcePath = AssetPath.ItemIcon(iconName);
      return true;
    }

    private bool LoadItemIcon(ItemParam itemParam)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
      if (itemParam == null)
        return false;
      iconLoader.ResourcePath = AssetPath.ItemIcon(itemParam);
      return true;
    }

    private void SetItemFrame(ItemParam itemParam)
    {
      if (itemParam == null)
        return;
      Image component = ((Component) this).GetComponent<Image>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Sprite itemFrame = GameSettings.Instance.GetItemFrame(itemParam);
      component.sprite = itemFrame;
    }

    private void SetEquipItemFrame(ItemParam itemParam)
    {
      Sprite[] normalFrames = GameSettings.Instance.ItemIcons.NormalFrames;
      Image component = ((Component) this).GetComponent<Image>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || normalFrames.Length <= 0)
        return;
      if (itemParam != null && itemParam.rare < normalFrames.Length)
        component.sprite = normalFrames[itemParam.rare];
      else
        component.sprite = normalFrames[0];
    }

    private MailData GetMailData()
    {
      return DataSource.FindDataOfClass<MailData>(((Component) this).gameObject, (MailData) null);
    }

    private SellItem GetSellItem()
    {
      return DataSource.FindDataOfClass<SellItem>(((Component) this).gameObject, (SellItem) null);
    }

    private List<SellItem> GetSellItemList()
    {
      return DataSource.FindDataOfClass<List<SellItem>>(((Component) this).gameObject, (List<SellItem>) null);
    }

    private ShopItem GetShopItem()
    {
      return DataSource.FindDataOfClass<ShopItem>(((Component) this).gameObject, (ShopItem) null) ?? (ShopItem) this.GetLimitedShopItem() ?? (ShopItem) this.GetEventShopItem();
    }

    private LimitedShopItem GetLimitedShopItem()
    {
      return DataSource.FindDataOfClass<LimitedShopItem>(((Component) this).gameObject, (LimitedShopItem) null);
    }

    private EventShopItem GetEventShopItem()
    {
      return DataSource.FindDataOfClass<EventShopItem>(((Component) this).gameObject, (EventShopItem) null);
    }

    private GachaParam GetGachaParam()
    {
      return DataSource.FindDataOfClass<GachaParam>(((Component) this).gameObject, (GachaParam) null);
    }

    private void SetBuyPriceTypeIcon(ESaleType type)
    {
      Sprite[] itemPriceIconFrames = GameSettings.Instance.ItemPriceIconFrames;
      if (itemPriceIconFrames == null || type == ESaleType.EventCoin)
        return;
      Image component = ((Component) this).GetComponent<Image>();
      int index = type == ESaleType.Coin_P ? 1 : (int) type;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || index >= itemPriceIconFrames.Length)
        return;
      component.sprite = itemPriceIconFrames[index];
    }

    private void SetBuyPriceEventCoinTypeIcon(string cost_iname)
    {
      Image component = ((Component) this).GetComponent<Image>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) || cost_iname == null)
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("EventShopCmn/eventcoin_small");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet, (UnityEngine.Object) null))
        return;
      component.sprite = spriteSheet.GetSprite(cost_iname);
    }

    private void SelectCoinDescription(string description)
    {
      string str;
      if (this.Index == 0)
      {
        str = description;
      }
      else
      {
        string[] strArray = description.Split('|');
        int num = this.Index >= 0 ? this.Index : -this.Index;
        str = strArray == null || num - 1 >= strArray.Length ? (string) null : strArray[num - 1];
      }
      if (this.Index >= 0)
        this.SetTextValue(str ?? string.Empty);
      else
        ((Component) this).gameObject.SetActive(str != null);
    }

    private TrophyParam GetTrophyParam()
    {
      return DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
    }

    private TrophyObjective GetTrophyObjective()
    {
      return DataSource.FindDataOfClass<TrophyObjective>(((Component) this).gameObject, (TrophyObjective) null);
    }

    private EnhanceEquipData GetEnhanceEquipData()
    {
      return DataSource.FindDataOfClass<EnhanceEquipData>(((Component) this).gameObject, (EnhanceEquipData) null);
    }

    private EnhanceMaterial GetEnhanceMaterial()
    {
      return DataSource.FindDataOfClass<EnhanceMaterial>(((Component) this).gameObject, (EnhanceMaterial) null);
    }

    private EquipItemParameter GetEquipItemParameter()
    {
      return DataSource.FindDataOfClass<EquipItemParameter>(((Component) this).gameObject, (EquipItemParameter) null);
    }

    private string GetParamTypeName(ParamTypes type, string tag = "")
    {
      switch (type)
      {
        case ParamTypes.None:
          return (string) null;
        case ParamTypes.Tokkou:
        case ParamTypes.Tokubou:
          return string.Format(LocalizedText.Get("sys." + type.ToString()), (object) tag);
        default:
          return LocalizedText.Get("sys." + type.ToString());
      }
    }

    private bool CheckUnlockInstanceType()
    {
      return MonoSingleton<GameManager>.Instance.Player.CheckUnlock((UnlockTargets) this.InstanceType);
    }

    private TrickParam GetTrickParam()
    {
      return DataSource.FindDataOfClass<TrickParam>(((Component) this).gameObject, (TrickParam) null);
    }

    private BattleCore.OrderData GetOrderData()
    {
      return DataSource.FindDataOfClass<BattleCore.OrderData>(((Component) this).gameObject, (BattleCore.OrderData) null);
    }

    private MapEffectParam GetMapEffectParam()
    {
      return DataSource.FindDataOfClass<MapEffectParam>(((Component) this).gameObject, (MapEffectParam) null);
    }

    private WeatherParam GetWeatherParam()
    {
      return DataSource.FindDataOfClass<WeatherParam>(((Component) this).gameObject, (WeatherParam) null);
    }

    private bool LoadArtifactIcon(ArtifactParam param)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
      if (param == null)
        return false;
      iconLoader.ResourcePath = AssetPath.ArtifactIcon(param);
      return true;
    }

    private void GetQuestObjectiveCount(QuestParam questParam, out int compCount, out int maxCount)
    {
      maxCount = questParam.bonusObjective.Length;
      compCount = 0;
      for (int index = 0; index < maxCount; ++index)
      {
        if ((questParam.clear_missions & 1 << index) != 0)
          ++compCount;
      }
    }

    private BindRuneData GetBindRuneData()
    {
      return this.InstanceType == 0 ? DataSource.FindDataOfClass<BindRuneData>(((Component) this).gameObject, (BindRuneData) null) : (BindRuneData) null;
    }

    private void InternalUpdateValue()
    {
      if (GameParameter.ParameterTypes.GUILDRAID_ST < this.ParameterType && this.ParameterType < GameParameter.ParameterTypes.GUILDRAID_ED)
      {
        this.GuildRaidUpdateValue();
      }
      else
      {
        GameParameter.ParameterTypes parameterType = this.ParameterType;
        QuestParam questParam1;
        GameManager instance1;
        UnitData unitData1;
        SupportData supportData;
        SkillParam skillParam;
        switch (parameterType)
        {
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.Name);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
            GameManager instance2 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance2.Player.CalcLevel());
            this.SetSliderValue(instance2.Player.CalcLevel(), 99);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
            GameManager instance3 = MonoSingleton<GameManager>.Instance;
            instance3.Player.UpdateStamina();
            this.SetTextValue(instance3.Player.Stamina);
            this.SetSliderValue(instance3.Player.Stamina, instance3.Player.StaminaMax);
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.StaminaMax);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
            GameManager instance4 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance4.Player.Exp);
            this.SetSliderValue(instance4.Player.GetExp(), instance4.Player.GetExp() + instance4.Player.GetNextExp());
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetNextExp());
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.Gold);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.Coin);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
            this.SetTextValue(TimeManager.ToMinSecString(MonoSingleton<GameManager>.Instance.Player.GetNextStaminaRecoverySec()));
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.QUEST_NAME:
            QuestParam questParam2;
            if ((questParam2 = this.GetQuestParam()) != null)
            {
              if (this.InstanceType == 1 && questParam2.IsGuildRaid && GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Mock)
              {
                this.SetTextValue(questParam2.name + "(" + LocalizedText.Get("sys.GUILDRAID_SWITCH_BATTALETEST") + ")");
                break;
              }
              this.SetTextValue(questParam2.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_STAMINA:
            QuestParam questParam3;
            if ((questParam3 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam3.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_STATE:
            QuestParam questParam4;
            if ((questParam4 = this.GetQuestParam()) != null)
            {
              this.SetAnimatorInt("state", (int) questParam4.state);
              this.SetImageIndex((int) questParam4.state);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
            QuestParam questParam5;
            if ((questParam5 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam5.cond);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
            QuestParam questParam6;
            if ((questParam6 = this.GetQuestParam()) != null && questParam6.bonusObjective != null && 0 <= this.Index && this.Index < questParam6.bonusObjective.Length)
            {
              this.SetTextValue(GameUtility.ComposeQuestBonusObjectiveText(questParam6.bonusObjective[this.Index]));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_ICON:
          case GameParameter.ParameterTypes.INVENTORY_ITEMICON:
            ItemParam itemParam1 = this.ParameterType != GameParameter.ParameterTypes.ITEM_ICON ? this.GetInventoryItemParam() : this.GetItemParam();
            if (itemParam1 != null && this.LoadItemIcon(itemParam1))
            {
              if (this.ViewType != 1)
                break;
              ((Component) this).gameObject.RequireComponent<ItemLimitedIconAttach>().Refresh(itemParam1);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
            QuestParam questParam7;
            if ((questParam7 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam7.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_NAME:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue(supportData.PlayerName);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue(supportData.PlayerLevel);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue(supportData.UnitLevel);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
            if ((supportData = this.GetSupportData()) != null)
            {
              skillParam = supportData.LeaderSkill;
              if (skillParam == null)
                break;
              this.SetTextValue(skillParam.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_ATK:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue((int) supportData.UnitParam.ini_status.param.atk);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_HP:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue((int) supportData.UnitParam.ini_status.param.hp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_MAGIC:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue((int) supportData.UnitParam.ini_status.param.mag);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_RARITY:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetAnimatorInt("rare", supportData.UnitRarity);
              this.SetImageIndex(supportData.UnitRarity);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_ELEMENT:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetAnimatorInt("element", (int) supportData.UnitElement);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_ICON:
            if ((supportData = this.GetSupportData()) != null && supportData.Unit != null)
            {
              string str = AssetPath.UnitSkinIconSmall(supportData.Unit.UnitParam, supportData.Unit.GetSelectedSkin(), supportData.JobID);
              if (!string.IsNullOrEmpty(str))
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLDESC:
            if ((supportData = this.GetSupportData()) != null)
            {
              skillParam = supportData.LeaderSkill;
              if (skillParam == null)
                break;
              this.SetTextValue(skillParam.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_SUBTITLE:
            QuestParam questParam8;
            if ((questParam8 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam8.title);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_LEVEL:
            UnitData unitData2;
            if ((unitData2 = this.GetUnitData()) != null)
            {
              int num = unitData2.CalcLevel();
              this.SetTextValue(num);
              this.SetSliderValue(num, 99);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_HP:
            Unit unit1;
            if ((unit1 = this.GetUnit()) != null)
            {
              this.SetTextValue(unit1.GetCurrentHP());
              this.SetSliderValue(unit1.GetCurrentHP(), unit1.GetMaximumHP());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_HPMAX:
            UnitData unitData3;
            if ((unitData3 = this.GetUnitData()) != null)
            {
              Unit unit2 = this.GetUnit();
              this.SetTextValue(unit2 == null ? (long) (int) unitData3.Status.param.hp : unit2.GetMaximumHP());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_ATK:
            UnitData unitData4;
            if ((unitData4 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData4.Status.param.atk);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_MAG:
            UnitData unitData5;
            if ((unitData5 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData5.Status.param.mag);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_ICON:
            UnitData unitData6;
            if ((unitData6 = this.GetUnitData()) != null)
            {
              string str = AssetPath.UnitSkinIconSmall(unitData6.UnitParam, unitData6.GetSelectedSkin(), unitData6.CurrentJob.JobID);
              if (!string.IsNullOrEmpty(str))
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_NAME:
            UnitParam unitParam1;
            if ((unitParam1 = this.GetUnitParam()) != null)
            {
              this.SetTextValue(unitParam1.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_RARITY:
            UnitData unitData7;
            if ((unitData7 = this.GetUnitData()) != null)
            {
              StarGauge component = ((Component) this).GetComponent<StarGauge>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                component.Max = unitData7.GetRarityCap() + 1;
                component.Value = unitData7.Rarity + 1;
                break;
              }
              this.SetAnimatorInt("rare", unitData7.Rarity);
              this.SetImageIndex(unitData7.Rarity);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PARTY_LEADERSKILLNAME:
            PartyData partyData1;
            if ((partyData1 = this.GetPartyData()) != null)
            {
              skillParam = this.GetLeaderSkill(partyData1);
              if (skillParam != null)
              {
                this.SetTextValue(skillParam.name);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PARTY_LEADERSKILLDESC:
            PartyData partyData2;
            if ((partyData2 = this.GetPartyData()) != null)
            {
              skillParam = this.GetLeaderSkill(partyData2);
              if (skillParam != null)
              {
                this.SetTextValue(skillParam.expr);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_DEF:
            UnitData unitData8;
            if ((unitData8 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData8.Status.param.def);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_MND:
            UnitData unitData9;
            if ((unitData9 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData9.Status.param.mnd);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_SPEED:
            UnitData unitData10;
            if ((unitData10 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData10.Status.param.spd);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_LUCK:
            UnitData unitData11;
            if ((unitData11 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData11.Status.param.luk);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOBNAME:
            UnitData unitData12;
            if ((unitData12 = this.GetUnitData()) != null && unitData12.CurrentJob != null)
            {
              this.SetTextValue(unitData12.CurrentJob.Name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOBRANK:
            UnitData unitData13;
            if ((unitData13 = this.GetUnitData()) != null)
            {
              int rank = unitData13.CurrentJob.Rank;
              int jobRankCap = unitData13.GetJobRankCap();
              this.SetTextValue(rank);
              this.SetSliderValue(rank, jobRankCap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_ELEMENT:
            UnitData unitData14;
            if ((unitData14 = this.GetUnitData()) != null)
            {
              int element = (int) unitData14.Element;
              this.SetAnimatorInt("element", element);
              this.SetImageIndex(element);
              break;
            }
            UnitParam unitParam2;
            if ((unitParam2 = this.GetUnitParam()) != null)
            {
              int element = (int) unitParam2.element;
              this.SetAnimatorInt("element", element);
              this.SetImageIndex(element);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PARTY_TOTALATK:
            PartyData partyData3;
            if ((partyData3 = this.GetPartyData()) != null)
            {
              GameManager instance5 = MonoSingleton<GameManager>.Instance;
              int num = 0;
              for (int index = 0; index < partyData3.MAX_UNIT; ++index)
              {
                long unitUniqueId = partyData3.GetUnitUniqueID(index);
                UnitData unitDataByUniqueId = instance5.Player.FindUnitDataByUniqueID(unitUniqueId);
                if (unitDataByUniqueId != null)
                {
                  JobData jobFor = unitDataByUniqueId.GetJobFor(partyData3.PartyType);
                  int jobIndex = unitDataByUniqueId.JobIndex;
                  if (jobFor != unitDataByUniqueId.CurrentJob)
                    unitDataByUniqueId.SetJob(jobFor);
                  num = num + (int) unitDataByUniqueId.Status.param.atk + (int) unitDataByUniqueId.Status.param.mag;
                  if (unitDataByUniqueId.JobIndex != jobIndex)
                    unitDataByUniqueId.SetJobIndex(jobIndex);
                }
              }
              if (this.InstanceType == 1)
              {
                string selectedQuestId = GlobalVars.SelectedQuestID;
                if (!string.IsNullOrEmpty(selectedQuestId))
                {
                  QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(selectedQuestId);
                  if (quest != null && quest.units.IsNotNull())
                  {
                    for (int index = 0; index < quest.units.Length; ++index)
                    {
                      UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(quest.units.Get(index));
                      if (unitDataByUnitId != null)
                      {
                        JobData jobFor = unitDataByUnitId.GetJobFor(partyData3.PartyType);
                        int jobIndex = unitDataByUnitId.JobIndex;
                        if (jobFor != unitDataByUnitId.CurrentJob)
                          unitDataByUnitId.SetJob(jobFor);
                        num = num + (int) unitDataByUnitId.Status.param.atk + (int) unitDataByUnitId.Status.param.mag;
                        if (jobIndex != unitDataByUnitId.JobIndex)
                          unitDataByUnitId.SetJobIndex(jobIndex);
                      }
                    }
                  }
                }
                supportData = (SupportData) GlobalVars.SelectedSupport;
                if (supportData != null)
                {
                  SupportData supportData1 = MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == supportData.FUID));
                  if (supportData1 != null && supportData1.Unit != null)
                    num = num + (int) supportData1.Unit.Status.param.atk + (int) supportData1.Unit.Status.param.mag;
                }
              }
              this.SetTextValue(num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.INVENTORY_ITEMNAME:
            ItemParam inventoryItemParam;
            if ((inventoryItemParam = this.GetInventoryItemParam()) != null)
            {
              this.SetTextValue(inventoryItemParam.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_NAME:
            ItemParam itemParam2;
            if ((itemParam2 = this.GetItemParam()) != null)
            {
              if (itemParam2.type == EItemType.Unit)
              {
                UnitParam unitParam3 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(itemParam2.iname);
                if (unitParam3 == null)
                  break;
                this.SetTextValue(unitParam3.name);
                break;
              }
              this.SetTextValue(itemParam2.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_DESC:
            ItemParam itemParam3;
            if ((itemParam3 = this.GetItemParam()) != null)
            {
              RuneData dataOfClass = DataSource.FindDataOfClass<RuneData>(((Component) this).gameObject, (RuneData) null);
              if (dataOfClass != null)
              {
                if ((!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null) || !string.IsNullOrEmpty(this.mText.text)) && (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null) || !(this.mText.text == this.mDefaultValue)))
                  break;
                BaseStatus addStatus = (BaseStatus) null;
                BaseStatus scaleStatus = (BaseStatus) null;
                dataOfClass.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, false);
                Array values = Enum.GetValues(typeof (ParamTypes));
                for (int index = 0; index < values.Length; ++index)
                {
                  ParamTypes type = (ParamTypes) values.GetValue(index);
                  int num = addStatus[type];
                  if (num != 0)
                  {
                    this.SetTextValue(LocalizedText.Get("sys." + (object) type) + " +" + num.ToString());
                    break;
                  }
                }
                break;
              }
              this.SetTextValue(itemParam3.Expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_SELLPRICE:
            ItemParam itemParam4;
            if ((itemParam4 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam4.sell);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_BUYPRICE:
            ItemParam itemParam5;
            if ((itemParam5 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam5.buy);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_AMOUNT:
            switch (this.InstanceType)
            {
              case 0:
                ItemData dataOfClass1;
                if ((dataOfClass1 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null)) != null)
                {
                  this.SetTextValue(dataOfClass1.Num);
                  this.SetSliderValue(dataOfClass1.Num, dataOfClass1.HaveCap);
                  return;
                }
                break;
              case 1:
                GameManager instance6 = MonoSingleton<GameManager>.Instance;
                if (0 <= this.Index && this.Index < instance6.Player.Inventory.Length)
                {
                  ItemData itemData = instance6.Player.Inventory[this.Index];
                  this.SetTextValue(itemData.Num);
                  this.SetSliderValue(itemData.Num, itemData.HaveCap);
                  return;
                }
                break;
              case 2:
                QuestParam questParamAuto1;
                if ((questParamAuto1 = this.GetQuestParamAuto()) != null && questParamAuto1.type == QuestTypes.Tower)
                {
                  TowerRewardItem towerRewardItem = this.GetTowerRewardItem();
                  if (towerRewardItem == null)
                    return;
                  this.SetTextValue(towerRewardItem.num);
                  if (string.IsNullOrEmpty(towerRewardItem.iname) || towerRewardItem.type != TowerRewardItem.RewardType.Item)
                    return;
                  ItemParam itemParam6 = MonoSingleton<GameManager>.Instance.GetItemParam(towerRewardItem.iname);
                  if (itemParam6 == null)
                    return;
                  this.SetSliderValue(towerRewardItem.num, itemParam6.cap);
                  return;
                }
                QuestParam questParamAuto2;
                if ((questParamAuto2 = this.GetQuestParamAuto()) != null && questParamAuto2.IsVersus)
                {
                  GameManager instance7 = MonoSingleton<GameManager>.Instance;
                  PlayerData player = instance7.Player;
                  VersusTowerParam versusTowerParam = instance7.GetCurrentVersusTowerParam(player.VersusTowerFloor + 1);
                  if (versusTowerParam != null)
                  {
                    this.SetTextValue((int) versusTowerParam.ArrivalItemNum);
                    string arrivalIteminame = (string) versusTowerParam.ArrivalIteminame;
                    if (string.IsNullOrEmpty(arrivalIteminame) || versusTowerParam.ArrivalItemType != VERSUS_ITEM_TYPE.item)
                      return;
                    ItemParam itemParam7 = MonoSingleton<GameManager>.Instance.GetItemParam(arrivalIteminame);
                    if (itemParam7 == null)
                      return;
                    this.SetSliderValue((int) versusTowerParam.ArrivalItemNum, itemParam7.cap);
                    return;
                  }
                  this.ResetToDefault();
                  return;
                }
                QuestParam questParamAuto3;
                ItemParam itemParam8;
                if ((questParamAuto3 = this.GetQuestParamAuto()) != null && questParamAuto3.bonusObjective != null && 0 <= this.Index && this.Index < questParamAuto3.bonusObjective.Length && (itemParam8 = MonoSingleton<GameManager>.Instance.GetItemParam(questParamAuto3.bonusObjective[this.Index].item)) != null)
                {
                  this.SetTextValue(questParamAuto3.bonusObjective[this.Index].itemNum);
                  this.SetSliderValue(questParamAuto3.bonusObjective[this.Index].itemNum, itemParam8.cap);
                  return;
                }
                break;
              case 3:
                ItemParam itemParam9 = this.GetItemParam();
                if (itemParam9 != null)
                {
                  ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam9.iname);
                  if (itemDataByItemId != null)
                  {
                    this.SetTextValue(itemDataByItemId.Num);
                    this.SetSliderValue(itemDataByItemId.Num, itemDataByItemId.HaveCap);
                    return;
                  }
                  break;
                }
                break;
              case 4:
                EnhanceMaterial dataOfClass2 = DataSource.FindDataOfClass<EnhanceMaterial>(((Component) this).gameObject, (EnhanceMaterial) null);
                if (dataOfClass2 != null && dataOfClass2.item != null)
                {
                  this.SetTextValue(dataOfClass2.item.Num);
                  this.SetSliderValue(dataOfClass2.item.Num, dataOfClass2.item.HaveCap);
                  return;
                }
                break;
              case 5:
                EnhanceEquipData dataOfClass3 = DataSource.FindDataOfClass<EnhanceEquipData>(((Component) this).gameObject, (EnhanceEquipData) null);
                if (dataOfClass3 != null && dataOfClass3.equip != null)
                {
                  ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(dataOfClass3.equip.ItemID);
                  if (itemDataByItemId != null)
                  {
                    this.SetTextValue(itemDataByItemId.Num);
                    this.SetSliderValue(itemDataByItemId.Num, itemDataByItemId.HaveCap);
                    return;
                  }
                  break;
                }
                break;
              case 6:
                SellItem dataOfClass4 = DataSource.FindDataOfClass<SellItem>(((Component) this).gameObject, (SellItem) null);
                if (dataOfClass4 != null && dataOfClass4.item != null)
                {
                  this.SetTextValue(dataOfClass4.item.Num);
                  this.SetSliderValue(dataOfClass4.item.Num, dataOfClass4.item.HaveCap);
                  return;
                }
                break;
              case 7:
                ConsumeItemData dataOfClass5 = DataSource.FindDataOfClass<ConsumeItemData>(((Component) this).gameObject, (ConsumeItemData) null);
                if (dataOfClass5 != null)
                {
                  this.SetTextValue(dataOfClass5.num);
                  return;
                }
                break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.INVENTORY_ITEMAMOUNT:
            ItemData inventoryItemData;
            if ((inventoryItemData = this.GetInventoryItemData()) != null && inventoryItemData.Param != null)
            {
              this.SetTextValue(inventoryItemData.Num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYER_NUMUNITS:
            GameManager instance8 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance8.Player.UnitNum);
            this.SetSliderValue(instance8.Player.UnitNum, instance8.Player.UnitCap);
            break;
          case GameParameter.ParameterTypes.PLAYER_MAXUNITS:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.UnitCap);
            break;
          case GameParameter.ParameterTypes.SKILL_NAME:
            SkillData skillData1;
            if ((skillData1 = this.GetSkillData()) != null)
            {
              this.SetTextValue(skillData1.SkillParam.name);
              break;
            }
            if ((skillParam = this.GetSkillParam()) != null)
            {
              this.SetTextValue(skillParam.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SKILL_ICON:
label_3654:
            if (this.IsParameterType_Rune)
            {
              this.UpdateValue_Rune();
              break;
            }
            if (this.IsParameterType_Ranking)
            {
              this.UpdateValue_Ranking();
              break;
            }
            if (!this.IsParameterType_CombatPower)
              break;
            this.UpdateValue_CombatPower();
            break;
          case GameParameter.ParameterTypes.SKILL_DESCRIPTION:
            SkillData skillData2;
            if ((skillData2 = this.GetSkillData()) != null)
            {
              this.SetTextValue(skillData2.SkillParam.expr);
              break;
            }
            if ((skillParam = this.GetSkillParam()) != null)
            {
              this.SetTextValue(skillParam.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SKILL_MP:
            SkillData skillData3;
            if ((skillData3 = this.GetSkillData()) != null)
            {
              Unit unit3;
              if ((unit3 = this.GetUnit()) != null)
              {
                this.SetTextValue(unit3.GetSkillUsedCost(skillData3));
                break;
              }
              UnitData unitData15;
              if ((unitData15 = this.GetUnitData()) != null)
              {
                this.SetTextValue(unitData15.GetSkillUsedCost(skillData3));
                break;
              }
              this.SetTextValue(skillData3.Cost);
              break;
            }
            if ((skillParam = this.GetSkillParam()) != null)
            {
              UnitData unitData16;
              if ((unitData16 = this.GetUnitData()) != null)
              {
                this.SetTextValue(unitData16.GetSkillUsedCost(skillParam));
                break;
              }
              this.SetTextValue(skillParam.cost);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.BATTLE_GOLD:
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
              break;
            this.SetTextValue(SceneBattle.Instance.GoldCount);
            break;
          case GameParameter.ParameterTypes.BATTLE_TREASURE:
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
              break;
            this.SetTextValue(SceneBattle.Instance.DispTreasureCount);
            break;
          case GameParameter.ParameterTypes.UNIT_MP:
            Unit unit4;
            if ((unit4 = this.GetUnit()) != null)
            {
              int gems = unit4.Gems;
              int mp = (int) unit4.MaximumStatus.param.mp;
              this.SetTextValue(gems);
              this.SetSliderValue(gems, mp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_MPMAX:
            UnitData unitData17;
            if ((unitData17 = this.GetUnitData()) != null)
            {
              OInt oint = (OInt) 0;
              Unit unit5 = this.GetUnit();
              this.SetTextValue((int) (unit5 == null ? (OInt) unitData17.Status.param.mp : (OInt) unit5.MaximumStatus.param.mp));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_ICON:
            AbilityParam abilityParam1;
            if ((abilityParam1 = this.GetAbilityParam()) != null)
            {
              GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.AbilityIcon(abilityParam1);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_NAME:
            AbilityParam abilityParam2;
            if ((abilityParam2 = this.GetAbilityParam()) != null)
            {
              this.SetTextValue(abilityParam2.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_KAKERA_ICON:
            QuestParam questParam9;
            if ((questParam9 = this.GetQuestParam()) != null)
            {
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
                break;
              ItemParam hardDropPiece = QuestDropParam.Instance.GetHardDropPiece(questParam9.iname, GlobalVars.GetDropTableGeneratedDateTime());
              if (hardDropPiece != null && this.LoadItemIcon(hardDropPiece.icon))
                break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_EXP:
            UnitData unitData18;
            if ((unitData18 = this.GetUnitData()) != null)
            {
              GameManager instance9 = MonoSingleton<GameManager>.Instance;
              int exp = unitData18.GetExp();
              int maxValue = instance9.MasterParam.GetUnitLevelExp(unitData18.GetNextLevel()) - instance9.MasterParam.GetUnitLevelExp(unitData18.Lv);
              this.SetTextValue(exp);
              this.SetSliderValue(exp, maxValue);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_EXPMAX:
            UnitData unitData19;
            if ((unitData19 = this.GetUnitData()) != null)
            {
              GameManager instance10 = MonoSingleton<GameManager>.Instance;
              this.SetTextValue(instance10.MasterParam.GetUnitLevelExp(unitData19.GetNextLevel()) - instance10.MasterParam.GetUnitLevelExp(unitData19.Lv));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_EXPTOGO:
            UnitData unitData20;
            if ((unitData20 = this.GetUnitData()) != null)
            {
              this.SetTextValue(unitData20.GetNextExp());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_KAKERA_NUM:
            UnitData unitData21;
            if ((unitData21 = this.GetUnitData()) != null)
            {
              int pieces = unitData21.GetPieces();
              int maxValue = unitData21.AwakeLv >= unitData21.GetAwakeLevelCap() ? pieces : unitData21.GetAwakeNeedPieces();
              this.SetTextValue(pieces);
              this.SetSliderValue(pieces, maxValue);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_KAKERA_MAX:
            UnitData unitData22;
            if ((unitData22 = this.GetUnitData()) != null)
            {
              this.SetTextValue(unitData22.GetAwakeNeedPieces());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_EXP:
            EquipData equipData1;
            if ((equipData1 = this.GetEquipData()) != null && equipData1.IsValid())
            {
              int exp = equipData1.GetExp();
              int nextExp = equipData1.GetNextExp();
              this.SetTextValue(exp);
              this.SetSliderValue(exp, nextExp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_EXPMAX:
            EquipData equipData2;
            if ((equipData2 = this.GetEquipData()) != null && equipData2.IsValid())
            {
              this.SetTextValue(equipData2.GetNextExp());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_RANK:
            EquipData equipData3;
            if ((equipData3 = this.GetEquipData()) != null && equipData3.IsValid())
            {
              this.SetTextValue(equipData3.Rank);
              this.SetAnimatorInt("rank", equipData3.Rank);
              this.SetImageIndex(equipData3.Rank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_RANK:
            AbilityData abilityData1;
            if ((abilityData1 = this.GetAbilityData()) != null)
            {
              this.SetTextValue(abilityData1.Rank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.OBSOLETE_ABILITY:
            break;
          case GameParameter.ParameterTypes.ABILITY_NEXTGOLD:
            AbilityData abilityData2;
            if ((abilityData2 = this.GetAbilityData()) != null)
            {
              this.SetTextValue(abilityData2.GetNextGold());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_SLOT:
            AbilityParam abilityParam3;
            if ((abilityParam3 = this.GetAbilityParam()) != null)
            {
              if (abilityParam3.type_detail != EAbilityTypeDetail.Default)
              {
                this.SetAnimatorInt("type", (int) abilityParam3.slot);
                if (this.GetImageLength() <= 3)
                {
                  this.SetImageIndex((int) abilityParam3.slot);
                  break;
                }
                this.SetImageIndex(GameParameter.AbilityTypeDetailToImageIndex(abilityParam3.slot, abilityParam3.type_detail));
                break;
              }
              this.SetAnimatorInt("type", (int) abilityParam3.slot);
              this.SetImageIndex((int) abilityParam3.slot);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_JOBICON:
          case GameParameter.ParameterTypes.UNIT_JOBICON:
          case GameParameter.ParameterTypes.UNIT_JOBICON2:
          case GameParameter.ParameterTypes.UNIT_JOB_JOBICON2:
          case GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_JOBICON:
          case GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_JOBICON2:
label_351:
            UnitData unitData23;
            JobParam job1 = (unitData23 = this.GetUnitData()) == null || !unitData23.IsJobAvailable(this.Index) ? this.GetJobParam() : (this.ParameterType == GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_JOBICON || this.ParameterType == GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_JOBICON2 ? unitData23.GetClassChangeJobParam(this.Index) : (this.ParameterType == GameParameter.ParameterTypes.UNIT_JOBICON || this.ParameterType == GameParameter.ParameterTypes.UNIT_JOBICON2 || this.ParameterType == GameParameter.ParameterTypes.UNIT_JOBICON2_BUGFIX ? unitData23.CurrentJob.Param : unitData23.Jobs[this.Index].Param));
            if (job1 != null)
            {
              string str = this.ParameterType == GameParameter.ParameterTypes.UNIT_JOB_JOBICON2 || this.ParameterType == GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_JOBICON2 || this.ParameterType == GameParameter.ParameterTypes.UNIT_JOBICON2_BUGFIX ? AssetPath.JobIconMedium(job1) : AssetPath.JobIconSmall(job1);
              if (string.IsNullOrEmpty(str))
                break;
              GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str;
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_RANK:
            UnitData unitData24;
            if ((unitData24 = this.GetUnitData()) != null && unitData24.IsJobAvailable(this.Index))
            {
              int rank = unitData24.Jobs[this.Index].Rank;
              int jobRankCap = unitData24.GetJobRankCap();
              this.SetTextValue(rank);
              this.SetSliderValue(rank, jobRankCap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_NAME:
            UnitData unitData25;
            if ((unitData25 = this.GetUnitData()) != null && unitData25.IsJobAvailable(this.Index))
            {
              this.SetTextValue(unitData25.Jobs[this.Index].Name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_RANKMAX:
            UnitData unitData26;
            if ((unitData26 = this.GetUnitData()) != null && unitData26.IsJobAvailable(this.Index))
            {
              this.SetTextValue(unitData26.Jobs[this.Index].GetJobRankCap(unitData26));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_HP:
            EquipData equipData4;
            if ((equipData4 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData4.Skill == null ? 0 : equipData4.Skill.GetBuffEffectValue(ParamTypes.Hp);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_AP:
            EquipData equipData5;
            if ((equipData5 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData5.Skill == null ? 0 : equipData5.Skill.GetBuffEffectValue(ParamTypes.Mp);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_IAP:
            EquipData equipData6;
            if ((equipData6 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData6.Skill == null ? 0 : equipData6.Skill.GetBuffEffectValue(ParamTypes.MpIni);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_ATK:
            EquipData equipData7;
            if ((equipData7 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData7.Skill == null ? 0 : equipData7.Skill.GetBuffEffectValue(ParamTypes.Atk);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_DEF:
            EquipData equipData8;
            if ((equipData8 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData8.Skill == null ? 0 : equipData8.Skill.GetBuffEffectValue(ParamTypes.Def);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_MAG:
            EquipData equipData9;
            if ((equipData9 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData9.Skill == null ? 0 : equipData9.Skill.GetBuffEffectValue(ParamTypes.Mag);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_MND:
            EquipData equipData10;
            if ((equipData10 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData10.Skill == null ? 0 : equipData10.Skill.GetBuffEffectValue(ParamTypes.Mnd);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_REC:
            EquipData equipData11;
            if ((equipData11 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData11.Skill == null ? 0 : equipData11.Skill.GetBuffEffectValue(ParamTypes.Rec);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_SPD:
            EquipData equipData12;
            if ((equipData12 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData12.Skill == null ? 0 : equipData12.Skill.GetBuffEffectValue(ParamTypes.Spd);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_CRI:
            EquipData equipData13;
            if ((equipData13 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData13.Skill == null ? 0 : equipData13.Skill.GetBuffEffectValue(ParamTypes.Cri);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_LUK:
            EquipData equipData14;
            if ((equipData14 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData14.Skill == null ? 0 : equipData14.Skill.GetBuffEffectValue(ParamTypes.Luk);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_MOV:
            EquipData equipData15;
            if ((equipData15 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData15.Skill == null ? 0 : equipData15.Skill.GetBuffEffectValue(ParamTypes.Mov);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_JMP:
            EquipData equipData16;
            if ((equipData16 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData16.Skill == null ? 0 : equipData16.Skill.GetBuffEffectValue(ParamTypes.Jmp);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_RANGE:
            EquipData equipData17;
            if ((equipData17 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData17.Skill == null ? 0 : equipData17.Skill.GetBuffEffectValue(ParamTypes.EffectRange);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_SCOPE:
            EquipData equipData18;
            if ((equipData18 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData18.Skill == null ? 0 : equipData18.Skill.GetBuffEffectValue(ParamTypes.EffectScope);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_EFFECTHEIGHT:
            EquipData equipData19;
            if ((equipData19 = this.GetEquipData()) != null)
            {
              int buffEffectValue = equipData19.Skill == null ? 0 : equipData19.Skill.GetBuffEffectValue(ParamTypes.EffectHeight);
              this.SetTextValue(buffEffectValue);
              this.ToggleEmpty(buffEffectValue != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_NAME:
            EquipData equipData20;
            if ((equipData20 = this.GetEquipData()) != null && equipData20.IsValid())
            {
              this.SetTextValue(equipData20.ItemParam.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_ICON:
            EquipData equipData21;
            if ((equipData21 = this.GetEquipData()) != null && equipData21.IsValid() && !string.IsNullOrEmpty(equipData21.ItemParam.icon) && this.LoadItemIcon(equipData21.ItemParam.icon))
              break;
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_NUM:
            break;
          case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNT:
            break;
          case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNTMAX:
            break;
          case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_COOLDOWNTIME:
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_AMOUNT:
            EquipData equipData22;
            if ((equipData22 = this.GetEquipData()) != null)
            {
              this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetItemAmount(equipData22.ItemID));
              this.SetUpdateInterval(1f);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_REQLV:
            ItemParam itemParam10;
            if ((itemParam10 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam10.equipLv);
              break;
            }
            EquipData equipData23;
            if ((equipData23 = this.GetEquipData()) != null)
            {
              this.SetTextValue(equipData23.ItemParam.equipLv);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOBEVOITEM_AMOUNT:
            JobEvolutionRecipe dataOfClass6;
            if ((dataOfClass6 = DataSource.FindDataOfClass<JobEvolutionRecipe>(((Component) this).gameObject, (JobEvolutionRecipe) null)) != null)
            {
              this.SetTextValue(dataOfClass6.Amount);
              this.SetSliderValue(dataOfClass6.Amount, dataOfClass6.RequiredAmount);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOBEVOITEM_REQAMOUNT:
            JobEvolutionRecipe dataOfClass7;
            if ((dataOfClass7 = DataSource.FindDataOfClass<JobEvolutionRecipe>(((Component) this).gameObject, (JobEvolutionRecipe) null)) != null)
            {
              this.SetTextValue(dataOfClass7.RequiredAmount);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOBEVOITEM_ICON:
            JobEvolutionRecipe dataOfClass8;
            if ((dataOfClass8 = DataSource.FindDataOfClass<JobEvolutionRecipe>(((Component) this).gameObject, (JobEvolutionRecipe) null)) != null && this.LoadItemIcon(dataOfClass8.Item.icon))
              break;
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOBEVOITEM_NAME:
            JobEvolutionRecipe dataOfClass9;
            if ((dataOfClass9 = DataSource.FindDataOfClass<JobEvolutionRecipe>(((Component) this).gameObject, (JobEvolutionRecipe) null)) != null)
            {
              this.SetTextValue(dataOfClass9.Item.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_EVOCOST:
            RecipeParam dataOfClass10 = DataSource.FindDataOfClass<RecipeParam>(((Component) this).gameObject, (RecipeParam) null);
            if (dataOfClass10 != null)
            {
              this.SetTextValue(dataOfClass10.cost);
              this.SetSliderValue(MonoSingleton<GameManager>.Instance.Player.Gold, dataOfClass10.cost);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_CRIT:
            UnitData unitData27;
            if ((unitData27 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData27.Status.param.cri);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_REGEN:
            UnitData unitData28;
            if ((unitData28 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData28.Status.param.rec);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_LEADERSKILLNAME:
            SkillData dataOfClass11 = DataSource.FindDataOfClass<SkillData>(((Component) this).gameObject, (SkillData) null);
            if (dataOfClass11 != null)
            {
              this.SetTextValue(dataOfClass11.Name);
              break;
            }
            UnitData unitData29;
            if ((unitData29 = this.GetUnitData()) != null && unitData29.LeaderSkill != null)
            {
              UnitData unit6 = (UnitData) null;
              if (!unitData29.IsNotFindUniqueID)
                unit6 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitData29.UniqueID);
              UnitData unitData30 = unit6 == null ? unitData29 : UnitOverWriteUtility.Apply(unit6, (eOverWritePartyType) GlobalVars.OverWritePartyType);
              if (this.Index >= 1 && unitData30.LeaderSkill != null)
              {
                this.SetTextValue(unitData30.LeaderSkill.Name);
                break;
              }
              if (unitData30.CurrentLeaderSkill != null)
              {
                this.SetTextValue(unitData30.CurrentLeaderSkill.Name);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_LEADERSKILLDESC:
            SkillData dataOfClass12 = DataSource.FindDataOfClass<SkillData>(((Component) this).gameObject, (SkillData) null);
            if (dataOfClass12 != null)
            {
              this.SetTextValue(dataOfClass12.SkillParam.expr);
              break;
            }
            UnitData unitData31;
            if ((unitData31 = this.GetUnitData()) != null)
            {
              UnitData unit7 = (UnitData) null;
              if (!unitData31.IsNotFindUniqueID)
                unit7 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitData31.UniqueID);
              UnitData unitData32 = unit7 == null ? unitData31 : UnitOverWriteUtility.Apply(unit7, (eOverWritePartyType) GlobalVars.OverWritePartyType);
              if (this.Index >= 1 && unitData32.LeaderSkill != null)
              {
                this.SetTextValue(unitData32.LeaderSkill.SkillParam.expr);
                break;
              }
              if (unitData32.CurrentLeaderSkill != null)
              {
                this.SetTextValue(unitData32.CurrentLeaderSkill.SkillParam.expr);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_VALUE:
            ItemParam itemParam11;
            if ((itemParam11 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam11.value);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_LEVELMAX:
            UnitData unitData33;
            if ((unitData33 = this.GetUnitData()) != null)
            {
              this.SetTextValue(unitData33.GetLevelCap());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_UNLOCKSTATE:
            UnitData unitData34;
            if ((unitData34 = this.GetUnitData()) != null && 0 <= this.Index && this.Index < unitData34.Jobs.Length)
            {
              this.SetAnimatorBool("unlocked", unitData34.Jobs[this.Index].IsActivated);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_JOBRANKMAX:
            UnitData unitData35;
            if ((unitData35 = this.GetUnitData()) != null)
            {
              int rank = unitData35.CurrentJob.Rank;
              int jobRankCap = unitData35.GetJobRankCap();
              this.SetTextValue(jobRankCap);
              this.SetSliderValue(rank, jobRankCap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_UNLOCKINFO:
            AbilityUnlockInfo dataOfClass13 = DataSource.FindDataOfClass<AbilityUnlockInfo>(((Component) this).gameObject, (AbilityUnlockInfo) null);
            if (dataOfClass13 != null)
            {
              this.SetTextValue(string.Format(LocalizedText.Get("sys.ABILITY_UNLOCK_RANK"), (object) dataOfClass13.JobName, (object) dataOfClass13.Rank));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ABILITY_DESC:
            AbilityParam abilityParam4;
            if ((abilityParam4 = this.GetAbilityParam()) != null)
            {
              this.SetTextValue(abilityParam4.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_FRAME:
            this.SetItemFrame(this.GetItemParam());
            break;
          case GameParameter.ParameterTypes.INVENTORY_FRAME:
            this.SetItemFrame(this.GetInventoryItemParam());
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_AMOUNT:
            RecipeItemParameter dataOfClass14;
            if ((dataOfClass14 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null)
            {
              dataOfClass14.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(dataOfClass14.Item.iname);
              this.SetTextValue(dataOfClass14.Amount);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_REQAMOUNT:
            RecipeItemParameter dataOfClass15;
            if ((dataOfClass15 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null)
            {
              this.SetTextValue(dataOfClass15.RequiredAmount);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_ICON:
            RecipeItemParameter dataOfClass16;
            if ((dataOfClass16 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null && this.LoadItemIcon(dataOfClass16.Item.icon))
              break;
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_NAME:
            RecipeItemParameter dataOfClass17;
            if ((dataOfClass17 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null)
            {
              this.SetTextValue(dataOfClass17.Item.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_CREATE_COST:
            RecipeItemParameter dataOfClass18;
            if ((dataOfClass18 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null)
            {
              RecipeParam recipeParam = MonoSingleton<GameManager>.Instance.GetRecipeParam(dataOfClass18.Item.recipe);
              if (recipeParam != null)
              {
                this.SetTextValue(recipeParam.cost);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_CREATE_ITEM_NAME:
            RecipeItemParameter dataOfClass19;
            if ((dataOfClass19 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) != null)
            {
              this.SetTextValue(dataOfClass19.Item.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.RECIPEITEM_FRAME:
            RecipeItemParameter dataOfClass20;
            if ((dataOfClass20 = DataSource.FindDataOfClass<RecipeItemParameter>(((Component) this).gameObject, (RecipeItemParameter) null)) == null)
              break;
            this.SetItemFrame(dataOfClass20.Item);
            break;
          case GameParameter.ParameterTypes.UNIT_PORTRAIT_MEDIUM:
            UnitData unitData36;
            if ((unitData36 = this.GetUnitData()) != null)
            {
              GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitSkinIconMedium(unitData36.UnitParam, unitData36.GetSelectedSkin(), unitData36.CurrentJobId);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTRESULT_GOLD:
            BattleCore.Record dataOfClass21 = DataSource.FindDataOfClass<BattleCore.Record>(((Component) this).gameObject, (BattleCore.Record) null);
            if (dataOfClass21 != null)
            {
              this.SetTextValue((int) dataOfClass21.gold);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTRESULT_PLAYEREXP:
            BattleCore.Record dataOfClass22 = DataSource.FindDataOfClass<BattleCore.Record>(((Component) this).gameObject, (BattleCore.Record) null);
            if (dataOfClass22 != null)
            {
              this.SetTextValue((int) dataOfClass22.playerexp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTRESULT_PARTYEXP:
            BattleCore.Record dataOfClass23 = DataSource.FindDataOfClass<BattleCore.Record>(((Component) this).gameObject, (BattleCore.Record) null);
            if (dataOfClass23 != null)
            {
              this.SetTextValue((int) dataOfClass23.unitexp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTRESULT_RATE:
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_LEVEL:
            PlayerLevelUpInfo levelUpInfo1;
            if ((levelUpInfo1 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo1.LevelPrev);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_LEVELNEXT:
            PlayerLevelUpInfo levelUpInfo2;
            if ((levelUpInfo2 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo2.LevelNext);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_STAMINA:
            PlayerLevelUpInfo levelUpInfo3;
            if ((levelUpInfo3 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo3.StaminaNext);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_STAMINAMAX:
            PlayerLevelUpInfo levelUpInfo4;
            if ((levelUpInfo4 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo4.StaminaMaxPrev);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_STAMINAMAXNEXT:
            PlayerLevelUpInfo levelUpInfo5;
            if ((levelUpInfo5 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo5.StaminaMaxNext);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_FRIENDNUM:
            PlayerLevelUpInfo levelUpInfo6;
            if ((levelUpInfo6 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo6.MaxFriendNumPrev);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_FRIENDNUMNEXT:
            PlayerLevelUpInfo levelUpInfo7;
            if ((levelUpInfo7 = this.GetLevelUpInfo()) != null)
            {
              this.SetTextValue(levelUpInfo7.MaxFriendNumNext);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYERLEVELUP_UNLOCKINFO:
            PlayerLevelUpInfo levelUpInfo8;
            if ((levelUpInfo8 = this.GetLevelUpInfo()) != null && 0 <= this.Index && this.Index < levelUpInfo8.UnlockInfo.Length)
            {
              this.SetTextValue(levelUpInfo8.UnlockInfo[this.Index]);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE_STATE:
            QuestParam questParam10;
            if ((questParam10 = this.GetQuestParam()) != null && questParam10.bonusObjective != null && 0 <= this.Index && this.Index < questParam10.bonusObjective.Length)
            {
              int index = (questParam10.clear_missions & 1 << this.Index) == 0 ? 0 : 1;
              this.SetAnimatorInt("state", index);
              this.SetImageIndex(index);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_SIDE:
            Unit unit8;
            if ((unit8 = this.GetUnit()) != null)
            {
              this.SetImageIndex((int) unit8.Side);
              this.SetAnimatorInt("index", (int) unit8.Side);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_CREATECOST:
            ItemParam itemParam12;
            if ((itemParam12 = this.GetItemParam()) != null)
            {
              this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetCreateItemCost(itemParam12));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_CAVESTAMINA:
            GameManager instance11 = MonoSingleton<GameManager>.Instance;
            instance11.Player.UpdateCaveStamina();
            this.SetTextValue(instance11.Player.CaveStamina);
            this.SetSliderValue(instance11.Player.CaveStamina, instance11.Player.CaveStaminaMax);
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_CAVESTAMINAMAX:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.CaveStaminaMax);
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_CAVESTAMINATIME:
            this.SetTextValue(TimeManager.ToMinSecString(MonoSingleton<GameManager>.Instance.Player.GetNextCaveStaminaRecoverySec()));
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.ITEM_AMOUNTMAX:
            ItemParam itemParam13;
            if ((itemParam13 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam13.cap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYER_NUMITEMS:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetItemSlotAmount());
            break;
          case GameParameter.ParameterTypes.QUEST_DIFFICULTY:
            QuestParam questParam11;
            if ((questParam11 = this.GetQuestParam()) != null)
            {
              int index = 0;
              switch (questParam11.difficulty)
              {
                case QuestDifficulties.Normal:
                  index = 0;
                  break;
                case QuestDifficulties.Elite:
                  index = 1;
                  break;
                case QuestDifficulties.Extra:
                  index = 2;
                  break;
              }
              this.SetImageIndex(index);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_HEIGHT:
            Unit unit9;
            if ((unit9 = this.GetUnit()) != null)
            {
              int num = 0;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
                num = SceneBattle.Instance.GetDisplayHeight(unit9);
              else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MultiPlayVersusReady.Instance, (UnityEngine.Object) null))
                num = MultiPlayVersusReady.Instance.GetDisplayHeight(unit9);
              this.SetTextValue(num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPMENT_FRAME:
            EquipData equipData24;
            if ((equipData24 = this.GetEquipData()) != null && equipData24.IsValid())
            {
              this.SetItemFrame(equipData24.ItemParam);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTLIST_CHAPTERNAME:
            ChapterParam dataOfClass24 = DataSource.FindDataOfClass<ChapterParam>(((Component) this).gameObject, (ChapterParam) null);
            if (dataOfClass24 != null)
            {
              this.SetTextValue(dataOfClass24.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTLIST_SECTIONNAME:
            ChapterParam dataOfClass25 = DataSource.FindDataOfClass<ChapterParam>(((Component) this).gameObject, (ChapterParam) null);
            if (dataOfClass25 != null)
            {
              this.SetTextValue(dataOfClass25.sectionParam != null ? dataOfClass25.sectionParam.name : string.Empty);
              break;
            }
            UIQuestSectionData dataOfClass26 = DataSource.FindDataOfClass<UIQuestSectionData>(((Component) this).gameObject, (UIQuestSectionData) null);
            if (dataOfClass26 != null)
            {
              this.SetTextValue(dataOfClass26.Name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MAIL_MESSAGE:
            MailData mailData1;
            if ((mailData1 = this.GetMailData()) != null)
            {
              this.SetTextValue(mailData1.msg);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_MULTI_TYPE:
            if ((questParam1 = this.GetQuestParam()) != null)
            {
              this.SetImageIndex(!GlobalVars.SelectedMultiPlayQuestIsEvent ? 0 : 1);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_NAME:
            JSON_MyPhotonPlayerParam roomPlayerParam1 = this.GetRoomPlayerParam();
            if (roomPlayerParam1 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(roomPlayerParam1.playerName);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_LEVEL:
            JSON_MyPhotonPlayerParam roomPlayerParam2 = this.GetRoomPlayerParam();
            if (roomPlayerParam2 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(roomPlayerParam2.playerLevel);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_STATE:
            JSON_MyPhotonPlayerParam roomPlayerParam3 = this.GetRoomPlayerParam();
            if (roomPlayerParam3 != null)
            {
              MyPhoton instance12 = PunMonoSingleton<MyPhoton>.Instance;
              bool flag = false;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance12, (UnityEngine.Object) null))
              {
                List<MyPhoton.MyPlayer> roomPlayerList = instance12.GetRoomPlayerList();
                if (roomPlayerList != null)
                {
                  MyPhoton.MyPlayer player = instance12.FindPlayer(roomPlayerList, roomPlayerParam3.playerID, roomPlayerParam3.playerIndex);
                  if (player != null)
                    flag = player.start;
                }
              }
              if (this.Index == 0)
              {
                ((Component) this).gameObject.SetActive(roomPlayerParam3.state != 0 && roomPlayerParam3.state != 4 && roomPlayerParam3.state != 5 && !flag);
                break;
              }
              if (this.Index != 1)
                break;
              ((Component) this).gameObject.SetActive(roomPlayerParam3.state == this.InstanceType);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_ICON:
            UnitData unit10 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit10 == null)
            {
              this.ResetToDefault();
              break;
            }
            string str1 = AssetPath.UnitSkinIconSmall(unit10.UnitParam, unit10.GetSelectedSkin(), unit10.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str1))
            {
              this.ResetToDefault();
              break;
            }
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str1;
            break;
          case GameParameter.ParameterTypes.MAIL_GIFT_STRING:
            MailData mailData2;
            if ((mailData2 = this.GetMailData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            string str2 = string.Empty;
            GiftData[] gifts = mailData2.gifts;
            for (int index = 0; index < gifts.Length; ++index)
            {
              if (gifts[index].coin > 0)
                str2 = str2 + LocalizedText.Get("sys.COIN") + "×" + gifts[index].coin.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
              if (gifts[index].gold > 0)
              {
                string formatedText = CurrencyBitmapText.CreateFormatedText(gifts[index].gold.ToString());
                str2 += string.Format(LocalizedText.Get("sys.CONVERT_TO_GOLD"), (object) formatedText);
              }
              if (gifts[index].arenacoin > 0)
                str2 = str2 + LocalizedText.Get("sys.ARENA_COIN") + "×" + gifts[index].arenacoin.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
              if (gifts[index].multicoin > 0)
                str2 = str2 + LocalizedText.Get("sys.MULTI_COIN") + "×" + gifts[index].multicoin.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
              if (gifts[index].kakeracoin > 0)
                str2 = str2 + LocalizedText.Get("sys.PIECE_POINT") + "×" + gifts[index].kakeracoin.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
              if (!string.IsNullOrEmpty(gifts[index].ConceptCardIname) && gifts[index].CheckGiftTypeIncluded(GiftTypes.ConceptCard))
              {
                ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(gifts[index].ConceptCardIname);
                if (conceptCardParam != null)
                {
                  string str3 = "×";
                  str2 = str2 + conceptCardParam.name + str3 + gifts[index].ConceptCardNum.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
                }
              }
              if (gifts[index].iname != null && gifts[index].num > 0)
              {
                if (gifts[index].CheckGiftTypeIncluded(GiftTypes.Artifact))
                {
                  ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(gifts[index].iname);
                  if (artifactParam != null)
                  {
                    string str4 = "×";
                    str2 = str2 + artifactParam.name + str4 + gifts[index].num.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
                  }
                }
                if (gifts[index].CheckGiftTypeIncluded(GiftTypes.Item) || gifts[index].CheckGiftTypeIncluded(GiftTypes.SelectSummonTickets))
                {
                  ItemParam itemParam14 = MonoSingleton<GameManager>.Instance.GetItemParam(gifts[index].iname);
                  if (itemParam14 != null)
                  {
                    string str5 = "×";
                    str2 = str2 + itemParam14.name + str5 + gifts[index].num.ToString() + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
                  }
                }
                if (gifts[index].CheckGiftTypeIncluded(GiftTypes.Unit))
                {
                  UnitParam unitParam4 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(gifts[index].iname);
                  if (unitParam4 != null)
                    str2 += unitParam4.name;
                }
                if (gifts[index].CheckGiftTypeIncluded(GiftTypes.Award))
                {
                  AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(gifts[index].iname);
                  if (awardParam != null)
                    str2 = str2 + LocalizedText.Get("sys.MAILBOX_ITEM_AWARD") + awardParam.name;
                }
              }
              if (str2 != string.Empty && str2[str2.Length - 1] != '、')
                str2 += "、";
            }
            if (str2 != string.Empty)
              str2 = str2.Substring(0, str2.Length - 1);
            this.SetTextValue(str2);
            break;
          case GameParameter.ParameterTypes.MAIL_GIFT_LIMIT:
            MailData mailData3;
            if ((mailData3 = this.GetMailData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(GameUtility.UnixtimeToLocalTime(mailData3.post_at).AddDays(14.0).ToString("yyyy/MM/dd HH:mm"));
            break;
          case GameParameter.ParameterTypes.MAIL_GIFT_GETAT:
            MailData mailData4;
            if ((mailData4 = this.GetMailData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(GameUtility.UnixtimeToLocalTime(mailData4.read).ToString("yyyy/MM/dd HH:mm:ss"));
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_COMMENT:
            MultiPlayAPIRoom room1 = this.GetRoom();
            if (room1 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(!string.IsNullOrEmpty(room1.comment) ? room1.comment : string.Empty);
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_OWNER_NAME:
            MultiPlayAPIRoom room2 = this.GetRoom();
            if (room2 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(room2.owner == null || string.IsNullOrEmpty(room2.owner.name) ? "???" : room2.owner.name);
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_OWNER_LV:
            MultiPlayAPIRoom room3 = this.GetRoom();
            if (room3 == null || room3.owner == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(room3.owner.level);
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_QUEST_NAME:
            MultiPlayAPIRoom room4 = this.GetRoom();
            QuestParam quest1 = room4 == null || room4.quest == null || string.IsNullOrEmpty(room4.quest.iname) ? (QuestParam) null : MonoSingleton<GameManager>.Instance.FindQuest(room4.quest.iname);
            if (quest1 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(!string.IsNullOrEmpty(quest1.name) ? quest1.name : "ERROR");
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_LOCKED_ICON:
            MultiPlayAPIRoom room5 = this.GetRoom();
            if (room5 == null)
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(MultiPlayAPIRoom.IsLocked(room5.pwd_hash));
              break;
            }
            ((Component) this).gameObject.SetActive(!MultiPlayAPIRoom.IsLocked(room5.pwd_hash));
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_PLAYER_NUM:
            MultiPlayAPIRoom room6 = this.GetRoom();
            if (room6 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(room6.num);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_FRIENDCODE:
            GameManager instance13 = MonoSingleton<GameManager>.Instance;
            if (instance13.Player.FUID == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance13.Player.FUID);
            break;
          case GameParameter.ParameterTypes.FRIEND_FRIENDCODE:
            FriendData friendData1;
            if ((friendData1 = this.GetFriendData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(friendData1.FUID);
            break;
          case GameParameter.ParameterTypes.FRIEND_NAME:
            FriendData friendData2;
            if ((friendData2 = this.GetFriendData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(friendData2.PlayerName);
            break;
          case GameParameter.ParameterTypes.FRIEND_LEVEL:
            FriendData friendData3;
            if ((friendData3 = this.GetFriendData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(friendData3.PlayerLevel.ToString());
            break;
          case GameParameter.ParameterTypes.FRIEND_LASTLOGIN:
            FriendData friendData4;
            if ((friendData4 = this.GetFriendData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            TimeSpan timeSpan1 = DateTime.Now - GameUtility.UnixtimeToLocalTime(friendData4.LastLogin);
            int days1 = timeSpan1.Days;
            if (days1 > 0)
            {
              this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_DAY", (object) days1.ToString()));
              break;
            }
            int hours1 = timeSpan1.Hours;
            if (hours1 > 0)
            {
              this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_HOUR", (object) hours1.ToString()));
              break;
            }
            this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_MINUTE", (object) timeSpan1.Minutes.ToString()));
            break;
          case GameParameter.ParameterTypes.PLAYER_MAXITEMS:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetItemSlotAmount());
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_SELLPRICE:
            SellItem sellItem1 = this.GetSellItem();
            if (sellItem1 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(sellItem1.item.Sell * sellItem1.num);
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_SELLNUM:
            SellItem sellItem2 = this.GetSellItem();
            if (sellItem2 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(sellItem2.num);
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_SELLINDEX:
            SellItem sellItem3 = this.GetSellItem();
            if (sellItem3 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(sellItem3.index + 1);
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_SELLSELECTCOUNT:
            List<SellItem> sellItemList1 = this.GetSellItemList();
            if (sellItemList1 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(sellItemList1.Count);
            break;
          case GameParameter.ParameterTypes.SHOP_SELLPRICETOTAL:
            List<SellItem> sellItemList2 = this.GetSellItemList();
            if (sellItemList2 == null)
            {
              this.ResetToDefault();
              break;
            }
            int num1 = 0;
            for (int index = 0; index < sellItemList2.Count; ++index)
              num1 += sellItemList2[index].item.Sell * sellItemList2[index].num;
            this.SetTextValue(num1);
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_STATE_INVENTORY:
            SellItem sellItem4 = this.GetSellItem();
            if (sellItem4 != null)
            {
              ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(sellItem4.item.Param.iname) != null);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_BUYAMOUNT:
            ShopItem shopItem1 = this.GetShopItem();
            if (shopItem1 != null)
            {
              this.SetTextValue(shopItem1.num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_BUYPRICE:
            LimitedShopItem limitedShopItem1 = this.GetLimitedShopItem();
            EventShopItem eventShopItem1 = this.GetEventShopItem();
            if (limitedShopItem1 != null && limitedShopItem1.isSetSaleValue)
            {
              this.SetTextValue(limitedShopItem1.saleType != ESaleType.Gold ? limitedShopItem1.saleValue.ToString() : CurrencyBitmapText.CreateFormatedText(limitedShopItem1.saleValue.ToString()));
              break;
            }
            if (eventShopItem1 != null && eventShopItem1.isSetSaleValue)
            {
              this.SetTextValue(eventShopItem1.saleType != ESaleType.Gold ? eventShopItem1.saleValue.ToString() : CurrencyBitmapText.CreateFormatedText(eventShopItem1.saleValue.ToString()));
              break;
            }
            ShopItem shopItem2 = this.GetShopItem();
            if (shopItem2 != null)
            {
              if (shopItem2.isSetSaleValue)
              {
                this.SetTextValue(shopItem2.saleType != ESaleType.Gold ? shopItem2.saleValue.ToString() : CurrencyBitmapText.CreateFormatedText(shopItem2.saleValue.ToString()));
                break;
              }
              string str6 = string.Empty;
              ItemParam itemParam15 = this.GetItemParam();
              if (itemParam15 != null)
              {
                switch (shopItem2.saleType)
                {
                  case ESaleType.Gold:
                    str6 = CurrencyBitmapText.CreateFormatedText((shopItem2.num * itemParam15.buy).ToString());
                    break;
                  case ESaleType.Coin:
                  case ESaleType.Coin_P:
                    str6 = (shopItem2.num * itemParam15.coin).ToString();
                    break;
                  case ESaleType.TourCoin:
                    str6 = (shopItem2.num * itemParam15.tour_coin).ToString();
                    break;
                  case ESaleType.ArenaCoin:
                    str6 = (shopItem2.num * itemParam15.arena_coin).ToString();
                    break;
                  case ESaleType.PiecePoint:
                    str6 = (shopItem2.num * itemParam15.piece_point).ToString();
                    break;
                  case ESaleType.MultiCoin:
                    str6 = (shopItem2.num * itemParam15.multi_coin).ToString();
                    break;
                  case ESaleType.EventCoin:
                    DebugUtility.Assert("There is no common price in the event coin.");
                    break;
                }
                this.SetTextValue(str6);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_STATE_SOLDOUT:
            ShopItem shopItem3 = this.GetShopItem();
            if (shopItem3 == null)
              break;
            ((Component) this).gameObject.SetActive(shopItem3.is_soldout);
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_BUYTYPEICON:
            LimitedShopItem limitedShopItem2 = this.GetLimitedShopItem();
            if (limitedShopItem2 != null && limitedShopItem2.saleType == ESaleType.EventCoin)
            {
              this.SetBuyPriceEventCoinTypeIcon(limitedShopItem2.cost_iname);
              break;
            }
            ShopItem shopItem4 = this.GetShopItem();
            if (shopItem4 != null)
            {
              this.SetBuyPriceTypeIcon(shopItem4.saleType);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_STATE_SELLSELECT:
            SellItem sellItem5 = this.GetSellItem();
            if (sellItem5 != null)
            {
              ((Component) this).gameObject.SetActive(sellItem5.index != -1);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_ICONSELLNUM:
            SellItem sellItem6 = this.GetSellItem();
            if (sellItem6 != null)
            {
              this.SetTextValue(-sellItem6.num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_STATE_ENABLEEQUIPUNIT:
            ItemParam itemParam16 = this.GetItemParam();
            if (itemParam16 != null)
            {
              List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
              for (int index1 = 0; index1 < units.Count; ++index1)
              {
                for (int index2 = 0; index2 < units[index1].Jobs.Length; ++index2)
                {
                  JobData job2 = units[index1].Jobs[index2];
                  if (job2.IsActivated)
                  {
                    int equipSlotByItemId = job2.FindEquipSlotByItemID(itemParam16.iname);
                    if (equipSlotByItemId != -1 && job2.CheckEnableEquipSlot(equipSlotByItemId))
                    {
                      ((Component) this).gameObject.SetActive(true);
                      return;
                    }
                  }
                }
              }
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_UPDATETIME:
            FixParam fixParam1 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (fixParam1.ShopUpdateTime != null)
            {
              DateTime serverTime = TimeManager.ServerTime;
              int num2 = (int) fixParam1.ShopUpdateTime[0];
              for (int index = 0; index < fixParam1.ShopUpdateTime.Length; ++index)
              {
                if (serverTime.Hour < (int) fixParam1.ShopUpdateTime[index])
                {
                  num2 = (int) fixParam1.ShopUpdateTime[index];
                  break;
                }
              }
              this.SetTextValue(num2.ToString().PadLeft(2, '0') + ":00");
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYER_FRIENDREQUESTNUM:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.FollowerNum);
            break;
          case GameParameter.ParameterTypes.UNIT_JOB_CLASSCHANGE_NAME:
            UnitData unitData37;
            if ((unitData37 = this.GetUnitData()) != null && unitData37.IsJobAvailable(this.Index))
            {
              JobParam classChangeJobParam = unitData37.GetClassChangeJobParam(this.Index);
              if (classChangeJobParam != null)
              {
                this.SetTextValue(classChangeJobParam.name);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_ICONSELLNUMSHOWED:
            SellItem sellItem7 = this.GetSellItem();
            if (sellItem7 != null)
            {
              ((Component) this).gameObject.SetActive(sellItem7.num != 0);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PLAYER_FRIENDMAX:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.FriendCap.ToString());
            break;
          case GameParameter.ParameterTypes.PLAYER_FRIENDNUM:
            PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
            if (player1.Friends == null)
              this.ResetToDefault();
            this.SetTextValue(player1.mFriendNum);
            break;
          case GameParameter.ParameterTypes.UNIT_PROFILETEXT:
            UnitData unitData38;
            if ((unitData38 = this.GetUnitData()) != null)
            {
              this.SetTextValue(LocalizedText.Get("unit." + unitData38.UnitParam.iname + "_PROFILE"));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_IMAGE:
            UnitData unitData39;
            if ((unitData39 = this.GetUnitData()) != null && !string.IsNullOrEmpty(unitData39.UnitParam.image))
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
              string str7 = AssetPath.UnitSkinImage(unitData39.UnitParam, unitData39.GetSelectedSkin(), unitData39.CurrentJob.JobID);
              if (!string.IsNullOrEmpty(str7))
              {
                iconLoader.ResourcePath = str7;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_LIST_PLAYER_NUM_MAX:
            MultiPlayAPIRoom room7 = this.GetRoom();
            QuestParam quest2 = room7 == null || room7.quest == null || string.IsNullOrEmpty(room7.quest.iname) ? (QuestParam) null : MonoSingleton<GameManager>.Instance.FindQuest(room7.quest.iname);
            if (quest2 == null)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue((int) quest2.playerNum);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_ICON_FRAME:
            JSON_MyPhotonRoomParam roomParam1 = this.GetRoomParam();
            int unitSlotNum1 = roomParam1 != null ? roomParam1.GetUnitSlotNum() : 0;
            ((Component) this).gameObject.SetActive(0 <= this.Index && this.Index < unitSlotNum1);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_INDEX:
            JSON_MyPhotonPlayerParam roomPlayerParam4 = this.GetRoomPlayerParam();
            if (roomPlayerParam4 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(roomPlayerParam4.playerIndex);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_IS_ROOM_OWNER:
            MyPhoton instance14 = PunMonoSingleton<MyPhoton>.Instance;
            JSON_MyPhotonPlayerParam roomPlayerParam5 = this.GetRoomPlayerParam();
            List<MyPhoton.MyPlayer> roomPlayerList1 = instance14.GetRoomPlayerList();
            MyPhoton.MyPlayer player2 = roomPlayerParam5 == null ? (MyPhoton.MyPlayer) null : instance14.FindPlayer(roomPlayerList1, roomPlayerParam5.playerID, roomPlayerParam5.playerIndex);
            ((Component) this).gameObject.SetActive(player2 != null && instance14.IsHost(player2.playerID));
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_IS_EMPTY:
            JSON_MyPhotonPlayerParam roomPlayerParam6 = this.GetRoomPlayerParam();
            if (roomPlayerParam6 != null)
            {
              ((Component) this).gameObject.SetActive(roomPlayerParam6 != null && roomPlayerParam6.playerID <= 0);
              break;
            }
            ((Component) this).gameObject.SetActive(true);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_IS_VALID:
            JSON_MyPhotonPlayerParam roomPlayerParam7 = this.GetRoomPlayerParam();
            ((Component) this).gameObject.SetActive(roomPlayerParam7 != null && roomPlayerParam7.playerID > 0);
            break;
          case GameParameter.ParameterTypes.TROPHY_NAME:
            TrophyParam dataOfClass27 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
            if (dataOfClass27 != null)
            {
              this.SetTextValue(dataOfClass27.Name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_TYPE_IS_RAID:
            ((Component) this).gameObject.SetActive(GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID);
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_TYPE_IS_VERSUS:
            ((Component) this).gameObject.SetActive(GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS);
            break;
          case GameParameter.ParameterTypes.MULTI_PARTY_TOTALATK:
            JSON_MyPhotonRoomParam roomParam2 = this.GetRoomParam();
            JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
            if (multiPlayerParam == null || multiPlayerParam.units == null || roomParam2 == null)
            {
              this.ResetToDefault();
              break;
            }
            int num3 = 0;
            int unitSlotNum2 = roomParam2.GetUnitSlotNum(multiPlayerParam.playerIndex);
            for (int index = 0; index < multiPlayerParam.units.Length; ++index)
            {
              if (multiPlayerParam.units[index].slotID < unitSlotNum2 && multiPlayerParam.units[index].unit != null)
                num3 = num3 + (int) multiPlayerParam.units[index].unit.Status.param.atk + (int) multiPlayerParam.units[index].unit.Status.param.mag;
            }
            this.SetTextValue(num3);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_PLAYER_INDEX:
            SceneBattle instance15 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance15, (UnityEngine.Object) null) || instance15.Battle == null || instance15.Battle.CurrentUnit == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance15.Battle.CurrentUnit.OwnerPlayerIndex);
            break;
          case GameParameter.ParameterTypes.MULTI_MY_NEXT_TURN:
            SceneBattle instance16 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance16, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance16.GetNextMyTurn());
            break;
          case GameParameter.ParameterTypes.MULTI_INPUT_TIME_LIMIT:
            SceneBattle instance17 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance17, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance17.DisplayMultiPlayInputTimeLimit);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_PLAYER_NAME:
            SceneBattle bs1 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs1, (UnityEngine.Object) null) || bs1.Battle == null || bs1.Battle.CurrentUnit == null)
            {
              this.ResetToDefault();
              break;
            }
            JSON_MyPhotonPlayerParam photonPlayerParam1 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs1.Battle.CurrentUnit.OwnerPlayerIndex));
            if (photonPlayerParam1 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(photonPlayerParam1.playerName);
            break;
          case GameParameter.ParameterTypes.QUEST_MULTI_LOCK:
            ((Component) this).gameObject.SetActive(MultiPlayAPIRoom.IsLocked(GlobalVars.EditMultiPlayRoomPassCode));
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_COMMENT:
            MyPhoton.MyRoom currentRoom1 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            JSON_MyPhotonRoomParam myPhotonRoomParam1 = currentRoom1 != null ? JSON_MyPhotonRoomParam.Parse(currentRoom1.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam1 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(myPhotonRoomParam1.comment);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_PASSCODE:
            MyPhoton.MyRoom currentRoom2 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            JSON_MyPhotonRoomParam myPhotonRoomParam2 = currentRoom2 != null ? JSON_MyPhotonRoomParam.Parse(currentRoom2.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam2 == null || !MultiPlayAPIRoom.IsLocked(myPhotonRoomParam2.passCode))
            {
              this.ResetToDefault();
              break;
            }
            string input = string.Format("{0:D5}", (object) myPhotonRoomParam2.roomid);
            if (this.Index == 1)
              input = Regex.Replace(input, "[0-9]", (MatchEvaluator) (m => ((char) (65296 + ((int) m.Value[0] - 48))).ToString()));
            this.SetTextValue(input);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE:
            bool flag1 = false;
            if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
            {
              JSON_MyPhotonRoomParam roomParam3 = this.GetRoomParam();
              if (roomParam3 == null || !string.IsNullOrEmpty(roomParam3.iname))
              {
                JSON_MyPhotonPlayerParam roomPlayerParam8 = this.GetRoomPlayerParam();
                int playerIndex = roomPlayerParam8 != null ? roomPlayerParam8.playerIndex : 0;
                flag1 = this.Index >= (roomParam3 != null ? roomParam3.GetUnitSlotNum(playerIndex) : 0);
              }
            }
            ((Component) this).gameObject.SetActive(flag1);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_QUEST_NAME:
            JSON_MyPhotonRoomParam roomParam4 = this.GetRoomParam();
            if (roomParam4 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(MonoSingleton<GameManager>.Instance.FindQuest(roomParam4.iname).name);
            break;
          case GameParameter.ParameterTypes.QUEST_IS_MULTI:
            GameUtility.EScene currentScene1 = GameUtility.GetCurrentScene();
            bool flag2 = currentScene1 == GameUtility.EScene.HOME_MULTI || currentScene1 == GameUtility.EScene.BATTLE_MULTI;
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(!flag2);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else if (this.Index == 1)
            {
              ((Component) this).gameObject.SetActive(flag2);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              Button component = ((Component) this).gameObject.GetComponent<Button>();
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                this.ResetToDefault();
                goto case GameParameter.ParameterTypes.SKILL_ICON;
              }
              else if (this.Index == 2)
              {
                ((Selectable) component).interactable = !flag2;
                goto case GameParameter.ParameterTypes.SKILL_ICON;
              }
              else if (this.Index == 3)
              {
                ((Selectable) component).interactable = flag2;
                goto case GameParameter.ParameterTypes.SKILL_ICON;
              }
              else
                goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.TROPHY_CONDITION_TITLE:
            if (this.InstanceType == 0)
            {
              TrophyObjectiveData dataOfClass28 = DataSource.FindDataOfClass<TrophyObjectiveData>(((Component) this).gameObject, (TrophyObjectiveData) null);
              if (dataOfClass28 != null)
              {
                this.SetTextValue(dataOfClass28.Description);
                break;
              }
            }
            else
            {
              TrophyParam dataOfClass29 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
              if (dataOfClass29 != null && 0 <= this.Index && this.Index < dataOfClass29.Objectives.Length)
              {
                this.SetTextValue(dataOfClass29.Objectives[this.Index].GetDescription());
                break;
              }
            }
            this.ResetToDefault();
            goto case GameParameter.ParameterTypes.SKILL_ICON;
          case GameParameter.ParameterTypes.TROPHY_CONDITION_COUNT:
            if (this.InstanceType == 0)
            {
              TrophyObjectiveData dataOfClass30 = DataSource.FindDataOfClass<TrophyObjectiveData>(((Component) this).gameObject, (TrophyObjectiveData) null);
              if (dataOfClass30 != null)
              {
                if (dataOfClass30.Objective.type == TrophyConditionTypes.up_conceptcard_trust || dataOfClass30.Objective.type == TrophyConditionTypes.up_conceptcard_trust_target)
                {
                  float num4 = Mathf.Min((float) dataOfClass30.Count / 100f, (float) dataOfClass30.CountMax / 100f);
                  this.SetTextValue(string.Format("{0:0.0}", (object) num4));
                  this.SetSliderValue((int) num4, dataOfClass30.CountMax);
                  break;
                }
                int num5 = Mathf.Min(dataOfClass30.Count, dataOfClass30.CountMax);
                this.SetTextValue(num5);
                this.SetSliderValue(num5, dataOfClass30.CountMax);
                break;
              }
            }
            else
            {
              TrophyParam dataOfClass31 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
              if (dataOfClass31 != null && 0 <= this.Index && this.Index < dataOfClass31.Objectives.Length)
              {
                instance1 = MonoSingleton<GameManager>.Instance;
                TrophyState trophyCounter = dataOfClass31.GetTrophyCounter();
                if (trophyCounter == null || this.Index >= trophyCounter.Count.Length)
                  break;
                if (dataOfClass31.Objectives[this.Index].type == TrophyConditionTypes.up_conceptcard_trust || dataOfClass31.Objectives[this.Index].type == TrophyConditionTypes.up_conceptcard_trust_target)
                {
                  float num6 = Mathf.Min((float) trophyCounter.Count[this.Index] / 100f, (float) dataOfClass31.Objectives[this.Index].RequiredCount / 100f);
                  this.SetTextValue(string.Format("{0:0.0}", (object) num6));
                  this.SetSliderValue((int) num6, dataOfClass31.Objectives[this.Index].RequiredCount / 100);
                  break;
                }
                int num7 = Mathf.Min(trophyCounter.Count[this.Index], dataOfClass31.Objectives[this.Index].RequiredCount);
                this.SetTextValue(num7);
                this.SetSliderValue(num7, dataOfClass31.Objectives[this.Index].RequiredCount);
                break;
              }
            }
            this.ResetToDefault();
            goto case GameParameter.ParameterTypes.SKILL_ICON;
          case GameParameter.ParameterTypes.TROPHY_CONDITION_COUNTMAX:
            if (this.InstanceType == 0)
            {
              TrophyObjectiveData dataOfClass32 = DataSource.FindDataOfClass<TrophyObjectiveData>(((Component) this).gameObject, (TrophyObjectiveData) null);
              if (dataOfClass32 != null)
              {
                if (dataOfClass32.Objective.type == TrophyConditionTypes.up_conceptcard_trust || dataOfClass32.Objective.type == TrophyConditionTypes.up_conceptcard_trust_target)
                {
                  this.SetTextValue(string.Format("{0:0.0}", (object) ((float) dataOfClass32.CountMax / 100f)));
                  break;
                }
                this.SetTextValue(dataOfClass32.CountMax);
                break;
              }
            }
            else
            {
              TrophyParam dataOfClass33 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
              if (dataOfClass33 != null && 0 <= this.Index && this.Index < dataOfClass33.Objectives.Length)
              {
                if (dataOfClass33.Objectives[this.Index].type == TrophyConditionTypes.up_conceptcard_trust || dataOfClass33.Objectives[this.Index].type == TrophyConditionTypes.up_conceptcard_trust_target)
                {
                  this.SetTextValue(string.Format("{0:0.0}", (object) ((float) dataOfClass33.Objectives[this.Index].ival / 100f)));
                  break;
                }
                this.SetTextValue(dataOfClass33.Objectives[this.Index].RequiredCount);
                break;
              }
            }
            this.ResetToDefault();
            goto case GameParameter.ParameterTypes.SKILL_ICON;
          case GameParameter.ParameterTypes.ITEM_ENHANCEPOINT:
            ItemParam itemParam17 = this.GetItemParam();
            if (itemParam17 != null)
            {
              this.SetTextValue(itemParam17.enhace_point);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_MATERIALSELECTCOUNT:
            EnhanceMaterial enhanceMaterial1 = this.GetEnhanceMaterial();
            if (enhanceMaterial1 != null)
            {
              this.SetTextValue(enhanceMaterial1.num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_SHOWED_AMOUNT:
            ItemParam itemParam18 = this.InstanceType == 1 ? this.GetInventoryItemParam() : this.GetItemParam();
            if (itemParam18 != null)
            {
              ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.GetItemAmount(itemParam18.iname) > 0);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_PARAMETER_NAME:
            EquipItemParameter equipItemParameter1 = this.GetEquipItemParameter();
            if (equipItemParameter1 != null)
            {
              BuffEffect buffEffect = equipItemParameter1.equip.Skill.GetBuffEffect();
              int paramIndex = equipItemParameter1.param_index;
              this.SetTextValue(this.GetParamTypeName(buffEffect.targets[paramIndex].paramType, (string) buffEffect.targets[paramIndex].tokkou));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_PARAMETER_INITVALUE:
            EquipItemParameter equipItemParameter2 = this.GetEquipItemParameter();
            if (equipItemParameter2 != null)
            {
              int paramIndex = equipItemParameter2.param_index;
              SkillData skill = equipItemParameter2.equip.Skill;
              if (skill != null)
              {
                BuffEffect buffEffect = skill.GetBuffEffect();
                if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
                {
                  this.SetTextValue((int) buffEffect.targets[paramIndex].value);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_PARAMETER_RANKUPVALUE:
            EquipItemParameter equipItemParameter3 = this.GetEquipItemParameter();
            if (equipItemParameter3 != null)
            {
              int paramIndex = equipItemParameter3.param_index;
              int num8 = 0;
              SkillData skill = equipItemParameter3.equip.Skill;
              if (skill != null)
              {
                BuffEffect buffEffect = skill.GetBuffEffect();
                if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
                  num8 = buffEffect == null ? 0 : (int) buffEffect.targets[paramIndex].value - buffEffect.param.buffs[paramIndex].value_ini;
              }
              if (num8 != 0)
              {
                this.SetTextValue("(+" + num8.ToString() + ")");
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_PARAMETER_SHOWED_RANKUPVALUE:
            EquipItemParameter equipItemParameter4 = this.GetEquipItemParameter();
            if (equipItemParameter4 != null)
            {
              int paramIndex = equipItemParameter4.param_index;
              int num9 = 0;
              SkillData skill = equipItemParameter4.equip.Skill;
              if (skill != null)
              {
                BuffEffect buffEffect = skill.GetBuffEffect();
                if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
                  num9 = buffEffect == null ? 0 : (int) buffEffect.targets[paramIndex].value - buffEffect.param.buffs[paramIndex].value_ini;
              }
              if (num9 != 0)
              {
                ((Component) this).gameObject.SetActive(num9 > 0);
                break;
              }
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECTCOUNT:
            EnhanceMaterial enhanceMaterial2 = this.GetEnhanceMaterial();
            if (enhanceMaterial2 != null)
            {
              ((Component) this).gameObject.SetActive(enhanceMaterial2.num > 0);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECT:
            EnhanceMaterial enhanceMaterial3 = this.GetEnhanceMaterial();
            if (enhanceMaterial3 != null)
            {
              ((Component) this).gameObject.SetActive(enhanceMaterial3.selected);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_GAUGE:
            EnhanceEquipData enhanceEquipData1;
            if ((enhanceEquipData1 = this.GetEnhanceEquipData()) != null && enhanceEquipData1.is_enhanced)
            {
              EquipData equip = enhanceEquipData1.equip;
              int current = equip.Exp + enhanceEquipData1.gainexp;
              int num10 = equip.CalcRankFromExp(current);
              int rankCap = equip.GetRankCap();
              int num11 = num10 >= rankCap ? equip.GetNextExp(rankCap) : equip.GetExpFromExp(current);
              int nextExp = equip.GetNextExp(num10 >= rankCap ? rankCap : num10 + 1);
              this.SetTextValue(num11);
              this.SetSliderValue(num11, nextExp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_CURRENTEXP:
            EnhanceEquipData enhanceEquipData2;
            if ((enhanceEquipData2 = this.GetEnhanceEquipData()) != null)
            {
              EquipData equip = enhanceEquipData2.equip;
              int current = equip.Exp + enhanceEquipData2.gainexp;
              int num12 = equip.CalcRankFromExp(current);
              int rankCap = equip.GetRankCap();
              this.SetTextValue(num12 >= rankCap ? equip.GetNextExp(rankCap) : equip.GetExpFromExp(current));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_NEXTEXP:
            EnhanceEquipData enhanceEquipData3;
            if ((enhanceEquipData3 = this.GetEnhanceEquipData()) != null)
            {
              EquipData equip = enhanceEquipData3.equip;
              int current = equip.Exp + enhanceEquipData3.gainexp;
              int num13 = equip.CalcRankFromExp(current);
              int rankCap = equip.GetRankCap();
              this.SetTextValue(equip.GetNextExp(num13 >= rankCap ? rankCap : num13 + 1));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_RANKBEFORE:
            EnhanceEquipData enhanceEquipData4;
            if ((enhanceEquipData4 = this.GetEnhanceEquipData()) != null)
            {
              this.SetTextValue(enhanceEquipData4.equip.Rank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPITEM_ENHANCE_RANKAFTER:
            EnhanceEquipData enhanceEquipData5;
            if ((enhanceEquipData5 = this.GetEnhanceEquipData()) != null)
            {
              EquipData equip = enhanceEquipData5.equip;
              this.SetTextValue(equip.CalcRankFromExp(equip.Exp + enhanceEquipData5.gainexp));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPDATA_RANKBADGE:
            EquipData equipData25;
            if ((equipData25 = this.GetEquipData()) != null)
            {
              int num14 = equipData25.Rank - 1;
              if (num14 > 0)
              {
                int index = num14 - 1;
                ((Component) this).gameObject.SetActive(true);
                this.SetAnimatorInt("rare", index);
                this.SetImageIndex(index);
                break;
              }
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.DONTUSE_UNLOCK_SHOWED:
            ((Component) this).gameObject.SetActive(this.CheckUnlockInstanceType());
            break;
          case GameParameter.ParameterTypes.MULTI_NOTIFY_DISCONNECTED_PLAYER_INDEX:
            SceneBattle instance18 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance18, (UnityEngine.Object) null) || instance18.CurrentNotifyDisconnectedPlayer == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance18.CurrentNotifyDisconnectedPlayer.playerIndex);
            break;
          case GameParameter.ParameterTypes.MULTI_NOTIFY_DISCONNECTED_PLAYER_IS_ROOM_OWNER:
            SceneBattle instance19 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance19, (UnityEngine.Object) null) || instance19.CurrentNotifyDisconnectedPlayer == null)
            {
              this.ResetToDefault();
              break;
            }
            ((Component) this).gameObject.SetActive(instance19.CurrentNotifyDisconnectedPlayerType == (SceneBattle.ENotifyDisconnectedPlayerType) this.Index);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_PLAYER_IS_DISCONNECTED:
            SceneBattle bs2 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs2, (UnityEngine.Object) null) || bs2.Battle == null || bs2.Battle.CurrentUnit == null)
            {
              this.ResetToDefault();
              break;
            }
            GameManager instance20 = MonoSingleton<GameManager>.Instance;
            MyPhoton instance21 = PunMonoSingleton<MyPhoton>.Instance;
            JSON_MyPhotonPlayerParam photonPlayerParam2 = instance21.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs2.Battle.CurrentUnit.OwnerPlayerIndex));
            List<MyPhoton.MyPlayer> roomPlayerList2 = instance21.GetRoomPlayerList();
            MyPhoton.MyPlayer player3 = photonPlayerParam2 != null ? instance21.FindPlayer(roomPlayerList2, photonPlayerParam2.playerID, photonPlayerParam2.playerIndex) : (MyPhoton.MyPlayer) null;
            if (instance20.AudienceMode || instance20.IsVSCpuBattle)
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(player3 == null);
              break;
            }
            if (this.Index == 1)
            {
              ((Component) this).gameObject.SetActive(player3 != null);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_PLAYER_IS_ROOM_OWNER:
            SceneBattle bs3 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs3, (UnityEngine.Object) null) || bs3.Battle == null || bs3.Battle.CurrentUnit == null)
            {
              this.ResetToDefault();
              break;
            }
            MyPhoton instance22 = PunMonoSingleton<MyPhoton>.Instance;
            JSON_MyPhotonPlayerParam photonPlayerParam3 = instance22.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs3.Battle.CurrentUnit.OwnerPlayerIndex));
            ((Component) this).gameObject.SetActive(photonPlayerParam3 != null && instance22.IsOldestPlayer(photonPlayerParam3.playerID));
            break;
          case GameParameter.ParameterTypes.MULTI_I_AM_ROOM_OWNER:
            MyPhoton instance23 = PunMonoSingleton<MyPhoton>.Instance;
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(instance23.IsOldestPlayer());
              break;
            }
            ((Component) this).gameObject.SetActive(!instance23.IsOldestPlayer());
            break;
          case GameParameter.ParameterTypes.MULTI_ROOM_OWNER_PLAYER_INDEX:
            this.SetTextValue(PunMonoSingleton<MyPhoton>.Instance.GetOldestPlayer());
            break;
          case GameParameter.ParameterTypes.GACHA_DROPNAME:
            GachaResultData_old dataOfClass34 = DataSource.FindDataOfClass<GachaResultData_old>(((Component) this).gameObject, (GachaResultData_old) null);
            if (dataOfClass34 == null)
              break;
            this.SetTextValue(dataOfClass34.Name);
            break;
          case GameParameter.ParameterTypes.TROPHY_BADGE:
            List<TrophyState> trophyStatesList = MonoSingleton<GameManager>.Instance.Player.TrophyData.TrophyStatesList;
            bool flag3 = false;
            switch (this.InstanceType)
            {
              case 0:
                for (int index = 0; index < trophyStatesList.Count; ++index)
                {
                  if (trophyStatesList[index].Param.IsShowBadge(trophyStatesList[index]))
                  {
                    flag3 = true;
                    break;
                  }
                }
                break;
              case 1:
                for (int index = 0; index < trophyStatesList.Count; ++index)
                {
                  if (!trophyStatesList[index].Param.IsDaily && trophyStatesList[index].Param.IsShowBadge(trophyStatesList[index]))
                  {
                    flag3 = true;
                    break;
                  }
                }
                break;
            }
            ((Component) this).gameObject.SetActive(flag3);
            break;
          case GameParameter.ParameterTypes.TROPHY_REWARDGOLD:
            TrophyParam trophyParam1 = this.GetTrophyParam();
            if (trophyParam1 == null)
              break;
            this.SetTextValue(trophyParam1.Gold);
            ((Component) this).gameObject.SetActive(trophyParam1.Gold > 0);
            break;
          case GameParameter.ParameterTypes.TROPHY_REWARDCOIN:
            TrophyParam trophyParam2 = this.GetTrophyParam();
            if (trophyParam2 == null)
              break;
            this.SetTextValue(trophyParam2.Coin);
            ((Component) this).gameObject.SetActive(trophyParam2.Coin > 0);
            break;
          case GameParameter.ParameterTypes.TROPHY_REWARDEXP:
            TrophyParam trophyParam3 = this.GetTrophyParam();
            if (trophyParam3 == null)
              break;
            this.SetTextValue(trophyParam3.Exp);
            ((Component) this).gameObject.SetActive(trophyParam3.Exp > 0);
            break;
          case GameParameter.ParameterTypes.REWARD_EXP:
            RewardData dataOfClass35 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass35 != null)
            {
              this.SetTextValue(dataOfClass35.Exp);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.REWARD_COIN:
            RewardData dataOfClass36 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass36 != null)
            {
              this.SetTextValue(dataOfClass36.Coin);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.REWARD_GOLD:
            RewardData dataOfClass37 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass37 != null)
            {
              this.SetTextValue(dataOfClass37.Gold);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_FAVORITE:
            UnitData unitData40 = this.GetUnitData();
            if (unitData40 != null)
            {
              ((Component) this).gameObject.SetActive(unitData40.IsFavorite);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.EQUIPDATA_FRAME:
            EquipData equipData26 = this.GetEquipData();
            if (equipData26 != null && equipData26.IsEquiped())
            {
              this.SetEquipItemFrame(equipData26.ItemParam);
              break;
            }
            this.SetEquipItemFrame((ItemParam) null);
            break;
          case GameParameter.ParameterTypes.UNIT_JOBRANKFRAME:
            UnitData unitData41;
            if ((unitData41 = this.GetUnitData()) != null)
            {
              this.SetImageIndex(unitData41.CurrentJob.Rank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_CAPPEDLEVELMAX:
            UnitData unitData42;
            if ((unitData42 = this.GetUnitData()) != null)
            {
              GameManager instance24 = MonoSingleton<GameManager>.Instance;
              int num15 = unitData42.GetLevelCap();
              if (unitData42.IsMyUnit)
              {
                int lv = instance24.Player.Lv;
                num15 = Mathf.Min(num15, lv);
              }
              this.SetTextValue(num15);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.APPLICATION_REVISION:
            TextAsset textAsset1 = Resources.Load<TextAsset>("revision");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) textAsset1, (UnityEngine.Object) null))
            {
              this.SetTextValue(textAsset1.text);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.APPLICATION_BUILD:
            TextAsset textAsset2 = Resources.Load<TextAsset>("build");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) textAsset2, (UnityEngine.Object) null))
            {
              this.SetTextValue(textAsset2.text);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.APPLICATION_ASSETREVISION:
            int assetRevision = AssetManager.AssetRevision;
            if (assetRevision > 0)
            {
              this.SetTextValue(assetRevision.ToString());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PRODUCT_NAME:
            PaymentManager.Product dataOfClass38 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
            if (dataOfClass38 == null || string.IsNullOrEmpty(dataOfClass38.name))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(dataOfClass38.name);
            break;
          case GameParameter.ParameterTypes.PRODUCT_PRICE:
            PaymentManager.Product dataOfClass39 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
            if (dataOfClass39 == null || string.IsNullOrEmpty(dataOfClass39.price))
            {
              this.ResetToDefault();
              break;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(dataOfClass39.ID).IsShopOpen())
            {
              this.SetTextValue(dataOfClass39.price);
              break;
            }
            GameUtility.SetGameObjectActive((Component) this.mText, false);
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_RANK:
            ArenaPlayer arenaPlayer1 = this.GetArenaPlayer();
            if (arenaPlayer1 != null && arenaPlayer1.ArenaRank > 0)
            {
              this.SetTextValue(arenaPlayer1.ArenaRank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_TOTALATK:
            ArenaPlayer arenaPlayer2 = this.GetArenaPlayer();
            if (arenaPlayer2 != null)
            {
              this.SetTextValue(arenaPlayer2.TotalAtk);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_LEADERSKILLNAME:
            ArenaPlayer arenaPlayer3 = this.GetArenaPlayer();
            if (arenaPlayer3 != null && arenaPlayer3.Unit[0] != null && arenaPlayer3.Unit[0].CurrentLeaderSkill != null)
            {
              this.SetTextValue(arenaPlayer3.Unit[0].CurrentLeaderSkill.Name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_LEADERSKILLDESC:
            ArenaPlayer arenaPlayer4 = this.GetArenaPlayer();
            if (arenaPlayer4 != null && arenaPlayer4.Unit[0] != null && arenaPlayer4.Unit[0].CurrentLeaderSkill != null)
            {
              this.SetTextValue(arenaPlayer4.Unit[0].CurrentLeaderSkill.SkillParam.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_NAME:
            ArenaPlayer arenaPlayer5 = this.GetArenaPlayer();
            if (arenaPlayer5 != null)
            {
              this.SetTextValue(arenaPlayer5.PlayerName);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_ARENARANK:
            GameManager instance25 = MonoSingleton<GameManager>.Instance;
            if (instance25.Player.ArenaRank > 0)
            {
              this.SetTextValue(instance25.Player.ArenaRank);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_TICKET:
            QuestParam questParam12;
            if ((questParam12 = this.GetQuestParam()) != null && !string.IsNullOrEmpty(questParam12.ticket))
            {
              ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(questParam12.ticket);
              if (itemDataByItemId != null)
              {
                this.SetTextValue(itemDataByItemId.Num);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_IS_TICKET:
            QuestParam questParam13;
            if ((questParam13 = this.GetQuestParam()) != null && questParam13.state == QuestStates.Cleared && !string.IsNullOrEmpty(questParam13.ticket))
            {
              ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(questParam13.ticket);
              if (itemDataByItemId != null)
              {
                ((Component) this).gameObject.SetActive(itemDataByItemId.Num > 0);
                break;
              }
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_ARENATICKETS:
            GameManager instance26 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance26.Player.ChallengeArenaNum);
            this.SetSliderValue(instance26.Player.ChallengeArenaNum, instance26.Player.ChallengeArenaMax);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_ARENACOOLDOWNTIME:
            GameManager instance27 = MonoSingleton<GameManager>.Instance;
            instance27.Player.UpdateChallengeArenaTimer();
            long arenaCoolDownSec = instance27.Player.GetNextChallengeArenaCoolDownSec();
            long num16 = arenaCoolDownSec / 60L;
            long num17 = arenaCoolDownSec % 60L;
            this.SetTextValue(string.Format(LocalizedText.Get("sys.ARENA_COOLDOWN"), (object) num16, (object) num17));
            this.SetUpdateInterval(0.25f);
            break;
          case GameParameter.ParameterTypes.MULTI_REST_REWARD:
            string format1 = LocalizedText.Get("sys.MP_REST_REWARD");
            if (string.IsNullOrEmpty(format1))
            {
              this.ResetToDefault();
              break;
            }
            PlayerData player4 = MonoSingleton<GameManager>.Instance.Player;
            int num18 = Math.Max(player4.ChallengeMultiMax - player4.ChallengeMultiNum, 0);
            this.SetTextValue(string.Format(format1, (object) num18));
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE_NOT_INTERACTIVE:
            bool flag4 = false;
            if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
            {
              JSON_MyPhotonRoomParam roomParam5 = this.GetRoomParam();
              JSON_MyPhotonPlayerParam roomPlayerParam9 = this.GetRoomPlayerParam();
              int playerIndex = roomPlayerParam9 != null ? roomPlayerParam9.playerIndex : 0;
              flag4 = this.Index >= roomParam5.GetUnitSlotNum(playerIndex);
            }
            Button component1 = ((Component) this).gameObject.GetComponent<Button>();
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
              break;
            ((Selectable) component1).interactable = !flag4;
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_OKYAKUSAMACODE:
            string configOkyakusamaCode1 = GameUtility.Config_OkyakusamaCode;
            if (string.IsNullOrEmpty(configOkyakusamaCode1))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(LocalizedText.Get("sys.OKYAKUSAMACODE", (object) configOkyakusamaCode1));
            break;
          case GameParameter.ParameterTypes.REWARD_ARENAMEDAL:
            RewardData dataOfClass40 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass40 != null)
            {
              this.SetTextValue(dataOfClass40.ArenaMedal);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_UPDATEDAY:
            FixParam fixParam2 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (fixParam2.ShopUpdateTime != null)
            {
              DateTime serverTime = TimeManager.ServerTime;
              for (int index = 0; index < fixParam2.ShopUpdateTime.Length; ++index)
              {
                if (serverTime.Hour < (int) fixParam2.ShopUpdateTime[index])
                {
                  this.SetTextValue(LocalizedText.Get("sys.TODAY"));
                  return;
                }
              }
              this.SetTextValue(LocalizedText.Get("sys.TOMORROW"));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARENAPLAYER_LEVEL:
            ArenaPlayer arenaPlayer6 = this.GetArenaPlayer();
            if (arenaPlayer6 != null)
            {
              this.SetTextValue(arenaPlayer6.PlayerLevel);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_VIPRANK:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.VipRank);
            break;
          case GameParameter.ParameterTypes.UNIT_EQUIPSLOT_UPDATE:
            UnitData unitData43;
            if ((unitData43 = this.GetUnitData()) == null)
              break;
            UnitEquipmentSlotEvents component2 = ((Component) this).gameObject.GetComponent<UnitEquipmentSlotEvents>();
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              break;
            int index3 = this.Index;
            EquipData[] rankupEquips = unitData43.GetRankupEquips(unitData43.JobIndex);
            if (!rankupEquips[index3].IsValid())
            {
              ((Component) component2).gameObject.SetActive(false);
              break;
            }
            ((Component) component2).gameObject.SetActive(true);
            ItemParam itemParam19 = rankupEquips[index3].ItemParam;
            DataSource.Bind<ItemParam>(((Component) component2).gameObject, itemParam19);
            DataSource.Bind<EquipData>(((Component) component2).gameObject, rankupEquips[index3]);
            if (rankupEquips[index3].IsEquiped())
            {
              component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.Equipped;
              break;
            }
            if (MonoSingleton<GameManager>.Instance.Player.HasItem(itemParam19.iname))
            {
              if (itemParam19.equipLv > unitData43.Lv)
              {
                component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.NeedMoreLevel;
                break;
              }
              component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.HasEquipment;
              break;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckCreateItem(itemParam19) == CreateItemResult.CanCreate)
            {
              if (itemParam19.equipLv > unitData43.Lv)
              {
                component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.EnableCraftNeedMoreLevel;
                break;
              }
              component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.EnableCraft;
              break;
            }
            if (unitData43.CheckCommon(unitData43.JobIndex, index3))
            {
              if (itemParam19.equipLv > unitData43.Lv)
              {
                component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.EnableCommonSoul;
                break;
              }
              component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.EnableCommon;
              break;
            }
            component2.StateType = UnitEquipmentSlotEvents.SlotStateTypes.Empty;
            break;
          case GameParameter.ParameterTypes.UNITPARAM_ICON:
            UnitParam unitParam5;
            if ((unitParam5 = this.GetUnitParam()) != null)
            {
              string str8 = AssetPath.UnitIconSmall(unitParam5, unitParam5.GetJobId(0));
              if (!string.IsNullOrEmpty(str8))
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str8;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_RARITY:
            UnitParam unitParam6;
            if ((unitParam6 = this.GetUnitParam()) != null)
            {
              this.SetAnimatorInt("rare", (int) unitParam6.rare);
              this.SetImageIndex((int) unitParam6.rare);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_JOBICON:
            UnitParam unitParam7;
            if ((unitParam7 = this.GetUnitParam()) != null)
            {
              string str9 = GameUtility.ComposeJobIconPath(unitParam7);
              if (!string.IsNullOrEmpty(str9))
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str9;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_PIECE_AMOUNT:
            UnitParam unitParam8;
            if ((unitParam8 = this.GetUnitParam()) != null)
            {
              this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetItemAmount(unitParam8.piece));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_PIECE_NEED:
            UnitParam unitParam9;
            if ((unitParam9 = this.GetUnitParam()) != null)
            {
              this.SetTextValue(unitParam9.GetUnlockNeedPieces());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_PIECE_GAUGE:
            UnitParam unitParam10;
            if ((unitParam10 = this.GetUnitParam()) != null)
            {
              GameManager instance28 = MonoSingleton<GameManager>.Instance;
              int unlockNeedPieces = unitParam10.GetUnlockNeedPieces();
              int num19 = Math.Min(instance28.Player.GetItemAmount(unitParam10.piece), unlockNeedPieces);
              this.SetTextValue(num19);
              this.SetSliderValue(num19, unlockNeedPieces);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNITPARAM_IS_UNLOCKED:
            UnitParam unitParam11;
            if ((unitParam11 = this.GetUnitParam()) != null)
            {
              GameManager instance29 = MonoSingleton<GameManager>.Instance;
              int unlockNeedPieces = unitParam11.GetUnlockNeedPieces();
              ((Component) this).gameObject.SetActive(instance29.Player.GetItemAmount(unitParam11.piece) >= unlockNeedPieces);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.QUEST_KAKERA_FRAME:
            QuestParam questParam14;
            if ((questParam14 = this.GetQuestParam()) != null)
            {
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
                break;
              ItemParam hardDropPiece = QuestDropParam.Instance.GetHardDropPiece(questParam14.iname, GlobalVars.GetDropTableGeneratedDateTime());
              if (hardDropPiece != null)
              {
                this.SetItemFrame(hardDropPiece);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_COMBINATION:
            UnitData unitData44;
            if ((unitData44 = this.GetUnitData()) != null)
            {
              this.SetTextValue(unitData44.GetCombination());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_JOBCHANGED:
            UnitData unitData45;
            if ((unitData45 = this.GetUnitData()) != null)
            {
              JobData currentJob = unitData45.CurrentJob;
              ((Component) this).gameObject.SetActive(currentJob != null && currentJob.IsActivated);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.SHOP_STATE_MAINCOSTFRAME:
            ShopData shopData1 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (shopData1 != null)
            {
              for (int index4 = 0; index4 < shopData1.items.Count; ++index4)
              {
                if (shopData1.items[index4].saleType != ESaleType.Gold && shopData1.items[index4].saleType != ESaleType.Coin)
                {
                  ((Component) this).gameObject.SetActive(true);
                  return;
                }
              }
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.SHOP_MAINCOST_ICON:
            LimitedShopItem limitedShopItem3 = this.GetLimitedShopItem();
            if (limitedShopItem3 != null)
            {
              PlayerData player5 = MonoSingleton<GameManager>.Instance.Player;
              if (limitedShopItem3.saleType == ESaleType.EventCoin)
              {
                this.SetBuyPriceEventCoinTypeIcon(limitedShopItem3.cost_iname);
                break;
              }
              if (limitedShopItem3.saleType == ESaleType.Gold || limitedShopItem3.saleType == ESaleType.Coin)
                break;
              this.SetBuyPriceTypeIcon(limitedShopItem3.saleType);
              break;
            }
            ShopData shopData2 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (shopData2 != null)
            {
              for (int index5 = 0; index5 < shopData2.items.Count; ++index5)
              {
                if (shopData2.items[index5].saleType != ESaleType.Gold && shopData2.items[index5].saleType != ESaleType.Coin)
                {
                  this.SetBuyPriceTypeIcon(shopData2.items[index5].saleType);
                  return;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_MAINCOST_AMOUNT:
            LimitedShopItem limitedShopItem4 = this.GetLimitedShopItem();
            if (limitedShopItem4 != null)
            {
              PlayerData player6 = MonoSingleton<GameManager>.Instance.Player;
              if (limitedShopItem4.saleType == ESaleType.EventCoin)
              {
                this.SetTextValue(player6.EventCoinNum(limitedShopItem4.cost_iname));
                break;
              }
              if (limitedShopItem4.saleType == ESaleType.Gold || limitedShopItem4.saleType == ESaleType.Coin)
                break;
              this.SetTextValue(player6.GetShopTypeCostAmount(limitedShopItem4.saleType));
              break;
            }
            ShopData shopData3 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (shopData3 != null)
            {
              for (int index6 = 0; index6 < shopData3.items.Count; ++index6)
              {
                if (shopData3.items[index6].saleType != ESaleType.Gold && shopData3.items[index6].saleType != ESaleType.Coin)
                {
                  this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.GetShopTypeCostAmount(shopData3.items[index6].saleType));
                  return;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_BADGE_GROWUP:
            UnitData unitData46;
            if ((unitData46 = this.GetUnitData()) != null)
            {
              ((Component) this).gameObject.SetActive((unitData46.BadgeState & UnitBadgeTypes.EnableEquipment) != (UnitBadgeTypes) 0);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.UNITPARAM_BADGE_UNLOCK:
            UnitParam unitParam12;
            if ((unitParam12 = this.GetUnitParam()) != null)
            {
              ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(unitParam12));
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.ITEM_BADGE_EQUIPUNIT:
            ItemParam itemParam20;
            if ((itemParam20 = this.GetItemParam()) != null && MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableEquipUnit(itemParam20))
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.BADGE_UNIT:
            if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Unit) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All)
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.BADGE_UNITENHANCED:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Unit) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_UNITUNLOCKED:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_GACHA:
            if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GoldGacha) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.RareGacha) != ~GameManager.BadgeTypes.All)
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.BADGE_GOLDGACHA:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GoldGacha) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_RAREGACHA:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.RareGacha) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_ARENA:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Arena) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_MULTIPLAY:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Multiplay) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_DAILYMISSION:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.DailyMission) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_FRIEND:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Friend) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_GIFTBOX:
            ((Component) this).gameObject.SetActive((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GiftBox) != ~GameManager.BadgeTypes.All);
            break;
          case GameParameter.ParameterTypes.BADGE_SHORTCUTMENU:
            if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.Unit) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.DailyMission) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GiftBox) != ~GameManager.BadgeTypes.All)
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_VIPPOINT:
            GameManager instance30 = MonoSingleton<GameManager>.Instance;
            int num20 = instance30.Player.VipPoint - (instance30.Player.VipRank <= 0 ? 0 : instance30.MasterParam.GetVipRankNextPoint(instance30.Player.VipRank - 1));
            int vipRankNextPoint = instance30.MasterParam.GetVipRankNextPoint(instance30.Player.VipRank);
            this.SetTextValue(num20);
            this.SetSliderValue(num20, vipRankNextPoint);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_VIPPOINTMAX:
            GameManager instance31 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance31.MasterParam.GetVipRankNextPoint(instance31.Player.VipRank));
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COINFREE:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.FreeCoin);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COINPAID:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.PaidCoin);
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_PARTYMEMBER:
            UnitData unitData47;
            if ((unitData47 = this.GetUnitData()) != null)
            {
              List<PartyData> partys = MonoSingleton<GameManager>.Instance.Player.Partys;
              for (int index7 = 0; index7 < partys.Count; ++index7)
              {
                if (partys[index7].IsPartyUnit(unitData47.UniqueID))
                {
                  ((Component) this).gameObject.SetActive(true);
                  return;
                }
              }
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.LOGINBONUS_DAYNUM:
            ItemData dataOfClass41;
            if ((dataOfClass41 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null)) != null && dataOfClass41 is LoginBonusData)
            {
              this.SetTextValue(((LoginBonusData) dataOfClass41).DayNum);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.None:
            this.BatchUpdate();
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_LVSORT:
            if ((unitData1 = this.GetUnitData()) != null)
            {
              ((Component) this).gameObject.SetActive(GameUtility.GetUnitShowSetting(17));
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_PARAMSORT:
            if ((unitData1 = this.GetUnitData()) != null && !GameUtility.GetUnitShowSetting(18) && !GameUtility.GetUnitShowSetting(17))
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.UNIT_SORTTYPE_VALUE:
            UnitData unitData48;
            if ((unitData48 = this.GetUnitData()) != null)
            {
              if (GameUtility.GetUnitShowSetting(17))
              {
                this.SetTextValue(unitData48.Lv);
                break;
              }
              if (GameUtility.GetUnitShowSetting(18))
              {
                this.ResetToDefault();
                break;
              }
              if (GameUtility.GetUnitShowSetting(19))
              {
                JobData currentJob = unitData48.CurrentJob;
                this.SetTextValue(currentJob == null ? 1 : currentJob.Rank);
                break;
              }
              if (GameUtility.GetUnitShowSetting(20))
              {
                this.SetTextValue((int) unitData48.Status.param.hp);
                break;
              }
              if (GameUtility.GetUnitShowSetting(21))
              {
                this.SetTextValue((int) unitData48.Status.param.atk);
                break;
              }
              if (GameUtility.GetUnitShowSetting(22))
              {
                this.SetTextValue((int) unitData48.Status.param.def);
                break;
              }
              if (GameUtility.GetUnitShowSetting(23))
              {
                this.SetTextValue((int) unitData48.Status.param.mag);
                break;
              }
              if (GameUtility.GetUnitShowSetting(24))
              {
                this.SetTextValue((int) unitData48.Status.param.mnd);
                break;
              }
              if (GameUtility.GetUnitShowSetting(25))
              {
                this.SetTextValue((int) unitData48.Status.param.spd);
                break;
              }
              if (GameUtility.GetUnitShowSetting(26))
              {
                this.SetTextValue(0 + (int) unitData48.Status.param.atk + (int) unitData48.Status.param.def + (int) unitData48.Status.param.mag + (int) unitData48.Status.param.mnd + (int) unitData48.Status.param.spd + (int) unitData48.Status.param.dex + (int) unitData48.Status.param.cri + (int) unitData48.Status.param.luk);
                break;
              }
              if (GameUtility.GetUnitShowSetting(27))
              {
                this.SetTextValue(unitData48.AwakeLv);
                break;
              }
              if (GameUtility.GetUnitShowSetting(28))
              {
                this.SetTextValue(unitData48.GetCombination());
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SKILL_STATE_CONDITION:
            if ((skillParam = this.GetSkillParam()) != null)
            {
              UnitData unitData49;
              if ((unitData49 = this.GetUnitData()) != null)
              {
                ((Component) this).gameObject.SetActive(unitData49.GetSkillData(skillParam.iname) == null);
                break;
              }
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.SKILL_CONDITION:
            AbilityParam abilityParam5;
            if ((skillParam = this.GetSkillParam()) != null && (abilityParam5 = this.GetAbilityParam()) != null && abilityParam5.skills != null && abilityParam5.skills.Length > 0)
            {
              if (!string.IsNullOrEmpty(skillParam.ReplacedTargetId))
              {
                SkillParam skillParam1 = MonoSingleton<GameManager>.Instance.GetSkillParam(skillParam.ReplacedTargetId);
                skillParam = skillParam1 == null ? skillParam : skillParam1;
              }
              LearningSkill learningSkill = Array.Find<LearningSkill>(abilityParam5.skills, (Predicate<LearningSkill>) (p => p.iname == skillParam.iname));
              if (learningSkill != null)
              {
                this.SetTextValue(string.Format(LocalizedText.Get("sys.SKILL_LEANING_CONDITION1"), (object) learningSkill.locklv));
                break;
              }
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.SKILL_LEANING_CONDITION1"), (object) 1));
            break;
          case GameParameter.ParameterTypes.ABILITY_CONDITION:
            UnitData unitData50;
            if ((unitData50 = this.GetUnitData()) != null)
            {
              int rank = 1;
              string abilityID = (string) null;
              AbilityData abilityData3;
              if ((abilityData3 = this.GetAbilityData()) != null)
                abilityID = abilityData3.AbilityID;
              AbilityParam abilityParam6;
              if ((abilityParam6 = this.GetAbilityParam()) != null)
                abilityID = abilityParam6.iname;
              JobParam job3;
              MonoSingleton<GameManager>.Instance.GetLearningAbilitySource(unitData50, abilityID, out job3, out rank);
              if (job3 != null)
              {
                this.SetTextValue(string.Format(LocalizedText.Get("sys.ABILITY_LEANING_COND1"), (object) job3.name, (object) rank));
                break;
              }
            }
            this.SetTextValue((string) null);
            break;
          case GameParameter.ParameterTypes.GACHA_COST:
            GachaParam gachaParam1;
            if ((gachaParam1 = this.GetGachaParam()) != null)
            {
              if (gachaParam1.gold != 0)
              {
                this.SetTextValue(gachaParam1.gold);
                break;
              }
              if (gachaParam1.coin != 0)
              {
                this.SetTextValue(gachaParam1.coin);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GACHA_NUM:
            GachaParam gachaParam2;
            if ((gachaParam2 = this.GetGachaParam()) != null)
            {
              this.SetTextValue(gachaParam2.num);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GACHA_GOLD_RESTNUM:
            break;
          case GameParameter.ParameterTypes.GACHA_GOLD_STATE_RESTNUM:
            break;
          case GameParameter.ParameterTypes.GACHA_GOLD_TIMER:
            long gachaGoldCoolDownSec = MonoSingleton<GameManager>.Instance.Player.GetNextFreeGachaGoldCoolDownSec();
            string str10 = (gachaGoldCoolDownSec / 3600L).ToString();
            string str11 = (gachaGoldCoolDownSec % 3600L / 60L).ToString();
            string str12 = (gachaGoldCoolDownSec % 60L).ToString();
            this.SetTextValue(str10.PadLeft(2, '0') + ":" + str11.PadLeft(2, '0') + ":" + str12.PadLeft(2, '0'));
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.GACHA_GOLD_STATE_TIMER:
            GameManager instance32 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(((int) instance32.MasterParam.FixParam.FreeGachaGoldMax - instance32.Player.FreeGachaGold.num).ToString());
            break;
          case GameParameter.ParameterTypes.GACHA_GOLD_STATE_INTERACTIVE:
            break;
          case GameParameter.ParameterTypes.GACHA_COIN_TIMER:
            long gachaCoinCoolDownSec = MonoSingleton<GameManager>.Instance.Player.GetNextFreeGachaCoinCoolDownSec();
            string str13 = (gachaCoinCoolDownSec / 3600L).ToString();
            string str14 = (gachaCoinCoolDownSec % 3600L / 60L).ToString();
            string str15 = (gachaCoinCoolDownSec % 60L).ToString();
            this.SetTextValue(str13.PadLeft(2, '0') + ":" + str14.PadLeft(2, '0') + ":" + str15.PadLeft(2, '0'));
            this.SetUpdateInterval(1f);
            break;
          case GameParameter.ParameterTypes.GACHA_COIN_STATE_TIMER:
            this.SetTextValue((1 - MonoSingleton<GameManager>.Instance.Player.FreeGachaCoin.num).ToString());
            break;
          case GameParameter.ParameterTypes.GACHA_COIN_STATE_INTERACTIVE:
            break;
          case GameParameter.ParameterTypes.UNIT_IMAGE2:
            UnitData unitData51;
            if ((unitData51 = this.GetUnitData()) != null && !string.IsNullOrEmpty(unitData51.UnitParam.image))
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
              string str16 = AssetPath.UnitSkinImage2(unitData51.UnitParam, unitData51.GetSelectedSkin(), unitData51.CurrentJob.JobID);
              if (!string.IsNullOrEmpty(str16))
              {
                iconLoader.ResourcePath = str16;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ITEM_FLAVOR:
            ItemParam itemParam21;
            if ((itemParam21 = this.GetItemParam()) != null)
            {
              this.SetTextValue(itemParam21.Flavor);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_AWAKE:
          case GameParameter.ParameterTypes.UNIT_AWAKE2:
            UnitData unitData52;
            if ((unitData52 = this.GetUnitData()) != null)
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
              {
                int awakeLv = unitData52.AwakeLv;
                int num21 = unitData52.GetAwakeLevelCap();
                if (this.ParameterType == GameParameter.ParameterTypes.UNIT_AWAKE2)
                  num21 = (int) MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(unitData52.GetRarityCap()).UnitAwakeLvCap;
                int num22 = 5;
                if (num21 / num22 > this.Index)
                {
                  int index8 = num22;
                  int num23 = this.Index * num22;
                  if ((this.Index + 1) * num22 > awakeLv)
                    index8 = awakeLv - num23 >= 0 ? awakeLv % num22 : 0;
                  this.SetImageIndex(index8);
                  ((Component) this).gameObject.SetActive(true);
                  break;
                }
                ((Component) this).gameObject.SetActive(false);
                break;
              }
              this.SetTextValue(unitData52.AwakeLv);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SUPPORTER_ISFRIEND:
            if ((supportData = this.GetSupportData()) != null)
            {
              ((Component) this).gameObject.SetActive(supportData.IsFriend());
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.SUPPORTER_COST:
            if ((supportData = this.GetSupportData()) != null)
            {
              this.SetTextValue(supportData.GetCost());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.Unit_ThumbnailedJobIcon:
            UnitData unitData53;
            if ((unitData53 = this.GetUnitData()) == null)
              break;
            SpriteSheet spriteSheet1 = AssetManager.Load<SpriteSheet>(AssetPath.JobIconThumbnail());
            Image component3 = ((Component) this).GetComponent<Image>();
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet1, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
              break;
            JobData currentJob1 = unitData53.CurrentJob;
            if (currentJob1 != null)
            {
              Sprite sprite = spriteSheet1.GetSprite(currentJob1.JobResourceID);
              component3.sprite = sprite;
              break;
            }
            component3.sprite = (Sprite) null;
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_JOBRANKFRAME:
            UnitData unit11 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit11 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetImageIndex(unit11.CurrentJob.Rank);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_JOBRANK:
            UnitData unit12 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit12 == null)
            {
              this.ResetToDefault();
              break;
            }
            int rank1 = unit12.CurrentJob.Rank;
            int jobRankCap1 = unit12.GetJobRankCap();
            this.SetTextValue(rank1);
            this.SetSliderValue(rank1, jobRankCap1);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_JOBICON:
            UnitData unit13 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit13 == null || unit13.CurrentJob == null)
            {
              this.ResetToDefault();
              break;
            }
            JobParam job4 = unit13.CurrentJob.Param;
            string str17 = job4 != null ? AssetPath.JobIconSmall(job4) : (string) null;
            if (string.IsNullOrEmpty(str17))
              break;
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str17;
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_RARITY:
            UnitData unit14 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit14 == null)
            {
              this.ResetToDefault();
              break;
            }
            StarGauge component4 = ((Component) this).GetComponent<StarGauge>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
            {
              component4.Max = unit14.GetRarityCap() + 1;
              component4.Value = unit14.Rarity + 1;
              break;
            }
            this.SetAnimatorInt("rare", unit14.Rarity);
            this.SetImageIndex(unit14.Rarity);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_ELEMENT:
            UnitData unit15 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit15 == null)
            {
              this.ResetToDefault();
              break;
            }
            int element1 = (int) unit15.Element;
            this.SetAnimatorInt("element", element1);
            this.SetImageIndex(element1);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_UNIT_LEVEL:
            UnitData unit16 = this.GetMultiPlayerUnitData(this.Index)?.unit;
            if (unit16 == null)
            {
              this.ResetToDefault();
              break;
            }
            int num24 = unit16.CalcLevel();
            this.SetTextValue(num24);
            this.SetSliderValue(num24, 99);
            break;
          case GameParameter.ParameterTypes.TROPHY_REWARDSTAMINA:
            TrophyParam trophyParam4 = this.GetTrophyParam();
            if (trophyParam4 == null)
              break;
            this.SetTextValue(trophyParam4.Stamina);
            ((Component) this).gameObject.SetActive(trophyParam4.Stamina > 0);
            break;
          case GameParameter.ParameterTypes.JOB_JOBICON:
          case GameParameter.ParameterTypes.JOB_JOBICON2:
            JobParam jobParam1;
            if ((jobParam1 = this.GetJobParam()) != null)
            {
              string str18 = this.ParameterType != GameParameter.ParameterTypes.JOB_JOBICON2 ? AssetPath.JobIconSmall(jobParam1) : AssetPath.JobIconMedium(jobParam1);
              if (!string.IsNullOrEmpty(str18))
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str18;
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOB_NAME:
            JobParam jobParam2;
            if ((jobParam2 = this.GetJobParam()) != null)
            {
              this.SetTextValue(jobParam2.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUESTRESULT_MULTICOIN:
            BattleCore.Record dataOfClass42 = DataSource.FindDataOfClass<BattleCore.Record>(((Component) this).gameObject, (BattleCore.Record) null);
            if (dataOfClass42 != null)
            {
              this.SetTextValue((int) dataOfClass42.multicoin);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_REST_REWARD_IS_ZERO:
            GameUtility.EScene currentScene2 = GameUtility.GetCurrentScene();
            if (currentScene2 != GameUtility.EScene.HOME_MULTI && currentScene2 != GameUtility.EScene.BATTLE_MULTI)
              break;
            PlayerData player7 = MonoSingleton<GameManager>.Instance.Player;
            int num25 = player7.ChallengeMultiMax - player7.ChallengeMultiNum;
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(num25 <= 0);
              break;
            }
            if (this.Index == 1)
            {
              ((Component) this).gameObject.SetActive(num25 > 0);
              break;
            }
            if (this.Index == 2)
            {
              ((Component) this).gameObject.SetActive(num25 >= 0);
              break;
            }
            if (this.Index == 3)
            {
              ((Component) this).gameObject.SetActive(num25 < 0);
              break;
            }
            if (this.Index == 4)
            {
              ((Component) this).gameObject.SetActive(num25 == 0);
              break;
            }
            ((Component) this).gameObject.SetActive(num25 != 0);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_UNIT_SLOT_ENABLE:
            bool flag5 = false;
            if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
            {
              JSON_MyPhotonRoomParam roomParam6 = this.GetRoomParam();
              JSON_MyPhotonPlayerParam roomPlayerParam10 = this.GetRoomPlayerParam();
              int playerIndex = roomPlayerParam10 != null ? roomPlayerParam10.playerIndex : 0;
              flag5 = this.Index < (roomParam6 != null ? roomParam6.GetUnitSlotNum(playerIndex) : 0);
            }
            ((Component) this).gameObject.SetActive(flag5);
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_UNIT_SLOT_VALID:
            bool flag6 = false;
            if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
              flag6 = this.GetMultiPlayerUnitData(this.Index)?.unit != null;
            ((Component) this).gameObject.SetActive(flag6);
            break;
          case GameParameter.ParameterTypes.REWARD_STAMINA:
            RewardData dataOfClass43 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass43 != null)
            {
              this.SetTextValue(dataOfClass43.Stamina);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_CHALLENGE_NUM:
            QuestParam questParam15;
            if ((questParam15 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam15.GetChallangeCount());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_CHALLENGE_MAX:
            QuestParam questParam16;
            if ((questParam16 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(questParam16.GetChallangeLimit());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_RESET_NUM:
            QuestParam questParam17;
            if ((questParam17 = this.GetQuestParam()) != null)
            {
              this.SetTextValue((int) questParam17.dailyReset);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_RESET_MAX:
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_POISON:
            Unit unit17;
            if ((unit17 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit17.IsUnitCondition(EUnitCondition.Poison));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_PARALYSED:
            Unit unit18;
            if ((unit18 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit18.IsUnitCondition(EUnitCondition.Paralysed));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_STUN:
            Unit unit19;
            if ((unit19 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit19.IsUnitCondition(EUnitCondition.Stun));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_SLEEP:
            Unit unit20;
            if ((unit20 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit20.IsUnitCondition(EUnitCondition.Sleep));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_CHARM:
            Unit unit21;
            if ((unit21 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit21.IsUnitCondition(EUnitCondition.Charm));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_STONE:
            Unit unit22;
            if ((unit22 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit22.IsUnitCondition(EUnitCondition.Stone));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_BLINDNESS:
            Unit unit23;
            if ((unit23 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit23.IsUnitCondition(EUnitCondition.Blindness));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLESKILL:
            Unit unit24;
            if ((unit24 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit24.IsUnitCondition(EUnitCondition.DisableSkill));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLEMOVE:
            Unit unit25;
            if ((unit25 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit25.IsUnitCondition(EUnitCondition.DisableMove));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLEATTACK:
            Unit unit26;
            if ((unit26 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit26.IsUnitCondition(EUnitCondition.DisableAttack));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_ZOMBIE:
            Unit unit27;
            if ((unit27 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit27.IsUnitCondition(EUnitCondition.Zombie));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DEATHSENTENCE:
            Unit unit28;
            if ((unit28 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit28.IsUnitCondition(EUnitCondition.DeathSentence));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_BERSERK:
            Unit unit29;
            if ((unit29 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit29.IsUnitCondition(EUnitCondition.Berserk));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLEKNOCKBACK:
            Unit unit30;
            if ((unit30 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit30.IsUnitCondition(EUnitCondition.DisableKnockback));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLEBUFF:
            Unit unit31;
            if ((unit31 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit31.IsUnitCondition(EUnitCondition.DisableBuff));
            break;
          case GameParameter.ParameterTypes.UNIT_STATE_CONDITION_DISABLEDEBUFF:
            Unit unit32;
            if ((unit32 = this.GetUnit()) == null)
              break;
            ((Component) this).gameObject.SetActive(unit32.IsUnitCondition(EUnitCondition.DisableDebuff));
            break;
          case GameParameter.ParameterTypes.TURN_UNIT_SIDE_FRAME:
            Unit unit33;
            if ((unit33 = this.GetUnit()) != null)
            {
              if (unit33.Side == EUnitSide.Player)
              {
                this.SetImageIndex(0);
                break;
              }
              this.SetImageIndex(1);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.JOBSET_UNLOCKCONDITION:
            JobSetParam dataOfClass44 = DataSource.FindDataOfClass<JobSetParam>(((Component) this).gameObject, (JobSetParam) null);
            if (dataOfClass44 != null)
            {
              StringBuilder stringBuilder = GameUtility.GetStringBuilder();
              if (dataOfClass44.lock_rarity > 0)
              {
                stringBuilder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_RARITY"), (object) (dataOfClass44.lock_rarity + 1)));
                stringBuilder.Append('\n');
              }
              if (dataOfClass44.lock_awakelv > 0)
              {
                stringBuilder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_AWAKELV"), (object) dataOfClass44.lock_awakelv));
                stringBuilder.Append('\n');
              }
              if (dataOfClass44.lock_jobs != null)
              {
                for (int index9 = 0; index9 < dataOfClass44.lock_jobs.Length; ++index9)
                {
                  if (!string.IsNullOrEmpty(dataOfClass44.lock_jobs[index9].iname) && dataOfClass44.lock_jobs[index9].lv > 0)
                  {
                    JobParam jobParam3 = MonoSingleton<GameManager>.Instance.GetJobParam(dataOfClass44.lock_jobs[index9].iname);
                    stringBuilder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_CONDITION"), (object) jobParam3.name, (object) dataOfClass44.lock_jobs[index9].lv));
                    stringBuilder.Append('\n');
                  }
                }
              }
              if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\n')
                --stringBuilder.Length;
              this.SetTextValue(stringBuilder.ToString());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_REST_MY_UNIT_IS_ZERO:
            SceneBattle instance33 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance33, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            int nextMyTurn = instance33.GetNextMyTurn();
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(nextMyTurn < 0);
              break;
            }
            ((Component) this).gameObject.SetActive(nextMyTurn >= 0);
            break;
          case GameParameter.ParameterTypes.MULTI_PLAYER_IS_ME:
            JSON_MyPhotonPlayerParam roomPlayerParam11 = this.GetRoomPlayerParam();
            if (roomPlayerParam11 == null || roomPlayerParam11.playerID <= 0)
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            bool flag7 = roomPlayerParam11.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex;
            bool flag8 = true & roomPlayerParam11.state != 0 & roomPlayerParam11.state != 4 & roomPlayerParam11.state != 5;
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(flag7);
              break;
            }
            if (this.Index == 1)
            {
              ((Component) this).gameObject.SetActive(!flag7);
              break;
            }
            if (this.Index == 2)
            {
              ((Component) this).gameObject.SetActive(true);
              this.SetImageIndex(!flag7 ? 1 : 0);
              break;
            }
            if (this.Index == 3)
            {
              ((Component) this).gameObject.SetActive(flag7 && !flag8);
              break;
            }
            if (this.Index == 4)
            {
              ((Component) this).gameObject.SetActive(!flag7 || flag8);
              break;
            }
            if (this.Index == 5)
            {
              SRPG_Button component5 = ((Component) this).gameObject.GetComponent<SRPG_Button>();
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component5, (UnityEngine.Object) null))
                break;
              ((Selectable) component5).interactable = flag7;
              break;
            }
            if (this.Index == 6)
            {
              MyPhoton instance34 = PunMonoSingleton<MyPhoton>.Instance;
              List<MyPhoton.MyPlayer> roomPlayerList3 = instance34.GetRoomPlayerList();
              if (roomPlayerList3 != null)
              {
                MyPhoton.MyPlayer player8 = instance34.FindPlayer(roomPlayerList3, roomPlayerParam11.playerID, roomPlayerParam11.playerIndex);
                if (player8 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  break;
                }
                ((Component) this).gameObject.SetActive(player8.start);
                break;
              }
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            if (this.Index != 7)
              break;
            JSON_MyPhotonPlayerParam dataOfClass45 = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).gameObject, (JSON_MyPhotonPlayerParam) null);
            ((Component) this).gameObject.SetActive((!flag7 || flag8) && dataOfClass45 != null && dataOfClass45.units != null && dataOfClass45.units.Length >= 2 && dataOfClass45.units[1].unit != null && dataOfClass45.units[1].unit.UniqueID > 0L);
            break;
          case GameParameter.ParameterTypes.QUESTLIST_SECTIONEXPR:
            ChapterParam dataOfClass46 = DataSource.FindDataOfClass<ChapterParam>(((Component) this).gameObject, (ChapterParam) null);
            if (dataOfClass46 != null)
            {
              this.SetTextValue(dataOfClass46.expr);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTI_CURRENT_ROOM_IS_LOCKED:
            JSON_MyPhotonRoomParam roomParam7 = this.GetRoomParam();
            if (roomParam7 == null)
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            bool flag9 = MultiPlayAPIRoom.IsLocked(roomParam7.passCode);
            bool flag10 = PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer();
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(flag9);
              break;
            }
            if (this.Index == 1)
            {
              ((Component) this).gameObject.SetActive(!flag9);
              break;
            }
            if (this.Index == 2)
            {
              ((Component) this).gameObject.SetActive(flag10 && flag9);
              break;
            }
            ((Component) this).gameObject.SetActive(flag10 && !flag9);
            break;
          case GameParameter.ParameterTypes.MAIL_GIFT_RECEIVE:
            MailData mailData5;
            if ((mailData5 = this.GetMailData()) == null)
            {
              this.ResetToDefault();
              break;
            }
            DateTime serverTime1 = TimeManager.ServerTime;
            DateTime localTime1 = GameUtility.UnixtimeToLocalTime(mailData5.post_at);
            TimeSpan timeSpan2 = serverTime1 - localTime1;
            string empty1 = string.Empty;
            string str19;
            if (timeSpan2.Days >= 1)
            {
              string format2 = "yyyy/MM/dd";
              str19 = localTime1.ToString(format2);
            }
            else
              str19 = timeSpan2.Hours < 1 ? (timeSpan2.Minutes < 1 ? timeSpan2.Seconds.ToString() + "秒前" : timeSpan2.Minutes.ToString() + "分前") : timeSpan2.Hours.ToString() + "時間前";
            this.SetTextValue(str19);
            break;
          case GameParameter.ParameterTypes.QUEST_TIMELIMIT:
            if ((questParam1 = this.GetQuestParam()) == null)
              break;
            break;
          case GameParameter.ParameterTypes.UNIT_CHARGETIME:
            Unit unit34;
            if ((unit34 = this.GetUnit()) != null)
            {
              OInt oint = (OInt) Mathf.Min((int) unit34.ChargeTime, (int) unit34.ChargeTimeMax);
              this.SetTextValue(Mathf.Floor((float) (int) oint / 10f).ToString());
              this.SetSliderValue((int) oint, (int) unit34.ChargeTimeMax);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.UNIT_CHARGETIMEMAX:
            Unit unit35;
            if ((unit35 = this.GetUnit()) != null)
            {
              this.SetTextValue(Mathf.Floor((float) (int) unit35.ChargeTimeMax / 10f).ToString());
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GIMMICK_DESCRIPTION:
            Unit unit36;
            if ((unit36 = this.GetUnit()) != null)
            {
              this.SetTextValue(LocalizedText.Get("quest." + unit36.UnitParam.iname));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.PRODUCT_DESC:
            PaymentManager.Product dataOfClass47 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
            if (dataOfClass47 == null || string.IsNullOrEmpty(dataOfClass47.desc))
            {
              this.ResetToDefault();
              break;
            }
            this.SelectCoinDescription(dataOfClass47.desc);
            break;
          case GameParameter.ParameterTypes.PRODUCT_NUMX:
            PaymentManager.Product dataOfClass48 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
            BuyCoinProductParam dataOfClass49 = DataSource.FindDataOfClass<BuyCoinProductParam>(((Component) this).gameObject, (BuyCoinProductParam) null);
            if (dataOfClass48 == null)
            {
              this.ResetToDefault();
              break;
            }
            string str20 = string.Empty;
            FixParam fixParam3 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (dataOfClass48.productID.Contains((string) fixParam3.VipCardProduct))
            {
              if (MonoSingleton<GameManager>.Instance.Player.CheckEnableVipCard())
              {
                DateTime vipExpiredAt = MonoSingleton<GameManager>.Instance.Player.VipExpiredAt;
                TimeSpan timeSpan3 = vipExpiredAt - TimeManager.FromUnixTime(Network.GetServerTime());
                str20 = 0 >= timeSpan3.Days ? string.Format(LocalizedText.Get("sys.VIP_EXPIRE_HHMM"), (object) vipExpiredAt.Hour, (object) vipExpiredAt.Minute) : string.Format(LocalizedText.Get("sys.VIP_REMAIN_D"), (object) timeSpan3.Days);
              }
            }
            else if (dataOfClass48.productID.Contains((string) fixParam3.PremiumProduct))
            {
              if (MonoSingleton<GameManager>.Instance.Player.CheckEnablePremiumMember())
              {
                TimeSpan timeSpan4 = MonoSingleton<GameManager>.Instance.Player.PremiumExpiredAt - TimeManager.FromUnixTime(Network.GetServerTime());
                int days2 = timeSpan4.Days;
                if (new TimeSpan(0, timeSpan4.Hours, timeSpan4.Minutes, timeSpan4.Seconds, timeSpan4.Milliseconds).TotalMilliseconds > 0.0)
                  ++days2;
                str20 = string.Format(LocalizedText.Get("sys.VIP_REMAIN_D"), (object) days2);
              }
            }
            else if (dataOfClass49 != null && dataOfClass49.IsExpansionShop())
            {
              long expansionGroupExpiredAt = MonoSingleton<GameManager>.Instance.Player.GetExpansionGroupExpiredAt(dataOfClass49);
              if (expansionGroupExpiredAt > 0L)
              {
                TimeSpan timeSpan5 = TimeManager.FromUnixTime(expansionGroupExpiredAt) - TimeManager.FromUnixTime(Network.GetServerTime());
                int days3 = timeSpan5.Days;
                if (new TimeSpan(0, timeSpan5.Hours, timeSpan5.Minutes, timeSpan5.Seconds, timeSpan5.Milliseconds).TotalMilliseconds > 0.0)
                  ++days3;
                str20 = string.Format(LocalizedText.Get("sys.VIP_REMAIN_D"), (object) days3);
              }
            }
            else
            {
              int num26 = dataOfClass48.numPaid + dataOfClass48.numFree;
              if (0 < num26)
                str20 = string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num26);
            }
            if (this.Index == -1)
            {
              ((Component) this).gameObject.SetActive(!string.IsNullOrEmpty(str20));
              break;
            }
            if (this.Index == -2)
            {
              Button component6 = ((Component) this).gameObject.GetComponent<Button>();
              if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component6))
                break;
              ((Selectable) component6).interactable = string.IsNullOrEmpty(str20);
              break;
            }
            if (this.Index == -3)
            {
              UnityEngine.UI.Text component7 = ((Component) this).gameObject.GetComponent<UnityEngine.UI.Text>();
              if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component7))
                break;
              ((Behaviour) component7).enabled = string.IsNullOrEmpty(str20);
              break;
            }
            this.SetTextValue(str20);
            break;
          case GameParameter.ParameterTypes.UNIT_DEX:
            UnitData unitData54;
            if ((unitData54 = this.GetUnitData()) != null)
            {
              this.SetTextValue((int) unitData54.Status.param.dex);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_NAME:
            ArtifactParam artifactParam1 = this.GetArtifactParam();
            if (artifactParam1 != null)
            {
              this.SetTextValue(artifactParam1.name);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_DESC:
            ArtifactParam artifactParam2 = this.GetArtifactParam();
            if (artifactParam2 != null)
            {
              this.SetTextValue(artifactParam2.Expr);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_SPEC:
            ArtifactParam artifactParam3 = this.GetArtifactParam();
            if (artifactParam3 != null)
            {
              this.SetTextValue(artifactParam3.spec);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_RARITY:
            ArtifactData dataOfClass50 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
            if (dataOfClass50 != null)
            {
              this.SetTextValue((int) dataOfClass50.Rarity + 1);
              this.SetImageIndex((int) dataOfClass50.Rarity);
              this.SetSliderValue((int) dataOfClass50.Rarity, (int) dataOfClass50.RarityCap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARTIFACT_RARITYCAP:
            ArtifactData dataOfClass51 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
            if (dataOfClass51 != null)
            {
              this.SetTextValue((int) dataOfClass51.RarityCap + 1);
              this.SetImageIndex((int) dataOfClass51.RarityCap);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.ARTIFACT_NUM:
            ArtifactParam artifactParam4 = this.GetArtifactParam();
            if (artifactParam4 != null)
            {
              int num27 = 0;
              List<ArtifactData> artifacts = MonoSingleton<GameManager>.Instance.Player.Artifacts;
              for (int index10 = 0; index10 < artifacts.Count; ++index10)
              {
                if (artifacts[index10].ArtifactParam == artifactParam4)
                  ++num27;
              }
              this.SetTextValue(num27);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_SELLPRICE:
            ArtifactData dataOfClass52 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
            if (dataOfClass52 != null)
            {
              this.SetTextValue(dataOfClass52.GetSellPrice());
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.APPLICATION_VERSION:
            this.SetTextValue(MyApplicationPlugin.version);
            goto case GameParameter.ParameterTypes.SKILL_ICON;
          case GameParameter.ParameterTypes.SUPPORTER_UNITCAPPEDLEVELMAX:
            if ((supportData = this.GetSupportData()) != null)
            {
              instance1 = MonoSingleton<GameManager>.Instance;
              this.SetTextValue(Mathf.Min(supportData.Unit.GetLevelCap(), supportData.PlayerLevel));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_PIECEPOINT:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.PiecePoint);
            break;
          case GameParameter.ParameterTypes.SHOP_KAKERA_SELLPRICETOTAL:
            List<SellItem> sellItemList3 = this.GetSellItemList();
            if (sellItemList3 == null)
            {
              this.ResetToDefault();
              break;
            }
            int num28 = 0;
            for (int index11 = 0; index11 < sellItemList3.Count; ++index11)
              num28 += (int) sellItemList3[index11].item.RarityParam.PieceToPoint * sellItemList3[index11].num;
            this.SetTextValue(num28);
            break;
          case GameParameter.ParameterTypes.ITEM_KAKERA_PRICE:
            ItemParam itemParam22;
            if ((itemParam22 = this.GetItemParam()) != null)
            {
              this.SetTextValue((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(itemParam22.rare).PieceToPoint);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_ITEM_KAKERA_SELLPRICE:
            SellItem sellItem8 = this.GetSellItem();
            if (sellItem8 == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue((int) sellItem8.item.RarityParam.PieceToPoint * sellItem8.num);
            break;
          case GameParameter.ParameterTypes.REWARD_MULTICOIN:
            RewardData dataOfClass53 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass53 != null)
            {
              this.SetTextValue(dataOfClass53.MultiCoin);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.REWARD_KAKERACOIN:
            RewardData dataOfClass54 = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
            if (dataOfClass54 != null)
            {
              this.SetTextValue(dataOfClass54.KakeraCoin);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_UNIT_ENTRYCONDITION:
            QuestParam questParam18;
            if ((questParam18 = this.GetQuestParam()) != null)
            {
              List<string> stringList = questParam18.type != QuestTypes.Character ? questParam18.GetEntryQuestConditions() : questParam18.GetEntryQuestConditionsCh(includeUnitLv: false);
              if (stringList != null && stringList.Count > 0)
              {
                string str21 = string.Empty;
                for (int index12 = 0; index12 < stringList.Count; ++index12)
                {
                  if (index12 > 0)
                  {
                    switch (this.Index)
                    {
                      case 0:
                      case 2:
                        str21 += "\n";
                        break;
                      default:
                        str21 += "、";
                        break;
                    }
                  }
                  str21 += stringList[index12];
                }
                if (!string.IsNullOrEmpty(str21))
                {
                  if (this.Index != 0)
                  {
                    switch (questParam18.GetPartyCondType())
                    {
                      case PartyCondType.Limited:
                        str21 = LocalizedText.Get("sys.PARTYEDITOR_COND_LIMIT") + str21;
                        break;
                      case PartyCondType.Forced:
                        str21 = LocalizedText.Get("sys.PARTYEDITOR_COND_FIXED") + str21;
                        break;
                    }
                  }
                  if (this.Index == 4)
                    str21 = str21.Replace("\n", string.Empty);
                  this.SetTextValue(str21);
                  break;
                }
              }
              else
              {
                this.SetTextValue(LocalizedText.Get("sys.PARTYEDITOR_COND_NO_LIMIT"));
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.OBSOLETE_QUEST_IS_UNIT_ENTRYCONDITION:
            QuestParam questParam19;
            if ((questParam19 = this.GetQuestParam()) != null)
            {
              List<string> entryQuestConditions = questParam19.GetEntryQuestConditions();
              bool flag11 = entryQuestConditions != null && entryQuestConditions.Count > 0;
              if (this.Index == 0)
              {
                ((Component) this).gameObject.SetActive(flag11);
                break;
              }
              ((Component) this).gameObject.SetActive(!flag11);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.QUEST_IS_UNIT_ENABLEENTRYCONDITION:
            GameObject gameObject1 = (GameObject) null;
            GameObject gameObject2 = (GameObject) null;
            GameObject gameObject3 = (GameObject) null;
            if (this.SerializeGameObject != null && this.SerializeGameObject.Length > 0)
            {
              if (this.SerializeGameObject.Length > 0)
                gameObject1 = this.SerializeGameObject[0];
              if (this.SerializeGameObject.Length > 1)
                gameObject2 = this.SerializeGameObject[1];
              if (this.SerializeGameObject.Length > 2)
                gameObject3 = this.SerializeGameObject[2];
            }
            else
              gameObject1 = ((Component) this).gameObject;
            UnitData unitData55 = this.GetUnitData();
            if (unitData55 != null)
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject3, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidBossManager.Instance, (UnityEngine.Object) null))
              {
                if (WorldRaidBossManager.GetCurrentBossUsedUnitInameList().Contains(unitData55.UnitID))
                {
                  gameObject3.SetActive(true);
                  GameUtility.SetGameObjectActive(gameObject1, false);
                  GameUtility.SetGameObjectActive(gameObject2, false);
                  break;
                }
                gameObject3.SetActive(false);
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
              {
                QuestParam questParam20;
                if ((questParam20 = this.GetQuestParam()) != null)
                {
                  string empty2 = string.Empty;
                  bool flag12 = questParam20.IsEntryQuestCondition(unitData55, ref empty2);
                  if (this.Index == 0)
                    gameObject1.SetActive(flag12);
                  else
                    gameObject1.SetActive(!flag12);
                  if (!flag12)
                  {
                    GameUtility.SetGameObjectActive(gameObject2, false);
                    GameUtility.SetGameObjectActive(gameObject3, false);
                    break;
                  }
                }
                else
                  gameObject1.SetActive(false);
              }
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
                break;
              if (DataSource.FindDataOfClass<UnitSameGroupParam>(((Component) this).gameObject, (UnitSameGroupParam) null) != null)
              {
                gameObject2.SetActive(true);
                GameUtility.SetGameObjectActive(gameObject1, false);
                GameUtility.SetGameObjectActive(gameObject3, false);
                break;
              }
              gameObject2.SetActive(false);
              break;
            }
            GameUtility.SetGameObjectActive(gameObject1, false);
            GameUtility.SetGameObjectActive(gameObject2, false);
            GameUtility.SetGameObjectActive(gameObject3, false);
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_MAINUNITCONDITION:
            UnitData unitData56;
            QuestParam questParam21;
            if ((unitData56 = this.GetUnitData()) != null && (questParam21 = this.GetQuestParam()) != null)
            {
              this.SetTextValue(GameUtility.ComposeCharacterQuestMainUnitConditionText(unitData56, questParam21));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_EPISODENUM:
            UnitData unitData57;
            if ((unitData57 = this.GetUnitData()) != null)
            {
              UnitData.CharacterQuestParam charaEpisodeData = unitData57.GetCurrentCharaEpisodeData();
              if (charaEpisodeData != null)
              {
                this.SetTextValue(LocalizedText.Get("sys.CHARQUEST_EP_NUM", (object) charaEpisodeData.EpisodeNum));
                break;
              }
              this.ResetToDefault();
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_EPISODENAME:
            UnitData unitData58;
            if ((unitData58 = this.GetUnitData()) != null)
            {
              UnitData.CharacterQuestParam charaEpisodeData = unitData58.GetCurrentCharaEpisodeData();
              if (charaEpisodeData != null)
              {
                this.SetTextValue(charaEpisodeData.EpisodeTitle);
                break;
              }
              this.ResetToDefault();
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_LIMITED_ITEM_BUYAMOUNT:
            LimitedShopItem limitedShopItem5 = this.GetLimitedShopItem();
            if (limitedShopItem5 == null)
              break;
            this.SetTextValue(limitedShopItem5.remaining_num);
            break;
          case GameParameter.ParameterTypes.UNIT_IS_JOBMASTER:
            JobData jobData = DataSource.FindDataOfClass<JobData>(((Component) this).gameObject, (JobData) null);
            if (jobData != null)
            {
              ((Component) this).gameObject.SetActive(jobData.CheckJobMaster());
              break;
            }
            UnitData unitData59;
            if ((unitData59 = this.GetUnitData()) != null)
            {
              if (this.Index == -1)
                jobData = unitData59.CurrentJob;
              else if (unitData59.IsJobAvailable(this.Index))
                jobData = unitData59.GetJobData(this.Index);
              if (jobData != null && jobData.CheckJobMaster())
              {
                ((Component) this).gameObject.SetActive(true);
                break;
              }
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.UNIT_NEXTAWAKE:
            UnitData unitData60;
            if ((unitData60 = this.GetUnitData()) != null)
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
              {
                int num29 = unitData60.AwakeLv + 1;
                int awakeLevelCap = unitData60.GetAwakeLevelCap();
                int num30 = 5;
                if (awakeLevelCap / num30 > this.Index)
                {
                  int index13 = num30;
                  int num31 = this.Index * num30;
                  if ((this.Index + 1) * num30 > num29)
                    index13 = num29 - num31 >= 0 ? num29 % num30 : 0;
                  this.SetImageIndex(index13);
                  ((Component) this).gameObject.SetActive(true);
                  break;
                }
                ((Component) this).gameObject.SetActive(false);
                break;
              }
              this.SetTextValue(unitData60.AwakeLv);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTIPLAY_ADD_INPUTTIME:
            SceneBattle instance35 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance35, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue("+" + ((int) instance35.MultiPlayAddInputTime).ToString());
            break;
          case GameParameter.ParameterTypes.UNIT_IS_AWAKEMAX:
            UnitData unitData61;
            if ((unitData61 = this.GetUnitData()) != null)
            {
              int awakeLv = unitData61.AwakeLv;
              int awakeLevelCap = unitData61.GetAwakeLevelCap();
              if (this.Index == 0)
              {
                ((Component) this).gameObject.SetActive(awakeLevelCap > awakeLv);
                break;
              }
              ((Component) this).gameObject.SetActive(awakeLevelCap <= awakeLv);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.CONFIG_IS_AUTOPLAY:
            bool flag13 = GameUtility.Config_UseAutoPlay.Value;
            ((Component) this).gameObject.SetActive(this.Index != 0 ? !flag13 : flag13);
            break;
          case GameParameter.ParameterTypes.FRIEND_ISFAVORITE:
            bool flag14 = false;
            FriendData friendData5 = this.GetFriendData();
            if (friendData5 != null)
              flag14 = friendData5.IsFavorite;
            if (this.Index == 1)
              flag14 = !flag14;
            ((Component) this).gameObject.SetActive(flag14);
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_ENTRYCONDITION:
            QuestParam questParam22;
            if ((questParam22 = this.GetQuestParam()) != null)
            {
              List<string> questConditionsCh = questParam22.GetEntryQuestConditionsCh(includeUnits: false);
              string str22 = string.Empty;
              if (questConditionsCh != null && questConditionsCh.Count > 0)
              {
                for (int index14 = 0; index14 < questConditionsCh.Count; ++index14)
                {
                  if (index14 > 0)
                    str22 = this.Index != 0 ? str22 + "、" : str22 + "\n";
                  str22 += questConditionsCh[index14];
                }
              }
              UnitData unitData62;
              if ((unitData62 = this.GetUnitData()) != null)
              {
                List<string> unlockConditions = unitData62.GetQuestUnlockConditions(questParam22);
                if (unlockConditions != null && unlockConditions.Count > 0)
                {
                  for (int index15 = 0; index15 < unlockConditions.Count; ++index15)
                  {
                    if (!string.IsNullOrEmpty(str22))
                      str22 = this.Index != 0 ? str22 + "、" : str22 + "\n";
                    str22 += unlockConditions[index15];
                  }
                }
              }
              if (!string.IsNullOrEmpty(str22))
              {
                this.SetTextValue(str22);
                break;
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_IS_ENTRYCONDITION:
            QuestParam questParam23;
            if ((questParam23 = this.GetQuestParam()) != null)
            {
              List<string> questConditionsCh = questParam23.GetEntryQuestConditionsCh();
              bool flag15 = true;
              UnitData unitData63;
              if ((unitData63 = this.GetUnitData()) != null)
              {
                List<string> unlockConditions = unitData63.GetQuestUnlockConditions(questParam23);
                if (unlockConditions != null && unlockConditions.Count > 0)
                {
                  for (int index16 = 0; index16 < unlockConditions.Count; ++index16)
                  {
                    if (!string.IsNullOrEmpty(unlockConditions[index16]))
                    {
                      flag15 = false;
                      break;
                    }
                  }
                }
              }
              bool flag16 = questConditionsCh != null && questConditionsCh.Count > 0 || !flag15;
              if (this.Index == 0)
              {
                ((Component) this).gameObject.SetActive(flag16);
                break;
              }
              ((Component) this).gameObject.SetActive(!flag16);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.QUEST_CHARACTER_CONDITIONATTENTION:
            UnitData unitData64;
            if ((unitData64 = this.GetUnitData()) != null)
            {
              this.SetTextValue(LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_TITLE", (object) unitData64.UnitParam.name));
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.MULTIPLAY_RESUME_PLAYER_INDEX:
            SceneBattle instance36 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance36, (UnityEngine.Object) null) || instance36.CurrentResumePlayer == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(instance36.CurrentResumePlayer.playerIndex);
            break;
          case GameParameter.ParameterTypes.MULTIPLAY_RESUME_PLAYER_IS_HOST:
            MyPhoton instance37 = PunMonoSingleton<MyPhoton>.Instance;
            SceneBattle instance38 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance38, (UnityEngine.Object) null) || instance38.CurrentResumePlayer == null)
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            ((Component) this).gameObject.SetActive(instance37.IsHost(instance38.CurrentResumePlayer.playerID));
            break;
          case GameParameter.ParameterTypes.MULTIPLAY_RESUME_BUT_NOT_PLAYER:
            SceneBattle instance39 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance39, (UnityEngine.Object) null))
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            ((Component) this).gameObject.SetActive(instance39.ResumeOnly);
            break;
          case GameParameter.ParameterTypes.EVENTCOIN_SHOPTYPEICON:
            EventCoinData dataOfClass55 = DataSource.FindDataOfClass<EventCoinData>(((Component) this).gameObject, (EventCoinData) null);
            if (dataOfClass55 == null || dataOfClass55.param == null)
              break;
            this.SetBuyPriceEventCoinTypeIcon(dataOfClass55.iname);
            break;
          case GameParameter.ParameterTypes.EVENTCOIN_ITEMNAME:
            EventCoinData dataOfClass56 = DataSource.FindDataOfClass<EventCoinData>(((Component) this).gameObject, (EventCoinData) null);
            if (dataOfClass56 != null && dataOfClass56.param != null)
            {
              this.SetTextValue(dataOfClass56.param.name);
              break;
            }
            this.SetTextValue(0);
            break;
          case GameParameter.ParameterTypes.EVENTCOIN_MESSAGE:
            EventCoinData dataOfClass57 = DataSource.FindDataOfClass<EventCoinData>(((Component) this).gameObject, (EventCoinData) null);
            if (dataOfClass57 != null && dataOfClass57.param != null)
            {
              this.SetTextValue(dataOfClass57.param.Flavor);
              break;
            }
            this.SetTextValue(0);
            break;
          case GameParameter.ParameterTypes.EVENTCOIN_POSSESSION:
            EventCoinData dataOfClass58 = DataSource.FindDataOfClass<EventCoinData>(((Component) this).gameObject, (EventCoinData) null);
            if (dataOfClass58 != null && dataOfClass58.have != null)
            {
              this.SetTextValue(dataOfClass58.have.Num);
              break;
            }
            this.SetTextValue(0);
            break;
          case GameParameter.ParameterTypes.EVENTCOIN_PRICEICON:
            EventShopItem eventShopItem2 = this.GetEventShopItem();
            if (eventShopItem2 != null)
            {
              this.SetBuyPriceEventCoinTypeIcon(eventShopItem2.cost_iname);
              break;
            }
            ItemParam dataOfClass59 = DataSource.FindDataOfClass<ItemParam>(((Component) this).gameObject, (ItemParam) null);
            if (dataOfClass59 != null)
            {
              this.SetBuyPriceEventCoinTypeIcon(dataOfClass59.iname);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.SHOP_EVENT_ITEM_BUYAMOUNT:
            EventShopItem eventShopItem3 = this.GetEventShopItem();
            if (eventShopItem3 != null)
            {
              this.SetTextValue(eventShopItem3.remaining_num);
              break;
            }
            this.SetTextValue(0);
            break;
          case GameParameter.ParameterTypes.TROPHY_REMAININGTIME:
            TrophyParam trophyParam5 = this.GetTrophyParam();
            if (trophyParam5 == null)
              break;
            DateTime serverTime2 = TimeManager.ServerTime;
            DateTime base_time = trophyParam5.CategoryParam.end_at.DateTimes;
            if (trophyParam5.CategoryParam.IsBeginner)
            {
              DateTime beginnerEndTime = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
              base_time = !(base_time <= beginnerEndTime) ? beginnerEndTime : base_time;
            }
            if (!trophyParam5.CategoryParam.IsBeginner)
              base_time = trophyParam5.CategoryParam.GetQuestTime(base_time, true);
            if (MonoSingleton<GameManager>.Instance.Player.TrophyData.GetTrophyCounter(trophyParam5).IsCompleted)
              base_time = trophyParam5.GetGraceRewardTime();
            if (!string.IsNullOrEmpty(trophyParam5.CategoryParam.end_at.StrTime) && !trophyParam5.IsDaily && base_time >= serverTime2)
            {
              TimeSpan timeSpan6 = base_time - serverTime2;
              if (timeSpan6.Days > 0)
                this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_DAY"), (object) timeSpan6.Days));
              else if (timeSpan6.Hours > 0)
                this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_HOUR"), (object) timeSpan6.Hours));
              else
                this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_MINUTE"), (object) timeSpan6.Minutes));
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_OKYAKUSAMACODE2:
            string configOkyakusamaCode2 = GameUtility.Config_OkyakusamaCode;
            if (this.Index == 0)
            {
              ((Component) this).gameObject.SetActive(!string.IsNullOrEmpty(configOkyakusamaCode2));
              break;
            }
            if (this.Index != 1)
              break;
            if (string.IsNullOrEmpty(configOkyakusamaCode2))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(configOkyakusamaCode2);
            break;
          case GameParameter.ParameterTypes.VERSUS_UNIT_IMAGE:
            if (MonoSingleton<GameManager>.Instance.AudienceMode)
            {
              JSON_MyPhotonPlayerParam player9 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
              if (player9 != null)
              {
                player9.SetupUnits();
                UnitData unit37 = player9.units[0].unit;
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitSkinImage(unit37.UnitParam, unit37.GetSelectedSkin(), unit37.CurrentJob.JobID);
                break;
              }
            }
            else
            {
              JSON_MyPhotonPlayerParam versusPlayerParam = this.GetVersusPlayerParam();
              if (versusPlayerParam != null)
              {
                versusPlayerParam.SetupUnits();
                UnitData unit38 = versusPlayerParam.units[0].unit;
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitSkinImage(unit38.UnitParam, unit38.GetSelectedSkin(), unit38.CurrentJob.JobID);
                break;
              }
              if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
              {
                VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                if (versusCpu != null && versusCpu.Units != null && versusCpu.Units.Length > 0)
                {
                  UnitData unit39 = versusCpu.Units[0];
                  GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitSkinImage(unit39.UnitParam, unit39.GetSelectedSkin(), unit39.CurrentJob.JobID);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_PLAYER_NAME:
            if (MonoSingleton<GameManager>.Instance.AudienceMode)
            {
              JSON_MyPhotonPlayerParam player10 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
              if (player10 != null)
              {
                this.SetTextValue(player10.playerName);
                break;
              }
            }
            else
            {
              JSON_MyPhotonPlayerParam versusPlayerParam = this.GetVersusPlayerParam();
              if (versusPlayerParam != null)
              {
                this.SetTextValue(versusPlayerParam.playerName);
                break;
              }
              if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
              {
                VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                if (versusCpu != null)
                {
                  this.SetTextValue(versusCpu.Name);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_PLAYER_LEVEL:
            if (MonoSingleton<GameManager>.Instance.AudienceMode)
            {
              JSON_MyPhotonPlayerParam player11 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
              if (player11 != null)
              {
                this.SetTextValue(player11.playerLevel.ToString());
                break;
              }
            }
            else
            {
              JSON_MyPhotonPlayerParam versusPlayerParam = this.GetVersusPlayerParam();
              if (versusPlayerParam != null)
              {
                this.SetTextValue(versusPlayerParam.playerLevel.ToString());
                break;
              }
              if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
              {
                VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                if (versusCpu != null)
                {
                  this.SetTextValue(versusCpu.Lv);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_PLAYER_TOTALATK:
            if (MonoSingleton<GameManager>.Instance.AudienceMode)
            {
              JSON_MyPhotonPlayerParam player12 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
              if (player12 != null)
              {
                this.SetTextValue(player12.totalAtk.ToString());
                break;
              }
            }
            else
            {
              JSON_MyPhotonPlayerParam versusPlayerParam = this.GetVersusPlayerParam();
              if (versusPlayerParam != null)
              {
                this.SetTextValue(versusPlayerParam.totalAtk.ToString());
                break;
              }
              if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
              {
                VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                if (versusCpu != null)
                {
                  this.SetTextValue(versusCpu.Score);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_RESULT:
            SceneBattle instance40 = SceneBattle.Instance;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance40, (UnityEngine.Object) null))
            {
              BattleCore battle = instance40.Battle;
              if (battle != null)
              {
                BattleCore.Record questRecord = battle.GetQuestRecord();
                if (questRecord != null)
                {
                  ((Component) this).gameObject.SetActive(questRecord.result == (BattleCore.QuestResult) this.InstanceType);
                  break;
                }
              }
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_RANK:
            break;
          case GameParameter.ParameterTypes.VERSUS_RANK_POINT:
            break;
          case GameParameter.ParameterTypes.VERSUS_RANK_NEXT_POINT:
            break;
          case GameParameter.ParameterTypes.VERSUS_RANK_ICON:
            break;
          case GameParameter.ParameterTypes.VERSUS_RANK_ICON_INDEX:
            break;
          case GameParameter.ParameterTypes.VERSUS_ROOMPLAYER_RANK_ICON:
            break;
          case GameParameter.ParameterTypes.VERSUS_ROOMPLAYER_RANK_ICON_INDEX:
            break;
          case GameParameter.ParameterTypes.VERSUS_MAP_THUMNAIL:
            Image component8 = ((Component) this).GetComponent<Image>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component8, (UnityEngine.Object) null))
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            QuestParam dataOfClass60 = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
            if (dataOfClass60 != null && !string.IsNullOrEmpty(dataOfClass60.VersusThumnail))
            {
              SpriteSheet spriteSheet2 = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet2, (UnityEngine.Object) null))
              {
                component8.sprite = spriteSheet2.GetSprite(dataOfClass60.VersusThumnail);
                ((Behaviour) component8).enabled = true;
                break;
              }
            }
            component8.sprite = (Sprite) null;
            ((Behaviour) component8).enabled = false;
            break;
          case GameParameter.ParameterTypes.VERSUS_MAP_THUMNAIL2:
            Image component9 = ((Component) this).GetComponent<Image>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component9, (UnityEngine.Object) null))
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            VersusMapParam dataOfClass61 = DataSource.FindDataOfClass<VersusMapParam>(((Component) this).gameObject, (VersusMapParam) null);
            if (dataOfClass61 != null && dataOfClass61.quest != null && !string.IsNullOrEmpty(dataOfClass61.quest.VersusThumnail))
            {
              SpriteSheet spriteSheet3 = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet3, (UnityEngine.Object) null))
              {
                component9.sprite = spriteSheet3.GetSprite(dataOfClass61.quest.VersusThumnail);
                break;
              }
            }
            component9.sprite = (Sprite) null;
            ((Component) component9).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.VERSUS_MAP_NAME:
            VersusMapParam dataOfClass62 = DataSource.FindDataOfClass<VersusMapParam>(((Component) this).gameObject, (VersusMapParam) null);
            if (dataOfClass62 != null)
            {
              this.SetTextValue(dataOfClass62.quest.name);
              break;
            }
            this.ResetToDefault();
            break;
          case GameParameter.ParameterTypes.VERSUS_MAP_SELECT:
            VersusMapParam dataOfClass63 = DataSource.FindDataOfClass<VersusMapParam>(((Component) this).gameObject, (VersusMapParam) null);
            if (dataOfClass63 != null)
            {
              ((Component) this).gameObject.SetActive(dataOfClass63.selected);
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          case GameParameter.ParameterTypes.SHOP_ARENA_COIN:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.ArenaCoin);
            break;
          case GameParameter.ParameterTypes.SHOP_MULTI_COIN:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.MultiCoin);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COINCOM:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.ComCoin);
            break;
          case GameParameter.ParameterTypes.GLOBAL_PLAYER_FREECOINSET:
            GameManager instance41 = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(instance41.Player.FreeCoin + instance41.Player.ComCoin);
            break;
          case GameParameter.ParameterTypes.PRODUCT_COINNUM:
            PaymentManager.Product dataOfClass64 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
            if (dataOfClass64 == null)
            {
              this.ResetToDefault();
              break;
            }
            string str23 = string.Empty;
            FixParam fixParam4 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (!dataOfClass64.productID.Contains((string) fixParam4.VipCardProduct) && !dataOfClass64.productID.Contains((string) fixParam4.PremiumProduct))
            {
              if (this.Index == 2)
              {
                int numPaid = dataOfClass64.numPaid;
                str23 = string.Format(LocalizedText.Get("sys.NUM_PAID"), (object) numPaid);
              }
              else if (this.Index == 1)
              {
                int numFree = dataOfClass64.numFree;
                str23 = string.Format(LocalizedText.Get("sys.NUM_FREE"), (object) numFree);
              }
              else
              {
                int num32 = dataOfClass64.numFree + dataOfClass64.numPaid;
                str23 = string.Format(LocalizedText.Get("sys.NUM_TOTAL"), (object) num32);
              }
            }
            this.SetTextValue(str23);
            break;
          case GameParameter.ParameterTypes.ARTIFACT_DRILLING_COUNT:
            ArtifactParam artifactParam5 = this.GetArtifactParam();
            if (artifactParam5 != null)
            {
              this.SetTextValue(artifactParam5.maxnum);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              this.ResetToDefault();
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_DRILLING_MAXDRAW:
            ArtifactParam artifactParam6 = this.GetArtifactParam();
            if (artifactParam6 != null && artifactParam6.maxnum == 999)
            {
              ((Component) this).gameObject.SetActive(true);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              ((Component) this).gameObject.SetActive(false);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.ARTIFACT_DRILLING_NOTMAXDRAW:
            ArtifactParam artifactParam7 = this.GetArtifactParam();
            if (artifactParam7 != null && artifactParam7.maxnum < 999)
            {
              ((Component) this).gameObject.SetActive(true);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
            else
            {
              ((Component) this).gameObject.SetActive(false);
              goto case GameParameter.ParameterTypes.SKILL_ICON;
            }
          case GameParameter.ParameterTypes.PRODUCT_BUYCOINNAME:
            BuyCoinProductParam dataOfClass65 = DataSource.FindDataOfClass<BuyCoinProductParam>(((Component) this).gameObject, (BuyCoinProductParam) null);
            if (dataOfClass65 == null || string.IsNullOrEmpty(dataOfClass65.Title))
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(dataOfClass65.Title);
            break;
          case GameParameter.ParameterTypes.PRODUCT_BUYCOINDESC:
            BuyCoinProductParam dataOfClass66 = DataSource.FindDataOfClass<BuyCoinProductParam>(((Component) this).gameObject, (BuyCoinProductParam) null);
            if (dataOfClass66 == null || string.IsNullOrEmpty(dataOfClass66.Description))
            {
              this.ResetToDefault();
              break;
            }
            this.SelectCoinDescription(dataOfClass66.Description);
            break;
          default:
            switch (parameterType - 3200)
            {
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                RaidBossParam dataOfClass67 = DataSource.FindDataOfClass<RaidBossParam>(((Component) this).gameObject, (RaidBossParam) null);
                if (dataOfClass67 != null)
                {
                  this.SetTextValue(dataOfClass67.Name);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                RaidBossInfo dataOfClass68 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass68 != null)
                {
                  this.SetTextValue(dataOfClass68.HP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                RaidBossInfo dataOfClass69 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass69 != null)
                {
                  this.SetTextValue(dataOfClass69.MaxHP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                RaidBossInfo dataOfClass70 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass70 != null)
                {
                  this.SetTextValue((dataOfClass70.HP * 100 / dataOfClass70.MaxHP).ToString() + "%");
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                RaidBossInfo dataOfClass71 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass71 != null)
                {
                  this.SetSliderValue(dataOfClass71.HP, dataOfClass71.MaxHP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                UnitParam dataOfClass72 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                if (dataOfClass72 != null)
                {
                  GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitIconMedium(dataOfClass72, (string) null);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                UnitParam dataOfClass73 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                if (dataOfClass73 != null)
                {
                  GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(dataOfClass73, (string) null);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                RaidRescueMember raidRescueMember1 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember1 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss1 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember1.BossId);
                if (raidBoss1 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                UnitParam unitParam13 = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss1.UnitIName);
                if (unitParam13 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                string str24 = AssetPath.UnitIconSmall(unitParam13, (string) null);
                if (string.IsNullOrEmpty(str24))
                {
                  this.ResetToDefault();
                  return;
                }
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str24;
                return;
              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                RaidRescueMember raidRescueMember2 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember2 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(raidRescueMember2.Round);
                return;
              case GameParameter.ParameterTypes.QUEST_NAME:
                RaidRescueMember raidRescueMember3 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember3 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss2 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember3.BossId);
                if (raidBoss2 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(raidBoss2.Name);
                return;
              case GameParameter.ParameterTypes.QUEST_STAMINA:
                RaidRescueMember raidRescueMember4 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember4 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(raidRescueMember4.AreaId);
                if (raidArea == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_RESCUE_AREA"), (object) raidArea.Order));
                return;
              case GameParameter.ParameterTypes.QUEST_STATE:
                RaidRescueMember raidRescueMember5 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember5 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(raidRescueMember5.Name);
                return;
              case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                RaidRescueMember raidRescueMember6 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember6 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(raidRescueMember6.Lv);
                return;
              case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                RaidRescueMember raidRescueMember7 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember7 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss3 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember7.BossId);
                if (raidBoss3 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                TimeSpan timeSpan7 = TimeManager.ServerTime - TimeManager.FromUnixTime(raidRescueMember7.StartTime);
                TimeSpan timeSpan8 = raidBoss3.TimeLimitSpan - timeSpan7;
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_RESCUE_REMAIN_TIME"), (object) (int) timeSpan8.TotalHours, (object) timeSpan8.Minutes));
                return;
              case GameParameter.ParameterTypes.ITEM_ICON:
                RaidRescueMember raidRescueMember8 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember8 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(raidRescueMember8.CurrentHp);
                return;
              case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                RaidRescueMember raidRescueMember9 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember9 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss4 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember9.BossId);
                if (raidBoss4 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(RaidBossParam.CalcMaxHP(raidBoss4, raidRescueMember9.Round));
                return;
              case GameParameter.ParameterTypes.SUPPORTER_NAME:
                RaidRescueMember raidRescueMember10 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember10 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss5 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember10.BossId);
                if (raidBoss5 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                int num33 = RaidBossParam.CalcMaxHP(raidBoss5, raidRescueMember10.Round);
                this.SetTextValue((raidRescueMember10.CurrentHp * 100 / num33).ToString() + "%");
                return;
              case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                RaidRescueMember raidRescueMember11 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember11 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss6 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember11.BossId);
                if (raidBoss6 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                int maxValue1 = RaidBossParam.CalcMaxHP(raidBoss6, raidRescueMember11.Round);
                this.SetSliderValue(raidRescueMember11.CurrentHp, maxValue1);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                RaidRescueMember dataOfClass74 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null);
                if (dataOfClass74 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ImageArray component10 = ((Component) this).gameObject.GetComponent<ImageArray>();
                if (dataOfClass74.MemberType == RaidRescueMemberType.Friend)
                {
                  component10.ImageIndex = 0;
                  return;
                }
                if (dataOfClass74.MemberType == RaidRescueMemberType.Guild)
                {
                  component10.ImageIndex = 1;
                  return;
                }
                ((Component) this).gameObject.SetActive(false);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
                RaidSOSMember dataOfClass75 = DataSource.FindDataOfClass<RaidSOSMember>(((Component) this).gameObject, (RaidSOSMember) null);
                if (dataOfClass75 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass75.Name);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_ATK:
                RaidSOSMember dataOfClass76 = DataSource.FindDataOfClass<RaidSOSMember>(((Component) this).gameObject, (RaidSOSMember) null);
                if (dataOfClass76 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass76.Lv);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_HP:
                RaidSOSMember dataOfClass77 = DataSource.FindDataOfClass<RaidSOSMember>(((Component) this).gameObject, (RaidSOSMember) null);
                if (dataOfClass77 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ImageArray component11 = ((Component) this).gameObject.GetComponent<ImageArray>();
                if (dataOfClass77.MemberType == RaidRescueMemberType.Friend)
                {
                  component11.ImageIndex = 0;
                  return;
                }
                if (dataOfClass77.MemberType == RaidRescueMemberType.Guild)
                {
                  component11.ImageIndex = 1;
                  return;
                }
                ((Component) this).gameObject.SetActive(false);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_MAGIC:
                RaidSOSMember dataOfClass78 = DataSource.FindDataOfClass<RaidSOSMember>(((Component) this).gameObject, (RaidSOSMember) null);
                if (dataOfClass78 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                TimeSpan timeSpan9 = TimeManager.ServerTime - TimeManager.FromUnixTime(dataOfClass78.LastBattleTime);
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_RESCUE_REMAIN_TIME"), (object) timeSpan9.Hours, (object) timeSpan9.Minutes));
                return;
              case GameParameter.ParameterTypes.SUPPORTER_RARITY:
                RaidBossInfo dataOfClass79 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass79 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                if (dataOfClass79.HP > 0)
                {
                  ((Component) this).gameObject.SetActive(true);
                  if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.CloseSchedule)
                    return;
                  Button componentInChildren = ((Component) this).gameObject.GetComponentInChildren<Button>();
                  if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
                    return;
                  ((Selectable) componentInChildren).interactable = false;
                  return;
                }
                ((Component) this).gameObject.SetActive(false);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_ELEMENT:
                RaidBossData dataOfClass80 = DataSource.FindDataOfClass<RaidBossData>(((Component) this).gameObject, (RaidBossData) null);
                if (dataOfClass80 == null || MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() == RaidManager.RaidScheduleType.CloseSchedule)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                if (dataOfClass80.RaidBossInfo.HP <= 0 || RaidManager.Instance.SelectedRaidOwnerType != RaidManager.RaidOwnerType.Self)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                if (dataOfClass80.SOSStatus == RaidSOSStatus.Invalid)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                if (this.InstanceType == 0)
                {
                  ((Component) this).gameObject.SetActive(dataOfClass80.SOSStatus == RaidSOSStatus.Valid);
                  return;
                }
                ((Component) this).gameObject.SetActive(dataOfClass80.SOSStatus == RaidSOSStatus.Done);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_ICON:
                RaidBossData dataOfClass81 = DataSource.FindDataOfClass<RaidBossData>(((Component) this).gameObject, (RaidBossData) null);
                if (dataOfClass81 == null || MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() == RaidManager.RaidScheduleType.CloseSchedule)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(dataOfClass81.RaidBossInfo.HP > 0 && RaidManager.Instance.SelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue);
                return;
              case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLDESC:
                RaidBossInfo dataOfClass82 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass82 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(dataOfClass82.IsReward);
                return;
              case GameParameter.ParameterTypes.QUEST_SUBTITLE:
                RaidPeriodParam activeRaidPeriod1 = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(end_offset_hour: 24);
                if (activeRaidPeriod1 != null)
                {
                  ((Component) this).gameObject.SetActive(true);
                  if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() == RaidManager.RaidScheduleType.CloseSchedule)
                  {
                    this.SetImageIndex(2);
                    return;
                  }
                  this.SetImageIndex(!(activeRaidPeriod1.EndAt > TimeManager.ServerTime) ? 1 : 0);
                  return;
                }
                ((Component) this).gameObject.SetActive(false);
                return;
              case GameParameter.ParameterTypes.UNIT_LEVEL:
                RaidBossInfo dataOfClass83 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass83 != null)
                {
                  ImageArray component12 = ((Component) this).gameObject.GetComponent<ImageArray>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component12, (UnityEngine.Object) null))
                  {
                    float num34 = (float) dataOfClass83.HP / (float) dataOfClass83.MaxHP;
                    if ((double) num34 > 0.6)
                    {
                      component12.ImageIndex = 0;
                      return;
                    }
                    if ((double) num34 > 0.3)
                    {
                      component12.ImageIndex = 1;
                      return;
                    }
                    component12.ImageIndex = 2;
                    return;
                  }
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.UNIT_HP:
                RaidRescueMember raidRescueMember12 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember12 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                UnitIcon component13 = ((Component) this).gameObject.GetComponent<UnitIcon>();
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) component13, (UnityEngine.Object) null))
                {
                  this.ResetToDefault();
                  return;
                }
                DataSource.Bind<UnitData>(((Component) this).gameObject, raidRescueMember12.Unit);
                component13.UpdateValue();
                return;
              case GameParameter.ParameterTypes.UNIT_HPMAX:
                RaidSOSMember dataOfClass84 = DataSource.FindDataOfClass<RaidSOSMember>(((Component) this).gameObject, (RaidSOSMember) null);
                if (dataOfClass84 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                UnitIcon component14 = ((Component) this).gameObject.GetComponent<UnitIcon>();
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) component14, (UnityEngine.Object) null))
                {
                  this.ResetToDefault();
                  return;
                }
                DataSource.Bind<UnitData>(((Component) this).gameObject, dataOfClass84.Unit);
                component14.UpdateValue();
                return;
              case GameParameter.ParameterTypes.UNIT_ATK:
                AwardParam data1 = DataSource.FindDataOfClass<AwardParam>(((Component) this).gameObject, (AwardParam) null);
                if (data1 != null)
                  return;
                RaidRescueMember raidRescueMember13 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember13 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                if (!string.IsNullOrEmpty(raidRescueMember13.SelectedAward))
                  data1 = MonoSingleton<GameManager>.Instance.GetAwardParam(raidRescueMember13.SelectedAward);
                DataSource.Bind<AwardParam>(((Component) this).gameObject, data1);
                AwardItem component15 = ((Component) this).gameObject.GetComponent<AwardItem>();
                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component15, (UnityEngine.Object) null))
                  return;
                ((Behaviour) component15).enabled = false;
                ((Behaviour) component15).enabled = true;
                return;
              case GameParameter.ParameterTypes.UNIT_MAG:
                RaidRescueMember raidRescueMember14 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember14 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                TimeSpan timeSpan10 = TimeManager.ServerTime - raidRescueMember14.LastLogin;
                if (timeSpan10.TotalDays >= 1.0)
                {
                  this.SetTextValue(string.Format(LocalizedText.Get("sys.LASTLOGIN_DAY"), (object) timeSpan10.Days));
                  return;
                }
                if (timeSpan10.TotalHours >= 1.0)
                {
                  this.SetTextValue(string.Format(LocalizedText.Get("sys.LASTLOGIN_HOUR"), (object) timeSpan10.Hours));
                  return;
                }
                this.SetTextValue(string.Format(LocalizedText.Get("sys.LASTLOGIN_MINUTE"), (object) timeSpan10.Minutes));
                return;
              case GameParameter.ParameterTypes.UNIT_ICON:
                RaidRankingData dataOfClass85 = DataSource.FindDataOfClass<RaidRankingData>(((Component) this).gameObject, (RaidRankingData) null);
                if (dataOfClass85 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                if (dataOfClass85.Rank == 1)
                {
                  this.SetImageIndex(0);
                  return;
                }
                if (dataOfClass85.Rank == 2)
                {
                  this.SetImageIndex(1);
                  return;
                }
                if (dataOfClass85.Rank == 3)
                {
                  this.SetImageIndex(2);
                  return;
                }
                this.SetImageIndex(3);
                return;
              case GameParameter.ParameterTypes.UNIT_NAME:
                RaidRankingData dataOfClass86 = DataSource.FindDataOfClass<RaidRankingData>(((Component) this).gameObject, (RaidRankingData) null);
                if (dataOfClass86 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                if (dataOfClass86.Rank <= 3)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(true);
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_RANKING_RANK"), (object) dataOfClass86.Rank));
                return;
              case GameParameter.ParameterTypes.UNIT_RARITY:
                RaidRankingData dataOfClass87 = DataSource.FindDataOfClass<RaidRankingData>(((Component) this).gameObject, (RaidRankingData) null);
                if (dataOfClass87 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass87.Score);
                return;
              case GameParameter.ParameterTypes.PARTY_LEADERSKILLNAME:
                RaidRankingData dataOfClass88 = DataSource.FindDataOfClass<RaidRankingData>(((Component) this).gameObject, (RaidRankingData) null);
                if (dataOfClass88 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass88.Name);
                return;
              case GameParameter.ParameterTypes.PARTY_LEADERSKILLDESC:
                RaidRankingData dataOfClass89 = DataSource.FindDataOfClass<RaidRankingData>(((Component) this).gameObject, (RaidRankingData) null);
                if (dataOfClass89 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass89.Lv);
                return;
              case GameParameter.ParameterTypes.UNIT_DEF:
                Image component16 = ((Component) this).GetComponent<Image>();
                RaidBossParam dataOfClass90 = DataSource.FindDataOfClass<RaidBossParam>(((Component) this).gameObject, (RaidBossParam) null);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component16, (UnityEngine.Object) null) && dataOfClass90 != null)
                {
                  UnitParam unitParam14 = MonoSingleton<GameManager>.Instance.GetUnitParam(dataOfClass90.UnitIName);
                  GameSettings instance42 = GameSettings.Instance;
                  if (unitParam14 != null && EElement.None <= unitParam14.element && unitParam14.element < (EElement) instance42.Elements_IconSmall.Length)
                  {
                    component16.sprite = instance42.Elements_IconSmall[(int) unitParam14.element];
                    return;
                  }
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.UNIT_MND:
                Image component17 = ((Component) this).GetComponent<Image>();
                RaidRescueMember raidRescueMember15 = DataSource.FindDataOfClass<RaidRescueMember>(((Component) this).gameObject, (RaidRescueMember) null) ?? RaidManager.Instance.SelectedRaidRescueMember;
                if (raidRescueMember15 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                RaidBossParam raidBoss7 = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(raidRescueMember15.BossId);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component17, (UnityEngine.Object) null) && raidBoss7 != null)
                {
                  UnitParam unitParam15 = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss7.UnitIName);
                  GameSettings instance43 = GameSettings.Instance;
                  if (unitParam15 != null && EElement.None <= unitParam15.element && unitParam15.element < (EElement) instance43.Elements_IconSmall.Length)
                  {
                    component17.sprite = instance43.Elements_IconSmall[(int) unitParam15.element];
                    return;
                  }
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.UNIT_SPEED:
                RaidRankingGuildData dataOfClass91 = DataSource.FindDataOfClass<RaidRankingGuildData>(((Component) this).gameObject, (RaidRankingGuildData) null);
                if (dataOfClass91 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                if (dataOfClass91.Rank == 1)
                {
                  this.SetImageIndex(0);
                  return;
                }
                if (dataOfClass91.Rank == 2)
                {
                  this.SetImageIndex(1);
                  return;
                }
                if (dataOfClass91.Rank == 3)
                {
                  this.SetImageIndex(2);
                  return;
                }
                this.SetImageIndex(3);
                return;
              case GameParameter.ParameterTypes.UNIT_LUCK:
                RaidRankingGuildData dataOfClass92 = DataSource.FindDataOfClass<RaidRankingGuildData>(((Component) this).gameObject, (RaidRankingGuildData) null);
                if (dataOfClass92 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                if (dataOfClass92.Rank <= 3)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(true);
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_RANKING_RANK"), (object) dataOfClass92.Rank));
                return;
              case GameParameter.ParameterTypes.UNIT_JOBNAME:
                RaidRankingGuildData dataOfClass93 = DataSource.FindDataOfClass<RaidRankingGuildData>(((Component) this).gameObject, (RaidRankingGuildData) null);
                if (dataOfClass93 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass93.Score);
                return;
              case GameParameter.ParameterTypes.UNIT_JOBRANK:
                RaidRankingGuildData dataOfClass94 = DataSource.FindDataOfClass<RaidRankingGuildData>(((Component) this).gameObject, (RaidRankingGuildData) null);
                if (dataOfClass94 == null || dataOfClass94.ViewGuild == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass94.ViewGuild.level);
                return;
              case GameParameter.ParameterTypes.UNIT_ELEMENT:
                ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(end_offset_hour: 24) != null);
                return;
              case GameParameter.ParameterTypes.PARTY_TOTALATK:
                RaidGuildInfo dataOfClass95 = DataSource.FindDataOfClass<RaidGuildInfo>(((Component) this).gameObject, (RaidGuildInfo) null);
                if (dataOfClass95 == null || dataOfClass95.Beat == null || dataOfClass95.Beat.Rank == 0)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass95.Beat.Rank);
                return;
              case GameParameter.ParameterTypes.INVENTORY_ITEMICON:
                RaidGuildInfo dataOfClass96 = DataSource.FindDataOfClass<RaidGuildInfo>(((Component) this).gameObject, (RaidGuildInfo) null);
                if (dataOfClass96 == null || dataOfClass96.Beat == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass96.Beat.Score);
                return;
              case GameParameter.ParameterTypes.INVENTORY_ITEMNAME:
                RaidGuildInfo dataOfClass97 = DataSource.FindDataOfClass<RaidGuildInfo>(((Component) this).gameObject, (RaidGuildInfo) null);
                if (dataOfClass97 == null || dataOfClass97.Rescue == null || dataOfClass97.Rescue.Rank == 0)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass97.Rescue.Rank);
                return;
              case GameParameter.ParameterTypes.ITEM_NAME:
                RaidGuildInfo dataOfClass98 = DataSource.FindDataOfClass<RaidGuildInfo>(((Component) this).gameObject, (RaidGuildInfo) null);
                if (dataOfClass98 == null || dataOfClass98.Rescue == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass98.Rescue.Score);
                return;
              case GameParameter.ParameterTypes.ITEM_DESC:
                RaidGuildMemberData dataOfClass99 = DataSource.FindDataOfClass<RaidGuildMemberData>(((Component) this).gameObject, (RaidGuildMemberData) null);
                if (dataOfClass99 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass99.BeatScore);
                return;
              case GameParameter.ParameterTypes.ITEM_SELLPRICE:
                RaidGuildMemberData dataOfClass100 = DataSource.FindDataOfClass<RaidGuildMemberData>(((Component) this).gameObject, (RaidGuildMemberData) null);
                if (dataOfClass100 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass100.RescueScore);
                return;
              case GameParameter.ParameterTypes.ITEM_BUYPRICE:
                RaidGuildMemberData dataOfClass101 = DataSource.FindDataOfClass<RaidGuildMemberData>(((Component) this).gameObject, (RaidGuildMemberData) null);
                if (dataOfClass101 == null)
                {
                  this.ResetToDefault();
                  return;
                }
                this.SetTextValue(dataOfClass101.Round);
                return;
              case GameParameter.ParameterTypes.ITEM_AMOUNT:
                RaidPeriodParam activeRaidPeriod2 = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(end_offset_hour: 24);
                if (activeRaidPeriod2 == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidCompleteReward(activeRaidPeriod2.CompleteRewardId) == null)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(true);
                return;
              case GameParameter.ParameterTypes.INVENTORY_ITEMAMOUNT:
                UnityEngine.UI.Text component18 = ((Component) this).gameObject.GetComponent<UnityEngine.UI.Text>();
                if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.CloseSchedule)
                  return;
                bool nowCheck;
                RaidPeriodTimeScheduleParam raidScheduleTime = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleTime(out nowCheck);
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) component18, (UnityEngine.Object) null) || raidScheduleTime == null || nowCheck)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                RaidPeriodParam activeRaidPeriod3 = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod();
                DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + raidScheduleTime.Begin + ":00");
                TimeSpan timeSpan11 = RaidManager.GetTimeSpan(raidScheduleTime.Open);
                DateTime dateTime2 = dateTime1 + timeSpan11;
                if (activeRaidPeriod3.EndAt < dateTime2)
                  dateTime2 = activeRaidPeriod3.EndAt;
                if (dateTime1.Hour == dateTime2.Hour && dateTime1.Day != dateTime2.Day)
                {
                  component18.text = string.Format(LocalizedText.Get("sys.RAIDTIME_BETWEEN"), (object) dateTime1.Hour, (object) dateTime1.Minute, (object) (dateTime2.Hour + 24), (object) dateTime2.Minute);
                  return;
                }
                component18.text = string.Format(LocalizedText.Get("sys.RAIDTIME_BETWEEN"), (object) dateTime1.Hour, (object) dateTime1.Minute, (object) dateTime2.Hour, (object) dateTime2.Minute);
                return;
              case GameParameter.ParameterTypes.PLAYER_NUMUNITS:
                switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
                {
                  case RaidManager.RaidScheduleType.Open:
                  case RaidManager.RaidScheduleType.OpenSchedule:
                    if (!((Component) this).gameObject.GetActive())
                      return;
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  default:
                    return;
                }
              case GameParameter.ParameterTypes.PLAYER_MAXUNITS:
                switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
                {
                  case RaidManager.RaidScheduleType.Close:
                  case RaidManager.RaidScheduleType.CloseSchedule:
                    if (!((Component) this).gameObject.GetActive())
                      return;
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  default:
                    return;
                }
              case GameParameter.ParameterTypes.GRID_HEIGHT:
                Button componentInChildren1 = ((Component) this).gameObject.GetComponentInChildren<Button>();
                if (!((Component) this).gameObject.GetActive() || UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
                  return;
                switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
                {
                  case RaidManager.RaidScheduleType.Close:
                  case RaidManager.RaidScheduleType.CloseSchedule:
                    ((Selectable) componentInChildren1).interactable = false;
                    return;
                  default:
                    ((Selectable) componentInChildren1).interactable = true;
                    return;
                }
              case GameParameter.ParameterTypes.SKILL_NAME:
                RaidBossInfo dataOfClass102 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
                if (dataOfClass102 != null && !dataOfClass102.IsReward && dataOfClass102.HP > 0 && MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() == RaidManager.RaidScheduleType.CloseSchedule)
                {
                  ((Component) this).gameObject.SetActive(true);
                  return;
                }
                ((Component) this).gameObject.SetActive(false);
                return;
              case GameParameter.ParameterTypes.SKILL_ICON:
                switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
                {
                  case RaidManager.RaidScheduleType.Open:
                  case RaidManager.RaidScheduleType.Close:
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  default:
                    ((Component) this).gameObject.SetActive(true);
                    return;
                }
              case GameParameter.ParameterTypes.SKILL_DESCRIPTION:
                if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.CloseSchedule)
                {
                  ((Component) this).gameObject.SetActive(false);
                  return;
                }
                ((Component) this).gameObject.SetActive(true);
                return;
              case GameParameter.ParameterTypes.SKILL_MP:
                GameManager instance44 = MonoSingleton<GameManager>.Instance;
                if (instance44.Player.mRaidRankRewardResult == null)
                  return;
                RaidPeriodParam raidPeriod = instance44.MasterParam.GetRaidPeriod(instance44.Player.mRaidRankRewardResult.PeriodId);
                if (raidPeriod == null)
                  return;
                this.SetTextValue(string.Format(LocalizedText.Get("sys.RAID_PERIOD_SCHEDULE"), (object) raidPeriod.BeginAt.Month, (object) raidPeriod.BeginAt.Day, (object) raidPeriod.BeginAt.Hour, (object) raidPeriod.BeginAt.Minute, (object) raidPeriod.EndAt.Month, (object) raidPeriod.EndAt.Day, (object) raidPeriod.EndAt.Hour, (object) raidPeriod.EndAt.Minute));
                goto label_3654;
              case GameParameter.ParameterTypes.BATTLE_GOLD:
                GameManager instance45 = MonoSingleton<GameManager>.Instance;
                if (instance45.Player.mRaidRankRewardResult != null && instance45.Player.mRaidRankRewardResult.Rank > 0)
                {
                  this.SetTextValue(instance45.Player.mRaidRankRewardResult.Rank);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.BATTLE_TREASURE:
                GameManager instance46 = MonoSingleton<GameManager>.Instance;
                if (instance46.Player.mRaidRankRewardResult != null && instance46.Player.mRaidRankRewardResult.Score > 0)
                {
                  this.SetTextValue(instance46.Player.mRaidRankRewardResult.Score);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.UNIT_MP:
                GameManager instance47 = MonoSingleton<GameManager>.Instance;
                if (instance47.Player.mRaidRankRewardResult != null && instance47.Player.mRaidRankRewardResult.ResqueRank > 0)
                {
                  this.SetTextValue(instance47.Player.mRaidRankRewardResult.ResqueRank);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.UNIT_MPMAX:
                GameManager instance48 = MonoSingleton<GameManager>.Instance;
                if (instance48.Player.mRaidRankRewardResult != null && instance48.Player.mRaidRankRewardResult.ResqueScore > 0)
                {
                  this.SetTextValue(instance48.Player.mRaidRankRewardResult.ResqueScore);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.TARGET_OUTPUTPOINT:
                GameManager instance49 = MonoSingleton<GameManager>.Instance;
                if (instance49.Player.mRaidRankRewardResult != null && instance49.Player.mRaidRankRewardResult.GuildRank > 0)
                {
                  this.SetTextValue(instance49.Player.mRaidRankRewardResult.GuildRank);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.TARGET_CRITICALRATE:
                GameManager instance50 = MonoSingleton<GameManager>.Instance;
                if (instance50.Player.mRaidRankRewardResult != null && instance50.Player.mRaidRankRewardResult.GuildScore > 0)
                {
                  this.SetTextValue(instance50.Player.mRaidRankRewardResult.GuildScore);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.TARGET_ACTIONTYPE:
                GameManager instance51 = MonoSingleton<GameManager>.Instance;
                if (instance51.Player.mRaidRankRewardResult != null && instance51.Player.mRaidRankRewardResult.Guild != null && !string.IsNullOrEmpty(instance51.Player.mRaidRankRewardResult.Guild.award_id))
                {
                  Image component19 = ((Component) this).GetComponent<Image>();
                  string awardId = instance51.Player.mRaidRankRewardResult.Guild.award_id;
                  if (UnityEngine.Object.op_Equality((UnityEngine.Object) component19, (UnityEngine.Object) null))
                  {
                    ((Behaviour) component19).enabled = false;
                    return;
                  }
                  SpriteSheet spriteSheet4 = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet4, (UnityEngine.Object) null))
                  {
                    component19.sprite = spriteSheet4.GetSprite(awardId);
                    ((Behaviour) component19).enabled = true;
                    goto label_3654;
                  }
                  else
                    goto label_3654;
                }
                else
                  goto label_3654;
              case GameParameter.ParameterTypes.ABILITY_ICON:
                GameManager instance52 = MonoSingleton<GameManager>.Instance;
                if (instance52.Player.mRaidRankRewardResult != null && instance52.Player.mRaidRankRewardResult.Guild != null && !string.IsNullOrEmpty(instance52.Player.mRaidRankRewardResult.Guild.name))
                {
                  this.SetTextValue(instance52.Player.mRaidRankRewardResult.Guild.name);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.ABILITY_NAME:
                GameManager instance53 = MonoSingleton<GameManager>.Instance;
                if (instance53.Player.mRaidRankRewardResult != null && instance53.Player.mRaidRankRewardResult.Guild != null)
                {
                  this.SetTextValue(instance53.Player.mRaidRankRewardResult.Guild.level);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.QUEST_KAKERA_ICON:
                GameManager instance54 = MonoSingleton<GameManager>.Instance;
                if (instance54.Player.mRaidRankRewardResult != null && instance54.Player.mRaidRankRewardResult.Guild != null)
                {
                  this.SetTextValue(instance54.Player.mRaidRankRewardResult.Guild.guild_master);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.UNIT_EXP:
                GameManager instance55 = MonoSingleton<GameManager>.Instance;
                if (instance55.Player.mRaidRankRewardResult != null && instance55.Player.mRaidRankRewardResult.Guild != null)
                {
                  this.SetTextValue(instance55.Player.mRaidRankRewardResult.Guild.count);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.UNIT_EXPMAX:
                GameManager instance56 = MonoSingleton<GameManager>.Instance;
                if (instance56.Player.mRaidRankRewardResult != null && instance56.Player.mRaidRankRewardResult.Guild != null)
                {
                  this.SetTextValue(instance56.Player.mRaidRankRewardResult.Guild.max_count);
                  goto label_3654;
                }
                else
                {
                  this.ResetToDefault();
                  goto label_3654;
                }
              case GameParameter.ParameterTypes.EQUIPMENT_RANGE:
                GenesisBossInfo.GenesisBossData dataOfClass103 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (dataOfClass103 != null && dataOfClass103.unit != null)
                {
                  this.SetTextValue(dataOfClass103.unit.name);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.EQUIPMENT_SCOPE:
                GenesisBossInfo.GenesisBossData dataOfClass104 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (dataOfClass104 != null && dataOfClass104.unit != null)
                {
                  GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(dataOfClass104.unit, (string) null);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.EQUIPMENT_EFFECTHEIGHT:
                Image component20 = ((Component) this).GetComponent<Image>();
                GenesisBossInfo.GenesisBossData dataOfClass105 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component20, (UnityEngine.Object) null) && dataOfClass105 != null && dataOfClass105.unit != null)
                {
                  GameSettings instance57 = GameSettings.Instance;
                  if (EElement.None <= dataOfClass105.unit.element && dataOfClass105.unit.element < (EElement) instance57.Elements_IconSmall.Length)
                  {
                    component20.sprite = instance57.Elements_IconSmall[(int) dataOfClass105.unit.element];
                    return;
                  }
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.EQUIPMENT_NAME:
                GenesisBossInfo.GenesisBossData dataOfClass106 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (dataOfClass106 != null)
                {
                  this.SetTextValue(dataOfClass106.currentHP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.EQUIPMENT_ICON:
                GenesisBossInfo.GenesisBossData dataOfClass107 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (dataOfClass107 != null)
                {
                  this.SetTextValue(dataOfClass107.maxHP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_NUM:
                GenesisBossInfo.GenesisBossData dataOfClass108 = DataSource.FindDataOfClass<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, (GenesisBossInfo.GenesisBossData) null);
                if (dataOfClass108 != null)
                {
                  this.SetSliderValue(dataOfClass108.currentHP, dataOfClass108.maxHP);
                  this.SetTextValue(dataOfClass108.currentHP);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNT:
                Button component21 = ((Component) this).GetComponent<Button>();
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) component21, (UnityEngine.Object) null))
                  return;
                GenesisChapterModeInfoParam dataOfClass109 = DataSource.FindDataOfClass<GenesisChapterModeInfoParam>(((Component) this).gameObject, (GenesisChapterModeInfoParam) null);
                if (dataOfClass109 == null)
                {
                  ((Selectable) component21).interactable = false;
                  return;
                }
                ItemData itemDataByItemParam1 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(dataOfClass109.BossChallengeItemParam);
                if (itemDataByItemParam1 == null || itemDataByItemParam1.Num < dataOfClass109.BossChallengeItemNum)
                {
                  ((Selectable) component21).interactable = false;
                  return;
                }
                ((Selectable) component21).interactable = true;
                return;
              case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNTMAX:
                bool flag17 = false;
                GenesisChapterParam genesisChapterParam1 = (GenesisChapterParam) null;
                GenesisChapterManager instance58 = GenesisChapterManager.Instance;
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance58))
                  genesisChapterParam1 = instance58.CurrentChapterParam;
                if (genesisChapterParam1 != null)
                  flag17 = genesisChapterParam1.GetBossQuest(QuestDifficulties.Extra) != null;
                if (this.Index != 0)
                  flag17 = !flag17;
                ((Component) this).gameObject.SetActive(flag17);
                return;
              case GameParameter.ParameterTypes.OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_COOLDOWNTIME:
                bool flag18 = false;
                GenesisChapterParam genesisChapterParam2 = (GenesisChapterParam) null;
                GenesisChapterManager instance59 = GenesisChapterManager.Instance;
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance59))
                  genesisChapterParam2 = instance59.CurrentChapterParam;
                if (genesisChapterParam2 != null)
                {
                  GenesisChapterModeInfoParam modeInfo = genesisChapterParam2.GetModeInfo(instance59.BossDifficulty);
                  if (modeInfo != null)
                    flag18 = modeInfo.IsLapBoss;
                }
                if (this.Index != 0)
                  flag18 = !flag18;
                ((Component) this).gameObject.SetActive(flag18);
                return;
              case GameParameter.ParameterTypes.EQUIPMENT_AMOUNT:
                GenesisBossInfo.LapBossBattleInfo lapBossBattleData1 = GenesisBossInfo.LapBossBattleData;
                if (lapBossBattleData1 != null)
                {
                  this.SetTextValue(lapBossBattleData1.Round);
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.UNIT_LEADERSKILLDESC:
                GenesisParam genesisParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetGenesisParam();
                if (genesisParam1 != null)
                {
                  this.SetTextValue(string.Format("{0}～{1}", (object) genesisParam1.BeginAt.ToString("MM/dd HH:mm"), (object) genesisParam1.EndAt.ToString("MM/dd HH:mm")));
                  return;
                }
                this.ResetToDefault();
                return;
              case GameParameter.ParameterTypes.QUESTRESULT_RATE:
                bool flag19 = false;
                GenesisParam genesisParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetGenesisParam();
                if (genesisParam2 != null)
                {
                  flag19 = genesisParam2.IsWithinPeriod();
                  if (this.Index != 0)
                    flag19 = !flag19;
                }
                ((Component) this).gameObject.SetActive(flag19);
                return;
              default:
                switch (parameterType - 3000)
                {
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                    GuildData dataOfClass110 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass110 == null)
                      return;
                    this.SetTextValue(dataOfClass110.Name);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                    Image component22 = ((Component) this).GetComponent<Image>();
                    string name1 = string.Empty;
                    GuildEmblemParam dataOfClass111 = DataSource.FindDataOfClass<GuildEmblemParam>(((Component) this).gameObject, (GuildEmblemParam) null);
                    if (dataOfClass111 != null)
                      name1 = dataOfClass111.Image;
                    if (string.IsNullOrEmpty(name1))
                    {
                      GuildData dataOfClass112 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                      if (dataOfClass112 != null)
                        name1 = dataOfClass112.Emblem;
                    }
                    if (string.IsNullOrEmpty(name1) || UnityEngine.Object.op_Equality((UnityEngine.Object) component22, (UnityEngine.Object) null))
                      return;
                    SpriteSheet spriteSheet5 = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet5, (UnityEngine.Object) null))
                      return;
                    component22.sprite = spriteSheet5.GetSprite(name1);
                    ((Behaviour) component22).enabled = true;
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                    GuildData dataOfClass113 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass113 == null)
                      return;
                    this.SetTextValue(dataOfClass113.Board);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                    GuildData dataOfClass114 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass114 != null)
                    {
                      this.SetTextValue(dataOfClass114.MemberCount);
                      return;
                    }
                    FriendData friendData6 = this.GetFriendData();
                    if (friendData6 == null || friendData6.ViewGuild == null)
                      return;
                    this.SetTextValue(friendData6.ViewGuild.count);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                    GuildData dataOfClass115 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass115 != null)
                      this.SetTextValue(dataOfClass115.MemberMax);
                    FriendData friendData7 = this.GetFriendData();
                    if (friendData7 == null || friendData7.ViewGuild == null)
                      return;
                    this.SetTextValue(friendData7.ViewGuild.max_count);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                    GuildMemberData dataOfClass116 = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
                    if (dataOfClass116 == null)
                      return;
                    this.SetTextValue(dataOfClass116.Name);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                    GuildMemberData dataOfClass117 = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
                    if (dataOfClass117 == null)
                      return;
                    this.SetTextValue(dataOfClass117.Level);
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                    GuildMemberData dataOfClass118 = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
                    if (dataOfClass118 == null)
                    {
                      this.ResetToDefault();
                      return;
                    }
                    TimeSpan timeSpan12 = DateTime.Now - GameUtility.UnixtimeToLocalTime(dataOfClass118.LastLogin);
                    int days4 = timeSpan12.Days;
                    if (days4 > 0)
                    {
                      this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_DAY", (object) days4.ToString()));
                      return;
                    }
                    int hours2 = timeSpan12.Hours;
                    if (hours2 > 0)
                    {
                      this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_HOUR", (object) hours2.ToString()));
                      return;
                    }
                    this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_MINUTE", (object) timeSpan12.Minutes.ToString()));
                    return;
                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                    GuildData dataOfClass119 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass119 == null || dataOfClass119.Facilities == null)
                      return;
                    GuildFacilityData guildFacilityData1 = Array.Find<GuildFacilityData>(dataOfClass119.Facilities, (Predicate<GuildFacilityData>) (facility => facility.Param.Type == GuildFacilityParam.eFacilityType.BASE_CAMP));
                    this.SetTextValue(guildFacilityData1 == null ? 0 : guildFacilityData1.Level);
                    return;
                  case GameParameter.ParameterTypes.QUEST_NAME:
                    GuildData dataOfClass120 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass120 == null || dataOfClass120.EntryConditions == null)
                      return;
                    if (dataOfClass120.EntryConditions.LowerLevel <= 0)
                    {
                      this.SetTextValue(LocalizedText.Get("sys.GUILD_ENTRY_CONDITIONS_LV0_LINE"));
                      return;
                    }
                    this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDENTRY_TITLE"), (object) dataOfClass120.EntryConditions.LowerLevel));
                    return;
                  case GameParameter.ParameterTypes.QUEST_STAMINA:
                    GuildData dataOfClass121 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass121 == null || dataOfClass121.EntryConditions == null)
                      return;
                    if (dataOfClass121.EntryConditions.LowerLevel <= 0)
                    {
                      this.SetTextValue(LocalizedText.Get("sys.GUILD_ENTRY_CONDITIONS_LV0"));
                      return;
                    }
                    this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDENTRY_TITLE"), (object) dataOfClass121.EntryConditions.LowerLevel));
                    return;
                  case GameParameter.ParameterTypes.QUEST_STATE:
                    PlayerGuildData playerGuild1 = MonoSingleton<GameManager>.Instance.Player.PlayerGuild;
                    if (playerGuild1 != null)
                    {
                      ((Component) this).gameObject.SetActive(playerGuild1.RoleId == GuildMemberData.eRole.MASTAER);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                    PlayerGuildData playerGuild2 = MonoSingleton<GameManager>.Instance.Player.PlayerGuild;
                    if (playerGuild2 != null)
                    {
                      ((Component) this).gameObject.SetActive(playerGuild2.RoleId == GuildMemberData.eRole.SUB_MASTAER);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                    PlayerGuildData playerGuild3 = MonoSingleton<GameManager>.Instance.Player.PlayerGuild;
                    if (playerGuild3 != null)
                    {
                      ((Component) this).gameObject.SetActive(playerGuild3.RoleId == GuildMemberData.eRole.MASTAER || playerGuild3.RoleId == GuildMemberData.eRole.SUB_MASTAER);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  case GameParameter.ParameterTypes.ITEM_ICON:
                    PlayerGuildData playerGuild4 = MonoSingleton<GameManager>.Instance.Player.PlayerGuild;
                    if (playerGuild4 != null)
                    {
                      ((Component) this).gameObject.SetActive(playerGuild4.RoleId != GuildMemberData.eRole.MASTAER && playerGuild4.RoleId != GuildMemberData.eRole.SUB_MASTAER);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(true);
                    return;
                  case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                    GuildData dataOfClass122 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass122 == null || dataOfClass122.GuildMaster == null)
                      return;
                    this.SetTextValue(dataOfClass122.GuildMaster.Name);
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_NAME:
                    GuildData dataOfClass123 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass123 == null)
                      return;
                    this.SetTextValue(dataOfClass123.EntryConditions.Comment);
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                    GuildMemberData dataOfClass124 = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) || dataOfClass124 == null)
                      return;
                    this.mImageArray.ImageIndex = Mathf.Max(0, (int) (dataOfClass124.RoleId - 1));
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                    GuildData dataOfClass125 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass125 == null)
                      return;
                    this.SetTextValue(LocalizedText.Get(!dataOfClass125.EntryConditions.IsAutoApproval ? "sys.GUILD_AUTO_APPROVAL_OFF" : "sys.GUILD_AUTO_APPROVAL_ON"));
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
                    GuildData dataOfClass126 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass126 != null && dataOfClass126.GuildMaster != null)
                    {
                      UnitData unit40 = dataOfClass126.GuildMaster.Unit;
                      string str25 = AssetPath.UnitSkinIconSmall(unit40.UnitParam, unit40.GetSelectedSkin(), unit40.CurrentJob.JobID);
                      if (!string.IsNullOrEmpty(str25))
                      {
                        GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str25;
                        return;
                      }
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_ATK:
                    GuildData dataOfClass127 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass127 == null)
                      return;
                    this.SetTextValue(LocalizedText.Get(dataOfClass127.UniqueID.ToString()));
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_HP:
                    GuildData dataOfClass128 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass128 == null)
                      return;
                    DateTime localTime2 = GameUtility.UnixtimeToLocalTime(dataOfClass128.CreatedAt);
                    this.SetTextValue(string.Format("{0}/{1}/{2}", (object) localTime2.Year, (object) localTime2.Month, (object) localTime2.Day));
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_MAGIC:
                    long num35 = 0;
                    GuildFacilityParam guildFacilityParam1 = (GuildFacilityParam) null;
                    GuildFacilityData dataOfClass129 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    if (dataOfClass129 != null)
                      guildFacilityParam1 = dataOfClass129.Param;
                    if (guildFacilityParam1 == null)
                      guildFacilityParam1 = DataSource.FindDataOfClass<GuildFacilityParam>(((Component) this).gameObject, (GuildFacilityParam) null);
                    if (guildFacilityParam1 != null)
                      num35 = guildFacilityParam1.DayLimitInvest;
                    this.SetTextValue(num35.ToString());
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_RARITY:
                    GuildFacilityData dataOfClass130 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    if (dataOfClass130 == null)
                      return;
                    this.SetTextValue(dataOfClass130.InvestPoint);
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_ELEMENT:
                    Image component23 = ((Component) this).GetComponent<Image>();
                    string name2 = string.Empty;
                    GuildFacilityParam dataOfClass131 = DataSource.FindDataOfClass<GuildFacilityParam>(((Component) this).gameObject, (GuildFacilityParam) null);
                    if (dataOfClass131 != null)
                    {
                      name2 = dataOfClass131.Image;
                    }
                    else
                    {
                      GuildFacilityData dataOfClass132 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                      if (dataOfClass132 != null)
                        name2 = dataOfClass132.Param.Image;
                    }
                    if (string.IsNullOrEmpty(name2) || UnityEngine.Object.op_Equality((UnityEngine.Object) component23, (UnityEngine.Object) null))
                    {
                      ((Component) this).gameObject.SetActive(false);
                      return;
                    }
                    SpriteSheet spriteSheet6 = AssetManager.Load<SpriteSheet>("GuildFacilityImage/GuildFacilities");
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet6, (UnityEngine.Object) null))
                      return;
                    component23.sprite = spriteSheet6.GetSprite(name2);
                    ((Behaviour) component23).enabled = true;
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_ICON:
                    ViewGuildData dataOfClass133 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass133 != null)
                    {
                      this.SetTextValue(dataOfClass133.name);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLDESC:
                    Image component24 = ((Component) this).GetComponent<Image>();
                    string name3 = string.Empty;
                    ViewGuildData dataOfClass134 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass134 != null)
                      name3 = dataOfClass134.award_id;
                    if (string.IsNullOrEmpty(name3) || UnityEngine.Object.op_Equality((UnityEngine.Object) component24, (UnityEngine.Object) null))
                    {
                      ((Behaviour) component24).enabled = false;
                      return;
                    }
                    SpriteSheet spriteSheet7 = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet7, (UnityEngine.Object) null))
                      return;
                    component24.sprite = spriteSheet7.GetSprite(name3);
                    ((Behaviour) component24).enabled = true;
                    return;
                  case GameParameter.ParameterTypes.QUEST_SUBTITLE:
                    ViewGuildData dataOfClass135 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass135 != null)
                    {
                      this.SetTextValue(dataOfClass135.level);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_LEVEL:
                    ViewGuildData dataOfClass136 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass136 != null)
                    {
                      this.SetTextValue(dataOfClass136.count);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_HP:
                    ViewGuildData dataOfClass137 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass137 != null)
                    {
                      this.SetTextValue(dataOfClass137.max_count);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_HPMAX:
                    ViewGuildData dataOfClass138 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    ((Component) this).gameObject.SetActive(dataOfClass138 != null && dataOfClass138.id > 0);
                    return;
                  case GameParameter.ParameterTypes.UNIT_ATK:
                    ViewGuildData dataOfClass139 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    ((Component) this).gameObject.SetActive(dataOfClass139 == null || dataOfClass139.id <= 0);
                    return;
                  case GameParameter.ParameterTypes.UNIT_MAG:
                    GuildMemberData dataOfClass140 = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) || dataOfClass140 == null)
                      return;
                    this.mImageArray.ImageIndex = Mathf.Max(0, (int) (dataOfClass140.RoleId - 1));
                    return;
                  case GameParameter.ParameterTypes.UNIT_ICON:
                    ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined);
                    return;
                  case GameParameter.ParameterTypes.UNIT_NAME:
                    ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined);
                    return;
                  case GameParameter.ParameterTypes.UNIT_RARITY:
                    if (MonoSingleton<GameManager>.Instance.Player.Guild != null)
                    {
                      this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.Guild.Name);
                      return;
                    }
                    if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
                      return;
                    this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Name);
                    return;
                  case GameParameter.ParameterTypes.PARTY_LEADERSKILLNAME:
                    GuildFacilityData dataOfClass141 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    if (dataOfClass141 == null)
                      return;
                    this.SetTextValue(dataOfClass141.Level);
                    return;
                  case GameParameter.ParameterTypes.PARTY_LEADERSKILLDESC:
                    GuildFacilityData dataOfClass142 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    GuildFacilityParam guildFacilityParam2 = dataOfClass142 == null ? DataSource.FindDataOfClass<GuildFacilityParam>(((Component) this).gameObject, (GuildFacilityParam) null) : dataOfClass142.Param;
                    GuildFacilityParam.eFacilityType type1 = GuildFacilityParam.eFacilityType.NONE;
                    if (guildFacilityParam2 != null)
                      type1 = guildFacilityParam2.Type;
                    ItemParam itemParam23 = this.GetItemParam();
                    if (itemParam23 != null)
                    {
                      this.SetTextValue(itemParam23.GetFacilityPoint(type1));
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_DEF:
                    ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined);
                    return;
                  case GameParameter.ParameterTypes.UNIT_MND:
                    ViewGuildData dataOfClass143 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass143 != null)
                    {
                      this.SetTextValue(dataOfClass143.guild_master);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_SPEED:
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildManager.Instance.AppearsGuildRaidPeriodId) != null)
                    {
                      ((Component) this).gameObject.SetActive(true);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(false);
                    return;
                  case GameParameter.ParameterTypes.UNIT_LUCK:
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildManager.Instance.AppearsGuildRaidPeriodId) != null)
                    {
                      ((Component) this).gameObject.SetActive(false);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(true);
                    return;
                  case GameParameter.ParameterTypes.UNIT_JOBNAME:
                    ViewGuildData dataOfClass144 = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    if (dataOfClass144 != null)
                    {
                      this.SetTextValue(dataOfClass144.create_at.ToString("yyyy/M/d"));
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.UNIT_JOBRANK:
                    GameUtility.SetGameObjectActive(((Component) this).gameObject, UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null) && GuildManager.Instance.AttendStatus == GuildManager.GuildAttendStatus.UNATTENDED);
                    return;
                  case GameParameter.ParameterTypes.UNIT_ELEMENT:
                    GameUtility.SetGameObjectActive(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined && MonoSingleton<GameManager>.Instance.Player.HasGuildReward);
                    return;
                  case GameParameter.ParameterTypes.PARTY_TOTALATK:
                    BindViewGuildMemberData dataOfClass145 = DataSource.FindDataOfClass<BindViewGuildMemberData>(((Component) this).gameObject, (BindViewGuildMemberData) null);
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) || this.mImageArray.Images == null || dataOfClass145 == null)
                      return;
                    this.mImageArray.ImageIndex = Mathf.Max(0, Mathf.Min(this.mImageArray.Images.Length, (int) dataOfClass145.AttendStatus));
                    return;
                  case GameParameter.ParameterTypes.INVENTORY_ITEMICON:
                    GuildData dataOfClass146 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                    if (dataOfClass146 != null && dataOfClass146.EntryConditions != null)
                    {
                      GuildSearchFilterParam guildSearchFilter = MonoSingleton<GameManager>.Instance.MasterParam.FindGuildSearchFilter(eGuildSearchFilterTypes.Policy);
                      if (guildSearchFilter != null)
                      {
                        GuildSearchFilterConditionParam conditionsParam = guildSearchFilter.GetConditionsParam(dataOfClass146.EntryConditions.Policy);
                        if (conditionsParam != null && conditionsParam.sval > 0)
                        {
                          this.SetTextValue(conditionsParam.name);
                          return;
                        }
                      }
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.INVENTORY_ITEMNAME:
                    GuildFacilityData guildFacilityData2 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    if (guildFacilityData2 == null)
                    {
                      GuildData dataOfClass147 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                      if (dataOfClass147 != null)
                        guildFacilityData2 = dataOfClass147.GetFacilityData(GuildFacilityParam.eFacilityType.GUILD_SHOP);
                    }
                    if (guildFacilityData2 != null && guildFacilityData2.Param.Type == GuildFacilityParam.eFacilityType.GUILD_SHOP)
                    {
                      this.SetTextValue(guildFacilityData2.Level);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.ITEM_NAME:
                    GuildFacilityData guildFacilityData3 = DataSource.FindDataOfClass<GuildFacilityData>(((Component) this).gameObject, (GuildFacilityData) null);
                    if (guildFacilityData3 == null)
                    {
                      GuildData dataOfClass148 = DataSource.FindDataOfClass<GuildData>(((Component) this).gameObject, (GuildData) null);
                      if (dataOfClass148 != null)
                        guildFacilityData3 = dataOfClass148.GetFacilityData(GuildFacilityParam.eFacilityType.BASE_CAMP);
                    }
                    if (guildFacilityData3 != null && guildFacilityData3.Param.Type == GuildFacilityParam.eFacilityType.BASE_CAMP)
                    {
                      this.SetTextValue(guildFacilityData3.Level);
                      return;
                    }
                    this.ResetToDefault();
                    return;
                  case GameParameter.ParameterTypes.ITEM_DESC:
                    ViewGuildData dataOfClassAs1 = DataSource.FindDataOfClassAs<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    GuildData guild1 = MonoSingleton<GameManager>.Instance.Player.Guild;
                    if (dataOfClassAs1 == null || guild1 == null)
                    {
                      ((Component) this).gameObject.SetActive(false);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(guild1.UniqueID == (long) dataOfClassAs1.id);
                    return;
                  case GameParameter.ParameterTypes.ITEM_SELLPRICE:
                    ViewGuildData dataOfClassAs2 = DataSource.FindDataOfClassAs<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
                    GuildData guild2 = MonoSingleton<GameManager>.Instance.Player.Guild;
                    if (dataOfClassAs2 == null || guild2 == null)
                    {
                      ((Component) this).gameObject.SetActive(true);
                      return;
                    }
                    ((Component) this).gameObject.SetActive(guild2.UniqueID != (long) dataOfClassAs2.id);
                    return;
                  case GameParameter.ParameterTypes.ITEM_BUYPRICE:
                    GameUtility.SetGameObjectActive(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.HasRewards);
                    return;
                  case GameParameter.ParameterTypes.EQUIPMENT_RANGE:
                    Button component25 = ((Component) this).GetComponent<Button>();
                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component25, (UnityEngine.Object) null))
                      return;
                    bool dataOfClass149 = DataSource.FindDataOfClass<bool>(((Component) this).gameObject, true);
                    ((Selectable) component25).interactable = dataOfClass149;
                    return;
                  default:
                    switch (parameterType - 2400)
                    {
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                        GameManager instance60 = MonoSingleton<GameManager>.Instance;
                        if (instance60.Player.RankMatchRank > 0)
                        {
                          this.SetTextValue(instance60.Player.RankMatchRank);
                          return;
                        }
                        this.ResetToDefault();
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                        this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.RankMatchScore);
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                        GameManager instance61 = MonoSingleton<GameManager>.Instance;
                        int nextVersusRankClass = instance61.GetNextVersusRankClass(instance61.RankMatchScheduleId, instance61.Player.RankMatchClass, instance61.Player.RankMatchScore);
                        if (nextVersusRankClass > 0)
                        {
                          this.SetTextValue(nextVersusRankClass);
                          return;
                        }
                        this.ResetToDefault();
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                        GameManager instance62 = MonoSingleton<GameManager>.Instance;
                        int maxBattlePoint = instance62.GetMaxBattlePoint(instance62.RankMatchScheduleId);
                        this.SetTextValue(instance62.Player.RankMatchBattlePoint.ToString() + "/" + (object) maxBattlePoint);
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                        instance1 = MonoSingleton<GameManager>.Instance;
                        Image component26 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component26, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        VersusRankClassParam dataOfClass150 = DataSource.FindDataOfClass<VersusRankClassParam>(((Component) this).gameObject, (VersusRankClassParam) null);
                        if (dataOfClass150 == null)
                        {
                          component26.sprite = (Sprite) null;
                          ((Behaviour) component26).enabled = false;
                          return;
                        }
                        SpriteSheet spriteSheet8 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet8, (UnityEngine.Object) null))
                          return;
                        component26.sprite = spriteSheet8.GetSprite("class_" + (object) (int) dataOfClass150.Class);
                        ((Behaviour) component26).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                        Image component27 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component27, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        VersusRankClassParam dataOfClass151 = DataSource.FindDataOfClass<VersusRankClassParam>(((Component) this).gameObject, (VersusRankClassParam) null);
                        if (dataOfClass151 == null)
                        {
                          component27.sprite = (Sprite) null;
                          ((Behaviour) component27).enabled = false;
                          return;
                        }
                        SpriteSheet spriteSheet9 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet9, (UnityEngine.Object) null))
                          return;
                        component27.sprite = spriteSheet9.GetSprite("classname_" + (object) (int) dataOfClass151.Class);
                        ((Behaviour) component27).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                        Image component28 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component28, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        VersusRankClassParam dataOfClass152 = DataSource.FindDataOfClass<VersusRankClassParam>(((Component) this).gameObject, (VersusRankClassParam) null);
                        if (dataOfClass152 == null)
                        {
                          component28.sprite = (Sprite) null;
                          ((Behaviour) component28).enabled = false;
                          return;
                        }
                        SpriteSheet spriteSheet10 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet10, (UnityEngine.Object) null))
                          return;
                        component28.sprite = spriteSheet10.GetSprite("classframe_" + (object) (int) dataOfClass152.Class);
                        ((Behaviour) component28).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                        GameManager instance63 = MonoSingleton<GameManager>.Instance;
                        VersusRankClassParam dataOfClass153 = DataSource.FindDataOfClass<VersusRankClassParam>(((Component) this).gameObject, (VersusRankClassParam) null);
                        if (dataOfClass153 == null)
                          return;
                        ((Component) this).gameObject.SetActive(dataOfClass153.Class == instance63.Player.RankMatchClass);
                        return;
                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                        GameManager instance64 = MonoSingleton<GameManager>.Instance;
                        if (instance64.Player.RankMatchRank > 3)
                        {
                          this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_RANKING_RANK"), (object) instance64.Player.RankMatchRank));
                          return;
                        }
                        ((Component) this).gameObject.SetActive(false);
                        return;
                      case GameParameter.ParameterTypes.QUEST_NAME:
                        GameManager instance65 = MonoSingleton<GameManager>.Instance;
                        if (instance65.Player.RankMatchRank > 3)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        Image component29 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component29, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet11 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet11, (UnityEngine.Object) null))
                          return;
                        component29.sprite = spriteSheet11.GetSprite("ranking_" + (object) instance65.Player.RankMatchRank);
                        ((Behaviour) component29).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.QUEST_STAMINA:
                        PlayerData player13 = MonoSingleton<GameManager>.Instance.Player;
                        PartyData partyOfType = player13.FindPartyOfType(PlayerPartyTypes.RankMatch);
                        int num36 = 0;
                        for (int index17 = 0; index17 < partyOfType.MAX_UNIT; ++index17)
                        {
                          long unitUniqueId = partyOfType.GetUnitUniqueID(index17);
                          UnitData unitDataByUniqueId = player13.FindUnitDataByUniqueID(unitUniqueId);
                          if (unitDataByUniqueId != null)
                            num36 = num36 + (int) unitDataByUniqueId.Status.param.atk + (int) unitDataByUniqueId.Status.param.mag;
                        }
                        this.SetTextValue(num36);
                        return;
                      case GameParameter.ParameterTypes.QUEST_STATE:
                        ReqRankMatchRanking.ResponceRanking dataOfClass154 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        if (dataOfClass154 == null)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        if (dataOfClass154.rank > 3)
                        {
                          this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_RANKING_RANK"), (object) dataOfClass154.rank));
                          return;
                        }
                        ((Component) this).gameObject.SetActive(false);
                        return;
                      case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                        ReqRankMatchRanking.ResponceRanking dataOfClass155 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        if (dataOfClass155 == null)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        if (dataOfClass155.rank > 3)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        Image component30 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component30, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet12 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet12, (UnityEngine.Object) null))
                          return;
                        component30.sprite = spriteSheet12.GetSprite("ranking_" + (object) dataOfClass155.rank);
                        ((Behaviour) component30).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                        ReqRankMatchRanking.ResponceRanking dataOfClass156 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        Image component31 = ((Component) this).GetComponent<Image>();
                        if (dataOfClass156 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component31, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet13 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet13, (UnityEngine.Object) null))
                          return;
                        component31.sprite = spriteSheet13.GetSprite("class_" + (object) dataOfClass156.type);
                        ((Behaviour) component31).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.ITEM_ICON:
                        ReqRankMatchRanking.ResponceRanking dataOfClass157 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        if (dataOfClass157 == null)
                          return;
                        this.SetTextValue(dataOfClass157.enemy.name);
                        return;
                      case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                        ReqRankMatchRanking.ResponceRanking dataOfClass158 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        if (dataOfClass158 == null)
                          return;
                        this.SetTextValue(dataOfClass158.enemy.lv);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_NAME:
                        ReqRankMatchRanking.ResponceRanking dataOfClass159 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(((Component) this).gameObject, (ReqRankMatchRanking.ResponceRanking) null);
                        if (dataOfClass159 == null)
                          return;
                        this.SetTextValue(dataOfClass159.score);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                        GameManager instance66 = MonoSingleton<GameManager>.Instance;
                        Image component32 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component32, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet14 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet14, (UnityEngine.Object) null))
                          return;
                        int num37 = (int) instance66.Player.RankMatchClass;
                        if (this.InstanceType == 1)
                          num37 = (int) instance66.Player.RankMatchOldClass;
                        component32.sprite = spriteSheet14.GetSprite("class_" + (object) num37);
                        ((Behaviour) component32).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                      case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
                        VersusRankRankingRewardParam dataOfClass160 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(((Component) this).gameObject, (VersusRankRankingRewardParam) null);
                        if (dataOfClass160 == null)
                        {
                          ((Component) ((Component) this).transform.parent).gameObject.SetActive(false);
                          return;
                        }
                        int num38;
                        if (this.ParameterType == GameParameter.ParameterTypes.RANKMATCH_RANKING_RANKREWARD_IMAGESET_BEGIN)
                        {
                          num38 = dataOfClass160.RankBegin;
                          if (num38 == dataOfClass160.RankEnd)
                            ((Component) ((Component) this).transform.parent).gameObject.SetActive(false);
                        }
                        else
                          num38 = dataOfClass160.RankEnd;
                        if (dataOfClass160.RankBegin > 3)
                        {
                          this.SetTextValue(num38);
                          return;
                        }
                        ((Component) ((Component) this).transform.parent).gameObject.SetActive(false);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_ATK:
                        VersusRankRankingRewardParam dataOfClass161 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(((Component) this).gameObject, (VersusRankRankingRewardParam) null);
                        if (dataOfClass161 == null)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        if (dataOfClass161.RankBegin > 3)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        Image component33 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component33, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet15 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet15, (UnityEngine.Object) null))
                          return;
                        component33.sprite = spriteSheet15.GetSprite("ranking_" + (object) dataOfClass161.RankBegin);
                        ((Behaviour) component33).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_HP:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass162 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        if (dataOfClass162 == null)
                          return;
                        Transform transform1 = !(dataOfClass162.result.result == "win") ? (dataOfClass162.result.result == "lose" || dataOfClass162.result.result == "retire" || dataOfClass162.result.result == "cancel" || dataOfClass162.result.result == "crashed" ? ((Component) this).transform.Find("Lose") : ((Component) this).transform.Find("Draw")) : ((Component) this).transform.Find("Win");
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
                          return;
                        ((Component) transform1).gameObject.SetActive(true);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_MAGIC:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass163 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        string str26 = "0";
                        Color color1 = Color.white;
                        if (dataOfClass163 != null)
                        {
                          str26 = dataOfClass163.value.ToString();
                          if (dataOfClass163.result.result == "win")
                          {
                            str26 = "+" + str26;
                            color1 = Color.cyan;
                          }
                          else if (dataOfClass163.result.result == "lose")
                            color1 = Color.red;
                        }
                        this.mText.text = str26;
                        ((Graphic) this.mText).color = color1;
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_RARITY:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass164 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        Image component34 = ((Component) this).GetComponent<Image>();
                        if (dataOfClass164 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component34, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet16 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet16, (UnityEngine.Object) null))
                          return;
                        component34.sprite = spriteSheet16.GetSprite("class_" + (object) dataOfClass164.type);
                        ((Behaviour) component34).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_ELEMENT:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass165 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        if (dataOfClass165 == null)
                          return;
                        this.SetTextValue(dataOfClass165.enemy.name);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_ICON:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass166 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        if (dataOfClass166 == null)
                          return;
                        this.SetTextValue(dataOfClass166.enemy.lv);
                        return;
                      case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLDESC:
                        ReqRankMatchHistory.ResponceHistoryList dataOfClass167 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(((Component) this).gameObject, (ReqRankMatchHistory.ResponceHistoryList) null);
                        if (dataOfClass167 == null)
                          return;
                        this.SetTextValue(dataOfClass167.enemyscore);
                        return;
                      case GameParameter.ParameterTypes.QUEST_SUBTITLE:
                        VersusRankMissionParam dataOfClass168 = DataSource.FindDataOfClass<VersusRankMissionParam>(((Component) this).gameObject, (VersusRankMissionParam) null);
                        if (dataOfClass168 == null)
                          return;
                        this.SetTextValue(dataOfClass168.Name);
                        return;
                      case GameParameter.ParameterTypes.UNIT_LEVEL:
                        VersusRankMissionParam dataOfClass169 = DataSource.FindDataOfClass<VersusRankMissionParam>(((Component) this).gameObject, (VersusRankMissionParam) null);
                        if (dataOfClass169 == null)
                          return;
                        this.SetTextValue(dataOfClass169.IVal);
                        return;
                      case GameParameter.ParameterTypes.UNIT_HP:
                        ReqRankMatchMission.MissionProgress dataOfClass170 = DataSource.FindDataOfClass<ReqRankMatchMission.MissionProgress>(((Component) this).gameObject, (ReqRankMatchMission.MissionProgress) null);
                        if (dataOfClass170 == null)
                        {
                          this.SetTextValue("0");
                          return;
                        }
                        this.SetTextValue(dataOfClass170.prog);
                        return;
                      case GameParameter.ParameterTypes.UNIT_HPMAX:
                        GameManager instance67 = MonoSingleton<GameManager>.Instance;
                        Image component35 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component35, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet17 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet17, (UnityEngine.Object) null))
                          return;
                        int num39 = (int) instance67.Player.RankMatchClass;
                        if (this.InstanceType == 1)
                          num39 = (int) instance67.Player.RankMatchOldClass;
                        component35.sprite = spriteSheet17.GetSprite("classname_result_" + (object) num39);
                        ((Behaviour) component35).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.UNIT_ATK:
                        this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.RankMatchTotalCount);
                        return;
                      case GameParameter.ParameterTypes.UNIT_MAG:
                        this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.RankMatchWinCount);
                        return;
                      case GameParameter.ParameterTypes.UNIT_ICON:
                        this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.RankMatchLoseCount);
                        return;
                      case GameParameter.ParameterTypes.UNIT_NAME:
                        GameManager instance68 = MonoSingleton<GameManager>.Instance;
                        int num40 = 0;
                        int num41 = 1;
                        VersusRankClassParam versusRankClass = instance68.GetVersusRankClass(instance68.RankMatchScheduleId, instance68.Player.RankMatchClass);
                        if (versusRankClass != null)
                        {
                          num40 = versusRankClass.UpPoint - instance68.Player.RankMatchScore;
                          num41 = versusRankClass.UpPoint - versusRankClass.DownPoint;
                        }
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
                          return;
                        this.mSlider.value = (float) (1.0 - (double) num40 / (double) num41);
                        return;
                      case GameParameter.ParameterTypes.UNIT_RARITY:
                        VersusRankRankingRewardParam dataOfClass171 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(((Component) this).gameObject, (VersusRankRankingRewardParam) null);
                        if (dataOfClass171 == null)
                        {
                          ((Component) ((Component) this).transform).gameObject.SetActive(false);
                          return;
                        }
                        if (dataOfClass171.RankBegin != dataOfClass171.RankEnd)
                          return;
                        ((Component) ((Component) this).transform).gameObject.SetActive(false);
                        return;
                      case GameParameter.ParameterTypes.PARTY_LEADERSKILLNAME:
                        GameManager instance69 = MonoSingleton<GameManager>.Instance;
                        List<VersusRankMissionParam> versusRankMissionList = instance69.GetVersusRankMissionList(instance69.RankMatchScheduleId);
                        bool flag20 = false;
                        foreach (RankMatchMissionState matchMissionState in instance69.Player.RankMatchMissionState)
                        {
                          RankMatchMissionState rmms = matchMissionState;
                          VersusRankMissionParam rankMissionParam = versusRankMissionList.Find((Predicate<VersusRankMissionParam>) (mission => mission.IName == rmms.IName));
                          if (!rmms.IsRewarded && rankMissionParam != null && rankMissionParam.IVal <= rmms.Progress)
                          {
                            flag20 = true;
                            break;
                          }
                        }
                        ((Component) this).gameObject.SetActive(flag20);
                        return;
                      case GameParameter.ParameterTypes.PARTY_LEADERSKILLDESC:
                        GameManager instance70 = MonoSingleton<GameManager>.Instance;
                        if (instance70.Player.mRankMatchSeasonResult == null)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        this.SetTextValue(instance70.Player.mRankMatchSeasonResult.Rank);
                        return;
                      case GameParameter.ParameterTypes.UNIT_DEF:
                        GameManager instance71 = MonoSingleton<GameManager>.Instance;
                        if (instance71.Player.mRankMatchSeasonResult == null)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        if (instance71.Player.mRankMatchSeasonResult.Rank > 3)
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        Image component36 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component36, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet18 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet18, (UnityEngine.Object) null))
                          return;
                        component36.sprite = spriteSheet18.GetSprite("ranking_" + (object) instance71.Player.mRankMatchSeasonResult.Rank);
                        ((Behaviour) component36).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.UNIT_MND:
                        GameManager instance72 = MonoSingleton<GameManager>.Instance;
                        Image component37 = ((Component) this).GetComponent<Image>();
                        if (instance72.Player.mRankMatchSeasonResult == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component37, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet19 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet19, (UnityEngine.Object) null))
                          return;
                        component37.sprite = spriteSheet19.GetSprite("class_" + (object) (int) instance72.Player.mRankMatchSeasonResult.Class);
                        ((Behaviour) component37).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.UNIT_SPEED:
                        GameManager instance73 = MonoSingleton<GameManager>.Instance;
                        if (instance73.Player.mRankMatchSeasonResult == null)
                          return;
                        this.SetTextValue(instance73.Player.mRankMatchSeasonResult.Score);
                        return;
                      case GameParameter.ParameterTypes.UNIT_LUCK:
                        this.InstanceType = 1;
                        JSON_MyPhotonPlayerParam versusPlayerParam1 = this.GetVersusPlayerParam();
                        if (versusPlayerParam1 != null)
                        {
                          this.SetTextValue(versusPlayerParam1.rankmatch_score.ToString());
                          return;
                        }
                        this.ResetToDefault();
                        return;
                      case GameParameter.ParameterTypes.UNIT_JOBNAME:
                        GameManager instance74 = MonoSingleton<GameManager>.Instance;
                        if (instance74.Player.mRankMatchSeasonResult == null)
                          return;
                        Image component38 = ((Component) this).GetComponent<Image>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component38, (UnityEngine.Object) null))
                        {
                          ((Component) this).gameObject.SetActive(false);
                          return;
                        }
                        SpriteSheet spriteSheet20 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet20, (UnityEngine.Object) null))
                          return;
                        int num42 = (int) instance74.Player.mRankMatchSeasonResult.Class;
                        component38.sprite = spriteSheet20.GetSprite("classname_result_" + (object) num42);
                        ((Behaviour) component38).enabled = true;
                        return;
                      case GameParameter.ParameterTypes.UNIT_JOBRANK:
                        GameManager instance75 = MonoSingleton<GameManager>.Instance;
                        if (instance75.Player.mRankMatchSeasonResult == null)
                          return;
                        VersusRankParam versusRankParam1 = instance75.GetVersusRankParam(instance75.Player.mRankMatchSeasonResult.ScheduleId);
                        if (versusRankParam1 == null)
                          return;
                        LocalizedText.Get("sys.RANK_MATCH_MATCHING_SEASONTIME");
                        this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_MATCHING_SEASONTIME"), (object) versusRankParam1.BeginAt.Month, (object) versusRankParam1.BeginAt.Day, (object) versusRankParam1.BeginAt.Hour, (object) versusRankParam1.BeginAt.Minute, (object) versusRankParam1.EndAt.Month, (object) versusRankParam1.EndAt.Day, (object) versusRankParam1.EndAt.Hour, (object) versusRankParam1.EndAt.Minute));
                        return;
                      case GameParameter.ParameterTypes.UNIT_ELEMENT:
                        GameManager instance76 = MonoSingleton<GameManager>.Instance;
                        VersusRankParam versusRankParam2 = instance76.GetVersusRankParam(instance76.RankMatchScheduleId);
                        if (versusRankParam2 == null)
                          return;
                        this.SetTextValue(versusRankParam2.Name);
                        return;
                      case GameParameter.ParameterTypes.PARTY_TOTALATK:
                        ArenaRewardParam dataOfClass172 = DataSource.FindDataOfClass<ArenaRewardParam>(((Component) this).gameObject, (ArenaRewardParam) null);
                        if (dataOfClass172 == null)
                          return;
                        ImageArray component39 = ((Component) this).gameObject.GetComponent<ImageArray>();
                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component39, (UnityEngine.Object) null))
                          return;
                        int num43 = dataOfClass172.Rank - 1;
                        if (num43 < 0)
                          return;
                        if (num43 >= component39.Images.Length)
                          num43 = component39.Images.Length - 1;
                        component39.ImageIndex = num43;
                        return;
                      case GameParameter.ParameterTypes.INVENTORY_ITEMICON:
                        PartyData partyData4;
                        if ((partyData4 = this.GetPartyData()) != null)
                        {
                          UnitData unitData65 = UnitOverWriteUtility.Apply(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(partyData4.GetUnitUniqueID(partyData4.LeaderIndex)), (eOverWritePartyType) GlobalVars.OverWritePartyType);
                          this.SetImageIndex(unitData65 == null || !unitData65.IsEquipConceptLeaderSkill() ? 0 : 1);
                          return;
                        }
                        this.ResetToDefault();
                        return;
                      case GameParameter.ParameterTypes.INVENTORY_ITEMNAME:
                        ArenaPlayer arenaPlayer7 = this.GetArenaPlayer();
                        if (arenaPlayer7 != null && arenaPlayer7.Unit[0] != null)
                        {
                          this.SetImageIndex(arenaPlayer7.Unit[0] == null || !arenaPlayer7.Unit[0].IsEquipConceptLeaderSkill() ? 0 : 1);
                          return;
                        }
                        this.ResetToDefault();
                        return;
                      default:
                        switch (parameterType - 4100)
                        {
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                            GvGPeriodParam gvGperiod1 = GvGPeriodParam.GetGvGPeriod();
                            if (this.Index == 0)
                            {
                              ((Component) this).gameObject.SetActive(gvGperiod1 != null);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(gvGperiod1 == null);
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                            if (this.Index == 0)
                            {
                              ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.HasGvGReward);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(!MonoSingleton<GameManager>.Instance.Player.HasGvGReward);
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null && GvGManager.Instance.CurrentRule.IsExistConditions())
                            {
                              ((Component) this).gameObject.SetActive(true);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(false);
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null)
                            {
                              this.SetTextValue(GvGManager.Instance.CurrentRule.Name);
                              return;
                            }
                            this.ResetToDefault();
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                            GvGPeriodParam gvGperiod2 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod2 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(string.Format(LocalizedText.Get("sys.GVG_TEXT_LOBBY_PERIOD_TIME"), (object) gvGperiod2.EndAt.Month, (object) gvGperiod2.EndAt.Day, (object) gvGperiod2.EndAt.Hour, (object) gvGperiod2.EndAt.Minute));
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                            GvGPeriodParam gvGperiod3 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod3 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(string.Format(LocalizedText.Get("sys.GVG_TEXT_LOBBY_PERIOD_TIME"), (object) gvGperiod3.BeginAt.Month, (object) gvGperiod3.BeginAt.Day, (object) gvGperiod3.BeginAt.Hour, (object) gvGperiod3.BeginAt.Minute));
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null && GvGManager.Instance.CurrentRuleUnitCount > 0)
                            {
                              ((Component) this).gameObject.SetActive(true);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(false);
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null)
                            {
                              this.SetTextValue(Mathf.Max(0, GvGManager.Instance.CurrentRuleUnitCount - (GvGManager.Instance.UsedUnitList != null ? GvGManager.Instance.UsedUnitTodayCount : 0)));
                              return;
                            }
                            this.ResetToDefault();
                            return;
                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                            GvGPeriodParam gvGperiod4 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod4 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(string.Format(LocalizedText.Get("sys.GVG_PERIOD_DATETIME"), (object) gvGperiod4.BeginAt.Month, (object) gvGperiod4.BeginAt.Day, (object) gvGperiod4.BeginAt.Hour, (object) gvGperiod4.BeginAt.Minute, (object) gvGperiod4.EndAt.Month, (object) gvGperiod4.EndAt.Day, (object) gvGperiod4.EndAt.Hour, (object) gvGperiod4.EndAt.Minute));
                            return;
                          case GameParameter.ParameterTypes.QUEST_NAME:
                            GvGPeriodParam gvGperiod5 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod5 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            if (this.Index == 0)
                            {
                              this.SetTextValue(gvGperiod5.DeclarationStartTime.ToString());
                              return;
                            }
                            if (this.Index == 1)
                            {
                              this.SetTextValue(gvGperiod5.DeclarationEndTime.ToString());
                              return;
                            }
                            if (this.Index == 2)
                            {
                              this.SetTextValue(gvGperiod5.BattleStartTime.ToString());
                              return;
                            }
                            if (this.Index != 3)
                              return;
                            this.SetTextValue(gvGperiod5.BattleEndTime.ToString());
                            return;
                          case GameParameter.ParameterTypes.QUEST_STAMINA:
                            GvGPeriodParam gvGperiod6 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod6 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(gvGperiod6.Id.ToString());
                            return;
                          case GameParameter.ParameterTypes.QUEST_STATE:
                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null)
                            {
                              this.SetTextValue(GvGManager.Instance.CurrentRuleUnitCount);
                              return;
                            }
                            this.ResetToDefault();
                            return;
                          case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                            GameParameter.GvGStatus gvGstatus1 = GameParameter.GvGStatus.GVG_DECLARE;
                            if (GvGPeriodParam.GetGvGPeriod() != null)
                              gvGstatus1 = GameParameter.GvGStatus.GVG_BATTLE;
                            else if (MonoSingleton<GameManager>.Instance.GetNowScheduleGuildRaidPeriodParam() != null)
                              gvGstatus1 = GameParameter.GvGStatus.GVG_DECLARE_COOLTIME;
                            if (this.Index == 0 && gvGstatus1 == GameParameter.GvGStatus.GVG_BATTLE || this.Index == 1 && gvGstatus1 == GameParameter.GvGStatus.GVG_DECLARE_COOLTIME)
                            {
                              ((Component) this).gameObject.SetActive(true);
                              goto label_3654;
                            }
                            else
                            {
                              ((Component) this).gameObject.SetActive(false);
                              goto label_3654;
                            }
                          case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                            GvGManager instance77 = GvGManager.Instance;
                            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance77, (UnityEngine.Object) null))
                            {
                              ((Component) this).gameObject.SetActive(false);
                              return;
                            }
                            GvGManager.GvGStatus gvGstatus2 = instance77.GvGStatusPhaseSetPeriod();
                            if (this.Index == 0 && gvGstatus2 == GvGManager.GvGStatus.Declaration || this.Index == 1 && gvGstatus2 == GvGManager.GvGStatus.OffenseFirstHalf || this.Index == 1 && gvGstatus2 == GvGManager.GvGStatus.OffenseLatterHalf || this.Index == 2 && gvGstatus2 == GvGManager.GvGStatus.DeclarationCoolTime || this.Index == 3 && gvGstatus2 == GvGManager.GvGStatus.AggregationTime || this.Index == 4 && gvGstatus2 == GvGManager.GvGStatus.Finished || this.Index == 5 && gvGstatus2 == GvGManager.GvGStatus.OffenseCoolTime)
                            {
                              ((Component) this).gameObject.SetActive(true);
                              goto label_3654;
                            }
                            else
                            {
                              ((Component) this).gameObject.SetActive(false);
                              goto label_3654;
                            }
                          case GameParameter.ParameterTypes.ITEM_ICON:
                            GvGPeriodParam gvGperiod7 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod7 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(gvGperiod7.DeclareNum);
                            return;
                          case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                            if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(GvGManager.Instance.RemainDeclareCount);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_NAME:
                            GvGPeriodParam gvGperiod8 = GvGPeriodParam.GetGvGPeriod();
                            if (gvGperiod8 == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(string.Format(LocalizedText.Get("sys.GVG_DEFENSE_UNITMIN"), (object) gvGperiod8.DefenseUnitMin));
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                            GvGManager instance78 = GvGManager.Instance;
                            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance78, (UnityEngine.Object) null))
                            {
                              this.ResetToDefault();
                              return;
                            }
                            GvGNodeParam node = GvGNodeParam.GetNode(instance78.SelectNodeId);
                            if (node == null)
                            {
                              this.ResetToDefault();
                              return;
                            }
                            this.SetTextValue(string.Format(LocalizedText.Get("sys.GVG_DEFENSE_TEAMMAX"), (object) node.DefenseMax));
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                            if (GvGPeriodParam.GetGvGExitPeriod() != null)
                            {
                              ((Component) this).gameObject.SetActive(this.Index == 0);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(this.Index == 1);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
                            Button componentInChildren2 = ((Component) this).gameObject.GetComponentInChildren<Button>();
                            if (!((Component) this).gameObject.GetActive() || UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
                              return;
                            if (GvGPeriodParam.GetGvGExitPeriod() != null)
                            {
                              ((Selectable) componentInChildren2).interactable = this.Index == 0;
                              return;
                            }
                            ((Selectable) componentInChildren2).interactable = this.Index == 1;
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_ATK:
                            GvGLeagueViewGuild dataOfClass173 = DataSource.FindDataOfClass<GvGLeagueViewGuild>(((Component) this).gameObject, (GvGLeagueViewGuild) null);
                            if (dataOfClass173 == null || dataOfClass173.league == null)
                              return;
                            this.SetTextValue(dataOfClass173.league.Rank);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_HP:
                            GvGLeagueViewGuild dataOfClass174 = DataSource.FindDataOfClass<GvGLeagueViewGuild>(((Component) this).gameObject, (GvGLeagueViewGuild) null);
                            if (dataOfClass174 == null || dataOfClass174.league == null)
                              return;
                            this.SetTextValue(dataOfClass174.league.Rate);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_MAGIC:
                            GvGLeagueParam dataOfClass175 = DataSource.FindDataOfClass<GvGLeagueParam>(((Component) this).gameObject, (GvGLeagueParam) null);
                            if (dataOfClass175 == null)
                              return;
                            this.SetTextValue(dataOfClass175.MinRate);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_RARITY:
                            GvGLeagueParam dataOfClass176 = DataSource.FindDataOfClass<GvGLeagueParam>(((Component) this).gameObject, (GvGLeagueParam) null);
                            if (dataOfClass176 == null)
                              return;
                            this.SetTextValue(dataOfClass176.MaxRate);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_ELEMENT:
                            GvGLeagueViewGuild dataOfClass177 = DataSource.FindDataOfClass<GvGLeagueViewGuild>(((Component) this).gameObject, (GvGLeagueViewGuild) null);
                            Image component40 = ((Component) this).GetComponent<Image>();
                            if (dataOfClass177 == null || dataOfClass177.league == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component40, (UnityEngine.Object) null))
                            {
                              ((Component) this).gameObject.SetActive(false);
                              return;
                            }
                            GvGLeagueParam gvGleagueParam1 = GvGLeagueParam.GetGvGLeagueParam(dataOfClass177.league.Rate);
                            ((Component) this).gameObject.SetActive(true);
                            SpriteSheet spriteSheet21 = AssetManager.Load<SpriteSheet>("UI/GvG/gvg_league");
                            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet21, (UnityEngine.Object) null) || gvGleagueParam1 == null)
                              return;
                            component40.sprite = spriteSheet21.GetSprite(gvGleagueParam1.LeagueIconSpriteKey);
                            ((Behaviour) component40).enabled = true;
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_ICON:
                            GvGLeagueViewGuild dataOfClass178 = DataSource.FindDataOfClass<GvGLeagueViewGuild>(((Component) this).gameObject, (GvGLeagueViewGuild) null);
                            if (dataOfClass178 == null || dataOfClass178.league == null)
                            {
                              ((Component) this).gameObject.SetActive(false);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(true);
                            GvGLeagueParam gvGleagueParam2 = GvGLeagueParam.GetGvGLeagueParam(dataOfClass178.league.Rate);
                            if (gvGleagueParam2 == null)
                              return;
                            this.SetTextValue(gvGleagueParam2.Name);
                            return;
                          case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLDESC:
                            GvGLeagueViewGuild dataOfClass179 = DataSource.FindDataOfClass<GvGLeagueViewGuild>(((Component) this).gameObject, (GvGLeagueViewGuild) null);
                            Image component41 = ((Component) this).GetComponent<Image>();
                            if (dataOfClass179 == null || dataOfClass179.league == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component41, (UnityEngine.Object) null))
                            {
                              ((Component) this).gameObject.SetActive(false);
                              return;
                            }
                            GvGLeagueParam gvGleagueParam3 = GvGLeagueParam.GetGvGLeagueParam(dataOfClass179.league.Rate);
                            ((Component) this).gameObject.SetActive(true);
                            SpriteSheet spriteSheet22 = AssetManager.Load<SpriteSheet>("UI/GvG/gvg_league");
                            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet22, (UnityEngine.Object) null) || gvGleagueParam3 == null)
                              return;
                            component41.sprite = spriteSheet22.GetSprite(gvGleagueParam3.LeagueNameSpriteKey);
                            ((Behaviour) component41).enabled = true;
                            return;
                          case GameParameter.ParameterTypes.QUEST_SUBTITLE:
                            Image component42 = ((Component) this).GetComponent<Image>();
                            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component42, (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.Player == null || MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate < 0)
                            {
                              ((Component) this).gameObject.SetActive(false);
                              return;
                            }
                            GvGLeagueParam gvGleagueParam4 = GvGLeagueParam.GetGvGLeagueParam(MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate);
                            ((Component) this).gameObject.SetActive(true);
                            SpriteSheet spriteSheet23 = AssetManager.Load<SpriteSheet>("UI/GvG/gvg_league");
                            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet23, (UnityEngine.Object) null) || gvGleagueParam4 == null)
                              return;
                            component42.sprite = spriteSheet23.GetSprite(gvGleagueParam4.LeagueIconSpriteKey);
                            ((Behaviour) component42).enabled = true;
                            return;
                          case GameParameter.ParameterTypes.UNIT_LEVEL:
                            bool flag21 = MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate > 0;
                            if (!flag21 && MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate == 0 && MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin)
                              flag21 = true;
                            if (this.Index == 0)
                            {
                              ((Component) this).gameObject.SetActive(flag21);
                              return;
                            }
                            ((Component) this).gameObject.SetActive(!flag21);
                            return;
                          case GameParameter.ParameterTypes.UNIT_HP:
                            bool flag22 = false;
                            UnitData unitData66 = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
                            if (unitData66 == null)
                            {
                              Unit dataOfClass180 = DataSource.FindDataOfClass<Unit>(((Component) this).gameObject, (Unit) null);
                              if (dataOfClass180 != null)
                                unitData66 = dataOfClass180.UnitData;
                            }
                            if (unitData66 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.UsedUnitList != null && GvGManager.Instance.UsedUnitList.Contains(unitData66.UniqueID))
                              flag22 = true;
                            ((Component) this).gameObject.SetActive(flag22);
                            goto label_3654;
                          case GameParameter.ParameterTypes.UNIT_HPMAX:
                            bool flag23 = false;
                            ArtifactData dataOfClass181 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
                            if (dataOfClass181 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.GvGUsedList != null)
                              flag23 = GvGManager.Instance.GvGUsedList.IsArtifactUsed((long) dataOfClass181.UniqueID);
                            ((Component) this).gameObject.SetActive(flag23);
                            goto label_3654;
                          case GameParameter.ParameterTypes.UNIT_ATK:
                            bool flag24 = false;
                            ConceptCardData dataOfClass182 = DataSource.FindDataOfClass<ConceptCardData>(((Component) this).gameObject, (ConceptCardData) null);
                            if (dataOfClass182 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.GvGUsedList != null)
                              flag24 = GvGManager.Instance.GvGUsedList.IsConceptCardUsed((long) dataOfClass182.UniqueID);
                            ((Component) this).gameObject.SetActive(flag24);
                            goto label_3654;
                          case GameParameter.ParameterTypes.UNIT_MAG:
                            bool flag25 = false;
                            RuneData dataOfClass183 = DataSource.FindDataOfClass<RuneData>(((Component) this).gameObject, (RuneData) null);
                            if (dataOfClass183 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.GvGUsedList != null)
                              flag25 = GvGManager.Instance.GvGUsedList.IsRuneUsed((long) dataOfClass183.UniqueID);
                            ((Component) this).gameObject.SetActive(flag25);
                            goto label_3654;
                          default:
                            switch (parameterType - 1600)
                            {
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                MultiPlayAPIRoom dataOfClass184 = DataSource.FindDataOfClass<MultiPlayAPIRoom>(((Component) this).gameObject, (MultiPlayAPIRoom) null);
                                if (dataOfClass184 != null)
                                {
                                  if (dataOfClass184.limit == 0)
                                  {
                                    this.SetTextValue("-");
                                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
                                      return;
                                    ((Graphic) this.mText).color = new Color(0.0f, 0.0f, 0.0f);
                                    return;
                                  }
                                  if (dataOfClass184.unitlv == 0)
                                  {
                                    this.SetTextValue(LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"));
                                    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
                                      return;
                                    ((Graphic) this.mText).color = new Color(0.0f, 0.0f, 0.0f);
                                    return;
                                  }
                                  this.SetTextValue(dataOfClass184.unitlv.ToString() + LocalizedText.Get("sys.MULTI_JOINLIMIT_OVER"));
                                  if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
                                    return;
                                  GameManager instance79 = MonoSingleton<GameManager>.Instance;
                                  PlayerData player14 = instance79.Player;
                                  PartyData party = player14.Partys[2];
                                  QuestParam quest3 = instance79.FindQuest(dataOfClass184.quest.iname);
                                  bool flag26 = true;
                                  if (party != null && quest3 != null)
                                  {
                                    for (int index18 = 0; index18 < (int) quest3.unitNum; ++index18)
                                    {
                                      long unitUniqueId = party.GetUnitUniqueID(0);
                                      if (unitUniqueId <= 0L)
                                      {
                                        flag26 = false;
                                        break;
                                      }
                                      UnitData unitDataByUniqueId = player14.FindUnitDataByUniqueID(unitUniqueId);
                                      if (unitDataByUniqueId != null)
                                        flag26 &= unitDataByUniqueId.CalcLevel() >= dataOfClass184.unitlv;
                                    }
                                  }
                                  if (flag26)
                                  {
                                    ((Graphic) this.mText).color = new Color(0.0f, 0.0f, 0.0f);
                                    return;
                                  }
                                  ((Graphic) this.mText).color = new Color(1f, 0.0f, 0.0f);
                                  return;
                                }
                                this.ResetToDefault();
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                MultiPlayAPIRoom dataOfClass185 = DataSource.FindDataOfClass<MultiPlayAPIRoom>(((Component) this).gameObject, (MultiPlayAPIRoom) null);
                                if (dataOfClass185 != null)
                                {
                                  if (dataOfClass185.limit == 0)
                                  {
                                    this.SetTextValue("-");
                                    return;
                                  }
                                  if (dataOfClass185.clear == 0)
                                  {
                                    this.SetTextValue(LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"));
                                    return;
                                  }
                                  this.SetTextValue(LocalizedText.Get("sys.MULTI_JOIN_LIMIT_ONLY_CLEAR"));
                                  return;
                                }
                                this.ResetToDefault();
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                GameManager instance80 = MonoSingleton<GameManager>.Instance;
                                SceneBattle bs4 = SceneBattle.Instance;
                                if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs4, (UnityEngine.Object) null) || bs4.Battle == null || bs4.Battle.CurrentUnit == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                if (instance80.AudienceMode)
                                {
                                  AudienceStartParam startedParam = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
                                  if (bs4.Battle.CurrentUnit.OwnerPlayerIndex <= 0 || bs4.Battle.CurrentUnit.OwnerPlayerIndex > 2)
                                  {
                                    this.ResetToDefault();
                                    return;
                                  }
                                  JSON_MyPhotonPlayerParam player15 = startedParam.players[bs4.Battle.CurrentUnit.OwnerPlayerIndex - 1];
                                  if (player15 == null)
                                    return;
                                  string str27 = player15.playerName;
                                  if (this.Index == 1)
                                    str27 = player15.playerName + LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
                                  else if (this.Index == 2)
                                    str27 = player15.playerName + LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
                                  this.SetTextValue(str27);
                                  return;
                                }
                                if (instance80.IsVSCpuBattle)
                                {
                                  string name4 = instance80.Player.Name;
                                  bool flag27 = bs4.Battle.CurrentUnit.OwnerPlayerIndex != 1;
                                  if (flag27)
                                  {
                                    VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                                    if (versusCpu != null)
                                      name4 = versusCpu.Name;
                                  }
                                  if (this.Index == 1)
                                    name4 += LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
                                  else if (this.Index == 2)
                                  {
                                    if (flag27)
                                      name4 += LocalizedText.Get("sys.MULTI_VS_CPU");
                                    name4 += LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
                                  }
                                  this.SetTextValue(name4);
                                  return;
                                }
                                MyPhoton instance81 = PunMonoSingleton<MyPhoton>.Instance;
                                JSON_MyPhotonPlayerParam photonPlayerParam4 = instance81.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs4.Battle.CurrentUnit.OwnerPlayerIndex));
                                List<MyPhoton.MyPlayer> roomPlayerList4 = instance81.GetRoomPlayerList();
                                MyPhoton.MyPlayer player16 = photonPlayerParam4 != null ? instance81.FindPlayer(roomPlayerList4, photonPlayerParam4.playerID, photonPlayerParam4.playerIndex) : (MyPhoton.MyPlayer) null;
                                if (photonPlayerParam4 == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                string str28 = photonPlayerParam4.playerName;
                                if (this.Index == 0)
                                {
                                  if (player16 == null)
                                    ((Graphic) this.mText).color = new Color(0.5f, 0.5f, 0.5f);
                                  else
                                    ((Graphic) this.mText).color = new Color(1f, 1f, 1f);
                                }
                                else if (this.Index == 1)
                                  str28 = photonPlayerParam4.playerName + LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
                                else if (this.Index == 2)
                                  str28 = photonPlayerParam4.playerName + LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
                                this.SetTextValue(str28);
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                JSON_MyPhotonRoomParam roomParam8 = this.GetRoomParam();
                                JSON_MyPhotonPlayerParam roomPlayerParam12 = this.GetRoomPlayerParam();
                                if (roomPlayerParam12 == null || roomPlayerParam12.units == null || roomParam8 == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                int num44 = 0;
                                int unitSlotNum3 = roomParam8.GetUnitSlotNum(roomPlayerParam12.playerIndex);
                                for (int index19 = 0; index19 < roomPlayerParam12.units.Length; ++index19)
                                {
                                  if (roomPlayerParam12.units[index19].slotID < unitSlotNum3 && roomPlayerParam12.units[index19].unit != null)
                                    num44 = num44 + (int) roomPlayerParam12.units[index19].unit.Status.param.atk + (int) roomPlayerParam12.units[index19].unit.Status.param.mag;
                                }
                                this.SetTextValue(num44);
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                List<MyPhoton.MyPlayer> roomPlayerList5 = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
                                if (roomPlayerList5 == null)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                MyPhoton.MyPlayer myPlayer1 = roomPlayerList5.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == 1));
                                if (myPlayer1 != null)
                                {
                                  bool flag28 = JSON_MyPhotonPlayerParam.Parse(myPlayer1.json).state == this.InstanceType;
                                  switch (this.Index)
                                  {
                                    case 0:
                                      ((Component) this).gameObject.SetActive(flag28);
                                      return;
                                    case 1:
                                      Button component43 = ((Component) this).gameObject.GetComponent<Button>();
                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component43, (UnityEngine.Object) null))
                                      {
                                        ((Selectable) component43).interactable = flag28;
                                        return;
                                      }
                                      ((Component) this).gameObject.SetActive(flag28);
                                      return;
                                    default:
                                      ((Component) this).gameObject.SetActive(false);
                                      return;
                                  }
                                }
                                else
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                UnityEngine.UI.Text component44 = ((Component) this).GetComponent<UnityEngine.UI.Text>();
                                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component44, (UnityEngine.Object) null))
                                  return;
                                MyPhoton instance82 = PunMonoSingleton<MyPhoton>.Instance;
                                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance82, (UnityEngine.Object) null))
                                {
                                  MyPhoton.MyRoom currentRoom3 = instance82.GetCurrentRoom();
                                  if (currentRoom3 != null)
                                  {
                                    JSON_MyPhotonRoomParam myPhotonRoomParam3 = JSON_MyPhotonRoomParam.Parse(currentRoom3.json);
                                    if (myPhotonRoomParam3 != null)
                                    {
                                      component44.text = myPhotonRoomParam3.challegedMTFloor.ToString() + "!";
                                      return;
                                    }
                                  }
                                }
                                this.ResetToDefault();
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                QuestParam dataOfClass186 = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
                                if (dataOfClass186 != null)
                                {
                                  ((Component) this).gameObject.SetActive(dataOfClass186.IsMultiLeaderSkill);
                                  return;
                                }
                                ((Component) this).gameObject.SetActive(false);
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                MultiPlayAPIRoom room8 = this.GetRoom();
                                if (room8.floor <= 0)
                                {
                                  this.SetTextValue(string.Empty);
                                  return;
                                }
                                this.SetTextValue(string.Format(LocalizedText.Get("sys.MULTI_TOWER_FLOOR_NAME"), (object) room8.floor));
                                return;
                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                MultiPlayAPIRoom room9 = this.GetRoom();
                                Transform transform2 = ((Component) this).transform.Find("Clear");
                                Transform transform3 = ((Component) this).transform.Find("NotClear");
                                bool flag29 = room9.is_clear == 1;
                                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
                                  ((Component) transform2).gameObject.SetActive(flag29);
                                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform3, (UnityEngine.Object) null))
                                  return;
                                ((Component) transform3).gameObject.SetActive(!flag29);
                                return;
                              case GameParameter.ParameterTypes.QUEST_NAME:
                                bool flag30 = false;
                                GameManager instance83 = MonoSingleton<GameManager>.Instance;
                                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance83))
                                  flag30 = instance83.IsMTCanSkip();
                                if (this.Index != 0)
                                  flag30 = !flag30;
                                ((Component) this).gameObject.SetActive(flag30);
                                return;
                              case GameParameter.ParameterTypes.QUEST_STAMINA:
                                MyPhoton.MyRoom currentRoom4 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
                                JSON_MyPhotonRoomParam myPhotonRoomParam4 = currentRoom4 != null ? JSON_MyPhotonRoomParam.Parse(currentRoom4.json) : (JSON_MyPhotonRoomParam) null;
                                if (myPhotonRoomParam4 != null)
                                {
                                  float[] battleSpeedList = BattleSpeedController.CreateBattleSpeedList();
                                  for (int index20 = 0; index20 < battleSpeedList.Length; ++index20)
                                  {
                                    if ((double) battleSpeedList[index20] == (double) myPhotonRoomParam4.btlSpd)
                                    {
                                      this.SetImageIndex(index20);
                                      return;
                                    }
                                  }
                                }
                                this.ResetToDefault();
                                return;
                              case GameParameter.ParameterTypes.QUEST_STATE:
                                MultiPlayAPIRoom room10 = this.GetRoom();
                                if (room10 != null)
                                {
                                  float[] battleSpeedList = BattleSpeedController.CreateBattleSpeedList();
                                  for (int index21 = 0; index21 < battleSpeedList.Length; ++index21)
                                  {
                                    if ((double) battleSpeedList[index21] == (double) room10.btl_speed)
                                    {
                                      this.SetImageIndex(index21);
                                      return;
                                    }
                                  }
                                }
                                this.ResetToDefault();
                                return;
                              case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                                MyPhoton.MyRoom currentRoom5 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
                                JSON_MyPhotonRoomParam myPhotonRoomParam5 = currentRoom5 != null ? JSON_MyPhotonRoomParam.Parse(currentRoom5.json) : (JSON_MyPhotonRoomParam) null;
                                if (myPhotonRoomParam5 == null || myPhotonRoomParam5.autoAllowed == 0)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                this.SetImageIndex(1);
                                return;
                              case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                                MultiPlayAPIRoom room11 = this.GetRoom();
                                if (room11 == null || room11.enable_auto == 0)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                this.SetImageIndex(1);
                                return;
                              case GameParameter.ParameterTypes.ITEM_ICON:
                                if (!PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer())
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                JSON_MyPhotonPlayerParam roomPlayerParam13 = this.GetRoomPlayerParam();
                                if (roomPlayerParam13 == null || roomPlayerParam13.playerID <= 0)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                if (roomPlayerParam13.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                if (roomPlayerParam13.isSupportAI != 0)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                ((Component) this).gameObject.SetActive(true);
                                return;
                              case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                                if (!PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer())
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                JSON_MyPhotonPlayerParam roomPlayerParam14 = this.GetRoomPlayerParam();
                                if (roomPlayerParam14 == null || roomPlayerParam14.playerID <= 0)
                                {
                                  ((Component) this).gameObject.SetActive(this.Index == 0);
                                  return;
                                }
                                if (roomPlayerParam14.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                if (roomPlayerParam14.isSupportAI == 0)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                ((Component) this).gameObject.SetActive(true);
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_NAME:
                                JSON_MyPhotonRoomParam roomParam9 = this.GetRoomParam();
                                if (roomParam9 == null)
                                {
                                  GameUtility.SetGameObjectActive(((Component) this).gameObject, false);
                                  return;
                                }
                                int index22 = 0;
                                if (roomParam9.autoAllowed != 0)
                                {
                                  JSON_MyPhotonPlayerParam roomPlayerParam15 = this.GetRoomPlayerParam();
                                  if (roomPlayerParam15 == null || string.IsNullOrEmpty(roomPlayerParam15.UID))
                                  {
                                    GameUtility.SetGameObjectActive(((Component) this).gameObject, false);
                                    return;
                                  }
                                  GameUtility.SetGameObjectActive(((Component) this).gameObject, true);
                                  if (roomPlayerParam15.isAuto != 0)
                                    index22 = 1;
                                }
                                this.SetImageIndex(index22);
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                                JSON_MyPhotonPlayerParam roomPlayerParam16 = this.GetRoomPlayerParam();
                                if (roomPlayerParam16 == null || roomPlayerParam16.playerID <= 0)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                ((Component) this).gameObject.SetActive(roomPlayerParam16.isSupportAI != 0);
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                                SceneBattle instance84 = SceneBattle.Instance;
                                if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance84, (UnityEngine.Object) null) || instance84.Battle == null || instance84.Battle.CurrentUnit == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                GameManager instance85 = MonoSingleton<GameManager>.Instance;
                                if (instance85.AudienceMode || instance85.IsVSCpuBattle)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                if (UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                ((Component) this).gameObject.SetActive(SceneBattle.Instance.IsMultiPlayerAuto(instance84.Battle.CurrentUnit, true));
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_LEADERSKILLNAME:
                                SceneBattle bs5 = SceneBattle.Instance;
                                if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs5, (UnityEngine.Object) null) || bs5.Battle == null || bs5.Battle.CurrentUnit == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                GameManager instance86 = MonoSingleton<GameManager>.Instance;
                                if (instance86.AudienceMode || instance86.IsVSCpuBattle)
                                {
                                  ((Component) this).gameObject.SetActive(false);
                                  return;
                                }
                                JSON_MyPhotonPlayerParam photonPlayerParam5 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs5.Battle.CurrentUnit.OwnerPlayerIndex));
                                ((Component) this).gameObject.SetActive(photonPlayerParam5 != null && photonPlayerParam5.isSupportAI != 0);
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_ATK:
                                UnityEngine.UI.Text component45 = ((Component) this).GetComponent<UnityEngine.UI.Text>();
                                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component45, (UnityEngine.Object) null))
                                  return;
                                ((Behaviour) component45).enabled = false;
                                QuestParam questParam24 = this.GetQuestParam();
                                if (questParam24 == null || questParam24.EntryCondition == null)
                                  return;
                                ((Behaviour) component45).enabled = questParam24.EntryCondition.is_not_solo;
                                return;
                              case GameParameter.ParameterTypes.SUPPORTER_HP:
                                JSON_MyPhotonRoomParam roomParam10 = this.GetRoomParam();
                                JSON_MyPhotonPlayerParam roomPlayerParam17 = this.GetRoomPlayerParam();
                                if (roomPlayerParam17 == null || roomPlayerParam17.units == null || roomParam10 == null)
                                {
                                  this.ResetToDefault();
                                  return;
                                }
                                int unitNum = roomParam10.GetUnitSlotNum(roomPlayerParam17.playerIndex);
                                this.SetTextValue(PartyUtility.CalcTotalCombatPower(((IEnumerable<JSON_MyPhotonPlayerParam.UnitDataElem>) roomPlayerParam17.units).Where<JSON_MyPhotonPlayerParam.UnitDataElem>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, bool>) (u => u.slotID < unitNum && u.unit != null)).Select<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>) (u => u.unit)).ToList<UnitData>()));
                                return;
                              default:
                                switch (parameterType - 1000)
                                {
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                    SceneBattle instance87 = SceneBattle.Instance;
                                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance87, (UnityEngine.Object) null) && instance87.Battle != null)
                                    {
                                      if (instance87.Battle.GetQuestResult() == BattleCore.QuestResult.Win)
                                      {
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_WIN"));
                                        return;
                                      }
                                      this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_JOIN"));
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                    ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.VersusSeazonGiftReceipt);
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                    instance1 = MonoSingleton<GameManager>.Instance;
                                    SceneBattle instance88 = SceneBattle.Instance;
                                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance88, (UnityEngine.Object) null) && instance88.CurrentQuest != null)
                                    {
                                      BattleCore battle = instance88.Battle;
                                      if (battle != null)
                                      {
                                        BattleCore.Record questRecord = battle.GetQuestRecord();
                                        if (questRecord != null)
                                        {
                                          this.SetTextValue((int) questRecord.pvpcoin);
                                          return;
                                        }
                                      }
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                    GameManager instance89 = MonoSingleton<GameManager>.Instance;
                                    this.SetTextValue(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_COIN"), (object) instance89.VersusCoinRemainCnt));
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                    GameManager instance90 = MonoSingleton<GameManager>.Instance;
                                    if (this.Index == 0)
                                    {
                                      ((Component) this).gameObject.SetActive(instance90.VersusTowerMatchBegin);
                                      return;
                                    }
                                    if (this.Index != 1)
                                      return;
                                    ((Component) this).gameObject.SetActive(!instance90.VersusTowerMatchBegin);
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                    if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
                                    {
                                      this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_CPU"));
                                      return;
                                    }
                                    switch (GlobalVars.SelectedMultiPlayVersusType)
                                    {
                                      case VERSUS_TYPE.Free:
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_FREE"));
                                        return;
                                      case VERSUS_TYPE.Tower:
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_TOWER"));
                                        return;
                                      case VERSUS_TYPE.Friend:
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND"));
                                        return;
                                      case VERSUS_TYPE.RankMatch:
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_RANKMATCH"));
                                        return;
                                      default:
                                        return;
                                    }
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                    PlayerData player17 = MonoSingleton<GameManager>.Instance.Player;
                                    SceneBattle instance91 = SceneBattle.Instance;
                                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance91, (UnityEngine.Object) null) && player17 != null)
                                    {
                                      BattleCore battle = instance91.Battle;
                                      if (battle != null)
                                      {
                                        BattleCore.Record questRecord = battle.GetQuestRecord();
                                        if (questRecord != null)
                                        {
                                          ((Component) this).gameObject.SetActive(questRecord.result == BattleCore.QuestResult.Win && player17.VersusTowerWinBonus > 0);
                                          return;
                                        }
                                      }
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                    SceneBattle bs6 = SceneBattle.Instance;
                                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) bs6, (UnityEngine.Object) null) || bs6.Battle == null || bs6.Battle.CurrentUnit == null)
                                    {
                                      this.ResetToDefault();
                                      return;
                                    }
                                    MyPhoton instance92 = PunMonoSingleton<MyPhoton>.Instance;
                                    JSON_MyPhotonPlayerParam param = instance92.GetMyPlayersStarted()?.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex == bs6.Battle.CurrentUnit.OwnerPlayerIndex));
                                    List<MyPhoton.MyPlayer> roomPlayerList6 = instance92.GetRoomPlayerList();
                                    MyPhoton.MyPlayer myPlayer2 = roomPlayerList6 == null || param == null ? (MyPhoton.MyPlayer) null : roomPlayerList6.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == param.playerID));
                                    if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                    {
                                      ((Component) this).gameObject.SetActive(true);
                                      return;
                                    }
                                    if (this.Index == 0)
                                    {
                                      ((Component) this).gameObject.SetActive(myPlayer2 == null);
                                      return;
                                    }
                                    if (this.Index == 1)
                                    {
                                      ((Component) this).gameObject.SetActive(myPlayer2 != null);
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                    SceneBattle instance93 = SceneBattle.Instance;
                                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance93, (UnityEngine.Object) null) || instance93.Battle == null || instance93.Battle.CurrentUnit == null)
                                    {
                                      this.ResetToDefault();
                                      return;
                                    }
                                    BattleCore.QuestResult questResult = instance93.CheckAudienceResult();
                                    if (questResult == BattleCore.QuestResult.Pending)
                                    {
                                      this.ResetToDefault();
                                      return;
                                    }
                                    if (this.Index == 0)
                                    {
                                      ((Component) this).gameObject.SetActive(questResult == BattleCore.QuestResult.Win);
                                      return;
                                    }
                                    if (this.Index != 1)
                                      return;
                                    ((Component) this).gameObject.SetActive(questResult == BattleCore.QuestResult.Lose);
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_NAME:
                                    ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.AudienceMode);
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_STAMINA:
                                    MyPhoton.MyRoom audienceRoom = MonoSingleton<GameManager>.Instance.AudienceRoom;
                                    if (audienceRoom != null)
                                    {
                                      if (audienceRoom.name.IndexOf("_free") != -1)
                                      {
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_FREE"));
                                        return;
                                      }
                                      if (audienceRoom.name.IndexOf("_tower") != -1)
                                      {
                                        this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_TOWER"));
                                        return;
                                      }
                                      if (audienceRoom.name.IndexOf("_friend") == -1)
                                        return;
                                      this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_FRIEND"));
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_STATE:
                                    this.SetTextValue(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_TOWER_NOW_FLOOR"), (object) GameUtility.HalfNum2FullNum(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor.ToString())));
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                                    ((Component) this).gameObject.SetActive(GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Tower);
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                                    GameManager instance94 = MonoSingleton<GameManager>.Instance;
                                    if (instance94.AudienceRoom != null)
                                    {
                                      int audienceMax = instance94.AudienceRoom.audienceMax;
                                      JSON_MyPhotonRoomParam roomParam11 = instance94.AudienceManager.GetRoomParam();
                                      if (roomParam11 != null)
                                      {
                                        this.SetTextValue(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_NUM"), (object) GameUtility.HalfNum2FullNum(roomParam11.audienceNum.ToString()), (object) GameUtility.HalfNum2FullNum(audienceMax.ToString())));
                                        return;
                                      }
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.ITEM_ICON:
                                    if (GlobalVars.VersusCpu != null)
                                    {
                                      ImageArray component46 = ((Component) this).gameObject.GetComponent<ImageArray>();
                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component46, (UnityEngine.Object) null))
                                      {
                                        component46.ImageIndex = GlobalVars.VersusCpu.Get().Deck - 1;
                                        return;
                                      }
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.QUEST_DESCRIPTION:
                                    VersusCpuData dataOfClass187 = DataSource.FindDataOfClass<VersusCpuData>(((Component) this).gameObject, (VersusCpuData) null);
                                    if (dataOfClass187 != null)
                                    {
                                      this.SetTextValue(dataOfClass187.Name);
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.SUPPORTER_NAME:
                                    VersusCpuData dataOfClass188 = DataSource.FindDataOfClass<VersusCpuData>(((Component) this).gameObject, (VersusCpuData) null);
                                    if (dataOfClass188 != null)
                                    {
                                      this.SetTextValue(dataOfClass188.Lv);
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.SUPPORTER_LEVEL:
                                    VersusCpuData dataOfClass189 = DataSource.FindDataOfClass<VersusCpuData>(((Component) this).gameObject, (VersusCpuData) null);
                                    if (dataOfClass189 != null)
                                    {
                                      this.SetTextValue(dataOfClass189.Score);
                                      return;
                                    }
                                    this.ResetToDefault();
                                    return;
                                  case GameParameter.ParameterTypes.SUPPORTER_UNITLEVEL:
                                    GameUtility.SetGameObjectActive(((Component) this).gameObject, DataSource.FindDataOfClass<UnitSameGroupParam>(((Component) this).gameObject, (UnitSameGroupParam) null) != null);
                                    return;
                                  default:
                                    switch (parameterType - 2100)
                                    {
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                        UnitParam unitParam16;
                                        if ((unitParam16 = this.GetUnitParam()) == null)
                                          return;
                                        UnitUnlockTimeParam unitUnlockTimeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitUnlockTimeParam(unitParam16.unlock_time);
                                        if (unitUnlockTimeParam == null)
                                          return;
                                        string format3 = "yyyy/MM/dd HH:mm";
                                        this.SetTextValue(unitUnlockTimeParam.end_at.ToString(format3));
                                        return;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                        if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                        {
                                          JSON_MyPhotonPlayerParam player18 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
                                          if (player18 != null)
                                          {
                                            this.SetTextValue(player18.totalStatus.ToString());
                                            return;
                                          }
                                        }
                                        else
                                        {
                                          JSON_MyPhotonPlayerParam versusPlayerParam2 = this.GetVersusPlayerParam();
                                          if (versusPlayerParam2 != null)
                                          {
                                            this.SetTextValue(versusPlayerParam2.totalStatus.ToString());
                                            return;
                                          }
                                          if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
                                          {
                                            VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
                                            if (versusCpu != null)
                                            {
                                              this.SetTextValue(versusCpu.CombatPower);
                                              return;
                                            }
                                          }
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                        MyPhoton instance95 = PunMonoSingleton<MyPhoton>.Instance;
                                        UnitData unitData67 = (UnitData) null;
                                        if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                        {
                                          JSON_MyPhotonPlayerParam player19 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
                                          if (player19 != null)
                                          {
                                            player19.SetupUnits();
                                            if (player19.units.Length > 0)
                                              unitData67 = player19.units[0].unit;
                                          }
                                        }
                                        else
                                        {
                                          MyPhoton.MyPlayer player = instance95.GetMyPlayer();
                                          if (this.InstanceType == 0)
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam6 = JSON_MyPhotonPlayerParam.Parse(player.json);
                                            if (photonPlayerParam6.units != null && photonPlayerParam6.units.Length > 0 && photonPlayerParam6.units[0].unit != null)
                                              unitData67 = photonPlayerParam6.units[0].unit;
                                          }
                                          else
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam7 = instance95.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != player.playerID));
                                            if (photonPlayerParam7.units != null && photonPlayerParam7.units.Length > 0 && photonPlayerParam7.units[0].unit != null)
                                              unitData67 = photonPlayerParam7.units[0].unit;
                                          }
                                        }
                                        if (unitData67 != null && !string.IsNullOrEmpty(unitData67.UnitParam.image))
                                        {
                                          IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
                                          string str29 = AssetPath.UnitSkinImage(unitData67.UnitParam, unitData67.GetSelectedSkin(), unitData67.CurrentJob.JobID);
                                          if (!string.IsNullOrEmpty(str29))
                                          {
                                            iconLoader.ResourcePath = str29;
                                            return;
                                          }
                                        }
                                        this.ResetToDefault();
                                        goto label_3654;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                        UnitData data2 = (UnitData) null;
                                        if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                        {
                                          AudienceStartParam startedParam = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
                                          int index23;
                                          int index24;
                                          if (this.InstanceType < 5)
                                          {
                                            index23 = 0;
                                            index24 = this.InstanceType;
                                          }
                                          else
                                          {
                                            index23 = 1;
                                            index24 = this.InstanceType - 5;
                                          }
                                          JSON_MyPhotonPlayerParam player20 = startedParam.players[index23];
                                          if (player20 != null)
                                          {
                                            player20.SetupUnits();
                                            if (player20.units.Length > index24)
                                              data2 = player20.units[index24].unit;
                                          }
                                        }
                                        else
                                        {
                                          MyPhoton instance96 = PunMonoSingleton<MyPhoton>.Instance;
                                          MyPhoton.MyPlayer player = instance96.GetMyPlayer();
                                          JSON_MyPhotonPlayerParam.UnitDataElem[] unitDataElemArray = (JSON_MyPhotonPlayerParam.UnitDataElem[]) null;
                                          int index25;
                                          if (this.InstanceType < 5)
                                          {
                                            index25 = this.InstanceType;
                                            JSON_MyPhotonPlayerParam photonPlayerParam8 = JSON_MyPhotonPlayerParam.Parse(player.json);
                                            if (photonPlayerParam8.units != null && photonPlayerParam8.units.Length > 0)
                                              unitDataElemArray = photonPlayerParam8.units;
                                          }
                                          else
                                          {
                                            index25 = this.InstanceType - 5;
                                            JSON_MyPhotonPlayerParam photonPlayerParam9 = instance96.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != player.playerID));
                                            if (photonPlayerParam9.units != null && photonPlayerParam9.units.Length > 0)
                                              unitDataElemArray = photonPlayerParam9.units;
                                          }
                                          if (unitDataElemArray != null && index25 < unitDataElemArray.Length && unitDataElemArray[index25].unit != null)
                                            data2 = unitDataElemArray[index25].unit;
                                        }
                                        if (this.ParameterType == GameParameter.ParameterTypes.VERSUS_DRAFT_PARTY_IMAGE_ICON)
                                        {
                                          if (data2 != null)
                                          {
                                            DataSource.Bind<UnitData>(((Component) this).gameObject, data2);
                                            UnitIcon component47 = ((Component) this).gameObject.GetComponent<UnitIcon>();
                                            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component47, (UnityEngine.Object) null))
                                              return;
                                            component47.UpdateValue();
                                            return;
                                          }
                                          ((Component) this).gameObject.SetActive(false);
                                          goto label_3654;
                                        }
                                        else if (this.ParameterType == GameParameter.ParameterTypes.VERSUS_DRAFT_PARTY_IMAGE)
                                        {
                                          if (data2 != null && !string.IsNullOrEmpty(data2.UnitParam.image))
                                          {
                                            IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject);
                                            string str30 = AssetPath.UnitSkinImage(data2.UnitParam, data2.GetSelectedSkin(), data2.CurrentJob.JobID);
                                            if (!string.IsNullOrEmpty(str30))
                                            {
                                              iconLoader.ResourcePath = str30;
                                              return;
                                            }
                                          }
                                          this.ResetToDefault();
                                          goto label_3654;
                                        }
                                        else
                                          goto label_3654;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                        if (this.InstanceType == 0)
                                        {
                                          ((Component) this).gameObject.SetActive(VersusDraftList.VersusDraftTurnOwn);
                                          goto label_3654;
                                        }
                                        else
                                        {
                                          ((Component) this).gameObject.SetActive(!VersusDraftList.VersusDraftTurnOwn);
                                          goto label_3654;
                                        }
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                        int num45 = 0;
                                        if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                        {
                                          JSON_MyPhotonPlayerParam player21 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType != 0 ? 1 : 0];
                                          if (player21 != null)
                                          {
                                            player21.SetupUnits();
                                            for (int index26 = 0; index26 < player21.units.Length; ++index26)
                                              num45 = num45 + (int) player21.units[index26].unit.Status.param.atk + (int) player21.units[index26].unit.Status.param.mag;
                                          }
                                        }
                                        else
                                        {
                                          MyPhoton instance97 = PunMonoSingleton<MyPhoton>.Instance;
                                          MyPhoton.MyPlayer player = instance97.GetMyPlayer();
                                          JSON_MyPhotonPlayerParam.UnitDataElem[] unitDataElemArray = (JSON_MyPhotonPlayerParam.UnitDataElem[]) null;
                                          if (this.InstanceType == 0)
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam10 = JSON_MyPhotonPlayerParam.Parse(player.json);
                                            if (photonPlayerParam10.units != null && photonPlayerParam10.units.Length > 0)
                                              unitDataElemArray = photonPlayerParam10.units;
                                          }
                                          else
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam11 = instance97.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != player.playerID));
                                            if (photonPlayerParam11.units != null && photonPlayerParam11.units.Length > 0)
                                              unitDataElemArray = photonPlayerParam11.units;
                                          }
                                          if (unitDataElemArray != null)
                                          {
                                            for (int index27 = 0; index27 < unitDataElemArray.Length; ++index27)
                                              num45 = num45 + (int) unitDataElemArray[index27].unit.Status.param.atk + (int) unitDataElemArray[index27].unit.Status.param.mag;
                                          }
                                        }
                                        this.SetTextValue(num45);
                                        goto label_3654;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                        UnitData unitData68;
                                        if ((unitData68 = this.GetUnitData()) != null)
                                        {
                                          UnitRentalParam unitRentalParam = UnitRentalParam.GetParam(unitData68.RentalIname);
                                          if (unitRentalParam != null)
                                          {
                                            this.SetSliderValue(unitData68.RentalFavoritePoint, (int) unitRentalParam.PtMax);
                                            return;
                                          }
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                        UnitData unitData69;
                                        if ((unitData69 = this.GetUnitData()) != null)
                                        {
                                          UnitRentalParam unitRentalParam = UnitRentalParam.GetParam(unitData69.RentalIname);
                                          if (unitRentalParam != null)
                                          {
                                            this.SetTextValue(Math.Min(unitData69.RentalFavoritePoint * 100 / (int) unitRentalParam.PtMax, 100));
                                            return;
                                          }
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.QUEST_NAME:
                                        RentalQuestInfo dataOfClass190 = DataSource.FindDataOfClass<RentalQuestInfo>(((Component) this).gameObject, (RentalQuestInfo) null);
                                        UnitRentalParam dataOfClass191 = DataSource.FindDataOfClass<UnitRentalParam>(((Component) this).gameObject, (UnitRentalParam) null);
                                        if (dataOfClass190 != null && dataOfClass191 != null)
                                        {
                                          this.SetTextValue(string.Format(LocalizedText.Get("sys.UR_QUEST_UNLOCK_COND_MSG"), (object) (int) ((double) ((int) dataOfClass190.Point * 100) / (double) (int) dataOfClass191.PtMax)));
                                          return;
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.QUEST_STAMINA:
                                        UnitData unitData70;
                                        if ((unitData70 = this.GetUnitData()) != null)
                                        {
                                          UnitRentalParam data3 = UnitRentalParam.GetParam(unitData70.RentalIname);
                                          if (data3 != null && data3.UnitQuestInfo != null && data3.UnitQuestInfo.Count > 0)
                                          {
                                            UnitRentalProgressLine[] componentsInChildren = ((Component) this).gameObject.GetComponentsInChildren<UnitRentalProgressLine>(true);
                                            if (componentsInChildren == null || componentsInChildren.Length <= 0)
                                              return;
                                            Transform parent = ((Component) componentsInChildren[0]).transform.parent;
                                            List<RentalQuestInfo> unitQuestInfo = data3.UnitQuestInfo;
                                            foreach (Component component48 in componentsInChildren)
                                              component48.gameObject.SetActive(false);
                                            for (int index28 = 0; index28 < unitQuestInfo.Count; ++index28)
                                            {
                                              UnitRentalProgressLine rentalProgressLine = index28 >= componentsInChildren.Length ? UnityEngine.Object.Instantiate<UnitRentalProgressLine>(componentsInChildren[0], parent) : componentsInChildren[index28];
                                              DataSource.Bind<RentalQuestInfo>(((Component) rentalProgressLine).gameObject, unitQuestInfo[index28]);
                                              DataSource.Bind<UnitRentalParam>(((Component) rentalProgressLine).gameObject, data3);
                                              rentalProgressLine.UpdateValue();
                                              ((Component) rentalProgressLine).gameObject.SetActive(true);
                                            }
                                            return;
                                          }
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.QUEST_STATE:
                                        UnitPieceShopParam currentParam = UnitPieceShopParam.GetCurrentParam();
                                        if (currentParam == null)
                                          return;
                                        this.SetBuyPriceEventCoinTypeIcon(currentParam.CostIname);
                                        return;
                                      case GameParameter.ParameterTypes.QUEST_OBJECTIVE:
                                        Button component49 = ((Component) this).GetComponent<Button>();
                                        UnitData unitData71 = this.GetUnitData();
                                        bool dataOfClass192 = DataSource.FindDataOfClass<bool>(((Component) this).gameObject, false);
                                        PlayerPartyTypes dataOfClass193 = DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).gameObject, PlayerPartyTypes.Max);
                                        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component49) || unitData71 == null)
                                          return;
                                        bool flag31 = (unitData71.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && dataOfClass192 && dataOfClass193 != PlayerPartyTypes.Max;
                                        if (!GlobalVars.IsTutorialEnd)
                                          flag31 = false;
                                        if (UnitOverWriteUtility.IsSupportPartyType((eOverWritePartyType) GlobalVars.OverWritePartyType))
                                          flag31 = false;
                                        if (flag31)
                                          flag31 = unitData71.MainConceptCard != null && unitData71.MainConceptCard.LeaderSkillIsAvailable();
                                        ((Selectable) component49).interactable = flag31;
                                        return;
                                      case GameParameter.ParameterTypes.QUEST_BONUSOBJECTIVE:
                                        UnitData unitData72 = this.GetUnitData();
                                        if (unitData72 != null)
                                        {
                                          UnitData unit41 = (UnitData) null;
                                          if (!unitData72.IsNotFindUniqueID)
                                            unit41 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitData72.UniqueID);
                                          UnitData unitData73 = unit41 == null ? unitData72 : UnitOverWriteUtility.Apply(unit41, (eOverWritePartyType) GlobalVars.OverWritePartyType);
                                          this.SetImageIndex(unitData73 == null || !unitData73.IsEquipConceptLeaderSkill() ? 0 : 1);
                                          return;
                                        }
                                        this.ResetToDefault();
                                        return;
                                      case GameParameter.ParameterTypes.ITEM_ICON:
                                        int num46 = 0;
                                        if (MonoSingleton<GameManager>.Instance.AudienceMode)
                                        {
                                          JSON_MyPhotonPlayerParam player22 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType != 0 ? 1 : 0];
                                          if (player22 != null)
                                          {
                                            player22.SetupUnits();
                                            num46 = PartyUtility.CalcTotalCombatPower(((IEnumerable<JSON_MyPhotonPlayerParam.UnitDataElem>) player22.units).Select<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>) (unit_elem => unit_elem.unit)));
                                          }
                                        }
                                        else
                                        {
                                          MyPhoton instance98 = PunMonoSingleton<MyPhoton>.Instance;
                                          MyPhoton.MyPlayer player = instance98.GetMyPlayer();
                                          JSON_MyPhotonPlayerParam.UnitDataElem[] source = (JSON_MyPhotonPlayerParam.UnitDataElem[]) null;
                                          if (this.InstanceType == 0)
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam12 = JSON_MyPhotonPlayerParam.Parse(player.json);
                                            if (photonPlayerParam12.units != null && photonPlayerParam12.units.Length > 0)
                                              source = photonPlayerParam12.units;
                                          }
                                          else
                                          {
                                            JSON_MyPhotonPlayerParam photonPlayerParam13 = instance98.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != player.playerID));
                                            if (photonPlayerParam13.units != null && photonPlayerParam13.units.Length > 0)
                                              source = photonPlayerParam13.units;
                                          }
                                          if (source != null)
                                            num46 = PartyUtility.CalcTotalCombatPower(((IEnumerable<JSON_MyPhotonPlayerParam.UnitDataElem>) source).Select<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>((Func<JSON_MyPhotonPlayerParam.UnitDataElem, UnitData>) (unit_elem => unit_elem.unit)));
                                        }
                                        this.SetTextValue(num46);
                                        goto label_3654;
                                      default:
                                        switch (parameterType - 3400)
                                        {
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                            AdvanceBossInfo.AdvanceBossData dataOfClass194 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (dataOfClass194 != null && dataOfClass194.unit != null)
                                            {
                                              this.SetTextValue(dataOfClass194.unit.name);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                            AdvanceBossInfo.AdvanceBossData dataOfClass195 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (dataOfClass195 != null && dataOfClass195.unit != null)
                                            {
                                              GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(dataOfClass195.unit, (string) null);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                            Image component50 = ((Component) this).GetComponent<Image>();
                                            AdvanceBossInfo.AdvanceBossData dataOfClass196 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component50, (UnityEngine.Object) null) && dataOfClass196 != null && dataOfClass196.unit != null)
                                            {
                                              GameSettings instance99 = GameSettings.Instance;
                                              if (EElement.None <= dataOfClass196.unit.element && dataOfClass196.unit.element < (EElement) instance99.Elements_IconSmall.Length)
                                              {
                                                component50.sprite = instance99.Elements_IconSmall[(int) dataOfClass196.unit.element];
                                                return;
                                              }
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                            AdvanceBossInfo.AdvanceBossData dataOfClass197 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (dataOfClass197 != null)
                                            {
                                              this.SetTextValue(dataOfClass197.currentHP);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                            AdvanceBossInfo.AdvanceBossData dataOfClass198 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (dataOfClass198 != null)
                                            {
                                              this.SetTextValue(dataOfClass198.maxHP);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                            AdvanceBossInfo.AdvanceBossData dataOfClass199 = DataSource.FindDataOfClass<AdvanceBossInfo.AdvanceBossData>(((Component) this).gameObject, (AdvanceBossInfo.AdvanceBossData) null);
                                            if (dataOfClass199 != null)
                                            {
                                              this.SetSliderValue(dataOfClass199.currentHP, dataOfClass199.maxHP);
                                              this.SetTextValue(dataOfClass199.currentHP);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                            Button component51 = ((Component) this).GetComponent<Button>();
                                            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component51, (UnityEngine.Object) null))
                                              return;
                                            AdvanceEventModeInfoParam dataOfClass200 = DataSource.FindDataOfClass<AdvanceEventModeInfoParam>(((Component) this).gameObject, (AdvanceEventModeInfoParam) null);
                                            if (dataOfClass200 == null)
                                            {
                                              ((Selectable) component51).interactable = false;
                                              return;
                                            }
                                            ItemData itemDataByItemParam2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(dataOfClass200.BossChallengeItemParam);
                                            if (itemDataByItemParam2 == null || itemDataByItemParam2.Num < dataOfClass200.BossChallengeItemNum)
                                            {
                                              ((Selectable) component51).interactable = false;
                                              return;
                                            }
                                            ((Selectable) component51).interactable = true;
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                            bool flag32 = false;
                                            AdvanceEventParam advanceEventParam1 = (AdvanceEventParam) null;
                                            AdvanceEventManager instance100 = AdvanceEventManager.Instance;
                                            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance100))
                                              advanceEventParam1 = instance100.CurrentEventParam;
                                            if (advanceEventParam1 != null)
                                              flag32 = advanceEventParam1.GetBossQuest(QuestDifficulties.Extra) != null;
                                            if (this.Index != 0)
                                              flag32 = !flag32;
                                            ((Component) this).gameObject.SetActive(flag32);
                                            return;
                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                            bool flag33 = false;
                                            AdvanceEventParam advanceEventParam2 = (AdvanceEventParam) null;
                                            AdvanceEventManager instance101 = AdvanceEventManager.Instance;
                                            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance101))
                                              advanceEventParam2 = instance101.CurrentEventParam;
                                            if (advanceEventParam2 != null)
                                            {
                                              AdvanceEventModeInfoParam modeInfo = advanceEventParam2.GetModeInfo(instance101.BossDifficulty);
                                              if (modeInfo != null)
                                                flag33 = modeInfo.IsLapBoss;
                                            }
                                            if (this.Index != 0)
                                              flag33 = !flag33;
                                            ((Component) this).gameObject.SetActive(flag33);
                                            return;
                                          case GameParameter.ParameterTypes.QUEST_NAME:
                                            AdvanceBossInfo.LapBossBattleInfo lapBossBattleData2 = AdvanceBossInfo.LapBossBattleData;
                                            if (lapBossBattleData2 != null)
                                            {
                                              this.SetTextValue(lapBossBattleData2.Round);
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.SUPPORTER_ATK:
                                            AdvanceEventManager instance102 = AdvanceEventManager.Instance;
                                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance102, (UnityEngine.Object) null) && instance102.CurrentEventParam != null)
                                            {
                                              DateTime startDateTime = AdvanceEventParam.GetStartDateTime(instance102.CurrentEventParam.AreaId);
                                              DateTime endDateTime = AdvanceEventParam.GetEndDateTime(instance102.CurrentEventParam.AreaId);
                                              this.SetTextValue(string.Format("{0}～{1}", (object) startDateTime.ToString("MM/dd HH:mm"), (object) endDateTime.ToString("MM/dd HH:mm")));
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          case GameParameter.ParameterTypes.SUPPORTER_HP:
                                            AdvanceEventParam dataOfClass201 = DataSource.FindDataOfClass<AdvanceEventParam>(((Component) this).gameObject, (AdvanceEventParam) null);
                                            if (dataOfClass201 != null)
                                            {
                                              DateTime startDateTime = AdvanceEventParam.GetStartDateTime(dataOfClass201.AreaId);
                                              DateTime endDateTime = AdvanceEventParam.GetEndDateTime(dataOfClass201.AreaId);
                                              this.SetTextValue(string.Format("{0}～{1}", (object) startDateTime.ToString("MM/dd HH:mm"), (object) endDateTime.ToString("MM/dd HH:mm")));
                                              return;
                                            }
                                            this.ResetToDefault();
                                            return;
                                          default:
                                            switch (parameterType - 1200)
                                            {
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                QuestParam questParam25;
                                                if ((questParam25 = this.GetQuestParam()) != null)
                                                {
                                                  bool flag34 = questParam25.CheckDisableContinue();
                                                  if (this.Index == 0)
                                                  {
                                                    ((Component) this).gameObject.SetActive(flag34);
                                                    return;
                                                  }
                                                  ((Component) this).gameObject.SetActive(!flag34);
                                                  return;
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                QuestParam questParam26;
                                                if ((questParam26 = this.GetQuestParam()) != null)
                                                {
                                                  bool flag35 = questParam26.DamageUpprPl != 0 || questParam26.DamageUpprEn != 0;
                                                  if (this.Index == 0)
                                                  {
                                                    ((Component) this).gameObject.SetActive(flag35);
                                                    return;
                                                  }
                                                  ((Component) this).gameObject.SetActive(!flag35);
                                                  return;
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                QuestParam questParam27;
                                                if ((questParam27 = this.GetQuestParam()) == null)
                                                  return;
                                                string str31 = LocalizedText.Get("quest_info." + questParam27.iname);
                                                string str32 = !(str31 == questParam27.iname) ? LocalizedText.Get("sys.PARTYEDITOR_COND_DETAIL") + str31 : string.Empty;
                                                LocalizedText.UnloadTable("quest_info");
                                                if (!string.IsNullOrEmpty(str32))
                                                  str32 += "\n";
                                                List<string> addQuestInfo = questParam27.GetAddQuestInfo();
                                                if (addQuestInfo.Count != 0)
                                                {
                                                  string empty3 = string.Empty;
                                                  for (int index29 = 0; index29 < addQuestInfo.Count; ++index29)
                                                  {
                                                    if (index29 > 0)
                                                      empty3 += "\n";
                                                    empty3 += addQuestInfo[index29];
                                                  }
                                                  str32 += empty3;
                                                }
                                                this.SetTextValue(str32);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                QuestParam questParam28;
                                                if ((questParam28 = this.GetQuestParam()) != null)
                                                {
                                                  QuestCondParam questCondParam = (QuestCondParam) null;
                                                  if (questParam28.EntryCondition != null)
                                                    questCondParam = questParam28.EntryCondition;
                                                  else if (questParam28.EntryConditionCh != null)
                                                    questCondParam = questParam28.EntryConditionCh;
                                                  if (questCondParam != null && questCondParam.party_type == PartyCondType.Forced)
                                                  {
                                                    bool flag36 = this.GetUnitData() != null;
                                                    if (this.Index == 0)
                                                    {
                                                      ((Component) this).gameObject.SetActive(flag36);
                                                      return;
                                                    }
                                                    ((Component) this).gameObject.SetActive(!flag36);
                                                    return;
                                                  }
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                QuestParam questParam29;
                                                if ((questParam29 = this.GetQuestParam()) != null)
                                                {
                                                  QuestCondParam questCondParam = (QuestCondParam) null;
                                                  if (questParam29.EntryCondition != null)
                                                    questCondParam = questParam29.EntryCondition;
                                                  else if (questParam29.EntryConditionCh != null)
                                                    questCondParam = questParam29.EntryConditionCh;
                                                  if (questCondParam != null && questCondParam.party_type == PartyCondType.Limited)
                                                  {
                                                    ((Component) this).gameObject.SetActive(true);
                                                    return;
                                                  }
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                QuestParam questParam30;
                                                if ((questParam30 = this.GetQuestParam()) != null)
                                                {
                                                  if (questParam30.questParty != null)
                                                  {
                                                    if (!((IEnumerable<PartySlotTypeUnitPair>) questParam30.questParty.GetMainSubSlots()).Any<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (s => s.Type == PartySlotType.Free)))
                                                    {
                                                      ((Component) this).gameObject.SetActive(true);
                                                      return;
                                                    }
                                                  }
                                                  else
                                                  {
                                                    QuestCondParam questCondParam = (QuestCondParam) null;
                                                    if (questParam30.EntryCondition != null)
                                                      questCondParam = questParam30.EntryCondition;
                                                    else if (questParam30.EntryConditionCh != null)
                                                      questCondParam = questParam30.EntryConditionCh;
                                                    if (questCondParam != null && questCondParam.party_type == PartyCondType.Forced)
                                                    {
                                                      ((Component) this).gameObject.SetActive(true);
                                                      return;
                                                    }
                                                  }
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                QuestParam questParam31;
                                                if ((questParam31 = this.GetQuestParam()) == null)
                                                  return;
                                                Selectable component52 = ((Component) this).gameObject.GetComponent<Selectable>();
                                                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component52, (UnityEngine.Object) null))
                                                  return;
                                                bool flag37 = questParam31.state == QuestStates.Cleared;
                                                if (this.Index == 0)
                                                {
                                                  component52.interactable = flag37;
                                                  return;
                                                }
                                                component52.interactable = !flag37;
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                                QuestParam questParam32;
                                                if ((questParam32 = this.GetQuestParam()) != null)
                                                {
                                                  bool isUnitChange = questParam32.IsUnitChange;
                                                  if (this.Index == 0)
                                                  {
                                                    ((Component) this).gameObject.SetActive(isUnitChange);
                                                    return;
                                                  }
                                                  ((Component) this).gameObject.SetActive(!isUnitChange);
                                                  return;
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                                QuestParam questParam33;
                                                if ((questParam33 = this.GetQuestParam()) != null)
                                                {
                                                  bool flag38 = questParam33.IsOpenUnitHave();
                                                  if (this.Index == 0)
                                                  {
                                                    ((Component) this).gameObject.SetActive(!flag38);
                                                    return;
                                                  }
                                                  ((Component) this).gameObject.SetActive(flag38);
                                                  return;
                                                }
                                                ((Component) this).gameObject.SetActive(false);
                                                return;
                                              case GameParameter.ParameterTypes.QUEST_NAME:
                                                ((Component) this).gameObject.SetActive(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax > 0);
                                                return;
                                              case GameParameter.ParameterTypes.QUEST_STAMINA:
                                                this.SetTextValue(Mathf.Max(0, MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.RestChallengeCount).ToString());
                                                return;
                                              default:
                                                switch (parameterType - 1700)
                                                {
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                    QuestParam questParam34;
                                                    if ((questParam34 = this.GetQuestParam()) != null && questParam34.bonusObjective != null)
                                                    {
                                                      int compCount;
                                                      int maxCount;
                                                      this.GetQuestObjectiveCount(questParam34, out compCount, out maxCount);
                                                      if (compCount > 0)
                                                      {
                                                        if (compCount > 0 && compCount < maxCount)
                                                        {
                                                          this.SetImageIndex(0);
                                                          return;
                                                        }
                                                        this.SetImageIndex(1);
                                                        return;
                                                      }
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                    QuestParam questParam35;
                                                    if ((questParam35 = this.GetQuestParam()) != null && questParam35.bonusObjective != null)
                                                    {
                                                      int compCount;
                                                      int maxCount;
                                                      this.GetQuestObjectiveCount(questParam35, out compCount, out maxCount);
                                                      this.SetTextValue(compCount);
                                                      if (compCount >= maxCount)
                                                      {
                                                        ((Graphic) this.mText).color = new Color(1f, 1f, 0.0f);
                                                        return;
                                                      }
                                                      ((Graphic) this.mText).color = new Color(1f, 1f, 1f);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                    QuestParam questParam36;
                                                    if ((questParam36 = this.GetQuestParam()) != null && questParam36.bonusObjective != null)
                                                    {
                                                      int maxCount;
                                                      this.GetQuestObjectiveCount(questParam36, out int _, out maxCount);
                                                      this.SetTextValue(maxCount);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                    QuestParam questParamAuto4 = this.GetQuestParamAuto();
                                                    if (questParamAuto4 != null && 0 <= this.Index && questParamAuto4.bonusObjective != null && this.Index < questParamAuto4.bonusObjective.Length)
                                                    {
                                                      this.SetTextValue(questParamAuto4.bonusObjective[this.Index].itemNum);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                    QuestParam questParamAuto5 = this.GetQuestParamAuto();
                                                    if (questParamAuto5 != null && 0 <= this.Index && questParamAuto5.bonusObjective != null && this.Index < questParamAuto5.bonusObjective.Length)
                                                    {
                                                      if (questParamAuto5.bonusObjective[this.Index].IsProgressMission())
                                                      {
                                                        ((Component) this).gameObject.SetActive(true);
                                                        return;
                                                      }
                                                      ((Component) this).gameObject.SetActive(false);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                    QuestParam questParamAuto6 = this.GetQuestParamAuto();
                                                    if (questParamAuto6 != null && 0 <= this.Index && questParamAuto6.bonusObjective != null && this.Index < questParamAuto6.bonusObjective.Length)
                                                    {
                                                      if (questParamAuto6.bonusObjective[this.Index].IsProgressMission())
                                                      {
                                                        if (questParamAuto6.mission_values != null && this.Index < questParamAuto6.mission_values.Length)
                                                        {
                                                          int missions_val = Mathf.Max(questParamAuto6.GetMissionValue(this.Index), 0);
                                                          bool isAchievable = questParamAuto6.bonusObjective[this.Index].CheckHomeMissionValueAchievable(missions_val);
                                                          this.SetTextValue(GameUtility.ComposeQuestMissionProgressText(questParamAuto6.bonusObjective[this.Index], missions_val, isAchievable));
                                                          return;
                                                        }
                                                        this.SetTextValue("-");
                                                        return;
                                                      }
                                                      ((Component) this).gameObject.SetActive(false);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                    QuestParam questParamAuto7 = this.GetQuestParamAuto();
                                                    if (questParamAuto7 != null && 0 <= this.Index && questParamAuto7.bonusObjective != null && this.Index < questParamAuto7.bonusObjective.Length)
                                                    {
                                                      int result = 0;
                                                      if (questParamAuto7.bonusObjective[this.Index].IsProgressMission())
                                                      {
                                                        string questMissionTextId = GameUtility.GetQuestMissionTextID(questParamAuto7.bonusObjective[this.Index]);
                                                        if (int.TryParse(questParamAuto7.bonusObjective[this.Index].TypeParam, out result))
                                                        {
                                                          this.SetTextValue(LocalizedText.Get(questMissionTextId + "_PROGRESS_TARGET", (object) result));
                                                          return;
                                                        }
                                                        this.SetTextValue(LocalizedText.Get(questMissionTextId + "_PROGRESS_TARGET"));
                                                        return;
                                                      }
                                                      ((Component) this).gameObject.SetActive(false);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                                    QuestParam questParam37;
                                                    if ((questParam37 = this.GetQuestParam()) != null && questParam37.HasMission())
                                                    {
                                                      int index30 = !questParam37.IsMissionCompleteALL() || questParam37.state != QuestStates.Cleared ? 0 : 1;
                                                      this.SetAnimatorInt("state", index30);
                                                      this.SetImageIndex(index30);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                                    QuestParam questParam38;
                                                    if ((questParam38 = this.GetQuestParam()) != null && questParam38.bonusObjective != null)
                                                    {
                                                      int compCount;
                                                      int maxCount;
                                                      this.GetQuestObjectiveCount(questParam38, out compCount, out maxCount);
                                                      if (questParam38.IsMissionCompleteALL() && questParam38.state == QuestStates.Cleared)
                                                        ++compCount;
                                                      int num47 = maxCount + 1;
                                                      this.SetTextValue(compCount);
                                                      if (compCount < num47)
                                                        return;
                                                      ((Graphic) this.mText).color = new Color(1f, 1f, 0.0f);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.QUEST_NAME:
                                                    QuestParam questParam39;
                                                    if ((questParam39 = this.GetQuestParam()) != null && questParam39.bonusObjective != null)
                                                    {
                                                      int maxCount;
                                                      this.GetQuestObjectiveCount(questParam39, out int _, out maxCount);
                                                      this.SetTextValue(maxCount + 1);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  case GameParameter.ParameterTypes.QUEST_STAMINA:
                                                    QuestParam questParamAuto8 = this.GetQuestParamAuto();
                                                    if (questParamAuto8 != null && 0 <= this.Index && questParamAuto8.bonusObjective != null && this.Index < questParamAuto8.bonusObjective.Length)
                                                    {
                                                      if (questParamAuto8.bonusObjective[this.Index].IsProgressMission())
                                                      {
                                                        if (questParamAuto8.mission_values != null && this.Index < questParamAuto8.mission_values.Length)
                                                        {
                                                          int missions_val = Mathf.Max(questParamAuto8.GetMissionValue(this.Index), 0);
                                                          bool isAchievable = questParamAuto8.bonusObjective[this.Index].CheckHomeMissionValueAchievable(missions_val);
                                                          Color color2 = Color.white;
                                                          if (!questParamAuto8.IsMissionClear(this.Index))
                                                            color2 = !isAchievable ? Color.red : Color.green;
                                                          this.SetTextValue(GameUtility.ComposeQuestMissionProgressText(questParamAuto8.bonusObjective[this.Index], missions_val, isAchievable));
                                                          this.SetTextColor(color2);
                                                          return;
                                                        }
                                                        this.SetTextValue("-");
                                                        return;
                                                      }
                                                      ((Component) this).gameObject.SetActive(false);
                                                      return;
                                                    }
                                                    this.ResetToDefault();
                                                    return;
                                                  default:
                                                    switch (parameterType - 1100)
                                                    {
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                        ArtifactData artifactData1 = this.GetArtifactData();
                                                        if (artifactData1 != null)
                                                        {
                                                          ArtifactData.RarityUpResults rarityUpResults = artifactData1.CheckEnableRarityUp();
                                                          string key = (string) null;
                                                          if ((rarityUpResults & ArtifactData.RarityUpResults.RarityMaxed) != ArtifactData.RarityUpResults.Success)
                                                            key = "sys.ARTI_RARITYUP_MAX";
                                                          else if ((rarityUpResults & ArtifactData.RarityUpResults.NoLv) != ArtifactData.RarityUpResults.Success)
                                                            key = "sys.ARTI_RARITYUP_NOLV";
                                                          else if ((rarityUpResults & ArtifactData.RarityUpResults.NoGold) != ArtifactData.RarityUpResults.Success)
                                                            key = "sys.ARTI_RARITYUP_NOGOLD";
                                                          else if ((rarityUpResults & ArtifactData.RarityUpResults.NoKakera) != ArtifactData.RarityUpResults.Success)
                                                            key = "sys.ARTI_RARITYUP_NOMTRL";
                                                          if (!string.IsNullOrEmpty(key))
                                                          {
                                                            ((Component) this).gameObject.SetActive(true);
                                                            this.SetTextValue(LocalizedText.Get(key));
                                                            return;
                                                          }
                                                        }
                                                        ((Component) this).gameObject.SetActive(false);
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                        bool flag39 = false;
                                                        ArtifactData artifactData2 = this.GetArtifactData();
                                                        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
                                                        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) && artifactData2 != null)
                                                        {
                                                          ArtifactData artifactByUniqueId = instanceDirect.Player.FindArtifactByUniqueID((long) artifactData2.UniqueID);
                                                          if (artifactByUniqueId != null)
                                                            flag39 = artifactByUniqueId.IsFavorite;
                                                        }
                                                        if (this.Index == 1)
                                                          flag39 = !flag39;
                                                        ((Component) this).gameObject.SetActive(flag39);
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                        ArtifactData artifactData3 = this.GetArtifactData();
                                                        if (artifactData3 != null)
                                                        {
                                                          bool flag40 = artifactData3.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
                                                          if (flag40)
                                                            this.SetImageIndex((int) artifactData3.Rarity + 1);
                                                          ((Component) this).gameObject.SetActive(flag40);
                                                          return;
                                                        }
                                                        ((Component) this).gameObject.SetActive(false);
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                        switch ((GameParameter.ArtifactInstanceTypes) this.InstanceType)
                                                        {
                                                          case GameParameter.ArtifactInstanceTypes.Any:
                                                          case GameParameter.ArtifactInstanceTypes.QuestReward:
                                                            ArtifactParam artifactParam8 = this.GetArtifactParam();
                                                            if (artifactParam8 != null && this.LoadArtifactIcon(artifactParam8))
                                                              return;
                                                            this.ResetToDefault();
                                                            return;
                                                          case GameParameter.ArtifactInstanceTypes.Trophy:
                                                            ArtifactRewardData dataOfClass202 = DataSource.FindDataOfClass<ArtifactRewardData>(((Component) this).gameObject, (ArtifactRewardData) null);
                                                            if (dataOfClass202 == null)
                                                              return;
                                                            ArtifactParam artifactParam9 = dataOfClass202.ArtifactParam;
                                                            if (artifactParam9 != null && this.LoadArtifactIcon(artifactParam9))
                                                              return;
                                                            this.ResetToDefault();
                                                            return;
                                                          default:
                                                            return;
                                                        }
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                        switch ((GameParameter.ArtifactInstanceTypes) this.InstanceType)
                                                        {
                                                          case GameParameter.ArtifactInstanceTypes.Any:
                                                          case GameParameter.ArtifactInstanceTypes.QuestReward:
                                                            this.SetArtifactFrame(this.GetArtifactParam());
                                                            return;
                                                          case GameParameter.ArtifactInstanceTypes.Trophy:
                                                            ArtifactRewardData dataOfClass203 = DataSource.FindDataOfClass<ArtifactRewardData>(((Component) this).gameObject, (ArtifactRewardData) null);
                                                            if (dataOfClass203 == null)
                                                              return;
                                                            ArtifactParam artifactParam10 = dataOfClass203.ArtifactParam;
                                                            if (artifactParam10 == null)
                                                              return;
                                                            this.SetArtifactFrame(artifactParam10);
                                                            return;
                                                          default:
                                                            return;
                                                        }
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                        switch ((GameParameter.ArtifactInstanceTypes) this.InstanceType)
                                                        {
                                                          case GameParameter.ArtifactInstanceTypes.QuestReward:
                                                            QuestParam questParamAuto9 = this.GetQuestParamAuto();
                                                            if (questParamAuto9 != null && 0 <= this.Index && questParamAuto9.bonusObjective != null && this.Index < questParamAuto9.bonusObjective.Length)
                                                            {
                                                              this.SetTextValue(questParamAuto9.bonusObjective[this.Index].itemNum);
                                                              return;
                                                            }
                                                            break;
                                                          case GameParameter.ArtifactInstanceTypes.Trophy:
                                                            ArtifactRewardData dataOfClass204 = DataSource.FindDataOfClass<ArtifactRewardData>(((Component) this).gameObject, (ArtifactRewardData) null);
                                                            if (dataOfClass204 == null)
                                                              return;
                                                            if (dataOfClass204.ArtifactParam != null)
                                                            {
                                                              this.SetTextValue(dataOfClass204.Num);
                                                              return;
                                                            }
                                                            break;
                                                        }
                                                        this.ResetToDefault();
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                        RecommendedArtifactViewParam dataOfClass205 = DataSource.FindDataOfClass<RecommendedArtifactViewParam>(((Component) this).gameObject, (RecommendedArtifactViewParam) null);
                                                        if (dataOfClass205 == null)
                                                        {
                                                          ((Component) this).gameObject.SetActive(false);
                                                          return;
                                                        }
                                                        ((Component) this).gameObject.SetActive(dataOfClass205.IsRecommend);
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                                        ArtifactParam artifactParam11 = this.GetArtifactParam();
                                                        if (artifactParam11 == null)
                                                        {
                                                          ((Component) this).gameObject.SetActive(false);
                                                          return;
                                                        }
                                                        ((Component) this).gameObject.SetActive(artifactParam11.insp_lv_bonus > 0);
                                                        return;
                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                                        ArtifactParam artifactParam12 = this.GetArtifactParam();
                                                        if (artifactParam12 != null)
                                                        {
                                                          int inspLvBonus = artifactParam12.insp_lv_bonus;
                                                          if (inspLvBonus > 0)
                                                          {
                                                            this.SetTextValue(inspLvBonus);
                                                            return;
                                                          }
                                                        }
                                                        this.ResetToDefault();
                                                        return;
                                                      case GameParameter.ParameterTypes.QUEST_NAME:
                                                        ArtifactParam artifactParam13 = this.GetArtifactParam();
                                                        if (artifactParam13 != null)
                                                        {
                                                          this.SetTextValue(artifactParam13.Flavor);
                                                          return;
                                                        }
                                                        this.ResetToDefault();
                                                        return;
                                                      default:
                                                        switch (parameterType - 1400)
                                                        {
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                            BattleCore.OrderData orderData = this.GetOrderData();
                                                            if (orderData == null)
                                                              return;
                                                            Image component53 = ((Component) this).gameObject.GetComponent<Image>();
                                                            if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component53))
                                                              return;
                                                            ((Behaviour) component53).enabled = orderData.IsCastSkill;
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                            skillParam = (SkillParam) null;
                                                            SkillData skillData4;
                                                            skillParam = (skillData4 = this.GetSkillData()) == null ? this.GetSkillParam() : skillData4.SkillParam;
                                                            if (skillParam != null)
                                                            {
                                                              this.SetTextValue(skillParam.count);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                            skillParam = (SkillParam) null;
                                                            SkillData skillData5;
                                                            skillParam = (skillData5 = this.GetSkillData()) == null ? this.GetSkillParam() : skillData5.SkillParam;
                                                            int index31 = 0;
                                                            if (skillParam != null)
                                                              index31 = (int) skillParam.element_type;
                                                            if (index31 != 0)
                                                            {
                                                              this.SetImageIndex(index31);
                                                              ((Component) this).gameObject.SetActive(true);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            ((Component) this).gameObject.SetActive(false);
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                            skillParam = (SkillParam) null;
                                                            SkillData skillData6;
                                                            skillParam = (skillData6 = this.GetSkillData()) == null ? this.GetSkillParam() : skillData6.SkillParam;
                                                            int index32 = 0;
                                                            if (skillParam != null)
                                                              index32 = (int) skillParam.attack_detail;
                                                            if (index32 != 0)
                                                            {
                                                              this.SetImageIndex(index32);
                                                              ((Component) this).gameObject.SetActive(true);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            ((Component) this).gameObject.SetActive(false);
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                            skillParam = (SkillParam) null;
                                                            SkillData skillData7;
                                                            skillParam = (skillData7 = this.GetSkillData()) == null ? this.GetSkillParam() : skillData7.SkillParam;
                                                            int index33 = 0;
                                                            if (skillParam != null)
                                                              index33 = (int) skillParam.attack_type;
                                                            if (index33 != 0)
                                                            {
                                                              this.SetImageIndex(index33);
                                                              ((Component) this).gameObject.SetActive(true);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            ((Component) this).gameObject.SetActive(false);
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                            ((Component) this).gameObject.SetActive(WeatherData.CurrentWeatherData != null);
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                            WeatherParam weatherParam1 = this.GetWeatherParam();
                                                            if (weatherParam1 != null)
                                                            {
                                                              this.SetTextValue(weatherParam1.Name);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN:
                                                            WeatherParam weatherParam2 = this.GetWeatherParam();
                                                            if (weatherParam2 != null && !string.IsNullOrEmpty(weatherParam2.Icon))
                                                            {
                                                              GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.WeatherIcon(weatherParam2.Icon);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            return;
                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINATIME:
                                                            Unit unit42;
                                                            if ((unit42 = this.GetUnit()) != null)
                                                            {
                                                              int hp1 = (int) unit42.CurrentStatus.param.hp;
                                                              int hp2 = (int) unit42.MaximumStatus.param.hp;
                                                              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance) && SceneBattle.Instance.Battle != null)
                                                              {
                                                                int mp = 0;
                                                                SceneBattle.Instance.Battle.DtuGetHpMpRate(unit42, ref hp1, ref mp);
                                                              }
                                                              this.SetTextValue(hp1);
                                                              this.SetSliderValue(hp1, hp2);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            return;
                                                          case GameParameter.ParameterTypes.QUEST_NAME:
                                                            Unit unit43;
                                                            if ((unit43 = this.GetUnit()) != null)
                                                            {
                                                              int gems = unit43.Gems;
                                                              int mp = (int) unit43.MaximumStatus.param.mp;
                                                              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance) && SceneBattle.Instance.Battle != null)
                                                              {
                                                                int hp = 0;
                                                                SceneBattle.Instance.Battle.DtuGetHpMpRate(unit43, ref hp, ref gems);
                                                              }
                                                              this.SetTextValue(gems);
                                                              this.SetSliderValue(gems, mp);
                                                              return;
                                                            }
                                                            this.ResetToDefault();
                                                            return;
                                                          default:
                                                            switch (parameterType - 1800)
                                                            {
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                skillParam = (SkillParam) null;
                                                                SkillData skillData8;
                                                                skillParam = (skillData8 = this.GetSkillData()) == null ? this.GetSkillParam() : skillData8.SkillParam;
                                                                if (skillParam != null)
                                                                {
                                                                  this.SetTextValue(skillParam.MapEffectDesc);
                                                                  return;
                                                                }
                                                                this.ResetToDefault();
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                MapEffectParam mapEffectParam = this.GetMapEffectParam();
                                                                if (mapEffectParam != null)
                                                                {
                                                                  this.SetTextValue(mapEffectParam.Name);
                                                                  return;
                                                                }
                                                                this.ResetToDefault();
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                JobParam jobParam4 = this.GetJobParam();
                                                                if (jobParam4 != null)
                                                                {
                                                                  this.SetTextValue(jobParam4.DescCharacteristic);
                                                                  return;
                                                                }
                                                                this.ResetToDefault();
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                JobParam jobParam5 = this.GetJobParam();
                                                                if (jobParam5 != null)
                                                                {
                                                                  ((Component) this).gameObject.SetActive(!string.IsNullOrEmpty(jobParam5.DescCharacteristic));
                                                                  return;
                                                                }
                                                                ((Component) this).gameObject.SetActive(false);
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                JobParam jobParam6 = this.GetJobParam();
                                                                if (jobParam6 != null)
                                                                {
                                                                  this.SetTextValue(jobParam6.DescOther);
                                                                  return;
                                                                }
                                                                this.ResetToDefault();
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                                JobParam jobParam7 = this.GetJobParam();
                                                                if (jobParam7 != null)
                                                                {
                                                                  ((Component) this).gameObject.SetActive(!string.IsNullOrEmpty(jobParam7.DescOther));
                                                                  return;
                                                                }
                                                                ((Component) this).gameObject.SetActive(false);
                                                                return;
                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                                goto label_351;
                                                              default:
                                                                switch (parameterType - 2200)
                                                                {
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                    TobiraRecipeParam dataOfClass206 = DataSource.FindDataOfClass<TobiraRecipeParam>(((Component) this).gameObject, (TobiraRecipeParam) null);
                                                                    if (dataOfClass206 == null)
                                                                      return;
                                                                    this.SetTextValue(dataOfClass206.Cost);
                                                                    return;
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                    UnlockTobiraRecipe dataOfClass207 = DataSource.FindDataOfClass<UnlockTobiraRecipe>(((Component) this).gameObject, (UnlockTobiraRecipe) null);
                                                                    if (dataOfClass207 == null)
                                                                      return;
                                                                    this.SetTextValue(dataOfClass207.RequiredAmount);
                                                                    return;
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                    UnlockTobiraRecipe dataOfClass208 = DataSource.FindDataOfClass<UnlockTobiraRecipe>(((Component) this).gameObject, (UnlockTobiraRecipe) null);
                                                                    if (dataOfClass208 == null)
                                                                      return;
                                                                    this.SetTextValue(dataOfClass208.Amount);
                                                                    return;
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                    TobiraEnhanceRecipe dataOfClass209 = DataSource.FindDataOfClass<TobiraEnhanceRecipe>(((Component) this).gameObject, (TobiraEnhanceRecipe) null);
                                                                    if (dataOfClass209 == null)
                                                                      return;
                                                                    this.SetTextValue(dataOfClass209.RequiredAmount);
                                                                    if (dataOfClass209.Amount < dataOfClass209.RequiredAmount)
                                                                    {
                                                                      this.SetTextColor(Color.red);
                                                                      return;
                                                                    }
                                                                    this.SetTextColor(Color.white);
                                                                    return;
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                    TobiraEnhanceRecipe dataOfClass210 = DataSource.FindDataOfClass<TobiraEnhanceRecipe>(((Component) this).gameObject, (TobiraEnhanceRecipe) null);
                                                                    if (dataOfClass210 == null)
                                                                      return;
                                                                    this.SetTextValue(dataOfClass210.Amount);
                                                                    return;
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD:
                                                                    TobiraData dataOfClass211 = DataSource.FindDataOfClass<TobiraData>(((Component) this).gameObject, (TobiraData) null);
                                                                    if (dataOfClass211 != null)
                                                                    {
                                                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
                                                                      {
                                                                        bool flag41 = dataOfClass211.ViewLv >= this.Index + 1;
                                                                        this.SetImageIndex(!flag41 ? 0 : 1);
                                                                        if (this.ParameterType != GameParameter.ParameterTypes.UNIT_TOBIRA_LEVEL_ICON)
                                                                          return;
                                                                        ((Component) this).gameObject.SetActive(flag41);
                                                                        return;
                                                                      }
                                                                      this.SetTextValue(dataOfClass211.ViewLv);
                                                                      return;
                                                                    }
                                                                    this.ResetToDefault();
                                                                    return;
                                                                  default:
                                                                    switch (parameterType - 1500)
                                                                    {
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                        UnitData unitData74;
                                                                        if ((unitData74 = this.GetUnitData()) != null)
                                                                        {
                                                                          this.SetTextValue((int) unitData74.Status.param.mov);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                        UnitData unitData75;
                                                                        if ((unitData75 = this.GetUnitData()) != null)
                                                                        {
                                                                          this.SetTextValue((int) unitData75.Status.param.jmp);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                        UnitParam dataOfClass212 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                                                                        if (dataOfClass212 != null)
                                                                        {
                                                                          GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitIconSmall(dataOfClass212, (string) null);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                        UnitParam dataOfClass213 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                                                                        if (dataOfClass213 != null)
                                                                        {
                                                                          GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitIconMedium(dataOfClass213, (string) null);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                        UnitParam dataOfClass214 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                                                                        if (dataOfClass214 != null)
                                                                        {
                                                                          GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(dataOfClass214, (string) null);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                                        UnitParam dataOfClass215 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
                                                                        if (dataOfClass215 != null)
                                                                        {
                                                                          GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitEyeImage(dataOfClass215, (string) null);
                                                                          return;
                                                                        }
                                                                        this.ResetToDefault();
                                                                        return;
                                                                      default:
                                                                        switch (parameterType - 2500)
                                                                        {
                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                            bool flag42 = false;
                                                                            AbilityData abilityData4;
                                                                            if ((abilityData4 = this.GetAbilityData()) != null)
                                                                            {
                                                                              flag42 = abilityData4.IsDerivedAbility;
                                                                            }
                                                                            else
                                                                            {
                                                                              AbilityParam abilityParam7;
                                                                              if ((abilityParam7 = this.GetAbilityParam()) != null)
                                                                              {
                                                                                AbilityDeriveParam dataOfClass216 = DataSource.FindDataOfClass<AbilityDeriveParam>(((Component) this).gameObject, (AbilityDeriveParam) null);
                                                                                if (dataOfClass216 != null && dataOfClass216.m_DeriveParam == abilityParam7)
                                                                                  flag42 = true;
                                                                              }
                                                                            }
                                                                            ((Component) this).gameObject.SetActive(flag42);
                                                                            return;
                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                            Color color3;
                                                                            // ISSUE: explicit constructor call
                                                                            ((Color) ref color3).\u002Ector(1f, 1f, 0.0f, 1f);
                                                                            Color color4 = Color.white;
                                                                            AbilityData abilityData5;
                                                                            if ((abilityData5 = this.GetAbilityData()) != null)
                                                                            {
                                                                              if (abilityData5.IsDerivedAbility)
                                                                                color4 = color3;
                                                                            }
                                                                            else
                                                                            {
                                                                              AbilityParam abilityParam8;
                                                                              if ((abilityParam8 = this.GetAbilityParam()) != null)
                                                                              {
                                                                                AbilityDeriveParam dataOfClass217 = DataSource.FindDataOfClass<AbilityDeriveParam>(((Component) this).gameObject, (AbilityDeriveParam) null);
                                                                                if (dataOfClass217 != null && dataOfClass217.m_DeriveParam == abilityParam8)
                                                                                  color4 = color3;
                                                                              }
                                                                            }
                                                                            this.SetTextColor(color4);
                                                                            this.SetSyncColorOriginColor(color4);
                                                                            return;
                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                            string key1 = string.Empty;
                                                                            ImageSpriteSheet component54 = ((Component) this).GetComponent<ImageSpriteSheet>();
                                                                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component54, (UnityEngine.Object) null))
                                                                              key1 = component54.DefaultKey;
                                                                            AbilityData abilityData6;
                                                                            if ((abilityData6 = this.GetAbilityData()) != null)
                                                                            {
                                                                              key1 = AbilityParam.TypeDetailToSpriteSheetKey(abilityData6.Param.type_detail);
                                                                            }
                                                                            else
                                                                            {
                                                                              AbilityParam abilityParam9;
                                                                              if ((abilityParam9 = this.GetAbilityParam()) != null)
                                                                                key1 = AbilityParam.TypeDetailToSpriteSheetKey(abilityParam9.type_detail);
                                                                            }
                                                                            this.SetImageBySpriteSheet(key1);
                                                                            return;
                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                            AbilityData abilityData7;
                                                                            if ((abilityData7 = this.GetAbilityData()) != null)
                                                                            {
                                                                              this.SetTextValue(abilityData7.GetRankMaxCap());
                                                                              return;
                                                                            }
                                                                            AbilityParam abilityParam10;
                                                                            if ((abilityParam10 = this.GetAbilityParam()) != null)
                                                                            {
                                                                              this.SetTextValue(abilityParam10.GetRankCap());
                                                                              return;
                                                                            }
                                                                            this.ResetToDefault();
                                                                            return;
                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                            Color green = Color.green;
                                                                            Color yellow = Color.yellow;
                                                                            AbilityData abilityData8;
                                                                            if ((abilityData8 = this.GetAbilityData()) != null)
                                                                            {
                                                                              int rank2 = abilityData8.Rank;
                                                                              this.SetTextColor(green);
                                                                              this.SetTextValue(rank2);
                                                                              return;
                                                                            }
                                                                            this.SetTextColor(yellow);
                                                                            this.ResetToDefault();
                                                                            return;
                                                                          default:
                                                                            switch (parameterType - 3900)
                                                                            {
                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                FilterUtility.FilterBindData dataOfClass218 = DataSource.FindDataOfClass<FilterUtility.FilterBindData>(((Component) this).gameObject, (FilterUtility.FilterBindData) null);
                                                                                if (dataOfClass218 == null || ((Component) this).gameObject.transform.childCount <= 0)
                                                                                  return;
                                                                                GameObject gameObject4 = ((Component) ((Component) this).gameObject.transform.GetChild(0)).gameObject;
                                                                                int num48 = Math.Max(0, dataOfClass218.Rarity - ((Component) this).gameObject.transform.childCount + 1);
                                                                                for (int index34 = 0; index34 < num48; ++index34)
                                                                                  UnityEngine.Object.Instantiate<GameObject>(gameObject4).transform.SetParent(gameObject4.transform.parent, false);
                                                                                return;
                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                FilterUtility.FilterBindData dataOfClass219 = DataSource.FindDataOfClass<FilterUtility.FilterBindData>(((Component) this).gameObject, (FilterUtility.FilterBindData) null);
                                                                                UnityEngine.UI.Text componentInChildren3 = ((Component) this).gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
                                                                                if (dataOfClass219 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren3, (UnityEngine.Object) null))
                                                                                  return;
                                                                                componentInChildren3.text = dataOfClass219.Name;
                                                                                return;
                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                FilterUtility.FilterBindData dataOfClass220 = DataSource.FindDataOfClass<FilterUtility.FilterBindData>(((Component) this).gameObject, (FilterUtility.FilterBindData) null);
                                                                                ImageArray component55 = ((Component) this).gameObject.GetComponent<ImageArray>();
                                                                                if (dataOfClass220 != null && UnityEngine.Object.op_Implicit((UnityEngine.Object) component55))
                                                                                {
                                                                                  component55.ImageIndex = (int) dataOfClass220.EquipType;
                                                                                  return;
                                                                                }
                                                                                this.ResetToDefault();
                                                                                return;
                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                                ArtifactParam artifactParam14 = this.GetArtifactParam();
                                                                                if (artifactParam14 != null)
                                                                                {
                                                                                  this.SetTextValue(string.Format(LocalizedText.Get("sys.ARTI_TYPE"), (object) artifactParam14.tag));
                                                                                  return;
                                                                                }
                                                                                this.ResetToDefault();
                                                                                return;
                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                                FilterUtility.FilterBindData dataOfClass221 = DataSource.FindDataOfClass<FilterUtility.FilterBindData>(((Component) this).gameObject, (FilterUtility.FilterBindData) null);
                                                                                if (dataOfClass221 != null)
                                                                                {
                                                                                  this.SetImageBySpriteSheet(dataOfClass221.RuneSetEffectIconIndex.ToString());
                                                                                  return;
                                                                                }
                                                                                this.ResetToDefault();
                                                                                return;
                                                                              default:
                                                                                switch (parameterType - 4000)
                                                                                {
                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                    JukeBoxParam dataOfClass222 = DataSource.FindDataOfClass<JukeBoxParam>(((Component) this).gameObject, (JukeBoxParam) null);
                                                                                    if (dataOfClass222 != null)
                                                                                    {
                                                                                      this.SetTextValue(dataOfClass222.Title);
                                                                                      return;
                                                                                    }
                                                                                    this.ResetToDefault();
                                                                                    return;
                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                    JukeBoxParam dataOfClass223 = DataSource.FindDataOfClass<JukeBoxParam>(((Component) this).gameObject, (JukeBoxParam) null);
                                                                                    if (dataOfClass223 != null)
                                                                                    {
                                                                                      this.SetTextValue(dataOfClass223.TitleEn);
                                                                                      return;
                                                                                    }
                                                                                    this.ResetToDefault();
                                                                                    return;
                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                                    JukeBoxParam dataOfClass224 = DataSource.FindDataOfClass<JukeBoxParam>(((Component) this).gameObject, (JukeBoxParam) null);
                                                                                    if (dataOfClass224 != null)
                                                                                    {
                                                                                      if (!string.IsNullOrEmpty(dataOfClass224.Composer))
                                                                                      {
                                                                                        this.SetTextValue(dataOfClass224.Composer);
                                                                                        return;
                                                                                      }
                                                                                      this.SetTextValue(string.Format(LocalizedText.Get("sys.JUKE_BOX_COMPOSER_NONE")));
                                                                                      return;
                                                                                    }
                                                                                    this.ResetToDefault();
                                                                                    return;
                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXP:
                                                                                    JukeBoxParam dataOfClass225 = DataSource.FindDataOfClass<JukeBoxParam>(((Component) this).gameObject, (JukeBoxParam) null);
                                                                                    if (dataOfClass225 != null)
                                                                                    {
                                                                                      this.SetTextValue(dataOfClass225.Situation);
                                                                                      return;
                                                                                    }
                                                                                    this.ResetToDefault();
                                                                                    return;
                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_EXPNEXT:
                                                                                    JukeBoxSectionParam dataOfClass226 = DataSource.FindDataOfClass<JukeBoxSectionParam>(((Component) this).gameObject, (JukeBoxSectionParam) null);
                                                                                    if (dataOfClass226 != null)
                                                                                    {
                                                                                      this.SetTextValue(dataOfClass226.Title);
                                                                                      return;
                                                                                    }
                                                                                    this.ResetToDefault();
                                                                                    return;
                                                                                  default:
                                                                                    switch (parameterType - 2300)
                                                                                    {
                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                        ItemParam itemParam24 = this.GetItemParam();
                                                                                        Image component56 = ((Component) this).GetComponent<Image>();
                                                                                        if (itemParam24 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) component56, (UnityEngine.Object) null))
                                                                                        {
                                                                                          GameSettings instance103 = GameSettings.Instance;
                                                                                          int rare = itemParam24.rare;
                                                                                          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance103, (UnityEngine.Object) null) && rare >= 0 && rare < instance103.ArtifactIcon_Rarity.Length)
                                                                                          {
                                                                                            component56.sprite = instance103.ArtifactIcon_Rarity[rare];
                                                                                            return;
                                                                                          }
                                                                                        }
                                                                                        this.ResetToDefault();
                                                                                        return;
                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                        ItemParam itemParam25;
                                                                                        if ((itemParam25 = this.GetItemParam()) != null)
                                                                                        {
                                                                                          if (!string.IsNullOrEmpty(itemParam25.Flavor))
                                                                                          {
                                                                                            this.SetTextValue(itemParam25.Flavor);
                                                                                            return;
                                                                                          }
                                                                                          this.SetTextValue(itemParam25.Expr);
                                                                                          return;
                                                                                        }
                                                                                        this.ResetToDefault();
                                                                                        return;
                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                        this.SetTextValue(DataSource.FindDataOfClass<int>(((Component) this).gameObject, 0));
                                                                                        return;
                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                                        ItemData dataOfClass227 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
                                                                                        if (dataOfClass227 != null && dataOfClass227.Param.IsLimited)
                                                                                        {
                                                                                          DateTime endAt = dataOfClass227.Param.end_at;
                                                                                          string empty4 = string.Empty;
                                                                                          if (this.InstanceType == 1)
                                                                                            empty4 = LocalizedText.Get("sys.TEXT_DISCOUNT_GACHA_PERIOD", (object) endAt.Year, (object) endAt.Month, (object) endAt.Day, (object) endAt.Hour, (object) endAt.Minute);
                                                                                          else if (this.InstanceType == 2)
                                                                                            empty4 = LocalizedText.Get("sys.TEXT_ITEM_DETAIL_PERIOD_FORMAT", (object) endAt.Year, (object) endAt.Month, (object) endAt.Day, (object) endAt.Hour, (object) endAt.Minute);
                                                                                          this.SetTextValue(empty4);
                                                                                          return;
                                                                                        }
                                                                                        this.ResetToDefault();
                                                                                        return;
                                                                                      default:
                                                                                        switch (parameterType - 3700)
                                                                                        {
                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                            LoginBonusMonthParam dataOfClass228 = DataSource.FindDataOfClass<LoginBonusMonthParam>(((Component) this).gameObject, (LoginBonusMonthParam) null);
                                                                                            if (dataOfClass228 != null)
                                                                                            {
                                                                                              ((Component) this).gameObject.SetActive(dataOfClass228.State == LoginBonusMonthState.NotReceived);
                                                                                              return;
                                                                                            }
                                                                                            this.ResetToDefault();
                                                                                            return;
                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                            LoginBonusMonthParam dataOfClass229 = DataSource.FindDataOfClass<LoginBonusMonthParam>(((Component) this).gameObject, (LoginBonusMonthParam) null);
                                                                                            if (dataOfClass229 != null)
                                                                                            {
                                                                                              this.SetTextValue(LocalizedText.Get("sys.LOGINBONUS_MONTH_DAYLY", (object) dataOfClass229.Day));
                                                                                              return;
                                                                                            }
                                                                                            this.ResetToDefault();
                                                                                            return;
                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                            TrophyParam dataOfClass230 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
                                                                                            if (dataOfClass230 != null)
                                                                                            {
                                                                                              this.SetTextValue(dataOfClass230.Objectives[0].ival);
                                                                                              return;
                                                                                            }
                                                                                            this.ResetToDefault();
                                                                                            return;
                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINAMAX:
                                                                                            bool flag43 = false;
                                                                                            Json_LoginBonusTable loginBonus30days = MonoSingleton<GameManager>.Instance.Player.LoginBonus30days;
                                                                                            if (loginBonus30days != null)
                                                                                              flag43 = loginBonus30days.IsCanRecover;
                                                                                            TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.MasterParam.GetTrophiesOfType(TrophyConditionTypes.logincount);
                                                                                            if (trophiesOfType != null)
                                                                                            {
                                                                                              for (int index35 = 0; index35 < trophiesOfType.Length; ++index35)
                                                                                              {
                                                                                                TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.TrophyData.GetTrophyCounter(trophiesOfType[index35].Param);
                                                                                                if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
                                                                                                {
                                                                                                  flag43 = true;
                                                                                                  break;
                                                                                                }
                                                                                              }
                                                                                            }
                                                                                            ((Component) this).gameObject.SetActive(flag43);
                                                                                            return;
                                                                                          default:
                                                                                            switch (parameterType - 1300)
                                                                                            {
                                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                                TrickParam trickParam1 = this.GetTrickParam();
                                                                                                if (trickParam1 != null)
                                                                                                {
                                                                                                  this.SetTextValue(trickParam1.Name);
                                                                                                  return;
                                                                                                }
                                                                                                this.ResetToDefault();
                                                                                                return;
                                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                                TrickParam trickParam2 = this.GetTrickParam();
                                                                                                if (trickParam2 != null)
                                                                                                {
                                                                                                  this.SetTextValue(trickParam2.Expr);
                                                                                                  return;
                                                                                                }
                                                                                                this.ResetToDefault();
                                                                                                return;
                                                                                              case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                                TrickParam trickParam3 = this.GetTrickParam();
                                                                                                if (trickParam3 != null)
                                                                                                {
                                                                                                  GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.TrickIconUI(trickParam3.MarkerName);
                                                                                                  return;
                                                                                                }
                                                                                                this.ResetToDefault();
                                                                                                return;
                                                                                              default:
                                                                                                switch (parameterType - 2000)
                                                                                                {
                                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                                    Image component57 = ((Component) this).GetComponent<Image>();
                                                                                                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) component57, (UnityEngine.Object) null))
                                                                                                    {
                                                                                                      ((Component) this).gameObject.SetActive(false);
                                                                                                      return;
                                                                                                    }
                                                                                                    ChallengeCategoryParam dataOfClass231 = DataSource.FindDataOfClass<ChallengeCategoryParam>(((Component) this).gameObject, (ChallengeCategoryParam) null);
                                                                                                    if (dataOfClass231 != null && !string.IsNullOrEmpty(dataOfClass231.iname))
                                                                                                    {
                                                                                                      SpriteSheet spriteSheet24 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
                                                                                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet24, (UnityEngine.Object) null))
                                                                                                      {
                                                                                                        component57.sprite = spriteSheet24.GetSprite("help/" + dataOfClass231.iname);
                                                                                                        return;
                                                                                                      }
                                                                                                    }
                                                                                                    component57.sprite = (Sprite) null;
                                                                                                    return;
                                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                                    Image component58 = ((Component) this).GetComponent<Image>();
                                                                                                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) component58, (UnityEngine.Object) null))
                                                                                                    {
                                                                                                      ((Component) this).gameObject.SetActive(false);
                                                                                                      return;
                                                                                                    }
                                                                                                    ChallengeCategoryParam dataOfClass232 = DataSource.FindDataOfClass<ChallengeCategoryParam>(((Component) this).gameObject, (ChallengeCategoryParam) null);
                                                                                                    if (dataOfClass232 != null && !string.IsNullOrEmpty(dataOfClass232.iname))
                                                                                                    {
                                                                                                      SpriteSheet spriteSheet25 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
                                                                                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet25, (UnityEngine.Object) null))
                                                                                                      {
                                                                                                        component58.sprite = spriteSheet25.GetSprite("button/" + dataOfClass232.iname);
                                                                                                        return;
                                                                                                      }
                                                                                                    }
                                                                                                    component58.sprite = (Sprite) null;
                                                                                                    return;
                                                                                                  case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                                    Image component59 = ((Component) this).GetComponent<Image>();
                                                                                                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) component59, (UnityEngine.Object) null))
                                                                                                    {
                                                                                                      ((Component) this).gameObject.SetActive(false);
                                                                                                      return;
                                                                                                    }
                                                                                                    TrophyParam dataOfClass233 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
                                                                                                    if (dataOfClass233 != null && !string.IsNullOrEmpty(dataOfClass233.iname))
                                                                                                    {
                                                                                                      SpriteSheet spriteSheet26 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
                                                                                                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet26, (UnityEngine.Object) null))
                                                                                                      {
                                                                                                        component59.sprite = spriteSheet26.GetSprite("reward/" + dataOfClass233.iname);
                                                                                                        return;
                                                                                                      }
                                                                                                    }
                                                                                                    component59.sprite = (Sprite) null;
                                                                                                    return;
                                                                                                  default:
                                                                                                    switch (parameterType - 2800)
                                                                                                    {
                                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                                        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
                                                                                                          return;
                                                                                                        this.mImageArray.sprite = (Sprite) null;
                                                                                                        ConceptCardData dataOfClass234 = DataSource.FindDataOfClass<ConceptCardData>(((Component) this).gameObject, (ConceptCardData) null);
                                                                                                        if (dataOfClass234 == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) || (int) dataOfClass234.AwakeCount <= 0)
                                                                                                          return;
                                                                                                        int index36 = (int) dataOfClass234.AwakeCount - 1;
                                                                                                        if ((int) dataOfClass234.Lv >= (int) dataOfClass234.LvCap)
                                                                                                          index36 = this.mImageArray.Images.Length - 1;
                                                                                                        this.SetImageIndex(index36);
                                                                                                        ((Component) this).gameObject.SetActive(true);
                                                                                                        return;
                                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                                        ConceptCardData dataOfClass235 = DataSource.FindDataOfClass<ConceptCardData>(((Component) this).gameObject, (ConceptCardData) null);
                                                                                                        int num49 = 0;
                                                                                                        IEnumerator enumerator = ((Component) this).gameObject.transform.GetEnumerator();
                                                                                                        try
                                                                                                        {
                                                                                                          while (enumerator.MoveNext())
                                                                                                          {
                                                                                                            Transform current = (Transform) enumerator.Current;
                                                                                                            foreach (Component component60 in current)
                                                                                                              component60.gameObject.SetActive(false);
                                                                                                            string str33 = "off";
                                                                                                            if (num49 < (int) dataOfClass235.AwakeCount)
                                                                                                              str33 = "on";
                                                                                                            Transform transform4 = current.Find(str33);
                                                                                                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform4, (UnityEngine.Object) null))
                                                                                                            {
                                                                                                              ((Component) transform4).gameObject.SetActive(true);
                                                                                                              ++num49;
                                                                                                            }
                                                                                                          }
                                                                                                          return;
                                                                                                        }
                                                                                                        finally
                                                                                                        {
                                                                                                          if (enumerator is IDisposable disposable)
                                                                                                            disposable.Dispose();
                                                                                                        }
                                                                                                      case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                                        ConceptCardParam dataOfClass236 = DataSource.FindDataOfClass<ConceptCardParam>(((Component) this).gameObject, (ConceptCardParam) null);
                                                                                                        if (dataOfClass236 == null)
                                                                                                        {
                                                                                                          ConceptCardData dataOfClass237 = DataSource.FindDataOfClass<ConceptCardData>(((Component) this).gameObject, (ConceptCardData) null);
                                                                                                          if (dataOfClass237 != null)
                                                                                                            dataOfClass236 = dataOfClass237.Param;
                                                                                                        }
                                                                                                        ImageSpriteSheet[] componentsInChildren1 = ((Component) this).gameObject.GetComponentsInChildren<ImageSpriteSheet>(true);
                                                                                                        if (componentsInChildren1 == null || componentsInChildren1.Length <= 0)
                                                                                                          return;
                                                                                                        foreach (Component component61 in componentsInChildren1)
                                                                                                          component61.gameObject.SetActive(false);
                                                                                                        if (dataOfClass236 == null || dataOfClass236.concept_card_groups == null)
                                                                                                          return;
                                                                                                        ImageSpriteSheet imageSpriteSheet1 = componentsInChildren1[0];
                                                                                                        if (UnityEngine.Object.op_Equality((UnityEngine.Object) imageSpriteSheet1, (UnityEngine.Object) null))
                                                                                                          return;
                                                                                                        string[] conceptCardGroups = dataOfClass236.concept_card_groups;
                                                                                                        for (int index37 = 0; index37 < conceptCardGroups.Length; ++index37)
                                                                                                        {
                                                                                                          ImageSpriteSheet imageSpriteSheet2 = index37 >= componentsInChildren1.Length ? UnityEngine.Object.Instantiate<ImageSpriteSheet>(imageSpriteSheet1, ((Component) imageSpriteSheet1).transform.parent) : componentsInChildren1[index37];
                                                                                                          ((Component) imageSpriteSheet2).gameObject.SetActive(true);
                                                                                                          imageSpriteSheet2.ForceLoad();
                                                                                                          imageSpriteSheet2.SetSprite(conceptCardGroups[index37]);
                                                                                                        }
                                                                                                        return;
                                                                                                      default:
                                                                                                        switch (parameterType - 3439)
                                                                                                        {
                                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME:
                                                                                                            bool flag44 = AdvanceEventParam.IsWithinPeriod();
                                                                                                            if (this.Index != 0)
                                                                                                              flag44 = !flag44;
                                                                                                            ((Component) this).gameObject.SetActive(flag44);
                                                                                                            return;
                                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_LEVEL:
                                                                                                            bool flag45 = false;
                                                                                                            AdvanceEventManager instance104 = AdvanceEventManager.Instance;
                                                                                                            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance104, (UnityEngine.Object) null) && instance104.CurrentEventParam != null)
                                                                                                            {
                                                                                                              flag45 = AdvanceEventParam.IsWithinPeriod(instance104.CurrentEventParam.AreaId);
                                                                                                              if (this.Index != 0)
                                                                                                                flag45 = !flag45;
                                                                                                            }
                                                                                                            ((Component) this).gameObject.SetActive(flag45);
                                                                                                            return;
                                                                                                          case GameParameter.ParameterTypes.GLOBAL_PLAYER_STAMINA:
                                                                                                            bool flag46 = false;
                                                                                                            AdvanceEventParam dataOfClass238 = DataSource.FindDataOfClass<AdvanceEventParam>(((Component) this).gameObject, (AdvanceEventParam) null);
                                                                                                            if (dataOfClass238 != null)
                                                                                                            {
                                                                                                              flag46 = AdvanceEventParam.IsWithinPeriod(dataOfClass238.AreaId);
                                                                                                              if (this.Index != 0)
                                                                                                                flag46 = !flag46;
                                                                                                            }
                                                                                                            ((Component) this).gameObject.SetActive(flag46);
                                                                                                            return;
                                                                                                          default:
                                                                                                            if (parameterType != GameParameter.ParameterTypes.FIRST_FRIEND_MAX)
                                                                                                            {
                                                                                                              if (parameterType != GameParameter.ParameterTypes.FIRST_FRIEND_COUNT)
                                                                                                              {
                                                                                                                if (parameterType != GameParameter.ParameterTypes.GAMEOBJECT_ACTIVE)
                                                                                                                {
                                                                                                                  if (parameterType != GameParameter.ParameterTypes.GAMEOBJECT_INACTIVE)
                                                                                                                  {
                                                                                                                    if (parameterType != GameParameter.ParameterTypes.TROPHY_STAR_COUNT)
                                                                                                                    {
                                                                                                                      if (parameterType != GameParameter.ParameterTypes.TROPHY_STAR_MISSION_IS_CHECK_ZERO_STAR_NUM)
                                                                                                                      {
                                                                                                                        if (parameterType != GameParameter.ParameterTypes.SKILL_DERIVED_TEXTCOLOR)
                                                                                                                        {
                                                                                                                          if (parameterType != GameParameter.ParameterTypes.ARTIFACT_SETEFFECT_TRIGGER_ICON)
                                                                                                                          {
                                                                                                                            if (parameterType == GameParameter.ParameterTypes.EXTRA_KAKERA_DIFFICULTY_SYMBOL)
                                                                                                                            {
                                                                                                                              QuestBookmarkWindow.ItemAndQuests dataOfClass239 = DataSource.FindDataOfClass<QuestBookmarkWindow.ItemAndQuests>(((Component) this).gameObject, (QuestBookmarkWindow.ItemAndQuests) null);
                                                                                                                              if (dataOfClass239 != null && dataOfClass239.quests != null)
                                                                                                                              {
                                                                                                                                ((Component) this).gameObject.SetActive(dataOfClass239.quests.FindIndex((Predicate<QuestParam>) (quest => quest.difficulty == QuestDifficulties.Extra)) >= 0);
                                                                                                                                return;
                                                                                                                              }
                                                                                                                              ((Component) this).gameObject.SetActive(false);
                                                                                                                              return;
                                                                                                                            }
                                                                                                                            goto label_3654;
                                                                                                                          }
                                                                                                                          else
                                                                                                                          {
                                                                                                                            SkillAbilityDeriveParam dataOfClass240 = DataSource.FindDataOfClass<SkillAbilityDeriveParam>(((Component) this).gameObject, (SkillAbilityDeriveParam) null);
                                                                                                                            if (dataOfClass240 == null)
                                                                                                                              return;
                                                                                                                            string triggerArtifactIname = dataOfClass240.GetTriggerArtifactIname(this.Index);
                                                                                                                            if (string.IsNullOrEmpty(triggerArtifactIname))
                                                                                                                              return;
                                                                                                                            this.LoadArtifactIcon(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(triggerArtifactIname));
                                                                                                                            return;
                                                                                                                          }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                          Color color5;
                                                                                                                          // ISSUE: explicit constructor call
                                                                                                                          ((Color) ref color5).\u002Ector(1f, 1f, 0.0f, 1f);
                                                                                                                          Color color6 = Color.white;
                                                                                                                          SkillData skillData9;
                                                                                                                          if ((skillData9 = this.GetSkillData()) != null)
                                                                                                                          {
                                                                                                                            if (skillData9.IsDerivedSkill)
                                                                                                                              color6 = color5;
                                                                                                                          }
                                                                                                                          else if ((skillParam = this.GetSkillParam()) != null)
                                                                                                                          {
                                                                                                                            SkillDeriveParam dataOfClass241 = DataSource.FindDataOfClass<SkillDeriveParam>(((Component) this).gameObject, (SkillDeriveParam) null);
                                                                                                                            if (dataOfClass241 != null && dataOfClass241.m_DeriveParam == skillParam)
                                                                                                                              color6 = color5;
                                                                                                                          }
                                                                                                                          this.SetTextColor(color6);
                                                                                                                          this.SetSyncColorOriginColor(color6);
                                                                                                                          return;
                                                                                                                        }
                                                                                                                      }
                                                                                                                      else
                                                                                                                      {
                                                                                                                        bool flag47 = false;
                                                                                                                        TrophyParam dataOfClass242 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
                                                                                                                        if (dataOfClass242 != null)
                                                                                                                        {
                                                                                                                          flag47 = dataOfClass242.StarNum == 0;
                                                                                                                          if (this.Index != 0)
                                                                                                                            flag47 = !flag47;
                                                                                                                        }
                                                                                                                        ((Component) this).gameObject.SetActive(flag47);
                                                                                                                        return;
                                                                                                                      }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                      TrophyParam dataOfClass243 = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
                                                                                                                      if (dataOfClass243 != null)
                                                                                                                      {
                                                                                                                        this.SetTextValue(dataOfClass243.StarNum);
                                                                                                                        return;
                                                                                                                      }
                                                                                                                      this.ResetToDefault();
                                                                                                                      return;
                                                                                                                    }
                                                                                                                  }
                                                                                                                  else
                                                                                                                  {
                                                                                                                    ((Component) this).gameObject.SetActive(!DataSource.FindDataOfClass<bool>(((Component) this).gameObject, false));
                                                                                                                    return;
                                                                                                                  }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                  ((Component) this).gameObject.SetActive(DataSource.FindDataOfClass<bool>(((Component) this).gameObject, true));
                                                                                                                  return;
                                                                                                                }
                                                                                                              }
                                                                                                              else
                                                                                                              {
                                                                                                                PlayerData player23 = MonoSingleton<GameManager>.Instance.Player;
                                                                                                                if (player23 == null)
                                                                                                                  return;
                                                                                                                this.SetTextValue(player23.FirstFriendCount);
                                                                                                                return;
                                                                                                              }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                              FixParam fixParam5 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
                                                                                                              if (fixParam5 == null)
                                                                                                                return;
                                                                                                              this.SetTextValue((int) fixParam5.FirstFriendMax);
                                                                                                              return;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
      }
    }

    private void GuildRaidUpdateValue()
    {
      switch (this.ParameterType)
      {
        case GameParameter.ParameterTypes.GUILDRAID_NAME:
          GuildRaidBossParam dataOfClass1 = DataSource.FindDataOfClass<GuildRaidBossParam>(((Component) this).gameObject, (GuildRaidBossParam) null);
          if (dataOfClass1 != null)
          {
            this.SetTextValue(dataOfClass1.Name);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HP_VALUE:
          GuildRaidBossInfo dataOfClass2 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass2 != null)
          {
            this.SetTextValue(dataOfClass2.CurrentHP);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HP_MAX:
          GuildRaidBossInfo dataOfClass3 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass3 != null)
          {
            this.SetTextValue(dataOfClass3.MaxHP);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HP_PERCENT:
          GuildRaidBossInfo dataOfClass4 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass4 != null)
          {
            this.SetTextValue((dataOfClass4.CurrentHP * 100 / dataOfClass4.MaxHP).ToString() + "%");
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HP_GAUGE:
          GuildRaidBossInfo dataOfClass5 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass5 != null)
          {
            this.SetSliderValue(dataOfClass5.CurrentHP, dataOfClass5.MaxHP);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_ICON_MEDIUM:
          UnitParam dataOfClass6 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
          if (dataOfClass6 != null)
          {
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitIconMedium(dataOfClass6, (string) null);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_IMAGE:
          UnitParam dataOfClass7 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
          if (dataOfClass7 != null)
          {
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(dataOfClass7, (string) null);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_CHALLENGE:
          Button component1 = ((Component) this).gameObject.GetComponent<Button>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
            break;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            ((Selectable) component1).interactable = GuildRaidManager.Instance.IsBossChallenge(GuildRaidManager.Instance.CurrentBossInfo);
            if (GuildRaidManager.Instance.CurrentBossInfo != null && GuildRaidManager.Instance.CurrentBossInfo.CurrentHP == 0)
              ((Selectable) component1).interactable = false;
            GuildRaidBossParam dataOfClass8 = DataSource.FindDataOfClass<GuildRaidBossParam>(((Component) this).gameObject, (GuildRaidBossParam) null);
            if (dataOfClass8 == null || GuildRaidManager.Instance.CurrentAreaNo >= dataOfClass8.AreaNo)
              break;
            ((Selectable) component1).interactable = false;
            break;
          }
          ((Selectable) component1).interactable = false;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HP_GAUGE_COLOR:
          GuildRaidBossInfo dataOfClass9 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass9 != null)
          {
            ImageArray component2 = ((Component) this).gameObject.GetComponent<ImageArray>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            {
              float num = (float) dataOfClass9.CurrentHP / (float) dataOfClass9.MaxHP;
              if ((double) num > 0.6)
              {
                component2.ImageIndex = 0;
                break;
              }
              if ((double) num > 0.3)
              {
                component2.ImageIndex = 1;
                break;
              }
              component2.ImageIndex = 2;
              break;
            }
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_ELEMENT:
          Image component3 = ((Component) this).GetComponent<Image>();
          GuildRaidBossParam dataOfClass10 = DataSource.FindDataOfClass<GuildRaidBossParam>(((Component) this).gameObject, (GuildRaidBossParam) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null) && dataOfClass10 != null)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(dataOfClass10.UnitIName);
            GameSettings instance = GameSettings.Instance;
            if (unitParam != null && EElement.None <= unitParam.element && unitParam.element < (EElement) instance.Elements_IconSmall.Length)
            {
              component3.sprite = instance.Elements_IconSmall[(int) unitParam.element];
              break;
            }
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_SCHEDULE_OPENNODRAW:
          switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
          {
            case GuildRaidManager.GuildRaidScheduleType.Open:
            case GuildRaidManager.GuildRaidScheduleType.OpenSchedule:
              if (!((Component) this).gameObject.GetActive())
                return;
              ((Component) this).gameObject.SetActive(false);
              return;
            default:
              return;
          }
        case GameParameter.ParameterTypes.GUILDRAID_SCHEDULE_CLOSENODRAW:
          switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
          {
            case GuildRaidManager.GuildRaidScheduleType.Close:
            case GuildRaidManager.GuildRaidScheduleType.CloseSchedule:
              if (!((Component) this).gameObject.GetActive())
                return;
              ((Component) this).gameObject.SetActive(false);
              return;
            default:
              return;
          }
        case GameParameter.ParameterTypes.GUILDRAID_SCHEDULE_CLOSEINTRACTALE:
          Button component4 = ((Component) this).gameObject.GetComponent<Button>();
          if (!((Component) this).gameObject.GetActive() || UnityEngine.Object.op_Equality((UnityEngine.Object) component4, (UnityEngine.Object) null))
            break;
          switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
          {
            case GuildRaidManager.GuildRaidScheduleType.Close:
            case GuildRaidManager.GuildRaidScheduleType.CloseSchedule:
              ((Selectable) component4).interactable = false;
              return;
            default:
              ((Selectable) component4).interactable = true;
              return;
          }
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_INFOSCHEDULECLOSE:
          GuildRaidBossInfo dataOfClass11 = DataSource.FindDataOfClass<GuildRaidBossInfo>(((Component) this).gameObject, (GuildRaidBossInfo) null);
          if (dataOfClass11 != null)
          {
            if (dataOfClass11.CurrentHP > 0 && MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.CloseSchedule)
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            GuildRaidManager instance = GuildRaidManager.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            if (!MonoSingleton<GameManager>.Instance.IsGuildRaidBattleSchedule(instance.PeriodId))
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            if (instance.ChallengedBp == instance.MaxBp && instance.BpHealType != GuildRaidManager.GuildRaidBpHealType.Eternal)
            {
              ((Component) this).gameObject.SetActive(true);
              break;
            }
          }
          ((Component) this).gameObject.SetActive(false);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_NOSHEDULE:
          switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
          {
            case GuildRaidManager.GuildRaidScheduleType.Open:
            case GuildRaidManager.GuildRaidScheduleType.Close:
              ((Component) this).gameObject.SetActive(false);
              return;
            default:
              ((Component) this).gameObject.SetActive(true);
              return;
          }
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_CLOTHSCHEDULE:
          if (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() != GuildRaidManager.GuildRaidScheduleType.CloseSchedule)
          {
            ((Component) this).gameObject.SetActive(false);
            break;
          }
          ((Component) this).gameObject.SetActive(true);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_REWARD:
          RaidBossInfo dataOfClass12 = DataSource.FindDataOfClass<RaidBossInfo>(((Component) this).gameObject, (RaidBossInfo) null);
          if (dataOfClass12 == null)
          {
            ((Component) this).gameObject.SetActive(false);
            break;
          }
          ((Component) this).gameObject.SetActive(dataOfClass12.IsReward);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_ENTRANCE:
          Button component5 = ((Component) this).gameObject.GetComponent<Button>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component5, (UnityEngine.Object) null))
            break;
          ((Selectable) component5).interactable = false;
          switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
          {
            case GuildRaidManager.GuildRaidScheduleType.Open:
            case GuildRaidManager.GuildRaidScheduleType.OpenSchedule:
              this.SetImageIndex(1);
              ((Selectable) component5).interactable = true;
              return;
            case GuildRaidManager.GuildRaidScheduleType.Close:
              this.SetImageIndex(0);
              return;
            case GuildRaidManager.GuildRaidScheduleType.CloseSchedule:
              this.SetImageIndex(2);
              return;
            default:
              return;
          }
        case GameParameter.ParameterTypes.GUILDRAID_CHALLENGE_NAME:
          GuildRaidChallengingPlayer dataOfClass13 = DataSource.FindDataOfClass<GuildRaidChallengingPlayer>(((Component) this).gameObject, (GuildRaidChallengingPlayer) null);
          if (dataOfClass13 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass13.Name);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_CHALLENGE_LEVEL:
          GuildRaidChallengingPlayer dataOfClass14 = DataSource.FindDataOfClass<GuildRaidChallengingPlayer>(((Component) this).gameObject, (GuildRaidChallengingPlayer) null);
          if (dataOfClass14 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass14.Lv);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_CHALLENGE_UNIT:
          GuildRaidChallengingPlayer dataOfClass15 = DataSource.FindDataOfClass<GuildRaidChallengingPlayer>(((Component) this).gameObject, (GuildRaidChallengingPlayer) null);
          if (dataOfClass15 != null && dataOfClass15.Unit != null)
          {
            UnitData unitDataForDisplay = UnitData.CreateUnitDataForDisplay(dataOfClass15.Unit);
            UnitIcon component6 = ((Component) this).gameObject.GetComponent<UnitIcon>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component6, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            DataSource.Bind<UnitData>(((Component) this).gameObject, unitDataForDisplay);
            component6.UpdateValue();
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_CHALLENGE_ROLEIMAGE:
          GuildRaidChallengingPlayer dataOfClass16 = DataSource.FindDataOfClass<GuildRaidChallengingPlayer>(((Component) this).gameObject, (GuildRaidChallengingPlayer) null);
          if (dataOfClass16 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
          {
            this.mImageArray.ImageIndex = Mathf.Max(0, (int) (dataOfClass16.Role - 1));
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BATTLELOG_MESSAGE:
          GuildRaidBattleLog dataOfClass17 = DataSource.FindDataOfClass<GuildRaidBattleLog>(((Component) this).gameObject, (GuildRaidBattleLog) null);
          if (dataOfClass17 == null)
            break;
          this.SetTextValue(dataOfClass17.Message);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BATTLELOG_POSTEDAT:
          GuildRaidBattleLog dataOfClass18 = DataSource.FindDataOfClass<GuildRaidBattleLog>(((Component) this).gameObject, (GuildRaidBattleLog) null);
          if (dataOfClass18 == null)
            break;
          TimeSpan timeSpan1 = TimeManager.ServerTime - dataOfClass18.PostedAt;
          if (timeSpan1.TotalDays >= 1.0)
          {
            this.SetTextValue(string.Format(LocalizedText.Get("sys.CHAT_POSTAT_DAY"), (object) (int) timeSpan1.TotalDays));
            break;
          }
          if (timeSpan1.TotalHours >= 1.0)
          {
            this.SetTextValue(string.Format(LocalizedText.Get("sys.CHAT_POSTAT_HOUR"), (object) (int) timeSpan1.TotalHours));
            break;
          }
          if (timeSpan1.TotalMinutes >= 1.0)
          {
            this.SetTextValue(string.Format(LocalizedText.Get("sys.CHAT_POSTAT_MINUTE"), (object) (int) timeSpan1.TotalMinutes));
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.CHAT_POSTAT_SECOND"), (object) (int) timeSpan1.TotalSeconds));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BP_CHALLENGE:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.CurrentBp.ToString());
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BP_MAX:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.MaxBp.ToString());
          break;
        case GameParameter.ParameterTypes.GUILDRAID_GIFT_NAME:
          GuildRaidMailListItem dataOfClass19 = DataSource.FindDataOfClass<GuildRaidMailListItem>(((Component) this).gameObject, (GuildRaidMailListItem) null);
          if (dataOfClass19 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          if (MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(dataOfClass19.BossId) == null)
          {
            this.ResetToDefault();
            break;
          }
          string empty1 = string.Empty;
          switch (dataOfClass19.RewardType)
          {
            case GuildRaidRewardType.Beat:
              empty1 = LocalizedText.Get("sys.GUILDRAID_GIFT_MESSAGE_BEAT");
              break;
            case GuildRaidRewardType.DamageRanking:
              empty1 = LocalizedText.Get("sys.GUILDRAID_GIFT_MESSAGE_RANKING_DAMAGE");
              break;
            case GuildRaidRewardType.HpRatio:
              empty1 = LocalizedText.Get("sys.GUILDRAID_GIFT_MESSAGE_RATIO");
              break;
            case GuildRaidRewardType.LastAttack:
              empty1 = LocalizedText.Get("sys.GUILDRAID_GIFT_MESSAGE_LAST_ATTACK");
              break;
          }
          this.SetTextValue(string.Format(empty1, (object) dataOfClass19.Round));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_LEVEL:
          GuildRaidRanking dataOfClass20 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass20 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass20.Guild.level);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_NAME:
          GuildRaidRanking dataOfClass21 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass21 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass21.Guild.name);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_MASTERNAME:
          GuildRaidRanking dataOfClass22 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass22 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass22.Guild.guild_master);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_MEMBER:
          GuildRaidRanking dataOfClass23 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass23 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass23.Guild.MemberCount);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_MEMBERMAX:
          GuildRaidRanking dataOfClass24 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass24 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass24.Guild.MemberMax);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_SCORE:
          GuildRaidRanking dataOfClass25 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass25 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass25.Score >= 0L)
          {
            this.SetTextValue(dataOfClass25.Score);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BOSSNO:
          GuildRaidBossParam dataOfClass26 = DataSource.FindDataOfClass<GuildRaidBossParam>(((Component) this).gameObject, (GuildRaidBossParam) null);
          if (dataOfClass26 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.GetBossNo(dataOfClass26));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_ICON_SMALL:
          UnitParam dataOfClass27 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
          if (dataOfClass27 != null)
          {
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitIconSmall(dataOfClass27, (string) null);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_LAP:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.CurrentRound.ToString());
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_RANK:
          GuildRaidRanking dataOfClass28 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass28 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass28.Rank > 0)
          {
            this.SetTextValue(dataOfClass28.Rank);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_BOSSIMAGE:
          GuildRaidRankingDamage dataOfClass29 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass29 != null)
          {
            GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(dataOfClass29.BossId);
            if (guildRaidBossParam == null)
            {
              this.ResetToDefault();
              break;
            }
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
            if (unitParam == null)
              break;
            string str = AssetPath.UnitIconSmall(unitParam, (string) null);
            if (string.IsNullOrEmpty(str))
              break;
            GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = str;
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_BEATTIME:
          GuildRaidRankingDamage dataOfClass30 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass30 != null && dataOfClass30.KnockDownAt.Ticks > 0L)
          {
            this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_DMG_RANKING_BEATTIME"), (object) dataOfClass30.KnockDownAt.Year, (object) dataOfClass30.KnockDownAt.Month, (object) dataOfClass30.KnockDownAt.Day, (object) dataOfClass30.KnockDownAt.Hour, (object) dataOfClass30.KnockDownAt.Minute));
            break;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_OPENSCHEJUDE:
          GuildRaidManager instance1 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam1 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance1.PeriodId);
          if (guildRaidPeriodParam1 == null || guildRaidPeriodParam1.BeginAt == new DateTime())
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_DATETIME"), (object) guildRaidPeriodParam1.BeginAt.Month, (object) guildRaidPeriodParam1.BeginAt.Day, (object) guildRaidPeriodParam1.BeginAt.Hour, (object) guildRaidPeriodParam1.BeginAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_CLOSESCHEJUDE:
          GuildRaidManager instance2 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam2 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance2.PeriodId);
          if (guildRaidPeriodParam2 == null || guildRaidPeriodParam2.EndAt == new DateTime())
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_DATETIME"), (object) guildRaidPeriodParam2.EndAt.Month, (object) guildRaidPeriodParam2.EndAt.Day, (object) guildRaidPeriodParam2.EndAt.Hour, (object) guildRaidPeriodParam2.EndAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REWARDOPENSCHEJUDE:
          GuildRaidManager instance3 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance3, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam3 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance3.PeriodId);
          if (guildRaidPeriodParam3 == null || guildRaidPeriodParam3.BeginAt == new DateTime())
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_DATETIME"), (object) guildRaidPeriodParam3.BeginAt.Month, (object) guildRaidPeriodParam3.BeginAt.Day, (object) guildRaidPeriodParam3.BeginAt.Hour, (object) guildRaidPeriodParam3.BeginAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REWARDCLOSESCHEJUDE:
          GuildRaidManager instance4 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance4, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam4 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance4.PeriodId);
          if (guildRaidPeriodParam4 == null || guildRaidPeriodParam4.RewardEndAt == new DateTime())
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_DATETIME"), (object) guildRaidPeriodParam4.RewardEndAt.Month, (object) guildRaidPeriodParam4.RewardEndAt.Day, (object) guildRaidPeriodParam4.RewardEndAt.Hour, (object) guildRaidPeriodParam4.RewardEndAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_CHALLENGETOP:
          Button component7 = ((Component) this).gameObject.GetComponent<Button>();
          if (!((Component) this).gameObject.GetActive() || UnityEngine.Object.op_Equality((UnityEngine.Object) component7, (UnityEngine.Object) null))
            break;
          GuildRaidManager.GuildRaidScheduleType periodScheduleType = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType();
          if (GuildRaidManager.Instance.IsFinishGuildRaid() || periodScheduleType == GuildRaidManager.GuildRaidScheduleType.Close || periodScheduleType == GuildRaidManager.GuildRaidScheduleType.CloseSchedule)
          {
            ((Selectable) component7).interactable = false;
            break;
          }
          ((Selectable) component7).interactable = true;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_MAILBOX_BADGE:
          GuildRaidManager instance5 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance5, (UnityEngine.Object) null) || ((Component) this).gameObject.activeSelf == instance5.IsReceiveMail)
            break;
          ((Component) this).gameObject.SetActive(instance5.IsReceiveMail);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_CHALLENGE_TIME:
          GuildRaidChallengingPlayer dataOfClass31 = DataSource.FindDataOfClass<GuildRaidChallengingPlayer>(((Component) this).gameObject, (GuildRaidChallengingPlayer) null);
          if (dataOfClass31 != null)
          {
            TimeSpan timeSpan2 = TimeManager.ServerTime - dataOfClass31.ChallengeTime;
            string empty2 = string.Empty;
            string str = timeSpan2.TotalDays < 1.0 ? (timeSpan2.TotalHours < 1.0 ? (timeSpan2.TotalMinutes < 1.0 ? string.Format(LocalizedText.Get("sys.CHAT_POSTAT_SECOND"), (object) (int) timeSpan2.TotalSeconds) : string.Format(LocalizedText.Get("sys.CHAT_POSTAT_MINUTE"), (object) (int) timeSpan2.TotalMinutes)) : string.Format(LocalizedText.Get("sys.CHAT_POSTAT_HOUR"), (object) (int) timeSpan2.TotalHours)) : string.Format(LocalizedText.Get("sys.CHAT_POSTAT_DAY"), (object) (int) timeSpan2.TotalDays);
            this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_BOSS_MEMBER_CHALLENGE_TIME"), (object) str));
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_PERIOD:
          GuildRaidSeasonResult raidSeasonResult1 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult1 == null)
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam5 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(raidSeasonResult1.Id);
          if (guildRaidPeriodParam5 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_SCHEDULE"), (object) guildRaidPeriodParam5.BeginAt.Month, (object) guildRaidPeriodParam5.BeginAt.Day, (object) guildRaidPeriodParam5.BeginAt.Hour, (object) guildRaidPeriodParam5.BeginAt.Minute, (object) guildRaidPeriodParam5.EndAt.Month, (object) guildRaidPeriodParam5.EndAt.Day, (object) guildRaidPeriodParam5.EndAt.Hour, (object) guildRaidPeriodParam5.EndAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_RANK:
          GuildRaidSeasonResult raidSeasonResult2 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult2 == null || raidSeasonResult2.mRanking == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult2.mRanking.rank);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_SCORE:
          GuildRaidSeasonResult raidSeasonResult3 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult3 == null || raidSeasonResult3.mRanking == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult3.mRanking.score);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_EMBLEM:
          Image component8 = ((Component) this).GetComponent<Image>();
          string name1 = string.Empty;
          GuildRaidRanking dataOfClass32 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass32 != null)
            name1 = dataOfClass32.Guild.award_id;
          if (string.IsNullOrEmpty(name1) || UnityEngine.Object.op_Equality((UnityEngine.Object) component8, (UnityEngine.Object) null))
            break;
          SpriteSheet spriteSheet1 = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet1, (UnityEngine.Object) null))
            break;
          component8.sprite = spriteSheet1.GetSprite(name1);
          ((Behaviour) component8).enabled = true;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_RANKIMAGE:
          GuildRaidRanking dataOfClass33 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass33 == null)
          {
            this.ResetToDefault();
            break;
          }
          ImageArray component9 = ((Component) this).gameObject.GetComponent<ImageArray>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component9, (UnityEngine.Object) null))
            break;
          int num1 = dataOfClass33.Rank - 1;
          if (num1 < 0)
            break;
          if (num1 >= component9.Images.Length)
            num1 = component9.Images.Length - 1;
          component9.ImageIndex = num1;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_GUILD_RANKTEXT:
          GuildRaidRanking dataOfClass34 = DataSource.FindDataOfClass<GuildRaidRanking>(((Component) this).gameObject, (GuildRaidRanking) null);
          if (dataOfClass34 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass34.Rank <= 3)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_RANKING_RANK"), (object) dataOfClass34.Rank));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_ICON:
          GuildRaidRankingMember dataOfClass35 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass35 != null && dataOfClass35.Unit != null)
          {
            UnitData unitDataForDisplay = UnitData.CreateUnitDataForDisplay(dataOfClass35.Unit);
            UnitIcon component10 = ((Component) this).gameObject.GetComponent<UnitIcon>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component10, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            DataSource.Bind<UnitData>(((Component) this).gameObject, unitDataForDisplay);
            component10.UpdateValue();
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_ROLE:
          GuildRaidRankingMember dataOfClass36 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass36 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
          {
            this.mImageArray.ImageIndex = Mathf.Max(0, dataOfClass36.Role - 1);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_NAME:
          GuildRaidRankingMember dataOfClass37 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass37 != null)
          {
            this.SetTextValue(dataOfClass37.Name);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_LEVEL:
          GuildRaidRankingMember dataOfClass38 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass38 != null)
          {
            this.SetTextValue(dataOfClass38.Lv);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_SCORE:
          GuildRaidRankingMember dataOfClass39 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass39 != null && dataOfClass39.Score != -1L)
          {
            this.SetTextValue(dataOfClass39.Score);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_POWER:
          GuildRaidRankingMember dataOfClass40 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass40 != null && dataOfClass40.UnitStrengthTotal != -1)
          {
            this.SetTextValue(dataOfClass40.UnitStrengthTotal);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_RANKIMAGE:
          GuildRaidRankingMember dataOfClass41 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass41 == null)
          {
            this.ResetToDefault();
            break;
          }
          ImageArray component11 = ((Component) this).gameObject.GetComponent<ImageArray>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component11, (UnityEngine.Object) null))
            break;
          int num2 = dataOfClass41.Rank - 1;
          if (num2 < 0)
          {
            this.ResetToDefault();
            break;
          }
          if (num2 >= component11.Images.Length)
            num2 = component11.Images.Length - 1;
          component11.ImageIndex = num2;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_RANKTEXT:
          GuildRaidRankingMember dataOfClass42 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass42 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass42.Rank <= 3)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_RANKING_RANK"), (object) dataOfClass42.Rank));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BP_REMAIN:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.MaxBp - GuildRaidManager.Instance.ChallengedBp);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_AP_CONSUMPTION:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          if (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.Close)
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam6 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildRaidManager.Instance.PeriodId);
          if (guildRaidPeriodParam6 == null || guildRaidPeriodParam6.HealAp <= 0)
          {
            this.ResetToDefault();
            break;
          }
          if (guildRaidPeriodParam6.BpType == GuildRaidManager.GuildRaidApDrawType.Slider)
          {
            this.SetTextValue(GuildRaidManager.Instance.CurrentAp);
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.CurrentAp % guildRaidPeriodParam6.HealAp);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_AP_MAX:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          if (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.Close)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.MaxAp);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_HOME_RANKING_RANK:
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          if (GuildRaidManager.Instance.PortRankingRank == 0)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(GuildRaidManager.Instance.PortRankingRank);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_MEMBER_RANK:
          GuildRaidRankingMember dataOfClass43 = DataSource.FindDataOfClass<GuildRaidRankingMember>(((Component) this).gameObject, (GuildRaidRankingMember) null);
          if (dataOfClass43 == null || dataOfClass43.Rank == -1)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass43.Rank);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_RANKIMAGE:
          GuildRaidRankingDamage dataOfClass44 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass44 == null)
          {
            this.ResetToDefault();
            break;
          }
          ImageArray component12 = ((Component) this).gameObject.GetComponent<ImageArray>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component12, (UnityEngine.Object) null))
            break;
          int num3 = dataOfClass44.Rank - 1;
          if (num3 < 0)
          {
            this.ResetToDefault();
            break;
          }
          if (num3 >= component12.Images.Length)
            num3 = component12.Images.Length - 1;
          component12.ImageIndex = num3;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_RANKTEXT:
          GuildRaidRankingDamage dataOfClass45 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass45 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass45.Rank <= 3)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_RANKING_RANK"), (object) dataOfClass45.Rank));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_ICON:
          GuildRaidRankingDamage dataOfClass46 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass46 != null && dataOfClass46.Unit != null)
          {
            UnitData unitDataForDisplay = UnitData.CreateUnitDataForDisplay(dataOfClass46.Unit);
            UnitIcon component13 = ((Component) this).gameObject.GetComponent<UnitIcon>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component13, (UnityEngine.Object) null))
            {
              this.ResetToDefault();
              break;
            }
            DataSource.Bind<UnitData>(((Component) this).gameObject, unitDataForDisplay);
            component13.UpdateValue();
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_NAME:
          GuildRaidRankingDamage dataOfClass47 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass47 != null)
          {
            this.SetTextValue(dataOfClass47.Name);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_LEVEL:
          GuildRaidRankingDamage dataOfClass48 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass48 != null)
          {
            this.SetTextValue(dataOfClass48.Lv);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_SCORE:
          GuildRaidRankingDamage dataOfClass49 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass49 != null)
          {
            this.SetTextValue(dataOfClass49.Score);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_POWER:
          GuildRaidRankingDamage dataOfClass50 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass50 != null)
          {
            this.SetTextValue(dataOfClass50.UnitStrengthTotal);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_BOSSNAME:
          GuildRaidRankingDamage dataOfClass51 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass51 != null)
          {
            GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(dataOfClass51.BossId);
            if (guildRaidBossParam == null)
            {
              this.ResetToDefault();
              break;
            }
            this.SetTextValue(guildRaidBossParam.Name);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REPORT_ROUND:
          GuildRaidReportData dataOfClass52 = DataSource.FindDataOfClass<GuildRaidReportData>(((Component) this).gameObject, (GuildRaidReportData) null);
          if (dataOfClass52 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass52.Round);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REPORT_DAMAGE:
          GuildRaidReportData dataOfClass53 = DataSource.FindDataOfClass<GuildRaidReportData>(((Component) this).gameObject, (GuildRaidReportData) null);
          if (dataOfClass53 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass53.Damage);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REPORT_DATE_DAY:
          GuildRaidReportData dataOfClass54 = DataSource.FindDataOfClass<GuildRaidReportData>(((Component) this).gameObject, (GuildRaidReportData) null);
          if (dataOfClass54 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_REPORT_DATE_DAY"), (object) dataOfClass54.PostedAt.Year, (object) dataOfClass54.PostedAt.Month, (object) dataOfClass54.PostedAt.Day));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_BEAT_RANK_IMAGE:
          GuildRaidRewardDmgRankingRankParam dataOfClass55 = DataSource.FindDataOfClass<GuildRaidRewardDmgRankingRankParam>(((Component) this).gameObject, (GuildRaidRewardDmgRankingRankParam) null);
          if (dataOfClass55 == null)
          {
            this.ResetToDefault();
            break;
          }
          ImageArray component14 = ((Component) this).gameObject.GetComponent<ImageArray>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component14, (UnityEngine.Object) null))
            break;
          int num4;
          if (dataOfClass55.RankStart == dataOfClass55.RankEnd)
          {
            num4 = dataOfClass55.RankStart - 1;
            if (num4 < 0)
            {
              this.ResetToDefault();
              break;
            }
            if (num4 >= component14.Images.Length)
              num4 = component14.Images.Length - 1;
          }
          else
            num4 = component14.Images.Length - 1;
          component14.ImageIndex = num4;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_BEAT_RANK_TEXT:
          GuildRaidRewardDmgRankingRankParam dataOfClass56 = DataSource.FindDataOfClass<GuildRaidRewardDmgRankingRankParam>(((Component) this).gameObject, (GuildRaidRewardDmgRankingRankParam) null);
          if (dataOfClass56 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass56.RankStart == dataOfClass56.RankEnd)
          {
            if (dataOfClass56.RankStart <= 3)
            {
              this.SetTextValue(string.Empty);
              break;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_RANKING_RANK"), (object) dataOfClass56.RankStart));
            break;
          }
          this.SetTextValue(dataOfClass56.RankStart);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_REPORT_DATE_TIME:
          GuildRaidReportData dataOfClass57 = DataSource.FindDataOfClass<GuildRaidReportData>(((Component) this).gameObject, (GuildRaidReportData) null);
          if (dataOfClass57 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_REPORT_DATE_TIME"), (object) dataOfClass57.PostedAt.Hour, (object) dataOfClass57.PostedAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_TRIAL:
          Button component15 = ((Component) this).gameObject.GetComponent<Button>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component15, (UnityEngine.Object) null))
            break;
          ((Selectable) component15).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_QUEST_NAME:
          QuestParam questParam1;
          if ((questParam1 = this.GetQuestParam()) != null)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null) && GuildRaidManager.Instance.BattleType == GuildRaidBattleType.Mock)
            {
              this.SetTextValue(questParam1.name + "(" + LocalizedText.Get("sys.GUILDRAID_SWITCH_BATTALETEST") + ")");
              break;
            }
            this.SetTextValue(questParam1.name);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_DMGBOSS_ROLE:
          GuildRaidRankingDamage dataOfClass58 = DataSource.FindDataOfClass<GuildRaidRankingDamage>(((Component) this).gameObject, (GuildRaidRankingDamage) null);
          if (dataOfClass58 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
          {
            this.mImageArray.ImageIndex = Mathf.Max(0, dataOfClass58.Role - 1);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BUTTON_NOCHALLENGE:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null) && GuildRaidManager.Instance.CurrentBossInfo.CurrentHP > 0)
          {
            if (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.OpenSchedule || MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.Open)
            {
              ((Component) this).gameObject.SetActive(!GuildRaidManager.Instance.IsBossChallenge(GuildRaidManager.Instance.CurrentBossInfo));
              Button component16 = ((Component) this).gameObject.GetComponent<Button>();
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) component16, (UnityEngine.Object) null))
                break;
              GuildRaidBossParam dataOfClass59 = DataSource.FindDataOfClass<GuildRaidBossParam>(((Component) this).gameObject, (GuildRaidBossParam) null);
              if (dataOfClass59 == null || GuildRaidManager.Instance.CurrentAreaNo >= dataOfClass59.AreaNo)
                break;
              ((Selectable) component16).interactable = false;
              break;
            }
            ((Component) this).gameObject.SetActive(false);
            break;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_OLDIMAGE:
          GuildRaidManager instance6 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance6, (UnityEngine.Object) null))
          {
            GuildRaidBossParam guildRaidBossParam = GuildRaidManager.Instance.CurrentAreaNo <= 1 ? MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(instance6.PeriodId, MonoSingleton<GameManager>.Instance.GetGuildRaidBossCountByPeriod(instance6.PeriodId)) : MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(instance6.PeriodId, instance6.CurrentAreaNo - 1);
            if (guildRaidBossParam != null)
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
              if (unitParam != null)
              {
                GameUtility.RequireComponent<IconLoader>(((Component) this).gameObject).ResourcePath = AssetPath.UnitImage(unitParam, (string) null);
                break;
              }
              this.ResetToDefault();
              break;
            }
            this.ResetToDefault();
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.GUILDRAID_PARTYEDITOR_ENABLE:
          UnitData unitData;
          QuestParam questParam2;
          if ((unitData = this.GetUnitData()) != null && (questParam2 = this.GetQuestParam()) != null)
          {
            string empty3 = string.Empty;
            if (questParam2.IsEntryQuestCondition(unitData, ref empty3))
            {
              ((Component) this).gameObject.SetActive(false);
              break;
            }
            if (questParam2.IsGuildRaid)
            {
              if (this.Index == 0)
              {
                ((Component) this).gameObject.SetActive(!empty3.Equals("sys.GUILDRAID_PARTY_ERROR_USED_UNIT"));
                break;
              }
              ((Component) this).gameObject.SetActive(empty3.Equals("sys.GUILDRAID_PARTY_ERROR_USED_UNIT"));
              break;
            }
            ((Component) this).gameObject.SetActive(true);
            break;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_BEAT_RANK_RANKENDTEXT:
          GuildRaidRewardDmgRankingRankParam dataOfClass60 = DataSource.FindDataOfClass<GuildRaidRewardDmgRankingRankParam>(((Component) this).gameObject, (GuildRaidRewardDmgRankingRankParam) null);
          if (dataOfClass60 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(dataOfClass60.RankEnd);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_BEAT_RANK_RANKONLY:
          GuildRaidRewardDmgRankingRankParam dataOfClass61 = DataSource.FindDataOfClass<GuildRaidRewardDmgRankingRankParam>(((Component) this).gameObject, (GuildRaidRewardDmgRankingRankParam) null);
          if (dataOfClass61 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass61.RankStart == dataOfClass61.RankEnd)
          {
            ((Component) this).gameObject.SetActive(true);
            break;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_BEAT_RANK_RANKMULTI:
          GuildRaidRewardDmgRankingRankParam dataOfClass62 = DataSource.FindDataOfClass<GuildRaidRewardDmgRankingRankParam>(((Component) this).gameObject, (GuildRaidRewardDmgRankingRankParam) null);
          if (dataOfClass62 == null)
          {
            this.ResetToDefault();
            break;
          }
          if (dataOfClass62.RankStart == dataOfClass62.RankEnd)
          {
            ((Component) this).gameObject.SetActive(false);
            break;
          }
          ((Component) this).gameObject.SetActive(true);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_GUILDNAME:
          GuildRaidSeasonResult raidSeasonResult4 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult4 == null || raidSeasonResult4.mGuild == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult4.mGuild.name);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_GUILDLEVEL:
          GuildRaidSeasonResult raidSeasonResult5 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult5 == null || raidSeasonResult5.mGuild == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult5.mGuild.level);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_MASTER:
          GuildRaidSeasonResult raidSeasonResult6 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult6 == null || raidSeasonResult6.mGuild == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult6.mGuild.guild_master);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_EMBLEM:
          Image component17 = ((Component) this).GetComponent<Image>();
          string name2 = string.Empty;
          GuildRaidSeasonResult raidSeasonResult7 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult7 != null && raidSeasonResult7.mGuild != null)
            name2 = raidSeasonResult7.mGuild.award_id;
          if (string.IsNullOrEmpty(name2) || UnityEngine.Object.op_Equality((UnityEngine.Object) component17, (UnityEngine.Object) null))
            break;
          SpriteSheet spriteSheet2 = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet2, (UnityEngine.Object) null))
            break;
          component17.sprite = spriteSheet2.GetSprite(name2);
          ((Behaviour) component17).enabled = true;
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_MEMBER:
          GuildRaidSeasonResult raidSeasonResult8 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult8 == null || raidSeasonResult8.mGuild == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult8.mGuild.member_count);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_RANKING_REWARD_MEMBERFULL:
          GuildRaidSeasonResult raidSeasonResult9 = MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult;
          if (raidSeasonResult9 == null || raidSeasonResult9.mGuild == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(raidSeasonResult9.mGuild.max_count);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_ISPERIOD:
          GuildRaidPeriodParam guildRaidPeriodParam7 = MonoSingleton<GameManager>.Instance.GetNowScheduleGuildRaidPeriodParam();
          if (this.Index == 0)
          {
            ((Component) this).gameObject.SetActive(guildRaidPeriodParam7 != null);
            break;
          }
          ((Component) this).gameObject.SetActive(guildRaidPeriodParam7 == null);
          break;
        case GameParameter.ParameterTypes.GUILDRAID_PERIOD_TIME:
          GuildRaidPeriodParam guildRaidPeriodParam8 = MonoSingleton<GameManager>.Instance.GetNowScheduleGuildRaidPeriodParam();
          if (guildRaidPeriodParam8 == null)
          {
            this.ResetToDefault();
            break;
          }
          this.SetTextValue(string.Format(LocalizedText.Get("sys.GUILDRAID_TEXT_LOBBY_PERIOD_TIME"), (object) guildRaidPeriodParam8.EndAt.Month, (object) guildRaidPeriodParam8.EndAt.Day, (object) guildRaidPeriodParam8.EndAt.Hour, (object) guildRaidPeriodParam8.EndAt.Minute));
          break;
        case GameParameter.ParameterTypes.GUILDRAID_BP_REMAIN_APHEAL:
          GuildRaidManager instance7 = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance7, (UnityEngine.Object) null))
          {
            this.ResetToDefault();
            break;
          }
          GuildRaidPeriodParam guildRaidPeriodParam9 = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance7.PeriodId);
          if (guildRaidPeriodParam9 == null)
          {
            this.ResetToDefault();
            break;
          }
          int num5 = guildRaidPeriodParam9.Bp - instance7.HealBp;
          if (num5 < 0)
            num5 = 0;
          this.SetTextValue(num5);
          break;
      }
    }

    private void UpdateValue_Rune()
    {
      MasterParam masterParam = (MasterParam) null;
      GameManager instanceDirect;
      PlayerData player;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) (instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect()), (UnityEngine.Object) null) || (masterParam = instanceDirect.MasterParam) == null || (player = instanceDirect.Player) == null)
        return;
      switch (this.ParameterType)
      {
        case GameParameter.ParameterTypes.RUNE_RARITY_FRAME:
          BindRuneData bindRuneData1 = this.GetBindRuneData();
          Image component1 = ((Component) this).GetComponent<Image>();
          if (bindRuneData1 != null && bindRuneData1.Rune != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          {
            GameSettings instance = GameSettings.Instance;
            int rare = bindRuneData1.Rune.RuneParam.ItemParam.rare;
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) || rare < 0 || rare >= instance.ArtifactIcon_Frames.Length)
              break;
            component1.sprite = instance.ArtifactIcon_Frames[rare];
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.RUNE_ICON:
          BindRuneData bindRuneData2 = this.GetBindRuneData();
          if (bindRuneData2 != null && bindRuneData2.Rune != null && this.LoadItemIcon(bindRuneData2.Rune.RuneParam.ItemParam))
          {
            if (this.ViewType != 1)
              break;
            ((Component) this).gameObject.RequireComponent<ItemLimitedIconAttach>().Refresh(bindRuneData2.Rune.RuneParam.ItemParam);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.RUNE_NUM:
          this.SetTextValue(player.CurrentRuneStorageUsed);
          break;
        case GameParameter.ParameterTypes.RUNE_STORAGE_SIZE:
          this.SetTextValue(player.CurrentRuneStorageSize);
          break;
        case GameParameter.ParameterTypes.RUNE_FAVORITE_ICON:
          BindRuneData bindRuneData3 = this.GetBindRuneData();
          if (bindRuneData3 != null)
          {
            this.SetImageIndex(!bindRuneData3.IsFavorite ? 0 : 1);
            break;
          }
          this.ResetToDefault();
          break;
        case GameParameter.ParameterTypes.RUNE_SETEFFECT_ICON:
          string empty = string.Empty;
          ImageSpriteSheet component2 = ((Component) this).GetComponent<ImageSpriteSheet>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          {
            RuneParam runeParam = (RuneParam) null;
            ShopItem dataOfClass1 = DataSource.FindDataOfClass<ShopItem>(((Component) this).gameObject, (ShopItem) null);
            if (dataOfClass1 != null)
            {
              if (dataOfClass1.ShopItemType == EShopItemType.Item)
                runeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParamByItemId(dataOfClass1.iname);
            }
            else
            {
              if (runeParam == null)
              {
                BindRuneData bindRuneData4 = this.GetBindRuneData();
                if (bindRuneData4 != null)
                  runeParam = bindRuneData4.RuneParam;
              }
              if (runeParam == null)
                runeParam = DataSource.FindDataOfClass<RuneParam>(((Component) this).gameObject, (RuneParam) null);
              if (runeParam == null)
              {
                ItemData dataOfClass2 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
                if (dataOfClass2 != null)
                {
                  ItemParam itemParam = dataOfClass2.Param;
                  if (itemParam != null)
                    runeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParamByItemId(itemParam.iname);
                }
              }
              if (runeParam == null)
              {
                ItemParam dataOfClass3 = DataSource.FindDataOfClass<ItemParam>(((Component) this).gameObject, (ItemParam) null);
                if (dataOfClass3 != null)
                  runeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParamByItemId(dataOfClass3.iname);
              }
            }
            if (runeParam != null)
              empty = runeParam.SetEffTypeIconIndex.ToString();
          }
          if (!string.IsNullOrEmpty(empty))
          {
            ((Behaviour) component2).enabled = true;
            this.SetImageBySpriteSheet(empty);
            break;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          {
            ((Behaviour) component2).enabled = false;
            break;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
      }
    }

    private void UpdateValue_Ranking()
    {
      MasterParam masterParam = (MasterParam) null;
      PlayerData playerData = (PlayerData) null;
      GameManager instanceDirect;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) (instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect()), (UnityEngine.Object) null) || (masterParam = instanceDirect.MasterParam) == null || (playerData = instanceDirect.Player) == null)
        return;
      switch (this.ParameterType)
      {
        case GameParameter.ParameterTypes.RANKING_RANK_IMAGE:
          IRankingContent dataOfClassAs1 = DataSource.FindDataOfClassAs<IRankingContent>(((Component) this).gameObject, (IRankingContent) null);
          if (dataOfClassAs1 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) || this.mImageArray.Images.Length == 0)
            break;
          if (dataOfClassAs1.Rank == -1)
          {
            this.mImageArray.ImageIndex = Mathf.Max(0, this.mImageArray.Images.Length - 1);
            break;
          }
          this.mImageArray.ImageIndex = Mathf.Clamp(dataOfClassAs1.Rank - 1, 0, this.mImageArray.Images.Length - 1);
          break;
        case GameParameter.ParameterTypes.RANKING_RANK_TEXT:
          IRankingContent dataOfClassAs2 = DataSource.FindDataOfClassAs<IRankingContent>(((Component) this).gameObject, (IRankingContent) null);
          if (dataOfClassAs2 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
            break;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
          {
            if (dataOfClassAs2.Rank == -1)
            {
              this.mText.text = LocalizedText.Get("sys.RANKING_OUT_OF_RANK_TEXT");
              if (((Component) this).gameObject.activeInHierarchy)
                break;
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            if (dataOfClassAs2.Rank >= 4)
            {
              this.mText.text = LocalizedText.Get("sys.RANKING_RANK_TEXT", (object) dataOfClassAs2.Rank);
              if (((Component) this).gameObject.activeInHierarchy)
                break;
              ((Component) this).gameObject.SetActive(true);
              break;
            }
            if (!((Component) this).gameObject.activeInHierarchy)
              break;
            ((Component) this).gameObject.SetActive(false);
            break;
          }
          if (!((Component) this).gameObject.activeInHierarchy)
            break;
          ((Component) this).gameObject.SetActive(false);
          break;
      }
    }

    private void UpdateValue_CombatPower()
    {
      MasterParam masterParam = (MasterParam) null;
      PlayerData playerData = (PlayerData) null;
      GameManager instanceDirect;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) (instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect()), (UnityEngine.Object) null) || (masterParam = instanceDirect.MasterParam) == null || (playerData = instanceDirect.Player) == null || this.ParameterType != GameParameter.ParameterTypes.COMBATPOWER_VALUE)
        return;
      ICombatPowerContent dataOfClassAs = DataSource.FindDataOfClassAs<ICombatPowerContent>(((Component) this).gameObject, (ICombatPowerContent) null);
      if (dataOfClassAs == null)
        return;
      this.SetTextValue(dataOfClassAs.CombatPower);
    }

    public static void UpdateValuesOfType(GameParameter.ParameterTypes type)
    {
      for (int index = 0; index < GameParameter.Instances.Count; ++index)
      {
        if (GameParameter.Instances[index].ParameterType == type)
          GameParameter.Instances[index].UpdateValue();
      }
    }

    public static void UpdateAll(GameObject root)
    {
      Component[] componentsInChildren = root.GetComponentsInChildren(typeof (IGameParameter), true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (componentsInChildren[index] is GameParameter)
        {
          GameParameter gameParameter = componentsInChildren[index] as GameParameter;
          if (((Component) gameParameter).gameObject.activeInHierarchy)
          {
            gameParameter.UpdateValue();
          }
          else
          {
            Transform transform = ((Component) gameParameter).transform;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform.parent, (UnityEngine.Object) null) && ((Component) transform.parent).gameObject.activeInHierarchy)
              gameParameter.UpdateValue();
          }
        }
        else
          ((IGameParameter) componentsInChildren[index]).UpdateValue();
      }
    }

    private void SetUpdateInterval(float interval)
    {
      if (!((Component) this).gameObject.activeInHierarchy)
        return;
      if ((double) interval <= 0.0)
      {
        if (this.mUpdateCoroutine == null)
          return;
        this.StopCoroutine(this.mUpdateCoroutine);
      }
      else
      {
        this.mNextUpdateTime = Time.time + interval;
        if (this.mUpdateCoroutine != null)
          return;
        this.mUpdateCoroutine = this.StartCoroutine(this.UpdateTimer());
      }
    }

    [DebuggerHidden]
    private IEnumerator UpdateTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameParameter.\u003CUpdateTimer\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnDestroy() => GameParameter.Instances.Remove(this);

    public void ToggleChildren(bool visible)
    {
      Transform transform = ((Component) this).transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
        ((Component) transform.GetChild(index)).gameObject.SetActive(visible);
    }

    public void ToggleEmpty(bool visible)
    {
      if (!this.mIsEmptyGO)
        return;
      ((Component) this).gameObject.SetActive(visible);
    }

    public void ResetToDefault()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
        this.mText.text = this.mDefaultValue;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
      {
        this.mSlider.value = this.mDefaultRangeValue.x;
        this.mSlider.maxValue = this.mDefaultRangeValue.y;
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInputField, (UnityEngine.Object) null))
        this.mInputField.text = this.mDefaultValue;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImage, (UnityEngine.Object) null))
      {
        this.mImage.texture = this.mDefaultImage;
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
          return;
        this.mImageArray.sprite = this.mDefaultSprite;
      }
    }

    private void SetTextValue(string value)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
        this.mText.text = value;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInputField, (UnityEngine.Object) null))
        return;
      this.mInputField.SetText(value);
    }

    private void SetTextValue(int value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null) && !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInputField, (UnityEngine.Object) null))
        return;
      this.SetTextValue(value.ToString());
    }

    private void SetTextValue(long value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null) && !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInputField, (UnityEngine.Object) null))
        return;
      this.SetTextValue(value.ToString());
    }

    private void SetTextColor(Color color)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
        return;
      ((Graphic) this.mText).color = color;
    }

    private void SetSyncColorOriginColor(Color color)
    {
      SyncColor component = ((Component) this).GetComponent<SyncColor>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.ForceOriginColorChange(color);
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
        return;
      this.mSlider.maxValue = (float) maxValue;
      this.mSlider.minValue = 0.0f;
      this.mSlider.value = (float) value;
    }

    private void SetSliderValue(long value, long maxValue)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSlider))
        return;
      this.mSlider.normalizedValue = maxValue == 0L ? 0.0f : Mathf.Clamp01((float) value / (float) maxValue);
    }

    private void SetImageIndex(int index)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
        return;
      this.mImageArray.ImageIndex = index;
    }

    private void SetAnimatorInt(string name, int value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimator, (UnityEngine.Object) null))
        return;
      this.mAnimator.SetInteger(name, value);
    }

    private void SetAnimatorBool(string name, bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimator, (UnityEngine.Object) null))
        return;
      this.mAnimator.SetBool(name, value);
    }

    private int GetImageLength()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null) ? this.mImageArray.Images.Length : 0;
    }

    private void SetImageBySpriteSheet(string key)
    {
      ImageSpriteSheet component = ((Component) this).GetComponent<ImageSpriteSheet>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetSprite(key);
    }

    private void Awake()
    {
      GameParameter.Instances.Add(this);
      this.mText = ((Component) this).GetComponent<UnityEngine.UI.Text>();
      this.mSlider = ((Component) this).GetComponent<Slider>();
      this.mInputField = ((Component) this).GetComponent<InputField>();
      this.mAnimator = ((Component) this).GetComponent<Animator>();
      this.mImage = ((Component) this).GetComponent<RawImage>();
      this.mImageArray = ((Component) this).GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mText, (UnityEngine.Object) null))
        this.mDefaultValue = this.mText.text;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
      {
        this.mDefaultRangeValue.x = this.mSlider.value;
        this.mDefaultRangeValue.y = this.mSlider.maxValue;
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInputField, (UnityEngine.Object) null))
        this.mDefaultValue = this.mInputField.text;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImage, (UnityEngine.Object) null))
        this.mDefaultImage = this.mImage.texture;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mImageArray, (UnityEngine.Object) null))
        this.mDefaultSprite = this.mImageArray.sprite;
      else
        this.mIsEmptyGO = true;
    }

    private void Start()
    {
      this.mStarted = true;
      this.UpdateValue();
    }

    private void OnEnable()
    {
      if (this.mUpdateCoroutine != null)
      {
        this.StopCoroutine(this.mUpdateCoroutine);
        this.mUpdateCoroutine = (Coroutine) null;
      }
      if (!this.mStarted)
        return;
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      if (!((Component) this).gameObject.activeInHierarchy)
      {
        if (typeof (GameParameter.ParameterTypes).GetField(this.ParameterType.ToString()).GetCustomAttributes(typeof (GameParameter.AlwaysUpdate), true).Length <= 0)
          return;
        this.InternalUpdateValue();
      }
      else
        this.InternalUpdateValue();
    }

    protected virtual void BatchUpdate()
    {
    }

    private static int AbilityTypeDetailToImageIndex(
      EAbilitySlot type,
      EAbilityTypeDetail typeDetail)
    {
      switch (typeDetail)
      {
        case EAbilityTypeDetail.Job_Unique:
        case EAbilityTypeDetail.Job_Basic:
          return 0;
        case EAbilityTypeDetail.Job_Support:
          return 1;
        case EAbilityTypeDetail.Job_Reaction:
          return 2;
        default:
          return 3;
      }
    }

    public enum QuestInstanceTypes
    {
      Any,
      Playing,
      Selected,
    }

    public enum ArenaPlayerInstanceTypes
    {
      Any,
      Enemy,
    }

    public enum UnitInstanceTypes
    {
      Any,
      OBSOLETE_MainTarget,
      OBSOLETE_SubTarget,
      CurrentTurn,
      ArenaPlayerUnit0,
      ArenaPlayerUnit1,
      ArenaPlayerUnit2,
      EnemyArenaPlayerUnit0,
      EnemyArenaPlayerUnit1,
      EnemyArenaPlayerUnit2,
      PartyUnit0,
      PartyUnit1,
      PartyUnit2,
      VersusUnit,
      MultiUnit,
      MultiTowerUnit,
      VersusCpuUnit0,
      VersusCpuUnit1,
      VersusCpuUnit2,
      RankMatchUnit,
      FriendPartyUnit,
      VersusDraftPlayerUnit0,
      VersusDraftPlayerUnit1,
      VersusDraftPlayerUnit2,
      VersusDraftPlayerUnit3,
      VersusDraftPlayerUnit4,
      VersusDraftPlayerUnit5,
      VersusDraftEnemyUnit0,
      VersusDraftEnemyUnit1,
      VersusDraftEnemyUnit2,
      VersusDraftEnemyUnit3,
      VersusDraftEnemyUnit4,
      VersusDraftEnemyUnit5,
    }

    public enum PartyAttackTypes
    {
      Normal,
      Quest,
    }

    public enum TargetInstanceTypes
    {
      Main,
      Sub,
    }

    public enum ItemInstanceTypes
    {
      Any,
      Inventory,
      QuestReward,
      Equipment,
      EnhanceMaterial,
      EnhanceEquipData,
      SellItem,
      ConsumeItem,
    }

    public enum ItemViewTypes
    {
      HideLimitedIcon,
      DisplayLimitedIcon,
    }

    public enum ArtifactInstanceTypes
    {
      Any,
      QuestReward,
      Trophy,
    }

    public enum TrophyBadgeInstanceTypes
    {
      Any,
      Normal,
      Daily,
      NormalStory,
      NormalEvent,
      NormalTraining,
      NormalOther,
    }

    public enum TrophyConditionInstances
    {
      List,
      Index,
    }

    public enum VersusPlayerInstanceType
    {
      Player,
      Enemy,
    }

    public enum RankMatchPlayer
    {
      Current,
      Before,
    }

    public enum VersusDraftSlot
    {
      PlayerLeader,
      PlayerSlot2,
      PlayerSlot3,
      PlayerSlot4,
      PlayerSlot5,
      EnemyLeader,
      EnemySlot2,
      EnemySlot3,
      EnemySlot4,
      EnemySlot5,
    }

    public enum LimitItemInstanceType
    {
      Any,
      GachaDiscount,
      ItemDetail,
    }

    public enum RaidRescueStatusType
    {
      Yet,
      Done,
    }

    private enum eEntryCondition
    {
      EntryCondition,
      SameUnit,
      Used_WorldRaid,
      Max,
    }

    private enum GvGStatus
    {
      GVG_DECLARE,
      GVG_BATTLE,
      GVG_DECLARE_COOLTIME,
      GVG_AGGREGATION,
      GVG_FINISH,
      GVG_BATTLE_COOLTIME,
    }

    public enum ParameterTypes
    {
      [GameParameter.ParameterDesc("プレイヤーの名前")] GLOBAL_PLAYER_NAME = 0,
      [GameParameter.ParameterDesc("プレイヤーのレベル")] GLOBAL_PLAYER_LEVEL = 1,
      [GameParameter.ParameterDesc("プレイヤーの現在のスタミナ")] GLOBAL_PLAYER_STAMINA = 2,
      [GameParameter.ParameterDesc("プレイヤーの最大スタミナ")] GLOBAL_PLAYER_STAMINAMAX = 3,
      [GameParameter.ParameterDesc("プレイヤーの経験値")] GLOBAL_PLAYER_EXP = 4,
      [GameParameter.ParameterDesc("プレイヤーが次のレベルまでに必要な経験値")] GLOBAL_PLAYER_EXPNEXT = 5,
      [GameParameter.ParameterDesc("プレイヤーの所持ゴールド")] GLOBAL_PLAYER_GOLD = 6,
      [GameParameter.ParameterDesc("プレイヤーの所持コイン")] GLOBAL_PLAYER_COIN = 7,
      [GameParameter.ParameterDesc("プレイヤーのスタミナが次に回復するまでの時間")] GLOBAL_PLAYER_STAMINATIME = 8,
      [GameParameter.ParameterDesc("クエスト名"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_NAME = 9,
      [GameParameter.ParameterDesc("クエストに必要なスタミナ"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_STAMINA = 10, // 0x0000000A
      [GameParameter.EnumParameterDesc("クエストのクリア状態にあわせてAnimatorのstateという名前のInt値、ImageArrayのインデックスを切り替えます。", typeof (QuestStates)), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_STATE = 11, // 0x0000000B
      [GameParameter.ParameterDesc("クエスト勝利条件"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_OBJECTIVE = 12, // 0x0000000C
      [GameParameter.ParameterDesc("クエストボーナス条件。インデックスでボーナス条件の番号を指定してください。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE = 13, // 0x0000000D
      [GameParameter.ParameterDesc("アイテムアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.ViewTypes(typeof (GameParameter.ItemViewTypes)), GameParameter.UsesIndex] ITEM_ICON = 14, // 0x0000000E
      [GameParameter.ParameterDesc("クエストの説明"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_DESCRIPTION = 15, // 0x0000000F
      [GameParameter.ParameterDesc("フレンドの名前")] SUPPORTER_NAME = 16, // 0x00000010
      [GameParameter.ParameterDesc("フレンドのレベル")] SUPPORTER_LEVEL = 17, // 0x00000011
      [GameParameter.ParameterDesc("フレンドのユニットレベル")] SUPPORTER_UNITLEVEL = 18, // 0x00000012
      [GameParameter.ParameterDesc("フレンドのリーダースキル名")] SUPPORTER_LEADERSKILLNAME = 19, // 0x00000013
      [GameParameter.ParameterDesc("フレンドの攻撃力")] SUPPORTER_ATK = 20, // 0x00000014
      [GameParameter.ParameterDesc("フレンドのHP")] SUPPORTER_HP = 21, // 0x00000015
      [GameParameter.ParameterDesc("フレンドの魔法攻撃力")] SUPPORTER_MAGIC = 22, // 0x00000016
      [GameParameter.EnumParameterDesc("フレンドのレアリティにあわせてAnimatorのrareという名前のInt値を切り替えます。", typeof (ERarity))] SUPPORTER_RARITY = 23, // 0x00000017
      [GameParameter.EnumParameterDesc("フレンドの属性にあわせてAnimatorのelementという名前のInt値を切り替えます。", typeof (EElement))] SUPPORTER_ELEMENT = 24, // 0x00000018
      [GameParameter.ParameterDesc("フレンドのアイコン")] SUPPORTER_ICON = 25, // 0x00000019
      [GameParameter.ParameterDesc("フレンドのリーダースキルの説明")] SUPPORTER_LEADERSKILLDESC = 26, // 0x0000001A
      [GameParameter.ParameterDesc("クエストの副題"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_SUBTITLE = 27, // 0x0000001B
      [GameParameter.ParameterDesc("ユニットのレベル"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_LEVEL = 28, // 0x0000001C
      [GameParameter.ParameterDesc("ユニットのHP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_HP = 29, // 0x0000001D
      [GameParameter.ParameterDesc("ユニットの最大HP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_HPMAX = 30, // 0x0000001E
      [GameParameter.ParameterDesc("ユニットの攻撃力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_ATK = 31, // 0x0000001F
      [GameParameter.ParameterDesc("ユニットの魔力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_MAG = 32, // 0x00000020
      [GameParameter.ParameterDesc("ユニットのアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_ICON = 33, // 0x00000021
      [GameParameter.ParameterDesc("ユニットの名前"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_NAME = 34, // 0x00000022
      [GameParameter.EnumParameterDesc("ユニットのレアリティに応じてAnimatorにrareというint値を設定します。ImageArrayの場合レアリティに応じた番号のイメージを使用します。StarGaugeの場合現在のレアリティと最大のレアリティがそれぞれ現在値と最大値になります。", typeof (ERarity)), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_RARITY = 35, // 0x00000023
      [GameParameter.ParameterDesc("パーティのリーダースキルの名前")] PARTY_LEADERSKILLNAME = 36, // 0x00000024
      [GameParameter.ParameterDesc("パーティのリーダースキルの説明")] PARTY_LEADERSKILLDESC = 37, // 0x00000025
      [GameParameter.ParameterDesc("ユニットの防御力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_DEF = 38, // 0x00000026
      [GameParameter.ParameterDesc("ユニットの魔法防御力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_MND = 39, // 0x00000027
      [GameParameter.ParameterDesc("ユニットの素早さ"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_SPEED = 40, // 0x00000028
      [GameParameter.ParameterDesc("ユニットの運"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_LUCK = 41, // 0x00000029
      [GameParameter.ParameterDesc("ユニットジョブ名"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBNAME = 42, // 0x0000002A
      [GameParameter.ParameterDesc("ユニットジョブランク"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBRANK = 43, // 0x0000002B
      [GameParameter.EnumParameterDesc("ユニット属性にあわせてAnimatorのelementという名前のInt値、もしくはImageArrayを切り替えます。", typeof (EElement)), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_ELEMENT = 44, // 0x0000002C
      [GameParameter.ParameterDesc("パーティの総攻撃力"), GameParameter.InstanceTypes(typeof (GameParameter.PartyAttackTypes))] PARTY_TOTALATK = 45, // 0x0000002D
      [GameParameter.ParameterDesc("インベントリーにセットされたアイテムのアイコン\n*スロット番号をIndexで指定"), GameParameter.ViewTypes(typeof (GameParameter.ItemViewTypes)), GameParameter.UsesIndex] INVENTORY_ITEMICON = 46, // 0x0000002E
      [GameParameter.ParameterDesc("インベントリーにセットされたアイテムの名前*スロット番号をIndexで指定"), GameParameter.UsesIndex] INVENTORY_ITEMNAME = 47, // 0x0000002F
      [GameParameter.ParameterDesc("アイテムの名前"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_NAME = 48, // 0x00000030
      [GameParameter.ParameterDesc("アイテムの説明"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_DESC = 49, // 0x00000031
      [GameParameter.ParameterDesc("アイテムの売却価格"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_SELLPRICE = 50, // 0x00000032
      [GameParameter.ParameterDesc("アイテムの購入価格"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_BUYPRICE = 51, // 0x00000033
      [GameParameter.ParameterDesc("アイテムの所持個数"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_AMOUNT = 52, // 0x00000034
      [GameParameter.ParameterDesc("インベントリーにセットされたアイテムの所持数*スロット番号をIndexで指定"), GameParameter.UsesIndex] INVENTORY_ITEMAMOUNT = 53, // 0x00000035
      [GameParameter.ParameterDesc("所持ユニット数")] PLAYER_NUMUNITS = 54, // 0x00000036
      [GameParameter.ParameterDesc("所持可能の最大ユニット数")] PLAYER_MAXUNITS = 55, // 0x00000037
      [GameParameter.ParameterDesc("選択しているグリッドの高さ")] GRID_HEIGHT = 56, // 0x00000038
      [GameParameter.ParameterDesc("スキルの名前")] SKILL_NAME = 57, // 0x00000039
      [GameParameter.ParameterDesc("スキルのアイコン")] SKILL_ICON = 58, // 0x0000003A
      [GameParameter.ParameterDesc("スキルの説明")] SKILL_DESCRIPTION = 59, // 0x0000003B
      [GameParameter.ParameterDesc("スキルの消費ジュエル")] SKILL_MP = 60, // 0x0000003C
      [GameParameter.ParameterDesc("クエストで入手したゴールド")] BATTLE_GOLD = 61, // 0x0000003D
      [GameParameter.ParameterDesc("クエストで入手した宝箱の個数")] BATTLE_TREASURE = 62, // 0x0000003E
      [GameParameter.ParameterDesc("ユニットのMP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_MP = 63, // 0x0000003F
      [GameParameter.ParameterDesc("ユニットの最大MP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_MPMAX = 64, // 0x00000040
      [GameParameter.ParameterDesc("ターゲットの出力ポイント"), GameParameter.InstanceTypes(typeof (GameParameter.TargetInstanceTypes))] TARGET_OUTPUTPOINT = 65, // 0x00000041
      [GameParameter.ParameterDesc("ターゲットのクリティカル率"), GameParameter.InstanceTypes(typeof (GameParameter.TargetInstanceTypes))] TARGET_CRITICALRATE = 66, // 0x00000042
      [GameParameter.EnumParameterDesc("ターゲットの行動の種類にあわせてAnimatorにstate(Int)を設定する。", typeof (SceneBattle.TargetActionTypes)), GameParameter.InstanceTypes(typeof (GameParameter.TargetInstanceTypes))] TARGET_ACTIONTYPE = 67, // 0x00000043
      [GameParameter.ParameterDesc("アビリティのアイコン")] ABILITY_ICON = 68, // 0x00000044
      [GameParameter.ParameterDesc("アビリティの名前")] ABILITY_NAME = 69, // 0x00000045
      [GameParameter.ParameterDesc("クエストで入手可能な欠片のアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_KAKERA_ICON = 70, // 0x00000046
      [GameParameter.ParameterDesc("ユニットの経験値"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_EXP = 71, // 0x00000047
      [GameParameter.ParameterDesc("ユニットのレベルアップに必要な経験値の合計"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_EXPMAX = 72, // 0x00000048
      [GameParameter.ParameterDesc("ユニットのレベルアップに必要な経験値の残り"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_EXPTOGO = 73, // 0x00000049
      [GameParameter.ParameterDesc("ユニットの覚醒に必要な欠片の所持数"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_KAKERA_NUM = 74, // 0x0000004A
      [GameParameter.ParameterDesc("ユニットの覚醒に必要な欠片の数"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_KAKERA_MAX = 75, // 0x0000004B
      [GameParameter.ParameterDesc("装備品の経験値 (未実装)")] EQUIPMENT_EXP = 76, // 0x0000004C
      [GameParameter.ParameterDesc("装備品の強化に必要な経験値 (未実装)")] EQUIPMENT_EXPMAX = 77, // 0x0000004D
      [GameParameter.ParameterDesc("装備品のランク。Animatorであればrankというint値にランクを設定する。ImageArrayであればランクに対応したイメージを使用する。")] EQUIPMENT_RANK = 78, // 0x0000004E
      [GameParameter.ParameterDesc("アビリティのレベル")] ABILITY_RANK = 79, // 0x0000004F
      [GameParameter.ParameterDesc("アビリティの経験値")] OBSOLETE_ABILITY = 80, // 0x00000050
      [GameParameter.ParameterDesc("アビリティの最大経験値")] ABILITY_NEXTGOLD = 81, // 0x00000051
      [GameParameter.EnumParameterDesc("アビリティの種類にあわせて、Animatorのtype、ImageArrayを切り替えます。", typeof (EAbilitySlot))] ABILITY_SLOT = 82, // 0x00000052
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブのアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_JOBICON = 83, // 0x00000053
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブのランク"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_RANK = 84, // 0x00000054
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブの名前"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_NAME = 85, // 0x00000055
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブの最大ランク"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_RANKMAX = 86, // 0x00000056
      [GameParameter.ParameterDesc("装備アイテムのHP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_HP = 87, // 0x00000057
      [GameParameter.ParameterDesc("装備アイテムのAP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_AP = 88, // 0x00000058
      [GameParameter.ParameterDesc("装備アイテムの初期AP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_IAP = 89, // 0x00000059
      [GameParameter.ParameterDesc("装備アイテムの攻撃力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_ATK = 90, // 0x0000005A
      [GameParameter.ParameterDesc("装備アイテムの防御力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_DEF = 91, // 0x0000005B
      [GameParameter.ParameterDesc("装備アイテムの魔法攻撃力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_MAG = 92, // 0x0000005C
      [GameParameter.ParameterDesc("装備アイテムの魔法防御力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_MND = 93, // 0x0000005D
      [GameParameter.ParameterDesc("装備アイテムの回復力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_REC = 94, // 0x0000005E
      [GameParameter.ParameterDesc("装備アイテムの速度。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_SPD = 95, // 0x0000005F
      [GameParameter.ParameterDesc("装備アイテムのクリティカル。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_CRI = 96, // 0x00000060
      [GameParameter.ParameterDesc("装備アイテムの運。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_LUK = 97, // 0x00000061
      [GameParameter.ParameterDesc("装備アイテムの移動量。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_MOV = 98, // 0x00000062
      [GameParameter.ParameterDesc("装備アイテムの移動高低差。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_JMP = 99, // 0x00000063
      [GameParameter.ParameterDesc("装備アイテムの射程。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_RANGE = 100, // 0x00000064
      [GameParameter.ParameterDesc("装備アイテムの範囲。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_SCOPE = 101, // 0x00000065
      [GameParameter.ParameterDesc("装備アイテムの影響可能な高低差範囲。値が0の時、子供を非表示にし、LayoutElementを無効にします。"), GameParameter.AlwaysUpdate] EQUIPMENT_EFFECTHEIGHT = 102, // 0x00000066
      [GameParameter.ParameterDesc("装備アイテムの名前")] EQUIPMENT_NAME = 103, // 0x00000067
      [GameParameter.ParameterDesc("装備アイテムのアイコン")] EQUIPMENT_ICON = 104, // 0x00000068
      [GameParameter.ParameterDesc("アビリティ強化に使用できるポイントの残り")] OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_NUM = 105, // 0x00000069
      [GameParameter.ParameterDesc("アビリティを強化できる回数")] OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNT = 106, // 0x0000006A
      [GameParameter.ParameterDesc("アビリティを強化できる回数の最大値")] OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNTMAX = 107, // 0x0000006B
      [GameParameter.ParameterDesc("アビリティを強化できる回数のクールダウン時間。\nクールダウン時間が無い場合は子供を非表示にします。"), GameParameter.AlwaysUpdate] OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_COOLDOWNTIME = 108, // 0x0000006C
      [GameParameter.ParameterDesc("装備アイテムの所持数")] EQUIPMENT_AMOUNT = 109, // 0x0000006D
      [GameParameter.ParameterDesc("装備アイテムを装備するために必要なレベル")] EQUIPMENT_REQLV = 110, // 0x0000006E
      [GameParameter.ParameterDesc("進化素材の所持個数。スライダー対応")] JOBEVOITEM_AMOUNT = 111, // 0x0000006F
      [GameParameter.ParameterDesc("進化素材の必要個数")] JOBEVOITEM_REQAMOUNT = 112, // 0x00000070
      [GameParameter.ParameterDesc("進化素材のアイコン")] JOBEVOITEM_ICON = 113, // 0x00000071
      [GameParameter.ParameterDesc("進化素材の名前")] JOBEVOITEM_NAME = 114, // 0x00000072
      [GameParameter.ParameterDesc("ユニットの現在のジョブを進化させるのに必要なゴールド")] UNIT_EVOCOST = 115, // 0x00000073
      [GameParameter.ParameterDesc("ユニットのクリティカル値"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_CRIT = 116, // 0x00000074
      [GameParameter.ParameterDesc("ユニットの回復力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_REGEN = 117, // 0x00000075
      [GameParameter.ParameterDesc("ユニットが持つリーダースキルの名前"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_LEADERSKILLNAME = 118, // 0x00000076
      [GameParameter.ParameterDesc("ユニットが持つリーダースキルの説明"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_LEADERSKILLDESC = 119, // 0x00000077
      [GameParameter.ParameterDesc("アイテムの効果値")] ITEM_VALUE = 120, // 0x00000078
      [GameParameter.ParameterDesc("ユニットのレベルの最大値")] UNIT_LEVELMAX = 121, // 0x00000079
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブの解放状態にあわせてAnimatorにBoolパラメーターunlockedを設定します。解放済みであればunlockedがオンになります。"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_UNLOCKSTATE = 122, // 0x0000007A
      [GameParameter.ParameterDesc("ユニットの現在のジョブのランク"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBRANKMAX = 123, // 0x0000007B
      [GameParameter.ParameterDesc("アビリティの解放条件")] ABILITY_UNLOCKINFO = 124, // 0x0000007C
      [GameParameter.ParameterDesc("アビリティの説明")] ABILITY_DESC = 125, // 0x0000007D
      [GameParameter.ParameterDesc("アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_FRAME = 126, // 0x0000007E
      [GameParameter.ParameterDesc("インベントリーにセットされたアイテムのフレーム。Item/Frameと同じです。"), GameParameter.UsesIndex] INVENTORY_FRAME = 127, // 0x0000007F
      [GameParameter.ParameterDesc("アイテム作成素材の所持個数")] RECIPEITEM_AMOUNT = 128, // 0x00000080
      [GameParameter.ParameterDesc("アイテム作成素材の必要個数")] RECIPEITEM_REQAMOUNT = 129, // 0x00000081
      [GameParameter.ParameterDesc("アイテム作成素材のアイコン")] RECIPEITEM_ICON = 130, // 0x00000082
      [GameParameter.ParameterDesc("アイテム作成素材の名前")] RECIPEITEM_NAME = 131, // 0x00000083
      [GameParameter.ParameterDesc("アイテム作成コスト")] RECIPEITEM_CREATE_COST = 132, // 0x00000084
      [GameParameter.ParameterDesc("作成アイテム名")] RECIPEITEM_CREATE_ITEM_NAME = 133, // 0x00000085
      [GameParameter.ParameterDesc("アイテム作成素材のフレーム")] RECIPEITEM_FRAME = 134, // 0x00000086
      [GameParameter.ParameterDesc("ユニットのキャライメージ (中サイズ) GameSettings.Unit_PortraitMedium で命名規則を決めれます。"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_PORTRAIT_MEDIUM = 135, // 0x00000087
      [GameParameter.ParameterDesc("クエストで入手した補正値等も含めたゴールドの合計")] QUESTRESULT_GOLD = 136, // 0x00000088
      [GameParameter.ParameterDesc("クエストでプレイヤーが得た経験値")] QUESTRESULT_PLAYEREXP = 137, // 0x00000089
      [GameParameter.ParameterDesc("クエストでパーティが得た経験値")] QUESTRESULT_PARTYEXP = 138, // 0x0000008A
      [GameParameter.ParameterDesc("クエストの評価結果にあわせてAnimatorのrate(int)、ImageArrayを切り替えます。※使用してない")] QUESTRESULT_RATE = 139, // 0x0000008B
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ前のレベル")] PLAYERLEVELUP_LEVEL = 140, // 0x0000008C
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ後のレベル")] PLAYERLEVELUP_LEVELNEXT = 141, // 0x0000008D
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ後のスタミナ")] PLAYERLEVELUP_STAMINA = 142, // 0x0000008E
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ前の最大スタミナ")] PLAYERLEVELUP_STAMINAMAX = 143, // 0x0000008F
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ後の最大スタミナ")] PLAYERLEVELUP_STAMINAMAXNEXT = 144, // 0x00000090
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ前の最大フレンドスロット数")] PLAYERLEVELUP_FRIENDNUM = 145, // 0x00000091
      [GameParameter.ParameterDesc("プレイヤーのレベルアップ後の最大フレンドスロット数")] PLAYERLEVELUP_FRIENDNUMNEXT = 146, // 0x00000092
      [GameParameter.ParameterDesc("アンロックされた物の説明。インデックスで行数を指定してください。"), GameParameter.UsesIndex] PLAYERLEVELUP_UNLOCKINFO = 147, // 0x00000093
      [GameParameter.EnumParameterDesc("プレイ中クエストのボーナス条件の達成状態にあわせてAnimatorのstate(int)、ImageArrayを切り替えます。インデックスでボーナス条件の番号を指定してください。", typeof (QuestBonusObjectiveState)), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE_STATE = 148, // 0x00000094
      OBSOLETE_QUEST_BONUSOBJECTIVE_ITEMICON = 149, // 0x00000095
      OBSOLETE_QUEST_BONUSOBJECTIVE_ITEMAMOUNT = 150, // 0x00000096
      [GameParameter.EnumParameterDesc("ユニットの陣営にあわせてImageArrayのインデックス、Animatorのindex(int)を切り替えます。", typeof (EUnitSide))] UNIT_SIDE = 151, // 0x00000097
      [GameParameter.ParameterDesc("ユニットのジョブのアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBICON = 152, // 0x00000098
      [GameParameter.ParameterDesc("ユニットの現ジョブのアイコン。GameSettingsのJobIcon2のパスを使用する。"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBICON2 = 153, // 0x00000099
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブのアイコン。GameSettingsのJobIcon2のパスを使用する。"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_JOBICON2 = 154, // 0x0000009A
      [GameParameter.ParameterDesc("アイテムの作成コスト"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_CREATECOST = 155, // 0x0000009B
      [GameParameter.ParameterDesc("プレイヤーの現在の洞窟用スタミナ")] GLOBAL_PLAYER_CAVESTAMINA = 156, // 0x0000009C
      [GameParameter.ParameterDesc("プレイヤーの最大の洞窟用スタミナ")] GLOBAL_PLAYER_CAVESTAMINAMAX = 157, // 0x0000009D
      [GameParameter.ParameterDesc("プレイヤーの洞窟用スタミナが次に回復するまでの時間")] GLOBAL_PLAYER_CAVESTAMINATIME = 158, // 0x0000009E
      [GameParameter.ParameterDesc("アイテムの種類ごとの所持上限"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_AMOUNTMAX = 159, // 0x0000009F
      [GameParameter.ParameterDesc("所持しているアイテムの種類")] PLAYER_NUMITEMS = 160, // 0x000000A0
      [GameParameter.ParameterDesc("クエストが通常クエストかエリートクエストかどうかでImageArrayのインデックスを切り替えます。0=通常、1=エリート、2=エクストラ"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_DIFFICULTY = 161, // 0x000000A1
      [GameParameter.ParameterDesc("ユニットの現在位置の高さ"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_HEIGHT = 162, // 0x000000A2
      [GameParameter.ParameterDesc("装備アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。"), GameParameter.UsesIndex] EQUIPMENT_FRAME = 163, // 0x000000A3
      [GameParameter.ParameterDesc("クエストリストで使用するチャプター(章)の名前")] QUESTLIST_CHAPTERNAME = 164, // 0x000000A4
      [GameParameter.ParameterDesc("クエストリストで使用するセクション(部)の名前")] QUESTLIST_SECTIONNAME = 165, // 0x000000A5
      [GameParameter.ParameterDesc("メールの文面")] MAIL_MESSAGE = 166, // 0x000000A6
      [GameParameter.ParameterDesc("マルチクエストが通常マップかイベントマップかどうかでImageArrayのインデックスを切り替えます。0=通常、1=イベント"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_MULTI_TYPE = 167, // 0x000000A7
      [GameParameter.ParameterDesc("マルチプレイヤーの名前")] MULTI_PLAYER_NAME = 168, // 0x000000A8
      [GameParameter.ParameterDesc("マルチプレイヤーのレベル")] MULTI_PLAYER_LEVEL = 169, // 0x000000A9
      [GameParameter.ParameterDesc("マルチプレイヤーの状態( Index: 0 == 否定 / 1 == 完全一致"), GameParameter.AlwaysUpdate, GameParameter.InstanceTypes(typeof (JSON_MyPhotonPlayerParam.EState)), GameParameter.UsesIndex] MULTI_PLAYER_STATE = 170, // 0x000000AA
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットアイコン"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_ICON = 171, // 0x000000AB
      [GameParameter.ParameterDesc("メールで付与されるアイテムの文字列表現")] MAIL_GIFT_STRING = 172, // 0x000000AC
      [GameParameter.ParameterDesc("メールの有効期限")] MAIL_GIFT_LIMIT = 173, // 0x000000AD
      [GameParameter.ParameterDesc("メールを既読にした日時")] MAIL_GIFT_GETAT = 174, // 0x000000AE
      [GameParameter.ParameterDesc("マルチ部屋リストのコメント")] MULTI_ROOM_LIST_COMMENT = 175, // 0x000000AF
      [GameParameter.ParameterDesc("マルチ部屋リストの部屋主名")] MULTI_ROOM_LIST_OWNER_NAME = 176, // 0x000000B0
      [GameParameter.ParameterDesc("マルチ部屋リストの部屋主レベル")] MULTI_ROOM_LIST_OWNER_LV = 177, // 0x000000B1
      [GameParameter.ParameterDesc("マルチ部屋リストのクエスト名")] MULTI_ROOM_LIST_QUEST_NAME = 178, // 0x000000B2
      [GameParameter.ParameterDesc("マルチ部屋リストの鍵アイコン. 0:鍵あり 1:鍵なし"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_ROOM_LIST_LOCKED_ICON = 179, // 0x000000B3
      [GameParameter.ParameterDesc("マルチ部屋リストの参加人数")] MULTI_ROOM_LIST_PLAYER_NUM = 180, // 0x000000B4
      [GameParameter.ParameterDesc("プレイヤーのフレンドコード")] GLOBAL_PLAYER_FRIENDCODE = 181, // 0x000000B5
      [GameParameter.ParameterDesc("フレンドのフレンドコード")] FRIEND_FRIENDCODE = 182, // 0x000000B6
      [GameParameter.ParameterDesc("フレンドの名前")] FRIEND_NAME = 183, // 0x000000B7
      [GameParameter.ParameterDesc("フレンドのレベル")] FRIEND_LEVEL = 184, // 0x000000B8
      [GameParameter.ParameterDesc("フレンドの最終ログイン")] FRIEND_LASTLOGIN = 185, // 0x000000B9
      [GameParameter.ParameterDesc("所持可能の最大アイテム数")] PLAYER_MAXITEMS = 186, // 0x000000BA
      [GameParameter.ParameterDesc("売却アイテムの選択数分の価格")] SHOP_ITEM_SELLPRICE = 187, // 0x000000BB
      [GameParameter.ParameterDesc("売却アイテムの数")] SHOP_ITEM_SELLNUM = 188, // 0x000000BC
      [GameParameter.ParameterDesc("売却アイテムの選択インデックス")] SHOP_ITEM_SELLINDEX = 189, // 0x000000BD
      [GameParameter.ParameterDesc("売却アイテムの選択数")] SHOP_ITEM_SELLSELECTCOUNT = 190, // 0x000000BE
      [GameParameter.ParameterDesc("ショップ総売却価格")] SHOP_SELLPRICETOTAL = 191, // 0x000000BF
      [GameParameter.ParameterDesc("ショップアイテムのインベントリ設定状態で表示状態を切り替え"), GameParameter.AlwaysUpdate] SHOP_ITEM_STATE_INVENTORY = 192, // 0x000000C0
      [GameParameter.ParameterDesc("ショップアイテムの設置数を取得")] SHOP_ITEM_BUYAMOUNT = 193, // 0x000000C1
      [GameParameter.ParameterDesc("ショップアイテムの購入総額を取得")] SHOP_ITEM_BUYPRICE = 194, // 0x000000C2
      [GameParameter.ParameterDesc("ショップアイテムの売却済み状態で表示状態を切り替え"), GameParameter.AlwaysUpdate] SHOP_ITEM_STATE_SOLDOUT = 195, // 0x000000C3
      [GameParameter.ParameterDesc("ショップアイテムの購入通貨別のアイコン"), GameParameter.UsesIndex] SHOP_ITEM_BUYTYPEICON = 196, // 0x000000C4
      [GameParameter.ParameterDesc("ショップアイテムの売却選択状態で表示状態を切り替え"), GameParameter.AlwaysUpdate] SHOP_ITEM_STATE_SELLSELECT = 197, // 0x000000C5
      [GameParameter.ParameterDesc("ショップアイテムのアイコン上の売却数")] SHOP_ITEM_ICONSELLNUM = 198, // 0x000000C6
      [GameParameter.ParameterDesc("装備可能ユニットが存在する場合のバッジアイコンの表示状態の切り替え"), GameParameter.AlwaysUpdate] SHOP_ITEM_STATE_ENABLEEQUIPUNIT = 199, // 0x000000C7
      [GameParameter.ParameterDesc("ショップアイテムの商品一覧の更新時間")] SHOP_ITEM_UPDATETIME = 200, // 0x000000C8
      [GameParameter.ParameterDesc("プレイヤーに来ているフレンド申請通知の数")] PLAYER_FRIENDREQUESTNUM = 201, // 0x000000C9
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブのクラスチェンジ先のジョブのアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_CLASSCHANGE_JOBICON = 202, // 0x000000CA
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブの名前"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_CLASSCHANGE_NAME = 203, // 0x000000CB
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブのクラスチェンジ先のジョブのアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.UsesIndex] UNIT_JOB_CLASSCHANGE_JOBICON2 = 204, // 0x000000CC
      [GameParameter.ParameterDesc("ショップアイテムのアイコン上の売却数表示切り替え"), GameParameter.AlwaysUpdate] SHOP_ITEM_ICONSELLNUMSHOWED = 205, // 0x000000CD
      [GameParameter.ParameterDesc("プレイヤーのフレンド保持上限")] PLAYER_FRIENDMAX = 206, // 0x000000CE
      [GameParameter.ParameterDesc("プレイヤーの保持しているフレンドの数")] PLAYER_FRIENDNUM = 207, // 0x000000CF
      [GameParameter.ParameterDesc("ユニットの長い説明文"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_PROFILETEXT = 208, // 0x000000D0
      [GameParameter.ParameterDesc("ユニットのイメージ画像"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_IMAGE = 209, // 0x000000D1
      [GameParameter.ParameterDesc("マルチ部屋リストの募集人数")] MULTI_ROOM_LIST_PLAYER_NUM_MAX = 210, // 0x000000D2
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットアイコンフレーム"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_ICON_FRAME = 211, // 0x000000D3
      [GameParameter.ParameterDesc("マルチプレイヤーのプレイヤーID")] MULTI_PLAYER_INDEX = 212, // 0x000000D4
      [GameParameter.ParameterDesc("マルチプレイヤーが部屋主のときに表示"), GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_ROOM_OWNER = 213, // 0x000000D5
      [GameParameter.ParameterDesc("マルチプレイヤーがいないときに表示"), GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_EMPTY = 214, // 0x000000D6
      [GameParameter.ParameterDesc("マルチプレイヤーがいるときに表示"), GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_VALID = 215, // 0x000000D7
      [GameParameter.ParameterDesc("実績の名前")] TROPHY_NAME = 216, // 0x000000D8
      [GameParameter.ParameterDesc("共闘マルチのとき表示"), GameParameter.AlwaysUpdate] MULTI_ROOM_TYPE_IS_RAID = 217, // 0x000000D9
      [GameParameter.ParameterDesc("対戦マルチのとき表示"), GameParameter.AlwaysUpdate] MULTI_ROOM_TYPE_IS_VERSUS = 218, // 0x000000DA
      [GameParameter.ParameterDesc("マルチパーティの総攻撃力")] MULTI_PARTY_TOTALATK = 219, // 0x000000DB
      [GameParameter.ParameterDesc("現在のユニット操作プレイヤーID"), GameParameter.AlwaysUpdate] MULTI_CURRENT_PLAYER_INDEX = 220, // 0x000000DC
      [GameParameter.ParameterDesc("自キャラ行動までの残りターン"), GameParameter.AlwaysUpdate] MULTI_MY_NEXT_TURN = 221, // 0x000000DD
      [GameParameter.ParameterDesc("残りの入力制限時間"), GameParameter.AlwaysUpdate] MULTI_INPUT_TIME_LIMIT = 222, // 0x000000DE
      [GameParameter.ParameterDesc("現在のユニット操作プレイヤー名"), GameParameter.AlwaysUpdate] MULTI_CURRENT_PLAYER_NAME = 223, // 0x000000DF
      [GameParameter.ParameterDesc("鍵つき部屋を作るとき表示"), GameParameter.AlwaysUpdate] QUEST_MULTI_LOCK = 224, // 0x000000E0
      [GameParameter.ParameterDesc("現在の部屋コメント"), GameParameter.AlwaysUpdate] MULTI_CURRENT_ROOM_COMMENT = 225, // 0x000000E1
      [GameParameter.ParameterDesc("現在の部屋パスコード/0 == 半角 / 1 == 全角"), GameParameter.UsesIndex] MULTI_CURRENT_ROOM_PASSCODE = 226, // 0x000000E2
      [GameParameter.ParameterDesc("ユニットが不参加スロット枠のとき表示"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE = 227, // 0x000000E3
      [GameParameter.ParameterDesc("現在の部屋のクエスト名"), GameParameter.AlwaysUpdate] MULTI_CURRENT_ROOM_QUEST_NAME = 228, // 0x000000E4
      [GameParameter.ParameterDesc("マルチプレイのとき非表示(0)/表示(1)/NotInteractive(2)/Interactive(3)"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] QUEST_IS_MULTI = 229, // 0x000000E5
      [GameParameter.ParameterDesc("実績の条件のテキスト"), GameParameter.InstanceTypes(typeof (GameParameter.TrophyConditionInstances)), GameParameter.UsesIndex] TROPHY_CONDITION_TITLE = 230, // 0x000000E6
      [GameParameter.ParameterDesc("実績の条件のカウント、スライダーにもできるよ"), GameParameter.InstanceTypes(typeof (GameParameter.TrophyConditionInstances)), GameParameter.UsesIndex] TROPHY_CONDITION_COUNT = 231, // 0x000000E7
      [GameParameter.ParameterDesc("実績の条件の必要カウント"), GameParameter.InstanceTypes(typeof (GameParameter.TrophyConditionInstances)), GameParameter.UsesIndex] TROPHY_CONDITION_COUNTMAX = 232, // 0x000000E8
      [GameParameter.ParameterDesc("アイテムの素材経験値"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes))] ITEM_ENHANCEPOINT = 233, // 0x000000E9
      [GameParameter.ParameterDesc("装備アイテムの強化素材の選択数")] EQUIPITEM_ENHANCE_MATERIALSELECTCOUNT = 234, // 0x000000EA
      [GameParameter.ParameterDesc("アイテム所持数によって表示状態を変更（0個の場合非表示）"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.AlwaysUpdate] ITEM_SHOWED_AMOUNT = 235, // 0x000000EB
      [GameParameter.ParameterDesc("強化パラメータ名")] EQUIPITEM_PARAMETER_NAME = 236, // 0x000000EC
      [GameParameter.ParameterDesc("装備アイテムの初期値")] EQUIPITEM_PARAMETER_INITVALUE = 237, // 0x000000ED
      [GameParameter.ParameterDesc("装備アイテムの上昇値")] EQUIPITEM_PARAMETER_RANKUPVALUE = 238, // 0x000000EE
      [GameParameter.ParameterDesc("装備アイテムの上昇量に応じて表示状態を変更"), GameParameter.AlwaysUpdate] EQUIPITEM_PARAMETER_SHOWED_RANKUPVALUE = 239, // 0x000000EF
      [GameParameter.ParameterDesc("装備アイテムの強化素材の選択個数によって表示状態を変更（選択数が0の場合は非表示）"), GameParameter.AlwaysUpdate] EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECTCOUNT = 240, // 0x000000F0
      [GameParameter.ParameterDesc("装備アイテムの強化素材の選択状態によって表示状態を変更（選択していない場合は非表示）"), GameParameter.AlwaysUpdate] EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECT = 241, // 0x000000F1
      [GameParameter.ParameterDesc("装備アイテムの強化ゲージ")] EQUIPITEM_ENHANCE_GAUGE = 242, // 0x000000F2
      [GameParameter.ParameterDesc("装備アイテムの現在の強化ポイント")] EQUIPITEM_ENHANCE_CURRENTEXP = 243, // 0x000000F3
      [GameParameter.ParameterDesc("装備アイテムのランクアップまでの強化ポイント")] EQUIPITEM_ENHANCE_NEXTEXP = 244, // 0x000000F4
      [GameParameter.ParameterDesc("装備アイテムの強化前のランク")] EQUIPITEM_ENHANCE_RANKBEFORE = 245, // 0x000000F5
      [GameParameter.ParameterDesc("装備アイテムの強化後のランク")] EQUIPITEM_ENHANCE_RANKAFTER = 246, // 0x000000F6
      [GameParameter.ParameterDesc("装備アイテムのランクに応じたイメージを使用します"), GameParameter.AlwaysUpdate] EQUIPDATA_RANKBADGE = 247, // 0x000000F7
      [GameParameter.ParameterDesc("【使用禁止】機能がアンロックされている場合のみ表示"), GameParameter.InstanceTypes(typeof (UnlockTargets))] DONTUSE_UNLOCK_SHOWED = 248, // 0x000000F8
      [GameParameter.ParameterDesc("切断されたプレイヤーIndex")] MULTI_NOTIFY_DISCONNECTED_PLAYER_INDEX = 249, // 0x000000F9
      [GameParameter.ParameterDesc("切断されたプレイヤーが(0:部屋主じゃなかったとき表示 1:他人が部屋主になったとき表示 2:自分が部屋主になったとき表示)"), GameParameter.UsesIndex] MULTI_NOTIFY_DISCONNECTED_PLAYER_IS_ROOM_OWNER = 250, // 0x000000FA
      [GameParameter.ParameterDesc("行動順のプレイヤーが切断されているとき表示(0) 非表示(1)"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_PLAYER_IS_DISCONNECTED = 251, // 0x000000FB
      [GameParameter.ParameterDesc("行動順のプレイヤーが部屋主かどうか"), GameParameter.AlwaysUpdate] MULTI_CURRENT_PLAYER_IS_ROOM_OWNER = 252, // 0x000000FC
      [GameParameter.ParameterDesc("自分が部屋主のとき表示(0) 非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_I_AM_ROOM_OWNER = 253, // 0x000000FD
      [GameParameter.ParameterDesc("部屋主のプレイヤーIndex"), GameParameter.AlwaysUpdate] MULTI_ROOM_OWNER_PLAYER_INDEX = 254, // 0x000000FE
      [GameParameter.ParameterDesc("ガチャでドロップしたものの名称")] GACHA_DROPNAME = 255, // 0x000000FF
      [GameParameter.ParameterDesc("達成済みデイリーミッションの有無で表示状態を切り替える"), GameParameter.InstanceTypes(typeof (GameParameter.TrophyBadgeInstanceTypes)), GameParameter.AlwaysUpdate] TROPHY_BADGE = 256, // 0x00000100
      [GameParameter.ParameterDesc("実績の報酬ゴールド。ゴールドが0なら自身を非表示にする。"), GameParameter.AlwaysUpdate] TROPHY_REWARDGOLD = 257, // 0x00000101
      [GameParameter.ParameterDesc("実績の報酬コイン。コインが0なら自身を非表示にする。"), GameParameter.AlwaysUpdate] TROPHY_REWARDCOIN = 258, // 0x00000102
      [GameParameter.ParameterDesc("実績の報酬プレイヤー経験値。経験値が0なら自身を非表示にする。"), GameParameter.AlwaysUpdate] TROPHY_REWARDEXP = 259, // 0x00000103
      [GameParameter.ParameterDesc("報酬に含まれる経験値")] REWARD_EXP = 260, // 0x00000104
      [GameParameter.ParameterDesc("報酬に含まれるコイン")] REWARD_COIN = 261, // 0x00000105
      [GameParameter.ParameterDesc("報酬に含まれるゴールド")] REWARD_GOLD = 262, // 0x00000106
      [GameParameter.ParameterDesc("ユニットのお気に入りロック状態"), GameParameter.AlwaysUpdate] UNIT_FAVORITE = 263, // 0x00000107
      [GameParameter.ParameterDesc("装備アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] EQUIPDATA_FRAME = 264, // 0x00000108
      [GameParameter.ParameterDesc("ジョブのランクにあわせてImageArrayを切り替えます"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBRANKFRAME = 265, // 0x00000109
      [GameParameter.ParameterDesc("ローカルプレイヤーのレベルによってキャップされたユニットの最大レベル"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_CAPPEDLEVELMAX = 266, // 0x0000010A
      [GameParameter.ParameterDesc("リビジョン番号")] APPLICATION_REVISION = 267, // 0x0000010B
      [GameParameter.ParameterDesc("ビルドID")] APPLICATION_BUILD = 268, // 0x0000010C
      [GameParameter.ParameterDesc("アセットのリビジョン番号")] APPLICATION_ASSETREVISION = 269, // 0x0000010D
      [GameParameter.ParameterDesc("プロダクト名称")] PRODUCT_NAME = 270, // 0x0000010E
      [GameParameter.ParameterDesc("プロダクト値段")] PRODUCT_PRICE = 271, // 0x0000010F
      [GameParameter.ParameterDesc("アリーナプレイヤーの順位"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_RANK = 272, // 0x00000110
      [GameParameter.ParameterDesc("アリーナプレイヤーの総攻撃力"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_TOTALATK = 273, // 0x00000111
      [GameParameter.ParameterDesc("アリーナプレイヤーのリーダースキル"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_LEADERSKILLNAME = 274, // 0x00000112
      [GameParameter.ParameterDesc("アリーナプレイヤーのリーダースキルの説明"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_LEADERSKILLDESC = 275, // 0x00000113
      [GameParameter.ParameterDesc("アリーナプレイヤーの名前"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_NAME = 276, // 0x00000114
      [GameParameter.ParameterDesc("プレイヤーのアリーナランク")] GLOBAL_PLAYER_ARENARANK = 277, // 0x00000115
      [GameParameter.ParameterDesc("チケット数")] QUEST_TICKET = 278, // 0x00000116
      [GameParameter.ParameterDesc("チケット使用可能な場合のみ表示"), GameParameter.AlwaysUpdate] QUEST_IS_TICKET = 279, // 0x00000117
      [GameParameter.ParameterDesc("アリーナの挑戦権")] GLOBAL_PLAYER_ARENATICKETS = 280, // 0x00000118
      [GameParameter.ParameterDesc("アリーナのクールダウンタイム")] GLOBAL_PLAYER_ARENACOOLDOWNTIME = 281, // 0x00000119
      [GameParameter.ParameterDesc("本日のマルチプレイ残り報酬獲得回数")] MULTI_REST_REWARD = 282, // 0x0000011A
      [GameParameter.ParameterDesc("ユニットが不参加スロット枠のとき押せない"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE_NOT_INTERACTIVE = 283, // 0x0000011B
      [GameParameter.ParameterDesc("お客様コード")] GLOBAL_PLAYER_OKYAKUSAMACODE = 284, // 0x0000011C
      [GameParameter.ParameterDesc("【使用禁止】機能がアンロックされている場合のみ有効"), GameParameter.InstanceTypes(typeof (UnlockTargets))] DONTUSE_UNLOCK_ENABLED = 285, // 0x0000011D
      [GameParameter.ParameterDesc("【使用禁止】機能がアンロックされていると表示されなくなる"), GameParameter.InstanceTypes(typeof (UnlockTargets)), GameParameter.AlwaysUpdate] DONTUSE_UNLOCK_HIDDEN = 286, // 0x0000011E
      [GameParameter.ParameterDesc("報酬に含まれるアリーナメダル")] REWARD_ARENAMEDAL = 287, // 0x0000011F
      [GameParameter.ParameterDesc("ショップアイテムの商品一覧の更新日")] SHOP_ITEM_UPDATEDAY = 288, // 0x00000120
      [GameParameter.ParameterDesc("アリーナプレイヤーのレベル"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_LEVEL = 289, // 0x00000121
      [GameParameter.ParameterDesc("プレイヤーのVIPランク")] GLOBAL_PLAYER_VIPRANK = 290, // 0x00000122
      [GameParameter.ParameterDesc("ユニットの装備品を更新"), GameParameter.UsesIndex] UNIT_EQUIPSLOT_UPDATE = 291, // 0x00000123
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態でのアイコン表示")] UNITPARAM_ICON = 292, // 0x00000124
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態でのレアリティ")] UNITPARAM_RARITY = 293, // 0x00000125
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態でのジョブアイコン")] UNITPARAM_JOBICON = 294, // 0x00000126
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態での欠片所持数")] UNITPARAM_PIECE_AMOUNT = 295, // 0x00000127
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態での欠片必要数")] UNITPARAM_PIECE_NEED = 296, // 0x00000128
      [GameParameter.ParameterDesc("ユニットパラメータ指定の初期状態での欠片ゲージ")] UNITPARAM_PIECE_GAUGE = 297, // 0x00000129
      [GameParameter.ParameterDesc("ユニットパラメータ指定でアンロック可能か確認"), GameParameter.AlwaysUpdate] UNITPARAM_IS_UNLOCKED = 298, // 0x0000012A
      [GameParameter.ParameterDesc("クエストで入手可能な欠片のフレーム"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_KAKERA_FRAME = 299, // 0x0000012B
      [GameParameter.ParameterDesc("ユニットの連携値"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_COMBINATION = 300, // 0x0000012C
      [GameParameter.ParameterDesc("ユニットのジョブ変更可能か確認"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_JOBCHANGED = 301, // 0x0000012D
      [GameParameter.ParameterDesc("ショップの主要通貨の表示状態"), GameParameter.AlwaysUpdate] SHOP_STATE_MAINCOSTFRAME = 302, // 0x0000012E
      [GameParameter.ParameterDesc("ショップの主要通貨アイコン")] SHOP_MAINCOST_ICON = 303, // 0x0000012F
      [GameParameter.ParameterDesc("ショップの主要通貨の所持量")] SHOP_MAINCOST_AMOUNT = 304, // 0x00000130
      [GameParameter.ParameterDesc("対象ユニットの成長バッジ"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_BADGE_GROWUP = 305, // 0x00000131
      [GameParameter.ParameterDesc("対象ユニットの解放バッジ"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNITPARAM_BADGE_UNLOCK = 306, // 0x00000132
      [GameParameter.ParameterDesc("アイテムで装備可能なユニットが存在する場合に表示状態を変更するバッジ"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.AlwaysUpdate] ITEM_BADGE_EQUIPUNIT = 307, // 0x00000133
      [GameParameter.ParameterDesc("ユニットのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_UNIT = 308, // 0x00000134
      [GameParameter.ParameterDesc("ユニット強化のバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_UNITENHANCED = 309, // 0x00000135
      [GameParameter.ParameterDesc("ユニット開放のバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_UNITUNLOCKED = 310, // 0x00000136
      [GameParameter.ParameterDesc("ガチャのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_GACHA = 311, // 0x00000137
      [GameParameter.ParameterDesc("ゴールドガチャのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_GOLDGACHA = 312, // 0x00000138
      [GameParameter.ParameterDesc("レアガチャのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_RAREGACHA = 313, // 0x00000139
      [GameParameter.ParameterDesc("アリーナのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_ARENA = 314, // 0x0000013A
      [GameParameter.ParameterDesc("マルチプレイのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_MULTIPLAY = 315, // 0x0000013B
      [GameParameter.ParameterDesc("デイリーミッションのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_DAILYMISSION = 316, // 0x0000013C
      [GameParameter.ParameterDesc("フレンドのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_FRIEND = 317, // 0x0000013D
      [GameParameter.ParameterDesc("ギフトのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_GIFTBOX = 318, // 0x0000013E
      [GameParameter.ParameterDesc("ショートカットメニューのバッジ表示状態を変更"), GameParameter.AlwaysUpdate] BADGE_SHORTCUTMENU = 319, // 0x0000013F
      [GameParameter.ParameterDesc("現VIPランクにおけるVIPポイント。スライダー対応")] GLOBAL_PLAYER_VIPPOINT = 320, // 0x00000140
      [GameParameter.ParameterDesc("現VIPランクにおける最大VIPポイント")] GLOBAL_PLAYER_VIPPOINTMAX = 321, // 0x00000141
      [GameParameter.ParameterDesc("プレイヤーの所持コイン (固有無償幻晶石)")] GLOBAL_PLAYER_COINFREE = 322, // 0x00000142
      [GameParameter.ParameterDesc("プレイヤーの所持コイン (固有有償幻晶石)")] GLOBAL_PLAYER_COINPAID = 323, // 0x00000143
      [GameParameter.ParameterDesc("ユニットが編成中のパーティメンバーか？"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_PARTYMEMBER = 324, // 0x00000144
      [GameParameter.ParameterDesc("ログインボーナスの日付")] LOGINBONUS_DAYNUM = 325, // 0x00000145
      None = 326, // 0x00000146
      [GameParameter.ParameterDesc("ユニットがレベルソート中か？"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_LVSORT = 327, // 0x00000147
      [GameParameter.ParameterDesc("ユニットがパラメータソート中か？"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_PARAMSORT = 328, // 0x00000148
      [GameParameter.ParameterDesc("ユニットのソート対象パラメータの値"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_SORTTYPE_VALUE = 329, // 0x00000149
      [GameParameter.ParameterDesc("スキルの修得条件の表示有無"), GameParameter.AlwaysUpdate] SKILL_STATE_CONDITION = 330, // 0x0000014A
      [GameParameter.ParameterDesc("スキルの修得条件")] SKILL_CONDITION = 331, // 0x0000014B
      [GameParameter.ParameterDesc("アビリティの修得条件")] ABILITY_CONDITION = 332, // 0x0000014C
      [GameParameter.ParameterDesc("ガチャのコスト")] GACHA_COST = 333, // 0x0000014D
      [GameParameter.ParameterDesc("ガチャの回数")] GACHA_NUM = 334, // 0x0000014E
      [GameParameter.ParameterDesc("無料通常ガチャの残り回数")] GACHA_GOLD_RESTNUM = 335, // 0x0000014F
      [GameParameter.ParameterDesc("無料通常ガチャの残り回数の表示状態変更")] GACHA_GOLD_STATE_RESTNUM = 336, // 0x00000150
      [GameParameter.ParameterDesc("無料通常ガチャのインターバル時間表示"), GameParameter.AlwaysUpdate] GACHA_GOLD_TIMER = 337, // 0x00000151
      [GameParameter.ParameterDesc("無料通常ガチャの状態によって表示状態変更"), GameParameter.AlwaysUpdate] GACHA_GOLD_STATE_TIMER = 338, // 0x00000152
      [GameParameter.ParameterDesc("無料通常ガチャのボタン状態変更"), GameParameter.AlwaysUpdate] GACHA_GOLD_STATE_INTERACTIVE = 339, // 0x00000153
      [GameParameter.ParameterDesc("無料レアガチャのインターバル時間表示"), GameParameter.AlwaysUpdate] GACHA_COIN_TIMER = 340, // 0x00000154
      [GameParameter.ParameterDesc("無料レアガチャの状態によって表示状態変更"), GameParameter.AlwaysUpdate] GACHA_COIN_STATE_TIMER = 341, // 0x00000155
      [GameParameter.ParameterDesc("無料レアガチャのボタン状態変更"), GameParameter.AlwaysUpdate] GACHA_COIN_STATE_INTERACTIVE = 342, // 0x00000156
      [GameParameter.ParameterDesc("ユニットのイメージ画像2"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_IMAGE2 = 343, // 0x00000157
      [GameParameter.ParameterDesc("アイテムのフレーバーテキスト"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_FLAVOR = 344, // 0x00000158
      [GameParameter.ParameterDesc("ユニット覚醒レベル"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] UNIT_AWAKE = 345, // 0x00000159
      [GameParameter.ParameterDesc("ユニットがフレンドか？"), GameParameter.AlwaysUpdate] SUPPORTER_ISFRIEND = 346, // 0x0000015A
      [GameParameter.ParameterDesc("ユニット覚醒レベル")] SUPPORTER_COST = 347, // 0x0000015B
      [GameParameter.ParameterDesc("サムネイル化されたジョブのアイコンをImageコンポーネントに設定します"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] Unit_ThumbnailedJobIcon = 348, // 0x0000015C
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットジョブランクフレーム"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_JOBRANKFRAME = 349, // 0x0000015D
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットジョブランク"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_JOBRANK = 350, // 0x0000015E
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットジョブアイコン"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_JOBICON = 351, // 0x0000015F
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットレア度"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_RARITY = 352, // 0x00000160
      [GameParameter.ParameterDesc("マルチプレイヤーのユニット属性"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_ELEMENT = 353, // 0x00000161
      [GameParameter.ParameterDesc("マルチプレイヤーのユニットレベル"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_UNIT_LEVEL = 354, // 0x00000162
      [GameParameter.ParameterDesc("実績の報酬スタミナ。スタミナが0なら自身を非表示にする。"), GameParameter.AlwaysUpdate] TROPHY_REWARDSTAMINA = 355, // 0x00000163
      [GameParameter.ParameterDesc("ジョブアイコン")] JOB_JOBICON = 356, // 0x00000164
      [GameParameter.ParameterDesc("ジョブ名")] JOB_NAME = 357, // 0x00000165
      [GameParameter.ParameterDesc("クエストでプレイヤーが得たマルチコイン")] QUESTRESULT_MULTICOIN = 358, // 0x00000166
      [GameParameter.ParameterDesc("本日のマルチプレイ残り報酬獲得回数が0のとき表示(0)/非表示(1)/受け取れたとき表示(2)/受け取れなかったとき表示(3)/今回が最後のうけとりのとき表示(4)"), GameParameter.UsesIndex] MULTI_REST_REWARD_IS_ZERO = 359, // 0x00000167
      [GameParameter.ParameterDesc("ユニットが参加スロット枠のとき表示"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_ROOM_UNIT_SLOT_ENABLE = 360, // 0x00000168
      [GameParameter.ParameterDesc("ユニットが割り当てられているスロット枠のとき表示"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_ROOM_UNIT_SLOT_VALID = 361, // 0x00000169
      [GameParameter.ParameterDesc("報酬に含まれるスタミナ")] REWARD_STAMINA = 362, // 0x0000016A
      [GameParameter.ParameterDesc("当日クエストに挑戦した回数")] QUEST_CHALLENGE_NUM = 363, // 0x0000016B
      [GameParameter.ParameterDesc("当日クエストに挑戦できる限度")] QUEST_CHALLENGE_MAX = 364, // 0x0000016C
      [GameParameter.ParameterDesc("クエストの挑戦回数をリセットした数")] QUEST_RESET_NUM = 365, // 0x0000016D
      [GameParameter.ParameterDesc("クエストの挑戦回数をリセットできる限度")] QUEST_RESET_MAX = 366, // 0x0000016E
      [GameParameter.ParameterDesc("ジョブアイコン2")] JOB_JOBICON2 = 367, // 0x0000016F
      [GameParameter.ParameterDesc("ユニットの国"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_COUNTRY = 368, // 0x00000170
      [GameParameter.ParameterDesc("ユニットの身長"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_HEIGHT = 369, // 0x00000171
      [GameParameter.ParameterDesc("ユニットの体重"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_WEIGHT = 370, // 0x00000172
      [GameParameter.ParameterDesc("ユニットの誕生日"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_BIRTHDAY = 371, // 0x00000173
      [GameParameter.ParameterDesc("ユニットの星座"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_ZODIAC = 372, // 0x00000174
      [GameParameter.ParameterDesc("ユニットの血液型"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_BLOOD = 373, // 0x00000175
      [GameParameter.ParameterDesc("ユニットの好きなもの"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_FAVORITE = 374, // 0x00000176
      [GameParameter.ParameterDesc("ユニットの趣味"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] OBSOLETE_UNIT_PROFILE_HOBBY = 375, // 0x00000177
      [GameParameter.ParameterDesc("ユニットの状態異常【毒】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_STATE_CONDITION_POISON = 376, // 0x00000178
      [GameParameter.ParameterDesc("ユニットの状態異常【麻痺】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_PARALYSED = 377, // 0x00000179
      [GameParameter.ParameterDesc("ユニットの状態異常【スタン】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_STUN = 378, // 0x0000017A
      [GameParameter.ParameterDesc("ユニットの状態異常【睡眠】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_SLEEP = 379, // 0x0000017B
      [GameParameter.ParameterDesc("ユニットの状態異常【魅了】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_CHARM = 380, // 0x0000017C
      [GameParameter.ParameterDesc("ユニットの状態異常【石化】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_STONE = 381, // 0x0000017D
      [GameParameter.ParameterDesc("ユニットの状態異常【暗闇】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_BLINDNESS = 382, // 0x0000017E
      [GameParameter.ParameterDesc("ユニットの状態異常【沈黙】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLESKILL = 383, // 0x0000017F
      [GameParameter.ParameterDesc("ユニットの状態異常【移動禁止】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLEMOVE = 384, // 0x00000180
      [GameParameter.ParameterDesc("ユニットの状態異常【攻撃禁止】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLEATTACK = 385, // 0x00000181
      [GameParameter.ParameterDesc("ユニットの状態異常【ゾンビ化・狂乱】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_ZOMBIE = 386, // 0x00000182
      [GameParameter.ParameterDesc("ユニットの状態異常【死の宣告】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DEATHSENTENCE = 387, // 0x00000183
      [GameParameter.ParameterDesc("ユニットの状態異常【狂化】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_BERSERK = 388, // 0x00000184
      [GameParameter.ParameterDesc("ユニットの状態異常【ノックバック無効】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLEKNOCKBACK = 389, // 0x00000185
      [GameParameter.ParameterDesc("ユニットの状態異常【バフ無効】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLEBUFF = 390, // 0x00000186
      [GameParameter.ParameterDesc("ユニットの状態異常【デバフ無効】"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] UNIT_STATE_CONDITION_DISABLEDEBUFF = 391, // 0x00000187
      [GameParameter.ParameterDesc("ターン表示のユニット陣営フレーム"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate] TURN_UNIT_SIDE_FRAME = 392, // 0x00000188
      [GameParameter.ParameterDesc("JobSetの開放条件")] JOBSET_UNLOCKCONDITION = 393, // 0x00000189
      [GameParameter.ParameterDesc("マルチで自キャラ生存数が0のとき表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_REST_MY_UNIT_IS_ZERO = 394, // 0x0000018A
      [GameParameter.ParameterDesc("マルチ部屋画面で対象プレイヤーが自分のとき0:表示/1:非表示/2:ImageArrayのインデックス切り替え(0=自分 1=他人)/3:チーム編成ボタン/4:情報をみるボタン/5:チーム編成ボタン(マルチ塔)/6:プレイヤーがまだリザルト画面に存在するか/7:ペアマルチ時の2体目のユニットが存在すれば表示"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_ME = 395, // 0x0000018B
      [GameParameter.ParameterDesc("クエストリストで使用するセクション(部)の説明")] QUESTLIST_SECTIONEXPR = 396, // 0x0000018C
      [GameParameter.ParameterDesc("マルチプレイの部屋に鍵がかかっているとき表示(0)/非表示(1)/部屋主かつ鍵ありで表示(2)/部屋主かつ鍵なしで表示(3)"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_CURRENT_ROOM_IS_LOCKED = 397, // 0x0000018D
      [GameParameter.ParameterDesc("メールの受け取り日時")] MAIL_GIFT_RECEIVE = 398, // 0x0000018E
      [GameParameter.ParameterDesc("クエストのタイムリミット")] QUEST_TIMELIMIT = 399, // 0x0000018F
      [GameParameter.ParameterDesc("ユニットの現在のチャージタイム")] UNIT_CHARGETIME = 400, // 0x00000190
      [GameParameter.ParameterDesc("ユニットのチャージタイム")] UNIT_CHARGETIMEMAX = 401, // 0x00000191
      [GameParameter.ParameterDesc("ギミックオブジェクトの説明文")] GIMMICK_DESCRIPTION = 402, // 0x00000192
      [GameParameter.ParameterDesc("プロダクトDesc 0:そのまま 1:前半 2:後半"), GameParameter.UsesIndex] PRODUCT_DESC = 403, // 0x00000193
      [GameParameter.ParameterDesc("プロダクト個数 (x10)")] PRODUCT_NUMX = 404, // 0x00000194
      [GameParameter.ParameterDesc("ユニットの器用さ (ver1.1以降で表示されます)"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_DEX = 405, // 0x00000195
      [GameParameter.ParameterDesc("アーティファクトの名前"), GameParameter.InstanceTypes(typeof (GameParameter.ArtifactInstanceTypes)), GameParameter.UsesIndex] ARTIFACT_NAME = 406, // 0x00000196
      [GameParameter.ParameterDesc("アーティファクトのフレーバーテキスト")] ARTIFACT_DESC = 407, // 0x00000197
      [GameParameter.ParameterDesc("アーティファクトのボーナス条件")] ARTIFACT_SPEC = 408, // 0x00000198
      [GameParameter.ParameterDesc("アーティファクトのレアリティ")] ARTIFACT_RARITY = 409, // 0x00000199
      [GameParameter.ParameterDesc("アーティファクトのレアリティ (最大)")] ARTIFACT_RARITYCAP = 410, // 0x0000019A
      [GameParameter.ParameterDesc("アーティファクトの所持数")] ARTIFACT_NUM = 411, // 0x0000019B
      [GameParameter.ParameterDesc("アーティファクトの売却額")] ARTIFACT_SELLPRICE = 412, // 0x0000019C
      [GameParameter.ParameterDesc("アプリのバンドルバージョン")] APPLICATION_VERSION = 413, // 0x0000019D
      [GameParameter.ParameterDesc("フレンドユニットの最大レベル")] SUPPORTER_UNITCAPPEDLEVELMAX = 414, // 0x0000019E
      [GameParameter.ParameterDesc("欠片ポイント")] GLOBAL_PLAYER_PIECEPOINT = 415, // 0x0000019F
      [GameParameter.ParameterDesc("ショップ欠片ポイント総売却価格")] SHOP_KAKERA_SELLPRICETOTAL = 416, // 0x000001A0
      [GameParameter.ParameterDesc("魂の欠片の売却価格"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_KAKERA_PRICE = 417, // 0x000001A1
      [GameParameter.ParameterDesc("魂の欠片変換の選択数分の価格")] SHOP_ITEM_KAKERA_SELLPRICE = 418, // 0x000001A2
      [GameParameter.ParameterDesc("報酬に含まれるマルチコイン")] REWARD_MULTICOIN = 419, // 0x000001A3
      [GameParameter.ParameterDesc("報酬に含まれる欠片ポイント")] REWARD_KAKERACOIN = 420, // 0x000001A4
      [GameParameter.ParameterDesc("クエスト出撃条件 (0)改行表記、指定文字なし/(1)一行表記、指定文字付/(2)改行表記、指定文字付"), GameParameter.UsesIndex] QUEST_UNIT_ENTRYCONDITION = 421, // 0x000001A5
      [GameParameter.ParameterDesc("クエスト出撃条件が設定されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] OBSOLETE_QUEST_IS_UNIT_ENTRYCONDITION = 422, // 0x000001A6
      [GameParameter.ParameterDesc("クエストにユニットが出撃可能な場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.SerializeGameObjects, GameParameter.AlwaysUpdate] QUEST_IS_UNIT_ENABLEENTRYCONDITION = 423, // 0x000001A7
      [GameParameter.ParameterDesc("キャラクタークエスト：エピソード解放条件")] QUEST_CHARACTER_MAINUNITCONDITION = 424, // 0x000001A8
      [GameParameter.ParameterDesc("キャラクタークエスト：話数")] QUEST_CHARACTER_EPISODENUM = 425, // 0x000001A9
      [GameParameter.ParameterDesc("キャラクタークエスト：エピソード名")] QUEST_CHARACTER_EPISODENAME = 426, // 0x000001AA
      [GameParameter.ParameterDesc("限定ショップアイテムの残り購入可能数を取得")] SHOP_LIMITED_ITEM_BUYAMOUNT = 427, // 0x000001AB
      [GameParameter.ParameterDesc("ユニットのIndexで指定したジョブがマスターしている場合に表示。Indexが-1の場合は選択中のジョブで判定。JobDataが直接設定されている場合はバインドされたJobDataで判定"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] UNIT_IS_JOBMASTER = 428, // 0x000001AC
      [GameParameter.ParameterDesc("ユニット覚醒レベル"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] UNIT_NEXTAWAKE = 429, // 0x000001AD
      [GameParameter.ParameterDesc("操作時間が延長された際に表示する秒数")] MULTIPLAY_ADD_INPUTTIME = 430, // 0x000001AE
      [GameParameter.ParameterDesc("ユニットの限界突破最大値に達している場合にIndex:0で非表示。Index:1で表示。"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] UNIT_IS_AWAKEMAX = 431, // 0x000001AF
      [GameParameter.ParameterDesc("コンフィグでオートプレイ選択時場合にIndex:0で表示。Index:1で非表示。"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] CONFIG_IS_AUTOPLAY = 432, // 0x000001B0
      [GameParameter.ParameterDesc("フレンドがお気に入りなら表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] FRIEND_ISFAVORITE = 433, // 0x000001B1
      [GameParameter.ParameterDesc("キャラクタークエスト出撃条件(0)改行表記/(1)一行表記"), GameParameter.UsesIndex] QUEST_CHARACTER_ENTRYCONDITION = 434, // 0x000001B2
      [GameParameter.ParameterDesc("キャラクタークエスト出撃条件が設定されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_CHARACTER_IS_ENTRYCONDITION = 435, // 0x000001B3
      [GameParameter.ParameterDesc("キャラクタークエスト出撃条件のタイトル表示"), GameParameter.AlwaysUpdate] QUEST_CHARACTER_CONDITIONATTENTION = 436, // 0x000001B4
      [GameParameter.ParameterDesc("復帰したプレイヤーINDEX")] MULTIPLAY_RESUME_PLAYER_INDEX = 437, // 0x000001B5
      [GameParameter.ParameterDesc("復帰したプレイヤーがホストか？")] MULTIPLAY_RESUME_PLAYER_IS_HOST = 438, // 0x000001B6
      [GameParameter.ParameterDesc("復帰したが、他プレイヤーがいない")] MULTIPLAY_RESUME_BUT_NOT_PLAYER = 439, // 0x000001B7
      [GameParameter.ParameterDesc("ショップごとに保持数を表示するイベントコイン")] EVENTCOIN_SHOPTYPEICON = 440, // 0x000001B8
      [GameParameter.ParameterDesc("イベントコイン一覧のアイテム名")] EVENTCOIN_ITEMNAME = 441, // 0x000001B9
      [GameParameter.ParameterDesc("イベントコイン一覧の説明")] EVENTCOIN_MESSAGE = 442, // 0x000001BA
      [GameParameter.ParameterDesc("イベントコイン一覧の所持数")] EVENTCOIN_POSSESSION = 443, // 0x000001BB
      [GameParameter.ParameterDesc("ショップアイテムのイベントコイン別アイコン")] EVENTCOIN_PRICEICON = 444, // 0x000001BC
      [GameParameter.ParameterDesc("イベントショップアイテムの残り購入可能数を取得")] SHOP_EVENT_ITEM_BUYAMOUNT = 445, // 0x000001BD
      [GameParameter.ParameterDesc("イベント終了までの時間")] TROPHY_REMAININGTIME = 446, // 0x000001BE
      [GameParameter.ParameterDesc("お客様コードのみを表示"), GameParameter.UsesIndex] GLOBAL_PLAYER_OKYAKUSAMACODE2 = 447, // 0x000001BF
      [GameParameter.ParameterDesc("対戦相手のユニット"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_UNIT_IMAGE = 448, // 0x000001C0
      [GameParameter.ParameterDesc("対戦相手の名前"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_PLAYER_NAME = 449, // 0x000001C1
      [GameParameter.ParameterDesc("対戦相手のレベル"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_PLAYER_LEVEL = 450, // 0x000001C2
      [GameParameter.ParameterDesc("対戦相手の総合攻撃力"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_PLAYER_TOTALATK = 451, // 0x000001C3
      [GameParameter.ParameterDesc("対戦結果"), GameParameter.InstanceTypes(typeof (BattleCore.QuestResult))] VERSUS_RESULT = 452, // 0x000001C4
      [GameParameter.ParameterDesc("対戦ランク表示")] VERSUS_RANK = 453, // 0x000001C5
      [GameParameter.ParameterDesc("現在のランクポイントを表示")] VERSUS_RANK_POINT = 454, // 0x000001C6
      [GameParameter.ParameterDesc("ランクアップまでのポイントを表示")] VERSUS_RANK_NEXT_POINT = 455, // 0x000001C7
      [GameParameter.ParameterDesc("現在のランクのアイコン")] VERSUS_RANK_ICON = 456, // 0x000001C8
      [GameParameter.ParameterDesc("現在のランクのインデックス")] VERSUS_RANK_ICON_INDEX = 457, // 0x000001C9
      [GameParameter.ParameterDesc("部屋内プレイヤーのランクのアイコン")] VERSUS_ROOMPLAYER_RANK_ICON = 458, // 0x000001CA
      [GameParameter.ParameterDesc("部屋内プレイヤーのランクのインデックス")] VERSUS_ROOMPLAYER_RANK_ICON_INDEX = 459, // 0x000001CB
      [GameParameter.ParameterDesc("対戦マップのサムネイル")] VERSUS_MAP_THUMNAIL = 460, // 0x000001CC
      [GameParameter.ParameterDesc("マップ選択中のサムネイル")] VERSUS_MAP_THUMNAIL2 = 461, // 0x000001CD
      [GameParameter.ParameterDesc("マップ選択中のマップ名")] VERSUS_MAP_NAME = 462, // 0x000001CE
      [GameParameter.ParameterDesc("マップが選択されていれば表示"), GameParameter.AlwaysUpdate] VERSUS_MAP_SELECT = 463, // 0x000001CF
      [GameParameter.ParameterDesc("所有アリーナコイン")] SHOP_ARENA_COIN = 464, // 0x000001D0
      [GameParameter.ParameterDesc("所有マルチコイン")] SHOP_MULTI_COIN = 465, // 0x000001D1
      [GameParameter.ParameterDesc("プレイヤーの所持コイン (共通無償幻晶石)")] GLOBAL_PLAYER_COINCOM = 466, // 0x000001D2
      [GameParameter.ParameterDesc("プレイヤーの所持コイン (無償幻晶石　共通＆固有)")] GLOBAL_PLAYER_FREECOINSET = 467, // 0x000001D3
      [GameParameter.ParameterDesc("ユニット覚醒レベル2"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes)), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] UNIT_AWAKE2 = 468, // 0x000001D4
      [GameParameter.ParameterDesc("プロダクト個数Index: 0:トータル 1:無償 2:有償"), GameParameter.UsesIndex] PRODUCT_COINNUM = 469, // 0x000001D5
      [GameParameter.ParameterDesc("アーティファクトの錬成可能数")] ARTIFACT_DRILLING_COUNT = 470, // 0x000001D6
      [GameParameter.ParameterDesc("アーティファクトの錬成限界数が最大値なら表示")] ARTIFACT_DRILLING_MAXDRAW = 471, // 0x000001D7
      [GameParameter.ParameterDesc("アーティファクトの錬成限界数が最大値でないなら表示")] ARTIFACT_DRILLING_NOTMAXDRAW = 472, // 0x000001D8
      [GameParameter.ParameterDesc("BuycoinProductで指定しているタイトル名")] PRODUCT_BUYCOINNAME = 473, // 0x000001D9
      [GameParameter.ParameterDesc("BuycoinProductで指定している説明")] PRODUCT_BUYCOINDESC = 474, // 0x000001DA
      VS_ST = 999, // 0x000003E7
      [GameParameter.ParameterDesc("対戦の報酬タイプ")] VS_TOWER_REWARD_NAME = 1000, // 0x000003E8
      [GameParameter.ParameterDesc("シーズン報酬受け取り可能か？")] VS_TOWER_SEASON_RECEIPT = 1001, // 0x000003E9
      [GameParameter.ParameterDesc("PvPコインの枚数"), GameParameter.AlwaysUpdate] VS_COIN = 1002, // 0x000003EA
      [GameParameter.ParameterDesc("フリーマッチでのpvpコイン受け取り可能枚数"), GameParameter.AlwaysUpdate] VS_REMAIN_COIN = 1003, // 0x000003EB
      [GameParameter.ParameterDesc("タワーマッチ開催中か"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] VS_OPEN_TOWERMATCH = 1004, // 0x000003EC
      [GameParameter.ParameterDesc("対戦の種類を表示"), GameParameter.AlwaysUpdate] VS_QUEST_CATEGORY = 1005, // 0x000003ED
      [GameParameter.ParameterDesc("タワーマッチで連勝した際のボーナス表示"), GameParameter.AlwaysUpdate] VS_TOWER_WINBONUS = 1006, // 0x000003EE
      [GameParameter.ParameterDesc("観戦時の表示・非表示対応"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] VS_AUDIENCE_DISPLAY = 1007, // 0x000003EF
      [GameParameter.ParameterDesc("Index == 0:1P勝利 / Index == 1:2P勝利 時に表示する"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] VS_AUDIENCE_WIN_PLAYER = 1008, // 0x000003F0
      [GameParameter.ParameterDesc("観戦時のときだけ表示する"), GameParameter.AlwaysUpdate] VS_AUDIENCE_ONLY_DISPLAY = 1009, // 0x000003F1
      [GameParameter.ParameterDesc("観戦している部屋の種類"), GameParameter.AlwaysUpdate] VS_AUDIENCE_TYPE = 1010, // 0x000003F2
      [GameParameter.ParameterDesc("現在の階層"), GameParameter.AlwaysUpdate] VS_TOWER_FLOOR = 1011, // 0x000003F3
      [GameParameter.ParameterDesc("タワーマッチのときだけ表示する"), GameParameter.AlwaysUpdate] VS_TOWER_ONLY_DISPLAY = 1012, // 0x000003F4
      [GameParameter.ParameterDesc("観戦者数"), GameParameter.AlwaysUpdate] VS_AUDIENCE_NUM = 1013, // 0x000003F5
      [GameParameter.ParameterDesc("CPU対戦の難易度"), GameParameter.AlwaysUpdate] VS_CPU_DIFFICULTY = 1014, // 0x000003F6
      [GameParameter.ParameterDesc("CPU対戦の名前"), GameParameter.AlwaysUpdate] VS_CPU_NAME = 1015, // 0x000003F7
      [GameParameter.ParameterDesc("CPU対戦のレベル"), GameParameter.AlwaysUpdate] VS_CPU_LV = 1016, // 0x000003F8
      [GameParameter.ParameterDesc("CPU対戦の攻撃力"), GameParameter.AlwaysUpdate] VS_CPU_TOTALATK = 1017, // 0x000003F9
      [GameParameter.ParameterDesc("タワーマッチの同一ユニット判定があるか？"), GameParameter.AlwaysUpdate] VS_TOWER_SAMEUNIT = 1018, // 0x000003FA
      VS_ED = 1098, // 0x0000044A
      [GameParameter.ParameterDesc("武具コンディション"), GameParameter.AlwaysUpdate] ARTIFACT_ST = 1099, // 0x0000044B
      [GameParameter.ParameterDesc("武具コンディション"), GameParameter.AlwaysUpdate] ARTIFACT_EVOLUTION_COND = 1100, // 0x0000044C
      [GameParameter.ParameterDesc("武具がお気に入りなら表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] ARTIFACT_ISFAVORITE = 1101, // 0x0000044D
      [GameParameter.ParameterDesc("武具の進化後の★の数"), GameParameter.AlwaysUpdate] ARTIFACT_EVOLUTION_RARITY = 1102, // 0x0000044E
      [GameParameter.ParameterDesc("武具アイコン"), GameParameter.InstanceTypes(typeof (GameParameter.ArtifactInstanceTypes)), GameParameter.UsesIndex] ARTIFACT_ICON = 1103, // 0x0000044F
      [GameParameter.ParameterDesc("武具の種類にあわせたフレームをImageに設定します。"), GameParameter.InstanceTypes(typeof (GameParameter.ArtifactInstanceTypes)), GameParameter.UsesIndex] ARTIFACT_FRAME = 1104, // 0x00000450
      [GameParameter.ParameterDesc("武具の個数"), GameParameter.InstanceTypes(typeof (GameParameter.ArtifactInstanceTypes)), GameParameter.UsesIndex] ARTIFACT_AMOUNT = 1105, // 0x00000451
      [GameParameter.ParameterDesc("武具のオススメバッジ"), GameParameter.AlwaysUpdate] ARTIFACT_RECOMMEND_BADGE = 1106, // 0x00000452
      [GameParameter.ParameterDesc("武具にひらめきスキル合成ボーナスがある場合に表示(0)/非表示(1)"), GameParameter.AlwaysUpdate] ARTIFACT_INSPSKILL_BONUS = 1107, // 0x00000453
      [GameParameter.ParameterDesc("武具にひらめきスキル合成ボーナスの値"), GameParameter.AlwaysUpdate] ARTIFACT_INSPSKILL_BONUS_VALUE = 1108, // 0x00000454
      [GameParameter.ParameterDesc("武具のフレーバーテキスト"), GameParameter.AlwaysUpdate] ARTIFACT_FLAVOR = 1109, // 0x00000455
      ARTIFACT_ED = 1198, // 0x000004AE
      QUEST_UI_ST = 1199, // 0x000004AF
      [GameParameter.ParameterDesc("クエストコンティニュー不可が設定されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_MAP_NO_CONTINUE = 1200, // 0x000004B0
      [GameParameter.ParameterDesc("クエストダメージ制限が設定されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_MAP_DAMAGE_LIMIT = 1201, // 0x000004B1
      [GameParameter.ParameterDesc("クエスト情報の詳細テキスト Loc/japanese/quest_info.txt参照"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_UI_DETAIL_INFO = 1202, // 0x000004B2
      [GameParameter.ParameterDesc("クエスト出撃条件でチームが固定されていて、かつユニットが設定されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_UNIT_ENTRYCONDITION_FORCE_AVAILABLEUNIT = 1203, // 0x000004B3
      [GameParameter.ParameterDesc("クエスト出撃条件で出撃ユニットが制限されていた場合に表示"), GameParameter.AlwaysUpdate] QUEST_IS_UNIT_ENTRYCONDITION_LIMIT = 1204, // 0x000004B4
      [GameParameter.ParameterDesc("クエスト出撃条件で出撃ユニットが固定されていた場合に表示"), GameParameter.AlwaysUpdate] QUEST_IS_UNIT_ENTRYCONDITION_FORCE = 1205, // 0x000004B5
      [GameParameter.ParameterDesc("クエストをクリアしていた場合にinteractable=true(0)/false(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_CLEARD_INTERACTABLE = 1206, // 0x000004B6
      [GameParameter.ParameterDesc("クエストユニット交代が許可されている場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_UNIT_CHANGE = 1207, // 0x000004B7
      [GameParameter.ParameterDesc("ユニット未所持による出撃不可か？:表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_HAVE_UNIT_LOCK = 1208, // 0x000004B8
      [GameParameter.ParameterDesc("ストーリーEXの挑戦回数に制限が設定されているなら表示")] QUEST_UI_STORYEX_RESTCOUNT_ACTIVE = 1209, // 0x000004B9
      [GameParameter.ParameterDesc("ストーリーEXの残り挑戦回数")] QUEST_UI_STORYEX_RESTCOUNT = 1210, // 0x000004BA
      QUEST_IS_ED = 1298, // 0x00000512
      TRICK_ST = 1299, // 0x00000513
      [GameParameter.ParameterDesc("特殊パネルの名称")] TRICK_NAME = 1300, // 0x00000514
      [GameParameter.ParameterDesc("特殊パネルの説明")] TRICK_DESC = 1301, // 0x00000515
      [GameParameter.ParameterDesc("特殊パネルUIアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] TRICK_UI_ICON = 1302, // 0x00000516
      TRICK_ED = 1398, // 0x00000576
      BATTLE_UI_ST = 1399, // 0x00000577
      [GameParameter.ParameterDesc("オーダーユニットが詠唱中か？")] BATTLE_UI_IMG_IS_CAST_ORDER = 1400, // 0x00000578
      [GameParameter.ParameterDesc("スキルの使用回数")] SKILL_USE_COUNT = 1401, // 0x00000579
      [GameParameter.EnumParameterDesc("スキル属性にあわせてImageArrayを切り替えます。(属性がない場合は非表示)", typeof (EElement)), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] SKILL_ELEMENT = 1402, // 0x0000057A
      [GameParameter.EnumParameterDesc("スキル攻撃詳細区分にあわせてImageArrayを切り替えます。(攻撃詳細区分がない場合は非表示)", typeof (AttackDetailTypes)), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] SKILL_ATTACK_DETAIL = 1403, // 0x0000057B
      [GameParameter.EnumParameterDesc("スキル攻撃タイプにあわせてImageArrayを切り替えます。(攻撃タイプがない場合は非表示)", typeof (AttackTypes)), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] SKILL_ATTACK_TYPE = 1404, // 0x0000057C
      [GameParameter.ParameterDesc("天候が発動していれば表示(0)/非表示(1)")] BATTLE_UI_WTH_IS_ENABLE = 1405, // 0x0000057D
      [GameParameter.ParameterDesc("天候名を表示")] BATTLE_UI_WTH_NAME = 1406, // 0x0000057E
      [GameParameter.ParameterDesc("天候アイコンを表示")] BATTLE_UI_WTH_ICON = 1407, // 0x0000057F
      [GameParameter.ParameterDesc("変身を加味したユニットのHP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_DTU_HP = 1408, // 0x00000580
      [GameParameter.ParameterDesc("変身を加味したユニットのMP"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_DTU_MP = 1409, // 0x00000581
      BATTLE_UI_ED = 1498, // 0x000005DA
      UNIT_EXTRA_PARAM_ST = 1499, // 0x000005DB
      [GameParameter.ParameterDesc("ユニットの移動力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_EXTRA_PARAM_MOVE = 1500, // 0x000005DC
      [GameParameter.ParameterDesc("ユニットのジャンプ力"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_EXTRA_PARAM_JUMP = 1501, // 0x000005DD
      [GameParameter.ParameterDesc("ユニット小アイコン表示")] UNITPARAM_ICON_SMALL = 1502, // 0x000005DE
      [GameParameter.ParameterDesc("ユニット中アイコン表示")] UNITPARAM_ICON_MEDIUM = 1503, // 0x000005DF
      [GameParameter.ParameterDesc("ユニット一枚絵表示")] UNITPARAM_IMAGE = 1504, // 0x000005E0
      [GameParameter.ParameterDesc("ユニット目アイコン表示")] UNITPARAM_EYE_IMAGE = 1505, // 0x000005E1
      UNIT_EXTRA_PARAM_ED = 1598, // 0x0000063E
      MULTI_UI_ST = 1599, // 0x0000063F
      [GameParameter.ParameterDesc("マルチのユニットレベル制限")] MULTI_UI_ROOM_LIMIT_UNITLV = 1600, // 0x00000640
      [GameParameter.ParameterDesc("クリアの有無")] MULTI_UI_ROOM_LIMIT_CLEAR = 1601, // 0x00000641
      [GameParameter.ParameterDesc("現在の操作プレイヤー名を表示 [0]:切断時グレー表示 [1]:思考中追加 [2]～のターン追加"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_UI_CURRENT_PLAYER_NAME = 1602, // 0x00000642
      [GameParameter.ParameterDesc("マルチタワーでの総攻撃力")] MULTI_TOWER_UI_PARTY_TOTALATK = 1603, // 0x00000643
      [GameParameter.ParameterDesc("オーナーの状態"), GameParameter.AlwaysUpdate, GameParameter.InstanceTypes(typeof (JSON_MyPhotonPlayerParam.EState)), GameParameter.UsesIndex] MULTI_OWNER_STATE = 1604, // 0x00000644
      [GameParameter.ParameterDesc("マルチタワー階層")] MULTI_TOWER_FLOOR = 1605, // 0x00000645
      [GameParameter.ParameterDesc("リーダースキル有効なクエストか？")] MULTI_QUEST_IS_LEADERSKILL = 1606, // 0x00000646
      [GameParameter.ParameterDesc("マルチ部屋リストの階層")] MULTI_TOWER_ROOM_LIST_FLOOR = 1607, // 0x00000647
      [GameParameter.ParameterDesc("マルチ部屋リストのクリア済みか")] MULTI_TOWER_ROOM_LIST_ISCLEAR = 1608, // 0x00000648
      [GameParameter.ParameterDesc("マルチタワーのスキップができる状態であれば表示(0)/非表示(1)")] MULTI_TOWER_IS_CAN_SKIP = 1609, // 0x00000649
      [GameParameter.ParameterDesc("現在の部屋の倍速設定")] MULTI_CURRENT_ROOM_SPEED = 1610, // 0x0000064A
      [GameParameter.ParameterDesc("リストの部屋の倍速設定")] MULTI_ROOM_LIST_SPEED = 1611, // 0x0000064B
      [GameParameter.ParameterDesc("現在の部屋のオート許可設定")] MULTI_CURRENT_ROOM_AUTOPERMISSION = 1612, // 0x0000064C
      [GameParameter.ParameterDesc("リストの部屋のオート許可設定")] MULTI_ROOM_LIST_AUTOPERMISSION = 1613, // 0x0000064D
      [GameParameter.ParameterDesc("キック可能なら表示（プレイヤーがホストかつ、プレイヤーが埋まっている枠）"), GameParameter.AlwaysUpdate] MULTI_PLAYER_ENABLE_KICK = 1614, // 0x0000064E
      [GameParameter.ParameterDesc("マルチフレンドAI設定ボタン（プレイヤーがホストかつ、0:空き枠、1:フレンドAIの枠で表示）"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] MULTI_PLAYER_FRIENDAI = 1615, // 0x0000064F
      [GameParameter.ParameterDesc("マルチのオート設定状況表示"), GameParameter.AlwaysUpdate] MULTI_PLAYER_AUTOSETTING = 1616, // 0x00000650
      [GameParameter.ParameterDesc("マルチフレンドAIなら表示"), GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_FRIENDAI = 1617, // 0x00000651
      [GameParameter.ParameterDesc("行動順のプレイヤーがオート操作のとき表示"), GameParameter.AlwaysUpdate] MULTI_CURRENT_PLAYER_IS_AUTO = 1618, // 0x00000652
      [GameParameter.ParameterDesc("行動順のプレイヤーがサポートAIのとき表示"), GameParameter.AlwaysUpdate] MULTI_CURRENT_PLAYER_IS_SUPPORTAI = 1619, // 0x00000653
      [GameParameter.ParameterDesc("マルチタワーのソロ出撃可不可")] MULTI_TOWER_CONDITION_NOT_SORO = 1620, // 0x00000654
      [GameParameter.ParameterDesc("マルチタワー専用の総戦闘力表示")] MULTI_TOWER_TOTALCOMBATPOWER = 1621, // 0x00000655
      MULTI_UI_ED = 1698, // 0x000006A2
      QUEST_BONUSOBJECTIVE_ST = 1699, // 0x000006A3
      [GameParameter.EnumParameterDesc("プレイ中クエストのボーナス条件の達成状態にあわせてImageArrayを切り替えます(未達成/達成/全達成)。インデックスでボーナス条件の番号を指定してください。", typeof (QuestBonusObjectiveState)), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_BONUSOBJECTIVE_STAR = 1700, // 0x000006A4
      [GameParameter.ParameterDesc("クエストミッションの達成個数を表示します。全てのミッションを達成していた場合、テキストの色が変わります。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_BONUSOBJECTIVE_AMOUNT = 1701, // 0x000006A5
      [GameParameter.ParameterDesc("クエストミッションの最大個数を表示します。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_BONUSOBJECTIVE_AMOUNTMAX = 1702, // 0x000006A6
      [GameParameter.ParameterDesc("クエストミッションの報酬の数を表示します。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE_REWARD_NUM = 1703, // 0x000006A7
      [GameParameter.ParameterDesc("クエストミッションの進捗を表示するタイプのミッションならActive、表示しないタイプなら非Active"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE_PROGRESS_BADGE = 1704, // 0x000006A8
      [GameParameter.ParameterDesc("クエストミッションの進捗（現在）"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE_PROGRESS_CURRENT = 1705, // 0x000006A9
      [GameParameter.ParameterDesc("クエストミッションの進捗（目標値）"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_BONUSOBJECTIVE_PROGRESS_TARGET = 1706, // 0x000006AA
      [GameParameter.ParameterDesc("塔クエストの固定ミッションの星"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_TOWER_BONUSOBJECTIVE_FIXED_STATE = 1707, // 0x000006AB
      [GameParameter.ParameterDesc("塔クエストミッションの達成個数を表示します。全てのミッションを達成していた場合、テキストの色が変わります。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_TOWER_BONUSOBJECTIVE_AMOUNT = 1708, // 0x000006AC
      [GameParameter.ParameterDesc("塔クエストミッションの最大個数を表示します。"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes))] QUEST_TOWER_BONUSOBJECTIVE_AMOUNTMAX = 1709, // 0x000006AD
      [GameParameter.ParameterDesc("クエストミッションの進捗（現在）\n▼色変更あり\n　達成可能：緑\n　達成不可：赤\n　達成済み：白"), GameParameter.InstanceTypes(typeof (GameParameter.QuestInstanceTypes)), GameParameter.UsesIndex] QUEST_TOWER_BONUSOBJECTIVE_PROGRESS_COLOR = 1710, // 0x000006AE
      QUEST_BONUSOBJECTIVE_ED = 1798, // 0x00000706
      ME_UI_ST = 1799, // 0x00000707
      [GameParameter.ParameterDesc("スキルのマップ(地形)効果説明")] ME_UI_SKILL_DESC = 1800, // 0x00000708
      [GameParameter.ParameterDesc("マップ(地形)効果名")] ME_UI_NAME = 1801, // 0x00000709
      [GameParameter.ParameterDesc("ジョブ特効説明")] JOB_DESC_CH = 1802, // 0x0000070A
      [GameParameter.ParameterDesc("ジョブ特効説明が設定されていれば表示(0)")] IS_JOB_DESC_CH = 1803, // 0x0000070B
      [GameParameter.ParameterDesc("ジョブその他の効果説明")] JOB_DESC_OT = 1804, // 0x0000070C
      [GameParameter.ParameterDesc("ジョブその他の効果説明が設定されていれば表示(0)")] IS_JOB_DESC_OT = 1805, // 0x0000070D
      [GameParameter.ParameterDesc("ユニットの現ジョブのMediumアイコン。UNIT_JOBICON2に参照不具合があるため新設"), GameParameter.InstanceTypes(typeof (GameParameter.UnitInstanceTypes))] UNIT_JOBICON2_BUGFIX = 1806, // 0x0000070E
      ME_UI_ED = 1898, // 0x0000076A
      FIRST_FRIEND_ST = 1899, // 0x0000076B
      [GameParameter.ParameterDesc("初フレンド成立人数上限")] FIRST_FRIEND_MAX = 1900, // 0x0000076C
      [GameParameter.ParameterDesc("初フレンド成立人数")] FIRST_FRIEND_COUNT = 1901, // 0x0000076D
      FIRST_FRIEND_ED = 1998, // 0x000007CE
      CHALLENGEMISSION_ST = 1999, // 0x000007CF
      [GameParameter.ParameterDesc("カテゴリ名からヘルプ画像を表示")] CHALLENGEMISSION_IMG_HELP = 2000, // 0x000007D0
      [GameParameter.ParameterDesc("カテゴリ名からタブ画像を表示")] CHALLENGEMISSION_IMG_BUTTON = 2001, // 0x000007D1
      [GameParameter.ParameterDesc("トロフィー名から報酬画像を表示")] CHALLENGEMISSION_IMG_REWARD = 2002, // 0x000007D2
      CHALLENGEMISSION_ED = 2098, // 0x00000832
      UNITPARAM_EXTRA_ST = 2099, // 0x00000833
      [GameParameter.ParameterDesc("ユニットパラメーターの錬成可能終了日時(Y/m/d H:i)")] UNITPARAM_EXTRA_UNLOCKLIMIT = 2100, // 0x00000834
      [GameParameter.ParameterDesc("訓練対戦のの総戦闘力"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_COM_TOTALSTATUS = 2101, // 0x00000835
      [GameParameter.ParameterDesc("運営指定戦のリーダーユニット画像"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_DRAFT_IMAGE = 2102, // 0x00000836
      [GameParameter.ParameterDesc("運営指定戦のパーティユニットアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.VersusDraftSlot))] VERSUS_DRAFT_PARTY_IMAGE_ICON = 2103, // 0x00000837
      [GameParameter.ParameterDesc("運営指定戦で先攻/後攻でGameObjectのOn/Off切り替え"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_DRAFT_TURN = 2104, // 0x00000838
      [GameParameter.ParameterDesc("運営指定戦のパーティユニット画像"), GameParameter.InstanceTypes(typeof (GameParameter.VersusDraftSlot))] VERSUS_DRAFT_PARTY_IMAGE = 2105, // 0x00000839
      [GameParameter.ParameterDesc("運営指定戦のパーティ総攻撃力"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_DRAFT_PARTY_TOTALATK = 2106, // 0x0000083A
      [GameParameter.ParameterDesc("レンタルユニットの親密度プログレスバー")] RENTAL_UNIT_FAVORITEPOINT_BAR = 2107, // 0x0000083B
      [GameParameter.ParameterDesc("レンタルユニットの親密度％")] RENTAL_UNIT_FAVORITEPOINT_PERCENT = 2108, // 0x0000083C
      [GameParameter.ParameterDesc("レンタルユニットクエストの解放条件")] RENTAL_UNIT_UNLOCK_CONDITION = 2109, // 0x0000083D
      [GameParameter.ParameterDesc("レンタルユニットの親密度プログレスバー目盛り")] RENTAL_UNIT_FAVORITEPOINT_LINE = 2110, // 0x0000083E
      [GameParameter.ParameterDesc("ユニット欠片ショップの通貨アイコン")] UNIT_PIECE_SHOP_CURRENCY_ICON = 2111, // 0x0000083F
      [GameParameter.ParameterDesc("UnitTooltip用のリーダースキル切替ボタンON/OFF")] UNIT_LEADERSKILL_CHANGE_TOOLTIP_BUTTON = 2112, // 0x00000840
      [GameParameter.ParameterDesc("リーダースキル枠画像")] UNIT_LEADERSKILL_IMAGE = 2113, // 0x00000841
      [GameParameter.ParameterDesc("運営指定戦のパーティ総戦闘力"), GameParameter.InstanceTypes(typeof (GameParameter.VersusPlayerInstanceType))] VERSUS_DRAFT_PARTY_TOTALCOMBATPOWER = 2114, // 0x00000842
      UNITPARAM_EXTRA_ED = 2198, // 0x00000896
      UNIT_TOBIRA_ST = 2199, // 0x00000897
      [GameParameter.ParameterDesc("真理開眼の開放に必要なゼニー")] UNIT_TOBIRA_UNLOCK_COST = 2200, // 0x00000898
      [GameParameter.ParameterDesc("真理開眼の開放に必要な素材の数")] UNIT_TOBIRA_UNLOCK_REQAMOUNT = 2201, // 0x00000899
      [GameParameter.ParameterDesc("真理開眼の開放に必要な素材の所持数")] UNIT_TOBIRA_UNLOCK_AMOUNT = 2202, // 0x0000089A
      [GameParameter.ParameterDesc("真理開眼の扉の強化に必要な素材の数")] UNIT_TOBIRA_ENHANCE_REQAMOUNT = 2203, // 0x0000089B
      [GameParameter.ParameterDesc("真理開眼の扉の強化に必要な素材の所持数")] UNIT_TOBIRA_ENHANCE_AMOUNT = 2204, // 0x0000089C
      [GameParameter.ParameterDesc("真理開眼の扉のレベルを示すアイコン表示"), GameParameter.UsesIndex] UNIT_TOBIRA_LEVEL_ICON = 2205, // 0x0000089D
      [GameParameter.ParameterDesc("真理開眼の扉のレベルを示すアイコン表示"), GameParameter.UsesIndex] UNIT_TOBIRA_LEVEL_ICON2 = 2206, // 0x0000089E
      UNIT_TOBIRA_ED = 2298, // 0x000008FA
      ITEM_ST = 2299, // 0x000008FB
      [GameParameter.ParameterDesc("アイテムのレアリティ")] ITEM_RARITY = 2300, // 0x000008FC
      [GameParameter.ParameterDesc("アイテムのフレーバーテキスト(フレーバーが無ければ説明)")] ITEM_FLAVORORDESC = 2301, // 0x000008FD
      [GameParameter.ParameterDesc("任意の数をテキストにセット")] ITEM_INTTONUM = 2302, // 0x000008FE
      [GameParameter.ParameterDesc("期限付きアイテムの有効期限"), GameParameter.InstanceTypes(typeof (GameParameter.LimitItemInstanceType))] ITEM_USE_PERIOD = 2303, // 0x000008FF
      ITEM_ED = 2398, // 0x0000095E
      GLOBAL_PLAYER_RANKMATCH_ST = 2399, // 0x0000095F
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのランキング順位")] GLOBAL_PLAYER_RANKMATCH_RANK = 2400, // 0x00000960
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのポイント")] GLOBAL_PLAYER_RANKMATCH_SCORE_CURRENT = 2401, // 0x00000961
      [GameParameter.ParameterDesc("プレイヤーの次のクラスまでに必要なポイント")] GLOBAL_PLAYER_RANKMATCH_SCORE_NEXT = 2402, // 0x00000962
      [GameParameter.ParameterDesc("プレイヤーのランクマッチ挑戦可能回数")] GLOBAL_PLAYER_RANKMATCH_BP = 2403, // 0x00000963
      [GameParameter.ParameterDesc("ランクマッチのクラスアイコン")] RANKMATCH_CLASS_ICON = 2404, // 0x00000964
      [GameParameter.ParameterDesc("ランクマッチのクラス名")] RANKMATCH_CLASS_NAME = 2405, // 0x00000965
      [GameParameter.ParameterDesc("ランクマッチのクラスの枠")] RANKMATCH_CLASS_FRAME = 2406, // 0x00000966
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのクラスと同じなら表示")] RANKMATCH_PLAYER_ISCLASS = 2407, // 0x00000967
      [GameParameter.ParameterDesc("プレイヤーのランクマッチの順位(3位以上なら非表示になる)")] GLOBAL_PLAYER_RANKMATCH_RANK_IMAGESET = 2408, // 0x00000968
      [GameParameter.ParameterDesc("プレイヤーのランクマッチの順位アイコン(4位以下なら非表示になる)")] GLOBAL_PLAYER_RANKMATCH_RANK_ICON = 2409, // 0x00000969
      [GameParameter.ParameterDesc("プレイヤーのランクマッチパーティの総攻撃力")] PARTY_RANKMATCHTOTALATK = 2410, // 0x0000096A
      [GameParameter.ParameterDesc("ランクマッチのランキングの順位(3位以上なら非表示になる)")] RANKMATCH_RANKING_RANK_IMAGESET = 2411, // 0x0000096B
      [GameParameter.ParameterDesc("ランクマッチのランキングの順位アイコン(4位以下なら非表示になる)")] RANKMATCH_RANKING_RANK_ICON = 2412, // 0x0000096C
      [GameParameter.ParameterDesc("ランクマッチのランキングのクラスアイコン")] RANKMATCH_RANKING_CLASS = 2413, // 0x0000096D
      [GameParameter.ParameterDesc("ランクマッチのランキングのユーザー名")] RANKMATCH_RANKING_NAME = 2414, // 0x0000096E
      [GameParameter.ParameterDesc("ランクマッチのランキングのユーザーランク")] RANKMATCH_RANKING_LEVEL = 2415, // 0x0000096F
      [GameParameter.ParameterDesc("ランクマッチのランキングのスコア")] RANKMATCH_RANKING_SCORE = 2416, // 0x00000970
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのクラス"), GameParameter.InstanceTypes(typeof (GameParameter.RankMatchPlayer))] GLOBAL_PLAYER_RANKMATCH_CLASS = 2417, // 0x00000971
      [GameParameter.ParameterDesc("ランクマッチのランキングの順位(3位以上なら非表示になる)")] RANKMATCH_RANKING_RANKREWARD_IMAGESET_BEGIN = 2418, // 0x00000972
      RANKMATCH_RANKING_RANKREWARD_IMAGESET_END = 2419, // 0x00000973
      [GameParameter.ParameterDesc("ランクマッチのランキングの順位アイコン(4位以下なら非表示になる)")] RANKMATCH_RANKING_RANKREWARD_ICON = 2420, // 0x00000974
      [GameParameter.ParameterDesc("ランクマッチの戦績の勝敗")] RANKMATCH_HISTORY_RESULT = 2421, // 0x00000975
      [GameParameter.ParameterDesc("ランクマッチの戦績の変動ポイント")] RANKMATCH_HISTORY_VALUE = 2422, // 0x00000976
      [GameParameter.ParameterDesc("ランクマッチの戦績のクラスアイコン")] RANKMATCH_HISTORY_CLASS = 2423, // 0x00000977
      [GameParameter.ParameterDesc("ランクマッチの戦績のユーザー名")] RANKMATCH_HISTORY_NAME = 2424, // 0x00000978
      [GameParameter.ParameterDesc("ランクマッチの戦績のユーザーランク")] RANKMATCH_HISTORY_LEVEL = 2425, // 0x00000979
      [GameParameter.ParameterDesc("ランクマッチの戦績のスコア")] RANKMATCH_HISTORY_SCORE = 2426, // 0x0000097A
      [GameParameter.ParameterDesc("ランクマッチのミッション名")] RANKMATCH_MISSION_NAME = 2427, // 0x0000097B
      [GameParameter.ParameterDesc("ランクマッチのミッションの必要回数")] RANKMATCH_MISSION_NEED = 2428, // 0x0000097C
      [GameParameter.ParameterDesc("ランクマッチのミッションの進行度")] RANKMATCH_MISSION_PROGRESS = 2429, // 0x0000097D
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのクラス"), GameParameter.InstanceTypes(typeof (GameParameter.RankMatchPlayer))] GLOBAL_PLAYER_RANKMATCH_CLASSNAME = 2430, // 0x0000097E
      [GameParameter.ParameterDesc("プレイヤーのランクマッチの対戦回数")] GLOBAL_PLAYER_RANKMATCH_COUNT_TOTAL = 2431, // 0x0000097F
      [GameParameter.ParameterDesc("プレイヤーのランクマッチの対戦勝利数")] GLOBAL_PLAYER_RANKMATCH_COUNT_WIN = 2432, // 0x00000980
      [GameParameter.ParameterDesc("プレイヤーのランクマッチの対戦敗北数")] GLOBAL_PLAYER_RANKMATCH_COUNT_LOSE = 2433, // 0x00000981
      [GameParameter.ParameterDesc("プレイヤーのランクマッチのスコアゲージ")] GLOBAL_PLAYER_RANKMATCH_SCORE_GAUGE = 2434, // 0x00000982
      RANKMATCH_RANKING_RANKREWARD_IMAGESET_SPERATE = 2435, // 0x00000983
      [GameParameter.ParameterDesc("ランクマッチのミッションの報酬受取可能ならActiveに"), GameParameter.AlwaysUpdate] RANKMATCH_MISSION_COMPLETED_ACTIVE = 2436, // 0x00000984
      [GameParameter.ParameterDesc("ランクマッチのシーズン報酬ランキングの順位(3位以上なら非表示になる)")] RANKMATCH_RESULT_RANK_NUM = 2437, // 0x00000985
      [GameParameter.ParameterDesc("ランクマッチのシーズン報酬ランキングの順位アイコン(4位以下なら非表示になる)")] RANKMATCH_RESULT_RANK_ICON = 2438, // 0x00000986
      [GameParameter.ParameterDesc("ランクマッチのシーズン報酬ランキングのクラスアイコン")] RANKMATCH_RESULT_CLASS = 2439, // 0x00000987
      [GameParameter.ParameterDesc("ランクマッチのシーズン報酬ランキングのスコア")] RANKMATCH_RESULT_SCORE = 2440, // 0x00000988
      [GameParameter.ParameterDesc("対戦相手のランクマッチスコア")] VERSUS_PLAYER_RANKMATCHSCORE = 2441, // 0x00000989
      [GameParameter.ParameterDesc("ランクマッチのシーズン報酬ランキングのクラス名")] RANKMATCH_RESULT_CLASS_NAME = 2442, // 0x0000098A
      [GameParameter.ParameterDesc("ランクマッチのシーズン期間")] RANKMATCH_RESULT_PERIOD = 2443, // 0x0000098B
      [GameParameter.ParameterDesc("ランクマッチのタイトル")] RANKMATCH_TITLE = 2444, // 0x0000098C
      [GameParameter.ParameterDesc("闘技場の順位アイコン")] ARENA_RANKING_RANK_ICON = 2445, // 0x0000098D
      [GameParameter.ParameterDesc("闘技場パーティのリーダースキルの背景画像")] ARENA_LEADERSKILL_IMAGE = 2446, // 0x0000098E
      [GameParameter.ParameterDesc("闘技場プレイヤーのリーダースキルの背景画像"), GameParameter.InstanceTypes(typeof (GameParameter.ArenaPlayerInstanceTypes))] ARENAPLAYER_LEADERSKILLIMAGE = 2447, // 0x0000098F
      GLOBAL_PLAYER_RANKMATCH_ED = 2498, // 0x000009C2
      ABILITY_ST = 2499, // 0x000009C3
      [GameParameter.ParameterDesc("派生アビリティならActiveになる"), GameParameter.AlwaysUpdate] ABILITY_DERIVED_BADGE = 2500, // 0x000009C4
      [GameParameter.ParameterDesc("派生アビリティならテキストの色が変わる")] ABILITY_DERIVED_TEXTCOLOR = 2501, // 0x000009C5
      [GameParameter.ParameterDesc("アビリティの種類詳細によってタイトルが変わる（SpriteSheet）")] ABILITY_TITLE_DETAIL_TYPE = 2502, // 0x000009C6
      [GameParameter.ParameterDesc("アビリティの最大RANK")] ABILITY_RANKCAP = 2503, // 0x000009C7
      [GameParameter.ParameterDesc("アビリティのRANK(色変化付き)")] ABILITY_ADDRANK = 2504, // 0x000009C8
      ABILITY_ED = 2598, // 0x00000A26
      SKILL_ST = 2599, // 0x00000A27
      [GameParameter.ParameterDesc("派生スキルならテキストの色が変わる")] SKILL_DERIVED_TEXTCOLOR = 2600, // 0x00000A28
      SKILL_ED = 2698, // 0x00000A8A
      ARTIFACT_SETEFFECT_ST = 2699, // 0x00000A8B
      [GameParameter.ParameterDesc("セット効果の発動条件のアイコン"), GameParameter.UsesIndex] ARTIFACT_SETEFFECT_TRIGGER_ICON = 2700, // 0x00000A8C
      ARTIFACT_SETEFFECT_ED = 2798, // 0x00000AEE
      CONCEPTCARD_ST = 2799, // 0x00000AEF
      [GameParameter.ParameterDesc("真理念装の限界突破回数を示すアイコン")] CONCEPTCARD_AWAKE = 2800, // 0x00000AF0
      [GameParameter.ParameterDesc("真理念装の限界突破回数を示す星アイコン")] CONCEPTCARD_AWAKESTAR = 2801, // 0x00000AF1
      [GameParameter.ParameterDesc("真理念装のグループアイコン")] CONCEPTCARD_GROUPICON = 2802, // 0x00000AF2
      CONCEPTCARD_ED = 2898, // 0x00000B52
      GAMEOBJECT_ST = 2899, // 0x00000B53
      [GameParameter.ParameterDesc("ゲームオブジェクトの表示/非表示")] GAMEOBJECT_ACTIVE = 2900, // 0x00000B54
      GAMEOBJECT_INACTIVE = 2901, // 0x00000B55
      GAMEOBJECT_ED = 2998, // 0x00000BB6
      GUILD_ST = 2999, // 0x00000BB7
      [GameParameter.ParameterDesc("ギルド名")] GUILD_NAME = 3000, // 0x00000BB8
      [GameParameter.ParameterDesc("ギルドエンブレム")] GUILD_EMBLEM = 3001, // 0x00000BB9
      [GameParameter.ParameterDesc("ギルド掲示板")] GUILD_BOARD = 3002, // 0x00000BBA
      [GameParameter.ParameterDesc("ギルドメンバー数")] GUILD_MEMBER_COUNT = 3003, // 0x00000BBB
      [GameParameter.ParameterDesc("ギルドメンバー最大数")] GUILD_MEMBER_COUNTMAX = 3004, // 0x00000BBC
      [GameParameter.ParameterDesc("ギルドメンバー名前")] GUILD_MEMBER_NAME = 3005, // 0x00000BBD
      [GameParameter.ParameterDesc("ギルドメンバーのレベル")] GUILD_MEMBER_LEVEL = 3006, // 0x00000BBE
      [GameParameter.ParameterDesc("ギルドメンバーの最終ログイン日時")] GUILD_MEMBER_LASTLOGIN = 3007, // 0x00000BBF
      [GameParameter.ParameterDesc("ギルドレベル")] GUILD_LEVEL = 3008, // 0x00000BC0
      [GameParameter.ParameterDesc("ギルド加入条件：レベルその1")] GUILD_CONDITIONS_LEVEL1 = 3009, // 0x00000BC1
      [GameParameter.ParameterDesc("ギルド加入条件：レベルその2")] GUILD_CONDITIONS_LEVEL2 = 3010, // 0x00000BC2
      [GameParameter.ParameterDesc("ギルドマスター(表示)")] GUILD_ROLE_MASTER_ACTIVE = 3011, // 0x00000BC3
      [GameParameter.ParameterDesc("サブギルドマスター(表示)")] GUILD_ROLE_SUBMASTER_ACTIVE = 3012, // 0x00000BC4
      [GameParameter.ParameterDesc("ギルドマスorサブマス(表示)")] GUILD_ROLE_MANAGER_ACTIVE = 3013, // 0x00000BC5
      [GameParameter.ParameterDesc("ギルドマスorサブマス(非表示)")] GUILD_ROLE_MANAGER_INACTIVE = 3014, // 0x00000BC6
      [GameParameter.ParameterDesc("ギルドマスター名")] GUILD_MASTER_NAME = 3015, // 0x00000BC7
      [GameParameter.ParameterDesc("ギルド募集メッセージ")] GUILD_INVITE_MESSAGE = 3016, // 0x00000BC8
      [GameParameter.ParameterDesc("ギルド役職画像")] GUILD_ROLE_IMAGE = 3017, // 0x00000BC9
      [GameParameter.ParameterDesc("ギルド自動承認ON/OFF")] GUILD_AUTOAPPROVAL = 3018, // 0x00000BCA
      [GameParameter.ParameterDesc("ギルドマスターのユニット")] GUILD_MASTER_UNIT = 3019, // 0x00000BCB
      [GameParameter.ParameterDesc("ギルドID")] GUILD_ID = 3020, // 0x00000BCC
      [GameParameter.ParameterDesc("ギルド設立日")] GUILD_CREATEDAT = 3021, // 0x00000BCD
      [GameParameter.ParameterDesc("ギルド施設強化の1日の上限値")] GUILD_FACILITY_INVEST_LIMIT = 3022, // 0x00000BCE
      [GameParameter.ParameterDesc("現在のギルド施設強化値")] GUILD_FACILITY_INVEST_CURRENT = 3023, // 0x00000BCF
      [GameParameter.ParameterDesc("ギルド施設の画像")] GUILD_FACILITY_IMAGE = 3024, // 0x00000BD0
      [GameParameter.ParameterDesc("他人ギルド名")] GUILD_OTHER_NAME = 3025, // 0x00000BD1
      [GameParameter.ParameterDesc("他人ギルドエンブレム")] GUILD_OTHER_EMBLEM = 3026, // 0x00000BD2
      [GameParameter.ParameterDesc("他人ギルドレベル")] GUILD_OTHER_LEVEL = 3027, // 0x00000BD3
      [GameParameter.ParameterDesc("他人ギルドの人数")] GUILD_OTHER_MEMBER_COUNT = 3028, // 0x00000BD4
      [GameParameter.ParameterDesc("他人ギルドの最大人数")] GUILD_OTHER_MEMBER_COUNTMAX = 3029, // 0x00000BD5
      [GameParameter.ParameterDesc("他人ギルド加入中にON")] GUILD_OTHER_TOGGLE_ON = 3030, // 0x00000BD6
      [GameParameter.ParameterDesc("他人ギルド加入中にOFF")] GUILD_OTHER_TOGGLE_OFF = 3031, // 0x00000BD7
      [GameParameter.ParameterDesc("ギルドメンバー表示時の枠画像")] GUILD_MEMBER_FRAMEIMAGE = 3032, // 0x00000BD8
      [GameParameter.ParameterDesc("ギルド加入中にON")] GUILD_SELF_TOGGLE_ON = 3033, // 0x00000BD9
      [GameParameter.ParameterDesc("ギルド加入中にOFF")] GUILD_SELF_TOGGLE_OFF = 3034, // 0x00000BDA
      [GameParameter.ParameterDesc("ギルド名")] GUILD_SELF_NAME = 3035, // 0x00000BDB
      [GameParameter.ParameterDesc("ギルド施設レベル")] GUILD_FACILITY_LEVEL = 3036, // 0x00000BDC
      [GameParameter.ParameterDesc("ギルド施設強化アイテムのポイント値"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes))] GUILD_FACILITY_ENHANCEPOINT = 3037, // 0x00000BDD
      [GameParameter.ParameterDesc("ギルドに所属しているなら表示/所属してないなら非表示")] GUILD_JOIN = 3038, // 0x00000BDE
      [GameParameter.ParameterDesc("他人ギルドマスター名")] GUILD_OTHER_MASTERNAME = 3039, // 0x00000BDF
      [GameParameter.ParameterDesc("ギルドのポートレイドが開放されてるかどうか？")] GUILD_GUILDRAIDOPEN = 3040, // 0x00000BE0
      [GameParameter.ParameterDesc("ギルドのポートレイドが閉じているかどうか？")] GUILD_GUILDRAIDCLOSE = 3041, // 0x00000BE1
      [GameParameter.ParameterDesc("他人ギルド設立日")] GUILD_OTHER_CREATEDAT = 3042, // 0x00000BE2
      [GameParameter.ParameterDesc("ポート出席報酬の未受け取りバッジ")] GUILD_BADGE_ATTEND = 3043, // 0x00000BE3
      [GameParameter.ParameterDesc("ポートMaster/SubMaster報酬の未受け取りバッジ")] GUILD_BADGE_ROLE_BONUS = 3044, // 0x00000BE4
      [GameParameter.ParameterDesc("ポートへの出席状況画像")] GUILD_ATTEND_STATUS = 3045, // 0x00000BE5
      [GameParameter.ParameterDesc("ポート方針")] GUILD_CONDITIONS_POLICY = 3046, // 0x00000BE6
      [GameParameter.ParameterDesc("ポートショップレベル")] GUILD_FACILITY_SHOP_LEVEL = 3047, // 0x00000BE7
      [GameParameter.ParameterDesc("本部レベル")] GUILD_FACILITY_BASE_LEVEL = 3048, // 0x00000BE8
      [GameParameter.ParameterDesc("自身のギルドの場合アクティブになるオブジェクト"), GameParameter.AlwaysUpdate] GUILD_OWNGUILD_ACTIVE = 3049, // 0x00000BE9
      [GameParameter.ParameterDesc("自身のギルドではない場合アクティブになるオブジェクト"), GameParameter.AlwaysUpdate] GUILD_OWNGUILD_NONACTIVE = 3050, // 0x00000BEA
      [GameParameter.ParameterDesc("ポートミッション報酬の未受け取りバッジ"), GameParameter.AlwaysUpdate] GUILD_BADGE_TROPHY_REWARD = 3051, // 0x00000BEB
      GUILD_ED = 3098, // 0x00000C1A
      BUTTON_ST = 3099, // 0x00000C1B
      [GameParameter.ParameterDesc("ボタンの有効/無効")] BUTTON_INTERACTABLE = 3100, // 0x00000C1C
      BUTTON_ED = 3198, // 0x00000C7E
      RAID_ST = 3199, // 0x00000C7F
      [GameParameter.ParameterDesc("レイド名")] RAID_NAME = 3200, // 0x00000C80
      [GameParameter.ParameterDesc("レイドHP数値")] RAID_HP_VALUE = 3201, // 0x00000C81
      [GameParameter.ParameterDesc("レイド最大HP")] RAID_HP_MAX = 3202, // 0x00000C82
      [GameParameter.ParameterDesc("レイドHP％")] RAID_HP_PERCENT = 3203, // 0x00000C83
      [GameParameter.ParameterDesc("レイドHPゲージ")] RAID_HP_GAUGE = 3204, // 0x00000C84
      [GameParameter.ParameterDesc("レイドアイコン")] RAID_ICON_MEDIUM = 3205, // 0x00000C85
      [GameParameter.ParameterDesc("レイド一枚絵")] RAID_IMAGE = 3206, // 0x00000C86
      [GameParameter.ParameterDesc("レイド救援一覧レイドアイコン")] RAID_RESCUE_RAID_ICON = 3207, // 0x00000C87
      [GameParameter.ParameterDesc("レイド救援一覧レイドレベル（周回数）")] RAID_RESCUE_RAID_ROUND = 3208, // 0x00000C88
      [GameParameter.ParameterDesc("レイド救援一覧レイド名")] RAID_RESCUE_RAID_NAME = 3209, // 0x00000C89
      [GameParameter.ParameterDesc("レイド救援一覧レイドエリア")] RAID_RESCUE_RAID_AREAORDER = 3210, // 0x00000C8A
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤー名")] RAID_RESCUE_PLAYER_NAME = 3211, // 0x00000C8B
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤーLV")] RAID_RESCUE_PLAYER_LEVEL = 3212, // 0x00000C8C
      [GameParameter.ParameterDesc("レイド救援一覧残り時間")] RAID_RESCUE_REMAIN = 3213, // 0x00000C8D
      [GameParameter.ParameterDesc("レイド救援一覧HP数値")] RAID_RESCUE_RAID_HP_VALUE = 3214, // 0x00000C8E
      [GameParameter.ParameterDesc("レイド救援一覧最大HP")] RAID_RESCUE_RAID_HP_MAX = 3215, // 0x00000C8F
      [GameParameter.ParameterDesc("レイド救援一覧HP％")] RAID_RESCUE_RAID_HP_PERCENT = 3216, // 0x00000C90
      [GameParameter.ParameterDesc("レイド救援一覧HPゲージ")] RAID_RESCUE_RAID_HP_GAUGE = 3217, // 0x00000C91
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤータイプ"), GameParameter.AlwaysUpdate] RAID_RESCUE_PLAYER_TYPE = 3218, // 0x00000C92
      [GameParameter.ParameterDesc("レイド救援プレイヤー名")] RAID_SOS_PLAYER_NAME = 3219, // 0x00000C93
      [GameParameter.ParameterDesc("レイド救援プレイヤーLV")] RAID_SOS_PLAYER_LEVEL = 3220, // 0x00000C94
      [GameParameter.ParameterDesc("レイド救援プレイヤータイプ"), GameParameter.AlwaysUpdate] RAID_SOS_PLAYER_TYPE = 3221, // 0x00000C95
      [GameParameter.ParameterDesc("レイド救援プレイヤー最終攻撃時間")] RAID_SOS_BATTLETIME = 3222, // 0x00000C96
      [GameParameter.ParameterDesc("レイド詳細撃挑戦ボタン"), GameParameter.AlwaysUpdate] RAID_BUTTON_CHALLENGE = 3223, // 0x00000C97
      [GameParameter.ParameterDesc("レイド詳細救援ボタン"), GameParameter.AlwaysUpdate, GameParameter.InstanceTypes(typeof (GameParameter.RaidRescueStatusType))] RAID_BUTTON_RESCUE_SEND = 3224, // 0x00000C98
      [GameParameter.ParameterDesc("レイド詳細救援終了ボタン"), GameParameter.AlwaysUpdate] RAID_BUTTON_RESCUE_CANCEL = 3225, // 0x00000C99
      [GameParameter.ParameterDesc("レイド詳細撃破報酬受取ボタン"), GameParameter.AlwaysUpdate] RAID_BUTTON_REWARD = 3226, // 0x00000C9A
      [GameParameter.ParameterDesc("ホームのレイドアイコン")] RAID_PERIOD_BUTTON = 3227, // 0x00000C9B
      [GameParameter.ParameterDesc("レイドHPゲージカラー")] RAID_HP_GAUGE_COLOR = 3228, // 0x00000C9C
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤーユニット")] RAID_RESCUE_PLAYER_UNIT = 3229, // 0x00000C9D
      [GameParameter.ParameterDesc("レイド救援プレイヤーユニット")] RAID_SOS_PLAYER_UNIT = 3230, // 0x00000C9E
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤー称号")] RAID_RESCUE_PLAYER_AWARD = 3231, // 0x00000C9F
      [GameParameter.ParameterDesc("レイド救援一覧プレイヤー最終ログイン日時")] RAID_RESCUE_PLAYER_LASTLOGIN = 3232, // 0x00000CA0
      [GameParameter.ParameterDesc("レイド撃破ランキングランク画像")] RAID_RANKING_BEAT_RANK_IMAGE = 3233, // 0x00000CA1
      [GameParameter.ParameterDesc("レイド撃破ランキングランクテキスト")] RAID_RANKING_BEAT_RANK_TEXT = 3234, // 0x00000CA2
      [GameParameter.ParameterDesc("レイド撃破ランキングスコア")] RAID_RANKING_BEAT_SCORE = 3235, // 0x00000CA3
      [GameParameter.ParameterDesc("レイド撃破ランキングプレイヤー名")] RAID_RANKING_BEAT_NAME = 3236, // 0x00000CA4
      [GameParameter.ParameterDesc("レイド撃破ランキングプレイヤーレベル")] RAID_RANKING_BEAT_LEVEL = 3237, // 0x00000CA5
      [GameParameter.ParameterDesc("レイド属性")] RAID_ELEMENT = 3238, // 0x00000CA6
      [GameParameter.ParameterDesc("レイド救援一覧レイド属性")] RAID_RESCUE_RAID_ELEMENT = 3239, // 0x00000CA7
      [GameParameter.ParameterDesc("レイドギルドランキングランク画像")] RAID_RANKING_GUILD_RANK_IMAGE = 3240, // 0x00000CA8
      [GameParameter.ParameterDesc("レイドギルドランキングランクテキスト")] RAID_RANKING_GUILD_RANK_TEXT = 3241, // 0x00000CA9
      [GameParameter.ParameterDesc("レイドギルドランキングスコア")] RAID_RANKING_GUILD_SCORE = 3242, // 0x00000CAA
      [GameParameter.ParameterDesc("レイドギルドランキングプレイヤーレベル")] RAID_RANKING_GUILD_LEVEL = 3243, // 0x00000CAB
      [GameParameter.ParameterDesc("レイドギルド進捗ボタン")] RAID_GUILD_STATS_BUTTON = 3244, // 0x00000CAC
      [GameParameter.ParameterDesc("レイドギルド進捗撃破ランク")] RAID_GUILD_STATS_BEAT_RANK = 3245, // 0x00000CAD
      [GameParameter.ParameterDesc("レイドギルド進捗撃破スコア")] RAID_GUILD_STATS_BEAT_SCORE = 3246, // 0x00000CAE
      [GameParameter.ParameterDesc("レイドギルド進捗救援撃破ランク")] RAID_GUILD_STATS_RESCUE_RANK = 3247, // 0x00000CAF
      [GameParameter.ParameterDesc("レイドギルド進捗救援撃破スコア")] RAID_GUILD_STATS_RESCUE_SCORE = 3248, // 0x00000CB0
      [GameParameter.ParameterDesc("レイドギルド進捗メンバー撃破スコア")] RAID_GUILD_STATS_MEMBER_BEAT_SCORE = 3249, // 0x00000CB1
      [GameParameter.ParameterDesc("レイドギルド進捗メンバー救援撃破スコア")] RAID_GUILD_STATS_MEMBER_RESCUE_SCORE = 3250, // 0x00000CB2
      [GameParameter.ParameterDesc("レイドギルド進捗メンバー周回数")] RAID_GUILD_STATS_MEMBER_LAP = 3251, // 0x00000CB3
      [GameParameter.ParameterDesc("レイドスタンプラリーボタン")] RAID_STAMPRALLY = 3252, // 0x00000CB4
      [GameParameter.ParameterDesc("一番近いレイドが開催されている時間帯を取得する")] RAID_SCHEDULE_TIMEGET = 3253, // 0x00000CB5
      [GameParameter.ParameterDesc("レイドが開催されているなら非表示")] RAID_SCHEDULE_OPENNODRAW = 3254, // 0x00000CB6
      [GameParameter.ParameterDesc("レイドが開催してないなら非表示")] RAID_SCHEDULE_CLOSENODRAW = 3255, // 0x00000CB7
      [GameParameter.ParameterDesc("レイドが開催してないならクリック不可")] RAID_SCHEDULE_CLOSEINTRACTALE = 3256, // 0x00000CB8
      [GameParameter.ParameterDesc("レイド詳細画面の休戦中ボタン")] RAID_BUTTON_INFOSCHEDULECLOSE = 3257, // 0x00000CB9
      [GameParameter.ParameterDesc("レイドのスケジュールが登録されてないなら非表示")] RAID_BUTTON_NOSHEDULE = 3258, // 0x00000CBA
      [GameParameter.ParameterDesc("次回開催中のみ表示")] RAID_BUTTON_CLOTHSCHEDULE = 3259, // 0x00000CBB
      [GameParameter.ParameterDesc("レイドランキング結果の開催期間")] RAID_RANKREWARD_PERIODID = 3260, // 0x00000CBC
      [GameParameter.ParameterDesc("レイドランキング結果の個人ランク")] RAID_RANKREWARD_RANK = 3261, // 0x00000CBD
      [GameParameter.ParameterDesc("レイドランキング結果の個人スコア")] RAID_RANKREWARD_SCORE = 3262, // 0x00000CBE
      [GameParameter.ParameterDesc("レイドランキング結果の救援ランク")] RAID_RANKREWARD_RESQUERANK = 3263, // 0x00000CBF
      [GameParameter.ParameterDesc("レイドランキング結果の救援スコア")] RAID_RANKREWARD_RESQUESCORE = 3264, // 0x00000CC0
      [GameParameter.ParameterDesc("レイドランキング結果のギルドランク")] RAID_RANKREWARD_GUILDRANK = 3265, // 0x00000CC1
      [GameParameter.ParameterDesc("レイドランキング結果のギルドスコア")] RAID_RANKREWARD_GUILDSCORE = 3266, // 0x00000CC2
      [GameParameter.ParameterDesc("レイドランキング結果のギルドアイコン")] RAID_RANKREWARD_GUILDAWARD = 3267, // 0x00000CC3
      [GameParameter.ParameterDesc("レイドランキング結果のギルド名")] RAID_RANKREWARD_GUILDNAME = 3268, // 0x00000CC4
      [GameParameter.ParameterDesc("レイドランキング結果のギルドLv")] RAID_RANKREWARD_GUILDLEVEL = 3269, // 0x00000CC5
      [GameParameter.ParameterDesc("レイドランキング結果のギルドマスターの名前")] RAID_RANKREWARD_GUILDMASTERNAME = 3270, // 0x00000CC6
      [GameParameter.ParameterDesc("レイドランキング結果のギルドメンバー数")] RAID_RANKREWARD_GUILDMEMBER = 3271, // 0x00000CC7
      [GameParameter.ParameterDesc("レイドランキング結果のギルド最大メンバー数")] RAID_RANKREWARD_GUILDMAXMEMBER = 3272, // 0x00000CC8
      RAID_ED = 3298, // 0x00000CE2
      GENESIS_ST = 3299, // 0x00000CE3
      [GameParameter.ParameterDesc("創世編ボス名")] GENESIS_BOSS_NAME = 3300, // 0x00000CE4
      [GameParameter.ParameterDesc("創世編ボス一枚絵")] GENESIS_BOSS_IMAGE = 3301, // 0x00000CE5
      [GameParameter.ParameterDesc("創世編ボス属性アイコン")] GENESIS_BOSS_ELEMENT = 3302, // 0x00000CE6
      [GameParameter.ParameterDesc("創世編ボス現在HP")] GENESIS_BOSS_HP_CURRENT = 3303, // 0x00000CE7
      [GameParameter.ParameterDesc("創世編ボス最大HP")] GENESIS_BOSS_HP_MAX = 3304, // 0x00000CE8
      [GameParameter.ParameterDesc("創世編ボスHPゲージ")] GENESIS_BOSS_HP_GAUGE = 3305, // 0x00000CE9
      [GameParameter.ParameterDesc("創世編ボス挑戦ボタン")] GENESIS_BOSS_CHALLENGE_BUTTON = 3306, // 0x00000CEA
      [GameParameter.ParameterDesc("創世編ボスExtra存在するなら表示(0)/非表示(1)")] GENESIS_BOSS_IS_EXIST_EXTRA = 3307, // 0x00000CEB
      [GameParameter.ParameterDesc("創世編周回ボスなら表示(0)/非表示(1)")] GENESIS_BOSS_IS_LAP_BOSS = 3308, // 0x00000CEC
      [GameParameter.ParameterDesc("創世編周回ボスラップ数")] GENESIS_BOSS_LAP_COUNT = 3309, // 0x00000CED
      [GameParameter.ParameterDesc("創世編章TOP開催期間")] GENESIS_CHAPTER_PERIOD = 3319, // 0x00000CF7
      [GameParameter.ParameterDesc("創世編開催期間中であれば表示(0)/非表示(1)")] GENESIS_IS_CHECK_PERIOD = 3339, // 0x00000D0B
      GENESIS_ED = 3398, // 0x00000D46
      ADVANCE_ST = 3399, // 0x00000D47
      [GameParameter.ParameterDesc("新イベントボス名")] ADVANCE_BOSS_NAME = 3400, // 0x00000D48
      [GameParameter.ParameterDesc("新イベントボス一枚絵")] ADVANCE_BOSS_IMAGE = 3401, // 0x00000D49
      [GameParameter.ParameterDesc("新イベントボス属性アイコン")] ADVANCE_BOSS_ELEMENT = 3402, // 0x00000D4A
      [GameParameter.ParameterDesc("新イベントボス現在HP")] ADVANCE_BOSS_HP_CURRENT = 3403, // 0x00000D4B
      [GameParameter.ParameterDesc("新イベントボス最大HP")] ADVANCE_BOSS_HP_MAX = 3404, // 0x00000D4C
      [GameParameter.ParameterDesc("新イベントボスHPゲージ")] ADVANCE_BOSS_HP_GAUGE = 3405, // 0x00000D4D
      [GameParameter.ParameterDesc("新イベントボス挑戦ボタン")] ADVANCE_BOSS_CHALLENGE_BUTTON = 3406, // 0x00000D4E
      [GameParameter.ParameterDesc("新イベントボスExtra存在するなら表示(0)/非表示(1)")] ADVANCE_BOSS_IS_EXIST_EXTRA = 3407, // 0x00000D4F
      [GameParameter.ParameterDesc("新イベント周回ボスなら表示(0)/非表示(1)")] ADVANCE_BOSS_IS_LAP_BOSS = 3408, // 0x00000D50
      [GameParameter.ParameterDesc("新イベント周回ボスラップ数")] ADVANCE_BOSS_LAP_COUNT = 3409, // 0x00000D51
      [GameParameter.ParameterDesc("新イベント全体の開催期間")] ADVANCE_PERIOD = 3419, // 0x00000D5B
      [GameParameter.ParameterDesc("新イベント(カレント)開催期間")] ADVANCE_EVENT_PERIOD_CURRENT = 3420, // 0x00000D5C
      [GameParameter.ParameterDesc("新イベント開催期間")] ADVANCE_EVENT_PERIOD = 3421, // 0x00000D5D
      [GameParameter.ParameterDesc("新イベント全体が開催期間中であれば表示(0)/非表示(1)")] ADVANCE_IS_CHECK_PERIOD = 3439, // 0x00000D6F
      [GameParameter.ParameterDesc("新イベント(カレント)が開催期間中であれば表示(0)/非表示(1)")] ADVANCE_IS_CHECK_EVENT_PERIOD_CURRENT = 3440, // 0x00000D70
      [GameParameter.ParameterDesc("新イベントが開催期間中であれば表示(0)/非表示(1)")] ADVANCE_IS_CHECK_EVENT_PERIOD = 3441, // 0x00000D71
      ADVANCE_ED = 3498, // 0x00000DAA
      EXTRA_KAKERA_ST = 3499, // 0x00000DAB
      [GameParameter.ParameterDesc("欠片クエストがエクストラであれば表示/非表示")] EXTRA_KAKERA_DIFFICULTY_SYMBOL = 3500, // 0x00000DAC
      EXTRA_KAKERA_ED = 3598, // 0x00000E0E
      TROPHY_ST = 3599, // 0x00000E0F
      [GameParameter.ParameterDesc("トロフィーのスター獲得量")] TROPHY_STAR_COUNT = 3600, // 0x00000E10
      [GameParameter.ParameterDesc("スター獲得量が'0'であれば表示(0)/非表示(1)")] TROPHY_STAR_MISSION_IS_CHECK_ZERO_STAR_NUM = 3601, // 0x00000E11
      TROPHY_ED = 3698, // 0x00000E72
      LOGINBONUS_ST = 3699, // 0x00000E73
      [GameParameter.ParameterDesc("ログインボーナスの未受け取りバッチの表示/非表示")] LOGINBONUS_IS_NOT_RECEIVED = 3700, // 0x00000E74
      [GameParameter.ParameterDesc("ログインボーナスの日付")] LOGINBONUS_DAY = 3701, // 0x00000E75
      [GameParameter.ParameterDesc("累計ログイン回数の条件日数")] LOGINBONUS_TOTAL_LOGIN_COUNT_COND_VALUE = 3702, // 0x00000E76
      [GameParameter.ParameterDesc("ログインボーナスの補填が可能or累計ログインレコミが受け取れるか")] LOGINBONUS_IS_CAN_REWARD = 3703, // 0x00000E77
      LOGINBONUS_ED = 3798, // 0x00000ED6
      GUILDRAID_ST = 3799, // 0x00000ED7
      [GameParameter.ParameterDesc("ギルドレイド名")] GUILDRAID_NAME = 3800, // 0x00000ED8
      [GameParameter.ParameterDesc("ギルドレイドHP数値")] GUILDRAID_HP_VALUE = 3801, // 0x00000ED9
      [GameParameter.ParameterDesc("ギルドレイド最大HP")] GUILDRAID_HP_MAX = 3802, // 0x00000EDA
      [GameParameter.ParameterDesc("ギルドレイドHP％")] GUILDRAID_HP_PERCENT = 3803, // 0x00000EDB
      [GameParameter.ParameterDesc("ギルドレイドHPゲージ")] GUILDRAID_HP_GAUGE = 3804, // 0x00000EDC
      [GameParameter.ParameterDesc("ギルドレイドアイコン")] GUILDRAID_ICON_MEDIUM = 3805, // 0x00000EDD
      [GameParameter.ParameterDesc("ギルドレイド一枚絵")] GUILDRAID_IMAGE = 3806, // 0x00000EDE
      [GameParameter.ParameterDesc("ギルドレイド詳細撃挑戦ボタン"), GameParameter.AlwaysUpdate] GUILDRAID_BUTTON_CHALLENGE = 3807, // 0x00000EDF
      [GameParameter.ParameterDesc("ギルドレイドHPゲージカラー")] GUILDRAID_HP_GAUGE_COLOR = 3808, // 0x00000EE0
      [GameParameter.ParameterDesc("ギルドレイド属性")] GUILDRAID_ELEMENT = 3809, // 0x00000EE1
      [GameParameter.ParameterDesc("一番近いギルドレイドが開催されている時間帯を取得する")] GUILDRAID_SCHEDULE_TIMEGET = 3810, // 0x00000EE2
      [GameParameter.ParameterDesc("ギルドレイドが開催されているなら非表示")] GUILDRAID_SCHEDULE_OPENNODRAW = 3811, // 0x00000EE3
      [GameParameter.ParameterDesc("ギルドレイドが開催してないなら非表示")] GUILDRAID_SCHEDULE_CLOSENODRAW = 3812, // 0x00000EE4
      [GameParameter.ParameterDesc("ギルドレイドが開催してないならクリック不可")] GUILDRAID_SCHEDULE_CLOSEINTRACTALE = 3813, // 0x00000EE5
      [GameParameter.ParameterDesc("ギルドレイド詳細画面の休戦中ボタン")] GUILDRAID_BUTTON_INFOSCHEDULECLOSE = 3814, // 0x00000EE6
      [GameParameter.ParameterDesc("ギルドレイドのスケジュールが登録されてないなら非表示")] GUILDRAID_BUTTON_NOSHEDULE = 3815, // 0x00000EE7
      [GameParameter.ParameterDesc("次回開催中のみ表示")] GUILDRAID_BUTTON_CLOTHSCHEDULE = 3816, // 0x00000EE8
      [GameParameter.ParameterDesc("ギルドレイド詳細の報酬受取ボタン")] GUILDRAID_BUTTON_REWARD = 3817, // 0x00000EE9
      [GameParameter.ParameterDesc("ギルドレイド入り口の画像切り替え(ImageArray番号)\n　(0):終了\n　(1):開催中\n　(2):休戦中")] GUILDRAID_ENTRANCE = 3818, // 0x00000EEA
      [GameParameter.ParameterDesc("ギルドレイドBOSSチャレンジ中の名前")] GUILDRAID_CHALLENGE_NAME = 3819, // 0x00000EEB
      [GameParameter.ParameterDesc("ギルドレイドBOSSチャレンジ中のレベル")] GUILDRAID_CHALLENGE_LEVEL = 3820, // 0x00000EEC
      [GameParameter.ParameterDesc("ギルドレイドBOSSチャレンジ中のユニット")] GUILDRAID_CHALLENGE_UNIT = 3821, // 0x00000EED
      [GameParameter.ParameterDesc("ギルドレイドBOSSチャレンジ中のユニットの役職")] GUILDRAID_CHALLENGE_ROLEIMAGE = 3822, // 0x00000EEE
      [GameParameter.ParameterDesc("バトルログ：メッセージ")] GUILDRAID_BATTLELOG_MESSAGE = 3823, // 0x00000EEF
      [GameParameter.ParameterDesc("バトルログ：日時")] GUILDRAID_BATTLELOG_POSTEDAT = 3824, // 0x00000EF0
      [GameParameter.ParameterDesc("ギルドレイド挑戦可能数")] GUILDRAID_BP_CHALLENGE = 3825, // 0x00000EF1
      [GameParameter.ParameterDesc("ギルドレイド最大挑戦可能数")] GUILDRAID_BP_MAX = 3826, // 0x00000EF2
      [GameParameter.ParameterDesc("ギルドレイドギフト名")] GUILDRAID_GIFT_NAME = 3827, // 0x00000EF3
      [GameParameter.ParameterDesc("ギルドランキングのポートレベル")] GUILDRAID_RANKING_GUILD_LEVEL = 3828, // 0x00000EF4
      [GameParameter.ParameterDesc("ギルドランキングのポート名")] GUILDRAID_RANKING_GUILD_NAME = 3829, // 0x00000EF5
      [GameParameter.ParameterDesc("ギルドランキングのマスター名")] GUILDRAID_RANKING_GUILD_MASTERNAME = 3830, // 0x00000EF6
      [GameParameter.ParameterDesc("ギルドランキングのメンバー数")] GUILDRAID_RANKING_GUILD_MEMBER = 3831, // 0x00000EF7
      [GameParameter.ParameterDesc("ギルドランキングの最大メンバー数")] GUILDRAID_RANKING_GUILD_MEMBERMAX = 3832, // 0x00000EF8
      [GameParameter.ParameterDesc("ギルドランキングのポートスコア")] GUILDRAID_RANKING_GUILD_SCORE = 3833, // 0x00000EF9
      [GameParameter.ParameterDesc("ギルドレイドBOSS番号")] GUILDRAID_BOSSNO = 3834, // 0x00000EFA
      [GameParameter.ParameterDesc("ギルドレイドアイコン（小）")] GUILDRAID_ICON_SMALL = 3835, // 0x00000EFB
      [GameParameter.ParameterDesc("ギルドレイドLAP数")] GUILDRAID_LAP = 3836, // 0x00000EFC
      [GameParameter.ParameterDesc("ギルドレイドランキングのランキング")] GUILDRAID_RANKING_GUILD_RANK = 3837, // 0x00000EFD
      [GameParameter.ParameterDesc("ギルドレイドTOPのダメージランキングリストのBOSS画像")] GUILDRAID_RANKING_DMGBOSS_BOSSIMAGE = 3838, // 0x00000EFE
      [GameParameter.ParameterDesc("ギルドレイドTOPのダメージランキングリストの討伐時間")] GUILDRAID_RANKING_DMGBOSS_BEATTIME = 3839, // 0x00000EFF
      [GameParameter.ParameterDesc("ギルドレイドギルド開始期間")] GUILDRAID_OPENSCHEJUDE = 3840, // 0x00000F00
      [GameParameter.ParameterDesc("ギルドレイドギルド終了期間")] GUILDRAID_CLOSESCHEJUDE = 3841, // 0x00000F01
      [GameParameter.ParameterDesc("ギルドレイドギルド報酬受取開始期間")] GUILDRAID_REWARDOPENSCHEJUDE = 3842, // 0x00000F02
      [GameParameter.ParameterDesc("ギルドレイドギルド報酬受取終了期間")] GUILDRAID_REWARDCLOSESCHEJUDE = 3843, // 0x00000F03
      [GameParameter.ParameterDesc("ギルドレイドTOP撃挑戦ボタン"), GameParameter.AlwaysUpdate] GUILDRAID_BUTTON_CHALLENGETOP = 3844, // 0x00000F04
      [GameParameter.ParameterDesc("ギルドレイドTOP未受け取りギフトバッジ"), GameParameter.AlwaysUpdate] GUILDRAID_MAILBOX_BADGE = 3845, // 0x00000F05
      [GameParameter.ParameterDesc("ギルドレイドBOSSチャレンジ中ユニットの開始時間")] GUILDRAID_CHALLENGE_TIME = 3846, // 0x00000F06
      [GameParameter.ParameterDesc("ギルドレイド結果の期間")] GUILDRAID_RANKING_REWARD_PERIOD = 3847, // 0x00000F07
      [GameParameter.ParameterDesc("ギルドレイド結果のランク")] GUILDRAID_RANKING_REWARD_RANK = 3848, // 0x00000F08
      [GameParameter.ParameterDesc("ギルドレイド結果のスコア")] GUILDRAID_RANKING_REWARD_SCORE = 3849, // 0x00000F09
      [GameParameter.ParameterDesc("ギルドレイドランキングのエンブレム")] GUILDRAID_RANKING_GUILD_EMBLEM = 3850, // 0x00000F0A
      [GameParameter.ParameterDesc("ギルドレイドランキングの順位画像")] GUILDRAID_RANKING_GUILD_RANKIMAGE = 3851, // 0x00000F0B
      [GameParameter.ParameterDesc("ギルドレイドランキングの順位文字")] GUILDRAID_RANKING_GUILD_RANKTEXT = 3852, // 0x00000F0C
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングのアイコン")] GUILDRAID_RANKING_MEMBER_ICON = 3853, // 0x00000F0D
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングのギルド役職")] GUILDRAID_RANKING_MEMBER_ROLE = 3854, // 0x00000F0E
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングの名前")] GUILDRAID_RANKING_MEMBER_NAME = 3855, // 0x00000F0F
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングのレベル")] GUILDRAID_RANKING_MEMBER_LEVEL = 3856, // 0x00000F10
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングのスコア")] GUILDRAID_RANKING_MEMBER_SCORE = 3857, // 0x00000F11
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングの総戦闘力")] GUILDRAID_RANKING_MEMBER_POWER = 3858, // 0x00000F12
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングリストのランク画像")] GUILDRAID_RANKING_MEMBER_RANKIMAGE = 3859, // 0x00000F13
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングリストのランクテキスト")] GUILDRAID_RANKING_MEMBER_RANKTEXT = 3860, // 0x00000F14
      [GameParameter.ParameterDesc("ギルドレイドBP残り回数")] GUILDRAID_BP_REMAIN = 3861, // 0x00000F15
      [GameParameter.ParameterDesc("ギルドレイド消費AP")] GUILDRAID_AP_CONSUMPTION = 3862, // 0x00000F16
      [GameParameter.ParameterDesc("ギルドレイド必要最大AP")] GUILDRAID_AP_MAX = 3863, // 0x00000F17
      [GameParameter.ParameterDesc("ギルドレイドTOP画面のランキング")] GUILDRAID_HOME_RANKING_RANK = 3864, // 0x00000F18
      [GameParameter.ParameterDesc("ギルドレイド個人ランキングのランク")] GUILDRAID_RANKING_MEMBER_RANK = 3865, // 0x00000F19
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのランキング画像")] GUILDRAID_RANKING_DMGBOSS_RANKIMAGE = 3866, // 0x00000F1A
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのランキング")] GUILDRAID_RANKING_DMGBOSS_RANKTEXT = 3867, // 0x00000F1B
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのアイコン")] GUILDRAID_RANKING_DMGBOSS_ICON = 3868, // 0x00000F1C
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストの名前")] GUILDRAID_RANKING_DMGBOSS_NAME = 3869, // 0x00000F1D
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのレベル")] GUILDRAID_RANKING_DMGBOSS_LEVEL = 3870, // 0x00000F1E
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのスコア")] GUILDRAID_RANKING_DMGBOSS_SCORE = 3871, // 0x00000F1F
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストの総戦闘力")] GUILDRAID_RANKING_DMGBOSS_POWER = 3872, // 0x00000F20
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストのBOSS名")] GUILDRAID_RANKING_DMGBOSS_BOSSNAME = 3873, // 0x00000F21
      [GameParameter.ParameterDesc("ギルドレイドレポート：周回数")] GUILDRAID_REPORT_ROUND = 3874, // 0x00000F22
      [GameParameter.ParameterDesc("ギルドレイドレポート：与えたダメージ")] GUILDRAID_REPORT_DAMAGE = 3875, // 0x00000F23
      [GameParameter.ParameterDesc("ギルドレイドレポート：挑戦日")] GUILDRAID_REPORT_DATE_DAY = 3876, // 0x00000F24
      [GameParameter.ParameterDesc("ギルドレイド撃破ランキングランク画像")] GUILDRAID_RANKING_BEAT_RANK_IMAGE = 3877, // 0x00000F25
      [GameParameter.ParameterDesc("ギルドレイド撃破ランキングランクテキスト")] GUILDRAID_RANKING_BEAT_RANK_TEXT = 3878, // 0x00000F26
      [GameParameter.ParameterDesc("ギルドレイドレポート：挑戦時間")] GUILDRAID_REPORT_DATE_TIME = 3879, // 0x00000F27
      [GameParameter.ParameterDesc("ギルドレイド模擬戦ボタン")] GUILDRAID_BUTTON_TRIAL = 3880, // 0x00000F28
      [GameParameter.ParameterDesc("ギルドレイドクエスト名")] GUILDRAID_QUEST_NAME = 3881, // 0x00000F29
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストの役職")] GUILDRAID_RANKING_DMGBOSS_ROLE = 3882, // 0x00000F2A
      [GameParameter.ParameterDesc("ギルドレイド挑戦ボタンの挑戦不可ボタン")] GUILDRAID_BUTTON_NOCHALLENGE = 3883, // 0x00000F2B
      [GameParameter.ParameterDesc("ギルドレイドひとつ前のBOSSイメージ")] GUILDRAID_OLDIMAGE = 3884, // 0x00000F2C
      [GameParameter.ParameterDesc("レイド編成画面の出撃不可・出撃済みの表示（出撃不可：0、出撃済み：1）"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] GUILDRAID_PARTYEDITOR_ENABLE = 3885, // 0x00000F2D
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストの終了ランキング")] GUILDRAID_RANKING_BEAT_RANK_RANKENDTEXT = 3886, // 0x00000F2E
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストランクが単一")] GUILDRAID_RANKING_BEAT_RANK_RANKONLY = 3887, // 0x00000F2F
      [GameParameter.ParameterDesc("ギルドレイドBOSS挑戦中のダメージランキングリストランクが複数")] GUILDRAID_RANKING_BEAT_RANK_RANKMULTI = 3888, // 0x00000F30
      [GameParameter.ParameterDesc("ギルドレイド結果のギルド名")] GUILDRAID_RANKING_REWARD_GUILDNAME = 3889, // 0x00000F31
      [GameParameter.ParameterDesc("ギルドレイド結果のギルドレベル")] GUILDRAID_RANKING_REWARD_GUILDLEVEL = 3890, // 0x00000F32
      [GameParameter.ParameterDesc("ギルドレイド結果のギルドマスター")] GUILDRAID_RANKING_REWARD_MASTER = 3891, // 0x00000F33
      [GameParameter.ParameterDesc("ギルドレイド結果のエンブレム")] GUILDRAID_RANKING_REWARD_EMBLEM = 3892, // 0x00000F34
      [GameParameter.ParameterDesc("ギルドレイド結果のメンバー数")] GUILDRAID_RANKING_REWARD_MEMBER = 3893, // 0x00000F35
      [GameParameter.ParameterDesc("ギルドレイド結果のMAXメンバ数")] GUILDRAID_RANKING_REWARD_MEMBERFULL = 3894, // 0x00000F36
      [GameParameter.ParameterDesc("ギルドレイドの開催時に（ON：0 OFF：1）"), GameParameter.UsesIndex] GUILDRAID_ISPERIOD = 3895, // 0x00000F37
      [GameParameter.ParameterDesc("ギルドレイドの終了日時を表示")] GUILDRAID_PERIOD_TIME = 3896, // 0x00000F38
      [GameParameter.ParameterDesc("ギルドレイドの残りAP回復数")] GUILDRAID_BP_REMAIN_APHEAL = 3897, // 0x00000F39
      GUILDRAID_ED = 3898, // 0x00000F3A
      FILTER_ST = 3899, // 0x00000F3B
      [GameParameter.ParameterDesc("レアリティの★表示")] FILTER_TOGGLE_RARITY = 3900, // 0x00000F3C
      [GameParameter.ParameterDesc("名称の表示")] FILTER_TOGGLE_NAME = 3901, // 0x00000F3D
      [GameParameter.ParameterDesc("武具タイプアイコン")] FILTER_TOGGLE_ARTIFACT_TYPE = 3902, // 0x00000F3E
      [GameParameter.ParameterDesc("武具種の表示")] FILTER_ARTIFACT_TYPE_NAME = 3903, // 0x00000F3F
      [GameParameter.ParameterDesc("ルーンのセット効果のアイコン表示")] FILTER_RUNE_SETEFF_ICON = 3904, // 0x00000F40
      FILTER_ED = 3998, // 0x00000F9E
      JUKEBOX_ST = 3999, // 0x00000F9F
      [GameParameter.ParameterDesc("ジュークボックス曲名")] JUKEBOX_TITLE = 4000, // 0x00000FA0
      [GameParameter.ParameterDesc("ジュークボックス英語曲名")] JUKEBOX_TITLE_EN = 4001, // 0x00000FA1
      [GameParameter.ParameterDesc("ジュークボックス作詞家")] JUKEBOX_LYRICIST = 4002, // 0x00000FA2
      [GameParameter.ParameterDesc("ジュークボックス作曲者")] JUKEBOX_COMPOSER = 4003, // 0x00000FA3
      [GameParameter.ParameterDesc("ジュークボックスシチュエーション")] JUKEBOX_SITUATION = 4004, // 0x00000FA4
      [GameParameter.ParameterDesc("ジュークボックスセクション名")] JUKEBOX_SECTION = 4005, // 0x00000FA5
      JUKEBOX_ED = 4098, // 0x00001002
      GVG_ST = 4099, // 0x00001003
      [GameParameter.ParameterDesc("GvG開催中なら表示(0)/非表示(1)"), GameParameter.UsesIndex] GVG_OPEN = 4100, // 0x00001004
      [GameParameter.ParameterDesc("GvG報酬あるなら表示(0)/非表示(1)"), GameParameter.UsesIndex] GVG_HASREWARD = 4101, // 0x00001005
      [GameParameter.ParameterDesc("GvGの現在有効なルールが存在するか")] GVG_RULE_ISEXIST = 4102, // 0x00001006
      [GameParameter.ParameterDesc("GvGの現在有効なルール(タイトル表示)")] GVG_RULE_NAME = 4103, // 0x00001007
      [GameParameter.ParameterDesc("GvGの終了日時")] GVG_PERIOD_ENDTIME = 4104, // 0x00001008
      [GameParameter.ParameterDesc("GvGの開始日時")] GVG_PERIOD_STARTTIME = 4105, // 0x00001009
      [GameParameter.ParameterDesc("GvGの現在有効なルールにユニット使用可能数が設定されているか")] GVG_RULE_UNITCOUNT_ISEXIST = 4106, // 0x0000100A
      [GameParameter.ParameterDesc("GvGの現在有効なルール(残りユニット使用可能数)")] GVG_RULE_UNITCOUNT_REST = 4107, // 0x0000100B
      [GameParameter.ParameterDesc("GvGの全体期間")] GVG_PERIOD_ALLTIME = 4108, // 0x0000100C
      [GameParameter.ParameterDesc("GvGのフェーズの切り替え時間(index=0:宣戦開始時間 1:宣戦終了時間 2:攻撃開始時間 3:攻撃終了時間)"), GameParameter.UsesIndex] GVG_PHARSE_TIME = 4109, // 0x0000100D
      [GameParameter.ParameterDesc("GvGのPeriodID")] GVG_PERIOD_ID = 4110, // 0x0000100E
      [GameParameter.ParameterDesc("GvGの現在有効なルール(最大ユニット使用可能数)")] GVG_RULE_UNITCOUNT_MAX = 4111, // 0x0000100F
      [GameParameter.ParameterDesc("イベント同時開催のチェック(index=0:GVGか？ 1:ポートレイドか"), GameParameter.UsesIndex] GUILD_EVENT_PRIORITY = 4112, // 0x00001010
      [GameParameter.ParameterDesc("GvGのフェーズ状態がIndex番号と同じならアクティブ(index=0:宣戦中 1:攻撃中 2:宣戦後クールタイム 3:攻撃後クールタイム 4:期間終了"), GameParameter.UsesIndex] GVG_NOW_PHASE = 4113, // 0x00001011
      [GameParameter.ParameterDesc("GvGの1日の宣戦可能最大数")] GVG_DECLARE_COUNT_MAX = 4114, // 0x00001012
      [GameParameter.ParameterDesc("GvGの本日の残り宣戦可能数")] GVG_DECLARE_COUNT_REST = 4115, // 0x00001013
      [GameParameter.ParameterDesc("GvGの防衛最小ユニット数")] GVG_DEFENSE_UNITMIN = 4116, // 0x00001014
      [GameParameter.ParameterDesc("GvGの防衛チーム最大数")] GVG_DEFENSE_TEAMMAX = 4117, // 0x00001015
      [GameParameter.ParameterDesc("GvGの入口封鎖時間のアクティブ制御(index=0:時間内ならON/index=1:時間外ならtrue"), GameParameter.UsesIndex] GVG_EXITTIME_ACTIVE = 4118, // 0x00001016
      [GameParameter.ParameterDesc("GvGの入口封鎖時間のボタン制御(index=0:時間内ならON/index=1:時間外ならtrue"), GameParameter.UsesIndex] GVG_EXITTIME_INTERACTABLE = 4119, // 0x00001017
      [GameParameter.ParameterDesc("GvGのリーグのランク")] GVG_LEAGUE_RANK = 4120, // 0x00001018
      [GameParameter.ParameterDesc("GvGのリーグでのレート")] GVG_LEAGUE_RATE = 4121, // 0x00001019
      [GameParameter.ParameterDesc("GvGのリーグ分けの最低レート")] GVG_LEAGUE_MINRATE = 4122, // 0x0000101A
      [GameParameter.ParameterDesc("GvGのリーグ分けの最大レート")] GVG_LEAGUE_MAXRATE = 4123, // 0x0000101B
      [GameParameter.ParameterDesc("GvGのリーグのアイコン"), GameParameter.AlwaysUpdate] GVG_LEAGUE_ICON = 4124, // 0x0000101C
      [GameParameter.ParameterDesc("GvGのリーグの名称"), GameParameter.AlwaysUpdate] GVG_LEAGUE_NAME = 4125, // 0x0000101D
      [GameParameter.ParameterDesc("GvGのリーグの名称(アートフォント版)"), GameParameter.AlwaysUpdate] GVG_LEAGUE_NAMEART = 4126, // 0x0000101E
      [GameParameter.ParameterDesc("GvGのリーグのアイコン"), GameParameter.AlwaysUpdate] GVG_LEAGUE_HOMEICON = 4127, // 0x0000101F
      [GameParameter.ParameterDesc("GvGのリーグのアイコン表示フラグ"), GameParameter.AlwaysUpdate, GameParameter.UsesIndex] GVG_LEAGUE_HOMEICONDRAW = 4128, // 0x00001020
      [GameParameter.ParameterDesc("パーティ編成の使用済みユニットが指定されているなら表示"), GameParameter.AlwaysUpdate] GVG_PARTY_UNITUSED = 4129, // 0x00001021
      [GameParameter.ParameterDesc("パーティ編成の使用済み武具が指定されているなら表示"), GameParameter.AlwaysUpdate] GVG_PARTY_ARTIFACTUSED = 4130, // 0x00001022
      [GameParameter.ParameterDesc("パーティ編成の使用済み真理念装が指定されているなら表示"), GameParameter.AlwaysUpdate] GVG_PARTY_CONCEPTCARDUSED = 4131, // 0x00001023
      [GameParameter.ParameterDesc("パーティ編成の使用済みルーンが指定されているなら表示"), GameParameter.AlwaysUpdate] GVG_PARTY_RUNEUSED = 4132, // 0x00001024
      GVG_ED = 4198, // 0x00001066
      RUNE_ST = 4199, // 0x00001067
      [GameParameter.ParameterDesc("レアリティのフレーム表示")] RUNE_RARITY_FRAME = 4200, // 0x00001068
      [GameParameter.ParameterDesc("ルーンアイコン表示")] RUNE_ICON = 4201, // 0x00001069
      [GameParameter.ParameterDesc("ルーンの所持数")] RUNE_NUM = 4202, // 0x0000106A
      [GameParameter.ParameterDesc("ルーンの現在の倉庫サイズ")] RUNE_STORAGE_SIZE = 4203, // 0x0000106B
      [GameParameter.ParameterDesc("ルーンのお気に入りアイコン(0:OFF, 1:ON)")] RUNE_FAVORITE_ICON = 4204, // 0x0000106C
      [GameParameter.ParameterDesc("ルーンのセット効果アイコン表示")] RUNE_SETEFFECT_ICON = 4205, // 0x0000106D
      RUNE_ED = 4298, // 0x000010CA
      RANKING_ST = 4299, // 0x000010CB
      [GameParameter.ParameterDesc("ランクの表示(アートフォント版)"), GameParameter.AlwaysUpdate] RANKING_RANK_IMAGE = 4300, // 0x000010CC
      [GameParameter.ParameterDesc("ランクの表示(テキスト版)"), GameParameter.AlwaysUpdate] RANKING_RANK_TEXT = 4301, // 0x000010CD
      RANKING_ED = 4398, // 0x0000112E
      COMBATPOWER_ST = 4399, // 0x0000112F
      [GameParameter.ParameterDesc("戦闘力")] COMBATPOWER_VALUE = 4400, // 0x00001130
      COMBATPOWER_ED = 4498, // 0x00001192
    }

    public class UsesIndex : Attribute
    {
    }

    public class ParameterDesc : Attribute
    {
      public ParameterDesc(string text)
      {
      }
    }

    public class AlwaysUpdate : Attribute
    {
    }

    public class EnumParameterDesc : GameParameter.ParameterDesc
    {
      public EnumParameterDesc(string text, System.Type type)
        : base(text)
      {
      }
    }

    public class InstanceTypes : Attribute
    {
      public InstanceTypes(System.Type instanceType)
      {
      }
    }

    public class ViewTypes : Attribute
    {
      public ViewTypes(System.Type viewType)
      {
      }
    }

    public class SerializeGameObjects : Attribute
    {
    }
  }
}
