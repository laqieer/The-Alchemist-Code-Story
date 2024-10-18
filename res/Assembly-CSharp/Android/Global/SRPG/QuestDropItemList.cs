// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class QuestDropItemList : MonoBehaviour
  {
    protected List<GameObject> mItems = new List<GameObject>();
    public GameObject ItemTemplate;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    protected virtual void Refresh()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (dataOfClass == null || !((UnityEngine.Object) QuestDropParam.Instance != (UnityEngine.Object) null))
        return;
      List<ItemParam> questDropList = QuestDropParam.Instance.GetQuestDropList(dataOfClass.iname, GlobalVars.GetDropTableGeneratedDateTime());
      if (questDropList == null)
        return;
      for (int index = 0; index < questDropList.Count; ++index)
      {
        ItemParam data = questDropList[index];
        if (data != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          DataSource.Bind<ItemParam>(gameObject, data);
          gameObject.transform.SetParent(this.transform, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
