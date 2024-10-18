﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_GUISelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Common/GUISelect", 32741)]
[FlowNode.Pin(100, "Create", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "Destroy", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(102, "Preload", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(1, "Created", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(2, "Destroyed", FlowNode.PinTypes.Output, 11)]
public class FlowNode_GUISelect : FlowNode_ExternalLink
{
  [StringIsResourcePath(typeof (GameObject))]
  public string ResourcePath;
  public bool Modal;
  public bool SystemModal;
  private LoadRequest mResourceRequest;
  public bool OverridePriority;
  public int Priority;
  public bool LoadImmediately;
  public GameObject InstanceRef;
  protected DestroyEventListener mListener;
  private const int STARTADDPIN = 1000;
  [SerializeField]
  private int m_Num;
  [SerializeField]
  [HideInInspector]
  private FlowNode.Pin[] m_Pins = new FlowNode.Pin[0];
  [SerializeField]
  [HideInInspector]
  [StringIsResourcePath(typeof (GameObject))]
  private string[] AddResourcePath;

  protected override bool ShouldCreateInstanceOnStart => false;

  protected override void Awake()
  {
    base.Awake();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InstanceRef, (UnityEngine.Object) null))
      return;
    ((Behaviour) this).enabled = true;
  }

  protected override void Start()
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InstanceRef, (UnityEngine.Object) null))
      return;
    this.mInstance = this.InstanceRef;
    this.BindPins();
  }

  private void LoadResource()
  {
    if (this.mResourceRequest != null)
      return;
    DebugUtility.Log("Loading " + this.ResourcePath);
    this.mResourceRequest = AssetManager.LoadAsync(this.ResourcePath, typeof (GameObject));
  }

  protected virtual void OnCreatePinActive()
  {
    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.Instance, (UnityEngine.Object) null))
      return;
    if (!this.LoadImmediately && UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
    {
      this.LoadResource();
      ((Behaviour) this).enabled = true;
    }
    else
    {
      bool flag = false;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
      {
        GameObject gameObject = AssetManager.Load<GameObject>(this.ResourcePath);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          Debug.LogError((object) ("Failed to load '" + this.ResourcePath + "'"));
          return;
        }
        this.Target = gameObject;
        flag = true;
      }
      this.CreateInstance();
      this.ActivateOutputLinks(1);
      if (!flag)
        return;
      this.Target = (GameObject) null;
    }
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 102)
      this.LoadResource();
    else if (pinID == 100)
      this.OnCreatePinActive();
    else if (pinID == 101)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mListener, (UnityEngine.Object) null))
      {
        this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
        this.mListener = (DestroyEventListener) null;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Instance, (UnityEngine.Object) null))
      {
        ((Behaviour) this).enabled = false;
        this.DestroyInstance();
        this.ActivateOutputLinks(2);
      }
      else
      {
        if (this.mResourceRequest == null)
          return;
        this.mResourceRequest = (LoadRequest) null;
        ((Behaviour) this).enabled = false;
      }
    }
    else if (pinID >= 1000)
    {
      this.ResourcePath = this.AddResourcePath[pinID - 1000];
      this.OnCreatePinActive();
    }
    else
      base.OnActivate(pinID);
  }

  protected void OnInstanceDestroyTrigger(GameObject go)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mInstance, (UnityEngine.Object) null))
      return;
    this.OnActivate(101);
  }

  protected override void OnDestroy()
  {
    if (this.NoAutoDestruct && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mListener, (UnityEngine.Object) null))
    {
      this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
      this.mListener = (DestroyEventListener) null;
    }
    base.OnDestroy();
  }

  protected override void OnInstanceDestroy()
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mListener, (UnityEngine.Object) null))
      return;
    this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
    this.mListener = (DestroyEventListener) null;
  }

  private void OnApplicationQuit() => this.mInstance = (GameObject) null;

  private void Update()
  {
    if (this.mResourceRequest != null)
    {
      if (!this.mResourceRequest.isDone)
        return;
      if (UnityEngine.Object.op_Equality(this.mResourceRequest.asset, (UnityEngine.Object) null))
      {
        Debug.LogError((object) ("Failed to load '" + this.ResourcePath + "'"));
        ((Behaviour) this).enabled = false;
      }
      else
      {
        this.Target = this.mResourceRequest.asset as GameObject;
        this.mResourceRequest = (LoadRequest) null;
        this.CreateInstance();
        this.ActivateOutputLinks(1);
        this.Target = (GameObject) null;
      }
    }
    else
      ((Behaviour) this).enabled = false;
  }

  protected override void OnInstanceCreate()
  {
    Canvas canvas = this.Instance.GetComponent<Canvas>();
    if ((this.Modal || this.SystemModal) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Instance, (UnityEngine.Object) null))
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Instance.GetComponent<Canvas>(), (UnityEngine.Object) null))
      {
        GameObject gameObject = new GameObject("ModalCanvas", new System.Type[5]
        {
          typeof (Canvas),
          typeof (GraphicRaycaster),
          typeof (SRPG_CanvasScaler),
          typeof (CanvasStack),
          typeof (TemporaryCanvas)
        });
        Canvas component = gameObject.GetComponent<Canvas>();
        component.renderMode = (RenderMode) 0;
        gameObject.gameObject.SetActive(false);
        gameObject.gameObject.SetActive(true);
        gameObject.GetComponent<TemporaryCanvas>().Instance = this.Instance;
        this.mInstance.transform.SetParent(gameObject.transform, false);
        this.mInstance = gameObject;
        canvas = component;
      }
      this.Instance.transform.SetParent((Transform) null, false);
      CanvasStack component1 = this.mInstance.GetComponent<CanvasStack>();
      if (this.SystemModal)
      {
        component1.SystemModal = true;
        component1.Priority = this.Priority;
      }
      else if (this.OverridePriority)
      {
        component1.Modal = true;
        component1.Priority = this.Priority;
      }
      if (this.Modal || this.SystemModal)
      {
        ((Behaviour) canvas).enabled = false;
        ((Behaviour) canvas).enabled = true;
      }
    }
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
  }
}
