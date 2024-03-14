<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newGameDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Button1 = New Button()
        ComboBox1 = New ComboBox()
        Label2 = New Label()
        setOk = New CheckBox()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(21, 33)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 25)
        Label1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(242, 294)
        Button1.Name = "Button1"
        Button1.Size = New Size(112, 34)
        Button1.TabIndex = 1
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Simple", "Fast", "Novice", "Easy", "Moderate", "Hard", "Expert", "Extreme", "Diabolical"})
        ComboBox1.Location = New Point(214, 187)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(182, 33)
        ComboBox1.TabIndex = 2
        ComboBox1.Text = "Simple"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(214, 149)
        Label2.Name = "Label2"
        Label2.Size = New Size(175, 25)
        Label2.TabIndex = 3
        Label2.Text = "Select Dificulty Level:"
        ' 
        ' setOk
        ' 
        setOk.AutoSize = True
        setOk.Location = New Point(502, 325)
        setOk.Name = "setOk"
        setOk.Size = New Size(131, 29)
        setOk.TabIndex = 4
        setOk.Text = "Dont delete"
        setOk.UseVisualStyleBackColor = True
        setOk.Visible = False
        ' 
        ' newGameDialog
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(630, 357)
        Controls.Add(setOk)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Name = "newGameDialog"
        Text = "newGameDialog"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents setOk As CheckBox
End Class
