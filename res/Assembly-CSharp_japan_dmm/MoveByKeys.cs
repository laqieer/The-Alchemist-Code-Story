// Decompiled with JetBrains decompiler
// Type: MoveByKeys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class MoveByKeys : MonoBehaviour
{
  public float Speed = 10f;
  public float JumpForce = 200f;
  public float JumpTimeout = 0.5f;
  private bool isSprite;
  private float jumpingTime;
  private Rigidbody body;
  private Rigidbody2D body2d;

  public void Start()
  {
    this.isSprite = Object.op_Inequality((Object) ((Component) this).GetComponent<SpriteRenderer>(), (Object) null);
    this.body2d = ((Component) this).GetComponent<Rigidbody2D>();
    this.body = ((Component) this).GetComponent<Rigidbody>();
  }

  public void FixedUpdate()
  {
    if (!this.photonView.isMine)
      return;
    if ((double) Input.GetAxisRaw("Horizontal") < -0.10000000149011612 || (double) Input.GetAxisRaw("Horizontal") > 0.10000000149011612)
    {
      Transform transform = ((Component) this).transform;
      transform.position = Vector3.op_Addition(transform.position, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.right, this.Speed * Time.deltaTime), Input.GetAxisRaw("Horizontal")));
    }
    if ((double) this.jumpingTime <= 0.0)
    {
      if ((Object.op_Inequality((Object) this.body, (Object) null) || Object.op_Inequality((Object) this.body2d, (Object) null)) && Input.GetKey((KeyCode) 32))
      {
        this.jumpingTime = this.JumpTimeout;
        Vector2 vector2 = Vector2.op_Multiply(Vector2.up, this.JumpForce);
        if (Object.op_Inequality((Object) this.body2d, (Object) null))
          this.body2d.AddForce(vector2);
        else if (Object.op_Inequality((Object) this.body, (Object) null))
          this.body.AddForce(Vector2.op_Implicit(vector2));
      }
    }
    else
      this.jumpingTime -= Time.deltaTime;
    if (this.isSprite || (double) Input.GetAxisRaw("Vertical") >= -0.10000000149011612 && (double) Input.GetAxisRaw("Vertical") <= 0.10000000149011612)
      return;
    Transform transform1 = ((Component) this).transform;
    transform1.position = Vector3.op_Addition(transform1.position, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.forward, this.Speed * Time.deltaTime), Input.GetAxisRaw("Vertical")));
  }
}
