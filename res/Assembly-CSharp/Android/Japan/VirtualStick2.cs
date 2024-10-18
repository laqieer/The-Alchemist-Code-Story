// Decompiled with JetBrains decompiler
// Type: VirtualStick2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VirtualStick2 : MonoBehaviour
{
  public float Radius = 100f;
  public string VisibleBool = "open";
  private RectTransform mTransform;
  public RectTransform Knob;
  private Animator mAnimator;
  private Vector2 mInputDelta;
  private bool mVisible;

  private void Start()
  {
    this.mAnimator = this.GetComponent<Animator>();
    this.mTransform = this.transform as RectTransform;
  }

  public void SetPosition(Vector2 position)
  {
    this.mTransform.anchoredPosition = position;
  }

  public bool Visible
  {
    set
    {
      if (this.Visible != value)
        this.mInputDelta = Vector2.zero;
      this.mVisible = value;
    }
    get
    {
      return this.mVisible;
    }
  }

  public Vector2 Delta
  {
    set
    {
      this.mInputDelta = value;
      if ((double) this.mInputDelta.magnitude >= (double) this.Radius)
      {
        this.mInputDelta.Normalize();
        this.mInputDelta *= this.Radius;
      }
      if (!((Object) this.Knob != (Object) null))
        return;
      this.Knob.anchoredPosition = this.mInputDelta;
    }
  }

  public Vector2 Velocity
  {
    get
    {
      return this.mInputDelta * (1f / this.Radius);
    }
  }

  private void Update()
  {
    if (!((Object) this.mAnimator != (Object) null))
      return;
    this.mAnimator.SetBool(this.VisibleBool, this.mVisible);
  }
}
