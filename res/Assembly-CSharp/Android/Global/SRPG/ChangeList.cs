// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ChangeList : MonoBehaviour
  {
    [FlexibleArray]
    public ChangeListItem[] ListItems = new ChangeListItem[0];
    public GameObject List;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.List == (UnityEngine.Object) null))
        return;
      this.List = this.gameObject;
    }

    private void Start()
    {
      for (int index = 0; index < this.ListItems.Length; ++index)
      {
        if ((UnityEngine.Object) this.ListItems[index] != (UnityEngine.Object) null && this.ListItems[index].gameObject.activeInHierarchy)
          this.ListItems[index].gameObject.SetActive(false);
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
          if ((UnityEngine.Object) this.ListItems[index3] != (UnityEngine.Object) null && this.ListItems[index3].ID == changeListData.ItemID)
          {
            index2 = index3;
            break;
          }
        }
        if (index2 != -1)
        {
          ChangeListItem changeListItem = UnityEngine.Object.Instantiate<ChangeListItem>(this.ListItems[index2]);
          if (changeListData.MetaData != null && (object) changeListData.MetaDataType != null)
            DataSource.Bind(changeListItem.gameObject, changeListData.MetaDataType, changeListData.MetaData);
          if ((UnityEngine.Object) changeListItem.ValNew != (UnityEngine.Object) null && !string.IsNullOrEmpty(changeListData.ValNew))
            changeListItem.ValNew.text = changeListData.ValNew.ToString();
          if ((UnityEngine.Object) changeListItem.ValOld != (UnityEngine.Object) null && !string.IsNullOrEmpty(changeListData.ValOld))
            changeListItem.ValOld.text = changeListData.ValOld.ToString();
          long result1;
          long result2;
          if ((UnityEngine.Object) changeListItem.Diff != (UnityEngine.Object) null && !string.IsNullOrEmpty(changeListData.ValNew) && (!string.IsNullOrEmpty(changeListData.ValOld) && long.TryParse(changeListData.ValOld, out result1)) && long.TryParse(changeListData.ValNew, out result2))
            changeListItem.Diff.text = (result2 - result1).ToString();
          if ((UnityEngine.Object) changeListItem.Label != (UnityEngine.Object) null && !string.IsNullOrEmpty(changeListData.Label))
            changeListItem.Label.text = changeListData.Label;
          changeListItem.gameObject.SetActive(true);
          changeListItem.transform.SetParent(transform, false);
        }
      }
    }
  }
}
