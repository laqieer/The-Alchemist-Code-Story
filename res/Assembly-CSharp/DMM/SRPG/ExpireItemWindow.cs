// Decompiled with JetBrains decompiler
// Type: SRPG.ExpireItemWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ExpireItemWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject mTemplate;
    [SerializeField]
    private GameObject mTitle_Warning;
    [SerializeField]
    private GameObject mTitle_Expired;
    [SerializeField]
    private Text mWarningText_1;
    [SerializeField]
    private Text mWarningText_2;

    public void Setup_ExpireWarning(List<ItemData> item_list, int rest_day)
    {
      this.mTitle_Warning.SetActive(true);
      this.mTitle_Expired.SetActive(false);
      ((Component) this.mWarningText_1).gameObject.SetActive(false);
      ((Component) this.mWarningText_2).gameObject.SetActive(false);
      Text text = rest_day > 0 ? this.mWarningText_2 : this.mWarningText_1;
      text.text = LocalizedText.Get(text.text, (object) rest_day);
      ((Component) text).gameObject.SetActive(true);
      this.mTemplate.SetActive(false);
      if (item_list == null)
        return;
      for (int index = 0; index < item_list.Count; ++index)
      {
        if (item_list[index].Param != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.mTemplate);
          gameObject.transform.SetParent(this.mTemplate.transform.parent, false);
          gameObject.SetActive(true);
          DataSource.Bind<ItemParam>(gameObject, item_list[index].Param);
          DataSource.Bind<int>(gameObject, item_list[index].Num);
        }
      }
    }

    public void Setup_ExpiredNotify(ExpireItemParamList expire_item)
    {
      this.mTitle_Warning.SetActive(false);
      this.mTitle_Expired.SetActive(true);
      this.mTemplate.SetActive(false);
      if (expire_item == null)
        return;
      for (int index = 0; index < expire_item.items.Length; ++index)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(expire_item.items[index].iname);
        if (itemParam != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.mTemplate);
          gameObject.transform.SetParent(this.mTemplate.transform.parent, false);
          gameObject.SetActive(true);
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          DataSource.Bind<int>(gameObject, expire_item.items[index].num);
        }
      }
    }
  }
}
