<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Commodities
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Commodities))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.bnt_Edit = New System.Windows.Forms.Button()
        Me.bnt_Del = New System.Windows.Forms.Button()
        Me.bnt_New = New System.Windows.Forms.Button()
        Me.bnt_Save = New System.Windows.Forms.Button()
        Me.gb_Commodity = New System.Windows.Forms.GroupBox()
        Me.wID = New System.Windows.Forms.TextBox()
        Me.lb_ID = New System.Windows.Forms.Label()
        Me.wCommodity = New System.Windows.Forms.TextBox()
        Me.lb_Commodity = New System.Windows.Forms.Label()
        Me.cb_Commodity = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cv_Comdty = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgv_Audit = New System.Windows.Forms.DataGridView()
        Me.A_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A_Made = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A_Old_value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DsCommodity = New System.Data.DataSet()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.gb_Commodity.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgv_Audit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCommodity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(719, 564)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage1.Controls.Add(Me.bnt_Edit)
        Me.TabPage1.Controls.Add(Me.bnt_Del)
        Me.TabPage1.Controls.Add(Me.bnt_New)
        Me.TabPage1.Controls.Add(Me.bnt_Save)
        Me.TabPage1.Controls.Add(Me.gb_Commodity)
        Me.TabPage1.Controls.Add(Me.cb_Commodity)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage1.Size = New System.Drawing.Size(711, 538)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Commodity"
        '
        'bnt_Edit
        '
        Me.bnt_Edit.BackColor = System.Drawing.Color.Navy
        Me.bnt_Edit.BackgroundImage = CType(resources.GetObject("bnt_Edit.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Edit.ForeColor = System.Drawing.Color.Black
        Me.bnt_Edit.Image = CType(resources.GetObject("bnt_Edit.Image"), System.Drawing.Image)
        Me.bnt_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Edit.Location = New System.Drawing.Point(441, 13)
        Me.bnt_Edit.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Edit.Name = "bnt_Edit"
        Me.bnt_Edit.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Edit.TabIndex = 11588
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
        Me.bnt_Del.Location = New System.Drawing.Point(527, 13)
        Me.bnt_Del.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Del.Name = "bnt_Del"
        Me.bnt_Del.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Del.TabIndex = 11589
        Me.bnt_Del.Text = "Del"
        Me.bnt_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Del.UseVisualStyleBackColor = False
        '
        'bnt_New
        '
        Me.bnt_New.BackColor = System.Drawing.Color.Navy
        Me.bnt_New.BackgroundImage = CType(resources.GetObject("bnt_New.BackgroundImage"), System.Drawing.Image)
        Me.bnt_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_New.ForeColor = System.Drawing.Color.Black
        Me.bnt_New.Image = Global.MSI_Solution.My.Resources.Resources._new
        Me.bnt_New.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_New.Location = New System.Drawing.Point(354, 13)
        Me.bnt_New.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_New.Name = "bnt_New"
        Me.bnt_New.Size = New System.Drawing.Size(83, 33)
        Me.bnt_New.TabIndex = 11587
        Me.bnt_New.Text = "New"
        Me.bnt_New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_New.UseVisualStyleBackColor = False
        '
        'bnt_Save
        '
        Me.bnt_Save.BackColor = System.Drawing.Color.Navy
        Me.bnt_Save.BackgroundImage = CType(resources.GetObject("bnt_Save.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnt_Save.ForeColor = System.Drawing.Color.Black
        Me.bnt_Save.Image = Global.MSI_Solution.My.Resources.Resources.save
        Me.bnt_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Save.Location = New System.Drawing.Point(613, 13)
        Me.bnt_Save.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.bnt_Save.Name = "bnt_Save"
        Me.bnt_Save.Size = New System.Drawing.Size(83, 33)
        Me.bnt_Save.TabIndex = 11586
        Me.bnt_Save.Text = "Save"
        Me.bnt_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Save.UseVisualStyleBackColor = False
        '
        'gb_Commodity
        '
        Me.gb_Commodity.BackColor = System.Drawing.Color.LightCyan
        Me.gb_Commodity.Controls.Add(Me.wID)
        Me.gb_Commodity.Controls.Add(Me.lb_ID)
        Me.gb_Commodity.Controls.Add(Me.wCommodity)
        Me.gb_Commodity.Controls.Add(Me.lb_Commodity)
        Me.gb_Commodity.Location = New System.Drawing.Point(337, 102)
        Me.gb_Commodity.Name = "gb_Commodity"
        Me.gb_Commodity.Size = New System.Drawing.Size(368, 73)
        Me.gb_Commodity.TabIndex = 5
        Me.gb_Commodity.TabStop = False
        '
        'wID
        '
        Me.wID.BackColor = System.Drawing.Color.MidnightBlue
        Me.wID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wID.ForeColor = System.Drawing.Color.White
        Me.wID.Location = New System.Drawing.Point(70, 16)
        Me.wID.MaxLength = 10
        Me.wID.Name = "wID"
        Me.wID.ReadOnly = True
        Me.wID.Size = New System.Drawing.Size(86, 20)
        Me.wID.TabIndex = 2
        Me.wID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lb_ID
        '
        Me.lb_ID.AutoSize = True
        Me.lb_ID.Location = New System.Drawing.Point(50, 20)
        Me.lb_ID.Name = "lb_ID"
        Me.lb_ID.Size = New System.Drawing.Size(18, 13)
        Me.lb_ID.TabIndex = 0
        Me.lb_ID.Text = "ID"
        '
        'wCommodity
        '
        Me.wCommodity.BackColor = System.Drawing.Color.LightBlue
        Me.wCommodity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wCommodity.Location = New System.Drawing.Point(70, 41)
        Me.wCommodity.MaxLength = 100
        Me.wCommodity.Name = "wCommodity"
        Me.wCommodity.Size = New System.Drawing.Size(289, 20)
        Me.wCommodity.TabIndex = 3
        '
        'lb_Commodity
        '
        Me.lb_Commodity.AutoSize = True
        Me.lb_Commodity.Location = New System.Drawing.Point(8, 45)
        Me.lb_Commodity.Name = "lb_Commodity"
        Me.lb_Commodity.Size = New System.Drawing.Size(58, 13)
        Me.lb_Commodity.TabIndex = 1
        Me.lb_Commodity.Text = "Commodity"
        '
        'cb_Commodity
        '
        Me.cb_Commodity.DisplayMember = "ID"
        Me.cb_Commodity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cb_Commodity.FormattingEnabled = True
        Me.cb_Commodity.Location = New System.Drawing.Point(6, 3)
        Me.cb_Commodity.Name = "cb_Commodity"
        Me.cb_Commodity.Size = New System.Drawing.Size(323, 527)
        Me.cb_Commodity.TabIndex = 4
        Me.cb_Commodity.ValueMember = "ID"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage2.Controls.Add(Me.cv_Comdty)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage2.Size = New System.Drawing.Size(711, 528)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Report"
        '
        'cv_Comdty
        '
        Me.cv_Comdty.ActiveViewIndex = -1
        Me.cv_Comdty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cv_Comdty.Cursor = System.Windows.Forms.Cursors.Default
        Me.cv_Comdty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cv_Comdty.Location = New System.Drawing.Point(3, 3)
        Me.cv_Comdty.Name = "cv_Comdty"
        Me.cv_Comdty.Size = New System.Drawing.Size(705, 522)
        Me.cv_Comdty.TabIndex = 0
        Me.cv_Comdty.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgv_Audit)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(711, 528)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Audit"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgv_Audit
        '
        Me.dgv_Audit.AllowDrop = True
        Me.dgv_Audit.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Audit.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_Audit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgv_Audit.BackgroundColor = System.Drawing.Color.Azure
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Audit.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_Audit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Audit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.A_Date, Me.A_Made, Me.DataGridViewTextBoxColumn13, Me.A_Old_value})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Audit.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_Audit.GridColor = System.Drawing.Color.Silver
        Me.dgv_Audit.Location = New System.Drawing.Point(8, 10)
        Me.dgv_Audit.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dgv_Audit.Name = "dgv_Audit"
        Me.dgv_Audit.ReadOnly = True
        Me.dgv_Audit.RowHeadersVisible = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Audit.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_Audit.RowTemplate.Height = 24
        Me.dgv_Audit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv_Audit.Size = New System.Drawing.Size(693, 518)
        Me.dgv_Audit.TabIndex = 11563
        Me.dgv_Audit.TabStop = False
        '
        'A_Date
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle3.NullValue = Nothing
        Me.A_Date.DefaultCellStyle = DataGridViewCellStyle3
        Me.A_Date.HeaderText = "Date"
        Me.A_Date.MaxInputLength = 15
        Me.A_Date.Name = "A_Date"
        Me.A_Date.ReadOnly = True
        Me.A_Date.Width = 170
        '
        'A_Made
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.A_Made.DefaultCellStyle = DataGridViewCellStyle4
        Me.A_Made.HeaderText = "Made By"
        Me.A_Made.Name = "A_Made"
        Me.A_Made.ReadOnly = True
        Me.A_Made.Width = 120
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn13.HeaderText = " Description"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn13.Width = 240
        '
        'A_Old_value
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.A_Old_value.DefaultCellStyle = DataGridViewCellStyle6
        Me.A_Old_value.HeaderText = "Before"
        Me.A_Old_value.Name = "A_Old_value"
        Me.A_Old_value.ReadOnly = True
        Me.A_Old_value.Width = 160
        '
        'DsCommodity
        '
        Me.DsCommodity.DataSetName = "NewDataSet"
        '
        'Commodities
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(729, 568)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Commodities"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Commodities"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.gb_Commodity.ResumeLayout(False)
        Me.gb_Commodity.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgv_Audit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCommodity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents cb_Commodity As ComboBox
    Friend WithEvents wCommodity As TextBox
    Friend WithEvents wID As TextBox
    Friend WithEvents lb_Commodity As Label
    Friend WithEvents lb_ID As Label
    Friend WithEvents gb_Commodity As GroupBox
    Friend WithEvents bnt_New As Button
    Friend WithEvents bnt_Save As Button
    Friend WithEvents DsCommodity As DataSet
    Friend WithEvents bnt_Edit As Button
    Friend WithEvents bnt_Del As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents dgv_Audit As DataGridView
    Friend WithEvents A_Date As DataGridViewTextBoxColumn
    Friend WithEvents A_Made As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents A_Old_value As DataGridViewTextBoxColumn
    Friend WithEvents cv_Comdty As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
