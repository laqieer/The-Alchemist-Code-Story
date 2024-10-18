// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Master/ClearMasterParam", 32741)]
  [FlowNode.Pin(101, "Start", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "End", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ClearMasterParam : FlowNode
  {
    private const int PIN_INPUT_START = 101;
    private const int PIN_OUTPUT_END = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Clear();
      this.ActivateOutputLinks(1001);
    }

    private void Clear()
    {
      MonoSingleton<GameManager>.Instance.MasterParam = new MasterParam();
      CharacterDB.UnloadAll();
      SkillSequence.UnloadAll();
    }
  }
}
