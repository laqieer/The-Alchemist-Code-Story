// Decompiled with JetBrains decompiler
// Type: DataSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class DataSource : MonoBehaviour
{
  private List<DataSource.DataPair> mData = new List<DataSource.DataPair>();

  public void Clear()
  {
    this.mData.Clear();
  }

  public void Add(System.Type type, object data)
  {
    for (int index = 0; index < this.mData.Count; ++index)
    {
      if (this.mData[index].Type == type)
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
    if (componentsInParent.Length > 0)
      return componentsInParent[0].FindDataOfClass<T>(defaultValue);
    return defaultValue;
  }

  public static object FindDataOfClass(GameObject root, System.Type type, object defaultValue)
  {
    DataSource[] componentsInParent = root.GetComponentsInParent<DataSource>(true);
    if (componentsInParent.Length > 0)
      return componentsInParent[0].FindDataOfClass((object) type, defaultValue);
    return defaultValue;
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
    for (DataSource dataSource = this; (Object) dataSource != (Object) null; dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if (dataSource.mData[index].Type == typeof (T))
          return (T) dataSource.mData[index].Data;
      }
      Transform parent = dataSource.transform.parent;
      if (!((Object) parent == (Object) null))
        componentsInParent = parent.GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public object FindDataOfClass(object type, object defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; (Object) dataSource != (Object) null; dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if (dataSource.mData[index].Type == type)
          return dataSource.mData[index].Data;
      }
      Transform parent = dataSource.transform.parent;
      if (!((Object) parent == (Object) null))
        componentsInParent = parent.GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public static DataSource Create(GameObject obj)
  {
    DataSource dataSource = obj.GetComponent<DataSource>();
    if ((Object) dataSource == (Object) null)
    {
      dataSource = obj.AddComponent<DataSource>();
      dataSource.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
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
