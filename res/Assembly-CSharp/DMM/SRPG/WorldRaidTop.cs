// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "演出判定", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "更新ボタンクリック", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "自動更新確認開始", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "自動更新確認停止", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(101, "表示更新完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ボス撃破演出", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ラスボス出現演出", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "ボス詳細表示", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "演出判定終了", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(106, "自動更新開始", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(1000, "表示処理エラー時", FlowNode.PinTypes.Output, 1000)]
  public class WorldRaidTop : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_SETUP = 1;
    private const int PIN_INPUT_CHECK_PRODUCTION = 2;
    private const int PIN_INPUT_CLICK_REFRESH = 3;
    private const int PIN_INPUT_CHECK_START = 4;
    private const int PIN_INPUT_CHECK_STOP = 5;
    private const int PIN_OUTPUT_SETUP_FINISH = 101;
    private const int PIN_OUTPUT_BOSS_DIRECTION = 102;
    private const int PIN_OUTPUT_LAST_BOSS_APPEARS = 103;
    private const int PIN_OUTPUT_BOSS_INFO_OPEN = 104;
    private const int PIN_OUTPUT_NO_PRODUCTION = 105;
    private const int PIN_OUTPUT_AUTO_REFRESH_START = 106;
    private const int PIN_OUTPUT_ERROR = 1000;
    [SerializeField]
    private GameObject NormalBossBgObj;
    [SerializeField]
    private GameObject LastBossBgObj;
    [SerializeField]
    private GameObject EventEndObj;
    [SerializeField]
    private WorldRaidBossIcon TemplateWorldRaidBossIcon;
    [SerializeField]
    private WorldRaidBossIcon TemplateWorldRaidLastBossIcon;
    [SerializeField]
    private List<GameObject> WorldRaidBossPoints;
    [SerializeField]
    private List<GameObject> WorldRaidLastBossPoints;
    [SerializeField]
    private Text RemainText;
    [SerializeField]
    private Text ChallengeNum;
    [SerializeField]
    private Text OpeningText;
    [SerializeField]
    private Button RefreshButton;
    [SerializeField]
    private GameObject ParentInitialLogData;
    [SerializeField]
    private GameObject ParentSecondLogData;
    [SerializeField]
    private WorldRaidLogItem TemplateLogData;
    [SerializeField]
    private float CreateLogInterval;
    [SerializeField]
    private int DisplayLogMaxNum;
    [SerializeField]
    private Canvas TargetCanvas;
    [SerializeField]
    private Button TargetButton;
    private List<WorldRaidBossIcon> mBossIcons = new List<WorldRaidBossIcon>();
    private List<WorldRaidBossIcon> mLastBossIcons = new List<WorldRaidBossIcon>();
    private WorldRaidParam mWorldRaidParam;
    private bool mIsUpdateFlag;
    private float mAutoRefreshTime;
    private float mRefreshButtonTime;
    private float mFixAutoRefreshTime;
    private float mFixRefreshButtonTime;
    private bool mIsPlayAnim = true;
    private bool mIsLastBossRefresh;
    private bool mIsChallengeLastBoss;
    private bool mIsOnlyLastBossDefeatAnim;
    private bool mIsCreateLogFlag;
    private float mCreateLogTime;
    private List<WorldRaidLogData> mLogData = new List<WorldRaidLogData>();
    private List<WorldRaidLogItem> mDisplayLogData = new List<WorldRaidLogItem>();
    private RectTransform mTargetRect;
    private List<RaycastResult> mTargetRaycastResults = new List<RaycastResult>();

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.SetUp();
          break;
        case 2:
          this.CheckBossAnim();
          break;
        case 3:
          this.OnClickRefreshButton();
          break;
        case 4:
          this.AutoRefreshCheckStart(false);
          break;
        case 5:
          this.AutoRefreshCheckStop();
          break;
      }
    }

    private void SetUp()
    {
      this.mWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (this.mWorldRaidParam == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidManager.Instance, (UnityEngine.Object) null))
        {
          if ((this.mWorldRaidParam.EndAt - TimeManager.ServerTime).TotalSeconds <= 0.0)
          {
            WorldRaidManager.Instance.ClearCasheWorldRaidParam();
            this.mWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
          }
          if (this.mWorldRaidParam == null)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
            return;
          }
          this.mFixAutoRefreshTime = WorldRaidManager.Instance.GetAutoRefreshWaitSec();
          this.mFixRefreshButtonTime = WorldRaidManager.Instance.GetRefreshWaitSec();
        }
        this.mIsChallengeLastBoss = WorldRaidManager.IsChallengeLastBoss();
        TimeSpan timeSpan = this.mWorldRaidParam.ChallengeEndAt - TimeManager.ServerTime;
        GameUtility.SetGameObjectActive(this.EventEndObj, timeSpan.TotalSeconds <= 0.0);
        WorldRaidNoticeData worldRaidNoticeData = WorldRaidNoticeData.Load(this.mWorldRaidParam);
        bool _is_force_normal = worldRaidNoticeData != null && worldRaidNoticeData.IsNeedNotice_LastBoss() && this.mIsChallengeLastBoss;
        if (timeSpan.TotalSeconds <= 0.0)
        {
          this.ClearNormalBossIcon();
          this.ClearLastBossIcon();
          this.mIsUpdateFlag = false;
          _is_force_normal = false;
          this.mIsPlayAnim = false;
        }
        else
        {
          this.mIsUpdateFlag = true;
          if (WorldRaidManager.IsLastBossDefeat())
          {
            _is_force_normal = false;
            this.mIsOnlyLastBossDefeatAnim = true;
          }
          this.SetBossIcon(_is_force_normal);
        }
        this.SetBG(_is_force_normal);
        this.SetRemainTime();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallengeNum, (UnityEngine.Object) null))
          this.ChallengeNum.text = WorldRaidManager.GetTotalChallengeUserCount().ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OpeningText, (UnityEngine.Object) null))
        {
          DateTime challengeEndAt = this.mWorldRaidParam.ChallengeEndAt;
          this.OpeningText.text = string.Format(LocalizedText.Get("sys.WORLDRAID_OPENING_TIME"), (object) challengeEndAt.Month, (object) challengeEndAt.Day, (object) challengeEndAt.Hour, (object) challengeEndAt.Minute, (object) challengeEndAt.Second);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetButton, (UnityEngine.Object) null))
          this.mTargetRect = ((Component) this.TargetButton).GetComponent<RectTransform>();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void CheckBossAnim()
    {
      if (this.mIsPlayAnim)
      {
        DataSource.Clear(((Component) this).gameObject);
        WorldRaidNoticeData notice_data = WorldRaidNoticeData.Load(this.mWorldRaidParam);
        if (notice_data == null)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
          return;
        }
        if (!this.mIsOnlyLastBossDefeatAnim)
        {
          List<WorldRaidBossChallengedData> allBossesData = WorldRaidManager.GetAllBossesData();
          for (int index = 0; index < allBossesData.Count; ++index)
          {
            if (allBossesData[index].CurrentHP <= 0L && allBossesData[index].Challenged && notice_data.IsNeedNotice_BeatBoss(index))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
              return;
            }
          }
          if (this.mIsChallengeLastBoss && notice_data.IsNeedNotice_LastBoss())
          {
            List<WorldRaidBossChallengedData> lastBossData = WorldRaidManager.GetLastBossData();
            if (lastBossData.Count > 0)
            {
              bool flag = false;
              foreach (WorldRaidBossChallengedData bossChallengedData in lastBossData)
              {
                if (bossChallengedData.CurrentHP > 0L && !bossChallengedData.Challenged)
                {
                  flag = true;
                  break;
                }
              }
              notice_data.SetLastBoss(true);
              WorldRaidNoticeData.Save(notice_data);
              this.mIsLastBossRefresh = true;
              this.ClearNormalBossIcon();
              if (flag)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
                return;
              }
            }
          }
        }
        else
        {
          if (notice_data.IsNeedNotice_LastBoss())
          {
            notice_data.SetLastBoss(true);
            WorldRaidNoticeData.Save(notice_data);
          }
          List<WorldRaidBossChallengedData> boss_data = WorldRaidManager.GetAllBossesData();
          for (int i = 0; i < boss_data.Count; ++i)
          {
            if (boss_data[i].CurrentHP <= 0L && notice_data.IsNeedNotice_BeatBoss(i))
            {
              WorldRaidParam.BossInfo bossInfo = this.mWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == boss_data[i].BossIname));
              if (bossInfo != null)
              {
                if (!bossInfo.IsLastBoss || !boss_data[i].Challenged)
                {
                  notice_data.SetBoss(true, i);
                  WorldRaidNoticeData.Save(notice_data);
                }
                else
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
                  return;
                }
              }
            }
          }
        }
        if (HomeWindow.GetRestorePoint() == RestorePoints.WorldRaid)
        {
          HomeWindow.SetRestorePoint(RestorePoints.Home);
          string iname = GlobalVars.CurrentRaidBossIname.Get();
          if (!string.IsNullOrEmpty(iname) && WorldRaidManager.GetAllBossesData() != null && this.mWorldRaidParam != null)
          {
            WorldRaidBossChallengedData bossChallengedData = WorldRaidManager.GetAllBossesData().Find((Predicate<WorldRaidBossChallengedData>) (x => x.BossIname == iname));
            if (bossChallengedData != null && bossChallengedData.CurrentHP > 0L)
            {
              WorldRaidBossManager.SetBossIndex(this.mWorldRaidParam.BossInfoList.FindIndex((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == iname)));
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
              return;
            }
          }
        }
        if (this.mIsLastBossRefresh)
        {
          this.SetBossIcon();
          this.mIsLastBossRefresh = false;
        }
      }
      if (this.mIsUpdateFlag)
      {
        this.SetUpLogData();
        this.AutoRefreshCheckStart(true);
      }
      else
      {
        this.AllClearLog();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RefreshButton, (UnityEngine.Object) null))
          ((Selectable) this.RefreshButton).interactable = false;
      }
      this.mIsPlayAnim = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
    }

    private void SetBossIcon(bool _is_force_normal = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TemplateWorldRaidBossIcon, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TemplateWorldRaidLastBossIcon, (UnityEngine.Object) null))
        return;
      if (!this.mIsChallengeLastBoss || _is_force_normal)
      {
        this.ClearLastBossIcon();
        List<WorldRaidBossChallengedData> normalBossData = WorldRaidManager.GetNormalBossData();
        if (this.mBossIcons.Count > 0)
        {
          for (int index = 0; index < normalBossData.Count; ++index)
          {
            if (index < this.mBossIcons.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBossIcons[index], (UnityEngine.Object) null))
              this.mBossIcons[index].SetUp(normalBossData[index]);
          }
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TemplateWorldRaidBossIcon, (UnityEngine.Object) null))
            return;
          for (int index = 0; index < normalBossData.Count && this.WorldRaidBossPoints.Count > index; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WorldRaidBossPoints[index], (UnityEngine.Object) null))
            {
              WorldRaidBossIcon worldRaidBossIcon = UnityEngine.Object.Instantiate<WorldRaidBossIcon>(this.TemplateWorldRaidBossIcon, this.WorldRaidBossPoints[index].transform, false);
              ((Component) worldRaidBossIcon).gameObject.SetActive(true);
              worldRaidBossIcon.SetUp(normalBossData[index]);
              this.mBossIcons.Add(worldRaidBossIcon);
            }
          }
        }
      }
      else
      {
        this.ClearNormalBossIcon();
        List<WorldRaidBossChallengedData> lastBossData = WorldRaidManager.GetLastBossData();
        if (this.mLastBossIcons.Count > 0)
        {
          for (int index = 0; index < lastBossData.Count; ++index)
          {
            if (index < this.mLastBossIcons.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLastBossIcons[index], (UnityEngine.Object) null))
              this.mLastBossIcons[index].SetUp(lastBossData[index]);
          }
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TemplateWorldRaidLastBossIcon, (UnityEngine.Object) null))
            return;
          for (int index = 0; index < lastBossData.Count && this.WorldRaidLastBossPoints.Count > index; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WorldRaidLastBossPoints[index], (UnityEngine.Object) null))
            {
              WorldRaidBossIcon worldRaidBossIcon = UnityEngine.Object.Instantiate<WorldRaidBossIcon>(this.TemplateWorldRaidLastBossIcon, this.WorldRaidLastBossPoints[index].transform, false);
              ((Component) worldRaidBossIcon).gameObject.SetActive(true);
              worldRaidBossIcon.SetUp(lastBossData[index]);
              this.mLastBossIcons.Add(worldRaidBossIcon);
            }
          }
        }
      }
    }

    private void SetBG(bool _is_force_normal = false)
    {
      GameUtility.SetGameObjectActive(((Component) this.TemplateWorldRaidBossIcon).gameObject, false);
      GameUtility.SetGameObjectActive(((Component) this.TemplateWorldRaidLastBossIcon).gameObject, false);
      if (!this.mIsChallengeLastBoss || _is_force_normal)
      {
        GameUtility.SetGameObjectActive(this.NormalBossBgObj, true);
        GameUtility.SetGameObjectActive(this.LastBossBgObj, false);
      }
      else
      {
        GameUtility.SetGameObjectActive(this.NormalBossBgObj, false);
        GameUtility.SetGameObjectActive(this.LastBossBgObj, true);
      }
    }

    private void ClearNormalBossIcon()
    {
      if (this.mBossIcons.Count <= 0)
        return;
      foreach (Component mBossIcon in this.mBossIcons)
        UnityEngine.Object.Destroy((UnityEngine.Object) mBossIcon.gameObject);
      this.mBossIcons.Clear();
    }

    private void ClearLastBossIcon()
    {
      if (this.mLastBossIcons.Count <= 0)
        return;
      foreach (Component mLastBossIcon in this.mLastBossIcons)
        UnityEngine.Object.Destroy((UnityEngine.Object) mLastBossIcon.gameObject);
      this.mLastBossIcons.Clear();
    }

    private void OnClickRefreshButton()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RefreshButton, (UnityEngine.Object) null) || (double) this.mFixRefreshButtonTime <= 0.0)
        return;
      ((Selectable) this.RefreshButton).interactable = false;
      this.mRefreshButtonTime = 0.0f;
    }

    private void AutoRefreshCheckStart(bool _is_time_refresh)
    {
      if (_is_time_refresh)
      {
        this.mAutoRefreshTime = 0.0f;
        this.mRefreshButtonTime = 0.0f;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RefreshButton, (UnityEngine.Object) null))
          ((Selectable) this.RefreshButton).interactable = false;
      }
      this.mIsUpdateFlag = true;
    }

    private void AutoRefreshCheckStop() => this.mIsUpdateFlag = false;

    private void SetRemainTime()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainText, (UnityEngine.Object) null))
        return;
      this.RemainText.text = WorldRaidManager.GetRemainTimeText();
    }

    private void SetUpLogData()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.Instance, (UnityEngine.Object) null))
        return;
      int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_WORLD_RAID_LOG_ID);
      List<WorldRaidLogData> logData = WorldRaidManager.Instance.GetLogData();
      foreach (WorldRaidLogData worldRaidLogData in logData)
      {
        WorldRaidLogData data = worldRaidLogData;
        if (data.LogId > num)
        {
          WorldRaidParam.BossInfo bossInfo = this.mWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == data.BossIname));
          if (!this.mIsChallengeLastBoss || bossInfo.IsLastBoss)
            this.mLogData.Add(data);
        }
      }
      if (this.mLogData.Count == 0 && this.mDisplayLogData.Count == 0)
      {
        foreach (WorldRaidLogData worldRaidLogData in logData)
        {
          WorldRaidLogData data = worldRaidLogData;
          WorldRaidParam.BossInfo bossInfo = this.mWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == data.BossIname));
          if (!this.mIsChallengeLastBoss || bossInfo.IsLastBoss)
          {
            this.mLogData.Add(data);
            break;
          }
        }
      }
      if (this.mLogData.Count > 0)
      {
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_WORLD_RAID_LOG_ID, logData[logData.Count - 1].LogId);
        this.mIsCreateLogFlag = true;
        this.mCreateLogTime = 0.0f;
      }
      else
        this.mIsCreateLogFlag = false;
    }

    private void CreateLogData()
    {
      this.mCreateLogTime = 0.0f;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ParentInitialLogData, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ParentSecondLogData, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TemplateLogData, (UnityEngine.Object) null))
        return;
      if (this.mLogData.Count > 0)
      {
        if (this.mLogData[0] != null)
        {
          foreach (WorldRaidLogItem worldRaidLogItem in this.mDisplayLogData)
            worldRaidLogItem.ItemMove();
          WorldRaidLogItem worldRaidLogItem1 = UnityEngine.Object.Instantiate<WorldRaidLogItem>(this.TemplateLogData, this.ParentInitialLogData.transform);
          worldRaidLogItem1.SetUp(this.mLogData[0], this.ParentSecondLogData.transform);
          ((Component) worldRaidLogItem1).gameObject.SetActive(true);
          this.mDisplayLogData.Add(worldRaidLogItem1);
          if (this.mDisplayLogData.Count > this.DisplayLogMaxNum)
          {
            this.mDisplayLogData[0].RemoveItem();
            this.mDisplayLogData.RemoveAt(0);
          }
        }
        this.mLogData.RemoveAt(0);
        if (this.mLogData.Count != 0)
          return;
        this.mLogData.Clear();
      }
      else
        this.mIsCreateLogFlag = false;
    }

    private void AllClearLog()
    {
      foreach (WorldRaidLogItem worldRaidLogItem in this.mDisplayLogData)
        worldRaidLogItem.RemoveItem();
      this.mDisplayLogData.Clear();
    }

    private bool IsTargetRaycast()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetButton, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetCanvas, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTargetRect, (UnityEngine.Object) null))
        return true;
      PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
      {
        position = RectTransformUtility.WorldToScreenPoint(this.TargetCanvas.worldCamera, ((Transform) this.mTargetRect).position)
      };
      this.mTargetRaycastResults.Clear();
      EventSystem.current.RaycastAll(pointerEventData, this.mTargetRaycastResults);
      if (this.mTargetRaycastResults.Count > 0)
      {
        RaycastResult targetRaycastResult = this.mTargetRaycastResults[0];
        if (!(((UnityEngine.Object) ((RaycastResult) ref targetRaycastResult).gameObject).name != ((UnityEngine.Object) ((Component) this.TargetButton).gameObject).name))
          return true;
      }
      return false;
    }

    private void Update()
    {
      if (!this.mIsUpdateFlag)
        return;
      this.SetRemainTime();
      if ((double) this.mAutoRefreshTime < (double) this.mFixAutoRefreshTime)
        this.mAutoRefreshTime += Time.deltaTime;
      if (!this.IsTargetRaycast())
        return;
      if ((double) this.mFixAutoRefreshTime > 0.0 && (double) this.mAutoRefreshTime >= (double) this.mFixAutoRefreshTime)
      {
        this.mIsPlayAnim = true;
        this.mIsUpdateFlag = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
      }
      this.mRefreshButtonTime += Time.deltaTime;
      if ((double) this.mFixRefreshButtonTime > 0.0 && (double) this.mRefreshButtonTime >= (double) this.mFixRefreshButtonTime)
        GameUtility.SetButtonIntaractable(this.RefreshButton, true);
      if (!this.mIsCreateLogFlag)
        return;
      this.mCreateLogTime += Time.deltaTime;
      if ((double) this.CreateLogInterval <= 0.0 || (double) this.mCreateLogTime < (double) this.CreateLogInterval)
        return;
      this.CreateLogData();
    }
  }
}
