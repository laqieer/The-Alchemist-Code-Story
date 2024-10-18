// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.UpdateStereoMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    [SerializeField]
    private StereoEye _forceEyeMode;
    private static int _cameraPositionId;
    private static int _viewMatrixId;
    private StereoEye _setForceEyeMode;
    private Camera _foundCamera;

    public StereoEye ForceEyeMode
    {
      get => this._forceEyeMode;
      set => this._forceEyeMode = value;
    }

    private void Awake()
    {
      if (UpdateStereoMaterial._cameraPositionId == 0)
        UpdateStereoMaterial._cameraPositionId = Shader.PropertyToID("_cameraPosition");
      if (UpdateStereoMaterial._viewMatrixId == 0)
        UpdateStereoMaterial._viewMatrixId = Shader.PropertyToID("_ViewMatrix");
      if (!Object.op_Equality((Object) this._camera, (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] No camera set for UpdateStereoMaterial component. If you are rendering in stereo then it is recommended to set this.");
    }

    private void SetupMaterial(Material m, Camera camera)
    {
      m.SetVector(UpdateStereoMaterial._cameraPositionId, Vector4.op_Implicit(((Component) camera).transform.position));
      Material material = m;
      int viewMatrixId = UpdateStereoMaterial._viewMatrixId;
      Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
      Matrix4x4 transpose = ((Matrix4x4) ref worldToCameraMatrix).transpose;
      material.SetMatrix(viewMatrixId, transpose);
      if (this._forceEyeMode == this._setForceEyeMode)
        return;
      Helper.SetupStereoEyeModeMaterial(m, this._forceEyeMode);
      this._setForceEyeMode = this._forceEyeMode;
    }

    private void LateUpdate()
    {
      if (Object.op_Inequality((Object) this._camera, (Object) null) && Object.op_Inequality((Object) this._foundCamera, (Object) this._camera))
        this._foundCamera = this._camera;
      if (Object.op_Equality((Object) this._foundCamera, (Object) null))
      {
        this._foundCamera = Camera.main;
        if (Object.op_Equality((Object) this._foundCamera, (Object) null))
        {
          Debug.LogWarning((object) "[AVPro Video] Cannot find main camera for UpdateStereoMaterial, this can lead to eyes flickering");
          if (Camera.allCameras.Length > 0)
          {
            this._foundCamera = Camera.allCameras[0];
            Debug.LogWarning((object) ("[AVPro Video] UpdateStereoMaterial using camera " + ((Object) this._foundCamera).name));
          }
        }
      }
      if (Object.op_Equality((Object) this._renderer, (Object) null) && Object.op_Equality((Object) this._material, (Object) null))
        this._renderer = ((Component) this).gameObject.GetComponent<MeshRenderer>();
      if (!Object.op_Inequality((Object) this._foundCamera, (Object) null))
        return;
      if (Object.op_Inequality((Object) this._renderer, (Object) null))
        this.SetupMaterial(((Renderer) this._renderer).material, this._foundCamera);
      if (Object.op_Inequality((Object) this._material, (Object) null))
        this.SetupMaterial(this._material, this._foundCamera);
      if (!Object.op_Inequality((Object) this._uGuiComponent, (Object) null))
        return;
      this.SetupMaterial(this._uGuiComponent.material, this._foundCamera);
    }
  }
}
