// Decompiled with JetBrains decompiler
// Type: DataSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("")]
public class DataSource : MonoBehaviour
{
  private List<DataSource.DataPair> mData = new List<DataSource.DataPair>();

  public void Clear() => this.mData.Clear();

  public void Add(System.Type type, object data)
  {
    for (int index = 0; index < this.mData.Count; ++index)
    {
      if ((object) this.mData[index].Type == (object) type)
      {
        this.mData[index] = new DataSource.DataPair(type, data);
        return;
      }
    }
    this.mData.Add(new DataSource.DataPair(type, data));
  }

  public static T FindDataOfClass<T>(GameObject root, T defaultValue)
  {
    DataSource[] componentsInParent = root.GetComponentsInParent<DataSource>(true);
    return componentsInParent.Length > 0 ? componentsInParent[0].FindDataOfClass<T>(defaultValue) : defaultValue;
  }

  public static object FindDataOfClass(GameObject root, System.Type type, object defaultValue)
  {
    DataSource[] componentsInParent = root.GetComponentsInParent<DataSource>(true);
    return componentsInParent.Length > 0 ? componentsInParent[0].FindDataOfClass((object) type, defaultValue) : defaultValue;
  }

  public static T[] FindDataOfClassChildren<T>(GameObject root, T defaultValue)
  {
    DataSource[] componentsInChildren = root.GetComponentsInChildren<DataSource>(true);
    if (componentsInChildren.Length > 0)
    {
      List<T> objList = new List<T>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
        objList.Add(componentsInChildren[index].FindDataOfClass<T>(defaultValue));
      return objList.ToArray();
    }
    return new T[1]{ defaultValue };
  }

  public static object[] FindDataOfClassChildren(GameObject root, System.Type type, object defaultValue)
  {
    DataSource[] componentsInChildren = root.GetComponentsInChildren<DataSource>(true);
    if (componentsInChildren.Length > 0)
    {
      List<object> objectList = new List<object>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
        objectList.Add(componentsInChildren[index].FindDataOfClass<object>(defaultValue));
      return objectList.ToArray();
    }
    return new object[1]{ defaultValue };
  }

  public T FindDataOfClass<T>(T defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; Object.op_Inequality((Object) dataSource, (Object) null); dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == (object) typeof (T))
          return (T) dataSource.mData[index].Data;
      }
      Transform parent = ((Component) dataSource).transform.parent;
      if (!Object.op_Equality((Object) parent, (Object) null))
        componentsInParent = ((Component) parent).GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public object FindDataOfClass(object type, object defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; Object.op_Inequality((Object) dataSource, (Object) null); dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == type)
          return dataSource.mData[index].Data;
      }
      Transform parent = ((Component) dataSource).transform.parent;
      if (!Object.op_Equality((Object) parent, (Object) null))
        componentsInParent = ((Component) parent).GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public static T FindDataOfClassAs<T>(GameObject root, T defaultValue) where T : class
  {
    DataSource[] componentsInParent = root.GetComponentsInParent<DataSource>(true);
    return componentsInParent.Length > 0 ? componentsInParent[0].FindDataOfClassAs<T>(defaultValue) : defaultValue;
  }

  public T FindDataOfClassAs<T>(T defaultValue) where T : class
  {
    System.Type c = typeof (T);
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; Object.op_Inequality((Object) dataSource, (Object) null); dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == (object) c || dataSource.mData[index].Type.IsSubclassOf(c))
          return dataSource.mData[index].Data as T;
        if (dataSource.mData[index].Data != null && dataSource.mData[index].Data is T)
          return dataSource.mData[index].Data as T;
      }
      Transform parent = ((Component) dataSource).transform.parent;
      if (!Object.op_Equality((Object) parent, (Object) null))
        componentsInParent = ((Component) parent).GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public static DataSource Create(GameObject obj)
  {
    DataSource dataSource = obj.GetComponent<DataSource>();
    if (Object.op_Equality((Object) dataSource, (Object) null))
    {
      dataSource = obj.AddComponent<DataSource>();
      ((Object) dataSource).hideFlags = (HideFlags) 60;
    }
    return dataSource;
  }

  public static void Bind<T>(GameObject obj, T data, bool is_clear = false)
  {
    DataSource.Bind(obj, typeof (T), (object) data, is_clear);
  }

  public static void Bind(GameObject obj, System.Type type, object data, bool is_clear = false)
  {
    DataSource dataSource = DataSource.Create(obj);
    if (is_clear)
      dataSource.Clear();
    dataSource.Add(type, data);
  }

  public static void Clear(GameObject go)
  {
    if (!Object.op_Implicit((Object) go))
      return;
    DataSource component = go.GetComponent<DataSource>();
    if (!Object.op_Implicit((Object) component))
      return;
    component.Clear();
  }

  private struct DataPair
  {
    public System.Type Type;
    public object Data;

    public DataPair(System.Type type, object data)
    {
      this.Type = type;
      this.Data = data;
    }
  }
}
