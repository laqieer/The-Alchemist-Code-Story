// Decompiled with JetBrains decompiler
// Type: GemParticleHitEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class GemParticleHitEffect : MonoBehaviour
{
  private Vector3 mStartPosition;
  public static bool IsEnable;
  public GameObject EffectPrefab;

  private void Start() => this.mStartPosition = ((Component) this).transform.position;

  private void Update()
  {
    if (!GemParticleHitEffect.IsEnable || Object.op_Equality((Object) this.EffectPrefab, (Object) null))
      return;
    GemParticle component = ((Component) this).gameObject.GetComponent<GemParticle>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Inequality((Object) component.TargetObject, (Object) null))
      return;
    Vector3 vector3_1 = Vector3.op_Addition(component.TargetObject.position, component.TargetOffset);
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, this.mStartPosition);
    float magnitude = ((Vector3) ref vector3_2).magnitude;
    Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, ((Component) this).transform.position);
    if (0.20000000298023224 <= (double) ((Vector3) ref vector3_3).magnitude / (double) magnitude)
      return;
    GameUtility.RequireComponent<OneShotParticle>(Object.Instantiate<GameObject>(this.EffectPrefab, vector3_1, Quaternion.identity));
    GemParticleHitEffect.IsEnable = false;
  }

  private void OnDisable()
  {
    GemParticleHitEffect.IsEnable = false;
    Object.Destroy((Object) this);
  }
}
