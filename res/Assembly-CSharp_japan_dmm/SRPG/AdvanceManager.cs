// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceManager
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
  public class AdvanceManager : MonoBehaviour
  {
    private static AdvanceManager mInstance;
    private static AdvanceEventParam mCurrentEventParam;
    private static ChapterParam mCurrentChapterParam;

    public static AdvanceManager Instance => AdvanceManager.mInstance;

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) AdvanceManager.mInstance, (Object) null))
        return;
      AdvanceManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) AdvanceManager.mInstance, (Object) this))
        return;
      AdvanceManager.mInstance = (AdvanceManager) null;
    }

    public static AdvanceEventParam CurrentEventParam
    {
      get => AdvanceManager.mCurrentEventParam;
      set => AdvanceManager.mCurrentEventParam = value;
    }

    public static ChapterParam CurrentChapterParam
    {
      get => AdvanceManager.mCurrentChapterParam;
      set => AdvanceManager.mCurrentChapterParam = value;
    }

    public bool LoadAssets<T>(string name, AdvanceManager.LoadAssetCallback<T> callback) where T : Object
    {
      if (callback == null)
        return false;
      this.StartCoroutine(this.DownloadCoroutine<T>(name, callback));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DownloadCoroutine<T>(
      string name,
      AdvanceManager.LoadAssetCallback<T> callback)
      where T : Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AdvanceManager.\u003CDownloadCoroutine\u003Ec__Iterator0<T>()
      {
        name = name,
        callback = callback
      };
    }

    public static void SetStarMissionInfo(ReqBtlCom.AdvanceStar[] genesis_stars)
    {
      if (genesis_stars == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      for (int index1 = 0; index1 < genesis_stars.Length; ++index1)
      {
        ReqBtlCom.AdvanceStar genesisStar = genesis_stars[index1];
        if (genesisStar != null && genesisStar.mode != null)
        {
          AdvanceEventParam eventParamFromAreaId = instance.GetAdvanceEventParamFromAreaId(genesisStar.area_id);
          if (eventParamFromAreaId != null)
          {
            for (int difficulty = 0; difficulty < genesisStar.mode.Length; ++difficulty)
            {
              ReqBtlCom.AdvanceStar.Mode mode = genesisStar.mode[difficulty];
              if (mode != null && mode.is_reward != null)
              {
                AdvanceEventModeInfoParam modeInfo = eventParamFromAreaId.GetModeInfo((QuestDifficulties) difficulty);
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
