// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaDrawStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/BoxGachaDrawStatusWindow", 32741)]
  [FlowNode.Pin(100, "ラインナップ更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "ラインナップ更新終了", FlowNode.PinTypes.Output, 101)]
  public class BoxGachaDrawStatusWindow : FlowNodePersistent
  {
    public const int PIN_IN_REFRESH = 100;
    public const int PIN_OT_REFRESH = 101;
    public BoxGachaLineupListWindow.SerializeParam m_LineupListWindowparam;
    private FlowWindowController m_WindowController = new FlowWindowController();
    [SerializeField]
    private Text ListTitle;
    [SerializeField]
    private Button ResetBtn;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Release();
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_LineupListWindowparam);
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.ListTitle, (Object) null))
        return;
      this.ListTitle.text = string.Empty;
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update() => this.m_WindowController.Update();

    public override void OnActivate(int pinID)
    {
      if (Object.op_Inequality((Object) this.ListTitle, (Object) null))
      {
        BoxGachaLineupListWindow instance = BoxGachaLineupListWindow.Instance;
        int num = 1;
        if (instance != null)
          num = instance.step;
        this.ListTitle.text = LocalizedText.Get("sys.GENESIS_GACHA_CURRENT_TEXT", (object) num);
      }
      if (Object.op_Inequality((Object) this.ResetBtn, (Object) null) && BoxGachaManager.CurrentBoxGachaStatus != null)
        ((Selectable) this.ResetBtn).interactable = BoxGachaManager.CurrentBoxGachaStatus.IsReset;
      this.m_WindowController.OnActivate(pinID);
      this.ActivateOutputLinks(101);
    }
  }
}
