// Decompiled with JetBrains decompiler
// Type: SRPG.RewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class RewardData
  {
    public int Exp;
    public int Gold;
    public int Coin;
    public int ArenaMedal;
    public int MultiCoin;
    public int KakeraCoin;
    public int Stamina;
    public List<ItemData> Items = new List<ItemData>();
    public List<ArtifactRewardData> Artifacts = new List<ArtifactRewardData>();
    public List<int> ItemsBeforeAmount = new List<int>();
    public bool IsOverLimit;
    public Dictionary<string, GiftRecieveItemData> GiftRecieveItemDataDic = new Dictionary<string, GiftRecieveItemData>();

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

    public void AddRewardArtifacts(ArtifactParam artifact_param, int num)
    {
      if (artifact_param == null)
        return;
      ArtifactRewardData artifactRewardData = this.Artifacts.Find((Predicate<ArtifactRewardData>) (data => data.ArtifactParam.iname == artifact_param.iname));
      if (artifactRewardData != null)
        artifactRewardData.Num += num;
      else
        this.Artifacts.Add(new ArtifactRewardData()
        {
          ArtifactParam = artifact_param,
          Num = num
        });
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

    public void AddRewardItems(ItemParam item_param, int num)
    {
      if (item_param == null)
        return;
      ItemData itemData1 = this.Items.Find((Predicate<ItemData>) (data => data.ItemID == item_param.iname));
      if (itemData1 != null)
      {
        itemData1.SetNum(itemData1.Num + num);
      }
      else
      {
        ItemData itemData2 = new ItemData();
        if (!itemData2.Setup(0L, item_param.iname, num))
          return;
        this.Items.Add(itemData2);
      }
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
