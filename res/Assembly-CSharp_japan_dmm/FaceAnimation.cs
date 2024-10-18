// Decompiled with JetBrains decompiler
// Type: FaceAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class FaceAnimation : MonoBehaviour
{
  public bool isNewModel;
  public int Columns = 4;
  public int Rows = 4;
  public int NumTiles = 10;
  [HideInInspector]
  public int HeadMaterialIndex = 1;
  [NonSerialized]
  public int Face0;
  [NonSerialized]
  public int Face1;
  private Material mMaterial;
  public bool PlayAnimation;
  public FaceAnimation.FaceAnimationStruct Animation0 = new FaceAnimation.FaceAnimationStruct();
  public FaceAnimation.FaceAnimationStruct Animation1 = new FaceAnimation.FaceAnimationStruct();

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
    ref FaceAnimation.FaceAnimationStruct local = ref slot;
    double num1 = (double) slot.Time + (double) slot.Speed * (double) Time.deltaTime;
    Keyframe keyframe = slot.Curve[slot.Curve.length - 1];
    double time = (double) ((Keyframe) ref keyframe).time;
    double num2 = num1 % time;
    local.Time = (float) num2;
  }

  private void OnWillRenderObject()
  {
    Vector4 vector4 = Vector4.zero;
    this.mMaterial = !this.isNewModel ? ((Component) this).GetComponent<Renderer>().material : ((Component) this).GetComponent<Renderer>().materials[this.HeadMaterialIndex];
    this.Face0 = Mathf.Clamp(this.Face0, 0, this.NumTiles - 1);
    this.Face1 = Mathf.Clamp(this.Face1, 0, this.NumTiles - 1);
    float num1 = 1f / (float) this.Columns;
    float num2 = 1f / (float) this.Rows;
    vector4.x = (float) (this.Face0 % this.Columns) * num1;
    vector4.y = -num2 * (float) (this.Face0 / this.Columns);
    vector4.z = (float) (this.Face1 % this.Columns) * num1;
    vector4.w = -num2 * (float) (this.Face1 / this.Columns);
    if (this.isNewModel)
      vector4 = Vector4.op_Multiply(vector4, 0.5f);
    this.mMaterial.SetVector("_texParam", vector4);
  }

  public struct FaceAnimationStruct
  {
    public AnimationCurve Curve;
    public float Time;
    public float Speed;
  }
}
