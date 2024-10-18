// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakeraConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Decide", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Decided", FlowNode.PinTypes.Output, 10)]
  public class UnitKakeraConfirm : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject ItemTemplate;
    private UnitKakeraConfirm.OnDecide mOnDecide;

    public void Setup(UnitKakeraConfirm.OnDecide onDecide, ItemData[] items)
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && items != null)
      {
        this.ItemTemplate.SetActive(false);
        for (int index = 0; index < items.Length; ++index)
        {
          if (items[index] != null)
          {
            GameObject root = Object.Instantiate<GameObject>(this.ItemTemplate, this.ItemTemplate.transform.parent);
            DataSource.Bind<ItemData>(root, items[index]);
            root.SetActive(true);
            GameParameter.UpdateAll(root);
          }
        }
      }
      this.mOnDecide = onDecide;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.mOnDecide != null)
        this.mOnDecide();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    public delegate void OnDecide();
  }
}
