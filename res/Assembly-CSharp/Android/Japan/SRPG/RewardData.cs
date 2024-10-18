// Decompiled with JetBrains decompiler
// Type: SRPG.RewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPG
{
  public class RewardData
  {
    public List<ItemData> Items = new List<ItemData>();
    public List<ArtifactRewardData> Artifacts = new List<ArtifactRewardData>();
    public List<int> ItemsBeforeAmount = new List<int>();
    public Dictionary<string, GiftRecieveItemData> GiftRecieveItemDataDic = new Dictionary<string, GiftRecieveItemData>();
    public int Exp;
    public int Gold;
    public int Coin;
    public int ArenaMedal;
    public int MultiCoin;
    public int KakeraCoin;
    public int Stamina;
    public bool IsOverLimit;

    public RewardData()
    {
    }

    public RewardData(TrophyParam trophy)
    {
      this.Exp = trophy.Exp;
      this.Coin = trophy.Coin;
      this.Gold = trophy.Gold;
      this.Stamina = trophy.Stamina;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      foreach (TrophyParam.RewardItem rewardItem in trophy.Items)
      {
        ItemData itemData = new ItemData();
        if (itemData.Setup(0L, rewardItem.iname, rewardItem.Num))
        {
          this.Items.Add(itemData);
          if (itemData.Param.type != EItemType.Unit)
          {
            ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemData.Param.iname);
            this.ItemsBeforeAmount.Add(itemDataByItemId == null ? 0 : itemDataByItemId.Num);
          }
          else
            this.ItemsBeforeAmount.Add(instance.Player.Units.Find((Predicate<UnitData>) (u => u.UnitParam.iname == itemData.ItemID)) == null ? 0 : 1);
        }
      }
      foreach (TrophyParam.RewardItem artifact in trophy.Artifacts)
        this.Artifacts.Add(new ArtifactRewardData()
        {
          ArtifactParam = instance.MasterParam.GetArtifactParam(artifact.iname),
          Num = artifact.Num
        });
      foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
        this.AddReward(instance.MasterParam.GetConceptCardParam(conceptCard.iname), conceptCard.Num);
    }

    public void AddReward(TrophyParam trophy)
    {
      this.Exp += trophy.Exp;
      this.Coin += trophy.Coin;
      this.Gold += trophy.Gold;
      this.Stamina += trophy.Stamina;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      foreach (TrophyParam.RewardItem rewardItem in trophy.Items)
      {
        TrophyParam.RewardItem item = rewardItem;
        ItemData itemData = this.Items.FirstOrDefault<ItemData>((Func<ItemData, bool>) (i => i.ItemID == item.iname));
        if (itemData == null)
        {
          itemData = new ItemData();
          if (itemData.Setup(0L, item.iname, item.Num))
          {
            this.Items.Add(itemData);
            if (itemData.Param.type != EItemType.Unit)
            {
              ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemData.Param.iname);
              this.ItemsBeforeAmount.Add(itemDataByItemId == null ? 0 : itemDataByItemId.Num);
            }
            else
              this.ItemsBeforeAmount.Add(instance.Player.Units.Find((Predicate<UnitData>) (u => u.UnitParam.iname == itemData.ItemID)) == null ? 0 : 1);
          }
        }
        else
          itemData.SetNum(itemData.Num + item.Num);
      }
      foreach (TrophyParam.RewardItem artifact1 in trophy.Artifacts)
      {
        TrophyParam.RewardItem artifact = artifact1;
        ArtifactRewardData artifactRewardData = this.Artifacts.FirstOrDefault<ArtifactRewardData>((Func<ArtifactRewardData, bool>) (a => a.ArtifactParam.iname == artifact.iname));
        if (artifactRewardData == null)
          this.Artifacts.Add(new ArtifactRewardData()
          {
            ArtifactParam = instance.MasterParam.GetArtifactParam(artifact.iname),
            Num = artifact.Num
          });
        else
          artifactRewardData.Num += artifact.Num;
      }
      foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
        this.AddReward(instance.MasterParam.GetConceptCardParam(conceptCard.iname), conceptCard.Num);
    }

    public void AddReward(ArtifactParam param, int num)
    {
      if (param == null)
        return;
      this.AddReward(param.iname, GiftTypes.Artifact, param.rareini, num);
    }

    public void AddReward(ItemParam param, int num)
    {
      if (param == null)
        return;
      if (param.type == EItemType.Unit)
        this.AddReward(param.iname, GiftTypes.Unit, param.rare, num);
      else if (param.type == EItemType.Award)
        this.AddReward(param.iname, GiftTypes.Award, param.rare, num);
      else
        this.AddReward(param.iname, GiftTypes.Item, param.rare, num);
    }

    public void AddReward(ConceptCardParam param, int num)
    {
      if (param == null)
        return;
      this.AddReward(param.iname, GiftTypes.ConceptCard, param.rare, num);
    }

    private void AddReward(string iname, GiftTypes giftTipe, int rarity, int num)
    {
      if (this.GiftRecieveItemDataDic.ContainsKey(iname))
      {
        this.GiftRecieveItemDataDic[iname].num += num;
      }
      else
      {
        GiftRecieveItemData giftRecieveItemData = new GiftRecieveItemData();
        giftRecieveItemData.Set(iname, giftTipe, rarity, num);
        this.GiftRecieveItemDataDic.Add(iname, giftRecieveItemData);
      }
    }

    public void AddReward(GiftRecieveItemData giftRecieveItemData)
    {
      if (giftRecieveItemData == null)
        return;
      if (this.GiftRecieveItemDataDic.ContainsKey(giftRecieveItemData.iname))
        this.GiftRecieveItemDataDic[giftRecieveItemData.iname].num += giftRecieveItemData.num;
      else
        this.GiftRecieveItemDataDic.Add(giftRecieveItemData.iname, giftRecieveItemData);
    }
  }
}
