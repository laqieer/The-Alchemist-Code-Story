// Decompiled with JetBrains decompiler
// Type: SerializeValueList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[Serializable]
public class SerializeValueList
{
  [SerializeField]
  private List<SerializeValue> m_Fields = new List<SerializeValue>();

  public SerializeValueList()
  {
  }

  public SerializeValueList(SerializeValueList list) => this.Write(list);

  public SerializeValueList(List<SerializeValue> array) => this.m_Fields = array;

  public SerializeValueList(SerializeValue[] array)
  {
    this.m_Fields.AddRange((IEnumerable<SerializeValue>) array);
  }

  public List<SerializeValue> list => this.m_Fields;

  public int Count => this.m_Fields.Count;

  public SerializeValue[] ToArray() => this.m_Fields.ToArray();

  public List<string> GetKeys()
  {
    List<string> keys = new List<string>();
    for (int i = 0; i < this.m_Fields.Count; ++i)
    {
      if (keys.FindIndex((Predicate<string>) (prop => prop == this.m_Fields[i].key)) == -1)
        keys.Add(this.m_Fields[i].key);
    }
    return keys;
  }

  public List<int> GetGroups()
  {
    List<int> groups = new List<int>();
    for (int i = 0; i < this.m_Fields.Count; ++i)
    {
      if (groups.FindIndex((Predicate<int>) (prop => prop == this.m_Fields[i].group)) == -1)
        groups.Add(this.m_Fields[i].group);
    }
    return groups;
  }

