// Decompiled with JetBrains decompiler
// Type: SRPG.WindowControllerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
