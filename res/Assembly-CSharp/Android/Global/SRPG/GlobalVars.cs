﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalVars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SRPG
{
  public static class GlobalVars
  {
    public static int BanStatus = 0;
    public static string CustomerID = string.Empty;
    public static Vector2 Location = Vector2.zero;
    public static Dictionary<string, int> UsedArtifactExpItems = new Dictionary<string, int>();
    public static List<long> SellArtifactsList = new List<long>();
    public static List<long> ConvertArtifactsList = new List<long>();
    public static GlobalVars.GlobalVar<int> SelectedEquipmentSlot = new GlobalVars.GlobalVar<int>(-1);
    public static List<ItemParam> SelectedItemParamTree = new List<ItemParam>();
    public static List<EventShopListItem> EventShopListItems = new List<EventShopListItem>();
    public static GlobalVars.CoinListSelectionType SelectionCoinListType = GlobalVars.CoinListSelectionType.None;
    public static Dictionary<long, int> AbilitiesRankUp = new Dictionary<long, int>();
    public static Dictionary<string, int> UsedUnitExpItems = new Dictionary<string, int>();
    public static long VersusFreeMatchTime = -1;
    public static bool MultiInvitaionFlag = false;
    public static string MultiInvitaionComment = string.Empty;
    public static List<List<VersusRankReward>> RankMatchSeasonReward = new List<List<VersusRankReward>>();
    public static GlobalVars.GlobalVar<int> CurrentChatChannel = new GlobalVars.GlobalVar<int>(-1);
    public static long mDropTableGeneratedUnixTime = 0;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsEventShopOpen = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<bool> IsTitleStart = new GlobalVars.GlobalVar<bool>(false);
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<int> SelectedPartyIndex;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedChapter;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedSection;
    public static GlobalVars.GlobalVar<long> BtlID;
    public static QuestTypes QuestType;
    public static GlobalVars.GlobalVar<long> ContinueBtlID;
    public static BattleCore.Record ContinueBtlRecord;
    public static GlobalVars.GlobalVar<SupportData> SelectedSupport;
    public static string SelectedFriendID;
    public static string SelectedQuestID;
    public static string SelectedItemID;
    public static string SelectedCreateItemID;
    public static string EditPlayerName;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> LastPlayedQuest;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<QuestStates> LastQuestState;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<BattleCore.QuestResult> LastQuestResult;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> SelectedAbilityID;
    public static string UnlockUnitID;
    public static GlobalVars.GlobalVar<ArenaPlayer> SelectedArenaPlayer;
    public static ArenaBattleResponse ResultArenaBattleResponse;
    public static GlobalVars.GlobalVar<string> SelectedTrophy;
    public static GlobalVars.GlobalVar<long> SelectedUnitUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedEquipUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedJobUniqueID;
    public static GlobalVars.GlobalVar<long> SelectedArtifactUniqueID;
    public static GlobalVars.GlobalVar<string> SelectedArtifactIname;
    public static GlobalVars.GlobalVar<int> SelectedUnitJobIndex;
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
    public static string SelectedGachaId;
    public static string SelectedGachaType;
    public static EShopType ShopType;
    public static int ShopBuyIndex;
    public static LimitedShopListItem LimitedShopItem;
    public static EventShopListItem EventShopItem;
    public static EventShopListItem SelectionEventShop;
    public static SellItem SelectSellItem;
    public static List<SellItem> SellItemList;
    public static List<SellItem> ConvertAwakePieceList;
    public static GlobalVars.GlobalVar<GlobalVars.JobRankUpTypes> JobRankUpType;
    public static List<AbilityData> LearningAbilities;
    public static List<ItemData> ReturnItems;
    public static EquipData SelectedEquipData;
    public static List<EnhanceMaterial> SelectedEnhanceMaterials;
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
    public static GlobalVars.EMultiPlayContinue SelectedMultiPlayContinue;
    public static BattleCore.Json_BattleCont MultiPlayBattleCont;
    public static QuestDifficulties QuestDifficulty;
    public static int ResumeMultiplayPlayerID;
    public static int ResumeMultiplaySeatID;
    public static int MultiInvitation;
    public static string MultiInvitationRoomOwner;
    public static bool MultiInvitationRoomLocked;
    public static int SelectedMultiPlayRoomID;
    public static string SelectedMultiPlayPhotonAppID;
    public static int SelectedTowerMultiPartyIndex;
    public static string SelectedProductID;
    public static string SelectedProductPrice;
    public static string SelectedProductIcon;
    public static int EditedYear;
    public static int EditedMonth;
    public static int EditedDay;
    public static int BeforeCoin;
    public static int AfterCoin;
    public static bool IsTutorialEnd;
    public static bool DebugIsPlayTutorial;
    public static string PubHash;
    public static string UrgencyPubHash;
    public static string SelectedChallengeMissionTrophy;
    public static int ChatChannelViewNum;
    public static int ChatChannelMax;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedChapter;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedSection;
    [GlobalVars.ResetOnLogin]
    public static GlobalVars.GlobalVar<string> ReplaySelectedQuestID;
    public static UnitGetParam UnitGetReward;
    public static string PreEventName;
    public static bool ForceSceneChange;
    public static GlobalVars.EventQuestListType ReqEventPageListType;
    public static int EventQuestMinimumLevel;
    public static bool KeyQuestTimeOver;
    public static string SelectedTowerID;
    public static string SelectedFloorID;
    public static GlobalVars.GlobalVar<long> SelectedSupportUnitUniqueID;
    public static ItemSelectListItemData ItemSelectListItemData;
    public static ArtifactSelectListItemData ArtifactListItem;
    public static string[] ConditionJobs;
    public static CollaboSkillParam.Pair SelectedCollaboSkillPair;
    public static string TeamName;
    public static GlobalVars.UserSelectionPartyData UserSelectionPartyDataInfo;
    public static bool PartyUploadFinished;
    public static bool RankingQuestSelected;
    public static RankingQuestParam SelectedRankingQuestParam;
    public static TrophyType SelectedTrophyType;
    public static GlobalVars.PurchaseType SelectedPurchaseType;
    public static GlobalVars.RecommendTeamSetting RecommendTeamSettingValue;
    public static string OAuth2AccessToken;
    public static string NewPlayerName;
    public static string NewPlayerLevel;
    public static string FacebookID;
    public static OInt MaxDamageInBattle;
    public static OInt MaxHpInBattle;
    public static OInt MaxLevelInBattle;
    public static bool ShowUnitNames;

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

    public static void SetDropTableGeneratedTime()
    {
      GlobalVars.mDropTableGeneratedUnixTime = Network.GetServerTime();
    }

    public static long GetDropTableGeneratedUnixTime()
    {
      return GlobalVars.mDropTableGeneratedUnixTime;
    }

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
    }

    public enum PurchaseType
    {
      Bundles,
      Gems,
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

      public void Reset()
      {
        this.mValue = this.mDefaultValue;
      }

      public T Get()
      {
        return this.mValue;
      }

      public void Set(T value)
      {
        if (((object) value != null || (object) this.mValue == null) && ((object) value == null || value.Equals((object) this.mValue)))
          return;
        this.mValue = value;
        if (this.mListeners == null)
          return;
        this.mListeners();
      }

      public void AddChangeEventListener(GlobalVars.GlobalVar<T>.VariableChangeEvent callback)
      {
        if (this.mListeners == null)
          this.mListeners = callback;
        else
          this.mListeners += callback;
      }

      public void RemoveChangeEventListener(GlobalVars.GlobalVar<T>.VariableChangeEvent callback)
      {
        this.mListeners -= callback;
      }

      public override string ToString()
      {
        if ((object) this.mValue != null)
          return this.mValue.ToString();
        return (string) null;
      }

      public static implicit operator T(GlobalVars.GlobalVar<T> src)
      {
        return src.mValue;
      }

      public delegate void VariableChangeEvent();
    }

    public class ResetOnLogin : Attribute
    {
    }
  }
}
