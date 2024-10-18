// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.ACTkDetectorBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("")]
  public abstract class ACTkDetectorBase : MonoBehaviour
  {
    protected const string ContainerName = "Anti-Cheat Toolkit Detectors";
    protected const string MenuPath = "Code Stage/Anti-Cheat Toolkit/";
    protected const string GameObjectMenuPath = "GameObject/Create Other/Code Stage/Anti-Cheat Toolkit/";
    protected static GameObject detectorsContainer;
    [Tooltip("Automatically start detector. Detection Event will be called on detection.")]
    public bool autoStart = true;
    [Tooltip("Detector will survive new level (scene) load if checked.")]
    public bool keepAlive = true;
    [Tooltip("Automatically dispose Detector after firing callback.")]
    public bool autoDispose = true;
    [SerializeField]
    protected UnityEvent detectionEvent;
    [SerializeField]
    protected bool detectionEventHasListener;
    protected bool started;
    protected bool isRunning;

    public event Action CheatDetected;

    public bool IsRunning => this.isRunning;

    private void Start()
    {
      if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null) && ((Object) ((Component) this).gameObject).name == "Anti-Cheat Toolkit Detectors")
        ACTkDetectorBase.detectorsContainer = ((Component) this).gameObject;
      if (!this.autoStart || this.started)
        return;
      this.StartDetectionAutomatically();
    }

    private void OnEnable() => this.ResumeDetector();

    private void OnDisable() => this.PauseDetector();

    private void OnApplicationQuit() => this.DisposeInternal();

    protected virtual void OnDestroy()
    {
      this.StopDetectionInternal();
      if (((Component) this).transform.childCount == 0 && ((Component) this).GetComponentsInChildren<Component>().Length <= 2)
      {
        Object.Destroy((Object) ((Component) this).gameObject);
      }
      else
      {
        if (!(((Object) this).name == "Anti-Cheat Toolkit Detectors") || ((Component) this).GetComponentsInChildren<ACTkDetectorBase>().Length > 1)
          return;
        Object.Destroy((Object) ((Component) this).gameObject);
      }
    }

    internal virtual void OnCheatingDetected()
    {
      if (this.CheatDetected != null)
        this.CheatDetected();
      if (this.detectionEventHasListener)
        this.detectionEvent.Invoke();
      if (this.autoDispose)
        this.DisposeInternal();
      else
        this.StopDetectionInternal();
    }

    protected virtual bool Init(ACTkDetectorBase instance, string detectorName)
    {
      if (Object.op_Inequality((Object) instance, (Object) null) && Object.op_Inequality((Object) instance, (Object) this) && instance.keepAlive)
      {
        Debug.LogWarning((object) ("[ACTk] " + ((Object) this).name + ": self-destroying, other instance already exists & only one instance allowed!"), (Object) ((Component) this).gameObject);
        Object.Destroy((Object) this);
        return false;
      }
      Object.DontDestroyOnLoad(!Object.op_Inequality((Object) ((Component) this).transform.parent, (Object) null) ? (Object) ((Component) this).gameObject : (Object) ((Component) ((Component) this).transform.root).gameObject);
      return true;
    }

    protected virtual void DisposeInternal() => Object.Destroy((Object) this);

    protected virtual bool DetectorHasCallbacks()
    {
      return this.CheatDetected != null || this.detectionEventHasListener;
    }

    protected virtual void StopDetectionInternal()
    {
      this.CheatDetected = (Action) null;
      this.started = false;
      this.isRunning = false;
    }

    protected virtual void PauseDetector()
    {
      if (!this.started)
        return;
      this.isRunning = false;
    }

    protected virtual bool ResumeDetector()
    {
      if (!this.started || !this.DetectorHasCallbacks())
        return false;
      this.isRunning = true;
      return true;
    }

    protected abstract void StartDetectionAutomatically();
  }
}
