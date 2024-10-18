// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/セーブデータリセット", 32741)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ResetSaveData : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<GameManager>.Instance.Player.InitPlayerPrefs();
      MonoSingleton<GameManager>.Instance.Player.LoadPlayerPrefs();
      this.ActivateOutputLinks(1);
    }
  }
}
