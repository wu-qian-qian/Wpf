using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using YuanShen_Demo.Local.Model;

namespace YuanShen_Demo.Local.ViewModels
{
    internal partial class CharacterBrowserViewModel:ViewModelBase
    {
        [ObservableProperty]
        ObservableCollection<Character> charList = new();

        [ObservableProperty]
        Character selectedItem;
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
