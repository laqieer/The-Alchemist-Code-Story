// Decompiled with JetBrains decompiler
// Type: PlayerPrefsUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

#nullable disable
public class PlayerPrefsUtility
{
  private const string GN_SOUND = "サウンド設定";
  private const string GN_DEBUG = "デバッグ用";
  private const string GN_IOS_TERMS = "iOS規約対応";
  private const string GN_PARTY = "パーティ";
  private const string GN_PARTY_EDIT = "パーティ編成情報";
  private const string GN_SORT_FILTER = "ソート/フィルター";
  public static readonly string CONFIG_SOUNDVOLUME = "SoundVolume";
  public static readonly string CONFIG_MUSICVOLUME = "MusicVolume";
  public static readonly string CONFIG_VOICEVOLUME = "VoiceVolume";
  public static readonly string CONFIG_INPUTMETHOD = "InputMethod";
  public static readonly string CONFIG_OKYAKUSAMACODE = "CUID";
  public static readonly string PARTY_TEAM_PREFIX = "TEAM_";
  public static readonly string PARTY_TEAM_SUFFIX = "_FORCED";
  public static readonly string VERSUS_ID_KEY = "VERSUS_PLACEMENT_";
  public static readonly string RANKMATCH_ID_KEY = "RANKMATCH_PLACEMENT_";
  public static readonly string MULTITW_ID_KEY = "MULTITW_PLACEMENT_";
  public static readonly string FRIEND_REQUEST_CACHE = "FRIENDS";
  public static readonly string HOME_LASTACCESS_PLAYER_LV = "lastplv";
  public static readonly string HOME_LASTACCESS_VIP_LV = "lastviplv";
  public static readonly string PREFS_KEY_FRIEND_SORT = "FRIENDLIST_SORTTYPE";
  public static readonly string SAVE_UPDATE_TROPHY_LIST_KEY = "gm_sutlist";
  public static readonly string GAMEMANAGER_UPSCALE = "UPSCALE";
  public static readonly string TEAM_ID_KEY = "TeamID";
  public static readonly string MULTI_PLAY_TEAM_ID_KEY = "MultiPlayTeamID";
  public static readonly string ARENA_TEAM_ID_KEY = "ArenaTeamID";
  public static readonly string RANKMATCH_TEAM_ID_KEY = "RankMatchTeamID";
  public static readonly string RAID_TEAM_ID_KEY = "RaidTeamID";
  public static readonly string ROOM_COMMENT_KEY = "MultiPlayRoomComment";
  public static readonly string MULTI_HI_SPEED = "MultiPlayHiSpeed";
  public static readonly string MULTI_BATTLE_SPEED = "MultiPlayBattleSpeed";
  public static readonly string MULTI_AUTO_ALLOWED = "MultiPlayAutoAllowed";
  public static readonly string USE_ASSETBUNDLES = "UseAssetBundles";
  public static readonly string USE_SERIALIZED_PARAMS = "UseSerializedParams";
  public static readonly string USE_ENCRYPTION = "UseEncryption";
  public static readonly string DEBUG_USE_DEV_SERVER = "UseDevServer";
  public static readonly string DEBUG_USE_AWS_SERVER = "UseAwsServer";
  public static readonly string DEBUG_NEWGAME = "NewGame";
  public static readonly string DEBUG_USE_LOCAL_DATA = "LocalData";
  public static readonly string DEBUG_USE_SERVER_DATA = "ServerData";
  public static readonly string DEBUG_AUTO_MARK = "AutoPlayMark";
  public static readonly string DEBUG_AUTOPLAY = "AutoPlay";
  public static readonly string DEBUG_IS_PLAY_TUTORIAL = "IsPlayTutorial";
  public static readonly string SCENESTARTUP_CLEARCACHE = "CLEARCACHE";
  public static readonly string PLAYERDATA_INVENTORY = "INVENTORY";
  public static readonly string ALTER_PREV_CHECK_HASH = "PREV_CHECK_HASH";
  public static readonly string UNITLIST_UNIT_SORT_MODE = "UnitSortMode";
  public static readonly string UNIT_SORT_FILTER_PREFIX = "UNITLIST";
  public const string FILTER_UNIT = "FilterUnit";
  public static readonly string FILTER_CONCEPT_CARD = "FilterConceptCard";
  public static readonly string FILTER_RUNE = "FilterRune";
  public const string FILTER_ARTIFACT = "FilterArtifact";
  public const string SORT_RUNE = "SortRune";
  public static readonly string EDITOR_SELECT_PLATFORM = "EditorPlatform";
  public static readonly string CONFIG_USE_PUSH_STAMINA = "UsePushStamina";
  public static readonly string CONFIG_USE_PUSH_NEWS = "UsePushNews";
  public static readonly string CONFIG_USE_CHAT_STATE = "ChatState";
  public static readonly string CONFIG_USE_STAMP_STATE = "StampState";
  public static readonly string CONFIG_USE_CHARGE_DISP = "CONFIG_CHARGE_DISP";
  public static readonly string CONFIG_USE_AUTO_PLAY = "UseAutoPlay";
  public static readonly string CONFIG_USE_AUTOMODE_TREASURE = "UseAutoTreasure";
  public static readonly string CONFIG_USE_AUTOMODE_DISABLE_SKILL = "UseAutoDisableSkill";
  public static readonly string CONFIG_USE_DIRECTIONCUT = "DirectionCut";
  public static readonly string CONFIG_USE_AUTO_PLAY_MULTI = "UseAutoPlayMulti";
  public static readonly string CONFIG_USE_AUTOMODE_TREASURE_MULTI = "UseAutoTreasureMulti";
  public static readonly string CONFIG_USE_AUTOMODE_DISABLE_SKILL_MULTI = "UseAutoDisableSkillMulti";
  public static readonly string CONFIG_USE_AUTO_PLAY_RM = "UseAutoPlayRM";
  public static readonly string CONFIG_USE_BATTLE_SPEED = "UseBattleSpeed";
  public static readonly string RECOMMENDED_TEAM_SETTING_KEY = "RecommendedTeamSetting";
  public static readonly string UNIT_LEVELUP_EXPITEM_CHECKS = "UnitLevelUpExpItemChecks";
  public static readonly string ARTIFACT_BULK_LEVELUP = "ArtifactBulkLevelUp";
  public static readonly string CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS = "ConceptCardLevelUpExpItemChecks";
  public static readonly string CONCEPT_CARD_LEVELUP_PAGE_CHECKS = "ConceptCardLevelUpPageChecks";
  public static readonly string UNIT_AWAKE_PIECE_CHECKS = "UnitAwakePieceChecks";
  public static readonly string CHAT_TEMPLATE_MESSAGE = "ChatTemplateMessage";
  public static readonly string CHAT_OFFICIAL_MESSAGE_LAST_READ_DATE = nameof (CHAT_OFFICIAL_MESSAGE_LAST_READ_DATE);
  public static readonly string CHALLENGEMISSION_HAS_SHOW_MESSAGE = nameof (CHALLENGEMISSION_HAS_SHOW_MESSAGE);
  public static readonly string AWAKE_VERSION = nameof (AWAKE_VERSION);
  public static readonly string ARTIFACT_EXCLUDE_EQUIPED = nameof (ARTIFACT_EXCLUDE_EQUIPED);
  public static readonly string GALLERY_SETTING = nameof (GALLERY_SETTING);
  public static readonly string GALLERY_CONCEPTCARD_SETTING = nameof (GALLERY_CONCEPTCARD_SETTING);
  public static readonly string GALLERY_ARTIFACT_SETTING = nameof (GALLERY_ARTIFACT_SETTING);
  public static readonly string CONCEPTCARD_EXCLUDE_EQUIPED = nameof (CONCEPTCARD_EXCLUDE_EQUIPED);
  public static readonly string ORDEAL_RELEASE_ANIMATION_PLAYED = nameof (ORDEAL_RELEASE_ANIMATION_PLAYED);
  public static readonly string RANKMATCH_SEASON_REWARD_RECEIVE_DATE = nameof (RANKMATCH_SEASON_REWARD_RECEIVE_DATE);
  public static readonly string RAID_RANKING_RECEIVE_DATE = "RAID_RANKING_REWARD_RECEIVE_DATE";
  public static readonly string GUILDRAID_RANKING_RECEIVE_DATE = "GUILDRAID_RANKING_REWARD_RECEIVE_DATE";
  public static readonly string WORLDRAID_REWARD_RECEIVE_DATE = nameof (WORLDRAID_REWARD_RECEIVE_DATE);
  public static readonly string UNLOCK_LEVEL_ANIMATION_PLAYED = nameof (UNLOCK_LEVEL_ANIMATION_PLAYED);
  public static readonly string UNLOCK_LEVEL_NOTIFIED = nameof (UNLOCK_LEVEL_NOTIFIED);
  public static readonly string WARNED_SHOP_ITEM_TIMEOUT = nameof (WARNED_SHOP_ITEM_TIMEOUT);
  public static readonly string WARNED_EVENTSHOP_ITEM_TIMEOUT = nameof (WARNED_EVENTSHOP_ITEM_TIMEOUT);
  public static readonly string WARNED_LIMITEDSHOP_ITEM_TIMEOUT = nameof (WARNED_LIMITEDSHOP_ITEM_TIMEOUT);
  public static readonly string WARNED_PORTSHOP_ITEM_TIMEOUT = nameof (WARNED_PORTSHOP_ITEM_TIMEOUT);
  public static readonly string JUKEBOX_UNLOCK_BADGE_INFO = nameof (JUKEBOX_UNLOCK_BADGE_INFO);
  public static readonly string RELEASE_STORY_PART_KEY = "RELEASE_STORY_PART_KEY_";
  public static readonly string CONFIRM_TOWER_MISSION_QUEST_ID = "CONFIRM_TOWER_CHALLANGE_MISSION_ID";
  public static readonly string LAST_SELECTED_STORY_QUEST_ID = nameof (LAST_SELECTED_STORY_QUEST_ID);
  public static readonly string BEGINNER_TOP_HAS_VISITED = nameof (BEGINNER_TOP_HAS_VISITED);
  public static readonly string LAST_BEGINNER_NOTIFIED_DATE = nameof (LAST_BEGINNER_NOTIFIED_DATE);
  public static readonly string PREFS_KEY_GUILD_MEMBER_SORT = "GUILD_MEMBER_SORTTYPE";
  public static readonly string PREFS_KEY_USE_DLC = "USE_DLC";
  public static readonly string PREFS_KEY_TUTORIAL_CLEARED = "TUTORIAL_CLEARED";
  public static readonly string PREFS_KEY_GENESIS_DIFF_OPEN_QUEST_CACHE = "GENESIS_DIFF_OPEN_QUEST";
  public static readonly string PREFS_KEY_GENESIS_BOSS_OPEN_QUEST_CACHE = "GENESIS_BOSS_OPEN_QUEST";
  public static readonly string PREFS_KEY_GENESIS_SKIP_OPEN_QUEST_CACHE = "GENESIS_SKIP_OPEN_QUEST";
  public static readonly string PREFS_KEY_ADVANCE_DIFF_OPEN_QUEST_CACHE = "ADVANCE_DIFF_OPEN_QUEST";
  public static readonly string PREFS_KEY_ADVANCE_BOSS_OPEN_QUEST_CACHE = "ADVANCE_BOSS_OPEN_QUEST";
  public static readonly string PREFS_KEY_ADVANCE_SKIP_OPEN_QUEST_CACHE = "ADVANCE_SKIP_OPEN_QUEST";
  public static readonly string PREFS_GVG_PERIOD = "GVG_PERIOD";
  public static readonly string PREFS_GVG_PERIOD_LEAGUE = "GVG_PERIOD_LEAGUE";
  public static readonly string PREFS_KEY_EXPIRE_ITEM = "EXPIRE_ITEM";
  public static readonly string IS_ENABLE_SHOW_SIZE_BEFORE_DOWNLOADING = nameof (IS_ENABLE_SHOW_SIZE_BEFORE_DOWNLOADING);
  public static readonly string PREFS_KEY_GET_UNIT_QUESTS_POPUP_CACHE = "GET_UNIT_QUESTS_POPUP";
  public static readonly string PREFS_KEY_LAST_SELECT_HOME_UNIT = "LAST_SELECT_HOME_UNIT";
  public static readonly string RUNE_EXCLUDE_EQUIPED = nameof (RUNE_EXCLUDE_EQUIPED);
  public static readonly string PREFS_KEY_WORLD_RAID_NOTICE = "WORLD_RAID_NOTICE";
  public static readonly string PREFS_KEY_WORLD_RAID_LOG_ID = "WORLD_RAID_LOG_ID";
  private static Dictionary<string, PlayerPrefsUtility.WinCacheData> mWinCache = new Dictionary<string, PlayerPrefsUtility.WinCacheData>();

