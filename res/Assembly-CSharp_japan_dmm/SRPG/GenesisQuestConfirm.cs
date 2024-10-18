// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    [SerializeField]
    private List<GameObject> mRefDifficultyObject;
    private QuestParam mQuestParam;
    private QuestCampaignData[] mCampaigns;

    private bool Init()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Equality((Object) component, (Object) null))
        return false;
      this.mQuestParam = component.list.GetObject<QuestParam>("GENESIS_QUEST_PARAM");
      if (this.mQuestParam == null)
        return false;
      Transform transform = ((Component) this).transform.Find("CanvasBoundsPanel/window/campaign_root");
      DataSource.Bind<QuestParam>(((Component) this).gameObject, this.mQuestParam);
      this.mRefDifficultyObject[(int) this.mQuestParam.difficulty].SetActive(true);
      if (Object.op_Inequality((Object) this.CampaignPrefab, (Object) null))
      {
        this.mCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mQuestParam);
        if (this.mCampaigns != null)
        {
          QuestCampaignList questCampaignList = Object.Instantiate<QuestCampaignList>(this.CampaignPrefab, transform, false);
          if (Object.op_Inequality((Object) questCampaignList, (Object) null))
          {
            DataSource.Bind<QuestCampaignData[]>(((Component) this).gameObject, this.mCampaigns == null || this.mCampaigns.Length <= 0 ? (QuestCampaignData[]) null : this.mCampaigns);
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
      if (this.mQuestParam == null || Object.op_Equality((Object) this.DetailInfoPrefab, (Object) null))
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.DetailInfoPrefab);
      DataSource.Bind<QuestParam>(gameObject, this.mQuestParam);
      DataSource.Bind<QuestCampaignData[]>(gameObject, this.mCampaigns == null || this.mCampaigns.Length <= 0 ? (QuestCampaignData[]) null : this.mCampaigns);
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
      if (this.mRefMissionStarLists == null || param.bonusObjective == null || param.bonusObjective.Length <= 0 || this.mRefMissionStarLists == null)
        return;
      int num = param.bonusObjective.Length - 1;
      ImageArray[] imageArrayArray = (ImageArray[]) null;
      for (int index = 0; index < this.mRefMissionStarLists.Count; ++index)
      {
        GameObject refMissionStarList = this.mRefMissionStarLists[index];
        if (Object.op_Inequality((Object) refMissionStarList, (Object) null))
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
