// Decompiled with JetBrains decompiler
// Type: MoveByKeys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
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
    this.isSprite = (Object) this.GetComponent<SpriteRenderer>() != (Object) null;
    this.body2d = this.GetComponent<Rigidbody2D>();
    this.body = this.GetComponent<Rigidbody>();
  }

  public void FixedUpdate()
  {
    if (!this.photonView.isMine)
      return;
    if ((double) Input.GetAxisRaw("Horizontal") < -0.100000001490116 || (double) Input.GetAxisRaw("Horizontal") > 0.100000001490116)
      this.transform.position += Vector3.right * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Horizontal");
    if ((double) this.jumpingTime <= 0.0)
    {
      if (((Object) this.body != (Object) null || (Object) this.body2d != (Object) null) && Input.GetKey(KeyCode.Space))
      {
        this.jumpingTime = this.JumpTimeout;
        Vector2 force = Vector2.up * this.JumpForce;
        if ((Object) this.body2d != (Object) null)
          this.body2d.AddForce(force);
        else if ((Object) this.body != (Object) null)
          this.body.AddForce((Vector3) force);
      }
    }
    else
      this.jumpingTime -= Time.deltaTime;
    if (this.isSprite || (double) Input.GetAxisRaw("Vertical") >= -0.100000001490116 && (double) Input.GetAxisRaw("Vertical") <= 0.100000001490116)
      return;
    this.transform.position += Vector3.forward * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Vertical");
  }
}
