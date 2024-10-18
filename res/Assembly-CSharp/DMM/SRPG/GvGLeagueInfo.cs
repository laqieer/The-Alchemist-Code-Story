// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "表示更新", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "リーグの報酬情報表示", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "自身のリーグ情報取得", FlowNode.PinTypes.Output, 101)]
  public class GvGLeagueInfo : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_REFRESH = 11;
    private const int PIN_OUTPUT_REWARD_INFO = 100;
    private const int PIN_OUTPUT_REQUEST = 101;
    [SerializeField]
    private GvGLeagueInfoContent mLeagueInfoTemplate;
    [SerializeField]
    private GameObject mHeaderObject;
    [SerializeField]
    private FlowNode_ReqGvGLeague mRequestNode;
    private List<GameObject> LeagueList = new List<GameObject>();
    private List<GvGLeagueParam> m_LeagueParams = new List<GvGLeagueParam>();

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.mLeagueInfoTemplate, false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 11)
          return;
        this.Refresh();
      }
      else
      {
        this.Init();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRequestNode, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("mRequestNode が null");
      }
      else
      {
        this.m_LeagueParams = GvGLeagueParam.GetGvGLeagueParamAll();
        this.mRequestNode.SetRequestMyLeague();
      }
    }

    private void Refresh()
    {
      this.CreateLeagueInfo();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHeaderObject, (UnityEngine.Object) null))
        return;
      DataSource.Bind<GvGLeagueViewGuild>(this.mHeaderObject, GvGLeagueManager.Instance.MyGuildLeagueData);
      DataSource.Bind<GuildData>(this.mHeaderObject, MonoSingleton<GameManager>.Instance.Player.Guild);
      GameParameter.UpdateAll(this.mHeaderObject);
    }

    public void OnClickGvGLeagueReward(GameObject obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
        return;
      GvGLeagueInfoContent componentInParent = obj.GetComponentInParent<GvGLeagueInfoContent>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
        return;
      GuildManager.Instance.SelectLeagueParam = componentInParent.GetGvGLeagueParam();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void CreateLeagueInfo()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mLeagueInfoTemplate, (UnityEngine.Object) null))
        return;
      Transform parent = ((Component) this.mLeagueInfoTemplate).transform.parent;
      this.LeagueList.ForEach((Action<GameObject>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) p)));
      this.LeagueList.Clear();
      for (int index = 0; index < this.m_LeagueParams.Count; ++index)
      {
        GvGLeagueInfoContent gleagueInfoContent = UnityEngine.Object.Instantiate<GvGLeagueInfoContent>(this.mLeagueInfoTemplate, parent, false);
        gleagueInfoContent.Setup(this.m_LeagueParams[index]);
        ((Component) gleagueInfoContent).gameObject.SetActive(true);
        this.LeagueList.Add(((Component) gleagueInfoContent).gameObject);
      }
      GameParameter.UpdateAll(((Component) parent).gameObject);
    }
  }
}
