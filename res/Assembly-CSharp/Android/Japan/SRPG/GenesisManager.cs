// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class GenesisManager : MonoBehaviour
  {
    private static GenesisManager mInstance;
    private static GenesisChapterParam mCurrentChapterParam;

    public static GenesisManager Instance
    {
      get
      {
        return GenesisManager.mInstance;
      }
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) GenesisManager.mInstance == (UnityEngine.Object) null))
        return;
      GenesisManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) GenesisManager.mInstance == (UnityEngine.Object) this))
        return;
      GenesisManager.mInstance = (GenesisManager) null;
    }

    public static GenesisChapterParam CurrentChapterParam
    {
      get
      {
        return GenesisManager.mCurrentChapterParam;
      }
      set
      {
        GenesisManager.mCurrentChapterParam = value;
      }
    }

    public bool LoadAssets<T>(string name, GenesisManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      if (callback == null)
        return false;
      this.StartCoroutine(this.DownloadCoroutine<T>(name, callback));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DownloadCoroutine<T>(string name, GenesisManager.LoadAssetCallback<T> callback) where T : UnityEngine.Object
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisManager.\u003CDownloadCoroutine\u003Ec__Iterator0<T>() { name = name, callback = callback };
    }

    public static void SetStarMissionInfo(ReqBtlCom.GenesisStar[] genesis_stars)
    {
      if (genesis_stars == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      for (int index1 = 0; index1 < genesis_stars.Length; ++index1)
      {
        ReqBtlCom.GenesisStar genesisStar = genesis_stars[index1];
        if (genesisStar != null && genesisStar.mode != null)
        {
          GenesisChapterParam chapterParamFromAreaId = instance.GetGenesisChapterParamFromAreaId(genesisStar.area_id);
          if (chapterParamFromAreaId != null)
          {
            for (int index2 = 0; index2 < genesisStar.mode.Length; ++index2)
            {
              ReqBtlCom.GenesisStar.Mode mode = genesisStar.mode[index2];
              if (mode != null && mode.is_reward != null)
              {
                GenesisChapterModeInfoParam modeInfo = chapterParamFromAreaId.GetModeInfo((QuestDifficulties) index2);
                if (modeInfo != null && modeInfo.StarParam != null)
                {
                  for (int index3 = 0; index3 < mode.is_reward.Length && index3 < modeInfo.StarParam.StarList.Count; ++index3)
                    modeInfo.StarParam.StarList[index3].IsReward = mode.is_reward[index3] != 0;
                }
              }
            }
          }
        }
      }
    }

    public delegate void LoadAssetCallback<T>(T obj) where T : UnityEngine.Object;
  }
}
