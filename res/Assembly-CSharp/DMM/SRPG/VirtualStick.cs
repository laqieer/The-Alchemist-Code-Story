// Decompiled with JetBrains decompiler
// Type: SRPG.VirtualStick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class VirtualStick : MonoBehaviour
  {
    public static VirtualStick Instance;
    public RectTransform VirtualStickBG;
    public RectTransform VirtualStickFG;
    public RectTransform TouchArea;
    private bool mTouched;
    private Vector3 mTouchStart;
    private Vector3 mTouchPos;
    private Vector3 mVelocity = Vector3.zero;
    public string OpenFlagName = "open";

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) VirtualStick.Instance, (Object) null))
        return;
      VirtualStick.Instance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) VirtualStick.Instance, (Object) this))
        return;
      VirtualStick.Instance = (VirtualStick) null;
    }

    public Vector2 GetVelocity(Transform cameraTransform)
    {
      if (!Object.op_Inequality((Object) cameraTransform, (Object) null))
        return Vector2.op_Implicit(this.mVelocity);
      Vector3 forward = cameraTransform.forward;
      Vector3 right = cameraTransform.right;
      forward.y = 0.0f;
      ((Vector3) ref forward).Normalize();
      right.y = 0.0f;
      ((Vector3) ref right).Normalize();
      return new Vector2((float) ((double) right.x * (double) this.mVelocity.x + (double) forward.x * (double) this.mVelocity.y), (float) ((double) right.z * (double) this.mVelocity.x + (double) forward.z * (double) this.mVelocity.y));
    }

    private void Start()
    {
      UIEventListener uiEventListener = UIEventListener.Get((Component) this.TouchArea);
      uiEventListener.onPointerDown = (UIEventListener.PointerEvent) (eventData =>
      {
        ((Component) this.VirtualStickBG).GetComponent<Animator>().SetBool(this.OpenFlagName, true);
        RaycastResult pointerCurrentRaycast = eventData.pointerCurrentRaycast;
        Vector3 vector3 = ((RaycastResult) ref pointerCurrentRaycast).gameObject.transform.InverseTransformPoint(Vector2.op_Implicit(eventData.position));
        ((RectTransform) ((Component) this.VirtualStickBG).transform).anchoredPosition = new Vector2(vector3.x, vector3.y);
        this.mTouchStart = vector3;
        this.mTouchPos = vector3;
        this.mTouched = true;
        this.mVelocity = Vector3.zero;
      });
      uiEventListener.onPointerUp = (UIEventListener.PointerEvent) (eventData =>
      {
        ((Component) this.VirtualStickBG).GetComponent<Animator>().SetBool(this.OpenFlagName, false);
        this.mTouched = false;
        this.mVelocity = Vector3.zero;
      });
      uiEventListener.onDrag = (UIEventListener.PointerEvent) (eventData => this.mTouchPos = eventData.pointerPress.transform.InverseTransformPoint(Vector2.op_Implicit(eventData.position)));
    }

    private void Update()
    {
      if (!this.mTouched)
        return;
      Vector3 vector3 = Vector3.op_Subtraction(this.mTouchPos, this.mTouchStart);
      RectTransform transform = (RectTransform) ((Component) this.VirtualStickFG).transform;
      float num = (float) (((double) ((RectTransform) ((Component) this.VirtualStickBG).transform).sizeDelta.x - (double) transform.sizeDelta.x) * 0.5);
      if ((double) ((Vector3) ref vector3).magnitude >= (double) num)
        vector3 = Vector3.op_Multiply(((Vector3) ref vector3).normalized, num);
      transform.anchoredPosition = Vector2.op_Implicit(vector3);
      this.mVelocity = Vector3.op_Multiply(vector3, 1f / num);
    }
  }
}
