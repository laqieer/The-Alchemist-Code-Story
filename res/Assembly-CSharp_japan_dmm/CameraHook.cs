// Decompiled with JetBrains decompiler
// Type: CameraHook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
public class CameraHook : MonoBehaviour
{
  private AmbientLightSettings mCurrentAmbientVolume;
  private AmbientLightSettings mNextAmbientVolume;
  private AmbientLightSettings.State mCurrentAmbientState;
  private AmbientLightSettings.State mAmbientStateStart;
  private AmbientLightSettings.State mAmbientStateEnd;
  private float mAmbientStateTransition;
  public static CameraHook.PreCullEvent mPreCullEventListeners;
  public static Color ColorMod = Color.white;
  private static CameraHook mInstance = (CameraHook) null;
  private bool mDisableFog = true;

  private void Start()
  {
    this.mCurrentAmbientState = (AmbientLightSettings.State) (AmbientLightSettings) null;
    this.mDisableFog = false;
  }

  public static void Inject()
  {
    Camera main = Camera.main;
    if (!Object.op_Inequality((Object) main, (Object) null))
      return;
    CameraHook.mInstance = ((Component) main).gameObject.RequireComponent<CameraHook>();
  }

  public static void SetDisableFog(bool disable)
  {
    if (!Object.op_Inequality((Object) CameraHook.mInstance, (Object) null))
      return;
    CameraHook.mInstance.mDisableFog = disable;
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
    AmbientLightSettings volume = AmbientLightSettings.FindVolume(((Component) this).transform.position);
    if (Object.op_Inequality((Object) volume, (Object) this.mNextAmbientVolume) && Object.op_Inequality((Object) volume, (Object) this.mCurrentAmbientVolume))
    {
      this.mNextAmbientVolume = volume;
      this.mAmbientStateStart = this.mCurrentAmbientState;
      this.mAmbientStateEnd = (AmbientLightSettings.State) this.mNextAmbientVolume;
      this.mAmbientStateTransition = 0.0f;
    }
    if (!Object.op_Inequality((Object) this.mCurrentAmbientVolume, (Object) this.mNextAmbientVolume))
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
      ((Component) this).GetComponent<Camera>().cullingMask &= ~(1 << GameUtility.LayerHidden);
    if (CameraHook.mPreCullEventListeners == null)
      return;
    CameraHook.mPreCullEventListeners(((Component) this).GetComponent<Camera>());
  }

  protected void OnPreRender()
  {
    if (Object.op_Inequality((Object) CameraHook.mInstance, (Object) null) && CameraHook.mInstance.mDisableFog)
      Shader.SetGlobalVector("_fogParam", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
    else if (!RenderSettings.fog && (double) this.mCurrentAmbientState.FogStartDistance < (double) this.mCurrentAmbientState.FogEndDistance)
    {
      Shader.SetGlobalVector("_fogParam", new Vector4(this.mCurrentAmbientState.FogStartDistance, (float) (1.0 / ((double) this.mCurrentAmbientState.FogEndDistance - (double) this.mCurrentAmbientState.FogStartDistance))));
      Shader.SetGlobalColor("_fogColor", this.mCurrentAmbientState.FogColor);
    }
    else
      Shader.SetGlobalVector("_fogParam", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
    Shader.SetGlobalColor("_ambientLight", this.mCurrentAmbientState.AmbientLightColor);
    Shader.SetGlobalColor("_colorMod", CameraHook.ColorMod);
  }

  private void OnDestroy() => CameraHook.mInstance = (CameraHook) null;

  public delegate void PreCullEvent(Camera camera);
}
