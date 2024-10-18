// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  public class GenesisQuestConfirm : MonoBehaviour, IFlowInterface
  {
    public const int PIN_IN_INIT = 1;
    [SerializeField]
    private QuestCampaignList CampaignPrefab;
    [SerializeField]
    private GameObject DetailInfoPrefab;
    [SerializeField]
    private Text Text_StaminaVal;
    [SerializeField]
    private List<GameObject> mRefMissionStarLists;
    private QuestParam mQuestParam;
    private QuestCampaignData[] mCampaigns;

    private bool Init()
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return false;
      this.mQuestParam = component.list.GetObject<QuestParam>("GENESIS_QUEST_PARAM");
      if (this.mQuestParam == null)
        return false;
      Transform parent = this.transform.Find("CanvasBoundsPanel/window/campaign_root");
      DataSource.Bind<QuestParam>(this.gameObject, this.mQuestParam, false);
      if ((UnityEngine.Object) this.CampaignPrefab != (UnityEngine.Object) null)
      {
        this.mCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mQuestParam);
        if (this.mCampaigns != null)
        {
          QuestCampaignList questCampaignList = UnityEngine.Object.Instantiate<QuestCampaignList>(this.CampaignPrefab, parent, false);
          if ((UnityEngine.Object) questCampaignList != (UnityEngine.Object) null)
          {
            DataSource.Bind<QuestCampaignData[]>(this.gameObject, this.mCampaigns == null || this.mCampaigns.Length <= 0 ? (QuestCampaignData[]) null : this.mCampaigns, false);
            questCampaignList.TextConsumeAp = this.Text_StaminaVal;
            questCampaignList.RefreshIcons();
          }
        }
      }
      this.SetMissionStar(this.mQuestParam);
      GlobalVars.SelectedQuestID = this.mQuestParam.iname;
      return true;
    }

    public void OnOpenItemDetail()
    {
      if (this.mQuestParam == null || (UnityEngine.Object) this.DetailInfoPrefab == (UnityEngine.Object) null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailInfoPrefab);
      DataSource.Bind<QuestParam>(gameObject, this.mQuestParam, false);
      DataSource.Bind<QuestCampaignData[]>(gameObject, this.mCampaigns == null || this.mCampaigns.Length <= 0 ? (QuestCampaignData[]) null : this.mCampaigns, false);
      gameObject.SetActive(true);
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
      if (pinID != 1 || this.Init())
        return;
      DebugUtility.LogError("おかしい");
    }

    public void SetMissionStar(QuestParam param)
    {
      if (this.mRefMissionStarLists == null || param.bonusObjective == null || (param.bonusObjective.Length <= 0 || this.mRefMissionStarLists == null))
        return;
      int num = param.bonusObjective.Length - 1;
      ImageArray[] imageArrayArray = (ImageArray[]) null;
      for (int index = 0; index < this.mRefMissionStarLists.Count; ++index)
      {
        GameObject refMissionStarList = this.mRefMissionStarLists[index];
        if ((UnityEngine.Object) refMissionStarList != (UnityEngine.Object) null)
        {
          refMissionStarList.SetActive(index == num);
          if (index == num)
            imageArrayArray = refMissionStarList.GetComponentsInChildren<ImageArray>();
        }
      }
      if (imageArrayArray == null || imageArrayArray.Length <= 0 || param.bonusObjective.Length > imageArrayArray.Length)
        return;
      for (int index = 0; index < imageArrayArray.Length; ++index)
        imageArrayArray[index].ImageIndex = !param.IsMissionClear(index) ? 0 : 1;
    }
  }
}
