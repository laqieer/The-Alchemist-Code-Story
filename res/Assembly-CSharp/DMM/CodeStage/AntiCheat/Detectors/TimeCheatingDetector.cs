// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.TimeCheatingDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#nullable disable
namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Time Cheating Detector")]
  [DisallowMultipleComponent]
  [HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_time_cheating_detector.html")]
  public class TimeCheatingDetector : ACTkDetectorBase
  {
    internal const string ComponentName = "Time Cheating Detector";
    private const string LogPrefix = "[ACTk] Time Cheating Detector: ";
    private const int DefaultTimeoutSeconds = 10;
    private static readonly WaitForEndOfFrame cachedEndOfFrame = new WaitForEndOfFrame();
    private static int instancesInScene;
    private static bool gettingOnlineTime;
    [Header("Request settings")]
    [Tooltip("Absolute URL which will return correct datetime in response headers (you may use popular web servers like google.com, microsoft.com etc.).")]
    [SerializeField]
    private string requestUrl = "https://google.com";
    [Tooltip("Method to use for url request. Use Head method if possible and fall back to get if server does not reply or block head requests.")]
    public TimeCheatingDetector.RequestMethod requestMethod;
    [Tooltip("Online time request timeout in seconds.")]
    public int timeoutSeconds = 10;
    [Header("Settings in minutes")]
    [Tooltip("Time (in minutes) between detector checks.")]
    [Range(0.0f, 60f)]
    public float interval = 5f;
    [Tooltip("Maximum allowed difference between subsequent measurements, in minutes.")]
    [FormerlySerializedAs("threshold")]
    [Range(10f, 180f)]
    public int realCheatThreshold = 65;
    [Tooltip("Maximum allowed difference between local and online time, in minutes.")]
    [Range(1f, 180f)]
    public int wrongTimeThreshold = 65;
    private readonly string onlineOfflineDifferencePrefsKey = ObscuredString.EncryptDecrypt("onlineOfflineSecondsDifference", "TeslaOnMars");
    private Uri cachedUri;
    private TimeCheatingDetector.TimeCheatingDetectorEventHandler cheatChecked;
    private float timeElapsed;
    private bool updateAfterPause;
    private double lastOnlineSecondsUtc;
    [Obsolete("Use wrongTimeThreshold instead.", true)]
    [NonSerialized]
    public int threshold = 65;
    [Obsolete("Use requestUrl instead", true)]
    [NonSerialized]
    public string timeServer = "google.com";

    public event TimeCheatingDetector.TimeCheatingDetectorEventHandler CheatChecked;

    public string RequestUrl
    {
      get => this.requestUrl;
      set
      {
        if (this.requestUrl == value || !Application.isPlaying)
          return;
        this.requestUrl = value;
        this.cachedUri = TimeCheatingDetector.UrlToUri(this.requestUrl);
      }
    }

    public TimeCheatingDetector.ErrorKind LastError { get; private set; }

    public TimeCheatingDetector.CheckResult LastResult { get; private set; }

    public bool IsCheckingForCheat { get; private set; }

    public static TimeCheatingDetector Instance { get; private set; }

    private static TimeCheatingDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) TimeCheatingDetector.Instance, (Object) null))
          return TimeCheatingDetector.Instance;
        if (Object.op_Equality((Object) ACTkDetectorBase.detectorsContainer, (Object) null))
          ACTkDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        TimeCheatingDetector.Instance = ACTkDetectorBase.detectorsContainer.AddComponent<TimeCheatingDetector>();
        return TimeCheatingDetector.Instance;
      }
    }

    private void Awake()
    {
      ++TimeCheatingDetector.instancesInScene;
      // ISSUE: method pointer
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
      if (!this.Init((ACTkDetectorBase) TimeCheatingDetector.Instance, "Time Cheating Detector"))
        return;
      TimeCheatingDetector.Instance = this;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      // ISSUE: method pointer
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>((object) this, __methodptr(OnLevelWasLoadedNew));
      --TimeCheatingDetector.instancesInScene;
    }

    private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
    {
      if (TimeCheatingDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) TimeCheatingDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
      if (!this.started)
        return;
      if (pauseStatus)
        this.PauseDetector();
      else
        this.ResumeDetector();
    }

    private void Update()
    {
      if (!this.started || !this.isRunning || (double) this.interval <= 0.0)
        return;
      if (this.updateAfterPause)
      {
        this.updateAfterPause = false;
      }
      else
      {
        this.timeElapsed += Time.unscaledDeltaTime;
        if ((double) this.timeElapsed < (double) this.interval * 60.0)
          return;
        this.timeElapsed = 0.0f;
        this.StartCoroutine(this.CheckForCheat());
      }
    }

    public static TimeCheatingDetector AddToSceneOrGetExisting()
    {
      return TimeCheatingDetector.GetOrCreateInstance;
    }

    public static void StartDetection(
      TimeCheatingDetector.TimeCheatingDetectorEventHandler cheatCheckedEventHandler = null)
    {
      if (cheatCheckedEventHandler == null)
      {
        if (Object.op_Inequality((Object) TimeCheatingDetector.Instance, (Object) null))
          TimeCheatingDetector.Instance.StartDetectionInternal(TimeCheatingDetector.Instance.interval);
        else
          TimeCheatingDetector.StartDetection(TimeCheatingDetector.GetOrCreateInstance.interval);
      }
      else
        TimeCheatingDetector.StartDetection(TimeCheatingDetector.GetOrCreateInstance.interval, cheatCheckedEventHandler);
    }

    public static void StartDetection(
      float interval,
      TimeCheatingDetector.TimeCheatingDetectorEventHandler cheatCheckedEventHandler = null)
    {
      TimeCheatingDetector.GetOrCreateInstance.StartDetectionInternal(interval, cheatCheckedEventHandler);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) TimeCheatingDetector.Instance, (Object) null))
        return;
      TimeCheatingDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) TimeCheatingDetector.Instance, (Object) null))
        return;
      TimeCheatingDetector.Instance.DisposeInternal();
    }

    [DebuggerHidden]
    public static IEnumerator GetOnlineTimeCoroutine(
      string url,
      TimeCheatingDetector.OnlineTimeCallback callback,
      TimeCheatingDetector.RequestMethod method = TimeCheatingDetector.RequestMethod.Head)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TimeCheatingDetector.\u003CGetOnlineTimeCoroutine\u003Ec__Iterator0()
      {
        url = url,
        callback = callback,
        method = method
      };
    }

    [DebuggerHidden]
    public static IEnumerator GetOnlineTimeCoroutine(
      Uri uri,
      TimeCheatingDetector.OnlineTimeCallback callback,
      TimeCheatingDetector.RequestMethod method = TimeCheatingDetector.RequestMethod.Head)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TimeCheatingDetector.\u003CGetOnlineTimeCoroutine\u003Ec__Iterator1()
      {
        uri = uri,
        method = method,
        callback = callback
      };
    }

    private static UnityWebRequest GetWebRequest(Uri uri, TimeCheatingDetector.RequestMethod method)
    {
      return new UnityWebRequest(uri, method != TimeCheatingDetector.RequestMethod.Head ? "GET" : "HEAD")
      {
        useHttpContinue = false,
        timeout = !Object.op_Implicit((Object) TimeCheatingDetector.Instance) ? 10 : TimeCheatingDetector.Instance.timeoutSeconds,
        certificateHandler = (CertificateHandler) null
      };
    }

    private static void FillRequestResult(
      UnityWebRequest request,
      ref TimeCheatingDetector.OnlineTimeResult result)
    {
      if (!string.IsNullOrEmpty(request.error))
      {
        result.SetError(request.error, request.responseCode);
      }
      else
      {
        string responseHeader = request.GetResponseHeader("Date");
        if (!string.IsNullOrEmpty(responseHeader))
        {
          DateTime date;
          if (TimeCheatingDetector.TryGetDate(responseHeader, out date))
          {
            double secondsUtc = (double) date.ToUniversalTime().Ticks / 10000000.0;
            result.SetTime(secondsUtc);
          }
          else
            result.SetError("Couldn't parse 'Date' response header value\n " + responseHeader, request.responseCode);
        }
        else
          result.SetError("Couldn't find 'Date' response header value!", request.responseCode);
      }
      if (result.success)
        return;
      Debug.Log((object) ("[ACTk] Time Cheating Detector: Online Time Retrieve error:\n" + (object) result));
    }

    private static Uri UrlToUri(string url)
    {
      Uri result;
      if (!Uri.TryCreate(url, UriKind.Absolute, out result))
        Debug.LogError((object) ("[ACTk] Time Cheating Detector: Could not create URI from URL: " + url));
      return result;
    }

    private static bool TryGetDate(string source, out DateTime date)
    {
      try
      {
        char[] charArray = source.ToCharArray(5, 20);
        int day = ((int) charArray[0] & 15) * 10 + ((int) charArray[1] & 15);
        int num = (int) charArray[4] + (int) charArray[5];
        int month;
        switch (num)
        {
          case 211:
            month = 3;
            break;
          case 213:
            month = 9;
            break;
          case 215:
            month = 10;
            break;
          case 218:
            month = 5;
            break;
          case 220:
            month = 8;
            break;
          default:
            switch (num - 225)
            {
              case 0:
                month = 7;
                break;
              case 1:
                month = 4;
                break;
              case 2:
                month = 6;
                break;
              case 4:
                month = 11;
                break;
              default:
                month = num == 199 ? 2 : (num == 200 ? 12 : (num == 207 ? 1 : 1));
                break;
            }
            break;
        }
        int year = ((int) charArray[7] & 15) * 1000 + ((int) charArray[8] & 15) * 100 + ((int) charArray[9] & 15) * 10 + ((int) charArray[10] & 15);
        int hour = ((int) charArray[12] & 15) * 10 + ((int) charArray[13] & 15);
        int minute = ((int) charArray[15] & 15) * 10 + ((int) charArray[16] & 15);
        int second = ((int) charArray[18] & 15) * 10 + ((int) charArray[19] & 15);
        date = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        return true;
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("[ACTk] Time Cheating Detector: Error while parsing date: " + (object) ex));
        date = new DateTime();
        return false;
      }
    }

    public bool ForceCheck()
    {
      if (!this.started || !this.isRunning)
      {
        Debug.LogWarning((object) "[ACTk] Time Cheating Detector: Detector should be started to use ForceCheck().");
        this.LastError = TimeCheatingDetector.ErrorKind.NotStarted;
        this.LastResult = TimeCheatingDetector.CheckResult.Error;
        return false;
      }
      if (this.IsCheckingForCheat)
      {
        Debug.LogWarning((object) "[ACTk] Time Cheating Detector: Can't force cheating check since another check is already in progress.");
        this.LastError = TimeCheatingDetector.ErrorKind.AlreadyCheckingForCheat;
        this.LastResult = TimeCheatingDetector.CheckResult.Error;
        return false;
      }
      this.timeElapsed = 0.0f;
      this.StartCoroutine(this.CheckForCheat());
      return true;
    }

    [DebuggerHidden]
    public IEnumerator ForceCheckEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TimeCheatingDetector.\u003CForceCheckEnumerator\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }

    private void StartDetectionInternal(
      float checkInterval,
      TimeCheatingDetector.TimeCheatingDetectorEventHandler cheatCheckedEventHandler = null)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Time Cheating Detector: already running!", (Object) this);
      else if (!((Behaviour) this).enabled)
      {
        Debug.LogWarning((object) "[ACTk] Time Cheating Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (cheatCheckedEventHandler != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Time Cheating Detector: has properly configured Detection Event in the inspector, but still get started with TimeCheatingDetectorCallback callback. Both TimeCheatingDetectorCallback and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        this.timeElapsed = 0.0f;
        this.cheatChecked = cheatCheckedEventHandler;
        this.interval = checkInterval;
        this.started = true;
        this.isRunning = true;
      }
    }

    protected override bool Init(ACTkDetectorBase instance, string detectorName)
    {
      if (this.cachedUri == (Uri) null)
        this.cachedUri = TimeCheatingDetector.UrlToUri(this.requestUrl);
      return base.Init(instance, detectorName);
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal(this.interval);
    }

    protected override bool DetectorHasCallbacks()
    {
      return base.DetectorHasCallbacks() || this.CheatChecked != null || this.cheatChecked != null;
    }

    protected override void PauseDetector()
    {
      base.PauseDetector();
      this.updateAfterPause = true;
    }

    protected override void StopDetectionInternal()
    {
      base.StopDetectionInternal();
      this.cheatChecked = (TimeCheatingDetector.TimeCheatingDetectorEventHandler) null;
      this.CheatChecked = (TimeCheatingDetector.TimeCheatingDetectorEventHandler) null;
    }

    protected override void DisposeInternal()
    {
      if (Object.op_Equality((Object) TimeCheatingDetector.Instance, (Object) this))
        TimeCheatingDetector.Instance = (TimeCheatingDetector) null;
      base.DisposeInternal();
    }

    [DebuggerHidden]
    private IEnumerator CheckForCheat()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TimeCheatingDetector.\u003CCheckForCheat\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    private void ReportCheckResult()
    {
      if (this.cheatChecked != null)
        this.cheatChecked(this.LastResult, this.LastError);
      if (this.CheatChecked != null)
        this.CheatChecked(this.LastResult, this.LastError);
      switch (this.LastResult)
      {
        case TimeCheatingDetector.CheckResult.Unknown:
          break;
        case TimeCheatingDetector.CheckResult.CheckPassed:
          break;
        case TimeCheatingDetector.CheckResult.WrongTimeDetected:
          break;
        case TimeCheatingDetector.CheckResult.CheatDetected:
          this.OnCheatingDetected();
          break;
        case TimeCheatingDetector.CheckResult.Error:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnOnlineTimeReceived(TimeCheatingDetector.OnlineTimeResult result)
    {
      if (result.success)
      {
        this.lastOnlineSecondsUtc = result.onlineSecondsUtc;
      }
      else
      {
        this.lastOnlineSecondsUtc = -1.0;
        this.LastError = TimeCheatingDetector.ErrorKind.OnlineTimeError;
      }
    }

    private double GetLocalSecondsUtc() => (double) DateTime.UtcNow.Ticks / 10000000.0;

    [Obsolete("Please use CheatChecked event instead", true)]
    public event Action<TimeCheatingDetector.ErrorKind> Error;

    [Obsolete("Please use CheatChecked event instead", true)]
    public event Action CheckPassed;

    [Obsolete("Please use GetOnlineTimeCoroutine or GetOnlineTimeTask instead", true)]
    public static double GetOnlineTime(string server) => -1.0;

    [Obsolete("Please use Instance.Error event instead.", true)]
    public static void SetErrorCallback(
      Action<TimeCheatingDetector.ErrorKind> errorCallback)
    {
    }

    [Obsolete("Please use StartDetection(int, ...) instead.", true)]
    public static void StartDetection(Action detectionCallback, int interval)
    {
    }

    [Obsolete("Please use StartDetection(int, ...) instead.", true)]
    public static void StartDetection(
      Action detectionCallback,
      Action<TimeCheatingDetector.ErrorKind> errorCallback,
      int interval)
    {
    }

    [Obsolete("Please use other overloads of this method instead", true)]
    public static void StartDetection(
      float interval,
      Action detectionCallback,
      Action<TimeCheatingDetector.ErrorKind> errorCallback,
      Action checkPassedCallback)
    {
    }

    public delegate void OnlineTimeCallback(TimeCheatingDetector.OnlineTimeResult result);

    public delegate void TimeCheatingDetectorEventHandler(
      TimeCheatingDetector.CheckResult checkResult,
      TimeCheatingDetector.ErrorKind errorKind);

    public struct OnlineTimeResult
    {
      public bool success;
      public string error;
      public long errorResponseCode;
      public double onlineSecondsUtc;

      internal void SetTime(double secondsUtc)
      {
        this.success = true;
        this.error = (string) null;
        this.errorResponseCode = -1L;
        this.onlineSecondsUtc = secondsUtc;
      }

      internal void SetError(string errorText, long responseCode = -1)
      {
        this.success = false;
        this.error = errorText;
        this.errorResponseCode = responseCode;
        this.onlineSecondsUtc = -1.0;
      }

      public override string ToString()
      {
        if (this.success)
          return "onlineSecondsUtc: " + (object) this.onlineSecondsUtc;
        return "Error response code: " + (object) this.errorResponseCode + "\nError: " + this.error;
      }
    }

    public enum CheckResult
    {
      Unknown = 0,
      CheckPassed = 5,
      WrongTimeDetected = 10, // 0x0000000A
      CheatDetected = 15, // 0x0000000F
      Error = 100, // 0x00000064
    }

    public enum ErrorKind
    {
      NoError = 0,
      IncorrectUri = 3,
      OnlineTimeError = 5,
      NotStarted = 10, // 0x0000000A
      AlreadyCheckingForCheat = 15, // 0x0000000F
      Unknown = 100, // 0x00000064
    }

    public enum RequestMethod
    {
      Head,
      Get,
    }
  }
}
