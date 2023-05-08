<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Vessels
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Vessels))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cb_Vessel = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.bnt_New = New System.Windows.Forms.Button()
        Me.bnt_Save = New System.Windows.Forms.Button()
        Me.bnt_Edit = New System.Windows.Forms.Button()
        Me.gb_Dtl = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cb_Route = New System.Windows.Forms.ComboBox()
        Me.wRoute_Desc = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.wRoute_Name = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_Dtl = New System.Windows.Forms.DataGridView()
        Me.leg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Departure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Discharge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.wGross_Tons = New System.Windows.Forms.TextBox()
        Me.wNet_Tons = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.wLOA = New System.Windows.Forms.TextBox()
        Me.wDraft = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.wBeam = New System.Windows.Forms.TextBox()
        Me.bnt_Del = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.wCountry_Code = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.wVessel_Code = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.wIMO_Nro = New System.Windows.Forms.TextBox()
        Me.wCaptain = New System.Windows.Forms.TextBox()
        Me.wFlag = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.wVessel_Name = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.wRegistry = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.wLLoyds = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_Country = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lb_Vessel_Code = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cb_Owner = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cv_Vessels = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgv_Audit = New System.Windows.Forms.DataGridView()
        Me.A_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A_Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A_Made = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A_Old_value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ds_Vessels = New System.Data.DataSet()
        Me.ds_Country = New System.Data.DataSet()
        Me.ds_Owner = New System.Data.DataSet()
        Me.DsVessels_x_Line = New System.Data.DataSet()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.gb_Dtl.SuspendLayout()
        CType(Me.dgv_Dtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgv_Audit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds_Vessels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds_Country, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds_Owner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsVessels_x_Line, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cb_Vessel
        '
        Me.cb_Vessel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cb_Vessel.FormattingEnabled = True
        Me.cb_Vessel.Location = New System.Drawing.Point(8, 7)
        Me.cb_Vessel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cb_Vessel.Name = "cb_Vessel"
        Me.cb_Vessel.Size = New System.Drawing.Size(357, 376)
        Me.cb_Vessel.TabIndex = 1000
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 2)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1039, 809)
        Me.TabControl1.TabIndex = 1001
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage1.Controls.Add(Me.bnt_New)
        Me.TabPage1.Controls.Add(Me.bnt_Save)
        Me.TabPage1.Controls.Add(Me.bnt_Edit)
        Me.TabPage1.Controls.Add(Me.gb_Dtl)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.bnt_Del)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.cb_Owner)
        Me.TabPage1.Controls.Add(Me.cb_Vessel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Size = New System.Drawing.Size(1031, 780)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entry"
        '
        'bnt_New
        '
        Me.bnt_New.BackColor = System.Drawing.Color.Navy
        Me.bnt_New.BackgroundImage = CType(resources.GetObject("bnt_New.BackgroundImage"), System.Drawing.Image)
        Me.bnt_New.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bnt_New.ForeColor = System.Drawing.Color.Black
        Me.bnt_New.Image = Global.MSI_Solution.My.Resources.Resources._new
        Me.bnt_New.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_New.Location = New System.Drawing.Point(536, 9)
        Me.bnt_New.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_New.Name = "bnt_New"
        Me.bnt_New.Size = New System.Drawing.Size(111, 41)
        Me.bnt_New.TabIndex = 16
        Me.bnt_New.Text = "New"
        Me.bnt_New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_New.UseVisualStyleBackColor = False
        '
        'bnt_Save
        '
        Me.bnt_Save.BackColor = System.Drawing.Color.Navy
        Me.bnt_Save.BackgroundImage = CType(resources.GetObject("bnt_Save.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Save.Enabled = False
        Me.bnt_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bnt_Save.ForeColor = System.Drawing.Color.Black
        Me.bnt_Save.Image = Global.MSI_Solution.My.Resources.Resources.save
        Me.bnt_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Save.Location = New System.Drawing.Point(884, 9)
        Me.bnt_Save.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Save.Name = "bnt_Save"
        Me.bnt_Save.Size = New System.Drawing.Size(111, 41)
        Me.bnt_Save.TabIndex = 19
        Me.bnt_Save.Text = "Save"
        Me.bnt_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Save.UseVisualStyleBackColor = False
        '
        'bnt_Edit
        '
        Me.bnt_Edit.BackColor = System.Drawing.Color.Navy
        Me.bnt_Edit.BackgroundImage = CType(resources.GetObject("bnt_Edit.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bnt_Edit.ForeColor = System.Drawing.Color.Black
        Me.bnt_Edit.Image = CType(resources.GetObject("bnt_Edit.Image"), System.Drawing.Image)
        Me.bnt_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Edit.Location = New System.Drawing.Point(652, 9)
        Me.bnt_Edit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Edit.Name = "bnt_Edit"
        Me.bnt_Edit.Size = New System.Drawing.Size(111, 41)
        Me.bnt_Edit.TabIndex = 17
        Me.bnt_Edit.Text = "Edit"
        Me.bnt_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Edit.UseVisualStyleBackColor = False
        '
        'gb_Dtl
        '
        Me.gb_Dtl.Controls.Add(Me.Label16)
        Me.gb_Dtl.Controls.Add(Me.cb_Route)
        Me.gb_Dtl.Controls.Add(Me.wRoute_Desc)
        Me.gb_Dtl.Controls.Add(Me.Label6)
        Me.gb_Dtl.Controls.Add(Me.wRoute_Name)
        Me.gb_Dtl.Controls.Add(Me.Label1)
        Me.gb_Dtl.Controls.Add(Me.dgv_Dtl)
        Me.gb_Dtl.Location = New System.Drawing.Point(31, 409)
        Me.gb_Dtl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gb_Dtl.Name = "gb_Dtl"
        Me.gb_Dtl.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gb_Dtl.Size = New System.Drawing.Size(964, 359)
        Me.gb_Dtl.TabIndex = 11632
        Me.gb_Dtl.TabStop = False
        Me.gb_Dtl.Text = "Sailing Schedule"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(47, 23)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 16)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Routes"
        '
        'cb_Route
        '
        Me.cb_Route.FormattingEnabled = True
        Me.cb_Route.Location = New System.Drawing.Point(105, 18)
        Me.cb_Route.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cb_Route.Name = "cb_Route"
        Me.cb_Route.Size = New System.Drawing.Size(801, 24)
        Me.cb_Route.TabIndex = 13
        '
        'wRoute_Desc
        '
        Me.wRoute_Desc.BackColor = System.Drawing.Color.LightBlue
        Me.wRoute_Desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wRoute_Desc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wRoute_Desc.Location = New System.Drawing.Point(105, 85)
        Me.wRoute_Desc.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wRoute_Desc.Multiline = True
        Me.wRoute_Desc.Name = "wRoute_Desc"
        Me.wRoute_Desc.ReadOnly = True
        Me.wRoute_Desc.Size = New System.Drawing.Size(802, 49)
        Me.wRoute_Desc.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 89)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Description"
        '
        'wRoute_Name
        '
        Me.wRoute_Name.BackColor = System.Drawing.Color.LightBlue
        Me.wRoute_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wRoute_Name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wRoute_Name.Location = New System.Drawing.Point(105, 53)
        Me.wRoute_Name.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wRoute_Name.Name = "wRoute_Name"
        Me.wRoute_Name.ReadOnly = True
        Me.wRoute_Name.Size = New System.Drawing.Size(802, 22)
        Me.wRoute_Name.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Name"
        '
        'dgv_Dtl
        '
        Me.dgv_Dtl.AllowUserToAddRows = False
        Me.dgv_Dtl.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Dtl.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_Dtl.BackgroundColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Dtl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_Dtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Dtl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.leg, Me.Departure, Me.Discharge})
        Me.dgv_Dtl.Location = New System.Drawing.Point(24, 144)
        Me.dgv_Dtl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgv_Dtl.Name = "dgv_Dtl"
        Me.dgv_Dtl.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Dtl.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_Dtl.RowHeadersVisible = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgv_Dtl.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_Dtl.Size = New System.Drawing.Size(883, 208)
        Me.dgv_Dtl.TabIndex = 8
        '
        'leg
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.leg.DefaultCellStyle = DataGridViewCellStyle3
        Me.leg.HeaderText = "Leg #"
        Me.leg.Name = "leg"
        Me.leg.ReadOnly = True
        Me.leg.Width = 40
        '
        'Departure
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.Departure.DefaultCellStyle = DataGridViewCellStyle4
        Me.Departure.HeaderText = "Departure"
        Me.Departure.Name = "Departure"
        Me.Departure.ReadOnly = True
        Me.Departure.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Departure.Width = 300
        '
        'Discharge
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.Discharge.DefaultCellStyle = DataGridViewCellStyle5
        Me.Discharge.HeaderText = "Discharge"
        Me.Discharge.Name = "Discharge"
        Me.Discharge.ReadOnly = True
        Me.Discharge.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Discharge.Width = 300
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.wGross_Tons)
        Me.GroupBox2.Controls.Add(Me.wNet_Tons)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.wLOA)
        Me.GroupBox2.Controls.Add(Me.wDraft)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.wBeam)
        Me.GroupBox2.Location = New System.Drawing.Point(381, 310)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(613, 98)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Technical Data"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(312, 18)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(168, 13)
        Me.Label17.TabIndex = 11627
        Me.Label17.Text = "Length Overall x Breadth Extreme:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 42)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 16)
        Me.Label9.TabIndex = 11618
        Me.Label9.Text = "Gross Tons"
        '
        'wGross_Tons
        '
        Me.wGross_Tons.BackColor = System.Drawing.Color.LightBlue
        Me.wGross_Tons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wGross_Tons.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wGross_Tons.Location = New System.Drawing.Point(104, 37)
        Me.wGross_Tons.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wGross_Tons.MaxLength = 7
        Me.wGross_Tons.Name = "wGross_Tons"
        Me.wGross_Tons.ReadOnly = True
        Me.wGross_Tons.Size = New System.Drawing.Size(121, 22)
        Me.wGross_Tons.TabIndex = 9
        Me.wGross_Tons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'wNet_Tons
        '
        Me.wNet_Tons.BackColor = System.Drawing.Color.LightBlue
        Me.wNet_Tons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wNet_Tons.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wNet_Tons.Location = New System.Drawing.Point(104, 65)
        Me.wNet_Tons.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wNet_Tons.Name = "wNet_Tons"
        Me.wNet_Tons.ReadOnly = True
        Me.wNet_Tons.Size = New System.Drawing.Size(121, 22)
        Me.wNet_Tons.TabIndex = 10
        Me.wNet_Tons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(32, 70)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 16)
        Me.Label10.TabIndex = 11620
        Me.Label10.Text = "Net Tons"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(497, 46)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(36, 16)
        Me.Label13.TabIndex = 11626
        Me.Label13.Text = "Draft"
        '
        'wLOA
        '
        Me.wLOA.BackColor = System.Drawing.Color.LightBlue
        Me.wLOA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wLOA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wLOA.Location = New System.Drawing.Point(273, 65)
        Me.wLOA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wLOA.Name = "wLOA"
        Me.wLOA.ReadOnly = True
        Me.wLOA.Size = New System.Drawing.Size(91, 22)
        Me.wLOA.TabIndex = 11
        Me.wLOA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'wDraft
        '
        Me.wDraft.BackColor = System.Drawing.Color.LightBlue
        Me.wDraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wDraft.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wDraft.Location = New System.Drawing.Point(463, 65)
        Me.wDraft.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wDraft.Name = "wDraft"
        Me.wDraft.ReadOnly = True
        Me.wDraft.Size = New System.Drawing.Size(91, 22)
        Me.wDraft.TabIndex = 13
        Me.wDraft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(309, 46)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(34, 16)
        Me.Label11.TabIndex = 11622
        Me.Label11.Text = "LOA"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(404, 46)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 16)
        Me.Label12.TabIndex = 11624
        Me.Label12.Text = "Beam"
        '
        'wBeam
        '
        Me.wBeam.BackColor = System.Drawing.Color.LightBlue
        Me.wBeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wBeam.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wBeam.Location = New System.Drawing.Point(368, 65)
        Me.wBeam.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wBeam.Name = "wBeam"
        Me.wBeam.ReadOnly = True
        Me.wBeam.Size = New System.Drawing.Size(91, 22)
        Me.wBeam.TabIndex = 12
        Me.wBeam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'bnt_Del
        '
        Me.bnt_Del.BackColor = System.Drawing.Color.Navy
        Me.bnt_Del.BackgroundImage = CType(resources.GetObject("bnt_Del.BackgroundImage"), System.Drawing.Image)
        Me.bnt_Del.Enabled = False
        Me.bnt_Del.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bnt_Del.ForeColor = System.Drawing.Color.Black
        Me.bnt_Del.Image = CType(resources.GetObject("bnt_Del.Image"), System.Drawing.Image)
        Me.bnt_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bnt_Del.Location = New System.Drawing.Point(768, 9)
        Me.bnt_Del.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bnt_Del.Name = "bnt_Del"
        Me.bnt_Del.Size = New System.Drawing.Size(111, 41)
        Me.bnt_Del.TabIndex = 18
        Me.bnt_Del.Text = "Del"
        Me.bnt_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bnt_Del.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.wCountry_Code)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.wVessel_Code)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.wIMO_Nro)
        Me.GroupBox1.Controls.Add(Me.wCaptain)
        Me.GroupBox1.Controls.Add(Me.wFlag)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.wVessel_Name)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.wRegistry)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.wLLoyds)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cb_Country)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lb_Vessel_Code)
        Me.GroupBox1.Location = New System.Drawing.Point(381, 53)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(613, 218)
        Me.GroupBox1.TabIndex = 11631
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vessel Identification"
        '
        'wCountry_Code
        '
        Me.wCountry_Code.BackColor = System.Drawing.Color.Navy
        Me.wCountry_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wCountry_Code.ForeColor = System.Drawing.Color.White
        Me.wCountry_Code.Location = New System.Drawing.Point(397, 52)
        Me.wCountry_Code.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wCountry_Code.MaxLength = 3
        Me.wCountry_Code.Name = "wCountry_Code"
        Me.wCountry_Code.Size = New System.Drawing.Size(38, 22)
        Me.wCountry_Code.TabIndex = 11632
        Me.wCountry_Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Blue
        Me.Label18.Location = New System.Drawing.Point(407, 84)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(86, 16)
        Me.Label18.TabIndex = 11631
        Me.Label18.Text = "( Home Port:)"
        Me.Label18.Visible = False
        '
        'wVessel_Code
        '
        Me.wVessel_Code.BackColor = System.Drawing.Color.LightBlue
        Me.wVessel_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wVessel_Code.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wVessel_Code.Location = New System.Drawing.Point(529, 22)
        Me.wVessel_Code.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wVessel_Code.MaxLength = 6
        Me.wVessel_Code.Name = "wVessel_Code"
        Me.wVessel_Code.ReadOnly = True
        Me.wVessel_Code.Size = New System.Drawing.Size(71, 22)
        Me.wVessel_Code.TabIndex = 1
        Me.wVessel_Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 137)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 16)
        Me.Label4.TabIndex = 11610
        Me.Label4.Text = "IMO Nro."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(40, 188)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 16)
        Me.Label15.TabIndex = 11630
        Me.Label15.Text = "Captain"
        '
        'wIMO_Nro
        '
        Me.wIMO_Nro.BackColor = System.Drawing.Color.LightBlue
        Me.wIMO_Nro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wIMO_Nro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wIMO_Nro.Location = New System.Drawing.Point(100, 132)
        Me.wIMO_Nro.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wIMO_Nro.MaxLength = 10
        Me.wIMO_Nro.Name = "wIMO_Nro"
        Me.wIMO_Nro.ReadOnly = True
        Me.wIMO_Nro.Size = New System.Drawing.Size(105, 22)
        Me.wIMO_Nro.TabIndex = 5
        Me.wIMO_Nro.Text = " "
        '
        'wCaptain
        '
        Me.wCaptain.BackColor = System.Drawing.Color.LightBlue
        Me.wCaptain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wCaptain.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wCaptain.Location = New System.Drawing.Point(100, 183)
        Me.wCaptain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wCaptain.Name = "wCaptain"
        Me.wCaptain.ReadOnly = True
        Me.wCaptain.Size = New System.Drawing.Size(297, 22)
        Me.wCaptain.TabIndex = 7
        '
        'wFlag
        '
        Me.wFlag.BackColor = System.Drawing.Color.LightBlue
        Me.wFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wFlag.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wFlag.ForeColor = System.Drawing.Color.Black
        Me.wFlag.Location = New System.Drawing.Point(100, 106)
        Me.wFlag.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wFlag.Name = "wFlag"
        Me.wFlag.ReadOnly = True
        Me.wFlag.Size = New System.Drawing.Size(289, 22)
        Me.wFlag.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(60, 111)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 16)
        Me.Label7.TabIndex = 11616
        Me.Label7.Text = "Flag"
        '
        'wVessel_Name
        '
        Me.wVessel_Name.BackColor = System.Drawing.Color.LightBlue
        Me.wVessel_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wVessel_Name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wVessel_Name.Location = New System.Drawing.Point(100, 23)
        Me.wVessel_Name.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wVessel_Name.Name = "wVessel_Name"
        Me.wVessel_Name.ReadOnly = True
        Me.wVessel_Name.Size = New System.Drawing.Size(287, 22)
        Me.wVessel_Name.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(49, 28)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 16)
        Me.Label8.TabIndex = 11603
        Me.Label8.Text = "Name"
        '
        'wRegistry
        '
        Me.wRegistry.BackColor = System.Drawing.Color.LightBlue
        Me.wRegistry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wRegistry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wRegistry.Location = New System.Drawing.Point(100, 79)
        Me.wRegistry.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wRegistry.Name = "wRegistry"
        Me.wRegistry.ReadOnly = True
        Me.wRegistry.Size = New System.Drawing.Size(289, 22)
        Me.wRegistry.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 84)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 16)
        Me.Label5.TabIndex = 11612
        Me.Label5.Text = "Registry"
        '
        'wLLoyds
        '
        Me.wLLoyds.BackColor = System.Drawing.Color.LightBlue
        Me.wLLoyds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wLLoyds.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.wLLoyds.Location = New System.Drawing.Point(100, 158)
        Me.wLLoyds.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.wLLoyds.MaxLength = 10
        Me.wLLoyds.Name = "wLLoyds"
        Me.wLLoyds.ReadOnly = True
        Me.wLLoyds.Size = New System.Drawing.Size(105, 22)
        Me.wLLoyds.TabIndex = 6
        Me.wLLoyds.Text = " "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 162)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 16)
        Me.Label3.TabIndex = 11608
        Me.Label3.Text = "LLOYDS"
        '
        'cb_Country
        '
        Me.cb_Country.BackColor = System.Drawing.Color.LightBlue
        Me.cb_Country.Enabled = False
        Me.cb_Country.FormattingEnabled = True
        Me.cb_Country.Location = New System.Drawing.Point(100, 50)
        Me.cb_Country.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cb_Country.Name = "cb_Country"
        Me.cb_Country.Size = New System.Drawing.Size(288, 24)
        Me.cb_Country.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 1002
        Me.Label2.Text = "Country "
        '
        'lb_Vessel_Code
        '
        Me.lb_Vessel_Code.AutoSize = True
        Me.lb_Vessel_Code.Location = New System.Drawing.Point(415, 27)
        Me.lb_Vessel_Code.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_Vessel_Code.Name = "lb_Vessel_Code"
        Me.lb_Vessel_Code.Size = New System.Drawing.Size(105, 16)
        Me.lb_Vessel_Code.TabIndex = 11602
        Me.lb_Vessel_Code.Text = "Code (Call Sign)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(400, 281)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(46, 16)
        Me.Label14.TabIndex = 11628
        Me.Label14.Text = "Owner"
        '
        'cb_Owner
        '
        Me.cb_Owner.BackColor = System.Drawing.Color.LightBlue
        Me.cb_Owner.Enabled = False
        Me.cb_Owner.FormattingEnabled = True
        Me.cb_Owner.Location = New System.Drawing.Point(453, 277)
        Me.cb_Owner.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cb_Owner.Name = "cb_Owner"
        Me.cb_Owner.Size = New System.Drawing.Size(333, 24)
        Me.cb_Owner.TabIndex = 8
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage2.Controls.Add(Me.cv_Vessels)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Size = New System.Drawing.Size(1031, 780)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Report"
        '
        'cv_Vessels
        '
        Me.cv_Vessels.ActiveViewIndex = -1
        Me.cv_Vessels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cv_Vessels.Cursor = System.Windows.Forms.Cursors.Default
        Me.cv_Vessels.Location = New System.Drawing.Point(4, 4)
        Me.cv_Vessels.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cv_Vessels.Name = "cv_Vessels"
        Me.cv_Vessels.Size = New System.Drawing.Size(1015, 935)
        Me.cv_Vessels.TabIndex = 3
        Me.cv_Vessels.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.LightCyan
        Me.TabPage3.Controls.Add(Me.dgv_Audit)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1031, 780)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Audit"
        '
        'dgv_Audit
        '
        Me.dgv_Audit.AllowDrop = True
        Me.dgv_Audit.AllowUserToOrderColumns = True
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Blue
        Me.dgv_Audit.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_Audit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgv_Audit.BackgroundColor = System.Drawing.Color.PaleTurquoise
        Me.dgv_Audit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Audit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.A_Date, Me.A_Time, Me.A_Made, Me.DataGridViewTextBoxColumn11, Me.A_Old_value})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Audit.DefaultCellStyle = DataGridViewCellStyle14
        Me.dgv_Audit.GridColor = System.Drawing.Color.Silver
        Me.dgv_Audit.Location = New System.Drawing.Point(64, 7)
        Me.dgv_Audit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgv_Audit.Name = "dgv_Audit"
        Me.dgv_Audit.ReadOnly = True
        Me.dgv_Audit.RowHeadersVisible = False
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Audit.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.dgv_Audit.RowTemplate.Height = 24
        Me.dgv_Audit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv_Audit.Size = New System.Drawing.Size(888, 649)
        Me.dgv_Audit.TabIndex = 11560
        Me.dgv_Audit.TabStop = False
        '
        'A_Date
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle9.NullValue = Nothing
        Me.A_Date.DefaultCellStyle = DataGridViewCellStyle9
        Me.A_Date.HeaderText = "Date"
        Me.A_Date.MaxInputLength = 15
        Me.A_Date.Name = "A_Date"
        Me.A_Date.ReadOnly = True
        Me.A_Date.Width = 80
        '
        'A_Time
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.A_Time.DefaultCellStyle = DataGridViewCellStyle10
        Me.A_Time.HeaderText = "Time"
        Me.A_Time.Name = "A_Time"
        Me.A_Time.ReadOnly = True
        Me.A_Time.Width = 80
        '
        'A_Made
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.A_Made.DefaultCellStyle = DataGridViewCellStyle11
        Me.A_Made.HeaderText = "Made By"
        Me.A_Made.Name = "A_Made"
        Me.A_Made.ReadOnly = True
        Me.A_Made.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn11.HeaderText = " Description"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn11.Width = 240
        '
        'A_Old_value
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.A_Old_value.DefaultCellStyle = DataGridViewCellStyle13
        Me.A_Old_value.HeaderText = "Before"
        Me.A_Old_value.Name = "A_Old_value"
        Me.A_Old_value.ReadOnly = True
        Me.A_Old_value.Width = 160
        '
        'ds_Vessels
        '
        Me.ds_Vessels.DataSetName = "NewDataSet"
        '
        'ds_Country
        '
        Me.ds_Country.DataSetName = "NewDataSet"
        '
        'ds_Owner
        '
        Me.ds_Owner.DataSetName = "NewDataSet"
        '
        'DsVessels_x_Line
        '
        Me.DsVessels_x_Line.DataSetName = "NewDataSet"
        '
        'Vessels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1044, 814)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Vessels"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Vessels"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.gb_Dtl.ResumeLayout(False)
        Me.gb_Dtl.PerformLayout()
        CType(Me.dgv_Dtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgv_Audit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds_Vessels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds_Country, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds_Owner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsVessels_x_Line, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cb_Vessel As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ds_Vessels As DataSet
    Friend WithEvents Label2 As Label
    Friend WithEvents cb_Country As ComboBox
    Friend WithEvents ds_Country As DataSet
    Friend WithEvents Label8 As Label
    Friend WithEvents wVessel_Name As TextBox
    Friend WithEvents lb_Vessel_Code As Label
    Friend WithEvents bnt_New As Button
    Friend WithEvents bnt_Save As Button
    Friend WithEvents bnt_Edit As Button
    Friend WithEvents bnt_Del As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents wRegistry As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents wIMO_Nro As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents wLLoyds As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents wFlag As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents wNet_Tons As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents wGross_Tons As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents wDraft As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents wBeam As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents wLOA As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents cb_Owner As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents wCaptain As TextBox
    Friend WithEvents ds_Owner As DataSet
    Friend WithEvents cv_Vessels As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents gb_Dtl As GroupBox
    Friend WithEvents dgv_Dtl As DataGridView
    Friend WithEvents wRoute_Desc As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents leg As DataGridViewTextBoxColumn
    Friend WithEvents Departure As DataGridViewTextBoxColumn
    Friend WithEvents Discharge As DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents dgv_Audit As DataGridView
    Friend WithEvents A_Date As DataGridViewTextBoxColumn
    Friend WithEvents A_Time As DataGridViewTextBoxColumn
    Friend WithEvents A_Made As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents A_Old_value As DataGridViewTextBoxColumn
    Friend WithEvents DsVessels_x_Line As DataSet
    Friend WithEvents wVessel_Code As TextBox
    Friend WithEvents cb_Route As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents wRoute_Name As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents wCountry_Code As TextBox
End Class
