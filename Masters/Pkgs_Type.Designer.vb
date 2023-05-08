<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pkgs_Type
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pkgs_Type))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.bnt_New = New System.Windows.Forms.Button()
        Me.bnt_Save = New System.Windows.Forms.Button()
        Me.bnt_Edit = New System.Windows.Forms.Button()
        Me.bnt_Del = New System.Windows.Forms.Button()
        Me.dgv_Pkgs = New System.Windows.Forms.DataGridView()
        Me.p_Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.p_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.p_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txt_Unit_Type = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Desc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Booking_Notes = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        CType(Me.dgv_Pkgs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bnt_New
        '
        Me.bnt_New.BackColor = System.Drawing.Color.Navy
        Me.bnt_New.BackgroundImage = CType(resources.GetObject("bnt_New.BackgroundImage"), System.Drawing.Image)
        Me.bnt_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_New.ForeColor = System.Drawing.Color.Black
        Me.bnt_New.Image = Global.MSI_Solution.My.Resources.Resources._new
        Me.bnt_New.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_New.Location = New System.Drawing.Point(259, 11)
        Me.bnt_New.Margin = New System.Windows.Forms.Padding(2)
        Me.bnt_New.Name = "bnt_New"
        Me.bnt_New.Size = New System.Drawing.Size(83, 33)
        Me.bnt_New.TabIndex = 22
        Me.bnt_New.Text = "New"
        Me.bnt_New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_New.UseVisualStyleBackColor = False
        '
        'bnt_Save
        '
        Me.bnt_Save.BackColor = System.Drawing.Color.Navy
        Me.bnt_Save.BackgroundImage = CType(resources.GetObject("bnt_Save.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Save.Enabled = False
        Me.bnt_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Save.ForeColor = System.Drawing.Color.Black
        Me.bnt_Save.Image = Global.MSI_Solution.My.Resources.Resources.save
        Me.bnt_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Save.Location = New System.Drawing.Point(517, 11)
        Me.bnt_Save.Margin = New System.Windows.Forms.Padding(2)
        Me.bnt_Save.Name = "bnt_Save"
        Me.bnt_Save.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Save.TabIndex = 25
        Me.bnt_Save.Text = "Save"
        Me.bnt_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Save.UseVisualStyleBackColor = False
        '
        'bnt_Edit
        '
        Me.bnt_Edit.BackColor = System.Drawing.Color.Navy
        Me.bnt_Edit.BackgroundImage = CType(resources.GetObject("bnt_Edit.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Edit.ForeColor = System.Drawing.Color.Black
        Me.bnt_Edit.Image = CType(resources.GetObject("bnt_Edit.Image"), System.Drawing.Image)
        Me.bnt_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Edit.Location = New System.Drawing.Point(345, 11)
        Me.bnt_Edit.Margin = New System.Windows.Forms.Padding(2)
        Me.bnt_Edit.Name = "bnt_Edit"
        Me.bnt_Edit.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Edit.TabIndex = 23
        Me.bnt_Edit.Text = "Edit"
        Me.bnt_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Edit.UseVisualStyleBackColor = False
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
        Me.bnt_Del.Location = New System.Drawing.Point(431, 11)
        Me.bnt_Del.Margin = New System.Windows.Forms.Padding(2)
        Me.bnt_Del.Name = "bnt_Del"
        Me.bnt_Del.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Del.TabIndex = 24
        Me.bnt_Del.Text = "Del"
        Me.bnt_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Del.UseVisualStyleBackColor = False
        '
        'dgv_Pkgs
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgv_Pkgs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_Pkgs.BackgroundColor = System.Drawing.Color.Azure
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Pkgs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_Pkgs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Pkgs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.p_Unit, Me.p_Desc, Me.p_ID})
        Me.dgv_Pkgs.Location = New System.Drawing.Point(8, 4)
        Me.dgv_Pkgs.Name = "dgv_Pkgs"
        Me.dgv_Pkgs.ReadOnly = True
        Me.dgv_Pkgs.RowHeadersVisible = False
        Me.dgv_Pkgs.Size = New System.Drawing.Size(237, 617)
        Me.dgv_Pkgs.TabIndex = 26
        '
        'p_Unit
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.p_Unit.DefaultCellStyle = DataGridViewCellStyle3
        Me.p_Unit.HeaderText = "Unit"
        Me.p_Unit.Name = "p_Unit"
        Me.p_Unit.ReadOnly = True
        Me.p_Unit.Width = 50
        '
        'p_Desc
        '
        Me.p_Desc.HeaderText = "Description"
        Me.p_Desc.Name = "p_Desc"
        Me.p_Desc.ReadOnly = True
        Me.p_Desc.Width = 120
        '
        'p_ID
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.p_ID.DefaultCellStyle = DataGridViewCellStyle4
        Me.p_ID.HeaderText = "ID"
        Me.p_ID.Name = "p_ID"
        Me.p_ID.ReadOnly = True
        Me.p_ID.Width = 40
        '
        'txt_Unit_Type
        '
        Me.txt_Unit_Type.Location = New System.Drawing.Point(326, 68)
        Me.txt_Unit_Type.MaxLength = 3
        Me.txt_Unit_Type.Name = "txt_Unit_Type"
        Me.txt_Unit_Type.Size = New System.Drawing.Size(26, 20)
        Me.txt_Unit_Type.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(298, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Unit"
        '
        'txt_Desc
        '
        Me.txt_Desc.Location = New System.Drawing.Point(326, 95)
        Me.txt_Desc.MaxLength = 21
        Me.txt_Desc.Name = "txt_Desc"
        Me.txt_Desc.Size = New System.Drawing.Size(121, 20)
        Me.txt_Desc.TabIndex = 1
        Me.txt_Desc.Text = " "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(263, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Description"
        '
        'Txt_Booking_Notes
        '
        Me.Txt_Booking_Notes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.Txt_Booking_Notes.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Booking_Notes.Location = New System.Drawing.Point(250, 152)
        Me.Txt_Booking_Notes.Multiline = True
        Me.Txt_Booking_Notes.Name = "Txt_Booking_Notes"
        Me.Txt_Booking_Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Txt_Booking_Notes.Size = New System.Drawing.Size(638, 232)
        Me.Txt_Booking_Notes.TabIndex = 30
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(263, 136)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(77, 13)
        Me.Label38.TabIndex = 31
        Me.Label38.Text = "Booking Notes"
        '
        'Pkgs_Type
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 633)
        Me.Controls.Add(Me.Txt_Booking_Notes)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.txt_Desc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_Unit_Type)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgv_Pkgs)
        Me.Controls.Add(Me.bnt_New)
        Me.Controls.Add(Me.bnt_Save)
        Me.Controls.Add(Me.bnt_Edit)
        Me.Controls.Add(Me.bnt_Del)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Pkgs_Type"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Pkgs Type"
        CType(Me.dgv_Pkgs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents bnt_New As Button
    Friend WithEvents bnt_Save As Button
    Friend WithEvents bnt_Edit As Button
    Friend WithEvents bnt_Del As Button
    Friend WithEvents dgv_Pkgs As DataGridView
    Friend WithEvents txt_Unit_Type As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Desc As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents p_Unit As DataGridViewTextBoxColumn
    Friend WithEvents p_Desc As DataGridViewTextBoxColumn
    Friend WithEvents p_ID As DataGridViewTextBoxColumn
    Friend WithEvents Txt_Booking_Notes As TextBox
    Friend WithEvents Label38 As Label
End Class
