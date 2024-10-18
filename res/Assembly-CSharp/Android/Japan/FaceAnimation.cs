// Decompiled with JetBrains decompiler
// Type: FaceAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[ExecuteInEditMode]
public class FaceAnimation : MonoBehaviour
{
  public int Columns = 4;
  public int Rows = 4;
  public int NumTiles = 10;
  [HideInInspector]
  public int HeadMaterialIndex = 1;
  public FaceAnimation.FaceAnimationStruct Animation0 = new FaceAnimation.FaceAnimationStruct();
  public FaceAnimation.FaceAnimationStruct Animation1 = new FaceAnimation.FaceAnimationStruct();
  public bool isNewModel;
  [NonSerialized]
  public int Face0;
  [NonSerialized]
  public int Face1;
  private Material mMaterial;
  public bool PlayAnimation;

  public void SetAnimation(CurveAsset asset)
  {
    this.Animation0.Curve = asset.FindCurve("FAC0");
    this.Animation0.Time = 0.0f;
    this.Animation0.Speed = 1f;
    this.Animation1.Curve = asset.FindCurve("FAC1");
    this.Animation1.Time = 0.0f;
    this.Animation1.Speed = 1f;
    this.PlayAnimation = true;
  }

  private void LateUpdate()
  {
    if (!this.PlayAnimation)
      return;
    if (this.Animation0.Curve == null && this.Animation1.Curve == null)
    {
      this.PlayAnimation = false;
    }
    else
    {
      this.UpdateAnimation(ref this.Animation0);
      this.UpdateAnimation(ref this.Animation1);
      if (this.Animation0.Curve != null)
        this.Face0 = Mathf.FloorToInt(this.Animation0.Curve.Evaluate(this.Animation0.Time));
      if (this.Animation1.Curve == null)
        return;
      this.Face1 = Mathf.FloorToInt(this.Animation1.Curve.Evaluate(this.Animation1.Time));
    }
  }

  private void UpdateAnimation(ref FaceAnimation.FaceAnimationStruct slot)
  {
    if (slot.Curve == null || slot.Curve.keys.Length < 2)
      return;
    slot.Time = (slot.Time + slot.Speed * Time.deltaTime) % slot.Curve[slot.Curve.length - 1].time;
  }

  private void OnWillRenderObject()
  {
    Vector4 zero = Vector4.zero;
    this.mMaterial = !this.isNewModel ? this.GetComponent<Renderer>().material : this.GetComponent<Renderer>().materials[this.HeadMaterialIndex];
    this.Face0 = Mathf.Clamp(this.Face0, 0, this.NumTiles - 1);
    this.Face1 = Mathf.Clamp(this.Face1, 0, this.NumTiles - 1);
    float num1 = 1f / (float) this.Columns;
    float num2 = 1f / (float) this.Rows;
    Vector4[] values = new Vector4[4];
    values[0] = Vector4.zero;
    values[3] = Vector4.zero;
    zero.x = (float) (this.Face0 % this.Columns) * num1;
    zero.y = -num2 * (float) (this.Face0 / this.Columns);
    if (this.isNewModel)
      zero *= 0.5f;
    values[1] = zero;
    zero.x = (float) (this.Face1 % this.Columns) * num1;
    zero.y = -num2 * (float) (this.Face1 / this.Columns);
    if (this.isNewModel)
      zero *= 0.5f;
    values[2] = zero;
    this.mMaterial.SetVectorArray("_texParam", values);
  }

  public struct FaceAnimationStruct
  {
    public AnimationCurve Curve;
    public float Time;
    public float Speed;
  }
}
