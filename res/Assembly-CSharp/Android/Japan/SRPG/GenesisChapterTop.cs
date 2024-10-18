// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  public class GenesisChapterTop : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PIN_IN_INIT = 1;
    public const int PIN_OUT_INIT = 101;
    [SerializeField]
    private Transform mBGParent;
    [SerializeField]
    private GameObject[] mBossButtonGO;

    public void Activated(int pinID)
    {
      if (pinID != 1 || this.Init())
        return;
      DebugUtility.LogError("おかしい");
    }

    private bool Init()
    {
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      GenesisChapterParam genesisChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(instance.CurrentChapterParam.Iname);
      if (genesisChapterParam == null || instance.GenesisAssets.ChapterBG.Length <= genesisChapterParam.ChapterUiIndex)
        return false;
      instance.LoadAssets<GameObject>(instance.GenesisAssets.ChapterBG[genesisChapterParam.ChapterUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.Downloaded));
      for (int index = 0; index < this.mBossButtonGO.Length; ++index)
        this.SetupBossButton(this.mBossButtonGO[index], (QuestDifficulties) index);
      return true;
    }

    private void SetupBossButton(GameObject buttonGO, QuestDifficulties difficulty)
    {
      if ((UnityEngine.Object) buttonGO == (UnityEngine.Object) null)
        return;
      GenesisChapterParam genesisChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(GenesisChapterManager.Instance.CurrentChapterParam.Iname);
      if (genesisChapterParam == null || !genesisChapterParam.IsBossLiberation(difficulty))
        return;
      Transform transform = buttonGO.transform.Find("lock");
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
        transform.gameObject.SetActive(false);
      GenesisChapterModeInfoParam modeInfo = genesisChapterParam.GetModeInfo(difficulty);
      if (modeInfo == null)
        return;
      ItemData data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(modeInfo.BossChallengeItemParam.iname);
      if (data == null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(modeInfo.BossChallengeItemParam.iname);
        if (itemParam != null)
        {
          data = new ItemData();
          data.Setup(0L, itemParam, 0);
        }
      }
      DataSource.Bind<ItemData>(buttonGO, data, true);
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
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return false;
      GenesisChapterParam genesisChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(instance.CurrentChapterParam.Iname);
      if (genesisChapterParam == null || string.IsNullOrEmpty(genesisChapterParam.ChapterDetailUrl))
        return false;
      title = genesisChapterParam.Name;
      url = genesisChapterParam.ChapterDetailUrl;
      return true;
    }
  }
}
