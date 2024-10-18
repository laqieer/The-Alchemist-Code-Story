// Decompiled with JetBrains decompiler
// Type: UnitVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("Audio/UnitVoice")]
public class UnitVoice : MonoBehaviour
{
  public UnitVoice.ECharType CharType;
  public string DirectCharName;
  public string DirectSkinName;
  public string CueName;
  public bool PlayOnAwake;
  public UnitVoice.eCollaboType CollaboType;
  private bool mPlayAutomatic;
  private string mCharName;
  private string mSkinName;
  private string mCueName;
  private MySound.Voice mVoice;

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnAwake;
    string directName = (string) null;
    string sheetName = (string) null;
    string cueName = (string) null;
    this.GetDefaultCharName(ref directName, ref sheetName, ref cueName);
    this.SetCharName(directName, sheetName, cueName);
  }

  private void OnDisable() => this.mPlayAutomatic = false;

  private void OnDestroy() => this.Discard();

  private void Update()
  {
    if (!this.mPlayAutomatic)
      return;
    this.Play();
    this.mPlayAutomatic = false;
  }

  public void SearchUnitSkinVoiceName(ref string sheetName, ref string cueName)
  {
    UnitParam dataOfClass1 = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
    if (dataOfClass1 != null)
    {
      sheetName = dataOfClass1.voice;
      cueName = dataOfClass1.voice;
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    Unit dataOfClass2 = DataSource.FindDataOfClass<Unit>(((Component) this).gameObject, (Unit) null);
    if (dataOfClass2 != null)
    {
      sheetName = dataOfClass2.GetUnitSkinVoiceSheetName();
      cueName = dataOfClass2.GetUnitSkinVoiceCueName();
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    UnitData dataOfClass3 = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
    if (dataOfClass3 != null)
    {
      sheetName = dataOfClass3.GetUnitSkinVoiceSheetName();
      cueName = dataOfClass3.GetUnitSkinVoiceCueName();
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    SceneBattle instance = SceneBattle.Instance;
    Unit currentUnit = (!UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null)?.CurrentUnit;
    if (currentUnit != null)
    {
      sheetName = currentUnit.GetUnitSkinVoiceSheetName();
      cueName = currentUnit.GetUnitSkinVoiceCueName();
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    if (GameUtility.GetCurrentScene() != GameUtility.EScene.HOME_MULTI)
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
        return;
      sheetName = unitDataByUniqueId.GetUnitSkinVoiceSheetName();
      cueName = unitDataByUniqueId.GetUnitSkinVoiceCueName();
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        ;
    }
    else
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      int slotID = !UnityEngine.Object.op_Equality((UnityEngine.Object) PartyUnitSlot.Active, (UnityEngine.Object) null) ? PartyUnitSlot.Active.Index : -1;
      if (multiPlayerParam == null || multiPlayerParam.units == null || slotID < 0)
        return;
      UnitData unit = Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(multiPlayerParam.units, (Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>) (e => e.slotID == slotID))?.unit;
      if (unit == null)
        return;
      sheetName = unit.GetUnitSkinVoiceSheetName();
      cueName = unit.GetUnitSkinVoiceCueName();
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        ;
    }
  }

  public void GetDefaultCharName(ref string directName, ref string sheetName, ref string cueName)
  {
    directName = this.DirectCharName;
    if (this.CharType != UnitVoice.ECharType.AUTO)
      return;
    this.SearchUnitSkinVoiceName(ref sheetName, ref cueName);
  }

  public bool SetCharName(string directName, string sheetName, string cueName)
  {
    if (this.CharType == UnitVoice.ECharType.BATTLE_SKILL)
    {
      SceneBattle instance = SceneBattle.Instance;
      Unit currentUnit = (!UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null)?.CurrentUnit;
      if (currentUnit != null)
      {
        sheetName = currentUnit.GetUnitSkinVoiceSheetName();
        cueName = currentUnit.GetUnitSkinVoiceCueName();
        if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
          return false;
        this.mCharName = cueName;
        this.mSkinName = (string) null;
        this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
        this.SetupCueName();
        return true;
      }
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      this.mSkinName = (string) null;
      return false;
    }
    if (this.CharType == UnitVoice.ECharType.BATTLE_SKILL_COLLABO)
    {
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      this.mSkinName = (string) null;
      SceneBattle instance = SceneBattle.Instance;
      Unit unit = instance.CollaboMainUnit;
      if (this.CollaboType == UnitVoice.eCollaboType.SUB)
        unit = instance.CollaboSubUnit;
      if (unit == null)
        return false;
      sheetName = unit.GetUnitSkinVoiceSheetName();
      cueName = unit.GetUnitSkinVoiceCueName();
      if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
    {
      if (this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mSkinName = (string) null;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(directName))
    {
      if (this.mVoice != null && directName.Equals(this.mCharName) && this.DirectSkinName == this.mSkinName)
        return false;
      this.mCharName = directName;
      this.mSkinName = this.DirectSkinName;
      this.mVoice = new MySound.Voice(!string.IsNullOrEmpty(this.mSkinName) ? this.mCharName + "_" + this.mSkinName : this.mCharName, this.mCharName);
      this.SetupCueName();
      return true;
    }
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
    this.mSkinName = (string) null;
    return false;
  }

  public bool SetupCueName()
  {
    if (string.IsNullOrEmpty(this.CueName) || string.IsNullOrEmpty(this.mCharName))
      return false;
    this.mCueName = MySound.Voice.ReplaceCharNameOfCueName(this.CueName, this.mCharName);
    return true;
  }

  public void Play()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.PlayDirect(this.mCueName);
  }

  public void Discard()
  {
    if (this.mVoice != null)
      this.mVoice.Cleanup();
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
    this.mSkinName = (string) null;
  }

  public enum ECharType
  {
    AUTO,
    DIRECT_CHAR_NAME,
    BATTLE_SKILL,
    BATTLE_SKILL_COLLABO,
  }

  public enum eCollaboType
  {
    MAIN,
    SUB,
  }
}
