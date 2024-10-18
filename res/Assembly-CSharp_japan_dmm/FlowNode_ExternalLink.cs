// Decompiled with JetBrains decompiler
// Type: FlowNode_ExternalLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("ExternalLink", 32741)]
public class FlowNode_ExternalLink : FlowNode
{
  public GameObject Target;
  protected GameObject mInstance;
  public string InstanceName;
  public bool NoAutoDestruct;
  public bool SetSibling;
  public Transform Parent;
  [HideInInspector]
  [SerializeField]
  private FlowNode_ExternalLink.PinData[] mPins = new FlowNode_ExternalLink.PinData[0];

  public GameObject Instance => this.mInstance;

  protected virtual bool ShouldCreateInstanceOnStart => true;

  protected override void Awake()
  {
    base.Awake();
    if (!this.ShouldCreateInstanceOnStart)
      return;
    ((Behaviour) this).enabled = true;
  }

  protected void CreateInstance()
  {
    ((Behaviour) this).enabled = false;
    this.DestroyInstance();
    if (Object.op_Equality((Object) this.Target, (Object) null))
      return;
    this.mInstance = Object.Instantiate<GameObject>(this.Target);
    if (this.SetSibling && Object.op_Inequality((Object) ((Component) this).transform.parent, (Object) null))
      this.mInstance.transform.SetParent(((Component) this).transform.parent, false);
    else if (Object.op_Inequality((Object) this.Parent, (Object) null))
    {
      this.mInstance.transform.SetParent(this.Parent, false);
    }
    else
    {
      Component[] componentsInChildren = (Component[]) this.mInstance.GetComponentsInChildren<SetCanvasBounds>();
      if (componentsInChildren != null && componentsInChildren.Length > 0)
        this.mInstance.transform.SetParent(((Component) this).transform, false);
      else if (Object.op_Inequality((Object) ((Component) ((Component) this).transform).GetComponent<SetCanvasBounds>(), (Object) null))
      {
        foreach (Transform transform in ((Component) this).transform)
        {
          if (((Object) ((Component) transform).transform).name == "CanvasBoundsPanel")
          {
            this.mInstance.transform.SetParent(transform, false);
            break;
          }
        }
        if (Object.op_Equality((Object) this.mInstance.transform.parent, (Object) null))
          this.mInstance.transform.SetParent(((Component) this).transform, false);
      }
      else
        this.mInstance.transform.SetParent(((Component) this).transform, false);
    }
    RectTransform component1 = this.Target.GetComponent<RectTransform>();
    if (Object.op_Inequality((Object) component1, (Object) null) && Object.op_Inequality((Object) this.Target.GetComponent<Canvas>(), (Object) null))
    {
      RectTransform component2 = this.mInstance.GetComponent<RectTransform>();
      component2.anchorMax = component1.anchorMax;
      component2.anchorMin = component1.anchorMin;
      component2.anchoredPosition = component1.anchoredPosition;
      component2.sizeDelta = component1.sizeDelta;
    }
    if (!string.IsNullOrEmpty(this.InstanceName))
      ((Object) this.mInstance).name = this.InstanceName;
    this.BindPins();
    this.OnInstanceCreate();
  }

  protected void BindPins()
  {
    FlowNode[] components = this.mInstance.GetComponents<FlowNode>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] is FlowNode_Output)
      {
        FlowNode_Output flowNodeOutput = components[index] as FlowNode_Output;
        int embeddedPin = this.FindEmbeddedPin(flowNodeOutput.PinName);
        if (embeddedPin >= 0 && this.mPins[embeddedPin].PinType == FlowNode.PinTypes.Output)
        {
          this.mPins[embeddedPin].NodeBinding = components[index];
          flowNodeOutput.TargetNode = this;
          flowNodeOutput.TargetPinID = this.mPins[embeddedPin].PinID;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        int embeddedPin = this.FindEmbeddedPin((components[index] as FlowNode_Input).PinName);
        if (embeddedPin >= 0 && this.mPins[embeddedPin].PinType == FlowNode.PinTypes.Input)
          this.mPins[embeddedPin].NodeBinding = components[index];
      }
    }
  }

  protected void DestroyInstance()
  {
    if (Object.op_Equality((Object) this.mInstance, (Object) null))
      return;
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.mPins[index].NodeBinding, (Object) null))
      {
        if (this.mPins[index].PinType == FlowNode.PinTypes.Output)
        {
          FlowNode_Output nodeBinding = this.mPins[index].NodeBinding as FlowNode_Output;
          nodeBinding.TargetNode = (FlowNode_ExternalLink) null;
          nodeBinding.TargetPinID = -1;
        }
        this.mPins[index].NodeBinding = (FlowNode) null;
      }
    }
    Object.Destroy((Object) this.mInstance);
    this.mInstance = (GameObject) null;
    this.OnInstanceDestroy();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (this.NoAutoDestruct)
      return;
    this.DestroyInstance();
  }

  public override FlowNode.Pin[] GetDynamicPins()
  {
    FlowNode.Pin[] dynamicPins = new FlowNode.Pin[this.mPins.Length];
    for (int index = 0; index < this.mPins.Length; ++index)
      dynamicPins[index] = new FlowNode.Pin(this.mPins[index].PinID, this.mPins[index].PinName, this.mPins[index].PinType, 1000 + index);
    return dynamicPins;
  }

  protected virtual void Start() => this.CreateInstance();

  protected virtual void OnInstanceCreate()
  {
  }

  protected virtual void OnInstanceDestroy()
  {
  }

  public override void OnActivate(int pinID)
  {
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if (this.mPins[index].PinID == pinID && this.mPins[index].PinType == FlowNode.PinTypes.Input && Object.op_Inequality((Object) this.mPins[index].NodeBinding, (Object) null))
        this.mPins[index].NodeBinding.ActivateOutputLinks(1);
    }
  }

  public void RefreshPins()
  {
    if (Object.op_Equality((Object) this.Target, (Object) null))
      return;
    FlowNode[] components = this.Target.GetComponents<FlowNode>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] is FlowNode_Output)
      {
        FlowNode_Output flowNodeOutput = components[index] as FlowNode_Output;
        if (this.FindEmbeddedPin(flowNodeOutput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID();
          this.mPins[this.mPins.Length - 1].PinName = flowNodeOutput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Output;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        FlowNode_Input flowNodeInput = components[index] as FlowNode_Input;
        if (this.FindEmbeddedPin(flowNodeInput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID();
          this.mPins[this.mPins.Length - 1].PinName = flowNodeInput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Input;
        }
      }
    }
  }

  private int GenerateUniquePinID()
  {
    FlowNode.Pin[] pins = this.Pins;
    int uniquePinId;
    do
    {
      uniquePinId = Random.Range(1, (int) ushort.MaxValue);
      for (int index = 0; index < pins.Length; ++index)
      {
        if (pins[index].PinID == uniquePinId)
        {
          uniquePinId = -1;
          break;
        }
      }
    }
    while (uniquePinId < 0);
    return uniquePinId;
  }

  private int FindEmbeddedPin(string pinName)
  {
    for (int embeddedPin = 0; embeddedPin < this.mPins.Length; ++embeddedPin)
    {
      if (this.mPins[embeddedPin].PinName == pinName)
        return embeddedPin;
    }
    return -1;
  }

  [Serializable]
  public struct PinData
  {
    public int PinID;
    public string PinName;
    public FlowNode.PinTypes PinType;
    [NonSerialized]
    public FlowNode NodeBinding;
  }
}
