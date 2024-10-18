// Decompiled with JetBrains decompiler
// Type: SRPG.ReviewAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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

    private void Success() => Application.OpenURL(this.url);
  }
}
