namespace TalkingDataGAWP
{
    using System;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.controllers;
    using TalkingDataGAWP.Entity;

    public class TDGAItem
    {
        public static void onPurchase(string item, int itemNumber, double priceInVirtualCurrency)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeRequest()");
            }
            else
            {
                TDGAEventListManager.addEvent(new EventPurchase(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, priceInVirtualCurrency, item, itemNumber));
            }
        }

        public static void onUse(string item, int itemNumber)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeRequest()");
            }
            else
            {
                TDGAEventListManager.addEvent(new EventUse(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, item, itemNumber));
            }
        }
    }
}

