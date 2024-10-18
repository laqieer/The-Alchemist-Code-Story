// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayJoinCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
