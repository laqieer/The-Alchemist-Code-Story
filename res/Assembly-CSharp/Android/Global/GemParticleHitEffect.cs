// Decompiled with JetBrains decompiler
// Type: GemParticleHitEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GemParticleHitEffect : MonoBehaviour
{
  private Vector3 mStartPosition;
  public static bool IsEnable;
  public GameObject EffectPrefab;

  private void Start()
  {
    this.mStartPosition = this.transform.position;
  }

  private void Update()
  {
    if (!GemParticleHitEffect.IsEnable || (Object) this.EffectPrefab == (Object) null)
      return;
    GemParticle component = this.gameObject.GetComponent<GemParticle>();
    if (!((Object) component != (Object) null) || !((Object) component.TargetObject != (Object) null))
      return;
    Vector3 position = component.TargetObject.position + component.TargetOffset;
    float magnitude = (position - this.mStartPosition).magnitude;
    if (0.200000002980232 <= (double) (position - this.transform.position).magnitude / (double) magnitude)
      return;
    GameUtility.RequireComponent<OneShotParticle>(Object.Instantiate((Object) this.EffectPrefab, position, Quaternion.identity) as GameObject);
    GemParticleHitEffect.IsEnable = false;
  }

  private void OnDisable()
  {
    GemParticleHitEffect.IsEnable = false;
    Object.Destroy((Object) this);
  }
}
