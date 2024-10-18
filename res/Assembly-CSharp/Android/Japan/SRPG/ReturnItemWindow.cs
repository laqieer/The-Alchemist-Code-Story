// Decompiled with JetBrains decompiler
// Type: SRPG.ReturnItemWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ReturnItemWindow : MonoBehaviour, IFlowInterface
  {
    public Transform ItemLayout;
    public GameObject ItemTemplate;
    public Text Title;
    public List<ItemData> ReturnItems;

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (this.ReturnItems == null)
      {
        this.ReturnItems = GlobalVars.ReturnItems;
        GlobalVars.ReturnItems = (List<ItemData>) null;
      }
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("sys.RETURN_ITEM_TITLE");
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.ReturnItems != null)
      {
        this.ItemTemplate.SetActive(true);
        for (int index = 0; index < this.ReturnItems.Count; ++index)
        {
          ItemData returnItem = this.ReturnItems[index];
          if (!string.IsNullOrEmpty(returnItem.ItemID) && returnItem.Num != 0)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
            gameObject.transform.SetParent(this.ItemLayout, false);
            DataSource.Bind<ItemData>(gameObject, returnItem, false);
            gameObject.SetActive(true);
          }
        }
        this.ItemTemplate.SetActive(false);
      }
      this.enabled = true;
    }
  }
}
