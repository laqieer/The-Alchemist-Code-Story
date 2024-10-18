// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CharacterSettings : MonoBehaviour
  {
    public Color32 GlowColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    public RigSetup Rig;
    public string DefaultSkeleton;
    public Projector ShadowProjector;
    private CharacterSettings.BoneStateCache[] mBoneStates;
    private RigSetup.SkeletonInfo mActiveSkeleton;

    public float Height
    {
      get
      {
        if ((UnityEngine.Object) this.Rig != (UnityEngine.Object) null)
          return this.Rig.Height;
        return 1f;
      }
    }

    private void Awake()
    {
      this.enabled = false;
      if (string.IsNullOrEmpty(this.DefaultSkeleton))
        return;
      this.SetSkeleton(this.DefaultSkeleton);
    }

    public void SetSkeleton(string rigName)
    {
      this.enabled = false;
      if ((UnityEngine.Object) this.Rig == (UnityEngine.Object) null)
        return;
      this.mActiveSkeleton = (RigSetup.SkeletonInfo) null;
      for (int index = 0; index < this.Rig.Skeletons.Length; ++index)
      {
        if (this.Rig.Skeletons[index].name == rigName)
        {
          this.mActiveSkeleton = this.Rig.Skeletons[index];
          break;
        }
      }
      if (this.mActiveSkeleton == null)
        return;
      this.mBoneStates = new CharacterSettings.BoneStateCache[this.mActiveSkeleton.bones.Length];
      for (int index = 0; index < this.mActiveSkeleton.bones.Length; ++index)
      {
        this.mBoneStates[index].boneInfo = this.mActiveSkeleton.bones[index];
        this.mBoneStates[index].transform = GameUtility.findChildRecursively(this.transform, this.mActiveSkeleton.bones[index].name);
        if ((UnityEngine.Object) this.mBoneStates[index].transform != (UnityEngine.Object) null)
        {
          this.mBoneStates[index].transform.localScale = this.mBoneStates[index].boneInfo.scale;
          this.mBoneStates[index].transform.localPosition += this.mBoneStates[index].boneInfo.offset;
        }
      }
      this.CacheBoneStates();
      this.enabled = true;
    }

    private void CacheBoneStates()
    {
      if (this.mBoneStates == null)
        return;
      for (int index = 0; index < this.mBoneStates.Length; ++index)
      {
        if ((UnityEngine.Object) this.mBoneStates[index].transform != (UnityEngine.Object) null)
        {
          this.mBoneStates[index].localScale = this.mBoneStates[index].transform.localScale;
          this.mBoneStates[index].localPosition = this.mBoneStates[index].transform.localPosition;
        }
      }
    }

    private void AdjustBones()
    {
      if (this.mBoneStates == null)
        return;
      for (int index = 0; index < this.mBoneStates.Length; ++index)
      {
        if ((UnityEngine.Object) this.mBoneStates[index].transform != (UnityEngine.Object) null)
        {
          if (this.mBoneStates[index].transform.localScale != this.mBoneStates[index].localScale)
            this.mBoneStates[index].transform.localScale = this.mBoneStates[index].boneInfo.scale;
          if (this.mBoneStates[index].transform.localPosition != this.mBoneStates[index].localPosition)
            this.mBoneStates[index].transform.localPosition += this.mBoneStates[index].boneInfo.offset;
        }
      }
    }

    private void Update()
    {
      this.CacheBoneStates();
    }

    private void LateUpdate()
    {
      this.AdjustBones();
      this.CacheBoneStates();
    }

    private struct BoneStateCache
    {
      public RigSetup.BoneInfo boneInfo;
      public Transform transform;
      public Vector3 localPosition;
      public Vector3 localScale;
    }
  }
}
