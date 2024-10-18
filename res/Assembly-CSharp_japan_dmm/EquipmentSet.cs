// Decompiled with JetBrains decompiler
// Type: EquipmentSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EquipmentSet : ScriptableObject
{
  public EquipmentSet.EquipmentType Type;
  public bool PrimaryHidden;
  public bool PrimaryForceOverride;
  public GameObject PrimaryHand;
  public List<GameObject> PrimaryHandChangeLists = new List<GameObject>();
  public bool SecondaryHidden;
  public bool SecondaryForceOverride;
  public GameObject SecondaryHand;
  public List<GameObject> SecondaryHandChangeLists = new List<GameObject>();
  public List<GameObject> OptionEquipmentLists = new List<GameObject>();

  public enum EquipmentType
  {
    Melee,
    Bow,
    Gun,
  }
}
