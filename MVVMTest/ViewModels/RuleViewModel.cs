using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Caliburn.Micro;
using MVVMTest.Models;

namespace MVVMTest.ViewModels
{
    class RuleViewModel : Screen, PermissionsSetListener
    {         
        private BindableCollection<TblRule> rules = new BindableCollection<TblRule>(TblRule.getAll());

        private TblRule rule = new TblRule();

        public TblRule Rule
        {
            get => rule;
            set
            {
                rule = value;
                NotifyOfPropertyChange(() => Rule);
            }
        }

        public BindableCollection<TblRule> Rules
        {
            get => rules;
            set
            {
                rules = value;
                NotifyOfPropertyChange(() => Rules);
            }
        }

        public void addRule()
        {
            Rule.save();
            Rules.Add(Rule);
            NotifyOfPropertyChange(() => Rules);
            Rule = new TblRule();
            NotifyOfPropertyChange(() => Rule);
        }

        public void updateRule()
        {
            Rule.update();
            NotifyOfPropertyChange(() => Rule);
            NotifyOfPropertyChange(() => Rules);
        }


        public void removeRule()
        {
            Rule.delete();
            Rules.Remove(Rule);
            Rule = new TblRule();
            NotifyOfPropertyChange(() => Rule);
            NotifyOfPropertyChange(() => Rules);
        }

        public void editRulePermissions()
        {
            var manager = new WindowManager();
            manager.ShowWindow(new RulePermissionsViewModel(Rule, this));
        }

        public void onPermissionsSet(List<TblPermission> permissions)
        {
            Rule.Permissions = permissions;
        }
    }
}
