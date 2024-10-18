// Decompiled with JetBrains decompiler
// Type: SRPG.GachaInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Gacha/GachaInfoWindow", 32741)]
  [FlowNode.Pin(10, "Setup", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "TabSelect Detail", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "TabSelect Rate", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "TabSelect Pickup", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(200, "TabSelected", FlowNode.PinTypes.Output, 200)]
  public class GachaInfoWindow : FlowNodePersistent
  {
    public const int PIN_IN_SETUP_URL = 10;
    public const int PIN_IN_TAB_WEBVIEW = 100;
    public const int PIN_IN_TAB_RATEVIEW = 110;
    public const int PIN_IN_TAB_PICKUPVIEW = 120;
    public const int PIN_OT_TABCHANGED = 200;
    private GachaInfoWindow.Tab m_Tab;
    private FlowWindowController m_WindowController = new FlowWindowController();
    private string m_BaseURL = string.Empty;
    [Header("召喚/提供割合を表示する際のURLのPrefix")]
    [SerializeField]
    private string url_prefix = "_rate";
    [Header("召喚/ピックアップを表示する際のURLのPrefix")]
    [SerializeField]
    private string url_pickup_prefix = "_pick";
    [SerializeField]
    private SharedWebWindow m_Target;
    [SerializeField]
    private Toggle DetailTab;
    [SerializeField]
    private Toggle RateTab;
    [SerializeField]
    private Toggle PickupTab;
    [SerializeField]
    private GachaInfoWindow.Tab InitTab;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      GameUtility.SetGameObjectActive((Component) this.RateTab, false);
      GameUtility.SetGameObjectActive((Component) this.PickupTab, false);
    }

    private void Start()
    {
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update() => this.m_WindowController.Update();

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Setup();
          this.SetTab(this.InitTab);
          break;
        case 100:
          this.SetTab(GachaInfoWindow.Tab.DETAIL);
          if (!Object.op_Inequality((Object) this.m_Target, (Object) null))
            break;
          this.m_Target.UpdateWebView(true);
          break;
        case 110:
          this.SetTab(GachaInfoWindow.Tab.RATE);
          if (!Object.op_Inequality((Object) this.m_Target, (Object) null))
            break;
          this.m_Target.UpdateWebView(true);
          break;
        case 120:
          this.SetTab(GachaInfoWindow.Tab.PICKUP);
          if (!Object.op_Inequality((Object) this.m_Target, (Object) null))
            break;
          this.m_Target.UpdateWebView(true);
          break;
      }
    }

    private void Setup()
    {
      if (Object.op_Equality((Object) GachaWindow.Instance, (Object) null))
        return;
      if (Object.op_Equality((Object) this.m_Target, (Object) null))
        DebugUtility.LogError("Webviewが設定されていません");
      else
        this.m_Target.SetClose(new UIUtility.DialogResultEvent(this.OnClose), ((Component) ((Component) this).transform.Find("CanvasBoundsPanel/window/close")).GetComponent<Button>());
      this.m_BaseURL = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL2");
      GachaTopParamNew[] currentGacha = GachaWindow.Instance.GetCurrentGacha();
      if (currentGacha == null || currentGacha.Length <= 0)
        return;
      GameUtility.SetGameObjectActive((Component) this.RateTab, currentGacha[0].is_rate_view);
      GameUtility.SetGameObjectActive((Component) this.PickupTab, currentGacha[0].IsPickupView);
      if (this.InitTab == GachaInfoWindow.Tab.NONE)
        this.InitTab = !currentGacha[0].IsPickupView ? GachaInfoWindow.Tab.DETAIL : GachaInfoWindow.Tab.PICKUP;
      if (Object.op_Inequality((Object) this.DetailTab, (Object) null))
        GameUtility.SetToggle(this.DetailTab, this.InitTab == GachaInfoWindow.Tab.DETAIL);
      if (Object.op_Inequality((Object) this.RateTab, (Object) null))
        GameUtility.SetToggle(this.RateTab, this.InitTab == GachaInfoWindow.Tab.RATE);
      if (!Object.op_Inequality((Object) this.PickupTab, (Object) null))
        return;
      GameUtility.SetToggle(this.PickupTab, this.InitTab == GachaInfoWindow.Tab.PICKUP);
    }

    public void OnClose(GameObject go) => ButtonEvent.Invoke("GACHAINFO_BTN_CLOSE", (object) this);

    private bool SetTab(GachaInfoWindow.Tab tab)
    {
      bool flag = false;
      string empty = string.Empty;
      if (this.m_Tab != tab)
      {
        flag = true;
        switch (tab)
        {
          case GachaInfoWindow.Tab.DETAIL:
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.m_BaseURL);
            break;
          case GachaInfoWindow.Tab.RATE:
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", !string.IsNullOrEmpty(this.m_BaseURL) ? this.m_BaseURL + this.url_prefix : string.Empty);
            break;
          case GachaInfoWindow.Tab.PICKUP:
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", !string.IsNullOrEmpty(this.m_BaseURL) ? this.m_BaseURL + this.url_pickup_prefix : string.Empty);
            break;
        }
        this.m_Tab = tab;
      }
      return flag;
    }

    public enum Tab : byte
    {
      NONE,
      DETAIL,
      RATE,
      PICKUP,
    }
  }
}
