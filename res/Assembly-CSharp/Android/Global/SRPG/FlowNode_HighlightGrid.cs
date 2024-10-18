// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HighlightGrid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(12, "Reinstantiate and Enter", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Highlight Next", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "Highlight", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/HighlightGrid", 32741)]
  [FlowNode.Pin(11, "Remove", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_HighlightGrid : FlowNode
  {
    [SerializeField]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    private int gridX;
    [SerializeField]
    private int gridY;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool interactable;
    [FlowNode.ShowInInfo]
    [SerializeField]
    private bool portraitvisible;
    private string UnitID;
    [SerializeField]
    [StringIsTextID(true)]
    private string TextID;
    [SerializeField]
    private EventDialogBubble.Anchors dialogBubbleAnchor;
    private LoadRequest mResourceRequest;
    private static SGHighlightObject highlight;

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.IsTutorial())
        return;
      this.enabled = false;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.CreateHighlightObject();
          FlowNode_HighlightGrid.highlight.highlightedGrid = new IntVector2(this.gridX, this.gridY);
          FlowNode_HighlightGrid.highlight.Highlight(this.UnitID, this.TextID, (SGHighlightObject.OnActivateCallback) (() => this.ActivateOutputLinks(2)), this.dialogBubbleAnchor, this.portraitvisible, this.interactable, false);
          break;
        case 11:
          if ((UnityEngine.Object) FlowNode_HighlightGrid.highlight != (UnityEngine.Object) null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) FlowNode_HighlightGrid.highlight.gameObject);
            FlowNode_HighlightGrid.highlight = (SGHighlightObject) null;
            break;
          }
          break;
        case 12:
          if ((UnityEngine.Object) FlowNode_HighlightGrid.highlight != (UnityEngine.Object) null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) FlowNode_HighlightGrid.highlight.gameObject);
            FlowNode_HighlightGrid.highlight = (SGHighlightObject) null;
          }
          this.CreateHighlightObject();
          break;
      }
      this.ActivateOutputLinks(1);
    }

    private void CreateHighlightObject()
    {
      if (!((UnityEngine.Object) FlowNode_HighlightGrid.highlight == (UnityEngine.Object) null))
        return;
      GameObject original = AssetManager.Load<GameObject>("SGDevelopment/Tutorial/Tutorial_Guidance");
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("Failed to load");
      }
      else
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
        RectTransform component1 = original.GetComponent<RectTransform>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null && (UnityEngine.Object) original.GetComponent<Canvas>() != (UnityEngine.Object) null)
        {
          RectTransform component2 = gameObject.GetComponent<RectTransform>();
          component2.anchorMax = component1.anchorMax;
          component2.anchorMin = component1.anchorMin;
          component2.anchoredPosition = component1.anchoredPosition;
          component2.sizeDelta = component1.sizeDelta;
        }
        FlowNode_HighlightGrid.highlight = gameObject.GetComponent<SGHighlightObject>();
      }
    }
  }
}
