// Decompiled with JetBrains decompiler
// Type: SRPG.MultiConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class MultiConceptCard
  {
    private List<OLong> mMultiSelectedUniqueID = new List<OLong>();

    public void Clone(MultiConceptCard mbase)
    {
      this.mMultiSelectedUniqueID = new List<OLong>((IEnumerable<OLong>) mbase.mMultiSelectedUniqueID);
    }

    public bool Contains(long uniqueID)
    {
      return this.mMultiSelectedUniqueID.Contains((OLong) uniqueID);
    }

    public void SetArray(ConceptCardData[] array)
    {
      this.mMultiSelectedUniqueID.Clear();
      if (array == null)
        return;
      foreach (ConceptCardData conceptCardData in array)
        this.mMultiSelectedUniqueID.Add(conceptCardData.UniqueID);
    }

    public void Add(ConceptCardData ccd)
    {
      if (ccd == null || this.mMultiSelectedUniqueID.Contains(ccd.UniqueID))
        return;
      this.mMultiSelectedUniqueID.Add(ccd.UniqueID);
    }

    public void Remove(ConceptCardData ccd)
    {
      if (ccd == null || !this.mMultiSelectedUniqueID.Contains(ccd.UniqueID))
        return;
      this.mMultiSelectedUniqueID.Remove(ccd.UniqueID);
    }

    public void Remove(long uniqueID)
    {
      this.mMultiSelectedUniqueID.Remove((OLong) uniqueID);
    }

    public bool IsSelected(ConceptCardData ccd)
    {
      return ccd != null && this.mMultiSelectedUniqueID.Contains(ccd.UniqueID);
    }

    public void Clear()
    {
      this.mMultiSelectedUniqueID.Clear();
    }

    public int Count
    {
      get
      {
        return this.mMultiSelectedUniqueID.Count;
      }
    }

    public void Flip(ConceptCardData ccd)
    {
      if (!this.IsSelected(ccd))
        this.Add(ccd);
      else
        this.Remove(ccd);
    }

    public List<OLong> GetIDList()
    {
      return this.mMultiSelectedUniqueID;
    }

    public List<ConceptCardData> GetList()
    {
      List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>();
      foreach (OLong olong in this.mMultiSelectedUniqueID)
      {
        OLong uid = olong;
        ConceptCardData conceptCardData = MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find((Predicate<ConceptCardData>) (obj => (long) obj.UniqueID == (long) uid));
        conceptCardDataList.Add(conceptCardData);
      }
      return conceptCardDataList;
    }

    public List<long> GetUniqueIDs()
    {
      List<long> longList = new List<long>();
      foreach (OLong olong in this.mMultiSelectedUniqueID)
      {
        long num = (long) olong;
        longList.Add(num);
      }
      return longList;
    }

    public ConceptCardData GetItem(int index)
    {
      if (this.mMultiSelectedUniqueID.Count <= index)
        return (ConceptCardData) null;
      OLong uid = this.mMultiSelectedUniqueID[index];
      return MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find((Predicate<ConceptCardData>) (obj => (long) obj.UniqueID == (long) uid));
    }
  }
}
