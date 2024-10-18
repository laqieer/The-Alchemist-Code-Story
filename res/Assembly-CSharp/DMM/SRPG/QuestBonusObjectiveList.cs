// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/リスト/ボーナス勝利条件")]
  public class QuestBonusObjectiveList : MonoBehaviour
  {
    public GameObject ItemTemplate;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if (DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null) != null)
        ;
    }
  }
}
