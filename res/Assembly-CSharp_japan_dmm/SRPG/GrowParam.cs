// Decompiled with JetBrains decompiler
// Type: SRPG.GrowParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GrowParam
  {
    public string type;
    public List<GrowSample> curve;

    public bool Deserialize(JSON_GrowParam json)
    {
      if (json == null)
        return false;
      this.type = json.type;
      if (json.curve != null)
      {
        int length = json.curve.Length;
        this.curve = new List<GrowSample>(length);
        for (int index = 0; index < length; ++index)
          this.curve.Add(new GrowSample()
          {
            lv = (OInt) json.curve[index].lv,
            scale = (OInt) json.curve[index].val,
            status = {
              param = {
                hp = (OInt) json.curve[index].hp,
                mp = (OShort) json.curve[index].mp,
                atk = (OShort) json.curve[index].atk,
                def = (OShort) json.curve[index].def,
                mag = (OShort) json.curve[index].mag,
                mnd = (OShort) json.curve[index].mnd,
                dex = (OShort) json.curve[index].dex,
                spd = (OShort) json.curve[index].spd,
                cri = (OShort) json.curve[index].cri,
                luk = (OShort) json.curve[index].luk
              },
              element_assist = {
                fire = (short) json.curve[index].afi,
                water = (short) json.curve[index].awa,
                wind = (short) json.curve[index].awi,
                thunder = (short) json.curve[index].ath,
                shine = (short) json.curve[index].ash,
                dark = (short) json.curve[index].ada
              },
              element_resist = {
                fire = (short) json.curve[index].rfi,
                water = (short) json.curve[index].rwa,
                wind = (short) json.curve[index].rwi,
                thunder = (short) json.curve[index].rth,
                shine = (short) json.curve[index].rsh,
                dark = (short) json.curve[index].rda
              },
              enchant_assist = {
                poison = (short) json.curve[index].apo,
                paralyse = (short) json.curve[index].apa,
                stun = (short) json.curve[index].ast,
                sleep = (short) json.curve[index].asl,
                charm = (short) json.curve[index].ach,
                stone = (short) json.curve[index].asn,
                blind = (short) json.curve[index].abl,
                notskl = (short) json.curve[index].ans,
                notmov = (short) json.curve[index].anm,
                notatk = (short) json.curve[index].ana,
                zombie = (short) json.curve[index].azo,
                death = (short) json.curve[index].ade,
                knockback = (short) json.curve[index].akn,
                berserk = (short) json.curve[index].abe,
                resist_buff = (short) json.curve[index].abf,
                resist_debuff = (short) json.curve[index].adf,
                stop = (short) json.curve[index].acs,
                fast = (short) json.curve[index].acu,
                slow = (short) json.curve[index].acd,
                donsoku = (short) json.curve[index].ado,
                rage = (short) json.curve[index].ara,
                dec_ct = (short) json.curve[index].adc,
                inc_ct = (short) json.curve[index].aic
              },
              enchant_resist = {
                poison = (short) json.curve[index].rpo,
                paralyse = (short) json.curve[index].rpa,
                stun = (short) json.curve[index].rst,
                sleep = (short) json.curve[index].rsl,
                charm = (short) json.curve[index].rch,
                stone = (short) json.curve[index].rsn,
                blind = (short) json.curve[index].rbl,
                notskl = (short) json.curve[index].rns,
                notmov = (short) json.curve[index].rnm,
                notatk = (short) json.curve[index].rna,
                zombie = (short) json.curve[index].rzo,
                death = (short) json.curve[index].rde,
                knockback = (short) json.curve[index].rkn,
                berserk = (short) json.curve[index].rbe,
                resist_buff = (short) json.curve[index].rbf,
                resist_debuff = (short) json.curve[index].rdf,
                stop = (short) json.curve[index].rcs,
                fast = (short) json.curve[index].rcu,
                slow = (short) json.curve[index].rcd,
                donsoku = (short) json.curve[index].rdo,
                rage = (short) json.curve[index].rra,
                dec_ct = (short) json.curve[index].rdc,
                inc_ct = (short) json.curve[index].ric
              }
            }
          });
      }
      return true;
    }

    public int GetLevelCap()
    {
      return this.curve != null && this.curve.Count > 0 ? (int) this.curve[this.curve.Count - 1].lv : 0;
    }

    public void CalcLevelCurveStatus(
      int rank,
      ref BaseStatus result,
      UnitParam.Status ini_status,
      UnitParam.Status max_status)
    {
      int num1 = this.GetLevelCap() - 1;
      int num2 = rank - 1;
      result.bonus.Clear();
      result.enchant_assist.Clear();
      result.enchant_resist.Clear();
      result.element_assist.Clear();
      result.element_resist.Clear();
      ini_status.param.CopyTo(result.param);
      if (ini_status.enchant_resist != null)
        ini_status.enchant_resist.CopyTo(result.enchant_resist);
      if (num2 < 1 || num1 < 1)
        return;
      if (num2 >= num1)
      {
        max_status.param.CopyTo(result.param);
        if (max_status.enchant_resist == null)
          return;
        max_status.enchant_resist.CopyTo(result.enchant_resist);
      }
      else
      {
        BaseStatus baseStatus1 = new BaseStatus();
        BaseStatus baseStatus2 = new BaseStatus();
        for (int index1 = 0; index1 < this.curve.Count; ++index1)
        {
          long num3 = (long) ((int) this.curve[index1].lv - 1);
          for (int index2 = index1; index2 > 0; --index2)
            num3 -= (long) (int) this.curve[index2 - 1].lv;
          long num4 = (long) num2 >= num3 ? num3 : (long) num2;
          StatusParam statusParam1 = ini_status.param;
          StatusParam statusParam2 = max_status.param;
          StatusParam src = this.curve[index1].status.param;
          src.CopyTo(baseStatus1.param);
          baseStatus1.param.Sub(baseStatus2.param);
          baseStatus2.param.Add(src);
          for (int type1 = 0; type1 < baseStatus1.param.Length; ++type1)
          {
            long num5 = (long) (((int) statusParam2[(StatusTypes) type1] - (int) statusParam1[(StatusTypes) type1]) * (int) baseStatus1.param[(StatusTypes) type1] / 100);
            if (num5 != 0L)
            {
              StatusParam statusParam3;
              StatusTypes type2;
              (statusParam3 = result.param)[type2 = (StatusTypes) type1] = (OInt) ((int) statusParam3[type2] + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          if (ini_status.enchant_resist != null && max_status.enchant_resist != null)
          {
            EnchantParam enchantResist1 = ini_status.enchant_resist;
            EnchantParam enchantResist2 = max_status.enchant_resist;
            EnchantParam enchantResist3 = this.curve[index1].status.enchant_resist;
            enchantResist3.CopyTo(baseStatus1.enchant_resist);
            baseStatus1.enchant_resist.Sub(baseStatus2.enchant_resist);
            baseStatus2.enchant_resist.Add(enchantResist3);
            for (int index3 = 0; index3 < baseStatus1.enchant_resist.values.Length; ++index3)
            {
              long num6 = (long) (((int) enchantResist2.values[index3] - (int) enchantResist1.values[index3]) * (int) baseStatus1.enchant_resist.values[index3] / 100);
              if (num6 != 0L)
                result.enchant_resist.values[index3] += (short) (100000L * num6 / num3 * num4 / 100000L);
            }
          }
          if (rank <= (int) this.curve[index1].lv)
            break;
          num2 = rank - (int) this.curve[index1].lv - 1;
        }
      }
    }

    public int CalcLevelCurveValue(int rank, int ini, int max)
    {
      int num1 = this.GetLevelCap() - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = 0;
      int num4 = ini;
      for (int index1 = 0; index1 < this.curve.Count; ++index1)
      {
        long num5 = (long) ((int) this.curve[index1].lv - 1);
        for (int index2 = index1; index2 > 0; --index2)
          num5 -= (long) (int) this.curve[index2 - 1].lv;
        long num6 = (long) num2 >= num5 ? num5 : (long) num2;
        int num7 = (int) this.curve[index1].scale - num3;
        num3 += num7;
        long num8 = (long) ((max - ini) * num7 / 100);
        if (num8 != 0L)
          num4 += (int) (100000L * num8 / num5 * num6 / 100000L);
        if (rank > (int) this.curve[index1].lv)
          num2 = rank - (int) this.curve[index1].lv - 1;
        else
          break;
      }
      return num4;
    }
  }
}
