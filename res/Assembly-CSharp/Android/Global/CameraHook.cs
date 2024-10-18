// Decompiled with JetBrains decompiler
// Type: CameraHook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
public class CameraHook : MonoBehaviour
{
  public static Color ColorMod = Color.white;
  private AmbientLightSettings mCurrentAmbientVolume;
  private AmbientLightSettings mNextAmbientVolume;
  private AmbientLightSettings.State mCurrentAmbientState;
  private AmbientLightSettings.State mAmbientStateStart;
  private AmbientLightSettings.State mAmbientStateEnd;
  private float mAmbientStateTransition;
  public static CameraHook.PreCullEvent mPreCullEventListeners;

  private void Start()
  {
    this.mCurrentAmbientState = (AmbientLightSettings.State) ((AmbientLightSettings) null);
  }

  public static void Inject()
  {
    Camera main = Camera.main;
    if (!((Object) main != (Object) null))
      return;
    main.gameObject.RequireComponent<CameraHook>();
  }

  public static void AddPreCullEventListener(CameraHook.PreCullEvent e)
  {
    if (e == null)
      return;
    if (Application.isPlaying)
      CameraHook.Inject();
    CameraHook.mPreCullEventListeners += e;
  }

  public static void RemovePreCullEventListener(CameraHook.PreCullEvent e)
  {
    if (e == null)
      return;
    CameraHook.mPreCullEventListeners -= e;
  }

  private void LateUpdate()
  {
    AmbientLightSettings volume = AmbientLightSettings.FindVolume(this.transform.position);
    if ((Object) volume != (Object) this.mNextAmbientVolume && (Object) volume != (Object) this.mCurrentAmbientVolume)
    {
      this.mNextAmbientVolume = volume;
      this.mAmbientStateStart = this.mCurrentAmbientState;
      this.mAmbientStateEnd = (AmbientLightSettings.State) this.mNextAmbientVolume;
      this.mAmbientStateTransition = 0.0f;
    }
    if (!((Object) this.mCurrentAmbientVolume != (Object) this.mNextAmbientVolume))
      return;
    this.mAmbientStateTransition = Mathf.Clamp01(this.mAmbientStateTransition + Time.deltaTime);
    this.mCurrentAmbientState = AmbientLightSettings.State.Lerp(this.mAmbientStateStart, this.mAmbientStateEnd, this.mAmbientStateTransition);
    if ((double) this.mAmbientStateTransition < 1.0)
      return;
    this.mCurrentAmbientVolume = this.mNextAmbientVolume;
    this.mNextAmbientVolume = (AmbientLightSettings) null;
  }

  private void OnPreCull()
  {
    if (Application.isPlaying)
      this.GetComponent<Camera>().cullingMask &= ~(1 << GameUtility.LayerHidden);
    if (CameraHook.mPreCullEventListeners == null)
      return;
    CameraHook.mPreCullEventListeners(this.GetComponent<Camera>());
  }

  protected void OnPreRender()
  {
    if (!RenderSettings.fog && (double) this.mCurrentAmbientState.FogStartDistance < (double) this.mCurrentAmbientState.FogEndDistance)
    {
      Shader.SetGlobalVector("_fogParam", new Vector4(this.mCurrentAmbientState.FogStartDistance, (float) (1.0 / ((double) this.mCurrentAmbientState.FogEndDistance - (double) this.mCurrentAmbientState.FogStartDistance))));
      Shader.SetGlobalColor("_fogColor", this.mCurrentAmbientState.FogColor);
    }
    else
      Shader.SetGlobalVector("_fogParam", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
    Shader.SetGlobalColor("_ambientLight", this.mCurrentAmbientState.AmbientLightColor);
    Shader.SetGlobalColor("_colorMod", CameraHook.ColorMod);
  }

  public delegate void PreCullEvent(Camera camera);
}
