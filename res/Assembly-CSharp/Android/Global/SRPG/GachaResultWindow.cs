// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Setup", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Refresh", FlowNode.PinTypes.Output, 10)]
  public class GachaResultWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ThumbnailListWindow;
    public Button BackButton;
    private bool Initalized;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetUp();
    }

    private void Start()
    {
      if ((UnityEngine.Object) HomeWindow.Current != (UnityEngine.Object) null)
        HomeWindow.Current.SetVisible(true);
      if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
        this.BackButton.onClick.AddListener((UnityAction) (() => this.OnCloseWindow(this.BackButton)));
      this.Initalized = true;
    }

    private void OnCloseWindow(Button button)
    {
      if (!this.Initalized)
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSED_RESULT");
    }

    private void SetUp()
    {
      if (GachaResultData.drops == null)
        return;
      FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
      FlowNode_Variable.Set("GachaResultSingle", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
