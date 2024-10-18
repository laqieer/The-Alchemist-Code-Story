// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayJoinCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MultiPlayJoinCategory : MonoBehaviour
  {
    public void OnClickAll()
    {
      GlobalVars.SelectedMultiPlayArea = string.Empty;
      GlobalVars.SelectedQuestID = string.Empty;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "SELECT_ALL_ROOM");
    }
  }
}
