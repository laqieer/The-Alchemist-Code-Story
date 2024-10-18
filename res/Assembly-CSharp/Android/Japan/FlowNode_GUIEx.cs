// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Common/GUIEX", 32741)]
[FlowNode.Pin(200, "Opened", FlowNode.PinTypes.Output, 200)]
[FlowNode.Pin(210, "Closed", FlowNode.PinTypes.Output, 210)]
public class FlowNode_GUIEx : FlowNode_GUI
{
  public SerializeValueList m_ValueList = new SerializeValueList();
  public bool m_ButtonEventArg;
  public FlowNode_GUIEx.ValueType m_Type;
  public SerializeValueBehaviour m_Object;

  protected override void OnInstanceCreate()
  {
    base.OnInstanceCreate();
    if (!((UnityEngine.Object) this.mInstance != (UnityEngine.Object) null))
      return;
    SerializeValueBehaviour serializeValueBehaviour = this.mInstance.GetComponent<SerializeValueBehaviour>();
    if ((UnityEngine.Object) serializeValueBehaviour == (UnityEngine.Object) null)
    {
      for (int index = 0; index < this.mInstance.transform.childCount; ++index)
      {
        Transform child = this.mInstance.transform.GetChild(index);
        if ((UnityEngine.Object) child != (UnityEngine.Object) null)
        {
          serializeValueBehaviour = child.GetComponent<SerializeValueBehaviour>();
          if ((UnityEngine.Object) serializeValueBehaviour != (UnityEngine.Object) null)
            break;
        }
      }
      if ((UnityEngine.Object) serializeValueBehaviour == (UnityEngine.Object) null)
        serializeValueBehaviour = this.mInstance.GetComponentInChildren<SerializeValueBehaviour>();
    }
    if (!((UnityEngine.Object) serializeValueBehaviour != (UnityEngine.Object) null))
      return;
    SerializeValueList src = (SerializeValueList) null;
    if (this.m_Type == FlowNode_GUIEx.ValueType.Direct)
      src = this.m_ValueList;
    else if (this.m_Type == FlowNode_GUIEx.ValueType.RefObject && (UnityEngine.Object) this.m_Object != (UnityEngine.Object) null)
      src = this.m_Object.list;
    if (src == null)
      return;
    if (this.m_ButtonEventArg)
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        src.Write(currentValue);
    }
    serializeValueBehaviour.list.Write(src);
  }

  public enum ValueType
  {
    Direct,
    RefObject,
  }
}
