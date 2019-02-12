using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace MVVMTest.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool InRelation { get; set; }

        public Person save()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Persons.Add(this);
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

        public Person update()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Persons.AddOrUpdate(this);
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
                    db.Persons.Attach(this);
                    db.Persons.Remove(this);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<Person> getAll()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Persons.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
        public static Person getById(long Id)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Persons.FirstOrDefault(p => p.Id == Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
        public static List<Person> Search(string key)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    return db.Persons.Where(p => p.Name.Contains(key) || p.Gender.Contains( key)).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        public override string ToString()
        {
            return "Id : " + this.Id +
                   "Name : " + this.Name +
                   "Age : " + this.Age +
                   "Gender : " + this.Gender +
                   "InRelation : " + this.InRelation;
        }
    }
}