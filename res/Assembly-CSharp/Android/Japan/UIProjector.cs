// Decompiled with JetBrains decompiler
// Type: UIProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("UI/Link UI Position")]
public class UIProjector : MonoBehaviour
{
  public Camera ProjectCamera;
  public RectTransform UIObject;
  public string UIObjectID;
  private Canvas mCanvas;
  public bool AutoDestroyUIObject;
  public Vector3 LocalOffset;

  public void SetCanvas(Canvas canvas)
  {
    this.mCanvas = canvas;
    if (!((Object) this.UIObject != (Object) null))
      return;
    this.UIObject.SetParent(!((Object) this.mCanvas != (Object) null) ? (Transform) null : this.mCanvas.transform, false);
  }

  protected virtual void Awake()
  {
    this.AutoDestroyUIObject = (Object) this.UIObject == (Object) null;
  }

  protected virtual void Start()
  {
    if ((Object) this.UIObject == (Object) null && !string.IsNullOrEmpty(this.UIObjectID))
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.UIObjectID);
      if ((Object) gameObject != (Object) null)
        this.UIObject = gameObject.GetComponent<RectTransform>();
    }
    if ((Object) this.mCanvas == (Object) null && (Object) this.UIObject != (Object) null)
      this.mCanvas = this.UIObject.GetComponentInParent<Canvas>();
    if ((Object) this.mCanvas != (Object) null && (Object) this.UIObject == (Object) null)
    {
      this.UIObject = new GameObject(this.gameObject.name, new System.Type[1]
      {
        typeof (RectTransform)
      }).transform as RectTransform;
      this.UIObject.SetParent(this.mCanvas.transform, false);
    }
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  protected virtual void OnDestroy()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
    if (!this.AutoDestroyUIObject)
      return;
    GameUtility.DestroyGameObject((Component) this.UIObject);
  }

  public void PreCull(Camera camera)
  {
    if ((Object) this.ProjectCamera != (Object) null && (Object) camera != (Object) this.ProjectCamera || !((Object) this.UIObject != (Object) null))
      return;
    Transform transform1 = this.transform;
    Vector3 position = transform1.position + (this.LocalOffset.x * transform1.right + this.LocalOffset.y * transform1.up + this.LocalOffset.z * transform1.forward);
    RectTransform transform2 = this.mCanvas.transform as RectTransform;
    Vector3 screenPoint = camera.WorldToScreenPoint(position);
    screenPoint.x /= (float) Screen.width;
    screenPoint.y /= (float) Screen.height;
    this.UIObject.anchorMin = Vector2.zero;
    this.UIObject.anchorMax = Vector2.zero;
    this.UIObject.anchoredPosition = new Vector2(screenPoint.x * transform2.rect.width, screenPoint.y * transform2.rect.height);
  }

  public void ReStart()
  {
    this.Start();
  }
}
