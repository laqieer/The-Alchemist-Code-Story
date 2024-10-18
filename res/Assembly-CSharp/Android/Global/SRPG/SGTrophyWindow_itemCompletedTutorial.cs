// Decompiled with JetBrains decompiler
// Type: SRPG.SGTrophyWindow_itemCompletedTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Create", FlowNode.PinTypes.Output, 0)]
  public class SGTrophyWindow_itemCompletedTutorial : MonoBehaviour, IFlowInterface
  {
    private void Start()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(this.gameObject, (TrophyParam) null);
      if (dataOfClass == null || !(dataOfClass.iname == "LOGIN_GLTUTOTIAL_01"))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void Activated(int pinID)
    {
    }
  }
}
