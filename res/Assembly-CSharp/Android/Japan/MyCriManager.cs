// Decompiled with JetBrains decompiler
// Type: MyCriManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

public class MyCriManager
{
  public static readonly string AcfFileNameTutorialEmb = "Embeded/AlchemistAcf.emb";
  public static readonly string DatFileNameTutorialEmb = "Embeded/stream.emb";
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
    else if (Object.FindObjectOfType(typeof (CriWareInitializer)) != (Object) null)
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already exist. if you added it in scene, remove it.");
    else if (Object.FindObjectOfType(typeof (CriAtom)) != (Object) null)
    {
      DebugUtility.LogError("[MyCriManager] CriAtom already exist. if you added it in scene, remove it.");
    }
    else
    {
      GameObject gameObject = Object.Instantiate<GameObject>((GameObject) Resources.Load("CriWareLibraryInitializer"), Vector3.zero, Quaternion.identity);
      if ((Object) gameObject != (Object) null)
      {
        CriWareInitializer component = gameObject.GetComponent<CriWareInitializer>();
        if ((Object) component != (Object) null)
        {
          if (!AssetManager.UseDLC)
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameTutorialEmb);
            component.decrypterConfig.authenticationFile = MyCriManager.DatFileNameTutorialEmb;
          }
          else if (useEmb)
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameEmb);
            component.decrypterConfig.authenticationFile = MyCriManager.DatFileNameEmb;
          }
          else if (GameUtility.Config_UseAssetBundles.Value)
          {
            MyCriManager.AcfFileName = MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB, false);
            component.decrypterConfig.authenticationFile = MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB, false);
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
    if (AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value)
    {
      if (AssetManager.AssetList != null && AssetManager.AssetList.FindItemByPath("StreamingAssets/" + acb) == null && !isUnManaged)
        return acb;
      if (isUnManaged)
        return AssetManager.GetUnManagedStreamingAssetPath(acb);
      return AssetManager.GetStreamingAssetPath("StreamingAssets/" + acb);
    }
    if (!AssetManager.UseDLC)
      return "Embeded/" + acb;
    return acb;
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
      MyCriManager.AcfFileName = AssetManager.UseDLC ? (!useEmb ? (!GameUtility.Config_UseAssetBundles.Value ? Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameAB) : MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB, false)) : Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameEmb)) : Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameTutorialEmb);
      CriWareInitializer criWareInitializer = !((Object) MyCriManager.sCriWareInitializer == (Object) null) ? MyCriManager.sCriWareInitializer.GetComponent<CriWareInitializer>() : (CriWareInitializer) null;
      if ((Object) criWareInitializer != (Object) null && criWareInitializer.decrypterConfig != null)
      {
        ulong key = criWareInitializer.decrypterConfig.key.Length != 0 ? Convert.ToUInt64(criWareInitializer.decrypterConfig.key) : 0UL;
        string str = !useEmb ? MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB, false) : MyCriManager.DatFileNameEmb;
        if (!AssetManager.UseDLC)
          str = MyCriManager.DatFileNameTutorialEmb;
        if (CriWare.IsStreamingAssetsPath(str))
          str = Path.Combine(CriWare.streamingAssetsPath, str);
        CriWare.criWareUnity_SetDecryptionKey(key, str, criWareInitializer.decrypterConfig.enableAtomDecryption, criWareInitializer.decrypterConfig.enableManaDecryption);
      }
      MyCriManager.UsingEmb = useEmb;
    }
  }
}
