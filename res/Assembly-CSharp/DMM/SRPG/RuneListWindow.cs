// Decompiled with JetBrains decompiler
// Type: SRPG.RuneListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/リスト/ルーン")]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "詳細表示", FlowNode.PinTypes.Output, 100)]
  public class RuneListWindow : MonoBehaviour, IFlowInterface
  {
    private RuneManager mRuneManager;
    private RuneListWindow.RuneSource m_RuneSource;
    private ContentController m_ContenController;

    private event RuneListWindow.OnSelectedEvent mOnSelectedEvent = _param0 => { };

    private void Awake()
    {
      this.m_ContenController = ((Component) this).GetComponent<ContentController>();
      this.m_ContenController.SetWork((object) this);
    }

    public void Initialize(RuneManager manager, List<BindRuneData> runes)
    {
      this.mRuneManager = manager;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneManager, (UnityEngine.Object) null))
        DebugUtility.LogError("mRuneManager value not set in RuneListWindow.");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_ContenController, (UnityEngine.Object) null))
        DebugUtility.LogError("m_ContenController is unable to attach.");
      this.m_RuneSource = new RuneListWindow.RuneSource();
      for (int index = 0; index < runes.Count; ++index)
        this.m_RuneSource.Add(new RuneListWindow.RuneSource.ContentParam(runes[index]));
      this.m_ContenController.Initialize((ContentSource) this.m_RuneSource, Vector2.zero);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContenController, (UnityEngine.Object) null))
        this.m_ContenController.Release();
      this.m_ContenController = (ContentController) null;
      this.m_RuneSource = (RuneListWindow.RuneSource) null;
    }

    private void Update()
    {
    }

    public void UpdateGameParameterAll()
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.UpdateGameParameterAll();
    }

    public void Activated(int pinID)
    {
    }

    public void SetupNodeEvent(ContentNode node)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) node, (UnityEngine.Object) null))
        return;
      ListItemEvents component = ((Component) node).GetComponent<ListItemEvents>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
    }

    public void SetSelectedCallBack(RuneListWindow.OnSelectedEvent selected)
    {
      this.mOnSelectedEvent = selected;
    }

    private void OnSelect(GameObject go)
    {
      BindRuneData dataOfClass = DataSource.FindDataOfClass<BindRuneData>(go, (BindRuneData) null);
      if (dataOfClass == null)
        return;
      this.mOnSelectedEvent(dataOfClass);
    }

    public void SelectTypeCurrent(bool is_force = false)
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.SelectTypeCurrent(is_force);
    }

    public void SelectType(int seteff_type, bool is_force = false)
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.SelectType(seteff_type, is_force);
    }

    public void SelectType(RuneSlotIndex slot_index, bool is_force = false)
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.SelectType(slot_index, is_force);
    }

    public void SelectType(int seteff_type, RuneSlotIndex slot_index, bool is_force = false)
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.SelectType(seteff_type, slot_index, is_force);
    }

    public void SetLockDisable(bool is_lock_disable)
    {
      if (this.m_RuneSource == null)
        return;
      this.m_RuneSource.SetLockDisable(is_lock_disable);
    }

    public delegate void OnSelectedEvent(BindRuneData rune);

    private class RuneNode : ContentNode
    {
      private DataSource m_DataSource;
      private GameParameter[] m_GameParameters;
      private RuneDrawListIcon m_RuneDrawListIcon;

      public DataSource dataSource => this.m_DataSource;

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.m_DataSource = DataSource.Create(((Component) this).gameObject);
        this.m_GameParameters = ((Component) this).gameObject.GetComponentsInChildren<GameParameter>();
        this.m_RuneDrawListIcon = ((Component) this).gameObject.GetComponent<RuneDrawListIcon>();
        RuneListWindow work = controller.GetWork() as RuneListWindow;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) work, (UnityEngine.Object) null))
          return;
        work.SetupNodeEvent((ContentNode) this);
      }

      public override void Release() => base.Release();

      public void Refresh()
      {
        if (this.m_GameParameters != null)
        {
          for (int index = 0; index < this.m_GameParameters.Length; ++index)
          {
            GameParameter gameParameter = this.m_GameParameters[index];
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameParameter, (UnityEngine.Object) null))
              gameParameter.UpdateValue();
          }
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_RuneDrawListIcon, (UnityEngine.Object) null))
          return;
        this.m_RuneDrawListIcon.SetDrawParam(this.dataSource.FindDataOfClass<BindRuneData>((BindRuneData) null));
      }
    }

    public class RuneSource : ContentSource
    {
      private int m_RuneSetEffType;
      private RuneSlotIndex m_RuneSlotIndex = (RuneSlotIndex) byte.MaxValue;
      private List<RuneListWindow.RuneSource.ContentParam> m_Params = new List<RuneListWindow.RuneSource.ContentParam>();
      private bool m_IsLockDisable;

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.SelectType(0, (RuneSlotIndex) byte.MaxValue, true);
      }

      public override void Release()
      {
        base.Release();
        this.m_RuneSetEffType = 0;
        this.m_RuneSlotIndex = (RuneSlotIndex) byte.MaxValue;
      }

      public override ContentNode Instantiate(ContentNode res)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) res).gameObject);
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) ? (ContentNode) gameObject.AddComponent<RuneListWindow.RuneNode>() : (ContentNode) null;
      }

      public void Add(RuneListWindow.RuneSource.ContentParam param)
      {
        if (!param.IsValid())
          return;
        this.m_Params.Add(param);
      }

      public void UpdateGameParameterAll()
      {
        foreach (RuneListWindow.RuneSource.ContentParam contentParam in this.m_Params)
          contentParam?.GameParamUpdate();
      }

      public void SetLockDisable(bool is_lock_disable)
      {
        this.m_IsLockDisable = is_lock_disable;
        this.SelectTypeCurrent(true);
      }

      public void SelectTypeCurrent(bool is_force = false)
      {
        this.SelectType(this.m_RuneSetEffType, this.m_RuneSlotIndex, is_force);
      }

      public void SelectType(int seteff_type, bool is_force = false)
      {
        this.SelectType(seteff_type, this.m_RuneSlotIndex, is_force);
      }

      public void SelectType(RuneSlotIndex slot_index, bool is_force = false)
      {
        this.SelectType(this.m_RuneSetEffType, slot_index, is_force);
      }

      public void SelectType(int seteff_type, RuneSlotIndex slot_index, bool is_force = false)
      {
        if (!is_force && this.m_RuneSetEffType == seteff_type && (int) (byte) this.m_RuneSlotIndex == (int) (byte) slot_index)
          return;
        this.Clear();
        if (this.m_IsLockDisable)
          this.SetTable((ContentSource.Param[]) this.m_Params.Where<RuneListWindow.RuneSource.ContentParam>((Func<RuneListWindow.RuneSource.ContentParam, bool>) (param => (param.runeType == seteff_type || RuneSetEff.IsDefaultEffectType(seteff_type)) && ((int) (byte) param.runeSlotIndex == (int) (byte) slot_index || (byte) slot_index == byte.MaxValue) && !param.IsLock())).ToArray<RuneListWindow.RuneSource.ContentParam>());
        else
          this.SetTable((ContentSource.Param[]) this.m_Params.Where<RuneListWindow.RuneSource.ContentParam>((Func<RuneListWindow.RuneSource.ContentParam, bool>) (param =>
          {
            if (param.runeType != seteff_type && !RuneSetEff.IsDefaultEffectType(seteff_type))
              return false;
            return (int) (byte) param.runeSlotIndex == (int) (byte) slot_index || byte.MaxValue == (byte) slot_index;
          })).ToArray<RuneListWindow.RuneSource.ContentParam>());
        this.contentController.Resize();
        bool flag = false;
        Vector2 anchoredPosition = this.contentController.anchoredPosition;
        Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
        if ((double) anchoredPosition.x < (double) lastPageAnchorePos.x)
        {
          flag = true;
          anchoredPosition.x = lastPageAnchorePos.x;
        }
        if ((double) anchoredPosition.y < (double) lastPageAnchorePos.y)
        {
          flag = true;
          anchoredPosition.y = lastPageAnchorePos.y;
        }
        if (flag)
          this.contentController.anchoredPosition = anchoredPosition;
        this.contentController.scroller.StopMovement();
        this.m_RuneSetEffType = seteff_type;
        this.m_RuneSlotIndex = slot_index;
      }

      public class ContentParam : ContentSource.Param
      {
        private BindRuneData m_Rune;
        private RuneListWindow.RuneNode m_Node;

        public ContentParam(BindRuneData rune) => this.m_Rune = rune;

        public override bool IsValid() => this.m_Rune != null;

        public BindRuneData data => this.m_Rune;

        public int runeType => this.m_Rune.Rune.RuneParam.seteff_type;

        public RuneSlotIndex runeSlotIndex => this.m_Rune.Rune.RuneParam.slot_index;

        public override void OnSetup(ContentNode node)
        {
          this.m_Node = node as RuneListWindow.RuneNode;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
            return;
          this.m_Node.dataSource.Clear();
          this.m_Node.dataSource.Add(typeof (BindRuneData), (object) this.m_Rune);
          this.m_Node.Refresh();
        }

        public void GameParamUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
            return;
          this.m_Node.Refresh();
        }
      }
    }
  }
}
