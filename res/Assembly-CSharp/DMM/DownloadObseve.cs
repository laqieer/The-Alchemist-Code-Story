// Decompiled with JetBrains decompiler
// Type: DownloadObseve
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class DownloadObseve
{
  private IEnumerator _downloadEnumerator;
  private DLCDownloadClient p_downloadClient;
  private bool instantiated;
  private MonoBehaviour _coroutineExecuter;
  public int CompressedSize;

  public DownloadObseve()
  {
    this.State = DownloadObseve.StateEnum.IsRunning;
    this.State = DownloadObseve.StateEnum.IsNone;
  }

  public event DownloadObseve.FailureHandler Failure = _param0 => { };

  public event DownloadObseve.LoadingHandler Loading = (_param0, _param1) => { };

  public event DownloadObseve.SuccessHandler Success = (_param0, _param1, _param2, _param3, _param4) => { };

  public event DownloadObseve.RetryHandler Retry = () => { };

  public event DownloadObseve.CanWriteHandler CanWrite = _param0 => false;

  private int _size { get; set; }

  private string _url { get; set; }

  private string _identificationKey { get; set; }

  private DLCDownloadClient _downloadClient
  {
    get
    {
      if (!this.instantiated && this.p_downloadClient == null)
      {
        this.instantiated = true;
        this.p_downloadClient = !Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_DLC_DOWNLOAD_OLD) ? (DLCDownloadClient) new DownloaderClient() : (DLCDownloadClient) new HttpClient();
      }
      return this.p_downloadClient;
    }
  }

  public int RetryCount { get; private set; }

  public int MaxRetryNumber { get; private set; }

  public int ContentsSize => this._downloadClient.ContentLength;

  public bool CanWriteStream => this._downloadClient.CanWriteStream;

  public DownloadObseve.StateEnum State { get; private set; }

  public void SetRetryNumber(int retryNumber) => this.MaxRetryNumber = retryNumber;

  public void StartDownload(
    string identificationKey,
    MonoBehaviour behabior,
    string url,
    int size)
  {
    this._identificationKey = identificationKey;
    this._url = url;
    this._size = size;
    this.State = DownloadObseve.StateEnum.IsRunning;
    this._coroutineExecuter = behabior;
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this.Download();
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  [DebuggerHidden]
  private IEnumerator Download()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObseve.\u003CDownload\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  public DownloadObseve.DownloadInfo getDownloadInfo()
  {
    DownloadObseve.DownloadInfo downloadInfo;
    downloadInfo.identificationKey = this._identificationKey;
    if (this._downloadClient != null)
    {
      downloadInfo.position = (long) this._downloadClient.LoadingSize;
      downloadInfo.ContentLength = this._downloadClient.ContentLength;
    }
    else
    {
      downloadInfo.position = 0L;
      downloadInfo.ContentLength = 0;
    }
    return downloadInfo;
  }

  private void RetryDownload()
  {
    Debug.Log((object) ("download retry[" + (object) this.RetryCount + "]..."));
    ++this.RetryCount;
    this.Retry();
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this.Download();
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  private bool CanRetryDownload() => this.RetryCount < this.MaxRetryNumber;

  [DebuggerHidden]
  public IEnumerator Abort()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObseve.\u003CAbort\u003Ec__Iterator1()
    {
      \u0024this = this
    };
  }

  public void Dispose()
  {
    if (this._downloadClient != null)
      this._downloadClient.Dispose();
    this.p_downloadClient = (DLCDownloadClient) null;
    this._url = (string) null;
    this._identificationKey = (string) null;
  }

  public enum StateEnum
  {
    IsRunning,
    IsCompleted,
    IsFailureFinished,
    IsDestroying,
    IsDestroyed,
    IsNone,
  }

  public delegate void FailureHandler(string errorText);

  public delegate void LoadingHandler(string identificationKey, int loadingSize);

  public delegate void SuccessHandler(
    DownloadObseve observe,
    string identificationKey,
    byte[] bytes,
    int size,
    double downloadTime);

  public delegate bool CanWriteHandler(int contentsSize);

  public delegate void RetryHandler();

  public struct DownloadInfo
  {
    public string identificationKey;
    public long position;
    public int ContentLength;
  }
}
