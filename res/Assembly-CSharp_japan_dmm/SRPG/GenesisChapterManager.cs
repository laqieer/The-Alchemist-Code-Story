// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GenesisChapterManager : MonoBehaviour
  {
    [SerializeField]
    private GenesisAssets mGenesisAssets;
    private LoadRequest mResourceRequest;
    private static GenesisChapterManager mInstance;
    private GenesisChapterParam mCurrentChapterParam;
    private QuestDifficulties mStageDifficulty;
    private QuestDifficulties mBossDifficulty;
    private bool mRestorePointIsStage;
    private bool mJumpFromMission;
    private int mSelectedStarRewardIndex;
    private List<GiftData> mStarMissionGiftDataList;

    public static GenesisChapterManager Instance => GenesisChapterManager.mInstance;

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) GenesisChapterManager.mInstance, (Object) null))
        return;
      GenesisChapterManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) GenesisChapterManager.mInstance, (Object) this))
        return;
      GenesisChapterManager.mInstance = (GenesisChapterManager) null;
    }

    public GenesisChapterParam CurrentChapterParam => this.mCurrentChapterParam;

    public QuestDifficulties StageDifficulty => this.mStageDifficulty;

    public QuestDifficulties BossDifficulty => this.mBossDifficulty;

    public bool RestorePointIsStage => this.mRestorePointIsStage;

    public bool JumpFromMission => this.mJumpFromMission;

    public int SelectedStarRewardIndex => this.mSelectedStarRewardIndex;

    public GenesisAssets GenesisAssets => this.mGenesisAssets;

    public List<GiftData> StarMissionGiftDataList
    {
      get
      {
        return this.mStarMissionGiftDataList != null ? this.mStarMissionGiftDataList : new List<GiftData>();
      }
    }

    private void Start() => this.mCurrentChapterParam = GenesisManager.CurrentChapterParam;

    public void SetStageDifficulty(QuestDifficulties difficult)
    {
      this.mStageDifficulty = difficult;
    }

    public void SetBossDifficulty(QuestDifficulties difficult) => this.mBossDifficulty = difficult;

    public void SetStarRewardIndex(int index) => this.mSelectedStarRewardIndex = index;

    public void SetCurrentChapterParam(GenesisChapterParam chapter)
    {
      this.mCurrentChapterParam = chapter;
    }

    public void SetRestorePointIsStage(bool flag) => this.mRestorePointIsStage = flag;

    public void SetJumpFromMission(bool flag) => this.mJumpFromMission = flag;

    public bool LoadAssets<T>(
      string name,
      GenesisChapterManager.LoadAssetCallback<T> callback)
      where T : Object
    {
      if (callback == null || this.mResourceRequest != null)
        return false;
      this.mResourceRequest = AssetManager.LoadAsync<T>(name);
      if (this.mResourceRequest != null)
        this.StartCoroutine(this.DownloadCoroutine<T>(callback));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DownloadCoroutine<T>(
      GenesisChapterManager.LoadAssetCallback<T> callback)
      where T : Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisChapterManager.\u003CDownloadCoroutine\u003Ec__Iterator0<T>()
      {
        callback = callback,
        \u0024this = this
      };
    }

    public void SetStarMissionReward(Json_Gift[] rewards)
    {
      if (rewards == null)
        return;
      if (this.mStarMissionGiftDataList == null)
        this.mStarMissionGiftDataList = new List<GiftData>();
      this.mStarMissionGiftDataList.Clear();
      for (int index = 0; index < rewards.Length; ++index)
      {
        GiftData giftData = new GiftData();
        giftData.Deserialize(rewards[index]);
        this.mStarMissionGiftDataList.Add(giftData);
      }
    }

    public delegate void LoadAssetCallback<T>(T obj) where T : Object;
  }
}
