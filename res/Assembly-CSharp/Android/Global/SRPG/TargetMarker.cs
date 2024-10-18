// Decompiled with JetBrains decompiler
// Type: SRPG.TargetMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TargetMarker : MonoBehaviour
  {
    private Transform m_Transform;

    private void Awake()
    {
      this.m_Transform = this.GetComponent<Transform>();
    }

    private void LateUpdate()
    {
      SceneBattle instance = SceneBattle.Instance;
      Vector3 zero = Vector3.zero;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.isUpView)
        zero.y += 0.65f;
      this.m_Transform.localPosition = zero;
    }
  }
}
