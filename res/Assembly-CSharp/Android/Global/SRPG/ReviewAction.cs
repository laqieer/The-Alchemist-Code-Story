// Decompiled with JetBrains decompiler
// Type: SRPG.ReviewAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Action", FlowNode.PinTypes.Input, 1)]
  public class ReviewAction : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    public string url = string.Empty;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnAction();
    }

    private void Start()
    {
    }

    public void OnAction()
    {
      if (string.IsNullOrEmpty(this.url))
        return;
      this.Success();
    }

    private void Success()
    {
      Application.OpenURL(this.url);
    }
  }
}
