// Decompiled with JetBrains decompiler
// Type: InputToEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class InputToEvent : MonoBehaviour
{
  private GameObject lastGo;
  public static Vector3 inputHitPos;
  public bool DetectPointedAtGameObject;
  private Vector2 pressedPosition = Vector2.zero;
  private Vector2 currentPos = Vector2.zero;
  public bool Dragging;
  private Camera m_Camera;

  public static GameObject goPointedAt { get; private set; }

  public Vector2 DragVector
  {
    get
    {
      return this.Dragging ? Vector2.op_Subtraction(this.currentPos, this.pressedPosition) : Vector2.zero;
    }
  }

  private void Start() => this.m_Camera = ((Component) this).GetComponent<Camera>();

  private void Update()
  {
    if (this.DetectPointedAtGameObject)
      InputToEvent.goPointedAt = this.RaycastObject(Vector2.op_Implicit(Input.mousePosition));
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      this.currentPos = ((Touch) ref touch).position;
      if (((Touch) ref touch).phase == null)
      {
        this.Press(((Touch) ref touch).position);
      }
      else
      {
        if (((Touch) ref touch).phase != 3)
          return;
        this.Release(((Touch) ref touch).position);
      }
    }
    else
    {
      this.currentPos = Vector2.op_Implicit(Input.mousePosition);
      if (Input.GetMouseButtonDown(0))
        this.Press(Vector2.op_Implicit(Input.mousePosition));
      if (Input.GetMouseButtonUp(0))
        this.Release(Vector2.op_Implicit(Input.mousePosition));
      if (!Input.GetMouseButtonDown(1))
        return;
      this.pressedPosition = Vector2.op_Implicit(Input.mousePosition);
      this.lastGo = this.RaycastObject(this.pressedPosition);
      if (!Object.op_Inequality((Object) this.lastGo, (Object) null))
        return;
      this.lastGo.SendMessage("OnPressRight", (SendMessageOptions) 1);
    }
  }

  private void Press(Vector2 screenPos)
  {
    this.pressedPosition = screenPos;
    this.Dragging = true;
    this.lastGo = this.RaycastObject(screenPos);
    if (!Object.op_Inequality((Object) this.lastGo, (Object) null))
      return;
    this.lastGo.SendMessage("OnPress", (SendMessageOptions) 1);
  }

  private void Release(Vector2 screenPos)
  {
    if (Object.op_Inequality((Object) this.lastGo, (Object) null))
    {
      if (Object.op_Equality((Object) this.RaycastObject(screenPos), (Object) this.lastGo))
        this.lastGo.SendMessage("OnClick", (SendMessageOptions) 1);
      this.lastGo.SendMessage("OnRelease", (SendMessageOptions) 1);
      this.lastGo = (GameObject) null;
    }
    this.pressedPosition = Vector2.zero;
    this.Dragging = false;
  }

  private GameObject RaycastObject(Vector2 screenPos)
  {
    RaycastHit raycastHit;
    if (!Physics.Raycast(this.m_Camera.ScreenPointToRay(Vector2.op_Implicit(screenPos)), ref raycastHit, 200f))
      return (GameObject) null;
    InputToEvent.inputHitPos = ((RaycastHit) ref raycastHit).point;
    return ((Component) ((RaycastHit) ref raycastHit).collider).gameObject;
  }
}
