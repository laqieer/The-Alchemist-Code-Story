// Decompiled with JetBrains decompiler
// Type: HoldGestureObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if ((Object) HoldGestureObserver.mInstance == (Object) null)
        HoldGestureObserver.mInstance = new GameObject(nameof (HoldGestureObserver)).AddComponent<HoldGestureObserver>();
      return HoldGestureObserver.mInstance;
    }
  }

  public static void StartHoldGesture(IHoldGesture receiver)
  {
    HoldGestureObserver instance = HoldGestureObserver.Instance;
    instance.mReceiver = receiver;
    instance.mPressTime = Time.unscaledTime;
    instance.mPressStart = (Vector2) Input.mousePosition;
    instance.mOldPosition = instance.mPressStart;
    instance.mTriggered = false;
    instance.mDragDist = 0.0f;
  }

  private void Start()
  {
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Update()
  {
    if (this.mReceiver == null)
      return;
    if ((object) (this.mReceiver as Object) != null && this.mReceiver as Object == (Object) null)
    {
      this.mReceiver = (IHoldGesture) null;
    }
    else
    {
      if (!this.mTriggered)
      {
        Vector2 mousePosition = (Vector2) Input.mousePosition;
        this.mDragDist += (mousePosition - this.mOldPosition).magnitude;
        this.mOldPosition = mousePosition;
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
