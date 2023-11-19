Imports System
Imports System.Collections.ObjectModel
Imports System.Windows

Public Class MainWindow
    Public Property Data As ObservableCollection(Of DataItem)
    Public Property ResultData As ObservableCollection(Of Results)

    Public Sub New()
        InitializeComponent()
        Data = New ObservableCollection(Of DataItem) From {
            New DataItem With {.a1=0, .a2=0, .a3=0}
        }
        dataGrid.ItemsSource = Data
    End Sub

    Private Sub GetRes_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Try
            ' Get cellValues
            Dim rowCount As Integer = Data.Count
            Dim columnCount As Integer = GetType(DataItem).GetProperties().Length
            Dim cellValues(rowCount - 1, columnCount - 1) As Integer

            For i As Integer = 0 To rowCount - 1
                For j As Integer = 0 To columnCount - 1
                    Dim cell As TextBlock = TryCast(dataGrid.Columns(j).GetCellContent(dataGrid.Items(i)), TextBlock)
                    If cell IsNot Nothing Then
                        cellValues(i, j) = Integer.Parse(cell.Text)
                    End If
                Next
            Next

            ' Get summaries
            dim1.Content = "rows: " & Helpers.showDIm(cellValues, 0).ToString()
            dim2.Content = "columns: " & Helpers.showDIm(cellValues, 1).ToString()
            sumBox.Text = Helpers.Sum(cellValues).ToString()
            avgBox.Text = Helpers.Avg(cellValues).ToString()
            minBox.Text = Helpers.Min(cellValues).ToString()
            maxBox.Text = Helpers.Max(cellValues).ToString()

            ' Sorting Matrix
            Dim res = Helpers.SortMatrix(cellValues, cellValues.GetLength(0), cellValues.GetLength(1))
            sortedGrid.ItemsSource = res

            ' resGrid
            ResultData = New ObservableCollection(Of Results)()

            For i As Integer = 0 To cellValues.GetLength(0) - 1
                Dim sum As Integer = 0, min As Integer = cellValues(i, 0), max As Integer = cellValues(i, 0), 
                    avg As Integer = 0, minIdx As Integer = cellValues(i, 0), maxIdx As Integer = cellValues(i, 0)
                For j As Integer = 0 To cellValues.GetLength(1) - 1
                    sum += cellValues(i, j)
                    If max < cellValues(i, j) Then
                        max = cellValues(i, j)
                        maxIdx = j
                    End If
                    If min > cellValues(i, j) Then
                        min = cellValues(i, j)
                        minIdx = j
                    End If
                    avg = sum / cellValues.GetLength(1)
                Next
                Dim item = New Results With {.Avg = avg, .Max = max, .Sum = sum, .Min = min, .MaxIdx = maxIdx, .MinIdx = minIdx}
                ResultData.Add(item)
            Next

            resGrid.ItemsSource = ResultData
            resGrid.Items.Refresh()
        Catch exc As Exception
            MessageBox.Show(exc.Message)
        End Try
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ResultData = New ObservableCollection(Of Results)()
        Data = New ObservableCollection(Of DataItem)()
        dataGrid.ItemsSource = Data
        resGrid.ItemsSource = ResultData
        maxBox.Clear()
        minBox.Clear()
        sumBox.Clear()
        avgBox.Clear()
    End Sub
End Class