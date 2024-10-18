// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(70, "表示更新", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(1000, "アイテムで強化UI", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "ゼニーで強化UI", FlowNode.PinTypes.Output, 1010)]
  public class GuildFacilityEnhance : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_REFRESH = 70;
    private const int PIN_OUTPUT_OPEN_ENHANCE_ITEM = 1000;
    private const int PIN_OUTPUT_OPEN_ENHANCE_GOLD = 1010;
    [SerializeField]
    private GameObject mFacilityViewItemTemplate;
    private List<GameObject> mCreatedViewItems = new List<GameObject>();

    public void Activated(int pinID)
    {
      if (pinID != 70)
        return;
      this.Refresh_FacilityList();
    }

    private void Start() => this.Init();

    private void Init()
    {
      DataSource.Bind<GuildData>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player.Guild);
      this.Refresh_FacilityList();
    }

    public void Refresh_FacilityList()
    {
      this.mFacilityViewItemTemplate.SetActive(false);
      for (int index = 0; index < this.mCreatedViewItems.Count; ++index)
        this.mCreatedViewItems[index].SetActive(false);
      GuildData guild = MonoSingleton<GameManager>.Instance.Player.Guild;
      int num = guild.Facilities.Length - this.mCreatedViewItems.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.mFacilityViewItemTemplate);
        gameObject.transform.SetParent(this.mFacilityViewItemTemplate.transform.parent, false);
        this.mCreatedViewItems.Add(gameObject);
      }
      for (int index = 0; index < guild.Facilities.Length; ++index)
      {
        this.mCreatedViewItems[index].SetActive(true);
        DataSource.Bind<GuildFacilityData>(this.mCreatedViewItems[index], guild.Facilities[index]);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public void OnClick_GuildFacilityItem(GameObject item)
    {
      if (Object.op_Equality((Object) item, (Object) null))
        return;
      GuildFacilityData dataOfClass = DataSource.FindDataOfClass<GuildFacilityData>(item, (GuildFacilityData) null);
      if (dataOfClass == null)
        return;
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      component.list.SetObject(GuildSVB_Key.FACILITY, (object) dataOfClass);
      int pinID = 1000;
      switch (dataOfClass.Param.EnhanceType)
      {
        case GuildFacilityParam.eEnhanceType.ITEM:
          pinID = 1000;
          break;
        case GuildFacilityParam.eEnhanceType.GOLD:
          pinID = 1010;
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }
  }
}
