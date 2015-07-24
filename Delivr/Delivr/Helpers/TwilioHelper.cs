using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace Delivr.Helpers
{
    public  class TwilioHelper
    {

        private static TwilioRestClient instance = null;
        private static readonly object padlock = new object();
        private const string AccountSid = "ACa4d3761c899b5f97155623fe1aadb057";
        private const string AuthToken = "2800ee2f850377e6daf342558b096ce5";
        

        
        public static TwilioRestClient getInstance()
        {
           
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new TwilioRestClient(AccountSid, AuthToken);
                }
                return instance;
            }
            
        }

        public void SendSMS(string sujet, string sendTo)
        {
            getInstance().SendMessage("+15146132984", sendTo, sujet);
        }
    }
}