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
        public void GetDefinitionsTest()
        {
            string xaml = "<Grid xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">";

            GridLength[] widths = new GridLength[]
            {
                GridLength.Auto, new GridLength(25), new GridLength(10,GridUnitType.Star)
            };

            GridLength[] heights = new GridLength[]
            {
                new GridLength(0,GridUnitType.Star),GridLength.Auto, new GridLength(25),new GridLength(7,GridUnitType.Pixel)
            };
 
            string rows = Weniger.WPF.XamlHelpers.GridHelper.GetDefinitions(true, heights);
            string columns = Weniger.WPF.XamlHelpers.GridHelper.GetDefinitions(false,widths);

            xaml = xaml + columns +Environment.NewLine + rows + Environment.NewLine + "</Grid>";

            Grid res = XamlServices.Parse(xaml) as Grid;

            Assert.AreEqual(res.ColumnDefinitions.Count, widths.Length);
            Assert.AreEqual(res.RowDefinitions.Count, heights.Length);

            for (int i=0;i<res.ColumnDefinitions.Count;i++)
            {
                Assert.AreEqual(res.ColumnDefinitions[i].Width, widths[i]);
            }

            for (int i = 0; i < res.RowDefinitions.Count; i++)
            {
                Assert.AreEqual(res.RowDefinitions[i].Height, heights[i]);
            }
        }
    }
}
