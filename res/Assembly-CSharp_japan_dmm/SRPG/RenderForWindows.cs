// Decompiled with JetBrains decompiler
// Type: SRPG.RenderForWindows
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RenderForWindows : MonoBehaviour
  {
    [SerializeField]
    private RenderForWindows.eTargetType target_type;
    [SerializeField]
    private Camera target_camera;
    private const string MATERIAL_SHADER_NAME = "Unlit/Texture";
    private const string RAW_IMAGE_NAME = "RawImage";
    private const string CANVAS_NAME = "Canvas";
    private int canvas_sort_order = -1;
    private bool is_enable;
    private RenderTexture render_texture;
    private Canvas canvas;
    private RawImage image;
    private RectTransform img_rect_transform;
    private bool is_dont_create_render_texture;

    public bool IsDontCreteRenderTexture
    {
      set => this.is_dont_create_render_texture = value;
    }

    private void Awake()
    {
      this.is_enable = true;
      if (!this.is_enable)
        return;
      this.canvas = this.CreateCanvas();
      this.image = this.CreateRawImage(((Component) this.canvas).transform, (float) Screen.width, (float) Screen.height);
      this.img_rect_transform = ((Component) this.image).transform as RectTransform;
    }

    private void Start()
    {
      if (!this.is_enable)
        return;
      this.Init();
    }

    private void LateUpdate()
    {
      if (!this.is_enable || Object.op_Equality((Object) this.img_rect_transform, (Object) null) || (double) this.img_rect_transform.sizeDelta.x == (double) Screen.width && (double) this.img_rect_transform.sizeDelta.y == (double) Screen.height)
        return;
      this.img_rect_transform.sizeDelta = new Vector2((float) Screen.width, (float) Screen.height);
    }

    public void SetTargetType(RenderForWindows.eTargetType _type, Camera _target_camera = null)
    {
      this.target_type = _type;
      if (this.target_type != RenderForWindows.eTargetType.SELECTED)
        return;
      this.target_camera = _target_camera;
    }

    public void SetRenderTexture(RenderTexture _render_texture)
    {
      this.render_texture = _render_texture;
      this.image.texture = (Texture) this.render_texture;
    }

    private void Init()
    {
      if (!this.is_enable)
        return;
      switch (this.target_type)
      {
        case RenderForWindows.eTargetType.MAIN_CAMERA:
          this.target_camera = Camera.main;
          break;
        case RenderForWindows.eTargetType.SELECTED:
          this.target_camera = !Object.op_Inequality((Object) this.target_camera, (Object) null) ? Camera.main : this.target_camera;
          break;
      }
      if (!Object.op_Equality((Object) this.render_texture, (Object) null) || this.is_dont_create_render_texture)
        return;
      this.render_texture = this.CreateRenderTexture(Screen.width, Screen.height);
      this.target_camera.targetTexture = this.render_texture;
      this.image.texture = (Texture) this.render_texture;
    }

    private RenderTexture CreateRenderTexture(int _width, int _height)
    {
      if (!this.is_enable)
        return (RenderTexture) null;
      RenderTexture renderTexture = new RenderTexture(_width, _height, 0);
      renderTexture.format = (RenderTextureFormat) 0;
      renderTexture.depth = 24;
      ((Texture) renderTexture).filterMode = (FilterMode) 0;
      renderTexture.autoGenerateMips = false;
      renderTexture.useMipMap = false;
      if (renderTexture.Create())
        return renderTexture;
      DebugUtility.LogError("RenderTexture生成に失敗");
      return (RenderTexture) null;
    }

    private Canvas CreateCanvas()
    {
      if (!this.is_enable)
        return (Canvas) null;
      GameObject gameObject = new GameObject();
      ((Object) gameObject).name = "Canvas";
      gameObject.transform.SetParent(((Component) this).transform.parent, true);
      Canvas canvas = gameObject.AddComponent<Canvas>();
      canvas.renderMode = (RenderMode) 0;
      canvas.sortingOrder = this.canvas_sort_order;
      return canvas;
    }

    private RawImage CreateRawImage(Transform _parent, float _width, float _height)
    {
      if (!this.is_enable)
        return (RawImage) null;
      GameObject gameObject = new GameObject();
      ((Object) gameObject).name = "RawImage";
      gameObject.transform.SetParent(_parent, true);
      RawImage rawImage = gameObject.AddComponent<RawImage>();
      ((Graphic) rawImage).material = new Material(Shader.Find("Unlit/Texture"));
      RectTransform transform = ((Component) rawImage).transform as RectTransform;
      transform.anchoredPosition = Vector2.zero;
      transform.sizeDelta = new Vector2(_width, _height);
      return rawImage;
    }

    public enum eTargetType
    {
      MAIN_CAMERA,
      SELECTED,
    }
  }
}
