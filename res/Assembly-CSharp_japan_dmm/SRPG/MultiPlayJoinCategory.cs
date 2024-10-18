// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayJoinCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
