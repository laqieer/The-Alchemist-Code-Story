// Decompiled with JetBrains decompiler
// Type: GemParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class GemParticle : MonoBehaviour
{
  public float Speed = 5f;
  public float Damping = 12f;
  public float Delay = 0.8f;
  public float Accel = 20f;
  public GemParticle Prefab;
  [NonSerialized]
  public Transform TargetObject;
  [NonSerialized]
  public Vector3 TargetOffset;
  private float mSpeed;
  private float mDamping;
  private float mDelay;
  private float mAccel;
  private GemParticle.UpdateEvent mOnUpdate;

  private void Start()
  {
    this.Reset();
  }

  public void Reset()
  {
    this.mDelay = this.Delay * UnityEngine.Random.Range(0.9f, 1.1f);
    this.mSpeed = this.Speed * UnityEngine.Random.Range(0.9f, 1.1f);
    this.mDamping = this.Damping * UnityEngine.Random.Range(0.9f, 1.1f);
    this.mAccel = this.Accel * UnityEngine.Random.Range(0.9f, 1.1f);
    this.mOnUpdate = new GemParticle.UpdateEvent(this.MoveToMidPoint);
    ParticleSystem[] componentsInChildren1 = this.gameObject.GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren1.Length; ++index)
      componentsInChildren1[index].emission.enabled = true;
    if (!((UnityEngine.Object) this.Prefab != (UnityEngine.Object) null))
      return;
    ParticleSystem[] componentsInChildren2 = this.Prefab.GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren2.Length && index < componentsInChildren1.Length; ++index)
      componentsInChildren1[index].loop = componentsInChildren2[index].loop;
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
    this.mSpeed += this.mAccel * Time.deltaTime;
    Vector3 vector3 = this.TargetObject.position + this.TargetOffset - this.transform.position;
    float num = this.mSpeed * Time.deltaTime;
    if ((double) Vector3.Dot(vector3, vector3) <= (double) num * (double) num)
    {
      GameUtility.StopEmitters(this.gameObject);
      this.mSpeed = 0.0f;
      this.mOnUpdate = new GemParticle.UpdateEvent(this.DelayedDeactivate);
    }
    else
      this.transform.rotation = Quaternion.LookRotation(vector3);
  }

  private void DelayedDeactivate()
  {
    foreach (ParticleSystem componentsInChild in this.GetComponentsInChildren<ParticleSystem>())
    {
      if (componentsInChild.particleCount > 0)
        return;
    }
    this.mOnUpdate = (GemParticle.UpdateEvent) null;
    this.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (this.mOnUpdate == null)
      return;
    this.mOnUpdate();
    Transform transform = this.transform;
    transform.position += transform.forward * this.mSpeed * Time.deltaTime;
  }

  private delegate void UpdateEvent();
}
