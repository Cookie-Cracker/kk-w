Imports System.ComponentModel


Public Class Ports
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1
    Public Ds_Ports_Master As New DataSet
    Public ds_Ports As New DataSet
    Public ds_PortD As New DataSet
    Public ds_Port_Contracts As New DataSet

    Private Sub Ports_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, Me.Name)) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
        CreateMyToolTip()
        If Trim(md.Progran_ReadOnly(UserCode, Me.Name)) = "Y" Then
            Me.bnt_New.Visible = False
            Me.bnt_Save.Visible = False
            Me.bnt_Del.Visible = False
            Me.bnt_Edit.Visible = False
        End If
        Try
            'Inner Join CM_System as n ON n.Company_Number = isNull(p.Agent_Account,0) and n.Location_Number = 0

            strSQL = "Select p.PORT_NUMBER,p.PORT_NAME
                                              ,p.COUNTRY
                                              ,p.PORT_SHORT
                                              ,p.RATE_AS
                                              ,p.PORT_CODE
                                              ,p.COUNTRY_CODE
                                              ,p.THRU_PORT
                                              ,p.ISISPORT
                                              ,p.AMSPORT
                                              ,p.EMPTY_BL
                                              ,p.Port_Int_Code,p.uid,p.Actived,isnull(p.MUnit,'') as MUnit,isnull(p.WUnit,'') as Wunit, 
                                              isNull(p.Agent_Account, 0) As Agent_Account,isnull(p.FMC_Percent,0) As FMC_Percent,
                                              isNull(p.PortPlace,'N') as IsPort,isNull(p.Place,'N') as Place
                                        From Ports As p 
                                 Where isnull(p.[Delete],'') = '' order by p.port_name"
            ds_Ports.Clear()
            ds_Ports = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_Ports.Tables(0).Rows.Count > 0 Then
                Me.cb_Ports.DataSource = ds_Ports.Tables(0)
                Me.cb_Ports.DisplayMember = "Port_name"
                Me.cb_Ports.ValueMember = "Port_Number"
                Me.cb_Ports.Refresh()
            End If
            ds_Port_Thru.Clear()
            ds_Port_Thru = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_Port_Thru.Tables(0).Rows.Count > 0 Then
                Me.cb_Port_Thru.DataSource = ds_Port_Thru.Tables(0)
                Me.cb_Port_Thru.DisplayMember = "Port_name"
                Me.cb_Port_Thru.ValueMember = "Port_Number"
                Me.cb_Port_Thru.Refresh()
            End If
            ds_PortD.Clear()
            ds_PortD = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_PortD.Tables(0).Rows.Count > 0 Then
                Me.cb_Port_Discharge_Clauses.DataSource = ds_PortD.Tables(0)
                Me.cb_Port_Discharge_Clauses.DisplayMember = "Port_name"
                Me.cb_Port_Discharge_Clauses.ValueMember = "Port_Number"
                Me.cb_Port_Discharge_Clauses.Refresh()
            End If

            ds_Port_Rate_As.Clear()
            ds_Port_Rate_As = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_Port_Rate_As.Tables(0).Rows.Count > 0 Then
                Me.cb_Port_Rate_As.DataSource = ds_Port_Rate_As.Tables(0)
                Me.cb_Port_Rate_As.DisplayMember = "Port_name"
                Me.cb_Port_Rate_As.ValueMember = "Port_Number"
                Me.cb_Port_Rate_As.Refresh()
            End If
            ds_Country.Clear()
            ds_Country = ws.GetDataset(md.strConnect, "Select uid,upper(COUNTRY) as Country,A2_ISO,A3_UN,UN_nro FROM Country_Master Order by Country", 1)
            If ds_Country.Tables(0).Rows.Count > 0 Then
                Me.cb_Country.DataSource = ds_Country.Tables(0)
                Me.cb_Country.DisplayMember = "Country"
                Me.cb_Country.ValueMember = "Country"
            End If
            ds_Country_To_Search.Clear()
            ds_Country_To_Search = ws.GetDataset(md.strConnect, "Select uid,upper(COUNTRY) as Country,A2_ISO,A3_UN,UN_nro FROM Country_Master Order by Country", 1)
            If ds_Country_To_Search.Tables(0).Rows.Count > 0 Then
                Me.cb_Country_Search.DataSource = ds_Country_To_Search.Tables(0)
                Me.cb_Country_Search.DisplayMember = "Country"
                Me.cb_Country_Search.ValueMember = "Country"
            End If
            ds_M_Units = ws.GetDataset(md.strConnect, "Select [UNIT]
                                                  ,[UNIT_TYPE]
                                                  ,[DESCRIPTION]
                                       From [Units] Where Unit_Type = 'M' order by Unit", 1)
            If ds_M_Units.Tables(0).Rows.Count > 0 Then
                Me.cb_measure_Unit.DataSource = ds_M_Units.Tables(0)
                Me.cb_measure_Unit.DisplayMember = "Unit"
                Me.cb_measure_Unit.ValueMember = "Unit"
                Me.cb_measure_Unit.SelectedValue = md.Default_MUnit
            End If
            ds_W_Units = ws.GetDataset(md.strConnect, "SELECT [UNIT]
                                                  ,[UNIT_TYPE]
                                                  ,[DESCRIPTION]
                                       From [Units] Where Unit_Type = 'W' order by Unit", 1)
            If ds_W_Units.Tables(0).Rows.Count > 0 Then
                Me.cb_Weight_Unit.DataSource = ds_W_Units.Tables(0)
                Me.cb_Weight_Unit.DisplayMember = "Unit"
                Me.cb_Weight_Unit.ValueMember = "Unit"
                Me.cb_Weight_Unit.SelectedValue = md.Default_WUnit
            End If
            Me.Modo_Edit_ADD_Read_Only(1)

            If Me.rd_Port.Checked = True Then
                Me.gb_Port_Info.Visible = True
                Me.gb_Yards.Visible = True
                Me.gb_Agents.Visible = True
                Me.gb_BL_Terminal.Visible = True
            Else
                Me.gb_Port_Info.Visible = False
                Me.gb_Yards.Visible = False
                Me.gb_Agents.Visible = False
                Me.gb_BL_Terminal.Visible = False
            End If

            Me.Agents()
            Me.Yards()
            md.Insert_User_Log("Load Ports", md.UserName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Agents()
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Distinct Company_Number, Company_name From CM_System Where Agent = 'A' Order by Company_name", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim vAgent As String = ""
            Me.chk_list_Agent.Items.Clear()
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                vAgent = Trim(ds.Tables(0).Rows(j).Item("Company_number").ToString) & " -- " & Trim(ds.Tables(0).Rows(j).Item("Company_name"))
                Me.chk_list_Agent.Items.Add(Trim(vAgent), False)
            Next
            vAgent = Nothing
        End If
        ds = Nothing
    End Sub

    Private Sub Yards()
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Distinct Company_Number, Company_name From CM_System Where Yard = 'Y' Order by Company_name", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim vYard As String = ""
            Me.chk_list_Yards.Items.Clear()
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                vYard = Trim(ds.Tables(0).Rows(j).Item("Company_number").ToString) & " -- " & Trim(ds.Tables(0).Rows(j).Item("Company_name"))
                Me.chk_list_Yards.Items.Add(Trim(vYard), False)
            Next
            vYard = Nothing
        End If
        ds = Nothing
    End Sub

    Private Sub Clear()
        Me.wPort_Name.Clear()
        Me.wPort_Nro.Clear()
        Me.wPort_Int_Code.Clear()
        Me.wAbbreviation.Clear()
        Me.wCountry_Code.Clear()
        Me.wCustoms_Code.Clear()
        Me.wFMC.Clear()

        'Me.cb_AMS.SelectedItem = ""
        'Me.cb_ISIS.SelectedItem = ""
        Me.cb_Country.SelectedValue = ""
        Me.cb_Port_Rate_As.SelectedValue = 1
        Me.cb_Port_Thru.SelectedValue = 1
        Me.cb_Ports.SelectedValue = 1
        Me.chk_list_Agent.Items.Clear()
        Me.chk_list_Yards.Items.Clear()
        Me.cb_measure_Unit.SelectedValue = md.Default_MUnit
        Me.cb_Weight_Unit.SelectedValue = md.Default_WUnit
        Me.cb_Acived.SelectedItem = " "

        Me.txt_BL_Terminal_Clauses.Clear()
        Me.txt_BL_Terminal_Clauses_Port_Discharge.Clear()
        Me.cb_Port_Discharge_Clauses.SelectedValue = 1
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If rd_Port.Checked = True Then
            Me.gb_Port_Info.Visible = True
            Me.gb_Agents.Visible = True
            Me.gb_Yards.Visible = True
            Me.gb_BL_Terminal.Visible = True
        Else
            Me.gb_Port_Info.Visible = False
            Me.gb_Agents.Visible = False
            Me.gb_Yards.Visible = False
            Me.gb_BL_Terminal.Visible = False
        End If
        Select Case Flag_Modo
            Case 1
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = False
                Me.bnt_New.Enabled = True
                Me.bnt_Edit.Enabled = True

                'Me.wPort_Nro.BackColor = Color.MidnightBlue
                'Me.wPort_Nro.ForeColor = Color.White
                'Me.wPort_Nro.ReadOnly = True
                Me.wPort_Name.BackColor = Color.LightBlue
                Me.wPort_Name.ForeColor = Color.Black
                Me.wPort_Name.ReadOnly = True

                Me.gb_Port_Place.Enabled = False
                Me.wPort_Name.ReadOnly = True
                Me.cb_Acived.Enabled = False
                Me.cb_Country.Enabled = False

                Me.gb_Port_Info.Enabled = False
                Me.gb_Agents.Enabled = False
                Me.gb_Yards.Enabled = False

                'Me.gb_BL_Terminal.Enabled = False
                Me.txt_BL_Terminal_Clauses.ReadOnly = True
                Me.txt_BL_Terminal_Clauses.BackColor = Color.Navy
                Me.txt_BL_Terminal_Clauses.ForeColor = Color.White

                Me.txt_BL_Terminal_Clauses_Port_Discharge.ReadOnly = True
                Me.txt_BL_Terminal_Clauses_Port_Discharge.BackColor = Color.Navy
                Me.txt_BL_Terminal_Clauses_Port_Discharge.ForeColor = Color.White

            Case 2
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = False
                Me.bnt_New.Enabled = True
                Me.bnt_Edit.Enabled = True

                'Me.wPort_Nro.BackColor = Color.MidnightBlue
                'Me.wPort_Nro.ForeColor = Color.White
                'Me.wPort_Nro.ReadOnly = True
                Me.wPort_Name.BackColor = Color.White
                Me.wPort_Name.ForeColor = Color.Blue
                Me.wPort_Name.ReadOnly = False

                Me.gb_Port_Place.Enabled = True
                Me.wPort_Name.ReadOnly = False
                Me.cb_Acived.Enabled = True
                Me.cb_Country.Enabled = True

                Me.gb_Port_Info.Enabled = True
                Me.gb_Agents.Enabled = True
                Me.gb_Yards.Enabled = True
                'Me.gb_BL_Terminal.Enabled = True

                Me.txt_BL_Terminal_Clauses.ReadOnly = False
                Me.txt_BL_Terminal_Clauses.BackColor = Color.White
                Me.txt_BL_Terminal_Clauses.ForeColor = Color.Black

                Me.txt_BL_Terminal_Clauses_Port_Discharge.ReadOnly = False
                Me.txt_BL_Terminal_Clauses_Port_Discharge.BackColor = Color.White
                Me.txt_BL_Terminal_Clauses_Port_Discharge.ForeColor = Color.Black

            Case 3
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = True
                Me.bnt_New.Enabled = False
                Me.bnt_Edit.Enabled = False

                Me.wPort_Nro.BackColor = Color.White
                Me.wPort_Nro.ForeColor = Color.Blue
                Me.wPort_Nro.ReadOnly = False
                Me.wPort_Name.BackColor = Color.White
                Me.wPort_Name.ForeColor = Color.Blue
                Me.wPort_Name.ReadOnly = False

                Me.gb_Port_Place.Enabled = True
                Me.wPort_Name.ReadOnly = False
                Me.cb_Acived.Enabled = True
                Me.cb_Country.Enabled = True

                Me.gb_Port_Info.Enabled = True
                Me.gb_Agents.Enabled = True
                Me.gb_Yards.Enabled = True
                'Me.gb_BL_Terminal.Enabled = True

                Me.txt_BL_Terminal_Clauses.ReadOnly = False
                Me.txt_BL_Terminal_Clauses.BackColor = Color.White
                Me.txt_BL_Terminal_Clauses.ForeColor = Color.Black

                Me.txt_BL_Terminal_Clauses_Port_Discharge.ReadOnly = False
                Me.txt_BL_Terminal_Clauses_Port_Discharge.BackColor = Color.White
                Me.txt_BL_Terminal_Clauses_Port_Discharge.ForeColor = Color.Black
        End Select
    End Sub

    Private Sub Refresh_CMS(ByVal Account As Integer)
        Me.ds_CMS.Clear()
        ds_CMS = ws.GetDataset(md.strConnect, "Select DISTINCT Company_Name, Company_Number,isnull(shipper,'') as shipper,isnull(Consignee,'') as Consignee,isnull(FWDR,'') as FWDR,isnull(Notify,'') as Notify
                                              ,isnull(Trucker,'') as Trucker,isnull(Warehouse,'') as Warehouse,isnull(Yard,'') as Yard 
                             From CM_System 
                          Where Company_Number = " & Account & " and Location_Number = 0 ", 1) 'Me.CMS_MasterTableAdapter.Fill(Me.Ds_CMS_Master.CMS_Master, Trim(zDesc))
        If ds_CMS.Tables(0).Rows.Count > 0 Then

        End If
    End Sub

    Private Sub cb_Ports_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Ports.SelectionChangeCommitted
        Me.Refresh_Port_Seleted()
    End Sub
    Private Sub Refresh_Port_Seleted()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            'If Me.cb_Ports.SelectedItem = Nothing Then
            '    Exit Sub
            'End If
            Flag_Modo = 1
            Me.wPort_Nro.Text = Me.cb_Ports.SelectedItem("Port_Number")
            Me.wPort_Name.Text = Me.cb_Ports.SelectedItem("Port_Name")
            Me.cb_Country.SelectedValue = Trim(Me.cb_Ports.SelectedItem("Country"))
            Me.wCustoms_Code.Text = Trim(Me.cb_Ports.SelectedItem("Port_Code"))
            Me.wCountry_Code.Text = Trim(Me.cb_Ports.SelectedItem("Country_Code"))
            Me.wFMC.Text = Me.cb_Ports.SelectedItem("FMC_Percent")
            Me.cb_Port_Thru.SelectedValue = Me.cb_Ports.SelectedItem("THRU_PORT")
            Me.cb_Port_Rate_As.SelectedValue = Me.cb_Ports.SelectedItem("RATE_AS")
            'Me.cb_AMS.SelectedItem = Me.cb_Ports.SelectedItem("AMSPort")
            'Me.cb_ISIS.SelectedItem = Me.cb_Ports.SelectedItem("ISISPort")
            Me.wAbbreviation.Text = Me.cb_Ports.SelectedItem("Port_Short")
            Me.wPort_Int_Code.Text = Me.cb_Ports.SelectedItem("Port_Int_Code")
            Dim vMUnit As String = Me.cb_Ports.SelectedItem("MUnit")
            Me.cb_measure_Unit.SelectedValue = cb_Ports.SelectedItem("MUnit")
            Me.cb_Weight_Unit.SelectedValue = cb_Ports.SelectedItem("WUnit")
            Me.cb_Acived.SelectedItem = cb_Ports.SelectedItem("Actived")
            If cb_Ports.SelectedItem("IsPort") = "Y" Then
                Me.rd_Port.Checked = True
            Else
                Me.rd_Place.Checked = True
            End If

            Me.Refresh_Agents_by_Port()
            Me.Refresh_Yards_by_Ports()

            strSQL = "Select isNull(Clause,'') as Clause From BL_Terminal_Clauses Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = 1"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.txt_BL_Terminal_Clauses.Text = FormatStrLine(Trim(ds.Tables(0).Rows(0).Item("Clause")))
            Else
                Me.txt_BL_Terminal_Clauses.Clear()
            End If

            strSQL = "Select isNull(Clause,'') as Clause, Terminal_Dest From BL_Terminal_Clauses Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest <> 1"
            ds.Clear()
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.cb_Port_Discharge_Clauses.SelectedValue = ds.Tables(0).Rows(0).Item("Terminal_Dest")
                Me.txt_BL_Terminal_Clauses_Port_Discharge.Text = FormatStrLine(Trim(ds.Tables(0).Rows(0).Item("Clause")))
            Else
                Me.cb_Port_Discharge_Clauses.SelectedValue = 1
                Me.txt_BL_Terminal_Clauses_Port_Discharge.Clear()
            End If
            ds = Nothing
            Me.Modo_Edit_ADD_Read_Only(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Refresh_Agents()
        If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
            Dim j As Integer = 0
            For j = 0 To Me.chk_list_Agent.Items.Count - 1
                Me.chk_list_Agent.SetItemCheckState(j, CheckState.Unchecked)
            Next
            j = 0
            Dim jj As Integer = 0
            Dim Agent_New As String = ""
            Dim pos As Integer = 0
            Dim nAgent As Integer = 0
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select Agent From Port_Agents Where Port_Number = " & Trim(Me.wPort_Nro.Text), 1)
            If ds.Tables(0).Rows.Count > 0 Then
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    'Me.chk_list_Agent.Items.Add()
                    For jj = 0 To Me.chk_list_Agent.Items.Count - 1
                        Agent_New = Me.chk_list_Agent.Items(jj).ToString
                        pos = InStr(Agent_New, " -- ")
                        nAgent = CInt(Mid(Agent_New, 1, pos - 1))
                        If ds.Tables(0).Rows(j).Item("Agent") = nAgent Then
                            Me.chk_list_Agent.SetItemCheckState(jj, CheckState.Checked)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Private Sub Refresh_Yards()
        Dim j As Integer = 0
        For j = 0 To Me.chk_list_Yards.Items.Count - 1
            Me.chk_list_Yards.SetItemCheckState(j, CheckState.Unchecked)
        Next
        j = 0
        Dim jj As Integer = 0
        Dim Yard_New As String = ""
        Dim pos As Integer = 0
        Dim nYard As Integer = 0
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Yard From Port_Yards Where Port_Number = " & Trim(Me.wPort_Nro.Text), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            For j = 0 To ds.Tables(0).Rows.Count - 1
                For jj = 0 To Me.chk_list_Yards.Items.Count - 1
                    Yard_New = Me.chk_list_Yards.Items(jj).ToString
                    pos = InStr(Yard_New, " -- ")
                    If pos > 0 Then
                        nYard = CInt(Mid(Yard_New, 1, pos - 1))
                        If ds.Tables(0).Rows(j).Item("Yard") = nYard Then
                            Me.chk_list_Yards.SetItemCheckState(jj, CheckState.Checked)
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub Refresh_Ports(ByVal nAll As Integer)
        If nAll = 0 Then
            strSQL = "SELECT p.PORT_NUMBER,p.PORT_NAME
                                              ,p.COUNTRY
                                              ,p.PORT_SHORT
                                              ,p.RATE_AS
                                              ,p.PORT_CODE
                                              ,p.COUNTRY_CODE
                                              ,p.THRU_PORT
                                              ,p.ISISPORT
                                              ,p.AMSPORT
                                              ,p.EMPTY_BL
                                              ,p.Port_Int_Code,p.uid,p.Actived,isnull(p.MUnit,'') as MUnit,isnull(p.WUnit,'') as Wunit, 
                                              isNull(p.Agent_Account,0) as Agent_Account,isnull(p.FMC_Percent,0) as FMC_Percent,
                                              isNull(p.PortPlace,'N') as IsPort,isNull(p.Place,'N') as Place
                                        FROM Ports as p
                                       where p.[Delete] is NULL order by p.port_name"
        Else
            strSQL = "SELECT p.PORT_NUMBER,p.PORT_NAME
                                              ,p.COUNTRY
                                              ,p.PORT_SHORT
                                              ,p.RATE_AS
                                              ,p.PORT_CODE
                                              ,p.COUNTRY_CODE
                                              ,p.THRU_PORT
                                              ,p.ISISPORT
                                              ,p.AMSPORT
                                              ,p.EMPTY_BL
                                              ,p.Port_Int_Code,p.uid,p.Actived,isnull(p.MUnit,'') as MUnit,isnull(p.WUnit,'') as Wunit, 
                                              isNull(p.Agent_Account,0) as Agent_Account,isnull(p.FMC_Percent,0) as FMC_Percent,
                                              isNull(p.PortPlace,'N') as IsPort,isNull(p.Place,'N') as Place
                                        FROM Ports as p
                                       where p.[Delete] is NULL and isnull(PortPlace,'N') = 'Y' order by p.port_name"
        End If
        Me.Clear()
        ds_Ports.Clear()
        ds_Ports = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_Ports.Tables(0).Rows.Count > 0 Then
            Me.cb_Ports.DataSource = ds_Ports.Tables(0)
            Me.cb_Ports.DisplayMember = "Port_name"
            Me.cb_Ports.ValueMember = "Port_Number"
            Me.cb_Ports.Refresh()
        End If
    End Sub

    Private Sub rd_All_CheckedChanged(sender As Object, e As EventArgs) Handles rd_All.CheckedChanged
        If Me.rd_Thru.Focused Then
            If Me.rd_All.Checked = True Then
                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                Me.Refresh_Report()
            End If
        End If
    End Sub

    Private Sub Refresh_Report()
        If Me.Ds_Ports_Master.Tables(0).Rows.Count > 0 Then
            Dim cr_Ports_Master As New cr_Ports_Master
            cr_Ports_Master.SetDataSource(Ds_Ports_Master)
            Me.cv_Ports.CloseView(cr_Ports_Master)
            Me.cv_Ports.ReportSource = cr_Ports_Master
            Me.cv_Ports.BringToFront()
            Me.cv_Ports.RefreshReport()
            Me.cv_Ports.DisplayToolbar = True
            Me.cv_Ports.DisplayStatusBar = True
            'Me.cv_Sailing_Schedule.Zoom(75)
            Me.cv_Ports.Refresh()
            Me.cv_Ports.Show()
        End If
    End Sub

#Region "Update"
    Private Sub rd_Thru_CheckedChanged(sender As Object, e As EventArgs) Handles rd_Thru.CheckedChanged
        If Me.rd_Thru.Checked = True Then
            Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 0)
            Me.Refresh_Report()
        End If

    End Sub

    Private Sub cb_Port_Rate_As_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Port_Rate_As.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select Rate_As From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Rate_As")) <> Trim(Me.cb_Port_Rate_As.SelectedValue) Then
                        Dim nPort As Integer = Me.cb_Ports.SelectedValue
                        eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Rate_As = '" & Trim(Me.cb_Port_Rate_As.SelectedValue) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                        Dim nnPort As Integer = ds.Tables(0).Rows(0).Item("Rate_As")
                        ds.Clear()
                        ds = ws.GetDataset(strConnect, "Select Port_Name From Ports Where Port_Number = " & nnPort, 1)
                        If ds.Tables(0).Rows.Count Then
                            Me.Insert_Changes("Changed Rate as: " & Trim(Me.wPort_Name.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Name")))
                        End If
                        nnPort = Nothing
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        nPort = Nothing
                    End If
                End If
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Country_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Country.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select Country From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Country")) <> Trim(Me.cb_Country.SelectedValue) Then
                        Dim nPort As String = Me.cb_Ports.SelectedValue
                        eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Country = '" & Trim(Me.cb_Country.SelectedValue) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                        Me.Insert_Changes("Changed Country: " & Trim(Me.cb_Country.SelectedValue), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Country")))
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        nPort = Nothing
                    End If
                End If
                ds = Nothing
            Else
                If Flag_Modo = 3 Then
                    Me.wCountry_Code.Text = cb_Country.SelectedItem("A2_ISO")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    'Private Sub cb_AMS_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_AMS.SelectionChangeCommitted
    '    Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '    Try
    '        If Flag_Modo = 2 Then
    '            ' ------ Update Route
    '            Dim vResp As String = ""
    '            vResp = ws.ExecSQL(md.strConnect, "Update Ports Set AMSPort = '" & Trim(Me.cb_AMS.SelectedItem) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
    '            If Trim(vResp) <> "Success" Then
    '                MsgBox(vResp)
    '                Exit Sub
    '            End If
    '            Me.Ds_Ports_Master.Clear()
    '            Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
    '            If Me.chk_Only_Ports.Checked = True Then
    '                Me.Refresh_Ports(1)
    '            Else
    '                Me.Refresh_Ports(0)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    Cursor.Current = System.Windows.Forms.Cursors.Default
    'End Sub

    'Private Sub cb_ISIS_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_ISIS.SelectionChangeCommitted
    '    Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '    Try
    '        If Flag_Modo = 2 Then
    '            ' ------ Update Route
    '            Dim vResp As String = ""
    '            vResp = ws.ExecSQL(md.strConnect, "Update Ports Set ISISPort = '" & Trim(Me.cb_ISIS.SelectedItem) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
    '            If Trim(vResp) <> "Success" Then
    '                MsgBox(vResp)
    '                Exit Sub
    '            End If
    '            Me.Ds_Ports_Master.Clear()
    '            Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
    '            If Me.chk_Only_Ports.Checked = True Then
    '                Me.Refresh_Ports(1)
    '            Else
    '                Me.Refresh_Ports(0)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    Cursor.Current = System.Windows.Forms.Cursors.Default
    'End Sub

    Private Sub cb_Acived_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Acived.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select isnull(actived,'N') as Actived From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("actived")) <> Trim(Me.cb_Acived.SelectedItem) Then
                        Dim nPort As Integer = Me.cb_Ports.SelectedValue
                        eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Actived = '" & Trim(Me.cb_Acived.SelectedItem) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                        Me.Insert_Changes("Changed Actived: " & Trim(Me.cb_Acived.SelectedItem), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("actived")))
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        nPort = Nothing
                    End If
                End If
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub wFMC_KeyDown(sender As Object, e As KeyEventArgs) Handles wFMC.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wFMC.Text)) > 0 Then
                        Dim ds As New DataSet
                        ds = ws.GetDataset(strConnect, "Select FMC_Percent From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("FMC_Percent")) <> Trim(Me.wFMC.Text) Then
                                ' ------ Update FMC
                                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set FMC_Percent = " & Me.wFMC.Text & " Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                                Me.Insert_Changes("Changed FMC %: " & Trim(Me.wFMC.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("FMC_Percent")))
                                Me.Ds_Ports_Master.Clear()
                                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                                If Me.chk_Only_Ports.Checked = True Then
                                    Me.Refresh_Ports(1)
                                Else
                                    Me.Refresh_Ports(0)
                                End If
                                Me.cb_Ports.SelectedValue = nPort
                                Me.cb_Ports.Refresh()
                                nPort = Nothing
                            End If
                        End If
                        ds = Nothing
                    Else
                        MsgBox("FMC percent field is empty,...")
                        Me.wFMC.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
#End Region

#Region "Commands"
    Private Sub bnt_New_Click(sender As Object, e As EventArgs) Handles bnt_New.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 3
            Me.Clear()
            Me.Agents()
            'Me.Refresh_Agents()
            Me.Yards()
            'Me.Refresh_Yards()
            Me.cb_Acived.SelectedItem = "Y"
            Me.Modo_Edit_ADD_Read_Only(3)
            bnt_Del.Enabled = False
            bnt_Edit.Enabled = False
            bnt_Save.Enabled = True
            bnt_New.Enabled = False
            Me.wPort_Nro.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            ' ------- Validation
            If Len(Trim(Me.wPort_Nro.Text)) = 0 Then
                MsgBox("Code field is empty, The program is going to generate the port code automatic,....")
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "SELECT Top 1 (port_number + 1) as Port_Code FROM  Ports Where (PORT_NUMBER <> 999999) Order by Port_Number Desc", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.wPort_Nro.Text = ds.Tables(0).Rows(0).Item("Port_Code")
                Else
                    Me.wPort_Nro.Text = "100"
                End If
                Me.wPort_Nro.Refresh()
            End If
            If Not IsNumeric(Me.wPort_Nro.Text) Then
                MsgBox("Code field must be numeric,..")
                Me.wPort_Nro.Text = 1
                Me.wPort_Nro.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.wPort_Name.Text)) = 0 Then
                MsgBox("Name field is empty,..")
                Me.wPort_Name.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.cb_Country.SelectedValue)) = 0 Then
                MsgBox("Country field is empty,..")
                Me.cb_Country.Focus()
                Exit Sub
            End If
            'Dim ds As New DataSet

            Dim nPort As Integer = Me.wPort_Nro.Text
            'ds = ws.GetDataset(strConnect, "Select Top 1 Port_Number From Ports Where Port_Number <> 9999 Order By Port_Number Desc", 1)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    nPort = ds.Tables(0).Rows(0).Item("Port_Number") + 1
            'End If

            Dim strSql As String = ""
            Dim vResp As String = ""
            Dim PortL_Name As String = ""
            Dim PortD_Name As String = ""
            Dim nRoute_ID As Integer = 1
            Dim Agent_Account As Integer = 0
            Dim nCode As Integer = 0
            Dim nFMC As Double = 0.00
            Dim IsPort As String = "N"
            Dim IsPlace As String = "N"
            If Flag_Modo = 3 Then
                Dim nPort_Thru As Integer = nPort
                Dim nPort_Rate_As As Integer = nPort
                If Me.rd_Port.Checked = True Then
                    IsPort = "Y"
                    If Me.cb_Port_Thru.SelectedItem("Port_Number") <> 1 Then
                        nPort_Thru = Me.cb_Port_Thru.SelectedItem("Port_Number")
                    End If
                    If Me.cb_Port_Rate_As.SelectedItem("Port_Number") <> 1 Then
                        nPort_Rate_As = Me.cb_Port_Rate_As.SelectedItem("Port_Number")
                    End If
                    If Len(Trim(Me.wCustoms_Code.Text)) > 0 Then
                        nCode = Me.wCustoms_Code.Text
                    End If
                    If Len(Trim(Me.wFMC.Text)) > 0 Then
                        nFMC = Me.wFMC.Text
                    End If
                Else

                    IsPlace = "Y"
                End If
                'ISISPORT,AMSPORT,
                strSql = "Insert Into Ports (Port_Number,Port_Name,
                                             PORT_SHORT,PORT_CODE,Port_Int_Code,
                                             Thru_Port,RATE_AS,
                                             COUNTRY,
                                             COUNTRY_CODE,
                                             TRANSLATION,
                                             Created_By, Creation_Date,Actived,FMC_Percent,PortPlace, Place) Values (" &
                                    nPort & ",'" & Trim(Me.wPort_Name.Text) & "','" &
                                    Trim(Me.wAbbreviation.Text) & "'," & nCode & ",'" & Trim(Me.wPort_Int_Code.Text) & "'," &
                                    nPort_Thru & "," & nPort_Rate_As & ",'" &
                                    Trim(Me.cb_Country.SelectedValue) & "','" & Trim(Me.wCountry_Code.Text) & "','N','" &
                                    Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "','" & Trim(Me.cb_Acived.SelectedItem) & "'," &
                                    nFMC & ",'" & Trim(IsPort) & "','" & Trim(IsPlace) & "')"
                ' MsgBox(strSql)
                vResp = ws.ExecSQL(md.strConnect, strSql)
                Dim pos As Integer = 0
                Dim j As Integer = 0
                If Me.chk_list_Agent.SelectedItems.Count > 0 Then
                    Dim nAgent As Integer = 0
                    Dim Agent_New As String = ""
                    For j = 0 To Me.chk_list_Agent.CheckedItems.Count - 1
                        Agent_New = Me.chk_list_Agent.CheckedItems(j).ToString
                        pos = InStr(Agent_New, " -- ")
                        nAgent = CInt(Mid(Agent_New, 1, pos - 1))
                        strSql = "Insert Into Port_Agents (Port_Number, Agent, Location_Number, Created_By, Created_ON) Values (" & Trim(Me.wPort_Nro.Text) & "," & nAgent & ",0,'" & Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "')"
                        md.eResp = ws.ExecSQL(md.strConnect, strSql)
                        Me.Insert_Changes("New Agent" & Trim(Str(nAgent)), Me.wPort_Nro.Text, Trim(Agent_New), "")
                    Next
                End If

                If Me.chk_list_Yards.SelectedItems.Count > 0 Then
                    Dim nYard As Integer = 0
                    Dim Yard_New As String = ""
                    For j = 0 To Me.chk_list_Yards.CheckedItems.Count - 1
                        Yard_New = Me.chk_list_Yards.CheckedItems(j).ToString
                        pos = InStr(Yard_New, " -- ")
                        nYard = CInt(Mid(Yard_New, 1, pos - 1))
                        strSql = "Insert Into Port_Yards (Port_Number, Yard, Location_Number, Created_By, Created_ON) Values (" & Trim(Me.wPort_Nro.Text) & "," & nYard & ",0,'" & Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "')"
                        md.eResp = ws.ExecSQL(md.strConnect, strSql)
                        Me.Insert_Changes("New Yard" & Trim(Str(nYard)), Me.wPort_Nro.Text, Trim(Yard_New), "")
                    Next
                End If
                Flag_Modo = 1
                Modo_Edit_ADD_Read_Only(1)
            End If
            strSql = Nothing
            vResp = Nothing
            PortL_Name = Nothing
            PortD_Name = Nothing
            nRoute_ID = Nothing
            Agent_Account = Nothing
            nCode = Nothing
            nFMC = Nothing
            nPort = Nothing
            IsPort = Nothing

            If Me.chk_Only_Ports.Checked = True Then
                Me.Refresh_Ports(1)
            Else
                Me.Refresh_Ports(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo <> 2 Then
            Flag_Modo = 2
            Dim nPort As Integer = 1
            If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
                nPort = Me.wPort_Nro.Text
            End If
            Me.Agents()
            Me.Refresh_Agents()
            Me.Yards()
            Me.Refresh_Yards()
            Me.Modo_Edit_ADD_Read_Only(2)
            Me.wPort_Nro.Text = nPort
            Me.bnt_Save.Enabled = False
            Me.bnt_Del.Enabled = True
            Me.bnt_New.Enabled = False
            nPort = Nothing
        Else
            Flag_Modo = 1
            Dim nPort As Integer = 0
            nPort = Me.wPort_Nro.Text
            Me.Modo_Edit_ADD_Read_Only(1)
            Me.wPort_Nro.Text = nPort
            Me.bnt_Save.Enabled = False
            Me.bnt_Del.Enabled = False
            Me.bnt_New.Enabled = True
            nPort = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Del_Click(sender As Object, e As EventArgs) Handles bnt_Del.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                If Len(Trim(Me.wPort_Nro.Text)) = 0 Then
                    MsgBox("Booking # field is empty,....")
                    Me.wPort_Nro.Focus()
                    Exit Sub
                End If
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Top 1 Booking_Number From Bookings_Headline Where Port_Loading = " & Me.wPort_Nro.Text & " or Port_Discharge = " & Me.wPort_Nro.Text & " or Port_Origin = " & Me.wPort_Nro.Text & " or Port_Transh = " & Me.wPort_Nro.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    MsgBox("You can not delete this port, because There are Bks with this Port,....")
                End If

                ds.Clear()
                ds = ws.GetDataset(md.strConnect, "Select Top 1 DR_Number From DockReceipts Where Port_Loading = " & Me.wPort_Nro.Text & " or Port_Disch = " & Me.wPort_Nro.Text & " or Port_Transh = " & Me.wPort_Nro.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    MsgBox("You can not delete this port, because There are DRs with this Port,....")
                End If
                ' ------ Update [Delete],... Delete this Port
                Dim vResp As String = ""
                vResp = ws.ExecSQL(md.strConnect, "Update Ports Set [Delete] = 'Y' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                Me.Ds_Ports_Master.Clear()
                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                If Me.chk_Only_Ports.Checked = True Then
                    Me.Refresh_Ports(1)
                Else
                    Me.Refresh_Ports(0)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Country_Search_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Country_Search.SelectionChangeCommitted
        Dim strSQL As String = "SELECT p.PORT_NUMBER,p.PORT_NAME
                                              ,p.COUNTRY
                                              ,p.PORT_SHORT
                                              ,p.RATE_AS
                                              ,p.PORT_CODE
                                              ,p.COUNTRY_CODE
                                              ,p.THRU_PORT
                                              ,p.ISISPORT
                                              ,p.AMSPORT
                                              ,p.EMPTY_BL
                                              ,p.Port_Int_Code,p.uid,p.Actived,isnull(p.MUnit,'') as MUnit,isnull(p.WUnit,'') as Wunit, 
                                              isNull(p.Agent_Account,0) as Agent_Account,isnull(p.FMC_Percent,0) as FMC_Percent,
                                              isNull(p.PortPlace,'N') as IsPort,isNull(p.Place,'N') as Place
                                        FROM Ports as p Inner Join 
                                             CM_System as n ON n.Company_Number = isNull(p.Agent_Account,0) and n.Location_Number = 0
                                         where "
        If Len(Trim(cb_Country_Search.SelectedValue)) = 0 Then
            strSQL = Trim(strSQL) & " p.[Delete] is NULL order by port_name"
        Else
            strSQL = Trim(strSQL) & " p.Country_Code = '" & Trim(cb_Country_Search.SelectedItem("A2_ISO")) & "' and p.[Delete] is NULL order by p.port_name"
        End If
        ds_Ports.Clear()
        ds_Ports = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_Ports.Tables(0).Rows.Count > 0 Then
            Me.cb_Ports.DataSource = ds_Ports.Tables(0)
            Me.cb_Ports.DisplayMember = "Port_name"
            Me.cb_Ports.ValueMember = "Port_Number"
            Me.cb_Ports.Refresh()
        Else
            MsgBox("There is not port for this Country,..")
        End If
    End Sub

    Private Sub wCustoms_Code_Validating(sender As Object, e As CancelEventArgs) Handles wCustoms_Code.Validating
        If Not IsNumeric(Me.wCustoms_Code.Text) Then
            MsgBox("This field must be numeric,..")
            Me.wCustoms_Code.Clear()
            Me.wCustoms_Code.Focus()
        End If
    End Sub

#End Region

#Region "Changes"
    Private Sub chk_list_Agent_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chk_list_Agent.ItemCheck
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
                Dim strSQL As String = ""
                Dim n As Integer = e.Index
                Dim nAgent As Integer = 0
                Dim pos As Integer = 0
                Dim Agent_New As String = ""
                Agent_New = Me.chk_list_Agent.Items(n).ToString
                pos = InStr(Agent_New, " -- ")
                nAgent = CInt(Mid(Agent_New, 1, pos - 1))
                Dim vDesc As String = ""
                Dim ds As New DataSet
                If e.NewValue = 1 Then
                    ds.Clear()
                    ds = ws.GetDataset(md.strConnect, "Select top 1 Port_Number, Agent From Port_Agents Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Agent = " & nAgent, 1)
                    If ds.Tables(0).Rows.Count = 0 Then
                        ' ------- New Agent
                        vDesc = "New Agent: " & Trim(Str(nAgent))
                        strSQL = "Insert Into Port_Agents (Port_Number, Agent, Location_Number, Created_By, Created_ON) Values (" & Trim(Me.wPort_Nro.Text) & "," & nAgent & ",0,'" & Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "')"
                        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                        Me.Insert_Changes(Trim(vDesc), Me.wPort_Nro.Text, Trim(Agent_New), "")
                    End If
                    'Audit_Search()
                Else
                    ' ------- Change to False
                    ds.Clear()
                    ds = ws.GetDataset(md.strConnect, "Select top 1 Port_Number, Agent From Port_Agents Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Agent = " & nAgent, 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        ' ------- Remove Agent
                        vDesc = "Remove Agent: " & Trim(Str(nAgent))
                        strSQL = "Delete Port_Agents Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Agent = " & nAgent
                        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                        Me.Insert_Changes(Trim(vDesc), Me.wPort_Nro.Text, Trim(Agent_New), "")
                    End If
                End If ' ------- e.NewValue
                pos = Nothing
                strSQL = Nothing
                n = Nothing
                nAgent = Nothing
                Agent_New = Nothing
                vDesc = Nothing
                ds = Nothing
            End If  ' ------- wPort_Nro
        End If ' ------- Flag_Modo
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Ports_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        md.Insert_User_Log("Closing Ports", md.UserName)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.TabControl1.SelectedIndex = 1 Then
            Me.TabControl1.SelectedIndex = 1
            Me.Refresh()
            strSQL = "SELECT p.uid, p.[PORT_NAME],p.[PORT_NUMBER],p.[PORT_SHORT],p.[PORT_CODE],p.[THRU_PORT], pp.PORT_NAME as Thru_name,p.[RATE_AS],p.[COUNTRY],p.[COUNTRY_CODE],p.[TRANSLATION],p.[WUNIT],p.[MUNIT],p.[ISISPORT],p.[AMSPORT],p.Port_Int_Code,
			                   IsNull(p.Agent_Account,0) as Agent_Account,isnull(p.FMC_Percent,0) as FMC_Percent,isNull(p.PortPlace,'N') as IsPort,isNull(p.Place,'N') as Place
			            FROM [Ports] as p inner Join 
			              Ports as pp ON p.THRU_PORT = pp.PORT_NUMBER
			              Where Isnull(p.[Delete],'') = ''"
            If rd_All.Checked = False Then
                If Me.rd_Thru.Checked = True Then
                    strSQL = Trim(strSQL) & " and PortPlace = 'Y'"
                Else
                    strSQL = Trim(strSQL) & " and Place = 'Y'"
                End If
            End If
            strSQL = Trim(strSQL) & " Order by P.PORT_NAME"
            Ds_Ports_Master.Clear()
            Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
            If Me.Ds_Ports_Master.Tables(0).Rows.Count > 0 Then
                Dim cr_Ports_Master As New cr_Ports_Master
                cr_Ports_Master.SetDataSource(Ds_Ports_Master.Tables(0))
                Me.cv_Ports.CloseView(cr_Ports_Master)
                Me.cv_Ports.ReportSource = cr_Ports_Master
                Me.cv_Ports.BringToFront()
                Me.cv_Ports.RefreshReport()
                Me.cv_Ports.DisplayToolbar = True
                Me.cv_Ports.DisplayStatusBar = True
                'Me.cv_Sailing_Schedule.Zoom(75)
                Me.cv_Ports.Refresh()
                Me.cv_Ports.Show()
            End If
        End If
        If TabControl1.SelectedIndex = 2 Then
            Me.Refresh_Places()
        End If
        If TabControl1.SelectedIndex = 3 Then
            Me.Refresh_Ports_dgv()
        End If
        If TabControl1.SelectedIndex = 4 Then
            Me.Audit_Search()
        End If
        If TabControl1.SelectedIndex = 5 Then
            ds_Port_Contracts.Clear()
            strSQL = "SELECT CONTRACT_NUMBER, START_DATE, END_DATE, TEU, METRIC_TON, DOLLAR_AMOUNT, DESCRIPTION, Signed, Amendment_nro, Amendment_date, Amendment_Signed
                        FROM (SELECT T1.CONTRACT_NUMBER, Format(h.START_DATE,'MM/dd/yyyy') as Start_Date, Format(h.END_DATE,'MM/dd/yyyy') as End_Date, h.TEU, h.METRIC_TON, h.DOLLAR_AMOUNT, h.DESCRIPTION, h.Signed, ISNULL(h.Amendment_nro, 0) AS Amendment_nro, ISNULL(h.Amendment_Date, N'') 
                                     AS Amendment_date, 
                                   Case ISNULL(h.Amendment_Signed,'')
									    when 'N' then ''
										else ISNULL(h.Amendment_Signed,'')
									end AS Amendment_Signed
                          FROM (SELECT DISTINCT CONTRACT_NUMBER
                                    FROM dbo.Rate_Services
                                 WHERE (PORTD_NUMBER = " & Me.cb_Ports.SelectedValue & " or PortL_Number = " & Me.cb_Ports.SelectedValue & ") AND (ISNULL(CONTRACT_NUMBER, N'') <> '')) AS T1 INNER JOIN
                      dbo.Contract_HDR AS h ON h.CONTRACT_NUMBER = T1.CONTRACT_NUMBER) AS T2"
            ds_Port_Contracts = ws.GetDataset(strConnect, strSQL, 1)
            Me.dgv_Contracts.Rows.Clear()
            If ds_Port_Contracts.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds_Port_Contracts.Tables(0).Rows.Count - 1
                    Me.dgv_Contracts.Rows.Add(Trim(ds_Port_Contracts.Tables(0).Rows(j).Item("Contract_Number")), ds_Port_Contracts.Tables(0).Rows(j).Item("START_DATE"), ds_Port_Contracts.Tables(0).Rows(j).Item("END_DATE"), ds_Port_Contracts.Tables(0).Rows(j).Item("TEU"), ds_Port_Contracts.Tables(0).Rows(j).Item("METRIC_TON"), ds_Port_Contracts.Tables(0).Rows(j).Item("DOLLAR_AMOUNT"), ds_Port_Contracts.Tables(0).Rows(j).Item("DESCRIPTION"), ds_Port_Contracts.Tables(0).Rows(j).Item("Signed"), ds_Port_Contracts.Tables(0).Rows(j).Item("Amendment_nro"), ds_Port_Contracts.Tables(0).Rows(j).Item("Amendment_Date"), ds_Port_Contracts.Tables(0).Rows(j).Item("Amendment_Signed"))
                Next
            End If
            Me.dgv_Contracts.Refresh()
        End If
    End Sub
    Private Sub Refresh_Places()
        Me.Clear()
        Me.dgv_Places.Rows.Clear()
        Me.wCount_Places.Clear()
        strSQL = "SELECT PORT_NUMBER, PORT_NAME, COUNTRY, ISNULL(Actived, 'N') AS Actived, ISNULL(Place, 'N') AS Place, isNull(Place,'N') as IsPlace, CREATED_BY,  Format(ISNULL(CREATION_DATE, '12/31/1999'), 'MM/dd/yyyy') AS Creation_Date
                       FROM dbo.Ports WHERE (ISNULL(Place, N'N') = 'Y') ORDER BY PORT_NUMBER"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Places.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Port_Number")), Trim(ds.Tables(0).Rows(j).Item("Port_Name")), Trim(ds.Tables(0).Rows(j).Item("Country")), Trim(ds.Tables(0).Rows(j).Item("Actived")), Trim(ds.Tables(0).Rows(j).Item("IsPlace")), Trim(ds.Tables(0).Rows(j).Item("Created_By")), Trim(ds.Tables(0).Rows(j).Item("CREATION_DATE")))
            Next
            Me.wCount_Places.Text = ds.Tables(0).Rows.Count
        End If
        ds = Nothing
    End Sub
    Private Sub wPort_Int_Code_KeyDown(sender As Object, e As KeyEventArgs) Handles wPort_Int_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wPort_Int_Code.Text)) > 0 Then
                        Dim ds As New DataSet
                        ds = ws.GetDataset(strConnect, "Select Port_Int_Code From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Port_Int_Code")) <> Trim(Me.wPort_Int_Code.Text) Then
                                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Port_Int_Code = '" & Trim(Me.wPort_Int_Code.Text) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                                Me.Insert_Changes("Changed International Code: " & Trim(Me.wPort_Int_Code.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Int_Code")))
                                Me.Ds_Ports_Master.Clear()
                                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                                If Me.chk_Only_Ports.Checked = True Then
                                    Me.Refresh_Ports(1)
                                Else
                                    Me.Refresh_Ports(0)
                                End If
                                Me.cb_Ports.SelectedValue = nPort
                                Me.cb_Ports.Refresh()
                                nPort = Nothing
                            End If
                        End If
                        ds = Nothing
                    Else
                        MsgBox("Port International field is empty,...")
                        Me.wPort_Int_Code.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wCustoms_Code_KeyDown(sender As Object, e As KeyEventArgs) Handles wCustoms_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wCountry_Code.Text)) > 0 Then
                        If IsNumeric(Me.wCustoms_Code.Text) Then
                            Dim ds As New DataSet
                            ds = ws.GetDataset(strConnect, "Select Port_Code From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                            If ds.Tables(0).Rows.Count > 0 Then
                                If Trim(ds.Tables(0).Rows(0).Item("Port_Code")) <> Trim(Me.wCustoms_Code.Text) Then
                                    ' ------ Update Route
                                    Dim nPort As Integer = Me.cb_Ports.SelectedValue
                                    eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Port_Code = " & Trim(Me.wCustoms_Code.Text) & " Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                                    Me.Insert_Changes("Changed Customs Code: " & Trim(Me.wCustoms_Code.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Code")))
                                    Me.Ds_Ports_Master.Clear()
                                    Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                                    If Me.chk_Only_Ports.Checked = True Then
                                        Me.Refresh_Ports(1)
                                    Else
                                        Me.Refresh_Ports(0)
                                    End If
                                    Me.cb_Ports.SelectedValue = nPort
                                    Me.cb_Ports.Refresh()
                                    nPort = Nothing
                                End If
                            End If
                            ds = Nothing
                        End If
                    Else
                        MsgBox("Customs code field is empty,...")
                        Me.wCustoms_Code.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wPort_Name_KeyDown(sender As Object, e As KeyEventArgs) Handles wPort_Name.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wPort_Name.Text)) > 0 Then
                        Dim ds As New DataSet
                        ds = ws.GetDataset(strConnect, "Select Port_Name From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Port_Name")) <> Trim(Me.wPort_Name.Text) Then
                                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Port_Name = '" & Trim(Me.wPort_Name.Text) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                                Me.Insert_Changes("Changed Port Name: " & Trim(Me.wPort_Name.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Name")))
                                Me.Ds_Ports_Master.Clear()
                                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                                If Me.chk_Only_Ports.Checked = True Then
                                    Me.Refresh_Ports(1)
                                Else
                                    Me.Refresh_Ports(0)
                                End If
                                Me.cb_Ports.SelectedValue = nPort
                                Me.cb_Ports.Refresh()
                                nPort = Nothing
                            End If
                        End If
                        ds = Nothing
                    Else
                        MsgBox("Port Name field is empty,...")
                        Me.wPort_Name.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wAbbreviation_KeyDown(sender As Object, e As KeyEventArgs) Handles wAbbreviation.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wAbbreviation.Text)) > 0 Then
                        Dim ds As New DataSet
                        ds = ws.GetDataset(strConnect, "Select Port_Short From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Port_Short")) <> Trim(Me.wPort_Name.Text) Then
                                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Port_Short = '" & Trim(Me.wAbbreviation.Text) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                                Me.Insert_Changes("Changed Abbreviation: " & Trim(Me.wAbbreviation.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Short")))
                                Me.Ds_Ports_Master.Clear()
                                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                                If Me.chk_Only_Ports.Checked = True Then
                                    Me.Refresh_Ports(1)
                                Else
                                    Me.Refresh_Ports(0)
                                End If
                                Me.cb_Ports.SelectedValue = nPort
                                Me.cb_Ports.Refresh()
                                nPort = Nothing
                            End If
                        End If
                        ds = Nothing
                    Else
                        MsgBox("Abbreviation Port Code field is empty,...")
                        Me.wAbbreviation.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wCountry_Code_KeyDown(sender As Object, e As KeyEventArgs) Handles wCountry_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wPort_Int_Code.Text)) > 0 Then
                        ' ------ Update Route
                        Dim vResp As String = ""
                        vResp = ws.ExecSQL(md.strConnect, "Update Ports Set Country_Code = '" & Trim(Me.wCountry_Code.Text) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                        If Trim(vResp) <> "Success" Then
                            MsgBox(vResp)
                            Exit Sub
                        End If
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                    Else
                        MsgBox("Country Code field is empty,...")
                        Me.wCountry_Code.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub cb_measure_Unit_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_measure_Unit.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select isnull(MUnit,'') as MUnit From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("MUnit")) <> Trim(Me.cb_measure_Unit.SelectedValue) Then
                        strSQL = "Update Ports Set MUnit = '" & Trim(Me.cb_measure_Unit.SelectedValue) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid")
                        eResp = ws.ExecSQL(md.strConnect, strSQL)
                        Me.Insert_Changes("Measure Unit Changed: " & Trim(Me.cb_measure_Unit.SelectedValue), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("MUnit")))
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        nPort = Nothing
                    End If
                End If
                nPort = Nothing
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Weight_Unit_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Weight_Unit.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                ' ------ Update Route
                Dim nPort As Integer = Me.cb_Ports.SelectedItem("Port_Number")
                Dim vResp As String = ""
                Dim ds As New DataSet
                Dim nID As Integer = Me.cb_Ports.SelectedItem("uid")
                ds = ws.GetDataset(strConnect, "Select isnull(WUnit,'') as WUnit From Ports Where uid = " & nID, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("WUnit")) <> Trim(Me.cb_Weight_Unit.SelectedValue) Then
                        strSQL = "Update Ports Set WUnit = '" & Trim(Me.cb_Weight_Unit.SelectedValue) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid")
                        vResp = ws.ExecSQL(md.strConnect, strSQL)
                        If Trim(vResp) <> "Success" Then
                            MsgBox(vResp)
                            Exit Sub
                        End If
                        Me.Insert_Changes("Weight Unit Changed: " & Trim(Me.cb_Weight_Unit.SelectedValue), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("WUnit")))
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        Me.Refresh_Port_Seleted()
                    End If
                End If
                nID = Nothing
                nPort = Nothing
                vResp = Nothing
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub rd_Port_CheckedChanged(sender As Object, e As EventArgs) Handles rd_Port.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim vPort As String = "Y"
            Dim vPlace As String = "N"
            If Me.rd_Port.Checked = False Then
                vPort = "N"
                vPlace = "Y"
            End If
            If Flag_Modo = 2 Then
                Dim nPort As Integer = Me.cb_Ports.SelectedValue
                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set PortPlace = '" & Trim(vPort) & "', Place = '" & vPlace & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                Me.Insert_Changes("Changed Port: " & Trim(vPort) & " / Place: " & Trim(vPlace), nPort, Me.wPort_Name.Text, "")
                Me.Ds_Ports_Master.Clear()
                Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                If Me.chk_Only_Ports.Checked = True Then
                    Me.Refresh_Ports(1)
                Else
                    Me.Refresh_Ports(0)
                End If
                Me.cb_Ports.SelectedValue = nPort
                Me.cb_Ports.Refresh()
                nPort = Nothing
            End If
            If Me.rd_Port.Checked = True Then
                Me.gb_Port_Info.Visible = True
                Me.gb_Yards.Visible = True
                Me.gb_Agents.Visible = True
                Me.gb_BL_Terminal.Visible = True
            Else
                Me.gb_Port_Info.Visible = False
                Me.gb_Yards.Visible = False
                Me.gb_Agents.Visible = False
                Me.gb_BL_Terminal.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Only_Ports_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Only_Ports.CheckedChanged
        If Me.chk_Only_Ports.Checked = True Then
            Me.Refresh_Ports(1)
        Else
            Me.Refresh_Ports(0)
        End If
    End Sub

    Private Sub chk_list_Yards_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chk_list_Yards.ItemCheck
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
                Dim strSQL As String = ""
                Dim n As Integer = e.Index
                Dim nYard As Integer = 0
                Dim pos As Integer = 0
                Dim Yard_New As String = ""
                Yard_New = Me.chk_list_Yards.Items(n).ToString
                pos = InStr(Yard_New, " -- ")
                nYard = CInt(Mid(Yard_New, 1, pos - 1))
                Dim vDesc As String = ""
                Dim ds As New DataSet
                If e.NewValue = 1 Then
                    ds.Clear()
                    ds = ws.GetDataset(md.strConnect, "Select top 1 Port_Number, Yard From Port_Yards Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Yard = " & nYard, 1)
                    If ds.Tables(0).Rows.Count = 0 Then
                        ' ------- New Yard
                        vDesc = "New Yard: " & Trim(Str(nYard))
                        strSQL = "Insert Into Port_Yards (Port_Number, Yard, Location, Created_By, Created_ON) Values (" & Trim(Me.wPort_Nro.Text) & "," & nYard & ",0,'" & Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "')"
                        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                        Me.Insert_Changes(Trim(vDesc), Me.wPort_Nro.Text, Trim(Yard_New), "")
                    End If
                    'Audit_Search()
                Else
                    ' ------- Change to False
                    ds.Clear()
                    ds = ws.GetDataset(md.strConnect, "Select top 1 Port_Number, Yard From Port_Yards Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Yard = " & nYard, 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        ' ------- Remove Yard
                        vDesc = "Remove Yard: " & Trim(Str(nYard))
                        strSQL = "Delete Port_Yards Where Port_Number = " & Trim(Me.wPort_Nro.Text) & " and Yard = " & nYard
                        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                        Me.Insert_Changes(Trim(vDesc), Me.wPort_Nro.Text, Trim(Yard_New), "")
                    End If
                End If ' ------- e.NewValue
                pos = Nothing
                strSQL = Nothing
                n = Nothing
                nYard = Nothing
                Yard_New = Nothing
                vDesc = Nothing
                ds = Nothing
            End If  ' ------- wPort_Nro
        End If ' ------- Flag_Modo
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


#End Region

#Region "Places"

    Dim ComboMedType As ComboBox
    Private Sub dgv_Places_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_Places.EditingControlShowing
        Try
            If Me.dgv_Places.CurrentCellAddress.X = 3 Or Me.dgv_Places.CurrentCellAddress.X = 4 Then
                '------ ComboBox
                ComboMedType = CType(e.Control, ComboBox)
                If (ComboMedType IsNot Nothing) Then
                End If
                AddHandler ComboMedType.SelectedValueChanged, AddressOf ComboMedType_SelectedValueChanged
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ComboMedType_SelectedValueChanged(sender As Object, e As EventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_Places.CurrentCellAddress.X = 3 Then
                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Actived = '" & Trim(ComboMedType.SelectedItem) & "' Where Port_Number = " & Me.dgv_Places.Item(0, Me.dgv_Places.CurrentCell.RowIndex).Value)
                Me.Insert_Changes("Changed Active : " & Trim(Me.ComboMedType.SelectedItem), Me.dgv_Places.Item(0, Me.dgv_Places.CurrentCell.RowIndex).Value, Trim(Me.dgv_Places.Item(1, Me.dgv_Places.CurrentCell.RowIndex).Value), Trim(Me.dgv_Places.Item(3, Me.dgv_Places.CurrentCell.RowIndex).Value))
                Me.Refresh_Places()
            End If
            If Me.dgv_Places.CurrentCellAddress.X = 4 Then
                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Place = '" & Trim(ComboMedType.SelectedItem) & "' Where Port_Number = " & Me.dgv_Places.Item(0, Me.dgv_Places.CurrentCell.RowIndex).Value)
                Me.Insert_Changes("Changed Port : " & Trim(Me.ComboMedType.SelectedItem), Me.dgv_Places.Item(0, Me.dgv_Places.CurrentCell.RowIndex).Value, Trim(Me.dgv_Places.Item(1, Me.dgv_Places.CurrentCell.RowIndex).Value), Trim(Me.dgv_Places.Item(4, Me.dgv_Places.CurrentCell.RowIndex).Value))
                Me.Refresh_Places()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

    Private Sub Refresh_Agents_by_Port()
        Dim j As Integer = 0
        Me.chk_list_Agent.Items.Clear()
        j = 0
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Distinct a.Agent, n.Company_Name From Port_Agents as a Inner Join CM_System as n on a.Agent = n.Company_Number Where a.Port_Number = " & Trim(Me.wPort_Nro.Text), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.chk_list_Agent.Items.Add(Trim(Str(ds.Tables(0).Rows(j).Item("Agent"))) & " -- " & Trim(ds.Tables(0).Rows(j).Item("Company_Name")), True)
            Next
        End If
        ds = Nothing
        j = Nothing
    End Sub

    Private Sub Refresh_Yards_by_Ports()
        Dim j As Integer = 0
        Me.chk_list_Yards.Items.Clear()
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Distinct y.Yard, n.Company_Name From Port_Yards as y Inner Join CM_System as n on y.Yard = n.Company_Number  Where y.Port_Number = " & Trim(Me.wPort_Nro.Text), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.chk_list_Yards.Items.Add(Trim(Str(ds.Tables(0).Rows(j).Item("Yard"))) & " -- " & Trim(ds.Tables(0).Rows(j).Item("Company_Name")), True)
            Next
        End If
        ds = Nothing
        j = Nothing
    End Sub

    Private Sub Refresh_Ports_dgv()
        Me.dgv_Ports.Rows.Clear()
        Me.wCount_Ports.Clear()
        strSQL = "SELECT PORT_NUMBER, PORT_NAME, COUNTRY, ISNULL(Actived, 'N') AS Actived, ISNULL(PortPlace, 'N') AS IsPort, ISNULL(Place, 'N') AS Place, CREATED_BY,  Format(ISNULL(CREATION_DATE, '12/31/1999'), 'MM/dd/yyyy') AS Creation_Date
                       FROM dbo.Ports WHERE (ISNULL(PortPlace, N'N') = 'Y') ORDER BY PORT_NUMBER"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Ports.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Port_Number")), Trim(ds.Tables(0).Rows(j).Item("Port_Name")), Trim(ds.Tables(0).Rows(j).Item("Country")), Trim(ds.Tables(0).Rows(j).Item("Actived")), Trim(ds.Tables(0).Rows(j).Item("IsPort")), Trim(ds.Tables(0).Rows(j).Item("Created_By")), Trim(ds.Tables(0).Rows(j).Item("CREATION_DATE")))
            Next
            Me.wCount_Ports.Text = ds.Tables(0).Rows.Count
        End If
        ds = Nothing
    End Sub

    Private Sub dgv_Ports_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Ports.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Ports.RowCount Then
            If Me.dgv_Ports.RowCount > 0 Then
                Me.cb_Ports.SelectedValue = Trim(Me.dgv_Ports.Item(0, e.RowIndex).Value)
                Me.Refresh_Port_Seleted()
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_Places_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Places.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Places.RowCount Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Me.cb_Ports.SelectedValue = Me.dgv_Places.Item(0, e.RowIndex).Value
            Me.Refresh_Port_Seleted()
            Me.TabControl1.SelectedIndex = 0
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub Insert_Changes(ByVal Desc As String, ByVal Port_Number As Integer, ByVal Port_Name As String, ByVal Old_Name As String)
        Dim strSQL As String = "Insert Into PortsAudit (
                                               Created_ON, 
                                               Created_By, 
                                               Description, 
                                               Port_Number,
                                               Port_Name,
                                               Old_Name
                                                     ) Values ('" &
                                  System.DateTime.Now & "','" &
                                  Trim(Mid(System.Environment.UserName, 1, 30)) & "','" &
                                  Trim(Mid(Replace(Desc, "'", "''"), 1, 70)) & "'," &
                                  Port_Number & ",'" &
                                  Trim(Mid(Replace(Port_Name, "'", "''"), 1, 30)) & "','" &
                                  Trim(Mid(Replace(Old_Name, "'", "''"), 1, 30)) & "')"
        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
        If md.eResp <> "Success" Then
            MsgBox(Trim(md.eResp))
        End If
        strSQL = Nothing
    End Sub
    Private Sub Audit_Search()
        If Len(Trim(Me.wPort_Name.Text)) > 0 Then
            If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
                ' ------- Audit
                Dim ds As New DataSet
                strSQL = "SELECT Port_Number, Port_Name, Description, Old_Name, Created_By, Created_On FROM PortsAudit where Port_Number = " & Trim(Me.wPort_Nro.Text) & " Order By uid Desc "
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.dgv_Audit.Rows.Clear()
                    Dim i As Integer = 0
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Me.dgv_Audit.Rows.Add(ds.Tables(0).Rows(i).Item("Created_On"), ds.Tables(0).Rows(i).Item("Created_By"), ds.Tables(0).Rows(i).Item("Description"), ds.Tables(0).Rows(i).Item("Old_Name"))
                    Next
                    ds = Nothing
                    i = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub cb_Port_Thru_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Port_Thru.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select Thru_Port From Ports Where uid = " & Me.cb_Ports.SelectedItem("uid"), 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Thru_Port")) <> Trim(Me.cb_Port_Thru.SelectedValue) Then
                        Dim nPort As Integer = Me.cb_Ports.SelectedValue
                        eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Thru_Port = '" & Trim(Me.cb_Port_Thru.SelectedValue) & "' Where uid = " & Me.cb_Ports.SelectedItem("uid"))
                        Dim nnPort As Integer = ds.Tables(0).Rows(0).Item("Thru_Port")
                        ds.Clear()
                        ds = ws.GetDataset(strConnect, "Select Port_Name From Ports Where Port_Number = " & nnPort, 1)
                        If ds.Tables(0).Rows.Count Then
                            Me.Insert_Changes("Changed Thru Port: " & Trim(Me.wPort_Name.Text), nPort, Me.wPort_Name.Text, Trim(ds.Tables(0).Rows(0).Item("Port_Name")))
                        End If
                        nnPort = Nothing
                        Me.Ds_Ports_Master.Clear()
                        Ds_Ports_Master = ws.SP_1_Param_int(md.strConnect, "Ports_Master", "@all", 1)
                        If Me.chk_Only_Ports.Checked = True Then
                            Me.Refresh_Ports(1)
                        Else
                            Me.Refresh_Ports(0)
                        End If
                        Me.cb_Ports.SelectedValue = nPort
                        Me.cb_Ports.Refresh()
                        nPort = Nothing
                    End If
                End If
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#Region "Ports"
    Dim ComboMedType_Ports As ComboBox
    Private Sub dgv_Ports_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_Ports.EditingControlShowing
        Try
            If Me.dgv_Places.CurrentCellAddress.X = 3 Or Me.dgv_Places.CurrentCellAddress.X = 4 Then
                '------ ComboBox
                ComboMedType_Ports = CType(e.Control, ComboBox)
                If (ComboMedType_Ports IsNot Nothing) Then
                End If
                AddHandler ComboMedType_Ports.SelectedValueChanged, AddressOf ComboMedTypePorts_SelectedValueChanged
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboMedTypePorts_SelectedValueChanged(sender As Object, e As EventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_Ports.CurrentCellAddress.X = 3 Then
                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set Actived = '" & Trim(ComboMedType_Ports.SelectedItem) & "' Where Port_Number = " & Me.dgv_Ports.Item(0, Me.dgv_Ports.CurrentCell.RowIndex).Value)
                Me.Insert_Changes("Changed Active : " & Trim(Me.ComboMedType_Ports.SelectedItem), Me.dgv_Ports.Item(0, Me.dgv_Ports.CurrentCell.RowIndex).Value, Trim(Me.dgv_Ports.Item(1, Me.dgv_Ports.CurrentCell.RowIndex).Value), Trim(Me.dgv_Ports.Item(3, Me.dgv_Ports.CurrentCell.RowIndex).Value))
                Me.Refresh_Ports(1)
            End If
            If Me.dgv_Ports.CurrentCellAddress.X = 4 Then
                eResp = ws.ExecSQL(md.strConnect, "Update Ports Set PortPlace = '" & Trim(ComboMedType_Ports.SelectedItem) & "' Where Port_Number = " & Me.dgv_Ports.Item(0, Me.dgv_Ports.CurrentCell.RowIndex).Value)
                Me.Insert_Changes("Changed Port : " & Trim(Me.ComboMedType_Ports.SelectedItem), Me.dgv_Ports.Item(0, Me.dgv_Ports.CurrentCell.RowIndex).Value, Trim(Me.dgv_Ports.Item(1, Me.dgv_Ports.CurrentCell.RowIndex).Value), Trim(Me.dgv_Ports.Item(4, Me.dgv_Ports.CurrentCell.RowIndex).Value))
                Me.Refresh_Ports(1)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#Region "BL / BK Notes"
    Private Sub bnt_Save_Terminal_Clauses_Click(sender As Object, e As EventArgs) Handles bnt_Save_Terminal_Clauses.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
            strSQL = "Select isnull(Clause,'') as Clause From BL_Terminal_Clauses Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = 1 "
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                strSQL = "Update BL_Terminal_Clauses Set Clause = '" & Trim(Replace(Me.txt_BL_Terminal_Clauses.Text, "'", "''")) & "', Update_By = '" & System.Environment.UserName & "', Update_ON = '" & System.DateTime.Now & "'  Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = 1"
                eResp = ws.ExecSQL(strConnect, strSQL)
            Else
                If Len(Trim(Me.txt_BL_Terminal_Clauses.Text)) > 0 Then
                    strSQL = "Insert Into BL_Terminal_Clauses (Terminal, Terminal_Dest, Clause, Created_By, Created_ON) Values (" & Me.wPort_Nro.Text & ",1,'" & Trim(Replace(Me.txt_BL_Terminal_Clauses.Text, "'", "''")) & "','" & System.Environment.UserName & "','" & System.DateTime.Now & "')"
                    eResp = ws.ExecSQL(strConnect, strSQL)
                End If
            End If
            ds = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Save_Terminal_Discharge_Clauses_Click(sender As Object, e As EventArgs) Handles bnt_Save_Terminal_Discharge_Clauses.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
            Dim response = MsgBox("Are you sure, Do you want continue (Y)es or (N)o [Save]?", MsgBoxStyle.YesNo, "Warning")
            If response = MsgBoxResult.No Then
                Exit Sub
            End If
            strSQL = "Select isnull(Clause,'') as Clause From BL_Terminal_Clauses Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = " & Me.cb_Port_Discharge_Clauses.SelectedValue
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                strSQL = "Update BL_Terminal_Clauses Set Clause = '" & Trim(Replace(Me.txt_BL_Terminal_Clauses_Port_Discharge.Text, "'", "''")) & "', Update_By = '" & System.Environment.UserName & "', Update_ON = '" & System.DateTime.Now & "'  Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = " & Me.cb_Port_Discharge_Clauses.SelectedValue
                eResp = ws.ExecSQL(strConnect, strSQL)
            Else
                If Len(Trim(Me.txt_BL_Terminal_Clauses_Port_Discharge.Text)) > 0 Then
                    strSQL = "Insert Into BL_Terminal_Clauses (Terminal, Terminal_Dest, Clause, Created_By, Created_ON) Values (" & Me.wPort_Nro.Text & "," & Me.cb_Port_Discharge_Clauses.SelectedValue & ",'" & Trim(Replace(Me.txt_BL_Terminal_Clauses_Port_Discharge.Text, "'", "''")) & "','" & System.Environment.UserName & "','" & System.DateTime.Now & "')"
                    eResp = ws.ExecSQL(strConnect, strSQL)
                End If
            End If
            ds = Nothing
        Else
            MsgBox("Please chosse a port,..")
            Me.cb_Port_Discharge_Clauses.Focus()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
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
        toolTip1.SetToolTip(wCustoms_Code, "Customs and Border Protection (CBP), US Customs Appendix D Export Port Codes or Appendix F Foreign Port Codes.")
        toolTip1.SetToolTip(wPort_Int_Code, "United Nations UN/LOCODE Port Codes, Country Code + Abbreviation Code. Go to https://www.cbp.gov/sites/default/files/assets/documents/2017-Feb/appendix_f_0.pdf")
        toolTip1.SetToolTip(wAbbreviation, "United Nations UN/LOCODE Port Codes, without Country Code.")
        toolTip1.SetToolTip(txt_BL_Terminal_Clauses, "BLs clauses of the terminal, to be showed in the bottom of the B/L. Example- THESE COMMODITIES, TECHNOLOGY OR SOFTWARE WERE EXPORTED FROM THE UNITED STATES,....")
        toolTip1.SetToolTip(txt_BL_Terminal_Clauses_Port_Discharge, "Booking notes of the port discharge, to be showed in the bottom of the Booking. Example- Transit time to Guyana and Suriname is an estimated 12 - 15 days and is subject to change without notice.")
    End Sub

    Private Sub cb_Port_Discharge_Clauses_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Port_Discharge_Clauses.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.txt_BL_Terminal_Clauses_Port_Discharge.Clear()
        strSQL = "Select isNull(Clause,'') as Clause, Terminal_Dest From BL_Terminal_Clauses Where Terminal = " & Me.wPort_Nro.Text & " and Terminal_Dest = " & Me.cb_Port_Discharge_Clauses.SelectedValue
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.cb_Port_Discharge_Clauses.SelectedValue = ds.Tables(0).Rows(0).Item("Terminal_Dest")
            Me.txt_BL_Terminal_Clauses_Port_Discharge.Text = FormatStrLine(Trim(ds.Tables(0).Rows(0).Item("Clause")))
        Else
            Me.cb_Port_Discharge_Clauses.SelectedValue = 1
            Me.txt_BL_Terminal_Clauses_Port_Discharge.Clear()
        End If
        ds = Nothing
        Me.Modo_Edit_ADD_Read_Only(1)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Dim nLeng As Integer = 0


    Private Sub cb_Ports_GotFocus(sender As Object, e As EventArgs) Handles cb_Ports.GotFocus
        nLeng = 0
    End Sub

    Private Sub cb_Ports_LostFocus(sender As Object, e As EventArgs) Handles cb_Ports.LostFocus
        nLeng = 0
        Me.Refresh_Port_Seleted()
    End Sub

    Private Sub cb_Ports_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_Ports.KeyPress
        If (Char.IsControl(e.KeyChar)) Then Return
        Dim Str As String = cb_Ports.Text.Substring(0, cb_Ports.SelectionStart) + e.KeyChar

        Dim Index As Integer = cb_Ports.FindStringExact(Str)
        If Index = -1 Then
            Index = cb_Ports.FindString(Str)
        End If
        Me.cb_Ports.SelectedIndex = Index
        Me.cb_Ports.SelectionStart = Str.Length
        Me.cb_Ports.SelectionLength = Me.cb_Ports.Text.Length - Me.cb_Ports.SelectionStart
        e.Handled = True
    End Sub

    Private Sub bnt_Search_Abbreviation_Click(sender As Object, e As EventArgs) Handles bnt_Search_Abbreviation.Click
        If Len(Trim(Me.wAbbreviation.Text)) > 0 Then
            Me.wPort_Nro.Text = md.Ports_Number_x_Short_(Me.wAbbreviation.Text)
            Me.cb_Ports.SelectedValue = Me.wPort_Nro.Text
            Me.Refresh_Port_Seleted()
        End If
    End Sub

    Private Sub bnt_Search_Port_nro_Click(sender As Object, e As EventArgs) Handles bnt_Search_Port_nro.Click
        If Len(Trim(Me.wPort_Nro.Text)) > 0 Then
            Me.cb_Ports.SelectedValue = Me.wPort_Nro.Text
            Me.Refresh_Port_Seleted()
        End If
    End Sub
End Class