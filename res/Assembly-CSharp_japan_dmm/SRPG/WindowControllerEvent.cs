// Decompiled with JetBrains decompiler
// Type: SRPG.WindowControllerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WindowControllerEvent : StateMachineBehaviour
  {
    public WindowControllerEvent.EventTypes Type;

    public virtual void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      WindowController component = ((Component) animator).GetComponent<WindowController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (this.Type == WindowControllerEvent.EventTypes.Opened)
        component.OnOpen();
      else
        component.OnClose();
    }

    public enum EventTypes
    {
      Opened,
      Closed,
    }
  }
}
