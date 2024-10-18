// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultPieceDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class GachaResultPieceDetail : MonoBehaviour, IFlowInterface
  {
    private readonly int OUT_CLOSE_DETAIL = 100;
    public GameObject PieceInfo;
    public GameObject Bg;
    private ItemData mCurrentPiece;
    [SerializeField]
    private Button BackBtn;

    public void Activated(int pinID) => this.Refresh();

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.BackBtn, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.BackBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCloseDetail)));
    }

    private void OnEnable()
    {
      Animator component1 = ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.SetBool("close", false);
      if (Object.op_Inequality((Object) this.Bg, (Object) null))
      {
        CanvasGroup component2 = this.Bg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component2, (Object) null))
        {
          component2.interactable = true;
          component2.blocksRaycasts = true;
        }
      }
      this.Refresh();
    }

    private void OnCloseDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_CLOSE_DETAIL);
    }

    public void Setup(int _index)
    {
      ItemParam itemParam = GachaResultData.drops[_index].item;
      int num = GachaResultData.drops[_index].num;
      if (itemParam == null)
        return;
      ItemData _data = new ItemData();
      _data.Setup(0L, itemParam, num);
      this.Setup(_data);
    }

    public void Setup(ItemData _data) => this.mCurrentPiece = _data;

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.PieceInfo, (Object) null))
        return;
      DataSource.Bind<ItemData>(this.PieceInfo, this.mCurrentPiece);
      GameParameter.UpdateAll(this.PieceInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
