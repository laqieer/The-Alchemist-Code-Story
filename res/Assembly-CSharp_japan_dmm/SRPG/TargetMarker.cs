// Decompiled with JetBrains decompiler
// Type: SRPG.TargetMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TargetMarker : MonoBehaviour
  {
    private Transform m_Transform;

    private void Awake() => this.m_Transform = ((Component) this).GetComponent<Transform>();

    private void LateUpdate()
    {
      SceneBattle instance = SceneBattle.Instance;
      Vector3 zero = Vector3.zero;
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.isUpView)
        zero.y += 0.65f;
      this.m_Transform.localPosition = zero;
    }
  }
}
