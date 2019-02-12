using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MVVMTest.Models;

namespace MVVMTest.ViewModels
{
    class PermissionViewModel : Screen
    {
        private TblPermission permission=new TblPermission();
        private BindableCollection<TblPermission> permissions=new BindableCollection<TblPermission>(TblPermission.getAll());
        public TblPermission Permission
        {
            get => permission;
            set
            {
                permission = value;
                NotifyOfPropertyChange(()=> Permission);
            }
        }
        public BindableCollection<TblPermission> Permissions
        {
            get => permissions;
            set
            {
                permissions = value;
                NotifyOfPropertyChange(()=> Permissions);
            }
        }

        public void addPermission()
        {
            Permission.save();
            Permissions.Add(Permission);
            NotifyOfPropertyChange(() => Permissions);
            Permission = new TblPermission();
            NotifyOfPropertyChange(() => Permission);
        }

        public void updatePermission()
        {
            Permission.update();
            NotifyOfPropertyChange(() => Permission);
            NotifyOfPropertyChange(() => Permissions);          
        }

      
        public void removePermission()
        {
            Permission.delete();
            Permissions.Remove(Permission);
            Permission = new TblPermission();
            NotifyOfPropertyChange(() => Permission);
            NotifyOfPropertyChange(() => Permissions);
        }

    }
}
