<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Movements_Type
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Movements_Type))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txt_EDI_Code = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_EDI_Tran = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_Weight = New System.Windows.Forms.ComboBox()
        Me.cb_Seal = New System.Windows.Forms.ComboBox()
        Me.cb_TIR = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Desc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_code = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_Move = New System.Windows.Forms.DataGridView()
        Me.bnt_New = New System.Windows.Forms.Button()
        Me.bnt_Edit = New System.Windows.Forms.Button()
        Me.bnt_Save = New System.Windows.Forms.Button()
        Me.bnt_Del = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_Move, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(911, 578)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage1.Controls.Add(Me.txt_EDI_Code)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.txt_EDI_Tran)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.cb_Weight)
        Me.TabPage1.Controls.Add(Me.cb_Seal)
        Me.TabPage1.Controls.Add(Me.cb_TIR)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txt_Desc)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txt_code)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.dgv_Move)
        Me.TabPage1.Controls.Add(Me.bnt_New)
        Me.TabPage1.Controls.Add(Me.bnt_Edit)
        Me.TabPage1.Controls.Add(Me.bnt_Save)
        Me.TabPage1.Controls.Add(Me.bnt_Del)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage1.Size = New System.Drawing.Size(903, 552)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Movements Code"
        '
        'txt_EDI_Code
        '
        Me.txt_EDI_Code.BackColor = System.Drawing.Color.LightBlue
        Me.txt_EDI_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_EDI_Code.ForeColor = System.Drawing.Color.Black
        Me.txt_EDI_Code.Location = New System.Drawing.Point(582, 64)
        Me.txt_EDI_Code.Name = "txt_EDI_Code"
        Me.txt_EDI_Code.ReadOnly = True
        Me.txt_EDI_Code.Size = New System.Drawing.Size(54, 20)
        Me.txt_EDI_Code.TabIndex = 11630
        Me.txt_EDI_Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(583, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 13)
        Me.Label12.TabIndex = 11629
        Me.Label12.Text = "EDI Code"
        '
        'txt_EDI_Tran
        '
        Me.txt_EDI_Tran.BackColor = System.Drawing.Color.LightBlue
        Me.txt_EDI_Tran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_EDI_Tran.ForeColor = System.Drawing.Color.Black
        Me.txt_EDI_Tran.Location = New System.Drawing.Point(505, 64)
        Me.txt_EDI_Tran.Name = "txt_EDI_Tran"
        Me.txt_EDI_Tran.ReadOnly = True
        Me.txt_EDI_Tran.Size = New System.Drawing.Size(54, 20)
        Me.txt_EDI_Tran.TabIndex = 11628
        Me.txt_EDI_Tran.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(494, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 13)
        Me.Label11.TabIndex = 11627
        Me.Label11.Text = "EDI Tran. Code"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(444, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 11620
        Me.Label7.Text = "Weight"
        '
        'cb_Weight
        '
        Me.cb_Weight.FormattingEnabled = True
        Me.cb_Weight.Items.AddRange(New Object() {"Y", "N"})
        Me.cb_Weight.Location = New System.Drawing.Point(437, 63)
        Me.cb_Weight.Name = "cb_Weight"
        Me.cb_Weight.Size = New System.Drawing.Size(52, 21)
        Me.cb_Weight.TabIndex = 11619
        '
        'cb_Seal
        '
        Me.cb_Seal.FormattingEnabled = True
        Me.cb_Seal.Items.AddRange(New Object() {"Y", "N"})
        Me.cb_Seal.Location = New System.Drawing.Point(379, 63)
        Me.cb_Seal.Name = "cb_Seal"
        Me.cb_Seal.Size = New System.Drawing.Size(52, 21)
        Me.cb_Seal.TabIndex = 11618
        '
        'cb_TIR
        '
        Me.cb_TIR.FormattingEnabled = True
        Me.cb_TIR.Items.AddRange(New Object() {"Y", "N"})
        Me.cb_TIR.Location = New System.Drawing.Point(322, 63)
        Me.cb_TIR.Name = "cb_TIR"
        Me.cb_TIR.Size = New System.Drawing.Size(52, 21)
        Me.cb_TIR.TabIndex = 11616
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(389, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 13)
        Me.Label5.TabIndex = 11613
        Me.Label5.Text = "Seal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(335, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 11609
        Me.Label3.Text = "TIR"
        '
        'txt_Desc
        '
        Me.txt_Desc.BackColor = System.Drawing.Color.MidnightBlue
        Me.txt_Desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Desc.ForeColor = System.Drawing.Color.White
        Me.txt_Desc.Location = New System.Drawing.Point(72, 63)
        Me.txt_Desc.MaxLength = 50
        Me.txt_Desc.Name = "txt_Desc"
        Me.txt_Desc.ReadOnly = True
        Me.txt_Desc.Size = New System.Drawing.Size(244, 20)
        Me.txt_Desc.TabIndex = 11602
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(81, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 11601
        Me.Label2.Text = "Description"
        '
        'txt_code
        '
        Me.txt_code.BackColor = System.Drawing.Color.MidnightBlue
        Me.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_code.ForeColor = System.Drawing.Color.White
        Me.txt_code.Location = New System.Drawing.Point(14, 63)
        Me.txt_code.Name = "txt_code"
        Me.txt_code.ReadOnly = True
        Me.txt_code.Size = New System.Drawing.Size(54, 20)
        Me.txt_code.TabIndex = 11600
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 11599
        Me.Label1.Text = "Code"
        '
        'dgv_Move
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.dgv_Move.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_Move.BackgroundColor = System.Drawing.Color.Azure
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Move.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_Move.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Move.Location = New System.Drawing.Point(28, 88)
        Me.dgv_Move.Name = "dgv_Move"
        Me.dgv_Move.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.dgv_Move.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_Move.Size = New System.Drawing.Size(848, 464)
        Me.dgv_Move.TabIndex = 11598
        '
        'bnt_New
        '
        Me.bnt_New.BackColor = System.Drawing.Color.Navy
        Me.bnt_New.BackgroundImage = CType(resources.GetObject("bnt_New.BackgroundImage"), System.Drawing.Image)
        Me.bnt_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_New.ForeColor = System.Drawing.Color.Black
        Me.bnt_New.Image = Global.MSI_Solution.My.Resources.Resources._new
        Me.bnt_New.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_New.Location = New System.Drawing.Point(246, 5)
        Me.bnt_New.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_New.Name = "bnt_New"
        Me.bnt_New.Size = New System.Drawing.Size(83, 33)
        Me.bnt_New.TabIndex = 11595
        Me.bnt_New.Text = "New"
        Me.bnt_New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_New.UseVisualStyleBackColor = False
        '
        'bnt_Edit
        '
        Me.bnt_Edit.BackColor = System.Drawing.Color.Navy
        Me.bnt_Edit.BackgroundImage = CType(resources.GetObject("bnt_Edit.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Edit.ForeColor = System.Drawing.Color.Black
        Me.bnt_Edit.Image = CType(resources.GetObject("bnt_Edit.Image"), System.Drawing.Image)
        Me.bnt_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Edit.Location = New System.Drawing.Point(333, 5)
        Me.bnt_Edit.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Edit.Name = "bnt_Edit"
        Me.bnt_Edit.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Edit.TabIndex = 11596
        Me.bnt_Edit.Text = "Edit"
        Me.bnt_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Edit.UseVisualStyleBackColor = False
        '
        'bnt_Save
        '
        Me.bnt_Save.BackColor = System.Drawing.Color.Navy
        Me.bnt_Save.BackgroundImage = CType(resources.GetObject("bnt_Save.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Save.ForeColor = System.Drawing.Color.Black
        Me.bnt_Save.Image = Global.MSI_Solution.My.Resources.Resources.save
        Me.bnt_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Save.Location = New System.Drawing.Point(505, 5)
        Me.bnt_Save.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Save.Name = "bnt_Save"
        Me.bnt_Save.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Save.TabIndex = 11594
        Me.bnt_Save.Text = "Save"
        Me.bnt_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Save.UseVisualStyleBackColor = False
        '
        'bnt_Del
        '
        Me.bnt_Del.BackColor = System.Drawing.Color.Navy
        Me.bnt_Del.BackgroundImage = CType(resources.GetObject("bnt_Del.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Del.Enabled = False
        Me.bnt_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Del.ForeColor = System.Drawing.Color.Black
        Me.bnt_Del.Image = CType(resources.GetObject("bnt_Del.Image"), System.Drawing.Image)
        Me.bnt_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Del.Location = New System.Drawing.Point(419, 5)
        Me.bnt_Del.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Del.Name = "bnt_Del"
        Me.bnt_Del.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Del.TabIndex = 11597
        Me.bnt_Del.Text = "Del"
        Me.bnt_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Del.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage2.Size = New System.Drawing.Size(903, 552)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Report"
        '
        'Movements_Type
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(918, 582)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Movements_Type"
        Me.Text = "Equipment Movements "
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgv_Move, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents bnt_New As Button
    Friend WithEvents bnt_Edit As Button
    Friend WithEvents bnt_Save As Button
    Friend WithEvents bnt_Del As Button
    Friend WithEvents dgv_Move As DataGridView
    Friend WithEvents txt_Desc As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_code As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents cb_Weight As ComboBox
    Friend WithEvents cb_Seal As ComboBox
    Friend WithEvents cb_TIR As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_EDI_Code As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_EDI_Tran As TextBox
    Friend WithEvents Label11 As Label
End Class
