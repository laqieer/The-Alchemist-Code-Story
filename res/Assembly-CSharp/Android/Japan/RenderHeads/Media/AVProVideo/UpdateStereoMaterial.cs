// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.UpdateStereoMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Update Stereo Material", 400)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class UpdateStereoMaterial : MonoBehaviour
  {
    [Header("Stereo camera")]
    public Camera _camera;
    [Header("Rendering elements")]
    public MeshRenderer _renderer;
    public Graphic _uGuiComponent;
    public Material _material;
    private int _cameraPositionId;
    private int _viewMatrixId;

    private void Awake()
    {
      this._cameraPositionId = Shader.PropertyToID("_cameraPosition");
      this._viewMatrixId = Shader.PropertyToID("_ViewMatrix");
      if (!((Object) this._camera == (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] No camera set for UpdateStereoMaterial component. If you are rendering in stereo then it is recommended to set this.");
    }

    private void SetupMaterial(Material m, Camera camera)
    {
      m.SetVector(this._cameraPositionId, (Vector4) camera.transform.position);
      m.SetMatrix(this._viewMatrixId, camera.worldToCameraMatrix.transpose);
    }

    private void LateUpdate()
    {
      Camera camera = this._camera;
      if ((Object) camera == (Object) null)
        camera = Camera.main;
      if ((Object) this._renderer == (Object) null && (Object) this._material == (Object) null)
        this._renderer = this.gameObject.GetComponent<MeshRenderer>();
      if (!((Object) camera != (Object) null))
        return;
      if ((Object) this._renderer != (Object) null)
        this.SetupMaterial(this._renderer.material, camera);
      if ((Object) this._material != (Object) null)
        this.SetupMaterial(this._material, camera);
      if (!((Object) this._uGuiComponent != (Object) null))
        return;
      this.SetupMaterial(this._uGuiComponent.material, camera);
    }
  }
}
