// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCpuInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "Selected", FlowNode.PinTypes.Output, 200)]
  public class VersusCpuInfo : MonoBehaviour, IFlowInterface
  {
    public ListItemEvents CpuPlayerTemplate;
    public GameObject CpuList;
    public GameObject MapInfo;
    public GameObject PartyInfo;
    public Color[] RankColor = new Color[0];
    private List<ListItemEvents> mVersusMember = new List<ListItemEvents>();

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.StartCoroutine(this.RefreshEnemy());
    }

    private void Awake() => GlobalVars.SelectedPartyIndex.Set(7);

    private void Start()
    {
      if (Object.op_Inequality((Object) this.CpuPlayerTemplate, (Object) null))
        ((Component) this.CpuPlayerTemplate).gameObject.SetActive(false);
      this.RefreshData();
    }

    private void Update()
    {
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Inequality((Object) this.MapInfo, (Object) null))
      {
        QuestParam quest = instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
        {
          DataSource.Bind<QuestParam>(this.MapInfo, quest);
          GameParameter.UpdateAll(this.MapInfo);
        }
      }
      if (!Object.op_Inequality((Object) this.PartyInfo, (Object) null))
        return;
      GlobalVars.SelectedPartyIndex.Set(7);
      PartyData party = instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      if (party == null)
        return;
      DataSource.Bind<PartyData>(this.PartyInfo, party);
      GameParameter.UpdateAll(this.PartyInfo);
    }

    [DebuggerHidden]
    private IEnumerator RefreshEnemy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusCpuInfo.\u003CRefreshEnemy\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      VersusCpuInfo.\u003CDownloadUnitImage\u003Ec__Iterator1 unitImageCIterator1 = new VersusCpuInfo.\u003CDownloadUnitImage\u003Ec__Iterator1();
      return (IEnumerator) unitImageCIterator1;
    }

    private void OnSelect(GameObject go)
    {
      VersusCpuData dataOfClass = DataSource.FindDataOfClass<VersusCpuData>(go, (VersusCpuData) null);
      if (dataOfClass == null)
        return;
      MonoSingleton<GameManager>.Instance.IsVSCpuBattle = true;
      GlobalVars.VersusCpu.Set(dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    private void OnOpenDetail(GameObject _go)
    {
      DataSource.FindDataOfClass<VersusCpuData>(_go, (VersusCpuData) null).Units[0]?.ShowTooltip(_go);
    }
  }
}
