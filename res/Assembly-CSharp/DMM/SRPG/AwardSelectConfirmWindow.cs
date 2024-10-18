// Decompiled with JetBrains decompiler
// Type: SRPG.AwardSelectConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AwardSelectConfirmWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject AwardImg;
    [SerializeField]
    private Text AwardName;
    [SerializeField]
    private Text ExpText;
    private GameManager gm;
    private ImageArray mImageArray;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.AwardImg, (Object) null))
        return;
      ImageArray component = this.AwardImg.GetComponent<ImageArray>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mImageArray = component;
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      this.Refresh();
    }

    private void Refresh()
    {
      string key = FlowNode_Variable.Get("CONFIRM_SELECT_AWARD");
      if (string.IsNullOrEmpty(key))
      {
        DebugUtility.LogError("AwardSelectConfirmWindow:select_iname is Null or Empty");
      }
      else
      {
        AwardParam awardParam = this.gm.MasterParam.GetAwardParam(key);
        if (awardParam == null)
          return;
        if (Object.op_Inequality((Object) this.AwardImg, (Object) null) && Object.op_Inequality((Object) this.mImageArray, (Object) null))
        {
          if (this.mImageArray.Images.Length <= awardParam.grade)
          {
            this.SetExtraAwardImage(awardParam.bg);
            awardParam.name = string.Empty;
          }
          else
            this.mImageArray.ImageIndex = awardParam.grade;
        }
        if (Object.op_Inequality((Object) this.AwardName, (Object) null))
          this.AwardName.text = awardParam.name;
        if (!Object.op_Inequality((Object) this.ExpText, (Object) null))
          return;
        this.ExpText.text = awardParam.expr;
      }
    }

    private bool SetExtraAwardImage(string bg)
    {
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (!Object.op_Inequality((Object) spriteSheet, (Object) null))
        return false;
      this.mImageArray.sprite = spriteSheet.GetSprite(bg);
      return true;
    }
  }
}
