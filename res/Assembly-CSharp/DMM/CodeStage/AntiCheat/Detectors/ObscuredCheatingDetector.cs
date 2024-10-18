// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.ObscuredCheatingDetector
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
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Obscured Cheating Detector")]
  [DisallowMultipleComponent]
  [HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_obscured_cheating_detector.html")]
  public class ObscuredCheatingDetector : ACTkDetectorBase
  {
    internal const string ComponentName = "Obscured Cheating Detector";
    internal const string FinalLogPrefix = "[ACTk] Obscured Cheating Detector: ";
    private static int instancesInScene;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredDouble. Increase in case of false positives.")]
    public double doubleEpsilon = 0.0001;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredFloat. Increase in case of false positives.")]
    public float floatEpsilon = 0.0001f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector2. Increase in case of false positives.")]
    public float vector2Epsilon = 0.1f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector3. Increase in case of false positives.")]
    public float vector3Epsilon = 0.1f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredQuaternion. Increase in case of false positives.")]
    public float quaternionEpsilon = 0.1f;

    private ObscuredCheatingDetector()
    {
    }

    public static ObscuredCheatingDetector AddToSceneOrGetExisting()
    {
      return ObscuredCheatingDetector.GetOrCreateInstance;
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) ObscuredCheatingDetector.Instance, (Object) null))
        ObscuredCheatingDetector.Instance.StartDetectionInternal((Action) null);
      else
        Debug.LogError((object) "[ACTk] Obscured Cheating Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(Action callback)
    {
      ObscuredCheatingDetector.GetOrCreateInstance.StartDetectionInternal(callback);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) ObscuredCheatingDetector.Instance, (Object) null))
        return;
      ObscuredCheatingDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) ObscuredCheatingDetector.Instance, (Object) null))
        return;
      ObscuredCheatingDetector.Instance.DisposeInternal();
    }

    public static ObscuredCheatingDetector Instance { get; private set; }

    private static ObscuredCheatingDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) ObscuredCheatingDetector.Instance, (Object) null))
          return ObscuredCheatingDetector.Instance;
        if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null))
          ACTkDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        ObscuredCheatingDetector.Instance = ACTkDetectorBase.detectorsContainer.AddComponent<ObscuredCheatingDetector>();
        return ObscuredCheatingDetector.Instance;
      }
    }

    internal static bool ExistsAndIsRunning
    {
      get
      {
        return ObscuredCheatingDetector.Instance != null && ObscuredCheatingDetector.Instance.IsRunning;
      }
    }

    private void Awake()
    {
      ++ObscuredCheatingDetector.instancesInScene;
      if (this.Init((ACTkDetectorBase) ObscuredCheatingDetector.Instance, "Obscured Cheating Detector"))
        ObscuredCheatingDetector.Instance = this;
      // ISSUE: method pointer
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --ObscuredCheatingDetector.instancesInScene;
    }

    private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
    {
      if (ObscuredCheatingDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) ObscuredCheatingDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void StartDetectionInternal(Action callback)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Obscured Cheating Detector: already running!", (Object) this);
      else if (!((Behaviour) this).enabled)
      {
        Debug.LogWarning((object) "[ACTk] Obscured Cheating Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Obscured Cheating Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] Obscured Cheating Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).enabled = false;
        }
        else
        {
          this.CheatDetected += callback;
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((Action) null);
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) ObscuredCheatingDetector.Instance, (Object) this))
        return;
      ObscuredCheatingDetector.Instance = (ObscuredCheatingDetector) null;
    }
  }
}
