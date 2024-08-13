using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropFile.Local.ViewModels
{
   public partial class MainViewModel:ViewModelBase      
    {
        [ObservableProperty]
        public string[] fileData;

        [ObservableProperty]
        public string desData;

        public MainViewModel()
        {
            DesData = "请拖入文件";
        }

        [RelayCommand]
        void Test()
        {
            Console.WriteLine(1);
        }
    }
}
