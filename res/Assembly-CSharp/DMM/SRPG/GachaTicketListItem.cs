// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTicketListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "OnDetailClick", FlowNode.PinTypes.Output, 10)]
  public class GachaTicketListItem : MonoBehaviour, IFlowInterface
  {
    public UnityEngine.UI.Text TicketTitle;
    public GameObject Icon;
    public SRPG_Button DetailBtn;
    public SRPG_Button ExecBtn;
    private readonly string GACHA_URL_PREFIX = "notice/detail/gacha/";
    public UnityEngine.UI.Text Amount;
    private GachaTopParamNew mGachaParam;
    private string mDetailURL;

    public int SelectIndex { get; set; }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Refresh(GachaTopParamNew param, int index)
    {
      if (param == null || string.IsNullOrEmpty(param.ticket_iname))
        return;
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.ticket_iname);
      if (itemDataByItemId == null)
        return;
      DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
      this.SelectIndex = index;
      if (Object.op_Inequality((Object) this.DetailBtn, (Object) null))
        this.DetailBtn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnDetail));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append(this.GACHA_URL_PREFIX);
      stringBuilder.Append(param.detail_url);
      this.mDetailURL = stringBuilder.ToString();
    }

    public bool SetGachaButtonEvent(UnityAction action)
    {
      if (action == null || !Object.op_Inequality((Object) this.ExecBtn, (Object) null))
        return false;
      ((UnityEventBase) this.ExecBtn.onClick).RemoveAllListeners();
      ((UnityEvent) this.ExecBtn.onClick).AddListener(action);
      return true;
    }

    private void OnDetail(SRPG_Button button)
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDetailURL);
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL2", this.mDetailURL);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
