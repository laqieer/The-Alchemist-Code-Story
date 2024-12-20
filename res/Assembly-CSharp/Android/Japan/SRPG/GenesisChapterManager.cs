﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
    private int mSelectedStarRewardIndex;
    private List<GiftData> mStarMissionGiftDataList;

    public static GenesisChapterManager Instance
    {
      get
      {
        return GenesisChapterManager.mInstance;
      }
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) GenesisChapterManager.mInstance == (UnityEngine.Object) null))
        return;
      GenesisChapterManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) GenesisChapterManager.mInstance == (UnityEngine.Object) this))
        return;
      GenesisChapterManager.mInstance = (GenesisChapterManager) null;
    }

    public GenesisChapterParam CurrentChapterParam
    {
      get
      {
        return this.mCurrentChapterParam;
      }
    }

    public QuestDifficulties StageDifficulty
    {
      get
      {
        return this.mStageDifficulty;
      }
    }

    public QuestDifficulties BossDifficulty
    {
      get
      {
        return this.mBossDifficulty;
      }
    }

    public bool RestorePointIsStage
    {
      get
      {
        return this.mRestorePointIsStage;
      }
    }

    public int SelectedStarRewardIndex
    {
      get
      {
        return this.mSelectedStarRewardIndex;
      }
    }

    public GenesisAssets GenesisAssets
    {
      get
      {
        return this.mGenesisAssets;
      }
    }

    public List<GiftData> StarMissionGiftDataList
    {
      get
      {
        if (this.mStarMissionGiftDataList != null)
          return this.mStarMissionGiftDataList;
        return new List<GiftData>();
      }
    }

    private void Start()
    {
      this.mCurrentChapterParam = GenesisManager.CurrentChapterParam;
    }

    public void SetStageDifficulty(QuestDifficulties difficult)
    {
      this.mStageDifficulty = difficult;
    }

    public void SetBossDifficulty(QuestDifficulties difficult)
    {
      this.mBossDifficulty = difficult;
    }

    public void SetStarRewardIndex(int index)
    {
      this.mSelectedStarRewardIndex = index;
    }

    public void SetCurrentChapterParam(GenesisChapterParam chapter)
    {
      this.mCurrentChapterParam = chapter;
    }

    public void SetRestorePointIsStage(bool flag)
    {
      this.mRestorePointIsStage = flag;
    }

    public bool LoadAssets<T>(string name, GenesisChapterManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      if (callback == null || this.mResourceRequest != null)
        return false;
      this.mResourceRequest = AssetManager.LoadAsync<T>(name);
      if (this.mResourceRequest != null)
        this.StartCoroutine(this.DownloadCoroutine<T>(callback));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DownloadCoroutine<T>(GenesisChapterManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisChapterManager.\u003CDownloadCoroutine\u003Ec__Iterator0<T>() { callback = callback, \u0024this = this };
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

    public delegate void LoadAssetCallback<T>(T obj) where T : UnityEngine.Object;
  }
}
