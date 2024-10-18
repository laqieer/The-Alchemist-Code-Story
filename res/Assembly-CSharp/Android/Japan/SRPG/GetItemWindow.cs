// Decompiled with JetBrains decompiler
// Type: SRPG.GetItemWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "アイテム選択", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "アイテム更新", FlowNode.PinTypes.Output, 101)]
  public class GetItemWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> ItemSelectItem = new List<GameObject>();
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh(ItemSelectListItemData[] shopdata)
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.ItemSelectItem.Count; ++index)
        this.ItemSelectItem[index].gameObject.SetActive(false);
      int length = shopdata.Length;
      for (int index = 0; index < length; ++index)
      {
        ItemSelectListItemData data = shopdata[index];
        if (index >= this.ItemSelectItem.Count)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.ItemSelectItem.Add(gameObject);
        }
        GameObject gameObject1 = this.ItemSelectItem[index];
        DataSource.Bind<ItemSelectListItemData>(gameObject1, data, false);
        ItemData itemDataByItemId = player.FindItemDataByItemID(data.iiname);
        DataSource.Bind<ItemData>(gameObject1, itemDataByItemId, false);
        DataSource.Bind<ItemParam>(gameObject1, MonoSingleton<GameManager>.Instance.GetItemParam(data.iiname), false);
        ListItemEvents component = gameObject1.GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        gameObject1.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnSelect(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      GlobalVars.ItemSelectListItemData = DataSource.FindDataOfClass<ItemSelectListItemData>(go, (ItemSelectListItemData) null);
    }
  }
}
