// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ChangeList : MonoBehaviour
  {
    public GameObject List;
    [FlexibleArray]
    public ChangeListItem[] ListItems = new ChangeListItem[0];

    private void Awake()
    {
      if (!Object.op_Equality((Object) this.List, (Object) null))
        return;
      this.List = ((Component) this).gameObject;
    }

    private void Start()
    {
      for (int index = 0; index < this.ListItems.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.ListItems[index], (Object) null) && ((Component) this.ListItems[index]).gameObject.activeInHierarchy)
          ((Component) this.ListItems[index]).gameObject.SetActive(false);
      }
    }

    public void SetData(ChangeListData[] src)
    {
      Transform transform = this.List.transform;
      for (int index1 = 0; index1 < src.Length; ++index1)
      {
        ChangeListData changeListData = src[index1];
        int index2 = -1;
        for (int index3 = 0; index3 < this.ListItems.Length; ++index3)
        {
          if (Object.op_Inequality((Object) this.ListItems[index3], (Object) null) && this.ListItems[index3].ID == changeListData.ItemID)
          {
            index2 = index3;
            break;
          }
        }
        if (index2 != -1)
        {
          ChangeListItem changeListItem = Object.Instantiate<ChangeListItem>(this.ListItems[index2]);
          if (changeListData.MetaData != null && (object) changeListData.MetaDataType != null)
            DataSource.Bind(((Component) changeListItem).gameObject, changeListData.MetaDataType, changeListData.MetaData);
          if (Object.op_Inequality((Object) changeListItem.ValNew, (Object) null) && !string.IsNullOrEmpty(changeListData.ValNew))
            changeListItem.ValNew.text = changeListData.ValNew.ToString();
          if (Object.op_Inequality((Object) changeListItem.ValOld, (Object) null) && !string.IsNullOrEmpty(changeListData.ValOld))
            changeListItem.ValOld.text = changeListData.ValOld.ToString();
          long result1;
          long result2;
          if (Object.op_Inequality((Object) changeListItem.Diff, (Object) null) && !string.IsNullOrEmpty(changeListData.ValNew) && !string.IsNullOrEmpty(changeListData.ValOld) && long.TryParse(changeListData.ValOld, out result1) && long.TryParse(changeListData.ValNew, out result2))
            changeListItem.Diff.text = (result2 - result1).ToString();
          if (Object.op_Inequality((Object) changeListItem.Label, (Object) null) && !string.IsNullOrEmpty(changeListData.Label))
            changeListItem.Label.text = changeListData.Label;
          ((Component) changeListItem).gameObject.SetActive(true);
          ((Component) changeListItem).transform.SetParent(transform, false);
        }
      }
    }
  }
}
