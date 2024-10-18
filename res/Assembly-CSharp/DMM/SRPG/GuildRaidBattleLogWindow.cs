// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBattleLogWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize Top", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Reload", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(11, "Window Open", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Window Close", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(101, "Request Next", FlowNode.PinTypes.Output, 101)]
  public class GuildRaidBattleLogWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT_TOP = 1;
    public const int PIN_INPUT_RELOAD = 2;
    public const int PIN_INPUT_WINDOW_OPEN = 11;
    public const int PIN_INPUT_WINDOW_CLOSE = 12;
    public const int PIN_OUTPUT_REQUEST_NEXT = 101;
    private static GuildRaidBattleLogWindow mInstance;
    [SerializeField]
    private int WindowHeightMin;
    [SerializeField]
    private int WindowHeightMax;
    [SerializeField]
    private SRPG_ScrollRect Scroll;
    [SerializeField]
    private RectTransform ScrollContent;
    [SerializeField]
    private GameObject LogTemplate;
    private List<GameObject> LogList = new List<GameObject>();
    private bool IsLoading = true;
    private bool IsOpen;
    private const int LogCountByOneAPI = 20;
    public int CurrentPage;
    public int NextPage;

    public static GuildRaidBattleLogWindow Instance => GuildRaidBattleLogWindow.mInstance;

    public List<GuildRaidBattleLog> BattleLogList { get; private set; }

    private void Awake() => GuildRaidBattleLogWindow.mInstance = this;

    private void OnDestroy()
    {
      GuildRaidBattleLogWindow.mInstance = (GuildRaidBattleLogWindow) null;
    }

    private void Update()
    {
      if (this.IsLoading || !this.IsOpen || this.CurrentPage * 20 > this.BattleLogList.Count || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Scroll, (UnityEngine.Object) null) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ScrollContent) || (double) this.ScrollContent.sizeDelta.y - (double) (this.Scroll.verticalNormalizedPosition * this.ScrollContent.sizeDelta.y) >= 10.0)
        return;
      this.IsLoading = true;
      ++this.NextPage;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init(true);
          break;
        case 2:
          this.Init();
          break;
        case 11:
          this.StartCoroutine(this.Open());
          break;
        case 12:
          this.StartCoroutine(this.Close());
          break;
      }
    }

    private void Init(bool isTop = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.LogTemplate, (UnityEngine.Object) null))
        return;
      this.LogTemplate.SetActive(false);
      this.LogList.ForEach((Action<GameObject>) (item => UnityEngine.Object.Destroy((UnityEngine.Object) item)));
      this.LogList.Clear();
      if (this.BattleLogList == null)
        return;
      if (isTop)
      {
        if (this.BattleLogList.Count > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LogTemplate, this.LogTemplate.transform.parent);
          DataSource.Bind<GuildRaidBattleLog>(gameObject, this.BattleLogList[0]);
          gameObject.SetActive(true);
          this.LogList.Add(gameObject);
        }
      }
      else
      {
        for (int index = this.BattleLogList.Count - 1; index >= 0; --index)
        {
          if (this.BattleLogList[index] != null)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LogTemplate, this.LogTemplate.transform.parent);
            DataSource.Bind<GuildRaidBattleLog>(gameObject, this.BattleLogList[index]);
            gameObject.SetActive(true);
            this.LogList.Add(gameObject);
          }
        }
      }
      this.IsLoading = false;
    }

    public void SetupBattleLog(JSON_GuildRaidBattleLog json)
    {
      this.BattleLogList = new List<GuildRaidBattleLog>();
      if (json == null)
        return;
      GuildRaidBattleLog guildRaidBattleLog = new GuildRaidBattleLog();
      if (!guildRaidBattleLog.Deserialize(json))
        return;
      this.BattleLogList.Add(guildRaidBattleLog);
      this.CurrentPage = 0;
      this.NextPage = 1;
    }

    public void SetupBattleLog(JSON_GuildRaidBattleLog[] json, bool isOverwrite = false)
    {
      if (this.BattleLogList == null || isOverwrite)
        this.BattleLogList = new List<GuildRaidBattleLog>();
      if (json != null)
      {
        for (int index = 0; index < json.Length; ++index)
        {
          if (json[index] != null)
          {
            GuildRaidBattleLog guildRaidBattleLog = new GuildRaidBattleLog();
            if (guildRaidBattleLog.Deserialize(json[index]))
              this.BattleLogList.Add(guildRaidBattleLog);
          }
        }
      }
      ++this.CurrentPage;
    }

    [DebuggerHidden]
    private IEnumerator Open()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildRaidBattleLogWindow.\u003COpen\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator Close()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildRaidBattleLogWindow.\u003CClose\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }
  }
}
