// Decompiled with JetBrains decompiler
// Type: AssetManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetManager : MonoBehaviour
{
  private List<AssetManager.ManagedScene> mScenes = new List<AssetManager.ManagedScene>();
  private List<AssetManager.ManagedAsset> mAssets = new List<AssetManager.ManagedAsset>();
  private List<AssetManager.ManagedAsset> mLoadingAssets = new List<AssetManager.ManagedAsset>();
  private List<AssetBundleCache> mAssetBundles = new List<AssetBundleCache>();
  private List<SceneRequest> mSceneRequests = new List<SceneRequest>();
  private List<string> mRemoveSceneAssetLists = new List<string>();
  public static readonly string EMBEDED_SCENE_SUFFIX = "_Embeded";
  private static bool bUseDLC = true;
  private static Dictionary<uint, AssetList.Item> mRecordedPrepareAssets = new Dictionary<uint, AssetList.Item>();
  public static AssetManager.AssetFormats Format = AssetManager.AssetFormats.Windows;
  private static AssetList mAssetListRef = (AssetList) null;
  private static AssetManager mInstance;
  private static bool mIsRecordPrepareAssets;
  private static bool mInstanceCreated;
  private bool mScriptsLoaded;
  private AssetList mAssetList;
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
            AssetManager.Format = AssetManager.AssetFormats.AndroidGeneric;
            return;
          default:
            return;
        }
    }
  }

  public static bool UseDLC
  {
    get
    {
      return true;
    }
    set
    {
      AssetManager.bUseDLC = value;
    }
  }

  public static bool IsRecordPrepareAssets
  {
    get
    {
      return AssetManager.mIsRecordPrepareAssets;
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
      return (Object) AssetManager.mInstance != (Object) null;
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
    Object.DontDestroyOnLoad((Object) gameObject);
    AssetManager.mInstance = gameObject.GetComponent<AssetManager>();
    AssetManager.mInstanceCreated = true;
  }

  private void OnDestroy()
  {
    this.mAssets.Clear();
    this.mLoadingAssets.Clear();
    for (int index = this.mAssetBundles.Count - 1; index >= 0; --index)
      this.mAssetBundles[index].Unload();
    this.mAssetBundles.Clear();
    if ((Object) AssetManager.mInstance == (Object) this)
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
    if ((Object) AssetManager.mInstance != (Object) null)
    {
      Object.Destroy((Object) AssetManager.mInstance.gameObject);
      AssetManager.mInstance = (AssetManager) null;
    }
    AssetManager.mInstanceCreated = false;
    AssetManager.mAssetListRef = (AssetList) null;
  }

  public static string[] GetLoadingAssetNames()
  {
    if ((Object) AssetManager.mInstance == (Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mLoadingAssets.Count];
    for (int index = AssetManager.mInstance.mLoadingAssets.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mLoadingAssets[index].Name;
    return strArray;
  }

  public static string[] GetLoadedAssetNames()
  {
    if ((Object) AssetManager.mInstance == (Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mAssets.Count];
    for (int index = AssetManager.mInstance.mAssets.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mAssets[index].Name;
    return strArray;
  }

  public static string[] GetOpenedAssetBundleNames()
  {
    if ((Object) AssetManager.mInstance == (Object) null || !Application.isPlaying)
      return new string[0];
    string[] strArray = new string[AssetManager.mInstance.mAssetBundles.Count];
    for (int index = AssetManager.mInstance.mAssetBundles.Count - 1; index >= 0; --index)
      strArray[index] = AssetManager.mInstance.mAssetBundles[index].Name;
    return strArray;
  }

  private void Update()
  {
    bool flag = true;
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      if ((this.mAssets[index].Request_Weak == null || !this.mAssets[index].Request_Weak.IsAlive) && !this.mAssets[index].Object_Weak.IsAlive)
      {
        this.mAssets[index].Drop();
        this.mAssets.RemoveAt(index);
      }
    }
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      LoadRequest requestStrong = this.mLoadingAssets[index].Request_Strong;
      requestStrong.KeepSourceAlive();
      if (requestStrong.isDone)
      {
        Object asset = requestStrong.asset;
        AssetManager.ManagedAsset mLoadingAsset = this.mLoadingAssets[index];
        this.mAssets.Add(mLoadingAsset);
        mLoadingAsset.Object_Weak = new UnityWeakReference<Object>(asset);
        mLoadingAsset.HasError = asset == (Object) null;
        mLoadingAsset.Request_Weak = new WeakReference((object) requestStrong);
        mLoadingAsset.Request_Strong = (LoadRequest) null;
        this.mLoadingAssets.RemoveAt(index);
        if (asset != (Object) null)
        {
          System.Type type = asset.GetType();
          mLoadingAsset.IsIndependent = type == typeof (Texture2D) || type.IsSubclassOf(typeof (Texture2D));
        }
      }
    }
    FastLoadRequest.UpdateAll();
    if (this.mSceneRequests.Count > 0)
      flag = false;
    for (int index = this.mRemoveSceneAssetLists.Count - 1; index >= 0; --index)
    {
      AssetManager.RemoveSceneAsset(this.mRemoveSceneAssetLists[index]);
      this.mRemoveSceneAssetLists.RemoveAt(index);
    }
    for (int index = 0; index < this.mSceneRequests.Count; ++index)
    {
      if (this.mSceneRequests[index].isDone)
      {
        string sceneName = this.mSceneRequests[index].SceneName;
        if (!string.IsNullOrEmpty(sceneName))
          this.mRemoveSceneAssetLists.Add(sceneName);
        this.mSceneRequests.RemoveAt(index--);
      }
    }
    if (!flag)
      return;
    this.UnloadUnusedAssetBundles(false, false);
  }

  public void UnloadUnusedAssetBundles(bool immediate = false, bool forceUnload = false)
  {
    for (int index = this.mAssetBundles.Count - 1; index >= 0; --index)
    {
      AssetBundleCache mAssetBundle = this.mAssetBundles[index];
      if (!mAssetBundle.Persistent && (mAssetBundle.NumReferencers <= 0 || forceUnload) && !this.IsAssetBundleLoading(mAssetBundle))
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
    AssetManager.ManagedAsset managedAsset = (AssetManager.ManagedAsset) null;
    bool flag = true;
    int hashCode = name.GetHashCode();
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      if (this.mLoadingAssets[index].HashCode == hashCode && this.mLoadingAssets[index].AssetType == type && this.mLoadingAssets[index].Name == name)
        return (LoadRequest) this.mLoadingAssets[index].Request_Weak.Target;
    }
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      managedAsset = this.mAssets[index];
      if (managedAsset.HashCode == hashCode && managedAsset.AssetType == type && managedAsset.Name == name)
      {
        Object target = (Object) null;
        if (managedAsset.Object_Weak.TryGetTarget(out target))
          return (LoadRequest) new ResourceLoadRequest(target);
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
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(name);
    LoadRequest loadRequest;
    if (!AssetManager.UseDLC || itemByPath == null)
      loadRequest = !type.IsSubclassOf(typeof (Texture)) ? (LoadRequest) new ResourceLoadRequest(Resources.LoadAsync(AssetManager.ConvertEmbededResourcesPath(name), type)) : (LoadRequest) new FastLoadRequest((AssetList.Item) null, name, type);
    else if (type.IsSubclassOf(typeof (Texture)))
    {
      loadRequest = (LoadRequest) new FastLoadRequest(itemByPath, name, type);
    }
    else
    {
      managedAsset.AssetBundles = new List<AssetBundleCache>();
      AssetBundleCache assetBundle = this.OpenAssetBundleAndDependencies(itemByPath, 1, managedAsset.AssetBundles);
      if (assetBundle == null)
      {
        loadRequest = (LoadRequest) new ResourceLoadRequest((Object) null);
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
    if (AssetManager.UseDLC && itemByPath != null)
      return AssetManager.GetPath(itemByPath);
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    stringBuilder.Append(Application.streamingAssetsPath);
    stringBuilder.Append('/');
    stringBuilder.Append(Path.GetFileName(name));
    return stringBuilder.ToString();
  }

  public static string ConvertEmbededSceneName(string scene_name, bool is_force_embeded_name = false)
  {
    if (is_force_embeded_name || !AssetManager.UseDLC)
      return scene_name + AssetManager.EMBEDED_SCENE_SUFFIX;
    return scene_name;
  }

  public static string ConvertEmbededResourcesPath(string path)
  {
    if (AssetManager.UseDLC)
      return path;
    return "Embeded/" + path;
  }

  public static string GetUnManagedStreamingAssetPath(string name)
  {
    return AssetDownloader.DemoCachePath + name;
  }

  public static LoadRequest LoadAsync(string name)
  {
    return AssetManager.LoadAsync(name, typeof (Object));
  }

  public static LoadRequest LoadAsync<T>(string name)
  {
    return AssetManager.LoadAsync(name, typeof (T));
  }

  public static LoadRequest LoadAsync(string name, System.Type type)
  {
    return AssetManager.Instance.InternalLoadAsync(name, type);
  }

  public static T Load<T>(string name) where T : Object
  {
    return (T) AssetManager.Load(name, typeof (T));
  }

  public static Object Load(string name, System.Type type)
  {
    return AssetManager.Instance.InternalLoad(name, type);
  }

  private bool IsAssetBundleOpen(string name)
  {
    for (int index = 0; index < this.mAssetBundles.Count; ++index)
    {
      if (this.mAssetBundles[index].Name == name && (Object) this.mAssetBundles[index].AssetBundle != (Object) null)
        return true;
    }
    return false;
  }

  public AssetBundleCache OpenAssetBundleAndDependencies(AssetList.Item node, int refCount = 1, List<AssetBundleCache> result = null)
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
      AssetBundleCache assetBundleCache2 = this.OpenAssetBundle(node.Dependencies[index].IDStr, (node.Flags & AssetBundleFlags.Persistent) != (AssetBundleFlags) 0, false);
      if (flag2)
        assetBundleCache1.Dependencies[index] = assetBundleCache2;
      if (assetBundleCache2 != null)
      {
        result?.Add(assetBundleCache2);
        assetBundleCache2.AddReferencer(refCount);
      }
      flag1 |= assetBundleCache2 == null;
    }
    if (flag1)
      DebugUtility.LogError("Error occurred when opening '" + this.name + "'");
    return assetBundleCache1;
  }

  public Object InternalLoad(string name, System.Type type)
  {
    AssetManager.ManagedAsset managedAsset = (AssetManager.ManagedAsset) null;
    int hashCode = name.GetHashCode();
    for (int index = this.mLoadingAssets.Count - 1; index >= 0; --index)
    {
      if (this.mLoadingAssets[index].HashCode == hashCode && this.mLoadingAssets[index].AssetType == type && this.mLoadingAssets[index].Name == name)
      {
        managedAsset = this.mLoadingAssets[index];
        this.mLoadingAssets.RemoveAt(index);
        this.mAssets.Add(managedAsset);
        managedAsset.Request_Weak = new WeakReference((object) managedAsset.Request_Strong);
        managedAsset.Request_Strong = (LoadRequest) null;
        break;
      }
    }
    if (managedAsset == null)
    {
      for (int index = this.mAssets.Count - 1; index >= 0; --index)
      {
        managedAsset = this.mAssets[index];
        if (managedAsset.HashCode == hashCode && managedAsset.AssetType == type && managedAsset.Name == name)
        {
          Object target = (Object) null;
          if (managedAsset.Object_Weak.TryGetTarget(out target))
            return target;
          if (managedAsset.HasError)
            return (Object) null;
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
    Object target1 = (Object) null;
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(name);
    if (!AssetManager.UseDLC || itemByPath == null)
    {
      target1 = Resources.Load(AssetManager.ConvertEmbededResourcesPath(name), type);
      managedAsset.Object_Weak = new UnityWeakReference<Object>(target1);
    }
    else
    {
      AssetBundleCache assetBundleCache;
      if (managedAsset.AssetBundles != null && managedAsset.AssetBundles.Count != 0)
      {
        assetBundleCache = managedAsset.AssetBundles[0];
      }
      else
      {
        managedAsset.AssetBundles = new List<AssetBundleCache>();
        assetBundleCache = this.OpenAssetBundleAndDependencies(itemByPath, 1, managedAsset.AssetBundles);
      }
      if (assetBundleCache != null)
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(name);
        target1 = !type.IsSubclassOf(typeof (Component)) ? assetBundleCache.AssetBundle.LoadAsset(withoutExtension, type) : (Object) ((GameObject) assetBundleCache.AssetBundle.LoadAsset(withoutExtension)).GetComponent(type);
        LoadRequest.UntrackTextComponents(target1);
        managedAsset.Object_Weak = new UnityWeakReference<Object>(target1);
      }
      else
        managedAsset.Object_Weak = new UnityWeakReference<Object>((Object) null);
    }
    managedAsset.HasError = target1 == (Object) null;
    return target1;
  }

  public static bool IsLoading
  {
    get
    {
      if ((Object) AssetManager.mInstance == (Object) null)
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
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(path);
    if (AssetManager.UseDLC && itemByPath != null)
    {
      int size;
      IntPtr num = NativePlugin.DecompressFile(Path.GetFullPath(AssetManager.GetPath(itemByPath)).Replace(Directory.GetCurrentDirectory() + (object) Path.DirectorySeparatorChar, string.Empty), out size);
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
      TextAsset textAsset = Resources.Load<TextAsset>(AssetManager.ConvertEmbededResourcesPath(path));
      if ((Object) textAsset != (Object) null)
        return textAsset.text;
      return (string) null;
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
    AssetManager.mAssetListRef = this.mAssetList;
    this.mUnManagedList = new UnManagedAssetList();
    if (File.Exists(AssetDownloader.AssetListTmpPath))
    {
      this.mAssetList.ReadAssetList(AssetDownloader.AssetListTmpPath);
    }
    else
    {
      if (!File.Exists(AssetDownloader.AssetListPath))
        return;
      this.mAssetList.ReadAssetList(AssetDownloader.AssetListPath);
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

  private AssetBundleCache OpenAssetBundle(string assetbundleID, bool persistent = false, bool isDependency = false)
  {
    AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(assetbundleID);
    if (itemById == null)
    {
      DebugUtility.LogError("AssetBundle not found: " + assetbundleID);
      return (AssetBundleCache) null;
    }
    if (!AssetManager.UseDLC)
    {
      DebugUtility.LogError("AssetBundle dont use: " + assetbundleID);
      return (AssetBundleCache) null;
    }
    for (int index = 0; index < this.mAssetBundles.Count; ++index)
    {
      if (this.mAssetBundles[index].Name == assetbundleID)
      {
        if (persistent)
          this.mAssetBundles[index].Persistent = persistent;
        return this.mAssetBundles[index];
      }
    }
    if (this.mAssetBundles.Count >= AssetManager.MaxAssetBundles)
      this.UnloadUnusedAssetBundles(true, true);
    string path = Path.GetFullPath(AssetManager.GetPath(itemById)).Replace(Directory.GetCurrentDirectory() + (object) Path.DirectorySeparatorChar, string.Empty);
    if (!File.Exists(path))
    {
      DebugUtility.LogError("AssetBundle doesn't exist: " + assetbundleID);
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
        DebugUtility.LogError("Failed to decompress AssetBundle: " + assetbundleID);
        return (AssetBundleCache) null;
      }
      byte[] numArray = new byte[size];
      Marshal.Copy(num, numArray, 0, size);
      NativePlugin.FreePtr(num);
      ab = AssetBundle.LoadFromMemory(numArray);
      if ((Object) ab == (Object) null)
        DebugUtility.LogError("Failed to create AssetBundle from memory: " + assetbundleID);
    }
    else
    {
      ab = AssetBundle.LoadFromFile(path);
      if ((Object) ab == (Object) null)
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
    if (!AssetManager.mInstanceCreated)
      return;
    AssetList.Item itemByPath = AssetManager.mAssetListRef.FindItemByPath(resourcePath);
    if (itemByPath == null)
      return;
    AssetManager.PrepareAssets(itemByPath);
  }

  public static void PrepareAssets(AssetList.Item node)
  {
    if (!AssetManager.mInstanceCreated || node == null)
      return;
    if (!node.Exist)
      AssetDownloader.Add(node.IDStr);
    if (AssetManager.IsRecordPrepareAssets)
      AssetManager.AddRecordPrepareAsset(node);
    for (int index = 0; index < node.Dependencies.Length; ++index)
    {
      if (!node.Dependencies[index].Exist)
        AssetDownloader.Add(node.Dependencies[index].IDStr);
      if (AssetManager.IsRecordPrepareAssets)
        AssetManager.AddRecordPrepareAsset(node.Dependencies[index]);
    }
    for (int index = 0; index < node.AdditionalDependencies.Length; ++index)
    {
      if (!node.AdditionalDependencies[index].Exist)
        AssetDownloader.Add(node.AdditionalDependencies[index].IDStr);
      if (AssetManager.IsRecordPrepareAssets)
        AssetManager.AddRecordPrepareAsset(node.AdditionalDependencies[index]);
    }
    for (int index = 0; index < node.AdditionalStreamingAssets.Length; ++index)
    {
      if (!node.AdditionalStreamingAssets[index].Exist)
        AssetDownloader.Add(node.AdditionalStreamingAssets[index].IDStr);
      if (AssetManager.IsRecordPrepareAssets)
        AssetManager.AddRecordPrepareAsset(node.AdditionalStreamingAssets[index]);
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
        List<AssetManager.ManagedScene> managedSceneList = new List<AssetManager.ManagedScene>();
        if (!req.isAdditive)
        {
          for (int index2 = 0; index2 < this.mScenes.Count; ++index2)
          {
            if (index1 != index2 && this.mScenes[index2].Request == null)
            {
              this.mScenes[index2].Drop();
              managedSceneList.Add(this.mScenes[index2]);
            }
          }
        }
        this.mScenes[index1].Request = (SceneRequest) null;
        if (this.mScenes.Count == 0 || managedSceneList.Count == 0)
          break;
        for (int index2 = this.mScenes.Count - 1; index2 >= 0; --index2)
        {
          if (managedSceneList.Contains(this.mScenes[index2]))
            this.mScenes.RemoveAt(index2);
        }
        break;
      }
    }
  }

  public static void RemoveSceneAsset(string sceneName)
  {
    if (!(bool) ((Object) AssetManager.Instance) || AssetManager.Instance.mScenes == null)
      return;
    for (int index = AssetManager.Instance.mScenes.Count - 1; index >= 0; --index)
    {
      if (AssetManager.Instance.mScenes[index].Name == sceneName)
      {
        AssetManager.Instance.mScenes[index].Drop();
        AssetManager.Instance.mScenes.RemoveAt(index);
        break;
      }
    }
  }

  public static void UnloadScene(string sceneName)
  {
    SceneManager.UnloadSceneAsync(sceneName);
  }

  public static void LoadSceneImmediate(string sceneName, bool additive)
  {
    if (!additive)
      AssetManager.RemoveWeakAsset();
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(sceneName);
    if (!AssetManager.UseDLC || itemByPath == null)
    {
      if (additive)
      {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
      }
      else
      {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        AssetManager.Instance.ReleaseSceneAssetBundles();
      }
    }
    else
    {
      List<AssetBundleCache> result = new List<AssetBundleCache>();
      AssetManager.Instance.OpenAssetBundleAndDependencies(itemByPath, 1, result);
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
      AssetManager.RemoveSceneAsset(managedScene.Name);
    }
  }

  public static SceneRequest LoadSceneAsync(string sceneName, bool additive)
  {
    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(sceneName);
    List<AssetBundleCache> result = (List<AssetBundleCache>) null;
    if (!additive)
      AssetManager.RemoveWeakAsset();
    SceneRequest sceneRequest;
    if (!AssetManager.UseDLC || itemByPath == null)
    {
      sceneRequest = (SceneRequest) new DefaultSceneRequest(!additive ? SceneManager.LoadSceneAsync(AssetManager.ConvertEmbededSceneName(sceneName, false), LoadSceneMode.Single) : SceneManager.LoadSceneAsync(AssetManager.ConvertEmbededSceneName(sceneName, false), LoadSceneMode.Additive), additive);
    }
    else
    {
      result = new List<AssetBundleCache>();
      AssetManager.Instance.OpenAssetBundleAndDependencies(itemByPath, 1, result);
      sceneRequest = (SceneRequest) new DefaultSceneRequest(!additive ? SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single) : SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive), additive);
    }
    AssetManager.Instance.mSceneRequests.Add(sceneRequest);
    AssetManager.ManagedScene managedScene = new AssetManager.ManagedScene();
    managedScene.Name = sceneName;
    managedScene.AssetBundles = result;
    managedScene.Request = sceneRequest;
    sceneRequest.SceneName = sceneName;
    AssetManager.Instance.mScenes.Add(managedScene);
    return sceneRequest;
  }

  public static AsyncOperation UnloadUnusedAssets()
  {
    return AssetManager.Instance.InternalUnloadUnusedAssets();
  }

  private AsyncOperation InternalUnloadUnusedAssets()
  {
    this.removeWeakAsset();
    return Resources.UnloadUnusedAssets();
  }

  private bool removeWeakAsset()
  {
    bool flag = false;
    for (int index = this.mAssets.Count - 1; index >= 0; --index)
    {
      if (!this.mAssets[index].IsIndependent)
      {
        this.mAssets[index].Drop();
        this.mAssets.RemoveAt(index);
        flag = true;
      }
    }
    return flag;
  }

  public static bool RemoveWeakAsset()
  {
    if (!(bool) ((Object) AssetManager.mInstance))
      return false;
    return AssetManager.mInstance.removeWeakAsset();
  }

  public static string GetPath(AssetList.Item item)
  {
    return AssetDownloader.CachePath + item.IDStr;
  }

  private static void AddRecordPrepareAsset(AssetList.Item item)
  {
    if (item == null || AssetManager.mRecordedPrepareAssets.ContainsKey(item.ID))
      return;
    AssetManager.mRecordedPrepareAssets.Add(item.ID, item);
  }

  public static void ClearRecordPrepareAssets()
  {
    AssetManager.mRecordedPrepareAssets.Clear();
  }

  public static Dictionary<uint, AssetList.Item> GetRecordPrepareAssets()
  {
    return AssetManager.mRecordedPrepareAssets.Values.ToDictionary<AssetList.Item, uint>((Func<AssetList.Item, uint>) (item => item.ID));
  }

  public static void Begin_RecordPrepareAssets()
  {
    AssetManager.mIsRecordPrepareAssets = true;
  }

  public static void End_RecordPrepareAssets()
  {
    AssetManager.mIsRecordPrepareAssets = false;
  }

  public enum AssetFormats
  {
    AndroidGeneric,
    AndroidDXT,
    AndroidPVR,
    AndroidATC,
    iOS,
    Windows,
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
    public UnityWeakReference<Object> Object_Weak;
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

  public class RecordPrepareAssetsScope : IDisposable
  {
    public RecordPrepareAssetsScope()
    {
      AssetManager.Begin_RecordPrepareAssets();
    }

    public void Dispose()
    {
      AssetManager.End_RecordPrepareAssets();
    }
  }
}
