// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReadMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Mail/ReadMail", 32741)]
  [FlowNode.Pin(10, "一件既読", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "全件既読", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "一件受け取り完了", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "全件受け取り完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "いくつか受け取れなかった", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(23, "何も受け取れなかった", FlowNode.PinTypes.Output, 23)]
  public class FlowNode_ReadMail : FlowNode_Network
  {
    private FlowNode_ReadMail.RecieveStatus mRecieveStatus;
    private Dictionary<GiftTypes, int> currentNums;

    private int CalcConvertedGold(MailData mail)
    {
      int num1 = 0;
      if (mail == null)
        return num1;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < mail.gifts.Length; ++index)
      {
        if (!string.IsNullOrEmpty(mail.gifts[index].iname))
        {
          ItemParam itemParam = instance.GetItemParam(mail.gifts[index].iname);
          if (itemParam != null)
          {
            ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemParam.iname);
            if (itemDataByItemId != null)
            {
              int num2 = itemDataByItemId.Num + mail.gifts[index].num - itemParam.cap;
              if (num2 > 0)
                num1 += num2 * itemParam.sell;
            }
          }
        }
      }
      return num1;
    }

    private void OnOverItemAmount(long mailid, int period)
    {
      this.OnOverItemAmount(new long[1]{ mailid }, new int[1]
      {
        period
      });
    }

    private void OnOverItemAmount(long[] mailids, int[] periods)
    {
      UIUtility.ConfirmBox(LocalizedText.Get("sys.MAILBOX_ITEM_OVER_MSG"), (string) null, (UIUtility.DialogResultEvent) (go =>
      {
        this.ExecRequest((WebAPI) new ReqMailRead(mailids, periods, 0, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }), (UIUtility.DialogResultEvent) (go => this.enabled = false), (GameObject) null, false, -1);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (this.enabled)
            break;
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(20);
            this.enabled = false;
            break;
          }
          long mailid = (long) GlobalVars.SelectedMailUniqueID;
          int selectedMailPeriod = (int) GlobalVars.SelectedMailPeriod;
          MailData mailData1 = MonoSingleton<GameManager>.Instance.Player.Mails.Find((Predicate<MailData>) (m => m.mid == mailid));
          this.RefreshCurrentNums();
          if (!this.CheckRecievable(mailData1))
          {
            this.ActivateOutputLinks(23);
            this.enabled = false;
            break;
          }
          this.mRecieveStatus = FlowNode_ReadMail.RecieveStatus.Recieve;
          this.ExecRequest((WebAPI) new ReqMailRead(new long[1]
          {
            mailid
          }, new int[1]{ selectedMailPeriod }, 0, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
          break;
        case 11:
          if (this.enabled)
            break;
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(21);
            this.enabled = false;
            break;
          }
          List<MailData> mails = MonoSingleton<GameManager>.Instance.Player.Mails;
          List<MailData> mailDataList1 = new List<MailData>();
          List<MailData> mailDataList2 = new List<MailData>();
          for (int index = mails.Count - 1; index >= 0; --index)
          {
            if (mails[index] != null && mails[index].read <= 0L)
              mailDataList1.Add(mails[index]);
          }
          if (mailDataList1.Count < 1)
          {
            this.ActivateOutputLinks(21);
            this.enabled = false;
            break;
          }
          this.RefreshCurrentNums();
          foreach (MailData mailData2 in mailDataList1)
          {
            if (this.CheckRecievable(mailData2))
            {
              mailDataList2.Add(mailData2);
              this.AddCurrentNum(mailData2);
              if (mailDataList2.Count >= 10)
                break;
            }
          }
          if (mailDataList2.Count < 1)
          {
            this.ActivateOutputLinks(23);
            this.enabled = false;
            break;
          }
          this.mRecieveStatus = mailDataList2.Count >= 10 || mailDataList2.Count >= mailDataList1.Count ? FlowNode_ReadMail.RecieveStatus.Recieve : FlowNode_ReadMail.RecieveStatus.NotRecieveSome;
          long[] mailids = new long[mailDataList2.Count];
          int[] periods = new int[mailDataList2.Count];
          for (int index = 0; index < mailDataList2.Count; ++index)
          {
            mailids[index] = mailDataList2[index].mid;
            periods[index] = mailDataList2[index].period;
          }
          this.ExecRequest((WebAPI) new ReqMailRead(mailids, periods, 0, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
          break;
      }
    }

    private void RefreshCurrentNums()
    {
      this.currentNums = new Dictionary<GiftTypes, int>();
      this.currentNums.Add(GiftTypes.Artifact, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
    }

    private void AddCurrentNum(MailData mailData)
    {
      if (mailData.gifts == null)
        return;
      foreach (GiftData gift in mailData.gifts)
      {
        if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
        {
          Dictionary<GiftTypes, int> currentNums;
          (currentNums = this.currentNums)[GiftTypes.Artifact] = currentNums[GiftTypes.Artifact] + gift.num;
        }
      }
    }

    private bool CheckRecievable(MailData mailData)
    {
      int num = 0;
      if (mailData.gifts != null)
      {
        foreach (GiftData gift in mailData.gifts)
        {
          if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact) && gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
            num += this.currentNums[GiftTypes.Artifact] + gift.num;
        }
        if (num >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
          return false;
      }
      return true;
    }

    private void Success()
    {
      if (this.mRecieveStatus == FlowNode_ReadMail.RecieveStatus.Recieve)
        this.ActivateOutputLinks(20);
      else if (this.mRecieveStatus == FlowNode_ReadMail.RecieveStatus.NotRecieveAll)
      {
        this.ActivateOutputLinks(23);
      }
      else
      {
        if (this.mRecieveStatus != FlowNode_ReadMail.RecieveStatus.NotRecieveSome)
          return;
        this.ActivateOutputLinks(22);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoMail:
            this.OnBack();
            break;
          case Network.EErrCode.MailReadable:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.mails))
            {
              this.OnRetry();
              return;
            }
            if (jsonObject.body.artifacts != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, false);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.enabled = false;
          this.Success();
        }
      }
    }

    private enum RecieveStatus
    {
      Recieve,
      NotRecieveAll,
      NotRecieveSome,
    }
  }
}
