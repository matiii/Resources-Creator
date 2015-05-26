using ResourcesCreator.Core;
using System;

namespace ResourcesCreator.Pages.Choose
{
    class Messages
    {
        private readonly FromFileViewModel data;
        private readonly LogViewModel logs;



        public Messages(FromFileViewModel data, LogViewModel logs)
        {
            this.data = data;
            this.logs = logs;
        }


        public void AddLog(string msg)
        {
            data.Log = msg;
            logs.Add(DateTime.Now, msg);
        }
    }
}
