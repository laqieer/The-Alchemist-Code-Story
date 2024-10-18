// Decompiled with JetBrains decompiler
// Type: GameObjectID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameObjectID : MonoBehaviour
{
  private static Dictionary<string, List<GameObject>> mGameObjects = new Dictionary<string, List<GameObject>>();
  [HideInInspector]
  [SerializeField]
  [FormerlySerializedAs("ID")]
  private string mID = "NewGameObjectID";

  public string ID
  {
    set
    {
      if (!(this.mID != value))
        return;
      this.UnregisterInstance();
      this.mID = value;
      this.RegisterInstance();
    }
    get
    {
      return this.mID;
    }
  }

  public static GameObject FindGameObject(string name)
  {
    try
    {
      return GameObjectID.mGameObjects[name][0];
    }
    catch (Exception ex)
    {
      return (GameObject) null;
    }
  }

  public static T FindGameObject<T>(string name) where T : Component
  {
    try
    {
      return GameObjectID.mGameObjects[name][0].GetComponent<T>();
    }
    catch (Exception ex)
    {
      return (T) null;
    }
  }

  public static GameObject[] FindGameObjects(string name)
  {
    try
    {
      return GameObjectID.mGameObjects[name].ToArray();
    }
    catch (Exception ex)
    {
      return new GameObject[0];
    }
  }

  private void RegisterInstance()
  {
    if (string.IsNullOrEmpty(this.mID))
      return;
    if (!GameObjectID.mGameObjects.ContainsKey(this.ID))
      GameObjectID.mGameObjects[this.ID] = new List<GameObject>();
    GameObjectID.mGameObjects[this.ID].Add(this.gameObject);
  }

  private void UnregisterInstance()
  {
    if (string.IsNullOrEmpty(this.mID))
      return;
    try
    {
      List<GameObject> mGameObject = GameObjectID.mGameObjects[this.ID];
      mGameObject.Remove(this.gameObject);
      if (mGameObject.Count > 0)
        return;
      GameObjectID.mGameObjects.Remove(this.ID);
    }
    catch (Exception ex)
    {
    }
  }

  private void Awake()
  {
    this.RegisterInstance();
  }

  private void OnDestroy()
  {
    this.UnregisterInstance();
  }
}
