﻿using System;

namespace Blockchain.POC.Entities
{
    public class Account
    {
        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public DateTime CreationDate { get; set; }

        public string Balance { get; set; }

        public Account()
        {
        }

        public Account(string username, string password, string firstname, string lastname, DateTime dateOfBirth)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Username = username;
            this.Address = (this.Username + this.FirstName + this.LastName + dateOfBirth.ToString()).GetHash();
            this.CreationDate = DateTime.Now;
            GetHashedPassword(password);
        }

        public void GetHashedPassword(string password)
        {
            this.HashedPassword = password.GetHash();
        }
    }
}