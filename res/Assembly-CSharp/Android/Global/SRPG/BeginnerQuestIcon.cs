// Decompiled with JetBrains decompiler
// Type: SRPG.BeginnerQuestIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Disabled", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Enabled", FlowNode.PinTypes.Output, 1)]
  public class BeginnerQuestIcon : MonoBehaviour, IFlowInterface
  {
    private const int PLAYER_MAX_LEVEL = 20;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      bool flag = true;
      if (MonoSingleton<GameManager>.Instance.Player.Lv < 20)
      {
        foreach (QuestParam quest in MonoSingleton<GameManager>.Instance.Quests)
        {
          if (quest.iname.Contains("QE_EV_BEGINNER") && quest.state != QuestStates.Cleared)
          {
            flag = false;
            break;
          }
        }
      }
      if (!flag)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      this.gameObject.SetActive(!flag);
    }
  }
}
