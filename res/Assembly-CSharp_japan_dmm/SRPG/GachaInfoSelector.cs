// Decompiled with JetBrains decompiler
// Type: SRPG.GachaInfoSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Gacha/GachaInfoSelector", 32741)]
  [FlowNode.Pin(100, "Setup", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "Select Detail", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "Select Rate", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(130, "Select Pickup", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(200, "Selected", FlowNode.PinTypes.Output, 200)]
  public class GachaInfoSelector : FlowNodePersistent
  {
    private const int PIN_IN_SETUP_URL = 100;
    private const int PIN_IN_SELECT_DETAIL = 110;
    private const int PIN_IN_SELECT_RATE = 120;
    private const int PIN_IN_SELECT_PICKUP = 130;
    private const int PIN_OT_SELECTED = 200;
    [SerializeField]
    private string url = string.Empty;
    [Header("召喚->提供割合を表示する際のURLのPrefix")]
    [SerializeField]
    private string url_prefix = "_rate";
    [Header("召喚->ピックアップを表示する際のURLのPrefix")]
    [SerializeField]
    private string url_pickup_prefix = "_pick";
    [SerializeField]
    private GameObject RateButton;
    [SerializeField]
    private Button PickupButton;
    private string m_BaseURL = string.Empty;
    private FlowWindowController m_WindowController = new FlowWindowController();

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      GameUtility.SetGameObjectActive(this.RateButton, false);
      GameUtility.SetGameObjectActive((Component) this.PickupButton, false);
    }

    protected void Start()
    {
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.Setup();
          break;
        case 110:
          FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.m_BaseURL);
          this.ActivateOutputLinks(200);
          break;
        case 120:
          FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.m_BaseURL + this.url_prefix);
          this.ActivateOutputLinks(200);
          break;
        case 130:
          FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.m_BaseURL + this.url_pickup_prefix);
          this.ActivateOutputLinks(200);
          break;
      }
    }

    private void Setup()
    {
      this.m_BaseURL = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL2");
      if (!string.IsNullOrEmpty(this.url))
        this.m_BaseURL = this.url;
      if (Object.op_Equality((Object) GachaWindow.Instance, (Object) null))
        return;
      GachaTopParamNew[] currentGacha = GachaWindow.Instance.GetCurrentGacha();
      if (currentGacha == null || currentGacha.Length <= 0)
        return;
      GameUtility.SetGameObjectActive(this.RateButton, currentGacha[0].is_rate_view);
      GameUtility.SetGameObjectActive((Component) this.PickupButton, currentGacha[0].IsPickupView);
    }
  }
}
