Public Class Form1
    Dim PlayerxTurn As Boolean
    Dim NotesOn As Boolean = False
    Dim SudokuArray(81) As TextBox
    Dim SelectedCell As Integer = 1
    Dim SudokuDisplay(81) As String
    Dim SudokuAnswer(81) As String
    Dim CellDisplay As String
    Private stopwatch As New Stopwatch()

    Dim myBoardGame As New SudokuGenerator()
    Dim GameEnded As Boolean = False

    Dim undoRedoStack As List(Of List(Of String))
    Dim undoRedoPointer As Integer = 0
    Dim undoRedolastindex As Integer = -1

    Public Sub LoadGame()
        CellDisplay = "     " & vbCrLf & "     " & vbCrLf & "     "
        Dim selectedFirstCell As Boolean = False
        Dim Index = 0
        For row_index As Integer = 0 To 8
            For column_index As Integer = 0 To 8
                Index += 1
                SudokuArray(Index).Tag = Index
                SudokuAnswer(Index) = myBoardGame.board(row_index)(column_index).ToString()
                SudokuDisplay(Index) = myBoardGame.boardholes(row_index)(column_index).ToString()
                If SudokuDisplay(Index) <> "-" Then
                    SudokuArray(Index).Text = SudokuAnswer(Index)
                    SudokuArray(Index).Font = New Font("Verdana", 20, FontStyle.Bold)
                    SudokuArray(Index).Enabled = False
                Else
                    If selectedFirstCell = False Then
                        selectedFirstCell = True
                        SelectedCell = Index
                    End If
                    SudokuArray(Index).Text = CellDisplay
                End If
            Next
        Next

        GameEnded = False
        stopwatch.Reset()
        stopwatch.Start()

        undoRedoStack = New List(Of List(Of String))
        undoRedoPointer = 0
        BtnUndo.Enabled = False
        BtnRedo.Enabled = False
        If undoRedoPointer > 0 Then
            BtnUndo.Enabled = True
        End If
        If undoRedoPointer < undoRedoStack.Count - 2 Then
            BtnRedo.Enabled = True
        End If
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
        Dim newgame As New newGameDialog()
        newgame.Text = "New Sodoku Game"
        newgame.StartPosition = FormStartPosition.CenterParent
        newgame.Label1.Text = "Welcome to Sudoku Solvers' Sanctuary! Let's conquer puzzles!"
        newgame.ShowDialog()
        Label2.Text = newgame.ComboBox1.Text
        myBoardGame.SetDifficulty(myBoardGame.GetDifficultyFromString(newgame.ComboBox1.Text))
        LoadGame()
        HighlightRow()
        HighlightColumn()
        HighlightGrid()
        Timer1.Start()
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
        ' Check if Backspace key is pressed
        If e.KeyChar = ChrW(Keys.Back) Then
            ' Cancel the event to prevent value deletion
            e.Handled = True
        End If

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            ' Cancel the event to prevent the character from being entered
            e.Handled = True
        End If
        If Char.IsDigit(e.KeyChar) Then
            Dim value As String = e.KeyChar
            If NotesOn Then
                If SudokuArray(SelectedCell).Text <> "" AndAlso SudokuArray(SelectedCell).Font.Name = "Verdana" Then
                    SudokuArray(SelectedCell).Text = "     " & vbCrLf & "     " & vbCrLf & "     "
                End If
                SudokuArray(SelectedCell).Font = New Font("Courier New", 6, FontStyle.Regular)
                UndoRedo_add(SelectedCell, Notes(SudokuArray(SelectedCell).Text, value))
                SudokuArray(SelectedCell).Text = Notes(SudokuArray(SelectedCell).Text, Value)
            Else
                SudokuArray(SelectedCell).Font = New Font("Verdana", 20, FontStyle.Bold)
                UndoRedo_add(SelectedCell, value.ToString())
                SudokuArray(SelectedCell).Text = value.ToString()
                If SudokuArray(SelectedCell).Text = SudokuAnswer(SelectedCell) Then
                    SudokuArray(SelectedCell).ForeColor = Color.Green
                Else
                    SudokuArray(SelectedCell).ForeColor = Color.Red
                End If
                CheckEndGame()
            End If
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
        For Index = 1 To 81
            If SudokuArray(Index).Enabled Then
                SudokuArray(Index).Text = "" ' Clear the text of the enabled cells
            End If
        Next
        stopwatch.Reset()
        stopwatch.Start()
    End Sub

    Private Sub Button_click(sender As Object, e As EventArgs) Handles BtnOne.Click, BtnTwo.Click, BtnThree.Click, BtnFour.Click, BtnFive.Click, BtnSix.Click, BtnSeven.Click, BtnEight.Click, BtnNine.Click
        Dim Btn As Button = CType(sender, Button)
        Dim Value As Integer = Val(Btn.Tag)

        If NotesOn Then
            If SudokuArray(SelectedCell).Text <> "" AndAlso SudokuArray(SelectedCell).Font.Name = "Verdana" Then
                SudokuArray(SelectedCell).Text = "     " & vbCrLf & "     " & vbCrLf & "     "
            End If
            SudokuArray(SelectedCell).Font = New Font("Courier New", 6, FontStyle.Regular)
            UndoRedo_add(SelectedCell, Notes(SudokuArray(SelectedCell).Text, Value))
            SudokuArray(SelectedCell).Text = Notes(SudokuArray(SelectedCell).Text, Value)
        Else
            SudokuArray(SelectedCell).Font = New Font("Verdana", 20, FontStyle.Bold)
            UndoRedo_add(SelectedCell, Value.ToString())
            SudokuArray(SelectedCell).Text = Value.ToString()
            If SudokuArray(SelectedCell).Text = SudokuAnswer(SelectedCell) Then
                SudokuArray(SelectedCell).ForeColor = Color.Green
            Else
                SudokuArray(SelectedCell).ForeColor = Color.Red
            End If
            CheckEndGame()
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = String.Format("{0:hh\:mm\:ss}", stopwatch.Elapsed)
        CheckEndGame()
    End Sub
    Private Sub CheckEndGame()
        Dim mistakes = 0
        Dim answered = 0
        Dim Index = 0
        For row_index As Integer = 0 To 8
            For column_index As Integer = 0 To 8
                Index += 1
                SudokuAnswer(Index) = myBoardGame.board(row_index)(column_index)
                If SudokuArray(Index).Text = SudokuAnswer(Index) Then
                    answered += 1
                ElseIf SudokuArray(Index).Text <> "     " & vbCrLf & "     " & vbCrLf & "     " AndAlso SudokuArray(SelectedCell).Font.Name = "Verdana" Then
                    mistakes += 1
                End If
            Next
        Next
        Label3.Text = "Mistakes: " & mistakes.ToString()
        If GameEnded = True Then
            Exit Sub
        End If
        If answered >= 81 Then
            stopwatch.Stop()
            GameEnded = True
            Dim newgame As New newGameDialog()
            newgame.Text = "New Sodoku Game"
            newgame.StartPosition = FormStartPosition.CenterParent
            newgame.Label1.Text = "Congratulations! You have successfully completed the Sudoku puzzle!"
            Label2.Text = newgame.ComboBox1.Text
            newgame.ShowDialog()
            myBoardGame.NewGame(myBoardGame.GetDifficultyFromString(newgame.ComboBox1.Text))
            LoadGame()
        End If
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        stopwatch.Stop()
        Dim newgame As New newGameDialog()
        newgame.Text = "New Sodoku Game"
        newgame.StartPosition = FormStartPosition.CenterParent
        newgame.Label1.Text = "Try out a new game"
        Label2.Text = newgame.ComboBox1.Text
        newgame.ShowDialog()
        If newgame.setOk.Checked Then
            myBoardGame.NewGame(myBoardGame.GetDifficultyFromString(newgame.ComboBox1.Text))
            LoadGame()
        End If
    End Sub

    Private Sub BtnUndo_Click(sender As Object, e As EventArgs) Handles BtnUndo.Click
        If undoRedoStack Is Nothing OrElse undoRedoPointer > undoRedoStack.Count - 1 OrElse undoRedoPointer <= 0 Then
            Exit Sub
        End If
        undoRedoPointer -= 1
        Dim oneEntry As List(Of String) = undoRedoStack(undoRedoPointer)
        SelectedCell = CInt(oneEntry(0))
        If oneEntry(2) = "True" Then
            NotesOn = True
        Else
            NotesOn = False
        End If
        If NotesOn Then
            If SudokuArray(SelectedCell).Text <> "" AndAlso SudokuArray(SelectedCell).Font.Name = "Verdana" Then
                SudokuArray(SelectedCell).Text = "     " & vbCrLf & "     " & vbCrLf & "     "
            End If
            SudokuArray(SelectedCell).Font = New Font("Courier New", 6, FontStyle.Regular)
        Else
            SudokuArray(SelectedCell).Font = New Font("Verdana", 20, FontStyle.Bold)

        End If
        SudokuArray(SelectedCell).Text = oneEntry(1)
        If NotesOn = False Then
            If SudokuArray(SelectedCell).Text = SudokuAnswer(SelectedCell) Then
                SudokuArray(SelectedCell).ForeColor = Color.Green
            Else
                SudokuArray(SelectedCell).ForeColor = Color.Red
            End If
        End If

        BtnUndo.Enabled = False
        BtnRedo.Enabled = False
        If undoRedoPointer > 0 Then
            BtnUndo.Enabled = True
        End If
        If undoRedoPointer < undoRedoStack.Count - 2 Then
            BtnRedo.Enabled = True
        End If
    End Sub

    Private Sub BtnRedo_Click(sender As Object, e As EventArgs) Handles BtnRedo.Click
        If undoRedoStack Is Nothing OrElse undoRedoPointer > undoRedoStack.Count - 2 OrElse undoRedoPointer < 0 Then
            Exit Sub
        End If
        undoRedoPointer += 1
        Dim oneEntry As List(Of String) = undoRedoStack(undoRedoPointer)
        If oneEntry(2) = "True" Then
            NotesOn = True
        Else
            NotesOn = False
        End If
        If NotesOn Then
            If SudokuArray(SelectedCell).Text <> "" AndAlso SudokuArray(SelectedCell).Font.Name = "Verdana" Then
                SudokuArray(SelectedCell).Text = "     " & vbCrLf & "     " & vbCrLf & "     "
            End If
            SudokuArray(SelectedCell).Font = New Font("Courier New", 6, FontStyle.Regular)
        Else
            SudokuArray(SelectedCell).Font = New Font("Verdana", 20, FontStyle.Bold)
        End If
        SudokuArray(SelectedCell).Text = oneEntry(1)
        If NotesOn = False Then
            If SudokuArray(SelectedCell).Text = SudokuAnswer(SelectedCell) Then
                SudokuArray(SelectedCell).ForeColor = Color.Green
            Else
                SudokuArray(SelectedCell).ForeColor = Color.Red
            End If
        End If

        BtnUndo.Enabled = False
        BtnRedo.Enabled = False
        If undoRedoPointer > 0 Then
            BtnUndo.Enabled = True
        End If
        If undoRedoPointer < undoRedoStack.Count - 2 Then
            BtnRedo.Enabled = True
        End If

    End Sub

    Private Sub UndoRedo_add(index As Integer, new_val As String)
        If undoRedoStack Is Nothing Then
            undoRedoStack = New List(Of List(Of String))
        End If


        If undoRedoPointer > 0 AndAlso undoRedoPointer < undoRedoStack.Count Then
            ' Remove items above the specified index
            For i As Integer = undoRedoStack.Count - 1 To undoRedoPointer + 1 Step -1
                undoRedoStack.RemoveAt(i)
            Next
        End If
        If undoRedoStack.Count > 60 Then
            undoRedoStack.RemoveAt(0)
        End If
        Dim old_val As String = SudokuArray(index).Text

        Dim valu_old As New List(Of String)
        Dim valu_new As New List(Of String)
        valu_old.Add(index.ToString())
        valu_new.Add(index.ToString())
        valu_old.Add(old_val)
        valu_new.Add(new_val)
        Dim old_is_a_note As Boolean = True
        If SudokuArray(SelectedCell).Font.Name = "Verdana" Then
            old_is_a_note = False
        End If
        valu_old.Add(old_is_a_note.ToString())
        valu_new.Add(NotesOn.ToString())
        If undoRedolastindex <> index Then
            undoRedoStack.Add(New List(Of String)(valu_old))
        End If
        undoRedoStack.Add(New List(Of String)(valu_new))
        undoRedoPointer = undoRedoStack.Count - 1
        undoRedolastindex = index

        BtnUndo.Enabled = False
        BtnRedo.Enabled = False
        If undoRedoPointer > 0 Then
            BtnUndo.Enabled = True
        End If
        If undoRedoPointer < undoRedoStack.Count - 2 Then
            BtnRedo.Enabled = True
        End If
    End Sub

End Class
