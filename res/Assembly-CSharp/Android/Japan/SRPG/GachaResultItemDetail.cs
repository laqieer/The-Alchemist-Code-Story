// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
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

    public void Setup(ItemData _data)
    {
      this.mCurrentItem = _data;
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.ItemInfo == (UnityEngine.Object) null)
        return;
      DataSource.Bind<ItemData>(this.ItemInfo, this.mCurrentItem, false);
      GameParameter.UpdateAll(this.ItemInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
