// Decompiled with JetBrains decompiler
// Type: SerializeValueBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SerializeValueBehaviour : MonoBehaviour
{
  [SerializeField]
  private SerializeValueList m_List = new SerializeValueList();

  public SerializeValueList list => this.m_List;

  private void Awake() => this.m_List.Initialize();
}
