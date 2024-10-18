// Decompiled with JetBrains decompiler
// Type: SRPG.RigSetup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class RigSetup : ScriptableObject
  {
    public string RootBoneName = string.Empty;
    public RigSetup.SkeletonInfo[] Skeletons = new RigSetup.SkeletonInfo[0];
    public List<string> LeftHandChangeLists = new List<string>();
    public List<string> RightHandChangeLists = new List<string>();
    public List<string> OptionAttachLists = new List<string>();
    public float EquipmentScale = 1f;
    [Description("この骨格の基準となる身長です")]
    public float Height = 1f;
    public string Head = "Bip001 Head";
    public string LeftHand;
    public string RightHand;

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
