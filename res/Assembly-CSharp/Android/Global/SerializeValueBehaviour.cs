// Decompiled with JetBrains decompiler
// Type: SerializeValueBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SerializeValueBehaviour : MonoBehaviour
{
  [SerializeField]
  private SerializeValueList m_List = new SerializeValueList();

  public SerializeValueList list
  {
    get
    {
      return this.m_List;
    }
  }

  private void Awake()
  {
    this.m_List.Initialize();
  }
}
