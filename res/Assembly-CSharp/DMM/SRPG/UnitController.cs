// Decompiled with JetBrains decompiler
// Type: SRPG.UnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public abstract class UnitController : AnimationPlayer
  {
    public const string CharacterAnimationDir = "CHM/";
    private UnitData mUnitData;
    private string mCharacterID;
    private string mJobID;
    private string mJobResourceID;
    protected CharacterDB.Job mCharacterData;
    private bool mSetupStarted;
    private int mNumLoadRequests;
    private GameObject mUnitObject;
    protected CharacterSettings mCharacterSettings;
    public Color32 VesselColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public float VesselStrength = 0.3f;
    private GameObject mPrimaryEquipment;
    private GameObject mSecondaryEquipment;
    private List<GameObject> mPrimaryEquipmentChangeLists = new List<GameObject>();
    private List<GameObject> mSecondaryEquipmentChangeLists = new List<GameObject>();
    private GameObject mPrimaryEquipmentDefault;
    private GameObject mSecondaryEquipmentDefault;
    private bool mUseSubEquipment;
    private GameObject mSubPrimaryEquipment;
    private GameObject mSubSecondaryEquipment;
    private List<GameObject> mSubPrimaryEquipmentChangeLists = new List<GameObject>();
    private List<GameObject> mSubSecondaryEquipmentChangeLists = new List<GameObject>();
    private GameObject mSubPrimaryEquipmentDefault;
    private GameObject mSubSecondaryEquipmentDefault;
    private FaceAnimation mFaceAnimation;
    private bool mPlayingFaceAnimation;
    public bool LoadEquipments;
    public bool KeepUnitHidden;
    protected List<Material> CharacterMaterials = new List<Material>(4);
    protected List<CharacterDB.Job> mCharacterDataLists = new List<CharacterDB.Job>();
    protected List<GameObject> mUnitObjectLists = new List<GameObject>();
    protected List<CharacterSettings> mCharacterSettingsLists = new List<CharacterSettings>();
    protected List<FaceAnimation> mFaceAnimationLists = new List<FaceAnimation>();
    private Color UnitColor = Color.white;
    private List<GameObject> mGoRidingModelList;
    public const string COLLABO_SKILL_ASSET_PREFIX = "D";
    private float mVesselStrength;
    private float mVesselAnimTime;
    private float mVesselAnimDuration;
    private float mVesselAnimStart;
    private float mVesselAnimEnd;

    public CharacterDB.Job CharacterData => this.mCharacterData;

    protected RigSetup Rig
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null) ? this.mCharacterSettings.Rig : (RigSetup) null;
      }
    }

    public float Height
    {
      get
      {
        RigSetup rig = this.Rig;
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) rig, (UnityEngine.Object) null) ? rig.Height * ((Component) this).transform.localScale.y : 0.0f;
      }
    }

    public Vector3 CenterPosition
    {
      get
      {
        return Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Multiply(Vector3.up, this.Height * 0.5f));
      }
    }

    public GameObject UnitObject => this.mUnitObject;

    public float UIDispOffset
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null) ? this.mCharacterSettings.UIDispOffset : 0.0f;
      }
    }

    public Vector3 UIDispPosition
    {
      get
      {
        return Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Multiply(Vector3.up, (float) ((double) this.UIDispOffset / 2.0 + 0.5)));
      }
    }

    public List<GameObject> GoRidingModelList
    {
      get
      {
        if (this.mGoRidingModelList == null)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null) || this.mCharacterSettings.RidingModelNames == null)
            return (List<GameObject>) null;
          for (int index = 0; index < this.mCharacterSettings.RidingModelNames.Length; ++index)
          {
            string ridingModelName = this.mCharacterSettings.RidingModelNames[index];
            if (!string.IsNullOrEmpty(ridingModelName))
            {
              Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, ridingModelName);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) childRecursively))
              {
                if (this.mGoRidingModelList == null)
                  this.mGoRidingModelList = new List<GameObject>();
                this.mGoRidingModelList.Add(((Component) childRecursively).gameObject);
              }
            }
          }
        }
        return this.mGoRidingModelList;
      }
    }

    public UnitData UnitData => this.mUnitData;

    protected bool UseSubEquipment => this.mUseSubEquipment;

    public override bool IsLoading
    {
      get => !this.mSetupStarted || this.mNumLoadRequests > 0 || base.IsLoading;
    }

    protected void AddLoadThreadCount() => ++this.mNumLoadRequests;

    protected void RemoveLoadThreadCount()
    {
      --this.mNumLoadRequests;
      if (this.mNumLoadRequests >= 0)
        return;
      Debug.LogError((object) "Invalid load request count");
      this.mNumLoadRequests = 0;
    }

    public bool SetActivateUnitObject(int idx)
    {
      if (idx < 0 || idx >= this.mUnitObjectLists.Count)
        return false;
      int index = 0;
      while (index < this.mUnitObjectLists.Count && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitObject, (UnityEngine.Object) this.mUnitObjectLists[index]))
        ++index;
      foreach (GameObject mUnitObjectList in this.mUnitObjectLists)
        mUnitObjectList.SetActive(false);
      this.mUnitObject = this.mUnitObjectLists[idx];
      this.mUnitObject.SetActive(true);
      this.SetAnimationComponent(this.mUnitObject.GetComponent<Animation>());
      if (idx < this.mCharacterDataLists.Count)
        this.mCharacterData = this.mCharacterDataLists[idx];
      if (idx < this.mCharacterSettingsLists.Count)
        this.mCharacterSettings = this.mCharacterSettingsLists[idx];
      if (idx < this.mFaceAnimationLists.Count)
        this.mFaceAnimation = this.mFaceAnimationLists[idx];
      return idx != index;
    }

    protected override void Start()
    {
      base.Start();
      this.mCharacterDataLists.Clear();
      this.mUnitObjectLists.Clear();
      this.mCharacterSettingsLists.Clear();
      this.mFaceAnimationLists.Clear();
      CharacterDB.Character character = CharacterDB.FindCharacter(this.mCharacterID);
      if (character == null)
      {
        Debug.LogError((object) ("Unknown character '" + this.mCharacterID + "'"));
        this.SetLoadError();
      }
      else
      {
        this.mJobResourceID = !string.IsNullOrEmpty(this.mJobID) ? MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID).model : "none";
        string str = (string) null;
        if (this.mUnitData != null && this.mUnitData.Jobs != null)
        {
          ArtifactParam selectedSkin = this.mUnitData.GetSelectedSkin(Array.FindIndex<JobData>(this.mUnitData.Jobs, (Predicate<JobData>) (j => j.Param.iname == this.mJobID)));
          str = selectedSkin == null ? (string) null : selectedSkin.asset;
        }
        if (string.IsNullOrEmpty(str))
          str = this.mJobResourceID;
        int jobIndex = -1;
        for (int index = 0; index < character.Jobs.Count; ++index)
        {
          if (character.Jobs[index].JobID == str)
          {
            jobIndex = index;
            break;
          }
        }
        if (jobIndex < 0)
          jobIndex = 0;
        this.StartCoroutine(this.AsyncSetup(character, jobIndex));
      }
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      this.UpdateFaceAnimation();
      if ((double) this.mVesselAnimTime >= (double) this.mVesselAnimDuration)
        return;
      this.UpdateVesselAnimation();
    }

    private void createRootBoneList(Transform Root, ref List<Transform> Dest)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) Root, (UnityEngine.Object) null) || Dest == null || 0 >= Root.childCount)
        return;
      for (int index = 0; index < Root.childCount; ++index)
      {
        Transform child = Root.GetChild(index);
        Dest.Add(child);
        this.createRootBoneList(child, ref Dest);
      }
    }

    private void ReleaseMaterials()
    {
      if (this.CharacterMaterials == null)
        return;
      foreach (Material characterMaterial in this.CharacterMaterials)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) characterMaterial, (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) characterMaterial);
      }
      this.CharacterMaterials.Clear();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      foreach (GameObject equipmentChangeList in this.mPrimaryEquipmentChangeLists)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) equipmentChangeList))
          UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
      }
      this.mPrimaryEquipmentChangeLists = new List<GameObject>();
      foreach (GameObject equipmentChangeList in this.mSecondaryEquipmentChangeLists)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) equipmentChangeList))
          UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
      }
      this.mSecondaryEquipmentChangeLists = new List<GameObject>();
      this.ReleaseMaterials();
    }

    private void UpdateFaceAnimation()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mFaceAnimation, (UnityEngine.Object) null))
        return;
      if (this.mPlayingFaceAnimation)
      {
        this.mFaceAnimation.PlayAnimation = true;
        this.mPlayingFaceAnimation = false;
        if (this.mFaceAnimation.Animation0.Curve == null)
          this.mFaceAnimation.Face0 = 0;
        if (this.mFaceAnimation.Animation1.Curve == null)
          this.mFaceAnimation.Face1 = 0;
      }
      float position;
      AnimDef activeAnimation = this.GetActiveAnimation(out position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) activeAnimation, (UnityEngine.Object) null))
        return;
      AnimationCurve customCurve1 = activeAnimation.FindCustomCurve("FAC0");
      if (customCurve1 != null)
      {
        this.mFaceAnimation.Face0 = Mathf.FloorToInt(customCurve1.Evaluate(position)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      AnimationCurve customCurve2 = activeAnimation.FindCustomCurve("FAC1");
      if (customCurve2 != null)
      {
        this.mFaceAnimation.Face1 = Mathf.FloorToInt(customCurve2.Evaluate(position)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      if (!this.mPlayingFaceAnimation)
        return;
      this.mFaceAnimation.PlayAnimation = false;
    }

    public virtual void SetupUnit(UnitData unitData, int jobIndex = -1)
    {
      this.mUnitData = unitData;
      this.mCharacterID = unitData.UnitParam.model;
      if (jobIndex == -1)
        this.mJobID = unitData.CurrentJob == null ? (string) null : unitData.CurrentJob.JobID;
      else
        this.mJobID = unitData.Jobs[jobIndex].JobID;
    }

    public virtual void SetupUnit(string unitID, string jobID)
    {
      this.mCharacterID = MonoSingleton<GameManager>.Instance.GetUnitParam(unitID).model;
      this.mJobID = jobID;
    }

    protected Transform GetCharacterRoot()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCharacterSettings.Rig, (UnityEngine.Object) null))
        return ((Component) this).transform;
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, this.mCharacterSettings.Rig.RootBoneName);
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null) ? childRecursively : ((Component) this).transform;
    }

    protected virtual ESex Sex => this.mUnitData.UnitParam.sex;

    public void SetVisible(bool visible)
    {
      GameUtility.SetLayer((Component) this, !visible ? GameUtility.LayerHidden : GameUtility.LayerCH, true);
      this.OnVisibilityChange(visible);
    }

    public bool IsVisible() => ((Component) this).gameObject.layer != GameUtility.LayerHidden;

    protected virtual void OnVisibilityChange(bool visible)
    {
    }

    public void LoadUnitAnimationAsync(
      string id,
      string animationName,
      bool addJobName,
      bool is_collabo_skill = false)
    {
      string str = this.mCharacterData.AssetPrefix;
      if (is_collabo_skill)
        str = "D";
      if (addJobName)
        this.LoadAnimationAsync(id, "CHM/" + str + "_" + this.mJobResourceID + "_" + animationName);
      else
        this.LoadAnimationAsync(id, "CHM/" + str + "_" + animationName);
    }

    private LoadRequest LoadResource<T>(string path)
    {
      return string.IsNullOrEmpty(path) ? (LoadRequest) null : AssetManager.LoadAsync(path, typeof (T));
    }

    private void SetMaterialColor(List<Material> materials, Color color)
    {
      for (int index = 0; index < materials.Count; ++index)
      {
        materials[index].EnableKeyword("USE_FADE_COLOR");
        materials[index].SetColor("_fadeColor", color);
      }
    }

    public Color GetColor() => this.UnitColor;

    public void SetColor(Color color)
    {
      if ((double) color.r >= 1.0 && (double) color.g >= 1.0 && (double) color.b >= 1.0)
        color = Color.white;
      this.UnitColor = color;
      if (this.CharacterMaterials == null)
        return;
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.CharacterMaterials[index], (UnityEngine.Object) null))
          this.SetMaterialColor(this.CharacterMaterials, color);
      }
    }

    public bool LoadAddModels(int job_index)
    {
      if (this.mNumLoadRequests > 0)
        return false;
      CharacterDB.Character character = CharacterDB.FindCharacter(this.mCharacterID);
      if (character == null)
      {
        Debug.LogError((object) ("Unknown character '" + this.mCharacterID + "'"));
        return false;
      }
      if (job_index < 0 || job_index >= character.Jobs.Count)
        return false;
      this.StartCoroutine(this.AsyncSetup(character, job_index));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator AsyncSetup(CharacterDB.Character ch, int jobIndex)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitController.\u003CAsyncSetup\u003Ec__Iterator0()
      {
        ch = ch,
        jobIndex = jobIndex,
        \u0024this = this
      };
    }

    protected void ActivateRidingModelByJob()
    {
      List<GameObject> goRidingModelList = this.GoRidingModelList;
      if (goRidingModelList == null)
        return;
      bool flag = this.mUnitData != null && this.mUnitData.IsRiding;
      foreach (GameObject gameObject in goRidingModelList)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
          gameObject.SetActive(flag);
      }
    }

    private void FindCharacterMaterials()
    {
      Renderer[] componentsInChildren = ((Component) this).GetComponentsInChildren<Renderer>(true);
      for (int index1 = componentsInChildren.Length - 1; index1 >= 0; --index1)
      {
        Material[] materials = componentsInChildren[index1].materials;
        if (materials != null)
        {
          for (int index2 = 0; index2 < materials.Length; ++index2)
          {
            Material material = materials[index2];
            if (!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false)))
              this.CharacterMaterials.Add(material);
          }
        }
      }
    }

    protected virtual void PostSetup()
    {
    }

    public void SetEquipmentsVisible(bool visible)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        this.mPrimaryEquipment.gameObject.SetActive(visible);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      this.mSecondaryEquipment.gameObject.SetActive(visible);
    }

    public void SetPrimaryEquipmentsVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        return;
      this.mPrimaryEquipment.gameObject.SetActive(visible);
    }

    public void SetSecondaryEquipmentsVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      this.mSecondaryEquipment.gameObject.SetActive(visible);
    }

    public void SwitchEquipmentLists(UnitController.EquipmentType type, int no)
    {
      if (no <= 0)
        return;
      int index = no - 1;
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (index >= this.mPrimaryEquipmentChangeLists.Count)
            break;
          bool flag1 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.activeSelf;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            this.mPrimaryEquipment.SetActive(false);
          this.mPrimaryEquipment = this.mPrimaryEquipmentChangeLists[index];
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            break;
          this.mPrimaryEquipment.SetActive(flag1);
          break;
        case UnitController.EquipmentType.SECONDARY:
          if (index >= this.mSecondaryEquipmentChangeLists.Count)
            break;
          bool flag2 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            this.mSecondaryEquipment.SetActive(false);
          this.mSecondaryEquipment = this.mSecondaryEquipmentChangeLists[index];
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            break;
          this.mSecondaryEquipment.SetActive(flag2);
          break;
      }
    }

    public void ResetEquipmentLists(UnitController.EquipmentType type)
    {
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) this.mPrimaryEquipmentDefault))
            break;
          bool flag1 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.activeSelf;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            this.mPrimaryEquipment.SetActive(false);
          this.mPrimaryEquipment = this.mPrimaryEquipmentDefault;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            break;
          this.mPrimaryEquipment.SetActive(flag1);
          break;
        case UnitController.EquipmentType.SECONDARY:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) this.mSecondaryEquipmentDefault))
            break;
          bool flag2 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            this.mSecondaryEquipment.SetActive(false);
          this.mSecondaryEquipment = this.mSecondaryEquipmentDefault;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            break;
          this.mSecondaryEquipment.SetActive(flag2);
          break;
      }
    }

    private void SetAttachmentParent(GameObject go, Transform parent)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) parent))
        return;
      go.transform.SetParent(parent, false);
      go.transform.localScale = Vector3.op_Multiply(Vector3.one, this.Rig.EquipmentScale);
    }

    public void SwitchAttachmentLists(UnitController.EquipmentType type, int no)
    {
      if (no <= 0)
        return;
      int index = no - 1;
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (index >= this.Rig.RightHandChangeLists.Count || string.IsNullOrEmpty(this.Rig.RightHandChangeLists[index]))
            break;
          Transform childRecursively1 = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.RightHandChangeLists[index]);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively1, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mPrimaryEquipmentDefault, childRecursively1);
          using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively1);
            break;
          }
        case UnitController.EquipmentType.SECONDARY:
          if (index >= this.Rig.LeftHandChangeLists.Count || string.IsNullOrEmpty(this.Rig.LeftHandChangeLists[index]))
            break;
          Transform childRecursively2 = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.LeftHandChangeLists[index]);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively2, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mSecondaryEquipmentDefault, childRecursively2);
          using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively2);
            break;
          }
      }
    }

    public void ResetAttachmentLists(UnitController.EquipmentType type)
    {
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (string.IsNullOrEmpty(this.Rig.RightHand))
            break;
          Transform childRecursively1 = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.RightHand);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively1, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mPrimaryEquipmentDefault, childRecursively1);
          using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively1);
            break;
          }
        case UnitController.EquipmentType.SECONDARY:
          if (string.IsNullOrEmpty(this.Rig.LeftHand))
            break;
          Transform childRecursively2 = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.LeftHand);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively2, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mSecondaryEquipmentDefault, childRecursively2);
          using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively2);
            break;
          }
      }
    }

    public void SetSubEquipment(
      GameObject primaryHand,
      GameObject secondaryHand,
      List<GameObject> primary_lists,
      List<GameObject> secondary_lists,
      bool hidden = false)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) primaryHand, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.Rig.RightHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.RightHand);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        {
          this.mSubPrimaryEquipment = UnityEngine.Object.Instantiate<GameObject>(primaryHand, primaryHand.transform.position, primaryHand.transform.rotation);
          this.mSubPrimaryEquipment.transform.SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubPrimaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubPrimaryEquipment, GameUtility.LayerHidden, true);
          this.mSubPrimaryEquipment.transform.localScale = Vector3.op_Multiply(Vector3.one, this.Rig.EquipmentScale);
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.RightHand + " not found."));
      }
      if (primary_lists != null && !string.IsNullOrEmpty(this.Rig.RightHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.RightHand);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) childRecursively))
        {
          this.mSubPrimaryEquipmentDefault = this.mSubPrimaryEquipment;
          this.mSubPrimaryEquipmentChangeLists.Clear();
          foreach (GameObject primaryList in primary_lists)
          {
            GameObject gameObject = (GameObject) null;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) primaryList))
            {
              gameObject = UnityEngine.Object.Instantiate<GameObject>(primaryList, primaryList.transform.position, primaryList.transform.rotation);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
              {
                gameObject.transform.SetParent(childRecursively, false);
                gameObject.transform.localScale = Vector3.op_Multiply(Vector3.one, this.Rig.EquipmentScale);
                if (hidden)
                  GameUtility.SetLayer(gameObject, GameUtility.LayerHidden, true);
                gameObject.gameObject.SetActive(false);
                this.SetMaterialByGameObject(gameObject);
              }
            }
            this.mSubPrimaryEquipmentChangeLists.Add(gameObject);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) secondaryHand, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.Rig.LeftHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.LeftHand);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        {
          this.mSubSecondaryEquipment = UnityEngine.Object.Instantiate<GameObject>(secondaryHand, secondaryHand.transform.position, secondaryHand.transform.rotation);
          this.mSubSecondaryEquipment.transform.SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubSecondaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubSecondaryEquipment, GameUtility.LayerHidden, true);
          this.mSubSecondaryEquipment.transform.localScale = Vector3.op_Multiply(Vector3.one, this.Rig.EquipmentScale);
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.LeftHand + " not found."));
      }
      if (primary_lists != null && !string.IsNullOrEmpty(this.Rig.LeftHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.transform, this.Rig.LeftHand);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) childRecursively))
        {
          this.mSubSecondaryEquipmentDefault = this.mSubSecondaryEquipment;
          this.mSubSecondaryEquipmentChangeLists.Clear();
          foreach (GameObject secondaryList in secondary_lists)
          {
            GameObject gameObject = (GameObject) null;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) secondaryList))
            {
              gameObject = UnityEngine.Object.Instantiate<GameObject>(secondaryList, secondaryList.transform.position, secondaryList.transform.rotation);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
              {
                gameObject.transform.SetParent(childRecursively, false);
                gameObject.transform.localScale = Vector3.op_Multiply(Vector3.one, this.Rig.EquipmentScale);
                if (hidden)
                  GameUtility.SetLayer(gameObject, GameUtility.LayerHidden, true);
                gameObject.gameObject.SetActive(false);
                this.SetMaterialByGameObject(gameObject);
              }
            }
            this.mSubSecondaryEquipmentChangeLists.Add(gameObject);
          }
        }
      }
      this.mUseSubEquipment = true;
      this.SwitchEquipments();
    }

    private void SetMaterialByGameObject(GameObject materialObject)
    {
      this.ControlMaterialByGameObject(true, materialObject);
    }

    private void RemoveMaterialByGameObject(GameObject materialObject)
    {
      this.ControlMaterialByGameObject(false, materialObject);
    }

    private void ControlMaterialByGameObject(bool isSet, GameObject materialObject)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) materialObject, (UnityEngine.Object) null))
        return;
      Renderer[] componentsInChildren = materialObject.GetComponentsInChildren<Renderer>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
      {
        Material material = componentsInChildren[index].material;
        if (!string.IsNullOrEmpty(material.GetTag("Character", false)))
        {
          if (isSet)
            this.CharacterMaterials.Add(material);
          else
            this.CharacterMaterials.Remove(material);
        }
      }
    }

    public void SwitchEquipments()
    {
      GameObject primaryEquipment = this.mPrimaryEquipment;
      this.mPrimaryEquipment = this.mSubPrimaryEquipment;
      this.mSubPrimaryEquipment = primaryEquipment;
      GameObject secondaryEquipment = this.mSecondaryEquipment;
      this.mSecondaryEquipment = this.mSubSecondaryEquipment;
      this.mSubSecondaryEquipment = secondaryEquipment;
      GameObject equipmentDefault1 = this.mPrimaryEquipmentDefault;
      this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
      this.mSubPrimaryEquipmentDefault = equipmentDefault1;
      List<GameObject> equipmentChangeLists1 = this.mPrimaryEquipmentChangeLists;
      this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
      this.mSubPrimaryEquipmentChangeLists = equipmentChangeLists1;
      GameObject equipmentDefault2 = this.mSecondaryEquipmentDefault;
      this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
      this.mSubSecondaryEquipmentDefault = equipmentDefault2;
      List<GameObject> equipmentChangeLists2 = this.mSecondaryEquipmentChangeLists;
      this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
      this.mSubSecondaryEquipmentChangeLists = equipmentChangeLists2;
    }

    public void HideEquipments()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerHidden, true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerHidden, true);
    }

    public void ResetSubEquipments()
    {
      this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
      this.mSubPrimaryEquipmentDefault = (GameObject) null;
      this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
      this.mSubSecondaryEquipmentDefault = (GameObject) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
      {
        this.RemoveMaterialByGameObject(this.mPrimaryEquipment);
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mPrimaryEquipment.gameObject);
      }
      this.mPrimaryEquipment = this.mSubPrimaryEquipment;
      this.mSubPrimaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerCH, true);
      foreach (GameObject equipmentChangeList in this.mPrimaryEquipmentChangeLists)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) equipmentChangeList))
        {
          this.RemoveMaterialByGameObject(equipmentChangeList);
          UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
        }
      }
      this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
      this.mSubPrimaryEquipmentChangeLists = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
      {
        this.RemoveMaterialByGameObject(this.mSecondaryEquipment);
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSecondaryEquipment.gameObject);
      }
      this.mSecondaryEquipment = this.mSubSecondaryEquipment;
      this.mSubSecondaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerCH, true);
      foreach (GameObject equipmentChangeList in this.mSecondaryEquipmentChangeLists)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) equipmentChangeList))
        {
          this.RemoveMaterialByGameObject(equipmentChangeList);
          UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
        }
      }
      this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
      this.mSubSecondaryEquipmentChangeLists = new List<GameObject>();
      this.mUseSubEquipment = false;
    }

    private void SetVesselStrength(float strength)
    {
      this.mVesselStrength = strength;
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        Material characterMaterial = this.CharacterMaterials[index];
        if (!string.IsNullOrEmpty(characterMaterial.GetTag("Bloom", false, (string) null)))
        {
          float num = 0.003921569f;
          characterMaterial.EnableKeyword("MODE_BLOOM");
          characterMaterial.DisableKeyword("MODE_DEFAULT");
          characterMaterial.SetVector("_glowColor", new Vector4((float) this.VesselColor.r * num, (float) this.VesselColor.g * num, (float) this.VesselColor.b * num, Mathf.Pow(strength, 0.7f)));
          characterMaterial.SetFloat("_colorMultipler", (float) (1.0 - (double) strength * 0.40000000596046448));
          characterMaterial.SetFloat("_glowStrength", Mathf.Pow(strength, 1.5f) * this.VesselStrength);
        }
        else
        {
          characterMaterial.EnableKeyword("MODE_DEFAULT");
          characterMaterial.DisableKeyword("MODE_BLOOM");
          characterMaterial.SetFloat("_colorMultipler", 1f);
        }
      }
    }

    public void AnimateVessel(float desiredStrength, float duration)
    {
      if ((double) duration <= 0.0)
      {
        this.SetVesselStrength(desiredStrength);
        this.mVesselAnimTime = 0.0f;
        this.mVesselAnimDuration = 0.0f;
      }
      else
      {
        this.mVesselAnimStart = this.mVesselStrength;
        this.mVesselAnimEnd = desiredStrength;
        this.mVesselAnimTime = 0.0f;
        this.mVesselAnimDuration = duration;
      }
    }

    private void UpdateVesselAnimation()
    {
      this.mVesselAnimTime += Time.deltaTime;
      this.SetVesselStrength(Mathf.Lerp(this.mVesselAnimStart, this.mVesselAnimEnd, Mathf.Clamp01(this.mVesselAnimTime / this.mVesselAnimDuration)));
    }

    public enum EquipmentType
    {
      PRIMARY,
      SECONDARY,
    }
  }
}
