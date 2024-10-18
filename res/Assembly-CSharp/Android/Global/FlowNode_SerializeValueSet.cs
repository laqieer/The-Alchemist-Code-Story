// Decompiled with JetBrains decompiler
// Type: FlowNode_SerializeValueSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.Pin(100, "設定", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(110, "次へ", FlowNode.PinTypes.Output, 110)]
[FlowNode.NodeType("SerializeValue/Set", 32741)]
public class FlowNode_SerializeValueSet : FlowNode
{
  public List<FlowNode_SerializeValueSet.Value> m_Values = new List<FlowNode_SerializeValueSet.Value>();
  public const int INPUT_SET = 100;
  public const int OUTPUT_SETED = 110;
  public FlowNode_SerializeValueSet.ValueType m_Type;
  public SerializeValueBehaviour m_Object;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    if (this.m_Type == FlowNode_SerializeValueSet.ValueType.Global)
    {
      for (int index = 0; index < this.m_Values.Count; ++index)
        new SerializeValue(SerializeValue.Type.Global, (string) null, this.m_Values[index].m_Key)?.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
    }
    else if (this.m_Type == FlowNode_SerializeValueSet.ValueType.RefObject)
    {
      if ((UnityEngine.Object) this.m_Object != (UnityEngine.Object) null)
      {
        for (int index = 0; index < this.m_Values.Count; ++index)
          this.m_Object.list.GetField(this.m_Values[index].m_Key)?.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
      }
    }
    else if (this.m_Type == FlowNode_SerializeValueSet.ValueType.ButtonEventArg)
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
      {
        for (int index = 0; index < this.m_Values.Count; ++index)
          currentValue.GetField(this.m_Values[index].m_Key)?.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
      }
    }
    this.ActivateOutputLinks(110);
  }

  public enum ValueType
  {
    RefObject,
    Global,
    ButtonEventArg,
  }

  [Serializable]
  public class Value
  {
    public string m_Key = string.Empty;
    public SerializeValue m_Value = new SerializeValue();
    public SerializeValue.PropertyType m_PropertyType;
  }
}
