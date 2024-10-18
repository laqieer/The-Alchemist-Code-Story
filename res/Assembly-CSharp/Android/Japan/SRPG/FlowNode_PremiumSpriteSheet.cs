// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PremiumSpriteSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("Common/SpriteSheet", 32741)]
  [FlowNode.Pin(0, "show", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "output", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_PremiumSpriteSheet : FlowNode
  {
    [SerializeField]
    private string m_SpriteSheetName;
    [SerializeField]
    private Image m_Image;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || string.IsNullOrEmpty(this.m_SpriteSheetName) || !((UnityEngine.Object) this.m_Image != (UnityEngine.Object) null))
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(this.m_SpriteSheetName);
      long serverTime = Network.GetServerTime();
      if (MonoSingleton<GameManager>.Instance.MasterParam.Premium == null)
        DebugUtility.LogError("GameManager.Instance.MasterParam.Premium is null.");
      if (!((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.MasterParam.Premium == null)
        return;
      string name = string.Empty;
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.MasterParam.Premium.Length; ++index)
      {
        PremiumParam premiumParam = MonoSingleton<GameManager>.Instance.MasterParam.Premium[index];
        if (premiumParam.m_BeginAt < serverTime && serverTime <= premiumParam.m_EndAt)
        {
          name = premiumParam.m_Image;
          break;
        }
      }
      if (string.IsNullOrEmpty(name))
      {
        for (int index = 0; index < MonoSingleton<GameManager>.Instance.MasterParam.Premium.Length; ++index)
        {
          PremiumParam premiumParam = MonoSingleton<GameManager>.Instance.MasterParam.Premium[index];
          if (premiumParam.m_BeginAt == 0L && premiumParam.m_EndAt == 0L)
          {
            name = premiumParam.m_Image;
            break;
          }
        }
      }
      this.m_Image.sprite = spriteSheet.GetSprite(name);
    }
  }
}
