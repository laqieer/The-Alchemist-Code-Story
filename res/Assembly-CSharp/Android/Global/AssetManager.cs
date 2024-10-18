﻿// Decompiled with JetBrains decompiler
// Type: AssetManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetManager : MonoBehaviour
{
  public static AssetManager.AssetFormats Format = AssetManager.AssetFormats.Windows;
  private List<AssetManager.ManagedScene> mScenes = new List<AssetManager.ManagedScene>();
  private List<AssetManager.ManagedAsset> mAssets = new List<AssetManager.ManagedAsset>();
  private List<AssetManager.ManagedAsset> mLoadingAssets = new List<AssetManager.ManagedAsset>();
  private List<AssetBundleCache> mAssetBundles = new List<AssetBundleCache>();
  private List<SceneRequest> mSceneRequests = new List<SceneRequest>();
  public const float AssetBundleExpireTime = 15f;
  private static AssetManager mInstance;
  private static bool mInstanceCreated;
  private bool mScriptsLoaded;
  private static List<string> localizedFileList;
  private static Dictionary<string, string> locDict;
  private static string locLang;
  private AssetList mAssetList;
  private AssetList mTextAssetList;
  private static AssetList mAssetListRef;
  private UnManagedAssetList mUnManagedList;

  static AssetManager()
  {
    RuntimePlatform platform = Application.platform;
    switch (platform)
    {
      case RuntimePlatform.WindowsPlayer:
      case RuntimePlatform.WindowsWebPlayer:
        AssetManager.Format = AssetManager.AssetFormats.Windows;
        break;
      default:
        switch (platform - 8)
        {
          case RuntimePlatform.OSXEditor:
            AssetManager.Format = AssetManager.AssetFormats.iOS;
            return;
          case RuntimePlatform.OSXPlayer:
            return;
          case RuntimePlatform.WindowsPlayer:
            return;
          case RuntimePlatform.OSXWebPlayer:
            if (GameUtility.IsATCTextureSupported)
            {
              AssetManager.Format = AssetManager.AssetFormats.AndroidATC;
              return;
            }
            if (GameUtility.IsDXTTextureSupported)
            {
              AssetManager.Format = AssetManager.AssetFormats.AndroidDXT;
              return;
            }
            if (GameUtility.IsPVRTextureSupported)
            {
              AssetManager.Format = AssetManager.AssetFormats.AndroidPVR;
              return;
            }
            if (GameUtility.IsASTCTextureSupported)
            {
              AssetManager.Format = AssetManager.AssetFormats.AndroidASTC;
              return;
            }
            AssetManager.Format = AssetManager.AssetFormats.AndroidGeneric;
            return;
          default:
            return;
        }
    }
  }

  public static AssetManager Instance
  {
    get
    {
      AssetManager.CreateInstance();
      return AssetManager.mInstance;
    }
  }

  public static bool HasInstance
  {
    get
    {
      return (UnityEngine.Object) AssetManager.mInstance != (UnityEngine.Object) null;
    }
  }

  public static int MaxAssetBundles
  {
    get
    {
      return 200;
    }
  }

  public static void CreateInstance()
  {
    if (AssetManager.mInstanceCreated || !Application.isPlaying)
      return;
    GameObject gameObject = new GameObject(typeof (AssetManager).FullName, new System.Type[1]
    {
      typeof (AssetManager)
    });
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
    AssetManager.mInstance = gameObject.GetComponent<AssetManager>();
    AssetManager.mInstanceCreated = true;
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) AssetManager.mInstance == (UnityEngine.Object) this)
    {
      AssetManager.mInstance = (AssetManager) null;
      AssetManager.mInstanceCreated = false;
    }
    if (this.mAssetList != AssetManager.mAssetListRef)
      return;
    AssetManager.mAssetListRef = (AssetList) null;
  }

  public static void UnloadAll()
  {
    if ((UnityEngine.Object) AssetManager.mInstance != (UnityEngine.Object) null)
    {
      AssetManager.mInstance.mAssets.Clear();
      AssetManager.mInstance.mLoadingAssets.Clear();
      for (int index = AssetManager.mInstance.mAssetBundles.Count - 1; index >= 0; --index)
        AssetManager.mInstance.mAssetBundles[index].Unload();
      AssetManager.mInstance.mAssetBundles.Clear();
      UnityEngine.Object.Destroy((UnityEngine.Object) AssetManager.mInstance.gameObject);
      AssetManager.mInstance = (AssetManager) null;
    }
    AssetManager.mInstanceCreated = false;
    AssetManager.mAssetListRef = (AssetList) null;
  }

  public static string[] GetLoadingAssetNames()
  {
    if ((UnityEngine.Object) AssetManager.mInstance == (UnityEngine.Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mLoadingAssets.Count];
    for (int index = AssetManager.mInstance.mLoadingAssets.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mLoadingAssets[index].Name;
    return strArray;
  }

  public static string[] GetLoadedAssetNames()
  {
    if ((UnityEngine.Object) AssetManager.mInstance == (UnityEngine.Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mAssets.Count];
    for (int index = AssetManager.mInstance.mAssets.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mAssets[index].Name;
    return strArray;
  }

  public static string[] GetOpenedAssetBundleNames()
  {
    if ((UnityEngine.Object) AssetManager.mInstance == (UnityEngine.Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mAssetBundles.Count];
    for (int index = AssetManager.mInstance.mAssetBundles.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mAssetBundles[index].Name;
    return strArray;
  }

  private static bool IsObjectWeakRefAlive(WeakReference refObj)
  {
    if (refObj == null || !refObj.IsAlive)
      return false;
    return (UnityEngine.Object) refObj.Target != (UnityEngine.Object) null;
  }

  private void Update()
  {
    bool flag = true;
    float unscaledDeltaTime = Time.unscaledDeltaTime;
    for (int index = this.mAssetBundles.Count - 1; index >= 0; --index)
      this.mAssetBundles[index].NumReferencers = 0;
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      if (this.mAssets[index].Request_Weak == null || !this.mAssets[index].Request_Weak.IsAlive)
      {
        if (this.mAssets[index].Asset.IsStrong)
          this.mAssets[index].Asset.MakeWeak();
        else if (!this.mAssets[index].Asset.IsAlive)
        {
          this.mAssets[index].Drop();
          this.mAssets.RemoveAt(index);
        }
      }
    }
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      LoadRequest requestStrong = this.mLoadingAssets[index].Request_Strong;
      requestStrong.KeepSourceAlive();
      if (requestStrong.isDone)
      {
        UnityEngine.Object asset = requestStrong.asset;
        AssetManager.ManagedAsset mLoadingAsset = this.mLoadingAssets[index];
        this.mAssets.Add(mLoadingAsset);
        mLoadingAsset.Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateStrongRef(asset);
        mLoadingAsset.HasError = asset == (UnityEngine.Object) null;
        mLoadingAsset.Request_Weak = new WeakReference((object) requestStrong);
        mLoadingAsset.Request_Strong = (LoadRequest) null;
        this.mAssets.Add(this.mLoadingAssets[index]);
        this.mLoadingAssets[index].Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateStrongRef(asset);
        this.mLoadingAssets[index].HasError = asset == (UnityEngine.Object) null;
        this.mLoadingAssets[index].Request = new WeakReference((object) requestStrong);
        this.mLoadingAssets[index].Request2 = (WeakReference) null;
        this.mLoadingAssets.RemoveAt(index);
        if (asset != (UnityEngine.Object) null)
        {
          System.Type type = ((object) asset).GetType();
          mLoadingAsset.IsIndependent = (object) type == (object) typeof (Texture2D) || type.IsSubclassOf(typeof (Texture2D));
        }
      }
    }
    FastLoadRequest.UpdateAll();
    if (this.mSceneRequests.Count > 0)
      flag = false;
    for (int index = 0; index < this.mSceneRequests.Count; ++index)
    {
      if (this.mSceneRequests[index].isDone)
        this.mSceneRequests.RemoveAt(index--);
    }
    if (!flag)
      return;
    this.UpdateAssetBundles(unscaledDeltaTime);
    this.UnloadUnusedAssetBundles(false, false);
  }

  private void UpdateAssetBundles(float dt)
  {
    for (int index = this.mAssetBundles.Count - 1; index >= 0; --index)
    {
      if (!this.mAssetBundles[index].Persistent && this.mAssetBundles[index].NumReferencers <= 0 && (double) this.mAssetBundles[index].ExpireTime < 15.0)
        this.mAssetBundles[index].ExpireTime += dt;
    }
  }

  public void UnloadUnusedAssetBundles(bool immediate = false, bool forceUnload = false)
  {
    for (int index = this.mAssetBundles.Count - 1; index >= 0; --index)
    {
      AssetBundleCache mAssetBundle = this.mAssetBundles[index];
      if (!mAssetBundle.Persistent && (mAssetBundle.NumReferencers <= 0 || forceUnload) && ((immediate || (double) mAssetBundle.ExpireTime >= 15.0) && !this.IsAssetBundleLoading(mAssetBundle)))
      {
        mAssetBundle.Unload();
        this.mAssetBundles.RemoveAt(index);
      }
    }
  }

  private bool IsAssetBundleLoading(AssetBundleCache abc)
  {
    for (int index = 0; index < this.mLoadingAssets.Count; ++index)
    {
      if (this.mLoadingAssets[index].AssetBundles != null && this.mLoadingAssets[index].AssetBundles.Contains(abc))
        return true;
    }
    for (int index = 0; index < this.mScenes.Count; ++index)
    {
      if (this.mScenes[index].Request != null && this.mScenes[index].AssetBundles.Contains(abc))
        return true;
    }
    return false;
  }

  private void OpenScriptAssetBundle()
  {
    if (this.mScriptsLoaded)
      return;
    this.OpenAssetBundle(AssetManager.AssetList.FindItemByID("00000000").IDStr, true, false);
    this.mScriptsLoaded = true;
  }

  private LoadRequest InternalLoadAsync(string name, System.Type type)
  {
    string localizedObjectName = AssetManager.GetLocalizedObjectName(name, false);
    if (name != localizedObjectName)
    {
      if (GameUtility.Config_UseAssetBundles.Value)
      {
        AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(localizedObjectName);
        if (itemByPath != null && itemByPath.Exist)
          name = localizedObjectName;
      }
      else
        name = localizedObjectName;
    }
    AssetManager.ManagedAsset managedAsset = (AssetManager.ManagedAsset) null;
    bool flag = true;
    int hashCode = name.GetHashCode();
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      if (this.mLoadingAssets[index].HashCode == hashCode && (object) this.mLoadingAssets[index].AssetType == (object) type && this.mLoadingAssets[index].Name == name)
        return (LoadRequest) this.mLoadingAssets[index].Request_Weak.Target;
    }
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      managedAsset = this.mAssets[index];
      if (managedAsset.HashCode == hashCode && (object) managedAsset.AssetType == (object) type && managedAsset.Name == name)
      {
        if (managedAsset.Asset.IsAlive && managedAsset.Asset.Target != (UnityEngine.Object) null)
          return (LoadRequest) new ResourceLoadRequest(managedAsset.Asset.Target);
        this.mAssets.RemoveAt(index);
        flag = false;
        break;
      }
    }
    if (flag)
    {
      managedAsset = new AssetManager.ManagedAsset();
      managedAsset.Name = name;
      managedAsset.HashCode = hashCode;
      managedAsset.AssetType = type;
    }
    AssetList.Item itemByPath1 = AssetManager.AssetList.FindItemByPath(name);
    LoadRequest loadRequest;
    if (itemByPath1 == null)
      loadRequest = !type.IsSubclassOf(typeof (Texture)) ? (LoadRequest) new ResourceLoadRequest(Resources.LoadAsync(name, type)) : (LoadRequest) new FastLoadRequest((AssetList.Item) null, name, type);
    else if (type.IsSubclassOf(typeof (Texture)))
    {
      loadRequest = (LoadRequest) new FastLoadRequest(itemByPath1, name, type);
    }
    else
    {
      managedAsset.AssetBundles = new List<AssetBundleCache>();
      AssetBundleCache assetBundle = this.OpenAssetBundleAndDependencies(itemByPath1, 1, managedAsset.AssetBundles, 0.0f);
      if (assetBundle == null)
      {
        loadRequest = (LoadRequest) new ResourceLoadRequest((UnityEngine.Object) null);
        managedAsset.HasError = true;
      }
      else
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(name);
        loadRequest = (LoadRequest) new AssetBundleLoadRequest(assetBundle, withoutExtension, type);
      }
    }
    managedAsset.Request_Weak = new WeakReference((object) loadRequest);
    managedAsset.Request_Strong = loadRequest;
    this.mLoadingAssets.Add(managedAsset);
    return loadRequest;
  }

  public static string GetStreamingAssetPath(string name)
  {
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(name);
    if (itemByPath != null)
      return AssetManager.GetPath(itemByPath);
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    stringBuilder.Append(Application.streamingAssetsPath);
    stringBuilder.Append('/');
    stringBuilder.Append(Path.GetFileName(name));
    return stringBuilder.ToString();
  }

  public static string GetUnManagedStreamingAssetPath(string name)
  {
    return AssetDownloader.DemoCachePath + name;
  }

  public static LoadRequest LoadAsync(string name)
  {
    return AssetManager.LoadAsync(name, typeof (UnityEngine.Object));
  }

  public static LoadRequest LoadAsync<T>(string name)
  {
    return AssetManager.LoadAsync(name, typeof (T));
  }

  public static LoadRequest LoadAsync(string name, System.Type type)
  {
    return AssetManager.Instance.InternalLoadAsync(name, type);
  }

  public static T Load<T>(string name) where T : UnityEngine.Object
  {
    return (T) AssetManager.Load(name, typeof (T));
  }

  public static UnityEngine.Object Load(string name, System.Type type)
  {
    return AssetManager.Instance.InternalLoad(name, type);
  }

  private bool IsAssetBundleOpen(string name)
  {
    for (int index = 0; index < this.mAssetBundles.Count; ++index)
    {
      if (this.mAssetBundles[index].Name == name && (UnityEngine.Object) this.mAssetBundles[index].AssetBundle != (UnityEngine.Object) null)
        return true;
    }
    return false;
  }

  public AssetBundleCache OpenAssetBundleAndDependencies(AssetList.Item node, int refCount = 1, List<AssetBundleCache> result = null, float expireTime = 0.0f)
  {
    if (this.mAssetBundles.Count + (1 + node.Dependencies.Length) > AssetManager.MaxAssetBundles)
      this.UnloadUnusedAssetBundles(true, true);
    AssetBundleCache assetBundleCache1 = this.OpenAssetBundle(node.IDStr, (node.Flags & AssetBundleFlags.Persistent) != (AssetBundleFlags) 0, false);
    if (assetBundleCache1 == null)
      return (AssetBundleCache) null;
    assetBundleCache1.AddReferencer(refCount);
    result?.Add(assetBundleCache1);
    this.OpenScriptAssetBundle();
    bool flag1 = false;
    bool flag2 = false;
    if (assetBundleCache1.Dependencies == null)
    {
      assetBundleCache1.Dependencies = new AssetBundleCache[node.Dependencies.Length + 1];
      assetBundleCache1.Dependencies[assetBundleCache1.Dependencies.Length - 1] = assetBundleCache1;
      flag2 = true;
    }
    for (int index = 0; index < node.Dependencies.Length; ++index)
    {
      AssetList.Item dependency = node.Dependencies[index];
      AssetBundleCache assetBundleCache2 = this.OpenAssetBundle(dependency.IDStr, (dependency.Flags & AssetBundleFlags.Persistent) != (AssetBundleFlags) 0, false);
      if (flag2)
        assetBundleCache1.Dependencies[index] = assetBundleCache2;
      if (assetBundleCache2 != null)
      {
        if (assetBundleCache2.NumReferencers == 0)
          assetBundleCache2.ExpireTime = expireTime;
        result?.Add(assetBundleCache2);
        assetBundleCache2.AddReferencer(refCount);
      }
      flag1 |= assetBundleCache2 == null;
    }
    if (flag1)
      DebugUtility.LogError("Error occurred when opening '" + this.name + "'");
    return assetBundleCache1;
  }

  private static void GenerateLocalizedDict()
  {
    AssetManager.locLang = GameUtility.Config_Language;
    string str1 = AssetManager.LoadTextData("SGDevelopment/LocalizedObjectList");
    if (str1 == null)
      return;
    AssetManager.locDict = new Dictionary<string, string>();
    string str2 = str1;
    string[] separator = new string[2]{ "\r\n", "\n" };
    int num = 0;
    foreach (string path in str2.Split(separator, (StringSplitOptions) num))
    {
      string str3 = (string) null;
      string index;
      if (string.Equals(Path.GetExtension(path), ".unity", StringComparison.OrdinalIgnoreCase))
      {
        index = Path.GetFileNameWithoutExtension(path);
        if (AssetManager.locLang == "french")
          str3 = index + "_fr";
        else if (AssetManager.locLang == "german")
          str3 = index + "_de";
        else if (AssetManager.locLang == "spanish")
          str3 = index + "_es";
      }
      else
      {
        string locLang = AssetManager.locLang;
        if (string.Equals(Path.GetExtension(path), ".prefab", StringComparison.OrdinalIgnoreCase))
          locLang += "/Prefabs/";
        else if (string.Equals(Path.GetExtension(path), ".png", StringComparison.OrdinalIgnoreCase))
          locLang += "/Images/";
        else if (string.Equals(Path.GetExtension(path), ".asset", StringComparison.OrdinalIgnoreCase))
          locLang += "/ScriptObjs/";
        if (path.Contains("Assets/Resources"))
        {
          string str4 = path.Replace("Assets/Resources/", string.Empty);
          index = str4.Substring(0, str4.IndexOf('.'));
          str3 = "SGDevelopment/Loc/" + locLang + index;
        }
        else if (path.Contains("Assets/StreamingAssets/") && string.Equals(Path.GetExtension(path), ".usme", StringComparison.OrdinalIgnoreCase))
        {
          index = path.Replace("Assets/StreamingAssets/", "StreamingAssets/");
          if (AssetManager.locLang == "french")
            str3 = index.Replace(".usme", "_fr.usme");
          else if (AssetManager.locLang == "german")
            str3 = index.Replace(".usme", "_de.usme");
          else if (AssetManager.locLang == "spanish")
            str3 = index.Replace(".usme", "_es.usme");
        }
        else
        {
          index = path;
          str3 = index.Replace("Assets/", "Assets/SGDevelopment/Loc/" + locLang);
        }
      }
      AssetManager.locDict[index] = str3;
    }
  }

  public static string GetLocalizedObjectName(string assetName, bool isScene = false)
  {
    if (GameUtility.Config_Language == "english" || GameUtility.Config_UseAssetBundles.Value && !AssetDownloader.HasDownloadedText)
      return assetName;
    if (AssetManager.locDict == null || AssetManager.locLang != GameUtility.Config_Language)
      AssetManager.GenerateLocalizedDict();
    if (AssetManager.locDict == null)
      return assetName;
    string str = assetName;
    if (AssetManager.locDict.TryGetValue(assetName, out str))
      return str;
    return assetName;
  }

  private static string GetLocalizedFilename(string assetName, string folderType = "/Prefabs/")
  {
    string str = GameUtility.Config_Language + folderType;
    if (assetName.Contains("Assets/"))
      return assetName.Replace("Assets/", "Assets/SGDevelopment/Loc/" + str);
    return "SGDevelopment/Loc/" + str + assetName;
  }

  public UnityEngine.Object InternalLoad(string name, System.Type type)
  {
    AssetManager.ManagedAsset managedAsset = (AssetManager.ManagedAsset) null;
    string localizedObjectName = AssetManager.GetLocalizedObjectName(name, false);
    if (name != localizedObjectName)
    {
      if (GameUtility.Config_UseAssetBundles.Value)
      {
        AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(localizedObjectName);
        if (itemByPath != null && itemByPath.Exist)
          name = localizedObjectName;
      }
      else
        name = localizedObjectName;
    }
    int hashCode = name.GetHashCode();
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      if (this.mLoadingAssets[index].HashCode == hashCode && (object) this.mLoadingAssets[index].AssetType == (object) type && this.mLoadingAssets[index].Name == name)
      {
        managedAsset = this.mLoadingAssets[index];
        this.mLoadingAssets.RemoveAt(index);
        this.mAssets.Add(managedAsset);
        managedAsset.Request_Weak = new WeakReference((object) managedAsset.Request_Strong);
        managedAsset.Request_Strong = (LoadRequest) null;
        managedAsset.Request = new WeakReference((object) managedAsset.Request2);
        managedAsset.Request2 = (WeakReference) null;
        break;
      }
    }
    if (managedAsset == null)
    {
      for (int index = this.mAssets.Count - 1; index >= 0; --index)
      {
        managedAsset = this.mAssets[index];
        if (managedAsset.HashCode == hashCode && (object) managedAsset.AssetType == (object) type && managedAsset.Name == name)
        {
          if (managedAsset.Asset.IsAlive)
          {
            UnityEngine.Object target = managedAsset.Asset.Target;
            if (target != (UnityEngine.Object) null)
              return target;
          }
          if (managedAsset.HasError)
            return (UnityEngine.Object) null;
          break;
        }
        managedAsset = (AssetManager.ManagedAsset) null;
      }
    }
    if (managedAsset == null)
    {
      managedAsset = new AssetManager.ManagedAsset();
      managedAsset.Name = name;
      managedAsset.AssetType = type;
      managedAsset.HashCode = hashCode;
      this.mAssets.Add(managedAsset);
    }
    AssetList.Item itemByPath1 = AssetManager.AssetList.FindItemByPath(name);
    if (itemByPath1 == null)
    {
      managedAsset.Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateWeakRef(Resources.Load(name, type));
    }
    else
    {
      AssetBundleCache assetBundleCache = this.OpenAssetBundleAndDependencies(itemByPath1, 0, (List<AssetBundleCache>) null, 0.0f);
      if (assetBundleCache != null)
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(name);
        UnityEngine.Object @object = !type.IsSubclassOf(typeof (Component)) ? assetBundleCache.AssetBundle.LoadAsset(withoutExtension, type) : (UnityEngine.Object) ((GameObject) assetBundleCache.AssetBundle.LoadAsset(withoutExtension)).GetComponent(type);
        managedAsset.Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateStrongRef(@object);
        managedAsset.Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateWeakRef(@object);
      }
      else
        managedAsset.Asset = AssetManager.ObjectRef<UnityEngine.Object>.CreateStrongRef((UnityEngine.Object) null);
    }
    managedAsset.HasError = managedAsset.Asset.Target == (UnityEngine.Object) null;
    return managedAsset?.Asset.Target;
  }

  public static bool IsLoading
  {
    get
    {
      if ((UnityEngine.Object) AssetManager.mInstance == (UnityEngine.Object) null)
        return false;
      return AssetManager.Instance.mLoadingAssets.Count > 0;
    }
  }

  public static bool IsAssetBundle(string path)
  {
    return AssetManager.AssetList.FindItemByPath(path) != null;
  }

  public static string LoadTextData(string path)
  {
    AssetList.Item itemByPath1 = AssetManager.TxtAssetList.FindItemByPath(path);
    if (itemByPath1 != null)
    {
      int size;
      IntPtr num = NativePlugin.DecompressFile(AssetDownloader.TextCachePath + itemByPath1.IDStr, out size);
      if (num == IntPtr.Zero)
        return (string) null;
      byte[] numArray = new byte[size];
      Marshal.Copy(num, numArray, 0, size);
      NativePlugin.FreePtr(num);
      using (StreamReader streamReader = new StreamReader((Stream) new MemoryStream(numArray), Encoding.UTF8))
        return streamReader.ReadToEnd();
    }
    else
    {
      AssetList.Item itemByPath2 = AssetManager.AssetList.FindItemByPath(path);
      if (itemByPath2 != null)
      {
        int size;
        IntPtr num = NativePlugin.DecompressFile(AssetDownloader.CachePath + itemByPath2.IDStr, out size);
        if (num == IntPtr.Zero)
          return (string) null;
        byte[] numArray = new byte[size];
        Marshal.Copy(num, numArray, 0, size);
        NativePlugin.FreePtr(num);
        using (StreamReader streamReader = new StreamReader((Stream) new MemoryStream(numArray), Encoding.UTF8))
          return streamReader.ReadToEnd();
      }
      else
      {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if ((UnityEngine.Object) textAsset != (UnityEngine.Object) null)
          return textAsset.text;
        return (string) null;
      }
    }
  }

  public static int AssetRevision
  {
    get
    {
      return AssetManager.AssetList.Revision;
    }
  }

  public static UnManagedAssetList UnManagedAsset
  {
    get
    {
      return AssetManager.Instance.mUnManagedList;
    }
  }

  private void Awake()
  {
    this.mAssetList = new AssetList();
    this.mTextAssetList = new AssetList();
    AssetManager.mAssetListRef = this.mAssetList;
    this.mUnManagedList = new UnManagedAssetList();
    if (File.Exists(AssetDownloader.AssetListTmpPath))
      this.mAssetList.ReadAssetList(AssetDownloader.AssetListTmpPath);
    else if (File.Exists(AssetDownloader.AssetListPath))
      this.mAssetList.ReadAssetList(AssetDownloader.AssetListPath);
    if (File.Exists(AssetDownloader.TxtAssetListTmpPath))
    {
      this.mTextAssetList.ReadAssetList(AssetDownloader.TxtAssetListTmpPath);
    }
    else
    {
      if (!File.Exists(AssetDownloader.TxtAssetListPath))
        return;
      this.mTextAssetList.ReadAssetList(AssetDownloader.TxtAssetListPath);
    }
  }

  public static AssetList AssetList
  {
    get
    {
      if (!AssetManager.mInstanceCreated)
        AssetManager.CreateInstance();
      return AssetManager.mInstance.mAssetList;
    }
  }

  public static AssetList TxtAssetList
  {
    get
    {
      if (!AssetManager.mInstanceCreated)
        AssetManager.CreateInstance();
      return AssetManager.mInstance.mTextAssetList;
    }
  }

  private AssetBundleCache OpenAssetBundle(string assetbundleID, bool persistent = false, bool isDependency = false)
  {
    AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(assetbundleID);
    if (itemById == null)
    {
      DebugUtility.LogError("AssetBundle not found: " + assetbundleID);
      return (AssetBundleCache) null;
    }
    for (int index = 0; index < this.mAssetBundles.Count; ++index)
    {
      if (this.mAssetBundles[index].Name == assetbundleID)
        return this.mAssetBundles[index];
    }
    if (this.mAssetBundles.Count >= AssetManager.MaxAssetBundles)
      this.UnloadUnusedAssetBundles(true, true);
    string path = AssetManager.GetPath(itemById);
    if (!File.Exists(path))
    {
      DebugUtility.LogError("AssetBundle doesn't exist: " + assetbundleID + " nodePath: " + itemById.Path);
      return (AssetBundleCache) null;
    }
    if ((itemById.Flags & AssetBundleFlags.RawData) != (AssetBundleFlags) 0)
    {
      DebugUtility.LogError("AssetBundle is RawData: " + assetbundleID);
      return (AssetBundleCache) null;
    }
    AssetBundle ab;
    if ((itemById.Flags & AssetBundleFlags.Compressed) != (AssetBundleFlags) 0)
    {
      int size;
      IntPtr num = NativePlugin.DecompressFile(path, out size);
      if (num == IntPtr.Zero)
      {
        DebugUtility.LogError("Failed to decompress AssetBundle: " + assetbundleID + " nodePath: " + itemById.Path);
        return (AssetBundleCache) null;
      }
      byte[] numArray = new byte[size];
      Marshal.Copy(num, numArray, 0, size);
      NativePlugin.FreePtr(num);
      ab = AssetBundle.LoadFromMemory(numArray);
      if ((UnityEngine.Object) ab == (UnityEngine.Object) null)
        DebugUtility.LogError("Failed to create AssetBundle from memory: " + assetbundleID);
    }
    else
    {
      ab = AssetBundle.LoadFromFile(path);
      if ((UnityEngine.Object) ab == (UnityEngine.Object) null)
        DebugUtility.LogError("Failed to open AssetBundle: " + assetbundleID);
    }
    AssetBundleCache assetBundleCache = new AssetBundleCache(assetbundleID, ab);
    this.mAssetBundles.Add(assetBundleCache);
    assetBundleCache.Persistent = persistent;
    return assetBundleCache;
  }

  private void OnApplicationQuit()
  {
    AssetManager.UnloadAll();
  }

  public static bool IsAssetInCache(string assetID)
  {
    AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(assetID);
    return itemById != null && itemById.Exist;
  }

  public static void PrepareAssets(string resourcePath)
  {
    string localizedObjectName1 = AssetManager.GetLocalizedObjectName(resourcePath, false);
    if (resourcePath != localizedObjectName1)
      resourcePath = localizedObjectName1;
    if (!AssetManager.mInstanceCreated)
      return;
    AssetList.Item itemByPath1 = AssetManager.mAssetListRef.FindItemByPath(resourcePath);
    if (itemByPath1 == null)
      return;
    if (!itemByPath1.Exist)
      AssetDownloader.Add(itemByPath1.IDStr);
    for (int index = 0; index < itemByPath1.Dependencies.Length; ++index)
    {
      if (!itemByPath1.Dependencies[index].Exist)
        AssetDownloader.Add(itemByPath1.Dependencies[index].IDStr);
    }
    for (int index = 0; index < itemByPath1.AdditionalDependencies.Length; ++index)
    {
      string str = AssetDownloader.CachePath + itemByPath1.AdditionalDependencies[index].IDStr;
      AssetManager.PrepareAssets(itemByPath1.AdditionalDependencies[index].Path);
    }
    for (int index = 0; index < itemByPath1.AdditionalStreamingAssets.Length; ++index)
    {
      string path = AssetDownloader.CachePath + itemByPath1.AdditionalStreamingAssets[index].IDStr;
      string localizedObjectName2 = AssetManager.GetLocalizedObjectName(itemByPath1.AdditionalStreamingAssets[index].Path, false);
      if (localizedObjectName2 == itemByPath1.AdditionalStreamingAssets[index].Path)
      {
        if (!File.Exists(path))
          AssetDownloader.Add(itemByPath1.AdditionalStreamingAssets[index].IDStr);
      }
      else
      {
        DebugUtility.LogWarning("Checking localized streaming dependency: " + localizedObjectName2);
        AssetList.Item itemByPath2 = AssetManager.mAssetListRef.FindItemByPath(localizedObjectName2);
        if (itemByPath2 == null)
        {
          if (!File.Exists(path))
            AssetDownloader.Add(itemByPath1.AdditionalStreamingAssets[index].IDStr);
        }
        else if (!File.Exists(AssetDownloader.CachePath + itemByPath2.IDStr))
        {
          DebugUtility.LogWarning("Downloading localized streaming dependency: " + localizedObjectName2);
          AssetDownloader.Add(itemByPath2.IDStr);
        }
      }
    }
  }

  private void ReleaseSceneAssetBundles()
  {
    for (int index = 0; index < AssetManager.Instance.mScenes.Count; ++index)
      AssetManager.Instance.mScenes[index].Drop();
    AssetManager.Instance.mScenes.Clear();
  }

  public static void OnSceneActivate(SceneRequest req)
  {
    AssetManager.Instance.InternalOnSceneActivate(req);
  }

  private void InternalOnSceneActivate(SceneRequest req)
  {
    for (int index1 = 0; index1 < this.mScenes.Count; ++index1)
    {
      if (this.mScenes[index1].Request == req)
      {
        if (!req.isAdditive)
        {
          for (int index2 = 0; index2 < this.mScenes.Count; ++index2)
          {
            if (index1 != index2 && this.mScenes[index2].Request == null)
              this.mScenes[index2].Drop();
          }
        }
        this.mScenes[index1].Request = (SceneRequest) null;
        break;
      }
    }
  }

  public static void UnloadScene(string sceneName)
  {
    for (int index = 0; index < AssetManager.Instance.mScenes.Count; ++index)
    {
      if (AssetManager.Instance.mScenes[index].Name == sceneName)
      {
        AssetManager.Instance.mScenes[index].Drop();
        AssetManager.Instance.mScenes.RemoveAt(index);
        SceneManager.UnloadScene(sceneName);
        break;
      }
    }
  }

  public static void LoadSceneImmediate(string sceneName, bool additive)
  {
    sceneName = AssetManager.GetLocalizedObjectName(sceneName, true);
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(sceneName);
    if (itemByPath == null)
    {
      if (additive)
        Application.LoadLevelAdditive(sceneName);
      else
        Application.LoadLevel(sceneName);
    }
    else
    {
      List<AssetBundleCache> result = new List<AssetBundleCache>();
      AssetManager.Instance.OpenAssetBundleAndDependencies(itemByPath, 1, result, 0.0f);
      AssetManager.ManagedScene managedScene = new AssetManager.ManagedScene();
      managedScene.Name = sceneName;
      managedScene.AssetBundles = result;
      if (additive)
      {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
      }
      else
      {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        AssetManager.Instance.ReleaseSceneAssetBundles();
      }
      AssetManager.Instance.mScenes.Add(managedScene);
    }
  }

  public static SceneRequest LoadSceneAsync(string sceneName, bool additive)
  {
    sceneName = AssetManager.GetLocalizedObjectName(sceneName, true);
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(sceneName);
    List<AssetBundleCache> result = (List<AssetBundleCache>) null;
    SceneRequest sceneRequest;
    if (itemByPath == null)
    {
      sceneRequest = (SceneRequest) new DefaultSceneRequest(!additive ? SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single) : SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive), additive);
    }
    else
    {
      result = new List<AssetBundleCache>();
      AssetManager.Instance.OpenAssetBundleAndDependencies(itemByPath, 1, result, 0.0f);
      sceneRequest = (SceneRequest) new DefaultSceneRequest(!additive ? SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single) : SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive), additive);
    }
    AssetManager.Instance.mSceneRequests.Add(sceneRequest);
    AssetManager.Instance.mScenes.Add(new AssetManager.ManagedScene()
    {
      Name = sceneName,
      AssetBundles = result,
      Request = sceneRequest
    });
    return sceneRequest;
  }

  public static AsyncOperation UnloadUnusedAssets()
  {
    return AssetManager.Instance.InternalUnloadUnusedAssets();
  }

  private AsyncOperation InternalUnloadUnusedAssets()
  {
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      if (!this.mAssets[index].IsIndependent && !this.mAssets[index].Asset.IsStrong)
      {
        this.mAssets[index].Drop();
        this.mAssets.RemoveAt(index);
      }
    }
    return Resources.UnloadUnusedAssets();
  }

  public static string GetPath(AssetList.Item item)
  {
    return AssetDownloader.CachePath + item.IDStr;
  }

  public enum AssetFormats
  {
    AndroidGeneric,
    AndroidDXT,
    AndroidPVR,
    AndroidATC,
    iOS,
    Windows,
    Text,
    AndroidASTC,
  }

  private struct ObjectRef<T> where T : UnityEngine.Object
  {
    private T _StrongRef;
    private WeakReference _WeakRef;
    private bool _IsStrongRef;

    public static AssetManager.ObjectRef<T> CreateStrongRef(T obj)
    {
      return new AssetManager.ObjectRef<T>()
      {
        _StrongRef = obj,
        _WeakRef = new WeakReference((object) obj),
        _IsStrongRef = true
      };
    }

    public static AssetManager.ObjectRef<T> CreateWeakRef(T obj)
    {
      return new AssetManager.ObjectRef<T>()
      {
        _WeakRef = new WeakReference((object) obj)
      };
    }

    public void MakeStrong()
    {
      if (this._IsStrongRef || this._WeakRef == null)
        return;
      this._StrongRef = (T) this._WeakRef.Target;
      this._IsStrongRef = true;
    }

    public void MakeWeak()
    {
      if (!this._IsStrongRef || this._WeakRef == null)
        return;
      this._WeakRef.Target = (object) this._StrongRef;
      this._StrongRef = (T) null;
      this._IsStrongRef = false;
    }

    public T Target
    {
      get
      {
        if (this._IsStrongRef)
          return this._StrongRef;
        return (T) this._WeakRef.Target;
      }
    }

    public bool IsAlive
    {
      get
      {
        if (!((UnityEngine.Object) this._StrongRef != (UnityEngine.Object) null))
          return AssetManager.IsObjectWeakRefAlive(this._WeakRef);
        return true;
      }
    }

    public bool IsStrong
    {
      get
      {
        return (bool) ((UnityEngine.Object) this._StrongRef);
      }
    }
  }

  private class ManagedScene
  {
    public string Name;
    public List<AssetBundleCache> AssetBundles;
    public SceneRequest Request;

    public void Drop()
    {
      if (this.AssetBundles == null)
        return;
      for (int index = 0; index < this.AssetBundles.Count; ++index)
        this.AssetBundles[index].RemoveReferencer(1);
      this.AssetBundles.Clear();
      this.AssetBundles = (List<AssetBundleCache>) null;
    }
  }

  private class ManagedAsset
  {
    public string Name;
    public int HashCode;
    public bool HasError;
    public System.Type AssetType;
    public AssetManager.ObjectRef<UnityEngine.Object> Asset;
    public WeakReference Request;
    public WeakReference Request2;
    public WeakReference Request_Weak;
    public LoadRequest Request_Strong;
    public List<AssetBundleCache> AssetBundles;
    public bool IsIndependent;

    public void Drop()
    {
      if (this.AssetBundles == null)
        return;
      for (int index = 0; index < this.AssetBundles.Count; ++index)
        this.AssetBundles[index].RemoveReferencer(1);
      this.AssetBundles.Clear();
      this.AssetBundles = (List<AssetBundleCache>) null;
    }
  }
}
