// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WorldRaidManager : MonoBehaviour, IWebHelp
  {
    private static WorldRaidManager mInstance;
    private WorldRaidParam mCurrentWorldRaidParam;
    private List<WorldRaidBossChallengedData> mBosses = new List<WorldRaidBossChallengedData>();
    private int mTotalChallengeUserCount;
    private List<WorldRaidLogData> mLogs = new List<WorldRaidLogData>();
    private float mRefreshWaitSec;
    private float mAutoRefreshWaitSec;

    public static WorldRaidManager Instance => WorldRaidManager.mInstance;

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return;
      WorldRaidManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) this))
        return;
      WorldRaidManager.mInstance = (WorldRaidManager) null;
    }

    private static WorldRaidParam GetInternalCurrentWorldRaidParam()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return (WorldRaidParam) null;
      if (instance.WorldRaidParamList != null)
      {
        foreach (WorldRaidParam worldRaidParam in instance.WorldRaidParamList)
        {
          if (worldRaidParam.IsWithinPeriod())
            return worldRaidParam;
        }
      }
      return (WorldRaidParam) null;
    }

    private void Start()
    {
      this.mCurrentWorldRaidParam = WorldRaidManager.GetInternalCurrentWorldRaidParam();
    }

    [DebuggerHidden]
    private IEnumerator LoadAssetCoroutine<T>(
      string name,
      WorldRaidManager.LoadAssetCallback<T> callback)
      where T : UnityEngine.Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WorldRaidManager.\u003CLoadAssetCoroutine\u003Ec__Iterator0<T>()
      {
        name = name,
        callback = callback
      };
    }

    private bool InternalLoadAssets<T>(string name, WorldRaidManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      if (string.IsNullOrEmpty(name) || callback == null)
        return false;
      this.StartCoroutine(this.LoadAssetCoroutine<T>(name, callback));
      return true;
    }

    public static bool LoadAssets<T>(string name, WorldRaidManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      return UnityEngine.Object.op_Implicit((UnityEngine.Object) WorldRaidManager.mInstance) && WorldRaidManager.mInstance.InternalLoadAssets<T>(name, callback);
    }

    public static WorldRaidParam GetCurrentWorldRaidParam()
    {
      return UnityEngine.Object.op_Implicit((UnityEngine.Object) WorldRaidManager.mInstance) ? WorldRaidManager.mInstance.mCurrentWorldRaidParam : WorldRaidManager.GetInternalCurrentWorldRaidParam();
    }

    public static bool IsWithinPeriod()
    {
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      return currentWorldRaidParam != null && currentWorldRaidParam.IsWithinPeriod();
    }

    public static bool IsWithinChallenge()
    {
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      return currentWorldRaidParam != null && currentWorldRaidParam.IsWithinChallenge();
    }

    public void ClearCasheWorldRaidParam() => this.mCurrentWorldRaidParam = (WorldRaidParam) null;

    public static List<WorldRaidBossChallengedData> GetAllBossesData()
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null) ? (List<WorldRaidBossChallengedData>) null : WorldRaidManager.mInstance.mBosses;
    }

    public static bool IsChallengeLastBoss()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return false;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return false;
      foreach (WorldRaidBossChallengedData mBoss in WorldRaidManager.mInstance.mBosses)
      {
        WorldRaidBossChallengedData data = mBoss;
        WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == data.BossIname));
        if (bossInfo == null || !bossInfo.IsLastBoss && data.CurrentHP > 0L)
          return false;
      }
      return true;
    }

    public static List<WorldRaidBossChallengedData> GetNormalBossData()
    {
      List<WorldRaidBossChallengedData> normalBossData = new List<WorldRaidBossChallengedData>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return normalBossData;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return normalBossData;
      foreach (WorldRaidBossChallengedData mBoss in WorldRaidManager.mInstance.mBosses)
      {
        WorldRaidBossChallengedData data = mBoss;
        WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == data.BossIname));
        if (bossInfo != null && !bossInfo.IsLastBoss)
          normalBossData.Add(data);
      }
      return normalBossData;
    }

    public static List<WorldRaidBossChallengedData> GetLastBossData()
    {
      List<WorldRaidBossChallengedData> lastBossData = new List<WorldRaidBossChallengedData>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return lastBossData;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return lastBossData;
      foreach (WorldRaidBossChallengedData mBoss in WorldRaidManager.mInstance.mBosses)
      {
        WorldRaidBossChallengedData data = mBoss;
        WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == data.BossIname));
        if (bossInfo != null && bossInfo.IsLastBoss)
          lastBossData.Add(data);
      }
      return lastBossData;
    }

    public static List<WorldRaidBossChallengedData> GetChallengedList()
    {
      return WorldRaidManager.IsChallengeLastBoss() ? WorldRaidManager.GetAllBossesData() : WorldRaidManager.GetNormalBossData();
    }

    public static bool IsLastBossDefeat()
    {
      List<WorldRaidBossChallengedData> lastBossData = WorldRaidManager.GetLastBossData();
      if (lastBossData.Count <= 0)
        return false;
      bool flag = true;
      for (int index = 0; index < lastBossData.Count; ++index)
      {
        if (lastBossData[index].CurrentHP > 0L)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static int GetTotalChallengeUserCount()
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null) ? 0 : WorldRaidManager.mInstance.mTotalChallengeUserCount;
    }

    public float GetRefreshWaitSec() => this.mRefreshWaitSec;

    public float GetAutoRefreshWaitSec() => this.mAutoRefreshWaitSec;

    public List<WorldRaidLogData> GetLogData() => this.mLogs;

    public static bool SetAllBossesData(JSON_WorldRaidBossChallengedData[] bosses)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null) || bosses == null)
        return false;
      WorldRaidManager.mInstance.mBosses.Clear();
      foreach (JSON_WorldRaidBossChallengedData boss in bosses)
      {
        WorldRaidBossChallengedData bossChallengedData = new WorldRaidBossChallengedData();
        if (bossChallengedData.Deserialize(boss))
          WorldRaidManager.mInstance.mBosses.Add(bossChallengedData);
      }
      return true;
    }

    public static bool SetTotalChallengeUserCount(int count)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return false;
      WorldRaidManager.mInstance.mTotalChallengeUserCount = count;
      return true;
    }

    public static bool SetLogs(JSON_WorldRaidLogData[] logs)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return false;
      WorldRaidManager.mInstance.mLogs.Clear();
      if (logs != null)
      {
        foreach (JSON_WorldRaidLogData log in logs)
        {
          WorldRaidLogData worldRaidLogData = new WorldRaidLogData();
          if (worldRaidLogData.Deserialize(log))
            WorldRaidManager.mInstance.mLogs.Add(worldRaidLogData);
        }
      }
      return true;
    }

    public static bool SetRefreshTime(float refresh_wait_sec, float auto_refresh_wait_sec)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) WorldRaidManager.mInstance, (UnityEngine.Object) null))
        return false;
      WorldRaidManager.mInstance.mRefreshWaitSec = refresh_wait_sec;
      WorldRaidManager.mInstance.mAutoRefreshWaitSec = auto_refresh_wait_sec;
      return true;
    }

    public static string GetRemainTimeText()
    {
      if (WorldRaidManager.GetCurrentWorldRaidParam() == null)
        return string.Format(LocalizedText.Get("sys.WORLDRAID_REMAIN_TIME"), (object) 0, (object) 0, (object) 0);
      TimeSpan timeSpan = WorldRaidManager.GetCurrentWorldRaidParam().ChallengeEndAt - TimeManager.ServerTime;
      return timeSpan.TotalSeconds > 0.0 ? string.Format(LocalizedText.Get("sys.WORLDRAID_REMAIN_TIME"), (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds) : string.Format(LocalizedText.Get("sys.WORLDRAID_REMAIN_TIME"), (object) 0, (object) 0, (object) 0);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      url = (string) null;
      title = (string) null;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null || string.IsNullOrEmpty(currentWorldRaidParam.DescTitle) || string.IsNullOrEmpty(currentWorldRaidParam.DescURL))
        return false;
      title = currentWorldRaidParam.DescTitle;
      url = currentWorldRaidParam.DescURL;
      return true;
    }

    public delegate void LoadAssetCallback<T>(T obj) where T : UnityEngine.Object;
  }
}
