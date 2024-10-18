// Decompiled with JetBrains decompiler
// Type: FlowNode_GUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Common/GUI", 32741)]
[FlowNode.Pin(100, "Create", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "Destroy", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(102, "Preload", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(1, "Created", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(2, "Destroyed", FlowNode.PinTypes.Output, 11)]
public class FlowNode_GUI : FlowNode_ExternalLink
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

  protected override bool ShouldCreateInstanceOnStart
  {
    get
    {
      return false;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (!((UnityEngine.Object) this.InstanceRef != (UnityEngine.Object) null))
      return;
    this.enabled = true;
  }

  protected override void Start()
  {
    if (!((UnityEngine.Object) this.InstanceRef != (UnityEngine.Object) null))
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
    if (!((UnityEngine.Object) this.Instance == (UnityEngine.Object) null))
      return;
    if (!this.LoadImmediately && (UnityEngine.Object) this.Target == (UnityEngine.Object) null)
    {
      this.LoadResource();
      this.enabled = true;
    }
    else
    {
      bool flag = false;
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
      {
        GameObject gameObject = AssetManager.Load<GameObject>(this.ResourcePath);
        if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
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
    switch (pinID)
    {
      case 100:
        this.OnCreatePinActive();
        break;
      case 101:
        if ((UnityEngine.Object) this.mListener != (UnityEngine.Object) null)
        {
          this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
          this.mListener = (DestroyEventListener) null;
        }
        if ((UnityEngine.Object) this.Instance != (UnityEngine.Object) null)
        {
          this.enabled = false;
          this.DestroyInstance();
          this.ActivateOutputLinks(2);
          break;
        }
        if (this.mResourceRequest == null)
          break;
        this.mResourceRequest = (LoadRequest) null;
        this.enabled = false;
        break;
      case 102:
        this.LoadResource();
        break;
      default:
        base.OnActivate(pinID);
        break;
    }
  }

  protected void OnInstanceDestroyTrigger(GameObject go)
  {
    if ((UnityEngine.Object) this.mInstance == (UnityEngine.Object) null)
      return;
    this.OnActivate(101);
  }

  protected override void OnDestroy()
  {
    if (this.NoAutoDestruct && (UnityEngine.Object) this.mListener != (UnityEngine.Object) null)
    {
      this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
      this.mListener = (DestroyEventListener) null;
    }
    base.OnDestroy();
  }

  protected override void OnInstanceDestroy()
  {
    if (!((UnityEngine.Object) this.mListener != (UnityEngine.Object) null))
      return;
    this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
    this.mListener = (DestroyEventListener) null;
  }

  private void OnApplicationQuit()
  {
    this.mInstance = (GameObject) null;
  }

  private void Update()
  {
    if (this.mResourceRequest != null)
    {
      if (!this.mResourceRequest.isDone)
        return;
      if (this.mResourceRequest.asset == (UnityEngine.Object) null)
      {
        Debug.LogError((object) ("Failed to load '" + this.ResourcePath + "'"));
        this.enabled = false;
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
      this.enabled = false;
  }

  protected override void OnInstanceCreate()
  {
    Canvas canvas = this.Instance.GetComponent<Canvas>();
    if ((this.Modal || this.SystemModal) && (UnityEngine.Object) this.Instance != (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) this.Instance.GetComponent<Canvas>() == (UnityEngine.Object) null)
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
        component.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
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
        canvas.enabled = false;
        canvas.enabled = true;
      }
    }
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
  }
}
