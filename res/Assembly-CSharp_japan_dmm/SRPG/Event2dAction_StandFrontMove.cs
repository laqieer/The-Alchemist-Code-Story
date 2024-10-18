// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandFrontMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/立ち絵2/最前面移動(2D)", "指定した立ち絵を最前面に持ってきます", 5592405, 4473992)]
  public class Event2dAction_StandFrontMove : EventAction
  {
    public string CharaID;
    private EventDialogBubbleCustom mBubble;

    public override void PreStart()
    {
    }

    public override void OnActivate()
    {
      if (!string.IsNullOrEmpty(this.CharaID) && EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        foreach (EventStandCharaController2 instance in EventStandCharaController2.Instances)
        {
          if (instance.CharaID == this.CharaID)
          {
            if (!instance.IsClose)
            {
              ((Component) instance).transform.SetAsLastSibling();
              ((Component) instance).transform.SetSiblingIndex(((Component) instance).transform.GetSiblingIndex() - 1);
            }
          }
          else if (!instance.IsClose)
            ;
        }
      }
      this.ActivateNext();
    }
  }
}
