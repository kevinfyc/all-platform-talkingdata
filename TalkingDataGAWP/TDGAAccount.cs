namespace TalkingDataGAWP
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.controllers;
    using TalkingDataGAWP.Entity;

    public class TDGAAccount
    {
        private string _accountName;
        private AccountType _accountType;
        private int _age;
        private string _gameServer;
        private Gender _gender;
        private int _level;
        [CompilerGenerated]
        private string _accountID;
        private static string ACCOUNT_FILE = "account_file";
        private static string ACCOUNT_AGE = "age";
        private static string ACCOUNT_GENDER = "gender";
        private static string ACCOUNT_ID = "accountId";
        private static string ACCOUNT_LEVEL = "userLevel";
        private static string ACCOUNT_NAME = "accountName";
        private static string ACCOUNT_TYPE = "accountType";
        private static string GAME_DURATION = "game_duration";
        private static string LEVEL_UP_DURATION = "levelup_duration";
        private long mGameDuration;
        private long mGameDurationStart;
        private static string MIESSION_DURATION = "mission_duration";
        private static string PREF_ACCOUNT_FILE_NAME = "account_file";
        internal static TDGAAccount sAccount;

        static private TVR.SaveTool m_saveTool = null;
        static public TVR.SaveTool saveTool
        {
            get
            {
                if (m_saveTool == null)
                {
                    m_saveTool = new TVR.SaveTool();

                    m_saveTool.Initialize();
                    m_saveTool.SetPath(TalkingDataGA.idf.account_path, ACCOUNT_FILE);
                }
                return m_saveTool;
            }
        }

        private static Dictionary<string, string> getAccountPreference(string accountid, string server)
        {
            TVR.JsonDictionary<string, string> ret;

            if (saveTool.ReadArchive<TVR.JsonDictionary<string, string>>(accountid + "_" + PREF_ACCOUNT_FILE_NAME, out ret))
            {
                return ret.value;
            }

            return new Dictionary<string,string>();
        }

        internal long getCurrentDuration()
        {
            return ((this.mGameDuration + DateTimeUtils.getCurrentTime()) - this.mGameDurationStart);
        }

        public long getLevelUpDuration()
        {
            Dictionary<string, string> dic = getAccountPreference(this.accountID, this.gameServer);
            long num = !dic.ContainsKey(LEVEL_UP_DURATION) ? 0L : long.Parse(dic[LEVEL_UP_DURATION]);
            long num2 = this.getCurrentDuration();
            this.setKeyValue(LEVEL_UP_DURATION, num2 + string.Empty, dic);
            return (num2 - num);
        }

        public long getMissionDuration(string missionId)
        {
            Dictionary<string, string> dictionary = getAccountPreference(this.accountID, this.gameServer);
            long num = !dictionary.ContainsKey(MIESSION_DURATION + "_" + missionId) ? 0L : long.Parse(dictionary[MIESSION_DURATION + "_" + missionId]);
            return (this.getCurrentDuration() - num);
        }

        internal static TDGAAccount getTDGAccount()
        {
            string accountId = TDGAPreference.getAccountID();
            return getTDGAccount(accountId, TDGAPreference.getAccountGameServer(accountId));
        }

        internal static TDGAAccount getTDGAccount(string accountId, string gameServer)
        {
            Dictionary<string, string> dictionary = getAccountPreference(accountId, gameServer);
            TDGAAccount account = new TDGAAccount();
            account.accountID = accountId;
            account._gameServer = gameServer;
            if (dictionary != null)
            {
                account._accountType = !dictionary.ContainsKey(ACCOUNT_TYPE) ? AccountType.ANONYMOUS : ((AccountType) int.Parse(dictionary[ACCOUNT_TYPE]));
                account._accountName = !dictionary.ContainsKey(ACCOUNT_NAME) ? string.Empty : dictionary[ACCOUNT_NAME];
                account._level = !dictionary.ContainsKey(ACCOUNT_LEVEL) ? 0 : int.Parse(dictionary[ACCOUNT_LEVEL]);
                account._age = !dictionary.ContainsKey(ACCOUNT_AGE) ? 0 : int.Parse(dictionary[ACCOUNT_AGE]);
                account._gender = !dictionary.ContainsKey(ACCOUNT_GENDER) ? Gender.UNKNOW : ((Gender) int.Parse(dictionary[ACCOUNT_GENDER]));
                account.mGameDuration = !dictionary.ContainsKey(GAME_DURATION) ? 0L : long.Parse(dictionary[GAME_DURATION]);
                account.setDurationStartTime();
                saveTDGAAccount(account);
            }
            return account;
        }


        private void saveAccountPreference(Dictionary<string, string> dic)
        {
            saveTool.WriteArchive(dic[ACCOUNT_ID] + "_" + PREF_ACCOUNT_FILE_NAME, new TVR.JsonDictionary<string, string>(dic));
        }

        public static void saveTDGAAccount(TDGAAccount account)
        {
            Dictionary<string, string> dic = getAccountPreference(account.accountID, account.gameServer);
            account.setKeyValueNotSave(ACCOUNT_ID, account.accountID, dic);
            account.setKeyValueNotSave(ACCOUNT_TYPE, ((int) account.accountType) + string.Empty, dic);
            account.setKeyValueNotSave(ACCOUNT_NAME, account.accountName, dic);
            account.setKeyValueNotSave(ACCOUNT_LEVEL, account.level + string.Empty, dic);
            account.setKeyValueNotSave(ACCOUNT_AGE, account.age + string.Empty, dic);
            account.setKeyValueNotSave(ACCOUNT_GENDER, ((int) account.gender) + string.Empty, dic);
            account.saveAccountPreference(dic);
        }

        public static TDGAAccount setAccount(string accountid)
        {
            if (!TalkingDataGA.isAlreadyInit)
            {
                return null;
            }
            if ((accountid == null) || accountid.Trim().Equals(string.Empty))
            {
                return null;
            }
            TDGAAccount current = null;
            if (sAccount.accountID.Equals(string.Empty))
            {
                sAccount.accountID = accountid;
                current = sAccount;
            }
            else if (sAccount.accountID.Equals(accountid))
            {
                current = sAccount;
            }
            else
            {
                string gameServer = TDGAPreference.getAccountGameServer(accountid);
                current = getTDGAccount(accountid, gameServer);
                sAccount.updateGameDuration();
                TDGASessionController.changeAccount(sAccount, current);
                sAccount = current;
            }
            saveTDGAAccount(current);
            TDGAPreference.setAccountID(accountid);
            return current;
        }

        public void setAccountName(string accountName)
        {
            this._accountName = accountName;
            this.updateAccountEvent();
        }

        public void setAccountType(AccountType type)
        {
            this._accountType = type;
            this.updateAccountEvent();
        }

        public void setAge(int age)
        {
            this._age = age;
            this.updateAccountEvent();
        }

        internal void setDurationStartTime()
        {
            this.mGameDurationStart = DateTimeUtils.getCurrentTime();
        }

        public void setGameDurationByMissionStart(string missionId)
        {
            long num = this.getCurrentDuration();
            Dictionary<string, string> dic = getAccountPreference(this.accountID, this.gameServer);
            this.setKeyValue(MIESSION_DURATION + "_" + missionId, num + string.Empty, dic);
        }

        public void setGameServer(string gameServer)
        {
            this._gameServer = gameServer;
            this.updateGameServer();
        }

        public void setGender(Gender gender)
        {
            this._gender = gender;
            this.updateAccountEvent();
        }

        private void setKeyValue(string key, string value, Dictionary<string, string> dic)
        {
            this.setKeyValueNotSave(key, value, dic);
            this.saveAccountPreference(dic);
        }

        private void setKeyValueNotSave(string key, string value, Dictionary<string, string> dic)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public void setLevel(int level)
        {
            if (level != this._level)
            {
                int last = this._level;
                this._level = level;
                this.updateAccountLevel(last, level);
            }
        }

        private void updateAccountEvent()
        {
            saveTDGAAccount(this);
            TDGAEventListManager.addEvent(new EventUpdateAccount(TDGASessionController.sGameSessionId, this), true);
        }

        private void updateAccountLevel(int last, int current)
        {
            saveTDGAAccount(this);
            long timeConsuming = this.getLevelUpDuration();
            TDGAEventListManager.addEvent(new EventLevelUp(TDGASessionController.sGameSessionId, this, TDGAMission.sMissionId, last, timeConsuming));
        }

        public void updateGameDuration()
        {
            Dictionary<string, string> dic = getAccountPreference(this.accountID, this.gameServer);
            this.setKeyValue(GAME_DURATION, this.getCurrentDuration() + string.Empty, dic);
            this.setDurationStartTime();
        }

        private void updateGameServer()
        {
            TDGAPreference.setAccountGameServer(this.accountID, this.gameServer);
            saveTDGAAccount(this);
            TDGASessionController.restartSession(getTDGAccount(this.accountID, TDGAPreference.getAccountGameServer(this.accountID)), this);
            TDGAEventListManager.addEvent(new EventUpdateAccount(TDGASessionController.sGameSessionId, this));
        }

        public string accountID
        {
            [CompilerGenerated]
            get
            {
                return this._accountID;
            }
            [CompilerGenerated]
            internal set
            {
                this._accountID = value;
            }
        }

        public string accountName
        {
            get
            {
                return this._accountName;
            }
        }

        public AccountType accountType
        {
            get
            {
                return this._accountType;
            }
        }

        public int age
        {
            get
            {
                return this._age;
            }
        }

        public string gameServer
        {
            get
            {
                return this._gameServer;
            }
        }

        public Gender gender
        {
            get
            {
                return this._gender;
            }
        }

        public int level
        {
            get
            {
                return this._level;
            }
        }

        public enum AccountType
        {
            ANONYMOUS = 0,
            ND91 = 5,
            QQ = 3,
            QQ_WEIBO = 4,
            REGISTERED = 1,
            SINA_WEIBO = 2,
            TYPE1 = 11,
            TYPE10 = 20,
            TYPE2 = 12,
            TYPE3 = 13,
            TYPE4 = 14,
            TYPE5 = 15,
            TYPE6 = 0x10,
            TYPE7 = 0x11,
            TYPE8 = 0x12,
            TYPE9 = 0x13
        }

        public enum Gender
        {
            UNKNOW,
            MALE,
            FEMALE
        }
    }
}

