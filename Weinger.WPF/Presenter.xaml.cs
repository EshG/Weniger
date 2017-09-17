using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF
{
    /// <summary>
    /// Interaction logic for Presenter.xaml
    /// </summary>
    public partial class Presenter : UserControl
    {
        ParserContext _parserContext;

        public Presenter()
        {
            InitializeComponent();

            _parserContext = GetParserContext();
            DataContext = UiServices.ViewModels.DataContextManager.Instance;
        }

        private ParserContext GetParserContext()
        {
            ParserContext context = new ParserContext();
            var applicationDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var resourcesDirectory = System.IO.Path.Combine(applicationDirectory, "Resources");

            context.BaseUri = new Uri(resourcesDirectory, UriKind.Absolute);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("x:Class", this.GetType().FullName);

            return context;
        }

        public IList<Augmentor> Augmentors
        {
            get { return (IList<Augmentor>)GetValue(AugmentorsProperty); }
            set { SetValue(AugmentorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Augmentors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AugmentorsProperty =
            DependencyProperty.Register("Augmentors", typeof(IList<Augmentor>), typeof(Presenter), new PropertyMetadata(AugmentorsChanged));

        private static void AugmentorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Presenter presenter = (Presenter)d;
            presenter.RefreshAsync();
        }


        public async void RefreshAsync()
        {
            AugmentorsProcessor processor = new AugmentorsProcessor();
            string xaml = await processor.Process(Augmentors);
            rootContent.Content = GetRootObject(xaml);
        }


        private DependencyObject GetRootObject(string xaml)
        {
            System.IO.StringReader reader = new System.IO.StringReader(xaml);

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(xaml);
                writer.Flush();
                ms.Position = 0;

                DependencyObject rootObject = XamlReader.Load(ms, _parserContext) as DependencyObject;

                return rootObject;
            }
        }
    }
}
