// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      List<BattleCore.DropItemParam> dropItemParamList = QuestDropParam.Instance.GetQuestDropItemParamList(dataOfClass.iname, GlobalVars.GetDropTableGeneratedDateTime());
      if (dropItemParamList == null)
        return;
      for (int index = 0; index < dropItemParamList.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = dropItemParamList[index];
        if (dropItemParam != null)
        {
          GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          if (dropItemParam.IsItem)
            DataSource.Bind<ItemParam>(root, dropItemParam.itemParam, false);
          else if (dropItemParam.IsConceptCard)
            DataSource.Bind<ConceptCardParam>(root, dropItemParam.conceptCardParam, false);
          root.transform.SetParent(this.transform, false);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
      }
    }
  }
}