  public void Initialize()
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
      this.m_Fields[index].Reset();
  }

  public void Release()
  {
  }

  public void Clear()
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
      this.m_Fields[index].Reset();
  }

  public void Write(SerializeValueList src)
  {
    if (src.list == null)
      return;
    List<SerializeValue> list = src.list;
    for (int index = 0; index < list.Count; ++index)
      this.SetField(list[index]);
  }

  public void RemoveFieldAt(int index) => this.m_Fields.RemoveAt(index);

  public void RemoveField(SerializeValue value) => this.m_Fields.Remove(value);

  public void RemoveField(string key)
  {
    int index = this.m_Fields.FindIndex((Predicate<SerializeValue>) (o => o.key == key));
    if (index == -1)
      return;
    this.m_Fields.RemoveAt(index);
  }

  public SerializeValue NewField(SerializeValue value)
  {
    this.m_Fields.Add(value);
    return value;
  }

  public SerializeValue NewField(SerializeValue.Type type, string key)
  {
    SerializeValue serializeValue = new SerializeValue(type, key);
    this.m_Fields.Add(serializeValue);
    return serializeValue;
  }

  public void Add(SerializeValueList list)
  {
    SerializeValue[] array = list.ToArray();
    if (array == null)
      return;
    for (int index = 0; index < array.Length; ++index)
      this.m_Fields.Add(array[index]);
  }

  public SerializeValue AddField(SerializeValue value)
  {
    SerializeValue field = this.GetField(value.key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(value));
    return field;
  }

  public SerializeValue AddField(SerializeValue.Type type, string key)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(field);
    return field;
  }

  public SerializeValue AddField(SerializeValue.Type type, string key, GameObject obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, bool obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, int obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, float obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, string obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector2 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector3 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector4 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, GameObject obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Text obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Button obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Toggle obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddGlobal(string key, string fieldName, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(SerializeValue.Type.Global, key, fieldName)
      {
        v_Global = obj
      });
    return field;
  }

  public SerializeValue AddObject(string key, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(SerializeValue.Type.Object, key)
      {
        v_Object = obj
      });
    return field;
  }

  public void SetField(SerializeValue value)
  {
    SerializeValue field = this.GetField(value.key);
    if (field != null)
      field.Write(value);
    else
      this.AddField(value);
  }

  public void SetField(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_bool = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, int value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_int = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, float value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_float = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, string value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_string = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector2 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector2 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector3 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector3 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector4 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector4 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, GameObject value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_GameObject = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Text value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UILabel = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Button value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UIButton = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Toggle value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UIToggle = value;
    else
      this.AddField(key, value);
  }

  public void SetGlobal(string key, string fieldName, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Global = obj;
    else
      this.AddGlobal(key, fieldName, obj);
  }

  public void SetObject(string key, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Object = obj;
    else
      this.AddObject(key, obj);
  }

  public void SetActive(int group, bool sw)
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
    {
      if (this.m_Fields[index].group == group)
      {
        GameObject vGameObject = this.m_Fields[index].v_GameObject;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null) && vGameObject.activeSelf != sw)
          vGameObject.SetActive(sw);
      }
    }
  }

  public GameObject SetActive(string key, bool sw)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
      {
        if (vGameObject.activeSelf != sw)
          vGameObject.SetActive(sw);
        return vGameObject;
      }
    }
    return (GameObject) null;
  }

  public GameObject SetActive(string key, bool sw, string label)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
      {
        if (vGameObject.activeSelf != sw)
          vGameObject.SetActive(sw);
        if (field.type == SerializeValue.Type.UILabel)
        {
          Text component = vGameObject.GetComponent<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.text = label;
        }
        return vGameObject;
      }
    }
    return (GameObject) null;
  }

  public void SetUIOn(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      return;
    field.v_UIOn = value;
  }

  public Selectable SetInteractable(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      return (Selectable) null;
    Selectable vUiSelectable = field.v_UISelectable;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiSelectable, (UnityEngine.Object) null) && vUiSelectable.interactable != value)
      vUiSelectable.interactable = value;
    return vUiSelectable;
  }

  public SerializeValue GetField(string key)
  {
    return !string.IsNullOrEmpty(key) ? this.m_Fields.Find((Predicate<SerializeValue>) (o => o.key == key)) : (SerializeValue) null;
  }

  public bool TryGetField(string key, out SerializeValue field)
  {
    if (!string.IsNullOrEmpty(key))
    {
      field = this.m_Fields.Find((Predicate<SerializeValue>) (o => o.key == key));
      if (field != null)
        return true;
    }
    field = (SerializeValue) null;
    return false;
  }

  public SerializeValue[] GetFields(int group)
  {
    List<SerializeValue> serializeValueList = new List<SerializeValue>();
    for (int index = 0; index < this.m_Fields.Count; ++index)
    {
      SerializeValue field = this.m_Fields[index];
      if (field != null && field.group == group)
        serializeValueList.Add(field);
    }
    return serializeValueList.ToArray();
  }

  public void GetField(string key, ref bool result) => result = this.GetBool(key);

  public void GetField(string key, ref bool result, bool defaultValue)
  {
    result = this.GetBool(key, defaultValue);
  }

  public bool GetBool(string key) => this.GetBool(key, false);

  public bool GetBool(string key, bool defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_bool : defaultValue;
  }

  public void GetField(string key, ref int result) => result = this.GetInt(key);

  public void GetField(string key, ref int result, int defaultValue)
  {
    result = this.GetInt(key, defaultValue);
  }

  public int GetInt(string key) => this.GetInt(key, 0);

  public int GetInt(string key, int defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_int : defaultValue;
  }

  public void GetField(string key, ref float result) => result = this.GetFloat(key, 0.0f);

  public void GetField(string key, ref float result, float defaultValue)
  {
    result = this.GetFloat(key, defaultValue);
  }

  public float GetFloat(string key) => this.GetFloat(key, 0.0f);

  public float GetFloat(string key, float defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_float : defaultValue;
  }

  public void GetField(string key, ref string result) => result = this.GetString(key);

  public void GetField(string key, ref string result, string defaultValue)
  {
    result = this.GetString(key, defaultValue);
  }

  public string GetString(string key) => this.GetString(key, string.Empty);

  public string GetString(string key, string defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_string : defaultValue;
  }

  public void GetField(string key, ref Vector2 result) => result = this.GetVector2(key);

  public void GetField(string key, ref Vector2 result, Vector2 defaultValue)
  {
    result = this.GetVector2(key, defaultValue);
  }

  public Vector2 GetVector2(string key) => this.GetVector2(key, Vector2.zero);

  public Vector2 GetVector2(string key, Vector2 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_Vector2 : defaultValue;
  }

  public void GetField(string key, ref Vector3 result) => result = this.GetVector3(key);

  public void GetField(string key, ref Vector3 result, Vector3 defaultValue)
  {
    result = this.GetVector3(key, defaultValue);
  }

  public Vector3 GetVector3(string key) => this.GetVector3(key, Vector3.zero);

  public Vector3 GetVector3(string key, Vector3 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_Vector3 : defaultValue;
  }

  public void GetField(string key, ref Vector4 result) => result = this.GetVector4(key);

  public void GetField(string key, ref Vector4 result, Vector4 defaultValue)
  {
    result = this.GetVector4(key, defaultValue);
  }

  public Vector4 GetVector4(string key) => this.GetVector4(key, Vector4.zero);

  public Vector4 GetVector4(string key, Vector4 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_Vector4 : defaultValue;
  }

  public void GetField(string key, ref GameObject result) => result = this.GetGameObject(key);

  public void GetField(string key, ref GameObject result, GameObject defaultValue)
  {
    result = this.GetGameObject(key, defaultValue);
  }

  public GameObject GetGameObject(string key) => this.GetGameObject(key, (GameObject) null);

  public GameObject GetGameObject(string key, GameObject defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_GameObject : defaultValue;
  }

  public void GetField(string key, ref Text result) => result = this.GetUILabel(key);

  public void GetField(string key, ref Text result, Text defaultValue)
  {
    result = this.GetUILabel(key, defaultValue);
  }

  public Text GetUILabel(string key) => this.GetUILabel(key, (Text) null);

  public Text GetUILabel(string key, Text defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_UILabel : defaultValue;
  }

  public void GetField(string key, ref Image result) => result = this.GetUIImage(key);

  public void GetField(string key, ref Image result, Image defaultValue)
  {
    result = this.GetUIImage(key, defaultValue);
  }

  public Image GetUIImage(string key) => this.GetUIImage(key, (Image) null);

  public Image GetUIImage(string key, Image defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_UIImage : defaultValue;
  }

  public void GetField(string key, ref Button result) => result = this.GetUIButton(key);

  public void GetField(string key, ref Button result, Button defaultValue)
  {
    result = this.GetUIButton(key, defaultValue);
  }

  public Button GetUIButton(string key) => this.GetUIButton(key, (Button) null);

  public Button GetUIButton(string key, Button defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_UIButton : defaultValue;
  }

  public void GetField(string key, ref Toggle result) => result = this.GetUIToggle(key);

  public void GetField(string key, ref Toggle result, Toggle defaultValue)
  {
    result = this.GetUIToggle(key, defaultValue);
  }

  public Toggle GetUIToggle(string key) => this.GetUIToggle(key, (Toggle) null);

  public Toggle GetUIToggle(string key, Toggle defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_UIToggle : defaultValue;
  }

  public void GetField(string key, ref ScriptableObject result)
  {
    result = this.GetScriptableObject(key);
  }

  public void GetField(string key, ref ScriptableObject result, ScriptableObject defaultValue)
  {
    result = this.GetScriptableObject(key, defaultValue);
  }

  public ScriptableObject GetScriptableObject(string key)
  {
    return this.GetScriptableObject(key, (ScriptableObject) null);
  }

  public ScriptableObject GetScriptableObject(string key, ScriptableObject defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_ScriptableObject : defaultValue;
  }

  public object GetGlobal(string key) => this.GetGlobal(key, (object) null);

  public object GetGlobal(string key, object defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_Global : defaultValue;
  }

  public object GetObject(string key) => this.GetObject(key, (object) null);

  public object GetObject(string key, object defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_Object : defaultValue;
  }

  public T GetObject<T>(string key) => this.GetObject<T>(key, default (T));

  public T GetObject<T>(string key, T defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? (T) field.v_Object : defaultValue;
  }

  public T GetEnum<T>(string key) => this.GetEnum<T>(key, default (T));

  public T GetEnum<T>(string key, T defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.GetEnum<T>() : defaultValue;
  }

  public T GetComponent<T>(string key) where T : Component => this.GetComponent<T>(key, (T) null);

  public T GetComponent<T>(string key, T defaultValue) where T : Component
  {
    GameObject gameObject = this.GetGameObject(key);
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      return defaultValue;
    T component = gameObject.GetComponent<T>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) (object) component, (UnityEngine.Object) null))
      component = gameObject.GetComponentInParent<T>();
    return component;
  }

  public T GetDataSource<T>(string key) => this.GetDataSource<T>(key, default (T));

  public T GetDataSource<T>(string key, T defaultValue)
  {
    DataSource component = this.GetComponent<DataSource>(key);
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) ? component.FindDataOfClass<T>(defaultValue) : defaultValue;
  }

  public T GetContentParam<T>(string key) where T : ContentSource.Param
  {
    return this.GetContentParam<T>(key, (T) null);
  }

  public T GetContentParam<T>(string key, T defaultValue) where T : ContentSource.Param
  {
    ContentNode component = this.GetComponent<ContentNode>(key);
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) ? component.GetParam<T>() : defaultValue;
  }

  public bool GetUIOn(string key) => this.GetUIOn(key, false);

  public bool GetUIOn(string key, bool defaultValue)
  {
    SerializeValue field = this.GetField(key);
    return field != null ? field.v_UIOn : defaultValue;
  }

  public bool HasField(string key)
  {
    return this.m_Fields.FindIndex((Predicate<SerializeValue>) (o => o.key == key)) != -1;
  }

  public bool IsActive(string key)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
        return vGameObject.activeSelf;
    }
    return false;
  }

  public class Group
  {
    public int index;
    public List<SerializeValue> list = new List<SerializeValue>();
  }
}
