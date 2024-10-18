// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PrepareSceneChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Scene/PrepareSceneChange")]
  [FlowNode.Pin(0, "Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_PrepareSceneChange : FlowNodePersistent
  {
    private bool mStart;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!MonoSingleton<GameManager>.Instance.PrepareSceneChange())
        this.Cancel();
      else
        this.mStart = true;
    }

    private void Reset()
    {
      this.mStart = false;
    }

    private void Done()
    {
      this.Reset();
      this.ActivateOutputLinks(0);
    }

    private void Cancel()
    {
      this.Reset();
      this.ActivateOutputLinks(1);
    }

    private void Update()
    {
      if (!this.mStart || MonoSingleton<GameManager>.Instance.IsImportantJobRunning)
        return;
      this.Done();
    }
  }
}
