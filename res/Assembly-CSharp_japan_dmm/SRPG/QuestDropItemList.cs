// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestDropItemList : MonoBehaviour
  {
    public GameObject ItemTemplate;
    protected List<GameObject> mItems = new List<GameObject>();

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    protected virtual void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        Object.Destroy((Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) QuestDropParam.Instance, (Object) null))
        return;
      List<BattleCore.DropItemParam> dropItemParamList = QuestDropParam.Instance.GetQuestDropItemParamList(dataOfClass.iname, GlobalVars.GetDropTableGeneratedDateTime());
      if (dropItemParamList == null)
        return;
      for (int index = 0; index < dropItemParamList.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = dropItemParamList[index];
        if (dropItemParam != null)
        {
          GameObject root = Object.Instantiate<GameObject>(this.ItemTemplate);
          if (dropItemParam.IsItem)
            DataSource.Bind<ItemParam>(root, dropItemParam.itemParam);
          else if (dropItemParam.IsConceptCard)
            DataSource.Bind<ConceptCardParam>(root, dropItemParam.conceptCardParam);
          root.transform.SetParent(((Component) this).transform, false);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
      }
    }
  }
}
