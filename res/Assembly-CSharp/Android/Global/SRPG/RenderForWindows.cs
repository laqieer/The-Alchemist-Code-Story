// Decompiled with JetBrains decompiler
// Type: SRPG.RenderForWindows
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RenderForWindows : MonoBehaviour
  {
    private int canvas_sort_order = -1;
    private const string MATERIAL_SHADER_NAME = "Unlit/Texture";
    private const string RAW_IMAGE_NAME = "RawImage";
    private const string CANVAS_NAME = "Canvas";
    [SerializeField]
    private RenderForWindows.eTargetType target_type;
    [SerializeField]
    private UnityEngine.Camera target_camera;
    private bool is_enable;
    private RenderTexture render_texture;
    private Canvas canvas;
    private RawImage image;
    private RectTransform img_rect_transform;
    private bool is_dont_create_render_texture;

    public bool IsDontCreteRenderTexture
    {
      set
      {
        this.is_dont_create_render_texture = value;
      }
    }

    private void Awake()
    {
      if (!this.is_enable)
        return;
      this.canvas = this.CreateCanvas();
      this.image = this.CreateRawImage(this.canvas.transform, (float) Screen.width, (float) Screen.height);
      this.img_rect_transform = this.image.transform as RectTransform;
    }

    private void Start()
    {
      if (!this.is_enable)
        return;
      this.Init();
    }

    private void LateUpdate()
    {
      if (!this.is_enable || (UnityEngine.Object) this.img_rect_transform == (UnityEngine.Object) null || (double) this.img_rect_transform.sizeDelta.x == (double) Screen.width && (double) this.img_rect_transform.sizeDelta.y == (double) Screen.height)
        return;
      this.img_rect_transform.sizeDelta = new Vector2((float) Screen.width, (float) Screen.height);
    }

    public void SetTargetType(RenderForWindows.eTargetType _type, UnityEngine.Camera _target_camera = null)
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
          this.target_camera = UnityEngine.Camera.main;
          break;
        case RenderForWindows.eTargetType.SELECTED:
          this.target_camera = !((UnityEngine.Object) this.target_camera != (UnityEngine.Object) null) ? UnityEngine.Camera.main : this.target_camera;
          break;
      }
      if (!((UnityEngine.Object) this.render_texture == (UnityEngine.Object) null) || this.is_dont_create_render_texture)
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
      renderTexture.format = RenderTextureFormat.ARGB32;
      renderTexture.depth = 24;
      renderTexture.filterMode = FilterMode.Point;
      renderTexture.set_generateMips(false);
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
      gameObject.name = "Canvas";
      gameObject.transform.SetParent(this.transform.parent, true);
      Canvas canvas = gameObject.AddComponent<Canvas>();
      canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
      canvas.sortingOrder = this.canvas_sort_order;
      return canvas;
    }

    private RawImage CreateRawImage(Transform _parent, float _width, float _height)
    {
      if (!this.is_enable)
        return (RawImage) null;
      GameObject gameObject = new GameObject();
      gameObject.name = "RawImage";
      gameObject.transform.SetParent(_parent, true);
      RawImage rawImage = gameObject.AddComponent<RawImage>();
      rawImage.material = new Material(Shader.Find("Unlit/Texture"));
      RectTransform transform = rawImage.transform as RectTransform;
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
