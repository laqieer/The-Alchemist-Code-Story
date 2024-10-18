// Decompiled with JetBrains decompiler
// Type: SRPG.ReviewAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
