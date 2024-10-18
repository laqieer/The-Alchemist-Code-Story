// Decompiled with JetBrains decompiler
// Type: FlowNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

#nullable disable
[AddComponentMenu("")]
public abstract class FlowNode : MonoBehaviour
{
  public const int DefaultNodeColor = 32741;
  public const int ListenerNodeColor = 58751;
  public const int NativeNodeColor = 16750080;
  [HideInInspector]
  public Vector2 Position;
  [HideInInspector]
  public FlowNode.Link[] OutputLinks = new FlowNode.Link[0];

  public virtual void OnActivate(int pinID)
  {
  }

  public FlowNode.Pin[] Pins
  {
    get
    {
      FlowNode.Pin[] customAttributes = (FlowNode.Pin[]) ((object) this).GetType().GetCustomAttributes(typeof (FlowNode.Pin), true);
      FlowNode.Pin[] dynamicPins = this.GetDynamicPins();
      if (dynamicPins.Length > 0)
      {
        int length = customAttributes.Length;
        Array.Resize<FlowNode.Pin>(ref customAttributes, customAttributes.Length + dynamicPins.Length);
        for (int index = 0; index < dynamicPins.Length; ++index)
          customAttributes[length + index] = dynamicPins[index];
      }
      for (int index1 = 0; index1 < customAttributes.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < customAttributes.Length; ++index2)
        {
          if (customAttributes[index1].Priority > customAttributes[index2].Priority)
          {
            FlowNode.Pin pin = customAttributes[index1];
            customAttributes[index1] = customAttributes[index2];
            customAttributes[index2] = pin;
          }
        }
      }
      return customAttributes;
    }
  }

  public virtual string[] GetInfoLines()
  {
    FieldInfo[] fields = ((object) this).GetType().GetFields();
    List<string> stringList = new List<string>();
    for (int index = 0; index < fields.Length; ++index)
    {
      FlowNode.ShowInInfo[] customAttributes = (FlowNode.ShowInInfo[]) fields[index].GetCustomAttributes(typeof (FlowNode.ShowInInfo), false);
      if (customAttributes.Length > 0)
      {
        object obj = fields[index].GetValue((object) this);
        if (obj != null)
        {
          string str = !obj.GetType().IsSubclassOf(typeof (Object)) || !Object.op_Inequality((Object) obj, (Object) null) ? obj.ToString() : ((Object) obj).name;
          if (customAttributes[0].ShowType && obj != null)
            str = str + "(" + obj.GetType().Name + ")";
          if (str != "null")
            stringList.Add(fields[index].Name + " is " + str);
        }
      }
    }
    return stringList.ToArray();
  }

  protected virtual void Awake() => ((Behaviour) this).enabled = false;

  protected virtual void OnDestroy()
  {
  }

  public virtual FlowNode.Pin[] GetDynamicPins() => new FlowNode.Pin[0];

  public void Activate(int pinID) => this.OnActivate(pinID);

  public int ActivateOutputLinks(int pinID)
  {
    int num = 0;
    for (int index = 0; index < this.OutputLinks.Length; ++index)
    {
      if (this.OutputLinks[index].SrcPinID == pinID && !Object.op_Equality((Object) this.OutputLinks[index].Dest, (Object) null))
      {
        this.OutputLinks[index].Dest.Activate(this.OutputLinks[index].DestPinID);
        ++num;
      }
    }
    return num;
  }

  protected bool IsParentOf(GameObject go)
  {
    return !Object.op_Equality((Object) go, (Object) null) && go.transform.IsChildOf(((Component) this).transform);
  }

  public void RemoveOutputLink(int index)
  {
    List<FlowNode.Link> linkList = new List<FlowNode.Link>((IEnumerable<FlowNode.Link>) this.OutputLinks);
    linkList.RemoveAt(index);
    this.OutputLinks = linkList.ToArray();
  }

  public int FindPin(int pinID)
  {
    FlowNode.Pin[] pins = this.Pins;
    for (int pin = 0; pin < pins.Length; ++pin)
    {
      if (pins[pin].PinID == pinID)
        return pin;
    }
    return -1;
  }

  public void LinkPin(FlowNode.Pin srcPin, FlowNode dest, FlowNode.Pin destPin)
  {
    if (srcPin.PinType == destPin.PinType)
      return;
    FlowNode flowNode1;
    FlowNode flowNode2;
    if (srcPin.PinType == FlowNode.PinTypes.Output)
    {
      flowNode1 = this;
      flowNode2 = dest;
    }
    else
    {
      flowNode1 = dest;
      flowNode2 = this;
      FlowNode.Pin pin = srcPin;
      srcPin = destPin;
      destPin = pin;
    }
    for (int index = 0; index < this.OutputLinks.Length; ++index)
    {
      if (flowNode1.OutputLinks[index].SrcPinID == srcPin.PinID && Object.op_Equality((Object) flowNode1.OutputLinks[index].Dest, (Object) flowNode2) && flowNode1.OutputLinks[index].DestPinID == destPin.PinID)
        return;
    }
    Array.Resize<FlowNode.Link>(ref flowNode1.OutputLinks, flowNode1.OutputLinks.Length + 1);
    flowNode1.OutputLinks[flowNode1.OutputLinks.Length - 1] = new FlowNode.Link()
    {
      SrcPinID = srcPin.PinID,
      Dest = flowNode2,
      DestPinID = destPin.PinID
    };
  }

  public Color GetNodeColor()
  {
    FlowNode.NodeType[] customAttributes = (FlowNode.NodeType[]) ((object) this).GetType().GetCustomAttributes(typeof (FlowNode.NodeType), true);
    return customAttributes.Length > 0 ? customAttributes[0].Color : Color.gray;
  }

  public virtual string GetCaption()
  {
    string caption = ((object) this).GetType().Name;
    if (caption.StartsWith("FlowNode_"))
      caption = caption.Substring("FlowNode_".Length);
    return caption;
  }

  public virtual bool OnDragUpdate(object[] objectReferences) => false;

  public virtual bool OnDragPerform(object[] objectReferences) => false;

  public enum PinTypes
  {
    Input,
    Output,
  }

  public class NodeType : Attribute
  {
    public string Name;
    public Color Color;

    public NodeType(string name)
    {
      this.Name = name;
      this.Color = Color.cyan;
    }

    public NodeType(string name, int color)
    {
      this.Name = name;
      this.Color = Color32.op_Implicit(new Color32((byte) (color >> 16 & (int) byte.MaxValue), (byte) (color >> 8 & (int) byte.MaxValue), (byte) (color & (int) byte.MaxValue), byte.MaxValue));
    }
  }

  [Conditional("UNITY_EDITOR")]
  public class NodeUsage : Attribute
  {
    public string Text;

    public NodeUsage(string text) => this.Text = text;
  }

  public class ShowInInspector : Attribute
  {
  }

  public class ShowInInfo : PropertyAttribute
  {
    public bool ShowType;

    public ShowInInfo()
    {
    }

    public ShowInInfo(bool withType) => this.ShowType = withType;
  }

  public class FlowDefinitionSearchKeyAttribute : Attribute
  {
    public System.Type ObjType;

    public FlowDefinitionSearchKeyAttribute(System.Type type) => this.ObjType = type;
  }

  public class DropTarget : PropertyAttribute
  {
    public System.Type AcceptType;
    public bool OnlyChildren;

    public DropTarget(System.Type type, bool onlyChildren)
    {
      this.AcceptType = type;
      this.OnlyChildren = onlyChildren;
    }
  }

  [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
  public class Pin : Attribute
  {
    public int PinID;
    public string Name;
    public Color Color = Color.black;
    public FlowNode.PinTypes PinType;
    public int Priority;

    public Pin(int id, string name, FlowNode.PinTypes type, int priority = 0)
    {
      this.Name = name;
      this.PinID = id;
      this.PinType = type;
      this.Priority = priority;
    }
  }

  [Serializable]
  public struct Link
  {
    public FlowNode Dest;
    public int SrcPinID;
    public int DestPinID;
  }
}
