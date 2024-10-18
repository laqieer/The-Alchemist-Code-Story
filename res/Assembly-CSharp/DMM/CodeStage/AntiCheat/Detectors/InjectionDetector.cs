// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.InjectionDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#nullable disable
namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Injection Detector")]
  [DisallowMultipleComponent]
  [HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_injection_detector.html")]
  public class InjectionDetector : ACTkDetectorBase
  {
    internal const string ComponentName = "Injection Detector";
    internal const string FinalLogPrefix = "[ACTk] Injection Detector: ";
    private static int instancesInScene;
    private bool signaturesAreNotGenuine;
    private InjectionDetector.AllowedAssembly[] allowedAssemblies;
    private string[] hexTable;

    private InjectionDetector()
    {
    }

    public event Action<string> CheatDetected;

    public static InjectionDetector AddToSceneOrGetExisting()
    {
      return InjectionDetector.GetOrCreateInstance;
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        InjectionDetector.Instance.StartDetectionInternal((Action<string>) null);
      else
        Debug.LogError((object) "[ACTk] Injection Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(Action<string> callback)
    {
      InjectionDetector.GetOrCreateInstance.StartDetectionInternal(callback);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        return;
      InjectionDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        return;
      InjectionDetector.Instance.DisposeInternal();
    }

    public static InjectionDetector Instance { get; private set; }

    private static InjectionDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
          return InjectionDetector.Instance;
        if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null))
          ACTkDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        InjectionDetector.Instance = ACTkDetectorBase.detectorsContainer.AddComponent<InjectionDetector>();
        return InjectionDetector.Instance;
      }
    }

    private void Awake()
    {
      ++InjectionDetector.instancesInScene;
      if (this.Init((ACTkDetectorBase) InjectionDetector.Instance, "Injection Detector"))
        InjectionDetector.Instance = this;
      // ISSUE: method pointer
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --InjectionDetector.instancesInScene;
    }

    private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
    {
      if (InjectionDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) InjectionDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void StartDetectionInternal(Action<string> callback)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Injection Detector: already running!", (Object) this);
      else if (!((Behaviour) this).enabled)
      {
        Debug.LogWarning((object) "[ACTk] Injection Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if ((this.CheatDetected != null || callback != null) && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Injection Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (this.CheatDetected == null && callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] Injection Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).enabled = false;
        }
        else
        {
          this.CheatDetected += callback;
          this.started = true;
          this.isRunning = true;
          if (this.allowedAssemblies == null)
            this.LoadAndParseAllowedAssemblies();
          if (this.signaturesAreNotGenuine)
          {
            this.OnCheatingDetected("signatures");
          }
          else
          {
            string cause;
            if (!this.FindInjectionInCurrentAssemblies(out cause))
              AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
            else
              this.OnCheatingDetected(cause);
          }
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((Action<string>) null);
    }

    protected override void PauseDetector()
    {
      AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
      base.PauseDetector();
    }

    protected override bool ResumeDetector()
    {
      if (!base.ResumeDetector())
        return false;
      AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
      return true;
    }

    protected override bool DetectorHasCallbacks()
    {
      return base.DetectorHasCallbacks() || this.CheatDetected != null;
    }

    protected override void StopDetectionInternal()
    {
      if (this.started)
        AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
      base.StopDetectionInternal();
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) InjectionDetector.Instance, (Object) this))
        return;
      InjectionDetector.Instance = (InjectionDetector) null;
    }

    private void OnCheatingDetected(string cause)
    {
      if (this.CheatDetected != null)
        this.CheatDetected(cause);
      if (this.detectionEventHasListener)
        this.detectionEvent.Invoke();
      if (this.autoDispose)
        this.DisposeInternal();
      else
        this.StopDetectionInternal();
    }

    private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
    {
      if (this.AssemblyAllowed(args.LoadedAssembly))
        return;
      this.OnCheatingDetected(args.LoadedAssembly.FullName);
    }

    private bool FindInjectionInCurrentAssemblies(out string cause)
    {
      cause = (string) null;
      bool currentAssemblies = false;
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      if (assemblies.Length == 0)
      {
        cause = "no assemblies";
        currentAssemblies = true;
      }
      else
      {
        foreach (Assembly ass in assemblies)
        {
          if (!this.AssemblyAllowed(ass))
          {
            cause = ass.FullName;
            currentAssemblies = true;
            break;
          }
        }
      }
      return currentAssemblies;
    }

    private bool AssemblyAllowed(Assembly ass)
    {
      string name = ass.GetName().Name;
      int assemblyHash = this.GetAssemblyHash(ass);
      bool flag = false;
      for (int index = 0; index < this.allowedAssemblies.Length; ++index)
      {
        InjectionDetector.AllowedAssembly allowedAssembly = this.allowedAssemblies[index];
        if (allowedAssembly.name == name && Array.IndexOf<int>(allowedAssembly.hashes, assemblyHash) != -1)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private void LoadAndParseAllowedAssemblies()
    {
      TextAsset textAsset = (TextAsset) Resources.Load("fndid", typeof (TextAsset));
      if (Object.op_Equality((Object) textAsset, (Object) null))
      {
        this.signaturesAreNotGenuine = true;
      }
      else
      {
        string[] separator = new string[1]{ ":" };
        MemoryStream input = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int length1 = binaryReader.ReadInt32();
        this.allowedAssemblies = new InjectionDetector.AllowedAssembly[length1];
        for (int index1 = 0; index1 < length1; ++index1)
        {
          string str = ObscuredString.EncryptDecrypt(binaryReader.ReadString(), "Elina");
          string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
          int length2 = strArray.Length;
          if (length2 > 1)
          {
            string name = strArray[0];
            int[] hashes = new int[length2 - 1];
            for (int index2 = 1; index2 < length2; ++index2)
            {
              int result = 0;
              if (int.TryParse(strArray[index2], out result))
                hashes[index2 - 1] = result;
              else
                Debug.LogError((object) ("[ACTk] Injection Detector: Could not parse value: " + strArray[index2] + ", line:\n" + str));
            }
            this.allowedAssemblies[index1] = new InjectionDetector.AllowedAssembly(name, hashes);
          }
          else
          {
            this.signaturesAreNotGenuine = true;
            binaryReader.Close();
            input.Close();
            return;
          }
        }
        binaryReader.Close();
        input.Close();
        Resources.UnloadAsset((Object) textAsset);
        this.hexTable = new string[256];
        for (int index = 0; index < 256; ++index)
          this.hexTable[index] = index.ToString("x2");
      }
    }

    private int GetAssemblyHash(Assembly ass)
    {
      AssemblyName name = ass.GetName();
      byte[] publicKeyToken = name.GetPublicKeyToken();
      string str = publicKeyToken.Length < 8 ? name.Name : name.Name + this.PublicKeyTokenToString(publicKeyToken);
      int num1 = 0;
      int length = str.Length;
      for (int index = 0; index < length; ++index)
      {
        int num2 = num1 + (int) str[index];
        int num3 = num2 + (num2 << 10);
        num1 = num3 ^ num3 >> 6;
      }
      int num4 = num1 + (num1 << 3);
      int num5 = num4 ^ num4 >> 11;
      return num5 + (num5 << 15);
    }

    private string PublicKeyTokenToString(byte[] bytes)
    {
      string empty = string.Empty;
      for (int index = 0; index < 8; ++index)
        empty += this.hexTable[(int) bytes[index]];
      return empty;
    }

    private class AllowedAssembly
    {
      public readonly string name;
      public readonly int[] hashes;

      public AllowedAssembly(string name, int[] hashes)
      {
        this.name = name;
        this.hashes = hashes;
      }
    }
  }
}
