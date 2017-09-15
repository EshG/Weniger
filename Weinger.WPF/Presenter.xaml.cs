using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weniger.UiServices;

namespace Weniger.WPF
{
    /// <summary>
    /// Interaction logic for Presenter.xaml
    /// </summary>
    public partial class Presenter : UserControl
    {
        IList<Weniger.UiServices.Augmentor> _Augmentors;

        public Presenter()
        {
            InitializeComponent();
        }



        public IList<Weniger.UiServices.Augmentor> Augmentors
        {
            get { return (IList<Weniger.UiServices.Augmentor>)GetValue(AugmentorsProperty); }
            set { SetValue(AugmentorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Augmentors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AugmentorsProperty =
            DependencyProperty.Register("Augmentors", typeof(IList<Weniger.UiServices.Augmentor>), typeof(Presenter), new PropertyMetadata(AugmentorsChanged));

        private static void AugmentorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Presenter presenter = (Presenter)d;
            presenter.Refresh();
        }


        public void Refresh()
        {
            foreach(Augmentor aug in Augmentors)
            {
                
            }
        }
    }
}
