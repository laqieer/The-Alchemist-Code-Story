// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaLineupWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("UI/BoxGachaLineupWindow", 32741)]
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "ラインナップ更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "ラインナップ更新終了", FlowNode.PinTypes.Output, 101)]
  public class BoxGachaLineupWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    public const int PIN_IN_INITALIZE = 1;
    public const int PIN_IN_REFRESH = 100;
    public const int PIN_OT_REFRESH = 101;
    public BoxGachaLineupListWindow.SerializeParam m_LineupListWindowparam;
    private static BoxGachaLineupWindow m_Instnace;
    private BoxGachaLineupWindow.BoxAllLinupParam m_Lineup;
    [SerializeField]
    private Toggle StartTabToggle;
    [SerializeField]
    private Toggle EndTabToggle;
    [SerializeField]
    private Toggle TemplateTabToggle;

    public static BoxGachaLineupWindow Instance
    {
      get
      {
        return BoxGachaLineupWindow.m_Instnace;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Release();
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_LineupListWindowparam);
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) BoxGachaLineupWindow.m_Instnace == (UnityEngine.Object) null))
        return;
      BoxGachaLineupWindow.m_Instnace = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) BoxGachaLineupWindow.m_Instnace != (UnityEngine.Object) null))
        return;
      BoxGachaLineupWindow.m_Instnace = (BoxGachaLineupWindow) null;
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.StartTabToggle != (UnityEngine.Object) null)
        this.StartTabToggle.onValueChanged.AddListener((UnityAction<bool>) (_param1 => this.OnToggleChange(this.StartTabToggle)));
      if ((UnityEngine.Object) this.EndTabToggle != (UnityEngine.Object) null)
        this.EndTabToggle.onValueChanged.AddListener((UnityAction<bool>) (_param1 => this.OnToggleChange(this.EndTabToggle)));
      if (!((UnityEngine.Object) this.TemplateTabToggle != (UnityEngine.Object) null))
        return;
      this.TemplateTabToggle.onValueChanged.AddListener((UnityAction<bool>) (_param1 => this.OnToggleChange(this.TemplateTabToggle)));
      this.TemplateTabToggle.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update()
    {
      this.m_WindowController.Update();
    }

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
      SerializeValueBehaviour component1 = this.StartTabToggle.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      {
        Text uiLabel = component1.list.GetUILabel("count");
        if ((UnityEngine.Object) uiLabel != (UnityEngine.Object) null)
          uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT", new object[1]
          {
            (object) 1
          });
        component1.list.SetField("index", 0);
      }
      SerializeValueBehaviour component2 = this.EndTabToggle.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
      {
        Text uiLabel = component2.list.GetUILabel("count");
        if ((UnityEngine.Object) uiLabel != (UnityEngine.Object) null)
          uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT2", new object[1]
          {
            (object) this.m_Lineup.total_step
          });
        component2.list.SetField("index", this.m_Lineup.total_step - 1);
      }
      if (this.m_Lineup.total_step > 2)
      {
        int num = this.m_Lineup.total_step - 2;
        for (int index = 0; index < num; ++index)
        {
          Toggle tab = UnityEngine.Object.Instantiate<Toggle>(this.TemplateTabToggle);
          tab.onValueChanged.AddListener((UnityAction<bool>) (_param1 => this.OnToggleChange(tab)));
          tab.transform.SetParent(this.TemplateTabToggle.transform.parent, false);
          if ((UnityEngine.Object) tab != (UnityEngine.Object) null)
          {
            SerializeValueBehaviour component3 = tab.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
            {
              Text uiLabel = component3.list.GetUILabel("count");
              if ((UnityEngine.Object) uiLabel != (UnityEngine.Object) null)
              {
                string str = (index + 2).ToString();
                uiLabel.text = LocalizedText.Get("sys.GENESIS_GACHA_LINEUP_TAB_COUNT", new object[1]
                {
                  (object) str
                });
              }
              component3.list.SetField("index", index + 1);
            }
          }
          tab.gameObject.SetActive(true);
          tab.name = "Tab" + (index + 2).ToString();
        }
      }
      this.OnSelect(0);
    }

    private void OnToggleChange(Toggle toggle)
    {
      if ((UnityEngine.Object) toggle == (UnityEngine.Object) null || !toggle.isOn)
        return;
      SerializeValueBehaviour component = toggle.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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

      public string box_iname
      {
        get
        {
          return this.m_BoxIname;
        }
      }

      public int total_step
      {
        get
        {
          return this.m_TotalStep;
        }
      }

      public JSON_BoxGachaSteps[] steps
      {
        get
        {
          return this.m_Steps;
        }
      }

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
