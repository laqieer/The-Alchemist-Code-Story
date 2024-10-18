// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestCampaignCreate : MonoBehaviour
  {
    [SerializeField]
    private GameObject QuestCampaignItem;
    private GameObject mGoQuestCampaignItem;

    public QuestCampaignList GetQuestCampaignList
    {
      get
      {
        return Object.op_Equality((Object) this.mGoQuestCampaignItem, (Object) null) ? (QuestCampaignList) null : this.mGoQuestCampaignItem.GetComponent<QuestCampaignList>();
      }
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.QuestCampaignItem, (Object) null))
        return;
      this.mGoQuestCampaignItem = Object.Instantiate<GameObject>(this.QuestCampaignItem);
      this.mGoQuestCampaignItem.SetActive(true);
      Vector2 anchoredPosition = this.mGoQuestCampaignItem.GetComponent<RectTransform>().anchoredPosition;
      Vector3 localScale = this.mGoQuestCampaignItem.transform.localScale;
      this.mGoQuestCampaignItem.transform.SetParent(((Component) this).transform);
      this.mGoQuestCampaignItem.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
      this.mGoQuestCampaignItem.transform.localScale = localScale;
    }
  }
}
