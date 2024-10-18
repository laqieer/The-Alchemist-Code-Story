// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.CharacterGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class CharacterGenerator : AnimEventWithTarget
  {
    [HideInInspector]
    public EUnitSide UnitSide = EUnitSide.Neutral;
    private const string WpnPath = "Equipments/";
    private const string BodyTexturePath = "CH/BODYTEX/";
    [SerializeField]
    private bool Attach;
    [SerializeField]
    [StringIsCharacterDBList]
    private string UnitID;
    [SerializeField]
    [ReadOnly]
    private GameObject Body;
    [SerializeField]
    [ReadOnly]
    private Texture2D BodyTexture;
    [SerializeField]
    [ReadOnly]
    private GameObject BodyAttachment;
    [SerializeField]
    [ReadOnly]
    private GameObject HeadAttachment;
    [SerializeField]
    [ReadOnly]
    private GameObject Head;
    [SerializeField]
    [ReadOnly]
    private GameObject Hair;
    [SerializeField]
    [ReadOnly]
    private EquipmentSet Equip;
    [SerializeField]
    [StringIsResourcePath(typeof (UnityEngine.Object), "CH/BODY/", "デフォルト")]
    private string BodyName;
    [SerializeField]
    [StringIsResourcePath(typeof (Texture2D), "CH/BODYTEX/", "デフォルト")]
    private string BodyTextureName;
    [SerializeField]
    [StringIsResourcePath(typeof (Texture2D), "CH/BODYOPT/", "デフォルト")]
    private string BodyAttachmentName;
    [SerializeField]
    [StringIsResourcePath(typeof (UnityEngine.Object), "CH/HEAD/", "デフォルト")]
    private string HeadName;
    [SerializeField]
    [StringIsResourcePath(typeof (UnityEngine.Object), "CH/HAIR/", "デフォルト")]
    private string HairName;
    [SerializeField]
    [StringIsResourcePath(typeof (UnityEngine.Object), "CH/HEADOPT/", "デフォルト")]
    private string HeadAttachmentName;
    [SerializeField]
    [StringIsResourcePath(typeof (UnityEngine.Object), "Equipments/", "武器無し")]
    private string WeaponName;
    [SerializeField]
    private bool DisableHeadAttachment;
    [SerializeField]
    private bool DisableBodyAttachment;
    [SerializeField]
    private AnimDef AnimDef;
    [SerializeField]
    private bool LimLight;
    private CharacterDB.Character mCharacter;
    private CharacterDB.Job mJob;
    private GameObject mCharacterObject;
    private GameObject mCharacterWpnR;
    private GameObject mCharacterWpnL;
    private FaceAnimation mFaceAnimation;
    private GeneratedCharacter mGeneratedCharacter;
    private bool mPlayingFaceAnimation;

    private GeneratedCharacter GeneratedCharacter
    {
      get
      {
        if ((UnityEngine.Object) this.mGeneratedCharacter == (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) this.mCharacterObject == (UnityEngine.Object) null)
            return (GeneratedCharacter) null;
          this.mGeneratedCharacter = this.mCharacterObject.GetComponent<GeneratedCharacter>();
          if ((UnityEngine.Object) this.mGeneratedCharacter == (UnityEngine.Object) null)
            this.mGeneratedCharacter = this.mCharacterObject.AddComponent<GeneratedCharacter>();
          Color32 color = GameSettings.Instance.Character_PlayerGlowColor;
          if (this.UnitSide == EUnitSide.Enemy)
            color = GameSettings.Instance.Character_EnemyGlowColor;
          this.mGeneratedCharacter.SetVesselColor(color);
          this.mGeneratedCharacter.mDestroyCharacter += new GeneratedCharacter.OnDestroyCharacter(this.GeneratedCharacterDestroyed);
        }
        return this.mGeneratedCharacter;
      }
    }

    private void GeneratedCharacterDestroyed()
    {
      this.mGeneratedCharacter = (GeneratedCharacter) null;
    }

    private void Generate(GameObject parent_object)
    {
      this.mGeneratedCharacter = (GeneratedCharacter) null;
      if ((UnityEngine.Object) this.mCharacterObject != (UnityEngine.Object) null)
        return;
      if (!string.IsNullOrEmpty(this.UnitID))
      {
        string[] ids = this.UnitID.Split(',');
        if (ids == null || ids.Length < 2)
          return;
        this.mCharacter = CharacterDB.ReserveCharacter(ids[0]);
        this.mJob = this.mCharacter.Jobs.Find((Predicate<CharacterDB.Job>) (p => p.JobID == ids[1]));
      }
      CharacterComposer characterComposer = new CharacterComposer();
      characterComposer.Body = this.Body;
      characterComposer.BodyTexture = this.BodyTexture;
      if (!this.DisableBodyAttachment)
        characterComposer.BodyAttachment = this.BodyAttachment;
      characterComposer.Head = this.Head;
      characterComposer.Hair = this.Hair;
      if (!this.DisableHeadAttachment)
        characterComposer.HeadAttachment = this.HeadAttachment;
      if (this.mJob != null)
      {
        characterComposer.HairColor0 = this.mJob.HairColor0;
        characterComposer.HairColor1 = this.mJob.HairColor1;
      }
      else
      {
        characterComposer.HairColor0 = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
        characterComposer.HairColor1 = characterComposer.HairColor0;
      }
      this.mCharacterObject = characterComposer.Compose(Vector3.zero, Quaternion.identity);
      if ((UnityEngine.Object) this.mCharacterObject == (UnityEngine.Object) null)
        return;
      this.mCharacterObject.AddComponent<AnimationPlayer>().DefaultAnim = this.AnimDef;
      if ((UnityEngine.Object) this.Equip != (UnityEngine.Object) null && (UnityEngine.Object) this.GeneratedCharacter != (UnityEngine.Object) null)
        this.GeneratedCharacter.SetEquip(this.Equip);
      this.GeneratedCharacter.SetLim(this.LimLight);
      this.SetupLocation(parent_object);
      this.mCharacterObject.SetActive(true);
      if ((double) this.End <= (double) this.Start + 0.100000001490116)
        return;
      DestructTimer destructTimer = GameUtility.RequireComponent<DestructTimer>(this.mCharacterObject);
      if (!(bool) ((UnityEngine.Object) destructTimer))
        return;
      destructTimer.Timer = this.End - this.Start;
    }

    public override void OnStart(GameObject go)
    {
      this.Generate(go);
    }

    public override void OnEnd(GameObject go)
    {
      this.DestroyCharactr();
    }

    public void ResetAnimDef()
    {
      this.mCharacterObject.GetComponent<AnimationPlayer>().DefaultAnim = this.SetAnimDef;
    }

    private void SetupLocation(GameObject go)
    {
      if ((UnityEngine.Object) this.mCharacterObject != (UnityEngine.Object) null)
        this.mFaceAnimation = this.mCharacterObject.GetComponentInChildren<FaceAnimation>();
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return;
      Vector3 spawnPos;
      Quaternion spawnRot;
      this.CalcPosition(go, this.mCharacterObject, out spawnPos, out spawnRot);
      this.mCharacterObject.transform.localPosition = spawnPos;
      this.mCharacterObject.transform.localRotation = spawnRot;
      if ((double) go.transform.lossyScale.x * (double) go.transform.lossyScale.z < 0.0)
      {
        Vector3 localScale = this.mCharacterObject.transform.localScale;
        localScale.z *= -1f;
        this.mCharacterObject.transform.localScale = localScale;
      }
      if (!this.Attach || string.IsNullOrEmpty(this.BoneName))
        return;
      Transform parent = GameUtility.findChildRecursively(go.transform, this.BoneName);
      if (this.BoneName == "CAMERA" && (bool) ((UnityEngine.Object) UnityEngine.Camera.main))
        parent = UnityEngine.Camera.main.transform;
      if (!((UnityEngine.Object) parent != (UnityEngine.Object) null))
        return;
      this.mCharacterObject.transform.SetParent(parent);
    }

    public GameObject CharacterObject
    {
      get
      {
        return this.mCharacterObject;
      }
    }

    public AnimDef SetAnimDef
    {
      get
      {
        return this.AnimDef;
      }
    }

    public void SetAnimation(float adjust_time)
    {
      if ((UnityEngine.Object) this.AnimDef == (UnityEngine.Object) null || (UnityEngine.Object) this.mCharacterObject == (UnityEngine.Object) null)
        return;
      this.AnimDef.animation.SampleAnimation(this.mCharacterObject, adjust_time - this.Start);
    }

    public override void OnTick(GameObject go, float ratio)
    {
      base.OnTick(go, ratio);
      if ((UnityEngine.Object) this.mCharacterObject == (UnityEngine.Object) null)
        return;
      this.UpdateFaceAnimation(ratio);
    }

    private void UpdateFaceAnimation(float ratio)
    {
      if ((UnityEngine.Object) this.AnimDef == (UnityEngine.Object) null || (UnityEngine.Object) this.mCharacterObject == (UnityEngine.Object) null || (UnityEngine.Object) this.mFaceAnimation == (UnityEngine.Object) null)
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
      AnimDef animDef = this.AnimDef;
      if (!((UnityEngine.Object) animDef != (UnityEngine.Object) null))
        return;
      float time = (this.End - this.Start) * ratio;
      AnimationCurve customCurve1 = animDef.FindCustomCurve("FAC0");
      if (customCurve1 != null)
      {
        this.mFaceAnimation.Face0 = Mathf.FloorToInt(customCurve1.Evaluate(time)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      AnimationCurve customCurve2 = animDef.FindCustomCurve("FAC1");
      if (customCurve2 != null)
      {
        this.mFaceAnimation.Face1 = Mathf.FloorToInt(customCurve2.Evaluate(time)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      if (!this.mPlayingFaceAnimation)
        return;
      this.mFaceAnimation.PlayAnimation = false;
    }

    public void DestroyCharactr()
    {
      this.mGeneratedCharacter = (GeneratedCharacter) null;
      if ((UnityEngine.Object) this.mCharacterObject != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.mCharacterObject.GetComponent<AnimationPlayer>() != (UnityEngine.Object) null)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mCharacterObject.GetComponent<AnimationPlayer>());
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mCharacterObject);
        this.mCharacterObject = (GameObject) null;
      }
      if ((UnityEngine.Object) this.mCharacterWpnR != (UnityEngine.Object) null)
      {
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mCharacterWpnR);
        this.mCharacterWpnR = (GameObject) null;
      }
      if (!((UnityEngine.Object) this.mCharacterWpnL != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mCharacterWpnL);
      this.mCharacterWpnL = (GameObject) null;
    }
  }
}
