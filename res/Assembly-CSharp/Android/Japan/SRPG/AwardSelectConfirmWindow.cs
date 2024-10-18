// Decompiled with JetBrains decompiler
// Type: SRPG.AwardSelectConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      if (!((UnityEngine.Object) this.AwardImg != (UnityEngine.Object) null))
        return;
      ImageArray component = this.AwardImg.GetComponent<ImageArray>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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
        if ((UnityEngine.Object) this.AwardImg != (UnityEngine.Object) null && (UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null)
        {
          if (this.mImageArray.Images.Length <= awardParam.grade)
          {
            this.SetExtraAwardImage(awardParam.bg);
            awardParam.name = string.Empty;
          }
          else
            this.mImageArray.ImageIndex = awardParam.grade;
        }
        if ((UnityEngine.Object) this.AwardName != (UnityEngine.Object) null)
          this.AwardName.text = awardParam.name;
        if (!((UnityEngine.Object) this.ExpText != (UnityEngine.Object) null))
          return;
        this.ExpText.text = awardParam.expr;
      }
    }

    private bool SetExtraAwardImage(string bg)
    {
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (!((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null))
        return false;
      this.mImageArray.sprite = spriteSheet.GetSprite(bg);
      return true;
    }
  }
}
