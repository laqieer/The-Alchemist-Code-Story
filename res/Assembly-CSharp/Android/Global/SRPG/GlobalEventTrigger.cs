// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEventTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Event/Global Event Trigger")]
  [DisallowMultipleComponent]
  public class GlobalEventTrigger : MonoBehaviour
  {
    public void Trigger(string eventName)
    {
      GlobalEvent.Invoke(eventName, (object) this.gameObject);
    }
  }
}
