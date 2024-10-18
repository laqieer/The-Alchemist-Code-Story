// Decompiled with JetBrains decompiler
// Type: SRPG.HomeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "FadeIn Start", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "FadeOut Start", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "FadeOut End", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(15, "Home Enter", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(16, "Home Leave", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(17, "Home Popup Completed", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(30, "Restore", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "Reload", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(99, "To Home", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(100, "To Scene[0]", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "To Scene[1]", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "To Scene[2]", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "To Scene[3]", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(104, "To Scene[4]", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(105, "To Scene[5]", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(106, "To Scene[6]", FlowNode.PinTypes.Input, 106)]
  [FlowNode.Pin(107, "To Scene[7]", FlowNode.PinTypes.Input, 107)]
  [FlowNode.Pin(108, "To Scene[8]", FlowNode.PinTypes.Input, 108)]
  [FlowNode.Pin(109, "To Scene[9]", FlowNode.PinTypes.Input, 109)]
  [FlowNode.Pin(110, "To Scene[10]", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "To Scene[11]", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(112, "To Scene[12]", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(113, "To Scene[13]", FlowNode.PinTypes.Input, 113)]
  [FlowNode.Pin(114, "To Scene[14]", FlowNode.PinTypes.Input, 114)]
  [FlowNode.Pin(115, "To Scene[15]", FlowNode.PinTypes.Input, 115)]
  [FlowNode.Pin(116, "To Scene[16]", FlowNode.PinTypes.Input, 116)]
  [FlowNode.Pin(117, "To Scene[17]", FlowNode.PinTypes.Input, 117)]
  [FlowNode.Pin(118, "To Scene[18]", FlowNode.PinTypes.Input, 118)]
  [FlowNode.Pin(119, "To Scene[19]", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(120, "To Scene[20]", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(121, "To Scene[21]", FlowNode.PinTypes.Input, 121)]
  [FlowNode.Pin(122, "To Scene[22]", FlowNode.PinTypes.Input, 122)]
  [FlowNode.Pin(123, "To Scene[23]", FlowNode.PinTypes.Input, 123)]
  [FlowNode.Pin(124, "To Scene[24]", FlowNode.PinTypes.Input, 124)]
  [FlowNode.Pin(125, "To Scene[25]", FlowNode.PinTypes.Input, 125)]
  [FlowNode.Pin(126, "To Scene[26]", FlowNode.PinTypes.Input, 126)]
  [FlowNode.Pin(127, "To Scene[27]", FlowNode.PinTypes.Input, 127)]
  [FlowNode.Pin(128, "To Scene[28]", FlowNode.PinTypes.Input, 128)]
  [FlowNode.Pin(129, "To Scene[29]", FlowNode.PinTypes.Input, 129)]
  [FlowNode.Pin(130, "To Scene[30]", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(131, "To Scene[31]", FlowNode.PinTypes.Input, 131)]
  [FlowNode.Pin(132, "To Scene[32]", FlowNode.PinTypes.Input, 132)]
  [FlowNode.Pin(133, "To Scene[33]", FlowNode.PinTypes.Input, 133)]
  [FlowNode.Pin(134, "To Scene[34]", FlowNode.PinTypes.Input, 134)]
  [FlowNode.Pin(135, "To Scene[35]", FlowNode.PinTypes.Input, 135)]
  [FlowNode.Pin(136, "To Scene[36]", FlowNode.PinTypes.Input, 136)]
  [FlowNode.Pin(137, "To Scene[37]", FlowNode.PinTypes.Input, 137)]
  [FlowNode.Pin(138, "To Scene[38]", FlowNode.PinTypes.Input, 138)]
  [FlowNode.Pin(139, "To Scene[39]", FlowNode.PinTypes.Input, 139)]
  [FlowNode.Pin(140, "To Scene[40]", FlowNode.PinTypes.Input, 140)]
  [FlowNode.Pin(141, "To Scene[41]", FlowNode.PinTypes.Input, 141)]
  [FlowNode.Pin(1000, "Req Rank Match Reward", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "Rank Match Rewarded", FlowNode.PinTypes.Input, 1001)]
  [FlowNode.Pin(2000, "Beginner Notify", FlowNode.PinTypes.Output, 2000)]
  [FlowNode.Pin(2001, "Beginner Notified", FlowNode.PinTypes.Input, 2001)]
  [FlowNode.Pin(3000, "Gacha Redraw Notify", FlowNode.PinTypes.Output, 3000)]
  [FlowNode.Pin(3001, "Gacha Redraw Notified", FlowNode.PinTypes.Input, 3001)]
  [FlowNode.Pin(3002, "Go to Gacha Redraw", FlowNode.PinTypes.Output, 3002)]
  [FlowNode.Pin(4000, "Req Raid Reward", FlowNode.PinTypes.Output, 4000)]
  [FlowNode.Pin(4001, "Raid Rewarded", FlowNode.PinTypes.Input, 4001)]
  [FlowNode.Pin(5000, "Req Unit Rental Check", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(5001, "Unit Rental Checked", FlowNode.PinTypes.Input, 5001)]
  [FlowNode.Pin(6000, "Req GuildRaid Reward", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(6001, "GuildRaid Rewarded", FlowNode.PinTypes.Input, 6001)]
  [FlowNode.Pin(7000, "Req GuildTrophy", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.Pin(7001, "GuildTrophy", FlowNode.PinTypes.Input, 7001)]
  [FlowNode.Pin(8000, "Req WorldRaid Reward", FlowNode.PinTypes.Output, 8000)]
  public class HomeWindow : MonoBehaviour, IFlowInterface
  {
    public static HomeWindow mInstance;
    public const int PINID_FADEIN_START = 10;
    public const int PINID_FADEOUT_START = 11;
    public const int PINID_FADEOUT_END = 12;
    public const int PINID_HOME_ENTER = 15;
    public const int PINID_HOME_LEAVE = 16;
    public const int PINID_HOME_POPUP_COMPLETED = 17;
    public const int PINID_RELOAD = 40;
    public const int PINOUT_REQ_RANKMATCH_REWARD = 1000;
    public const int PININ_RANKMATCH_REWARDED = 1001;
    public const int PINOUT_BEGINNER_NOTIFY = 2000;
    public const int PININ_BEGINNER_NOTIFIED = 2001;
    public const int PINOUT_GACHAREDRAW_NOTIFY = 3000;
    public const int PININ_GACHAREDRAW_NOTIFIED = 3001;
    public const int PINOUT_GOTO_GACHAREDRAW = 3002;
    public const int PINOUT_REQ_RAID_REWARD = 4000;
    public const int PININ_RAID_REWARDED = 4001;
    public const int PINOUT_RENTAL_UNIT_CHECK = 5000;
    public const int PININ_RENTAL_UNIT_CHECKED = 5001;
    public const int PINOUT_REQ_GUILDRAID_REWARD = 6000;
    public const int PININ_GUILDRAID_REWARDED = 6001;
    public const int PINOUT_REQ_GUILD_TROPHY = 7000;
    public const int PININ_GUILD_TROPHY = 7001;
    public const int PINOUT_REQ_WORLDRAID_REWARD = 8000;
    private StateMachine<HomeWindow> mStateMachine;
    private static RestorePoints mRestorePoint;
    public string[] SceneNames = new string[0];
    public string[] IgnoreSameSceneCheck = new string[0];
    public string UnloadTrigger = "UNLOAD_MENU";
    public string DayChangeTrigger = "DAY_CHANGE";
    [StringIsResourcePath(typeof (GameObject))]
    public string NewsWindowPath;
    [StringIsResourcePath(typeof (GameObject))]
    public string LoginBonusPath;
    [StringIsResourcePath(typeof (GameObject))]
    public string LoginInfoPath;
    public bool DebugLoginBonus;
    private bool mDesirdSceneSet;
    private bool mCheckSceneChangeStatus;
    private bool mFadingOut;
    private string mDesiredSceneName;
    private string mLastSceneName;
    private bool mDesiredSceneIsHome;
    private bool mIgnorePopups;
    private float mSyncTrophyInterval = 5f;
    private bool mRelogin;
    private bool mReloginSuccess;
    private bool mRankmatchRewarded;
    private bool mRaidRewarded;
    private bool mGuildRaidRewarded;
    private bool mBeginnerNotified;
    private bool mGachaRedrawNotified;
    private bool mGuildTrophy;
    private bool mIsReqRentalUnitCheck;
    private bool mRentalUnitChecked;
    private UnityWeakReference<GameObject> mCurrentSceneParentObject = new UnityWeakReference<GameObject>((GameObject) null);
    private bool mIsReqWorldRaidRewardCheck;
    public HomeWindow.RestoreScene[] RestoreScenes = new HomeWindow.RestoreScene[0];
    public static int EnterHomeCount;
    private bool mNewsShown;

    public static HomeWindow Current => HomeWindow.mInstance;

    public static void SetRestorePoint(RestorePoints restorePoint)
    {
      HomeWindow.mRestorePoint = restorePoint;
    }

    public static RestorePoints GetRestorePoint() => HomeWindow.mRestorePoint;

    public bool IsReadyInTown
    {
      get
      {
        return this.mStateMachine != null && this.mStateMachine.IsInState<HomeWindow.State_Default>() && !this.mDesirdSceneSet;
      }
    }

    public bool IsSceneChanging => this.mDesirdSceneSet;

    public bool DesiredSceneIsHome => this.mDesiredSceneIsHome;

    public HomeWindow.WorldRaidLastBossStatus mWorldRaidLastBossStatus { get; private set; }

    public void SetWorldRaidLastBossStatus(HomeWindow.WorldRaidLastBossStatus status)
    {
      this.mWorldRaidLastBossStatus = status;
    }

    public void Activated(int pinID)
    {
      if (99 <= pinID && pinID < 142)
      {
        if (this.mDesirdSceneSet)
          return;
        string desiredSceneName = (string) null;
        bool flag1 = false;
        bool flag2 = GlobalVars.ForceSceneChange;
        GlobalVars.ForceSceneChange = false;
        if (pinID == 99)
        {
          SectionParam homeWorld = HomeUnitController.GetHomeWorld();
          if (homeWorld != null)
          {
            desiredSceneName = homeWorld.home;
            flag1 = true;
          }
        }
        else
          desiredSceneName = this.SceneNames[pinID - 100];
        if (Array.FindIndex<string>(this.IgnoreSameSceneCheck, (Predicate<string>) (scene => scene.Equals(desiredSceneName))) != -1)
          flag2 = true;
        if (!string.IsNullOrEmpty(desiredSceneName) && (flag2 || this.mLastSceneName != desiredSceneName))
        {
          if (!MonoSingleton<GameManager>.Instance.PrepareSceneChange())
            return;
          this.SceneChangeSendLog(this.mDesiredSceneName, desiredSceneName);
          this.mDesirdSceneSet = true;
          this.mDesiredSceneName = desiredSceneName;
          this.mDesiredSceneIsHome = flag1;
          this.mIgnorePopups = !this.mDesiredSceneIsHome;
          GlobalVars.SetDropTableGeneratedTime();
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) GameObject.Find("EventQuest"), (UnityEngine.Object) null))
            return;
          GlobalEvent.Invoke("UPDATE_EVENT_LIST", (object) this);
        }
      }
      else
      {
        switch (pinID)
        {
          case 12:
            this.mFadingOut = false;
            break;
          case 30:
            if (this.RestoreScenes == null)
              break;
            GlobalVars.IsTutorialEnd = true;
            if (HomeWindow.mRestorePoint != RestorePoints.Home)
            {
              if (!this.IsNotHomeBGM())
                FlowNode_PlayBGM.PlayHomeBGM();
              for (int index = 0; index < this.RestoreScenes.Length; ++index)
              {
                if (this.RestoreScenes[index].Type == HomeWindow.mRestorePoint)
                {
                  this.Activated(100 + this.RestoreScenes[index].Index);
                  return;
                }
              }
            }
            this.Activated(99);
            break;
          case 40:
            if (this.mDesiredSceneIsHome)
            {
              this.Activated(99);
              break;
            }
            if (!string.IsNullOrEmpty(this.mLastSceneName))
            {
              int index = Array.FindIndex<string>(this.SceneNames, (Predicate<string>) (s => s == this.mLastSceneName));
              if (index >= 0)
              {
                this.Activated(index + 100);
                break;
              }
            }
            DebugUtility.LogError("リロードするシーンが存在しません");
            break;
          case 1001:
            this.mRankmatchRewarded = true;
            break;
          case 2001:
            this.mBeginnerNotified = true;
            break;
          case 3001:
            this.mGachaRedrawNotified = true;
            break;
          case 4001:
            this.mRaidRewarded = true;
            break;
          case 5001:
            this.mRentalUnitChecked = true;
            break;
          case 6001:
            this.mGuildRaidRewarded = true;
            break;
          case 7001:
            this.mGuildTrophy = true;
            break;
        }
      }
    }

    private bool IsNotHomeBGM()
    {
      RestorePoints mRestorePoint = HomeWindow.mRestorePoint;
      switch (mRestorePoint)
      {
        case RestorePoints.Raid:
        case RestorePoints.GenesisStage:
        case RestorePoints.GenesisBoss:
        case RestorePoints.AdvanceStage:
        case RestorePoints.AdvanceBoss:
        case RestorePoints.GuildRaid:
        case RestorePoints.JukeBox:
        case RestorePoints.GVG:
        case RestorePoints.WorldRaid:
label_3:
          return true;
        default:
          switch (mRestorePoint - 9)
          {
            case RestorePoints.Home:
            case RestorePoints.QuestList:
            case RestorePoints.Arena:
            case RestorePoints.UnitCharacterQuest:
              goto label_3;
            default:
              if (mRestorePoint != RestorePoints.QuestList)
                return false;
              goto label_3;
          }
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) HomeWindow.mInstance, (UnityEngine.Object) null))
        HomeWindow.mInstance = this;
      this.mStateMachine = new StateMachine<HomeWindow>(this);
      this.mStateMachine.GotoState<HomeWindow.State_Default>();
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) HomeWindow.mInstance, (UnityEngine.Object) this))
        return;
      HomeWindow.mInstance = (HomeWindow) null;
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing || SRPG.Network.Mode != SRPG.Network.EConnectMode.Online || !MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (!player.TrophyData.IsTrophyDirty())
        return;
      List<TrophyState> trophyStatesList = player.TrophyData.TrophyStatesList;
      List<TrophyState> trophyprogs1 = new List<TrophyState>(trophyStatesList.Count);
      List<TrophyState> trophyprogs2 = new List<TrophyState>(trophyStatesList.Count);
      for (int index = 0; index < trophyStatesList.Count; ++index)
      {
        if (trophyStatesList[index].Param.IsChallengeMission)
        {
          if (trophyStatesList[index].IsDirty)
            trophyprogs2.Add(trophyStatesList[index]);
        }
        else if (trophyStatesList[index].IsDirty)
          trophyprogs1.Add(trophyStatesList[index]);
      }
      if (trophyprogs1.Count > 0)
        SRPG.Network.RequestAPIImmediate((WebAPI) new ReqUpdateTrophy(trophyprogs1, new SRPG.Network.ResponseCallback(this.OnUpdateTrophyImmediate), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK), true);
      if (trophyprogs2.Count <= 0)
        return;
      SRPG.Network.RequestAPIImmediate((WebAPI) new ReqUpdateBingo(trophyprogs2, new SRPG.Network.ResponseCallback(this.OnUpdateTrophyImmediate), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK), true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void OnUpdateTrophyImmediate(WWWResult www) => SRPG.Network.RemoveAPI();

    public void SetVisible(bool visible)
    {
      Canvas component = ((Component) this).GetComponent<Canvas>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((Behaviour) component).enabled = visible;
    }

    private void Update() => this.mStateMachine.Update();

    public void FgGIDLoginCheck()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || (CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0 || MonoSingleton<GameManager>.Instance.AuthStatus != ReqFgGAuth.eAuthStatus.Synchronized)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
    }

    public void UnlockContents()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || (CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag1 = true;
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV) || !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV))
        flag1 = false;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      if (flag1)
      {
        num1 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV);
        num2 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV);
        num3 = player.Lv;
        num4 = player.VipRank;
        if (num1 >= num3 && num2 >= num4)
          flag1 = false;
      }
      UnlockParam[] unlocks = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
      for (int index = 0; index < unlocks.Length; ++index)
      {
        if (!unlocks[index].IsExistConds_ClearQuest())
        {
          if (flag1)
          {
            bool flag2 = unlocks[index].PlayerLevel == 0 || num1 < unlocks[index].PlayerLevel && unlocks[index].PlayerLevel <= num3;
            bool flag3 = unlocks[index].VipRank == 0 || num2 < unlocks[index].VipRank && unlocks[index].VipRank <= num4;
            if (flag2 && flag3)
              NotifyList.PushContentsUnlock(unlocks[index]);
          }
        }
        else
          this.UnlockContents_ExistClearQuest(unlocks[index]);
      }
      if (flag1)
      {
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, num3);
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, num4);
      }
      LevelLock.UpdateLockStates();
    }

    private void UnlockContents_ExistClearQuest(UnlockParam unlock)
    {
      long num1 = PlayerPrefsUtility.GetLong(PlayerPrefsUtility.UNLOCK_LEVEL_NOTIFIED);
      if (((UnlockTargets) num1 & unlock.UnlockTarget) != (UnlockTargets) 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (unlock.PlayerLevel > player.Lv || unlock.VipRank > player.VipRank || !MonoSingleton<GameManager>.Instance.Player.IsClearUnclockConditions_Quest(unlock.ClearQuests))
        return;
      NotifyList.PushContentsUnlock(unlock);
      long num2 = (long) ((UnlockTargets) num1 | unlock.UnlockTarget);
      PlayerPrefsUtility.SetLong(PlayerPrefsUtility.UNLOCK_LEVEL_NOTIFIED, num2);
    }

    private void NotifySupportResult()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.SupportGold <= 0)
        return;
      NotifyList.PushQuestSupport(player.SupportCount, player.SupportGold);
      player.OnGoldChange(player.SupportGold);
    }

    private void CheckTrophies()
    {
    }

    private void NotifyNewFriendRequests()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num = 0;
      StringBuilder stringBuilder = new StringBuilder(200);
      string[] array;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.FRIEND_REQUEST_CACHE))
        array = PlayerPrefsUtility.GetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, string.Empty).Split(',');
      else
        array = new string[0];
      for (int index = 0; index < player.FollowerUID.Count; ++index)
      {
        if (Array.IndexOf<string>(array, player.FollowerUID[index]) < 0)
          ++num;
        if (stringBuilder.Length > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(player.FollowerUID[index]);
      }
      if (num > 0)
        NotifyList.Push(LocalizedText.Get("sys.FRIENDREQS", (object) num));
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, stringBuilder.ToString(), true);
    }

    public void ChangeNewsState()
    {
      if (!(this.mStateMachine.CurrentState.Name == "State_Default"))
        return;
      GameUtility.setLoginInfoRead(string.Empty);
      this.mStateMachine.GotoState<HomeWindow.State_News>();
      LoginNewsInfo.UpdateBeforePubInfo();
    }

    public void SendEventBeforeChangeScene()
    {
      if (!this.mCurrentSceneParentObject.IsAlive)
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this.mCurrentSceneParentObject.Target.transform, PredefinedLocalEvents.HOME_PREPARE_UNLOAD_MENU.ToString());
    }

    private void MiscBeforeDefaultState()
    {
      this.NotifyNewFriendRequests();
      if (HomeWindow.EnterHomeCount == 0)
        this.CheckTrophies();
      if (this.mNewsShown || GlobalVars.IsTitleStart.Get() || !MonoSingleton<GameManager>.Instance.Player.IsFirstLogin && !this.DebugLoginBonus)
        return;
      this.mNewsShown = true;
      Json_LoginBonus recentLoginBonus = MonoSingleton<GameManager>.Instance.Player.RecentLoginBonus;
      if (recentLoginBonus != null)
      {
        string iname;
        int num;
        if (recentLoginBonus.coin > 0)
        {
          iname = "$COIN";
          num = recentLoginBonus.coin;
        }
        else
        {
          iname = recentLoginBonus.iname;
          num = recentLoginBonus.num;
        }
        ItemData data = new ItemData();
        if (data.Setup(0L, iname, num))
          NotifyList.PushLoginBonus(data);
      }
      this.NotifySupportResult();
    }

    private void SceneChangeSendLog(string before, string after)
    {
      if (before == null)
        before = "Start";
      if (after == "EventQuestList")
      {
        string name = Enum.GetName(typeof (GlobalVars.EventQuestListType), (object) GlobalVars.ReqEventPageListType);
        after = after + "-" + name;
      }
      FlowNode_SendLogMessage.SceneChangeEvent("scene", before, after);
    }

    [Serializable]
    public struct RestoreScene
    {
      public RestorePoints Type;
      public int Index;
    }

    public enum WorldRaidLastBossStatus
    {
      None,
      Alive,
      Dead,
    }

    private class State_Default : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        if (self.mIsReqRentalUnitCheck)
        {
          self.mIsReqRentalUnitCheck = false;
          self.mStateMachine.GotoState<HomeWindow.State_UnitRentalCheck>();
        }
        else
        {
          if (!self.mIsReqWorldRaidRewardCheck)
            return;
          self.mIsReqWorldRaidRewardCheck = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 8000);
        }
      }

      public override void Update(HomeWindow self)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (self.mDesirdSceneSet && !self.mCheckSceneChangeStatus && !CriticalSection.IsActive)
        {
          self.mDesirdSceneSet = true;
          self.mStateMachine.GotoState<HomeWindow.State_ChangeScene>();
        }
        else
        {
          player.UpdateStaminaDailyMission();
          player.UpdateVipDailyMission(player.VipRank);
          player.UpdateCardDailyMission();
          if (!MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock || !player.TrophyData.IsTrophyDirty())
            return;
          if ((double) self.mSyncTrophyInterval > 0.0)
            self.mSyncTrophyInterval -= Time.unscaledDeltaTime;
          if ((double) self.mSyncTrophyInterval > 0.0 || CriticalSection.IsActive || SRPG.Network.Mode != SRPG.Network.EConnectMode.Online)
            return;
          self.mStateMachine.GotoState<HomeWindow.State_UpdateTrophy>();
        }
      }
    }

    private class State_UnitRentalCheck : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        self.mRentalUnitChecked = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 5000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mRentalUnitChecked)
          return;
        self.mStateMachine.GotoState<HomeWindow.State_Default>();
      }
    }

    private class State_ChangeScene : State<HomeWindow>
    {
      private GameObject mNewScene;
      private bool waitCriticalSection;
      private HomeWindow.State_ChangeScene.eChangeSceneState mChangeSceneState;
      private SceneRequest req;
      private AsyncOperation UnloadUnusedAssetTask;

      public override void Begin(HomeWindow self)
      {
        GlobalVars.OverWritePartyType.Set(eOverWritePartyType.None);
        if (CriticalSection.IsActive)
          this.waitCriticalSection = true;
        else
          CriticalSection.Enter(CriticalSections.SceneChange);
      }

      private void OnSceneAwake(GameObject scene)
      {
        this.mNewScene = scene;
        this.self.mCurrentSceneParentObject.Target = scene;
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      }

      public override void Update(HomeWindow self)
      {
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartFadeout)
        {
          self.mFadingOut = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 11);
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitCriticalSection;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitCriticalSection)
        {
          if (this.waitCriticalSection)
          {
            if (CriticalSection.IsActive)
              return;
            CriticalSection.Enter(CriticalSections.SceneChange);
            this.waitCriticalSection = false;
          }
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitAnyDownload;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitAnyDownload)
        {
          if (!AssetDownloader.isDone)
            return;
          this.mChangeSceneState = !AssetManager.IsAssetBundle(self.mDesiredSceneName) ? HomeWindow.State_ChangeScene.eChangeSceneState.WaitFadeout : HomeWindow.State_ChangeScene.eChangeSceneState.StartDownloadNextScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartDownloadNextScene)
        {
          AssetManager.PrepareAssets(self.mDesiredSceneName);
          if (self.mDesiredSceneIsHome)
          {
            foreach (string resourcePath in FlowNode_PlayBGM.GetHomeBGM())
              AssetManager.PrepareAssets(resourcePath);
          }
          if (!AssetDownloader.isDone)
          {
            ProgressWindow.OpenGenericDownloadWindow();
            AssetDownloader.StartDownload(false);
            this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitDownloadNextScene;
            return;
          }
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitFadeout;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitDownloadNextScene)
        {
          if (!AssetDownloader.isDone)
            return;
          ProgressWindow.Close();
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitFadeout;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitFadeout)
        {
          if (self.mFadingOut)
            return;
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.StartUnloadUnusedAsset_PreLoad;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartUnloadUnusedAsset_PreLoad)
        {
          this.UnloadUnusedAssetTask = AssetManager.UnloadUnusedAssets();
          AssetBundleUnloader.ReserveUnloadForce();
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitUnloadUnusedAsset_PreLoad;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitUnloadUnusedAsset_PreLoad)
        {
          if (this.UnloadUnusedAssetTask != null && !this.UnloadUnusedAssetTask.isDone || AssetBundleUnloader.IsForceUnloadNow)
            return;
          GC.Collect();
          this.UnloadUnusedAssetTask = (AsyncOperation) null;
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.StartLoadNextScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartLoadNextScene)
        {
          if (self.mDesiredSceneIsHome)
            FlowNode_PlayBGM.PlayHomeBGM();
          self.mLastSceneName = self.mDesiredSceneName;
          this.req = AssetManager.LoadSceneAsync(self.mDesiredSceneName, true);
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitLoadNextScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitLoadNextScene)
        {
          if (!this.req.canBeActivated)
            return;
          self.SendEventBeforeChangeScene();
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.DestroyCurrentScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.DestroyCurrentScene)
        {
          if (MonoSingleton<GameManager>.Instance.IsImportantJobRunning)
            return;
          if (!string.IsNullOrEmpty(self.UnloadTrigger))
            GlobalEvent.Invoke(self.UnloadTrigger, (object) this);
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.StartActivateNextScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartActivateNextScene)
        {
          SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
          this.req.ActivateScene();
          if (!string.IsNullOrEmpty(self.mLastSceneName))
            AssetManager.UnloadScene(self.mLastSceneName);
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitActivateNextScene;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitActivateNextScene)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNewScene, (UnityEngine.Object) null))
          {
            if (!this.req.isDone)
              return;
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
            if (!string.IsNullOrEmpty(self.mDesiredSceneName))
              AssetManager.UnloadScene(self.mDesiredSceneName);
            this.req = (SceneRequest) null;
            this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.StartLoadNextScene;
            return;
          }
          if (!this.req.isDone)
            return;
          CriticalSection.Leave(CriticalSections.SceneChange);
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.StartUnloadUnusedAsset_PostLoad;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.StartUnloadUnusedAsset_PostLoad)
        {
          if (CriticalSection.IsActive)
            return;
          this.UnloadUnusedAssetTask = AssetManager.UnloadUnusedAssets();
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.WaitUnloadUnusedAsset_PostLoad;
        }
        if (this.mChangeSceneState == HomeWindow.State_ChangeScene.eChangeSceneState.WaitUnloadUnusedAsset_PostLoad)
        {
          if (this.UnloadUnusedAssetTask != null && !this.UnloadUnusedAssetTask.isDone)
            return;
          this.UnloadUnusedAssetTask = (AsyncOperation) null;
          GC.Collect();
          this.mChangeSceneState = HomeWindow.State_ChangeScene.eChangeSceneState.TransitionNextState;
        }
        if (this.mChangeSceneState != HomeWindow.State_ChangeScene.eChangeSceneState.TransitionNextState)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 10);
        GameUtility.FadeIn(0.5f);
        self.mDesirdSceneSet = false;
        if (!self.mIgnorePopups)
        {
          self.mIsReqRentalUnitCheck = true;
          self.mIsReqWorldRaidRewardCheck = true;
        }
        if (!self.mIgnorePopups && !self.mNewsShown && !GlobalVars.IsTitleStart.Get() && MonoSingleton<GameManager>.Instance.Player.IsFirstLogin)
          self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
        else if (!self.mIgnorePopups && FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
          self.mStateMachine.GotoState<HomeWindow.State_GachaRedraw>();
        else if (!self.mIgnorePopups && !GlobalVars.IsGuildTrophy.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildTrophy>();
        else if (!self.mIgnorePopups && GameUtility.isLoginInfoDisplay() && !GlobalVars.IsLoginInfoNotified.Get())
          self.mStateMachine.GotoState<HomeWindow.State_News>();
        else if (!self.mIgnorePopups && !GlobalVars.IsRankMatchRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RankMatchReward>();
        else if (!self.mIgnorePopups && !GlobalVars.IsRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RaidReward>();
        else if (!self.mIgnorePopups && !GlobalVars.IsGuildRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
        else if (!self.mIgnorePopups && MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          if (self.mDesiredSceneIsHome)
            FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
        if (self.mDesiredSceneIsHome)
        {
          self.UnlockContents();
          self.FgGIDLoginCheck();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 15);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 16);
        self.mCheckSceneChangeStatus = false;
      }

      private enum eChangeSceneState
      {
        StartFadeout,
        WaitCriticalSection,
        WaitAnyDownload,
        WaitFadeout,
        StartUnloadUnusedAsset_PreLoad,
        WaitUnloadUnusedAsset_PreLoad,
        StartDownloadNextScene,
        WaitDownloadNextScene,
        StartLoadNextScene,
        WaitLoadNextScene,
        DestroyCurrentScene,
        StartActivateNextScene,
        WaitActivateNextScene,
        StartUnloadUnusedAsset_PostLoad,
        WaitUnloadUnusedAsset_PostLoad,
        TransitionNextState,
      }
    }

    private class State_UpdateTrophy : State<HomeWindow>
    {
      private List<TrophyState> mDirtyList;

      public override void Begin(HomeWindow self)
      {
        List<TrophyState> trophyStatesList = MonoSingleton<GameManager>.Instance.Player.TrophyData.TrophyStatesList;
        this.mDirtyList = new List<TrophyState>(trophyStatesList.Count);
        for (int index = 0; index < trophyStatesList.Count; ++index)
        {
          if (!trophyStatesList[index].Param.IsChallengeMission && trophyStatesList[index].IsDirty)
            this.mDirtyList.Add(trophyStatesList[index]);
        }
        if (this.mDirtyList.Count > 0)
          SRPG.Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mDirtyList, new SRPG.Network.ResponseCallback(this.ResponseCallback), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        else
          self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
      }

      private void ResponseCallback(WWWResult www)
      {
        this.self.mSyncTrophyInterval = 5f;
        if (!SRPG.Network.IsError)
        {
          for (int index = 0; index < this.mDirtyList.Count; ++index)
            this.mDirtyList[index].IsDirty = false;
          MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
          Json_TrophyPlayerDataAll body;
          if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
          {
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
            DebugUtility.Assert(jsonObject != null, "jsonRes == null");
            body = jsonObject.body;
          }
          else
          {
            FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse playerDataAllResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
            DebugUtility.Assert(playerDataAllResponse != null, "mpRes == null");
            body = playerDataAllResponse.body;
          }
          try
          {
            if (body == null)
              throw new Exception("ReqUpdateTrophy: illegal Server response!");
            if (!TrophyStarMissionParam.EntryTrophyStarMission(body.star_mission))
              throw new Exception("ReqUpdateTrophy: illegal StarMission information!");
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
          SRPG.Network.RemoveAPI();
          this.self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
        }
        else if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.TrophyStarMission_Future)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
    }

    private class State_UpdateChallengeMission : State<HomeWindow>
    {
      private List<TrophyState> mDirtyList;

      public override void Begin(HomeWindow self)
      {
        List<TrophyState> trophyStatesList = MonoSingleton<GameManager>.Instance.Player.TrophyData.TrophyStatesList;
        this.mDirtyList = new List<TrophyState>(trophyStatesList.Count);
        for (int index = 0; index < trophyStatesList.Count; ++index)
        {
          if (trophyStatesList[index].Param.IsChallengeMission && trophyStatesList[index].IsDirty)
            this.mDirtyList.Add(trophyStatesList[index]);
        }
        if (this.mDirtyList.Count > 0)
        {
          SRPG.Network.RequestAPI((WebAPI) new ReqUpdateBingo(this.mDirtyList, new SRPG.Network.ResponseCallback(this.ResponseCallback), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        }
        else
        {
          self.mSyncTrophyInterval = 5f;
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
      }

      private void ResponseCallback(WWWResult www)
      {
        this.self.mSyncTrophyInterval = 5f;
        if (!SRPG.Network.IsError)
        {
          for (int index = 0; index < this.mDirtyList.Count; ++index)
            this.mDirtyList[index].IsDirty = false;
          SRPG.Network.RemoveAPI();
          this.self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        else
          FlowNode_Network.Retry();
      }
    }

    private class State_GuildTrophy : State<HomeWindow>
    {
      private LoadRequest mReq;
      private GameObject mInstance;

      public override void Begin(HomeWindow self)
      {
        GlobalVars.IsGuildTrophy.Set(false);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 7000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mGuildTrophy)
          return;
        self.mGuildTrophy = false;
        if (GameUtility.isLoginInfoDisplay() && !GlobalVars.IsLoginInfoNotified.Get())
          self.mStateMachine.GotoState<HomeWindow.State_News>();
        else if (!GlobalVars.IsRankMatchRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RankMatchReward>();
        else if (!GlobalVars.IsRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RaidReward>();
        else if (!GlobalVars.IsGuildRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
        else if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_News : State<HomeWindow>
    {
      private LoadRequest mReq;
      private GameObject mInstance;

      public override void Begin(HomeWindow self)
      {
        GlobalVars.IsLoginInfoNotified.Set(true);
        if (string.IsNullOrEmpty(self.LoginInfoPath))
          return;
        this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginInfoPath);
      }

      public override void Update(HomeWindow self)
      {
        if (this.mReq != null)
        {
          if (!this.mReq.isDone)
            return;
          if (UnityEngine.Object.op_Inequality(this.mReq.asset, (UnityEngine.Object) null))
            this.mInstance = UnityEngine.Object.Instantiate(this.mReq.asset) as GameObject;
          this.mReq = (LoadRequest) null;
        }
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mInstance, (UnityEngine.Object) null))
          return;
        if (self.mDesirdSceneSet)
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        else if (!GlobalVars.IsRankMatchRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RankMatchReward>();
        else if (!GlobalVars.IsRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RaidReward>();
        else if (!GlobalVars.IsGuildRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
        else if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_RankMatchReward : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        GlobalVars.IsRankMatchRewarded.Set(true);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 1000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mRankmatchRewarded)
          return;
        self.mRankmatchRewarded = false;
        if (!GlobalVars.IsRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_RaidReward>();
        else if (!GlobalVars.IsGuildRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
        else if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_RaidReward : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        GlobalVars.IsRaidRewarded.Set(true);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 4000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mRaidRewarded)
          return;
        self.mRaidRewarded = false;
        if (!GlobalVars.IsGuildRaidRewarded.Get())
          self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
        else if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_GuildRaidReward : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        GlobalVars.IsGuildRaidRewarded.Set(true);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 6000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mGuildRaidRewarded)
          return;
        self.mGuildRaidRewarded = false;
        if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
        {
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_LoginBonus : State<HomeWindow>
    {
      private LoadRequest mReq;
      private GameObject mInstance;
      private string mLoginBonusType;

      public override void Begin(HomeWindow self)
      {
        MonoSingleton<GameManager>.Instance.Player.InitLoginBonusTable();
        self.mNewsShown = true;
        GlobalVars.IsTitleStart.Set(true);
        this.NextLoginBonusBegin();
        this.LoginBonusGameObjectLoad();
      }

      public override void Update(HomeWindow self)
      {
        if (this.LoginBonusGameObjectLoad() || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mInstance, (UnityEngine.Object) null))
          return;
        if (MonoSingleton<GameManager>.Instance.Player.HasQueuedLoginBonus)
        {
          this.NextLoginBonusBegin();
          this.LoginBonusGameObjectLoad();
        }
        else
        {
          self.NotifyNewFriendRequests();
          self.NotifySupportResult();
          self.CheckTrophies();
          if (FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
            self.mStateMachine.GotoState<HomeWindow.State_GachaRedraw>();
          else if (!GlobalVars.IsGuildTrophy.Get())
            self.mStateMachine.GotoState<HomeWindow.State_GuildTrophy>();
          else if (GameUtility.isLoginInfoDisplay() && !GlobalVars.IsLoginInfoNotified.Get())
            self.mStateMachine.GotoState<HomeWindow.State_News>();
          else if (!GlobalVars.IsRankMatchRewarded.Get())
            self.mStateMachine.GotoState<HomeWindow.State_RankMatchReward>();
          else if (!GlobalVars.IsRaidRewarded.Get())
            self.mStateMachine.GotoState<HomeWindow.State_RaidReward>();
          else if (!GlobalVars.IsGuildRaidRewarded.Get())
            self.mStateMachine.GotoState<HomeWindow.State_GuildRaidReward>();
          else if (MonoSingleton<GameManager>.Instance.Player.NeedsShowBeginnerNotify())
          {
            self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
          }
          else
          {
            self.MiscBeforeDefaultState();
            self.mStateMachine.GotoState<HomeWindow.State_Default>();
            FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
          }
        }
      }

      private bool LoginBonusGameObjectLoad()
      {
        if (this.mReq != null)
        {
          if (!this.mReq.isDone)
            return true;
          if (UnityEngine.Object.op_Inequality(this.mReq.asset, (UnityEngine.Object) null))
          {
            this.mInstance = UnityEngine.Object.Instantiate(this.mReq.asset) as GameObject;
            LoginBonusWindow component1 = this.mInstance.GetComponent<LoginBonusWindow>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
              component1.TableID = this.mLoginBonusType;
            LoginBonusWindow28days component2 = this.mInstance.GetComponent<LoginBonusWindow28days>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              component2.TableID = this.mLoginBonusType;
            LoginBonusWindowPremium component3 = this.mInstance.GetComponent<LoginBonusWindowPremium>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
              component3.ConfigWindow = false;
            LoginBonusManager component4 = this.mInstance.GetComponent<LoginBonusManager>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
            {
              component4.TableID = this.mLoginBonusType;
              component4.IsLoginCall = true;
            }
          }
          this.mReq = (LoadRequest) null;
        }
        return false;
      }

      private void NextLoginBonusBegin()
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (!player.HasQueuedLoginBonus)
          return;
        this.mLoginBonusType = player.DequeueNextLoginBonusTableID();
        if (string.IsNullOrEmpty(this.mLoginBonusType))
          return;
        string bonusePrefabName = player.GetLoginBonusePrefabName(this.mLoginBonusType);
        if (string.IsNullOrEmpty(bonusePrefabName))
        {
          if (string.IsNullOrEmpty(this.self.LoginBonusPath))
            return;
          this.mReq = AssetManager.LoadAsync<GameObject>(this.self.LoginBonusPath);
        }
        else
          this.mReq = AssetManager.LoadAsync<GameObject>("UI/LoginBonus/" + bonusePrefabName);
      }
    }

    private class State_BeginnerNotify : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        MonoSingleton<GameManager>.Instance.Player.SetBeginnerNotified();
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 2000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mBeginnerNotified)
          return;
        self.mBeginnerNotified = false;
        if (self.mDesirdSceneSet)
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 17);
        }
      }
    }

    private class State_GachaRedraw : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 3000);
      }

      public override void Update(HomeWindow self)
      {
        if (!self.mGachaRedrawNotified)
          return;
        self.mGachaRedrawNotified = false;
        self.MiscBeforeDefaultState();
        self.mStateMachine.GotoState<HomeWindow.State_Default>();
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 3002);
      }
    }
  }
}
