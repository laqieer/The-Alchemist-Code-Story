// Decompiled with JetBrains decompiler
// Type: MySound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MySound : MonoSingleton<MySound>
{
  private static readonly string SECueSheetName = "SE";
  private MySound.BGMManager mBGM = new MySound.BGMManager();
  private GameObject mCriAtomGO;
  private CriAtom mCriAtom;
  private MySound.EClearCacheState mClearCacheState;
  private MySound.CueSheetHandle mHandleSE;
  private MySound.Source mSourceToPlayHandle;

  private static string CueName(string cueID, string oldPrefix)
  {
    if ('0' <= cueID[0] && cueID[0] <= '9')
      return oldPrefix + cueID;
    return cueID;
  }

  private static string SECueName(string cueID)
  {
    return MySound.CueName(cueID, "SE_");
  }

  private static string BGMCueSheetName(string cueID)
  {
    return MySound.CueName(cueID, "BGM_");
  }

  private static string BGMCueName(string cueID)
  {
    return MySound.CueName(cueID, "BGM_");
  }

  private static string JingleCueSheetName(string cueID)
  {
    return MySound.CueName(cueID, "JIN_");
  }

  private static string JingleCueName(string cueID)
  {
    return MySound.CueName(cueID, "JIN_");
  }

  private static string VoiceCueSheetName(string charName)
  {
    return "VO_" + charName;
  }

  public static string[] VoiceCueSheetFileName(string charName)
  {
    return MySound.CueSheetFileName(MySound.VoiceCueSheetName(charName));
  }

  public static string[] CueSheetFileName(string sheetName)
  {
    if (string.IsNullOrEmpty(sheetName))
      return (string[]) null;
    return new string[2]
    {
      sheetName + ".acb",
      sheetName + ".awb"
    };
  }

  private void Setup()
  {
    this.mCriAtomGO = new GameObject("CRI Atom", new System.Type[1]
    {
      typeof (CriAtom)
    });
    if (!((Object) this.mCriAtomGO != (Object) null))
      return;
    Object.DontDestroyOnLoad((Object) this.mCriAtomGO);
    this.mCriAtomGO.transform.parent = this.gameObject.transform;
    this.mCriAtom = this.mCriAtomGO.GetComponent<CriAtom>();
    this.mCriAtom.acfFile = MyCriManager.AcfFileName;
    CriAtomEx.RegisterAcf((CriFsBinder) null, this.mCriAtom.acfFile);
    DebugUtility.LogWarning("RegisterAcf:" + this.mCriAtom.acfFile);
    this.mCriAtom.dontDestroyOnLoad = true;
  }

  public bool LoadSE()
  {
    if (this.mHandleSE != null)
      return true;
    this.mHandleSE = MySound.CueSheetHandle.Create(MySound.SECueSheetName, MySound.EType.SE, true, true, false, false);
    if (this.mHandleSE == null)
      return false;
    this.mHandleSE.CreateDefaultOneShotSource();
    return true;
  }

  public void PrepareToClearCache()
  {
    this.mClearCacheState = MySound.EClearCacheState.STOPPING;
    if (this.mBGM != null)
      this.mBGM.Cleanup();
    MySound.CueSheet.PrepareToClearCacheAll(1f);
    if (this.mHandleSE != null)
      this.mHandleSE.Cleanup();
    this.mHandleSE = (MySound.CueSheetHandle) null;
  }

  public bool IsReadyToClearCache()
  {
    return this.mClearCacheState == MySound.EClearCacheState.END;
  }

  public static void RestoreFromClearCache()
  {
    if ((Object) MonoSingleton<MySound>.GetInstanceDirect() == (Object) null || MonoSingleton<MySound>.Instance.mClearCacheState != MySound.EClearCacheState.END)
      return;
    MonoSingleton<MySound>.Instance.mClearCacheState = MySound.EClearCacheState.NOP;
    MonoSingleton<MySound>.Instance.Setup();
  }

  protected override void Initialize()
  {
    MyCriManager.Setup(false);
    Object.DontDestroyOnLoad((Object) this);
    CriAtomEx.ResetPerformanceMonitor();
    this.Setup();
  }

  protected override void Release()
  {
  }

  public void UpdateCueSheet()
  {
    MySound.CueSheet.UpdateAll();
  }

  private void Update()
  {
    if (this.mClearCacheState == MySound.EClearCacheState.FINISH)
      this.mClearCacheState = MySound.EClearCacheState.END;
    else if (this.mClearCacheState == MySound.EClearCacheState.STOPPING)
    {
      if (!MySound.CueSheet.IsStoppedAllForClearCache())
        ;
      if (MySound.CueSheet.GetLoadedNum() <= 0)
      {
        this.mClearCacheState = MySound.EClearCacheState.FINISH;
        if ((Object) this.mCriAtomGO != (Object) null)
        {
          DebugUtility.LogWarning("UnregisterAcf");
          CriAtomEx.UnregisterAcf();
          Object.Destroy((Object) this.mCriAtomGO);
          this.mCriAtomGO = (GameObject) null;
        }
      }
    }
    MySound.CueSheet.UpdateAll();
    this.mBGM.Update();
    MySound.VolumeManager.UpdateAll();
  }

  public void PlaySEOneShot(string cueID, float delaySec = 0.0f)
  {
    if (string.IsNullOrEmpty(cueID))
      DebugUtility.LogWarning("[MySound] NO CueID!");
    cueID = MySound.CueIDConverter.Convert(MySound.EType.SE, cueID);
    this.LoadSE();
    if (this.mHandleSE == null || string.IsNullOrEmpty(cueID))
      return;
    this.mHandleSE.PlayDefaultOneShot(MySound.SECueName(cueID), false, delaySec, false);
  }

  public void PlaySEOneShotAndroidLowLatency(string cueID, float delaySec = 0.0f)
  {
    if (string.IsNullOrEmpty(cueID))
      DebugUtility.LogWarning("[MySound] NO CueID!");
    cueID = MySound.CueIDConverter.Convert(MySound.EType.SE, cueID);
    this.LoadSE();
    if (this.mHandleSE == null || string.IsNullOrEmpty(cueID))
      return;
    this.mHandleSE.PlayDefaultOneShot(MySound.SECueName(cueID), true, delaySec, false);
  }

  public void StopSEOneShot(float fade)
  {
    if (this.mHandleSE == null)
      return;
    this.mHandleSE.StopDefaultAll(fade);
  }

  public MySound.PlayHandle PlaySELoop(string cueID, float delaySec = 0.0f)
  {
    if (string.IsNullOrEmpty(cueID))
      DebugUtility.LogWarning("[MySound] NO CueID!");
    cueID = MySound.CueIDConverter.Convert(MySound.EType.SE, cueID);
    this.LoadSE();
    if (this.mHandleSE == null || string.IsNullOrEmpty(cueID))
      return (MySound.PlayHandle) null;
    return this.mHandleSE.Play(MySound.SECueName(cueID), MySound.CueSheetHandle.ELoopFlag.LOOP, true, delaySec);
  }

  public bool IsLoopSE(string cueID)
  {
    if (string.IsNullOrEmpty(cueID))
      DebugUtility.LogWarning("[MySound] NO CueID!");
    cueID = MySound.CueIDConverter.Convert(MySound.EType.SE, cueID);
    this.LoadSE();
    if (this.mHandleSE == null || string.IsNullOrEmpty(cueID))
      return false;
    return this.mHandleSE.IsLoop(MySound.SECueName(cueID));
  }

  public MySound.PlayHandle CreatePlayHandleSE()
  {
    this.LoadSE();
    if (this.mHandleSE == null)
      return (MySound.PlayHandle) null;
    return this.mHandleSE.CreatePlayHandle(MySound.CueSheetHandle.ELoopFlag.DEFAULT);
  }

  public void PlayJingle(string cueID, float delaySec = 0.0f, string sheetName = null)
  {
    if (string.IsNullOrEmpty(cueID))
      DebugUtility.LogWarning("[MySound] NO CueID!");
    cueID = MySound.CueIDConverter.Convert(MySound.EType.JINGLE, cueID);
    if (string.IsNullOrEmpty(cueID))
      return;
    sheetName = !string.IsNullOrEmpty(sheetName) ? sheetName : MySound.JingleCueSheetName(cueID);
    MySound.CueSheetHandle.Create(sheetName, MySound.EType.JINGLE, true, true, false, false)?.Play(MySound.JingleCueName(cueID), MySound.CueSheetHandle.ELoopFlag.NOT_LOOP, false, delaySec);
  }

  public void PlayOneShot(string sheetName, string cueName, MySound.EType type = MySound.EType.SE, float delaySec = 0.0f)
  {
    MySound.CueSheetHandle.Create(sheetName, type, true, true, false, false)?.Play(cueName, MySound.CueSheetHandle.ELoopFlag.NOT_LOOP, false, delaySec);
  }

  public MySound.PlayHandle PlayLoop(string sheetName, string cueName, MySound.EType type = MySound.EType.SE, float delaySec = 0.0f)
  {
    return MySound.CueSheetHandle.Create(sheetName, type, true, true, false, false)?.Play(cueName, MySound.CueSheetHandle.ELoopFlag.LOOP, true, delaySec);
  }

  public void PlayBGM(string cueID, string sheetName = null, bool IsUnManaged = false)
  {
    if (this.mBGM == null || string.IsNullOrEmpty(cueID))
      return;
    this.mBGM.Play(cueID, !string.IsNullOrEmpty(this.mBGM.CurrentCueID) ? 1f : 0.0f, sheetName, IsUnManaged);
  }

  public void PlayBGM(string cueID, float delaySec, string sheetName = null)
  {
    if (this.mBGM == null || string.IsNullOrEmpty(cueID))
      return;
    this.mBGM.Play(cueID, delaySec, sheetName, false);
  }

  public void StopBGM()
  {
    if (this.mBGM == null)
      return;
    this.mBGM.Play((string) null, 1f, (string) null, false);
  }

  public void StopBGM(float sec)
  {
    if (this.mBGM == null)
      return;
    this.mBGM.Play((string) null, sec, (string) null, false);
  }

  public bool StopBGMFadeOut(float sec = 1f)
  {
    if (this.mBGM == null)
      return false;
    return this.mBGM.Stop(sec);
  }

  public bool CheckCueSheetNames()
  {
    return true;
  }

  private enum EClearCacheState
  {
    NOP,
    STOPPING,
    FINISH,
    END,
  }

  public enum EType
  {
    DIRECT = -1,
    BGM = 0,
    JINGLE = 1,
    SE = 2,
    VOICE = 3,
  }

  private class CueIDConverter
  {
    private static readonly Dictionary<string, string> sBGM = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> sJingle = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> sSE = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> sVoice = new Dictionary<string, string>();

    public static string Convert(MySound.EType type, string cueID)
    {
      if (string.IsNullOrEmpty(cueID))
        return cueID;
      Dictionary<string, string> sBgm = MySound.CueIDConverter.sBGM;
      Dictionary<string, string> dictionary;
      switch (type)
      {
        case MySound.EType.BGM:
          dictionary = MySound.CueIDConverter.sBGM;
          break;
        case MySound.EType.JINGLE:
          dictionary = MySound.CueIDConverter.sJingle;
          break;
        case MySound.EType.VOICE:
          dictionary = MySound.CueIDConverter.sVoice;
          break;
        default:
          dictionary = MySound.CueIDConverter.sSE;
          break;
      }
      string str;
      if (!dictionary.TryGetValue(cueID, out str))
        return cueID;
      DebugUtility.LogWarning("[MySound] convert " + (object) type + " cueID:" + cueID + " to:" + (str ?? string.Empty));
      return str;
    }
  }

  private class CueSheet
  {
    private static List<MySound.CueSheet> sCueSheets = new List<MySound.CueSheet>();
    private List<MySound.Source> mSrcList = new List<MySound.Source>();
    private CriAtomCueSheet mSheet;
    private int refCount;
    private MySound.CueSheet.EState mState;

    private CueSheet()
    {
    }

    private string name
    {
      get
      {
        if (this.mSheet == null)
          return (string) null;
        return this.mSheet.name;
      }
    }

    public static bool Load(string sheetName, bool useAcb = true, bool useAwb = true, bool loadAsync = false, bool isUnManaged = false)
    {
      if (string.IsNullOrEmpty(sheetName) || MonoSingleton<MySound>.Instance.mClearCacheState != MySound.EClearCacheState.NOP)
        return false;
      MySound.CueSheet cueSheet1 = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet1 != null)
      {
        ++cueSheet1.refCount;
        return true;
      }
      MySound.CueSheet cueSheet2 = new MySound.CueSheet();
      if (cueSheet2 == null)
        return false;
      if (loadAsync)
      {
        cueSheet2.mState = MySound.CueSheet.EState.LOADING;
        MonoSingleton<MySound>.Instance.StartCoroutine(cueSheet2.LoadCueSheetCore(sheetName, useAcb, useAwb, isUnManaged));
      }
      else
      {
        string acb1 = !useAcb ? (string) null : sheetName + ".acb";
        string acb2 = !useAwb ? (string) null : sheetName + ".awb";
        string loadFileName1 = MyCriManager.GetLoadFileName(acb1, isUnManaged);
        if (loadFileName1 == null)
          return false;
        string loadFileName2 = MyCriManager.GetLoadFileName(acb2, isUnManaged);
        cueSheet2.mSheet = CriAtom.AddCueSheet(sheetName, loadFileName1, loadFileName2, (CriFsBinder) null);
        cueSheet2.mState = MySound.CueSheet.EState.READY;
        MySound.CueSheet.CheckSheet(sheetName);
      }
      cueSheet2.refCount = 1;
      MySound.CueSheet.sCueSheets.Add(cueSheet2);
      DebugUtility.Log("[MySound] AddCueSheet:" + sheetName);
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadCueSheetCore(string sheetName, bool useAcb, bool useAwb, bool isUnManaged)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MySound.CueSheet.\u003CLoadCueSheetCore\u003Ec__Iterator0()
      {
        useAcb = useAcb,
        sheetName = sheetName,
        useAwb = useAwb,
        isUnManaged = isUnManaged,
        \u0024this = this
      };
    }

    private static bool IsSheetValid(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return false;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null || cueSheet.mState == MySound.CueSheet.EState.READY)
        ;
      CriAtomExAcb acb = CriAtom.GetAcb(sheetName);
      if (acb == null)
        return false;
      CriAtomEx.CueInfo[] cueInfoList = acb.GetCueInfoList();
      return cueInfoList != null && cueInfoList.Length > 0;
    }

    private static void CheckSheet(string sheetName)
    {
    }

    public static void Unload(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null)
        return;
      --cueSheet.refCount;
      if (cueSheet.refCount < 0)
        ;
    }

    public static bool IsReady(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return false;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null)
        return false;
      return cueSheet.mState == MySound.CueSheet.EState.READY;
    }

    public static MySound.Source CreateSource(string sheetName, string srcName, bool useAndroidLowLatencyMode, MySound.EType type)
    {
      if (string.IsNullOrEmpty(sheetName))
        return (MySound.Source) null;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null)
        return (MySound.Source) null;
      MySound.Source source = new MySound.Source();
      if (source == null)
        return (MySound.Source) null;
      if (!source.Setup(sheetName, srcName, useAndroidLowLatencyMode, type))
        return (MySound.Source) null;
      cueSheet.mSrcList.Add(source);
      DebugUtility.Log("[MySound] create Source:" + sheetName + "." + srcName + "(" + (object) useAndroidLowLatencyMode + ")");
      return source;
    }

    public static bool IsLoop(string sheetName, string cueName)
    {
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        return false;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null || cueSheet.mState != MySound.CueSheet.EState.READY)
        return false;
      CriAtomExAcb acb = CriAtom.GetAcb(sheetName);
      CriAtomEx.CueInfo info;
      if (acb == null || !acb.GetCueInfo(cueName, out info))
        return false;
      return info.length == -1L;
    }

    private void Update()
    {
      foreach (MySound.Source mSrc in this.mSrcList)
      {
        mSrc.Update();
        if (mSrc.IsReadyToRemove)
        {
          DebugUtility.Log("[MySound] remove Source:" + this.name + "." + mSrc.name);
          mSrc.Cleanup();
        }
      }
      this.mSrcList.RemoveAll((Predicate<MySound.Source>) (s => s.IsReadyToRemove));
    }

    private bool IsReadyToRemove
    {
      get
      {
        return this.mState != MySound.CueSheet.EState.LOADING && (MonoSingleton<MySound>.Instance.mClearCacheState != MySound.EClearCacheState.NOP || this.refCount <= 0) && this.mSrcList.Count <= 0;
      }
    }

    public static void UpdateAll()
    {
      foreach (MySound.CueSheet sCueSheet in MySound.CueSheet.sCueSheets)
      {
        sCueSheet.Update();
        if (sCueSheet.IsReadyToRemove)
        {
          DebugUtility.Log("[MySound] RemoveCueSheet:" + sCueSheet.name);
          CriAtom.RemoveCueSheet(sCueSheet.name);
        }
      }
      MySound.CueSheet.sCueSheets.RemoveAll((Predicate<MySound.CueSheet>) (sheet => sheet.IsReadyToRemove));
    }

    private void PrepareToClearCache(float sec)
    {
      foreach (MySound.Source mSrc in this.mSrcList)
      {
        mSrc.FadeOutStop(sec, false);
        mSrc.KeepInstance = false;
      }
    }

    public static void PrepareToClearCacheAll(float sec)
    {
      foreach (MySound.CueSheet sCueSheet in MySound.CueSheet.sCueSheets)
        sCueSheet.PrepareToClearCache(sec);
    }

    private bool IsStoppedForClearCache()
    {
      foreach (MySound.Source mSrc in this.mSrcList)
      {
        if (!mSrc.IsReadyToRemove)
          return false;
      }
      return true;
    }

    public static bool IsStoppedAllForClearCache()
    {
      foreach (MySound.CueSheet sCueSheet in MySound.CueSheet.sCueSheets)
      {
        if (!sCueSheet.IsStoppedForClearCache())
          return false;
      }
      return true;
    }

    public static int GetLoadedNum()
    {
      return MySound.CueSheet.sCueSheets.Count;
    }

    private bool IsPlaying()
    {
      foreach (MySound.Source mSrc in this.mSrcList)
      {
        if (mSrc.IsPlaying)
          return true;
      }
      return false;
    }

    public static bool IsPlaying(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return false;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null || cueSheet.mState != MySound.CueSheet.EState.READY)
        return false;
      return cueSheet.IsPlaying();
    }

    private void Stop(float sec, bool fadeOutTemporary)
    {
      foreach (MySound.Source mSrc in this.mSrcList)
        mSrc.FadeOutStop(sec, fadeOutTemporary);
    }

    public static void StopAll(string sheetName, float sec, bool fadeOutTemporary = false)
    {
      if (string.IsNullOrEmpty(sheetName))
        return;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null || cueSheet.mState != MySound.CueSheet.EState.READY)
        return;
      cueSheet.Stop(sec, fadeOutTemporary);
    }

    public static CriAtomExAcb FindAcb(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return (CriAtomExAcb) null;
      MySound.CueSheet cueSheet = MySound.CueSheet.sCueSheets.Find((Predicate<MySound.CueSheet>) (s => sheetName.Equals(s.name)));
      if (cueSheet == null)
        return (CriAtomExAcb) null;
      if (cueSheet.mSheet == null)
        return (CriAtomExAcb) null;
      return cueSheet.mSheet.acb;
    }

    private enum EState
    {
      NOP,
      LOADING,
      READY,
    }
  }

  public class CueSheetHandle
  {
    private string mSheetName;
    private MySound.EType mType;
    private MySound.Source mDefaultOneShot;
    private MySound.Source mDefaultOneShotAndroidLowLatency;

    private CueSheetHandle()
    {
    }

    public bool IsReady
    {
      get
      {
        return MySound.CueSheet.IsReady(this.mSheetName);
      }
    }

    public static MySound.CueSheetHandle Create(string sheetName, MySound.EType type, bool useAcb = true, bool useAwb = true, bool loadAsync = false, bool isUnManaged = false)
    {
      if (string.IsNullOrEmpty(sheetName))
        return (MySound.CueSheetHandle) null;
      MySound.CueSheetHandle cueSheetHandle = new MySound.CueSheetHandle();
      if (cueSheetHandle == null)
        return (MySound.CueSheetHandle) null;
      if (!MySound.CueSheet.Load(sheetName, useAcb, useAwb, loadAsync, isUnManaged))
        return (MySound.CueSheetHandle) null;
      cueSheetHandle.mSheetName = sheetName;
      cueSheetHandle.mType = type;
      return cueSheetHandle;
    }

    public void CreateDefaultOneShotSource()
    {
      if (string.IsNullOrEmpty(this.mSheetName))
        return;
      this.mDefaultOneShot = this.CreateSource(this.mSheetName + "_DefaultOneShot", false, this.mType, MySound.CueSheetHandle.ELoopFlag.DEFAULT);
      if (this.mDefaultOneShot != null)
        this.mDefaultOneShot.KeepInstance = true;
      this.mDefaultOneShotAndroidLowLatency = this.CreateSource(this.mSheetName + "_DefaultOneShot_AndroidLowLatency", true, this.mType, MySound.CueSheetHandle.ELoopFlag.DEFAULT);
      if (this.mDefaultOneShotAndroidLowLatency == null)
        return;
      this.mDefaultOneShotAndroidLowLatency.KeepInstance = true;
    }

    private MySound.Source CreateSource(string srcName, bool androidLatency, MySound.EType type, MySound.CueSheetHandle.ELoopFlag loop)
    {
      MySound.Source source = MySound.CueSheet.CreateSource(this.mSheetName, srcName, androidLatency, type);
      if (source == null)
        return (MySound.Source) null;
      switch (loop)
      {
        case MySound.CueSheetHandle.ELoopFlag.NOT_LOOP:
          source.SetLoop(false);
          break;
        case MySound.CueSheetHandle.ELoopFlag.LOOP:
          source.SetLoop(true);
          break;
      }
      return source;
    }

    public void PlayDefaultOneShot(string cueName, bool androidLowLatency, float delaySec = 0.0f, bool is_stopplay = false)
    {
      MySound.Source source = !androidLowLatency ? this.mDefaultOneShot : this.mDefaultOneShotAndroidLowLatency;
      if (source == null)
        return;
      if ((double) delaySec > 0.0)
        source.DelayPlay(cueName, delaySec);
      else
        source.Play(cueName, is_stopplay);
    }

    public void StopDefaultAll(float sec)
    {
      if (this.mDefaultOneShot != null)
        this.mDefaultOneShot.FadeOutStop(sec, false);
      if (this.mDefaultOneShotAndroidLowLatency == null)
        return;
      this.mDefaultOneShotAndroidLowLatency.FadeOutStop(sec, false);
    }

    public MySound.PlayHandle Play(string cueName, MySound.CueSheetHandle.ELoopFlag loop, bool useHandle, float delaySec = 0.0f)
    {
      MySound.Source source = this.CreateSource(cueName, false, this.mType, loop);
      if (source == null)
        return (MySound.PlayHandle) null;
      MySound.PlayHandle playHandle = (MySound.PlayHandle) null;
      if (useHandle)
      {
        playHandle = this.CreatePlayHandleCore(source);
        if (playHandle == null)
          return (MySound.PlayHandle) null;
      }
      if ((double) delaySec > 0.0)
        source.DelayPlay(cueName, delaySec);
      else
        source.Play(cueName, false);
      return playHandle;
    }

    private MySound.PlayHandle CreatePlayHandleCore(MySound.Source src)
    {
      MonoSingleton<MySound>.Instance.mSourceToPlayHandle = src;
      MySound.PlayHandle playHandle = MySound.PlayHandle.Create();
      MonoSingleton<MySound>.Instance.mSourceToPlayHandle = (MySound.Source) null;
      return playHandle;
    }

    public MySound.PlayHandle CreatePlayHandle(MySound.CueSheetHandle.ELoopFlag loop)
    {
      MySound.Source source = this.CreateSource(this.mSheetName + "_PlayHandle", false, this.mType, loop);
      if (source == null)
        return (MySound.PlayHandle) null;
      return this.CreatePlayHandleCore(source);
    }

    public bool IsLoop(string cueName)
    {
      return MySound.CueSheet.IsLoop(this.mSheetName, cueName);
    }

    ~CueSheetHandle()
    {
      this.Cleanup();
    }

    public void Cleanup()
    {
      if (this.mDefaultOneShot != null)
        this.mDefaultOneShot.KeepInstance = false;
      if (this.mDefaultOneShotAndroidLowLatency != null)
        this.mDefaultOneShotAndroidLowLatency.KeepInstance = false;
      if (string.IsNullOrEmpty(this.mSheetName))
        return;
      MySound.CueSheet.Unload(this.mSheetName);
      this.mSheetName = (string) null;
    }

    public bool IsDefaultOneShotPlaying
    {
      get
      {
        return this.mDefaultOneShot != null && this.mDefaultOneShot.IsPlaying || this.mDefaultOneShotAndroidLowLatency != null && this.mDefaultOneShotAndroidLowLatency.IsPlaying;
      }
    }

    public bool IsCueSheetPlaying
    {
      get
      {
        return MySound.CueSheet.IsPlaying(this.mSheetName);
      }
    }

    public enum ELoopFlag
    {
      DEFAULT,
      NOT_LOOP,
      LOOP,
    }
  }

  private class BGMManager
  {
    private MySound.PlayHandle mCurrent;
    private float mTimer;
    private float mWaitSec;
    private string mNextCueID;
    private MySound.CueSheetHandle mNextHandle;

    public string CurrentCueID { get; private set; }

    public void Cleanup()
    {
      if (this.mCurrent != null)
      {
        this.mCurrent.Stop(1f);
        this.mCurrent = (MySound.PlayHandle) null;
      }
      this.CurrentCueID = (string) null;
      this.mTimer = 0.0f;
      this.mWaitSec = 0.0f;
      this.mNextCueID = (string) null;
      if (this.mNextHandle != null)
        this.mNextHandle.Cleanup();
      this.mNextHandle = (MySound.CueSheetHandle) null;
    }

    public void Play(string cueID, float waitSec, string sheetName = null, bool IsUnManaged = false)
    {
      if (!string.IsNullOrEmpty(cueID))
        cueID = MySound.CueIDConverter.Convert(MySound.EType.BGM, cueID);
      if (string.IsNullOrEmpty(this.CurrentCueID))
      {
        if (string.IsNullOrEmpty(cueID))
          return;
      }
      else if (this.CurrentCueID.Equals(cueID))
        return;
      if (this.mCurrent != null)
      {
        this.mCurrent.Stop(1f);
        this.mCurrent = (MySound.PlayHandle) null;
      }
      this.CurrentCueID = (string) null;
      this.mTimer = 0.0f;
      this.mWaitSec = waitSec;
      this.mNextCueID = cueID;
      if (string.IsNullOrEmpty(cueID))
      {
        this.mNextHandle = (MySound.CueSheetHandle) null;
      }
      else
      {
        sheetName = !string.IsNullOrEmpty(sheetName) ? sheetName : MySound.BGMCueSheetName(this.mNextCueID);
        this.mNextHandle = MySound.CueSheetHandle.Create(sheetName, MySound.EType.BGM, true, true, false, IsUnManaged);
      }
    }

    public bool Stop(float fadeSec)
    {
      if (this.mCurrent == null)
        return false;
      this.mCurrent.Stop(fadeSec);
      return true;
    }

    public void Update()
    {
      if (string.IsNullOrEmpty(this.mNextCueID))
        return;
      this.mTimer += Time.deltaTime;
      if ((double) this.mTimer < (double) this.mWaitSec)
        return;
      if (this.mNextHandle != null)
      {
        if (!this.mNextHandle.IsReady)
          return;
        DebugUtility.Log("[MySound] BGM > " + this.mNextCueID);
        this.mCurrent = this.mNextHandle.Play(MySound.BGMCueName(this.mNextCueID), MySound.CueSheetHandle.ELoopFlag.DEFAULT, true, 0.0f);
        this.CurrentCueID = this.mNextCueID;
      }
      else
      {
        DebugUtility.Log("[MySound] BGM > null");
        this.mCurrent = (MySound.PlayHandle) null;
        this.CurrentCueID = (string) null;
      }
      this.mNextCueID = (string) null;
      this.mNextHandle = (MySound.CueSheetHandle) null;
    }
  }

  public class Voice
  {
    private string mCueNamePrefix;
    private MySound.CueSheetHandle mHandle;

    public Voice(string sheetName, string charName, string cueNamePrefix, bool isUnManaged = false)
    {
      if (string.IsNullOrEmpty(sheetName))
        return;
      MonoSingleton<MySound>.Instance.Ensure();
      this.CharName = !string.IsNullOrEmpty(charName) ? charName : "null";
      cueNamePrefix = !string.IsNullOrEmpty(cueNamePrefix) ? cueNamePrefix : string.Empty;
      this.Setup(sheetName, cueNamePrefix, isUnManaged);
    }

    public Voice(string charName)
    {
      if (string.IsNullOrEmpty(charName))
        return;
      MonoSingleton<MySound>.Instance.Ensure();
      this.CharName = charName;
      this.Setup(MySound.VoiceCueSheetName(charName), charName + "_", false);
    }

    public string CharName { get; private set; }

    public string SheetName { get; private set; }

    private void Setup(string sheetName, string cueNamePrefix, bool isUnManaged = false)
    {
      this.SheetName = sheetName;
      this.mHandle = MySound.CueSheetHandle.Create(sheetName, MySound.EType.VOICE, true, true, false, isUnManaged);
      if (this.mHandle != null)
        this.mHandle.CreateDefaultOneShotSource();
      this.mCueNamePrefix = cueNamePrefix;
    }

    public void Play(string cueID, float delaySec = 0.0f, bool is_stopplay = false)
    {
      if (string.IsNullOrEmpty(cueID) || string.IsNullOrEmpty(this.CharName) || this.mHandle == null)
        return;
      cueID = MySound.CueIDConverter.Convert(MySound.EType.VOICE, cueID);
      this.mHandle.PlayDefaultOneShot(this.mCueNamePrefix + cueID, false, delaySec, is_stopplay);
    }

    public static string ReplaceCharNameOfCueName(string cueName, string charName)
    {
      if (string.IsNullOrEmpty(cueName) || string.IsNullOrEmpty(charName))
        return cueName;
      return cueName.Replace("charactorname", charName);
    }

    public void PlayDirect(string cueID, float delaySec = 0.0f)
    {
      if (string.IsNullOrEmpty(cueID) || string.IsNullOrEmpty(this.CharName) || this.mHandle == null)
        return;
      cueID = MySound.CueIDConverter.Convert(MySound.EType.VOICE, cueID);
      this.mHandle.PlayDefaultOneShot(cueID, false, delaySec, false);
    }

    public void StopAll(float sec = 1f)
    {
      if (this.mHandle == null)
        return;
      this.mHandle.StopDefaultAll(sec);
    }

    public static void StopAll(string charName, float sec, bool fadeOutTemporary = false)
    {
      MySound.CueSheet.StopAll(MySound.VoiceCueSheetName(charName), sec, fadeOutTemporary);
    }

    public bool IsPlaying
    {
      get
      {
        if (this.mHandle == null)
          return false;
        return this.mHandle.IsDefaultOneShotPlaying;
      }
    }

    public static bool IsCueSheetPlaying(string charName)
    {
      return MySound.CueSheet.IsPlaying(MySound.VoiceCueSheetName(charName));
    }

    public void Cleanup()
    {
      if (this.mHandle == null)
        return;
      this.mHandle.Cleanup();
    }

    public bool CheckCueIDs()
    {
      return true;
    }

    public CriAtomExAcb FindAcb(string sheetName)
    {
      if (string.IsNullOrEmpty(sheetName))
        return (CriAtomExAcb) null;
      return MySound.CueSheet.FindAcb(sheetName);
    }
  }

  private class VolumeManager
  {
    private static List<MySound.VolumeManager> sInstanceList = new List<MySound.VolumeManager>();
    private float mVolumeTgt = 1f;
    private float mVolumeStart = 1f;
    private MySound.EType mType;
    private bool mDiscarded;
    private float mInterpSec;
    private float mPastSec;
    private bool mDiscardAtEnd;

    public VolumeManager(MySound.EType type)
    {
      this.mType = type;
      MySound.VolumeManager.sInstanceList.Add(this);
    }

    public void Discard()
    {
      this.mDiscarded = true;
    }

    public float Volume
    {
      get
      {
        if (this.mDiscarded)
          return 1f;
        if ((double) this.mInterpSec <= 0.0)
          return this.mVolumeTgt;
        return (this.mVolumeTgt - this.mVolumeStart) * this.mPastSec / this.mInterpSec + this.mVolumeStart;
      }
      set
      {
        this.mVolumeStart = this.mVolumeTgt = value;
        this.mPastSec = this.mInterpSec = 0.0f;
      }
    }

    public void SetVolume(float volume, float sec, bool discardAtEnd = false)
    {
      this.mVolumeStart = this.Volume;
      this.mVolumeTgt = volume;
      this.mPastSec = 0.0f;
      this.mInterpSec = sec;
      this.mDiscardAtEnd = discardAtEnd;
    }

    public void Update()
    {
      if ((double) this.mInterpSec <= 0.0)
        return;
      this.mPastSec += Time.unscaledDeltaTime;
      if ((double) this.mPastSec < (double) this.mInterpSec)
        return;
      this.mPastSec = this.mInterpSec = 0.0f;
      if (!this.mDiscardAtEnd)
        return;
      this.Discard();
    }

    public static void UpdateAll()
    {
      foreach (MySound.VolumeManager sInstance in MySound.VolumeManager.sInstanceList)
        sInstance.Update();
      MySound.VolumeManager.sInstanceList.RemoveAll((Predicate<MySound.VolumeManager>) (m => m.mDiscarded));
    }

    public static float GetVolume(MySound.EType type)
    {
      float val1 = 1f;
      foreach (MySound.VolumeManager sInstance in MySound.VolumeManager.sInstanceList)
      {
        if (sInstance.mType == type)
          val1 = Math.Min(val1, sInstance.Volume);
      }
      return val1;
    }
  }

  public class VolumeHandle
  {
    private MySound.VolumeManager mMng;

    public VolumeHandle(MySound.EType type)
    {
      this.mMng = new MySound.VolumeManager(type);
      this.DiscardAtEndSec = 1f;
    }

    public float DiscardAtEndSec { get; set; }

    ~VolumeHandle()
    {
      this.Discard();
    }

    public void Discard()
    {
      if (this.mMng == null)
        return;
      this.mMng.SetVolume(1f, this.DiscardAtEndSec, true);
      this.mMng = (MySound.VolumeManager) null;
    }

    public void SetVolume(float volume, float sec)
    {
      if (this.mMng == null)
        return;
      this.mMng.SetVolume(volume, sec, false);
    }
  }

  private class Source
  {
    private string mName = "null";
    private CriAtomSource mCore;
    private MySound.EType mType;
    private bool mFadeOutStop;
    private bool mFadeOutStopIsTemporary;
    private float mFadeOutSec;
    private float mTimer;
    private string mDelayCueName;
    private float mDelaySec;

    public string name
    {
      get
      {
        return this.mName;
      }
    }

    public float Pitch
    {
      get
      {
        if ((Object) this.mCore == (Object) null)
          return 0.0f;
        return this.mCore.pitch;
      }
      set
      {
        if (!((Object) this.mCore != (Object) null))
          return;
        this.mCore.pitch = value;
      }
    }

    private float GetTypeVolume()
    {
      float volume = MySound.VolumeManager.GetVolume(this.mType);
      if (this.mType == MySound.EType.BGM)
        return GameUtility.Config_MusicVolume * volume;
      if (this.mType == MySound.EType.VOICE)
        return GameUtility.Config_VoiceVolume * volume;
      return GameUtility.Config_SoundVolume * volume;
    }

    public bool Setup(string sheetName, string srcName, bool androidLowLatency, MySound.EType type)
    {
      GameObject gameObject = new GameObject(srcName, new System.Type[1]
      {
        typeof (CriAtomSource)
      });
      if ((Object) gameObject == (Object) null)
        return false;
      this.mCore = gameObject.GetComponent<CriAtomSource>();
      if ((Object) this.mCore == (Object) null)
      {
        Object.Destroy((Object) gameObject);
        return false;
      }
      this.mName = this.mCore.name;
      gameObject.transform.parent = MonoSingleton<MySound>.Instance.gameObject.transform;
      this.mCore.cueSheet = sheetName;
      this.mCore.androidUseLowLatencyVoicePool = androidLowLatency;
      this.mType = type;
      return true;
    }

    public void Cleanup()
    {
      if (!((Object) this.mCore != (Object) null))
        return;
      this.mCore.Stop();
      Object.Destroy((Object) this.mCore.gameObject);
      this.mCore = (CriAtomSource) null;
    }

    public bool KeepInstance { get; set; }

    public bool IsReadyToRemove
    {
      get
      {
        if ((Object) this.mCore == (Object) null)
        {
          DebugUtility.Log("[MySound] source is ready to remove:null");
          return true;
        }
        if (this.KeepInstance || this.IsPlaying)
          return false;
        DebugUtility.Log("[MySound] source is ready to remove:" + this.mCore.gameObject.name);
        return true;
      }
    }

    public bool IsPlaying
    {
      get
      {
        if ((Object) this.mCore == (Object) null)
          return false;
        if (!string.IsNullOrEmpty(this.mDelayCueName))
          return true;
        CriAtomSource.Status status = this.mCore.status;
        if (status != CriAtomSource.Status.Prep)
          return status == CriAtomSource.Status.Playing;
        return true;
      }
    }

    public void Update()
    {
      if ((Object) this.mCore == (Object) null)
        return;
      if (!string.IsNullOrEmpty(this.mDelayCueName))
      {
        this.mDelaySec -= Time.deltaTime;
        if (!this.mFadeOutStop && (double) this.mDelaySec <= 0.0)
        {
          this.Play(this.mDelayCueName, false);
          this.mDelayCueName = (string) null;
        }
      }
      float typeVolume = this.GetTypeVolume();
      if (!this.mFadeOutStop)
      {
        this.mCore.volume = typeVolume;
      }
      else
      {
        this.mTimer += Time.deltaTime;
        if ((double) this.mTimer < (double) this.mFadeOutSec)
        {
          this.mCore.volume = (float) (1.0 - (double) this.mTimer / (double) this.mFadeOutSec) * typeVolume;
        }
        else
        {
          this.mFadeOutStop = !this.mFadeOutStopIsTemporary;
          this.mCore.Stop();
        }
      }
    }

    public void Play(string cueName, bool is_stopplay = false)
    {
      if (MonoSingleton<MySound>.Instance.mClearCacheState != MySound.EClearCacheState.NOP)
        DebugUtility.LogWarning("[MySound] executing clear cache");
      else if ((Object) this.mCore == (Object) null || string.IsNullOrEmpty(cueName))
      {
        DebugUtility.LogWarning("[MySound] null source or cueName");
      }
      else
      {
        string cueSheet = this.mCore.cueSheet;
        if (string.IsNullOrEmpty(cueSheet))
        {
          DebugUtility.LogWarning("[MySound] source sheet is null for cueName:" + cueName);
        }
        else
        {
          CriAtomExAcb acb = CriAtom.GetAcb(cueSheet);
          if (acb == null || !acb.Exists(cueName))
          {
            DebugUtility.LogWarning("[MySound] not exist cueName:" + cueName + " in cueSheet:" + cueSheet);
          }
          else
          {
            this.mCore.volume = this.GetTypeVolume();
            if (is_stopplay)
              this.mCore.Stop();
            this.mCore.Play(cueName);
          }
        }
      }
    }

    public void DelayPlay(string cueName, float sec)
    {
      this.mDelayCueName = cueName;
      this.mDelaySec = sec;
    }

    public void SetLoop(bool flag)
    {
      if ((Object) this.mCore == (Object) null)
        return;
      this.mCore.loop = flag;
    }

    public void FadeOutStop(float sec, bool temporaryFadeOut = false)
    {
      if (this.mFadeOutStop)
        return;
      this.mFadeOutStop = true;
      this.mFadeOutStopIsTemporary = temporaryFadeOut;
      this.mFadeOutSec = sec;
      this.mTimer = 0.0f;
    }
  }

  public class PlayHandle
  {
    public static readonly float HALF_PITCH = 100f;
    private MySound.Source mSrc;

    private PlayHandle(MySound.Source src)
    {
      this.mSrc = src;
    }

    public float Pitch
    {
      get
      {
        if (this.mSrc == null)
          return 0.0f;
        return this.mSrc.Pitch;
      }
      set
      {
        if (this.mSrc == null)
          return;
        this.mSrc.Pitch = value;
      }
    }

    public static MySound.PlayHandle Create()
    {
      MySound.Source sourceToPlayHandle = MonoSingleton<MySound>.Instance.mSourceToPlayHandle;
      if (sourceToPlayHandle == null)
        return (MySound.PlayHandle) null;
      MySound.PlayHandle playHandle = new MySound.PlayHandle(sourceToPlayHandle);
      MonoSingleton<MySound>.Instance.mSourceToPlayHandle = (MySound.Source) null;
      return playHandle;
    }

    ~PlayHandle()
    {
      this.Stop(0.0f);
    }

    public bool KeepInstance
    {
      get
      {
        if (this.mSrc == null)
          return false;
        return this.mSrc.KeepInstance;
      }
      set
      {
        if (this.mSrc == null)
          return;
        this.mSrc.KeepInstance = value;
      }
    }

    public void Play(string cueName)
    {
      if (this.mSrc == null)
        return;
      this.mSrc.Play(cueName, false);
    }

    public void Stop(float sec)
    {
      if (this.mSrc == null)
        return;
      this.mSrc.FadeOutStop(sec, false);
      this.mSrc = (MySound.Source) null;
    }
  }
}
