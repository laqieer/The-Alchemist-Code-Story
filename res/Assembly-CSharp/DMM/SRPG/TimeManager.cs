// Decompiled with JetBrains decompiler
// Type: SRPG.TimeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Time")]
  public class TimeManager : MonoSingleton<TimeManager>
  {
    private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static long UTC2LOCAL = 32400;
    public static readonly int DEFAULT_FRAME_RATE = 30;
    public static readonly float FPS = 1f / (float) TimeManager.DEFAULT_FRAME_RATE;
    public static readonly string ISO_8601_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffZ";
    private float mDeltaTime;
    private float mFixedDeltaTime;
    private float mUnscaledDeltaTime;
    private float mUnscaledFixedDeltaTime;
    private float mTimeScale = 1f;
    private float mReqTimeScale;
    private float mHitStop;
    private float[] mTimeScales = new float[5]
    {
      1f,
      1f,
      1f,
      1f,
      1f
    };

    public static int FrameRate => Application.targetFrameRate;

    public static float RealTimeSinceStartup => Time.realtimeSinceStartup;

    public static float DeltaTime => MonoSingleton<TimeManager>.Instance.mDeltaTime;

    public static float UnscaledDeltaTime => MonoSingleton<TimeManager>.Instance.mUnscaledDeltaTime;

    public static float FixedDeltaTime => MonoSingleton<TimeManager>.Instance.mFixedDeltaTime;

    public static float UnscaledFixedDeltaTime
    {
      get => MonoSingleton<TimeManager>.Instance.mUnscaledFixedDeltaTime;
    }

    public static float TimeScale
    {
      get => MonoSingleton<TimeManager>.Instance.mTimeScale;
      set => MonoSingleton<TimeManager>.Instance.mReqTimeScale = value;
    }

    protected override void Initialize()
    {
      if (!GameUtility.IsDebugBuild)
        Application.targetFrameRate = TimeManager.DEFAULT_FRAME_RATE;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    private void Update()
    {
      if ((double) this.mReqTimeScale != 0.0)
      {
        this.mTimeScale = this.mReqTimeScale;
        this.mReqTimeScale = 0.0f;
      }
      this.mFixedDeltaTime = this.mUnscaledFixedDeltaTime = TimeManager.FPS;
      this.mFixedDeltaTime *= this.mTimeScale;
      this.mDeltaTime = this.mUnscaledDeltaTime = Time.deltaTime;
      this.mDeltaTime *= this.mTimeScale;
      if ((double) this.mHitStop > 0.0)
      {
        this.mHitStop -= Time.unscaledDeltaTime * this.mTimeScales[0];
        if ((double) this.mHitStop <= 0.0)
          TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.HitStop, 1f);
      }
      float mTimeScale = this.mTimeScales[0];
      for (int index = 1; index < this.mTimeScales.Length; ++index)
        mTimeScale *= this.mTimeScales[index];
      Time.timeScale = mTimeScale;
    }

    public static void StartHitSlow(float rate, float sec)
    {
      TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.HitStop, rate);
      MonoSingleton<TimeManager>.Instance.mHitStop = sec;
    }

    public static void SetTimeScale(TimeManager.TimeScaleGroups group, float value)
    {
      MonoSingleton<TimeManager>.Instance.mTimeScales[(int) group] = value;
    }

    public static long Now() => (long) (DateTime.UtcNow - TimeManager.UNIX_EPOCH).TotalSeconds;

    public static DateTime FromUnixTime(long unixtime)
    {
      return TimeManager.UNIX_EPOCH.AddSeconds((double) (unixtime + TimeManager.UTC2LOCAL));
    }

    public static long FromDateTime(DateTime datetime)
    {
      return (long) (datetime - TimeManager.UNIX_EPOCH).TotalSeconds - TimeManager.UTC2LOCAL;
    }

    public static string ToMinSecString(long time)
    {
      string str1 = (time / 60L).ToString();
      string str2 = (time % 60L).ToString();
      return str1.PadLeft(2, '0') + ":" + str2.PadLeft(2, '0');
    }

    public static void HitStop(float sec) => MonoSingleton<TimeManager>.Instance.mHitStop = sec;

    public static DateTime ServerTime => TimeManager.FromUnixTime(Network.GetServerTime());

    public static long GetUnixSec(DateTime targetTime)
    {
      targetTime = targetTime.ToUniversalTime();
      return (long) (targetTime - TimeManager.UNIX_EPOCH).TotalSeconds;
    }

    public enum TimeScaleGroups
    {
      Game,
      HitStop,
      Debug,
      BattleSpeed,
      BattleDebug,
      MAX,
    }
  }
}
