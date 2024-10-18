// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CharacterSettings : MonoBehaviour
  {
    public RigSetup Rig;
    public string DefaultSkeleton;
    public Projector ShadowProjector;
    public bool IsUseGlowColor;
    public Color32 GlowColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    public float GlowStrength = 0.25f;
    [SerializeField]
    public float UIDispOffset;
    public string[] RidingModelNames;
    private CharacterSettings.BoneStateCache[] mBoneStates;
    private RigSetup.SkeletonInfo mActiveSkeleton;

    public float Height
    {
      get => Object.op_Inequality((Object) this.Rig, (Object) null) ? this.Rig.Height : 1f;
    }

    private void Awake()
    {
      ((Behaviour) this).enabled = false;
      if (string.IsNullOrEmpty(this.DefaultSkeleton))
        return;
      this.SetSkeleton(this.DefaultSkeleton);
    }

    public void SetSkeleton(string rigName)
    {
      ((Behaviour) this).enabled = false;
      if (Object.op_Equality((Object) this.Rig, (Object) null))
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
        this.mBoneStates[index].transform = GameUtility.findChildRecursively(((Component) this).transform, this.mActiveSkeleton.bones[index].name);
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
        {
          this.mBoneStates[index].transform.localScale = this.mBoneStates[index].boneInfo.scale;
          Transform transform = this.mBoneStates[index].transform;
          transform.localPosition = Vector3.op_Addition(transform.localPosition, this.mBoneStates[index].boneInfo.offset);
        }
      }
      this.CacheBoneStates();
      ((Behaviour) this).enabled = true;
    }

    private void CacheBoneStates()
    {
      if (this.mBoneStates == null)
        return;
      for (int index = 0; index < this.mBoneStates.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
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
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
        {
          if (Vector3.op_Inequality(this.mBoneStates[index].transform.localScale, this.mBoneStates[index].localScale))
            this.mBoneStates[index].transform.localScale = this.mBoneStates[index].boneInfo.scale;
          if (Vector3.op_Inequality(this.mBoneStates[index].transform.localPosition, this.mBoneStates[index].localPosition))
          {
            Transform transform = this.mBoneStates[index].transform;
            transform.localPosition = Vector3.op_Addition(transform.localPosition, this.mBoneStates[index].boneInfo.offset);
          }
        }
      }
    }

    private void Update() => this.CacheBoneStates();

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
