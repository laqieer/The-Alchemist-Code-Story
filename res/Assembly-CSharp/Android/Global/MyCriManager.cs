// Decompiled with JetBrains decompiler
// Type: MyCriManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

public class MyCriManager
{
  public static readonly string AcfFileNameEmb = "AlchemistAcf.emb";
  public static readonly string DatFileNameEmb = "stream.emb";
  public static readonly string AcfFileNameAB = "Alchemist.acf";
  public static readonly string DatFileNameAB = "stream.dat";
  private static bool sInit;
  private static GameObject sCriWareInitializer;

  public static string AcfFileName { get; private set; }

  public static bool UsingEmb { get; private set; }

  public static void Setup(bool useEmb = false)
  {
    if (MyCriManager.sInit)
      return;
    if (CriWareInitializer.IsInitialized())
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already initialized. if you added it or CriAtomSource in scene, remove it.");
    else if (UnityEngine.Object.FindObjectOfType(typeof (CriWareInitializer)) != (UnityEngine.Object) null)
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already exist. if you added it in scene, remove it.");
    else if (UnityEngine.Object.FindObjectOfType(typeof (CriAtom)) != (UnityEngine.Object) null)
    {
      DebugUtility.LogError("[MyCriManager] CriAtom already exist. if you added it in scene, remove it.");
    }
    else
    {
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("CriWareLibraryInitializer"), Vector3.zero, Quaternion.identity);
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        CriWareInitializer component = gameObject.GetComponent<CriWareInitializer>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          if (useEmb)
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameEmb);
            component.decrypterConfig.authenticationFile = MyCriManager.DatFileNameEmb;
          }
          else
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameAB);
            component.decrypterConfig.authenticationFile = MyCriManager.DatFileNameAB;
          }
          component.atomConfig.acfFileName = string.Empty;
          DebugUtility.LogWarning("[MyCriManager] Init with EMB:" + (object) useEmb + " acf:" + MyCriManager.AcfFileName + " dat:" + component.decrypterConfig.authenticationFile);
          MyCriManager.sCriWareInitializer = gameObject;
          MyCriManager.UsingEmb = useEmb;
          string str = new AndroidJavaClass("android.os.Build")?.GetStatic<string>("MODEL");
          if (!string.IsNullOrEmpty(str))
          {
            if (str.CompareTo("F-05D") == 0)
            {
              component.atomConfig.androidBufferingTime = 200;
              component.atomConfig.androidStartBufferingTime = 150;
            }
            if (str.CompareTo("T-01D") == 0)
            {
              component.atomConfig.androidBufferingTime = 200;
              component.atomConfig.androidStartBufferingTime = 150;
            }
            if (str.CompareTo("AT200") == 0)
            {
              component.atomConfig.androidBufferingTime = 220;
              component.atomConfig.androidStartBufferingTime = 220;
            }
            if (str.CompareTo("F-04E") == 0)
            {
              component.atomConfig.androidBufferingTime = 220;
              component.atomConfig.androidStartBufferingTime = 220;
            }
            if (str.CompareTo("F-11D") == 0)
            {
              component.atomConfig.androidBufferingTime = 400;
              component.atomConfig.androidStartBufferingTime = 400;
            }
            if (str.CompareTo("IS15SH") == 0)
            {
              component.atomConfig.androidBufferingTime = 400;
              component.atomConfig.androidStartBufferingTime = 400;
            }
            if (str.CompareTo("IS17SH") == 0)
            {
              component.atomConfig.androidBufferingTime = 400;
              component.atomConfig.androidStartBufferingTime = 400;
            }
            if (str.CompareTo("ISW13F") == 0)
            {
              component.atomConfig.androidBufferingTime = 220;
              component.atomConfig.androidStartBufferingTime = 220;
            }
            DebugUtility.Log("MODEL:" + str);
          }
          component.Initialize();
        }
      }
      MyCriManager.sInit = true;
    }
  }

  public static string GetLoadFileName(string acb, bool isUnManaged = false)
  {
    if (string.IsNullOrEmpty(acb))
      return (string) null;
    if (!GameUtility.Config_UseAssetBundles.Value || AssetManager.AssetList != null && AssetManager.AssetList.FindItemByPath("StreamingAssets/" + acb) == null && !isUnManaged)
      return acb;
    if (isUnManaged)
      return AssetManager.GetUnManagedStreamingAssetPath(acb);
    return AssetManager.GetStreamingAssetPath("StreamingAssets/" + acb);
  }

  public static bool IsInitialized()
  {
    return MyCriManager.sInit;
  }

  public static void ReSetup(bool useEmb)
  {
    if (!MyCriManager.sInit)
    {
      MyCriManager.Setup(useEmb);
    }
    else
    {
      MyCriManager.AcfFileName = !useEmb ? (!GameUtility.Config_UseAssetBundles.Value ? Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameAB) : MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB, false)) : Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameEmb);
      CriWareInitializer criWareInitializer = !((UnityEngine.Object) MyCriManager.sCriWareInitializer == (UnityEngine.Object) null) ? MyCriManager.sCriWareInitializer.GetComponent<CriWareInitializer>() : (CriWareInitializer) null;
      if ((UnityEngine.Object) criWareInitializer != (UnityEngine.Object) null && criWareInitializer.decrypterConfig != null)
      {
        ulong key = criWareInitializer.decrypterConfig.key.Length != 0 ? Convert.ToUInt64(criWareInitializer.decrypterConfig.key) : 0UL;
        string str = !useEmb ? MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB, false) : MyCriManager.DatFileNameEmb;
        if (CriWare.IsStreamingAssetsPath(str))
          str = Path.Combine(CriWare.streamingAssetsPath, str);
        CriWare.criWareUnity_SetDecryptionKey(key, str, criWareInitializer.decrypterConfig.enableAtomDecryption, criWareInitializer.decrypterConfig.enableManaDecryption);
      }
      MyCriManager.UsingEmb = useEmb;
    }
  }
}
