// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/ボーナス勝利条件")]
  public class QuestBonusObjectiveList : MonoBehaviour
  {
    public GameObject ItemTemplate;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if (DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null) == null)
        ;
    }
  }
}
