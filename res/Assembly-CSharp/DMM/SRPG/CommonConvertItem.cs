// Decompiled with JetBrains decompiler
// Type: SRPG.CommonConvertItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class CommonConvertItem : MonoBehaviour
  {
    public GameObject Obj;
    public GameObject CommonObj;
    public LText Amount;
    public LText ItemName;
    public Text ItemUseNum;
    public Text CommonItemUseNum;

    public void Bind(ItemData data, ItemData cmmon_data, int need_num)
    {
      DataSource.Bind<ItemData>(this.Obj, data);
      DataSource.Bind<ItemData>(this.CommonObj, cmmon_data);
      this.Amount.text = LocalizedText.Get("sys.COMMON_EQUIP_NUM", (object) cmmon_data.Num);
      this.ItemName.text = LocalizedText.Get("sys.COMMON_EQUIP_NAME", (object) cmmon_data.Param.name, (object) need_num);
      Text itemUseNum = this.ItemUseNum;
      string str1 = need_num.ToString();
      this.CommonItemUseNum.text = str1;
      string str2 = str1;
      itemUseNum.text = str2;
    }

    public void Refresh(ItemData data, ItemData cmmon_data)
    {
    }
  }
}
