// Decompiled with JetBrains decompiler
// Type: BattleSpeedController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BattleSpeedController : MonoBehaviour
{
  public const float MIN_SPEED = 1f;
  public const float MAX_SPEED = 2f;
  public static readonly float[] SPEED_LIST = new float[2]
  {
    1f,
    2f
  };
  public const int DEFAULT_SPEED_NUM = 0;
  private const string IS_ENABLE_SPEED_CONTROLL = "IS_ENABLE_SPEED_CONTROLL";
  private const string BATTLE_SPEED_KEY = "BATTLE_SPEED_KEY";
  public static string REFLESH_KEY = "ON_BATTLE_SPEED_REFLESH";
  private static GameUtility.BooleanConfig isEnableSpeedControll = new GameUtility.BooleanConfig(nameof (IS_ENABLE_SPEED_CONTROLL), false);
  private static float speed = BattleSpeedController.SPEED_LIST[0];
  private static bool isEnd;
  private static float[] enableSpeedlist;
  private const string VIP_SPEED_AUTO_ONLY = "VIP_SPEED_AUTO_ONLY";

  public static bool IsEnableSpeedControll => BattleSpeedController.isEnableSpeedControll.Value;

  public static float PrefsSpeed
  {
    get
    {
      float num = PlayerPrefsUtility.GetFloat("BATTLE_SPEED_KEY", BattleSpeedController.SPEED_LIST[0]);
      if (BattleSpeedController.enableSpeedlist == null)
        BattleSpeedController.enableSpeedlist = BattleSpeedController.CreateBattleSpeedList();
      return Array.IndexOf<float>(BattleSpeedController.enableSpeedlist, num) < 0 ? 2f : num;
    }
  }

  public static float CurrentSpeed => BattleSpeedController.speed;

  public static float[] EnableSpeedList
  {
    get
    {
      if (BattleSpeedController.enableSpeedlist == null)
        BattleSpeedController.enableSpeedlist = BattleSpeedController.SPEED_LIST;
      return BattleSpeedController.enableSpeedlist;
    }
  }

  public static bool isPremium => true;

  public static bool isAutoOnly => FlowNode_Variable.Get("VIP_SPEED_AUTO_ONLY") == "TRUE";

  public static bool BattleTimeConfig { get; set; }

  public static void SetUp()
  {
    BattleSpeedController.isEnd = false;
    BattleSpeedController.CheckEnableSpeedControll();
    SceneBattle instance = SceneBattle.Instance;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentQuest != null && (instance.CurrentQuest.type == QuestTypes.Multi || instance.CurrentQuest.type == QuestTypes.MultiGps || instance.CurrentQuest.type == QuestTypes.MultiTower))
      BattleSpeedController.BattleTimeConfig = GlobalVars.SelectedMultiPlayHiSpeed;
    if (!BattleSpeedController.IsEnableSpeedControll || !BattleSpeedController.BattleTimeConfig)
    {
      if (!BattleSpeedController.isPremium)
        GameUtility.Config_UseBattleSpeed.Value = false;
      GlobalEvent.Invoke(BattleSpeedController.REFLESH_KEY, (object) null);
    }
    else
    {
      BattleSpeedController.SetSpeed(!MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring ? BattleSpeedController.PrefsSpeed : 2f);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
      {
        SceneBattle sceneBattle = instance;
        SceneBattle.QuestEndEvent onQuestEnd = sceneBattle.OnQuestEnd;
        // ISSUE: reference to a compiler-generated field
        if (BattleSpeedController.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BattleSpeedController.\u003C\u003Ef__mg\u0024cache0 = new SceneBattle.QuestEndEvent(BattleSpeedController.EndBattle);
        }
        // ISSUE: reference to a compiler-generated field
        SceneBattle.QuestEndEvent fMgCache0 = BattleSpeedController.\u003C\u003Ef__mg\u0024cache0;
        sceneBattle.OnQuestEnd = onQuestEnd + fMgCache0;
      }
      GlobalEvent.Invoke(BattleSpeedController.REFLESH_KEY, (object) null);
    }
  }

  public static bool CheckEnableSpeedControll()
  {
    bool flag1 = true;
    if (BattleSpeedController.isAutoOnly && UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null && !SceneBattle.Instance.Battle.RequestAutoBattle)
      flag1 = false;
    if (!BattleSpeedController.isPremium)
      flag1 = false;
    bool flag2 = flag1;
    BattleSpeedController.isEnableSpeedControll.Value = flag2;
    return flag2;
  }

  public static void ChangeNextSpeed(bool isChangeBefore = false)
  {
    if (!BattleSpeedController.CheckEnableSpeedControll() || !BattleSpeedController.BattleTimeConfig || !BattleSpeedController.IsChangeSpeed())
      return;
    int speedListNum = BattleSpeedController.FindSpeedListNum(BattleSpeedController.speed);
    int index = BattleSpeedController.WrapAroundSpeedNum(!isChangeBefore ? speedListNum + 1 : speedListNum - 1);
    BattleSpeedController.speed = BattleSpeedController.SPEED_LIST[index];
    BattleSpeedController.SetStartSpeed(BattleSpeedController.speed);
    TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.BattleSpeed, BattleSpeedController.speed);
  }

  public static int FindSpeedListNum(float inSpeed)
  {
    for (int speedListNum = 0; speedListNum < BattleSpeedController.SPEED_LIST.Length; ++speedListNum)
    {
      if ((double) BattleSpeedController.SPEED_LIST[speedListNum] == (double) inSpeed)
        return speedListNum;
    }
    return 0;
  }

  public static void OnContinue() => BattleSpeedController.SetUp();

  public static void SetSpeed(float inSpeed)
  {
    if (!BattleSpeedController.CheckEnableSpeedControll() || !BattleSpeedController.BattleTimeConfig)
      return;
    SceneBattle instance = SceneBattle.Instance;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentQuest != null && (instance.CurrentQuest.type == QuestTypes.Multi || instance.CurrentQuest.type == QuestTypes.MultiGps || instance.CurrentQuest.type == QuestTypes.MultiTower))
      inSpeed = !BattleSpeedController.BattleTimeConfig ? 1f : (float) GlobalVars.SelectedMultiPlayBtlSpeed;
    else if (!BattleSpeedController.IsChangeSpeed())
      return;
    BattleSpeedController.speed = inSpeed;
    BattleSpeedController.speed = BattleSpeedController.SaveLimitSpeed(BattleSpeedController.speed);
    TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.BattleSpeed, BattleSpeedController.speed);
  }

  public static void SetStartSpeed(float inSpeed)
  {
    PlayerPrefsUtility.SetFloat("BATTLE_SPEED_KEY", BattleSpeedController.SaveLimitSpeed(inSpeed));
  }

  public static void ResetSpeed()
  {
    BattleSpeedController.speed = BattleSpeedController.SPEED_LIST[0];
    TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.BattleSpeed, BattleSpeedController.speed);
    BattleSpeedController.CheckEnableSpeedControll();
  }

  public static void EndBattle()
  {
    BattleSpeedController.ResetSpeed();
    BattleSpeedController.isEnd = true;
  }

  private static bool IsChangeSpeed()
  {
    SceneBattle instance = SceneBattle.Instance;
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && BattleSpeedController.isEnableSpeedControll.Value && BattleSpeedController.CheckAllowedSpeedBattle(instance.CurrentQuest) && instance.QuestStart && !BattleSpeedController.isEnd;
  }

  public static bool IsShowSpeedButton()
  {
    SceneBattle instance = SceneBattle.Instance;
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && BattleSpeedController.CheckAllowedSpeedBattle(instance.CurrentQuest);
  }

  public static bool CheckAllowedSpeedBattle(QuestParam quest)
  {
    if (BattleSpeedController.isAutoOnly)
    {
      if (quest.CheckAllowedAutoBattle())
        return true;
    }
    else
    {
      switch (quest.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Arena:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
        case QuestTypes.UnitRental:
        case QuestTypes.GuildRaid:
        case QuestTypes.GvG:
        case QuestTypes.WorldRaid:
          return true;
      }
    }
    return false;
  }

  private static int WrapAroundSpeedNum(int num)
  {
    if (num >= BattleSpeedController.SPEED_LIST.Length)
      return num = 0;
    return num < 0 ? (num = BattleSpeedController.SPEED_LIST.Length - 1) : num;
  }

  public static float SaveLimitSpeed(float inSpeed)
  {
    float[] battleSpeedList = BattleSpeedController.CreateBattleSpeedList();
    float num1 = battleSpeedList[battleSpeedList.Length - 1];
    float num2 = battleSpeedList[0];
    if ((double) inSpeed > (double) num1)
      return num1;
    return (double) inSpeed < (double) num2 ? num2 : inSpeed;
  }

  public static float[] CreateBattleSpeedList()
  {
    List<float> floatList = new List<float>((IEnumerable<float>) BattleSpeedController.SPEED_LIST);
    ExpansionPurchaseParam[] expansionPurchaseParams = MonoSingleton<GameManager>.Instance.MasterParam.GetExpansionPurchaseParams(ExpansionPurchaseParam.eExpansionType.TripleSpeed);
    if (expansionPurchaseParams != null)
    {
      for (int index = 0; index < expansionPurchaseParams.Length; ++index)
      {
        if (expansionPurchaseParams[index] != null && !floatList.Contains((float) expansionPurchaseParams[index].Value))
          floatList.Add((float) expansionPurchaseParams[index].Value);
      }
    }
    return floatList.ToArray();
  }

  public static void SetupEnableBattleSpeedList(ExpansionPurchaseData[] expansions)
  {
    List<string> stringList = new List<string>();
    if (expansions != null)
    {
      for (int index = 0; index < expansions.Length; ++index)
      {
        if (expansions[index] != null && expansions[index].param != null && expansions[index].param.ExpansionType == ExpansionPurchaseParam.eExpansionType.TripleSpeed && !stringList.Contains(expansions[index].param.Iname))
          stringList.Add(expansions[index].param.Iname);
      }
    }
    BattleSpeedController.SetupEnableBattleSpeedList(stringList.ToArray());
  }

  public static void SetupEnableBattleSpeedList(string[] expansions)
  {
    List<float> floatList = new List<float>((IEnumerable<float>) BattleSpeedController.SPEED_LIST);
    if (expansions != null && expansions.Length > 0)
    {
      List<ExpansionPurchaseParam> expansionPurchaseParamList = new List<ExpansionPurchaseParam>((IEnumerable<ExpansionPurchaseParam>) MonoSingleton<GameManager>.Instance.MasterParam.GetExpansionPurchaseParams(ExpansionPurchaseParam.eExpansionType.TripleSpeed));
      if (expansionPurchaseParamList != null)
      {
        for (int i = 0; i < expansions.Length; ++i)
        {
          ExpansionPurchaseParam expansionPurchaseParam = expansionPurchaseParamList.Find((Predicate<ExpansionPurchaseParam>) (p => p.Iname == expansions[i]));
          if (expansionPurchaseParam != null && !floatList.Contains((float) expansionPurchaseParam.Value))
            floatList.Add((float) expansionPurchaseParam.Value);
        }
      }
    }
    BattleSpeedController.enableSpeedlist = floatList.ToArray();
  }
}
