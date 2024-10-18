// Decompiled with JetBrains decompiler
// Type: FlowNode_ExternalLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[FlowNode.NodeType("ExternalLink", 32741)]
public class FlowNode_ExternalLink : FlowNode
{
  [SerializeField]
  [HideInInspector]
  private FlowNode_ExternalLink.PinData[] mPins = new FlowNode_ExternalLink.PinData[0];
  public GameObject Target;
  protected GameObject mInstance;
  public string InstanceName;
  public bool NoAutoDestruct;

  public GameObject Instance
  {
    get
    {
      return this.mInstance;
    }
  }

  protected virtual bool ShouldCreateInstanceOnStart
  {
    get
    {
      return true;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (!this.ShouldCreateInstanceOnStart)
      return;
    this.enabled = true;
  }

  protected void CreateInstance()
  {
    this.enabled = false;
    this.DestroyInstance();
    if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
      return;
    this.mInstance = UnityEngine.Object.Instantiate<GameObject>(this.Target);
    this.mInstance.transform.SetParent(this.transform, false);
    RectTransform component1 = this.Target.GetComponent<RectTransform>();
    if ((UnityEngine.Object) component1 != (UnityEngine.Object) null && (UnityEngine.Object) this.Target.GetComponent<Canvas>() != (UnityEngine.Object) null)
    {
      RectTransform component2 = this.mInstance.GetComponent<RectTransform>();
      component2.anchorMax = component1.anchorMax;
      component2.anchorMin = component1.anchorMin;
      component2.anchoredPosition = component1.anchoredPosition;
      component2.sizeDelta = component1.sizeDelta;
    }
    if (!string.IsNullOrEmpty(this.InstanceName))
      this.mInstance.name = this.InstanceName;
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
        int indexOfEmbeddedPin = FlowNode_ExternalLink.FindIndexOfEmbeddedPin(this.mPins, flowNodeOutput.PinName);
        if (indexOfEmbeddedPin >= 0 && this.mPins[indexOfEmbeddedPin].PinType == FlowNode.PinTypes.Output)
        {
          this.mPins[indexOfEmbeddedPin].NodeBinding = components[index];
          flowNodeOutput.TargetNode = this;
          flowNodeOutput.TargetPinID = this.mPins[indexOfEmbeddedPin].PinID;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        int indexOfEmbeddedPin = FlowNode_ExternalLink.FindIndexOfEmbeddedPin(this.mPins, (components[index] as FlowNode_Input).PinName);
        if (indexOfEmbeddedPin >= 0 && this.mPins[indexOfEmbeddedPin].PinType == FlowNode.PinTypes.Input)
          this.mPins[indexOfEmbeddedPin].NodeBinding = components[index];
      }
    }
  }

  protected void DestroyInstance()
  {
    if ((UnityEngine.Object) this.mInstance == (UnityEngine.Object) null)
      return;
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if ((UnityEngine.Object) this.mPins[index].NodeBinding != (UnityEngine.Object) null)
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
    UnityEngine.Object.Destroy((UnityEngine.Object) this.mInstance);
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
    FlowNode.Pin[] pinArray = new FlowNode.Pin[this.mPins.Length];
    for (int index = 0; index < this.mPins.Length; ++index)
      pinArray[index] = new FlowNode.Pin(this.mPins[index].PinID, this.mPins[index].PinName, this.mPins[index].PinType, 1000 + index);
    return pinArray;
  }

  protected virtual void Start()
  {
    this.CreateInstance();
  }

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
      if (this.mPins[index].PinID == pinID && this.mPins[index].PinType == FlowNode.PinTypes.Input && (UnityEngine.Object) this.mPins[index].NodeBinding != (UnityEngine.Object) null)
        this.mPins[index].NodeBinding.ActivateOutputLinks(1);
    }
  }

  public void RefreshPins(GameObject inTargetGameObject)
  {
    if ((UnityEngine.Object) inTargetGameObject == (UnityEngine.Object) null)
      return;
    FlowNode[] components = inTargetGameObject.GetComponents<FlowNode>();
    this.mPins = this.UpdateMissingPinData(this.mPins, components);
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] is FlowNode_Output)
      {
        FlowNode_Output flowNodeOutput = components[index] as FlowNode_Output;
        if (FlowNode_ExternalLink.FindIndexOfEmbeddedPin(this.mPins, flowNodeOutput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID(this.Pins);
          this.mPins[this.mPins.Length - 1].PinName = flowNodeOutput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Output;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        FlowNode_Input flowNodeInput = components[index] as FlowNode_Input;
        if (FlowNode_ExternalLink.FindIndexOfEmbeddedPin(this.mPins, flowNodeInput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID(this.Pins);
          this.mPins[this.mPins.Length - 1].PinName = flowNodeInput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Input;
        }
      }
    }
  }

  private FlowNode_ExternalLink.PinData[] UpdateMissingPinData(FlowNode_ExternalLink.PinData[] inExistingPinData, FlowNode[] inExistingNodes)
  {
    List<FlowNode> flowNodeList = new List<FlowNode>((IEnumerable<FlowNode>) inExistingNodes);
    List<FlowNode_Output> allFlowNodeOutput = flowNodeList.FindAll((Predicate<FlowNode>) (node => node is FlowNode_Output)).ConvertAll<FlowNode_Output>((Converter<FlowNode, FlowNode_Output>) (node => node as FlowNode_Output));
    List<FlowNode_Input> allFlowNodeInput = flowNodeList.FindAll((Predicate<FlowNode>) (node => node is FlowNode_Input)).ConvertAll<FlowNode_Input>((Converter<FlowNode, FlowNode_Input>) (node => node as FlowNode_Input));
    List<FlowNode_ExternalLink.PinData> pinDataList = new List<FlowNode_ExternalLink.PinData>((IEnumerable<FlowNode_ExternalLink.PinData>) inExistingPinData);
    pinDataList.RemoveAll((Predicate<FlowNode_ExternalLink.PinData>) (existingPin =>
    {
      if ((UnityEngine.Object) allFlowNodeOutput.Find((Predicate<FlowNode_Output>) (node => node.PinName.Equals(existingPin.PinName))) == (UnityEngine.Object) null)
        return (UnityEngine.Object) allFlowNodeInput.Find((Predicate<FlowNode_Input>) (node => node.PinName.Equals(existingPin.PinName))) == (UnityEngine.Object) null;
      return false;
    }));
    return pinDataList.ToArray();
  }

  private int GenerateUniquePinID(FlowNode.Pin[] originalPins)
  {
    FlowNode.Pin[] pinArray = originalPins;
    int num;
    do
    {
      num = UnityEngine.Random.Range(1, (int) ushort.MaxValue);
      for (int index = 0; index < pinArray.Length; ++index)
      {
        if (pinArray[index].PinID == num)
        {
          num = -1;
          break;
        }
      }
    }
    while (num < 0);
    return num;
  }

  private static int FindIndexOfEmbeddedPin(FlowNode_ExternalLink.PinData[] mPins, string pinName)
  {
    for (int index = 0; index < mPins.Length; ++index)
    {
      if (mPins[index].PinName == pinName)
        return index;
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
