using System.Windows;
using YuanShen_Demo.View;

namespace YuanShen_Demo
{
  public class App:Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CharacterBrowser character = new();
            character.Show();
        }
    }
}
