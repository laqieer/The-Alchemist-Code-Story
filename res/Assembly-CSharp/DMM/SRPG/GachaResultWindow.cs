// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Setup", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Refresh", FlowNode.PinTypes.Output, 10)]
  public class GachaResultWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ThumbnailListWindow;
    public Button BackButton;
    private bool Initalized;
    [SerializeField]
    private TwitterMessage Twitter_iOS;
    [SerializeField]
    private TwitterMessage Twitter_Android;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetUp();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null))
        HomeWindow.Current.SetVisible(true);
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BackButton.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__0)));
      }
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
      if (Object.op_Inequality((Object) this.Twitter_iOS, (Object) null))
        this.Twitter_iOS.SetConditionsKey(GachaResultData.receipt.iname);
      if (Object.op_Inequality((Object) this.Twitter_Android, (Object) null))
        this.Twitter_Android.SetConditionsKey(GachaResultData.receipt.iname);
      if (GachaResultData.drops == null)
        return;
      FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
      FlowNode_Variable.Set("GachaResultSingle", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
