// Decompiled with JetBrains decompiler
// Type: SRPG.RuneContinueEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneContinueEnhance : MonoBehaviour
  {
    private readonly string SVB_KEY_IMG_ARRAY_ON = "on_img_array";
    private readonly string SVB_KEY_IMG_ARRAY_OFF = "off_img_array";
    [SerializeField]
    private int[] mPlusCount;
    [SerializeField]
    private int[] mEnhanceCount;
    [SerializeField]
    private Toggle mPlusCountTemplate;
    [SerializeField]
    private Toggle mEnhanceCountTemplate;
    [SerializeField]
    private Text mSelectedPlusCountText;
    [SerializeField]
    private Text mSelectedEnhanceCountText;
    [SerializeField]
    private Toggle mPlusCountTab;
    [SerializeField]
    private Toggle mEnhanceCountTab;
    private List<Toggle> mCreatedPlusCountToggles = new List<Toggle>();
    private List<Toggle> mCreatedEnhanceCountToggles = new List<Toggle>();
    private int mSelectedPlusCount;
    private int mSelectedEnhanceCount;
    private BindRuneData mTargetRune;

    public int SelectedPlusCount => this.mSelectedPlusCount;

    public int SelectedEnhanceCount => this.mSelectedEnhanceCount;

    public void Init(BindRuneData bind_rune)
    {
      this.mTargetRune = bind_rune;
      int default_plus_count_index = 0;
      for (int index = 0; index < this.mPlusCount.Length; ++index)
      {
        if (this.mTargetRune.EnhanceNum < this.mPlusCount[index])
        {
          default_plus_count_index = index;
          break;
        }
      }
      this.mSelectedPlusCount = this.mPlusCount[default_plus_count_index];
      this.mSelectedEnhanceCount = this.mEnhanceCount[0];
      GameUtility.SetToggle(this.mPlusCountTab, true);
      GameUtility.SetToggle(this.mEnhanceCountTab, false);
      this.CreateEnhanceUI(default_plus_count_index, 0);
      this.Refresh();
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedPlusCountText, (UnityEngine.Object) null))
        this.mSelectedPlusCountText.text = string.Format(LocalizedText.Get("sys.RUNE_CONTINUE_ENHANCE_SETTING_TEXT1"), (object) this.mSelectedPlusCount);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedEnhanceCountText, (UnityEngine.Object) null))
        return;
      this.mSelectedEnhanceCountText.text = string.Format(LocalizedText.Get("sys.RUNE_CONTINUE_ENHANCE_SETTING_TEXT2"), (object) this.mSelectedEnhanceCount);
    }

    private void CreateEnhanceUI(int default_plus_count_index, int default_enhance_count_index)
    {
      GameUtility.SetGameObjectActive((Component) this.mPlusCountTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.mEnhanceCountTemplate, false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlusCountTemplate, (UnityEngine.Object) null) && this.mPlusCount != null)
      {
        for (int img_index = 0; img_index < this.mPlusCount.Length; ++img_index)
        {
          Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.mPlusCountTemplate, ((Component) this.mPlusCountTemplate).transform.parent, false);
          this.SetImageArray(((Component) toggle).gameObject, img_index);
          ((Selectable) toggle).interactable = this.mTargetRune.EnhanceNum < this.mPlusCount[img_index];
          ((Component) toggle).gameObject.SetActive(true);
          this.mCreatedPlusCountToggles.Add(toggle);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEnhanceCountTemplate, (UnityEngine.Object) null) && this.mEnhanceCount != null)
      {
        for (int img_index = 0; img_index < this.mEnhanceCount.Length; ++img_index)
        {
          Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.mEnhanceCountTemplate, ((Component) this.mEnhanceCountTemplate).transform.parent, false);
          this.SetImageArray(((Component) toggle).gameObject, img_index);
          ((Component) toggle).gameObject.SetActive(true);
          this.mCreatedEnhanceCountToggles.Add(toggle);
        }
      }
      if (default_plus_count_index < this.mCreatedPlusCountToggles.Count)
        GameUtility.SetToggle(this.mCreatedPlusCountToggles[default_plus_count_index], true);
      if (default_enhance_count_index >= this.mCreatedEnhanceCountToggles.Count)
        return;
      GameUtility.SetToggle(this.mCreatedEnhanceCountToggles[default_enhance_count_index], true);
    }

    private void SetImageArray(GameObject obj, int img_index)
    {
      SerializeValueBehaviour component1 = obj.GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = component1.list.GetGameObject(this.SVB_KEY_IMG_ARRAY_ON);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
      {
        ImageArray component2 = gameObject1.GetComponent<ImageArray>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          component2.ImageIndex = img_index;
      }
      GameObject gameObject2 = component1.list.GetGameObject(this.SVB_KEY_IMG_ARRAY_OFF);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      ImageArray component3 = gameObject2.GetComponent<ImageArray>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
        return;
      component3.ImageIndex = img_index;
    }

    public void GetEnhanceSettingParam(ref RuneEnhanceSettings.eEnhanceMode mode, ref int value)
    {
      mode = RuneEnhanceSettings.eEnhanceMode.Normal;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlusCountTab, (UnityEngine.Object) null) && this.mPlusCountTab.isOn)
      {
        mode = RuneEnhanceSettings.eEnhanceMode.Limit_PlusCount;
        value = this.SelectedPlusCount;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEnhanceCountTab, (UnityEngine.Object) null) || !this.mEnhanceCountTab.isOn)
        return;
      mode = RuneEnhanceSettings.eEnhanceMode.Limit_EnhanceCount;
      value = this.SelectedEnhanceCount;
    }

    public void OnClickPlusCountToggle(Toggle self)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) self, (UnityEngine.Object) null))
        return;
      int index = this.mCreatedPlusCountToggles.FindIndex((Predicate<Toggle>) (tgl => UnityEngine.Object.op_Equality((UnityEngine.Object) tgl, (UnityEngine.Object) self)));
      if (index >= 0 && this.mPlusCount.Length > index)
        this.mSelectedPlusCount = this.mPlusCount[index];
      this.Refresh();
    }

    public void OnClickEnhanceCountToggle(Toggle self)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) self, (UnityEngine.Object) null))
        return;
      int index = this.mCreatedEnhanceCountToggles.FindIndex((Predicate<Toggle>) (tgl => UnityEngine.Object.op_Equality((UnityEngine.Object) tgl, (UnityEngine.Object) self)));
      if (index >= 0 && this.mEnhanceCount.Length > index)
        this.mSelectedEnhanceCount = this.mEnhanceCount[index];
      this.Refresh();
    }
  }
}
