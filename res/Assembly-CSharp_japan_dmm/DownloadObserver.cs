// Decompiled with JetBrains decompiler
// Type: DownloadObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class DownloadObserver
{
  private readonly MonoBehaviour _coroutineExecuter;
  private int _retryNumber;
  private Dictionary<string, int> _itemCompressedSize;
  private LinkedList<string> _requestUrlList = new LinkedList<string>();
  private string _prefix;
  private readonly List<DownloadObseve> _downloadObserveList = new List<DownloadObseve>();

  public DownloadObserver(
    MonoBehaviour behavior,
    int parallelNumber,
    int retryNumber,
    DownloadObserver.DownloadCondition downloadCondition)
  {
    this._downloadObserveList = new List<DownloadObseve>(parallelNumber);
    this._retryNumber = retryNumber;
    this._coroutineExecuter = behavior;
    this._downloadCondition = downloadCondition;
  }

  private DownloadObserver.DownloadedAction _downloadedAction { get; set; }

  private DownloadObserver.DownloadingAction _downloadingAction { get; set; }

  private DownloadObserver.ComposeDownloadUriAction _uriAction { get; set; }

  private DownloadObserver.DownloadCondition _downloadCondition { get; set; }

  public bool IsWait { get; set; }

  public bool IsAborting { get; private set; }

  public bool IsFailured { get; private set; }

  public bool IsCompleted { get; private set; }

  public void StartDownloads(
    string prefix,
    List<string> param,
    Dictionary<string, int> itemCompressedSize,
    DownloadObserver.DownloadedAction downloadedAction,
    DownloadObserver.DownloadingAction downloadingAction,
    DownloadObserver.ComposeDownloadUriAction uriAction)
  {
    this._requestUrlList.Clear();
    foreach (string str in param)
      this._requestUrlList.AddLast(str);
    this._prefix = prefix;
    this._itemCompressedSize = itemCompressedSize;
    this._downloadedAction = downloadedAction;
    this._downloadingAction = downloadingAction;
    this._uriAction = uriAction;
    this.IsFailured = false;
    this.IsCompleted = false;
    this.IsAborting = false;
    this._coroutineExecuter.StartCoroutine(this.RunDownloadTask());
  }

  [DebuggerHidden]
  private IEnumerator RunDownloadTask()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObserver.\u003CRunDownloadTask\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  private void AddDownloadObserve()
  {
    DownloadObseve downloadObseve = new DownloadObseve();
    downloadObseve.Failure += new DownloadObseve.FailureHandler(this.FailureEvent);
    downloadObseve.Loading += new DownloadObseve.LoadingHandler(this.DownloadingEvent);
    downloadObseve.Success += new DownloadObseve.SuccessHandler(this.SuccessEvent);
    downloadObseve.CanWrite += new DownloadObseve.CanWriteHandler(this.CanWriteMemoryStreamEvent);
    downloadObseve.SetRetryNumber(this._retryNumber);
    downloadObseve.StartDownload(this._requestUrlList.First.Value, this._coroutineExecuter, this._uriAction(this._prefix, this._requestUrlList.First.Value), this._itemCompressedSize[this._requestUrlList.First.Value]);
    downloadObseve.CompressedSize = this._itemCompressedSize[this._requestUrlList.First.Value];
    this._downloadObserveList.Add(downloadObseve);
    this._requestUrlList.RemoveFirst();
  }

  private bool CanWriteMemoryStreamEvent(int contentsSize)
  {
    if (this.IsWait || contentsSize == 0 || !this._downloadCondition())
      return false;
    if (5242880 >= contentsSize)
      return true;
    return this._downloadObserveList.Count == 1 && AssetDownloader.UnZipFileSize == 0L;
  }

  private void DownloadingEvent(string identificationKey, int loadingSize)
  {
    if (this._downloadingAction == null)
      return;
    this._downloadingAction(identificationKey, loadingSize);
  }

  public List<DownloadObseve.DownloadInfo> GetDownloadInfo()
  {
    List<DownloadObseve.DownloadInfo> downloadInfo1 = new List<DownloadObseve.DownloadInfo>();
    foreach (DownloadObseve downloadObserve in this._downloadObserveList)
    {
      DownloadObseve.DownloadInfo downloadInfo2 = downloadObserve.getDownloadInfo();
      downloadInfo1.Add(downloadInfo2);
    }
    return downloadInfo1;
  }

  public void Abort()
  {
    if (this.IsAborting)
      return;
    this.IsAborting = true;
    if (Object.op_Inequality((Object) this._coroutineExecuter, (Object) null) && ((Behaviour) this._coroutineExecuter).isActiveAndEnabled)
      this._coroutineExecuter.StartCoroutine(this.AllStopObserve());
    else
      this.IsAborting = false;
  }

  private void FailureEvent(string errorText) => this.Abort();

  [DebuggerHidden]
  private IEnumerator AllStopObserve()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObserver.\u003CAllStopObserve\u003Ec__Iterator1()
    {
      \u0024this = this
    };
  }

  private void SuccessEvent(
    DownloadObseve observe,
    string identificationKey,
    byte[] bytes,
    int size,
    double downloadTime)
  {
    if (!this.IsFailured && !this.IsAborting)
    {
      this._downloadedAction(identificationKey, bytes, size, downloadTime);
      if (5242880 >= size)
        ;
      if (this._downloadObserveList.Contains(observe))
        this._downloadObserveList.Remove(observe);
    }
    observe?.Dispose();
    observe = (DownloadObseve) null;
  }

  public bool IsDone() => this.IsCompleted || this.IsFailured;

  private bool IsAllDownloaded()
  {
    return !this.IsFailured && !this.IsAborting && this._requestUrlList.Count == 0 && this._downloadObserveList.Count == 0;
  }

  public void Dispose()
  {
    for (int index = 0; index < this._downloadObserveList.Count; ++index)
    {
      this._downloadObserveList[index].Dispose();
      this._downloadObserveList[index] = (DownloadObseve) null;
    }
    this._downloadObserveList.Clear();
  }

  public delegate void DownloadingAction(string identificationKey, int loadingSize);

  public delegate void DownloadedAction(
    string identificationKey,
    byte[] bytes,
    int size,
    double downloadTime);

  public delegate string ComposeDownloadUriAction(string prefix, string url);

  public delegate bool DownloadCondition();
}
