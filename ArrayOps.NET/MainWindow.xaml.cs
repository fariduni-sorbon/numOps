using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace NumOps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DataItem> Data { get; set; }
        public ObservableCollection<Results> ResultData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Data = new ObservableCollection<DataItem>
            {
                new DataItem {a1=0,a2=0,a3=0},
            };
            dataGrid.ItemsSource = Data;

        }
        private void GetRes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region get cellValues
                int rowCount = Data.Count;
                int columnCount = typeof(DataItem).GetProperties().Length;
                int[,] cellValues = new int[rowCount, columnCount];

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (dataGrid.Columns[j].GetCellContent(dataGrid.Items[i]) is TextBlock cell)
                        {
                            cellValues[i, j] = int.Parse(cell.Text);
                        }
                    }
                }
                #endregion

                #region get summaries
                dim1.Content = "rows: " + Helpers.showDIm(cellValues, 0).ToString();
                dim2.Content = "columns: " + Helpers.showDIm(cellValues, 1).ToString();

                sumBox.Text = Helpers.Sum(cellValues).ToString();
                avgBox.Text = Helpers.Avg(cellValues).ToString();
                minBox.Text = Helpers.Min(cellValues).ToString();
                maxBox.Text = Helpers.Max(cellValues).ToString();
                #endregion

                #region Sorting Matrix
                var res = Helpers.SortMatrix(cellValues,cellValues.GetLength(0), cellValues.GetLength(1));
                sortedGrid.ItemsSource = res;
                #endregion

                #region resGrid
                ResultData = new ObservableCollection<Results>();

                for (int i = 0; i < cellValues.GetLength(0); i++)
                {
                    int sum = 0, min = cellValues[i, 0], max = cellValues[i, 0], avg = 0, minIdx = cellValues[i, 0], maxIdx = cellValues[i, 0];

                    for (int j = 0; j < cellValues.GetLength(1); j++)
                    {
                        sum += cellValues[i, j];
                        if (max < cellValues[i, j])
                        {
                            max = cellValues[i, j];
                            maxIdx = j;
                        }
                        if (min > cellValues[i, j])
                        {
                            min = cellValues[i, j];
                            minIdx = j;
                        }
                        avg = sum / cellValues.GetLength(1);
                    }
                    var item = new Results { Avg = avg, Max = max, Sum = sum, Min = min, MaxIdx = maxIdx, MinIdx = minIdx };
                    ResultData.Add(item);
                }

                resGrid.ItemsSource = ResultData;
                resGrid.Items.Refresh();
                #endregion
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultData = new ObservableCollection<Results>();
            Data = new ObservableCollection<DataItem>();
            dataGrid.ItemsSource = Data;
            resGrid.ItemsSource = ResultData;

            maxBox.Clear();
            minBox.Clear();
            sumBox.Clear();
            avgBox.Clear();
        }
    }
}