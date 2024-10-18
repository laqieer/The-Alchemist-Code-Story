// Decompiled with JetBrains decompiler
// Type: VirtualStick2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class VirtualStick2 : MonoBehaviour
{
  private RectTransform mTransform;
  public RectTransform Knob;
  public float Radius = 100f;
  private Animator mAnimator;
  private Vector2 mInputDelta;
  private bool mVisible;
  public string VisibleBool = "open";

  private void Start()
  {
    this.mAnimator = ((Component) this).GetComponent<Animator>();
    this.mTransform = ((Component) this).transform as RectTransform;
  }

  public void SetPosition(Vector2 position) => this.mTransform.anchoredPosition = position;

  public bool Visible
  {
    set
    {
      if (this.Visible != value)
        this.mInputDelta = Vector2.zero;
      this.mVisible = value;
    }
    get => this.mVisible;
  }

  public Vector2 Delta
  {
    set
    {
      this.mInputDelta = value;
      if ((double) ((Vector2) ref this.mInputDelta).magnitude >= (double) this.Radius)
      {
        ((Vector2) ref this.mInputDelta).Normalize();
        VirtualStick2 virtualStick2 = this;
        virtualStick2.mInputDelta = Vector2.op_Multiply(virtualStick2.mInputDelta, this.Radius);
      }
      if (!Object.op_Inequality((Object) this.Knob, (Object) null))
        return;
      this.Knob.anchoredPosition = this.mInputDelta;
    }
  }

  public Vector2 Velocity => Vector2.op_Multiply(this.mInputDelta, 1f / this.Radius);

  private void Update()
  {
    if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
      return;
    this.mAnimator.SetBool(this.VisibleBool, this.mVisible);
  }
}
