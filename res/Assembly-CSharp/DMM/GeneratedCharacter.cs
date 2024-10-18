// Decompiled with JetBrains decompiler
// Type: GeneratedCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GeneratedCharacter : MonoBehaviour
{
  private FaceAnimation mFaceAnimation;
  private bool mPlayingFaceAnimation;
  public GeneratedCharacter.OnDestroyCharacter mDestroyCharacter;
  private Color32 VesselColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
  private Color UnitColor = Color.white;
  private bool mEnableStaticLight = true;
  private List<Material> mCharacterMaterials;
  private GameObject mPrimaryEquipment;
  private GameObject mSecondaryEquipment;
  private List<GameObject> PrimaryEquipmentChangeLists = new List<GameObject>();
  private List<GameObject> SecondaryEquipmentChangeLists = new List<GameObject>();
  private GameObject mPrimaryEquipmentDefault;
  private GameObject mSecondaryEquipmentDefault;
  private GameObject mSubPrimaryEquipment;
  private GameObject mSubSecondaryEquipment;
  private GameObject mSubPrimaryEquipmentDefault;
  private GameObject mSubSecondaryEquipmentDefault;

  private void Start()
  {
  }

  private void Update()
  {
  }

  public void SetVesselColor(Color32 color) => this.VesselColor = color;

  public void EnableStaticLight(bool enable) => this.mEnableStaticLight = enable;

  private List<Material> CharacterMaterials
  {
    get
    {
      if (this.mCharacterMaterials == null)
        this.FindCharacterMaterials();
      return this.mCharacterMaterials;
    }
  }

  private void DestroyMaterials()
  {
    if (this.mCharacterMaterials == null)
      return;
    foreach (Object characterMaterial in this.mCharacterMaterials)
      Object.Destroy(characterMaterial);
    this.mCharacterMaterials = (List<Material>) null;
  }

  public void SetEquip(EquipmentSet Equip)
  {
    CharacterSettings component = ((Component) this).GetComponent<CharacterSettings>();
    EquipmentSet equipmentSet = Equip;
    List<GameObject> primaryHandChangeLists = equipmentSet.PrimaryHandChangeLists;
    GameObject primaryHand;
    if (Object.op_Inequality((Object) equipmentSet, (Object) null) && equipmentSet.PrimaryForceOverride)
    {
      primaryHand = equipmentSet.PrimaryHand;
      primaryHandChangeLists = equipmentSet.PrimaryHandChangeLists;
    }
    else
      primaryHand = equipmentSet.PrimaryHand;
    GameObject secondaryHand1 = equipmentSet.SecondaryHand;
    List<GameObject> secondaryHandChangeLists = equipmentSet.SecondaryHandChangeLists;
    GameObject secondaryHand2;
    if (Object.op_Inequality((Object) equipmentSet, (Object) null) && equipmentSet.PrimaryForceOverride)
    {
      secondaryHand2 = equipmentSet.SecondaryHand;
      secondaryHandChangeLists = equipmentSet.SecondaryHandChangeLists;
    }
    else
      secondaryHand2 = equipmentSet.SecondaryHand;
    if (Object.op_Inequality((Object) primaryHand, (Object) null) && !string.IsNullOrEmpty(component.Rig.RightHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, component.Rig.RightHand);
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
      {
        this.mPrimaryEquipment = Object.Instantiate<GameObject>(primaryHand, primaryHand.transform.position, primaryHand.transform.rotation);
        this.mPrimaryEquipment.transform.SetParent(childRecursively, false);
        this.mPrimaryEquipment.transform.localScale = Vector3.op_Multiply(Vector3.one, component.Rig.EquipmentScale);
        if (equipmentSet.PrimaryHidden)
          this.SetPrimaryEquipmentsVisible(false);
      }
      else
        Debug.LogError((object) ("Node " + component.Rig.RightHand + " not found."));
    }
    if (primaryHandChangeLists != null && !string.IsNullOrEmpty(component.Rig.RightHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, component.Rig.RightHand);
      if (Object.op_Implicit((Object) childRecursively))
      {
        this.mPrimaryEquipmentDefault = this.mPrimaryEquipment;
        this.mPrimaryEquipmentChangeLists.Clear();
        foreach (GameObject gameObject1 in primaryHandChangeLists)
        {
          GameObject gameObject2 = (GameObject) null;
          if (Object.op_Implicit((Object) gameObject1))
          {
            gameObject2 = Object.Instantiate<GameObject>(gameObject1, gameObject1.transform.position, gameObject1.transform.rotation);
            if (Object.op_Implicit((Object) gameObject2))
            {
              gameObject2.transform.SetParent(childRecursively, false);
              gameObject2.transform.localScale = Vector3.op_Multiply(Vector3.one, component.Rig.EquipmentScale);
              gameObject2.gameObject.SetActive(false);
            }
          }
          this.mPrimaryEquipmentChangeLists.Add(gameObject2);
        }
      }
    }
    if (Object.op_Inequality((Object) secondaryHand2, (Object) null) && !string.IsNullOrEmpty(component.Rig.LeftHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, component.Rig.LeftHand);
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
      {
        this.mSecondaryEquipment = Object.Instantiate<GameObject>(secondaryHand2, secondaryHand2.transform.position, secondaryHand2.transform.rotation);
        this.mSecondaryEquipment.transform.SetParent(childRecursively, false);
        this.mSecondaryEquipment.transform.localScale = Vector3.op_Multiply(Vector3.one, component.Rig.EquipmentScale);
        if (equipmentSet.SecondaryHidden)
          this.SetSecondaryEquipmentsVisible(false);
      }
      else
        Debug.LogError((object) ("Node " + component.Rig.LeftHand + " not found."));
    }
    if (secondaryHandChangeLists != null && !string.IsNullOrEmpty(component.Rig.LeftHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, component.Rig.LeftHand);
      if (Object.op_Implicit((Object) childRecursively))
      {
        this.mSecondaryEquipmentDefault = this.mSecondaryEquipment;
        this.mSecondaryEquipmentChangeLists.Clear();
        foreach (GameObject gameObject3 in secondaryHandChangeLists)
        {
          GameObject gameObject4 = (GameObject) null;
          if (Object.op_Implicit((Object) gameObject3))
          {
            gameObject4 = Object.Instantiate<GameObject>(gameObject3, gameObject3.transform.position, gameObject3.transform.rotation);
            if (Object.op_Implicit((Object) gameObject4))
            {
              gameObject4.transform.SetParent(childRecursively, false);
              gameObject4.transform.localScale = Vector3.op_Multiply(Vector3.one, component.Rig.EquipmentScale);
              gameObject4.gameObject.SetActive(false);
            }
          }
          this.mSecondaryEquipmentChangeLists.Add(gameObject4);
        }
      }
    }
    this.ResetEquipmentLists(GeneratedCharacter.EquipmentType.PRIMARY);
    this.ResetEquipmentLists(GeneratedCharacter.EquipmentType.SECONDARY);
  }

  public void ResetEquipmentLists(GeneratedCharacter.EquipmentType type)
  {
    switch (type)
    {
      case GeneratedCharacter.EquipmentType.PRIMARY:
        if (!Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) this.mPrimaryEquipmentDefault))
          break;
        if (Object.op_Implicit((Object) this.mPrimaryEquipment))
          this.mPrimaryEquipment.SetActive(false);
        this.mPrimaryEquipment = this.mPrimaryEquipmentDefault;
        if (!Object.op_Implicit((Object) this.mPrimaryEquipment))
          break;
        this.mPrimaryEquipment.SetActive(true);
        break;
      case GeneratedCharacter.EquipmentType.SECONDARY:
        if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) this.mSecondaryEquipmentDefault))
          break;
        bool flag = !Object.op_Implicit((Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
        if (Object.op_Implicit((Object) this.mSecondaryEquipment))
          this.mSecondaryEquipment.SetActive(false);
        this.mSecondaryEquipment = this.mSecondaryEquipmentDefault;
        if (!Object.op_Implicit((Object) this.mSecondaryEquipment))
          break;
        this.mSecondaryEquipment.SetActive(flag);
        break;
    }
  }

  private void FindCharacterMaterials()
  {
    Renderer[] componentsInChildren = ((Component) this).gameObject.GetComponentsInChildren<Renderer>(true);
    if (componentsInChildren == null)
      return;
    List<Renderer> rendererList = new List<Renderer>((IEnumerable<Renderer>) componentsInChildren);
    Renderer component = ((Component) this).gameObject.GetComponent<Renderer>();
    if (Object.op_Inequality((Object) component, (Object) null))
      rendererList.Add(component);
    for (int index = rendererList.Count - 1; index >= 0; --index)
    {
      Material material = rendererList[index].material;
      if (!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false)))
      {
        if (this.mCharacterMaterials == null)
          this.mCharacterMaterials = new List<Material>();
        this.mCharacterMaterials.Add(material);
      }
    }
  }

  private void OnDestroy()
  {
    if (this.mDestroyCharacter != null)
      this.mDestroyCharacter();
    this.DestroyMaterials();
    foreach (GameObject equipmentChangeList in this.mPrimaryEquipmentChangeLists)
    {
      if (Object.op_Implicit((Object) equipmentChangeList))
        Object.Destroy((Object) equipmentChangeList.gameObject);
    }
    this.mPrimaryEquipmentChangeLists.Clear();
    foreach (GameObject equipmentChangeList in this.mSecondaryEquipmentChangeLists)
    {
      if (Object.op_Implicit((Object) equipmentChangeList))
        Object.Destroy((Object) equipmentChangeList.gameObject);
    }
    this.mSecondaryEquipmentChangeLists.Clear();
  }

  public void SetLim(bool enable)
  {
    if (this.CharacterMaterials == null)
      return;
    if (!enable)
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        this.CharacterMaterials[index].EnableKeyword("WITHOUT_LIM");
    }
    else
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        this.CharacterMaterials[index].DisableKeyword("WITHOUT_LIM");
      Vector3 position = ((Component) this).transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if (Object.op_Equality((Object) volume, (Object) null) || !this.mEnableStaticLight)
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].SetColor("_directLitColor", directLit);
        this.CharacterMaterials[index].SetColor("_indirectLitColor", indirectLit);
      }
    }
  }

  private void SetMaterialColor(List<Material> materials, Color color)
  {
    for (int index = 0; index < materials.Count; ++index)
    {
      materials[index].EnableKeyword("USE_FADE_COLOR");
      materials[index].SetColor("_fadeColor", color);
    }
  }

  public void SetVisible(bool visible)
  {
    GameUtility.SetLayer((Component) this, !visible ? GameUtility.LayerHidden : GameUtility.LayerCH, true);
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
      if (!Object.op_Equality((Object) this.CharacterMaterials[index], (Object) null))
        this.SetMaterialColor(this.CharacterMaterials, color);
    }
  }

  public void SetVesselStrength(float strength, GameObject go, float VesselStrength)
  {
    if ((double) strength > 1.0)
      strength = 1f;
    if ((double) strength < 0.0)
      strength = 0.0f;
    if (this.CharacterMaterials == null)
      return;
    for (int index = 0; index < this.CharacterMaterials.Count; ++index)
    {
      Material characterMaterial = this.CharacterMaterials[index];
      if (!Object.op_Equality((Object) characterMaterial, (Object) null))
      {
        if (!string.IsNullOrEmpty(characterMaterial.GetTag("Bloom", false, (string) null)))
        {
          float num = 0.003921569f;
          characterMaterial.EnableKeyword("MODE_BLOOM");
          characterMaterial.DisableKeyword("MODE_DEFAULT");
          characterMaterial.SetVector("_glowColor", new Vector4((float) this.VesselColor.r * num, (float) this.VesselColor.g * num, (float) this.VesselColor.b * num, Mathf.Pow(strength, 0.7f)));
          characterMaterial.SetFloat("_colorMultipler", (float) (1.0 - (double) strength * 0.40000000596046448));
          characterMaterial.SetFloat("_glowStrength", Mathf.Pow(strength, 1.5f) * VesselStrength);
        }
        else
        {
          characterMaterial.EnableKeyword("MODE_DEFAULT");
          characterMaterial.DisableKeyword("MODE_BLOOM");
          characterMaterial.SetFloat("_colorMultipler", 1f);
        }
      }
    }
  }

  private List<GameObject> mPrimaryEquipmentChangeLists => this.PrimaryEquipmentChangeLists;

  private List<GameObject> mSecondaryEquipmentChangeLists => this.SecondaryEquipmentChangeLists;

  public void SetEquipmentsVisible(bool visible)
  {
    if (Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
      this.mPrimaryEquipment.gameObject.SetActive(visible);
    if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
      return;
    this.mSecondaryEquipment.gameObject.SetActive(visible);
  }

  public void SetPrimaryEquipmentsVisible(bool visible)
  {
    if (!Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
      return;
    this.mPrimaryEquipment.gameObject.SetActive(visible);
  }

  public void SetSecondaryEquipmentsVisible(bool visible)
  {
    if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
      return;
    this.mSecondaryEquipment.gameObject.SetActive(visible);
  }

  public void SwitchEquipmentLists(GeneratedCharacter.EquipmentType type, int no)
  {
    if (no <= 0)
      return;
    int index = no - 1;
    switch (type)
    {
      case GeneratedCharacter.EquipmentType.PRIMARY:
        if (index >= this.mPrimaryEquipmentChangeLists.Count)
          break;
        bool flag1 = !Object.op_Implicit((Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.activeSelf;
        if (Object.op_Implicit((Object) this.mPrimaryEquipment))
          this.mPrimaryEquipment.SetActive(false);
        this.mPrimaryEquipment = this.mPrimaryEquipmentChangeLists[index];
        if (!Object.op_Implicit((Object) this.mPrimaryEquipment))
          break;
        this.mPrimaryEquipment.SetActive(flag1);
        break;
      case GeneratedCharacter.EquipmentType.SECONDARY:
        if (index >= this.mSecondaryEquipmentChangeLists.Count)
          break;
        bool flag2 = !Object.op_Implicit((Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
        if (Object.op_Implicit((Object) this.mSecondaryEquipment))
          this.mSecondaryEquipment.SetActive(false);
        this.mSecondaryEquipment = this.mSecondaryEquipmentChangeLists[index];
        if (!Object.op_Implicit((Object) this.mSecondaryEquipment))
          break;
        this.mSecondaryEquipment.SetActive(flag2);
        break;
    }
  }

  public delegate void OnDestroyCharacter();

  public enum EquipmentType
  {
    PRIMARY,
    SECONDARY,
  }
}
