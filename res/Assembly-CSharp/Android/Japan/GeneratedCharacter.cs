// Decompiled with JetBrains decompiler
// Type: GeneratedCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedCharacter : MonoBehaviour
{
  private Color32 VesselColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
  private Color UnitColor = Color.white;
  private List<GameObject> PrimaryEquipmentChangeLists = new List<GameObject>();
  private List<GameObject> SecondaryEquipmentChangeLists = new List<GameObject>();
  private FaceAnimation mFaceAnimation;
  private bool mPlayingFaceAnimation;
  public GeneratedCharacter.OnDestroyCharacter mDestroyCharacter;
  private List<Material> mCharacterMaterials;
  private GameObject mPrimaryEquipment;
  private GameObject mSecondaryEquipment;
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

  public void SetVesselColor(Color32 color)
  {
    this.VesselColor = color;
  }

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
    foreach (UnityEngine.Object characterMaterial in this.mCharacterMaterials)
      UnityEngine.Object.Destroy(characterMaterial);
    this.mCharacterMaterials = (List<Material>) null;
  }

  public void SetEquip(EquipmentSet Equip)
  {
    CharacterSettings component = this.GetComponent<CharacterSettings>();
    EquipmentSet equipmentSet = Equip;
    List<GameObject> primaryHandChangeLists = equipmentSet.PrimaryHandChangeLists;
    GameObject primaryHand;
    if ((UnityEngine.Object) equipmentSet != (UnityEngine.Object) null && equipmentSet.PrimaryForceOverride)
    {
      primaryHand = equipmentSet.PrimaryHand;
      primaryHandChangeLists = equipmentSet.PrimaryHandChangeLists;
    }
    else
      primaryHand = equipmentSet.PrimaryHand;
    GameObject secondaryHand1 = equipmentSet.SecondaryHand;
    List<GameObject> secondaryHandChangeLists = equipmentSet.SecondaryHandChangeLists;
    GameObject secondaryHand2;
    if ((UnityEngine.Object) equipmentSet != (UnityEngine.Object) null && equipmentSet.PrimaryForceOverride)
    {
      secondaryHand2 = equipmentSet.SecondaryHand;
      secondaryHandChangeLists = equipmentSet.SecondaryHandChangeLists;
    }
    else
      secondaryHand2 = equipmentSet.SecondaryHand;
    if ((UnityEngine.Object) primaryHand != (UnityEngine.Object) null && !string.IsNullOrEmpty(component.Rig.RightHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(this.transform, component.Rig.RightHand);
      if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
      {
        this.mPrimaryEquipment = UnityEngine.Object.Instantiate<GameObject>(primaryHand, primaryHand.transform.position, primaryHand.transform.rotation);
        this.mPrimaryEquipment.transform.SetParent(childRecursively, false);
        this.mPrimaryEquipment.transform.localScale = Vector3.one * component.Rig.EquipmentScale;
        if (equipmentSet.PrimaryHidden)
          this.SetPrimaryEquipmentsVisible(false);
      }
      else
        Debug.LogError((object) ("Node " + component.Rig.RightHand + " not found."));
    }
    if (primaryHandChangeLists != null && !string.IsNullOrEmpty(component.Rig.RightHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(this.transform, component.Rig.RightHand);
      if ((bool) ((UnityEngine.Object) childRecursively))
      {
        this.mPrimaryEquipmentDefault = this.mPrimaryEquipment;
        this.mPrimaryEquipmentChangeLists.Clear();
        foreach (GameObject original in primaryHandChangeLists)
        {
          GameObject gameObject = (GameObject) null;
          if ((bool) ((UnityEngine.Object) original))
          {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(original, original.transform.position, original.transform.rotation);
            if ((bool) ((UnityEngine.Object) gameObject))
            {
              gameObject.transform.SetParent(childRecursively, false);
              gameObject.transform.localScale = Vector3.one * component.Rig.EquipmentScale;
              gameObject.gameObject.SetActive(false);
            }
          }
          this.mPrimaryEquipmentChangeLists.Add(gameObject);
        }
      }
    }
    if ((UnityEngine.Object) secondaryHand2 != (UnityEngine.Object) null && !string.IsNullOrEmpty(component.Rig.LeftHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(this.transform, component.Rig.LeftHand);
      if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
      {
        this.mSecondaryEquipment = UnityEngine.Object.Instantiate<GameObject>(secondaryHand2, secondaryHand2.transform.position, secondaryHand2.transform.rotation);
        this.mSecondaryEquipment.transform.SetParent(childRecursively, false);
        this.mSecondaryEquipment.transform.localScale = Vector3.one * component.Rig.EquipmentScale;
        if (equipmentSet.SecondaryHidden)
          this.SetSecondaryEquipmentsVisible(false);
      }
      else
        Debug.LogError((object) ("Node " + component.Rig.LeftHand + " not found."));
    }
    if (secondaryHandChangeLists != null && !string.IsNullOrEmpty(component.Rig.LeftHand))
    {
      Transform childRecursively = GameUtility.findChildRecursively(this.transform, component.Rig.LeftHand);
      if ((bool) ((UnityEngine.Object) childRecursively))
      {
        this.mSecondaryEquipmentDefault = this.mSecondaryEquipment;
        this.mSecondaryEquipmentChangeLists.Clear();
        foreach (GameObject original in secondaryHandChangeLists)
        {
          GameObject gameObject = (GameObject) null;
          if ((bool) ((UnityEngine.Object) original))
          {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(original, original.transform.position, original.transform.rotation);
            if ((bool) ((UnityEngine.Object) gameObject))
            {
              gameObject.transform.SetParent(childRecursively, false);
              gameObject.transform.localScale = Vector3.one * component.Rig.EquipmentScale;
              gameObject.gameObject.SetActive(false);
            }
          }
          this.mSecondaryEquipmentChangeLists.Add(gameObject);
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
        if (!((UnityEngine.Object) this.mPrimaryEquipment != (UnityEngine.Object) this.mPrimaryEquipmentDefault))
          break;
        if ((bool) ((UnityEngine.Object) this.mPrimaryEquipment))
          this.mPrimaryEquipment.SetActive(false);
        this.mPrimaryEquipment = this.mPrimaryEquipmentDefault;
        if (!(bool) ((UnityEngine.Object) this.mPrimaryEquipment))
          break;
        this.mPrimaryEquipment.SetActive(true);
        break;
      case GeneratedCharacter.EquipmentType.SECONDARY:
        if (!((UnityEngine.Object) this.mSecondaryEquipment != (UnityEngine.Object) this.mSecondaryEquipmentDefault))
          break;
        bool flag = !(bool) ((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
        if ((bool) ((UnityEngine.Object) this.mSecondaryEquipment))
          this.mSecondaryEquipment.SetActive(false);
        this.mSecondaryEquipment = this.mSecondaryEquipmentDefault;
        if (!(bool) ((UnityEngine.Object) this.mSecondaryEquipment))
          break;
        this.mSecondaryEquipment.SetActive(flag);
        break;
    }
  }

  private void FindCharacterMaterials()
  {
    Renderer[] componentsInChildren = this.gameObject.GetComponentsInChildren<Renderer>(true);
    if (componentsInChildren == null)
      return;
    List<Renderer> rendererList = new List<Renderer>((IEnumerable<Renderer>) componentsInChildren);
    Renderer component = this.gameObject.GetComponent<Renderer>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
      if ((bool) ((UnityEngine.Object) equipmentChangeList))
        UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
    }
    this.mPrimaryEquipmentChangeLists.Clear();
    foreach (GameObject equipmentChangeList in this.mSecondaryEquipmentChangeLists)
    {
      if ((bool) ((UnityEngine.Object) equipmentChangeList))
        UnityEngine.Object.Destroy((UnityEngine.Object) equipmentChangeList.gameObject);
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
      Vector3 position = this.transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if ((UnityEngine.Object) volume == (UnityEngine.Object) null)
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

  public Color GetColor()
  {
    return this.UnitColor;
  }

  public void SetColor(Color color)
  {
    if ((double) color.r >= 1.0 && (double) color.g >= 1.0 && (double) color.b >= 1.0)
      color = Color.white;
    this.UnitColor = color;
    if (this.CharacterMaterials == null)
      return;
    for (int index = 0; index < this.CharacterMaterials.Count; ++index)
    {
      if (!((UnityEngine.Object) this.CharacterMaterials[index] == (UnityEngine.Object) null))
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
      if (!((UnityEngine.Object) characterMaterial == (UnityEngine.Object) null))
      {
        if (!string.IsNullOrEmpty(characterMaterial.GetTag("Bloom", false, (string) null)))
        {
          float num = 0.003921569f;
          characterMaterial.EnableKeyword("MODE_BLOOM");
          characterMaterial.DisableKeyword("MODE_DEFAULT");
          characterMaterial.SetVector("_glowColor", new Vector4((float) this.VesselColor.r * num, (float) this.VesselColor.g * num, (float) this.VesselColor.b * num, Mathf.Pow(strength, 0.7f)));
          characterMaterial.SetFloat("_colorMultipler", (float) (1.0 - (double) strength * 0.400000005960464));
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

  private List<GameObject> mPrimaryEquipmentChangeLists
  {
    get
    {
      return this.PrimaryEquipmentChangeLists;
    }
  }

  private List<GameObject> mSecondaryEquipmentChangeLists
  {
    get
    {
      return this.SecondaryEquipmentChangeLists;
    }
  }

  public void SetEquipmentsVisible(bool visible)
  {
    if ((UnityEngine.Object) this.mPrimaryEquipment != (UnityEngine.Object) null)
      this.mPrimaryEquipment.gameObject.SetActive(visible);
    if (!((UnityEngine.Object) this.mSecondaryEquipment != (UnityEngine.Object) null))
      return;
    this.mSecondaryEquipment.gameObject.SetActive(visible);
  }

  public void SetPrimaryEquipmentsVisible(bool visible)
  {
    if (!((UnityEngine.Object) this.mPrimaryEquipment != (UnityEngine.Object) null))
      return;
    this.mPrimaryEquipment.gameObject.SetActive(visible);
  }

  public void SetSecondaryEquipmentsVisible(bool visible)
  {
    if (!((UnityEngine.Object) this.mSecondaryEquipment != (UnityEngine.Object) null))
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
        bool flag1 = !(bool) ((UnityEngine.Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.activeSelf;
        if ((bool) ((UnityEngine.Object) this.mPrimaryEquipment))
          this.mPrimaryEquipment.SetActive(false);
        this.mPrimaryEquipment = this.mPrimaryEquipmentChangeLists[index];
        if (!(bool) ((UnityEngine.Object) this.mPrimaryEquipment))
          break;
        this.mPrimaryEquipment.SetActive(flag1);
        break;
      case GeneratedCharacter.EquipmentType.SECONDARY:
        if (index >= this.mSecondaryEquipmentChangeLists.Count)
          break;
        bool flag2 = !(bool) ((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.activeSelf;
        if ((bool) ((UnityEngine.Object) this.mSecondaryEquipment))
          this.mSecondaryEquipment.SetActive(false);
        this.mSecondaryEquipment = this.mSecondaryEquipmentChangeLists[index];
        if (!(bool) ((UnityEngine.Object) this.mSecondaryEquipment))
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
