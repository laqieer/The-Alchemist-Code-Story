// Decompiled with JetBrains decompiler
// Type: EquipmentSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class EquipmentSet : ScriptableObject
{
  public List<GameObject> PrimaryHandChangeLists = new List<GameObject>();
  public List<GameObject> SecondaryHandChangeLists = new List<GameObject>();
  public List<GameObject> OptionEquipmentLists = new List<GameObject>();
  public EquipmentSet.EquipmentType Type;
  public bool PrimaryHidden;
  public bool PrimaryForceOverride;
  public GameObject PrimaryHand;
  public bool SecondaryHidden;
  public bool SecondaryForceOverride;
  public GameObject SecondaryHand;

  public enum EquipmentType
  {
    Melee,
    Bow,
    Gun,
  }
}
