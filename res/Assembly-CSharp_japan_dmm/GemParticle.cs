// Decompiled with JetBrains decompiler
// Type: GemParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
public class GemParticle : MonoBehaviour
{
  public GemParticle Prefab;
  [NonSerialized]
  public Transform TargetObject;
  [NonSerialized]
  public Vector3 TargetOffset;
  public float Speed = 5f;
  public float Damping = 12f;
  public float Delay = 0.8f;
  public float Accel = 20f;
  private float mSpeed;
  private float mDamping;
  private float mDelay;
  private float mAccel;
  private GemParticle.UpdateEvent mOnUpdate;

  private void Start() => this.Reset();

  public void Reset()
  {
    this.mDelay = this.Delay * Random.Range(0.9f, 1.1f);
    this.mSpeed = this.Speed * Random.Range(0.9f, 1.1f);
    this.mDamping = this.Damping * Random.Range(0.9f, 1.1f);
    this.mAccel = this.Accel * Random.Range(0.9f, 1.1f);
    this.mOnUpdate = new GemParticle.UpdateEvent(this.MoveToMidPoint);
    ParticleSystem[] componentsInChildren1 = ((Component) this).gameObject.GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren1.Length; ++index)
    {
      ParticleSystem.EmissionModule emission = componentsInChildren1[index].emission;
      ((ParticleSystem.EmissionModule) ref emission).enabled = true;
    }
    if (!Object.op_Inequality((Object) this.Prefab, (Object) null))
      return;
    ParticleSystem[] componentsInChildren2 = ((Component) this.Prefab).GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren2.Length && index < componentsInChildren1.Length; ++index)
    {
      ParticleSystem.MainModule main1 = componentsInChildren1[index].main;
      ref ParticleSystem.MainModule local = ref main1;
      ParticleSystem.MainModule main2 = componentsInChildren2[index].main;
      int num = ((ParticleSystem.MainModule) ref main2).loop ? 1 : 0;
      ((ParticleSystem.MainModule) ref local).loop = num != 0;
    }
  }

  private void StopPartical()
  {
    GameUtility.StopEmitters(((Component) this).gameObject);
    this.mSpeed = 0.0f;
    this.mOnUpdate = new GemParticle.UpdateEvent(this.DelayedDeactivate);
  }

  private void MoveToMidPoint()
  {
    this.mSpeed = Mathf.Max(this.mSpeed - this.mDamping * Time.deltaTime, 0.0f);
    this.mDelay -= Time.deltaTime;
    if ((double) this.mDelay > 0.0)
      return;
    this.mOnUpdate = new GemParticle.UpdateEvent(this.MoveToGoal);
  }

  private void MoveToGoal()
  {
    if (Object.op_Equality((Object) this.TargetObject, (Object) null))
    {
      this.StopPartical();
    }
    else
    {
      this.mSpeed += this.mAccel * Time.deltaTime;
      Vector3 vector3 = Vector3.op_Subtraction(Vector3.op_Addition(this.TargetObject.position, this.TargetOffset), ((Component) this).transform.position);
      float num = this.mSpeed * Time.deltaTime;
      if ((double) Vector3.Dot(vector3, vector3) <= (double) num * (double) num)
        this.StopPartical();
      else
        ((Component) this).transform.rotation = Quaternion.LookRotation(vector3);
    }
  }

  private void DelayedDeactivate()
  {
    foreach (ParticleSystem componentsInChild in ((Component) this).GetComponentsInChildren<ParticleSystem>())
    {
      if (componentsInChild.particleCount > 0)
        return;
    }
    this.mOnUpdate = (GemParticle.UpdateEvent) null;
    ((Component) this).gameObject.SetActive(false);
  }

  private void Update()
  {
    if (this.mOnUpdate == null)
      return;
    this.mOnUpdate();
    Transform transform1 = ((Component) this).transform;
    Transform transform2 = transform1;
    transform2.position = Vector3.op_Addition(transform2.position, Vector3.op_Multiply(Vector3.op_Multiply(transform1.forward, this.mSpeed), Time.deltaTime));
  }

  private delegate void UpdateEvent();
}
