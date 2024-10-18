// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_WaitAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/アセットを待つ", 32741)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_WaitAssets : FlowNode
  {
    public const int PINID_START = 0;
    public const int PINID_FINISHED = 10;
    private const float MIN_WAIT_TIME = 0.1f;
    private float mWaitTime;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.enabled = true;
      this.mWaitTime = 0.1f;
    }

    private void Update()
    {
      if (!AssetManager.IsLoading)
      {
        this.mWaitTime = Mathf.Max(this.mWaitTime - Time.unscaledDeltaTime, 0.0f);
        if ((double) this.mWaitTime > 0.0)
          return;
        this.enabled = false;
        this.ActivateOutputLinks(10);
      }
      else
        this.mWaitTime = 0.1f;
    }
  }
}
