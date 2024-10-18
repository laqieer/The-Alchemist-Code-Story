﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/Tutorial Trigger", 58751)]
  [FlowNode.Pin(0, "Activate", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Deactivate", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ForceExec", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Triggered", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_TutorialTrigger : FlowNode
  {
    [BitMask]
    public FlowNode_TutorialTrigger.ActivateFlags Flags = FlowNode_TutorialTrigger.ActivateFlags.AutoDeactivate;
    public FlowNode_TutorialTrigger.TriggerTypes TriggerType;
    [SerializeField]
    private string sVal0;
    [SerializeField]
    private string sVal1;
    [SerializeField]
    private int iVal0;
    [SerializeField]
    private int iVal1;
    public bool DontSkipFlag;

    private bool CompareUnit(Unit unit, int turn)
    {
      if (!string.IsNullOrEmpty(this.sVal0) && !(unit.UniqueName == this.sVal0))
        return false;
      return this.iVal0 < 0 || this.iVal0 == turn;
    }

    public void OnUnitStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitEnd(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitEnd && this.CompareUnit(unit, turn));
    }

    public void OnUnitMoveStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitMoveStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitSkillStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitSkillStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitSkillEnd(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitSkillEnd && this.CompareUnit(unit, turn));
    }

    public void OnTargetChange(Unit target, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.TargetChange && this.CompareUnit(target, turn));
    }

    public void OnHotTargetsChange(Unit unit, Unit target, int turn)
    {
      if (this.TriggerType != FlowNode_TutorialTrigger.TriggerTypes.HotTargetsChange || UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      TacticsUnitController unitController = SceneBattle.Instance.FindUnitController(target);
      FlowNode_TutorialMask[] components = ((Component) this).GetComponents<FlowNode_TutorialMask>();
      for (int index = 0; index < components.Length; ++index)
      {
        this.SetupMask_AttackTargetDesc(unitController, components[index]);
        if (components[index].ComponentId == FlowNode_TutorialMask.eComponentId.ATTACK_TARGET_DESC)
          this.TriggerIf(this.CompareUnit(unit, turn));
        this.SetupMask_NormalAttackDesc(components[index]);
        this.SetupMask_AbilityDesc(components[index]);
        this.SetupMask_TapNormalAttack(components[index]);
        this.SetupMask_ExecNormalAttack(components[index]);
      }
    }

    public void OnSelectDirectionStart(Unit unit, int turn)
    {
      if (this.TriggerType != FlowNode_TutorialTrigger.TriggerTypes.UnitSelectDirection)
        return;
      TacticsUnitController unitController = SceneBattle.Instance.FindUnitController(unit);
      foreach (FlowNode_TutorialMask component in ((Component) this).GetComponents<FlowNode_TutorialMask>())
        this.SetupMask_SelectDir(unitController, component);
      this.TriggerIf(this.CompareUnit(unit, turn));
    }

    public void OnMapStart()
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.MapStart);
    }

    public void OnMapEnd()
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.MapEnd);
    }

    public void OnFinishCameraUnitFocus(Unit unit, int turn)
    {
      if (this.TriggerType != FlowNode_TutorialTrigger.TriggerTypes.FinishCameraUnitFocus || UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      TacticsUnitController unitController = SceneBattle.Instance.FindUnitController(unit);
      FlowNode_TutorialMask[] components = ((Component) this).GetComponents<FlowNode_TutorialMask>();
      for (int index = 0; index < components.Length; ++index)
      {
        if (components[index].ComponentId == FlowNode_TutorialMask.eComponentId.MOVE_UNIT)
          this.SetupMask_MoveUnit(unitController, components[index]);
      }
      this.TriggerIf(this.CompareUnit(unit, turn));
    }

    public void OnCloseBattleInfo() => SceneBattle.Instance.SelectUnitDir(EUnitDirection.PositiveX);

    public void OnSkipBattleExplain()
    {
      FlowNode_TutorialTrigger[] components = ((Component) this).GetComponents<FlowNode_TutorialTrigger>();
      for (int index = 0; index < components.Length; ++index)
      {
        if (!components[index].DontSkipFlag)
          components[index].TriggerType = FlowNode_TutorialTrigger.TriggerTypes.None;
      }
    }

    private void TriggerIf(bool b)
    {
      if (!b)
        return;
      this.Trigger();
    }

    private void Trigger()
    {
      if (!((Behaviour) this).enabled)
        return;
      this.ActivateOutputLinks(10);
      ((Behaviour) this).enabled = (this.Flags & FlowNode_TutorialTrigger.ActivateFlags.AutoDeactivate) == (FlowNode_TutorialTrigger.ActivateFlags) 0;
    }

    protected override void Awake()
    {
      base.Awake();
      ((Behaviour) this).enabled = (this.Flags & FlowNode_TutorialTrigger.ActivateFlags.AutoActivate) != (FlowNode_TutorialTrigger.ActivateFlags) 0;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          ((Behaviour) this).enabled = true;
          break;
        case 1:
          ((Behaviour) this).enabled = false;
          break;
        case 2:
          this.ForceExec();
          break;
      }
    }

    private void ForceExec()
    {
      switch (this.TriggerType)
      {
        case FlowNode_TutorialTrigger.TriggerTypes.CloseBattleInfo:
          this.OnCloseBattleInfo();
          break;
        case FlowNode_TutorialTrigger.TriggerTypes.SkipBattleExplain:
          this.OnSkipBattleExplain();
          break;
      }
    }

    private void SetupMask_MoveUnit(
      TacticsUnitController controller,
      FlowNode_TutorialMask flownode_mask)
    {
      FlowNode_TutorialMask[] components = ((Component) this).GetComponents<FlowNode_TutorialMask>();
      for (int index1 = 0; index1 < components.Length; ++index1)
      {
        if (components[index1].ComponentId == FlowNode_TutorialMask.eComponentId.MOVE_UNIT && UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) components[index1], (UnityEngine.Object) null))
        {
          Vector3 world_pos;
          // ISSUE: explicit constructor call
          ((Vector3) ref world_pos).\u002Ector(controller.CenterPosition.x + (float) this.iVal1, controller.CenterPosition.y - controller.Height / 2f, controller.CenterPosition.z);
          components[index1].Setup(TutorialMask.eActionType.MOVE_UNIT, world_pos, true);
          Vector3[] vector3Array = new Vector3[4]
          {
            new Vector3(world_pos.x - 0.5f, world_pos.y, world_pos.z - 0.5f),
            new Vector3(world_pos.x - 0.5f, world_pos.y, world_pos.z + 0.5f),
            new Vector3(world_pos.x + 0.5f, world_pos.y, world_pos.z + 0.5f),
            new Vector3(world_pos.x + 0.5f, world_pos.y, world_pos.z - 0.5f)
          };
          float num1 = 99999f;
          float num2 = 99999f;
          float num3 = -99999f;
          float num4 = -99999f;
          for (int index2 = 0; index2 < vector3Array.Length; ++index2)
          {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, vector3Array[index2]);
            if ((double) num1 >= (double) screenPoint.x)
              num1 = screenPoint.x;
            if ((double) num2 >= (double) screenPoint.y)
              num2 = screenPoint.y;
            if ((double) num3 <= (double) screenPoint.x)
              num3 = screenPoint.x;
            if ((double) num4 <= (double) screenPoint.y)
              num4 = screenPoint.y;
          }
          float width = num3 - num1;
          float height = num4 - num2;
          components[index1].SetupMaskSize(width, height);
        }
      }
    }

    private void SetupMask_AttackTargetDesc(
      TacticsUnitController controller,
      FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.ATTACK_TARGET_DESC || !UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) flownode_mask, (UnityEngine.Object) null))
        return;
      Vector3 world_pos;
      // ISSUE: explicit constructor call
      ((Vector3) ref world_pos).\u002Ector(controller.CenterPosition.x + (float) this.iVal1, controller.CenterPosition.y, controller.CenterPosition.z);
      flownode_mask.Setup(TutorialMask.eActionType.ATTACK_TARGET_DESC, world_pos, true);
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(controller.CenterPosition.x + (float) this.iVal1, controller.CenterPosition.y - controller.Height / 2f, controller.CenterPosition.z);
      Vector3[] vector3Array = new Vector3[5]
      {
        new Vector3(vector3.x - 0.5f, vector3.y, vector3.z - 0.5f),
        new Vector3(vector3.x - 0.5f, vector3.y, vector3.z + 0.5f),
        new Vector3(vector3.x + 0.5f, vector3.y, vector3.z + 0.5f),
        new Vector3(vector3.x + 0.5f, vector3.y, vector3.z - 0.5f),
        new Vector3(vector3.x, vector3.y + controller.Height, vector3.z)
      };
      float num1 = 99999f;
      float num2 = 99999f;
      float num3 = -99999f;
      float num4 = -99999f;
      for (int index = 0; index < vector3Array.Length; ++index)
      {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, vector3Array[index]);
        if ((double) num1 >= (double) screenPoint.x)
          num1 = screenPoint.x;
        if ((double) num2 >= (double) screenPoint.y)
          num2 = screenPoint.y;
        if ((double) num3 <= (double) screenPoint.x)
          num3 = screenPoint.x;
        if ((double) num4 <= (double) screenPoint.y)
          num4 = screenPoint.y;
      }
      float width = num3 - num1;
      float num5 = num4 - num2;
      flownode_mask.SetupMaskSize(width, num5 * 1.5f);
    }

    private void SetupMask_NormalAttackDesc(FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.NORMAL_ATTACK_DESC || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow.AttackButton, (UnityEngine.Object) null))
        return;
      RectTransform transform = SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.transform as RectTransform;
      Vector2 vector2;
      ref Vector2 local = ref vector2;
      Rect rect1 = transform.rect;
      double num1 = (double) ((Rect) ref rect1).width * (double) ((Transform) transform).lossyScale.x;
      Rect rect2 = transform.rect;
      double num2 = (double) ((Rect) ref rect2).height * (double) ((Transform) transform).lossyScale.y;
      // ISSUE: explicit constructor call
      ((Vector2) ref local).\u002Ector((float) num1, (float) num2);
      string text = LocalizedText.Get("sys.TUTORIAL_NORMAL_ATTACK");
      flownode_mask.Setup(TutorialMask.eActionType.NORMAL_ATTACK_DESC, ((Transform) transform).position, false, text);
      flownode_mask.SetupMaskSize(vector2.x, vector2.y);
    }

    private void SetupMask_AbilityDesc(FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.ABILITY_DESC || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow, (UnityEngine.Object) null) || SceneBattle.Instance.BattleUI.CommandWindow.AbilityButtons == null)
        return;
      List<GameObject> abilityButtons = SceneBattle.Instance.BattleUI.CommandWindow.AbilityButtons;
      float num1 = 99999f;
      float num2 = 99999f;
      float num3 = -99999f;
      float num4 = -99999f;
      for (int index = 0; index < abilityButtons.Count; ++index)
      {
        RectTransform transform = abilityButtons[index].transform as RectTransform;
        double x1 = (double) ((Transform) transform).position.x;
        Rect rect1 = transform.rect;
        double num5 = (double) ((Rect) ref rect1).width / 2.0 * (double) ((Transform) transform).lossyScale.x;
        float num6 = (float) (x1 - num5);
        double y1 = (double) ((Transform) transform).position.y;
        Rect rect2 = transform.rect;
        double num7 = (double) ((Rect) ref rect2).height / 2.0 * (double) ((Transform) transform).lossyScale.y;
        float num8 = (float) (y1 - num7);
        double x2 = (double) ((Transform) transform).position.x;
        Rect rect3 = transform.rect;
        double num9 = (double) ((Rect) ref rect3).width / 2.0 * (double) ((Transform) transform).lossyScale.x;
        float num10 = (float) (x2 + num9);
        double y2 = (double) ((Transform) transform).position.y;
        Rect rect4 = transform.rect;
        double num11 = (double) ((Rect) ref rect4).height / 2.0 * (double) ((Transform) transform).lossyScale.y;
        float num12 = (float) (y2 + num11);
        if ((double) num1 >= (double) num6)
          num1 = num6;
        if ((double) num2 >= (double) num8)
          num2 = num8;
        if ((double) num3 <= (double) num10)
          num3 = num10;
        if ((double) num4 <= (double) num12)
          num4 = num12;
      }
      float width = num3 - num1;
      float height = num4 - num2;
      Vector3 world_pos;
      // ISSUE: explicit constructor call
      ((Vector3) ref world_pos).\u002Ector(num1 + width / 2f, num2 + height / 2f, 0.0f);
      string text = LocalizedText.Get("sys.TUTORIAL_ABILITY");
      flownode_mask.Setup(TutorialMask.eActionType.ABILITY_DESC, world_pos, false, text);
      flownode_mask.SetupMaskSize(width, height);
    }

    private void SetupMask_TapNormalAttack(FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.TAP_NORMAL_ATTACK || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow.AttackButton, (UnityEngine.Object) null))
        return;
      RectTransform transform = SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.transform as RectTransform;
      Vector2 vector2;
      ref Vector2 local = ref vector2;
      Rect rect1 = transform.rect;
      double num1 = (double) ((Rect) ref rect1).width * (double) ((Transform) transform).lossyScale.x;
      Rect rect2 = transform.rect;
      double num2 = (double) ((Rect) ref rect2).height * (double) ((Transform) transform).lossyScale.y;
      // ISSUE: explicit constructor call
      ((Vector2) ref local).\u002Ector((float) num1, (float) num2);
      flownode_mask.Setup(TutorialMask.eActionType.TAP_NORMAL_ATTACK, ((Transform) transform).position, false);
      flownode_mask.SetupMaskSize(vector2.x, vector2.y);
    }

    private void SetupMask_ExecNormalAttack(FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.EXEC_NORMAL_ATTACK || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance.BattleUI.CommandWindow.OKButton, (UnityEngine.Object) null))
        return;
      RectTransform transform = SceneBattle.Instance.BattleUI.CommandWindow.OKButton.transform as RectTransform;
      Vector2 vector2;
      ref Vector2 local = ref vector2;
      Rect rect1 = transform.rect;
      double num1 = (double) ((Rect) ref rect1).width * (double) ((Transform) transform).lossyScale.x;
      Rect rect2 = transform.rect;
      double num2 = (double) ((Rect) ref rect2).height * (double) ((Transform) transform).lossyScale.y;
      // ISSUE: explicit constructor call
      ((Vector2) ref local).\u002Ector((float) num1, (float) num2);
      flownode_mask.Setup(TutorialMask.eActionType.EXEC_NORMAL_ATTACK, ((Transform) transform).position, false);
      flownode_mask.SetupMaskSize(vector2.x, vector2.y);
    }

    private void SetupMask_SelectDir(
      TacticsUnitController controller,
      FlowNode_TutorialMask flownode_mask)
    {
      if (flownode_mask.ComponentId != FlowNode_TutorialMask.eComponentId.SELECT_DIR)
        return;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(controller.CenterPosition.x + (float) this.iVal1, controller.CenterPosition.y - controller.Height / 2f, controller.CenterPosition.z);
      Vector3[] vector3Array = new Vector3[4]
      {
        new Vector3(vector3.x - 0.5f, vector3.y, vector3.z - 0.5f),
        new Vector3(vector3.x - 0.5f, vector3.y, vector3.z + 0.5f),
        new Vector3(vector3.x + 0.5f, vector3.y, vector3.z + 0.5f),
        new Vector3(vector3.x + 0.5f, vector3.y, vector3.z - 0.5f)
      };
      float num1 = 99999f;
      float num2 = 99999f;
      float num3 = -99999f;
      float num4 = -99999f;
      for (int index = 0; index < vector3Array.Length; ++index)
      {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, vector3Array[index]);
        if ((double) num1 >= (double) screenPoint.x)
          num1 = screenPoint.x;
        if ((double) num2 >= (double) screenPoint.y)
          num2 = screenPoint.y;
        if ((double) num3 <= (double) screenPoint.x)
          num3 = screenPoint.x;
        if ((double) num4 <= (double) screenPoint.y)
          num4 = screenPoint.y;
      }
      float width = num3 - num1;
      float height = num4 - num2;
      DirectionArrow[] objectsOfType = UnityEngine.Object.FindObjectsOfType<DirectionArrow>();
      for (int index = 0; index < objectsOfType.Length; ++index)
      {
        if (objectsOfType[index].Direction == EUnitDirection.PositiveX)
        {
          Vector3 position = ((Component) objectsOfType[index]).transform.position;
          flownode_mask.Setup(TutorialMask.eActionType.SELECT_DIR, position, true);
          flownode_mask.SetupMaskSize(width, height);
          break;
        }
      }
    }

    [System.Flags]
    public enum ActivateFlags
    {
      AutoActivate = 1,
      AutoDeactivate = 2,
    }

    public enum TriggerTypes
    {
      None = 0,
      UnitStart = 10, // 0x0000000A
      UnitSkillStart = 11, // 0x0000000B
      UnitMoveStart = 12, // 0x0000000C
      UnitSkillEnd = 13, // 0x0000000D
      UnitEnd = 14, // 0x0000000E
      TargetChange = 15, // 0x0000000F
      HotTargetsChange = 16, // 0x00000010
      UnitSelectDirection = 17, // 0x00000011
      MapStart = 18, // 0x00000012
      FinishCameraUnitFocus = 19, // 0x00000013
      CloseBattleInfo = 20, // 0x00000014
      MapEnd = 21, // 0x00000015
      SkipBattleExplain = 100, // 0x00000064
    }
  }
}
