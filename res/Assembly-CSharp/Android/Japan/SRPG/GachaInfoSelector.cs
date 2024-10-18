// Decompiled with JetBrains decompiler
// Type: SRPG.GachaInfoSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/Gacha/GachaInfoSelector", 32741)]
  [FlowNode.Pin(100, "Setup", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "Select Detail", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "Select Rate", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(200, "Selected", FlowNode.PinTypes.Output, 200)]
  public class GachaInfoSelector : FlowNodePersistent
  {
    [SerializeField]
    private string url = string.Empty;
    [SerializeField]
    private string url_prefix = "_rate";
    private string m_BaseURL = string.Empty;
    private FlowWindowController m_WindowController = new FlowWindowController();
    private const int PIN_IN_SETUP_URL = 100;
    private const int PIN_IN_SELECT_DETAIL = 110;
    private const int PIN_IN_SELECT_RATE = 120;
    private const int PIN_OT_SELECTED = 200;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
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
      }
    }

    private void Setup()
    {
      this.m_BaseURL = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL2");
      if (string.IsNullOrEmpty(this.url))
        return;
      this.m_BaseURL = this.url;
    }
  }
}
