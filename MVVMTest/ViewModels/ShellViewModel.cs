using System.Collections.Generic;
using Caliburn.Micro;

namespace MVVMTest.ViewModels
{
    public class ShellViewModel : Conductor<object>, DataChangeListener
    {
        private string _name = "hi";
        readonly IWindowManager manager = new WindowManager();

        public string name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => name);
            }
        }

        public void onDataChanged()
        {
            foreach (var mainDataChangeListener in MainDataChangeListeners)
            {
                mainDataChangeListener.onMainDataChanged();
            }
        }

        public void SayHello()
        {
            //            using (var context = new TestDBContext())
            //            {
            //                var customers = context.Persons.ToList();
            //                foreach (var cust in customers)
            //                {
            //                    MessageBox.Show(cust.Name);
            //                }
            //            }

            NotifyOfPropertyChange(() => name);
            var personViewModel = new PersonViewModel
            {
                DataChangeListener = this
            };
            addMainDataChangeListener(personViewModel);
            ActivateItem(personViewModel);
//            manager.ShowWindow(personViewModel);
        }

        public void OpenTest()
        {
            ActivateItem(new RuleViewModel());
//            manager.ShowWindow(new UserViewModel());
//            manager.ShowWindow(new RuleViewModel());
//            manager.ShowWindow(new PermissionViewModel());
          
        }

        private List<MainDataChangeListener> MainDataChangeListeners = new List<MainDataChangeListener>();

        public void addMainDataChangeListener(MainDataChangeListener MainDataChangeListener)
        {
            this.MainDataChangeListeners.Add(MainDataChangeListener);
        }


    }

    public interface MainDataChangeListener
    {
        void onMainDataChanged();
    }
}