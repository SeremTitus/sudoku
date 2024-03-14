Public Class SudokuGenerator

    Public boardDimensions As Point = New Point(3, 3)
    Public board As List(Of List(Of String))
    Public boardholes As List(Of List(Of String))

    Public Sub New(Optional ByVal pBoardDimensions As Point = Nothing)
        If pBoardDimensions = Nothing Then
            pBoardDimensions = New Point(3, 3)
        End If
        Me.boardDimensions = pBoardDimensions
        InitializeBoard()
    End Sub
    
    Public Overrides Function ToString() As String
        Dim output As New System.Text.StringBuilder("{")

        Dim rowCount As Integer = Me.boardDimensions.X * Me.boardDimensions.Y

        For rowIndex As Integer = 0 To rowCount - 1
            output.AppendLine()
            ' Convert each row of the board to a string and append it to the output
            output.Append("[" & String.Join(", ", Me.boardholes(rowIndex)) & "]")
        Next

        output.AppendLine()
        output.Append("}")

        Return output.ToString()
    End Function

    Private Sub InitializeBoard()
        board = New List(Of List(Of String))
        Dim rowCount As Integer = boardDimensions.X * boardDimensions.Y
        Dim emptyRow As New List(Of String)

        For rowsIndex As Integer = 1 To rowCount
            emptyRow.Add("-")
        Next

        For columnIndex As Integer = 1 To rowCount
            board.Add(New List(Of String)(emptyRow))
        Next
        BoardFill()
        board_holes(30)
    End Sub

    Private Sub Shuffle(Of T)(ByRef array As T())
        Dim rand As New Random()
        Dim n As Integer = array.Length

        While n > 1
            n -= 1
            Dim k As Integer = rand.Next(n + 1)
            Dim value As T = array(k)
            array(k) = array(n)
            array(n) = value
        End While
    End Sub

    Private Function BoardFill(Optional ByVal alfanum() As String = Nothing, Optional ByRef pboard As List(Of List(Of String)) = Nothing)
        If alfanum Is Nothing Then
            alfanum = {"1", "2", "3", "4", "5", "6", "7", "8", "9"}
        End If
        If pboard Is Nothing Then
            pboard = board
        End If

        Dim rowCount As Integer = boardDimensions.X * boardDimensions.Y
        Dim row_index As Integer = -1

        While row_index < rowCount - 1
            row_index += 1

            If pboard(row_index)(0) = "-" Then
                Randomize()
                Shuffle(alfanum)
            End If

            Dim lastValue As New List(Of String)()
            Dim column_index As Integer = -1
            While column_index < rowCount - 1
                column_index += 1

                If pboard(row_index)(column_index) <> "-" Then
                    Continue While
                End If
                For Each value In alfanum
                    If lastValue.Contains(value) Then
                        Continue For
                    End If

                    If CellVerify(value, New Point(row_index, column_index), pboard) Then
                        pboard(row_index)(column_index) = value
                        If IsBoardFull(pboard) Then
                            Return True
                        ElseIf BoardFill(alfanum, pboard) Then
                            Return True
                        Else
                            lastValue.Add(value)
                            pboard(row_index)(column_index) = "-"
                        End If
                    End If
                Next

                If pboard(row_index)(column_index) = "-" Then
                    Return False
                End If
            End While
        End While
    End Function

    Sub board_holes(ByVal difficulty As Integer)
        If Not IsBoardFull(board) Then
            Return
        End If

        Dim row_count As Integer = boardDimensions.X * boardDimensions.Y
        Dim cells(row_count * row_count - 1) As Point
        Dim index As Integer = 0

        For row_index As Integer = 0 To row_count - 1
            For column_index As Integer = 0 To row_count - 1
                cells(index) = New Point(row_index, column_index)
                index += 1
            Next
        Next

        Randomize()
        Shuffle(cells)

        If boardholes Is Nothing OrElse boardholes.Count = 0 Then
            boardholes = DeepCopy(board)
        End If

        Dim holes As Integer = 0
        For Each cell As Point In cells
            If boardholes(cell.X)(cell.Y) = "-" Then
                holes += 1
            End If
        Next

        For Each cell As Point In cells
            If holes > difficulty Then
                Exit For
            End If

            Dim initial_value = boardholes(cell.X)(cell.Y)
            boardholes(cell.X)(cell.Y) = "-"

            Dim testboard = DeepCopy(boardholes)
            BoardFill(New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9"}, testboard)

            If AreEqual(testboard, board) Then
                holes += 1
            Else
                boardholes(cell.X)(cell.Y) = initial_value
            End If
        Next
    End Sub
    Function DeepCopy(ByVal original As List(Of List(Of String))) As List(Of List(Of String))
        Dim copy As New List(Of List(Of String))()

        For Each innerList As List(Of String) In original
            Dim innerCopy As New List(Of String)(innerList)
            copy.Add(innerCopy)
        Next

        Return copy
    End Function
    Function AreEqual(ByVal list1 As List(Of List(Of String)), ByVal list2 As List(Of List(Of String))) As Boolean
        ' Check if lists are the same reference
        If Object.ReferenceEquals(list1, list2) Then
            Return True
        End If

        ' Check if either list is Nothing
        If list1 Is Nothing OrElse list2 Is Nothing Then
            Return False
        End If

        ' Check if lists have different counts
        If list1.Count <> list2.Count Then
            Return False
        End If

        ' Check each element of the lists
        For i As Integer = 0 To list1.Count - 1
            ' Check if sublists have different counts
            If list1(i).Count <> list2(i).Count Then
                Return False
            End If

            ' Check each string in the sublists
            For j As Integer = 0 To list1(i).Count - 1
                ' Compare strings
                If Not String.Equals(list1(i)(j), list2(i)(j)) Then
                    Return False
                End If
            Next
        Next

        ' If all elements are equal, return true
        Return True
    End Function

    Private Function IsBoardFull(pboard As List(Of List(Of String))) As Boolean
        Dim rowCount As Integer = boardDimensions.X * boardDimensions.Y
        For i As Integer = 0 To rowCount - 1
            For j As Integer = 0 To rowCount - 1
                If pboard(i)(j) = "-" Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    Private Function CellVerify(ByVal pValue As String, ByVal pCoordinate As Point, Optional pboard As List(Of List(Of String)) = Nothing) As Boolean
        If pboard Is Nothing Then
            pboard = board
        End If
        ' Check the row
        If pboard(pCoordinate.X).Contains(pValue) Then
            Return False
        End If

        ' Check the column
        For rowIndex As Integer = 0 To boardDimensions.X * boardDimensions.Y - 1
            If pValue = pboard(rowIndex)(pCoordinate.Y) Then
                Return False
            End If
        Next

        ' Function to find bounds for the small square
        Dim findBound As Func(Of Integer, Point) = Function(location As Integer) As Point
                                                       Dim lowerBound As Integer = 0
                                                       Dim upperBound As Integer = lowerBound + boardDimensions.X - 1

                                                       While Not (location >= lowerBound AndAlso location <= upperBound)
                                                           If location > lowerBound Then
                                                               lowerBound += boardDimensions.X
                                                               upperBound += boardDimensions.X
                                                           ElseIf location < upperBound Then
                                                               lowerBound -= boardDimensions.X
                                                               upperBound -= boardDimensions.X
                                                           End If
                                                       End While

                                                       Return New Point(lowerBound, upperBound)
                                                   End Function

        ' Finding bounds for row and column
        Dim rowBound As Point = findBound(pCoordinate.X)
        Dim columnBound As Point = findBound(pCoordinate.Y)

        ' Check the small square
        For rowIndex As Integer = rowBound.X To rowBound.Y
            For columnIndex As Integer = columnBound.X To columnBound.Y
                If pValue = pboard(rowIndex)(columnIndex) Then
                    Return False
                End If
            Next
        Next

        Return True
    End Function

End Class
