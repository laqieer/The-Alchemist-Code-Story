// Decompiled with JetBrains decompiler
// Type: DataSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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

  public T FindDataOfClass<T>(T defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; (UnityEngine.Object) dataSource != (UnityEngine.Object) null; dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == (object) typeof (T))
          return (T) dataSource.mData[index].Data;
      }
      Transform parent = dataSource.transform.parent;
      if (!((UnityEngine.Object) parent == (UnityEngine.Object) null))
        componentsInParent = parent.GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public object FindDataOfClass(object type, object defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; (UnityEngine.Object) dataSource != (UnityEngine.Object) null; dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == type)
          return dataSource.mData[index].Data;
      }
      Transform parent = dataSource.transform.parent;
      if (!((UnityEngine.Object) parent == (UnityEngine.Object) null))
        componentsInParent = parent.GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public static DataSource Create(GameObject obj)
  {
    DataSource dataSource = obj.GetComponent<DataSource>();
    if ((UnityEngine.Object) dataSource == (UnityEngine.Object) null)
    {
      dataSource = obj.AddComponent<DataSource>();
      dataSource.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
    }
    return dataSource;
  }

  public static void Bind<T>(GameObject obj, T data)
  {
    DataSource.Bind(obj, typeof (T), (object) data);
  }

  public static void Bind(GameObject obj, System.Type type, object data)
  {
    DataSource.Create(obj).Add(type, data);
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
