// Decompiled with JetBrains decompiler
// Type: SRPG.ReturnItemWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ReturnItemWindow : MonoBehaviour, IFlowInterface
  {
    public Transform ItemLayout;
    public GameObject ItemTemplate;
    public Text Title;
    public List<ItemData> ReturnItems;

    public void Activated(int pinID) => this.Refresh();

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
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.Title, (Object) null))
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
            GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
            gameObject.transform.SetParent(this.ItemLayout, false);
            DataSource.Bind<ItemData>(gameObject, returnItem);
            gameObject.SetActive(true);
          }
        }
        this.ItemTemplate.SetActive(false);
      }
      ((Behaviour) this).enabled = true;
    }
  }
}
