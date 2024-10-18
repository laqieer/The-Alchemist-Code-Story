// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaLineupWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/BoxGachaLineupWindow", 32741)]
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "ラインナップ更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "ラインナップ更新終了", FlowNode.PinTypes.Output, 101)]
  public class BoxGachaLineupWindow : FlowNodePersistent
  {
    public const int PIN_IN_INITALIZE = 1;
    public const int PIN_IN_REFRESH = 100;
    public const int PIN_OT_REFRESH = 101;
    public BoxGachaLineupListWindow.SerializeParam m_LineupListWindowparam;
    private FlowWindowController m_WindowController = new FlowWindowController();
    private static BoxGachaLineupWindow m_Instnace;
    private BoxGachaLineupWindow.BoxAllLinupParam m_Lineup;
    [SerializeField]
    private Toggle StartTabToggle;
    [SerializeField]
    private Toggle EndTabToggle;
    [SerializeField]
    private Toggle TemplateTabToggle;

    public static BoxGachaLineupWindow Instance => BoxGachaLineupWindow.m_Instnace;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Release();
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_LineupListWindowparam);
    }

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) BoxGachaLineupWindow.m_Instnace, (Object) null))
        return;
      BoxGachaLineupWindow.m_Instnace = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Inequality((Object) BoxGachaLineupWindow.m_Instnace, (Object) null))
        return;
      BoxGachaLineupWindow.m_Instnace = (BoxGachaLineupWindow) null;
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.StartTabToggle, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.StartTabToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__0)));
      }
      if (Object.op_Inequality((Object) this.EndTabToggle, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.EndTabToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__1)));
      }
      if (!Object.op_Inequality((Object) this.TemplateTabToggle, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.TemplateTabToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__2)));
      ((Component) this.TemplateTabToggle).gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update() => this.m_WindowController.Update();

    public override void OnActivate(int pinID)
    {
      if (pinID >= 100)
        this.m_WindowController.OnActivate(pinID);
      else if (pinID == 1)
        this.Inialize();
      this.ActivateOutputLinks(101);
    }

    public void Inialize()
    {
      if (this.m_Lineup == null)
        return;
      SerializeValueBehaviour component1 = ((Component) this.StartTabToggle).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component1, (Object) null))
      {
        Text uiLabel = component1.list.GetUILabel("count");
        if (Object.op_Inequality((Object) uiLabel, (Object) null))
          uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT", (object) 1);
        component1.list.SetField("index", 0);
      }
      SerializeValueBehaviour component2 = ((Component) this.EndTabToggle).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component2, (Object) null))
      {
        Text uiLabel = component2.list.GetUILabel("count");
        if (Object.op_Inequality((Object) uiLabel, (Object) null))
          uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT2", (object) this.m_Lineup.total_step);
        component2.list.SetField("index", this.m_Lineup.total_step - 1);
      }
      if (this.m_Lineup.total_step > 2)
      {
        int num = this.m_Lineup.total_step - 2;
        for (int index = 0; index < num; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          BoxGachaLineupWindow.\u003CInialize\u003Ec__AnonStorey0 inializeCAnonStorey0 = new BoxGachaLineupWindow.\u003CInialize\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          inializeCAnonStorey0.\u0024this = this;
          // ISSUE: reference to a compiler-generated field
          inializeCAnonStorey0.tab = Object.Instantiate<Toggle>(this.TemplateTabToggle);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent<bool>) inializeCAnonStorey0.tab.onValueChanged).AddListener(new UnityAction<bool>((object) inializeCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          // ISSUE: reference to a compiler-generated field
          ((Component) inializeCAnonStorey0.tab).transform.SetParent(((Component) this.TemplateTabToggle).transform.parent, false);
          // ISSUE: reference to a compiler-generated field
          if (Object.op_Inequality((Object) inializeCAnonStorey0.tab, (Object) null))
          {
            // ISSUE: reference to a compiler-generated field
            SerializeValueBehaviour component3 = ((Component) inializeCAnonStorey0.tab).GetComponent<SerializeValueBehaviour>();
            if (Object.op_Inequality((Object) component3, (Object) null))
            {
              Text uiLabel = component3.list.GetUILabel("count");
              if (Object.op_Inequality((Object) uiLabel, (Object) null))
              {
                string str = (index + 2).ToString();
                uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT", (object) str);
              }
              component3.list.SetField("index", index + 1);
            }
          }
          // ISSUE: reference to a compiler-generated field
          ((Component) inializeCAnonStorey0.tab).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          ((Object) inializeCAnonStorey0.tab).name = "Tab" + (index + 2).ToString();
        }
      }
      this.OnSelect(0);
    }

    private void OnToggleChange(Toggle toggle)
    {
      if (Object.op_Equality((Object) toggle, (Object) null) || !toggle.isOn)
        return;
      SerializeValueBehaviour component = ((Component) toggle).GetComponent<SerializeValueBehaviour>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      int index = component.list.GetInt("index");
      if (index == -1)
        return;
      this.OnSelect(index);
    }

    private void OnSelect(int index)
    {
      if (this.m_Lineup.total_step < index)
      {
        DebugUtility.LogError(string.Empty);
      }
      else
      {
        BoxGachaLineupListWindow.Instance?.DeserializeLineupList(this.m_Lineup.steps[index], true);
        this.m_WindowController.OnActivate(100);
      }
    }

    public void DeserializeAllLineup(ReqBoxLineup.Response json)
    {
      this.m_Lineup = new BoxGachaLineupWindow.BoxAllLinupParam();
      this.m_Lineup.Deserialize(json);
    }

    public class BoxAllLinupParam
    {
      private string m_BoxIname = string.Empty;
      private int m_TotalStep;
      private JSON_BoxGachaSteps[] m_Steps;

      public string box_iname => this.m_BoxIname;

      public int total_step => this.m_TotalStep;

      public JSON_BoxGachaSteps[] steps => this.m_Steps;

      public bool Deserialize(ReqBoxLineup.Response json)
      {
        if (json == null)
          return false;
        this.m_BoxIname = json.box_iname;
        this.m_TotalStep = json.total_step;
        this.m_Steps = new JSON_BoxGachaSteps[json.steps.Length];
        for (int index = 0; index < json.steps.Length; ++index)
          this.m_Steps[index] = json.steps[index];
        return true;
      }
    }
  }
}
