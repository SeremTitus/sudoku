Public Class Form1
    Dim PlayerxTurn As Boolean
    Dim NotesOn As Boolean = True
    Dim SudokuArray(81) As TextBox
    Dim SelectedCell As Integer
    Dim SudokuDisplay(81) As Integer
    Dim SudokuAnswer(81) As Integer
    Dim CellDisplay As String

    Public Sub LoadGame()
        Dim Game As String = "_081700930409821006270000401760000028000057000000083079120064307907108205300000000"
        Dim Answer As String = "_681745932439821756275396481763419528892657143514283679128564397947138265356972814"
        CellDisplay = "     " & vbCrLf & "     " & vbCrLf & "     "

        For Index As Integer = 1 To 81
            SudokuArray(Index).Tag = Index
            SudokuAnswer(Index) = Val(Answer.Substring(Index - 1, 1))
            SudokuDisplay(Index) = Val(Game.Substring(Index - 1, 1))

            If SudokuDisplay(Index) <> 0 Then
                SudokuArray(Index).Text = SudokuDisplay(Index).ToString()
                SudokuArray(Index).Font = New Font("Verdana", 31, FontStyle.Bold)
                SudokuArray(Index).Enabled = False
            Else
                SudokuArray(Index).Text = CellDisplay
            End If
        Next
    End Sub

    Private Sub SetControlArray()
        For Index As Integer = 1 To 81
            SudokuArray(Index) = CType(Controls("TextBox" & Index), TextBox)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetControlArray()
        For Index As Integer = 1 To 81
            SudokuArray(Index).Tag = Index
        Next
        LoadGame()
    End Sub

    Private Sub ClickArray(sender As Object, e As EventArgs) Handles TextBox1.Click, TextBox2.Click, TextBox3.Click, TextBox4.Click, TextBox5.Click, TextBox6.Click, TextBox7.Click, TextBox8.Click, TextBox9.Click, TextBox10.Click, TextBox11.Click, TextBox12.Click, TextBox13.Click, TextBox14.Click, TextBox15.Click, TextBox16.Click, TextBox17.Click, TextBox18.Click, TextBox19.Click, TextBox20.Click, TextBox21.Click, TextBox22.Click, TextBox23.Click, TextBox24.Click, TextBox25.Click, TextBox26.Click, TextBox27.Click, TextBox28.Click, TextBox29.Click, TextBox30.Click, TextBox31.Click, TextBox32.Click, TextBox33.Click, TextBox34.Click, TextBox35.Click, TextBox36.Click, TextBox37.Click, TextBox38.Click, TextBox39.Click, TextBox40.Click, TextBox41.Click, TextBox42.Click, TextBox43.Click, TextBox44.Click, TextBox45.Click, TextBox46.Click, TextBox47.Click, TextBox48.Click, TextBox49.Click, TextBox50.Click, TextBox51.Click, TextBox52.Click, TextBox53.Click, TextBox54.Click, TextBox55.Click, TextBox56.Click, TextBox57.Click, TextBox58.Click, TextBox59.Click, TextBox60.Click, TextBox61.Click, TextBox62.Click, TextBox63.Click, TextBox64.Click, TextBox65.Click, TextBox66.Click, TextBox67.Click, TextBox68.Click, TextBox69.Click, TextBox70.Click, TextBox71.Click, TextBox72.Click, TextBox73.Click, TextBox74.Click, TextBox75.Click, TextBox76.Click, TextBox77.Click, TextBox78.Click, TextBox79.Click, TextBox80.Click, TextBox81.Click
        Dim TempTextBox As TextBox = CType(sender, TextBox)
        Dim CellNumber As Integer = CInt(TempTextBox.Tag)

        If SelectedCell <> 0 Then
            ResetBlockColor()
        End If

        SelectedCell = CellNumber
        SudokuArray(SelectedCell).BackColor = Color.SteelBlue

        HighlightRow()
        HighlightColumn()
        HighlightGrid()
    End Sub

    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress, TextBox10.KeyPress, TextBox11.KeyPress, TextBox12.KeyPress, TextBox13.KeyPress, TextBox14.KeyPress, TextBox15.KeyPress, TextBox16.KeyPress, TextBox17.KeyPress, TextBox18.KeyPress, TextBox19.KeyPress, TextBox20.KeyPress, TextBox21.KeyPress, TextBox22.KeyPress, TextBox23.KeyPress, TextBox24.KeyPress, TextBox25.KeyPress, TextBox26.KeyPress, TextBox27.KeyPress, TextBox28.KeyPress, TextBox29.KeyPress, TextBox30.KeyPress, TextBox31.KeyPress, TextBox32.KeyPress, TextBox33.KeyPress, TextBox34.KeyPress, TextBox35.KeyPress, TextBox36.KeyPress, TextBox37.KeyPress, TextBox38.KeyPress, TextBox39.KeyPress, TextBox40.KeyPress, TextBox41.KeyPress, TextBox42.KeyPress, TextBox43.KeyPress, TextBox44.KeyPress, TextBox45.KeyPress, TextBox46.KeyPress, TextBox47.KeyPress, TextBox48.KeyPress, TextBox49.KeyPress, TextBox50.KeyPress, TextBox51.KeyPress, TextBox52.KeyPress, TextBox53.KeyPress, TextBox54.KeyPress, TextBox55.KeyPress, TextBox56.KeyPress, TextBox57.KeyPress, TextBox58.KeyPress, TextBox59.KeyPress, TextBox60.KeyPress, TextBox61.KeyPress, TextBox62.KeyPress, TextBox63.KeyPress, TextBox64.KeyPress, TextBox65.KeyPress, TextBox66.KeyPress, TextBox67.KeyPress, TextBox68.KeyPress, TextBox69.KeyPress, TextBox70.KeyPress, TextBox71.KeyPress, TextBox72.KeyPress, TextBox73.KeyPress, TextBox74.KeyPress, TextBox75.KeyPress, TextBox76.KeyPress, TextBox77.KeyPress, TextBox78.KeyPress, TextBox79.KeyPress, TextBox80.KeyPress, TextBox81.KeyPress
        Dim TempTextBox As TextBox = CType(sender, TextBox)

        ' Check if Backspace key is pressed
        If e.KeyChar = ChrW(Keys.Back) Then
            ' Cancel the event to prevent value deletion
            e.Handled = True
        End If

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            ' Cancel the event to prevent the character from being entered
            e.Handled = True
        End If
    End Sub

    Private Sub HighlightRow()
        Dim RowNumber As Integer = ((SelectedCell - 1) \ 9) + 1 ' Get the row number of the selected cell
        Dim StartRow As Integer = (RowNumber - 1) * 9 + 1
        Dim EndRow As Integer = StartRow + 8

        For Index As Integer = StartRow To EndRow
            SudokuArray(Index).BackColor = Color.Yellow
        Next
    End Sub

    Private Sub HighlightColumn()
        Dim ColNumber As Integer = ((SelectedCell - 1) Mod 9) + 1 ' Get the column number of the selected cell
        Dim StartCol As Integer = ColNumber
        Dim EndCol As Integer = ColNumber + 72

        If EndCol > 81 Then
            EndCol = 81
        End If

        For Index As Integer = StartCol To EndCol Step 9 ' Step by 9 to move down the column
            SudokuArray(Index).BackColor = Color.Yellow
        Next
    End Sub

    Private Sub HighlightGrid()
        Dim RowNumber As Integer = ((SelectedCell - 1) \ 9) + 1 ' Get the row number of the selected cell
        Dim ColNumber As Integer = ((SelectedCell - 1) Mod 9) + 1 ' Get the column number of the selected cell

        Dim StartBlockRow As Integer = (((RowNumber - 1) \ 3) * 3) + 1 ' Get the starting row index of the block
        Dim StartBlockCol As Integer = (((ColNumber - 1) \ 3) * 3) + 1 ' Get the starting column index of the block

        For i As Integer = StartBlockRow To StartBlockRow + 2
            For j As Integer = StartBlockCol To StartBlockCol + 2
                SudokuArray((i - 1) * 9 + j).BackColor = Color.Yellow
            Next
        Next
    End Sub

    Private Sub ResetBlockColor()
        ' Reset the color of all cells to default
        For Index As Integer = 1 To 81
            If SudokuArray(Index) IsNot Nothing Then
                SudokuArray(Index).BackColor = Color.White
            End If
        Next
    End Sub


    Private Sub ButtonRestart_Click(sender As Object, e As EventArgs) Handles ButtonRestart.Click
        For Index As Integer = 1 To 81
            If SudokuArray(Index).Enabled Then
                SudokuArray(Index).Text = "" ' Clear the text of the enabled cells
            End If
        Next
    End Sub

    Private Sub Button_click(sender As Object, e As EventArgs) Handles BtnOne.Click, BtnTwo.Click, BtnThree.Click, BtnFour.Click, BtnFive.Click, BtnSix.Click, BtnSeven.Click, BtnEight.Click, BtnNine.Click
        Dim Btn As Button = CType(sender, Button)
        Dim Value As Integer = Val(Btn.Tag)

        If NotesOn Then
            SudokuArray(SelectedCell).Font = New Font("Courier New", 8, FontStyle.Regular)
            SudokuArray(SelectedCell).Text = Notes(SudokuArray(SelectedCell).Text, Value)
        Else
            SudokuArray(SelectedCell).Font = New Font("Verdana", 31, FontStyle.Bold)
            SudokuArray(SelectedCell).Text = Value.ToString()
        End If
    End Sub

    Function Notes(str As String, Value As Integer) As String
        Dim Position As Integer

        Select Case Value
            Case 1
                Position = 0
            Case 2
                Position = 2
            Case 3
                Position = 4
            Case 4
                Position = 7
            Case 5
                Position = 9
            Case 6
                Position = 11
            Case 7
                Position = 14
            Case 8
                Position = 16
            Case 9
                Position = 18
        End Select

        ' Check if Position is within the bounds of the string
        If Position >= 0 AndAlso Position < str.Length Then
            ' Check toggle value - if value exists, replace with BLANK
            Dim ToggleNumber As String = Value.ToString()
            If str.Substring(Position, 1) = ToggleNumber Then
                ToggleNumber = " "
            End If
            ' Remove string
            str = str.Remove(Position, 1)
            ' Insert character
            str = str.Insert(Position, ToggleNumber)
        End If

        Return str
    End Function

    Private Sub BtnNotes_Click(sender As Object, e As EventArgs) Handles BtnNotes.Click
        If NotesOn Then
            BtnNotes.Text = "Notes Off"
            NotesOn = False
        Else
            BtnNotes.Text = "Notes On"
            NotesOn = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
