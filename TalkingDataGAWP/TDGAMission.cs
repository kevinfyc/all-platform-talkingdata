namespace TalkingDataGAWP
{
    using System;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.controllers;
    using TalkingDataGAWP.Entity;

    public class TDGAMission
    {
        public static string sMissionId = "";

        public static void onBegin(string missionId)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAMission.onBegin()");
            }
            else
            {
                TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, string.Empty, 0L, EventMission.MissionStatus.START));
                TDGAPreference.setMissionID(missionId);
                TDGAAccount.sAccount.setGameDurationByMissionStart(missionId);
                sMissionId = missionId;
            }
        }

        public static void onCompleted(string missionId)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAMission.onCompleted()");
            }
            else
            {
                long num = (TDGAAccount.sAccount == null) ? 0L : TDGAAccount.sAccount.getMissionDuration(missionId);
                TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, string.Empty, num / 0x3e8L, EventMission.MissionStatus.COMPLETED));
                sMissionId = string.Empty;
                TDGAPreference.setMissionID(sMissionId);
            }
        }

        public static void onFailed(string missionId, string cause)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                Debugger.Log("SDK not initialized. TDGAMission.onFailed()");
            }
            else
            {
                long timeConsuming = (TDGAAccount.sAccount == null) ? 0L : TDGAAccount.sAccount.getMissionDuration(missionId);
                TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, cause, timeConsuming, EventMission.MissionStatus.FAILED));
                sMissionId = "";
                TDGAPreference.setMissionID(sMissionId);
            }
        }
    }
}

