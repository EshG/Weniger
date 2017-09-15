using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xaml;
using System.Windows.Controls;

namespace WpfTests
{
    [TestClass]
    public class GridHelperTests
    {
        [TestMethod]
        public void GetRowDefinitionsTest()
        {
            string xaml = "<Grid xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">";

            GridLength[] lengths = new GridLength[]
            {
                GridLength.Auto, new GridLength(25), new GridLength(10,GridUnitType.Star)
            };

            string output = Weniger.WPF.XamlHelpers.GridHelper.GetRowDefinitions(lengths);

            xaml = xaml + output + "</Grid>";

            Grid res = XamlServices.Parse(xaml) as Grid;

            Assert.AreEqual(res.ColumnDefinitions.Count, 3);

            for(int i=0;i<res.ColumnDefinitions.Count;i++)
            {
                Assert.AreEqual(res.ColumnDefinitions[i].Width, lengths[i]);
            }
        }
    }
}
