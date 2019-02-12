using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Caliburn.Micro;
using MVVMTest.Models;

namespace MVVMTest.ViewModels
{
    class RulePermissionsViewModel : Screen
    {
        private List<TblPermission> permissions;
        private TblPermission permission = new TblPermission();
        private TblRule rule = new TblRule();
        private PermissionsSetListener PermissionsSetListener;
        public RulePermissionsViewModel(TblRule rule, PermissionsSetListener PermissionsSetListener)
        {
            this.PermissionsSetListener = PermissionsSetListener;
            Rule = rule;
            permissions = Rule.Permissions;
            var myItems = new List<string>();
            foreach (var permission in TblPermission.getAll())
            {
                myItems.Add(permission.PermissionName);
            }

            ComboItems = CollectionViewSource.GetDefaultView(myItems);
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
        public List<TblPermission> Permissions
        {
            get => permissions;
            set
            {
                permissions = value;
                NotifyOfPropertyChange(() => Permissions);
                NotifyOfPropertyChange(() => Rule);
            }
        }


        public TblRule Rule
        {
            get => rule;
            set
            {
                rule = value;
                NotifyOfPropertyChange(() => Rule);
            }
        }
        public void addPermission()
        {
            Permissions.Add(Permission);
            NotifyOfPropertyChange(() => Permissions);
            NotifyOfPropertyChange(() => Rule);
        }

        public void removePermission()
        {
            Permissions.Remove(Permission);
            NotifyOfPropertyChange(() => Permissions);
            NotifyOfPropertyChange(() => Rule);
        }

        public void setPermissions()
        {
            PermissionsSetListener.onPermissionsSet(Permissions);
            this.TryClose();

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
    }

    public interface PermissionsSetListener
    {
        void onPermissionsSet(List<TblPermission> permissions);
    }
}
