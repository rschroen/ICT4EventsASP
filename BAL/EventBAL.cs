﻿namespace BAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL;

    public class EventBAL
    {

        public DataTable GetEvent(string eventName)
        {
            return new EventDAL().Load(eventName);
        }

        public DataTable GetAllEvents()
        {
            return new EventDAL().Load();
        }
    }
}
