// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class GachaResultItemDetail : MonoBehaviour, IFlowInterface
  {
    private readonly int OUT_CLOSE_DETAIL = 100;
    public GameObject ItemInfo;
    public GameObject Bg;
    private ItemData mCurrentItem;
    [SerializeField]
    private Button CloseBtn;

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.CloseBtn != (UnityEngine.Object) null))
        return;
      this.CloseBtn.onClick.AddListener(new UnityAction(this.OnCloseDetail));
    }

    private void OnEnable()
    {
      Animator component1 = this.GetComponent<Animator>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.SetBool("close", false);
      if ((UnityEngine.Object) this.Bg != (UnityEngine.Object) null)
      {
        CanvasGroup component2 = this.Bg.GetComponent<CanvasGroup>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
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

    private void Refresh()
    {
      if ((UnityEngine.Object) this.ItemInfo == (UnityEngine.Object) null)
        return;
      int index = int.Parse(FlowNode_Variable.Get("GachaResultDataIndex"));
      ItemParam itemParam = GachaResultData.drops[index].item;
      int num = GachaResultData.drops[index].num;
      if (itemParam == null)
        return;
      this.mCurrentItem = new ItemData();
      this.mCurrentItem.Setup(0L, itemParam, num);
      DataSource.Bind<ItemData>(this.ItemInfo, this.mCurrentItem);
      GameParameter.UpdateAll(this.ItemInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
