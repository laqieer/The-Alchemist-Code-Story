// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisStarRewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      GenesisStarRewardParam genesisStarRewardParam = component.list.GetObject<GenesisStarRewardParam>("GENESIS_STAR_REWARD", (GenesisStarRewardParam) null);
      if (genesisStarRewardParam == null || (UnityEngine.Object) this.mBodyText == (UnityEngine.Object) null)
        return;
      this.mBodyText.text = genesisStarRewardParam.IsReward ? string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_RECEIVED")) : string.Format(LocalizedText.Get("sys.GENESIS_QUEST_STARREWARD_TEXT"), (object) genesisStarRewardParam.NeedStarNum);
      if ((UnityEngine.Object) this.mRewardIconTemplate == (UnityEngine.Object) null)
        return;
      GenesisRewardParam genesisRewardParam = MonoSingleton<GameManager>.Instance.GetGenesisRewardParam(genesisStarRewardParam.RewardId);
      if (genesisRewardParam == null || genesisRewardParam.RewardList == null)
        return;
      for (int index = 0; index < genesisRewardParam.RewardList.Count; ++index)
      {
        GenesisRewardIcon genesisRewardIcon = UnityEngine.Object.Instantiate<GenesisRewardIcon>(this.mRewardIconTemplate, this.mRewardIconParent);
        genesisRewardIcon.Initialize(genesisRewardParam.RewardList[index]);
        genesisRewardIcon.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.mReceiveButton == (UnityEngine.Object) null)
        return;
      int num = component.list.GetInt("GENESIS_STAR_POINT", 0);
      this.mReceiveButton.interactable = genesisStarRewardParam.NeedStarNum <= num && !genesisStarRewardParam.IsReward;
      if (!((UnityEngine.Object) this.mButtonText != (UnityEngine.Object) null))
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
