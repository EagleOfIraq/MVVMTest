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
        public RuleViewModel()
        {
            var myItems = new List<string>();
            foreach (var permission in TblPermission.getAll())
            {
                myItems.Add(permission.PermissionName);
            }

            ComboItems = CollectionViewSource.GetDefaultView(myItems);
        }
        private BindableCollection<TblRule> rules = new BindableCollection<TblRule>(TblRule.getAll());
        private List<TblPermission> permissions;
        private TblPermission permission = new TblPermission();
        private PermissionsSetListener PermissionsSetListener;
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

       
        public TblPermission Permission
        {
            get => permission;
            set
            {
                permission = value;
                NotifyOfPropertyChange(() => Permission);
                NotifyOfPropertyChange(() => Rule);
            }
        }      


      
        public void addPermission()
        {
            Rule.Permissions.Add(Permission);
            NotifyOfPropertyChange(() => Rule);
        }

        public void removePermission()
        {
            Rule.Permissions.Remove(Permission);
            NotifyOfPropertyChange(() => Rule);
        }



        public ICollectionView ComboItems { get; set; }
        private string comboText;

        public string ComboText
        {
            get => comboText;
            set
            {
                comboText = value;
                ComboItems.Filter = item => item.ToString().ToLower().Contains(value.ToLower());

            }
        }


        public void changePermission()
        {
            var p = TblPermission.getByName(ComboText);
            Permission = p ?? Permission;
        }

        public void addPermissions()
        {
            Rule.Permissions.Add(Permission);

        }

        public void removePermissions()
        {
            MessageBox.Show("hi");
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
