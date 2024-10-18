// Decompiled with JetBrains decompiler
// Type: InputToEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class InputToEvent : MonoBehaviour
{
  private Vector2 pressedPosition = Vector2.zero;
  private Vector2 currentPos = Vector2.zero;
  private GameObject lastGo;
  public static Vector3 inputHitPos;
  public bool DetectPointedAtGameObject;
  public bool Dragging;
  private Camera m_Camera;

  public static GameObject goPointedAt { get; private set; }

  public Vector2 DragVector
  {
    get
    {
      if (this.Dragging)
        return this.currentPos - this.pressedPosition;
      return Vector2.zero;
    }
  }

  private void Start()
  {
    this.m_Camera = this.GetComponent<Camera>();
  }

  private void Update()
  {
    if (this.DetectPointedAtGameObject)
      InputToEvent.goPointedAt = this.RaycastObject((Vector2) Input.mousePosition);
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      this.currentPos = touch.position;
      if (touch.phase == TouchPhase.Began)
      {
        this.Press(touch.position);
      }
      else
      {
        if (touch.phase != TouchPhase.Ended)
          return;
        this.Release(touch.position);
      }
    }
    else
    {
      this.currentPos = (Vector2) Input.mousePosition;
      if (Input.GetMouseButtonDown(0))
        this.Press((Vector2) Input.mousePosition);
      if (Input.GetMouseButtonUp(0))
        this.Release((Vector2) Input.mousePosition);
      if (!Input.GetMouseButtonDown(1))
        return;
      this.pressedPosition = (Vector2) Input.mousePosition;
      this.lastGo = this.RaycastObject(this.pressedPosition);
      if (!((Object) this.lastGo != (Object) null))
        return;
      this.lastGo.SendMessage("OnPressRight", SendMessageOptions.DontRequireReceiver);
    }
  }

  private void Press(Vector2 screenPos)
  {
    this.pressedPosition = screenPos;
    this.Dragging = true;
    this.lastGo = this.RaycastObject(screenPos);
    if (!((Object) this.lastGo != (Object) null))
      return;
    this.lastGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
  }

  private void Release(Vector2 screenPos)
  {
    if ((Object) this.lastGo != (Object) null)
    {
      if ((Object) this.RaycastObject(screenPos) == (Object) this.lastGo)
        this.lastGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
      this.lastGo.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
      this.lastGo = (GameObject) null;
    }
    this.pressedPosition = Vector2.zero;
    this.Dragging = false;
  }

  private GameObject RaycastObject(Vector2 screenPos)
  {
    RaycastHit hitInfo;
    if (!Physics.Raycast(this.m_Camera.ScreenPointToRay((Vector3) screenPos), out hitInfo, 200f))
      return (GameObject) null;
    InputToEvent.inputHitPos = hitInfo.point;
    return hitInfo.collider.gameObject;
  }
}
