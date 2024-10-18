// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEventTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Event/Global Event Trigger")]
  [DisallowMultipleComponent]
  public class GlobalEventTrigger : MonoBehaviour
  {
    public void Trigger(string eventName)
    {
      GlobalEvent.Invoke(eventName, (object) ((Component) this).gameObject);
    }
  }
}
