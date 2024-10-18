// Decompiled with JetBrains decompiler
// Type: SRPG.VersusUnknownEnemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Unknown", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 3)]
  public class VersusUnknownEnemy : MonoBehaviour, IFlowInterface
  {
    public RawImage_Transparent EnemyImage;
    public GameObject UnknownObj;

    private void RefreshUnknow()
    {
      if ((UnityEngine.Object) this.EnemyImage != (UnityEngine.Object) null)
        this.EnemyImage.color = new Color(0.0f, 0.0f, 0.0f, 1f);
      if (!((UnityEngine.Object) this.UnknownObj != (UnityEngine.Object) null))
        return;
      this.UnknownObj.SetActive(true);
    }

    private void RefreshReset()
    {
      if ((UnityEngine.Object) this.EnemyImage != (UnityEngine.Object) null)
        this.EnemyImage.color = new Color(1f, 1f, 1f, 1f);
      if (!((UnityEngine.Object) this.UnknownObj != (UnityEngine.Object) null))
        return;
      this.UnknownObj.SetActive(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.RefreshUnknow();
          break;
        case 2:
          this.RefreshReset();
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
