// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.CubemapCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [RequireComponent(typeof (MeshRenderer))]
  [RequireComponent(typeof (MeshFilter))]
  [AddComponentMenu("AVPro Video/Cubemap Cube (VR)", 400)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class CubemapCube : MonoBehaviour
  {
    [SerializeField]
    private float expansion_coeff = 1.01f;
    private Mesh _mesh;
    protected MeshRenderer _renderer;
    [SerializeField]
    protected Material _material;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    private Texture _texture;
    private bool _verticalFlip;
    private int _textureWidth;
    private int _textureHeight;
    private static int _propApplyGamma;
    private static int _propUseYpCbCr;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;

    public MediaPlayer Player
    {
      set
      {
        this._mediaPlayer = value;
      }
      get
      {
        return this._mediaPlayer;
      }
    }

    private void Awake()
    {
      if (CubemapCube._propApplyGamma == 0)
        CubemapCube._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      if (CubemapCube._propUseYpCbCr == 0)
        CubemapCube._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
      if (CubemapCube._propChromaTex != 0)
        return;
      CubemapCube._propChromaTex = Shader.PropertyToID("_ChromaTex");
    }

    private void Start()
    {
      if (!((Object) this._mesh == (Object) null))
        return;
      this._mesh = new Mesh();
      this._mesh.MarkDynamic();
      MeshFilter component = this.GetComponent<MeshFilter>();
      if ((Object) component != (Object) null)
        component.mesh = this._mesh;
      this._renderer = this.GetComponent<MeshRenderer>();
      if ((Object) this._renderer != (Object) null)
        this._renderer.material = this._material;
      this.BuildMesh();
    }

    private void OnDestroy()
    {
      if ((Object) this._mesh != (Object) null)
      {
        MeshFilter component = this.GetComponent<MeshFilter>();
        if ((Object) component != (Object) null)
          component.mesh = (Mesh) null;
        Object.Destroy((Object) this._mesh);
        this._mesh = (Mesh) null;
      }
      if (!((Object) this._renderer != (Object) null))
        return;
      this._renderer.material = (Material) null;
      this._renderer = (MeshRenderer) null;
    }

    private void LateUpdate()
    {
      if (!Application.isPlaying)
        return;
      if ((Object) this._mediaPlayer != (Object) null && this._mediaPlayer.Control != null)
      {
        if (this._mediaPlayer.TextureProducer != null)
        {
          Texture texture = this._mediaPlayer.TextureProducer.GetTexture(0);
          bool flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
          if ((Object) this._texture != (Object) texture || this._verticalFlip != flipY || (Object) texture != (Object) null && (this._textureWidth != texture.width || this._textureHeight != texture.height))
          {
            this._texture = texture;
            if ((Object) texture != (Object) null)
              this.UpdateMeshUV(texture.width, texture.height, flipY);
          }
          if (this._renderer.material.HasProperty(CubemapCube._propApplyGamma) && this._mediaPlayer.Info != null)
            Helper.SetupGammaMaterial(this._renderer.material, this._mediaPlayer.Info.PlayerSupportsLinearColorSpace());
          if (this._renderer.material.HasProperty(CubemapCube._propUseYpCbCr) && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
          {
            this._renderer.material.EnableKeyword("USE_YPCBCR");
            this._renderer.material.SetTexture(CubemapCube._propChromaTex, this._mediaPlayer.TextureProducer.GetTexture(1));
          }
        }
        this._renderer.material.mainTexture = this._texture;
      }
      else
        this._renderer.material.mainTexture = (Texture) null;
    }

    private void BuildMesh()
    {
      Vector3 vector3 = new Vector3(-0.5f, -0.5f, -0.5f);
      Vector3[] vector3Array = new Vector3[24]{ new Vector3(0.0f, -1f, 0.0f) - vector3, new Vector3(0.0f, 0.0f, 0.0f) - vector3, new Vector3(0.0f, 0.0f, -1f) - vector3, new Vector3(0.0f, -1f, -1f) - vector3, new Vector3(0.0f, 0.0f, 0.0f) - vector3, new Vector3(-1f, 0.0f, 0.0f) - vector3, new Vector3(-1f, 0.0f, -1f) - vector3, new Vector3(0.0f, 0.0f, -1f) - vector3, new Vector3(-1f, 0.0f, 0.0f) - vector3, new Vector3(-1f, -1f, 0.0f) - vector3, new Vector3(-1f, -1f, -1f) - vector3, new Vector3(-1f, 0.0f, -1f) - vector3, new Vector3(-1f, -1f, 0.0f) - vector3, new Vector3(0.0f, -1f, 0.0f) - vector3, new Vector3(0.0f, -1f, -1f) - vector3, new Vector3(-1f, -1f, -1f) - vector3, new Vector3(0.0f, -1f, -1f) - vector3, new Vector3(0.0f, 0.0f, -1f) - vector3, new Vector3(-1f, 0.0f, -1f) - vector3, new Vector3(-1f, -1f, -1f) - vector3, new Vector3(-1f, -1f, 0.0f) - vector3, new Vector3(-1f, 0.0f, 0.0f) - vector3, new Vector3(0.0f, 0.0f, 0.0f) - vector3, new Vector3(0.0f, -1f, 0.0f) - vector3 };
      Matrix4x4 matrix4x4 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
      for (int index = 0; index < vector3Array.Length; ++index)
        vector3Array[index] = matrix4x4.MultiplyPoint(vector3Array[index]);
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
      float x = num4 / num1;
      float y = num4 / num2;
      Vector2[] vector2Array = new Vector2[24]{ new Vector2(0.3333333f + x, 1f - y), new Vector2(0.6666667f - x, 1f - y), new Vector2(0.6666667f - x, 0.5f + y), new Vector2(0.3333333f + x, 0.5f + y), new Vector2(0.3333333f + x, 0.5f - y), new Vector2(0.6666667f - x, 0.5f - y), new Vector2(0.6666667f - x, y), new Vector2(0.3333333f + x, y), new Vector2(x, 1f - y), new Vector2(0.3333333f - x, 1f - y), new Vector2(0.3333333f - x, 0.5f + y), new Vector2(x, 0.5f + y), new Vector2(0.6666667f + x, 0.5f - y), new Vector2(1f - x, 0.5f - y), new Vector2(1f - x, y), new Vector2(0.6666667f + x, y), new Vector2(x, y), new Vector2(x, 0.5f - y), new Vector2(0.3333333f - x, 0.5f - y), new Vector2(0.3333333f - x, y), new Vector2(1f - x, 1f - y), new Vector2(1f - x, 0.5f + y), new Vector2(0.6666667f + x, 0.5f + y), new Vector2(0.6666667f + x, 1f - y) };
      if (flipY)
      {
        for (int index = 0; index < vector2Array.Length; ++index)
          vector2Array[index].y = 1f - vector2Array[index].y;
      }
      this._mesh.uv = vector2Array;
      this._mesh.UploadMeshData(false);
    }
  }
}
