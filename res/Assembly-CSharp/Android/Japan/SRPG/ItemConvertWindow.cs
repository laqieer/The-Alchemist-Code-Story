// Decompiled with JetBrains decompiler
// Type: SRPG.ItemConvertWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 1)]
  public class ItemConvertWindow : MonoBehaviour, IFlowInterface
  {
    public Transform ItemLayout;
    public GameObject ItemTemplate;
    public Text ConvertItemName;
    public Text ConvertItemNum;
    public Text ConvertResult;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.gameObject.SetActive(false);
    }

    private void Refresh()
    {
      if (GlobalVars.SellItemList == null)
        GlobalVars.SellItemList = new List<SellItem>();
      else
        GlobalVars.SellItemList.Clear();
      int num = 0;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemData itemData = items[index];
        if (itemData.ItemType == EItemType.GoldConvert && itemData.Num != 0)
        {
          this.ConvertItemName.text = itemData.Param.name;
          this.ConvertItemNum.text = itemData.Num.ToString();
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent(this.ItemLayout, false);
          gameObject.SetActive(true);
          GlobalVars.SellItemList.Add(new SellItem()
          {
            item = itemData,
            num = itemData.Num
          });
          num += itemData.Param.sell * itemData.Num;
        }
      }
      if ((UnityEngine.Object) this.ConvertResult != (UnityEngine.Object) null)
        this.ConvertResult.text = string.Format(LocalizedText.Get("sys.CONVERT_TO_GOLD"), (object) num);
      this.enabled = true;
    }
  }
}
