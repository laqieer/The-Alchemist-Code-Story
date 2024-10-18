// Decompiled with JetBrains decompiler
// Type: AssetDownloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class AssetDownloader : MonoBehaviour
{
  public static string DownloadURL = string.Empty;
  public static string StreamingURL = string.Empty;
  public static string ExDownloadURL = string.Empty;
  public const string FileListName = "ASSETS";
  public readonly string FileAssetListName = "ASSETLIST";
  public readonly string FileExistName = "EXISTLIST";
  public readonly string FileUnManagedExistName = "UNMANAGEDEXISTLIST";
  public readonly string FileUnManagedAssetListName = "UnManagedAssetList";
  public readonly int SaveExistFileSize = 10485760;
  public readonly int SaveExistFileNum = 100;
  private const string MetaExt = ".meta";
  private static AssetDownloader mInstance;
  private static List<string> mRequestIDs = new List<string>();
  private static List<string> mRequestUnmanagedAssets = new List<string>();
  private static Dictionary<string, AssetList.Item> mRequestItems = new Dictionary<string, AssetList.Item>();
  private static Dictionary<string, UnManagedAssetList.Item> mRequestUnmanagedItems = new Dictionary<string, UnManagedAssetList.Item>();
  private Dictionary<string, int> itemCompressedSize = new Dictionary<string, int>();
  private static Coroutine mCoroutine;
  private static bool mHasError;
  private static bool mRetryOnError;
  private static float mDownloadProgress = 0.0f;
  private static long totalDownloadSize = 0;
  private static long currentDownloadSize = 0;
  private static long downloadedSize = 0;
  private static float mUnManagedDownloadProgress = 0.0f;
  private static long mUnManagedTotalDownloadSize = 0;
  private static long mUnManagedCurrentDownloadSize = 0;
  private static long mUnManagedDownloadedSize = 0;
  public const int maxRetryCount = 5;
  public const int SIZE_MB = 1048576;
  private float[] mSpeedHistory = new float[128];
  private int mSpeedHistorySize;
  private int mSpeedHistoryPos;
  private static float mAverageDownloadSpeed = 1048576f;
  private Thread mUnzipThread;
  private Mutex mMutex;
  private AutoResetEvent mUnzipSignal;
  private bool mMutexAcquired;
  private bool mShuttingDown;
  private List<AssetDownloader.UnzipJob> mUnzipJobs = new List<AssetDownloader.UnzipJob>();
  private AssetDownloader.UnzipThread2Arg mUnzipThreadArg;
  private Thread mCompareHashThread;
  private Mutex mCompareHashMutex;
  private float mCompareHashProgressShared;
  private static float mCompareHashProgress;
  private const int FileSizeRequestLimit = 10;
  private const int ConnectionSteamLimit = 10;
  public const int LimitDownloadSize = 5242880;
  public static long UnZipFileSize = 0;
  private const int UnZipRequestJobCapacity = 10;
  private DownloadObserver mDownloadObserver;
  public int mExistFileDownloadSize;
  public int mExistFileDownloadCount;
  public static List<AssetDownloader.ExistAssetList> mExistFile = new List<AssetDownloader.ExistAssetList>();
  public static string mExitFilePath = string.Empty;
  public static List<string> mUnmanagedExistFile = new List<string>();
  private static AssetDownloader.DownloadPhases mPhase;
  private bool mIsUnzipNow;
  private string mLog;
  public static bool BatchDownload = true;
  private bool mWWWError;
  private static string mCachePath;
  private static string mDemoCachePath;
  private static List<AssetDownloader.FileSizeRequest> m_FileSizeRequest = new List<AssetDownloader.FileSizeRequest>();
  private static bool ApproveAssetDownload = false;
  private static List<AssetList.Item> CachedDownloadRequests = (List<AssetList.Item>) null;
  private static List<KeyValuePair<string, int>> CachedUnmanagedDownloadRequests = (List<KeyValuePair<string, int>>) null;

  public static float AverageDownloadSpeed => AssetDownloader.mAverageDownloadSpeed;

  public static void Reset()
  {
    AssetDownloader.ResetDownloadRequestApproved();
    AssetDownloader.mHasError = false;
    AssetDownloader.mCoroutine = (Coroutine) null;
    AssetDownloader.mRequestIDs.Clear();
    AssetDownloader.mRequestItems.Clear();
    AssetDownloader.mRequestUnmanagedAssets.Clear();
    AssetDownloader.mRequestUnmanagedItems.Clear();
    AssetDownloader.CachedDownloadRequests = (List<AssetList.Item>) null;
    AssetDownloader.CachedUnmanagedDownloadRequests = (List<KeyValuePair<string, int>>) null;
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) AssetDownloader.mInstance, (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) AssetDownloader.mInstance).gameObject);
    AssetDownloader.mInstance = (AssetDownloader) null;
  }

  public static bool HasError => AssetDownloader.mHasError;

  private static AssetDownloader Instance
  {
    get
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) AssetDownloader.mInstance, (UnityEngine.Object) null))
      {
        GameObject gameObject = new GameObject(typeof (AssetDownloader).Name, new System.Type[1]
        {
          typeof (AssetDownloader)
        });
        AssetDownloader.mInstance = gameObject.GetComponent<AssetDownloader>();
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
      }
      return AssetDownloader.mInstance;
    }
  }

  public static void SetBaseDownloadURL(string url)
  {
  }

  private void Awake()
  {
    this.mMutex = new Mutex();
    this.mMutex.WaitOne();
    this.mMutexAcquired = true;
    this.mUnzipSignal = new AutoResetEvent(false);
    AssetDownloader.mExitFilePath = AssetDownloader.CachePath + this.FileExistName;
  }

  public static void ResetExistFilePath()
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) AssetDownloader.mInstance, (UnityEngine.Object) null))
      return;
    AssetDownloader.mExitFilePath = AssetDownloader.CachePath + AssetDownloader.mInstance.FileExistName;
  }

  private void Shutdown()
  {
    if (this.mDownloadObserver != null)
      this.mDownloadObserver.Abort();
    if (this.mMutex == null)
      return;
    if (!this.mMutexAcquired)
      this.mMutex.WaitOne();
    this.mShuttingDown = true;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    AssetDownloader.mDownloadProgress = 0.0f;
    AssetDownloader.currentDownloadSize = 0L;
    AssetDownloader.totalDownloadSize = 0L;
    AssetDownloader.downloadedSize = 0L;
    AssetDownloader.mPhase = AssetDownloader.DownloadPhases.FileCheck;
    AssetDownloader.mCompareHashProgress = 0.0f;
    this.mCompareHashProgressShared = 0.0f;
    this.mUnzipSignal.Set();
    if (this.mUnzipThread != null)
    {
      this.mUnzipThread.Join();
      this.mUnzipThread = (Thread) null;
    }
    this.mMutex.Close();
    this.mMutex = (Mutex) null;
    this.mUnzipSignal.Close();
    this.mUnzipSignal = (AutoResetEvent) null;
    if (this.mCompareHashThread != null)
    {
      this.mCompareHashThread.Join();
      this.mCompareHashThread = (Thread) null;
    }
    if (this.mCompareHashMutex == null)
      return;
    this.mCompareHashMutex.Close();
    this.mCompareHashMutex = (Mutex) null;
  }

  private void OnDestroy() => this.Shutdown();

  private void OnApplicationQuit() => this.Shutdown();

  private void SetError(Network.EErrCode code, string textID)
  {
    Network.ErrCode = code;
    Network.ErrMsg = LocalizedText.Get(textID);
    Network.ResetError();
    GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    if (AssetDownloader.mCoroutine != null)
    {
      this.StopCoroutine(AssetDownloader.mCoroutine);
      AssetDownloader.mCoroutine = (Coroutine) null;
    }
    AssetDownloader.mHasError = true;
    if (this.mUnzipThread == null)
      return;
    if (!this.mMutexAcquired)
    {
      this.mMutex.WaitOne();
      this.mMutexAcquired = true;
    }
    if (this.mUnzipThreadArg != null)
      this.mUnzipThreadArg.abort = true;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    this.mUnzipSignal.Set();
    this.mUnzipThread.Join();
    this.mUnzipThread = (Thread) null;
  }

  private void UnzipThread2()
  {
    AssetDownloader.UnzipThread2Arg mUnzipThreadArg = this.mUnzipThreadArg;
    if (mUnzipThreadArg == null)
      return;
    bool flag = false;
    while (!flag)
    {
      this.mMutex.WaitOne();
      AssetDownloader.UnzipJob[] array = this.mUnzipJobs.ToArray();
      this.mUnzipJobs.Clear();
      bool completed = mUnzipThreadArg.completed;
      bool abort = mUnzipThreadArg.abort;
      bool mShuttingDown = this.mShuttingDown;
      this.mMutex.ReleaseMutex();
      if (mShuttingDown || abort)
      {
        this.mIsUnzipNow = false;
        return;
      }
      if (array == null || array.Length <= 0)
      {
        this.mIsUnzipNow = false;
        if (completed)
          return;
        this.mUnzipSignal.WaitOne();
      }
      else
      {
        this.mIsUnzipNow = true;
        AssetDownloader.UnzipJob unzipJob = (AssetDownloader.UnzipJob) null;
        for (int index = 0; index < array.Length; ++index)
        {
          unzipJob = array[index];
          if (unzipJob.State == AssetDownloader.UnzipJobState.Error)
          {
            flag = true;
            break;
          }
          try
          {
            if (array != null)
              AssetDownloader.UnZipFileSize = (long) ((IEnumerable<AssetDownloader.UnzipJob>) array).Sum<AssetDownloader.UnzipJob>((Func<AssetDownloader.UnzipJob, int>) (arg => arg.Size));
          }
          catch (Exception ex)
          {
            Debug.Log((object) ex.ToString());
          }
          string path = AssetDownloader.CachePath + unzipJob.nodes[0].ID;
          File.WriteAllBytes(path, unzipJob.Bytes);
          if (!File.Exists(path))
          {
            flag = true;
            break;
          }
          unzipJob.nodes[0].Item.Exist = true;
          AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(unzipJob.nodes[0].Item.ID, unzipJob.nodes[0].Item.Index));
          this.mExistFileDownloadSize += unzipJob.nodes[0].Item.Size;
          ++this.mExistFileDownloadCount;
          if (this.mExistFileDownloadSize > this.SaveExistFileSize || this.mExistFileDownloadCount > this.SaveExistFileNum)
          {
            this.mExistFileDownloadSize = 0;
            this.mExistFileDownloadCount = 0;
            this.CreateExistFile();
          }
        }
        try
        {
          AssetDownloader.UnZipFileSize = 0L;
          if (this.mDownloadObserver != null)
            this.mDownloadObserver.IsWait = false;
        }
        catch (Exception ex)
        {
          Debug.Log((object) ex.ToString());
        }
        if (flag && unzipJob != null)
        {
          for (int index = 0; index < unzipJob.nodes.Count; ++index)
          {
            string path = unzipJob.Dest + unzipJob.nodes[index].ID;
            if (File.Exists(path))
              File.Delete(path);
            unzipJob.nodes[index].Item.Exist = false;
          }
        }
      }
    }
    this.mMutex.WaitOne();
    mUnzipThreadArg.error = true;
    this.mIsUnzipNow = false;
    this.mMutex.ReleaseMutex();
  }

  private static long GetFileSize(string path)
  {
    try
    {
      return new FileInfo(path).Length;
    }
    catch (Exception ex)
    {
      return 0;
    }
  }

  private void CompareFileListHashThread(object args)
  {
    if (!(args is AssetDownloader.CompareFileListHashArg compareFileListHashArg) || string.IsNullOrEmpty(compareFileListHashArg.cacheDir) || compareFileListHashArg.nodes == null)
      return;
    string cacheDir = compareFileListHashArg.cacheDir;
    if (compareFileListHashArg.dic.Count == 0)
    {
      for (int index = 0; index < compareFileListHashArg.nodes.Count; ++index)
      {
        this.mCompareHashMutex.WaitOne();
        this.mCompareHashProgressShared = (float) index / (float) compareFileListHashArg.nodes.Count;
        this.mCompareHashMutex.ReleaseMutex();
        AssetDownloader.CompareFileListHashArg.Node node = compareFileListHashArg.nodes[index];
        if (node != null && !string.IsNullOrEmpty(node.IDStr) && !string.IsNullOrEmpty(node.metaPath))
        {
          string path = cacheDir + node.IDStr;
          string metaPath = node.metaPath;
          bool flag1 = File.Exists(path);
          bool flag2 = false;
          if (File.Exists(metaPath))
          {
            if (flag1)
            {
              if (AssetDownloader.GetFileSize(path) != (long) node.Size)
              {
                flag2 = true;
                goto label_17;
              }
            }
            try
            {
              using (FileStream input = new FileStream(metaPath, FileMode.Open))
              {
                uint num = new BinaryReader((Stream) input).ReadUInt32();
                if ((int) node.Hash != (int) num)
                  flag2 = true;
              }
            }
            catch (Exception ex)
            {
              flag2 = true;
            }
label_17:
            if (flag2)
              File.Delete(metaPath);
          }
          if (flag1 && AssetDownloader.mExistFile.Find((Predicate<AssetDownloader.ExistAssetList>) (x => (int) x.FileID == (int) node.Item.ID)) != null)
            node.Item.Exist = true;
        }
      }
    }
    else
    {
      AssetDownloader.ExistAssetList[] array = AssetDownloader.mExistFile.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        this.mCompareHashMutex.WaitOne();
        this.mCompareHashProgressShared = (float) index / (float) array.Length;
        this.mCompareHashMutex.ReleaseMutex();
        bool flag = false;
        AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(array[index].FileID);
        AssetList.Item obj;
        if (itemById != null && compareFileListHashArg.dic.TryGetValue(array[index].FileID, out obj))
        {
          if (flag | itemById.Size != obj.Size | (int) itemById.Hash != (int) obj.Hash)
            AssetDownloader.mExistFile.Remove(array[index]);
          else
            itemById.Exist = true;
        }
      }
    }
  }

  public static void Add(string assetID)
  {
    if (!AssetManager.UseDLC)
      return;
    AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(assetID);
    if (AssetDownloader.mRequestItems.ContainsKey(assetID))
      return;
    if (AssetDownloader.ApproveAssetDownload)
      itemById.SetDownloadApproved(true);
    else
      itemById.SetDownloadCaller();
    AssetDownloader.mRequestIDs.Add(assetID);
    AssetDownloader.mRequestItems.Add(assetID, itemById);
    AssetDownloader.CachedDownloadRequests = (List<AssetList.Item>) null;
  }

  public static bool isDone => AssetDownloader.Instance.Internal_isDone;

  public static bool isManagedAssetsDone
  {
    get => AssetDownloader.mCoroutine == null && AssetDownloader.mRequestIDs.Count == 0;
  }

  public static bool isUnManagedAssetsDone
  {
    get => AssetDownloader.mCoroutine == null && AssetDownloader.mRequestUnmanagedAssets.Count == 0;
  }

  public static bool HasUnManagedAssetRequest => AssetDownloader.mRequestUnmanagedAssets.Count > 0;

  private bool Internal_isDone
  {
    get
    {
      return AssetDownloader.mCoroutine == null && AssetDownloader.mRequestIDs.Count == 0 && AssetDownloader.mRequestUnmanagedAssets.Count == 0;
    }
  }

  public static AssetDownloader.DownloadState StartDownload(
    bool checkUpdates,
    bool canRetry = true,
    ThreadPriority threadPriority = ThreadPriority.Normal,
    bool dispProgressBar = false,
    bool autoUpdateProgressText = true)
  {
    if (AssetDownloader.mCoroutine != null || !checkUpdates && AssetDownloader.mRequestIDs.Count == 0)
      return (AssetDownloader.DownloadState) null;
    AssetDownloader.DownloadState state = new AssetDownloader.DownloadState();
    AssetDownloader.mRetryOnError = canRetry;
    AssetDownloader.mCoroutine = AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.InternalDownloadAssets(state, checkUpdates, false, threadPriority, dispProgressBar, autoUpdateProgressText));
    return state;
  }

  public static void AddUnManagedData(string name)
  {
    if (!AssetManager.UseDLC || AssetDownloader.mRequestUnmanagedAssets.Contains(name))
      return;
    int num = name.LastIndexOf('/');
    string str = name;
    if (num >= 0)
      str = name.Substring(num + 1);
    if (AssetDownloader.mUnmanagedExistFile.Contains(str))
      return;
    UnManagedAssetList.Item byItemName = AssetManager.UnManagedAsset.FindByItemName(name);
    AssetDownloader.mRequestUnmanagedAssets.Add(name);
    if (byItemName != null && !AssetDownloader.mRequestUnmanagedItems.ContainsKey(name))
    {
      AssetDownloader.mRequestUnmanagedItems.Add(name, byItemName);
      if (AssetDownloader.ApproveAssetDownload)
        byItemName.SetDownloadApproved(true);
    }
    AssetDownloader.CachedUnmanagedDownloadRequests = (List<KeyValuePair<string, int>>) null;
  }

  public static void DeleteOldUnmanagedData(int max)
  {
    if (AssetDownloader.mUnmanagedExistFile.Count <= max)
      return;
    while (AssetDownloader.mUnmanagedExistFile.Count > max)
    {
      string str = AssetDownloader.mUnmanagedExistFile[0];
      int num = str.LastIndexOf('/');
      if (num >= 0)
        str = str.Substring(num + 1);
      if (File.Exists(AssetDownloader.DemoCachePath + str))
        File.Delete(AssetDownloader.DemoCachePath + str);
      AssetDownloader.mUnmanagedExistFile.RemoveAt(0);
    }
  }

  public static void StartDownloadUnmanagedData()
  {
    if (AssetDownloader.mRequestUnmanagedAssets.Count <= 0)
      return;
    AssetDownloader.mHasError = false;
    AssetDownloader.mCoroutine = AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.DonwoloadUnmanagedAsset(AssetDownloader.mRequestUnmanagedAssets, AssetDownloader.DemoCachePath));
  }

  public void RetryComfirmUnmanaged(bool retry)
  {
    if (retry)
      AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.DonwoloadUnmanagedAsset(AssetDownloader.mRequestUnmanagedAssets, AssetDownloader.DemoCachePath));
    else
      FlowNode_LoadScene.LoadBootScene();
  }

  [DebuggerHidden]
  private IEnumerator ConfirmRetryUnmanaged()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CConfirmRetryUnmanaged\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  [DebuggerHidden]
  public IEnumerator DonwoloadUnmanagedAsset(List<string> RequestAssets, string cacheDir)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CDonwoloadUnmanagedAsset\u003Ec__Iterator1()
    {
      RequestAssets = RequestAssets,
      cacheDir = cacheDir,
      \u0024this = this
    };
  }

  private string ComposeDownloadURI(string prefix, string[] fileID)
  {
    StringBuilder stringBuilder = new StringBuilder(1000);
    stringBuilder.Append(prefix);
    for (int index = 0; index < fileID.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(',');
      stringBuilder.Append(fileID[index]);
    }
    return stringBuilder.ToString();
  }

  private void RecordDownloadSpeed(float bytesPerSecond)
  {
    this.mSpeedHistory[this.mSpeedHistoryPos] = bytesPerSecond;
    this.mSpeedHistoryPos = (this.mSpeedHistoryPos + 1) % this.mSpeedHistory.Length;
    this.mSpeedHistorySize = Mathf.Min(this.mSpeedHistorySize + 1, this.mSpeedHistory.Length);
    AssetDownloader.mAverageDownloadSpeed = 0.0f;
    for (int index = this.mSpeedHistorySize - 1; index >= 0; --index)
      AssetDownloader.mAverageDownloadSpeed += this.mSpeedHistory[index];
    AssetDownloader.mAverageDownloadSpeed /= (float) this.mSpeedHistorySize;
  }

  private AssetDownloader.UnzipJobState UnzipState
  {
    get
    {
      if (!this.mMutexAcquired)
      {
        this.mMutex.WaitOne();
        this.mMutexAcquired = true;
      }
      AssetDownloader.UnzipJobState unzipState = AssetDownloader.UnzipJobState.Waiting;
      int num = 0;
      for (int index = 0; index < this.mUnzipJobs.Count; ++index)
      {
        if (this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Error || this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Extracting)
        {
          unzipState = this.mUnzipJobs[index].State;
          break;
        }
        if (this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Finished)
          ++num;
      }
      if (this.mUnzipJobs.Count > 0 && this.mUnzipJobs.Count == num)
        unzipState = AssetDownloader.UnzipJobState.Finished;
      this.mMutex.ReleaseMutex();
      this.mMutexAcquired = false;
      return unzipState;
    }
  }

  private byte[] LoadFileList()
  {
    byte[] numArray = (byte[]) null;
    if (File.Exists(AssetDownloader.FileListPath))
    {
      using (FileStream input = new FileStream(AssetDownloader.FileListPath, FileMode.Open))
        numArray = new BinaryReader((Stream) input).ReadBytes((int) input.Length);
    }
    return numArray;
  }

  private void AddRequiredAssets(string cacheDir, AssetList.Item[] assets)
  {
    for (int index = 0; index < assets.Length; ++index)
    {
      AssetList.Item asset = assets[index];
      if ((asset.Flags & AssetBundleFlags.Required) != (AssetBundleFlags) 0 && asset.Size > 0 && !asset.Exist)
        AssetDownloader.Add(asset.IDStr);
    }
  }

  private void RemoveCompletedDownloadRequests(string cacheDir, AssetList assets)
  {
    for (int index = 0; index < AssetDownloader.mRequestIDs.Count; ++index)
    {
      AssetList.Item itemById = assets.FindItemByID(AssetDownloader.mRequestIDs[index]);
      if (itemById != null && itemById.Exist)
      {
        itemById.ResetDownloadApproved();
        AssetDownloader.mRequestItems.Remove(itemById.IDStr);
        AssetDownloader.mRequestIDs.RemoveAt(index--);
        AssetDownloader.CachedDownloadRequests = (List<AssetList.Item>) null;
      }
    }
  }

  private static ThreadPriority TranslateThreadPriority(ThreadPriority priority)
  {
    switch (priority)
    {
      case ThreadPriority.Lowest:
        return (ThreadPriority) 0;
      case ThreadPriority.BelowNormal:
        return (ThreadPriority) 1;
      case ThreadPriority.Highest:
        return (ThreadPriority) 4;
      default:
        return (ThreadPriority) 2;
    }
  }

  private bool IsUnZipWorkerThreadNotTight()
  {
    return this.mUnzipJobs != null && this.mUnzipJobs.Count < 10;
  }

  private string ComposeDownloadURI(string prefix, string fileID) => prefix + fileID;

  private void BeginUnzip(
    byte[] bytes,
    int size,
    string dest,
    string requestID,
    AssetList assetList)
  {
    if (!this.mMutexAcquired)
    {
      this.mMutex.WaitOne();
      this.mMutexAcquired = true;
    }
    AssetDownloader.UnzipJob unzipJob = new AssetDownloader.UnzipJob();
    unzipJob.Bytes = bytes;
    unzipJob.Size = size;
    unzipJob.Dest = dest;
    unzipJob.nodes = new List<AssetDownloader.UnzipJob.Node>();
    AssetList.Item itemById = assetList.FindItemByID(requestID);
    unzipJob.nodes.Add(new AssetDownloader.UnzipJob.Node()
    {
      ID = requestID,
      hash = itemById.Hash,
      Item = itemById
    });
    this.mUnzipJobs.Add(unzipJob);
    if (size <= 0)
      unzipJob.State = AssetDownloader.UnzipJobState.Error;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    this.mUnzipSignal.Set();
  }

  [DebuggerHidden]
  private IEnumerator ParallelDonwloading(
    AssetList assetList,
    ThreadPriority threadPriority,
    string prefix,
    string cacheDir,
    Dictionary<string, int> itemCompressedSize,
    List<string> requestID)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CParallelDonwloading\u003Ec__Iterator2()
    {
      prefix = prefix,
      requestID = requestID,
      itemCompressedSize = itemCompressedSize,
      cacheDir = cacheDir,
      assetList = assetList,
      \u0024this = this
    };
  }

  private void LoadExistFile()
  {
    AssetDownloader.mExistFile.Clear();
    if (!File.Exists(AssetDownloader.CachePath + this.FileExistName))
      return;
    using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(AssetDownloader.CachePath + this.FileExistName, FileMode.Open)))
    {
      long length = binaryReader.BaseStream.Length;
      byte[] numArray = binaryReader.ReadBytes((int) length);
      int num = 8;
      int startIndex1 = 0;
      for (int index = 0; (long) index < length / (long) num; ++index)
      {
        uint uint32 = BitConverter.ToUInt32(numArray, startIndex1);
        int startIndex2 = startIndex1 + 4;
        int int32 = BitConverter.ToInt32(numArray, startIndex2);
        startIndex1 = startIndex2 + 4;
        AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(uint32, int32));
      }
    }
  }

  public static void VerifyExistList()
  {
    string[] files = Directory.GetFiles(AssetDownloader.CachePath);
    // ISSUE: reference to a compiler-generated field
    if (AssetDownloader.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AssetDownloader.\u003C\u003Ef__mg\u0024cache0 = new Func<string, string>(Path.GetFileName);
    }
    // ISSUE: reference to a compiler-generated field
    Func<string, string> fMgCache0 = AssetDownloader.\u003C\u003Ef__mg\u0024cache0;
    Dictionary<string, string> dictionary = ((IEnumerable<string>) files).ToDictionary<string, string>(fMgCache0);
    for (int index = 0; index < AssetDownloader.mExistFile.Count; ++index)
    {
      string key = AssetDownloader.mExistFile[index].FileID.ToString("x8");
      if (!dictionary.ContainsKey(key))
      {
        AssetDownloader.mExistFile.RemoveAt(index--);
        DebugUtility.LogError("file not found in exist list. => <color=#00ffff>" + key + "</color>");
      }
    }
    AssetDownloader.mInstance.CreateExistFile();
  }

  public void CreateExistFile()
  {
    if (!Directory.Exists(AssetDownloader.CachePath))
      return;
    using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(AssetDownloader.mExitFilePath, FileMode.Create)))
    {
      for (int index = 0; index < AssetDownloader.mExistFile.Count; ++index)
      {
        binaryWriter.Write(AssetDownloader.mExistFile[index].FileID);
        binaryWriter.Write(AssetDownloader.mExistFile[index].AssetID);
      }
    }
  }

  private void LoadBaseAsset()
  {
  }

  private bool CheckDemoCacheDirectory()
  {
    string demoCachePath = AssetDownloader.DemoCachePath;
    bool flag = true;
    try
    {
      Directory.CreateDirectory(demoCachePath.Substring(0, demoCachePath.Length - 1));
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("キャッシュディレクトリの生成に失敗しました。(" + ex.Message + ")");
      flag = false;
    }
    return flag;
  }

  private void DestroyDemoCacheDirectory()
  {
    try
    {
      string demoCachePath = AssetDownloader.DemoCachePath;
      string path = demoCachePath.Substring(0, demoCachePath.Length - 1);
      if (!Directory.Exists(path))
        return;
      Directory.Delete(path, true);
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("キャッシュディレクトリの削除に失敗しました。(" + ex.Message + ")");
    }
  }

  private void LoadUnmanagedExistFile()
  {
    AssetDownloader.mUnmanagedExistFile.Clear();
    if (!File.Exists(AssetDownloader.UnmanagedExistFilePath))
      return;
    using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(AssetDownloader.UnmanagedExistFilePath, FileMode.Open)))
    {
      int num = binaryReader.ReadInt32();
      for (int index = 0; index < num; ++index)
        AssetDownloader.mUnmanagedExistFile.Add(binaryReader.ReadString());
    }
  }

  private void CreateUnManagedExistFile()
  {
    if (AssetDownloader.mUnmanagedExistFile.Count <= 0)
      return;
    using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(AssetDownloader.CachePath + this.FileUnManagedExistName, FileMode.Create)))
    {
      binaryWriter.Write(AssetDownloader.mUnmanagedExistFile.Count);
      for (int index = 0; index < AssetDownloader.mUnmanagedExistFile.Count; ++index)
        binaryWriter.Write(AssetDownloader.mUnmanagedExistFile[index]);
    }
  }

  private void DestroyUnManagedExistFile()
  {
    string path = AssetDownloader.CachePath + this.FileUnManagedExistName;
    try
    {
      if (File.Exists(path))
        File.Delete(path);
      AssetDownloader.mUnmanagedExistFile.Clear();
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("UnManagedExistListの削除に失敗しました。(" + ex.Message + ")");
    }
  }

  [DebuggerHidden]
  private IEnumerator DownloadWWW(
    string prefix,
    string name,
    string writename,
    bool isError,
    Action<UnityWebRequest> onDownloadUpdate = null)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CDownloadWWW\u003Ec__Iterator3()
    {
      prefix = prefix,
      name = name,
      onDownloadUpdate = onDownloadUpdate,
      isError = isError,
      writename = writename,
      \u0024this = this
    };
  }

  [DebuggerHidden]
  private IEnumerator InternalDownloadAssets(
    AssetDownloader.DownloadState state,
    bool checkUpdates,
    bool isRetry,
    ThreadPriority threadPriority,
    bool dispProgressBar,
    bool autoUpdateProgressText)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CInternalDownloadAssets\u003Ec__Iterator4()
    {
      dispProgressBar = dispProgressBar,
      checkUpdates = checkUpdates,
      isRetry = isRetry,
      threadPriority = threadPriority,
      autoUpdateProgressText = autoUpdateProgressText,
      state = state,
      \u0024this = this
    };
  }

  private void SaveAssetListVersion()
  {
    File.WriteAllBytes(AssetDownloader.AssetListHashPath, Encoding.UTF8.GetBytes(AssetDownloader.CreateCurrentAssetListHashVersionClass().Serialize()));
  }

  private bool LoadAssetListVersion(ref string hash)
  {
    if (!File.Exists(AssetDownloader.AssetListHashPath))
      return false;
    string[] line = File.ReadAllLines(AssetDownloader.AssetListHashPath, Encoding.UTF8);
    if (line.Length < 2)
      return false;
    int result = 0;
    string[] strArray = line[0].Split(':');
    if (strArray.Length == 2)
      int.TryParse(strArray[1], out result);
    if (result != AssetDownloader.CurrentAssetListHashFormatVersion)
      return false;
    AssetDownloader.AssetListHashVersionBase hashVersionClass = AssetDownloader.CreateCurrentAssetListHashVersionClass();
    hashVersionClass.Deserialize(line);
    hash = hashVersionClass.Hash;
    return true;
  }

  private void SaveUnManagedAssetListVersion()
  {
    File.WriteAllBytes(AssetDownloader.UnmanagedListHashPath, Encoding.UTF8.GetBytes(AssetDownloader.CreateCurrentUnManagedAssetListHashVersionClass().Serialize()));
  }

  private bool LoadUnManagedAssetListVersion(ref string hash)
  {
    if (!File.Exists(AssetDownloader.UnmanagedListHashPath))
      return false;
    string[] line = File.ReadAllLines(AssetDownloader.UnmanagedListHashPath, Encoding.UTF8);
    if (line.Length < 2)
      return false;
    int result = 0;
    string[] strArray = line[0].Split(':');
    if (strArray.Length == 2)
      int.TryParse(strArray[1], out result);
    if (result != AssetDownloader.CurrentAssetListHashFormatVersion)
      return false;
    AssetDownloader.UnManagedAssetListHashVersionBase hashVersionClass = AssetDownloader.CreateCurrentUnManagedAssetListHashVersionClass();
    hashVersionClass.Deserialize(line);
    hash = hashVersionClass.Hash;
    return true;
  }

  private ulong CalcDLCSize()
  {
    ulong num = 0;
    List<AssetList.Item> objList = new List<AssetList.Item>();
    AssetList.Item[] assets = AssetManager.AssetList.Assets;
    for (int index = 0; index < assets.Length; ++index)
    {
      if (!AssetManager.IsAssetInCache(assets[index].IDStr) && (assets[index].Flags & AssetBundleFlags.TutorialMovie) == (AssetBundleFlags) 0)
        objList.Add(assets[index]);
    }
    for (int index = 0; index < objList.Count; ++index)
      num += (ulong) (uint) objList[index].Size;
    return num;
  }

  private string GetSizeText(ulong _size)
  {
    return _size >= 1024UL ? (_size >= 1048576UL ? (_size / 1024UL / 1024UL).ToString() + "MB" : (_size / 1024UL).ToString() + "KB") : _size.ToString() + "B";
  }

  public void FileCheckThread(object arg)
  {
    if (!(arg is AssetDownloader.FileCheckArg fileCheckArg))
      return;
    for (int index = 0; index < AssetDownloader.mExistFile.Count; ++index)
    {
      AssetList.Item itemById = fileCheckArg.assetList.FastFindItemByID(AssetDownloader.mExistFile[index].FileID);
      if (itemById != null)
        itemById.Exist = true;
    }
  }

  [DebuggerHidden]
  private IEnumerator ConfirmRetry(AssetDownloader.RetryParam param)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CConfirmRetry\u003Ec__Iterator5()
    {
      param = param
    };
  }

  public static AssetDownloader.DownloadPhases Phase => AssetDownloader.mPhase;

  public static float Progress
  {
    get
    {
      return AssetDownloader.mPhase == AssetDownloader.DownloadPhases.FileCheck ? AssetDownloader.mCompareHashProgress : AssetDownloader.mDownloadProgress;
    }
  }

  public static long TotalDownloadSize
  {
    get
    {
      return AssetDownloader.totalDownloadSize <= 0L ? 0L : (AssetDownloader.totalDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static long CurrentDownloadSize
  {
    get
    {
      return AssetDownloader.currentDownloadSize <= 0L ? 0L : (AssetDownloader.currentDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static float UnManagedProgress => AssetDownloader.mUnManagedDownloadProgress;

  public static long UnManagedTotalDownloadSize
  {
    get
    {
      return AssetDownloader.mUnManagedTotalDownloadSize <= 0L ? 0L : (AssetDownloader.mUnManagedTotalDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static long UnManagedCurrentDownloadSize
  {
    get
    {
      return AssetDownloader.mUnManagedCurrentDownloadSize <= 0L ? 0L : (AssetDownloader.mUnManagedCurrentDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static void ClearCachePath()
  {
    AssetDownloader.mCachePath = (string) null;
    AssetDownloader.mDemoCachePath = (string) null;
  }

  public static string CachePath
  {
    get
    {
      if (AssetDownloader.mCachePath == null)
        AssetDownloader.mCachePath = AppPath.assetCachePath + "/new_" + AssetManager.Format.ToPath();
      return AssetDownloader.mCachePath;
    }
  }

  public static string CachePathOld
  {
    get => AppPath.assetCachePathOld + "/" + AssetManager.Format.ToPath();
  }

  public static string DemoCachePath
  {
    get
    {
      if (AssetDownloader.mDemoCachePath == null)
        AssetDownloader.mDemoCachePath = AppPath.assetCachePath + "/new_" + AssetManager.Format.ToPath() + "cache/";
      return AssetDownloader.mDemoCachePath;
    }
  }

  public static string OldDownloadPath
  {
    get => AppPath.assetCachePath + "/" + AssetManager.Format.ToPath() + "cache/";
  }

  public static string FileListPath => AssetDownloader.CachePath + "ASSETS";

  public static string FileListVerPath => AssetDownloader.CachePath + "ASSETS.VER";

  public static string AssetListPath => AssetDownloader.CachePath + "ASSETLIST";

  public static string AssetListHashPath => AssetDownloader.CachePath + "AssetListHash";

  public static int CurrentAssetListHashFormatVersion => 1;

  private static AssetDownloader.AssetListHashVersionBase CreateCurrentAssetListHashVersionClass()
  {
    return (AssetDownloader.AssetListHashVersionBase) new AssetDownloader.AssetListHashVersion_V1();
  }

  public static string AssetListTmpPath => AssetDownloader.CachePath + "tmp/ASSETLIST";

  public static string AssetListTmpDir => AssetDownloader.CachePath + "tmp/";

  public static string ExistFilePath => AssetDownloader.CachePath + "EXISTLIST";

  public static string UnmanagedListFilePath => AssetDownloader.CachePath + "UnmanagedAssetList";

  public static string UnmanagedListHashPath
  {
    get => AssetDownloader.CachePath + "UnmanagedAssetListHash";
  }

  public static int UnmanagedListHashFormatVersion => 1;

  private static AssetDownloader.UnManagedAssetListHashVersionBase CreateCurrentUnManagedAssetListHashVersionClass()
  {
    return (AssetDownloader.UnManagedAssetListHashVersionBase) new AssetDownloader.UnManagedAssetListHashVersion_V1();
  }

  public static string UnmanagedExistFilePath => AssetDownloader.CachePath + "UNMANAGEDEXISTLIST";

  private static unsafe bool CompareBytes(byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      return false;
    int length = a.Length;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    fixed (byte* numPtr1 = &^(a == null || a.Length == 0 ? (byte&) IntPtr.Zero : ref a[0]))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      fixed (byte* numPtr2 = &^(b == null || b.Length == 0 ? (byte&) IntPtr.Zero : ref b[0]))
      {
        for (int index = 0; index < length; ++index)
        {
          if ((int) numPtr1[index] != (int) numPtr2[index])
            return false;
        }
      }
    }
    return true;
  }

  public static void ClearCache()
  {
    string cachePath = AssetDownloader.CachePath;
    if (File.Exists(AssetDownloader.AssetListHashPath))
      File.Delete(AssetDownloader.AssetListHashPath);
    if (File.Exists(AssetDownloader.AssetListPath))
      File.Delete(AssetDownloader.AssetListPath);
    if (File.Exists(AssetDownloader.ExistFilePath))
      File.Delete(AssetDownloader.ExistFilePath);
    if (File.Exists(AssetDownloader.UnmanagedListFilePath))
      File.Delete(AssetDownloader.UnmanagedListFilePath);
    if (File.Exists(AssetDownloader.UnmanagedExistFilePath))
      File.Delete(AssetDownloader.UnmanagedExistFilePath);
    if (Directory.Exists(AssetDownloader.DemoCachePath))
      Directory.Delete(AssetDownloader.DemoCachePath, true);
    if (Directory.Exists(cachePath))
      Directory.Delete(cachePath, true);
    if (!AssetManager.HasInstance)
      return;
    AssetList assetList = AssetManager.AssetList;
    if (assetList == null)
      return;
    AssetList.Item[] assets = assetList.Assets;
    for (int index = assets.Length - 1; index >= 0; --index)
      assets[index].Exist = false;
  }

  public static void DestroyAssetStart(AssetBundleFlags flags)
  {
    AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.DestroyAsset(flags));
  }

  [DebuggerHidden]
  public IEnumerator DestroyAsset(AssetBundleFlags flags)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CDestroyAsset\u003Ec__Iterator6()
    {
      flags = flags,
      \u0024this = this
    };
  }

  public static void DestroyAsset(string path)
  {
    if (!AssetManager.HasInstance)
      return;
    AssetList assetList = AssetManager.AssetList;
    AssetList.Item item = assetList == null ? (AssetList.Item) null : assetList.FindItemByPath(path);
    if (item == null)
      return;
    item.Exist = false;
    AssetDownloader.mExistFile.RemoveAll((Predicate<AssetDownloader.ExistAssetList>) (x => (int) x.FileID == (int) item.ID));
  }

  public static void DestroyAsset(AssetList.Item item)
  {
    if (!AssetManager.HasInstance || item == null)
      return;
    item.Exist = false;
    AssetDownloader.mExistFile.RemoveAll((Predicate<AssetDownloader.ExistAssetList>) (x => (int) x.FileID == (int) item.ID));
  }

  public static bool IsEnableShowSizeBeforeDownloading(bool usePlayerPrefs = true) => false;

  public static void AddFileSizeRequest(string url)
  {
    AssetDownloader.FileSizeRequest fileSizeRequest = new AssetDownloader.FileSizeRequest(url);
    AssetDownloader.m_FileSizeRequest.Add(fileSizeRequest);
  }

  [DebuggerHidden]
  private IEnumerator StartFileSizeRequest(Action<long> completeAllReqeust)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CStartFileSizeRequest\u003Ec__Iterator7()
    {
      completeAllReqeust = completeAllReqeust
    };
  }

  [DebuggerHidden]
  public static IEnumerator StartConfirmWindowYesNo(string msg)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CStartConfirmWindowYesNo\u003Ec__Iterator8()
    {
      msg = msg
    };
  }

  [DebuggerHidden]
  public static IEnumerator StartConfirmDownloadContentYesNo(
    UIUtility.DialogResultEvent okEventListener = null,
    UIUtility.DialogResultEvent cancelEventListener = null)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CStartConfirmDownloadContentYesNo\u003Ec__Iterator9()
    {
      okEventListener = okEventListener,
      cancelEventListener = cancelEventListener
    };
  }

  public static void StartConfirmDownloadQuestContentYesNo(
    List<UnitData> entryUnits,
    List<ItemData> itemData,
    QuestParam questParam,
    UIUtility.DialogResultEvent okEventListener,
    UIUtility.DialogResultEvent cancelEventListener)
  {
    AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.InternalStartConfirmDownloadQuestContentYesNo(entryUnits, itemData, questParam, okEventListener, cancelEventListener));
  }

  [DebuggerHidden]
  private IEnumerator InternalStartConfirmDownloadQuestContentYesNo(
    List<UnitData> entryUnits,
    List<ItemData> itemData,
    QuestParam questParam,
    UIUtility.DialogResultEvent okEventListener,
    UIUtility.DialogResultEvent cancelEventListener)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CInternalStartConfirmDownloadQuestContentYesNo\u003Ec__IteratorA()
    {
      entryUnits = entryUnits,
      itemData = itemData,
      questParam = questParam,
      okEventListener = okEventListener,
      cancelEventListener = cancelEventListener,
      \u0024this = this
    };
  }

  [DebuggerHidden]
  public static IEnumerator StartConfirmDownloadContent(
    UIUtility.DialogResultEvent okEventListener = null,
    AssetDownloader.OnDownloadPopupTimeout timeoutEventListener = null,
    float timeoutSec = 0.0f)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CStartConfirmDownloadContent\u003Ec__IteratorB()
    {
      okEventListener = okEventListener,
      timeoutEventListener = timeoutEventListener,
      timeoutSec = timeoutSec
    };
  }

  public static void StartConfirmDownloadQuestContent(
    List<UnitData> entryUnits,
    List<ItemData> itemData,
    QuestParam questParam,
    UIUtility.DialogResultEvent okEventListener,
    AssetDownloader.OnDownloadPopupTimeout timeoutEventListener = null,
    float timeoutSec = 0.0f)
  {
    AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.InternalStartConfirmDownloadQuestContent(entryUnits, itemData, questParam, okEventListener, timeoutEventListener, timeoutSec));
  }

  [DebuggerHidden]
  private IEnumerator InternalStartConfirmDownloadQuestContent(
    List<UnitData> entryUnits,
    List<ItemData> itemData,
    QuestParam questParam,
    UIUtility.DialogResultEvent okEventListener,
    AssetDownloader.OnDownloadPopupTimeout timeoutEventListener = null,
    float timeoutSec = 0.0f)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CInternalStartConfirmDownloadQuestContent\u003Ec__IteratorC()
    {
      entryUnits = entryUnits,
      itemData = itemData,
      questParam = questParam,
      timeoutEventListener = timeoutEventListener,
      timeoutSec = timeoutSec,
      okEventListener = okEventListener,
      \u0024this = this
    };
  }

  [DebuggerHidden]
  public static IEnumerator PrepareQuest(
    List<UnitData> entryUnits,
    List<ItemData> itemData,
    QuestParam questParam)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CPrepareQuest\u003Ec__IteratorD()
    {
      itemData = itemData,
      entryUnits = entryUnits,
      questParam = questParam
    };
  }

  public static void ForceCreateExistFile() => AssetDownloader.Instance.CreateExistFile();

  public static bool HasRequests()
  {
    return AssetDownloader.mRequestIDs.Count > 0 || AssetDownloader.mRequestUnmanagedAssets.Count > 0;
  }

  private void CheckIfDownloadRequestIsApproved()
  {
  }

  private void CheckIfDownloadUnManagedRequestIsApproved()
  {
  }

  public static void Begin_ApproveAssetDownload() => AssetDownloader.ApproveAssetDownload = true;

  public static void End_ApproveAssetDownload() => AssetDownloader.ApproveAssetDownload = false;

  public static void ClearDownloadRequests(bool resetDownlaodApproved)
  {
    if (resetDownlaodApproved)
      AssetDownloader.ResetDownloadRequestApproved();
    AssetDownloader.mRequestIDs.Clear();
    AssetDownloader.mRequestItems.Clear();
    AssetDownloader.mRequestUnmanagedAssets.Clear();
    AssetDownloader.mRequestUnmanagedItems.Clear();
    AssetDownloader.CachedDownloadRequests = (List<AssetList.Item>) null;
    AssetDownloader.CachedUnmanagedDownloadRequests = (List<KeyValuePair<string, int>>) null;
  }

  private static void ResetDownloadRequestApproved()
  {
    foreach (AssetList.Item obj in AssetDownloader.mRequestItems.Select<KeyValuePair<string, AssetList.Item>, AssetList.Item>((Func<KeyValuePair<string, AssetList.Item>, AssetList.Item>) (pair => pair.Value)).ToList<AssetList.Item>())
      obj?.ResetDownloadApproved();
    foreach (UnManagedAssetList.Item obj in AssetDownloader.mRequestUnmanagedItems.Select<KeyValuePair<string, UnManagedAssetList.Item>, UnManagedAssetList.Item>((Func<KeyValuePair<string, UnManagedAssetList.Item>, UnManagedAssetList.Item>) (pair => pair.Value)).ToList<UnManagedAssetList.Item>())
      obj?.ResetDownloadApproved();
  }

  public static List<AssetList.Item> GetDownloadRequests()
  {
    if (AssetDownloader.CachedDownloadRequests == null)
      AssetDownloader.CachedDownloadRequests = AssetDownloader.mRequestItems.Select<KeyValuePair<string, AssetList.Item>, AssetList.Item>((Func<KeyValuePair<string, AssetList.Item>, AssetList.Item>) (pair => pair.Value)).ToList<AssetList.Item>();
    return AssetDownloader.CachedDownloadRequests;
  }

  public static List<KeyValuePair<string, int>> GetUnmanagedDownloadRequests()
  {
    if (AssetDownloader.CachedUnmanagedDownloadRequests == null)
    {
      AssetDownloader.CachedUnmanagedDownloadRequests = new List<KeyValuePair<string, int>>();
      for (int index = 0; index < AssetDownloader.mRequestUnmanagedAssets.Count; ++index)
      {
        string requestUnmanagedAsset = AssetDownloader.mRequestUnmanagedAssets[index];
        int size = AssetManager.UnManagedAsset.GetSize(requestUnmanagedAsset);
        if (size > 0)
          AssetDownloader.CachedUnmanagedDownloadRequests.Add(new KeyValuePair<string, int>(requestUnmanagedAsset, size));
      }
    }
    return AssetDownloader.CachedUnmanagedDownloadRequests;
  }

  public static long CalcDownloadSize()
  {
    long num = 0;
    foreach (AssetList.Item obj in AssetDownloader.mRequestItems.Select<KeyValuePair<string, AssetList.Item>, AssetList.Item>((Func<KeyValuePair<string, AssetList.Item>, AssetList.Item>) (pair => pair.Value)).ToList<AssetList.Item>())
    {
      if (obj != null)
        num += (long) obj.Size;
    }
    for (int index = 0; index < AssetDownloader.mRequestUnmanagedAssets.Count; ++index)
    {
      int size = AssetManager.UnManagedAsset.GetSize(AssetDownloader.mRequestUnmanagedAssets[index]);
      if (size > 0)
        num += (long) size;
    }
    return num;
  }

  private void OnDownloadUpdate(UnityWebRequest www)
  {
    AssetDownloader.currentDownloadSize = (long) ((double) AssetDownloader.TotalDownloadSize * (double) www.downloadProgress) * 1048576L;
    AssetDownloader.mDownloadProgress = (float) ((double) AssetDownloader.TotalDownloadSize * (double) www.downloadProgress % 1.0);
  }

  public static void DestroyAssets(List<AssetList.Item> destroyAssets)
  {
    if (destroyAssets == null || destroyAssets.Count == 0 || AssetManager.AssetList == null)
      return;
    for (int i = destroyAssets.Count - 1; i >= 0; --i)
    {
      destroyAssets[i].Exist = false;
      AssetDownloader.mExistFile.RemoveAll((Predicate<AssetDownloader.ExistAssetList>) (x => (int) x.FileID == (int) destroyAssets[i].ID));
    }
    AssetDownloader.Instance.CreateExistFile();
  }

  public static void DestroyAssetsAll()
  {
    AssetDownloader.mExistFile.Clear();
    Directory.Delete(AssetDownloader.CachePath, true);
  }

  public enum DownloadPhases
  {
    FileCheck,
    Download,
  }

  private enum UnzipJobState
  {
    Error = -1, // 0xFFFFFFFF
    Waiting = 0,
    Extracting = 1,
    Finished = 2,
  }

  private class UnzipJob
  {
    public byte[] Bytes;
    public int Size;
    public string Dest;
    public AssetDownloader.UnzipJobState State;
    public List<AssetDownloader.UnzipJob.Node> nodes;

    public class Node
    {
      public string ID;
      public uint hash;
      public AssetList.Item Item;
    }
  }

  public class ExistAssetList
  {
    public uint FileID;
    public int AssetID;

    public ExistAssetList(uint file, int asset)
    {
      this.FileID = file;
      this.AssetID = asset;
    }
  }

  private abstract class AssetListHashVersionBase
  {
    protected string m_Hash;

    public string Hash => this.m_Hash;

    public virtual string Serialize()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("FileVersion:" + (object) AssetDownloader.CurrentAssetListHashFormatVersion);
      string assetVersion = Network.AssetVersion;
      stringBuilder.AppendLine(assetVersion);
      return stringBuilder.ToString();
    }

    public virtual void Deserialize(string[] line) => this.m_Hash = line[1];
  }

  private class AssetListHashVersion_V1 : AssetDownloader.AssetListHashVersionBase
  {
    public const int Version = 1;
  }

  private abstract class UnManagedAssetListHashVersionBase
  {
    protected string m_Hash;

    public string Hash => this.m_Hash;

    public virtual string Serialize()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("FileVersion:" + (object) AssetDownloader.UnmanagedListHashFormatVersion);
      stringBuilder.AppendLine(Network.AssetVersionEx);
      return stringBuilder.ToString();
    }

    public virtual void Deserialize(string[] line) => this.m_Hash = line[1];
  }

  private class UnManagedAssetListHashVersion_V1 : AssetDownloader.UnManagedAssetListHashVersionBase
  {
    public const int Version = 1;
  }

  private class UnzipThread2Arg
  {
    public bool completed;
    public bool abort;
    public bool error;
    public AssetList assetlist;
  }

  private class CompareFileListHashArg
  {
    public List<AssetDownloader.CompareFileListHashArg.Node> nodes;
    public string cacheDir;
    public Dictionary<uint, AssetList.Item> dic;

    public class Node
    {
      public string IDStr;
      public string metaPath;
      public uint Hash;
      public int Size;
      public AssetList.Item Item;
    }
  }

  public class DownloadState
  {
    public bool Finished;
    public bool HasError;
  }

  private class StorageException : Exception
  {
  }

  private class WWWException : Exception
  {
    private int mStatusCode;
    private string mMessage;

    public WWWException(UnityWebRequest www)
    {
      this.mStatusCode = int.Parse(www.GetResponseHeaders()["STATUS"]);
      this.mMessage = www.error;
    }

    public int StatusCode => this.mStatusCode;

    public override string Message => this.mMessage;
  }

  private class FileCheckArg
  {
    public AssetList assetList;
  }

  private class RetryParam
  {
    public AssetDownloader downloader;
    public AssetDownloader.DownloadState state;
    public bool checkUpdates;
    public bool isRetry;
    public ThreadPriority threadPriority = ThreadPriority.Normal;
    public string bodyText;
    public bool dispProgressBar;
    public bool autoUpdateProgressText;

    public void RetryEvent(bool retry)
    {
      if (retry)
        this.downloader.StartCoroutine(this.downloader.InternalDownloadAssets(this.state, this.checkUpdates, this.isRetry, this.threadPriority, this.dispProgressBar, this.autoUpdateProgressText));
      else
        FlowNode_LoadScene.LoadBootScene();
    }
  }

  public delegate void OnDownloadPopupTimeout();

  private class FileSizeRequest : IDisposable
  {
    private UnityWebRequest m_Request;
    private AssetDownloader.FileSizeRequest.DownloadHandler m_DownloadHandler;
    private long m_ResponseCode;
    private AssetDownloader.FileSizeRequest.eState m_State;
    private int m_SendCount;

    public FileSizeRequest(string url)
    {
      this.m_Request = UnityWebRequest.Head(url);
      this.m_DownloadHandler = AssetDownloader.FileSizeRequest.DownloadHandler.Create();
      this.m_Request.downloadHandler = (DownloadHandler) this.m_DownloadHandler;
    }

    public bool isDone => this.m_Request.isDone;

    public bool HasError
    {
      get => !string.IsNullOrEmpty(this.m_Request.error) || this.m_Request.responseCode != 200L;
    }

    public string ErrorMessage
    {
      get
      {
        if (!string.IsNullOrEmpty(this.m_Request.error))
          return this.m_Request.error;
        return this.m_Request.responseCode != 200L ? "Response code : " + (object) this.m_Request.responseCode : string.Empty;
      }
    }

    public int ContentLength => this.m_DownloadHandler.ContentLength;

    public bool IsSending => this.m_State == AssetDownloader.FileSizeRequest.eState.Sending;

    public void Dispose()
    {
      if (this.m_Request != null)
      {
        this.m_State = AssetDownloader.FileSizeRequest.eState.Disposed;
        this.m_Request.Dispose();
        this.m_Request = (UnityWebRequest) null;
      }
      if (this.m_DownloadHandler == null)
        return;
      ((DownloadHandler) this.m_DownloadHandler).Dispose();
      this.m_DownloadHandler = (AssetDownloader.FileSizeRequest.DownloadHandler) null;
    }

    public void Send()
    {
      if (this.m_Request != null)
      {
        ++this.m_SendCount;
        this.m_State = AssetDownloader.FileSizeRequest.eState.Sending;
        this.m_Request.SendWebRequest();
      }
      else
        DebugUtility.LogError("Already diposed \"m_Request\" ...");
    }

    public void Resend()
    {
      if (this.m_Request != null)
      {
        string url = this.m_Request.url;
        this.m_Request.Dispose();
        this.m_Request = UnityWebRequest.Head(url);
        this.m_Request.downloadHandler = (DownloadHandler) this.m_DownloadHandler;
        this.Send();
      }
      else
        DebugUtility.LogError("Already diposed \"m_Request\" ...");
    }

    private class DownloadHandler : DownloadHandlerScript
    {
      private int m_ContentLength;

      private DownloadHandler()
      {
      }

      public int ContentLength => this.m_ContentLength;

      protected virtual void ReceiveContentLength(int contentLength)
      {
        this.m_ContentLength = contentLength;
      }

      public static AssetDownloader.FileSizeRequest.DownloadHandler Create(int bufferSize = 0)
      {
        return new AssetDownloader.FileSizeRequest.DownloadHandler();
      }

      public void Reset() => this.m_ContentLength = 0;
    }

    private enum eState
    {
      Ready,
      Sending,
      Disposed,
    }
  }

  public class ApproveAssetDownloadScope : IDisposable
  {
    public ApproveAssetDownloadScope() => AssetDownloader.Begin_ApproveAssetDownload();

    public void Dispose() => AssetDownloader.End_ApproveAssetDownload();
  }
}
