// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceStarRewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AdvanceStarRewardWindow : MonoBehaviour
  {
    [SerializeField]
    private Text mBodyText;
    [SerializeField]
    private Transform mRewardIconParent;
    [SerializeField]
    private AdvanceRewardIcon mRewardIconTemplate;
    [SerializeField]
    private Button mReceiveButton;
    [SerializeField]
    private Text mButtonText;

    private void Start()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      AdvanceStarRewardParam advanceStarRewardParam = component.list.GetObject<AdvanceStarRewardParam>("ADVANCE_STAR_REWARD", (AdvanceStarRewardParam) null);
      if (advanceStarRewardParam == null || Object.op_Equality((Object) this.mBodyText, (Object) null))
        return;
      this.mBodyText.text = advanceStarRewardParam.IsReward ? string.Format(LocalizedText.Get("sys.ADVANCE_QUEST_STARREWARD_RECEIVED")) : string.Format(LocalizedText.Get("sys.ADVANCE_QUEST_STARREWARD_TEXT"), (object) advanceStarRewardParam.NeedStarNum);
      if (Object.op_Equality((Object) this.mRewardIconTemplate, (Object) null))
        return;
      AdvanceRewardParam advanceRewardParam = MonoSingleton<GameManager>.Instance.GetAdvanceRewardParam(advanceStarRewardParam.RewardId);
      if (advanceRewardParam == null || advanceRewardParam.RewardList == null)
        return;
      for (int index = 0; index < advanceRewardParam.RewardList.Count; ++index)
      {
        AdvanceRewardIcon advanceRewardIcon = Object.Instantiate<AdvanceRewardIcon>(this.mRewardIconTemplate, this.mRewardIconParent);
        advanceRewardIcon.Initialize(advanceRewardParam.RewardList[index]);
        ((Component) advanceRewardIcon).gameObject.SetActive(true);
      }
      if (Object.op_Equality((Object) this.mReceiveButton, (Object) null))
        return;
      int num = component.list.GetInt("ADVANCE_STAR_POINT", 0);
      ((Selectable) this.mReceiveButton).interactable = advanceStarRewardParam.NeedStarNum <= num && !advanceStarRewardParam.IsReward;
      if (!Object.op_Inequality((Object) this.mButtonText, (Object) null))
        return;
      if (!advanceStarRewardParam.IsReward)
      {
        if (advanceStarRewardParam.NeedStarNum <= num)
          this.mButtonText.text = string.Format(LocalizedText.Get("sys.ADVANCE_QUEST_STARREWARD_BTN_OK"));
        else
          this.mButtonText.text = string.Format(LocalizedText.Get("sys.ADVANCE_QUEST_STARREWARD_BTN_UNACHIEVED"));
      }
      else
        this.mButtonText.text = string.Format(LocalizedText.Get("sys.ADVANCE_QUEST_STARREWARD_BTN_RECEIVED"));
    }
  }
}
