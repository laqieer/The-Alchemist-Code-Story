// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceEventTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  public class AdvanceEventTop : MonoBehaviour, IFlowInterface, IWebHelp
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
      GlobalVars.SelectedQuestID = string.Empty;
      AdvanceEventManager instance = AdvanceEventManager.Instance;
      AdvanceEventParam advanceEventParam = MonoSingleton<GameManager>.Instance.GetAdvanceEventParam(instance.CurrentEventParam.Iname);
      if (advanceEventParam == null || instance.AdvanceAssets.EventBG.Length <= advanceEventParam.EventUiIndex)
        return false;
      instance.LoadAssets<GameObject>(instance.AdvanceAssets.EventBG[advanceEventParam.EventUiIndex], new AdvanceEventManager.LoadAssetCallback<GameObject>(this.Downloaded));
      for (int difficulty = 0; difficulty < this.mBossButtonGO.Length; ++difficulty)
        this.SetupBossButton(this.mBossButtonGO[difficulty], (QuestDifficulties) difficulty);
      return true;
    }

    private void SetupBossButton(GameObject buttonGO, QuestDifficulties difficulty)
    {
      if (Object.op_Equality((Object) buttonGO, (Object) null))
        return;
      AdvanceEventParam advanceEventParam = MonoSingleton<GameManager>.Instance.GetAdvanceEventParam(AdvanceEventManager.Instance.CurrentEventParam.Iname);
      if (advanceEventParam == null || !advanceEventParam.IsBossLiberation(difficulty))
        return;
      Transform transform = buttonGO.transform.Find("lock");
      if (Object.op_Inequality((Object) transform, (Object) null))
        ((Component) transform).gameObject.SetActive(false);
      AdvanceEventModeInfoParam modeInfo = advanceEventParam.GetModeInfo(difficulty);
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
      if (Object.op_Equality((Object) prefab, (Object) null))
        DebugUtility.LogError("おかしい");
      Object.Instantiate<GameObject>(prefab, this.mBGParent);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      AdvanceEventManager instance = AdvanceEventManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      AdvanceEventParam advanceEventParam = MonoSingleton<GameManager>.Instance.GetAdvanceEventParam(instance.CurrentEventParam.Iname);
      if (advanceEventParam == null || string.IsNullOrEmpty(advanceEventParam.EventDetailUrl))
        return false;
      title = advanceEventParam.Name;
      url = advanceEventParam.EventDetailUrl;
      return true;
    }
  }
}
