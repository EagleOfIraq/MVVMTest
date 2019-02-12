using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace MVVMTest.Models
{
    public class TblUser
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public List<TblRule> Rules { get; set; }

        public TblUser save()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Users.Add(this);
                    db.SaveChanges();
                    return this;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        public TblUser update()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Users.AddOrUpdate(this);
                    db.SaveChanges();

                    return this;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        public void delete()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Users.Attach(this);
                    db.Users.Remove(this);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<TblUser> getAll()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

    }
}