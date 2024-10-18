// Decompiled with JetBrains decompiler
// Type: RenkeiCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
public class RenkeiCamera : MonoBehaviour
{
  private RenderTexture mRT;
  private bool mRTDirty;
  private Transform mBackground;
  private float mRenderWidth = 1f;
  private float mRenderHeight = 1f;

  public bool IsSceneTextureDirty => this.mRTDirty;

  public RenderTexture SceneTexture => this.mRT;

  public Color FillColor
  {
    set => ((Component) this).GetComponent<Camera>().backgroundColor = value;
  }

  private void ReleaseRenderTarget()
  {
    RenderTexture.ReleaseTemporary(this.mRT);
    this.mRT = (RenderTexture) null;
  }

  public void SetRenderSize(float w, float h)
  {
    this.mRenderWidth = w;
    this.mRenderHeight = h;
    Camera component = ((Component) this).GetComponent<Camera>();
    int num1 = (int) ((double) Screen.width * (double) this.mRenderWidth);
    int num2 = (int) ((double) Screen.height * (double) this.mRenderHeight);
    this.ReleaseRenderTarget();
    this.mRT = RenderTexture.GetTemporary(num1, num2, 16);
    component.useOcclusionCulling = false;
    component.targetTexture = this.mRT;
    component.rect = new Rect(0.0f, 0.0f, 1f, 1f);
    component.clearFlags = (CameraClearFlags) 2;
  }

  public void SetBackgroundTemplate(GameObject backgroundTemplate)
  {
    GameUtility.DestroyGameObject((Component) this.mBackground);
    this.mBackground = (Transform) null;
    Transform transform = backgroundTemplate.transform;
    this.mBackground = Object.Instantiate<GameObject>(backgroundTemplate, transform.position, transform.rotation).transform;
    this.mBackground.SetParent(((Component) this).transform, false);
    GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
  }

  private void Start()
  {
    if (!Object.op_Equality((Object) this.mRT, (Object) null))
      return;
    this.SetRenderSize(this.mRenderWidth, this.mRenderHeight);
  }

  private void OnDestroy() => this.ReleaseRenderTarget();

  private void OnPreCull()
  {
    if (!Object.op_Inequality((Object) this.mBackground, (Object) null))
      return;
    GameUtility.SetLayer((Component) this.mBackground, ((Component) this).gameObject.layer, true);
  }

  private void OnPostRender()
  {
    if (Object.op_Inequality((Object) this.mBackground, (Object) null))
      GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
    this.mRTDirty = true;
  }
}
