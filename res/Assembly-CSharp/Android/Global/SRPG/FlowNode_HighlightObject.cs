// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HighlightObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Highlight Next", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(12, "Reinstantiate and Enter", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/HighlightObject", 32741)]
  [FlowNode.Pin(10, "Highlight", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Remove", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_HighlightObject : FlowNode
  {
    [FlowNode.ShowInInfo]
    [SerializeField]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    private GameObject HighlightTarget;
    [FlowNode.ShowInInfo]
    [SerializeField]
    private bool interactable;
    [FlowNode.ShowInInfo]
    [SerializeField]
    private bool portraitvisible;
    private bool smallhighlight;
    private string UnitID;
    [StringIsTextID(true)]
    [SerializeField]
    private string TextID;
    [SerializeField]
    private EventDialogBubble.Anchors dialogBubbleAnchor;
    private LoadRequest mResourceRequest;
    private static SGHighlightObject highlight;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.CreateHighlightObject();
          FlowNode_HighlightObject.highlight.highlightedObject = this.HighlightTarget;
          FlowNode_HighlightObject.highlight.Highlight(this.UnitID, this.TextID, (SGHighlightObject.OnActivateCallback) (() => this.ActivateOutputLinks(2)), this.dialogBubbleAnchor, this.portraitvisible, this.interactable, this.smallhighlight);
          break;
        case 11:
          if ((UnityEngine.Object) FlowNode_HighlightObject.highlight != (UnityEngine.Object) null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) FlowNode_HighlightObject.highlight.gameObject);
            FlowNode_HighlightObject.highlight = (SGHighlightObject) null;
            break;
          }
          break;
        case 12:
          if ((UnityEngine.Object) FlowNode_HighlightObject.highlight != (UnityEngine.Object) null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) FlowNode_HighlightObject.highlight.gameObject);
            FlowNode_HighlightObject.highlight = (SGHighlightObject) null;
          }
          this.CreateHighlightObject();
          break;
      }
      this.ActivateOutputLinks(1);
    }

    private void Start()
    {
      if (!MonoSingleton<GameManager>.Instance.IsTutorial())
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }

    private void CreateHighlightObject()
    {
      if (!((UnityEngine.Object) FlowNode_HighlightObject.highlight == (UnityEngine.Object) null))
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
        FlowNode_HighlightObject.highlight = gameObject.GetComponent<SGHighlightObject>();
        DebugUtility.LogWarning("highlight:" + (object) FlowNode_HighlightObject.highlight);
      }
    }
  }
}
