// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Start(Restore)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Start(Mission)", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Refresh", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Chara", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Collabo", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Chara(Restore or Mission)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Collabo(Restore or Mission)", FlowNode.PinTypes.Output, 13)]
  public class CharacterQuestController : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_START = 0;
    private const int PIN_ID_START_RESTORE = 1;
    private const int PIN_ID_START_MISSION = 2;
    private const int PIN_ID_REFRESH = 3;
    private const int PIN_ID_CHARA = 10;
    private const int PIN_ID_COLLABO = 11;
    private const int PIN_ID_CHARA_RESTORE_OR_MISSION = 12;
    private const int PIN_ID_COLLABO_RESTORE_OR_MISSION = 13;
    public string VARIABLE_KEY = "CHARA_QUEST_TYPE";
    public string VARIABLE_VALUE_CHARA = "CHARA";
    public string VARIABLE_VALUE_COLLABO = "COLLABO";

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
        case 2:
          string a1 = FlowNode_Variable.Get(this.VARIABLE_KEY);
          if (string.IsNullOrEmpty(a1))
          {
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (string.Equals(a1, this.VARIABLE_VALUE_CHARA))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
            break;
          }
          if (!string.Equals(a1, this.VARIABLE_VALUE_COLLABO))
            break;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
          break;
        case 3:
          string a2 = FlowNode_Variable.Get(this.VARIABLE_KEY);
          if (string.IsNullOrEmpty(a2))
          {
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (string.Equals(a2, this.VARIABLE_VALUE_CHARA))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (!string.Equals(a2, this.VARIABLE_VALUE_COLLABO))
            break;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
      }
    }
  }
}
