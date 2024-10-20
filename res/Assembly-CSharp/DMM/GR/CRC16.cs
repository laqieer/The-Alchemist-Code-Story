﻿// Decompiled with JetBrains decompiler
// Type: GR.CRC16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Security.Cryptography;

#nullable disable
namespace GR
{
  public sealed class CRC16 : HashAlgorithm
  {
    private static readonly uint MASK_CRC16 = (uint) ushort.MaxValue;
    private uint mCrcValue = CRC16.MASK_CRC16;
    private static readonly uint[] TABLE_CRC16 = new uint[256]
    {
      0U,
      4489U,
      8978U,
      12955U,
      17956U,
      22445U,
      25910U,
      29887U,
      35912U,
      40385U,
      44890U,
      48851U,
      51820U,
      56293U,
      59774U,
      63735U,
      4225U,
      264U,
      13203U,
      8730U,
      22181U,
      18220U,
      30135U,
      25662U,
      40137U,
      36160U,
      49115U,
      44626U,
      56045U,
      52068U,
      63999U,
      59510U,
      8450U,
      12427U,
      528U,
      5017U,
      26406U,
      30383U,
      17460U,
      21949U,
      44362U,
      48323U,
      36440U,
      40913U,
      60270U,
      64231U,
      51324U,
      55797U,
      12675U,
      8202U,
      4753U,
      792U,
      30631U,
      26158U,
      21685U,
      17724U,
      48587U,
      44098U,
      40665U,
      36688U,
      64495U,
      60006U,
      55549U,
      51572U,
      16900U,
      21389U,
      24854U,
      28831U,
      1056U,
      5545U,
      10034U,
      14011U,
      52812U,
      57285U,
      60766U,
      64727U,
      34920U,
      39393U,
      43898U,
      47859U,
      21125U,
      17164U,
      29079U,
      24606U,
      5281U,
      1320U,
      14259U,
      9786U,
      57037U,
      53060U,
      64991U,
      60502U,
      39145U,
      35168U,
      48123U,
      43634U,
      25350U,
      29327U,
      16404U,
      20893U,
      9506U,
      13483U,
      1584U,
      6073U,
      61262U,
      65223U,
      52316U,
      56789U,
      43370U,
      47331U,
      35448U,
      39921U,
      29575U,
      25102U,
      20629U,
      16668U,
      13731U,
      9258U,
      5809U,
      1848U,
      65487U,
      60998U,
      56541U,
      52564U,
      47595U,
      43106U,
      39673U,
      35696U,
      33800U,
      38273U,
      42778U,
      46739U,
      49708U,
      54181U,
      57662U,
      61623U,
      2112U,
      6601U,
      11090U,
      15067U,
      20068U,
      24557U,
      28022U,
      31999U,
      38025U,
      34048U,
      47003U,
      42514U,
      53933U,
      49956U,
      61887U,
      57398U,
      6337U,
      2376U,
      15315U,
      10842U,
      24293U,
      20332U,
      32247U,
      27774U,
      42250U,
      46211U,
      34328U,
      38801U,
      58158U,
      62119U,
      49212U,
      53685U,
      10562U,
      14539U,
      2640U,
      7129U,
      28518U,
      32495U,
      19572U,
      24061U,
      46475U,
      41986U,
      38553U,
      34576U,
      62383U,
      57894U,
      53437U,
      49460U,
      14787U,
      10314U,
      6865U,
      2904U,
      32743U,
      28270U,
      23797U,
      19836U,
      50700U,
      55173U,
      58654U,
      62615U,
      32808U,
      37281U,
      41786U,
      45747U,
      19012U,
      23501U,
      26966U,
      30943U,
      3168U,
      7657U,
      12146U,
      16123U,
      54925U,
      50948U,
      62879U,
      58390U,
      37033U,
      33056U,
      46011U,
      41522U,
      23237U,
      19276U,
      31191U,
      26718U,
      7393U,
      3432U,
      16371U,
      11898U,
      59150U,
      63111U,
      50204U,
      54677U,
      41258U,
      45219U,
      33336U,
      37809U,
      27462U,
      31439U,
      18516U,
      23005U,
      11618U,
      15595U,
      3696U,
      8185U,
      63375U,
      58886U,
      54429U,
      50452U,
      45483U,
      40994U,
      37561U,
      33584U,
      31687U,
      27214U,
      22741U,
      18780U,
      15843U,
      11370U,
      7921U,
      3960U
    };

    public CRC16()
    {
      this.mCrcValue = CRC16.MASK_CRC16;
      this.HashSizeValue = 16;
    }

    public override void Initialize() => this.mCrcValue = CRC16.MASK_CRC16;

    protected override void HashCore(byte[] bytes, int start, int size)
    {
      while (--size >= 0)
        this.mCrcValue = CRC16.TABLE_CRC16[(IntPtr) (uint) (((int) this.mCrcValue ^ (int) bytes[start++]) & (int) byte.MaxValue)] ^ this.mCrcValue >> 8;
    }

    protected override byte[] HashFinal()
    {
      this.HashValue = new byte[2]
      {
        (byte) (this.mCrcValue >> 8 & (uint) byte.MaxValue),
        (byte) (this.mCrcValue & (uint) byte.MaxValue)
      };
      return this.HashValue;
    }
  }
}