Public Module Helpers

    Public Function Sum(ByVal matrix As Integer(,)) As Integer
        Dim res As Integer = 0

        For i As Integer = 0 To matrix.GetLength(0) - 1
            For j As Integer = 0 To matrix.GetLength(1) - 1
                res += matrix(i, j)
            Next
        Next

        Return res
    End Function

    Public Function Avg(ByVal matrix As Integer(,)) As Integer
        Dim res = Sum(matrix) / matrix.Length
        Return res
    End Function

    Public Function Min(ByVal matrix As Integer(,)) As Integer
        Dim res As Integer = matrix(0, 0)
        For i As Integer = 0 To matrix.GetLength(0) - 1
            For j As Integer = 0 To matrix.GetLength(1) - 1
                If res > matrix(i, j) Then
                    res = matrix(i, j)
                End If
            Next
        Next
        Return res
    End Function

    Public Function Max(ByVal matrix As Integer(,)) As Integer
        Dim res As Integer = matrix(0, 0)
        For i As Integer = 0 To matrix.GetLength(0) - 1
            For j As Integer = 0 To matrix.GetLength(1) - 1
                If res < matrix(i, j) Then
                    res = matrix(i, j)
                End If
            Next
        Next
        Return res
    End Function

    '--- Matrix Sorting ---------------------

    Private Sub SortArray(array As Integer())
        Array.Sort(array)
    End Sub

    Private Function MatrixToArray(matrix As Integer(,), row As Integer, column As Integer) As Integer()
        Dim res = New Integer(row * column - 1) {}
        Dim counter As Integer = 0

        For i As Integer = 0 To row - 1
            For j As Integer = 0 To column - 1
                res(counter) = matrix(i, j)
                counter += 1
            Next
        Next
        Return res
    End Function

    Private Function ArrayToMatrix(array As Integer(), row As Integer, column As Integer) As Integer(,)
        Dim res = New Integer(row - 1, column - 1) {}
        Dim counter As Integer = 0
        For i As Integer = 0 To row - 1
            For j As Integer = 0 To column - 1
                res(i, j) = array(counter)
                counter += 1
            Next
        Next
        Return res
    End Function

    Public Function SortMatrix(matrix As Integer(,), row As Integer, column As Integer) As List(Of DataItem)
        Dim res As New List(Of DataItem)()
        Dim arr = MatrixToArray(matrix, row, column)

        SortArray(arr)

        Dim twoDim = ArrayToMatrix(arr, row, column)

        For i As Integer = 0 To twoDim.GetLength(0) - 1
            Dim data = New DataItem With {
                .a1 = twoDim(i, 0),
                .a2 = twoDim(i, 1),
                .a3 = twoDim(i, 2),
                .a4 = twoDim(i, 3),
                .a5 = twoDim(i, 4)
            }
            res.Add(data)
        Next

        Return res
    End Function
End Module
