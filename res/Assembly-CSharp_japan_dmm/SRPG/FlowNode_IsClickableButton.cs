// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsClickableButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/IsClickableButton", 58751)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Is Clickable", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Is Not Clickable", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "Error", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_IsClickableButton : FlowNode
  {
    private const int PININ_CHECK = 1;
    private const int PINOUT_IS_CLICKABLE = 2;
    private const int PINOUT_IS_NOT_CLICKABLE = 3;
    private const int PINOUT_ERROR = 10;
    [SerializeField]
    private Canvas targetCanvas;
    [SerializeField]
    private Button targetButton;
    private RectTransform targetRect;
    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    public override void OnActivate(int pinID)
    {
      if (Object.op_Equality((Object) this.targetCanvas, (Object) null) || Object.op_Equality((Object) this.targetButton, (Object) null))
      {
        this.ActivateOutputLinks(10);
      }
      else
      {
        this.targetRect = ((Component) this.targetButton).GetComponent<RectTransform>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
          position = RectTransformUtility.WorldToScreenPoint(this.targetCanvas.worldCamera, ((Transform) this.targetRect).position)
        };
        this.raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, this.raycastResults);
        if (this.raycastResults.Count > 0)
        {
          RaycastResult raycastResult = this.raycastResults[0];
          if (!(((Object) ((RaycastResult) ref raycastResult).gameObject).name != ((Object) ((Component) this.targetButton).gameObject).name))
          {
            this.ActivateOutputLinks(2);
            return;
          }
        }
        this.ActivateOutputLinks(3);
      }
    }
  }
}
