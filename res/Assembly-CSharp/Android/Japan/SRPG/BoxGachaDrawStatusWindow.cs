// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaDrawStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("UI/BoxGachaDrawStatusWindow", 32741)]
  [FlowNode.Pin(100, "ラインナップ更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "ラインナップ更新終了", FlowNode.PinTypes.Output, 101)]
  public class BoxGachaDrawStatusWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    public const int PIN_IN_REFRESH = 100;
    public const int PIN_OT_REFRESH = 101;
    public BoxGachaLineupListWindow.SerializeParam m_LineupListWindowparam;
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
      if (!((UnityEngine.Object) this.ListTitle != (UnityEngine.Object) null))
        return;
      this.ListTitle.text = string.Empty;
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
      if ((UnityEngine.Object) this.ListTitle != (UnityEngine.Object) null)
      {
        BoxGachaLineupListWindow instance = BoxGachaLineupListWindow.Instance;
        int num = 1;
        if (instance != null)
          num = instance.step;
        this.ListTitle.text = LocalizedText.Get("sys.GENESIS_GACHA_CURRENT_TEXT", new object[1]
        {
          (object) num
        });
      }
      if ((UnityEngine.Object) this.ResetBtn != (UnityEngine.Object) null && BoxGachaManager.CurrentBoxGachaStatus != null)
        this.ResetBtn.interactable = BoxGachaManager.CurrentBoxGachaStatus.IsReset;
      this.m_WindowController.OnActivate(pinID);
      this.ActivateOutputLinks(101);
    }
  }
}
