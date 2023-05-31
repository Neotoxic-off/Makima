using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Makima.Views
{
    /// <summary>
    /// Interaction logic for AnimeView.xaml
    /// </summary>
    public partial class AnimeView : UserControl
    {
        private int TotalChildren { get; set; }
        public bool Setup { get; set; }
        private ViewModels.AnimeViewModel Context { get; set; }

        public AnimeView()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            Setup = false;
            Context = null;
        }

        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsControl itemsControl = (ItemsControl)sender;
            UniformGrid uniformGrid = FindVisualChild<UniformGrid>(itemsControl);
            double controlWidth = e.NewSize.Width;
            int desiredColumns = 0;

            if (Context == null)
            {
                Context = (ViewModels.AnimeViewModel)this.DataContext;
            }
            desiredColumns = CalculateDesiredColumns(controlWidth);
            uniformGrid.Columns = desiredColumns;
        }

        private void Set(DependencyObject parent)
        {
            TotalChildren = VisualTreeHelper.GetChildrenCount(parent);
        }

        private int CalculateDesiredColumns(double controlWidth)
        {
            const double elementWidth = 330;
            const double margin = 5;
            int numElementsPerRow = (int)Math.Floor((controlWidth - margin) / (elementWidth + margin));
            int maxElements = numElementsPerRow * 5;
            int totalElements = Context.Database.Database.Series.Count();
            int desiredColumns = Math.Min(numElementsPerRow, totalElements);

            return (desiredColumns);
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            DependencyObject child = null;
            T foundChild = null;

            if (Setup == false)
            {
                Set(parent);
            }
            for (int i = 0; i < TotalChildren; i++)
            {
                child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && child != null)
                {
                    return (typedChild);
                }
                else
                {
                    foundChild = FindVisualChild<T>(child);
                    if (foundChild != null)
                    {
                        return (foundChild);
                    }
                }
            }
            return (null);
        }
    }
}
