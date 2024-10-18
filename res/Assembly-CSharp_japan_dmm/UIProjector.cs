// Decompiled with JetBrains decompiler
// Type: UIProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
    if (!Object.op_Inequality((Object) this.UIObject, (Object) null))
      return;
    ((Transform) this.UIObject).SetParent(!Object.op_Inequality((Object) this.mCanvas, (Object) null) ? (Transform) null : ((Component) this.mCanvas).transform, false);
  }

  protected virtual void Awake()
  {
    this.AutoDestroyUIObject = Object.op_Equality((Object) this.UIObject, (Object) null);
  }

  protected virtual void Start()
  {
    if (Object.op_Equality((Object) this.UIObject, (Object) null) && !string.IsNullOrEmpty(this.UIObjectID))
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.UIObjectID);
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        this.UIObject = gameObject.GetComponent<RectTransform>();
    }
    if (Object.op_Equality((Object) this.mCanvas, (Object) null) && Object.op_Inequality((Object) this.UIObject, (Object) null))
      this.mCanvas = ((Component) this.UIObject).GetComponentInParent<Canvas>();
    if (Object.op_Inequality((Object) this.mCanvas, (Object) null) && Object.op_Equality((Object) this.UIObject, (Object) null))
    {
      this.UIObject = new GameObject(((Object) ((Component) this).gameObject).name, new System.Type[1]
      {
        typeof (RectTransform)
      }).transform as RectTransform;
      ((Transform) this.UIObject).SetParent(((Component) this.mCanvas).transform, false);
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
    if (Object.op_Inequality((Object) this.ProjectCamera, (Object) null) && Object.op_Inequality((Object) camera, (Object) this.ProjectCamera))
      return;
    Rect cameraViewport = SetCanvasBounds.GetCameraViewport();
    if (!Object.op_Inequality((Object) this.UIObject, (Object) null))
      return;
    Transform transform1 = ((Component) this).transform;
    Vector3 vector3 = Vector3.op_Addition(transform1.position, Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(this.LocalOffset.x, transform1.right), Vector3.op_Multiply(this.LocalOffset.y, transform1.up)), Vector3.op_Multiply(this.LocalOffset.z, transform1.forward)));
    RectTransform transform2 = ((Component) this.mCanvas).transform as RectTransform;
    Vector3 screenPoint = camera.WorldToScreenPoint(vector3);
    screenPoint.x /= (float) Screen.width;
    screenPoint.y /= (float) Screen.height;
    this.UIObject.anchorMin = Vector2.zero;
    this.UIObject.anchorMax = Vector2.zero;
    Vector2 zero = Vector2.zero;
    ref Vector2 local1 = ref zero;
    Rect rect1 = transform2.rect;
    double num1 = (double) ((Rect) ref rect1).width * (double) ((Rect) ref cameraViewport).x;
    local1.x = (float) num1;
    ref Vector2 local2 = ref zero;
    Rect rect2 = transform2.rect;
    double num2 = (double) ((Rect) ref rect2).height * (double) ((Rect) ref cameraViewport).y;
    local2.y = (float) num2;
    RectTransform uiObject = this.UIObject;
    double x = (double) screenPoint.x;
    Rect rect3 = transform2.rect;
    double width = (double) ((Rect) ref rect3).width;
    double num3 = x * width - (double) zero.x;
    double y = (double) screenPoint.y;
    Rect rect4 = transform2.rect;
    double height = (double) ((Rect) ref rect4).height;
    double num4 = y * height - (double) zero.y;
    Vector2 vector2 = new Vector2((float) num3, (float) num4);
    uiObject.anchoredPosition = vector2;
  }

  public void ReStart() => this.Start();
}
