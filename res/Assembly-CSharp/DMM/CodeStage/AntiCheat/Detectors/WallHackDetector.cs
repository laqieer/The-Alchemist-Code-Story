﻿// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.WallHackDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

#nullable disable
namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/WallHack Detector")]
  [DisallowMultipleComponent]
  [HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_wall_hack_detector.html")]
  public class WallHackDetector : ACTkDetectorBase
  {
    internal const string ComponentName = "WallHack Detector";
    internal const string FinalLogPrefix = "[ACTk] WallHack Detector: ";
    private const string ServiceContainerName = "[WH Detector Service]";
    private const string WireframeShaderName = "Hidden/ACTk/WallHackTexture";
    private const int ShaderTextureSize = 4;
    private const int RenderTextureSize = 4;
    private readonly Vector3 rigidPlayerVelocity = new Vector3(0.0f, 0.0f, 1f);
    private static int instancesInScene;
    private readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    [SerializeField]
    [Tooltip("Check for the \"walk through the walls\" kind of cheats made via Rigidbody hacks?")]
    private bool checkRigidbody = true;
    [SerializeField]
    [Tooltip("Check for the \"walk through the walls\" kind of cheats made via Character Controller hacks?")]
    private bool checkController = true;
    [SerializeField]
    [Tooltip("Check for the \"see through the walls\" kind of cheats made via shader or driver hacks (wireframe, color alpha, etc.)?")]
    private bool checkWireframe = true;
    [SerializeField]
    [Tooltip("Check for the \"shoot through the walls\" kind of cheats made via Raycast hacks?")]
    private bool checkRaycast = true;
    [Tooltip("Delay between Wireframe module checks, from 1 up to 60 secs.")]
    [Range(1f, 60f)]
    public int wireframeDelay = 10;
    [Tooltip("Delay between Raycast module checks, from 1 up to 60 secs.")]
    [Range(1f, 60f)]
    public int raycastDelay = 10;
    [Tooltip("World position of the container for service objects within 3x3x3 cube (drawn as red wire cube in scene).")]
    public Vector3 spawnPosition;
    [Tooltip("Maximum false positives in a row for each detection module before registering a wall hack.")]
    public byte maxFalsePositives = 3;
    private GameObject serviceContainer;
    private GameObject solidWall;
    private GameObject thinWall;
    private Camera wfCamera;
    private MeshRenderer foregroundRenderer;
    private MeshRenderer backgroundRenderer;
    private Color wfColor1 = Color.black;
    private Color wfColor2 = Color.black;
    private Shader wfShader;
    private Material wfMaterial;
    private Texture2D shaderTexture;
    private Texture2D targetTexture;
    private RenderTexture renderTexture;
    private int whLayer = -1;
    private int raycastMask = -1;
    private Rigidbody rigidPlayer;
    private CharacterController charControllerPlayer;
    private float charControllerVelocity;
    private byte rigidbodyDetections;
    private byte controllerDetections;
    private byte wireframeDetections;
    private byte raycastDetections;
    private bool wireframeDetected;
    private readonly RaycastHit[] rayHits = new RaycastHit[10];

    private WallHackDetector()
    {
    }

    public bool CheckRigidbody
    {
      get => this.checkRigidbody;
      set
      {
        if (this.checkRigidbody == value || !Application.isPlaying || !((Behaviour) this).enabled || !((Component) this).gameObject.activeSelf)
          return;
        this.checkRigidbody = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkRigidbody)
          this.StartRigidModule();
        else
          this.StopRigidModule();
      }
    }

    public bool CheckController
    {
      get => this.checkController;
      set
      {
        if (this.checkController == value || !Application.isPlaying || !((Behaviour) this).enabled || !((Component) this).gameObject.activeSelf)
          return;
        this.checkController = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkController)
          this.StartControllerModule();
        else
          this.StopControllerModule();
      }
    }

    public bool CheckWireframe
    {
      get => this.checkWireframe;
      set
      {
        if (this.checkWireframe == value || !Application.isPlaying || !((Behaviour) this).enabled || !((Component) this).gameObject.activeSelf)
          return;
        this.checkWireframe = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkWireframe)
          this.StartWireframeModule();
        else
          this.StopWireframeModule();
      }
    }

    public bool CheckRaycast
    {
      get => this.checkRaycast;
      set
      {
        if (this.checkRaycast == value || !Application.isPlaying || !((Behaviour) this).enabled || !((Component) this).gameObject.activeSelf)
          return;
        this.checkRaycast = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkRaycast)
          this.StartRaycastModule();
        else
          this.StopRaycastModule();
      }
    }

    public static WallHackDetector AddToSceneOrGetExisting()
    {
      return WallHackDetector.GetOrCreateInstance;
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        WallHackDetector.Instance.StartDetectionInternal((Action) null, WallHackDetector.Instance.spawnPosition, WallHackDetector.Instance.maxFalsePositives);
      else
        Debug.LogError((object) "[ACTk] WallHack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(Action callback)
    {
      WallHackDetector.StartDetection(callback, WallHackDetector.GetOrCreateInstance.spawnPosition);
    }

    public static void StartDetection(Action callback, Vector3 spawnPosition)
    {
      WallHackDetector.StartDetection(callback, spawnPosition, WallHackDetector.GetOrCreateInstance.maxFalsePositives);
    }

    public static void StartDetection(
      Action callback,
      Vector3 spawnPosition,
      byte maxFalsePositives)
    {
      WallHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, spawnPosition, maxFalsePositives);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        return;
      WallHackDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        return;
      WallHackDetector.Instance.DisposeInternal();
    }

    public static WallHackDetector Instance { get; private set; }

    private static WallHackDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
          return WallHackDetector.Instance;
        if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null))
          ACTkDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        WallHackDetector.Instance = ACTkDetectorBase.detectorsContainer.AddComponent<WallHackDetector>();
        return WallHackDetector.Instance;
      }
    }

    private void Awake()
    {
      ++WallHackDetector.instancesInScene;
      if (this.Init((ACTkDetectorBase) WallHackDetector.Instance, "WallHack Detector"))
        WallHackDetector.Instance = this;
      // ISSUE: method pointer
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.StopAllCoroutines();
      if (Object.op_Inequality((Object) this.serviceContainer, (Object) null))
        Object.Destroy((Object) this.serviceContainer);
      if (Object.op_Inequality((Object) this.wfMaterial, (Object) null))
      {
        this.wfMaterial.mainTexture = (Texture) null;
        this.wfMaterial.shader = (Shader) null;
        this.wfMaterial = (Material) null;
        this.wfShader = (Shader) null;
        this.shaderTexture = (Texture2D) null;
        this.targetTexture = (Texture2D) null;
        this.renderTexture.DiscardContents();
        this.renderTexture.Release();
        this.renderTexture = (RenderTexture) null;
      }
      --WallHackDetector.instancesInScene;
    }

    private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
    {
      if (WallHackDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) WallHackDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void FixedUpdate()
    {
      if (!this.isRunning || !this.checkRigidbody || Object.op_Equality((Object) this.rigidPlayer, (Object) null) || (double) ((Component) this.rigidPlayer).transform.localPosition.z <= 1.0)
        return;
      ++this.rigidbodyDetections;
      if (this.Detect())
        return;
      this.StopRigidModule();
      this.StartRigidModule();
    }

    private void Update()
    {
      if (!this.isRunning || !this.checkController || Object.op_Equality((Object) this.charControllerPlayer, (Object) null) || (double) this.charControllerVelocity <= 0.0)
        return;
      this.charControllerPlayer.Move(new Vector3(Random.Range(-1f / 500f, 1f / 500f), 0.0f, this.charControllerVelocity));
      if ((double) ((Component) this.charControllerPlayer).transform.localPosition.z <= 1.0)
        return;
      ++this.controllerDetections;
      if (this.Detect())
        return;
      this.StopControllerModule();
      this.StartControllerModule();
    }

    private void StartDetectionInternal(
      Action callback,
      Vector3 servicePosition,
      byte falsePositivesInRow)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] WallHack Detector: already running!", (Object) this);
      else if (!((Behaviour) this).enabled)
      {
        Debug.LogWarning((object) "[ACTk] WallHack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] WallHack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] WallHack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).enabled = false;
        }
        else
        {
          this.CheatDetected += callback;
          this.spawnPosition = servicePosition;
          this.maxFalsePositives = falsePositivesInRow;
          this.rigidbodyDetections = (byte) 0;
          this.controllerDetections = (byte) 0;
          this.wireframeDetections = (byte) 0;
          this.raycastDetections = (byte) 0;
          this.StartCoroutine(this.InitDetector());
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((Action) null, this.spawnPosition, this.maxFalsePositives);
    }

    protected override void PauseDetector()
    {
      if (!this.isRunning)
        return;
      base.PauseDetector();
      this.StopRigidModule();
      this.StopControllerModule();
      this.StopWireframeModule();
      this.StopRaycastModule();
    }

    protected override bool ResumeDetector()
    {
      if (!base.ResumeDetector())
        return false;
      if (this.checkRigidbody)
        this.StartRigidModule();
      if (this.checkController)
        this.StartControllerModule();
      if (this.checkWireframe)
        this.StartWireframeModule();
      if (this.checkRaycast)
        this.StartRaycastModule();
      return true;
    }

    protected override void StopDetectionInternal()
    {
      if (this.started)
        this.PauseDetector();
      base.StopDetectionInternal();
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) WallHackDetector.Instance, (Object) this))
        return;
      WallHackDetector.Instance = (WallHackDetector) null;
    }

    private void UpdateServiceContainer()
    {
      if (((Behaviour) this).enabled && ((Component) this).gameObject.activeSelf)
      {
        if (this.whLayer == -1)
          this.whLayer = LayerMask.NameToLayer("Ignore Raycast");
        if (this.raycastMask == -1)
          this.raycastMask = LayerMask.GetMask(new string[1]
          {
            "Ignore Raycast"
          });
        if (Object.op_Equality((Object) this.serviceContainer, (Object) null))
        {
          this.serviceContainer = new GameObject("[WH Detector Service]");
          this.serviceContainer.layer = this.whLayer;
          this.serviceContainer.transform.position = this.spawnPosition;
          Object.DontDestroyOnLoad((Object) this.serviceContainer);
        }
        if ((this.checkRigidbody || this.checkController) && Object.op_Equality((Object) this.solidWall, (Object) null))
        {
          this.solidWall = new GameObject("SolidWall");
          this.solidWall.AddComponent<BoxCollider>();
          this.solidWall.layer = this.whLayer;
          this.solidWall.transform.parent = this.serviceContainer.transform;
          this.solidWall.transform.localScale = new Vector3(3f, 3f, 0.5f);
          this.solidWall.transform.localPosition = Vector3.zero;
        }
        else if (!this.checkRigidbody && !this.checkController && Object.op_Inequality((Object) this.solidWall, (Object) null))
          Object.Destroy((Object) this.solidWall);
        if (this.checkWireframe && Object.op_Equality((Object) this.wfCamera, (Object) null))
        {
          if (Object.op_Equality((Object) this.wfShader, (Object) null))
            this.wfShader = Shader.Find("Hidden/ACTk/WallHackTexture");
          if (Object.op_Equality((Object) this.wfShader, (Object) null))
          {
            Debug.LogError((object) "[ACTk] WallHack Detector: can't find 'Hidden/ACTk/WallHackTexture' shader!\nPlease make sure you have it included at the ACTk Settings widow.", (Object) this);
            this.checkWireframe = false;
          }
          else if (!this.wfShader.isSupported)
          {
            Debug.LogWarning((object) "[ACTk] WallHack Detector: can't detect wireframe cheats on this platform due to lack of needed shader support!", (Object) this);
            this.checkWireframe = false;
          }
          else
          {
            if (Color.op_Equality(this.wfColor1, Color.black))
            {
              this.wfColor1 = Color32.op_Implicit(WallHackDetector.GenerateColor());
              do
              {
                this.wfColor2 = Color32.op_Implicit(WallHackDetector.GenerateColor());
              }
              while (WallHackDetector.ColorsSimilar(Color32.op_Implicit(this.wfColor1), Color32.op_Implicit(this.wfColor2), 10));
            }
            if (Object.op_Equality((Object) this.shaderTexture, (Object) null))
            {
              this.shaderTexture = new Texture2D(4, 4, (TextureFormat) 3, false, true);
              ((Texture) this.shaderTexture).filterMode = (FilterMode) 0;
              Color[] colorArray = new Color[16];
              for (int index = 0; index < 16; ++index)
                colorArray[index] = index >= 8 ? this.wfColor2 : this.wfColor1;
              this.shaderTexture.SetPixels(colorArray, 0);
              this.shaderTexture.Apply();
            }
            if (Object.op_Equality((Object) this.renderTexture, (Object) null))
            {
              this.renderTexture = new RenderTexture(4, 4, 24, (RenderTextureFormat) 0, (RenderTextureReadWrite) 2);
              this.renderTexture.autoGenerateMips = false;
              ((Texture) this.renderTexture).filterMode = (FilterMode) 0;
              this.renderTexture.Create();
            }
            if (Object.op_Equality((Object) this.targetTexture, (Object) null))
            {
              this.targetTexture = new Texture2D(4, 4, (TextureFormat) 3, false, true);
              ((Texture) this.targetTexture).filterMode = (FilterMode) 0;
            }
            if (Object.op_Equality((Object) this.wfMaterial, (Object) null))
            {
              this.wfMaterial = new Material(this.wfShader);
              this.wfMaterial.mainTexture = (Texture) this.shaderTexture;
            }
            if (Object.op_Equality((Object) this.foregroundRenderer, (Object) null))
            {
              GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
              Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
              ((Object) primitive).name = "WireframeFore";
              primitive.layer = this.whLayer;
              primitive.transform.parent = this.serviceContainer.transform;
              primitive.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
              this.foregroundRenderer = primitive.GetComponent<MeshRenderer>();
              ((Renderer) this.foregroundRenderer).sharedMaterial = this.wfMaterial;
              ((Renderer) this.foregroundRenderer).shadowCastingMode = (ShadowCastingMode) 0;
              ((Renderer) this.foregroundRenderer).receiveShadows = false;
              ((Renderer) this.foregroundRenderer).enabled = false;
            }
            if (Object.op_Equality((Object) this.backgroundRenderer, (Object) null))
            {
              GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 5);
              Object.Destroy((Object) primitive.GetComponent<MeshCollider>());
              ((Object) primitive).name = "WireframeBack";
              primitive.layer = this.whLayer;
              primitive.transform.parent = this.serviceContainer.transform;
              primitive.transform.localPosition = new Vector3(0.0f, 0.0f, 1f);
              primitive.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
              this.backgroundRenderer = primitive.GetComponent<MeshRenderer>();
              ((Renderer) this.backgroundRenderer).sharedMaterial = this.wfMaterial;
              ((Renderer) this.backgroundRenderer).shadowCastingMode = (ShadowCastingMode) 0;
              ((Renderer) this.backgroundRenderer).receiveShadows = false;
              ((Renderer) this.backgroundRenderer).enabled = false;
            }
            this.wfCamera = new GameObject("WireframeCamera").AddComponent<Camera>();
            ((Component) this.wfCamera).gameObject.layer = this.whLayer;
            ((Component) this.wfCamera).transform.parent = this.serviceContainer.transform;
            ((Component) this.wfCamera).transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
            this.wfCamera.clearFlags = (CameraClearFlags) 2;
            this.wfCamera.backgroundColor = Color.black;
            this.wfCamera.orthographic = true;
            this.wfCamera.orthographicSize = 0.5f;
            this.wfCamera.nearClipPlane = 0.01f;
            this.wfCamera.farClipPlane = 2.1f;
            this.wfCamera.depth = 0.0f;
            this.wfCamera.renderingPath = (RenderingPath) 1;
            this.wfCamera.useOcclusionCulling = false;
            this.wfCamera.allowHDR = false;
            this.wfCamera.allowMSAA = false;
            this.wfCamera.targetTexture = this.renderTexture;
            ((Behaviour) this.wfCamera).enabled = false;
          }
        }
        else if (!this.checkWireframe && Object.op_Inequality((Object) this.wfCamera, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.foregroundRenderer).gameObject);
          Object.Destroy((Object) ((Component) this.backgroundRenderer).gameObject);
          this.wfCamera.targetTexture = (RenderTexture) null;
          Object.Destroy((Object) ((Component) this.wfCamera).gameObject);
        }
        if (this.checkRaycast && Object.op_Equality((Object) this.thinWall, (Object) null))
        {
          this.thinWall = GameObject.CreatePrimitive((PrimitiveType) 4);
          ((Object) this.thinWall).name = "ThinWall";
          this.thinWall.layer = this.whLayer;
          this.thinWall.transform.parent = this.serviceContainer.transform;
          this.thinWall.transform.localScale = new Vector3(0.2f, 1f, 0.2f);
          this.thinWall.transform.localRotation = Quaternion.Euler(270f, 0.0f, 0.0f);
          this.thinWall.transform.localPosition = new Vector3(0.0f, 0.0f, 1.4f);
          Object.Destroy((Object) this.thinWall.GetComponent<Renderer>());
          Object.Destroy((Object) this.thinWall.GetComponent<MeshFilter>());
        }
        else
        {
          if (this.checkRaycast || !Object.op_Inequality((Object) this.thinWall, (Object) null))
            return;
          Object.Destroy((Object) this.thinWall);
        }
      }
      else
      {
        if (!Object.op_Inequality((Object) this.serviceContainer, (Object) null))
          return;
        Object.Destroy((Object) this.serviceContainer);
      }
    }

    [DebuggerHidden]
    private IEnumerator InitDetector()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WallHackDetector.\u003CInitDetector\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void StartRigidModule()
    {
      if (!this.checkRigidbody)
      {
        this.StopRigidModule();
        this.UninitRigidModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (!Object.op_Implicit((Object) this.rigidPlayer))
          this.InitRigidModule();
        if ((double) ((Component) this.rigidPlayer).transform.localPosition.z <= 1.0 && this.rigidbodyDetections > (byte) 0)
          this.rigidbodyDetections = (byte) 0;
        this.rigidPlayer.rotation = Quaternion.identity;
        this.rigidPlayer.angularVelocity = Vector3.zero;
        ((Component) this.rigidPlayer).transform.localPosition = new Vector3(0.75f, 0.0f, -1f);
        this.rigidPlayer.velocity = this.rigidPlayerVelocity;
        this.Invoke(nameof (StartRigidModule), 4f);
      }
    }

    private void StartControllerModule()
    {
      if (!this.checkController)
      {
        this.StopControllerModule();
        this.UninitControllerModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (!Object.op_Implicit((Object) this.charControllerPlayer))
          this.InitControllerModule();
        if ((double) ((Component) this.charControllerPlayer).transform.localPosition.z <= 1.0 && this.controllerDetections > (byte) 0)
          this.controllerDetections = (byte) 0;
        ((Component) this.charControllerPlayer).transform.localPosition = new Vector3(-0.75f, 0.0f, -1f);
        this.charControllerVelocity = 0.01f;
        this.Invoke(nameof (StartControllerModule), 4f);
      }
    }

    private void StartWireframeModule()
    {
      if (!this.checkWireframe)
      {
        this.StopWireframeModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (this.wireframeDetected)
          return;
        this.Invoke("ShootWireframeModule", (float) this.wireframeDelay);
      }
    }

    private void ShootWireframeModule()
    {
      this.StartCoroutine(this.CaptureFrame());
      this.Invoke(nameof (ShootWireframeModule), (float) this.wireframeDelay);
    }

    [DebuggerHidden]
    private IEnumerator CaptureFrame()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WallHackDetector.\u003CCaptureFrame\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private void StartRaycastModule()
    {
      if (!this.checkRaycast)
      {
        this.StopRaycastModule();
        this.UpdateServiceContainer();
      }
      else
        this.Invoke("ShootRaycastModule", (float) this.raycastDelay);
    }

    private void ShootRaycastModule()
    {
      if (Physics.RaycastNonAlloc(this.serviceContainer.transform.position, this.serviceContainer.transform.TransformDirection(Vector3.forward), this.rayHits, 1.5f, this.raycastMask) > 0)
      {
        if (this.raycastDetections > (byte) 0)
          this.raycastDetections = (byte) 0;
      }
      else
      {
        ++this.raycastDetections;
        if (this.Detect())
          return;
      }
      this.Invoke(nameof (ShootRaycastModule), (float) this.raycastDelay);
    }

    private void StopRigidModule()
    {
      if (Object.op_Implicit((Object) this.rigidPlayer))
        this.rigidPlayer.velocity = Vector3.zero;
      this.CancelInvoke("StartRigidModule");
    }

    private void StopControllerModule()
    {
      if (Object.op_Implicit((Object) this.charControllerPlayer))
        this.charControllerVelocity = 0.0f;
      this.CancelInvoke("StartControllerModule");
    }

    private void StopWireframeModule() => this.CancelInvoke("ShootWireframeModule");

    private void StopRaycastModule() => this.CancelInvoke("ShootRaycastModule");

    private void InitRigidModule()
    {
      GameObject gameObject = new GameObject("RigidPlayer");
      gameObject.AddComponent<CapsuleCollider>().height = 2f;
      gameObject.layer = this.whLayer;
      gameObject.transform.parent = this.serviceContainer.transform;
      gameObject.transform.localPosition = new Vector3(0.75f, 0.0f, -1f);
      this.rigidPlayer = gameObject.AddComponent<Rigidbody>();
      this.rigidPlayer.useGravity = false;
    }

    private void InitControllerModule()
    {
      GameObject gameObject = new GameObject("ControlledPlayer");
      gameObject.AddComponent<CapsuleCollider>().height = 2f;
      gameObject.layer = this.whLayer;
      gameObject.transform.parent = this.serviceContainer.transform;
      gameObject.transform.localPosition = new Vector3(-0.75f, 0.0f, -1f);
      this.charControllerPlayer = gameObject.AddComponent<CharacterController>();
    }

    private void UninitRigidModule()
    {
      if (!Object.op_Implicit((Object) this.rigidPlayer))
        return;
      Object.Destroy((Object) ((Component) this.rigidPlayer).gameObject);
      this.rigidPlayer = (Rigidbody) null;
    }

    private void UninitControllerModule()
    {
      if (!Object.op_Implicit((Object) this.charControllerPlayer))
        return;
      Object.Destroy((Object) ((Component) this.charControllerPlayer).gameObject);
      this.charControllerPlayer = (CharacterController) null;
    }

    private bool Detect()
    {
      bool flag = false;
      if ((int) this.controllerDetections > (int) this.maxFalsePositives || (int) this.rigidbodyDetections > (int) this.maxFalsePositives || (int) this.wireframeDetections > (int) this.maxFalsePositives || (int) this.raycastDetections > (int) this.maxFalsePositives)
      {
        this.OnCheatingDetected();
        flag = true;
      }
      return flag;
    }

    private static Color32 GenerateColor()
    {
      return new Color32((byte) Random.Range(0, 256), (byte) Random.Range(0, 256), (byte) Random.Range(0, 256), byte.MaxValue);
    }

    private static bool ColorsSimilar(Color32 c1, Color32 c2, int tolerance)
    {
      return Math.Abs((int) c1.r - (int) c2.r) < tolerance && Math.Abs((int) c1.g - (int) c2.g) < tolerance && Math.Abs((int) c1.b - (int) c2.b) < tolerance;
    }
  }
}
