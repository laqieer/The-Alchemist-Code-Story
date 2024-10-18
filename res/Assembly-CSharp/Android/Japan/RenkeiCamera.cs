// Decompiled with JetBrains decompiler
// Type: RenkeiCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class RenkeiCamera : MonoBehaviour
{
  private float mRenderWidth = 1f;
  private float mRenderHeight = 1f;
  private RenderTexture mRT;
  private bool mRTDirty;
  private Transform mBackground;

  public bool IsSceneTextureDirty
  {
    get
    {
      return this.mRTDirty;
    }
  }

  public RenderTexture SceneTexture
  {
    get
    {
      return this.mRT;
    }
  }

  public Color FillColor
  {
    set
    {
      this.GetComponent<Camera>().backgroundColor = value;
    }
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
    Camera component = this.GetComponent<Camera>();
    int width = (int) ((double) Screen.width * (double) this.mRenderWidth);
    int height = (int) ((double) Screen.height * (double) this.mRenderHeight);
    this.ReleaseRenderTarget();
    this.mRT = RenderTexture.GetTemporary(width, height, 16);
    component.useOcclusionCulling = false;
    component.targetTexture = this.mRT;
    component.rect = new Rect(0.0f, 0.0f, 1f, 1f);
    component.clearFlags = CameraClearFlags.Color;
  }

  public void SetBackgroundTemplate(GameObject backgroundTemplate)
  {
    GameUtility.DestroyGameObject((Component) this.mBackground);
    this.mBackground = (Transform) null;
    Transform transform = backgroundTemplate.transform;
    this.mBackground = Object.Instantiate<GameObject>(backgroundTemplate, transform.position, transform.rotation).transform;
    this.mBackground.SetParent(this.transform, false);
    GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
  }

  private void Start()
  {
    if (!((Object) this.mRT == (Object) null))
      return;
    this.SetRenderSize(this.mRenderWidth, this.mRenderHeight);
  }

  private void OnDestroy()
  {
    this.ReleaseRenderTarget();
  }

  private void OnPreCull()
  {
    if (!((Object) this.mBackground != (Object) null))
      return;
    GameUtility.SetLayer((Component) this.mBackground, this.gameObject.layer, true);
  }

  private void OnPostRender()
  {
    if ((Object) this.mBackground != (Object) null)
      GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
    this.mRTDirty = true;
  }
}
