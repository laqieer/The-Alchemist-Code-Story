// Decompiled with JetBrains decompiler
// Type: SyncStateTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SyncStateTime : StateMachineBehaviour
{
  public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if ((double) stateInfo.length <= 0.0 || (double) (stateInfo.length * (stateInfo.normalizedTime % 1f)) > (animator.updateMode != AnimatorUpdateMode.UnscaledTime ? (double) Time.deltaTime : (double) Time.unscaledDeltaTime))
      return;
    float length = stateInfo.length;
    float normalizedTime = (animator.updateMode != AnimatorUpdateMode.UnscaledTime ? Time.time : Time.unscaledTime) % length / length;
    if ((double) normalizedTime <= 0.0)
      return;
    animator.Play(stateInfo.fullPathHash, layerIndex, normalizedTime);
  }
}
