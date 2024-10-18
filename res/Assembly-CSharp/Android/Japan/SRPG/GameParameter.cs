// Decompiled with JetBrains decompiler
// Type: SRPG.GameParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/Game Parameter")]
  public class GameParameter : MonoBehaviour, IGameParameter
  {
    public GameParameter.ParameterTypes ParameterType = GameParameter.ParameterTypes.None;
    public static List<GameParameter> Instances = new List<GameParameter>();
    private const int PARAMETER_CATEGORY_SIZE = 100;
    private static bool[] mAlwaysUpdate;
    public int InstanceType;
    public int Index;
    private Slider mSlider;
    private Text mText;
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

    static GameParameter()
    {
      string[] names = Enum.GetNames(typeof (GameParameter.ParameterTypes));
      GameParameter.mAlwaysUpdate = new bool[names.Length];
      for (int index = 0; index < names.Length; ++index)
      {
        FieldInfo field = typeof (GameParameter.ParameterTypes).GetField(names[index]);
        if (field != null)
          GameParameter.mAlwaysUpdate[index] = field.GetCustomAttributes(typeof (GameParameter.AlwaysUpdate), true).Length > 0;
      }
    }

    private SupportData GetSupportData()
    {
      return DataSource.FindDataOfClass<SupportData>(this.gameObject, (SupportData) null) ?? (SupportData) GlobalVars.SelectedSupport;
    }

    private FriendData GetFriendData()
    {
      return DataSource.FindDataOfClass<FriendData>(this.gameObject, (FriendData) null);
    }

    private AbilityParam GetAbilityParam()
    {
      AbilityParam dataOfClass = DataSource.FindDataOfClass<AbilityParam>(this.gameObject, (AbilityParam) null);
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
      return DataSource.FindDataOfClass<AbilityData>(this.gameObject, (AbilityData) null);
    }

    private ArenaPlayer GetArenaPlayer()
    {
      switch ((GameParameter.ArenaPlayerInstanceTypes) this.InstanceType)
      {
        case GameParameter.ArenaPlayerInstanceTypes.Enemy:
          return (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        default:
          return DataSource.FindDataOfClass<ArenaPlayer>(this.gameObject, (ArenaPlayer) null);
      }
    }

    private ArtifactParam GetArtifactParam()
    {
      switch ((GameParameter.ArtifactInstanceTypes) this.InstanceType)
      {
        case GameParameter.ArtifactInstanceTypes.Any:
          ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
          if (dataOfClass1 != null)
            return dataOfClass1.ArtifactParam;
          return DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null);
        case GameParameter.ArtifactInstanceTypes.QuestReward:
          QuestParam questParamAuto = this.GetQuestParamAuto();
          if (questParamAuto != null && 0 <= this.Index && (questParamAuto.bonusObjective != null && this.Index < questParamAuto.bonusObjective.Length))
            return MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(questParamAuto.bonusObjective[this.Index].item);
          break;
        case GameParameter.ArtifactInstanceTypes.Trophy:
          ArtifactRewardData dataOfClass2 = DataSource.FindDataOfClass<ArtifactRewardData>(this.gameObject, (ArtifactRewardData) null);
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
      return DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
    }

    private void SetArtifactFrame(ArtifactParam param)
    {
      if (param == null)
        return;
      Image component = this.GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      int rareini = param.rareini;
      GameSettings instance = GameSettings.Instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null) || rareini >= instance.ArtifactIcon_Frames.Length)
        return;
      component.sprite = instance.ArtifactIcon_Frames[rareini];
    }

    private Unit GetUnit()
    {
      switch ((GameParameter.UnitInstanceTypes) this.InstanceType)
      {
        case GameParameter.UnitInstanceTypes.CurrentTurn:
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.Battle.CurrentUnit != null)
            return SceneBattle.Instance.Battle.CurrentUnit;
          return (Unit) null;
        default:
          return DataSource.FindDataOfClass<Unit>(this.gameObject, (Unit) null);
      }
    }

    private UnitParam GetUnitParam()
    {
      UnitParam dataOfClass = DataSource.FindDataOfClass<UnitParam>(this.gameObject, (UnitParam) null);
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
          unitData = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
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
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.Battle.CurrentUnit != null)
            return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
          break;
        case 4:
        case 5:
        case 6:
          ArenaPlayer dataOfClass1 = DataSource.FindDataOfClass<ArenaPlayer>(this.gameObject, (ArenaPlayer) null);
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
          VersusCpuData dataOfClass2 = DataSource.FindDataOfClass<VersusCpuData>(this.gameObject, (VersusCpuData) null);
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
      if (roomPlayerParam == null || roomPlayerParam.units == null)
        return (JSON_MyPhotonPlayerParam.UnitDataElem) null;
      return Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(roomPlayerParam.units, (Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>) (e => e.slotID == index));
    }

    private JSON_MyPhotonPlayerParam GetVersusPlayerParam(JSON_MyPhotonPlayerParam[] players, int cnt)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      JSON_MyPhotonPlayerParam photonPlayerParam = (JSON_MyPhotonPlayerParam) null;
      if (players.Length > cnt)
      {
        for (int index = 0; index < players.Length; ++index)
        {
          JSON_MyPhotonPlayerParam player = players[index];
          if (player != null && player.playerID != instance.GetMyPlayer().playerID)
          {
            photonPlayerParam = player;
            break;
          }
        }
      }
      return photonPlayerParam;
    }

    private JSON_MyPhotonPlayerParam GetVersusPlayerParam()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      JSON_MyPhotonPlayerParam photonPlayerParam = (JSON_MyPhotonPlayerParam) null;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        if (this.InstanceType == 0)
        {
          photonPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
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
                photonPlayerParam = this.GetVersusPlayerParam(players, 1);
            }
            if (photonPlayerParam == null)
            {
              string roomParam = instance.GetRoomParam("started");
              if (roomParam != null)
              {
                FlowNode_StartMultiPlay.PlayerList jsonObject = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam);
                if (jsonObject != null)
                  photonPlayerParam = this.GetVersusPlayerParam(jsonObject.players, 1);
              }
            }
          }
        }
      }
      return photonPlayerParam;
    }

    private PartyData GetPartyData()
    {
      return DataSource.FindDataOfClass<PartyData>(this.gameObject, (PartyData) null);
    }

    private SkillParam GetLeaderSkill(PartyData party)
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(party.GetUnitUniqueID(party.LeaderIndex));
      if (unitDataByUniqueId != null && unitDataByUniqueId.LeaderSkill != null)
        return unitDataByUniqueId.LeaderSkill.SkillParam;
      return (SkillParam) null;
    }

    private ItemParam GetItemParam()
    {
      switch (this.InstanceType)
      {
        case 0:
          ItemData dataOfClass1 = DataSource.FindDataOfClass<ItemData>(this.gameObject, (ItemData) null);
          if (dataOfClass1 != null)
            return dataOfClass1.Param;
          return DataSource.FindDataOfClass<ItemParam>(this.gameObject, (ItemParam) null);
        case 1:
          PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
          if (0 <= this.Index && this.Index < player1.Inventory.Length)
            return player1.Inventory[this.Index].Param;
          break;
        case 2:
          QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
          if (questParam == null && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
            questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) ?? DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
          if (questParam != null && questParam.type == QuestTypes.Tower)
          {
            TowerRewardItem towerRewardItem = this.GetTowerRewardItem();
            if (towerRewardItem == null)
              return (ItemParam) null;
            if (towerRewardItem.type != TowerRewardItem.RewardType.Item)
              return (ItemParam) null;
            return MonoSingleton<GameManager>.Instance.GetItemParam(towerRewardItem.iname);
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
          if (questParam != null && 0 <= this.Index && (questParam.bonusObjective != null && this.Index < questParam.bonusObjective.Length))
            return MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[this.Index].item);
          break;
        case 3:
          EquipData dataOfClass2 = DataSource.FindDataOfClass<EquipData>(this.gameObject, (EquipData) null);
          if (dataOfClass2 != null)
            return dataOfClass2.ItemParam;
          break;
        case 4:
          EnhanceMaterial dataOfClass3 = DataSource.FindDataOfClass<EnhanceMaterial>(this.gameObject, (EnhanceMaterial) null);
          if (dataOfClass3 != null && dataOfClass3.item != null)
            return dataOfClass3.item.Param;
          break;
        case 5:
          EnhanceEquipData dataOfClass4 = DataSource.FindDataOfClass<EnhanceEquipData>(this.gameObject, (EnhanceEquipData) null);
          if (dataOfClass4 != null && dataOfClass4.equip != null)
            return dataOfClass4.equip.ItemParam;
          break;
        case 6:
          SellItem dataOfClass5 = DataSource.FindDataOfClass<SellItem>(this.gameObject, (SellItem) null);
          if (dataOfClass5 != null && dataOfClass5.item != null)
            return dataOfClass5.item.Param;
          break;
        case 7:
          ConsumeItemData dataOfClass6 = DataSource.FindDataOfClass<ConsumeItemData>(this.gameObject, (ConsumeItemData) null);
          if (dataOfClass6 != null)
            return dataOfClass6.param;
          break;
      }
      return (ItemParam) null;
    }

    private ItemData GetInventoryItemData()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (0 <= this.Index && this.Index < player.Inventory.Length)
        return player.Inventory[this.Index];
      return (ItemData) null;
    }

    private PlayerLevelUpInfo GetLevelUpInfo()
    {
      return DataSource.FindDataOfClass<PlayerLevelUpInfo>(this.gameObject, (PlayerLevelUpInfo) null);
    }

    private ItemParam GetInventoryItemParam()
    {
      return this.GetInventoryItemData()?.Param;
    }

    private SkillData GetSkillData()
    {
      return DataSource.FindDataOfClass<SkillData>(this.gameObject, (SkillData) null);
    }

    private SkillParam GetSkillParam()
    {
      return DataSource.FindDataOfClass<SkillParam>(this.gameObject, (SkillParam) null);
    }

    private JobParam GetJobParam()
    {
      JobParam dataOfClass = DataSource.FindDataOfClass<JobParam>(this.gameObject, (JobParam) null);
      if (dataOfClass != null)
        return dataOfClass;
      return DataSource.FindDataOfClass<JobData>(this.gameObject, (JobData) null)?.Param;
    }

    private EquipData GetUnitEquipData()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass != null && 0 <= this.Index && this.Index < dataOfClass.CurrentEquips.Length)
        return dataOfClass.CurrentEquips[this.Index];
      return (EquipData) null;
    }

    private EquipData GetEquipData()
    {
      return DataSource.FindDataOfClass<EquipData>(this.gameObject, (EquipData) null);
    }

    private QuestParam GetQuestParamAuto()
    {
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        return MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      return DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
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
      if (towerRewardItem == null || towerRewardItem.Count < this.Index)
        return (TowerRewardItem) null;
      return towerRewardItem[this.Index];
    }

    private QuestParam GetQuestParam()
    {
      QuestParam questParam = (QuestParam) null;
      switch ((GameParameter.QuestInstanceTypes) this.InstanceType)
      {
        case GameParameter.QuestInstanceTypes.Any:
          questParam = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
          break;
        case GameParameter.QuestInstanceTypes.Playing:
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
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
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(this.gameObject, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass == null)
        return (JSON_MyPhotonPlayerParam) null;
      if (dataOfClass.playerIndex <= 0)
        return (JSON_MyPhotonPlayerParam) null;
      return dataOfClass;
    }

    private MultiPlayAPIRoom GetRoom()
    {
      return DataSource.FindDataOfClass<MultiPlayAPIRoom>(this.gameObject, (MultiPlayAPIRoom) null);
    }

    private JSON_MyPhotonRoomParam GetRoomParam()
    {
      JSON_MyPhotonRoomParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonRoomParam>(this.gameObject, (JSON_MyPhotonRoomParam) null);
      if (dataOfClass != null)
        return dataOfClass;
      MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
      if (currentRoom == null)
        return (JSON_MyPhotonRoomParam) null;
      return JSON_MyPhotonRoomParam.Parse(currentRoom.json);
    }

    private bool LoadItemIcon(string iconName)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.gameObject);
      if (string.IsNullOrEmpty(iconName))
        return false;
      iconLoader.ResourcePath = AssetPath.ItemIcon(iconName);
      return true;
    }

    private bool LoadItemIcon(ItemParam itemParam)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.gameObject);
      if (itemParam == null)
        return false;
      iconLoader.ResourcePath = AssetPath.ItemIcon(itemParam);
      return true;
    }

    private void SetItemFrame(ItemParam itemParam)
    {
      if (itemParam == null)
        return;
      Image component = this.GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      Sprite itemFrame = GameSettings.Instance.GetItemFrame(itemParam);
      component.sprite = itemFrame;
    }

    private void SetEquipItemFrame(ItemParam itemParam)
    {
      Sprite[] normalFrames = GameSettings.Instance.ItemIcons.NormalFrames;
      Image component = this.GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || normalFrames.Length <= 0)
        return;
      if (itemParam != null && itemParam.rare < normalFrames.Length)
        component.sprite = normalFrames[itemParam.rare];
      else
        component.sprite = normalFrames[0];
    }

    private MailData GetMailData()
    {
      return DataSource.FindDataOfClass<MailData>(this.gameObject, (MailData) null);
    }

    private SellItem GetSellItem()
    {
      return DataSource.FindDataOfClass<SellItem>(this.gameObject, (SellItem) null);
    }

    private List<SellItem> GetSellItemList()
    {
      return DataSource.FindDataOfClass<List<SellItem>>(this.gameObject, (List<SellItem>) null);
    }

    private ShopItem GetShopItem()
    {
      return DataSource.FindDataOfClass<ShopItem>(this.gameObject, (ShopItem) null) ?? (ShopItem) this.GetLimitedShopItem() ?? (ShopItem) this.GetEventShopItem();
    }

    private LimitedShopItem GetLimitedShopItem()
    {
      return DataSource.FindDataOfClass<LimitedShopItem>(this.gameObject, (LimitedShopItem) null);
    }

    private EventShopItem GetEventShopItem()
    {
      return DataSource.FindDataOfClass<EventShopItem>(this.gameObject, (EventShopItem) null);
    }

    private GachaParam GetGachaParam()
    {
      return DataSource.FindDataOfClass<GachaParam>(this.gameObject, (GachaParam) null);
    }

    private void SetBuyPriceTypeIcon(ESaleType type)
    {
      Sprite[] itemPriceIconFrames = GameSettings.Instance.ItemPriceIconFrames;
      if (itemPriceIconFrames == null || type == ESaleType.EventCoin)
        return;
      Image component = this.GetComponent<Image>();
      int index = type == ESaleType.Coin_P ? 1 : (int) type;
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || index >= itemPriceIconFrames.Length)
        return;
      component.sprite = itemPriceIconFrames[index];
    }

    private void SetBuyPriceEventCoinTypeIcon(string cost_iname)
    {
      Image component = this.GetComponent<Image>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null || cost_iname == null)
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("EventShopCmn/eventcoin_small");
      if (!((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null))
        return;
      component.sprite = spriteSheet.GetSprite(cost_iname);
    }

    private TrophyParam GetTrophyParam()
    {
      return DataSource.FindDataOfClass<TrophyParam>(this.gameObject, (TrophyParam) null);
    }

    private TrophyObjective GetTrophyObjective()
    {
      return DataSource.FindDataOfClass<TrophyObjective>(this.gameObject, (TrophyObjective) null);
    }

    private EnhanceEquipData GetEnhanceEquipData()
    {
      return DataSource.FindDataOfClass<EnhanceEquipData>(this.gameObject, (EnhanceEquipData) null);
    }

    private EnhanceMaterial GetEnhanceMaterial()
    {
      return DataSource.FindDataOfClass<EnhanceMaterial>(this.gameObject, (EnhanceMaterial) null);
    }

    private EquipItemParameter GetEquipItemParameter()
    {
      return DataSource.FindDataOfClass<EquipItemParameter>(this.gameObject, (EquipItemParameter) null);
    }

    private string GetParamTypeName(ParamTypes type)
    {
      if (type == ParamTypes.None)
        return (string) null;
      return LocalizedText.Get("sys." + type.ToString());
    }

    private bool CheckUnlockInstanceType()
    {
      return MonoSingleton<GameManager>.Instance.Player.CheckUnlock((UnlockTargets) this.InstanceType);
    }

    private TrickParam GetTrickParam()
    {
      return DataSource.FindDataOfClass<TrickParam>(this.gameObject, (TrickParam) null);
    }

    private BattleCore.OrderData GetOrderData()
    {
      return DataSource.FindDataOfClass<BattleCore.OrderData>(this.gameObject, (BattleCore.OrderData) null);
    }

    private MapEffectParam GetMapEffectParam()
    {
      return DataSource.FindDataOfClass<MapEffectParam>(this.gameObject, (MapEffectParam) null);
    }

    private WeatherParam GetWeatherParam()
    {
      return DataSource.FindDataOfClass<WeatherParam>(this.gameObject, (WeatherParam) null);
    }

    private bool LoadArtifactIcon(ArtifactParam param)
    {
      IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.gameObject);
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

    private void InternalUpdateValue()
    {
      // ISSUE: unable to decompile the method.
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
          if (gameParameter.gameObject.activeInHierarchy)
          {
            gameParameter.UpdateValue();
          }
          else
          {
            Transform transform = gameParameter.transform;
            if ((UnityEngine.Object) transform.parent != (UnityEngine.Object) null && transform.parent.gameObject.activeInHierarchy)
              gameParameter.UpdateValue();
          }
        }
        else
          ((IGameParameter) componentsInChildren[index]).UpdateValue();
      }
    }

    private void SetUpdateInterval(float interval)
    {
      if (!this.gameObject.activeInHierarchy)
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
      return (IEnumerator) new GameParameter.\u003CUpdateTimer\u003Ec__Iterator0() { \u0024this = this };
    }

    private void OnDestroy()
    {
      GameParameter.Instances.Remove(this);
    }

    public void ToggleChildren(bool visible)
    {
      Transform transform = this.transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
        transform.GetChild(index).gameObject.SetActive(visible);
    }

    public void ToggleEmpty(bool visible)
    {
      if (!this.mIsEmptyGO)
        return;
      this.gameObject.SetActive(visible);
    }

    public void ResetToDefault()
    {
      if ((UnityEngine.Object) this.mText != (UnityEngine.Object) null)
        this.mText.text = this.mDefaultValue;
      else if ((UnityEngine.Object) this.mSlider != (UnityEngine.Object) null)
      {
        this.mSlider.value = this.mDefaultRangeValue.x;
        this.mSlider.maxValue = this.mDefaultRangeValue.y;
      }
      else if ((UnityEngine.Object) this.mInputField != (UnityEngine.Object) null)
        this.mInputField.text = this.mDefaultValue;
      else if ((UnityEngine.Object) this.mImage != (UnityEngine.Object) null)
      {
        this.mImage.texture = this.mDefaultImage;
      }
      else
      {
        if (!((UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null))
          return;
        this.mImageArray.sprite = this.mDefaultSprite;
      }
    }

    private void SetTextValue(string value)
    {
      if ((UnityEngine.Object) this.mText != (UnityEngine.Object) null)
        this.mText.text = value;
      if (!((UnityEngine.Object) this.mInputField != (UnityEngine.Object) null))
        return;
      this.mInputField.SetText(value);
    }

    private void SetTextValue(int value)
    {
      if (!((UnityEngine.Object) this.mText != (UnityEngine.Object) null) && !((UnityEngine.Object) this.mInputField != (UnityEngine.Object) null))
        return;
      this.SetTextValue(value.ToString());
    }

    private void SetTextColor(Color color)
    {
      if (!((UnityEngine.Object) this.mText != (UnityEngine.Object) null))
        return;
      this.mText.color = color;
    }

    private void SetSyncColorOriginColor(Color color)
    {
      SyncColor component = this.GetComponent<SyncColor>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.ForceOriginColorChange(color);
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!((UnityEngine.Object) this.mSlider != (UnityEngine.Object) null))
        return;
      this.mSlider.maxValue = (float) maxValue;
      this.mSlider.minValue = 0.0f;
      this.mSlider.value = (float) value;
    }

    private void SetImageIndex(int index)
    {
      if (!((UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null))
        return;
      this.mImageArray.ImageIndex = index;
    }

    private void SetAnimatorInt(string name, int value)
    {
      if (!((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null))
        return;
      this.mAnimator.SetInteger(name, value);
    }

    private void SetAnimatorBool(string name, bool value)
    {
      if (!((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null))
        return;
      this.mAnimator.SetBool(name, value);
    }

    private int GetImageLength()
    {
      if ((UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null)
        return this.mImageArray.Images.Length;
      return 0;
    }

    private void SetImageBySpriteSheet(string key)
    {
      ImageSpriteSheet component = this.GetComponent<ImageSpriteSheet>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetSprite(key);
    }

    private void Awake()
    {
      GameParameter.Instances.Add(this);
      this.mText = this.GetComponent<Text>();
      this.mSlider = this.GetComponent<Slider>();
      this.mInputField = this.GetComponent<InputField>();
      this.mAnimator = this.GetComponent<Animator>();
      this.mImage = this.GetComponent<RawImage>();
      this.mImageArray = this.GetComponent<ImageArray>();
      if ((UnityEngine.Object) this.mText != (UnityEngine.Object) null)
        this.mDefaultValue = this.mText.text;
      else if ((UnityEngine.Object) this.mSlider != (UnityEngine.Object) null)
      {
        this.mDefaultRangeValue.x = this.mSlider.value;
        this.mDefaultRangeValue.y = this.mSlider.maxValue;
      }
      else if ((UnityEngine.Object) this.mInputField != (UnityEngine.Object) null)
        this.mDefaultValue = this.mInputField.text;
      else if ((UnityEngine.Object) this.mImage != (UnityEngine.Object) null)
        this.mDefaultImage = this.mImage.texture;
      else if ((UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null)
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
      if (!this.gameObject.activeInHierarchy)
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

    private static int AbilityTypeDetailToImageIndex(EAbilitySlot type, EAbilityTypeDetail typeDetail)
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
      [GameParameter.ParameterDesc("アイテムアイコン"), GameParameter.InstanceTypes(typeof (GameParameter.ItemInstanceTypes)), GameParameter.UsesIndex] ITEM_ICON = 14, // 0x0000000E
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
      [GameParameter.ParameterDesc("インベントリーにセットされたアイテムのアイコン\n*スロット番号をIndexで指定"), GameParameter.UsesIndex] INVENTORY_ITEMICON = 46, // 0x0000002E
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
      [GameParameter.ParameterDesc("機能がアンロックされている場合のみ表示"), GameParameter.InstanceTypes(typeof (UnlockTargets))] UNLOCK_SHOWED = 248, // 0x000000F8
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
      [GameParameter.ParameterDesc("機能がアンロックされている場合のみ有効"), GameParameter.InstanceTypes(typeof (UnlockTargets))] UNLOCK_ENABLED = 285, // 0x0000011D
      [GameParameter.ParameterDesc("機能がアンロックされていると表示されなくなる"), GameParameter.InstanceTypes(typeof (UnlockTargets)), GameParameter.AlwaysUpdate] UNLOCK_HIDDEN = 286, // 0x0000011E
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
      [GameParameter.ParameterDesc("マルチで自キャラ生存数が0のとき表示(0)/非表示(1)"), GameParameter.UsesIndex] MULTI_REST_MY_UNIT_IS_ZERO = 394, // 0x0000018A
      [GameParameter.ParameterDesc("マルチ部屋画面で対象プレイヤーが自分のとき0:表示/1:非表示/2:ImageArrayのインデックス切り替え(0=自分 1=他人)/3:チーム編成ボタン/4:情報をみるボタン/5:チーム編成ボタン(マルチ塔)/6:プレイヤーがまだリザルト画面に存在するか"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] MULTI_PLAYER_IS_ME = 395, // 0x0000018B
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
      [GameParameter.ParameterDesc("クエストにユニットが出撃可能な場合に表示(0)/非表示(1)"), GameParameter.UsesIndex, GameParameter.AlwaysUpdate] QUEST_IS_UNIT_ENABLEENTRYCONDITION = 423, // 0x000001A7
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
      [GameParameter.ParameterDesc("現在の部屋の倍速設定")] MULTI_ROOM_LIST_SPEED = 1611, // 0x0000064B
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
      [GameParameter.ParameterDesc("レイド詳細救援ボタン"), GameParameter.AlwaysUpdate] RAID_BUTTON_RESCUE_SEND = 3224, // 0x00000C98
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
      RAID_ED = 3298, // 0x00000CE2
      GENESIS_ST = 3299, // 0x00000CE3
      [GameParameter.ParameterDesc("創世編ボス名")] GENESIS_BOSS_NAME = 3300, // 0x00000CE4
      [GameParameter.ParameterDesc("創世編ボス一枚絵")] GENESIS_BOSS_IMAGE = 3301, // 0x00000CE5
      [GameParameter.ParameterDesc("創世編ボス属性アイコン")] GENESIS_BOSS_ELEMENT = 3302, // 0x00000CE6
      [GameParameter.ParameterDesc("創世編ボス現在HP")] GENESIS_BOSS_HP_CURRENT = 3303, // 0x00000CE7
      [GameParameter.ParameterDesc("創世編ボス最大HP")] GENESIS_BOSS_HP_MAX = 3304, // 0x00000CE8
      [GameParameter.ParameterDesc("創世編ボスHPゲージ")] GENESIS_BOSS_HP_GAUGE = 3305, // 0x00000CE9
      [GameParameter.ParameterDesc("創生編ボス挑戦ボタン")] GENESIS_BOSS_CHALLENGE_BUTTON = 3306, // 0x00000CEA
      [GameParameter.ParameterDesc("創世編章TOP開催期間")] GENESIS_CHAPTER_PERIOD = 3319, // 0x00000CF7
      [GameParameter.ParameterDesc("創世編開催期間中であれば表示(0)/非表示(1)")] GENESIS_IS_CHECK_PERIOD = 3339, // 0x00000D0B
      GENESIS_ED = 3398, // 0x00000D46
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
  }
}
