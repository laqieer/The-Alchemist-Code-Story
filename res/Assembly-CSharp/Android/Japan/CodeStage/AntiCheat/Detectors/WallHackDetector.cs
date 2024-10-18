// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.WallHackDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/WallHack Detector")]
  public class WallHackDetector : ActDetectorBase
  {
    private readonly Vector3 rigidPlayerVelocity = new Vector3(0.0f, 0.0f, 1f);
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
    [Tooltip("Maximum false positives in a row for each detection module before registering a wall hack.")]
    public byte maxFalsePositives = 3;
    private Color wfColor1 = Color.black;
    private Color wfColor2 = Color.black;
    private int whLayer = -1;
    private int raycastMask = -1;
    internal const string COMPONENT_NAME = "WallHack Detector";
    internal const string FINAL_LOG_PREFIX = "[ACTk] WallHack Detector: ";
    private const string SERVICE_CONTAINER_NAME = "[WH Detector Service]";
    private const string WIREFRAME_SHADER_NAME = "Hidden/ACTk/WallHackTexture";
    private const int SHADER_TEXTURE_SIZE = 4;
    private const int RENDER_TEXTURE_SIZE = 4;
    private static int instancesInScene;
    [Tooltip("World position of the container for service objects within 3x3x3 cube (drawn as red wire cube in scene).")]
    public Vector3 spawnPosition;
    private GameObject serviceContainer;
    private GameObject solidWall;
    private GameObject thinWall;
    private Camera wfCamera;
    private MeshRenderer foregroundRenderer;
    private MeshRenderer backgroundRenderer;
    private Shader wfShader;
    private Material wfMaterial;
    private Texture2D shaderTexture;
    private Texture2D targetTexture;
    private RenderTexture renderTexture;
    private Rigidbody rigidPlayer;
    private CharacterController charControllerPlayer;
    private float charControllerVelocity;
    private byte rigidbodyDetections;
    private byte controllerDetections;
    private byte wireframeDetections;
    private byte raycastDetections;
    private bool wireframeDetected;

    private WallHackDetector()
    {
    }

    public bool CheckRigidbody
    {
      get
      {
        return this.checkRigidbody;
      }
      set
      {
        if (this.checkRigidbody == value || !Application.isPlaying || (!this.enabled || !this.gameObject.activeSelf))
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
      get
      {
        return this.checkController;
      }
      set
      {
        if (this.checkController == value || !Application.isPlaying || (!this.enabled || !this.gameObject.activeSelf))
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
      get
      {
        return this.checkWireframe;
      }
      set
      {
        if (this.checkWireframe == value || !Application.isPlaying || (!this.enabled || !this.gameObject.activeSelf))
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
      get
      {
        return this.checkRaycast;
      }
      set
      {
        if (this.checkRaycast == value || !Application.isPlaying || (!this.enabled || !this.gameObject.activeSelf))
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

    public static void StartDetection()
    {
      if ((Object) WallHackDetector.Instance != (Object) null)
        WallHackDetector.Instance.StartDetectionInternal((UnityAction) null, WallHackDetector.Instance.spawnPosition, WallHackDetector.Instance.maxFalsePositives);
      else
        Debug.LogError((object) "[ACTk] WallHack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(UnityAction callback)
    {
      WallHackDetector.StartDetection(callback, WallHackDetector.GetOrCreateInstance.spawnPosition);
    }

    public static void StartDetection(UnityAction callback, Vector3 spawnPosition)
    {
      WallHackDetector.StartDetection(callback, spawnPosition, WallHackDetector.GetOrCreateInstance.maxFalsePositives);
    }

    public static void StartDetection(UnityAction callback, Vector3 spawnPosition, byte maxFalsePositives)
    {
      WallHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, spawnPosition, maxFalsePositives);
    }

    public static void StopDetection()
    {
      if (!((Object) WallHackDetector.Instance != (Object) null))
        return;
      WallHackDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!((Object) WallHackDetector.Instance != (Object) null))
        return;
      WallHackDetector.Instance.DisposeInternal();
    }

    public static WallHackDetector Instance { get; private set; }

    private static WallHackDetector GetOrCreateInstance
    {
      get
      {
        if ((Object) WallHackDetector.Instance != (Object) null)
          return WallHackDetector.Instance;
        if ((Object) ActDetectorBase.detectorsContainer == (Object) null)
          ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        WallHackDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<WallHackDetector>();
        return WallHackDetector.Instance;
      }
    }

    private void Awake()
    {
      ++WallHackDetector.instancesInScene;
      if (this.Init((ActDetectorBase) WallHackDetector.Instance, "WallHack Detector"))
        WallHackDetector.Instance = this;
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.LoadedLevel);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.StopAllCoroutines();
      if ((Object) this.serviceContainer != (Object) null)
        Object.Destroy((Object) this.serviceContainer);
      if ((Object) this.wfMaterial != (Object) null)
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
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.LoadedLevel);
    }

    private void LoadedLevel(Scene scenename, LoadSceneMode SceneMode)
    {
      if (WallHackDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !((Object) WallHackDetector.Instance != (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void FixedUpdate()
    {
      if (!this.isRunning || !this.checkRigidbody || (Object) this.rigidPlayer == (Object) null || (double) this.rigidPlayer.transform.localPosition.z <= 1.0)
        return;
      ++this.rigidbodyDetections;
      if (this.Detect())
        return;
      this.StopRigidModule();
      this.StartRigidModule();
    }

    private void Update()
    {
      if (!this.isRunning || !this.checkController || ((Object) this.charControllerPlayer == (Object) null || (double) this.charControllerVelocity <= 0.0))
        return;
      int num = (int) this.charControllerPlayer.Move(new Vector3(Random.Range(-1f / 500f, 1f / 500f), 0.0f, this.charControllerVelocity));
      if ((double) this.charControllerPlayer.transform.localPosition.z <= 1.0)
        return;
      ++this.controllerDetections;
      if (this.Detect())
        return;
      this.StopControllerModule();
      this.StartControllerModule();
    }

    private void StartDetectionInternal(UnityAction callback, Vector3 servicePosition, byte falsePositivesInRow)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] WallHack Detector: already running!", (Object) this);
      else if (!this.enabled)
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
          this.enabled = false;
        }
        else
        {
          this.detectionAction = callback;
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
      this.StartDetectionInternal((UnityAction) null, this.spawnPosition, this.maxFalsePositives);
    }

    protected override void PauseDetector()
    {
      if (!this.isRunning)
        return;
      this.isRunning = false;
      this.StopRigidModule();
      this.StopControllerModule();
      this.StopWireframeModule();
      this.StopRaycastModule();
    }

    protected override void ResumeDetector()
    {
      if (this.detectionAction == null && !this.detectionEventHasListener)
        return;
      this.isRunning = true;
      if (this.checkRigidbody)
        this.StartRigidModule();
      if (this.checkController)
        this.StartControllerModule();
      if (this.checkWireframe)
        this.StartWireframeModule();
      if (!this.checkRaycast)
        return;
      this.StartRaycastModule();
    }

    protected override void StopDetectionInternal()
    {
      if (!this.started)
        return;
      this.PauseDetector();
      this.detectionAction = (UnityAction) null;
      this.isRunning = false;
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!((Object) WallHackDetector.Instance == (Object) this))
        return;
      WallHackDetector.Instance = (WallHackDetector) null;
    }

    private void UpdateServiceContainer()
    {
      if (this.enabled && this.gameObject.activeSelf)
      {
        if (this.whLayer == -1)
          this.whLayer = LayerMask.NameToLayer("Ignore Raycast");
        if (this.raycastMask == -1)
          this.raycastMask = LayerMask.GetMask("Ignore Raycast");
        if ((Object) this.serviceContainer == (Object) null)
        {
          this.serviceContainer = new GameObject("[WH Detector Service]");
          this.serviceContainer.layer = this.whLayer;
          this.serviceContainer.transform.position = this.spawnPosition;
          Object.DontDestroyOnLoad((Object) this.serviceContainer);
        }
        if ((this.checkRigidbody || this.checkController) && (Object) this.solidWall == (Object) null)
        {
          this.solidWall = new GameObject("SolidWall");
          this.solidWall.AddComponent<BoxCollider>();
          this.solidWall.layer = this.whLayer;
          this.solidWall.transform.parent = this.serviceContainer.transform;
          this.solidWall.transform.localScale = new Vector3(3f, 3f, 0.5f);
          this.solidWall.transform.localPosition = Vector3.zero;
        }
        else if (!this.checkRigidbody && !this.checkController && (Object) this.solidWall != (Object) null)
          Object.Destroy((Object) this.solidWall);
        if (this.checkWireframe && (Object) this.wfCamera == (Object) null)
        {
          if ((Object) this.wfShader == (Object) null)
            this.wfShader = Shader.Find("Hidden/ACTk/WallHackTexture");
          if ((Object) this.wfShader == (Object) null)
          {
            Debug.LogError((object) "[ACTk] WallHack Detector: can't find 'Hidden/ACTk/WallHackTexture' shader!\nPlease make sure you have it included at the Editor > Project Settings > Graphics.", (Object) this);
            this.checkWireframe = false;
          }
          else if (!this.wfShader.isSupported)
          {
            Debug.LogError((object) "[ACTk] WallHack Detector: can't detect wireframe cheats on this platform!", (Object) this);
            this.checkWireframe = false;
          }
          else
          {
            if (this.wfColor1 == Color.black)
            {
              this.wfColor1 = (Color) WallHackDetector.GenerateColor();
              do
              {
                this.wfColor2 = (Color) WallHackDetector.GenerateColor();
              }
              while (WallHackDetector.ColorsSimilar((Color32) this.wfColor1, (Color32) this.wfColor2, 10));
            }
            if ((Object) this.shaderTexture == (Object) null)
            {
              this.shaderTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);
              this.shaderTexture.filterMode = FilterMode.Point;
              Color[] colors = new Color[16];
              for (int index = 0; index < 16; ++index)
                colors[index] = index >= 8 ? this.wfColor2 : this.wfColor1;
              this.shaderTexture.SetPixels(colors, 0);
              this.shaderTexture.Apply();
            }
            if ((Object) this.renderTexture == (Object) null)
            {
              this.renderTexture = new RenderTexture(4, 4, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
              this.renderTexture.autoGenerateMips = false;
              this.renderTexture.filterMode = FilterMode.Point;
              this.renderTexture.Create();
            }
            if ((Object) this.targetTexture == (Object) null)
            {
              this.targetTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);
              this.targetTexture.filterMode = FilterMode.Point;
            }
            if ((Object) this.wfMaterial == (Object) null)
            {
              this.wfMaterial = new Material(this.wfShader);
              this.wfMaterial.mainTexture = (Texture) this.shaderTexture;
            }
            if ((Object) this.foregroundRenderer == (Object) null)
            {
              GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
              Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
              primitive.name = "WireframeFore";
              primitive.layer = this.whLayer;
              primitive.transform.parent = this.serviceContainer.transform;
              primitive.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
              this.foregroundRenderer = primitive.GetComponent<MeshRenderer>();
              this.foregroundRenderer.sharedMaterial = this.wfMaterial;
              this.foregroundRenderer.shadowCastingMode = ShadowCastingMode.Off;
              this.foregroundRenderer.receiveShadows = false;
              this.foregroundRenderer.enabled = false;
            }
            if ((Object) this.backgroundRenderer == (Object) null)
            {
              GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
              Object.Destroy((Object) primitive.GetComponent<MeshCollider>());
              primitive.name = "WireframeBack";
              primitive.layer = this.whLayer;
              primitive.transform.parent = this.serviceContainer.transform;
              primitive.transform.localPosition = new Vector3(0.0f, 0.0f, 1f);
              primitive.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
              this.backgroundRenderer = primitive.GetComponent<MeshRenderer>();
              this.backgroundRenderer.sharedMaterial = this.wfMaterial;
              this.backgroundRenderer.shadowCastingMode = ShadowCastingMode.Off;
              this.backgroundRenderer.receiveShadows = false;
              this.backgroundRenderer.enabled = false;
            }
            if ((Object) this.wfCamera == (Object) null)
            {
              this.wfCamera = new GameObject("WireframeCamera").AddComponent<Camera>();
              this.wfCamera.gameObject.layer = this.whLayer;
              this.wfCamera.transform.parent = this.serviceContainer.transform;
              this.wfCamera.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
              this.wfCamera.clearFlags = CameraClearFlags.Color;
              this.wfCamera.backgroundColor = Color.black;
              this.wfCamera.orthographic = true;
              this.wfCamera.orthographicSize = 0.5f;
              this.wfCamera.nearClipPlane = 0.01f;
              this.wfCamera.farClipPlane = 2.1f;
              this.wfCamera.depth = 0.0f;
              this.wfCamera.renderingPath = RenderingPath.Forward;
              this.wfCamera.useOcclusionCulling = false;
              this.wfCamera.allowHDR = false;
              this.wfCamera.targetTexture = this.renderTexture;
              this.wfCamera.enabled = false;
            }
          }
        }
        else if (!this.checkWireframe && (Object) this.wfCamera != (Object) null)
        {
          Object.Destroy((Object) this.foregroundRenderer.gameObject);
          Object.Destroy((Object) this.backgroundRenderer.gameObject);
          this.wfCamera.targetTexture = (RenderTexture) null;
          Object.Destroy((Object) this.wfCamera.gameObject);
        }
        if (this.checkRaycast && (Object) this.thinWall == (Object) null)
        {
          this.thinWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
          this.thinWall.name = "ThinWall";
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
          if (this.checkRaycast || !((Object) this.thinWall != (Object) null))
            return;
          Object.Destroy((Object) this.thinWall);
        }
      }
      else
      {
        if (!((Object) this.serviceContainer != (Object) null))
          return;
        Object.Destroy((Object) this.serviceContainer);
      }
    }

    [DebuggerHidden]
    private IEnumerator InitDetector()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WallHackDetector.\u003CInitDetector\u003Ec__Iterator0() { \u0024this = this };
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
        if (!(bool) ((Object) this.rigidPlayer))
          this.InitRigidModule();
        if ((double) this.rigidPlayer.transform.localPosition.z <= 1.0 && this.rigidbodyDetections > (byte) 0)
          this.rigidbodyDetections = (byte) 0;
        this.rigidPlayer.rotation = Quaternion.identity;
        this.rigidPlayer.angularVelocity = Vector3.zero;
        this.rigidPlayer.transform.localPosition = new Vector3(0.75f, 0.0f, -1f);
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
        if (!(bool) ((Object) this.charControllerPlayer))
          this.InitControllerModule();
        if ((double) this.charControllerPlayer.transform.localPosition.z <= 1.0 && this.controllerDetections > (byte) 0)
          this.controllerDetections = (byte) 0;
        this.charControllerPlayer.transform.localPosition = new Vector3(-0.75f, 0.0f, -1f);
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
      return (IEnumerator) new WallHackDetector.\u003CCaptureFrame\u003Ec__Iterator1() { \u0024this = this };
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
      if (Physics.Raycast(this.serviceContainer.transform.position, this.serviceContainer.transform.TransformDirection(Vector3.forward), 1.5f, this.raycastMask))
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
      if ((bool) ((Object) this.rigidPlayer))
        this.rigidPlayer.velocity = Vector3.zero;
      this.CancelInvoke("StartRigidModule");
    }

    private void StopControllerModule()
    {
      if ((bool) ((Object) this.charControllerPlayer))
        this.charControllerVelocity = 0.0f;
      this.CancelInvoke("StartControllerModule");
    }

    private void StopWireframeModule()
    {
      this.CancelInvoke("ShootWireframeModule");
    }

    private void StopRaycastModule()
    {
      this.CancelInvoke("ShootRaycastModule");
    }

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
      if (!(bool) ((Object) this.rigidPlayer))
        return;
      Object.Destroy((Object) this.rigidPlayer.gameObject);
      this.rigidPlayer = (Rigidbody) null;
    }

    private void UninitControllerModule()
    {
      if (!(bool) ((Object) this.charControllerPlayer))
        return;
      Object.Destroy((Object) this.charControllerPlayer.gameObject);
      this.charControllerPlayer = (CharacterController) null;
    }

    private bool Detect()
    {
      bool flag = false;
      if ((int) this.controllerDetections > (int) this.maxFalsePositives || (int) this.rigidbodyDetections > (int) this.maxFalsePositives || ((int) this.wireframeDetections > (int) this.maxFalsePositives || (int) this.raycastDetections > (int) this.maxFalsePositives))
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
      if (Math.Abs((int) c1.r - (int) c2.r) < tolerance && Math.Abs((int) c1.g - (int) c2.g) < tolerance)
        return Math.Abs((int) c1.b - (int) c2.b) < tolerance;
      return false;
    }
  }
}
