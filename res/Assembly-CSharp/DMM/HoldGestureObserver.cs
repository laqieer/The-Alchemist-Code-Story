// Decompiled with JetBrains decompiler
// Type: HoldGestureObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
internal class HoldGestureObserver : MonoBehaviour
{
  private static HoldGestureObserver mInstance;
  private IHoldGesture mReceiver;
  private float mPressTime;
  private bool mTriggered;
  private Vector2 mPressStart;
  private float mDragDist;
  private Vector2 mOldPosition;

  private static HoldGestureObserver Instance
  {
    get
    {
      if (Object.op_Equality((Object) HoldGestureObserver.mInstance, (Object) null))
        HoldGestureObserver.mInstance = new GameObject(nameof (HoldGestureObserver)).AddComponent<HoldGestureObserver>();
      return HoldGestureObserver.mInstance;
    }
  }

  public static void StartHoldGesture(IHoldGesture receiver)
  {
    HoldGestureObserver instance = HoldGestureObserver.Instance;
    instance.mReceiver = receiver;
    instance.mPressTime = Time.unscaledTime;
    instance.mPressStart = Vector2.op_Implicit(Input.mousePosition);
    instance.mOldPosition = instance.mPressStart;
    instance.mTriggered = false;
    instance.mDragDist = 0.0f;
  }

  private void Start() => Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);

  private void Update()
  {
    if (this.mReceiver == null)
      return;
    if (this.mReceiver is Object && Object.op_Equality(this.mReceiver as Object, (Object) null))
    {
      this.mReceiver = (IHoldGesture) null;
    }
    else
    {
      if (!this.mTriggered)
      {
        Vector2 vector2_1 = Vector2.op_Implicit(Input.mousePosition);
        HoldGestureObserver holdGestureObserver = this;
        double mDragDist = (double) holdGestureObserver.mDragDist;
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, this.mOldPosition);
        double magnitude = (double) ((Vector2) ref vector2_2).magnitude;
        holdGestureObserver.mDragDist = (float) (mDragDist + magnitude);
        this.mOldPosition = vector2_1;
        if (Input.GetMouseButton(0) && (double) this.mDragDist <= 10.0 && (double) Time.unscaledTime - (double) this.mPressTime >= 0.5)
        {
          this.mTriggered = true;
          this.mReceiver.OnPointerHoldStart();
        }
      }
      if (Input.GetMouseButton(0))
        return;
      this.mReceiver.OnPointerHoldEnd();
      this.mReceiver = (IHoldGesture) null;
    }
  }
}
