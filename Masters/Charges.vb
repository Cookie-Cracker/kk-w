Imports System.ComponentModel

Public Class Shipping_Rates
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1

    Private Sub Shipping_Rates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, "Shipping_Rates")) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        Ds_Shipping_Rate_Master = ws.SP(md.strConnect, "Shipping_Rate_Master")
        Dim i As Integer = 0
        Me.Refresh_Rating()
        ds_FCL.Clear()
        ds_FCL = ws.GetDataset(md.strConnect, "SELECT RATING_TYPE,CHARGE_TYPE,CALC_DESC FROM Rate_Type WHERE (actived = 'Y') ORDER BY CHARGE_TYPE", 1)
        If ds_FCL.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds_FCL.Tables(0).Rows.Count - 1
                cb_FCL.DataSource = ds_FCL.Tables(0)
                cb_FCL.DisplayMember = "Rating_Type"
                cb_FCL.ValueMember = "Rating_Type"
            Next
        End If
        ds_LCL.Clear()
        ds_LCL = ws.GetDataset(md.strConnect, "SELECT RATING_TYPE,CHARGE_TYPE,CALC_DESC FROM Rate_Type WHERE (actived = 'Y') ORDER BY CHARGE_TYPE", 1)
        If ds_LCL.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds_LCL.Tables(0).Rows.Count - 1
                cb_LCL.DataSource = ds_LCL.Tables(0)
                cb_LCL.DisplayMember = "Rating_Type"
                cb_LCL.ValueMember = "Rating_Type"
            Next
        End If
        ds_OVS.Clear()
        ds_OVS = ws.GetDataset(md.strConnect, "SELECT RATING_TYPE,CHARGE_TYPE,CALC_DESC FROM Rate_Type WHERE (actived = 'Y') ORDER BY CHARGE_TYPE", 1)
        If ds_OVS.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds_FCL.Tables(0).Rows.Count - 1
                cb_OVS.DataSource = ds_OVS.Tables(0)
                cb_OVS.DisplayMember = "Rating_Type"
                cb_OVS.ValueMember = "Rating_Type"
            Next
        End If
        ds_AR_Acc.Clear()
        ds_AR_Acc = ws.GetDataset(md.strConnect, "SELECT ACCOUNT,rtrim(account) + '  -  ' + rtrim([DESC_ENG]) as Desc_Acc  FROM GLACCT Where COMPANY_NUMBER = " & md.GL_Company & " and (substring(account,1,1) = '4' or substring(account,1,1) = '6' or substring(account,1,1) = '7') UNION
                    Select ' ' as Account,' ' as Desc_Acc From GLAcct Order By ACCOUNT", 1)
        If ds_AR_Acc.Tables(0).Rows.Count > 0 Then
            Me.cb_AR_Acc.DataSource = ds_AR_Acc.Tables(0)
            Me.cb_AR_Acc.DisplayMember = "Desc_Acc"
            Me.cb_AR_Acc.ValueMember = "Account"
        End If
        ds_AP_Acc.Clear()
        ds_AP_Acc = ws.GetDataset(md.strConnect, "SELECT ACCOUNT,rtrim(account) + '  -  ' + rtrim([DESC_ENG]) as Desc_Acc  FROM GLACCT Where COMPANY_NUMBER = " & md.GL_Company & " and (substring(account,1,1) = '4' or substring(account,1,1) = '6' or substring(account,1,1) = '7')  UNION
                    Select ' ' as Account,' ' as Desc_Acc From GLAcct Order By ACCOUNT", 1)
        If ds_AP_Acc.Tables(0).Rows.Count > 0 Then
            Me.cb_AP_Acc.DataSource = ds_AP_Acc.Tables(0)
            Me.cb_AP_Acc.DisplayMember = "Desc_Acc"
            Me.cb_AP_Acc.ValueMember = "Account"
        End If
        Me.Audit_Search()
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        md.Insert_User_Log("Load Charges", md.UserName)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Refresh_Rating()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, Me.Name)) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
        If Trim(md.Progran_ReadOnly(UserCode, Me.Name)) = "Y" Then
            Me.bnt_Rate_New.Visible = False
            Me.bnt_Save.Visible = False
            Me.bnt_Del.Visible = False
            Me.bnt_Edit.Visible = False
        End If
        Dim vGlobal As String = ""
        Dim vAdd As String = ""
        Dim i As Integer = 0
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT CHARGE_NUMBER, DESCRIPTION, Rating_FCL, Rating_LCL, RATING_OVS,Rate_Group,add_Charge,isnull(AR_Acc,'') as AR_Acc,isnull(AP_Acc,'') as AP_Acc,isnull(FMC_Commission,'N') as FMC, isnull(Active,0) as Active, isnull(Contract,'N') as Contract, isnull(Agent_Commission,'N') as Agent_Commission FROM  CARGOS ORDER BY CHARGE_NUMBER", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim vAR As String = ""
            Dim vAP As String = ""
            Dim vAct As String = ""
            Me.dgv_Charges.Rows.Clear()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                vGlobal = ""
                vAdd = ""
                vAR = ""
                vAP = ""
                If Trim(ds.Tables(0).Rows(i).Item("Rate_Group")) = "G" Then
                    vGlobal = "Y"
                End If
                If Trim(ds.Tables(0).Rows(i).Item("add_Charge")) = "Y" Then
                    vAdd = "Y"
                End If
                If Len(Trim(ds.Tables(0).Rows(i).Item("AR_Acc"))) > 0 Then
                    vAR = Trim(ds.Tables(0).Rows(i).Item("AR_Acc"))
                End If
                If Len(Trim(ds.Tables(0).Rows(i).Item("AP_Acc"))) > 0 Then
                    vAP = Trim(ds.Tables(0).Rows(i).Item("AP_Acc"))
                End If

                If ds.Tables(0).Rows(i).Item("Active") = 1 Then
                    Me.rd_Active.Checked = True
                    vAct = "Y"
                Else
                    Me.rd_Unactive.Checked = True
                    vAct = "N"
                End If

                Me.cb_Contract_Y_N.SelectedItem = Trim(ds.Tables(0).Rows(i).Item("Contract"))

                Me.dgv_Charges.Rows.Add(ds.Tables(0).Rows(i).Item("Charge_Number"), Trim(ds.Tables(0).Rows(i).Item("Description")), Trim(ds.Tables(0).Rows(i).Item("Rating_FCL")), Trim(ds.Tables(0).Rows(i).Item("Rating_LCL")), Trim(ds.Tables(0).Rows(i).Item("Rating_OVS")), Trim(vGlobal), Trim(vAdd), Trim(vAR), Trim(vAP), Trim(ds.Tables(0).Rows(i).Item("FMC")), Trim(vAct), Trim(ds.Tables(0).Rows(i).Item("Contract")), Trim(ds.Tables(0).Rows(i).Item("Agent_Commission")))
                If Trim(vAR) <> Trim(vAP) Then
                    Me.dgv_Charges.Rows(i).Cells(0).Style.ForeColor = Color.Red
                    Me.dgv_Charges.Rows(i).Cells(1).Style.ForeColor = Color.Red
                    Me.dgv_Charges.Rows(i).Cells(7).Style.ForeColor = Color.Red
                    Me.dgv_Charges.Rows(i).Cells(8).Style.ForeColor = Color.Red
                End If
            Next
            Me.dgv_Charges.Refresh()
            Me.bnt_Rate_New.Enabled = True
            Me.bnt_Save.Enabled = False
            vAdd = Nothing
            vAR = Nothing
            vAP = Nothing
            vAct = Nothing
        End If
        i = Nothing
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Cargo_Clear()
        Me.nCharge.Clear()
        Me.wCharge.Clear()
        Me.cb_LCL.SelectedItem = "W/M"
        Me.cb_FCL.SelectedItem = "LS"
        Me.cb_OVS.SelectedItem = "LS"
        Me.chk_Global.Checked = False
        Me.chk_Add_Charges.Checked = False
        cb_AR_Acc.SelectedValue = ""
        Me.cb_FMC.SelectedItem = "Y"
        Me.cb_Agent_Commission.SelectedItem = ""
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If Flag_Modo = 1 Then
            Me.dgv_Charges.Visible = True
            Me.cb_OVS.Enabled = False
            Me.cb_FCL.Enabled = False
            Me.cb_LCL.Enabled = False
            Me.chk_Global.Enabled = False
            Me.chk_Add_Charges.Enabled = False
            Me.wCharge.ReadOnly = True
            Me.nCharge.ReadOnly = True
            cb_AR_Acc.Enabled = False
            cb_AP_Acc.Enabled = False
            Me.cb_FMC.Enabled = False
            Me.cb_Agent_Commission.Enabled = False
            ' ------- Color
            Me.nCharge.BackColor = Color.LightBlue
            Me.wCharge.BackColor = Color.LightBlue
        Else
            'Me.dgv_Charges.Visible = False
            Me.cb_OVS.Enabled = True
            Me.cb_FCL.Enabled = True
            Me.cb_LCL.Enabled = True
            Me.chk_Global.Enabled = True
            Me.chk_Add_Charges.Enabled = True
            Me.nCharge.ReadOnly = False
            Me.wCharge.ReadOnly = False
            cb_AR_Acc.Enabled = True
            cb_AP_Acc.Enabled = True
            Me.cb_FMC.Enabled = True
            Me.cb_Agent_Commission.Enabled = True
            ' ------- Color
            Me.nCharge.BackColor = Color.White
            Me.wCharge.BackColor = Color.White
        End If
    End Sub

    Private Sub bnt_Rate_New_Click(sender As Object, e As EventArgs) Handles bnt_Rate_New.Click
        Me.Cargo_Clear()
        Me.Modo_Edit_ADD_Read_Only(0)
        Me.bnt_Rate_New.Enabled = False
        Me.bnt_Save.Enabled = True
        Flag_Modo = 0
        Me.wCharge.Focus()
    End Sub

    Private Sub Shipping_Rate_Print()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Me.Ds_Shipping_Rate_Master.Tables(0).Rows.Count > 0 Then

            Dim cr_Shipping_Rate As New cr_Shipping_Rate
            cr_Shipping_Rate.SetDataSource(Ds_Shipping_Rate_Master.Tables(0))
            'Dim obTxt_Printed_By As CrystalDecisions.CrystalReports.Engine.TextObject = cr_Shipping_Rate.ReportDefinition.Sections(5).ReportObjects("txt_Printed_By")
            'obTxt_Printed_By.Text = Trim(System.Environment.UserName)

            Me.cv_Shipping_Rate.CloseView(cr_Shipping_Rate)
            Me.cv_Shipping_Rate.ReportSource = cr_Shipping_Rate
            Me.cv_Shipping_Rate.BringToFront()
            Me.cv_Shipping_Rate.RefreshReport()
            Me.cv_Shipping_Rate.DisplayToolbar = True
            Me.cv_Shipping_Rate.DisplayStatusBar = True
            Me.cv_Shipping_Rate.Zoom(75)
            Me.cv_Shipping_Rate.Refresh()
            Me.cv_Shipping_Rate.Show()
        End If

        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        If Me.TabControl1.SelectedIndex = 1 Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Me.Shipping_Rate_Print()
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Len(Trim(Me.nCharge.Text)) = 0 Then
                MsgBox("Shipping Rate Number field is empty,..")
                Me.nCharge.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.wCharge.Text)) = 0 Then
                MsgBox("Shipping Rate field is empty,..")
                Me.wCharge.Focus()
                Exit Sub
            End If
            Dim vGlobal As String = "F"
            Dim vAdd_Charge As String = "N"
            If Me.chk_Global.Checked = True Then
                vGlobal = "G"
            End If
            If Me.chk_Add_Charges.Checked = True Then
                vAdd_Charge = "Y"
            End If
            Dim nActive As Integer = 1
            If rd_Unactive.Checked = True Then
                nActive = 0
            End If
            Dim nCharge As Integer = 0
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select Top 1 Charge_Number From Cargos Where Charge_Number = " & Me.nCharge.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("This cargo number already exits,..")
                Me.nCharge.Clear()
                Me.nCharge.Focus()
                Exit Sub
            End If
            md.strSQL = "Insert Into Cargos (Charge_Number,Description,Rating_FCL,Rating_LCL,Rating_OVS,Rate_Group,add_Charge,AR_Acc,AP_Acc,FMC_Commission, Agent_Commission, Active, Contract) Values (" &
            Me.nCharge.Text & ",'" & Trim(Me.wCharge.Text) & "','" & Trim(Me.cb_FCL.SelectedValue) & "','" & Trim(Me.cb_LCL.SelectedValue) &
              "','" & Trim(Me.cb_OVS.SelectedValue) & "','" & Trim(vGlobal) & "','" & Trim(vAdd_Charge) & "','" & Trim(Me.cb_AR_Acc.SelectedValue) & "','" & Trim(Me.cb_AP_Acc.SelectedValue) & "','" & Trim(Me.cb_FMC.SelectedItem) & "','" & Trim(Me.cb_Agent_Commission.SelectedItem) & "'," & nActive & ",'" & Trim(Me.cb_Contract_Y_N.SelectedItem) & "')"
            md.eResp = ws.ExecSQL(md.strConnect, strSQL)
            Me.Refresh_Rating()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Flag_Modo = 1
                Modo_Edit_ADD_Read_Only(1)
                bnt_Del.Enabled = False
                bnt_Rate_New.Enabled = True
                bnt_Edit.Enabled = True
                bnt_Save.Enabled = False
            Else
                Flag_Modo = 2
                Me.Modo_Edit_ADD_Read_Only(2)
                bnt_Del.Enabled = True
                bnt_Edit.Enabled = False
                bnt_Save.Enabled = False
                bnt_Rate_New.Enabled = False
                Me.wCharge.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Add_Charges_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Add_Charges.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                If Me.chk_Add_Charges.Checked = True Then
                    vAdd = "Y"
                Else
                    vAdd = "N"
                End If
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select add_Charge From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("add_Charge")) <> Trim(vAdd) Then
                        vDesc = "Changed Add Charge"
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set add_Charge ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Flag_Modo = 1
                Me.Refresh_Rating()
                Flag_Modo = 2
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub chk_Global_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Global.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                If Me.chk_Global.Checked = True Then
                    vAdd = "G"
                Else
                    vAdd = "F"
                End If
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select add_Charge From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("add_Charge")) <> Trim(vAdd) Then
                        vDesc = "Changed Rate Group "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Rate_Group ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Insert_Changes(ByVal Desc As String, ByVal Charge_Number As Integer, ByVal Old_value As String)
        Dim vResp As String = ""
        vResp = ws.ExecSQL(md.strConnect, "Insert Into Cargo_Journal (
                                               Charge_Number,                                               
                                               Created_ON, 
                                               Created_By, 
                                               Description, 
                                               Old_value
                                                     ) Values ('" &
                                               Charge_Number & "','" &
                                               Format(System.DateTime.Today, "MM/dd/yyyy") & "','" &
                                               Trim(System.Environment.UserName) & "','" &
                                               Trim(Desc) & "','" &
                                               Trim(Old_value) & "')")
        If vResp <> "Success" Then
            MsgBox(Trim(vResp))
        End If
        vResp = Nothing
    End Sub
    Private Sub Audit_Search()
        ' ------- Audit
        Dim vCreated_By As String
        Dim vCreation_Date As String
        Dim vDesc As String
        Dim vOLD As String

        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT c.Charge_Number, isnull(cc.Description,'') as Cargo_Name, c.Description, Format(c.Created_ON,'MM/dd/yyyy') as Created_On, c.Created_By, c.Old_value FROM Cargo_Journal as c Inner Join Cargos as cc on c.Charge_Number = cc.Charge_Number
                          Order By c.uid desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.dgv_Audit.Rows.Clear()
            Dim i As Integer = 0
            For i = 0 To ds.Tables(0).Rows.Count - 1
                vOLD = ""
                vCreated_By = ""
                vCreation_Date = ""
                vDesc = ""
                If Len(Trim(ds.Tables(0).Rows(i).Item("Created_ON"))) > 0 Then
                    vCreation_Date = Trim(ds.Tables(0).Rows(i).Item("Created_ON"))
                End If
                If Len(Trim(ds.Tables(0).Rows(i).Item("Created_By"))) > 0 Then
                    vCreated_By = Trim(ds.Tables(0).Rows(i).Item("Created_By"))
                End If
                If Len(Trim(ds.Tables(0).Rows(i).Item("Description"))) > 0 Then
                    vDesc = Trim(ds.Tables(0).Rows(i).Item("Description"))
                End If
                If Len(Trim(ds.Tables(0).Rows(i).Item("Old_value"))) > 0 Then
                    vOLD = Trim(ds.Tables(0).Rows(i).Item("Old_Value"))
                End If
                Me.dgv_Audit.Rows.Add(vCreation_Date, vCreated_By, Trim(ds.Tables(0).Rows(i).Item("Cargo_Name")), vDesc, vOLD)
            Next
            ds = Nothing
            i = Nothing
            vCreated_By = Nothing
            vCreation_Date = Nothing
            vDesc = Nothing
            vOLD = Nothing
        End If

    End Sub

    Private Sub cb_FCL_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_FCL.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                vAdd = Trim(Me.cb_FCL.SelectedValue)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Rating_FCL From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Rating_FCL")) <> Trim(vAdd) Then
                        vDesc = "Changed Rate FCL Type "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Rating_FCL ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_LCL_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_LCL.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                vAdd = Trim(Me.cb_LCL.SelectedValue)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Rating_LCL From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Rating_LCL")) <> Trim(vAdd) Then
                        vDesc = "Changed Rate LCL Type "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Rating_LCL ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_OVS_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_OVS.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                vAdd = Trim(Me.cb_OVS.SelectedValue)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Rating_OVS From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Rating_OVS")) <> Trim(vAdd) Then
                        vDesc = "Changed Rate OVS Type "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Rating_OVS ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_AR_Acc_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_AR_Acc.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim ndgv_Loc As Integer = Me.dgv_Charges.CurrentCell.RowIndex
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAR_Acc As String = ""
                vAR_Acc = Trim(Me.cb_AR_Acc.SelectedValue)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(AR_Acc,'') as AR_Acc From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("AR_Acc")) <> Trim(vAR_Acc) Then
                        vDesc = "Changed AR Account: " & Trim(vAR_Acc)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set AR_Acc ='" & Trim(vAR_Acc) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(ds.Tables(0).Rows(0).Item("AR_Acc")))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                'Me.Refresh_Rating()
                Me.dgv_Charges.Focus()

                Me.dgv_Charges.Rows(ndgv_Loc).Selected = True
                Me.dgv_Charges.Select()
                Me.dgv_Charges.Refresh()
                vDesc = Nothing
                vAR_Acc = Nothing
                'Me.dgv_Charges.Refresh()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub dgv_Charges_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Charges.CellClick
        If Me.dgv_Charges.RowCount > 0 Then
            If e.RowIndex > -1 And e.RowIndex < Me.dgv_Charges.RowCount Then
                Me.nCharge.Text = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Me.wCharge.Text = Me.dgv_Charges.Item(1, Me.dgv_Charges.CurrentCell.RowIndex).Value
                cb_FCL.Enabled = True
                cb_LCL.Enabled = True
                cb_OVS.Enabled = True
                Me.cb_FCL.SelectedValue = Me.dgv_Charges.Item(2, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Me.cb_LCL.SelectedValue = Me.dgv_Charges.Item(3, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Me.cb_OVS.SelectedValue = Me.dgv_Charges.Item(4, Me.dgv_Charges.CurrentCell.RowIndex).Value
                If Me.dgv_Charges.Item(5, Me.dgv_Charges.CurrentCell.RowIndex).Value = "Y" Then
                    Me.chk_Global.Checked = True
                Else
                    Me.chk_Global.Checked = False
                End If
                If Me.dgv_Charges.Item(6, Me.dgv_Charges.CurrentCell.RowIndex).Value = "Y" Then
                    Me.chk_Add_Charges.Checked = True
                Else
                    Me.chk_Add_Charges.Checked = False
                End If

                If Me.dgv_Charges.Item(10, Me.dgv_Charges.CurrentCell.RowIndex).Value = "Y" Then
                    Me.rd_Active.Checked = True
                Else
                    Me.rd_Unactive.Checked = True
                End If

                Me.cb_Contract_Y_N.SelectedItem = Trim(Me.dgv_Charges.Item(11, Me.dgv_Charges.CurrentCell.RowIndex).Value)

                If Flag_Modo <> 2 Then
                    cb_FCL.Enabled = False
                    cb_LCL.Enabled = False
                    cb_OVS.Enabled = False
                End If
                Me.cb_AR_Acc.SelectedValue = Me.dgv_Charges.Item(7, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Me.cb_AP_Acc.SelectedValue = Me.dgv_Charges.Item(8, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Me.cb_FMC.SelectedItem = Trim(Me.dgv_Charges.Item(9, Me.dgv_Charges.CurrentCell.RowIndex).Value)
                Me.cb_Agent_Commission.SelectedItem = Trim(Me.dgv_Charges.Item(12, Me.dgv_Charges.CurrentCell.RowIndex).Value)
            End If
        End If
    End Sub

    Private Sub wCharge_Validating(sender As Object, e As CancelEventArgs) Handles wCharge.Validating
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAdd As String = ""
                vAdd = Trim(Me.wCharge.Text)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Description From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Description")) <> Trim(vAdd) Then
                        vDesc = "Changed Description for Cargo From: " & Trim(ds.Tables(0).Rows(0).Item("Description")) & " To: " & Trim(vAdd) & ", Cargo # " & Trim(Str(nCharge))
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Description ='" & Trim(vAdd) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAdd))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Shipping_Rates_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        md.Insert_User_Log("Closing Charges", md.UserName)
    End Sub

    Private Sub cb_AP_Acc_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_AP_Acc.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim ndgv_Loc As Integer = Me.dgv_Charges.CurrentCell.RowIndex
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAP_Acc As String = ""
                vAP_Acc = Trim(Me.cb_AP_Acc.SelectedValue)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(AP_Acc,'') as AP_Acc From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("AP_Acc")) <> Trim(vAP_Acc) Then
                        vDesc = "Changed AP Account " & Trim(vAP_Acc)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set AP_Acc ='" & Trim(vAP_Acc) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(ds.Tables(0).Rows(0).Item("AP_Acc")))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                'Me.Refresh_Rating()
                Me.dgv_Charges.Focus()
                Me.dgv_Charges.Rows(ndgv_Loc).Selected = True
                Me.dgv_Charges.Select()
                ndgv_Loc = Nothing
                vDesc = Nothing
                vAP_Acc = Nothing
                nCharge = Nothing
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_FMC_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_FMC.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vFMC As String = ""
                vFMC = Trim(Me.cb_FMC.SelectedItem)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(FMC_Commission,'N') as FMC From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("FMC")) <> Trim(vFMC) Then
                        vDesc = "Changed FMC # " & Trim(Str(nCharge))
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set FMC_Commission = '" & Trim(vFMC) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(ds.Tables(0).Rows(0).Item("FMC")))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Me.Refresh_Rating()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Refresh_Click(sender As Object, e As EventArgs) Handles bnt_Refresh.Click
        Me.Refresh_Rating()
    End Sub

    Private Sub cb_Contract_Y_N_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Contract_Y_N.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vContract As String = ""
                vContract = Trim(Me.cb_Contract_Y_N.SelectedItem)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(Contract,'N') as Contract From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Contract")) <> Trim(vContract) Then
                        vDesc = "Changed Apply to Contract From: " & Trim(ds.Tables(0).Rows(0).Item("Contract")) & " To: " & Trim(vContract)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Contract = '" & Trim(vContract) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(ds.Tables(0).Rows(0).Item("Contract")))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Me.Refresh_Rating()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub rd_Active_CheckedChanged(sender As Object, e As EventArgs) Handles rd_Active.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim nActive As Integer = 0
                Dim vAct_New As String = ""
                Dim vAct_Old As String = ""
                If Me.rd_Active.Checked = True Then
                    nActive = 1
                    vAct_New = "Yes"
                    vAct_Old = "No"
                Else
                    vAct_New = "No"
                    vAct_Old = "Yes"
                End If

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(Active,0) as Active From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Active")) <> nActive Then
                        vDesc = "Changed Active From: " & Trim(vAct_Old) & " To: " & Trim(vAct_New)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Active = " & nActive & " Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAct_Old))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Me.Refresh_Rating()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub rd_Unactive_CheckedChanged(sender As Object, e As EventArgs) Handles rd_Unactive.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim nActive As Integer = 1
                Dim vAct_New As String = ""
                Dim vAct_Old As String = ""
                If Me.rd_Unactive.Checked = True Then
                    nActive = 0
                    vAct_New = "No"
                    vAct_Old = "Yes"
                Else
                    vAct_New = "Yes"
                    vAct_Old = "No"
                End If

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(Active,0) as Active From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Active")) <> nActive Then
                        vDesc = "Changed Active From: " & Trim(vAct_Old) & " To: " & Trim(vAct_New)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Active = " & nActive & " Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(vAct_Old))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Me.Refresh_Rating()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Agent_Commission_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Agent_Commission.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            If Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value > 0 Then
                Dim nCharge As Integer = 0
                nCharge = Me.dgv_Charges.Item(0, Me.dgv_Charges.CurrentCell.RowIndex).Value
                Dim vAgent As String = ""
                vAgent = Trim(Me.cb_Agent_Commission.SelectedItem)

                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isnull(FMC_Commission,'N') as FMC From Cargos Where Charge_Number = " & nCharge, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Agent_Commission")) <> Trim(vAgent) Then
                        vDesc = "Changed Agent Commission cargo ID: " & Trim(Str(nCharge)) & " From: " & Trim(ds.Tables(0).Rows(0).Item("Agent_Commission")) & " To: " & Trim(vAgent)
                        md.eResp = ws.ExecSQL(md.strConnect, "Update Cargos Set Agent_Commission = '" & Trim(vAgent) & "' Where Charge_Number = " & nCharge)
                        Me.Insert_Changes(Trim(vDesc), nCharge, Trim(ds.Tables(0).Rows(0).Item("Agent_Commission")))
                    End If
                End If
                ds = Nothing
                Me.dgv_Audit.Rows.Clear()
                Audit_Search()
                Me.Refresh_Rating()
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class