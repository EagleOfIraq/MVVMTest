using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using MVVMTest.Models;

namespace MVVMTest.ViewModels
{
    public class PersonViewModel : Screen, MainDataChangeListener
    {
        private BindableCollection<Person> _persons = new BindableCollection<Person>();

        public PersonViewModel()
        {

            new Thread(() =>
            {
                Persons = new BindableCollection<Person>(Person.getAll());
                Wait = Visibility.Collapsed;
            }).Start();
        }

        private int _i;
        private Person _person = new Person();
        private Visibility _wait;

        public Visibility Wait
        {
            get => _wait;
            set
            {
                _wait = value;
                NotifyOfPropertyChange(() => Wait);
            }
        }

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                NotifyOfPropertyChange(() => Person);
            }
        }

        public BindableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                NotifyOfPropertyChange(() => Persons);
            }
        }

        public void showPerson()
        {
            Person.update();
            NotifyOfPropertyChange(() => Person);
            NotifyOfPropertyChange(() => Persons);
            MessageBox.Show(Person.ToString());
            DataChangeListener.onDataChanged();
        }

        public void addPerson()
        {
            //            if (!CanAdd())
            //            {
            //                MessageBox.Show("Error");
            //                return;
            //            }
            if (Person == null) return;

            Person.Id = ++_i;
            NotifyOfPropertyChange(() => Person);
            Person.save();
            Persons.Add(Person);
            Person = new Person();
            NotifyOfPropertyChange(() => Persons);
            DataChangeListener.onDataChanged();
        }

        public void clearPerson()
        {
            Person.delete();
            Persons.Remove(Person);
            Person = null;
            NotifyOfPropertyChange(() => Person);
            NotifyOfPropertyChange(() => Persons);
            DataChangeListener.onDataChanged();
        }

        public DataChangeListener DataChangeListener { get; set; }


        public void onMainDataChanged()
        {
            Persons = new BindableCollection<Person>(Person.getAll());
        }

        private bool CanAdd()
        {
            bool can = true;
            if (String.IsNullOrEmpty(Person.Name.Trim())) return false;

            return can;

        }
    }
    public interface DataChangeListener
    {
        void onDataChanged();
    }
}