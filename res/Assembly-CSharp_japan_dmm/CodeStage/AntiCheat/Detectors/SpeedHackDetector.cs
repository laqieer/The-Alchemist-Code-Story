// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.SpeedHackDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#nullable disable
namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Speed Hack Detector")]
  [DisallowMultipleComponent]
  [HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_speed_hack_detector.html")]
  public class SpeedHackDetector : ACTkDetectorBase
  {
    internal const string ComponentName = "Speed Hack Detector";
    internal const string LogPrefix = "[ACTk] Speed Hack Detector: ";
    private const long TicksPerSecond = 10000000;
    private const int Threshold = 5000000;
    private const float ThresholdFloat = 0.5f;
    private static int instancesInScene;
    [Tooltip("Time (in seconds) between detector checks.")]
    public float interval = 1f;
    [Tooltip("Maximum false positives count allowed before registering speed hack.")]
    public byte maxFalsePositives = 3;
    [Tooltip("Amount of sequential successful checks before clearing internal false positives counter.\nSet 0 to disable Cool Down feature.")]
    public int coolDown = 30;
    private byte currentFalsePositives;
    private int currentCooldownShots;
    private long ticksOnStart;
    private long vulnerableTicksOnStart;
    private long previousTicks;
    private long previousIntervalTicks;
    private float vulnerableTimeOnStart;

    private SpeedHackDetector()
    {
    }

    public static SpeedHackDetector AddToSceneOrGetExisting()
    {
      return SpeedHackDetector.GetOrCreateInstance;
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        SpeedHackDetector.Instance.StartDetectionInternal((Action) null, SpeedHackDetector.Instance.interval, SpeedHackDetector.Instance.maxFalsePositives, SpeedHackDetector.Instance.coolDown);
      else
        Debug.LogError((object) "[ACTk] Speed Hack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(Action callback)
    {
      SpeedHackDetector.StartDetection(callback, SpeedHackDetector.GetOrCreateInstance.interval);
    }

    public static void StartDetection(Action callback, float interval)
    {
      SpeedHackDetector.StartDetection(callback, interval, SpeedHackDetector.GetOrCreateInstance.maxFalsePositives);
    }

    public static void StartDetection(Action callback, float interval, byte maxFalsePositives)
    {
      SpeedHackDetector.StartDetection(callback, interval, maxFalsePositives, SpeedHackDetector.GetOrCreateInstance.coolDown);
    }

    public static void StartDetection(
      Action callback,
      float interval,
      byte maxFalsePositives,
      int coolDown)
    {
      SpeedHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, interval, maxFalsePositives, coolDown);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        return;
      SpeedHackDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        return;
      SpeedHackDetector.Instance.DisposeInternal();
    }

    public static SpeedHackDetector Instance { get; private set; }

    private static SpeedHackDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
          return SpeedHackDetector.Instance;
        if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null))
          ACTkDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        SpeedHackDetector.Instance = ACTkDetectorBase.detectorsContainer.AddComponent<SpeedHackDetector>();
        return SpeedHackDetector.Instance;
      }
    }

    private void Awake()
    {
      ++SpeedHackDetector.instancesInScene;
      if (this.Init((ACTkDetectorBase) SpeedHackDetector.Instance, "Speed Hack Detector"))
        SpeedHackDetector.Instance = this;
      // ISSUE: method pointer
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --SpeedHackDetector.instancesInScene;
    }

    private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
    {
      if (SpeedHackDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void OnApplicationPause(bool pause)
    {
      if (pause)
        return;
      this.ResetStartTicks();
    }

    private void Update()
    {
      if (!this.isRunning)
        return;
      long reliableTicks = this.GetReliableTicks();
      long num1 = reliableTicks - this.previousTicks;
      if (num1 < 0L || num1 > 10000000L)
      {
        this.ResetStartTicks();
      }
      else
      {
        this.previousTicks = reliableTicks;
        long num2 = (long) ((double) this.interval * 10000000.0);
        if (reliableTicks - this.previousIntervalTicks < num2)
          return;
        long num3 = reliableTicks - this.ticksOnStart;
        bool flag1 = Math.Abs((long) Environment.TickCount * 10000L - this.vulnerableTicksOnStart - num3) > 5000000L;
        float num4 = Time.realtimeSinceStartup - this.vulnerableTimeOnStart;
        bool flag2 = (double) Math.Abs((float) num3 / 1E+07f - num4) > 0.5;
        if (flag1 || flag2)
        {
          ++this.currentFalsePositives;
          if ((int) this.currentFalsePositives > (int) this.maxFalsePositives)
          {
            this.OnCheatingDetected();
          }
          else
          {
            this.currentCooldownShots = 0;
            this.ResetStartTicks();
          }
        }
        else if (this.currentFalsePositives > (byte) 0 && this.coolDown > 0)
        {
          ++this.currentCooldownShots;
          if (this.currentCooldownShots >= this.coolDown)
            this.currentFalsePositives = (byte) 0;
        }
        this.previousIntervalTicks = reliableTicks;
      }
    }

    private void StartDetectionInternal(
      Action callback,
      float checkInterval,
      byte falsePositives,
      int shotsTillCooldown)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Speed Hack Detector: already running!", (Object) this);
      else if (!((Behaviour) this).enabled)
      {
        Debug.LogWarning((object) "[ACTk] Speed Hack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Speed Hack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] Speed Hack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).enabled = false;
        }
        else
        {
          this.CheatDetected += callback;
          this.interval = checkInterval;
          this.maxFalsePositives = falsePositives;
          this.coolDown = shotsTillCooldown;
          this.ResetStartTicks();
          this.currentFalsePositives = (byte) 0;
          this.currentCooldownShots = 0;
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((Action) null, this.interval, this.maxFalsePositives, this.coolDown);
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) SpeedHackDetector.Instance, (Object) this))
        return;
      SpeedHackDetector.Instance = (SpeedHackDetector) null;
    }

    private void ResetStartTicks()
    {
      this.ticksOnStart = this.GetReliableTicks();
      this.vulnerableTicksOnStart = (long) Environment.TickCount * 10000L;
      this.previousTicks = this.ticksOnStart;
      this.previousIntervalTicks = this.ticksOnStart;
      this.vulnerableTimeOnStart = Time.realtimeSinceStartup;
    }

    private long GetReliableTicks()
    {
      long reliableTicks = 0;
      if (reliableTicks == 0L)
        reliableTicks = DateTime.UtcNow.Ticks;
      return reliableTicks;
    }
  }
}
