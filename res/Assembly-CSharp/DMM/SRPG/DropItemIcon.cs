// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DropItemIcon : ItemIcon
  {
    private ConceptCardParam mSecretConceptCardParam;
    [SerializeField]
    private bool IsUpdateValueWithSecretFlag;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam = (ItemParam) null;
        int itemNum = 0;
        this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out itemNum);
        if (itemParam != null)
          return true;
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        return conceptCardParam != null || dropItemData != null;
      }
    }

    protected override void ShowTooltip(Vector2 screen) => this.CreateTooltip(screen, false);

    protected override void ShowTooltipByTap(Vector2 _screen) => this.CreateTooltip(_screen, true);

    private void CreateTooltip(Vector2 _screen, bool _is_tap)
    {
      RectTransform transform = ((Component) this).transform as RectTransform;
      Vector2 vector2_1 = _screen;
      Vector2 up = Vector2.up;
      Rect rect = transform.rect;
      double height = (double) ((Rect) ref rect).height;
      Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(up, (float) height), 0.5f);
      SRPG.Tooltip.TooltipPosition = Vector2.op_Addition(vector2_1, vector2_2);
      SRPG.Tooltip tooltip1 = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
      if (Object.op_Equality((Object) tooltip1, (Object) null))
        return;
      SRPG.Tooltip tooltip2 = Object.Instantiate<SRPG.Tooltip>(tooltip1);
      if (_is_tap)
      {
        tooltip2.CloseOnPress = true;
        tooltip2.DestroyDelay = 0.0f;
      }
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out itemNum);
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (this.IsSecret)
      {
        str1 = "sys.ITEMTOOLTIP_SECRET_NAME";
        str2 = "sys.ITEMTOOLTIP_SECRET_DESC";
      }
      else if (itemParam != null)
      {
        str1 = itemParam.name;
        str2 = itemParam.Expr;
        DataSource.Bind<ItemParam>(((Component) tooltip2).gameObject, itemParam);
      }
      else
      {
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        if (conceptCardParam != null)
        {
          str1 = conceptCardParam.name;
          str2 = conceptCardParam.expr;
        }
        else if (dropItemData != null)
        {
          if (dropItemData.IsItem)
          {
            str1 = dropItemData.itemParam.name;
            str2 = dropItemData.itemParam.Expr;
          }
          else if (dropItemData.IsConceptCard)
          {
            str1 = dropItemData.conceptCardParam.name;
            str2 = dropItemData.conceptCardParam.expr;
          }
          int num = dropItemData.Num;
        }
      }
      if (Object.op_Implicit((Object) tooltip2.TextName))
      {
        GameParameter component = ((Component) tooltip2.TextName).GetComponent<GameParameter>();
        if (Object.op_Implicit((Object) component))
          ((Behaviour) component).enabled = false;
        tooltip2.TextName.text = str1;
      }
      if (Object.op_Implicit((Object) tooltip2.TextDesc))
      {
        GameParameter component = ((Component) tooltip2.TextDesc).GetComponent<GameParameter>();
        if (Object.op_Implicit((Object) component))
          ((Behaviour) component).enabled = false;
        tooltip2.TextDesc.text = str2;
      }
      RuneData dataOfClass = DataSource.FindDataOfClass<RuneData>(((Component) this).gameObject, (RuneData) null);
      if (dataOfClass != null)
        DataSource.Bind<RuneData>(((Component) tooltip2).gameObject, dataOfClass);
      CanvasStack component1 = ((Component) tooltip2).GetComponent<CanvasStack>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.SystemModal = true;
      component1.Priority = 1;
    }

    public override void UpdateValue()
    {
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out itemNum);
      if (itemParam != null)
      {
        base.UpdateValue();
      }
      else
      {
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        if (conceptCardParam != null)
        {
          this.Refresh_ConceptCard(conceptCardParam);
        }
        else
        {
          if (dropItemData == null)
            return;
          this.Refresh_DropItem(dropItemData);
        }
      }
    }

    private void Refresh_Item(ItemParam param)
    {
      if (param == null)
        return;
      this.SetIconAsync(param, this.IsSecret);
      this.SetFrameSprite(param, this.IsSecret);
      this.SwapIconFramePriority(true);
      if (!this.IsSecret)
        return;
      if (Object.op_Implicit((Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!Object.op_Implicit((Object) this.SecretBadge))
        return;
      ((Behaviour) this.SecretBadge).enabled = false;
    }

    private void Refresh_ConceptCard(ConceptCardParam param)
    {
      if (param == null)
        return;
      this.mSecretConceptCardParam = param;
      this.SetIconAsync(param, this.IsSecret);
      this.SetFrameSprite(param, this.IsSecret);
      this.SwapIconFramePriority(this.IsSecret);
      if (!this.IsSecret)
        return;
      if (Object.op_Implicit((Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!Object.op_Implicit((Object) this.SecretBadge))
        return;
      ((Behaviour) this.SecretBadge).enabled = false;
    }

    private void Refresh_DropItem(QuestResult.DropItemData data)
    {
      if (data == null)
        return;
      if (this.IsUpdateValueWithSecretFlag)
        this.IsSecret = data.mIsSecret;
      if (data.IsItem)
        this.Refresh_Item(data.itemParam);
      else if (data.IsConceptCard)
        this.Refresh_ConceptCard(data.conceptCardParam);
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.text = data.Num.ToString();
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int num = -1;
      if (data.IsItem)
        num = this.GetHaveNum(data.itemParam, -1);
      else if (data.IsConceptCard)
        num = this.GetHaveNum(data.conceptCardParam, -1);
      if (num < 0)
        return;
      this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", (object) num);
    }

    private void SetFrameSprite(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Frame, (Object) null))
        return;
      this.Frame.sprite = this.GetFrameSprite(param, isSecret);
    }

    private void SetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Frame, (Object) null))
        return;
      this.Frame.sprite = this.GetFrameSprite(param, isSecret);
    }

    private void SetIconAsync(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void SetIconAsync(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void LoadIcon(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.texture = (Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret));
    }

    private void LoadIcon(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.texture = (Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret));
    }

    private void SwapIconFramePriority(bool iconIsTop)
    {
      if (!Object.op_Inequality((Object) this.Icon, (Object) null) || !Object.op_Inequality((Object) this.Frame, (Object) null))
        return;
      int siblingIndex1 = ((Component) this.Icon).transform.GetSiblingIndex();
      int siblingIndex2 = ((Component) this.Frame).transform.GetSiblingIndex();
      int num1 = Mathf.Min(siblingIndex1, siblingIndex2);
      int num2 = Mathf.Max(siblingIndex1, siblingIndex2);
      if (iconIsTop)
      {
        ((Component) this.Icon).transform.SetSiblingIndex(num2);
        ((Component) this.Frame).transform.SetSiblingIndex(num1);
      }
      else
      {
        ((Component) this.Icon).transform.SetSiblingIndex(num1);
        ((Component) this.Frame).transform.SetSiblingIndex(num2);
      }
    }

    private void GetParam(
      ref ConceptCardParam conceptCardParam,
      ref QuestResult.DropItemData dropItemData)
    {
      conceptCardParam = DataSource.FindDataOfClass<ConceptCardParam>(((Component) this).gameObject, (ConceptCardParam) null);
      if (conceptCardParam != null)
        return;
      dropItemData = DataSource.FindDataOfClass<QuestResult.DropItemData>(((Component) this).gameObject, (QuestResult.DropItemData) null);
      if (dropItemData == null)
        ;
    }

    private string GetSecretIconPath() => AssetPath.ItemIcon("IT_UNKNOWN");

    private string GetIconPath(ItemData data, bool isSecret)
    {
      return this.GetIconPath(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private string GetIconPath(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      return param == null ? string.Empty : AssetPath.ItemIcon(param);
    }

    private string GetIconPath(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      return param == null ? string.Empty : AssetPath.ConceptCardIcon(param);
    }

    private Sprite GetSecretFrameSprite(Sprite defaultSprite)
    {
      return GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0 ? GameSettings.Instance.ItemIcons.NormalFrames[0] : defaultSprite;
    }

    private Sprite GetFrameSprite(ItemData data, bool isSecret)
    {
      return this.GetFrameSprite(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private Sprite GetFrameSprite(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!Object.op_Inequality((Object) this.Frame, (Object) null) ? (Sprite) null : this.Frame.sprite);
      return param == null ? (Sprite) null : GameSettings.Instance.GetItemFrame(param);
    }

    private Sprite GetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!Object.op_Inequality((Object) this.Frame, (Object) null) ? (Sprite) null : this.Frame.sprite);
      return param == null ? (Sprite) null : GameSettings.Instance.GetConceptCardFrame(param.rare);
    }

    private int GetHaveNum(ItemParam param, int default_value)
    {
      if (param.iname == "$COIN")
        return MonoSingleton<GameManager>.Instance.Player.Coin;
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
      return itemDataByItemParam != null ? itemDataByItemParam.Num : default_value;
    }

    private int GetHaveNum(ConceptCardParam param, int default_value) => default_value;

    public override void ExchgSecretIcon()
    {
      if (!this.IsSecret || this.mReqExchgSecretIcon || this.mSecretItemParam == null && this.mSecretConceptCardParam == null)
        return;
      this.mReqExchgSecretIcon = true;
      this.StartCoroutine(this.exchgSecretIcon());
    }

    [DebuggerHidden]
    protected override IEnumerator exchgSecretIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DropItemIcon.\u003CexchgSecretIcon\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
