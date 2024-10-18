// Decompiled with JetBrains decompiler
// Type: BattleSpeedController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class BattleSpeedController : MonoBehaviour
{
  public static readonly float[] SPEED_LIST = new float[2]
  {
    1f,
    2f
  };
  public static string REFLESH_KEY = "ON_BATTLE_SPEED_REFLESH";
  private static GameUtility.BooleanConfig isEnableSpeedControll = new GameUtility.BooleanConfig(nameof (IS_ENABLE_SPEED_CONTROLL), false);
  private static float speed = BattleSpeedController.SPEED_LIST[0];
  public const float MIN_SPEED = 1f;
  public const float MAX_SPEED = 2f;
  public const int DEFAULT_SPEED_NUM = 0;
  private const string IS_ENABLE_SPEED_CONTROLL = "IS_ENABLE_SPEED_CONTROLL";
  private const string BATTLE_SPEED_KEY = "BATTLE_SPEED_KEY";
  private static bool isEnd;
  private const string VIP_SPEED_AUTO_ONLY = "VIP_SPEED_AUTO_ONLY";

  public static bool IsEnableSpeedControll
  {
    get
    {
      return BattleSpeedController.isEnableSpeedControll.Value;
    }
  }

  public static float PrefsSpeed
  {
    get
    {
      return PlayerPrefsUtility.GetFloat("BATTLE_SPEED_KEY", BattleSpeedController.SPEED_LIST[0]);
    }
  }

  public static float CurrentSpeed
  {
    get
    {
      return BattleSpeedController.speed;
    }
  }

  public static bool isPremium
  {
    get
    {
      return true;
    }
  }

  public static bool isAutoOnly
  {
    get
    {
      return FlowNode_Variable.Get("VIP_SPEED_AUTO_ONLY") == "TRUE";
    }
  }

  public static bool BattleTimeConfig { get; set; }

  public static void SetUp()
  {
    BattleSpeedController.isEnd = false;
    BattleSpeedController.SetStartSpeed(2f);
    BattleSpeedController.CheckEnableSpeedControll();
    SceneBattle instance = SceneBattle.Instance;
    if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.CurrentQuest != null && instance.CurrentQuest.type == QuestTypes.Multi)
      BattleSpeedController.BattleTimeConfig = GlobalVars.SelectedMultiPlayHiSpeed;
    if (!BattleSpeedController.IsEnableSpeedControll || !BattleSpeedController.BattleTimeConfig)
    {
      if (!BattleSpeedController.isPremium)
        GameUtility.Config_UseBattleSpeed.Value = false;
      GlobalEvent.Invoke(BattleSpeedController.REFLESH_KEY, (object) null);
    }
    else
    {
      BattleSpeedController.SetSpeed(BattleSpeedController.PrefsSpeed);
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
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
    if (BattleSpeedController.isAutoOnly && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && (SceneBattle.Instance.Battle != null && !SceneBattle.Instance.Battle.RequestAutoBattle))
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
    for (int index = 0; index < BattleSpeedController.SPEED_LIST.Length; ++index)
    {
      if ((double) BattleSpeedController.SPEED_LIST[index] == (double) inSpeed)
        return index;
    }
    return 0;
  }

  public static void OnContinue()
  {
    BattleSpeedController.SetUp();
  }

  public static void SetSpeed(float inSpeed)
  {
    if (!BattleSpeedController.CheckEnableSpeedControll() || !BattleSpeedController.BattleTimeConfig)
      return;
    SceneBattle instance = SceneBattle.Instance;
    if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.CurrentQuest != null && instance.CurrentQuest.type == QuestTypes.Multi)
      inSpeed = !BattleSpeedController.BattleTimeConfig ? 1f : 2f;
    else if (!BattleSpeedController.IsChangeSpeed())
      return;
    BattleSpeedController.speed = inSpeed;
    BattleSpeedController.speed = BattleSpeedController.SaveLimitSpeed(BattleSpeedController.speed);
    TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.BattleSpeed, BattleSpeedController.speed);
  }

  public static void SetStartSpeed(float inSpeed)
  {
    PlayerPrefsUtility.SetFloat("BATTLE_SPEED_KEY", BattleSpeedController.SaveLimitSpeed(inSpeed), false);
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
    return (UnityEngine.Object) instance != (UnityEngine.Object) null && BattleSpeedController.isEnableSpeedControll.Value && (BattleSpeedController.CheckAllowedSpeedBattle(instance.CurrentQuest) && instance.QuestStart) && !BattleSpeedController.isEnd;
  }

  public static bool IsShowSpeedButton()
  {
    SceneBattle instance = SceneBattle.Instance;
    return (UnityEngine.Object) instance != (UnityEngine.Object) null && BattleSpeedController.CheckAllowedSpeedBattle(instance.CurrentQuest);
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
        case QuestTypes.Extra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          return true;
      }
    }
    return false;
  }

  private static int WrapAroundSpeedNum(int num)
  {
    if (num >= BattleSpeedController.SPEED_LIST.Length)
      return num = 0;
    if (num < 0)
      return num = BattleSpeedController.SPEED_LIST.Length - 1;
    return num;
  }

  private static float SaveLimitSpeed(float inSpeed)
  {
    if ((double) inSpeed > 2.0)
      return 2f;
    if ((double) inSpeed < 2.0)
      return 1f;
    return inSpeed;
  }
}
