// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRankingTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "自身のリーグ情報取得", FlowNode.PinTypes.Output, 100)]
  public class GvGLeagueRankingTop : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 0;
    private const int PIN_INPUT_REFRESH = 1;
    private const int PIN_OUTPUT_REQUEST = 100;
    [SerializeField]
    private FlowNode_ReqGvGLeague mRequestNode;

    public void Activated(int pinID)
    {
      if (pinID != 0)
      {
        if (pinID != 1)
          return;
        this.Refresh();
      }
      else
      {
        this.Init();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void Init()
    {
      if (Object.op_Equality((Object) this.mRequestNode, (Object) null))
        DebugUtility.LogError("mRequestNode が null");
      else
        this.mRequestNode.SetRequestMyLeague();
    }

    private void Refresh()
    {
      if (GvGLeagueManager.Instance.MyGuildLeagueData == null)
        return;
      DataSource.Bind<GvGLeagueViewGuild>(((Component) this).gameObject, GvGLeagueManager.Instance.MyGuildLeagueData);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
