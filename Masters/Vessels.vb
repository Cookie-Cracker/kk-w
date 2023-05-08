Imports System.ComponentModel

Public Class Vessels
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1

    Private Sub Vessels_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, Me.Name)) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        If Trim(md.Progran_ReadOnly(UserCode, Me.Name)) = "Y" Then
            Me.bnt_New.Visible = False
            Me.bnt_Save.Visible = False
            Me.bnt_Del.Visible = False
            Me.bnt_Edit.Visible = False
        End If
        Try
            Dim strSQL As String = "SELECT [VESSEL_NAME], isnull(Voyage_Number,0) as Voyage_Number
                          ,[CAPTAIN]
                          ,[NATIONALITY]
                          ,[OFFICIAL_NO]
                          ,[GROSS_TONS]
                          ,[NET_TONS]
                          ,[REGISTRY]
                          ,[VESSEL_NUM]
                          ,[VESSEL_SHORT]
                          ,[CREW]
                          ,[LLOYDS_CODE]
                          ,[COUNTRY_CODE]
                          ,[LOA]
                          ,[DRAFT]
                          ,[BEAM]
                           ,[OWNER_NUM]
                           ,uid
                           FROM [Vessels] where LINE_NUMBER = 1 and [Delete] is NULL order by VESSEL_NAME"

            ds_Vessels.Clear()
            ds_Vessels = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_Vessels.Tables(0).Rows.Count > 0 Then
                Me.cb_Vessel.DataSource = ds_Vessels.Tables(0)
                Me.cb_Vessel.DisplayMember = "Vessel_Name"
                Me.cb_Vessel.ValueMember = "Vessel_Name"
                Me.cb_Vessel.Refresh()
            Else
                MsgBox("Vessel file is empty,...")
            End If

            ds_Country.Clear()
            ds_Country = ws.GetDataset(md.strConnect, "SELECT uid,COUNTRY,A2_ISO,A3_UN,UN_nro FROM Country_Master Order by Country", 1)
            If ds_Country.Tables(0).Rows.Count > 0 Then
                Me.cb_Country.DataSource = ds_Country.Tables(0)
                Me.cb_Country.DisplayMember = "Country"
                Me.cb_Country.ValueMember = "A2_ISO"
            End If

            ds_Owner.Clear()
            ds_Owner = ws.GetDataset(md.strConnect, "SELECT Distinct Company_Number, Company_name FROM CM_System Where Vessel_Owner = 'Y' Order by Company_Name", 1)
            If ds_Owner.Tables(0).Rows.Count > 0 Then
                Me.cb_Owner.DataSource = ds_Owner.Tables(0)
                Me.cb_Owner.DisplayMember = "Company_Name"
                Me.cb_Owner.ValueMember = "Company_Number"
            Else
                MsgBox("The CMS does not have vessel owner,...")
            End If

            Me.Modo_Edit_ADD_Read_Only(1)
            md.Insert_User_Log("Load Vessel", md.UserName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear()
        Me.wVessel_Name.Clear()
        Me.wVessel_Code.Clear()
        Me.wBeam.Clear()
        Me.wCaptain.Clear()
        Me.wDraft.Clear()
        Me.wFlag.Clear()
        Me.wGross_Tons.Clear()
        Me.wIMO_Nro.Clear()
        Me.wLLoyds.Clear()
        Me.wLOA.Clear()
        Me.wNet_Tons.Clear()
        Me.wRegistry.Clear()

        wRoute_Desc.Clear()
        Me.wRoute_Name.Clear()
        Me.cb_Route.SelectedValue = 1

        Me.cb_Owner.SelectedItem = ""
        Me.cb_Country.SelectedValue = ""

        Me.dgv_Dtl.Rows.Clear()
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If Flag_Modo = 1 Then
            Me.bnt_Del.Enabled = False
            Me.bnt_Save.Enabled = False
            Me.wVessel_Name.ReadOnly = True
            Me.wVessel_Code.ReadOnly = True
            Me.wBeam.ReadOnly = True
            Me.wCaptain.ReadOnly = True
            Me.wDraft.ReadOnly = True
            Me.wFlag.ReadOnly = True
            Me.wGross_Tons.ReadOnly = True
            Me.wIMO_Nro.ReadOnly = True
            Me.wLLoyds.ReadOnly = True
            Me.wLOA.ReadOnly = True
            Me.wNet_Tons.ReadOnly = True
            Me.wRegistry.ReadOnly = True
            Me.cb_Owner.Enabled = False
            Me.cb_Country.Enabled = False

            Me.dgv_Dtl.ReadOnly = True

            ' ------- Color
            Me.wVessel_Name.BackColor = Color.LightBlue
            Me.wVessel_Code.BackColor = Color.LightBlue
            Me.wBeam.BackColor = Color.LightBlue
            Me.wCaptain.BackColor = Color.LightBlue
            Me.wDraft.BackColor = Color.LightBlue
            Me.wFlag.BackColor = Color.LightBlue
            Me.wGross_Tons.BackColor = Color.LightBlue
            Me.wIMO_Nro.BackColor = Color.LightBlue
            Me.wLLoyds.BackColor = Color.LightBlue
            Me.wLOA.BackColor = Color.LightBlue
            Me.wNet_Tons.BackColor = Color.LightBlue
            Me.wRegistry.BackColor = Color.LightBlue

            Me.cb_Owner.BackColor = Color.LightBlue
            'Me.cb_Vessel.BackColor = Color.LightBlue
            Me.cb_Country.BackColor = Color.LightBlue

            Me.dgv_Dtl.BackgroundColor = Color.PaleTurquoise
            Me.dgv_Dtl.DefaultCellStyle.BackColor = Color.PaleTurquoise
        Else
            Me.bnt_Del.Enabled = True
            Me.bnt_Save.Enabled = True

            Me.wVessel_Name.ReadOnly = False
            Me.wVessel_Code.ReadOnly = False
            Me.wBeam.ReadOnly = False
            Me.wCaptain.ReadOnly = False
            Me.wDraft.ReadOnly = False
            Me.wFlag.ReadOnly = False
            Me.wGross_Tons.ReadOnly = False
            Me.wIMO_Nro.ReadOnly = False
            Me.wLLoyds.ReadOnly = False
            Me.wLOA.ReadOnly = False
            Me.wNet_Tons.ReadOnly = False
            Me.wRegistry.ReadOnly = False

            Me.cb_Owner.Enabled = True
            'Me.cb_Vessel.Enabled = True
            Me.cb_Country.Enabled = True

            Me.dgv_Dtl.ReadOnly = False
            ' ------- Color
            Me.wVessel_Name.BackColor = Color.White
            Me.wVessel_Code.BackColor = Color.White
            Me.wBeam.BackColor = Color.White
            Me.wCaptain.BackColor = Color.White
            Me.wDraft.BackColor = Color.White
            Me.wFlag.BackColor = Color.White
            Me.wGross_Tons.BackColor = Color.White
            Me.wIMO_Nro.BackColor = Color.White
            Me.wLLoyds.BackColor = Color.White
            Me.wLOA.BackColor = Color.White
            Me.wNet_Tons.BackColor = Color.White
            Me.wRegistry.BackColor = Color.White

            Me.cb_Owner.BackColor = Color.White
            'Me.cb_Vessel.BackColor = Color.White
            Me.cb_Country.BackColor = Color.White

        End If
    End Sub

    Private Sub cb_Vessel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Vessel.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 1
            Me.Clear()
            Me.wVessel_Code.Text = Me.cb_Vessel.SelectedItem("VESSEL_Short")
            Me.wVessel_Name.Text = Me.cb_Vessel.SelectedItem("VESSEL_Name")
            Me.wFlag.Text = Me.cb_Vessel.SelectedItem("Nationality")
            Me.wCaptain.Text = Me.cb_Vessel.SelectedItem("Captain")
            Me.wLLoyds.Text = Me.cb_Vessel.SelectedItem("Lloyds_Code")
            Me.wIMO_Nro.Text = Me.cb_Vessel.SelectedItem("OFFICIAL_NO")
            Me.wRegistry.Text = Me.cb_Vessel.SelectedItem("Registry")
            Me.wGross_Tons.Text = Format(Me.cb_Vessel.SelectedItem("Gross_Tons"), "###,###,###.##")
            Me.wNet_Tons.Text = Format(cb_Vessel.SelectedItem("Net_Tons"), "###,###,###.##")
            Me.wLOA.Text = Format(Me.cb_Vessel.SelectedItem("LOA"), "###,###,###.##")
            Me.wBeam.Text = Format(Me.cb_Vessel.SelectedItem("Beam"), "###,###,###.##")
            Me.wDraft.Text = Format(Me.cb_Vessel.SelectedItem("Draft"), "###,###,###.##")

            Me.cb_Country.SelectedValue = Me.cb_Vessel.SelectedItem("Country_Code")
            Me.cb_Owner.SelectedValue = Me.cb_Vessel.SelectedItem("Owner_Num")

            Me.Refresh_Route()

            Me.Audit_Search()

            Me.Modo_Edit_ADD_Read_Only(1)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Refrehs_Vessel()
        Dim strSQL As String = "SELECT [VESSEL_NAME],isnull(Voyage_Number,0) as Voyage_Number
                                      ,[CAPTAIN]
                                      ,[NATIONALITY]
                                      ,[OFFICIAL_NO]
                                      ,[GROSS_TONS]
                                      ,[NET_TONS]
                                      ,[REGISTRY]
                                      ,[VESSEL_NUM]
                                       ,[VESSEL_SHORT]
                                      ,[CREW]
                                      ,[LLOYDS_CODE]
                                      ,[COUNTRY_CODE]
                                      ,[LOA]
                                      ,[DRAFT]
                                      ,[BEAM]
                                       ,[OWNER_NUM]
                                       ,uid
                                FROM [Vessels] 
                                     where LINE_NUMBER = 1 and [Delete] is NULL 
                                order by VESSEL_NAME"
        ds_Vessels.Clear()
        ds_Vessels = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_Vessels.Tables(0).Rows.Count > 0 Then
            Me.cb_Vessel.DataSource = ds_Vessels.Tables(0)
            Me.cb_Vessel.DisplayMember = "Vessel_Name"
            Me.cb_Vessel.ValueMember = "Vessel_Name"
            Me.cb_Vessel.Refresh()
        End If
    End Sub

#Region "Validations"
    Private Sub wGross_Tons_Validating(sender As Object, e As CancelEventArgs) Handles wGross_Tons.Validating
        If Not IsNumeric(Me.wGross_Tons.Text) Then
            MsgBox("The Field must be numeric,... ")
            Me.wGross_Tons.Clear()
            Me.wGross_Tons.Focus()
        End If
    End Sub

    Private Sub wNet_Tons_Validating(sender As Object, e As CancelEventArgs) Handles wNet_Tons.Validating
        If Not IsNumeric(Me.wNet_Tons.Text) Then
            MsgBox("The Field must be numeric,... ")
            Me.wNet_Tons.Clear()
            Me.wNet_Tons.Focus()
        End If
    End Sub

    Private Sub wLOA_Validating(sender As Object, e As CancelEventArgs) Handles wLOA.Validating
        If Not IsNumeric(Me.wLOA.Text) Then
            MsgBox("The Field must be numeric,... ")
            Me.wLOA.Clear()
            Me.wLOA.Focus()
        End If
    End Sub

    Private Sub wDraft_Validating(sender As Object, e As CancelEventArgs) Handles wDraft.Validating
        If Not IsNumeric(Me.wDraft.Text) Then
            MsgBox("The Field must be numeric,... ")
            Me.wDraft.Clear()
            Me.wDraft.Focus()
        End If
    End Sub

    Private Sub wBeam_Validating(sender As Object, e As CancelEventArgs) Handles wBeam.Validating
        If Not IsNumeric(Me.wBeam.Text) Then
            MsgBox("The Field must be numeric,... ")
            Me.wBeam.Clear()
            Me.wBeam.Focus()
        End If
    End Sub
#End Region

    Private Sub Refresh_Report()
        If Me.DsVessels_x_Line.Tables(0).Rows.Count > 0 Then
            Dim cr_Vessels As New cr_Vessels
            cr_Vessels.SetDataSource(ds_Vessels)
            Me.cv_Vessels.CloseView(cr_Vessels)
            Me.cv_Vessels.ReportSource = cr_Vessels
            Me.cv_Vessels.BringToFront()
            Me.cv_Vessels.RefreshReport()
            Me.cv_Vessels.DisplayToolbar = True
            Me.cv_Vessels.DisplayStatusBar = True
            'Me.cv_Sailing_Schedule.Zoom(75)
            Me.cv_Vessels.Refresh()
            Me.cv_Vessels.Show()
        End If
    End Sub

#Region "Update"
    Private Sub cb_Country_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Country.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                ' ------ Update Route
                Dim vResp As String = ""
                vResp = ws.ExecSQL(md.strConnect, "Update Ports Set Country = '" & Trim(Me.cb_Country.SelectedValue) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                If Trim(vResp) <> "Success" Then
                    MsgBox(vResp)
                    Exit Sub
                End If
                Me.DsVessels_x_Line.Clear()
                DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                Me.Refrehs_Vessel()
                Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
            Else
                If Flag_Modo = 3 Then
                    Me.wFlag.Text = Trim(cb_Country.SelectedItem("Country"))
                    Me.wRegistry.Text = Trim(cb_Country.SelectedItem("Country"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Owner_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Owner.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                ' ------ Update Route
                Dim vResp As String = ""
                vResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Owner_Num = '" & Trim(Me.cb_Owner.SelectedValue) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                If Trim(vResp) <> "Success" Then
                    MsgBox(vResp)
                    Exit Sub
                End If
                Me.DsVessels_x_Line.Clear()
                DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                Me.Refrehs_Vessel()
                Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
            Else
                If Flag_Modo = 3 Then
                    'Me.wCountry_Code.Text = cb_Country.SelectedItem("A2_ISO")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#End Region
#Region "Audit"
    Private Sub Audit_Search()
        If Len(Trim(Me.wVessel_Name.Text)) > 0 Then
            ' ------- Audit
            Dim vCreated_By As String
            Dim vCreation_Date As String
            Dim vCreation_Time As String
            Dim vDesc As String
            Dim vOLD As String
            Dim nSeq As Integer = 0

            Dim ds As New DataSet
            ds = md.Vessel_Audit_BY_Vessel(Trim(Me.wVessel_Name.Text))
            If ds.Tables(0).Rows.Count > 0 Then
                Me.dgv_Audit.Rows.Clear()
                Dim i As Integer = 0
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    vOLD = ""
                    vCreated_By = ""
                    vCreation_Date = ""
                    vCreation_Time = ""
                    vDesc = ""
                    nSeq = 0
                    If Len(Trim(ds.Tables(0).Rows(i).Item("Created_Date"))) > 0 Then
                        vCreation_Date = Trim(ds.Tables(0).Rows(i).Item("Created_Date"))
                    End If
                    If Len(Trim(ds.Tables(0).Rows(i).Item("Created_Time"))) > 0 Then
                        vCreation_Time = Trim(ds.Tables(0).Rows(i).Item("Created_Time"))
                    End If
                    If Len(Trim(ds.Tables(0).Rows(i).Item("Created_By"))) > 0 Then
                        vCreated_By = Trim(ds.Tables(0).Rows(i).Item("Created_By"))
                    End If
                    If Len(Trim(ds.Tables(0).Rows(i).Item("Description"))) > 0 Then
                        vDesc = Trim(ds.Tables(0).Rows(i).Item("Description"))
                    End If
                    If Len(Trim(ds.Tables(0).Rows(i).Item("Old_Value"))) > 0 Then
                        vOLD = Trim(ds.Tables(0).Rows(i).Item("Old_Value"))
                    End If
                    Me.dgv_Audit.Rows.Add(vCreation_Date, vCreation_Time, vCreated_By, vDesc, vOLD)
                Next
                ds = Nothing
                i = Nothing
                vCreated_By = Nothing
                vCreation_Date = Nothing
                vDesc = Nothing
                vOLD = Nothing
                vCreation_Time = Nothing
                nSeq = Nothing
            End If

        End If
    End Sub
#End Region

#Region "Commands"
    Private Sub bnt_New_Click(sender As Object, e As EventArgs) Handles bnt_New.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 3
            Me.Clear()
            Me.Modo_Edit_ADD_Read_Only(3)
            Me.wVessel_Name.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo <> 2 Then
            Flag_Modo = 2
            Dim vVessel As String = ""
            vVessel = Trim(Me.wVessel_Name.Text)
            Me.Modo_Edit_ADD_Read_Only(2)
            Me.wVessel_Name.Text = Trim(vVessel)
            Me.wVessel_Name.BackColor = Color.White
            Me.wVessel_Name.ReadOnly = True
            vVessel = Nothing
        Else
            Flag_Modo = 1
            Me.Modo_Edit_ADD_Read_Only(1)
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            ' ------- Validation
            If Len(Trim(Me.wVessel_Name.Text)) = 0 Then
                MsgBox("Name field is empty,..")
                Me.wVessel_Name.Focus()
                Exit Sub
            End If
            'If Len(Trim(Me.wVessel_Nro.Text)) = 0 Then
            '    MsgBox("Nro. field is empty,..")
            '    Me.wVessel_Nro.Focus()
            '    Exit Sub
            'End If
            If Flag_Modo = 3 Then
                Dim strSql As String = ""
                Dim vResp As String = ""
                Dim nGross As Double = 0
                Dim nNet As Double = 0
                Dim nLOA As Double = 0
                Dim nDraft As Double = 0
                Dim nBeam As Double = 0
                If Len(Trim(Me.wGross_Tons.Text)) > 0 Then
                    nGross = Me.wGross_Tons.Text
                End If
                If Len(Trim(Me.wNet_Tons.Text)) > 0 Then
                    nNet = Me.wNet_Tons.Text
                End If
                If Len(Trim(Me.wLOA.Text)) > 0 Then
                    nLOA = Me.wLOA.Text
                End If
                If Len(Trim(Me.wDraft.Text)) > 0 Then
                    nDraft = Me.wDraft.Text
                End If
                If Len(Trim(Me.wBeam.Text)) > 0 Then
                    nBeam = Me.wBeam.Text
                End If
                Dim ds As New DataSet

                ds = ws.GetDataset(md.strConnect, "Select Vessel_Short From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Short = '" & Trim(Me.wVessel_Code.Text) & "' and Vessel_Name = '" & Trim(Me.wVessel_Name.Text) & "'", 1)
                If ds.Tables(0).Rows.Count = 0 Then
                    strSql = "Insert Into Vessels (Line_Number,
                                                    Line_name,
                                                     Vessel_Short,
                                                    Vessel_Name,
                                                    Captain,
                                                    Nationality,
                                                    Official_No,
                                                    Registry,
                                                    LLoyds_Code,
                                                    COUNTRY_CODE,
                                                    Gross_Tons,
                                                    Net_Tons,
                                                    LOA,Draft,Beam,
                                                    Owner_Num,
                                                 Created_By, Created_On) Values (" &
                                                 md.GL_Company & ",'" &
                                                 Trim(md.Line_default_name) & "','" &
                                                 Trim(Me.wVessel_Code.Text) & "','" &
                                                 Trim(Me.wVessel_Name.Text) & "','" &
                                                 Trim(Me.wCaptain.Text) & "','" &
                                                 Trim(Me.wFlag.Text) & "','" &
                                                 Trim(Me.wIMO_Nro.Text) & "','" &
                                                 Trim(Me.wRegistry.Text) & "','" &
                                                 Me.wLLoyds.Text & "','" &
                                                 cb_Country.SelectedValue & "'," &
                                                 nGross & "," &
                                                 nNet & "," &
                                                 nLOA & "," &
                                                 nDraft & "," &
                                                 nBeam & "," &
                                                 Me.cb_Owner.SelectedValue & ",'" &
                                                 Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "')"
                    'MsgBox(strSql)
                    vResp = ws.ExecSQL(md.strConnect, strSql)
                    Me.Refrehs_Vessel()
                    Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Modo_Edit_ADD_Read_Only(1)
                Else
                    MsgBox("This Vessel alredy exists,...")
                    Exit Sub
                End If
                ds = Nothing
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Del_Click(sender As Object, e As EventArgs) Handles bnt_Del.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                If Len(Trim(Me.wVessel_Name.Text)) = 0 Then
                    MsgBox("Vessel field is empty,....")
                    Me.wVessel_Name.Focus()
                    Exit Sub
                End If
                ' ------ Update [Delete] 
                Dim vResp As String = ""
                vResp = ws.ExecSQL(md.strConnect, "Update Vessels Set [Delete] = 'Y' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                Me.DsVessels_x_Line.Clear()
                DsVessels_x_Line = ws.sp_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                Me.Refrehs_Vessel()
                Flag_Modo = 1
                Modo_Edit_ADD_Read_Only(1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#End Region


    Public ds_Routes As New DataSet
    Private Sub Refresh_Route()
        Dim j As Integer = 0
        ds_Routes.Clear()
        strSQL = "Select Distinct v.Route_ID, h.Name From Sailing_Master as v Inner Join
                                Route_Hdr as h on h.Route_ID = v.Route_ID
                Where Vessel_Name = '" & cb_Vessel.SelectedItem("Vessel_name") & "'"
        ds_Routes = ws.GetDataset(md.strConnect, strSQL, 1)
        Me.dgv_Dtl.Rows.Clear()
        Me.wRoute_Desc.Clear()
        Me.cb_Route.DataSource = ds_Routes.Tables(0)
        Me.cb_Route.DisplayMember = "Name"
        Me.cb_Route.ValueMember = "Route_ID"

        If ds_Routes.Tables(0).Rows.Count > 0 Then
            Dim nRoute_ID As Integer = 0
            nRoute_ID = ds_Routes.Tables(0).Rows(0).Item("Route_ID")
            Me.Route_Selected(nRoute_ID)
        End If
    End Sub

    Private Sub Route_Selected(ByVal nRoute_ID As Integer)
        Me.dgv_Dtl.Rows.Clear()
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Name, Description From Route_HDR Where Route_ID = " & nRoute_ID, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.wRoute_Name.Text = Trim(ds.Tables(0).Rows(0).Item("Name"))
            Me.wRoute_Desc.Text = Trim(ds.Tables(0).Rows(0).Item("Description"))
            ds.Clear()
            ds = ws.GetDataset(md.strConnect, "Select * From Route_DTL Where Route_ID = " & nRoute_ID, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Dtl.Rows.Add(ds.Tables(0).Rows(j).Item("Leg_Nro"), ds.Tables(0).Rows(j).Item("PortL_Name"), ds.Tables(0).Rows(j).Item("PortD_Name"))
                Next
            End If
        End If
    End Sub

#Region "Changes"
    Private Sub Insert_Changes(ByVal Desc As String, ByVal Vessel_Name As String, ByVal Old_value As String)
        Dim vResp As String = ""
        vResp = ws.ExecSQL(md.strConnect, "Insert Into VesselAudit (Created_Date, 
                                               Created_Time, 
                                               Created_By, 
                                               Description, 
                                               Vessel_Name, 
                                               Old_Value
                                                     ) Values ('" &
                                  Format(System.DateTime.Today, "MM/dd/yyyy") & "','" &
                                  Format(System.DateTime.Now, "hh:mm") & "','" &
                                  Trim(System.Environment.UserName) & "','" &
                                  Trim(Desc) & "','" &
                                  Trim(Vessel_Name) & "','" &
                                  Trim(Old_value) & "')")
        If vResp <> "Success" Then
            MsgBox(Trim(vResp))
        End If
        vResp = Nothing
    End Sub

    Private Sub wFlag_KeyDown(sender As Object, e As KeyEventArgs) Handles wFlag.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wFlag.Text)) > 0 Then
                        ' ------ Update Nationality
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Nationality,'') as Nationality From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Nationality")) <> Trim(Me.wFlag.Text) Then
                                If Len(Trim(ds.Tables(0).Rows(0).Item("Nationality"))) = 0 Then
                                    vDesc = "Added Nationality"
                                Else
                                    vDesc = "Changed Nationality"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Nationality = '" & Trim(Me.wFlag.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Nationality"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Flag # field is empty,...")
                        Me.wFlag.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wCaptain_KeyDown(sender As Object, e As KeyEventArgs) Handles wCaptain.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wCaptain.Text)) > 0 Then
                        ' ------ Update Route
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Captain,'') as Captain From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Captain")) <> Trim(Me.wCaptain.Text) Then
                                If Len(Trim(ds.Tables(0).Rows(0).Item("Captain"))) = 0 Then
                                    vDesc = "Added Captain"
                                Else
                                    vDesc = "Changed Captain"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Captain = '" & Trim(Me.wCaptain.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Captain"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Captain # field is empty,...")
                        Me.wCaptain.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wLLoyds_KeyDown(sender As Object, e As KeyEventArgs) Handles wLLoyds.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wLLoyds.Text)) > 0 Then
                        ' ------ Update Route
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(LLoyds_Code,'') as LLoyds_Code From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("LLoyds_Code")) <> Trim(Me.wLLoyds.Text) Then
                                If Len(Trim(ds.Tables(0).Rows(0).Item("LLoyds_Code"))) = 0 Then
                                    vDesc = "Added LLoyds Code"
                                Else
                                    vDesc = "Changed LLoyds Code"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set LLoyds_Code = '" & Trim(Me.wLLoyds.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("LLoyds_Code"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Lloyds field is empty,...")
                        Me.wLLoyds.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wIMO_Nro_KeyDown(sender As Object, e As KeyEventArgs) Handles wIMO_Nro.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wIMO_Nro.Text)) > 0 Then
                        ' ------ Update Route
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Official_No,'') as Official_No From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Official_No")) <> Trim(Me.wIMO_Nro.Text) Then
                                If Len(Trim(ds.Tables(0).Rows(0).Item("Official_No"))) = 0 Then
                                    vDesc = "Added Official No"
                                Else
                                    vDesc = "Changed Official No"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Official_No = '" & Trim(Me.wIMO_Nro.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Official_No"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("IMO # field is empty,...")
                        Me.wIMO_Nro.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wRegistry_KeyDown(sender As Object, e As KeyEventArgs) Handles wRegistry.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wRegistry.Text)) > 0 Then
                        ' ------ Update Route
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Registry,'') as Registry From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Registry")) <> Trim(Me.wRegistry.Text) Then
                                If Len(Trim(ds.Tables(0).Rows(0).Item("Registry"))) = 0 Then
                                    vDesc = "Added Registry"
                                Else
                                    vDesc = "Changed Registry"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Registry = '" & Trim(Me.wRegistry.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Registry"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Registry field is empty,...")
                        Me.wRegistry.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wGross_Tons_KeyDown(sender As Object, e As KeyEventArgs) Handles wGross_Tons.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wGross_Tons.Text)) > 0 Then
                        ' ------ Update Route
                        Dim nGross As Double = 0
                        If Len(Trim(Me.wGross_Tons.Text)) <> 0 Then
                            nGross = Me.wGross_Tons.Text
                        End If
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Gross_Tons,0) as Gross_Tons From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("Gross_Tons") <> Me.wNet_Tons.Text Then
                                If ds.Tables(0).Rows(0).Item("Gross_Tons") = 0 Then
                                    vDesc = "Added Gross Tons"
                                Else
                                    vDesc = "Changed Gross Tons"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Gross_Tons = " & nGross & " Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Gross Tons"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Gross field is empty,...")
                        Me.wGross_Tons.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wNet_Tons_KeyDown(sender As Object, e As KeyEventArgs) Handles wNet_Tons.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wNet_Tons.Text)) > 0 Then
                        ' ------ Update Route
                        Dim nNet As Double = 0
                        If Len(Trim(Me.wNet_Tons.Text)) <> 0 Then
                            nNet = Me.wNet_Tons.Text
                        End If
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Net_Tons,0) as Net_Tons From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("Net_Tons") <> Me.wNet_Tons.Text Then
                                If ds.Tables(0).Rows(0).Item("Net_Tons") = 0 Then
                                    vDesc = "Added Net Tons"
                                Else
                                    vDesc = "Changed Net Tons"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Net_Tons = " & nNet & " Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Net_Tons"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Net field is empty,...")
                        Me.wNet_Tons.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wLOA_KeyDown(sender As Object, e As KeyEventArgs) Handles wLOA.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wLOA.Text)) > 0 Then
                        ' ------ Update Route
                        Dim nLOA As Double = 0
                        If Len(Trim(Me.wLOA.Text)) <> 0 Then
                            nLOA = Me.wLOA.Text
                        End If
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(LOA,0) as LOA From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("LOA") <> Me.wLOA.Text Then
                                If ds.Tables(0).Rows(0).Item("LOA") = 0 Then
                                    vDesc = "Added LOA"
                                Else
                                    vDesc = "Changed LOA"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set LOA = " & nLOA & " Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("LOA"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("LOA field is empty,...")
                        Me.wLOA.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wDraft_KeyDown(sender As Object, e As KeyEventArgs) Handles wDraft.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wDraft.Text)) > 0 Then
                        ' ------ Update Route
                        Dim nDraft As Double = 0
                        If Len(Trim(Me.wDraft.Text)) <> 0 Then
                            nDraft = Me.wDraft.Text
                        End If
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Draft,0) as Draft From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("Draft") <> Me.wDraft.Text Then
                                If ds.Tables(0).Rows(0).Item("Draft") = 0 Then
                                    vDesc = "Added Draft"
                                Else
                                    vDesc = "Changed Draft"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Draft = " & nDraft & " Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Draft"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Draft field is empty,...")
                        Me.wDraft.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub wBeam_KeyDown(sender As Object, e As KeyEventArgs) Handles wBeam.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wBeam.Text)) > 0 Then
                        ' ------ Update Route
                        Dim nBeam As Double = 0
                        If Len(Trim(Me.wBeam.Text)) <> 0 Then
                            nBeam = Me.wBeam.Text
                        End If
                        Dim vDesc As String = ""
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select isNULL(Beam,0) as Beam From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("Beam") <> Me.wBeam.Text Then
                                If ds.Tables(0).Rows(0).Item("Beam") = 0 Then
                                    vDesc = "Added Beam "
                                Else
                                    vDesc = "Changed Beam"
                                End If
                                md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Beam = " & Trim(Me.wBeam.Text) & " Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                                Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Beam"))
                            End If
                        End If
                        ds = Nothing
                        Audit_Search()
                        Me.DsVessels_x_Line.Clear()
                        DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                        Me.Refrehs_Vessel()
                        Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    Else
                        MsgBox("Beam field is empty,...")
                        Me.wBeam.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub Vessels_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        md.Insert_User_Log("Closing Vessel", md.UserName)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.TabControl1.SelectedIndex = 1 Then
            DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
            If Me.DsVessels_x_Line.Tables(0).Rows.Count > 0 Then
                Dim cr_Vessels As New cr_Vessels
                cr_Vessels.SetDataSource(DsVessels_x_Line.Tables(0))
                Me.cv_Vessels.CloseView(cr_Vessels)
                Me.cv_Vessels.ReportSource = cr_Vessels
                Me.cv_Vessels.BringToFront()
                Me.cv_Vessels.RefreshReport()
                Me.cv_Vessels.DisplayToolbar = True
                Me.cv_Vessels.DisplayStatusBar = True
                'Me.cv_Sailing_Schedule.Zoom(75)
                Me.cv_Vessels.Refresh()
                Me.cv_Vessels.Show()
            End If
        End If
    End Sub

    Private Sub cb_Route_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Route.SelectionChangeCommitted
        If Not Me.cb_Route.SelectedValue = Nothing Then
            Me.Route_Selected(Me.cb_Route.SelectedValue)
        End If
    End Sub
