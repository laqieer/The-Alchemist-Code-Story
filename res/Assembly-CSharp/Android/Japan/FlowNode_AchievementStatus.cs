// Decompiled with JetBrains decompiler
// Type: FlowNode_AchievementStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;

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
