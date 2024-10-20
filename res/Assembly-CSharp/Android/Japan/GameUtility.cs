﻿// Decompiled with JetBrains decompiler
// Type: GameUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using MsgPack;
using SRPG;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameUtility
{
  private static Toggle.ToggleEvent nullToggleEvent = new Toggle.ToggleEvent();
  private static StringBuilder mSB = new StringBuilder(128);
  private static string LOGININFO_ALREADY_READ = "LoginInfoRead";
  public static readonly string BEFORE_LOGIN_NEWS_INFO_TOKEN = nameof (BEFORE_LOGIN_NEWS_INFO_TOKEN);
  private static float m_sound_volume = -1f;
  private static float m_music_volume = -1f;
  private static float m_voice_volume = -1f;
  public static GameUtility.BooleanConfig Config_UseAssetBundles = new GameUtility.BooleanConfig(PlayerPrefsUtility.USE_ASSETBUNDLES, false);
  public static GameUtility.BooleanConfig Config_UseDevServer = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_DEV_SERVER, false);
  public static GameUtility.BooleanConfig Config_UseAwsServer = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_AWS_SERVER, false);
  public static GameUtility.BooleanConfig Config_NewGame = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_NEWGAME, false);
  public static GameUtility.BooleanConfig Config_UseLocalData = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_LOCAL_DATA, false);
  public static GameUtility.BooleanConfig Config_AutoPlayMark = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_AUTO_MARK, false);
  public static GameUtility.BooleanConfig Config_AutoPlay = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_AUTOPLAY, false);
  public static GameUtility.BooleanConfig Config_UseAutoPlay = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTO_PLAY, false);
  public static GameUtility.BooleanConfig Config_AutoMode_Treasure = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_TREASURE, false);
  public static GameUtility.BooleanConfig Config_AutoMode_DisableSkill = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_DISABLE_SKILL, false);
  public static GameUtility.BooleanConfig Config_DirectionCut = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_DIRECTIONCUT, true);
  public static GameUtility.BooleanConfig Config_UseBattleSpeed = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_BATTLE_SPEED, false);
  public static GameUtility.BooleanConfig Config_UsePushStamina = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_PUSH_STAMINA, true);
  public static GameUtility.BooleanConfig Config_UsePushNews = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_PUSH_NEWS, false);
  public static GameUtility.BooleanConfig Config_ChatState = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_CHAT_STATE, true);
  public static GameUtility.BooleanConfig Config_MultiState = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_STAMP_STATE, true);
  public static GameUtility.BooleanConfig Config_ChargeDisp = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_CHARGE_DISP, true);
  private static ObjectPacker Packer = new ObjectPacker();
  private static GameUtility.UnitShowSetting[] UnitShowSettings = new GameUtility.UnitShowSetting[29]
  {
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_FIRE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_WATER"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_WIND"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_THUNDER"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_SHINE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_DARK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_ZENEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_TYUEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_KOUEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE1"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE2"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE3"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE4"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE5"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE6"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_SYOJUN"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "SORT_KOUJUN"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "SORT_LEVEL"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_FAVORITE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_JOBRANK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_HP"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_ATK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_DEF"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_MAG"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_MND"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_SPD"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_TOTAL"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_AWAKE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_COMBINATION"
    }
  };
  public static readonly GameUtility.UnitSortModes[] UnitSortMenuItems = new GameUtility.UnitSortModes[14]
  {
    GameUtility.UnitSortModes.Time,
    GameUtility.UnitSortModes.Level,
    GameUtility.UnitSortModes.JobRank,
    GameUtility.UnitSortModes.HP,
    GameUtility.UnitSortModes.Atk,
    GameUtility.UnitSortModes.Def,
    GameUtility.UnitSortModes.Mag,
    GameUtility.UnitSortModes.Mnd,
    GameUtility.UnitSortModes.Spd,
    GameUtility.UnitSortModes.Total,
    GameUtility.UnitSortModes.Awake,
    GameUtility.UnitSortModes.Combination,
    GameUtility.UnitSortModes.Rarity,
    GameUtility.UnitSortModes.Name
  };
  private static GameUtility.EScene mCurrentScene = GameUtility.EScene.UNKNOWN;
  public static readonly Color32 Color32_White = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
  public static readonly Color32 Color32_Black = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
  private static Color mFadeInColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  private static readonly Vector3 _deformRadiusLength = new Vector3(100f, 0.01f, 0.0f);
  public static Quaternion Yaw180 = Quaternion.AngleAxis(180f, Vector3.up);
  public static Quaternion Yaw90 = Quaternion.AngleAxis(90f, Vector3.up);
  public static Quaternion Yaw90Inv = Quaternion.AngleAxis(-90f, Vector3.up);
  private static int mNeverSleepCount = 0;
  private static Application.LogCallback mLogCallbacks;
  private static bool mATCTextureSupport;
  private static bool mDXTTextureSupport;
  private static bool mPVRTextureSupport;
  private static string[] mGLExtensions;
  private static int mLayerBG;
  private static int mLayerCH;
  private static int mLayerHidden;
  private static int mLayerDefault;
  private static int mLayerCH0;
  private static int mLayerCH1;
  private static int mLayerCH2;
  private static int mLayerCHBG0;
  private static int mLayerCHBG1;
  private static int mLayerCHBG2;
  private static int mLayerUI;
  private static int mLayerEffect;
  private static bool mLayerIndexInitialized;
  private static Texture2D mTransparentTexture;
  public const float SmallNumber = 0.0001f;

  public static void SetToggle(Toggle toggle, bool value)
  {
    Toggle.ToggleEvent onValueChanged = toggle.onValueChanged;
    ToggleGroup group = toggle.group;
    toggle.onValueChanged = GameUtility.nullToggleEvent;
    toggle.group = (ToggleGroup) null;
    toggle.isOn = value;
    toggle.onValueChanged = onValueChanged;
    toggle.group = group;
  }

  public static void ClearPreferences()
  {
    PlayerPrefsUtility.DeleteAll();
    EditorPlayerPrefs.DeleteAll();
  }

  public static bool IsDebugBuild
  {
    get
    {
      return Debug.isDebugBuild;
    }
  }

  public static bool IsStripBuild
  {
    get
    {
      return (UnityEngine.Object) UnityEngine.Resources.Load<TextAsset>("strip") != (UnityEngine.Object) null;
    }
  }

  public static StringBuilder GetStringBuilder()
  {
    GameUtility.mSB.Length = 0;
    return GameUtility.mSB;
  }

  public static void ReparentGameObjects<T>(List<T> objects, Transform newParent) where T : Component
  {
    if (objects == null)
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if ((UnityEngine.Object) objects[index] != (UnityEngine.Object) null)
        objects[index].transform.SetParent(newParent, false);
    }
  }

  public static void ReparentGameObjects(List<GameObject> objects, Transform newParent)
  {
    if (objects == null)
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if ((UnityEngine.Object) objects[index] != (UnityEngine.Object) null)
        objects[index].transform.SetParent(newParent, false);
    }
  }

  public static void ToggleGraphic(GameObject go, bool enable)
  {
    foreach (Behaviour componentsInChild in go.GetComponentsInChildren<Graphic>(false))
      componentsInChild.enabled = enable;
  }

  public static MoveInputMethods Config_InputMethod
  {
    get
    {
      return (MoveInputMethods) PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONFIG_INPUTMETHOD, 0);
    }
    set
    {
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONFIG_INPUTMETHOD, (int) value, false);
    }
  }

  public static float Config_SoundVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_sound_volume)
        return GameUtility.m_sound_volume;
      GameUtility.m_sound_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_SOUNDVOLUME, 1f);
      return GameUtility.m_sound_volume;
    }
    set
    {
      GameUtility.m_sound_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_SOUNDVOLUME, value, false);
    }
  }

  public static float Config_MusicVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_music_volume)
        return GameUtility.m_music_volume;
      GameUtility.m_music_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_MUSICVOLUME, 1f);
      return GameUtility.m_music_volume;
    }
    set
    {
      GameUtility.m_music_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_MUSICVOLUME, value, false);
    }
  }

  public static float Config_VoiceVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_voice_volume)
        return GameUtility.m_voice_volume;
      GameUtility.m_voice_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_VOICEVOLUME, 1f);
      return GameUtility.m_voice_volume;
    }
    set
    {
      GameUtility.m_voice_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_VOICEVOLUME, value, false);
    }
  }

  public static string Config_OkyakusamaCode
  {
    get
    {
      return PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONFIG_OKYAKUSAMACODE, (string) null);
    }
    set
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIG_OKYAKUSAMACODE, value, true);
    }
  }

  public static string getLoginInfoRead()
  {
    string empty = string.Empty;
    return (string) MonoSingleton<UserInfoManager>.Instance.GetValue(GameUtility.LOGININFO_ALREADY_READ);
  }

  public static void setLoginInfoRead(string value)
  {
    string str = (string) MonoSingleton<UserInfoManager>.Instance.GetValue(GameUtility.LOGININFO_ALREADY_READ);
    if (value == null || !(str != value))
      return;
    MonoSingleton<UserInfoManager>.Instance.SetValue(GameUtility.LOGININFO_ALREADY_READ, (object) value, true);
  }

  public static bool isLoginInfoDisplay()
  {
    string loginInfoRead = GameUtility.getLoginInfoRead();
    if (string.IsNullOrEmpty(loginInfoRead))
      return true;
    return TimeManager.FromDateTime(TimeManager.ServerTime) > TimeManager.FromDateTime(DateTime.Parse(loginInfoRead).AddDays(1.0));
  }

  public static bool GetUnitShowSetting(int index)
  {
    if (index < 0 || index >= GameUtility.UnitShowSettings.Length)
      return false;
    if (!PlayerPrefsUtility.HasKey(GameUtility.UnitShowSettings[index].key))
      return GameUtility.UnitShowSettings[index].on != 0;
    return PlayerPrefsUtility.GetInt(GameUtility.UnitShowSettings[index].key, 0) != 0;
  }

  public static void SetUnitShowSetting(int index, bool value)
  {
    if (index < 0 || index >= GameUtility.UnitShowSettings.Length)
      return;
    PlayerPrefsUtility.SetInt(GameUtility.UnitShowSettings[index].key, !value ? 0 : 1, false);
  }

  public static void ResetUnitShowSetting()
  {
    for (int index = 0; index < GameUtility.UnitShowSettings.Length; ++index)
      GameUtility.SetUnitShowSetting(index, GameUtility.UnitShowSettings[index].on != 0);
  }

  public static void SortUnits(List<UnitData> units, GameUtility.UnitSortModes type, bool ascending, out int[] sortValues, bool outputSortedValues = false)
  {
    List<SortKeyValue<UnitData, int>> sortKeyValueList = new List<SortKeyValue<UnitData, int>>(units.Count);
    for (int index = 0; index < units.Count; ++index)
      sortKeyValueList.Add(new SortKeyValue<UnitData, int>()
      {
        mValue = units[index]
      });
    sortValues = new int[units.Count];
    List<Func<UnitData, UnitData, int>> compares = new List<Func<UnitData, UnitData, int>>();
    compares.Clear();
    List<Func<UnitData, UnitData, int>> funcList1 = compares;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache0 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_Lv);
    }
    // ISSUE: reference to a compiler-generated field
    Func<UnitData, UnitData, int> fMgCache0 = GameUtility.\u003C\u003Ef__mg\u0024cache0;
    funcList1.Add(fMgCache0);
    List<Func<UnitData, UnitData, int>> funcList2 = compares;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache1 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_Rarity);
    }
    // ISSUE: reference to a compiler-generated field
    Func<UnitData, UnitData, int> fMgCache1 = GameUtility.\u003C\u003Ef__mg\u0024cache1;
    funcList2.Add(fMgCache1);
    List<Func<UnitData, UnitData, int>> funcList3 = compares;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache2 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_JobRank);
    }
    // ISSUE: reference to a compiler-generated field
    Func<UnitData, UnitData, int> fMgCache2 = GameUtility.\u003C\u003Ef__mg\u0024cache2;
    funcList3.Add(fMgCache2);
    List<Func<UnitData, UnitData, int>> funcList4 = compares;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache3 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_RarityMax);
    }
    // ISSUE: reference to a compiler-generated field
    Func<UnitData, UnitData, int> fMgCache3 = GameUtility.\u003C\u003Ef__mg\u0024cache3;
    funcList4.Add(fMgCache3);
    List<Func<UnitData, UnitData, int>> funcList5 = compares;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache4 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_RarityInit);
    }
    // ISSUE: reference to a compiler-generated field
    Func<UnitData, UnitData, int> fMgCache4 = GameUtility.\u003C\u003Ef__mg\u0024cache4;
    funcList5.Add(fMgCache4);
    switch (type)
    {
      case GameUtility.UnitSortModes.Time:
        return;
      case GameUtility.UnitSortModes.Level:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].Lv;
        List<Func<UnitData, UnitData, int>> funcList6 = compares;
        // ISSUE: reference to a compiler-generated field
        if (GameUtility.\u003C\u003Ef__mg\u0024cache5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GameUtility.\u003C\u003Ef__mg\u0024cache5 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_Lv);
        }
        // ISSUE: reference to a compiler-generated field
        Func<UnitData, UnitData, int> fMgCache5 = GameUtility.\u003C\u003Ef__mg\u0024cache5;
        funcList6.Remove(fMgCache5);
        break;
      case GameUtility.UnitSortModes.JobRank:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].CurrentJob.Rank;
        List<Func<UnitData, UnitData, int>> funcList7 = compares;
        // ISSUE: reference to a compiler-generated field
        if (GameUtility.\u003C\u003Ef__mg\u0024cache7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GameUtility.\u003C\u003Ef__mg\u0024cache7 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_JobRank);
        }
        // ISSUE: reference to a compiler-generated field
        Func<UnitData, UnitData, int> fMgCache7 = GameUtility.\u003C\u003Ef__mg\u0024cache7;
        funcList7.Remove(fMgCache7);
        break;
      case GameUtility.UnitSortModes.HP:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.hp;
        break;
      case GameUtility.UnitSortModes.Atk:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.atk;
        break;
      case GameUtility.UnitSortModes.Def:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.def;
        break;
      case GameUtility.UnitSortModes.Mag:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.mag;
        break;
      case GameUtility.UnitSortModes.Mnd:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.mnd;
        break;
      case GameUtility.UnitSortModes.Spd:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.spd;
        break;
      case GameUtility.UnitSortModes.Total:
        for (int index = 0; index < units.Count; ++index)
        {
          sortKeyValueList[index].mKey = (int) units[index].Status.param.atk;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.def;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.mag;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.mnd;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.spd;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.dex;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.cri;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.luk;
        }
        break;
      case GameUtility.UnitSortModes.Awake:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].AwakeLv;
        break;
      case GameUtility.UnitSortModes.Combination:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].GetCombination();
        break;
      case GameUtility.UnitSortModes.Rarity:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].Rarity;
        List<Func<UnitData, UnitData, int>> funcList8 = compares;
        // ISSUE: reference to a compiler-generated field
        if (GameUtility.\u003C\u003Ef__mg\u0024cache6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GameUtility.\u003C\u003Ef__mg\u0024cache6 = new Func<UnitData, UnitData, int>(UnitData.CompareTo_Rarity);
        }
        // ISSUE: reference to a compiler-generated field
        Func<UnitData, UnitData, int> fMgCache6 = GameUtility.\u003C\u003Ef__mg\u0024cache6;
        funcList8.Remove(fMgCache6);
        break;
    }
    for (int index = 0; index < units.Count; ++index)
      sortValues[index] = sortKeyValueList[index].mKey;
    int compareResult = 0;
    sortKeyValueList.Sort((Comparison<SortKeyValue<UnitData, int>>) ((x, y) =>
    {
      compareResult = y.mKey - x.mKey;
      if (compareResult != 0)
        return compareResult;
      for (int index = 0; index < compares.Count; ++index)
      {
        compareResult = compares[index](x.mValue, y.mValue);
        if (compareResult != 0)
          return compareResult;
      }
      return UnitData.CompareTo_Iname(x.mValue, y.mValue);
    }));
    if (outputSortedValues)
    {
      for (int index = 0; index < sortKeyValueList.Count; ++index)
      {
        units[index] = sortKeyValueList[index].mValue;
        sortValues[index] = sortKeyValueList[index].mKey;
      }
    }
    else
    {
      for (int index = 0; index < sortKeyValueList.Count; ++index)
        units[index] = sortKeyValueList[index].mValue;
    }
  }

  public static void SortUnits(List<UnitData> units, GameUtility.UnitSortModes type, bool ascending)
  {
    switch (type)
    {
      case GameUtility.UnitSortModes.Level:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.Lv - src.Lv : src.Lv - dsc.Lv;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.JobRank:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.CurrentJob.Rank - src.CurrentJob.Rank : src.CurrentJob.Rank - dsc.CurrentJob.Rank;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.HP:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.hp - (int) src.Status.param.hp : (int) src.Status.param.hp - (int) dsc.Status.param.hp;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Atk:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.atk - (int) src.Status.param.atk : (int) src.Status.param.atk - (int) dsc.Status.param.atk;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Def:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.def - (int) src.Status.param.def : (int) src.Status.param.def - (int) dsc.Status.param.def;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Mag:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.mag - (int) src.Status.param.mag : (int) src.Status.param.mag - (int) dsc.Status.param.mag;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Mnd:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.mnd - (int) src.Status.param.mnd : (int) src.Status.param.mnd - (int) dsc.Status.param.mnd;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Spd:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.spd - (int) src.Status.param.spd : (int) src.Status.param.spd - (int) dsc.Status.param.spd;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Total:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num1 = 0 + (int) src.Status.param.atk + (int) src.Status.param.def + (int) src.Status.param.mag + (int) src.Status.param.mnd + (int) src.Status.param.spd + (int) src.Status.param.dex + (int) src.Status.param.cri + (int) src.Status.param.luk;
          int num2 = 0 + (int) dsc.Status.param.atk + (int) dsc.Status.param.def + (int) dsc.Status.param.mag + (int) dsc.Status.param.mnd + (int) dsc.Status.param.spd + (int) dsc.Status.param.dex + (int) dsc.Status.param.cri + (int) dsc.Status.param.luk;
          int num3 = !ascending ? num2 - num1 : num1 - num2;
          if (num3 == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num3;
        }));
        break;
      case GameUtility.UnitSortModes.Awake:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.AwakeLv - src.AwakeLv : src.AwakeLv - dsc.AwakeLv;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Combination:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.GetCombination() - src.GetCombination() : src.GetCombination() - dsc.GetCombination();
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Rarity:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.Rarity - src.Rarity : src.Rarity - dsc.Rarity;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
    }
  }

  public static void ApplyAvoidance(ref Vector4[] points, int iteration, float aspectRatio)
  {
    bool flag = false;
    for (int index = 0; index < points.Length; ++index)
    {
      if ((double) points[index].w <= 1.0 / 1000.0)
        points[index].w = 1f / 1000f;
      flag |= (double) points[index].z > 0.0;
    }
    if (!flag)
      return;
    float num1 = (double) aspectRatio <= 0.0 ? 0.0f : 1f / aspectRatio;
    for (int index1 = 0; index1 < iteration; ++index1)
    {
      Vector2[] vector2Array = new Vector2[points.Length];
      for (int index2 = 0; index2 < points.Length; ++index2)
      {
        for (int index3 = index2 + 1; index3 < points.Length; ++index3)
        {
          Vector2 vector2 = new Vector2(points[index3].x - points[index2].x, points[index3].y - points[index2].y);
          vector2.y *= num1;
          float num2 = points[index2].z + points[index3].z - vector2.magnitude;
          if ((double) num2 > 0.0)
          {
            vector2.Normalize();
            float num3 = points[index2].w + points[index3].w;
            if ((double) num3 <= 0.0)
              num3 = 1f;
            float num4 = 1f / num3 * num2;
            vector2Array[index2] -= vector2 * (points[index2].w * num4);
            vector2Array[index3] += vector2 * (points[index3].w * num4);
          }
        }
      }
      for (int index2 = 0; index2 < points.Length; ++index2)
      {
        points[index2].x += vector2Array[index2].x;
        points[index2].y += vector2Array[index2].y;
      }
    }
  }

  public static string GetGameObjectPath(GameObject go)
  {
    return go.GetPath((GameObject) null);
  }

  public static Color32 ParseColor(string src)
  {
    Color32 color32 = new Color32();
    string[] strArray = src.Split(',');
    try
    {
      color32.r = (byte) int.Parse(strArray[0]);
      color32.g = (byte) int.Parse(strArray[1]);
      color32.b = (byte) int.Parse(strArray[2]);
      color32.a = strArray.Length < 4 ? byte.MaxValue : (byte) int.Parse(strArray[3]);
    }
    catch (Exception ex)
    {
      color32 = (Color32) Color.clear;
    }
    return color32;
  }

  public static GameUtility.EScene GetCurrentScene()
  {
    if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      GameUtility.mCurrentScene = !PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay ? GameUtility.EScene.BATTLE : GameUtility.EScene.BATTLE_MULTI;
    return GameUtility.mCurrentScene;
  }

  public static void SetCurrentScene()
  {
    GameUtility.EScene escene = GameUtility.EScene.UNKNOWN;
    if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      GameUtility.mCurrentScene = !PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay ? GameUtility.EScene.BATTLE : GameUtility.EScene.BATTLE_MULTI;
    else if ((UnityEngine.Object) GameObject.Find("SRPG_HOME_MULTIPLAY") != (UnityEngine.Object) null)
      escene = GameUtility.EScene.HOME_MULTI;
    else if ((UnityEngine.Object) GameObject.Find("SRPG_HOME_MULTITOWER") != (UnityEngine.Object) null)
      escene = GameUtility.EScene.MULTI_TOWER;
    else if ((UnityEngine.Object) GameObject.Find("SRPG_MAINMENU") != (UnityEngine.Object) null)
      escene = GameUtility.EScene.HOME;
    else if ((UnityEngine.Object) GameObject.Find("SRPG_TITLE") != (UnityEngine.Object) null)
      escene = GameUtility.EScene.TITLE;
    GameUtility.mCurrentScene = escene;
  }

  public static string ComposeJobIconPath(UnitParam unitparam)
  {
    if (unitparam == null)
      return (string) null;
    JobParam job = (JobParam) null;
    if (unitparam.jobsets != null && unitparam.jobsets.Length > 0)
      job = MonoSingleton<GameManager>.Instance.GetJobParam(MonoSingleton<GameManager>.Instance.GetJobSetParam(unitparam.jobsets[0]).job);
    return AssetPath.JobIconSmall(job);
  }

  public static string GetQuestMissionTextID(QuestBonusObjective bonus)
  {
    int result = 0;
    switch (bonus.Type)
    {
      case EMissionType.KillAllEnemy:
        return "sys.BONUS_KILLALL";
      case EMissionType.NoDeath:
        return "sys.BONUS_NODEATH";
      case EMissionType.LimitedTurn:
        return "sys.BONUS_LIMITEDTURN";
      case EMissionType.ComboCount:
        return "sys.BONUS_COMBOCOUNT";
      case EMissionType.MaxSkillCount:
        return int.TryParse(bonus.TypeParam, out result) && result == 0 ? "sys.BONUS_NOSKILL" : "sys.BONUS_MAXSKILLCOUNT";
      case EMissionType.MaxItemCount:
        return int.TryParse(bonus.TypeParam, out result) && result == 0 ? "sys.BONUS_NOITEM" : "sys.BONUS_MAXITEMCOUNT";
      case EMissionType.MaxPartySize:
        return "sys.BONUS_MAXPARTYSIZE";
      case EMissionType.LimitedUnitElement:
        return "sys.BONUS_LIMITELEMENT";
      case EMissionType.LimitedUnitID:
        return "sys.BONUS_LIMITUNIT";
      case EMissionType.NoMercenary:
        return "sys.BONUS_NOMERCENARY";
      case EMissionType.Killstreak:
        return "sys.BONUS_KILLSTREAK";
      case EMissionType.TotalHealHPMax:
        return int.TryParse(bonus.TypeParam, out result) && result == 0 ? "sys.BONUS_NOHEAL" : "sys.BONUS_TOTALHEALMAX";
      case EMissionType.TotalHealHPMin:
        return "sys.BONUS_TOTALHEALMIN";
      case EMissionType.TotalDamagesTakenMax:
        return "sys.BONUS_TOTALDAMAGESTAKENMAX";
      case EMissionType.TotalDamagesTakenMin:
        return "sys.BONUS_TOTALDAMAGESTAKENMIN";
      case EMissionType.TotalDamagesMax:
        return "sys.BONUS_TOTALDAMAGESMAX";
      case EMissionType.TotalDamagesMin:
        return "sys.BONUS_TOTALDAMAGESMIN";
      case EMissionType.LimitedCT:
        return "sys.BONUS_CTMAX";
      case EMissionType.LimitedContinue:
        return int.TryParse(bonus.TypeParam, out result) && result == 0 ? "sys.BONUS_NOCONTINUE" : "sys.BONUS_CONTINUEMAX";
      case EMissionType.NoNpcDeath:
        return "sys.BONUS_NONPCDEATH";
      case EMissionType.TargetKillstreak:
        return "sys.BONUS_TARGETKILLSTREAK";
      case EMissionType.NoTargetDeath:
        return "sys.BONUS_NOTARGETDEATH";
      case EMissionType.BreakObjClashMax:
        return "sys.BONUS_BREAKOBJCLASHMAX";
      case EMissionType.BreakObjClashMin:
        return "sys.BONUS_BREAKOBJCLASHMIN";
      case EMissionType.WithdrawUnit:
        return "sys.BONUS_WITHDRAWUNIT";
      case EMissionType.UseMercenary:
        return "sys.BONUS_USEMERCENARY";
      case EMissionType.LimitedUnitID_MainOnly:
        return "sys.BONUS_LIMITEDUNITID_MAINONLY";
      case EMissionType.MissionAllCompleteAtOnce:
        return "sys.BONUS_MISSIONALLCOMPLETEATONCE";
      case EMissionType.OnlyTargetArtifactType:
        return "sys.BONUS_ONLYTARGETARTIFACTTYPE";
      case EMissionType.OnlyTargetArtifactType_MainOnly:
        return "sys.BONUS_ONLYTARGETARTIFACTTYPE_MAINONLY";
      case EMissionType.OnlyTargetJobs:
        return "sys.BONUS_ONLYTARGETJOBS";
      case EMissionType.OnlyTargetJobs_MainOnly:
        return "sys.BONUS_ONLYTARGETJOBS_MAINONLY";
      case EMissionType.OnlyTargetUnitBirthplace:
        return "sys.BONUS_ONLYTARGETUNITBIRTHPLACE";
      case EMissionType.OnlyTargetUnitBirthplace_MainOnly:
        return "sys.BONUS_ONLYTARGETUNITBIRTHPLACE_MAINONLY";
      case EMissionType.OnlyTargetSex:
        return "sys.BONUS_ONLYTARGETSEX";
      case EMissionType.OnlyTargetSex_MainOnly:
        return "sys.BONUS_ONLYTARGETSEX_MAINONLY";
      case EMissionType.OnlyHeroUnit:
        return "sys.BONUS_ONLYHEROUNIT";
      case EMissionType.OnlyHeroUnit_MainOnly:
        return "sys.BONUS_ONLYHEROUNIT_MAINONLY";
      case EMissionType.Finisher:
        return "sys.BONUS_FINISHER";
      case EMissionType.TotalGetTreasureCount:
        return "sys.BONUS_TOTALGETTREASURECOUNT";
      case EMissionType.KillstreakByUsingTargetItem:
        return "sys.BONUS_KILLSTREAKBYUSINGTARGETITEM";
      case EMissionType.KillstreakByUsingTargetSkill:
        return "sys.BONUS_KILLSTREAKBYUSINGTARGETSKILL";
      case EMissionType.MaxPartySize_IgnoreFriend:
        return "sys.BONUS_MAXPARTYSIZE_IGNOREFRIEND";
      case EMissionType.NoAutoMode:
        return "sys.BONUS_NOAUTOMODE";
      case EMissionType.NoDeath_NoContinue:
        return "sys.BONUS_NODEATH_NOCONTINUE";
      case EMissionType.OnlyTargetUnits:
        return "sys.BONUS_ONLYTARGETUNITS";
      case EMissionType.OnlyTargetUnits_MainOnly:
        return "sys.BONUS_ONLYTARGETUNITS_MAINONLY";
      case EMissionType.LimitedTurn_Leader:
        return "sys.BONUS_LIMITEDTURN_LEADER";
      case EMissionType.NoDeathTargetNpcUnits:
        return "sys.BONUS_NODEATHTARGETNPCUNITS";
      case EMissionType.UseTargetSkill:
        return "sys.BONUS_USETARGETSKILL";
      case EMissionType.TotalKillstreakCount:
        return "sys.BONUS_TOTALKILLSTREAKCOUNT";
      case EMissionType.TotalGetGemCount_Over:
        return "sys.BONUS_TOTALGETGEMCOUNT_OVER";
      case EMissionType.TotalGetGemCount_Less:
        return "sys.BONUS_TOTALGETGEMCOUNT_LESS";
      case EMissionType.TeamPartySizeMax_IncMercenary:
        return "sys.BONUS_TEAMPARTYSIZEMAX_INCMERCENARY";
      case EMissionType.TeamPartySizeMax_NoMercenary:
        return "sys.BONUS_TEAMPARTYSIZEMAX_NOMERCENARY";
      case EMissionType.ChallengeCountMax:
        return "sys.BONUS_CHALLENGECOUNT_MAX";
      case EMissionType.DeathCountMax:
        return "sys.BONUS_DEATHCOUNTMAX";
      case EMissionType.DamageOver:
        return "sys.BONUS_DAMAGE_OVER";
      case EMissionType.SurviveUnit:
        return "sys.BONUS_SURVIVE_UNIT";
      case EMissionType.KillTargetEnemy:
        return "sys.BONUS_KILL_TARGET_ENEMY";
      case EMissionType.TakenDamageLessEqual:
        bool flag = false;
        if (int.TryParse(bonus.TypeParam, out result) && result == 0)
          flag = true;
        return flag ? "sys.BONUS_NO_DAMAGE" : "sys.BONUS_TAKEN_DAMAGE_LESS_EQUAL";
      case EMissionType.TakenDamageGreatorEqual:
        return "sys.BONUS_TAKEN_DAMAGE_GREATOR_EQUAL";
      case EMissionType.LimitedTurnLessEqualPartyOnly:
        return "sys.BONUS_LIMITED_TURN_LESS_EQUAL_PARTY_ONLY";
      case EMissionType.LimitedTurnGreatorEqualPartyOnly:
        return "sys.BONUS_LIMITED_TURN_GREATOR_EQUAL_PARTY_ONLY";
      case EMissionType.KillEnemy:
        return "sys.BONUS_KILL_ENEMY";
      case EMissionType.BreakObj:
        return "sys.BONUS_BREAK_OBJ";
      case EMissionType.LimitedTurnLessEqualPartyAndNPC:
        return "sys.BONUS_LIMITED_TURN_LESS_EQUAL_PARTY_AND_NPC";
      case EMissionType.LimitedTurnGreatorEqualPartyAndNPC:
        return "sys.BONUS_LIMITED_TURN_GREATOR_EQUAL_PARTY_AND_NPC";
      default:
        return bonus.Type.ToString();
    }
  }

  public static string GetQuestMissionProgressTextID(QuestBonusObjective bonus, bool isAchievable)
  {
    if (isAchievable)
      return GameUtility.GetQuestMissionTextID(bonus) + "_PROGRESS_OK";
    return GameUtility.GetQuestMissionTextID(bonus) + "_PROGRESS_NG";
  }

  public static string ComposeQuestBonusObjectiveText(QuestBonusObjective bonus)
  {
    string questMissionTextId = GameUtility.GetQuestMissionTextID(bonus);
    if (!questMissionTextId.StartsWith("sys."))
      return questMissionTextId;
    if (bonus.IsProgressMission())
      questMissionTextId += "_PROGRESS";
    switch (bonus.Type)
    {
      case EMissionType.KillAllEnemy:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.NoDeath:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.LimitedTurn:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.ComboCount:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.MaxSkillCount:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.MaxItemCount:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.MaxPartySize:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedUnitElement:
        List<EElement> eelementList = new List<EElement>();
        string[] strArray1 = bonus.TypeParam.Split(',');
        if (strArray1 != null)
        {
          for (int index = 0; index < strArray1.Length; ++index)
          {
            try
            {
              EElement eelement = (EElement) Enum.Parse(typeof (EElement), strArray1[index], true);
              eelementList.Add(eelement);
            }
            catch (Exception ex)
            {
              return string.Empty;
            }
          }
        }
        string empty1 = string.Empty;
        for (int index = 0; index < eelementList.Count; ++index)
        {
          empty1 += LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) eelementList[index]);
          if (index < eelementList.Count - 1)
            empty1 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty1
        });
      case EMissionType.LimitedUnitID:
        UnitParam unitParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam1 == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) unitParam1.name
        });
      case EMissionType.NoMercenary:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.Killstreak:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalHealHPMax:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalHealHPMin:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalDamagesTakenMax:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalDamagesTakenMin:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalDamagesMax:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalDamagesMin:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedCT:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedContinue:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.NoNpcDeath:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.TargetKillstreak:
        string[] strArray2 = bonus.TypeParam.Split(',');
        string str1 = "XXXX";
        try
        {
          str1 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray2[0].Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        string str2 = strArray2.Length <= 1 ? "X" : strArray2[1].Trim();
        return LocalizedText.Get(questMissionTextId, (object) str1, (object) str2);
      case EMissionType.NoTargetDeath:
        string str3 = "XXXX";
        try
        {
          str3 = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam.Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) str3
        });
      case EMissionType.BreakObjClashMax:
        string str4 = string.Empty;
        string str5 = "1";
        string[] strArray3 = bonus.TypeParam.Split(',');
        if (strArray3 != null)
        {
          if (strArray3.Length >= 1)
            str4 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray3[0].Trim()).name;
          if (strArray3.Length >= 2)
            str5 = strArray3[1].Trim();
        }
        return LocalizedText.Get(questMissionTextId, (object) str4, (object) str5);
      case EMissionType.BreakObjClashMin:
        string str6 = string.Empty;
        string str7 = "1";
        string[] strArray4 = bonus.TypeParam.Split(',');
        if (strArray4 != null)
        {
          if (strArray4.Length >= 1)
            str6 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray4[0].Trim()).name;
          if (strArray4.Length >= 2)
            str7 = strArray4[1].Trim();
        }
        return LocalizedText.Get(questMissionTextId, (object) str6, (object) str7);
      case EMissionType.WithdrawUnit:
        string name = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam).name;
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) name
        });
      case EMissionType.UseMercenary:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.LimitedUnitID_MainOnly:
        UnitParam unitParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam2 == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) unitParam2.name
        });
      case EMissionType.MissionAllCompleteAtOnce:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.OnlyTargetArtifactType:
        string empty2 = string.Empty;
        string[] strArray5 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray5.Length; ++index)
        {
          empty2 += strArray5[index];
          if (strArray5.Length > index + 1)
            empty2 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty2
        });
      case EMissionType.OnlyTargetArtifactType_MainOnly:
        string empty3 = string.Empty;
        string[] strArray6 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray6.Length; ++index)
        {
          empty3 += strArray6[index];
          if (strArray6.Length > index + 1)
            empty3 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty3
        });
      case EMissionType.OnlyTargetJobs:
        string empty4 = string.Empty;
        string[] strArray7 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray7.Length; ++index)
        {
          empty4 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray7[index]).name;
          if (strArray7.Length > index + 1)
            empty4 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty4
        });
      case EMissionType.OnlyTargetJobs_MainOnly:
        string empty5 = string.Empty;
        string[] strArray8 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray8.Length; ++index)
        {
          empty5 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray8[index]).name;
          if (strArray8.Length > index + 1)
            empty5 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty5
        });
      case EMissionType.OnlyTargetUnitBirthplace:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.OnlyTargetUnitBirthplace_MainOnly:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.OnlyTargetSex:
        string str8 = LocalizedText.Get("sys.SEX_" + bonus.TypeParam);
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) str8
        });
      case EMissionType.OnlyTargetSex_MainOnly:
        string str9 = LocalizedText.Get("sys.SEX_" + bonus.TypeParam);
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) str9
        });
      case EMissionType.OnlyHeroUnit:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.OnlyHeroUnit_MainOnly:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.Finisher:
        UnitParam unitParam3 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam3 == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) unitParam3.name
        });
      case EMissionType.TotalGetTreasureCount:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.KillstreakByUsingTargetItem:
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(bonus.TypeParam);
        if (itemParam == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) itemParam.name
        });
      case EMissionType.KillstreakByUsingTargetSkill:
        SkillParam skillParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        if (skillParam1 == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) skillParam1.name
        });
      case EMissionType.MaxPartySize_IgnoreFriend:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.NoAutoMode:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.NoDeath_NoContinue:
        return LocalizedText.Get(questMissionTextId);
      case EMissionType.OnlyTargetUnits:
        string empty6 = string.Empty;
        string[] strArray9 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray9.Length; ++index)
        {
          empty6 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray9[index]).name;
          if (strArray9.Length > index + 1)
            empty6 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty6
        });
      case EMissionType.OnlyTargetUnits_MainOnly:
        string empty7 = string.Empty;
        string[] strArray10 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray10.Length; ++index)
        {
          empty7 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray10[index]).name;
          if (strArray10.Length > index + 1)
            empty7 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty7
        });
      case EMissionType.LimitedTurn_Leader:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.NoDeathTargetNpcUnits:
        string empty8 = string.Empty;
        string[] strArray11 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray11.Length; ++index)
        {
          empty8 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray11[index]).name;
          if (strArray11.Length > index + 1)
            empty8 += "、";
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty8
        });
      case EMissionType.UseTargetSkill:
        SkillParam skillParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        if (skillParam2 == null)
          return LocalizedText.Get(questMissionTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) skillParam2.name
        });
      case EMissionType.TotalKillstreakCount:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalGetGemCount_Over:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TotalGetGemCount_Less:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TeamPartySizeMax_IncMercenary:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TeamPartySizeMax_NoMercenary:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.ChallengeCountMax:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.DeathCountMax:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.DamageOver:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.SurviveUnit:
        string empty9 = string.Empty;
        string[] strArray12 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray12.Length; ++index)
        {
          UnitParam unitParam4 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray12[index]);
          if (unitParam4 != null)
          {
            empty9 += unitParam4.name;
            if (strArray12.Length > index + 1)
              empty9 += "、";
          }
          else
          {
            DebugUtility.LogError(bonus.Type.ToString() + ":指定のユニット「" + strArray12[index] + "」は存在しません。");
            empty9 += strArray12[index];
            if (strArray12.Length > index + 1)
              empty9 += "、";
          }
        }
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) empty9
        });
      case EMissionType.KillTargetEnemy:
        string[] strArray13 = bonus.TypeParam.Split(',');
        if (strArray13 == null)
        {
          DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
          return string.Empty;
        }
        if (strArray13.Length != 2)
        {
          DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
          return string.Empty;
        }
        string empty10 = string.Empty;
        string key = strArray13[0];
        UnitParam unitParam5 = MonoSingleton<GameManager>.Instance.GetUnitParam(key);
        string str10;
        if (unitParam5 != null)
        {
          str10 = empty10 + unitParam5.name;
        }
        else
        {
          DebugUtility.LogError(bonus.Type.ToString() + ":指定のユニット「" + key + "」は存在しません。");
          str10 = empty10 + key;
        }
        string str11 = strArray13[1];
        return LocalizedText.Get(questMissionTextId, (object) str10, (object) str11);
      case EMissionType.TakenDamageLessEqual:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.TakenDamageGreatorEqual:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedTurnLessEqualPartyOnly:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedTurnGreatorEqualPartyOnly:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.KillEnemy:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.BreakObj:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedTurnLessEqualPartyAndNPC:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.LimitedTurnGreatorEqualPartyAndNPC:
        return LocalizedText.Get(questMissionTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      default:
        return bonus.Type.ToString();
    }
  }

  public static string ComposeQuestMissionProgressText(QuestBonusObjective bonus, int missions_val, bool isAchievable)
  {
    string missionProgressTextId = GameUtility.GetQuestMissionProgressTextID(bonus, isAchievable);
    if (!missionProgressTextId.StartsWith("sys."))
      return missionProgressTextId;
    switch (bonus.Type)
    {
      case EMissionType.KillAllEnemy:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.NoDeath:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.LimitedTurn:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.ComboCount:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.MaxSkillCount:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.MaxItemCount:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.MaxPartySize:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedUnitElement:
        List<EElement> eelementList = new List<EElement>();
        string[] strArray1 = bonus.TypeParam.Split(',');
        if (strArray1 != null)
        {
          for (int index = 0; index < strArray1.Length; ++index)
          {
            try
            {
              EElement eelement = (EElement) Enum.Parse(typeof (EElement), strArray1[index], true);
              eelementList.Add(eelement);
            }
            catch (Exception ex)
            {
              return string.Empty;
            }
          }
        }
        string empty1 = string.Empty;
        for (int index = 0; index < eelementList.Count; ++index)
        {
          empty1 += LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) eelementList[index]);
          if (index < eelementList.Count - 1)
            empty1 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty1
        });
      case EMissionType.LimitedUnitID:
        UnitParam unitParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam1 == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) unitParam1.name
        });
      case EMissionType.NoMercenary:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.Killstreak:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalHealHPMax:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalHealHPMin:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalDamagesTakenMax:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalDamagesTakenMin:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalDamagesMax:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalDamagesMin:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedCT:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedContinue:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.NoNpcDeath:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.TargetKillstreak:
        string[] strArray2 = bonus.TypeParam.Split(',');
        string str1 = "XXXX";
        try
        {
          str1 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray2[0].Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        return LocalizedText.Get(missionProgressTextId, (object) str1, (object) missions_val);
      case EMissionType.NoTargetDeath:
        string str2 = "XXXX";
        try
        {
          str2 = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam.Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) str2
        });
      case EMissionType.BreakObjClashMax:
        string str3 = string.Empty;
        string[] strArray3 = bonus.TypeParam.Split(',');
        if (strArray3 != null && strArray3.Length >= 1)
          str3 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray3[0].Trim()).name;
        return LocalizedText.Get(missionProgressTextId, (object) str3, (object) missions_val);
      case EMissionType.BreakObjClashMin:
        string str4 = string.Empty;
        string[] strArray4 = bonus.TypeParam.Split(',');
        if (strArray4 != null && strArray4.Length >= 1)
          str4 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray4[0].Trim()).name;
        return LocalizedText.Get(missionProgressTextId, (object) str4, (object) missions_val);
      case EMissionType.WithdrawUnit:
        string name = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam).name;
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) name
        });
      case EMissionType.UseMercenary:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.LimitedUnitID_MainOnly:
        UnitParam unitParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam2 == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) unitParam2.name
        });
      case EMissionType.MissionAllCompleteAtOnce:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.OnlyTargetArtifactType:
        string empty2 = string.Empty;
        string[] strArray5 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray5.Length; ++index)
        {
          empty2 += strArray5[index];
          if (strArray5.Length > index + 1)
            empty2 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty2
        });
      case EMissionType.OnlyTargetArtifactType_MainOnly:
        string empty3 = string.Empty;
        string[] strArray6 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray6.Length; ++index)
        {
          empty3 += strArray6[index];
          if (strArray6.Length > index + 1)
            empty3 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty3
        });
      case EMissionType.OnlyTargetJobs:
        string empty4 = string.Empty;
        string[] strArray7 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray7.Length; ++index)
        {
          empty4 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray7[index]).name;
          if (strArray7.Length > index + 1)
            empty4 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty4
        });
      case EMissionType.OnlyTargetJobs_MainOnly:
        string empty5 = string.Empty;
        string[] strArray8 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray8.Length; ++index)
        {
          empty5 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray8[index]).name;
          if (strArray8.Length > index + 1)
            empty5 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty5
        });
      case EMissionType.OnlyTargetUnitBirthplace:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.OnlyTargetUnitBirthplace_MainOnly:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.OnlyTargetSex:
        string str5 = LocalizedText.Get("sys.SEX_" + bonus.TypeParam);
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) str5
        });
      case EMissionType.OnlyTargetSex_MainOnly:
        string str6 = LocalizedText.Get("sys.SEX_" + bonus.TypeParam);
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) str6
        });
      case EMissionType.OnlyHeroUnit:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.OnlyHeroUnit_MainOnly:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.Finisher:
        UnitParam unitParam3 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        if (unitParam3 == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) unitParam3.name
        });
      case EMissionType.TotalGetTreasureCount:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.KillstreakByUsingTargetItem:
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(bonus.TypeParam);
        if (itemParam == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) itemParam.name
        });
      case EMissionType.KillstreakByUsingTargetSkill:
        SkillParam skillParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        if (skillParam1 == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) skillParam1.name
        });
      case EMissionType.MaxPartySize_IgnoreFriend:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) bonus.TypeParam
        });
      case EMissionType.NoAutoMode:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.NoDeath_NoContinue:
        return LocalizedText.Get(missionProgressTextId);
      case EMissionType.OnlyTargetUnits:
        string empty6 = string.Empty;
        string[] strArray9 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray9.Length; ++index)
        {
          empty6 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray9[index]).name;
          if (strArray9.Length > index + 1)
            empty6 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty6
        });
      case EMissionType.OnlyTargetUnits_MainOnly:
        string empty7 = string.Empty;
        string[] strArray10 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray10.Length; ++index)
        {
          empty7 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray10[index]).name;
          if (strArray10.Length > index + 1)
            empty7 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty7
        });
      case EMissionType.LimitedTurn_Leader:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.NoDeathTargetNpcUnits:
        string empty8 = string.Empty;
        string[] strArray11 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray11.Length; ++index)
        {
          empty8 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray11[index]).name;
          if (strArray11.Length > index + 1)
            empty8 += "、";
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty8
        });
      case EMissionType.UseTargetSkill:
        SkillParam skillParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        if (skillParam2 == null)
          return LocalizedText.Get(missionProgressTextId, new object[1]
          {
            (object) string.Empty
          });
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) skillParam2.name
        });
      case EMissionType.TotalKillstreakCount:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalGetGemCount_Over:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TotalGetGemCount_Less:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TeamPartySizeMax_IncMercenary:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TeamPartySizeMax_NoMercenary:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.ChallengeCountMax:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.DeathCountMax:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.DamageOver:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.SurviveUnit:
        string empty9 = string.Empty;
        string[] strArray12 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray12.Length; ++index)
        {
          UnitParam unitParam4 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray12[index]);
          if (unitParam4 != null)
          {
            empty9 += unitParam4.name;
            if (strArray12.Length > index + 1)
              empty9 += "、";
          }
          else
          {
            DebugUtility.LogError(bonus.Type.ToString() + ":対象のユニット「" + strArray12[index] + "」は存在しません。");
            empty9 += strArray12[index];
            if (strArray12.Length > index + 1)
              empty9 += "、";
          }
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) empty9
        });
      case EMissionType.KillTargetEnemy:
        string[] strArray13 = bonus.TypeParam.Split(',');
        if (strArray13 == null)
        {
          DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
          return string.Empty;
        }
        if (strArray13.Length != 2)
        {
          DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
          return string.Empty;
        }
        string empty10 = string.Empty;
        string key = strArray13[0];
        UnitParam unitParam5 = MonoSingleton<GameManager>.Instance.GetUnitParam(key);
        string str7;
        if (unitParam5 != null)
        {
          str7 = empty10 + unitParam5.name;
        }
        else
        {
          DebugUtility.LogError(bonus.Type.ToString() + ":指定のユニット「" + key + "」は存在しません。");
          str7 = empty10 + key;
        }
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TakenDamageLessEqual:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.TakenDamageGreatorEqual:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedTurnLessEqualPartyOnly:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedTurnGreatorEqualPartyOnly:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.KillEnemy:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.BreakObj:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedTurnLessEqualPartyAndNPC:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      case EMissionType.LimitedTurnGreatorEqualPartyAndNPC:
        return LocalizedText.Get(missionProgressTextId, new object[1]
        {
          (object) missions_val
        });
      default:
        return bonus.Type.ToString();
    }
  }

  public static string ComposeCharacterQuestMainUnitConditionText(UnitData unit, QuestParam param)
  {
    string str = string.Format(LocalizedText.Get("sys.QUEST_CHARACTER_CONDITION"), (object) unit.UnitParam.name, (object) Mathf.Max(param.EntryConditionCh.ulvmin, 1));
    List<string> questConditionsCh = param.GetEntryQuestConditionsCh(true, true, true);
    List<string> unlockConditions = unit.GetQuestUnlockConditions(param);
    if (unlockConditions != null && unlockConditions.Count >= 1 || questConditionsCh != null && questConditionsCh.Count >= 2)
      str += LocalizedText.Get("sys.QUEST_CHARACTER_CONDITION_OTHER");
    return str;
  }

  public static double InternalToMapHeight(double InternalHeight)
  {
    return InternalHeight * 0.5;
  }

  public static float InternalToMapHeight(float InternalHeight)
  {
    return InternalHeight * 0.5f;
  }

  public static double MapToInternalHeight(double MapHeight)
  {
    return MapHeight * 2.0;
  }

  public static float MapToInternalHeight(float MapHeight)
  {
    return MapHeight * 2f;
  }

  public static Vector3 RaycastGround(Vector3 position)
  {
    RaycastHit hitInfo;
    if (Physics.Raycast(new Vector3(position.x, 1000f, position.z), -Vector3.up, out hitInfo))
      return hitInfo.point;
    position.y = 0.0f;
    return position;
  }

  public static float CalcHeight(float x, float y)
  {
    float y1 = 100f;
    float x1 = Mathf.Floor(x - 0.5f) + 0.5f;
    float z1 = Mathf.Floor(y - 0.5f) + 0.5f;
    float x2 = Mathf.Ceil(x - 0.5f) + 0.5f;
    float z2 = Mathf.Ceil(y - 0.5f) + 0.5f;
    float a1 = 0.0f;
    float b1 = 0.0f;
    float a2 = 0.0f;
    float b2 = 0.0f;
    RaycastHit hitInfo;
    if (Physics.Raycast(new Vector3(x1, y1, z1), -Vector3.up, out hitInfo))
      a1 = hitInfo.point.y;
    if (Physics.Raycast(new Vector3(x2, y1, z1), -Vector3.up, out hitInfo))
      b1 = hitInfo.point.y;
    if (Physics.Raycast(new Vector3(x1, y1, z2), -Vector3.up, out hitInfo))
      a2 = hitInfo.point.y;
    if (Physics.Raycast(new Vector3(x2, y1, z2), -Vector3.up, out hitInfo))
      b2 = hitInfo.point.y;
    float t1 = x - x1;
    float t2 = y - z1;
    return Mathf.Lerp(Mathf.Lerp(a1, b1, t1), Mathf.Lerp(a2, b2, t1), t2);
  }

  public static Vector3 RaycastGround(Vector2 position)
  {
    return GameUtility.RaycastGround(new Vector3(position.x, 0.0f, position.y));
  }

  public static float RaycastGround(float x, float z)
  {
    return GameUtility.RaycastGround(new Vector3(x, 0.0f, z)).y;
  }

  public static float CalcDistance2D(Vector3 a, Vector3 b)
  {
    a -= b;
    a.y = 0.0f;
    return a.magnitude;
  }

  public static float CalcDistance2D(Vector3 a)
  {
    a.y = 0.0f;
    return a.magnitude;
  }

  private static void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (GameUtility.mLogCallbacks == null)
      return;
    GameUtility.mLogCallbacks(logString, stackTrace, type);
  }

  public static void RegisterLogCallback(Application.LogCallback callback)
  {
    if (GameUtility.mLogCallbacks != null)
      GameUtility.mLogCallbacks += callback;
    else
      GameUtility.mLogCallbacks = callback;
    // ISSUE: reference to a compiler-generated field
    if (GameUtility.\u003C\u003Ef__mg\u0024cache8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GameUtility.\u003C\u003Ef__mg\u0024cache8 = new Application.LogCallback(GameUtility.HandleLog);
    }
    // ISSUE: reference to a compiler-generated field
    Application.logMessageReceived += GameUtility.\u003C\u003Ef__mg\u0024cache8;
  }

  public static void UnregisterLogCallback(Application.LogCallback callback)
  {
    GameUtility.mLogCallbacks -= callback;
  }

  public static float EvaluateCurveLooped(AnimationCurve curve, float time)
  {
    if (curve == null || curve.length < 2)
      return 0.0f;
    float num = curve[curve.length - 1].time - curve[0].time;
    if ((double) num <= 0.0)
      return 0.0f;
    time = (time - curve[0].time) % num + curve[0].time;
    return curve.Evaluate(time);
  }

  public static void RemoveDuplicatedMainCamera()
  {
    UnityEngine.Camera main = UnityEngine.Camera.main;
    if ((UnityEngine.Object) main == (UnityEngine.Object) null)
      return;
    UnityEngine.Camera[] objectsOfType = UnityEngine.Object.FindObjectsOfType<UnityEngine.Camera>();
    for (int index = objectsOfType.Length - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) objectsOfType[index] != (UnityEngine.Object) main && objectsOfType[index].CompareTag("MainCamera"))
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) objectsOfType[index].gameObject);
    }
  }

  public static bool IsATCTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mATCTextureSupport;
    }
  }

  public static bool IsDXTTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mDXTTextureSupport;
    }
  }

  public static bool IsPVRTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mPVRTextureSupport;
    }
  }

  public static void PrintGLExtensions()
  {
    GameUtility.CacheGLExtensions();
    for (int index = 0; index < GameUtility.mGLExtensions.Length; ++index)
      Debug.Log((object) GameUtility.mGLExtensions[index]);
  }

  public static bool IsGLExtensionSupported(string extName)
  {
    GameUtility.CacheGLExtensions();
    for (int index = GameUtility.mGLExtensions.Length - 1; index >= 0; --index)
    {
      if (GameUtility.mGLExtensions[index] == extName)
        return true;
    }
    return false;
  }

  private static void CacheGLExtensions()
  {
    if (GameUtility.mGLExtensions != null)
      return;
    GameUtility.mGLExtensions = new string[0];
    GameUtility.mATCTextureSupport = false;
    GameUtility.mDXTTextureSupport = false;
    GameUtility.mPVRTextureSupport = false;
  }

  public static bool ValidateAnimator(Animator animator)
  {
    if ((UnityEngine.Object) animator != (UnityEngine.Object) null)
      return animator.layerCount > 0;
    return false;
  }

  public static void EnableBehaviour<T>(GameObject go, bool enable) where T : Behaviour
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return;
    Behaviour component = (Behaviour) go.GetComponent(typeof (T));
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    component.enabled = enable;
  }

  public static void EnableBehaviour<T>(Component go, bool enable) where T : Behaviour
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return;
    Behaviour component = (Behaviour) go.GetComponent(typeof (T));
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    component.enabled = enable;
  }

  public static T GetArrayElementSafe<T>(T[] list, int index)
  {
    if (list != null && 0 <= index && index < list.Length)
      return list[index];
    return default (T);
  }

  public static bool IsScreenFading
  {
    get
    {
      return FadeController.Instance.IsFading(0);
    }
  }

  public static void FadeIn(float time)
  {
    FadeController.Instance.FadeTo(GameUtility.mFadeInColor, time, 0);
  }

  public static void FadeOut(float time)
  {
    FadeController.Instance.FadeTo(Color.black, time, 0);
  }

  private static int GetLayerIndex(string name)
  {
    int num = LayerMask.NameToLayer(name);
    if (num < 0)
    {
      Debug.LogError((object) ("Layer '" + name + "' not found."));
      num = 0;
    }
    return num;
  }

  private static void InitializeLayerIndices()
  {
    GameUtility.mLayerBG = GameUtility.GetLayerIndex("BG");
    GameUtility.mLayerHidden = GameUtility.GetLayerIndex("HIDDEN");
    GameUtility.mLayerDefault = GameUtility.GetLayerIndex("Default");
    GameUtility.mLayerUI = GameUtility.GetLayerIndex("UI");
    GameUtility.mLayerCH = GameUtility.GetLayerIndex("CH");
    GameUtility.mLayerCH0 = GameUtility.GetLayerIndex("CH0");
    GameUtility.mLayerCH1 = GameUtility.GetLayerIndex("CH1");
    GameUtility.mLayerCH2 = GameUtility.GetLayerIndex("CH2");
    GameUtility.mLayerCHBG0 = GameUtility.GetLayerIndex("CHBG0");
    GameUtility.mLayerCHBG1 = GameUtility.GetLayerIndex("CHBG1");
    GameUtility.mLayerCHBG2 = GameUtility.GetLayerIndex("CHBG2");
    GameUtility.mLayerEffect = GameUtility.GetLayerIndex("EFFECT");
    GameUtility.mLayerIndexInitialized = true;
  }

  public static int LayerDefault
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerDefault;
    }
  }

  public static int LayerBG
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerBG;
    }
  }

  public static int LayerCH
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH;
    }
  }

  public static int LayerHidden
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerHidden;
    }
  }

  public static int LayerUI
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerUI;
    }
  }

  public static int LayerEffect
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerEffect;
    }
  }

  public static int LayerCH0
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH0;
    }
  }

  public static int LayerCH1
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH1;
    }
  }

  public static int LayerCH2
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH2;
    }
  }

  public static int LayerCHBG0
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG0;
    }
  }

  public static int LayerCHBG1
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG1;
    }
  }

  public static int LayerCHBG2
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG2;
    }
  }

  public static int LayerMaskEffect
  {
    get
    {
      return 1 << GameUtility.LayerEffect;
    }
  }

  public static int LayerMaskCH
  {
    get
    {
      return 1 << GameUtility.LayerCH;
    }
  }

  public static int LayerMaskBG
  {
    get
    {
      return 1 << GameUtility.LayerBG;
    }
  }

  public static int LayerMaskHidden
  {
    get
    {
      return 1 << GameUtility.LayerHidden;
    }
  }

  public static int LayerMaskUI
  {
    get
    {
      return 1 << GameUtility.LayerUI;
    }
  }

  public static Texture2D TransparentTexture
  {
    get
    {
      if ((UnityEngine.Object) GameUtility.mTransparentTexture == (UnityEngine.Object) null)
      {
        GameUtility.mTransparentTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        GameUtility.mTransparentTexture.SetPixel(0, 0, Color.clear);
        GameUtility.mTransparentTexture.Apply();
      }
      return GameUtility.mTransparentTexture;
    }
  }

  public static void DeactivateActiveChildComponents<T>(Component parent)
  {
    Component[] componentsInChildren = parent.GetComponentsInChildren(typeof (T));
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].gameObject.activeSelf)
        componentsInChildren[index].gameObject.SetActive(false);
    }
  }

  public static Vector3 DeformPosition(Vector3 pos, float zOffset)
  {
    float f = (pos.z - zOffset) * GameUtility._deformRadiusLength.y;
    pos.y += GameUtility._deformRadiusLength.x;
    pos.z = 0.0f;
    float num = Mathf.Cos(f);
    float y = Mathf.Sin(f);
    Vector3 rhs1 = new Vector3(0.0f, num, -y);
    Vector3 rhs2 = new Vector3(0.0f, y, num);
    return new Vector3(pos.x, Vector3.Dot(pos, rhs1) - GameUtility._deformRadiusLength.x, Vector3.Dot(pos, rhs2) + zOffset);
  }

  public static T GetComponentInAllChildren<T>(GameObject go) where T : Component
  {
    Component[] componentsInChildren = go.GetComponentsInChildren(typeof (T), true);
    if (componentsInChildren.Length > 0)
      return (T) componentsInChildren[0];
    return (T) null;
  }

  public static void SetLayer(GameObject go, int layer, bool changeChildren)
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return;
    if (changeChildren)
    {
      foreach (Component componentsInChild in go.GetComponentsInChildren<Transform>(true))
        componentsInChild.gameObject.layer = layer;
    }
    else
      go.gameObject.layer = layer;
  }

  public static void SetLayer(Component go, int layer, bool changeChildren)
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return;
    GameUtility.SetLayer(go.gameObject, layer, changeChildren);
  }

  public static void SetGameObjectActive(GameObject obj, bool active)
  {
    if (!((UnityEngine.Object) obj != (UnityEngine.Object) null))
      return;
    obj.SetActive(active);
  }

  public static void SetGameObjectActive(Component go, bool active)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    go.gameObject.SetActive(active);
  }

  public static void DestroyGameObjects(GameObject[] list)
  {
    for (int index = 0; index < list.Length; ++index)
      GameUtility.DestroyGameObject(list[index]);
  }

  public static void DestroyGameObjects<T>(T[] list) where T : Component
  {
    for (int index = 0; index < list.Length; ++index)
      GameUtility.DestroyGameObject((Component) list[index]);
  }

  public static void DestroyGameObjects(List<GameObject> list)
  {
    for (int index = 0; index < list.Count; ++index)
      GameUtility.DestroyGameObject(list[index]);
  }

  public static void DestroyGameObjects<T>(List<T> list) where T : Component
  {
    for (int index = 0; index < list.Count; ++index)
      GameUtility.DestroyGameObject((Component) list[index]);
  }

  public static void DestroyGameObject(GameObject go)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) go.gameObject);
  }

  public static void DestroyGameObject(Component go)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    GameUtility.DestroyGameObject(go.gameObject);
  }

  public static bool IsAnimatorRunning(GameObject go)
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return false;
    Animator component = go.GetComponent<Animator>();
    if ((UnityEngine.Object) component == (UnityEngine.Object) null)
      return false;
    AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
    if (animatorStateInfo.loop)
      return true;
    if (component.IsInTransition(0))
      return false;
    return (double) animatorStateInfo.normalizedTime < 1.0;
  }

  public static bool IsAnimatorRunning(Component go)
  {
    return GameUtility.IsAnimatorRunning(go.gameObject);
  }

  public static bool CompareAnimatorStateName(GameObject go, string stateName)
  {
    return go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(stateName);
  }

  public static bool CompareAnimatorStateName(Component go, string stateName)
  {
    return GameUtility.CompareAnimatorStateName(go.gameObject, stateName);
  }

  public static void SetAnimatorTrigger(GameObject go, string trigger)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    Animator component = go.GetComponent<Animator>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    component.SetTrigger(trigger);
  }

  public static void SetAnimatorTrigger(Component go, string trigger)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    GameUtility.SetAnimatorTrigger(go.gameObject, trigger);
  }

  public static void SetAnimatorBool(Component go, string name, bool value)
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return;
    Animator component = go.GetComponent<Animator>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    component.SetBool(name, value);
  }

  public static void RemoveComponent<T>(GameObject go) where T : Component
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return;
    Component component;
    while ((UnityEngine.Object) (component = go.GetComponent(typeof (T))) != (UnityEngine.Object) null)
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) component);
  }

  public static T RequireComponent<T>(GameObject go) where T : Component
  {
    if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      return (T) null;
    T obj = go.GetComponent<T>();
    if ((UnityEngine.Object) obj == (UnityEngine.Object) null)
      obj = go.AddComponent<T>();
    return obj;
  }

  public static GameObject SpawnParticle(GameObject prefab, Vector3 position, Quaternion rotation, GameObject parentScene)
  {
    if ((UnityEngine.Object) prefab == (UnityEngine.Object) null)
      return (GameObject) null;
    GameObject go = UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation);
    go.RequireComponent<OneShotParticle>();
    if ((UnityEngine.Object) parentScene != (UnityEngine.Object) null)
      go.transform.SetParent(parentScene.transform, true);
    return go;
  }

  public static GameObject SpawnParticle(GameObject prefab, Transform position, GameObject parentScene)
  {
    return GameUtility.SpawnParticle(prefab, position.position, position.rotation, parentScene);
  }

  public static void StopEmitters(GameObject obj)
  {
    ParticleSystem[] componentsInChildren = obj.GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].emission.enabled = false;
      componentsInChildren[index].main.loop = false;
    }
  }

  public static LoadRequest LoadResourceAsyncChecked(string path)
  {
    LoadRequest loadRequest = AssetManager.LoadAsync(path);
    if (loadRequest.isDone && loadRequest.asset == (UnityEngine.Object) null)
      Debug.LogError((object) ("Failed to load resource [" + path + "]"));
    return loadRequest;
  }

  public static LoadRequest LoadResourceAsyncChecked<T>(string path)
  {
    LoadRequest loadRequest = AssetManager.LoadAsync(path, typeof (T));
    if (loadRequest.isDone && loadRequest.asset == (UnityEngine.Object) null)
      Debug.LogError((object) ("Failed to load resource [" + path + "]"));
    return loadRequest;
  }

  public static LoadRequest LoadResourceAsync(string path)
  {
    return AssetManager.LoadAsync(path);
  }

  public static LoadRequest LoadResourceAsync<T>(string path)
  {
    return AssetManager.LoadAsync(path, typeof (T));
  }

  public static void RequestScene(string name)
  {
    SceneManager.LoadScene(name);
  }

  public static void SetAnimationClip(Animation animation, AnimationClip clip)
  {
    if ((UnityEngine.Object) animation.GetClip(clip.name) != (UnityEngine.Object) null)
      animation.RemoveClip(clip.name);
    animation.AddClip(clip, clip.name);
  }

  public static void Reparent(Transform child, Transform newParent)
  {
    child.SetParent(newParent, false);
  }

  public static string GetPlatformID()
  {
    switch (Application.platform)
    {
      case RuntimePlatform.OSXEditor:
      case RuntimePlatform.OSXPlayer:
        return "mac";
      case RuntimePlatform.WindowsPlayer:
      case RuntimePlatform.WindowsEditor:
        return "win";
      case RuntimePlatform.IPhonePlayer:
        return "ios";
      case RuntimePlatform.Android:
        return "android";
      default:
        throw new Exception("Unknown Platform:" + Application.platform.ToString());
    }
  }

  public static void CopyTransform(Transform dest, Transform src)
  {
    dest.position = src.position;
    dest.rotation = src.rotation;
  }

  public static void InterpTransform(Transform tr, Transform start, Transform end, float t)
  {
    tr.position = Vector3.Lerp(start.position, end.position, t);
    tr.rotation = Quaternion.Slerp(start.rotation, end.rotation, t);
  }

  public static RectTransform GetRectTransform(GameObject obj)
  {
    return (RectTransform) obj.transform;
  }

  public static RectTransform GetRectTransform(Component obj)
  {
    return (RectTransform) obj.gameObject.transform;
  }

  public static Transform findChildRecursively(Transform parent, string name)
  {
    for (int index = 0; index < parent.childCount; ++index)
    {
      Transform parent1 = parent.GetChild(index);
      if (parent1.name == name || (UnityEngine.Object) (parent1 = GameUtility.findChildRecursively(parent1, name)) != (UnityEngine.Object) null)
        return parent1;
    }
    return (Transform) null;
  }

  public static void swap<T>(ref T a, ref T b)
  {
    T obj = a;
    a = b;
    b = obj;
  }

  public static float GUIScale
  {
    get
    {
      return (float) Screen.width / 960f;
    }
  }

  public static float ScaledScreenWidth
  {
    get
    {
      return (float) Screen.width / GameUtility.GUIScale;
    }
  }

  public static float ScaledScreenHeight
  {
    get
    {
      return (float) Screen.height / GameUtility.GUIScale;
    }
  }

  public static void ScaleGUI(float scale = 1f)
  {
    Matrix4x4 identity = Matrix4x4.identity;
    identity.m00 = identity.m11 = GameUtility.GUIScale * scale;
    GUI.matrix = identity;
  }

  public static string CalcResourceSizeSum<T>()
  {
    return "not available";
  }

  public static string CreateUUID()
  {
    Random random = new Random();
    string empty = string.Empty;
    for (int index = 0; index < 32; ++index)
    {
      int num1 = random.Next(0, 16);
      if (index == 8 || index == 12 || (index == 16 || index == 20))
        empty += "-";
      switch (index)
      {
        case 12:
          empty += Convert.ToString(4, 16);
          break;
        case 16:
          int num2 = num1 & 3 | 8;
          empty += Convert.ToString(num2, 16);
          break;
        default:
          empty += Convert.ToString(num1, 16);
          break;
      }
    }
    return empty;
  }

  public static DateTime UnixtimeToLocalTime(long unixtime)
  {
    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) unixtime).ToLocalTime();
  }

  public static void SetNeverSleep()
  {
    ++GameUtility.mNeverSleepCount;
    GameUtility.UpdateSetting();
  }

  public static void SetDefaultSleepSetting()
  {
    GameUtility.mNeverSleepCount = GameUtility.mNeverSleepCount <= 1 ? 0 : --GameUtility.mNeverSleepCount;
    GameUtility.UpdateSetting();
  }

  private static void UpdateSetting()
  {
    if (GameUtility.mNeverSleepCount > 0)
      Screen.sleepTimeout = -1;
    else
      Screen.sleepTimeout = -2;
  }

  public static void ForceSetDefaultSleepSetting()
  {
    GameUtility.mNeverSleepCount = 0;
    Screen.sleepTimeout = -2;
  }

  public static byte[] Object2Binary<T>(T obj)
  {
    if ((object) obj == null)
      return (byte[]) null;
    if (GameUtility.Packer == null)
      return (byte[]) null;
    return GameUtility.Packer.Pack((object) obj);
  }

  public static bool Binary2Object<T>(out T buffer, byte[] data)
  {
    if (data != null)
    {
      if (GameUtility.Packer != null)
      {
        try
        {
          buffer = GameUtility.Packer.Unpack<T>(data);
          return true;
        }
        catch (Exception ex)
        {
          DebugUtility.Log(ex.Message);
          buffer = default (T);
          return false;
        }
      }
    }
    buffer = default (T);
    return false;
  }

  public static string HalfNum2FullNum(string str)
  {
    string empty = string.Empty;
    return Regex.Replace(str, "[0-9]", (MatchEvaluator) (m => ((char) (65296 + ((int) m.Value[0] - 48))).ToString()));
  }

  public static string GetExternalLocalizedText(string sheetName, string iname, string columnName)
  {
    sheetName = sheetName.ToLower();
    iname = iname.ToUpper();
    columnName = columnName.ToUpper();
    return LocalizedText.Get(string.Format("external_{0}.{1}_{2}", (object) sheetName, (object) iname, (object) columnName));
  }

  public static T GetCustomAttribute<T>(object target, bool inherit) where T : Attribute
  {
    MemberInfo memberInfo;
    if (target is System.Type)
      memberInfo = (MemberInfo) (System.Type) target;
    else if (target.GetType().IsEnum)
    {
      memberInfo = (MemberInfo) target.GetType().GetField(target.ToString());
    }
    else
    {
      if (!target.GetType().IsClass)
        return (T) null;
      memberInfo = (MemberInfo) target.GetType();
    }
    if (memberInfo == null)
      return (T) null;
    T[] customAttributes = (T[]) memberInfo.GetCustomAttributes(typeof (T), inherit);
    if (customAttributes.Length < 1)
      return (T) null;
    return customAttributes[0];
  }

  public static string ByteSizeToString(long byteSize, bool forceMB, bool ceiling = true)
  {
    string empty = string.Empty;
    string str;
    double a;
    if (forceMB)
    {
      str = "MB";
      a = (double) byteSize / 1024.0 / 1024.0;
    }
    else if (byteSize < 1024L)
    {
      str = "B";
      a = (double) byteSize;
    }
    else if (byteSize < 1048576L)
    {
      str = "KB";
      a = (double) byteSize / 1024.0;
    }
    else if (byteSize < 1073741824L)
    {
      str = "MB";
      a = (double) byteSize / 1024.0 / 1024.0;
    }
    else
    {
      str = "GB";
      a = (double) byteSize / 1024.0 / 1024.0 / 1024.0;
    }
    return (!ceiling ? Math.Round(a, 2, MidpointRounding.AwayFromZero) : Math.Ceiling(a)).ToString() + str;
  }

  public class BooleanConfig
  {
    private string mKey;
    private bool mDefaultValue;

    public BooleanConfig(string configName, bool defaultValue)
    {
      this.mKey = configName;
      this.mDefaultValue = defaultValue;
    }

    public bool Value
    {
      get
      {
        if (!PlayerPrefsUtility.HasKey(this.mKey))
          return this.mDefaultValue;
        return PlayerPrefsUtility.GetInt(this.mKey, 0) != 0;
      }
      set
      {
        PlayerPrefsUtility.SetInt(this.mKey, !value ? 0 : 1, false);
      }
    }
  }

  private struct UnitShowSetting
  {
    public string key;
    public int on;
  }

  public enum UnitShowSettingTypes
  {
    FILTER_FIRE,
    FILTER_WATER,
    FILTER_WIND,
    FILTER_THUNDER,
    FILTER_SHINE,
    FILTER_DARK,
    FILTER_ZENEI,
    FILTER_TYUEI,
    FILTER_KOUEI,
    FILTER_RARE1,
    FILTER_RARE2,
    FILTER_RARE3,
    FILTER_RARE4,
    FILTER_RARE5,
    FILTER_RARE6,
    SORT_SYOJUN,
    SORT_KOUJUN,
    SORT_LEVEL,
    SORT_FAVORITE,
    SORT_JOBRANK,
    SORT_HP,
    SORT_ATK,
    SORT_DEF,
    SORT_MAG,
    SORT_MND,
    SORT_SPD,
    SORT_TOTAL,
    SORT_AWAKE,
    SORT_COMBINATION,
  }

  public enum UnitSortModes
  {
    Time,
    Level,
    JobRank,
    HP,
    Atk,
    Def,
    Mag,
    Mnd,
    Rec,
    Spd,
    Total,
    Awake,
    Combination,
    Rarity,
    Name,
  }

  public enum EScene
  {
    UNKNOWN,
    BATTLE,
    BATTLE_MULTI,
    HOME,
    HOME_MULTI,
    TITLE,
    MULTI_TOWER,
  }
}