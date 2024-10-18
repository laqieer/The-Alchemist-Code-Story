// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisStarRewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GenesisStarRewardWindow : MonoBehaviour
  {
    [SerializeField]
    private Text mBodyText;
    [SerializeField]
    private Transform mRewardIconParent;
    [SerializeField]
    private GenesisRewardIcon mRewardIconTemplate;
    [SerializeField]
    private Button mReceiveButton;
    [SerializeField]
    private Text mButtonText;

    private void Start()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      GenesisStarRewardParam genesisStarRewardParam = component.list.GetObject<GenesisStarRewardParam>("GENESIS_STAR_REWARD", (GenesisStarRewardParam) null);
      if (genesisStarRewardParam == null || Object.op_Equality((Object) this.mBodyText, (Object) null))
        return;
      this.mBodyText.text = genesisStarRewardParam.IsReward ? string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_RECEIVED")) : string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_TEXT"), (object) genesisStarRewardParam.NeedStarNum);
      if (Object.op_Equality((Object) this.mRewardIconTemplate, (Object) null))
        return;
      GenesisRewardParam genesisRewardParam = MonoSingleton<GameManager>.Instance.GetGenesisRewardParam(genesisStarRewardParam.RewardId);
      if (genesisRewardParam == null || genesisRewardParam.RewardList == null)
        return;
      for (int index = 0; index < genesisRewardParam.RewardList.Count; ++index)
      {
        GenesisRewardIcon genesisRewardIcon = Object.Instantiate<GenesisRewardIcon>(this.mRewardIconTemplate, this.mRewardIconParent);
        genesisRewardIcon.Initialize(genesisRewardParam.RewardList[index]);
        ((Component) genesisRewardIcon).gameObject.SetActive(true);
      }
      if (Object.op_Equality((Object) this.mReceiveButton, (Object) null))
        return;
      int num = component.list.GetInt("GENESIS_STAR_POINT", 0);
      ((Selectable) this.mReceiveButton).interactable = genesisStarRewardParam.NeedStarNum <= num && !genesisStarRewardParam.IsReward;
      if (!Object.op_Inequality((Object) this.mButtonText, (Object) null))
        return;
      if (!genesisStarRewardParam.IsReward)
      {
        if (genesisStarRewardParam.NeedStarNum <= num)
          this.mButtonText.text = string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_BTN_OK"));
        else
          this.mButtonText.text = string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_BTN_UNACHIEVED"));
      }
      else
        this.mButtonText.text = string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_BTN_RECEIVED"));
    }
  }
}
