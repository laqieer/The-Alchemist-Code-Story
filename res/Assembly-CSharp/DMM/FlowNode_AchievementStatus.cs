// Decompiled with JetBrains decompiler
// Type: FlowNode_AchievementStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
[FlowNode.NodeType("Achievement/Status", 58751)]
[FlowNode.Pin(1, "Turn Auth True", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(2, "Turn Auth False", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AchievementStatus : FlowNodePersistent
{
  private bool mIsAuth;

  private void Update()
  {
    bool flag = GameCenterManager.IsAuth();
    if (this.mIsAuth != flag)
    {
      if (flag)
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }
    this.mIsAuth = flag;
  }
}
