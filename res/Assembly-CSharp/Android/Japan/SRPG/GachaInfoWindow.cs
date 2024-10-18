// Decompiled with JetBrains decompiler
// Type: SRPG.GachaInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/Gacha/GachaInfoWindow", 32741)]
  [FlowNode.Pin(10, "Setup", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "TabSelect Detail", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "TabSelect Rate", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(200, "TabSelected", FlowNode.PinTypes.Output, 200)]
  public class GachaInfoWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    private string m_BaseURL = string.Empty;
    [SerializeField]
    private string url_prefix = "_rate";
    public const int PIN_IN_SETUP_URL = 10;
    public const int PIN_IN_TAB_WEBVIEW = 100;
    public const int PIN_IN_TAB_RATEVIEW = 110;
    public const int PIN_OT_TABCHANGED = 200;
    private GachaInfoWindow.Tab m_Tab;
    [SerializeField]
    private SharedWebWindow m_Target;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.Setup();
      this.SetTab(GachaInfoWindow.Tab.DETAIL);
    }

    private void Start()
    {
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update()
    {
      this.m_WindowController.Update();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 10)
      {
        this.Setup();
        this.SetTab(GachaInfoWindow.Tab.DETAIL);
      }
      if (pinID == 100)
      {
        this.SetTab(GachaInfoWindow.Tab.DETAIL);
        if (!((UnityEngine.Object) this.m_Target != (UnityEngine.Object) null))
          return;
        this.m_Target.UpdateWebView(true);
      }
      else
      {
        if (pinID != 110)
          return;
        this.SetTab(GachaInfoWindow.Tab.RATE);
        if (!((UnityEngine.Object) this.m_Target != (UnityEngine.Object) null))
          return;
        this.m_Target.UpdateWebView(true);
      }
    }

    private void Setup()
    {
      if ((UnityEngine.Object) this.m_Target == (UnityEngine.Object) null)
        DebugUtility.LogError("Webviewが設定されていません");
      this.m_BaseURL = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL2");
    }

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
    }
  }
}
