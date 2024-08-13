
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DropFile.Local.UI
{
    class DropFile : Behavior<FrameworkElement>
    {
        


        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                DropFileBehavior? control = this.GetParent<DropFileBehavior>();
                if(control!=null)
                  control.Data = files;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Drop -= AssociatedObject_Drop;
        }
    }
}
