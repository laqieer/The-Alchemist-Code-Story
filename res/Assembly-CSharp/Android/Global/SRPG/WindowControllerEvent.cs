// Decompiled with JetBrains decompiler
// Type: SRPG.WindowControllerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class WindowControllerEvent : StateMachineBehaviour
  {
    public WindowControllerEvent.EventTypes Type;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      WindowController component = animator.GetComponent<WindowController>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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
