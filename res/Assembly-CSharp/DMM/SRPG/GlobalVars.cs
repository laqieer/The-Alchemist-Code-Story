// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalVars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public static class GlobalVars
  {
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<eOverWritePartyType> OverWritePartyType;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<int> SelectedPartyIndex;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedChapter;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedSection;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<int> SelectedStoryPart;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> HomeBgSection;
    public static int BanStatus = 0;
    public static string CustomerID = string.Empty;
    public static Vector2 Location = Vector2.zero;
    public static GlobalVars.GlobalVar<long> BtlID;
    public static GlobalVars.GlobalVar<bool> BtlIDStatus;
    public static QuestTypes QuestType;
    public static GlobalVars.GlobalVar<long> ContinueBtlID;
    public static BattleCore.Record ContinueBtlRecord;
    public static GlobalVars.GlobalVar<SupportData> SelectedSupport;
    public static int SelectedTeamIndex;
    public static string SelectedFriendID;
    public static string SelectedQuestID;
    public static string SelectedItemID;
    public static string SelectedCreateItemID;
    public static string EditPlayerName;
    public static string SelectedArtifactID;
    public static string SelectedArchiveID;
    public static EncodingTypes.ESerializeCompressMethod SelectedSerializeCompressMethod;
    public static bool SelectedSerializeCompressMethodWasNodeSet = false;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> LastPlayedQuest;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<QuestStates> LastQuestState;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<BattleCore.QuestResult> LastQuestResult;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedAbilityID;
    [GlobalVars.ResetOnLogin]
    public static List<QuestDifficulties> GenesisAvailableDifficulties;
    public static string UnlockUnitID;
    public static GlobalVars.GlobalVar<ArenaPlayer> SelectedArenaPlayer;
    public static ArenaBattleResponse ResultArenaBattleResponse;
    public static GlobalVars.GlobalVar<string> SelectedTrophy;
    public static List<string> SelectedTrophies;
    public static GlobalVars.GlobalVar<long> SelectedUnitUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedEquipUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedJobUniqueID;
    public static GlobalVars.GlobalVar<long> PreBattleUnitUniqueID;
    public static GlobalVars.GlobalVar<TobiraParam.Category> PreBattleUnitTobiraCategory;
    public static GlobalVars.GlobalVar<long> SelectedLSChangeUnitUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedArtifactUniqueID;
    public static List<long> SelectedArtifactUniqueIDList;
    public static GlobalVars.GlobalVar<string> SelectedArtifactIname;
    public static Dictionary<string, int> UsedArtifactExpItems = new Dictionary<string, int>();
    public static List<long> SellArtifactsList = new List<long>();
    public static List<long> ConvertArtifactsList = new List<long>();
    public static GlobalVars.GlobalVar<int> SelectedEquipmentSlot = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<int> SelectedUnitJobIndex;
    public static List<ItemParam> SelectedItemParamTree = new List<ItemParam>();
    public static GlobalVars.GlobalVar<int> SelectedAbilitySlot;
    public static GlobalVars.GlobalVar<long> SelectedAbilityUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedMailUniqueID;
    public static GlobalVars.GlobalVar<int> SelectedMailPeriod;
    public static GlobalVars.GlobalVar<int> SelectedMailPage;
    public static GlobalVars.GlobalVar<RewardData> LastReward;
    public static Unit MainTarget;
    public static Unit SubTarget;
    public static FriendData SelectedFriend;
    public static FriendData FoundFriend;
    public static int RaidNum;
    public static RaidResult RaidResult;
    public static GlobalVars.GlobalVar<int> PlayerExpOld;
    public static GlobalVars.GlobalVar<int> PlayerExpNew;
    public static GlobalVars.GlobalVar<bool> PlayerLevelChanged;
    public static string SelectedGachaTableId;
    public static EShopType ShopType;
    public static int ShopBuyIndex;
    public static int ShopBuyAmount;
    public static UnitPieceShopItem BuyUnitPieceShopItem;
    public static List<ShopItem> TimeOutShopItems;
    public static LimitedShopListItem LimitedShopItem;
    public static EventShopInfo EventShopItem;
    public static List<EventShopListItem> EventShopListItems = new List<EventShopListItem>();
    public static EventShopListItem SelectionEventShop;
    public static GlobalVars.CoinListSelectionType SelectionCoinListType = GlobalVars.CoinListSelectionType.None;
    public static SellItem SelectSellItem;
    public static List<SellItem> SellItemList;
    public static List<SellItem> ConvertAwakePieceList;
    public static GlobalVars.GlobalVar<GlobalVars.JobRankUpTypes> JobRankUpType;
    public static List<AbilityData> LearningAbilities;
    public static List<ItemData> ReturnItems;
    public static Dictionary<long, int> AbilitiesRankUp = new Dictionary<long, int>();
    public static EquipData SelectedEquipData;
    public static List<EnhanceMaterial> SelectedEnhanceMaterials;
    public static Dictionary<string, int> UsedUnitExpItems = new Dictionary<string, int>();
    public static ArtifactData TargetInspSkillArtifact;
    public static InspirationSkillData TargetInspSkill;
    public static List<ArtifactData> MixInspSkillArtifactList;
    public static string EditMultiPlayRoomComment;
    public static string EditMultiPlayRoomPassCode;
    public static bool SelectedMultiPlayQuestIsEvent;
    public static JSON_MyPhotonRoomParam.EType SelectedMultiPlayRoomType;
    public static string SelectedMultiPlayArea;
    public static string SelectedMultiPlayRoomName;
    public static string SelectedMultiPlayRoomComment;
    public static string SelectedMultiPlayRoomPassCodeHash;
    public static JSON_MyPhotonPlayerParam SelectedMultiPlayerParam;
    public static List<int> SelectedMultiPlayerUnitIDs;
    public static VERSUS_TYPE SelectedMultiPlayVersusType;
    public static string MultiPlayVersusKey;
    public static bool VersusRoomReuse;
    public static bool SelectedMultiPlayLimit;
    public static bool MultiPlayClearOnly;
    public static int MultiPlayJoinUnitLv;
    public static string SelectedMultiTowerID;
    public static int SelectedMultiTowerFloor;
    public static bool CreateAutoMultiTower;
    public static bool InvtationSameUser;
    public static GlobalVars.GlobalVar<VersusCpuData> VersusCpu;
    public static long VersusFreeMatchTime = -1;
    public static bool IsVersusDraftMode = false;
    public static GlobalVars.EMultiPlayContinue SelectedMultiPlayContinue;
    public static BattleCore.Json_BattleCont MultiPlayBattleCont;
    public static int ResumeMultiplayPlayerID;
    public static int ResumeMultiplaySeatID;
    public static int MultiInvitation;
    public static string MultiInvitationRoomOwner;
    public static bool MultiInvitationRoomLocked;
    public static bool MultiInvitaionFlag = false;
    public static string MultiInvitaionComment = string.Empty;
    public static int SelectedMultiPlayRoomID;
    public static string SelectedMultiPlayPhotonAppID;
    public static bool SelectedMultiPlayHiSpeed;
    public static int SelectedMultiPlayBtlSpeed;
    public static bool SelectedMultiPlayAutoAllowed;
    public static int SelectedTowerMultiPartyIndex;
    public static List<List<VersusRankReward>> RankMatchSeasonReward = new List<List<VersusRankReward>>();
    public static GlobalVars.GlobalVar<RaidManager.RaidOwnerType> RestoreOwnerType = new GlobalVars.GlobalVar<RaidManager.RaidOwnerType>(RaidManager.RaidOwnerType.Self);
    public static GlobalVars.GlobalVar<int> CurrentRaidBossId = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<string> CurrentWorldRaidIname = new GlobalVars.GlobalVar<string>(string.Empty);
    public static GlobalVars.GlobalVar<string> CurrentRaidBossIname = new GlobalVars.GlobalVar<string>(string.Empty);
    public static GlobalVars.GlobalVar<int> CurrentRaidRound = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<int> CurrentRaidBossHP = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<long> CurrentWorldRaidBossHP = new GlobalVars.GlobalVar<long>(-1L);
    public static GlobalVars.GlobalVar<GuildRaidBattleType> CurrentBattleType = new GlobalVars.GlobalVar<GuildRaidBattleType>(GuildRaidBattleType.Main);
    public static GlobalVars.GlobalVar<int> CurrentGuildRaidTurnRemain = new GlobalVars.GlobalVar<int>(0);
    public static GlobalVars.GlobalVar<GvGParty> GvGOffenseParty = new GlobalVars.GlobalVar<GvGParty>((GvGParty) null);
    public static GlobalVars.GlobalVar<GvGParty> GvGDefenseParty = new GlobalVars.GlobalVar<GvGParty>((GvGParty) null);
    public static GlobalVars.GlobalVar<int> GvGBattleSeed = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<bool> GvGBattleReplay = new GlobalVars.GlobalVar<bool>(false);
    public static GlobalVars.GlobalVar<bool> GvGBattleMode = new GlobalVars.GlobalVar<bool>(false);
    public static GlobalVars.GlobalVar<int> GvGNodeId = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<int> GvGGroupId = new GlobalVars.GlobalVar<int>(-1);
    public static GlobalVars.GlobalVar<bool> ResultGvGCapture = new GlobalVars.GlobalVar<bool>(false);
    public static string SelectedProductID;
    public static string SelectedProductIname;
    public static int EditedYear;
    public static int EditedMonth;
    public static int EditedDay;
    public static int BeforeCoin;
    public static int AfterCoin;
    public static bool IsTutorialEnd;
    public static string PubHash;
    public static string UrgencyPubHash;
    public static string SelectedChallengeMissionTrophy;
    public static GlobalVars.GlobalVar<int> CurrentChatChannel = new GlobalVars.GlobalVar<int>(-1);
    public static int ChatChannelViewNum;
    public static int ChatChannelMax;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedChapter;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedSection;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedQuestID;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedNextQuestID;
    public static UnitGetParam UnitGetReward;
    public static string PreEventName;
    public static bool ForceSceneChange;
    public static GlobalVars.EventQuestListType ReqEventPageListType;
    public static bool KeyQuestTimeOver;
    public static long mDropTableGeneratedUnixTime = 0;
    public static string SelectedTowerID;
    public static string SelectedFloorID;
    public static GlobalVars.GlobalVar<long> SelectedSupportUnitUniqueID;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsEventShopOpen = new GlobalVars.GlobalVar<bool>(false);
    public static ItemSelectListItemData ItemSelectListItemData;
    public static ArtifactSelectListItemData ArtifactListItem;
    public static string[] ConditionJobs;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsDirtyArtifactData = new GlobalVars.GlobalVar<bool>(true);
    public static CollaboSkillParam.Pair SelectedCollaboSkillPair;
    public static string TeamName;
    public static GlobalVars.UserSelectionPartyData UserSelectionPartyDataInfo;
    public static bool PartyUploadFinished;
    public static bool RankingQuestSelected;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsTitleStart = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsLoginInfoNotified = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsRankMatchRewarded = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsRaidRewarded = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsGuildRaidRewarded = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsGuildTrophy = new GlobalVars.GlobalVar<bool>(false);
    public static RankingQuestParam SelectedRankingQuestParam;
    public static List<PartyEditData> OrdealParties = new List<PartyEditData>();
    public static List<SupportData> OrdealSupports = new List<SupportData>();
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsDirtyConceptCardData = new GlobalVars.GlobalVar<bool>(true);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsDirtySkinConceptCardData = new GlobalVars.GlobalVar<bool>(true);
    public static GlobalVars.GlobalVar<ConceptCardData> SelectedConceptCardData = new GlobalVars.GlobalVar<ConceptCardData>();
    public static bool RestoreBeginnerQuest;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<int> ConceptCardNum = new GlobalVars.GlobalVar<int>(0);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsDirtyRuneData = new GlobalVars.GlobalVar<bool>(true);
    public static bool IsSkipQuestDemo;
    public static Json_ArenaAward ArenaAward;
    public static UnitData UnitDataForUnitList;
    public static bool IsUnitRentalUnit;
    public static GlobalVars.RecommendTeamSetting RecommendTeamSettingValue;
    public static bool IsAutoEquipConceptCard = true;
    public static GlobalVars.SummonCoinInfo NewSummonCoinInfo;
    public static GlobalVars.SummonCoinInfo OldSummonCoinInfo;
    public static string SelectTips;
    public static string RequestTips;
    public static List<string> BlockList = new List<string>();
    public static string MonthlyLoginBonus_SelectTableIname;
    public static int MonthlyLoginBonus_SelectRecoverDay;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsHomeAPIAddGuildRoleBonus = new GlobalVars.GlobalVar<bool>(true);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsHomeAPI = new GlobalVars.GlobalVar<bool>(true);
    public static GlobalVars.SelectUnitTicketData SelectUnitTicketDataValue;

    static GlobalVars()
    {
      FieldInfo[] fields = typeof (GlobalVars).GetFields(BindingFlags.Static | BindingFlags.Public);
      for (int index = 0; index < fields.Length; ++index)
      {
        System.Type[] interfaces = fields[index].FieldType.GetInterfaces();
        if (interfaces != null && Array.IndexOf<System.Type>(interfaces, typeof (GlobalVars.IGlobalVar)) >= 0 && fields[index].GetValue((object) null) == null)
          fields[index].SetValue((object) null, Activator.CreateInstance(fields[index].FieldType));
      }
    }

    public static bool DebugIsPlayTutorial
    {
      get
      {
        return PlayerPrefsUtility.HasKey(PlayerPrefsUtility.DEBUG_IS_PLAY_TUTORIAL) && PlayerPrefsUtility.GetInt(PlayerPrefsUtility.DEBUG_IS_PLAY_TUTORIAL) == 1;
      }
      set => PlayerPrefsUtility.SetInt(PlayerPrefsUtility.DEBUG_IS_PLAY_TUTORIAL, !value ? 0 : 1);
    }

    public static void SetDropTableGeneratedTime()
    {
      GlobalVars.mDropTableGeneratedUnixTime = SRPG.Network.GetServerTime();
    }

    public static long GetDropTableGeneratedUnixTime() => GlobalVars.mDropTableGeneratedUnixTime;

    public static DateTime GetDropTableGeneratedDateTime()
    {
      return TimeManager.FromUnixTime(GlobalVars.mDropTableGeneratedUnixTime);
    }

    public static void ResetVarsWithAttribute(System.Type attrType)
    {
      FieldInfo[] fields = typeof (GlobalVars).GetFields(BindingFlags.Static | BindingFlags.Public);
      for (int index = 0; index < fields.Length; ++index)
      {
        if (fields[index].GetCustomAttributes(attrType, true).Length > 0)
        {
          System.Type[] interfaces = fields[index].FieldType.GetInterfaces();
          if (interfaces != null && Array.IndexOf<System.Type>(interfaces, typeof (GlobalVars.IGlobalVar)) >= 0)
            ((GlobalVars.IGlobalVar) fields[index].GetValue((object) null)).Reset();
          else if (fields[index].FieldType.IsValueType)
            fields[index].SetValue((object) null, Activator.CreateInstance(fields[index].FieldType));
        }
      }
    }

    public enum CoinListSelectionType
    {
      None,
      EventShop,
      ArenaShop,
      MultiShop,
    }

    public enum JobRankUpTypes
    {
      RankUp,
      Unlock,
      ClassChange,
    }

    public enum EMultiPlayContinue
    {
      PENDING,
      CONTINUE,
      CANCEL,
    }

    public enum EventQuestListType
    {
      EventQuest,
      KeyQuest,
      Tower,
      RankingQuest,
      BeginnerQuest,
      Seiseki,
      Babel,
      EventQuestArchive,
      DailyAndEnhance,
    }

    public interface IGlobalVar
    {
      void Reset();
    }

    public enum RecommendType
    {
      Total,
      Attack,
      Defence,
      Magic,
      Mind,
      Speed,
      AttackTypeSlash,
      AttackTypeStab,
      AttackTypeBlow,
      AttackTypeShot,
      AttackTypeMagic,
      AttackTypeNone,
      Hp,
    }

    public class UserSelectionPartyData
    {
      public UnitData[] unitData;
      public SupportData supportData;
      public int[] achievements;
      public ItemData[] usedItems;
    }

    [Serializable]
    public class RecommendTeamSetting
    {
      public GlobalVars.RecommendType recommendedType;
      public EElement recommendedElement;

      public RecommendTeamSetting(GlobalVars.RecommendType type, EElement elem)
      {
        this.recommendedType = type;
        this.recommendedElement = elem;
      }
    }

    public class SummonCoinInfo
    {
      public int ConvertedSummonCoin;
      public int ReceivedStone;
      public int SummonCoinStock;
      public long Period;
      public long ConvertedDate;
    }

    public class SelectUnitTicketData
    {
      public string SelectUnitId;
      public int ConvertPieceNum;
    }

    public class GlobalVar<T> : GlobalVars.IGlobalVar
    {
      private GlobalVars.GlobalVar<T>.VariableChangeEvent mListeners;
      private T mValue;
      private T mDefaultValue;

      public GlobalVar()
      {
        this.mValue = default (T);
        this.mDefaultValue = default (T);
        this.mListeners = (GlobalVars.GlobalVar<T>.VariableChangeEvent) null;
      }

      public GlobalVar(T defaultValue)
      {
        this.mValue = defaultValue;
        this.mDefaultValue = defaultValue;
        this.mListeners = (GlobalVars.GlobalVar<T>.VariableChangeEvent) null;
      }

      public void Reset() => this.mValue = this.mDefaultValue;

      public T Get() => this.mValue;

      public void Set(T value)
      {
        if (((object) value != null || (object) this.mValue == null) && ((object) value == null || value.Equals((object) this.mValue)))
          return;
        this.mValue = value;
        if (this.mListeners == null)
          return;
        this.mListeners();
      }

      public static implicit operator T(GlobalVars.GlobalVar<T> src) => src.mValue;

      public void AddChangeEventListener(
        GlobalVars.GlobalVar<T>.VariableChangeEvent callback)
      {
        if (this.mListeners == null)
          this.mListeners = callback;
        else
          this.mListeners += callback;
      }

      public void RemoveChangeEventListener(
        GlobalVars.GlobalVar<T>.VariableChangeEvent callback)
      {
        this.mListeners -= callback;
      }

      public override string ToString()
      {
        return (object) this.mValue != null ? this.mValue.ToString() : (string) null;
      }

      public delegate void VariableChangeEvent();
    }

    public class ResetOnLogin : Attribute
    {
    }
  }
}
