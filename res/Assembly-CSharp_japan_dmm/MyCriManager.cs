// Decompiled with JetBrains decompiler
// Type: MyCriManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.IO;
using UnityEngine;

#nullable disable
public class MyCriManager
{
  private static bool sInit;
  public static readonly string AcfFileNameTutorialEmb = "Embeded/AlchemistAcf.emb";
  public static readonly string DatFileNameTutorialEmb = "Embeded/stream.emb";
  public static readonly string AcfFileNameEmb = "AlchemistAcf.emb";
  public static readonly string DatFileNameEmb = "stream.emb";
  public static readonly string AcfFileNameAB = "Alchemist.acf";
  public static readonly string DatFileNameAB = "stream.dat";
  private static GameObject sCriWareInitializer;

  public static string AcfFileName { get; private set; }

  public static bool UsingEmb { get; private set; }

  public static void Setup(bool useEmb = false)
  {
    if (MyCriManager.sInit)
      return;
    if (CriWareInitializer.IsInitialized())
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already initialized. if you added it or CriAtomSource in scene, remove it.");
    else if (Object.op_Inequality(Object.FindObjectOfType(typeof (CriWareInitializer)), (Object) null))
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already exist. if you added it in scene, remove it.");
    else if (Object.op_Inequality(Object.FindObjectOfType(typeof (CriAtom)), (Object) null))
    {
      DebugUtility.LogError("[MyCriManager] CriAtom already exist. if you added it in scene, remove it.");
    }
    else
    {
      GameObject gameObject = Object.Instantiate<GameObject>((GameObject) Resources.Load("CriWareLibraryInitializer"), Vector3.zero, Quaternion.identity);
      if (Object.op_Inequality((Object) gameObject, (Object) null))
      {
        CriWareInitializer component = gameObject.GetComponent<CriWareInitializer>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
            MyCriManager.AcfFileName = MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB);
            component.decrypterConfig.authenticationFile = MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB);
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
      return isUnManaged ? AssetManager.GetUnManagedStreamingAssetPath(acb) : AssetManager.GetStreamingAssetPath("StreamingAssets/" + acb);
    }
    return !AssetManager.UseDLC ? "Embeded/" + acb : acb;
  }

  public static bool IsInitialized() => MyCriManager.sInit;

  public static void ReSetup(bool useEmb)
  {
    if (!MyCriManager.sInit)
    {
      MyCriManager.Setup(useEmb);
    }
    else
    {
      MyCriManager.AcfFileName = AssetManager.UseDLC ? (!useEmb ? (!GameUtility.Config_UseAssetBundles.Value ? Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameAB) : MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB)) : Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameEmb)) : Path.Combine(CriWare.streamingAssetsPath, MyCriManager.AcfFileNameTutorialEmb);
      CriWareInitializer component = !Object.op_Equality((Object) MyCriManager.sCriWareInitializer, (Object) null) ? MyCriManager.sCriWareInitializer.GetComponent<CriWareInitializer>() : (CriWareInitializer) null;
      if (Object.op_Inequality((Object) component, (Object) null) && component.decrypterConfig != null)
      {
        string path2 = !useEmb ? MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB) : MyCriManager.DatFileNameEmb;
        if (!AssetManager.UseDLC)
          path2 = MyCriManager.DatFileNameTutorialEmb;
        if (CriWare.IsStreamingAssetsPath(path2))
          path2 = Path.Combine(CriWare.streamingAssetsPath, path2);
        CriWareDecrypter.Initialize(component.decrypterConfig.key, path2, component.decrypterConfig.enableAtomDecryption, component.decrypterConfig.enableManaDecryption);
      }
      MyCriManager.UsingEmb = useEmb;
    }
  }
}
