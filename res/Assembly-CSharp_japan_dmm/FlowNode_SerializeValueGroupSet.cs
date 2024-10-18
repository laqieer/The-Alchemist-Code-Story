// Decompiled with JetBrains decompiler
// Type: FlowNode_SerializeValueGroupSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
[FlowNode.NodeType("SerializeValue/GroupSet", 32741)]
[FlowNode.Pin(100, "設定", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(110, "次へ", FlowNode.PinTypes.Output, 110)]
public class FlowNode_SerializeValueGroupSet : FlowNode
{
  public const int INPUT_SET = 100;
  public const int OUTPUT_SETED = 110;
  public FlowNode_SerializeValueGroupSet.ValueType m_Type;
  public SerializeValueBehaviour m_Object;
  public List<FlowNode_SerializeValueGroupSet.Value> m_Values = new List<FlowNode_SerializeValueGroupSet.Value>();

  protected override void Awake() => base.Awake();

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    if (this.m_Type == FlowNode_SerializeValueGroupSet.ValueType.RefObject)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Object, (UnityEngine.Object) null))
      {
        for (int index1 = 0; index1 < this.m_Values.Count; ++index1)
        {
          SerializeValue[] fields = this.m_Object.list.GetFields(this.m_Values[index1].m_Group);
          if (fields != null)
          {
            for (int index2 = 0; index2 < fields.Length; ++index2)
              fields[index2]?.Write(this.m_Values[index1].m_PropertyType, this.m_Values[index1].m_Value);
          }
        }
      }
    }
    else if (this.m_Type == FlowNode_SerializeValueGroupSet.ValueType.ButtonEventArg && FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
    {
      for (int index3 = 0; index3 < this.m_Values.Count; ++index3)
      {
        SerializeValue[] fields = currentValue.GetFields(this.m_Values[index3].m_Group);
        if (fields != null)
        {
          for (int index4 = 0; index4 < fields.Length; ++index4)
            fields[index4]?.Write(this.m_Values[index3].m_PropertyType, this.m_Values[index3].m_Value);
        }
      }
    }
    this.ActivateOutputLinks(110);
  }

  public enum ValueType
  {
    RefObject,
    ButtonEventArg,
  }

  [Serializable]
  public class Value
  {
    public int m_Group;
    public SerializeValue.PropertyType m_PropertyType;
    public SerializeValue m_Value = new SerializeValue();
  }
}
