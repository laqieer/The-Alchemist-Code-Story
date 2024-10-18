// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCpuInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "Selected", FlowNode.PinTypes.Output, 200)]
  public class VersusCpuInfo : MonoBehaviour, IFlowInterface
  {
    public Color[] RankColor = new Color[0];
    private List<ListItemEvents> mVersusMember = new List<ListItemEvents>();
    public ListItemEvents CpuPlayerTemplate;
    public GameObject CpuList;
    public GameObject MapInfo;
    public GameObject PartyInfo;

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.StartCoroutine(this.RefreshEnemy());
    }

    private void Awake()
    {
      GlobalVars.SelectedPartyIndex.Set(7);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.CpuPlayerTemplate != (UnityEngine.Object) null)
        this.CpuPlayerTemplate.gameObject.SetActive(false);
      this.RefreshData();
    }

    private void Update()
    {
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((UnityEngine.Object) this.MapInfo != (UnityEngine.Object) null)
      {
        QuestParam quest = instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
        {
          DataSource.Bind<QuestParam>(this.MapInfo, quest, false);
          GameParameter.UpdateAll(this.MapInfo);
        }
      }
      if (!((UnityEngine.Object) this.PartyInfo != (UnityEngine.Object) null))
        return;
      GlobalVars.SelectedPartyIndex.Set(7);
      PartyData party = instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      if (party == null)
        return;
      DataSource.Bind<PartyData>(this.PartyInfo, party, false);
      GameParameter.UpdateAll(this.PartyInfo);
    }

    [DebuggerHidden]
    private IEnumerator RefreshEnemy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusCpuInfo.\u003CRefreshEnemy\u003Ec__Iterator0() { \u0024this = this };
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
      DataSource.FindDataOfClass<VersusCpuData>(_go, (VersusCpuData) null).Units[0]?.ShowTooltip(_go, false, (UnitJobDropdown.ParentObjectEvent) null);
    }
  }
}
