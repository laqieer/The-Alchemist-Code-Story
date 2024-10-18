// Decompiled with JetBrains decompiler
// Type: SRPG.RigSetup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RigSetup : ScriptableObject
  {
    public string RootBoneName = string.Empty;
    public RigSetup.SkeletonInfo[] Skeletons = new RigSetup.SkeletonInfo[0];
    public string LeftHand;
    public List<string> LeftHandChangeLists = new List<string>();
    public string RightHand;
    public List<string> RightHandChangeLists = new List<string>();
    public List<string> OptionAttachLists = new List<string>();
    public float EquipmentScale = 1f;
    [Description("この骨格の基準となる身長です")]
    public float Height = 1f;
    public string Head = "Bip001 Head";

    [Serializable]
    public class BoneInfo
    {
      public string name = string.Empty;
      public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
      public Vector3 scale = new Vector3(1f, 1f, 1f);
    }

    [Serializable]
    public class SkeletonInfo
    {
      public string name = string.Empty;
      public RigSetup.BoneInfo[] bones = new RigSetup.BoneInfo[0];
    }
  }
}
