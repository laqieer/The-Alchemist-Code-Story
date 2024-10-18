// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DownloadAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/アセットのダウンロード", 16711935)]
  [FlowNode.Pin(0, "ダウンロード", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "確認", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "ダウンロード開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ダウンロード完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "キャンセル", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(99, "エラー発生", FlowNode.PinTypes.Output, 99)]
  public class FlowNode_DownloadAssets : FlowNode
  {
    public bool UpdateFileList;
    public string[] AssetPaths = new string[0];
    public string[] DownloadQuests = new string[0];
    public string[] DownloadUnits = new string[0];
    [BitMask]
    public FlowNode_DownloadAssets.DownloadAssets Download;
    public string ConfirmText;
    public string AlreadyDownloadText;
    public string CompleteText;
    public bool ProgressBar;
    public bool SkipIfTutIncomplete;
    public bool AutoRetry = true;
    private List<AssetList.Item> mQueue;
    private const string MANAGED_BGM_ASSET_PATH = "StreamingAssets/";
    private const string UNMANAGED_BGM_ASSET_PATH = "sound/";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 1 || ((Behaviour) this).enabled)
        return;
      if (!AssetManager.UseDLC || !GameUtility.Config_UseAssetBundles.Value)
        this.ActivateOutputLinks(11);
      else if (this.SkipIfTutIncomplete && (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
      {
        this.ActivateOutputLinks(11);
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.StartCoroutine(this.AsyncWork(pinID == 1, false));
      }
    }

    private void OnDownloadStart(GameObject dialog)
    {
      FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", "1");
      this.StartCoroutine(this.AsyncWork(false, true));
      this.ActivateOutputLinks(10);
    }

    private void OnDownloadCancel(GameObject dialog)
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    [DebuggerHidden]
    private IEnumerator AsyncWork(bool confirm, bool allDownload)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAsyncWork\u003Ec__Iterator0()
      {
        confirm = confirm,
        allDownload = allDownload,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator AddAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAddAssets\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    public static void AddAssets_Towns()
    {
      List<SectionParam> storySelctionParams = MonoSingleton<GameManager>.Instance.GetAllStorySelctionParams();
      for (int index = 0; index < storySelctionParams.Count; ++index)
        DownloadUtility.PrepareHomeBGAssets(storySelctionParams[index]);
    }

    public static void AddAssets_All_PlayableUnits()
    {
      List<UnitParam> playableUnitParams = MonoSingleton<GameManager>.Instance.MasterParam.GetAllPlayableUnitParams();
      for (int index = 0; index < playableUnitParams.Count; ++index)
        DownloadUtility.DownloadUnit(playableUnitParams[index]);
    }

    public static void AddAssets_All_Artifacts()
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      for (int index = 0; index < artifacts.Count; ++index)
      {
        ArtifactParam artifalct = artifacts[index];
        if (artifalct.type == ArtifactTypes.Arms)
          DownloadUtility.DownloadArtifact(artifalct);
      }
    }

    public static void AddAssets_All_ConceptCards()
    {
      List<ConceptCardParam> conceptCardParams = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParams();
      for (int index = 0; index < conceptCardParams.Count; ++index)
        DownloadUtility.DownloadConceptCard(conceptCardParams[index]);
    }

    [DebuggerHidden]
    private IEnumerator AddAssets_ChallengableAllSectionQuests()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAddAssets_ChallengableAllSectionQuests\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }

    public static void AddAssets_Home_Raid()
    {
      List<RaidBossParam> raidBossAll = RaidStampRallyWindow.GetRaidBossAll(RaidManager.Instance.RaidPeriodId);
      for (int index = 0; index < raidBossAll.Count; ++index)
        DownloadUtility.PrepareRaidBossAsset(raidBossAll[index]);
    }

    public static void AddAssets_Home_Tower()
    {
      List<TowerFloorParam> towerFloors = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
      if (towerFloors == null)
        return;
      DownloadUtility.DownloadTowerQuests(towerFloors);
    }

    [DebuggerHidden]
    private IEnumerator AddAssets_Home_MultiTower()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAddAssets_Home_MultiTower\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator AddAssets_Home_MultiPlay()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAddAssets_Home_MultiPlay\u003Ec__Iterator4()
      {
        \u0024this = this
      };
    }

    public static void AddUnManagedAssets_All()
    {
      foreach (KeyValuePair<string, UnManagedAssetList.Item> mAsset in AssetManager.UnManagedAsset.mAssets)
      {
        if (!mAsset.Key.StartsWith("movies"))
          AssetDownloader.AddUnManagedData(mAsset.Key);
      }
    }

    public static void AddAssets_Home_GuildRaid()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
        return;
      List<GuildRaidBossParam> raidBossParamAll = MonoSingleton<GameManager>.Instance.GetRaidBossParamAll(GuildRaidManager.Instance.PeriodId);
      for (int index = 0; index < raidBossParamAll.Count; ++index)
        DownloadUtility.PrepareGuildRaidBossAsset(raidBossParamAll[index]);
    }

    public static void AddAssets_History_GuildRaid()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
        return;
      List<GuildRaidBossParam> raidBossParamAll = MonoSingleton<GameManager>.Instance.GetRaidBossParamAll(GuildRaidManager.Instance.PeriodId - 1);
      for (int index = 0; index < raidBossParamAll.Count; ++index)
        DownloadUtility.PrepareGuildRaidBossAsset(raidBossParamAll[index]);
    }

    public static void AddAssets_Home_JukeBox()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) JukeBoxWindow.Instance, (UnityEngine.Object) null))
        return;
      foreach (JukeBoxWindow.JukeBoxData jukeBoxData in JukeBoxWindow.JukeBoxDataList)
      {
        if (jukeBoxData.is_unlock)
        {
          if (EventAction.IsUnManagedAssets(jukeBoxData.param.Sheet, true))
          {
            Debug.LogWarning((object) ("Unmanaged : " + jukeBoxData.param.Sheet));
            AssetDownloader.AddUnManagedData("sound/" + jukeBoxData.param.Sheet + ".acb");
            AssetDownloader.AddUnManagedData("sound/" + jukeBoxData.param.Sheet + ".awb");
          }
          else
          {
            AssetManager.PrepareAssets("StreamingAssets/" + jukeBoxData.param.Sheet + ".acb");
            AssetManager.PrepareAssets("StreamingAssets/" + jukeBoxData.param.Sheet + ".awb");
          }
        }
      }
    }

    public static void AddAssets_Home_AutoRepeatQuest()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) AutoRepeatQuestTop.Instance, (UnityEngine.Object) null))
        return;
      AutoRepeatQuestTop.Instance.PrepareAssets();
    }

    public static void AddAssets_Home_WorldRaid()
    {
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return;
      for (int index = 0; index < currentWorldRaidParam.BossInfoList.Count; ++index)
        DownloadUtility.PrepareWorldRaidBossAsset(currentWorldRaidParam.BossInfoList[index]);
    }

    [Flags]
    public enum DownloadAssets
    {
      Tutorial = 1,
      OwnUnits = 2,
      AllUnits = 4,
      ItemIcons = 8,
      Multiplay = 16, // 0x00000010
      Artifacts = 32, // 0x00000020
      LoginBonus = 64, // 0x00000040
      OwnConceptCard = 128, // 0x00000080
      SkinConceptCard = 256, // 0x00000100
      Town = 512, // 0x00000200
      iOSRequire = 1024, // 0x00000400
      All_PlayableUnits = 2048, // 0x00000800
      All_Artifacts = 4096, // 0x00001000
      All_ConceptCards = 8192, // 0x00002000
      ChallengableAllSectionQuests = 16384, // 0x00004000
      Home_Raid = 32768, // 0x00008000
      Home_Tower = 65536, // 0x00010000
      Home_MultiTower = 131072, // 0x00020000
      Home_MultiPlay = 262144, // 0x00040000
      All_UnManagedAssets = 524288, // 0x00080000
      Home_GuildRaid = 1048576, // 0x00100000
      History_GuildRaid = 2097152, // 0x00200000
      HomeAssets = 4194304, // 0x00400000
      Home_JukeBox = 8388608, // 0x00800000
      Home_AutoRepeatQuest = 16777216, // 0x01000000
      Home_WorldRaid = 33554432, // 0x02000000
    }
  }
}
