using System.Collections.Generic;
using System.Windows;
using Weniger.UiServices.Augmentors;
namespace Host.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            presenter.Augmentors = GetAugmentors();
        }

        private List<Augmentor> GetAugmentors()
        {
            return new List<Augmentor>() {new Augmentors.CustomerCardAugmentor(336) };
        }
    }
}
