﻿// <copyright file="PlaatsReservering.aspx.cs" company="RuudIT">
//      Copyright (c) ICT4Events. All rights reserved.
// </copyright>
// <author>Ruud Schroën</author>
namespace ICT4Events.Reservering
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BAL;

    /// <summary>
    /// WebForm for placing a new reservation
    /// </summary>
    public partial class PlaatsReservering : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new ReservationBAL().LoadWristbands().Rows.Count == 0)
            {
                Debug.WriteLine("INSERT POLSBANDJES");
                new MailBAL(false).SetBarcodes();
            }
            if (this.Session["USER_ID"] == null)
            {
                Response.Redirect("../Default.aspx");
            }
        }

        /// <summary>
        /// Event that gets fired whenever a date is selected in calBeginDate
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CalBeginData_SelectionChanged(object sender, EventArgs e)
        {
            this.cusValBeginDate.Validate();
        }

        /// <summary>
        /// Validator for calBeginDate that checks if the selected date if later than today
        /// </summary>
        /// <param name="source">The source of the Event.</param>
        /// <param name="args">The <see cref="System.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CusValBeginDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.calBeginData.SelectedDate <= DateTime.Now)
            {
                args.IsValid = false;
            }
        }

        /// <summary>
        /// Event that gets fired whenever a date is selected in calEndData
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CalEndDate_SelectionChanged(object sender, EventArgs e)
        {
            this.cusValEndDate.Validate();
        }

        /// <summary>
        /// Validator for calBeginDate that checks if the selected date if later than the begin date
        /// </summary>
        /// <param name="source">The source of the Event.</param>
        /// <param name="args">The <see cref="System.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CusValEndDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.calEndDate.SelectedDate <= this.calBeginData.SelectedDate)
            {
                args.IsValid = false;
            }
        }

        /// <summary>
        /// Click event for Button1
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) 
            { 
                return; 
            }

            string[] usernames;

            if (string.IsNullOrWhiteSpace(this.tbOtherPersons.Text))
            {
                usernames = new string[]{Session["USER_ID"].ToString()};
            }
            else
            {
                usernames = (this.tbOtherPersons.Text + "," + Session["USER_ID"].ToString()).Split(',').Select(sValue => sValue.Trim()).ToArray();
            }

            ReservationBAL rbal = new ReservationBAL();
            MailBAL mbal = new MailBAL(false);

            string insertion = this.tbMiddleName.Text;
            if (string.IsNullOrEmpty(insertion))
            {
                insertion = string.Empty;
            }

            int reservationID = rbal.CreateReservation(
                this.tbFirstName.Text,
                this.tbMiddleName.Text,
                this.tbLastName.Text,
                this.tbStreet.Text,
                this.tbHouseNr.Text,
                this.tbCity.Text,
                this.tbBankAccount.Text,
                this.calBeginData.SelectedDate.Date,
                this.calEndDate.SelectedDate.Date,
                Convert.ToInt32(this.ddPlace.SelectedValue));

            mbal.SendMail(null, usernames, reservationID);
            if (reservationID > 0)
            {
                Debug.WriteLine("Reservering aangemaakt: " + reservationID);
                Response.Redirect("../Default.aspx", false);
            }
        }
    }
}