  public static string GetTeamKey(int index)
  {
    return PartyUtility.GetCurrentTeamID((PartyWindow2.EditPartyTypes) index);
  }

  private static string PLAYER_PREFS_DATA_PATH => AppPath.persistentDataPath + "/prefs/";

  private static string PLAYER_PREFS_DATA_PATH_FORMAT
  {
    get => PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH + "{0}.bin";
  }

  public static bool SetInt(string key, int value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefsUtility.SetForWindows<int>(key, value);
    return true;
  }

  public static int GetInt(string key, int default_value = 0)
  {
    return PlayerPrefsUtility.GetForWindow<int>(key, default_value);
  }

  public static bool SetFloat(string key, float value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefsUtility.SetForWindows<float>(key, value);
    return true;
  }

  public static float GetFloat(string key, float default_value = 0.0f)
  {
    return PlayerPrefsUtility.GetForWindow<float>(key, default_value);
  }

  public static bool SetString(string key, string value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefsUtility.SetForWindows<string>(key, value);
    return true;
  }

  public static string GetString(string key, string default_value = "")
  {
    return PlayerPrefsUtility.GetForWindow<string>(key, default_value);
  }

  public static bool SetLong(string key, long value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefsUtility.SetForWindows<long>(key, value);
    return true;
  }

