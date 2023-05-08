<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dptos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dptos))
        Me.dgv_Dptos = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Dpto_Number = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Dpto_Name = New System.Windows.Forms.TextBox()
        Me.cb_PortL = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_PortD = New System.Windows.Forms.ComboBox()
        Me.bnt_New = New System.Windows.Forms.Button()
        Me.bnt_Save = New System.Windows.Forms.Button()
        Me.bnt_Edit = New System.Windows.Forms.Button()
        Me.bnt_Del = New System.Windows.Forms.Button()
        Me.bnt_Refresh = New System.Windows.Forms.Button()
        CType(Me.dgv_Dptos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_Dptos
        '
        Me.dgv_Dptos.AllowUserToAddRows = False
        Me.dgv_Dptos.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Dptos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_Dptos.BackgroundColor = System.Drawing.Color.Azure
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Dptos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_Dptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Dptos.Location = New System.Drawing.Point(4, 13)
        Me.dgv_Dptos.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_Dptos.Name = "dgv_Dptos"
        Me.dgv_Dptos.ReadOnly = True
        Me.dgv_Dptos.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Dptos.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_Dptos.Size = New System.Drawing.Size(952, 665)
        Me.dgv_Dptos.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1056, 192)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "#"
        '
        'txt_Dpto_Number
        '
        Me.txt_Dpto_Number.BackColor = System.Drawing.Color.MidnightBlue
        Me.txt_Dpto_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Dpto_Number.ForeColor = System.Drawing.Color.White
        Me.txt_Dpto_Number.Location = New System.Drawing.Point(1083, 188)
        Me.txt_Dpto_Number.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Dpto_Number.MaxLength = 10
        Me.txt_Dpto_Number.Name = "txt_Dpto_Number"
        Me.txt_Dpto_Number.ReadOnly = True
        Me.txt_Dpto_Number.Size = New System.Drawing.Size(75, 22)
        Me.txt_Dpto_Number.TabIndex = 2
        Me.txt_Dpto_Number.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1032, 222)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Name"
        '
        'txt_Dpto_Name
        '
        Me.txt_Dpto_Name.Location = New System.Drawing.Point(1083, 218)
        Me.txt_Dpto_Name.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Dpto_Name.MaxLength = 50
        Me.txt_Dpto_Name.Name = "txt_Dpto_Name"
        Me.txt_Dpto_Name.Size = New System.Drawing.Size(329, 22)
        Me.txt_Dpto_Name.TabIndex = 4
        '
        'cb_PortL
        '
        Me.cb_PortL.FormattingEnabled = True
        Me.cb_PortL.Location = New System.Drawing.Point(1083, 249)
        Me.cb_PortL.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_PortL.Name = "cb_PortL"
        Me.cb_PortL.Size = New System.Drawing.Size(329, 24)
        Me.cb_PortL.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1009, 254)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Loading"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1004, 286)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Discharge"
        '
        'cb_PortD
        '
        Me.cb_PortD.FormattingEnabled = True
        Me.cb_PortD.Location = New System.Drawing.Point(1083, 281)
        Me.cb_PortD.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_PortD.Name = "cb_PortD"
        Me.cb_PortD.Size = New System.Drawing.Size(329, 24)
        Me.cb_PortD.TabIndex = 7
        '
        'bnt_New
        '
        Me.bnt_New.BackColor = System.Drawing.Color.Navy
        Me.bnt_New.BackgroundImage = CType(resources.GetObject("bnt_New.BackgroundImage"), System.Drawing.Image)
        Me.bnt_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_New.ForeColor = System.Drawing.Color.Black
        Me.bnt_New.Image = Global.MSI_Solution.My.Resources.Resources._new
        Me.bnt_New.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_New.Location = New System.Drawing.Point(1069, 14)
        Me.bnt_New.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_New.Name = "bnt_New"
        Me.bnt_New.Size = New System.Drawing.Size(96, 41)
        Me.bnt_New.TabIndex = 18
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
        Me.bnt_Save.Location = New System.Drawing.Point(1378, 14)
        Me.bnt_Save.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Save.Name = "bnt_Save"
        Me.bnt_Save.Size = New System.Drawing.Size(91, 41)
        Me.bnt_Save.TabIndex = 21
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
        Me.bnt_Edit.Location = New System.Drawing.Point(1171, 14)
        Me.bnt_Edit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Edit.Name = "bnt_Edit"
        Me.bnt_Edit.Size = New System.Drawing.Size(97, 41)
        Me.bnt_Edit.TabIndex = 19
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
        Me.bnt_Del.Location = New System.Drawing.Point(1274, 14)
        Me.bnt_Del.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Del.Name = "bnt_Del"
        Me.bnt_Del.Size = New System.Drawing.Size(98, 41)
        Me.bnt_Del.TabIndex = 20
        Me.bnt_Del.Text = "Del"
        Me.bnt_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Del.UseVisualStyleBackColor = False
        '
        'bnt_Refresh
        '
        Me.bnt_Refresh.BackColor = System.Drawing.Color.Navy
        Me.bnt_Refresh.BackgroundImage = CType(resources.GetObject("bnt_Refresh.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Refresh.ForeColor = System.Drawing.Color.Black
        Me.bnt_Refresh.Image = Global.MSI_Solution.My.Resources.Resources.Refresh
        Me.bnt_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Refresh.Location = New System.Drawing.Point(961, 14)
        Me.bnt_Refresh.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Refresh.Name = "bnt_Refresh"
        Me.bnt_Refresh.Size = New System.Drawing.Size(104, 41)
        Me.bnt_Refresh.TabIndex = 22
        Me.bnt_Refresh.Text = "Refresh"
        Me.bnt_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Refresh.UseVisualStyleBackColor = False
        '
        'Dptos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1474, 691)
        Me.Controls.Add(Me.bnt_Refresh)
        Me.Controls.Add(Me.bnt_New)
        Me.Controls.Add(Me.bnt_Save)
        Me.Controls.Add(Me.bnt_Edit)
        Me.Controls.Add(Me.bnt_Del)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cb_PortD)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cb_PortL)
        Me.Controls.Add(Me.txt_Dpto_Name)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Dpto_Number)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgv_Dptos)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Dptos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Departments"
        CType(Me.dgv_Dptos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgv_Dptos As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Dpto_Number As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Dpto_Name As TextBox
    Friend WithEvents cb_PortL As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cb_PortD As ComboBox
    Friend WithEvents bnt_New As Button
    Friend WithEvents bnt_Save As Button
    Friend WithEvents bnt_Edit As Button
    Friend WithEvents bnt_Del As Button
    Friend WithEvents bnt_Refresh As Button
End Class
