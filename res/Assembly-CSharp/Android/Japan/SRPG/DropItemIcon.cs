// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class DropItemIcon : ItemIcon
  {
    private ConceptCardParam mSecretConceptCardParam;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam = (ItemParam) null;
        int itemNum = 0;
        this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
        if (itemParam != null)
          return true;
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        return conceptCardParam != null || dropItemData != null;
      }
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      RectTransform transform = this.transform as RectTransform;
      Tooltip.TooltipPosition = screen + Vector2.up * transform.rect.height * 0.5f;
      Tooltip original = AssetManager.Load<Tooltip>("UI/ItemTooltip");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      Tooltip tooltip = UnityEngine.Object.Instantiate<Tooltip>(original);
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
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
        DataSource.Bind<ItemParam>(tooltip.gameObject, itemParam, false);
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
      if ((bool) ((UnityEngine.Object) tooltip.TextName))
      {
        GameParameter component = tooltip.TextName.GetComponent<GameParameter>();
        if ((bool) ((UnityEngine.Object) component))
          component.enabled = false;
        tooltip.TextName.text = str1;
      }
      if ((bool) ((UnityEngine.Object) tooltip.TextDesc))
      {
        GameParameter component = tooltip.TextDesc.GetComponent<GameParameter>();
        if ((bool) ((UnityEngine.Object) component))
          component.enabled = false;
        tooltip.TextDesc.text = str2;
      }
      CanvasStack component1 = tooltip.GetComponent<CanvasStack>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
        return;
      component1.SystemModal = true;
      component1.Priority = 1;
    }

    public override void UpdateValue()
    {
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
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
      if ((bool) ((UnityEngine.Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!(bool) ((UnityEngine.Object) this.SecretBadge))
        return;
      this.SecretBadge.enabled = false;
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
      if ((bool) ((UnityEngine.Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!(bool) ((UnityEngine.Object) this.SecretBadge))
        return;
      this.SecretBadge.enabled = false;
    }

    private void Refresh_DropItem(QuestResult.DropItemData data)
    {
      if (data == null)
        return;
      if (data.IsItem)
        this.Refresh_Item(data.itemParam);
      else if (data.IsConceptCard)
        this.Refresh_ConceptCard(data.conceptCardParam);
      if ((UnityEngine.Object) this.Num != (UnityEngine.Object) null)
        this.Num.text = data.Num.ToString();
      if (!((UnityEngine.Object) this.HaveNum != (UnityEngine.Object) null))
        return;
      int num = -1;
      if (data.IsItem)
        num = this.GetHaveNum(data.itemParam, -1);
      else if (data.IsConceptCard)
        num = this.GetHaveNum(data.conceptCardParam, -1);
      if (num < 0)
        return;
      this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) num
      });
    }

    private void SetFrameSprite(ItemParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Frame == (UnityEngine.Object) null)
        return;
      this.Frame.sprite = this.GetFrameSprite(param, isSecret);
    }

    private void SetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Frame == (UnityEngine.Object) null)
        return;
      this.Frame.sprite = this.GetFrameSprite(param, isSecret);
    }

    private void SetIconAsync(ItemParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Icon == (UnityEngine.Object) null)
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void SetIconAsync(ConceptCardParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Icon == (UnityEngine.Object) null)
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void LoadIcon(ItemParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Icon == (UnityEngine.Object) null)
        return;
      this.Icon.texture = (Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret));
    }

    private void LoadIcon(ConceptCardParam param, bool isSecret)
    {
      if ((UnityEngine.Object) this.Icon == (UnityEngine.Object) null)
        return;
      this.Icon.texture = (Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret));
    }

    private void SwapIconFramePriority(bool iconIsTop)
    {
      if (!((UnityEngine.Object) this.Icon != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Frame != (UnityEngine.Object) null))
        return;
      int siblingIndex1 = this.Icon.transform.GetSiblingIndex();
      int siblingIndex2 = this.Frame.transform.GetSiblingIndex();
      int index1 = Mathf.Min(siblingIndex1, siblingIndex2);
      int index2 = Mathf.Max(siblingIndex1, siblingIndex2);
      if (iconIsTop)
      {
        this.Icon.transform.SetSiblingIndex(index2);
        this.Frame.transform.SetSiblingIndex(index1);
      }
      else
      {
        this.Icon.transform.SetSiblingIndex(index1);
        this.Frame.transform.SetSiblingIndex(index2);
      }
    }

    private void GetParam(ref ConceptCardParam conceptCardParam, ref QuestResult.DropItemData dropItemData)
    {
      conceptCardParam = DataSource.FindDataOfClass<ConceptCardParam>(this.gameObject, (ConceptCardParam) null);
      if (conceptCardParam != null)
        return;
      dropItemData = DataSource.FindDataOfClass<QuestResult.DropItemData>(this.gameObject, (QuestResult.DropItemData) null);
      if (dropItemData != null)
        ;
    }

    private string GetSecretIconPath()
    {
      return AssetPath.ItemIcon("IT_UNKNOWN");
    }

    private string GetIconPath(ItemData data, bool isSecret)
    {
      return this.GetIconPath(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private string GetIconPath(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      if (param == null)
        return string.Empty;
      return AssetPath.ItemIcon(param);
    }

    private string GetIconPath(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      if (param == null)
        return string.Empty;
      return AssetPath.ConceptCardIcon(param);
    }

    private Sprite GetSecretFrameSprite(Sprite defaultSprite)
    {
      if (GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0)
        return GameSettings.Instance.ItemIcons.NormalFrames[0];
      return defaultSprite;
    }

    private Sprite GetFrameSprite(ItemData data, bool isSecret)
    {
      return this.GetFrameSprite(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private Sprite GetFrameSprite(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!((UnityEngine.Object) this.Frame != (UnityEngine.Object) null) ? (Sprite) null : this.Frame.sprite);
      if (param == null)
        return (Sprite) null;
      return GameSettings.Instance.GetItemFrame(param);
    }

    private Sprite GetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!((UnityEngine.Object) this.Frame != (UnityEngine.Object) null) ? (Sprite) null : this.Frame.sprite);
      if (param == null)
        return (Sprite) null;
      return GameSettings.Instance.GetConceptCardFrame(param.rare);
    }

    private int GetHaveNum(ItemParam param, int default_value)
    {
      if (param.iname == "$COIN")
        return MonoSingleton<GameManager>.Instance.Player.Coin;
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
      if (itemDataByItemParam != null)
        return itemDataByItemParam.Num;
      return default_value;
    }

    private int GetHaveNum(ConceptCardParam param, int default_value)
    {
      return default_value;
    }

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
      return (IEnumerator) new DropItemIcon.\u003CexchgSecretIcon\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
