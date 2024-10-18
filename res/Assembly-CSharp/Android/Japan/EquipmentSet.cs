// Decompiled with JetBrains decompiler
// Type: EquipmentSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
