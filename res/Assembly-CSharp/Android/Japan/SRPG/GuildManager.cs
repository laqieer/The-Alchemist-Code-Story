// Decompiled with JetBrains decompiler
// Type: SRPG.GuildManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "チャットボタン非表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(1010, "ロビー生成", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "設立or加入", FlowNode.PinTypes.Output, 1020)]
  public class GuildManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 10;
    public const int PIN_INPUT_HIDE_CHAT_BUTTON = 20;
    public const int PIN_OUTPUT_CREATE_LOBBY = 1010;
    public const int PIN_OUTPUT_CREATE_COMMAND = 1020;
    private static GuildManager mInstance;
    private GuildMemberData[] mEntryRequests;

    public static GuildManager Instance
    {
      get
      {
        return GuildManager.mInstance;
      }
    }

    public GuildMemberData[] EntryRequests
    {
      get
      {
        return this.mEntryRequests;
      }
    }

    private void Awake()
    {
      GuildManager.mInstance = this;
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(false);
      }
      else
        this.Init();
    }

    private void Init()
    {
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    public void SetEntryRequest(JSON_GuildMember[] json)
    {
      if (json == null)
      {
        this.mEntryRequests = new GuildMemberData[0];
      }
      else
      {
        this.mEntryRequests = new GuildMemberData[json.Length];
        for (int index = 0; index < json.Length; ++index)
        {
          this.mEntryRequests[index] = new GuildMemberData();
          this.mEntryRequests[index].Deserialize(json[index]);
        }
      }
    }
  }
}