  public static long GetLong(string key, long default_value = 0)
  {
    return PlayerPrefsUtility.GetForWindow<long>(key, default_value);
  }

  public static bool HasKey(string key)
  {
    return !string.IsNullOrEmpty(key) && PlayerPrefsUtility.HasKeyForWindow(key);
  }

  public static void DeleteKey(string key) => PlayerPrefsUtility.DeleteKeyForWindows(key);

  public static void DeleteAll() => PlayerPrefsUtility.DeleteAllForWindows();

  public static void Save() => PlayerPrefs.Save();

  private static T CastValue<T>(string _str, T _default_value)
  {
    T obj = _default_value;
    if (!string.IsNullOrEmpty(_str))
      obj = (object) typeof (T) != (object) typeof (int) ? ((object) typeof (T) != (object) typeof (long) ? ((object) typeof (T) != (object) typeof (float) ? (T) (object) Convert.ToString(_str) : (T) (object) Convert.ToSingle(_str)) : (T) (object) Convert.ToInt64(_str)) : (T) (object) Convert.ToInt32(_str);
    return obj;
  }

  private static bool RegisterWinCacheAndCheckNeedCreateFile(string _key, string _default_value)
  {
    if (PlayerPrefsUtility.mWinCache.ContainsKey(_key))
      return false;
    if (PlayerPrefsUtility.IsExistFileForWindow(_key))
    {
      try
      {
        byte[] data = File.ReadAllBytes(string.Format(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH_FORMAT, (object) _key));
        if (data != null)
        {
          if (data.Length > 0)
          {
            string str = MyEncrypt.Decrypt(0, data);
            PlayerPrefsUtility.mWinCache.Add(_key, new PlayerPrefsUtility.WinCacheData(str, true));
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    PlayerPrefsUtility.mWinCache.Add(_key, new PlayerPrefsUtility.WinCacheData(_default_value));
    return true;
  }

  private static void SetForWindows<T>(string _key, T _value)
  {
    if (string.IsNullOrEmpty(_key))
      return;
    string str = (object) _value != null ? _value.ToString() : string.Empty;
    bool file = PlayerPrefsUtility.RegisterWinCacheAndCheckNeedCreateFile(_key, str);
    if (!PlayerPrefsUtility.mWinCache.ContainsKey(_key))
      return;
    bool flag = false;
    if (file || !file && !PlayerPrefsUtility.mWinCache[_key].is_initalized || !file && PlayerPrefsUtility.mWinCache[_key].is_initalized && PlayerPrefsUtility.mWinCache[_key].value != str)
      flag = true;
    if (!flag)
      return;
    try
    {
      if (!Directory.Exists(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH))
        Directory.CreateDirectory(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH);
      byte[] bytes = MyEncrypt.Encrypt(0, str) ?? new byte[0];
      File.WriteAllBytes(string.Format(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH_FORMAT, (object) _key), bytes);
      PlayerPrefsUtility.mWinCache[_key].value = str;
      PlayerPrefsUtility.mWinCache[_key].is_initalized = true;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private static T GetForWindow<T>(string _key, T _default_value)
  {
    if (string.IsNullOrEmpty(_key))
      return _default_value;
    string _default_value1 = (object) _default_value != null ? _default_value.ToString() : string.Empty;
    PlayerPrefsUtility.RegisterWinCacheAndCheckNeedCreateFile(_key, _default_value1);
    return !PlayerPrefsUtility.mWinCache.ContainsKey(_key) || !PlayerPrefsUtility.mWinCache[_key].is_initalized ? _default_value : PlayerPrefsUtility.CastValue<T>(PlayerPrefsUtility.mWinCache[_key].value, _default_value);
  }

  private static bool HasKeyForWindow(string _key)
  {
    PlayerPrefsUtility.RegisterWinCacheAndCheckNeedCreateFile(_key, string.Empty);
    return PlayerPrefsUtility.mWinCache.ContainsKey(_key) && PlayerPrefsUtility.mWinCache[_key].is_initalized;
  }

  private static bool IsExistFileForWindow(string _key)
  {
    return File.Exists(string.Format(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH_FORMAT, (object) _key));
  }

  private static void DeleteKeyForWindows(string _key)
  {
    try
    {
      if (PlayerPrefsUtility.IsExistFileForWindow(_key))
        File.Delete(string.Format(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH_FORMAT, (object) _key));
      if (!PlayerPrefsUtility.mWinCache.ContainsKey(_key))
        return;
      PlayerPrefsUtility.mWinCache.Remove(_key);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private static void DeleteAllForWindows()
  {
    foreach (string file in Directory.GetFiles(PlayerPrefsUtility.PLAYER_PREFS_DATA_PATH))
      PlayerPrefsUtility.DeleteKeyForWindows(Path.GetFileNameWithoutExtension(file));
  }

  public static void SetUnlockLevelAnimationPlayed(UnlockTargets value)
  {
    ulong num = (ulong) ((UnlockTargets) PlayerPrefsUtility.GetLong(PlayerPrefsUtility.UNLOCK_LEVEL_ANIMATION_PLAYED) | value);
    PlayerPrefsUtility.SetLong(PlayerPrefsUtility.UNLOCK_LEVEL_ANIMATION_PLAYED, (long) num);
  }

  public static bool GetIsUnlockLevelAnimationPlayed(UnlockTargets value)
  {
    return ((UnlockTargets) PlayerPrefsUtility.GetLong(PlayerPrefsUtility.UNLOCK_LEVEL_ANIMATION_PLAYED) & value) != (UnlockTargets) 0;
  }

  public enum ePlayerPrefsValueType
  {
    Int,
    Long,
    Float,
    String,
  }

  public enum eAttributeType
  {
    DirectKey,
    IndexKey,
  }

  [Conditional("UNITY_EDITOR")]
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
  public class PlayerPrefsValueTypeAttribute : Attribute
  {
    public const string DEFAULT_GROUP_NAME = "その他";
    private string m_GroupName;
    private string m_Description;
    private PlayerPrefsUtility.ePlayerPrefsValueType m_ValueType;
    protected PlayerPrefsUtility.eAttributeType m_AttributeType;

    public PlayerPrefsValueTypeAttribute(
      PlayerPrefsUtility.ePlayerPrefsValueType valueType,
      string groupName,
      string description)
    {
      this.m_ValueType = valueType;
      this.m_GroupName = !string.IsNullOrEmpty(groupName) ? groupName : "その他";
      this.m_Description = description;
    }

    public string GroupName => this.m_GroupName;

    public string Description => this.m_Description;

    public PlayerPrefsUtility.ePlayerPrefsValueType ValueType => this.m_ValueType;

    public PlayerPrefsUtility.eAttributeType AttributeType => this.m_AttributeType;
  }

  [Conditional("UNITY_EDITOR")]
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  public class PlayerPrefsValueTypeUseIndexAttribute : 
    PlayerPrefsUtility.PlayerPrefsValueTypeAttribute
  {
    private int m_Length;

    public PlayerPrefsValueTypeUseIndexAttribute(
      PlayerPrefsUtility.ePlayerPrefsValueType valueType,
      string groupName,
      string description,
      int length)
      : base(valueType, groupName, description)
    {
      this.m_AttributeType = PlayerPrefsUtility.eAttributeType.IndexKey;
      this.m_Length = length;
    }

    public int Length => this.m_Length;
  }

  private class WinCacheData
  {
    public string value;
    public bool is_initalized;

    public WinCacheData(string _value, bool _is_initalized = false)
    {
      this.value = _value;
      this.is_initalized = _is_initalized;
    }
  }
}
