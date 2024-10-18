// Decompiled with JetBrains decompiler
// Type: SRPG.GuildDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      SerializeValueBehaviour component1 = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
        return;
      GuildData data = component1.list.GetObject<GuildData>(GuildSVB_Key.GUILD);
      if (data != null)
      {
        DataSource.Bind<GuildData>(this.gameObject, data, false);
      }
      else
      {
        int num = component1.list.GetInt(GuildSVB_Key.GUILD_ID);
        if (num <= 0)
          return;
        FlowNode_ReqGuildInfo component2 = this.gameObject.GetComponent<FlowNode_ReqGuildInfo>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.SetParam((long) num);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
      }
    }

    private void Refresh()
    {
      FlowNode_ReqGuildInfo component = this.gameObject.GetComponent<FlowNode_ReqGuildInfo>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        DataSource.Bind<GuildData>(this.gameObject, component.GuildData, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
