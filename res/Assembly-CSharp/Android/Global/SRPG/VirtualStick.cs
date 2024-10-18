// Decompiled with JetBrains decompiler
// Type: SRPG.VirtualStick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class VirtualStick : MonoBehaviour
  {
    private Vector3 mVelocity = Vector3.zero;
    public string OpenFlagName = "open";
    public static VirtualStick Instance;
    public RectTransform VirtualStickBG;
    public RectTransform VirtualStickFG;
    public RectTransform TouchArea;
    private bool mTouched;
    private Vector3 mTouchStart;
    private Vector3 mTouchPos;

    private void OnEnable()
    {
      if (!((UnityEngine.Object) VirtualStick.Instance == (UnityEngine.Object) null))
        return;
      VirtualStick.Instance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) VirtualStick.Instance == (UnityEngine.Object) this))
        return;
      VirtualStick.Instance = (VirtualStick) null;
    }

    public Vector2 GetVelocity(Transform cameraTransform)
    {
      if (!((UnityEngine.Object) cameraTransform != (UnityEngine.Object) null))
        return (Vector2) this.mVelocity;
      Vector3 forward = cameraTransform.forward;
      Vector3 right = cameraTransform.right;
      forward.y = 0.0f;
      forward.Normalize();
      right.y = 0.0f;
      right.Normalize();
      return new Vector2((float) ((double) right.x * (double) this.mVelocity.x + (double) forward.x * (double) this.mVelocity.y), (float) ((double) right.z * (double) this.mVelocity.x + (double) forward.z * (double) this.mVelocity.y));
    }

    private void Start()
    {
      UIEventListener uiEventListener = UIEventListener.Get((Component) this.TouchArea);
      uiEventListener.onPointerDown = (UIEventListener.PointerEvent) (eventData =>
      {
        this.VirtualStickBG.GetComponent<Animator>().SetBool(this.OpenFlagName, true);
        Vector3 vector3 = eventData.pointerCurrentRaycast.gameObject.transform.InverseTransformPoint((Vector3) eventData.position);
        ((RectTransform) this.VirtualStickBG.transform).anchoredPosition = new Vector2(vector3.x, vector3.y);
        this.mTouchStart = vector3;
        this.mTouchPos = vector3;
        this.mTouched = true;
        this.mVelocity = Vector3.zero;
      });
      uiEventListener.onPointerUp = (UIEventListener.PointerEvent) (eventData =>
      {
        this.VirtualStickBG.GetComponent<Animator>().SetBool(this.OpenFlagName, false);
        this.mTouched = false;
        this.mVelocity = Vector3.zero;
      });
      uiEventListener.onDrag = (UIEventListener.PointerEvent) (eventData => this.mTouchPos = eventData.pointerPress.transform.InverseTransformPoint((Vector3) eventData.position));
    }

    private void Update()
    {
      if (!this.mTouched)
        return;
      Vector3 vector3 = this.mTouchPos - this.mTouchStart;
      RectTransform transform = (RectTransform) this.VirtualStickFG.transform;
      float num = (float) (((double) ((RectTransform) this.VirtualStickBG.transform).sizeDelta.x - (double) transform.sizeDelta.x) * 0.5);
      if ((double) vector3.magnitude >= (double) num)
        vector3 = vector3.normalized * num;
      transform.anchoredPosition = (Vector2) vector3;
      this.mVelocity = vector3 * (1f / num);
    }
  }
}
