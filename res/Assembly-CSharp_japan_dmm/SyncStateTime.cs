// Decompiled with JetBrains decompiler
// Type: SyncStateTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SyncStateTime : StateMachineBehaviour
{
  public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if ((double) ((AnimatorStateInfo) ref stateInfo).length <= 0.0 || (double) (((AnimatorStateInfo) ref stateInfo).length * (((AnimatorStateInfo) ref stateInfo).normalizedTime % 1f)) > (animator.updateMode != 2 ? (double) Time.deltaTime : (double) Time.unscaledDeltaTime))
      return;
    float length = ((AnimatorStateInfo) ref stateInfo).length;
    float num = (animator.updateMode != 2 ? Time.time : Time.unscaledTime) % length / length;
    if ((double) num <= 0.0)
      return;
    animator.Play(((AnimatorStateInfo) ref stateInfo).fullPathHash, layerIndex, num);
  }
}
