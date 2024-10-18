// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
        if ((UnityEngine.Object) this.mGoQuestCampaignItem == (UnityEngine.Object) null)
          return (QuestCampaignList) null;
        return this.mGoQuestCampaignItem.GetComponent<QuestCampaignList>();
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.QuestCampaignItem == (UnityEngine.Object) null)
        return;
      this.mGoQuestCampaignItem = UnityEngine.Object.Instantiate<GameObject>(this.QuestCampaignItem);
      this.mGoQuestCampaignItem.SetActive(true);
      Vector2 anchoredPosition = this.mGoQuestCampaignItem.GetComponent<RectTransform>().anchoredPosition;
      Vector3 localScale = this.mGoQuestCampaignItem.transform.localScale;
      this.mGoQuestCampaignItem.transform.SetParent(this.transform);
      this.mGoQuestCampaignItem.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
      this.mGoQuestCampaignItem.transform.localScale = localScale;
    }
  }
}
