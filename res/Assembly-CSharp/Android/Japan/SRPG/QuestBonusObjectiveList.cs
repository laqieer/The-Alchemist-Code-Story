// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
