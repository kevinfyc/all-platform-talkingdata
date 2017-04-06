namespace TalkingDataGAWP
{
    using System;
    using System.Collections.Generic;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.controllers;
    using TalkingDataGAWP.Entity;

    public class TDGAVirtualCurrency
    {
        private static Dictionary<string, EventCharge> sCache = new Dictionary<string, EventCharge>();

        public static void onChargeRequest(string orderid, string iapid, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeRequest()");
            }
            else if (!sCache.ContainsKey(orderid))
            {
                EventCharge mevent = new EventCharge(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, orderid, iapid, currencyAmount, currencyType, virtualCurrencyAmount, paymentType, EventCharge.ChargeStatus.REQUEST);
                TDGAEventListManager.addEvent(mevent, true);
                Type type = typeof(TDGAVirtualCurrency);
                lock (type)
                {
                    sCache.Add(orderid, mevent);
                }
            }
        }

        public static void onChargeSuccess(string orderId)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeSuccess()");
            }
            else
            {
                EventCharge charge = null;
                Type type = typeof(TDGAVirtualCurrency);
                lock (type)
                {
                    sCache.TryGetValue(orderId, out charge);
                    sCache.Remove(orderId);
                }
                if (charge == null)
                {
                    charge = new EventCharge(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, orderId, string.Empty, 0.0, string.Empty, 0.0, string.Empty, EventCharge.ChargeStatus.SUCCESS);
                }
                TDGAEventListManager.addEvent(charge, true);
            }
        }

        public static void onReward(double currencyAmount, string reason)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onReward()");
            }
            else
            {
                TDGAEventListManager.addEvent(new EventReward(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, currencyAmount, reason), true);
            }
        }
    }
}

