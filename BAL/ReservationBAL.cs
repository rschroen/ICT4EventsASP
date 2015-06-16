﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class ReservationBAL
    {
        public ReservationBAL()
        {

        }

        public int CreateReservation(string firstName, string insertion, string lastName, string street, string house_nr, string city, string iban)
        {
            return new ReservationDAL().Insert(firstName, insertion, lastName, street, house_nr, city, iban);
        }


    }
}
