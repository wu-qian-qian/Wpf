using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace YuanShen_Demo.Local.ViewModels
{
    internal partial class CharacterBrowserViewModel:ViewModelBase
    {
        [ObservableProperty]
        BitmapImage duskImage;
        [ObservableProperty]
        BitmapImage dawnImage;

        [RelayCommand]
        async Task LoadCity(string id)
        {

        }
    }
}
