// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Common/GUIEX", 32741)]
[FlowNode.Pin(200, "Opened", FlowNode.PinTypes.Output, 200)]
[FlowNode.Pin(210, "Closed", FlowNode.PinTypes.Output, 210)]
public class FlowNode_GUIEx : FlowNode_GUI
{
  public bool m_ButtonEventArg;
  public FlowNode_GUIEx.ValueType m_Type;
  public SerializeValueBehaviour m_Object;
  public SerializeValueList m_ValueList = new SerializeValueList();

  protected override void OnInstanceCreate()
  {
    base.OnInstanceCreate();
    if (!Object.op_Inequality((Object) this.mInstance, (Object) null))
      return;
    SerializeValueBehaviour serializeValueBehaviour = this.mInstance.GetComponent<SerializeValueBehaviour>();
    if (Object.op_Equality((Object) serializeValueBehaviour, (Object) null))
    {
      for (int index = 0; index < this.mInstance.transform.childCount; ++index)
      {
        Transform child = this.mInstance.transform.GetChild(index);
        if (Object.op_Inequality((Object) child, (Object) null))
        {
          serializeValueBehaviour = ((Component) child).GetComponent<SerializeValueBehaviour>();
          if (Object.op_Inequality((Object) serializeValueBehaviour, (Object) null))
            break;
        }
      }
      if (Object.op_Equality((Object) serializeValueBehaviour, (Object) null))
        serializeValueBehaviour = this.mInstance.GetComponentInChildren<SerializeValueBehaviour>();
    }
    if (!Object.op_Inequality((Object) serializeValueBehaviour, (Object) null))
      return;
    SerializeValueList src = (SerializeValueList) null;
    if (this.m_Type == FlowNode_GUIEx.ValueType.Direct)
      src = this.m_ValueList;
    else if (this.m_Type == FlowNode_GUIEx.ValueType.RefObject && Object.op_Inequality((Object) this.m_Object, (Object) null))
      src = this.m_Object.list;
    if (src == null)
      return;
    if (this.m_ButtonEventArg && FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
      src.Write(currentValue);
    serializeValueBehaviour.list.Write(src);
  }

  public enum ValueType
  {
    Direct,
    RefObject,
  }
}
