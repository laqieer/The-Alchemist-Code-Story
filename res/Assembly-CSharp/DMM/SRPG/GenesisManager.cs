// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GenesisManager : MonoBehaviour
  {
    private static GenesisManager mInstance;
    private static GenesisChapterParam mCurrentChapterParam;

    public static GenesisManager Instance => GenesisManager.mInstance;

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) GenesisManager.mInstance, (Object) null))
        return;
      GenesisManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) GenesisManager.mInstance, (Object) this))
        return;
      GenesisManager.mInstance = (GenesisManager) null;
    }

    public static GenesisChapterParam CurrentChapterParam
    {
      get => GenesisManager.mCurrentChapterParam;
      set => GenesisManager.mCurrentChapterParam = value;
    }

    public bool LoadAssets<T>(string name, GenesisManager.LoadAssetCallback<T> callback) where T : Object
    {
      if (callback == null)
        return false;
      this.StartCoroutine(this.DownloadCoroutine<T>(name, callback));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DownloadCoroutine<T>(
      string name,
      GenesisManager.LoadAssetCallback<T> callback)
      where T : Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisManager.\u003CDownloadCoroutine\u003Ec__Iterator0<T>()
      {
        name = name,
        callback = callback
      };
    }

    public static void SetStarMissionInfo(ReqBtlCom.GenesisStar[] genesis_stars)
    {
      if (genesis_stars == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      for (int index1 = 0; index1 < genesis_stars.Length; ++index1)
      {
        ReqBtlCom.GenesisStar genesisStar = genesis_stars[index1];
        if (genesisStar != null && genesisStar.mode != null)
        {
          GenesisChapterParam chapterParamFromAreaId = instance.GetGenesisChapterParamFromAreaId(genesisStar.area_id);
          if (chapterParamFromAreaId != null)
          {
            for (int difficulty = 0; difficulty < genesisStar.mode.Length; ++difficulty)
            {
              ReqBtlCom.GenesisStar.Mode mode = genesisStar.mode[difficulty];
              if (mode != null && mode.is_reward != null)
              {
                GenesisChapterModeInfoParam modeInfo = chapterParamFromAreaId.GetModeInfo((QuestDifficulties) difficulty);
                if (modeInfo != null && modeInfo.StarParam != null)
                {
                  for (int index2 = 0; index2 < mode.is_reward.Length && index2 < modeInfo.StarParam.StarList.Count; ++index2)
                    modeInfo.StarParam.StarList[index2].IsReward = mode.is_reward[index2] != 0;
                }
              }
            }
          }
        }
      }
    }

    public delegate void LoadAssetCallback<T>(T obj) where T : Object;
  }
}
