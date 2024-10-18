// Decompiled with JetBrains decompiler
// Type: UnitVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;

[AddComponentMenu("Audio/UnitVoice")]
public class UnitVoice : MonoBehaviour
{
  public UnitVoice.ECharType CharType;
  public string DirectCharName;
  public string CueName;
  public bool PlayOnAwake;
  public UnitVoice.eCollaboType CollaboType;
  private bool mPlayAutomatic;
  private string mCharName;
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

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
  }

  private void OnDestroy()
  {
    this.Discard();
  }

  private void Update()
  {
    if (!this.mPlayAutomatic)
      return;
    this.Play();
    this.mPlayAutomatic = false;
  }

  public void SearchUnitSkinVoiceName(ref string sheetName, ref string cueName)
  {
    UnitParam dataOfClass1 = DataSource.FindDataOfClass<UnitParam>(this.gameObject, (UnitParam) null);
    if (dataOfClass1 != null)
    {
      sheetName = dataOfClass1.voice;
      cueName = dataOfClass1.voice;
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    Unit dataOfClass2 = DataSource.FindDataOfClass<Unit>(this.gameObject, (Unit) null);
    if (dataOfClass2 != null)
    {
      sheetName = dataOfClass2.GetUnitSkinVoiceSheetName(-1);
      cueName = dataOfClass2.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    UnitData dataOfClass3 = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
    if (dataOfClass3 != null)
    {
      sheetName = dataOfClass3.GetUnitSkinVoiceSheetName(-1);
      cueName = dataOfClass3.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    SceneBattle instance = SceneBattle.Instance;
    Unit currentUnit = (!((UnityEngine.Object) instance == (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null)?.CurrentUnit;
    if (currentUnit != null)
    {
      sheetName = currentUnit.GetUnitSkinVoiceSheetName(-1);
      cueName = currentUnit.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    if (GameUtility.GetCurrentScene() != GameUtility.EScene.HOME_MULTI)
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
        return;
      sheetName = unitDataByUniqueId.GetUnitSkinVoiceSheetName(-1);
      cueName = unitDataByUniqueId.GetUnitSkinVoiceCueName(-1);
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        ;
    }
    else
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      int slotID = !((UnityEngine.Object) PartyUnitSlot.Active == (UnityEngine.Object) null) ? PartyUnitSlot.Active.Index : -1;
      if (multiPlayerParam == null || multiPlayerParam.units == null || slotID < 0)
        return;
      UnitData unit = Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(multiPlayerParam.units, (Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>) (e => e.slotID == slotID))?.unit;
      if (unit == null)
        return;
      sheetName = unit.GetUnitSkinVoiceSheetName(-1);
      cueName = unit.GetUnitSkinVoiceCueName(-1);
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
      Unit currentUnit = (!((UnityEngine.Object) instance == (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null)?.CurrentUnit;
      if (currentUnit != null)
      {
        sheetName = currentUnit.GetUnitSkinVoiceSheetName(-1);
        cueName = currentUnit.GetUnitSkinVoiceCueName(-1);
        if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
          return false;
        this.mCharName = cueName;
        this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_", false);
        this.SetupCueName();
        return true;
      }
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      return false;
    }
    if (this.CharType == UnitVoice.ECharType.BATTLE_SKILL_COLLABO)
    {
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      SceneBattle instance = SceneBattle.Instance;
      Unit unit = instance.CollaboMainUnit;
      if (this.CollaboType == UnitVoice.eCollaboType.SUB)
        unit = instance.CollaboSubUnit;
      if (unit == null)
        return false;
      sheetName = unit.GetUnitSkinVoiceSheetName(-1);
      cueName = unit.GetUnitSkinVoiceCueName(-1);
      if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_", false);
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
    {
      if (this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_", false);
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(directName))
    {
      if (this.mVoice != null && directName.Equals(this.mCharName))
        return false;
      this.mCharName = directName;
      this.mVoice = new MySound.Voice(this.mCharName);
      this.SetupCueName();
      return true;
    }
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
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
    this.mVoice.PlayDirect(this.mCueName, 0.0f);
  }

  public void Discard()
  {
    if (this.mVoice != null)
      this.mVoice.Cleanup();
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
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
