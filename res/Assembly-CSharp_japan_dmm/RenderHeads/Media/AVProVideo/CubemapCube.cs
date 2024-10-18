// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.CubemapCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [RequireComponent(typeof (MeshRenderer))]
  [RequireComponent(typeof (MeshFilter))]
  [AddComponentMenu("AVPro Video/Cubemap Cube (VR)", 400)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class CubemapCube : MonoBehaviour
  {
    private Mesh _mesh;
    protected MeshRenderer _renderer;
    [SerializeField]
    protected Material _material;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    [SerializeField]
    private float expansion_coeff = 1.01f;
    [SerializeField]
    private CubemapCube.Layout _layout;
    private Texture _texture;
    private bool _verticalFlip;
    private int _textureWidth;
    private int _textureHeight;
    private static int _propApplyGamma;
    private static int _propUseYpCbCr;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;
    private const string PropYpCbCrTransformName = "_YpCbCrTransform";
    private static int _propYpCbCrTransform;

    public MediaPlayer Player
    {
      set => this._mediaPlayer = value;
      get => this._mediaPlayer;
    }

    private void Awake()
    {
      if (CubemapCube._propApplyGamma == 0)
        CubemapCube._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      if (CubemapCube._propUseYpCbCr == 0)
        CubemapCube._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
      if (CubemapCube._propChromaTex == 0)
        CubemapCube._propChromaTex = Shader.PropertyToID("_ChromaTex");
      if (CubemapCube._propYpCbCrTransform != 0)
        return;
      CubemapCube._propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
    }

    private void Start()
    {
      if (!Object.op_Equality((Object) this._mesh, (Object) null))
        return;
      this._mesh = new Mesh();
      this._mesh.MarkDynamic();
      MeshFilter component = ((Component) this).GetComponent<MeshFilter>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.mesh = this._mesh;
      this._renderer = ((Component) this).GetComponent<MeshRenderer>();
      if (Object.op_Inequality((Object) this._renderer, (Object) null))
        ((Renderer) this._renderer).material = this._material;
      this.BuildMesh();
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this._mesh, (Object) null))
      {
        MeshFilter component = ((Component) this).GetComponent<MeshFilter>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.mesh = (Mesh) null;
        Object.Destroy((Object) this._mesh);
        this._mesh = (Mesh) null;
      }
      if (!Object.op_Inequality((Object) this._renderer, (Object) null))
        return;
      ((Renderer) this._renderer).material = (Material) null;
      this._renderer = (MeshRenderer) null;
    }

    private void LateUpdate()
    {
      if (!Application.isPlaying)
        return;
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null) && this._mediaPlayer.Control != null)
      {
        if (this._mediaPlayer.TextureProducer != null)
        {
          Texture texture1 = this._mediaPlayer.FrameResampler == null || this._mediaPlayer.FrameResampler.OutputTexture == null ? (Texture) null : this._mediaPlayer.FrameResampler.OutputTexture[0];
          Texture texture2 = !this._mediaPlayer.m_Resample ? this._mediaPlayer.TextureProducer.GetTexture() : texture1;
          bool flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
          if (Object.op_Inequality((Object) this._texture, (Object) texture2) || this._verticalFlip != flipY || Object.op_Inequality((Object) texture2, (Object) null) && (this._textureWidth != texture2.width || this._textureHeight != texture2.height))
          {
            this._texture = texture2;
            if (Object.op_Inequality((Object) texture2, (Object) null))
              this.UpdateMeshUV(texture2.width, texture2.height, flipY);
          }
          if (((Renderer) this._renderer).material.HasProperty(CubemapCube._propApplyGamma) && this._mediaPlayer.Info != null)
            Helper.SetupGammaMaterial(((Renderer) this._renderer).material, this._mediaPlayer.Info.PlayerSupportsLinearColorSpace());
          if (((Renderer) this._renderer).material.HasProperty(CubemapCube._propUseYpCbCr) && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
          {
            ((Renderer) this._renderer).material.EnableKeyword("USE_YPCBCR");
            Texture texture3 = this._mediaPlayer.FrameResampler == null || this._mediaPlayer.FrameResampler.OutputTexture == null ? (Texture) null : this._mediaPlayer.FrameResampler.OutputTexture[1];
            ((Renderer) this._renderer).material.SetTexture(CubemapCube._propChromaTex, !this._mediaPlayer.m_Resample ? this._mediaPlayer.TextureProducer.GetTexture(1) : texture3);
            ((Renderer) this._renderer).material.SetMatrix(CubemapCube._propYpCbCrTransform, this._mediaPlayer.TextureProducer.GetYpCbCrTransform());
          }
        }
        ((Renderer) this._renderer).material.mainTexture = this._texture;
      }
      else
        ((Renderer) this._renderer).material.mainTexture = (Texture) null;
    }

    private void BuildMesh()
    {
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(-0.5f, -0.5f, -0.5f);
      Vector3[] vector3Array = new Vector3[24]
      {
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3),
        Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3)
      };
      Matrix4x4 matrix4x4 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
      for (int index = 0; index < vector3Array.Length; ++index)
        vector3Array[index] = ((Matrix4x4) ref matrix4x4).MultiplyPoint(vector3Array[index]);
      this._mesh.vertices = vector3Array;
      this._mesh.triangles = new int[36]
      {
        0,
        1,
        2,
        0,
        2,
        3,
        4,
        5,
        6,
        4,
        6,
        7,
        8,
        9,
        10,
        8,
        10,
        11,
        12,
        13,
        14,
        12,
        14,
        15,
        16,
        17,
        18,
        16,
        18,
        19,
        20,
        21,
        22,
        20,
        22,
        23
      };
      this._mesh.normals = new Vector3[24]
      {
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f)
      };
      this.UpdateMeshUV(512, 512, false);
    }

    private void UpdateMeshUV(int textureWidth, int textureHeight, bool flipY)
    {
      this._textureWidth = textureWidth;
      this._textureHeight = textureHeight;
      this._verticalFlip = flipY;
      float num1 = (float) textureWidth;
      float num2 = (float) textureHeight;
      float num3 = num1 / 3f;
      float num4 = Mathf.Floor((float) (((double) this.expansion_coeff * (double) num3 - (double) num3) / 2.0));
      float num5 = num4 / num1;
      float num6 = num4 / num2;
      Vector2[] vector2Array = (Vector2[]) null;
      if (this._layout == CubemapCube.Layout.Facebook360Capture)
        vector2Array = new Vector2[24]
        {
          new Vector2(0.333333343f + num5, 0.5f - num6),
          new Vector2(0.6666667f - num5, 0.5f - num6),
          new Vector2(0.6666667f - num5, num6),
          new Vector2(0.333333343f + num5, num6),
          new Vector2(0.333333343f + num5, 1f - num6),
          new Vector2(0.6666667f - num5, 1f - num6),
          new Vector2(0.6666667f - num5, 0.5f + num6),
          new Vector2(0.333333343f + num5, 0.5f + num6),
          new Vector2(num5, 0.5f - num6),
          new Vector2(0.333333343f - num5, 0.5f - num6),
          new Vector2(0.333333343f - num5, num6),
          new Vector2(num5, num6),
          new Vector2(0.6666667f + num5, 1f - num6),
          new Vector2(1f - num5, 1f - num6),
          new Vector2(1f - num5, 0.5f + num6),
          new Vector2(0.6666667f + num5, 0.5f + num6),
          new Vector2(0.6666667f + num5, num6),
          new Vector2(0.6666667f + num5, 0.5f - num6),
          new Vector2(1f - num5, 0.5f - num6),
          new Vector2(1f - num5, num6),
          new Vector2(0.333333343f - num5, 1f - num6),
          new Vector2(0.333333343f - num5, 0.5f + num6),
          new Vector2(num5, 0.5f + num6),
          new Vector2(num5, 1f - num6)
        };
      else if (this._layout == CubemapCube.Layout.FacebookTransform32)
        vector2Array = new Vector2[24]
        {
          new Vector2(0.333333343f + num5, 1f - num6),
          new Vector2(0.6666667f - num5, 1f - num6),
          new Vector2(0.6666667f - num5, 0.5f + num6),
          new Vector2(0.333333343f + num5, 0.5f + num6),
          new Vector2(0.333333343f + num5, 0.5f - num6),
          new Vector2(0.6666667f - num5, 0.5f - num6),
          new Vector2(0.6666667f - num5, num6),
          new Vector2(0.333333343f + num5, num6),
          new Vector2(num5, 1f - num6),
          new Vector2(0.333333343f - num5, 1f - num6),
          new Vector2(0.333333343f - num5, 0.5f + num6),
          new Vector2(num5, 0.5f + num6),
          new Vector2(0.6666667f + num5, 0.5f - num6),
          new Vector2(1f - num5, 0.5f - num6),
          new Vector2(1f - num5, num6),
          new Vector2(0.6666667f + num5, num6),
          new Vector2(num5, num6),
          new Vector2(num5, 0.5f - num6),
          new Vector2(0.333333343f - num5, 0.5f - num6),
          new Vector2(0.333333343f - num5, num6),
          new Vector2(1f - num5, 1f - num6),
          new Vector2(1f - num5, 0.5f + num6),
          new Vector2(0.6666667f + num5, 0.5f + num6),
          new Vector2(0.6666667f + num5, 1f - num6)
        };
      if (flipY)
      {
        for (int index = 0; index < vector2Array.Length; ++index)
          vector2Array[index].y = 1f - vector2Array[index].y;
      }
      this._mesh.uv = vector2Array;
      this._mesh.UploadMeshData(false);
    }

    public enum Layout
    {
      FacebookTransform32,
      Facebook360Capture,
    }
  }
}
