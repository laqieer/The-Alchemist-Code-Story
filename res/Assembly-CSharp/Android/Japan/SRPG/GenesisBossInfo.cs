// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  public class GenesisBossInfo : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PIN_IN_INIT = 1;
    public const int PIN_OUT_INIT = 101;
    private static GenesisBossInfo mInstance;
    [SerializeField]
    private Transform mBGParent;
    [SerializeField]
    private GameObject[] mTitle;
    [SerializeField]
    private Text mChapterTitle;
    [SerializeField]
    private GameObject mRewardBox;
    [SerializeField]
    private Text mRwardItemName;
    [SerializeField]
    private Text mRwardItemNum;
    [SerializeField]
    private Transform mRewardIconParent;
    [SerializeField]
    private GenesisRewardIcon mRewardIcon;
    [SerializeField]
    private GameObject mChallengeButton;

    public static GenesisBossInfo Instance
    {
      get
      {
        return GenesisBossInfo.mInstance;
      }
    }

    public int BossHP { get; private set; }

    private void Awake()
    {
      GenesisBossInfo.mInstance = this;
    }

    private void OnDestroy()
    {
      GenesisBossInfo.mInstance = (GenesisBossInfo) null;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1 || this.Init())
        return;
      DebugUtility.LogError("おかしい");
    }

    public void SetBossHP(int hp)
    {
      this.BossHP = hp;
    }

    private bool Init()
    {
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      GenesisChapterParam genesisChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(instance.CurrentChapterParam.Iname);
      if (genesisChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = genesisChapterParam.GetModeInfo(instance.BossDifficulty);
      if (modeInfo == null)
        return false;
      DataSource.Bind<GenesisChapterModeInfoParam>(this.gameObject, modeInfo, false);
      DataSource.Bind<GenesisBossInfo.GenesisBossData>(this.gameObject, new GenesisBossInfo.GenesisBossData()
      {
        unit = modeInfo.BossUnitParam,
        maxHP = modeInfo.BossHp,
        currentHP = this.BossHP
      }, false);
      GameParameter.UpdateAll(this.gameObject);
      if (this.mTitle != null)
      {
        for (int index = 0; index < this.mTitle.Length; ++index)
          this.mTitle[index].SetActive((QuestDifficulties) index == instance.BossDifficulty);
      }
      if (instance.GenesisAssets.BossBG.Length <= modeInfo.ModeUiIndex)
        return false;
      instance.LoadAssets<GameObject>(instance.GenesisAssets.BossBG[modeInfo.ModeUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.Downloaded));
      this.mChapterTitle.text = genesisChapterParam.Name;
      if (modeInfo.BossRewardParam.RewardList.Count <= 0)
      {
        if ((UnityEngine.Object) this.mRewardBox != (UnityEngine.Object) null)
          this.mRewardBox.SetActive(false);
      }
      else
      {
        GenesisRewardDataParam reward = modeInfo.BossRewardParam.RewardList[0];
        string str = string.Empty;
        switch (reward.ItemType)
        {
          case 0:
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward.ItemIname);
            if (itemParam != null)
            {
              str = itemParam.name;
              break;
            }
            break;
          case 1:
            str = LocalizedText.Get("sys.GOLD");
            break;
          case 2:
            str = LocalizedText.Get("sys.COIN");
            break;
          case 3:
            AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(reward.ItemIname);
            if (awardParam != null)
            {
              str = awardParam.name;
              break;
            }
            break;
          case 4:
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(reward.ItemIname);
            if (unitParam != null)
            {
              str = unitParam.name;
              break;
            }
            break;
          case 5:
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(reward.ItemIname);
            if (conceptCardParam != null)
            {
              str = conceptCardParam.name;
              break;
            }
            break;
          case 6:
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.ItemIname);
            if (artifactParam != null)
            {
              str = artifactParam.name;
              break;
            }
            break;
        }
        if ((UnityEngine.Object) this.mRwardItemName != (UnityEngine.Object) null)
          this.mRwardItemName.text = str;
        if ((UnityEngine.Object) this.mRwardItemNum != (UnityEngine.Object) null)
          this.mRwardItemNum.text = reward.ItemNum.ToString();
        if ((UnityEngine.Object) this.mRewardIconParent != (UnityEngine.Object) null && (UnityEngine.Object) this.mRewardIcon != (UnityEngine.Object) null)
          UnityEngine.Object.Instantiate<GenesisRewardIcon>(this.mRewardIcon, this.mRewardIconParent).Initialize(reward);
      }
      QuestParam bossQuest = genesisChapterParam.GetBossQuest(instance.BossDifficulty, false);
      if (bossQuest == null)
        return false;
      GlobalVars.SelectedQuestID = bossQuest.iname;
      ItemData data = new ItemData();
      data.Setup(0L, modeInfo.BossChallengeItemParam, modeInfo.BossChallengeItemNum);
      DataSource.Bind<ItemData>(this.mChallengeButton, data, false);
      GameParameter.UpdateAll(this.mChallengeButton);
      return true;
    }

    private void Downloaded(GameObject prefab)
    {
      if ((UnityEngine.Object) prefab == (UnityEngine.Object) null)
        DebugUtility.LogError("おかしい");
      UnityEngine.Object.Instantiate<GameObject>(prefab, this.mBGParent);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if ((UnityEngine.Object) instance1 == (UnityEngine.Object) null)
        return false;
      GenesisChapterManager instance2 = GenesisChapterManager.Instance;
      if ((UnityEngine.Object) instance2 == (UnityEngine.Object) null)
        return false;
      GenesisChapterParam genesisChapterParam = instance1.GetGenesisChapterParam(instance2.CurrentChapterParam.Iname);
      if (genesisChapterParam == null || string.IsNullOrEmpty(genesisChapterParam.BossHintUrl))
        return false;
      title = genesisChapterParam.Name;
      url = genesisChapterParam.BossHintUrl;
      return true;
    }

    public class GenesisBossData
    {
      public UnitParam unit;
      public int maxHP;
      public int currentHP;
    }
  }
}
