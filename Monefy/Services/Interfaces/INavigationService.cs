using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services
{
    public interface INavigationService
    {
        public void NavigateTo<Ty>() where Ty : ViewModelBase;
    }
}
