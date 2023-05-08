<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Commdty_Sched_B
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Commdty_Sched_B))
        Me.cb_Schud_B = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cb_Chapters = New System.Windows.Forms.ComboBox()
        Me.dgv_SchedB = New System.Windows.Forms.DataGridView()
        Me.dgv_Chapters = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_SchedB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_Chapters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cb_Schud_B
        '
        Me.cb_Schud_B.FormattingEnabled = True
        Me.cb_Schud_B.Location = New System.Drawing.Point(475, 4)
        Me.cb_Schud_B.Margin = New System.Windows.Forms.Padding(2)
        Me.cb_Schud_B.MaxDropDownItems = 50
        Me.cb_Schud_B.MaxLength = 40
        Me.cb_Schud_B.Name = "cb_Schud_B"
        Me.cb_Schud_B.Size = New System.Drawing.Size(960, 21)
        Me.cb_Schud_B.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(9, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1449, 722)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cb_Chapters)
        Me.TabPage1.Controls.Add(Me.cb_Schud_B)
        Me.TabPage1.Controls.Add(Me.dgv_SchedB)
        Me.TabPage1.Controls.Add(Me.dgv_Chapters)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1441, 696)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Commodities (Schedule B)"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cb_Chapters
        '
        Me.cb_Chapters.FormattingEnabled = True
        Me.cb_Chapters.Location = New System.Drawing.Point(6, 5)
        Me.cb_Chapters.Name = "cb_Chapters"
        Me.cb_Chapters.Size = New System.Drawing.Size(454, 21)
        Me.cb_Chapters.TabIndex = 5
        '
        'dgv_SchedB
        '
        Me.dgv_SchedB.AllowUserToAddRows = False
        Me.dgv_SchedB.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Azure
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_SchedB.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_SchedB.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgv_SchedB.BackgroundColor = System.Drawing.Color.Azure
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_SchedB.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_SchedB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_SchedB.Location = New System.Drawing.Point(476, 30)
        Me.dgv_SchedB.Name = "dgv_SchedB"
        Me.dgv_SchedB.ReadOnly = True
        Me.dgv_SchedB.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_SchedB.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_SchedB.Size = New System.Drawing.Size(959, 660)
        Me.dgv_SchedB.TabIndex = 4
        '
        'dgv_Chapters
        '
        Me.dgv_Chapters.AllowUserToAddRows = False
        Me.dgv_Chapters.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Chapters.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_Chapters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgv_Chapters.BackgroundColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Chapters.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_Chapters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Chapters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.dgv_Chapters.Location = New System.Drawing.Point(6, 30)
        Me.dgv_Chapters.Name = "dgv_Chapters"
        Me.dgv_Chapters.ReadOnly = True
        Me.dgv_Chapters.RowHeadersVisible = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Chapters.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgv_Chapters.Size = New System.Drawing.Size(454, 660)
        Me.dgv_Chapters.TabIndex = 3
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn4.HeaderText = "Chapter"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn5.HeaderText = "#"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 5
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn6.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 370
        '
        'Commdty_Sched_B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1459, 731)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Commdty_Sched_B"
        Me.Text = "Commodies Schedule B"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgv_SchedB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_Chapters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cb_Schud_B As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents dgv_Chapters As DataGridView
    Friend WithEvents dgv_SchedB As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents cb_Chapters As ComboBox
End Class
