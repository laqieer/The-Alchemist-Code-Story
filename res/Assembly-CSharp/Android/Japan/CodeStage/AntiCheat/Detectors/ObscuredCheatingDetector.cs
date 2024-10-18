// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.ObscuredCheatingDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Obscured Cheating Detector")]
  public class ObscuredCheatingDetector : ActDetectorBase
  {
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredFloat. Increase in case of false positives.")]
    public float floatEpsilon = 0.0001f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector2. Increase in case of false positives.")]
    public float vector2Epsilon = 0.1f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector3. Increase in case of false positives.")]
    public float vector3Epsilon = 0.1f;
    [Tooltip("Max allowed difference between encrypted and fake values in ObscuredQuaternion. Increase in case of false positives.")]
    public float quaternionEpsilon = 0.1f;
    internal const string COMPONENT_NAME = "Obscured Cheating Detector";
    internal const string FINAL_LOG_PREFIX = "[ACTk] Obscured Cheating Detector: ";
    private static int instancesInScene;

    private ObscuredCheatingDetector()
    {
    }

    public static void StartDetection()
    {
      if ((Object) ObscuredCheatingDetector.Instance != (Object) null)
        ObscuredCheatingDetector.Instance.StartDetectionInternal((UnityAction) null);
      else
        Debug.LogError((object) "[ACTk] Obscured Cheating Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(UnityAction callback)
    {
      ObscuredCheatingDetector.GetOrCreateInstance.StartDetectionInternal(callback);
    }

    public static void StopDetection()
    {
      if (!((Object) ObscuredCheatingDetector.Instance != (Object) null))
        return;
      ObscuredCheatingDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!((Object) ObscuredCheatingDetector.Instance != (Object) null))
        return;
      ObscuredCheatingDetector.Instance.DisposeInternal();
    }

    public static ObscuredCheatingDetector Instance { get; private set; }

    private static ObscuredCheatingDetector GetOrCreateInstance
    {
      get
      {
        if ((Object) ObscuredCheatingDetector.Instance != (Object) null)
          return ObscuredCheatingDetector.Instance;
        if ((Object) ActDetectorBase.detectorsContainer == (Object) null)
          ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        ObscuredCheatingDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<ObscuredCheatingDetector>();
        return ObscuredCheatingDetector.Instance;
      }
    }

    internal static bool IsRunning
    {
      get
      {
        if (ObscuredCheatingDetector.Instance != null)
          return ObscuredCheatingDetector.Instance.isRunning;
        return false;
      }
    }

    private void Awake()
    {
      ++ObscuredCheatingDetector.instancesInScene;
      if (this.Init((ActDetectorBase) ObscuredCheatingDetector.Instance, "Obscured Cheating Detector"))
        ObscuredCheatingDetector.Instance = this;
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.LoadedLevel);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --ObscuredCheatingDetector.instancesInScene;
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.LoadedLevel);
    }

    private void LoadedLevel(Scene scenename, LoadSceneMode SceneMode)
    {
      if (ObscuredCheatingDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !((Object) ObscuredCheatingDetector.Instance != (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void StartDetectionInternal(UnityAction callback)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Obscured Cheating Detector: already running!", (Object) this);
      else if (!this.enabled)
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
          this.enabled = false;
        }
        else
        {
          this.detectionAction = callback;
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((UnityAction) null);
    }

    protected override void PauseDetector()
    {
      this.isRunning = false;
    }

    protected override void ResumeDetector()
    {
      if (this.detectionAction == null && !this.detectionEventHasListener)
        return;
      this.isRunning = true;
    }

    protected override void StopDetectionInternal()
    {
      if (!this.started)
        return;
      this.detectionAction = (UnityAction) null;
      this.started = false;
      this.isRunning = false;
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!((Object) ObscuredCheatingDetector.Instance == (Object) this))
        return;
      ObscuredCheatingDetector.Instance = (ObscuredCheatingDetector) null;
    }
  }
}