#End Region

    Public Sub CreateMyToolTip()
        ' Create the ToolTip and associate with the Form container.
        Dim toolTip1 As New ToolTip()

        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True

        ' Set up the ToolTip text for the Button and Checkbox.
        'toolTip1.SetToolTip(Me.DataGrid1.TableStyles(0).GridColumnStyles.Item("CanCreate"), "Permissions.")
        toolTip1.SetToolTip(wRegistry, "")
        toolTip1.SetToolTip(wIMO_Nro, "")
        toolTip1.SetToolTip(wLLoyds, "")
        'toolTip1.SetToolTip(txt_BL_Terminal_Clauses, "BLs clauses of the terminal, to be showed in the bottom of the B/L. Example- THESE COMMODITIES, TECHNOLOGY OR SOFTWARE WERE EXPORTED FROM THE UNITED STATES,....")
        'toolTip1.SetToolTip(txt_BL_Terminal_Clauses_Port_Discharge, "Booking notes of the port discharge, to be showed in the bottom of the Booking. Example- Transit time to Guyana and Suriname is an estimated 12 - 15 days and is subject to change without notice.")
    End Sub

    Private Sub wVessel_Code_KeyDown(sender As Object, e As KeyEventArgs) Handles wVessel_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    'If Len(Trim(Me.wVessel_Nro.Text)) > 0 Then
                    ' ------ Update Route
                    Dim vDesc As String = ""
                    Dim ds As New DataSet
                    ds = ws.GetDataset(md.strConnect, "Select isNULL(Vessel_Short,'') as Vessel_Short From Vessels Where Line_Number = " & md.GL_Company & " and Vessel_Name = '" & Me.wVessel_Name.Text & "'", 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Trim(ds.Tables(0).Rows(0).Item("Vessel_Short")) <> Trim(Me.wVessel_Code.Text) Then
                            If Len(Trim(ds.Tables(0).Rows(0).Item("Vessel_Short"))) = 0 Then
                                vDesc = "Added Vessel Short"
                            Else
                                vDesc = "Changed Vessel Short"
                            End If
                            md.eResp = ws.ExecSQL(md.strConnect, "Update Vessels Set Vessel_Short = '" & Trim(Me.wVessel_Code.Text) & "' Where uid = " & Me.cb_Vessel.SelectedItem("uid"))
                            Me.Insert_Changes(Trim(vDesc), Trim(Me.wVessel_Name.Text), ds.Tables(0).Rows(0).Item("Vessel_Short"))
                        End If
                    End If
                    ds = Nothing
                    Audit_Search()
                    Me.DsVessels_x_Line.Clear()
                    DsVessels_x_Line = ws.SP_1_Param_int(md.strConnect, "Vessels_x_Line", "@Line", md.GL_Company)
                    Me.Refrehs_Vessel()
                    Me.cb_Vessel.SelectedValue = Me.wVessel_Name.Text
                    'Else
                    '    MsgBox("Vessel # field is empty,...")
                    '    Me.wVessel_Nro.Focus()
                    'End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
End Class