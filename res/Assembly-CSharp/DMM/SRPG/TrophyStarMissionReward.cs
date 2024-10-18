// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyStarMissionReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  public class TrophyStarMissionReward : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private GameObject GoBindObject;
    [SerializeField]
    private RewardWindowTrophy RewardWindowTrophyComponent;
    [SerializeField]
    private Text TextBody;
    [SerializeField]
    private Button ButtonReceive;
    [SerializeField]
    private Text TextButton;
    private const int PIN_IN_INIT = 1;

    private void Awake()
    {
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.Player == null || instance.Player.TrophyStarMissionInfo == null)
        return;
      PlayerData.TrophyStarMission trophyStarMissionInfo = instance.Player.TrophyStarMissionInfo;
      PlayerData.TrophyStarMission.StarMission starMission = trophyStarMissionInfo.Daily;
      if (TrophyStarMissionParam.SelectStarMissionType == TrophyStarMissionParam.eStarMissionType.WEEKLY)
        starMission = trophyStarMissionInfo.Weekly;
      if (starMission == null || starMission.TsmParam == null)
        return;
      int index1 = TrophyStarMissionParam.SelectDailyTreasureIndex;
      if (TrophyStarMissionParam.SelectStarMissionType == TrophyStarMissionParam.eStarMissionType.WEEKLY)
        index1 = TrophyStarMissionParam.SelectWeeklyTreasureIndex;
      if (index1 < 0 || index1 >= starMission.TsmParam.StarSetList.Count)
        return;
      TrophyStarMissionRewardParam tsmReward = starMission.TsmParam.StarSetList[index1].TsmReward;
      if (tsmReward == null)
        return;
      RewardData data = new RewardData();
      for (int index2 = 0; index2 < tsmReward.RewardList.Count; ++index2)
      {
        TrophyStarMissionRewardParam.Data reward = tsmReward.RewardList[index2];
        switch (reward.ItemType)
        {
          case 0:
            data.AddRewardItems(instance.GetItemParam(reward.ItemIname), reward.ItemNum);
            break;
          case 1:
            data.Gold += reward.ItemNum;
            break;
          case 2:
            data.Coin += reward.ItemNum;
            break;
          case 5:
            data.AddReward(instance.MasterParam.GetConceptCardParam(reward.ItemIname), reward.ItemNum);
            break;
          case 6:
            data.AddRewardArtifacts(instance.MasterParam.GetArtifactParam(reward.ItemIname), reward.ItemNum);
            break;
        }
      }
      GameObject gameObject = this.GoBindObject;
      if (!Object.op_Implicit((Object) gameObject))
        gameObject = ((Component) ((Component) this).transform.parent).gameObject;
      DataSource.Bind<RewardData>(gameObject, data, true);
      if (Object.op_Implicit((Object) this.RewardWindowTrophyComponent))
        this.RewardWindowTrophyComponent.Refresh();
      int starNum = starMission.StarNum;
      int requireStar = (int) starMission.TsmParam.StarSetList[index1].RequireStar;
      bool flag = false;
      if (index1 < starMission.Rewards.Length)
        flag = starMission.Rewards[index1] != 0;
      if (Object.op_Implicit((Object) this.TextBody))
        this.TextBody.text = !flag ? string.Format(LocalizedText.Get("sys.TROPHY_STAR_MISSION_STARREWARD_TEXT"), (object) requireStar) : string.Format(LocalizedText.Get("sys.TROPHY_STAR_MISSION_STARREWARD_RECEIVED"));
      if (Object.op_Implicit((Object) this.ButtonReceive))
        ((Selectable) this.ButtonReceive).interactable = !flag && starNum >= requireStar;
      if (Object.op_Inequality((Object) this.TextButton, (Object) null))
        this.TextButton.text = !flag ? (starNum < requireStar ? string.Format(LocalizedText.Get("sys.TROPHY_STAR_MISSION_STARREWARD_BTN_UNACHIEVED")) : string.Format(LocalizedText.Get("sys.TROPHY_STAR_MISSION_STARREWARD_BTN_OK"))) : string.Format(LocalizedText.Get("sys.TROPHY_STAR_MISSION_STARREWARD_BTN_RECEIVED"));
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }
  }
}
