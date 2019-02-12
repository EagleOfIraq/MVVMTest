using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace MVVMTest.Models
{
    public class TblPermission
    {
        public long Id { get; set; }
        public string PermissionName { get; set; }
        public bool IsActive { get; set; } = false;
        public List<TblRule> Rules { get; set; }

        public TblPermission save()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Permissions.Add(this);
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

        public TblPermission update()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Permissions.AddOrUpdate(this);
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
                    db.Permissions.Attach(this);
                    db.Permissions.Remove(this);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<TblPermission> getAll()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Permissions.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        public static TblPermission getByName(string name)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Permissions.FirstOrDefault(c => c.PermissionName.Equals(name));
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