// Decompiled with JetBrains decompiler
// Type: SRPG.GuildDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "表示更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1010, "ギルド情報取得リクエスト", FlowNode.PinTypes.Output, 1010)]
  public class GuildDetailWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_REFRESH = 10;
    private const int PIN_OUTPUT_REQUEST_GUILD_INFO = 1010;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.Refresh();
    }

    private void Start()
    {
      SerializeValueBehaviour component1 = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Equality((Object) component1, (Object) null))
        return;
      GuildData data = component1.list.GetObject<GuildData>(GuildSVB_Key.GUILD);
      if (data != null)
      {
        DataSource.Bind<GuildData>(((Component) this).gameObject, data);
      }
      else
      {
        int guild_id = component1.list.GetInt(GuildSVB_Key.GUILD_ID);
        if (guild_id <= 0)
          return;
        FlowNode_ReqGuildInfo component2 = ((Component) this).gameObject.GetComponent<FlowNode_ReqGuildInfo>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          return;
        component2.SetParam((long) guild_id);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
      }
    }

    private void Refresh()
    {
      FlowNode_ReqGuildInfo component = ((Component) this).gameObject.GetComponent<FlowNode_ReqGuildInfo>();
      if (Object.op_Inequality((Object) component, (Object) null))
        DataSource.Bind<GuildData>(((Component) this).gameObject, component.GuildData);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
