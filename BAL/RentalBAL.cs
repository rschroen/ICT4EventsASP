﻿// <copyright file="RentalBAL.cs" company="TomICT">
//      Copyright (c) ICT4Events. All rights reserved.
// </copyright>
// <author>Thom van Poppel</author>﻿
namespace BAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL;

    /// <summary>
    /// Class for validating Rental data and sending it to DAL
    /// </summary>
    public class RentalBAL
    {
        /// <summary>
        /// Method that loads an account by barcode
        /// </summary>
        /// <param name="barcode">The barcode</param>
        /// <returns>DataTable from account</returns>
        public DataTable GetAccountByBarcode(string barcode)
        {
            return new RentalDAL().LoadPerson(barcode);
        }

        /// <summary>
        /// Update presence of account
        /// </summary>
        /// <param name="personID">ID of the person</param>
        /// <param name="aanwezig">0 or 1 based on presence</param>
        /// <returns>0 or 1</returns>
        public int UpdatePresence(int reserveringPolsbandjeID, int aanwezig)
        {
            return new RentalDAL().UpdatePresence(reserveringPolsbandjeID, aanwezig);
        }

        /// <summary>
        /// Update one item to be rented out or vice versa;
        /// </summary>
        /// <param name="itemID">The id of the item</param>
        /// <param name="isVerhuurd">Rented out or not , 1 or 0.</param>
        /// <returns>1 or 0</returns>
        public int UpdateExemplaar(int itemID, int isVerhuurd)
        {
            return new RentalDAL().UpdateExemplaar(itemID, isVerhuurd);
        }

        /// <summary>
        /// Updates a product with new variables.
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <param name="naam">The name of the product</param>
        /// <param name="merk">The brand of the product</param>
        /// <param name="serie">The series of the product</param>
        /// <param name="prijs">The price of the product</param>
        /// <param name="aantal">The amount of the product</param>
        /// <returns>1 or 0</returns>
        public int UpdateProduct(int id, string naam, string merk, string serie, decimal prijs, int aantal)
        {
            int succes = 0;
            return succes;
        }

        /// <summary>
        /// Gets all persons based on presence
        /// </summary>
        /// <param name="aanwezig">0 or 1 based on presence</param>
        /// <returns>DataTable of persons</returns>
        public DataTable GetPersonByAanwezig(int aanwezig)
        {
            if (aanwezig == 1 || aanwezig == 0)
            {
                return new RentalDAL().LoadAllPersons(aanwezig);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method that loads an account by name
        /// </summary>
        /// <param name="name">Account name</param>
        /// <returns>DataTable with account</returns>
        public DataTable GetAccountByName(string name)
        {
            return new RentalDAL().LoadPersonByName(name);
        }

        /// <summary>
        /// Method that loads an account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>DataTable with account</returns>
        public DataTable GetAccountByID(int id)
        {
            return new RentalDAL().LoadPersonByID(id);
        }

        /// <summary>
        /// Get all available items
        /// </summary>
        /// <param name="availlable">0 or 1 based on availability</param>
        /// <returns>0 or 1</returns>
        public DataTable GetAllAvaillableItems(int availlable)
        {
            return new RentalDAL().LoadAllAvaillableItems(availlable);
        }

        /// <summary>
        /// Method for creating a new rental
        /// </summary>
        /// <param name="id">ID of product</param>
        /// <param name="barcode">The barcode</param>
        /// <param name="datumOut">End date</param>
        /// <returns>0 or 1</returns>
        public int CreateRental(int id, string barcode, string datumOut)
        {
            string dateFormat = "d-MM-yyyy HH:mm:ss";
            string now = DateTime.Now.ToString(dateFormat);
            int succes = 0;
            DataTable dt = new RentalDAL().LoadPerson(barcode);
            long pbID = 0;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    pbID = row.Field<long>(0);
                }
            }
            catch
            {
            }
            if (pbID == 0)
            {
                throw new Exception();
            }
            succes = new RentalDAL().CreateRental(pbID, id, now, datumOut);
            if(succes == 0)
            {
                throw new Exception();
            }
            succes = new RentalDAL().UpdateExemplaar(id, 1);
            return succes;
        }

        /// <summary>
        /// Method that loads all items
        /// </summary>
        /// <returns>DataTable of items</returns>
        public DataTable GetAllItems()
        {
            return new RentalDAL().LoadAllItems();
        }

        /// <summary>
        /// Method for creating a new Item
        /// </summary>
        /// <param name="naam">Name of item</param>
        /// <param name="merk">Item brand</param>
        /// <param name="serie">Item series</param>
        /// <param name="prijs">The price</param>
        /// <param name="aantal">The amount</param>
        /// <returns>0 or 1</returns>
        public int CreateItem(string naam, string merk, string serie, double prijs, int aantal)
        {
            int result = 0;
            int id = new RentalDAL().CreateCategory(naam);
            int typenummer = 0;
            int volgnummer = 0;
            if (id != 0)
            {
                typenummer = new RentalDAL().LoadTypenummer();
                id = new RentalDAL().CreateProduct(id, merk, serie, prijs, typenummer);
            }

            if (id != 0)
            {
                for (int i = 0; i < aantal; i++)
                {
                    volgnummer = new RentalDAL().LoadVolgnummer();
                    string barcode = typenummer.ToString() + "." + volgnummer.ToString();
                    result = new RentalDAL().CreateExemplaar(id, barcode, volgnummer);
                }
            }

            return result;
        }

        public int UpdateItem(int id, string naam, string merk, string serie, double prijs, int aantalOld, int aantalNew, int typenummer)
        {
            int succes =  new RentalDAL().UpdateItem(id,naam,merk,serie,prijs);
            if(aantalNew > aantalOld)
            {
                for (int i = 0; i < aantalNew - aantalOld; i++)
                {
                    int volgnummer = new RentalDAL().LoadVolgnummer();
                    string barcode = typenummer.ToString() + "." + volgnummer.ToString();
                    new RentalDAL().CreateExemplaar(id, barcode, volgnummer);
                }
            }
            return succes;
        }
        public DataTable LoadExemplaar(string barcode)
        {
            return new RentalDAL().LoadExemplaar(barcode);
        }

        public DataTable LoadRentalFromPerson(string name)
        {
            return new RentalDAL().LoadRentalFromPerson(name);
        }

        public int LoadItemStatus(int id)
        {
            return new RentalDAL().LoadItemStatus(id);
        }

        public int DeleteItem(int id)
        {
            return new RentalDAL().DeleteItem(id);
        }

        public List<int> GetAllItemsFromProduct(int id)
        {
            return new RentalDAL().GetAllItemsFromProduct(id);
        }

        public int DeleteProduct(int id)
        {
            return new RentalDAL().DeleteProduct(id);
        }

        public List<int> GetAllItemsFromVerhuur(int id)
        {
            return new RentalDAL().GetAllItemsFromVerhuur(id);
        }

        public int DeleteVerhuur(int id)
        {
            return new RentalDAL().DeleteVerhuur(id);
        }
    }
}
