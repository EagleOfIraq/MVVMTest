using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using AutoMapper;

namespace MVVMTest.Models
{
    public class TblRule
    {
        public long Id { get; set; }
        public string RuleName { get; set; }
        public string RuleNote { get; set; }
        public bool IsActive { get; set; }
        public List<TblUser> Users { get; set; }
        public List<TblPermission> Permissions { get; set; } = new List<TblPermission>();


        public TblRule save()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Rules.Add(this);
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

        public TblRule update()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    if (this.Permissions != null)
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM TblRuleTblPermissions WHERE TblRule_Id=" + this.Id);
                        db.SaveChanges();
                    }

                    foreach (var Permission in this.Permissions)
                    {
                        var q = "INSERT INTO TblRuleTblPermissions  (TblRule_Id ,TblPermission_Id) VALUES(@TblRule_Id,@TblPermission_Id)";
                        db.Database.ExecuteSqlCommand(q,
                            new SqlParameter("@TblRule_Id", this.Id),
                            new SqlParameter("@TblPermission_Id", Permission.Id)
                         );
                        db.SaveChanges();
                    }
                    //                    var r = db.Rules.SingleOrDefault(x => x.Id == this.Id);
                    //                    r.Permissions = this.Permissions;
                    //                    db.Rules.AddOrUpdate(this);
                    //                    db.Entry(this).State = EntityState.Modified;
                    return this;
                }
                catch (Exception ex)
                {
//                    MessageBox.Show(ex.Message);
                    return this;
                }
            }
        }

        public void delete()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Rules.Attach(this);
                    db.Rules.Remove(this);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<TblRule> getAll()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Rules
                        .Include(b => b.Permissions)
                        .ToList();
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