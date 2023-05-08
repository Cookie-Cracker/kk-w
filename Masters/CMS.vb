Imports System.ComponentModel
Imports System.Net.Mail
Imports System.Net
Imports Excel = Microsoft.Office.Interop.Excel

' Documents
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.Text
Imports System.IO.Path


Public Class CMS
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1
    Dim ds_ThirdParty As New DataSet
    Dim ds_Contracts_Master As New DataSet
    Dim ds_SDN_List As New DataSet
    Dim ds_CMS_SDN As New DataSet
    Dim ds_AR_Collectors As New DataSet

    Dim oEnvironment As Environment
    Dim oDocument As Document

    Private Sub CMS_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Trim(md.Progran_access(UserCode, Me.Name)) = "N" Then
                Me.Close()
                Exit Sub
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
            '' Dim X As Integer = My.Computer.Screen.Bounds.Height
            ' 'Dim Y As Integer = My.Computer.Screen.Bounds.Width
            ' 'Dim xx As Integer = Me.Width
            ' 'Dim yy As Integer = Me.Height
            ' 'If xx > X Then
            '     'Me.Size = New Size(X, Y)
            ' Else
            '     'Me.Size = New Size(xx, Y)
            ' End If

            ' 'AutoSize = True
            ' 'AutoSizeMode = AutoSizeMode.GrowAndShrink
            ' 'Height = 100%
            ' 'Width = 100%
            ' 'Me.AutoScroll = True
            Me.Refresh()

            If Trim(md.Progran_ReadOnly(UserCode, Me.Name)) = "Y" Then
                Me.bnt_New.Visible = False
                Me.bnt_Save.Visible = False
                Me.bnt_Del.Visible = False
                Me.bnt_Edit.Visible = False
                Me.bnt_add_Sale.Visible = False
            End If

            strSQL = "SELECT Collector FROM  dbo.AR_Collectors Order by Collector"
            ds_AR_Collectors = ws.GetDataset(strConnect, strSQL, 1)
            If ds_AR_Collectors.Tables(0).Rows.Count > 0 Then
                Me.cb_AR_Collectors.DataSource = ds_AR_Collectors.Tables(0)
                Me.cb_AR_Collectors.DisplayMember = "Collector"
                Me.cb_AR_Collectors.ValueMember = "Collector"

                Me.cb_AR_Collectors_Display.DataSource = ds_AR_Collectors.Tables(0)
                Me.cb_AR_Collectors_Display.DisplayMember = "Collector"
                Me.cb_AR_Collectors_Display.ValueMember = "Collector"
            End If

            ' Me.CMS_Refresh()
            ds_Country.Clear()
            ds_Country = ws.GetDataset(md.strConnect, "SELECT uid,COUNTRY,A2_ISO,A3_UN,UN_nro FROM Country_Master Order by Country", 1)
            If ds_Country.Tables(0).Rows.Count > 0 Then
                Me.cb_Country.DataSource = ds_Country.Tables(0)
                Me.cb_Country.DisplayMember = "Country"
                Me.cb_Country.ValueMember = "Country"
            End If
            ds_Sales = ws.GetDataset(md.strConnect, "Select Seller From Sellers Order by Seller", 1)
            If ds_Sales.Tables(0).Rows.Count > 0 Then
                Me.cb_Sellers.DataSource = ds_Sales.Tables(0)
                Me.cb_Sellers.DisplayMember = "Seller"
                Me.cb_Sellers.ValueMember = "Seller"
            End If
            'ds_CMS_USA_FL.Clear()
            'ds_CMS_USA_FL = ws.GetDataset(md.strConnect, "SELECT Distinct COMPANY_NAME as name,isnull(Sales,'') as Sales, COMPANY_NUMBER From dbo.CM_System ORDER BY COMPANY_NAME", 1)
            'If ds_CMS_USA_FL.Tables(0).Rows.Count > 0 Then
            '    Me.cb_Customers.DataSource = ds_CMS_USA_FL.Tables(0)
            '    Me.cb_Customers.ValueMember = "Company_Number"
            '    Me.cb_Customers.DisplayMember = "Name"
            'End If
            'ds_CMS.Clear()
            'ds_CMS = ws.GetDataset(md.strConnect, "Select DISTINCT Company_Name, Company_Number From CM_System Order By Company_Name", 1) 'Me.CMS_MasterTableAdapter.Fill(Me.Ds_CMS_Master.CMS_Master, Trim(zDesc))
            'If ds_CMS.Tables(0).Rows.Count > 0 Then
            '    Me.cb_CMS.DataSource = ds_CMS.Tables(0)
            '    Me.cb_CMS.DisplayMember = "Company_Name"
            '    Me.cb_CMS.ValueMember = "Company_Number"
            '    'Me.cb_CMS.Refresh()
            '    '    'Me.cb_CMS.DroppedDown = True
            'End If

            Dim cb_Contract As New DataGridViewComboBoxColumn
            ' ------- Acc
            ds_Contracts_Master = ws.GetDataset(md.strConnect, "Select CONTRACT_NUMBER, START_DATE, END_DATE, ACTIVE, TEU, METRIC_TON, DOLLAR_AMOUNT, LCL_AS_TEU, ISNULL(SALES_REP, '') AS Sales_rep, CREATED_BY, CREATION_DATE, CREATION_TIME, DESCRIPTION FROM dbo.Contract_HDR ORDER BY CONTRACT_NUMBER", 1)
            If ds_Contracts_Master.Tables(0).Rows.Count > 0 Then
                cb_Contract = Me.dgv_Contracts.Columns.Item(0)
                cb_Contract.DataSource = ds_Contracts_Master.Tables(0)
                cb_Contract.DisplayMember = "Contract_Number"
                cb_Contract.ValueMember = "Contract_Number"
            End If

            ' ------- Documents
            Me.initEnvironment()
            Me.initDocument()
            Me.initAttachmentsDGV()

            'AddHandler ucPhotoCamera.ucTakePicture, AddressOf takePicture
            'Me.ucPhotoCamera.initCamera(oEnvironment.PhotoCamera)

            AddHandler ucScanner.ucScanDocument, AddressOf scanDocument
            Me.ucScanner.initScanner(oEnvironment.TwainKey, oEnvironment.Scanner)

            Me.Refresh_Sales()
            md.Insert_User_Log("Load CMS", md.UserName)
            Flag_Modo = 1
            Modo_Edit_ADD_Read_Only(1)

            Me.cb_CMS.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear()
        Me.wCMS_Nro.Clear()
        Me.wCMS_Name.Clear()
        Me.wFMC.Clear()
        Me.wEIN.Clear()
        Me.dtp_Trucker_Insure_Date.Value = System.DateTime.Today.ToShortDateString
        Me.wContact.Clear()
        ' ----------------------------------------
        Me.chk_Consignee.Checked = False
        Me.chk_FWDR.Checked = False
        Me.chk_Line.Checked = False
        Me.chk_Notify.Checked = False
        Me.chk_Shipper.Checked = False
        Me.chk_trucker.Checked = False
        Me.chk_Warehouse.Checked = False
        Me.chk_Yard.Checked = False
        Me.chk_Agent.Checked = False
        Me.chk_ThirdParty.Checked = False
        Me.chk_Ship_Owner.Checked = False
        Me.chk_Customer.Checked = False
        Me.chk_Vendor.Checked = False
        Me.chk_Terminal.Checked = False
        Me.rb_Actived.Checked = True

        Me.Clear_Address()
        Me.dgv_eMails.Rows.Clear()
        Me.dgv_Notes.Rows.Clear()
        Me.wDesc.Clear()

        Me.dgv_Contracts.Rows.Clear()

        Me.txt_SDN_List.Clear()
        Me.txt_SDN_List.Visible = False
        Me.txt_SDN_Black_List.Visible = False

        Me.txt_CMS_vs_SDN.Clear()

        Me.txt_Terminal_Notes_Comments.Clear()
        Me.Txt_Delivery_Terminal.Clear()

        Me.txt_Warehouse_Clauses.Clear()
        ' ----------------------------------------
    End Sub

    Private Sub Clear_Address()
        Me.wCity.Clear()
        Me.wLoc.Clear()
        Me.wStreet.Clear()
        Me.wSuite.Clear()
        Me.wState.Clear()
        Me.wZip.Clear()
        Me.wPhone.Clear()
        Me.cb_Country.SelectedValue = ""
        If Me.dgv_CMS_Address.RowCount > 0 Then
            Me.dgv_CMS_Address.Rows.Clear()
        End If
        If Flag_Modo = 2 Then
            Me.wCity.ReadOnly = False
            Me.wLoc.ReadOnly = False
            Me.wStreet.ReadOnly = False
            Me.wSuite.ReadOnly = False
            Me.wState.ReadOnly = False
            Me.wZip.ReadOnly = False
            Me.wPhone.ReadOnly = False
            Me.wContact.ReadOnly = False
            Me.cb_Country.Enabled = True
            Me.wDesc.ReadOnly = False
        End If
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If Flag_Modo = 1 Then
            Me.bnt_New.Visible = True
            Me.bnt_Edit.Visible = True
            Me.bnt_Del.Visible = False
            Me.bnt_Save.Visible = False
            bnt_Clear_Address.Enabled = True
            Me.rb_Actived.Enabled = False
            Me.dtp_Trucker_Insure_Date.Enabled = False
            Me.wEIN.ReadOnly = True
            Me.wCMS_Name.ReadOnly = True
            Me.wFMC.ReadOnly = True
            Me.wStreet.ReadOnly = True
            Me.wSuite.ReadOnly = True
            Me.wCity.ReadOnly = True
            Me.wState.ReadOnly = True
            Me.wZip.ReadOnly = True
            Me.wPhone.ReadOnly = True
            Me.wContact.ReadOnly = True
            Me.wDesc.ReadOnly = True
            Me.cb_Country.Enabled = False
            Me.gb_Type.Enabled = False

            ' ------- Color
            'Me.TabPage1.BackColor = Color.LightBlue
            Me.wEIN.BackColor = Color.LightBlue
            Me.wCMS_Name.BackColor = Color.LightBlue
            Me.wFMC.BackColor = Color.LightBlue
            Me.wStreet.BackColor = Color.LightBlue
            Me.wSuite.BackColor = Color.LightBlue
            Me.wCity.BackColor = Color.LightBlue
            Me.wState.BackColor = Color.LightBlue
            Me.wZip.BackColor = Color.LightBlue
            Me.wPhone.BackColor = Color.LightBlue
            Me.wContact.BackColor = Color.LightBlue
            Me.wDesc.BackColor = Color.LightBlue
            Me.cb_Country.BackColor = Color.LightBlue
            Me.gb_Type.BackColor = Color.LightBlue
            Me.dgv_Notes.RowsDefaultCellStyle.BackColor = Color.LightBlue
            Dim j As Integer = 0
            For j = 0 To Me.dgv_Notes.RowCount - 1
                Me.dgv_Notes.Rows(j).Cells(0).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(0).Style.BackColor = Color.LightBlue
                Me.dgv_Notes.Rows(j).Cells(1).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(1).Style.BackColor = Color.LightBlue
                Me.dgv_Notes.Rows(j).Cells(2).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(2).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(2).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(3).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(3).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(3).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(4).ReadOnly = True
            Next
            Me.dgv_Notes.AllowUserToAddRows = False
            For j = 0 To Me.dgv_Contracts.RowCount - 1
                Me.dgv_Contracts.Rows(j).Cells(0).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(0).Style.BackColor = Color.LightBlue
                Me.dgv_Contracts.Rows(j).Cells(0).Style.ForeColor = Color.Black
            Next
            Me.dgv_Contracts.AllowUserToAddRows = False
            For j = 0 To Me.dgv_Contracts.RowCount - 1
                Me.dgv_Contracts.Rows(j).Cells(0).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(0).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(0).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(1).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(1).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(1).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(2).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(2).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(2).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(3).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(3).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(3).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(4).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(5).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(5).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(5).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(6).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(6).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(6).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(7).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(7).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(7).ReadOnly = True
            Next
            Me.dgv_Contracts.AllowUserToAddRows = False

            For j = 0 To Me.dgv_eMails.RowCount - 1
                Me.dgv_eMails.Rows(j).Cells(0).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(0).Style.BackColor = Color.LightBlue
                Me.dgv_eMails.Rows(j).Cells(1).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(1).Style.BackColor = Color.LightBlue
                Me.dgv_eMails.Rows(j).Cells(2).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(2).Style.BackColor = Color.LightBlue
                Me.dgv_eMails.Rows(j).Cells(3).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(3).Style.BackColor = Color.LightBlue
                Me.dgv_eMails.Rows(j).Cells(4).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(5).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(5).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(5).Style.ForeColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(6).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(6).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(6).Style.ForeColor = Color.White
            Next
            Me.dgv_eMails.AllowUserToAddRows = False

            Me.txt_Terminal_Notes_Comments.ReadOnly = True
            Me.txt_Terminal_Notes_Comments.BackColor = Color.Navy
            Me.txt_Terminal_Notes_Comments.ForeColor = Color.White

            Me.Txt_Delivery_Terminal.ReadOnly = True
            Me.Txt_Delivery_Terminal.BackColor = Color.Navy
            Me.Txt_Delivery_Terminal.ForeColor = Color.White

            j = Nothing
        Else
            If Flag_Modo = 3 Then
                bnt_Clear_Address.Enabled = False
                Me.bnt_Del.Visible = False
                Me.bnt_New.Visible = False
                Me.bnt_Edit.Visible = False
                Me.bnt_Save.Visible = True
            Else
                bnt_Clear_Address.Enabled = False
                Me.bnt_Del.Visible = True
                Me.bnt_Save.Visible = False
                Me.bnt_Edit.Visible = True
                Me.bnt_Save.Visible = False
            End If
            Me.rb_Actived.Enabled = True
            Me.dtp_Trucker_Insure_Date.Enabled = True
            Me.wEIN.ReadOnly = False
            Me.wCMS_Nro.ReadOnly = False
            Me.wCMS_Name.ReadOnly = False
            Me.wFMC.ReadOnly = False
            Me.wStreet.ReadOnly = False
            Me.wSuite.ReadOnly = False
            Me.wCity.ReadOnly = False
            Me.wState.ReadOnly = False
            Me.wZip.ReadOnly = False
            Me.wPhone.ReadOnly = False
            Me.wContact.ReadOnly = False
            Me.wDesc.ReadOnly = False
            Me.cb_Country.Enabled = True
            Me.gb_Type.Enabled = True

            ' ------- Color
            Me.wEIN.BackColor = Color.White
            Me.wCMS_Nro.BackColor = Color.White
            Me.wCMS_Name.BackColor = Color.White
            Me.wFMC.BackColor = Color.White
            Me.wStreet.BackColor = Color.White
            Me.wSuite.BackColor = Color.White
            Me.wCity.BackColor = Color.White
            Me.wState.BackColor = Color.White
            Me.wZip.BackColor = Color.White
            Me.wPhone.BackColor = Color.White
            Me.wContact.BackColor = Color.White
            Me.cb_Country.BackColor = Color.White
            Me.gb_Type.BackColor = Color.White
            Me.wDesc.BackColor = Color.White

            Me.dgv_Notes.RowsDefaultCellStyle.BackColor = Color.White

            Dim j As Integer = 0

            For j = 0 To Me.dgv_eMails.RowCount - 1
                Me.dgv_eMails.Rows(j).Cells(0).ReadOnly = False
                Me.dgv_eMails.Rows(j).Cells(0).Style.BackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(0).Style.SelectionBackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(0).Style.SelectionForeColor = Color.Blue
                Me.dgv_eMails.Rows(j).Cells(1).ReadOnly = False
                Me.dgv_eMails.Rows(j).Cells(1).Style.BackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(1).Style.SelectionBackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(1).Style.SelectionForeColor = Color.Blue
                Me.dgv_eMails.Rows(j).Cells(2).ReadOnly = False
                Me.dgv_eMails.Rows(j).Cells(2).Style.BackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(2).Style.SelectionBackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(2).Style.SelectionForeColor = Color.Blue
                Me.dgv_eMails.Rows(j).Cells(3).ReadOnly = False
                Me.dgv_eMails.Rows(j).Cells(3).Style.BackColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(3).Style.SelectionForeColor = Color.Blue
                Me.dgv_eMails.Rows(j).Cells(4).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(5).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(5).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(5).Style.ForeColor = Color.White
                Me.dgv_eMails.Rows(j).Cells(6).ReadOnly = True
                Me.dgv_eMails.Rows(j).Cells(6).Style.BackColor = Color.MidnightBlue
                Me.dgv_eMails.Rows(j).Cells(6).Style.ForeColor = Color.White
            Next
            Me.dgv_eMails.AllowUserToAddRows = True
            For j = 0 To Me.dgv_Notes.RowCount - 1
                Me.dgv_Notes.Rows(j).Cells(0).ReadOnly = False
                Me.dgv_Notes.Rows(j).Cells(0).Style.BackColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(1).ReadOnly = False
                Me.dgv_Notes.Rows(j).Cells(1).Style.BackColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(2).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(2).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(2).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(3).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(3).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(3).ReadOnly = True
                Me.dgv_Notes.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_Notes.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_Notes.Rows(j).Cells(4).ReadOnly = True
            Next
            Me.dgv_Notes.AllowUserToAddRows = True
            For j = 0 To Me.dgv_Contracts.RowCount - 1
                Me.dgv_Contracts.Rows(j).Cells(0).ReadOnly = False
                Me.dgv_Contracts.Rows(j).Cells(0).Style.BackColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(0).Style.ForeColor = Color.Blue
                Me.dgv_Contracts.Rows(j).Cells(0).Style.SelectionBackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(0).Style.SelectionForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(1).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(1).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(1).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(2).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(2).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(2).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(3).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(3).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(3).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(4).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(4).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(5).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(5).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(5).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(6).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(6).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(6).ReadOnly = True
                Me.dgv_Contracts.Rows(j).Cells(7).Style.BackColor = Color.MidnightBlue
                Me.dgv_Contracts.Rows(j).Cells(7).Style.ForeColor = Color.White
                Me.dgv_Contracts.Rows(j).Cells(7).ReadOnly = True
            Next
            Me.dgv_Contracts.AllowUserToAddRows = True
            'Me.dgv_Contracts.ReadOnly = False
            Me.txt_Terminal_Notes_Comments.ReadOnly = False
            Me.txt_Terminal_Notes_Comments.BackColor = Color.White
            Me.txt_Terminal_Notes_Comments.ForeColor = Color.Black

            Me.Txt_Delivery_Terminal.ReadOnly = False
            Me.Txt_Delivery_Terminal.BackColor = Color.White
            Me.Txt_Delivery_Terminal.ForeColor = Color.Black

            j = Nothing
        End If
    End Sub

    Private Sub cb_CMS_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_CMS.SelectionChangeCommitted
        If Not Me.cb_CMS.SelectedValue = Nothing Then
            CMS_Refresh()
        End If
    End Sub

    Private Sub CMS_Refresh()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 1
            Me.wCMS_Nro.Text = Me.cb_CMS.SelectedValue
            Me.wCMS_Name.Text = Me.cb_CMS.SelectedItem("Company_Name")
            Dim dsT As New DataSet
            strSQL = "Select Company_name, isnull(shipper,'') as shipper,isnull(Consignee,'') as Consignee,isnull(FWDR,'') as FWDR,isnull(Notify,'') as Notify
                             ,isnull(Trucker,'') as Trucker,isnull(Warehouse,'') as Warehouse,isnull(Yard,'') as Yard,
                             isnull(Port_Yard,'') as Terminal,
                             isnull(Agent,'') as Agent,isnull(ThirdParty,'') as ThirdParty,isnull(Vessel_Owner,'') as Vessel_owner, isnull(Contract,'') as Contract, isnull(FMC,'') as FMC,
                             isnull(EIN,'') as EIN,isnull(Customer,'') as Customer, isnull(Vendor,'') as Vendor,
                             isnull(Contact,'') as Contact,
                             isnull(Format(Trucker_Ins_Date,'MM/dd/yyyy'),'01/01/1990') as Trucker_Ins_Date,
                             isNull(Active,0) as Active, isnull(Description,'') as Description, isnull(AR_Collectors,'') AR_Collector From CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = 0"
            dsT = ws.GetDataset(strConnect, strSQL, 1)
            If dsT.Tables(0).Rows.Count > 0 Then
                Me.cb_AR_Collectors.SelectedValue = dsT.Tables(0).Rows(0).Item("AR_Collector")

                Me.wEIN.Text = dsT.Tables(0).Rows(0).Item("EIN")
                Me.wFMC.Text = dsT.Tables(0).Rows(0).Item("FMC")
                Me.wContact.Text = dsT.Tables(0).Rows(0).Item("Contact")
                Me.wDesc.Text = dsT.Tables(0).Rows(0).Item("Description")

                Me.rb_Actived.Checked = dsT.Tables(0).Rows(0).Item("Active")

                Me.dtp_Trucker_Insure_Date.Value = dsT.Tables(0).Rows(0).Item("Trucker_Ins_Date")
                If Trim(dsT.Tables(0).Rows(0).Item("Agent")) = "A" Then
                    Me.chk_Agent.Checked = True
                Else
                    Me.chk_Agent.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Shipper")) = "S" Then
                    Me.chk_Shipper.Checked = True
                Else
                    Me.chk_Shipper.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Consignee")) = "C" Then
                    Me.chk_Consignee.Checked = True
                Else
                    Me.chk_Consignee.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("FWDR")) = "F" Then
                    Me.chk_FWDR.Checked = True
                Else
                    Me.chk_FWDR.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Notify")) = "N" Then
                    Me.chk_Notify.Checked = True
                Else
                    Me.chk_Notify.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Warehouse")) = "W" Then
                    Me.chk_Warehouse.Checked = True
                Else
                    Me.chk_Warehouse.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Yard")) = "Y" Then
                    Me.chk_Yard.Checked = True
                Else
                    Me.chk_Yard.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Trucker")) = "T" Then
                    Me.chk_trucker.Checked = True
                Else
                    Me.chk_trucker.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Contract")) = "Y" Then
                    Me.chk_Contract.Checked = True
                Else
                    Me.chk_Contract.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Vessel_Owner")) = "Y" Then
                    Me.chk_Ship_Owner.Checked = True
                Else
                    Me.chk_Ship_Owner.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Customer")) = "C" Then
                    Me.chk_Customer.Checked = True
                Else
                    Me.chk_Customer.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Vendor")) = "V" Then
                    Me.chk_Vendor.Checked = True
                Else
                    Me.chk_Vendor.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("ThirdParty")) = "Y" Then
                    Me.chk_ThirdParty.Checked = True
                Else
                    Me.chk_ThirdParty.Checked = False
                End If
                If Trim(dsT.Tables(0).Rows(0).Item("Terminal")) = "Y" Then
                    Me.chk_Terminal.Checked = True
                Else
                    Me.chk_Terminal.Checked = False
                End If
            End If

            Dim vSDN_list As String = md.CMS_Get_SDN_List(Me.cb_CMS.SelectedItem("Company_Number"))
            If Len(Trim(vSDN_list)) > 0 Then
                Me.txt_SDN_Black_List.Visible = True
                Me.txt_SDN_List.Visible = True
                Me.txt_SDN_List.Text = Trim(vSDN_list)
            Else
                Me.txt_SDN_Black_List.Visible = False
                Me.txt_SDN_List.Visible = False
                Me.txt_SDN_List.Clear()
            End If
            vSDN_list = Nothing
            Me.CMS_Address_Refresh()
            Me.CMS_eMail_Refresh()
            Me.CMS_Notes_refresh()
            Me.CMS_Contracts()

            Me.Modo_Edit_ADD_Read_Only(1)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CMS_Contracts()
        'strSQL = "SELECT  isnull(CONTRACT_NUMBER,'') as Contract_Number, Customer_Number, Customer_Name FROM Contract_Cust WHERE (Customer_Number = " & Me.wCMS_Nro.Text & ")"

        strSQL = "SELECT Top 1 h.CONTRACT_NUMBER, 
                       case isnull(start_date,'1900-01-01')
	                     when '1900-01-01' then ''
		                 else Format(h.START_DATE,'MM/dd/yyyy')
                       end as Start_Date, 
	                   case isnull(h.end_date,'1900-01-01')
	                     when '1900-01-01' then ''
		                 else Format(h.end_DATE,'MM/dd/yyyy')
                       end as End_Date, h.ACTIVE, isnull(h.TEU,0) as TEU, ISNULL(h.SALES_REP, N'') AS Sales_Rep, h.CREATED_BY, 
	                   case isnull(h.CREATION_DATE,'1900-01-01')
	                     when '1900-01-01' then ''
		                 else Format(h.CREATION_DATE,'MM/dd/yyyy')
                       end as Creation_Date, h.CREATION_TIME, h.DESCRIPTION
                FROM dbo.Contract_Cust AS c INNER JOIN
                     dbo.Contract_HDR AS h ON h.CONTRACT_NUMBER = c.CONTRACT_NUMBER
                  WHERE (c.Customer_Number = " & Trim(Me.wCMS_Nro.Text) & ") Order By  Format(h.CREATION_DATE,'yyyy-MM-dd') Desc"
        Dim ds As New DataSet
        Me.dgv_Contracts.Rows.Clear()
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Contracts.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Contract_Number")), Trim(ds.Tables(0).Rows(j).Item("Start_Date")), Trim(ds.Tables(0).Rows(j).Item("End_Date")), ds.Tables(0).Rows(j).Item("TEU"), Trim(ds.Tables(0).Rows(j).Item("Sales_Rep")), Trim(ds.Tables(0).Rows(j).Item("Active")), Trim(ds.Tables(0).Rows(j).Item("Created_By")), Trim(ds.Tables(0).Rows(j).Item("Creation_Date")))
            Next
            j = Nothing
        End If
        ds = Nothing
    End Sub

    Private Sub CMS_Address_Refresh()
        Me.dgv_CMS_Address.Rows.Clear()
        ds_Address.Clear()
        ds_Address = ws.GetDataset(md.strConnect, "Select location_Number, rtrim(Isnull(Street,'')) + ' ' + rtrim(isnull(Suite,'')) + char(13) + char(10) + rtrim(isnull(City,'')) + ' ' + rtrim(isnull(State,'')) + ' ' + rtrim(isnull(zip,'')) + ' ' + rtrim(isnull(Country,'')) + char(13) + char(10) + rtrim(isnull(Phone,'')) as Address, Street, Suite, City, State, ZIP, Phone, Country, uid, isnull(Active,0) as Active From CM_System Where Company_Number =" & Me.cb_CMS.SelectedValue & " Order By Location_number", 1)
        If ds_Address.Tables(0).Rows.Count > 0 Then
            Me.Clear_Address()
            Me.wLoc.Text = ds_Address.Tables(0).Rows(0).Item("Location_Number")
            Me.wStreet.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Street"))
            Me.wSuite.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Suite"))
            Me.wCity.Text = Trim(ds_Address.Tables(0).Rows(0).Item("City"))
            Me.wState.Text = Trim(ds_Address.Tables(0).Rows(0).Item("State"))
            Me.wZip.Text = Trim(ds_Address.Tables(0).Rows(0).Item("zip"))
            Me.cb_Country.SelectedValue = Trim(ds_Address.Tables(0).Rows(0).Item("country"))
            Me.wPhone.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Phone"))
            Me.rb_Actived.Checked = ds_Address.Tables(0).Rows(0).Item("Active")
            Dim j As Integer = 0
            For j = 0 To ds_Address.Tables(0).Rows.Count - 1
                Me.dgv_CMS_Address.Rows.Add(ds_Address.Tables(0).Rows(j).Item("Location_number"), Trim(ds_Address.Tables(0).Rows(j).Item("Street")), Trim(ds_Address.Tables(0).Rows(j).Item("Suite")), Trim(ds_Address.Tables(0).Rows(j).Item("City")), Trim(ds_Address.Tables(0).Rows(j).Item("State")), Trim(ds_Address.Tables(0).Rows(j).Item("zip")), Trim(ds_Address.Tables(0).Rows(j).Item("Phone")), Trim(ds_Address.Tables(0).Rows(j).Item("Country")), ds_Address.Tables(0).Rows(j).Item("uid"), ds_Address.Tables(0).Rows(j).Item("Active"))
            Next
        End If
    End Sub

    Private Sub CMS_eMail_Refresh()
        Dim ds_eMail As New DataSet
        ds_eMail = ws.GetDataset(md.strConnect, "SELECT [DOCUMENT] as Mod, Isnull(Contact_Name,'') as Contact_name, isnull(EMAIL,'') as eMail, isnull(Phone,'') as Phone, CREATED_BY, Format(CREATION_DATE,'MM/dd/yyyy') as Creation_Date, uid
                                                    FROM dbo.CMSeMail
                                                 WHERE (CMSNUM = " & Me.cb_CMS.SelectedValue & ")
                                                ORDER BY Format(CREATION_DATE,'yyyy-MM-dd') DESC", 1)
        Me.dgv_eMails.Rows.Clear()
        If ds_eMail.Tables(0).Rows.Count > 0 Then
            Dim j As Integer = 0
            For j = 0 To ds_eMail.Tables(0).Rows.Count - 1
                Me.dgv_eMails.Rows.Add(Trim(ds_eMail.Tables(0).Rows(j).Item("email")), Trim(ds_eMail.Tables(0).Rows(j).Item("Contact_name")), Trim(ds_eMail.Tables(0).Rows(j).Item("Phone")), Trim(ds_eMail.Tables(0).Rows(j).Item("Mod")), Trim(ds_eMail.Tables(0).Rows(j).Item("Created_By")), ds_eMail.Tables(0).Rows(j).Item("Creation_Date"), ds_eMail.Tables(0).Rows(j).Item("uid"))
            Next
        End If
    End Sub

    Private Sub CMS_Notes_refresh()
        Dim strSQL As String = "SELECT isnull([Subject],'') as Subj ,isnull([text],'') as Note,Format(Created_on,'MM/dd/yyyy') as Created_On,Created_by,uid FROM [dbo].[CMSNotes] Where Company_Number = " & Me.cb_CMS.SelectedValue & " order by Created_on Desc"
        If Me.dgv_Notes.RowCount > 0 Then
            Me.dgv_Notes.Rows.Clear()
        End If

        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Notes.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Subj")), ds.Tables(0).Rows(j).Item("Note"), Trim(ds.Tables(0).Rows(j).Item("Created_On")), Trim(ds.Tables(0).Rows(j).Item("Created_By")), ds.Tables(0).Rows(j).Item("uid"))
            Next
        End If
        strSQL = Nothing
    End Sub

    Private Sub dgv_CMS_Address_DoubleClick(sender As Object, e As EventArgs) Handles dgv_CMS_Address.DoubleClick
        If Me.dgv_CMS_Address.RowCount > 0 Then
            If Me.dgv_CMS_Address.CurrentCell.RowIndex > -1 And Me.dgv_CMS_Address.CurrentCell.RowIndex < Me.dgv_CMS_Address.RowCount Then
                Dim uid As Integer = Me.dgv_CMS_Address.Item(8, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value
                Me.wCity.Clear()
                Me.wLoc.Clear()
                Me.wStreet.Clear()
                Me.wSuite.Clear()
                Me.wState.Clear()
                Me.wZip.Clear()
                Me.wPhone.Clear()
                ds_Address.Clear()
                ds_Address = ws.GetDataset(md.strConnect, "Select location_Number, Street, Suite, City, State, ZIP, Phone, Country, uid, isnull(Active,0) as Active From CM_System Where uid =" & uid, 1)
                If ds_Address.Tables(0).Rows.Count > 0 Then
                    Me.wLoc.Text = ds_Address.Tables(0).Rows(0).Item("Location_Number")
                    Me.wStreet.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Street"))
                    Me.wSuite.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Suite"))
                    Me.wCity.Text = Trim(ds_Address.Tables(0).Rows(0).Item("City"))
                    Me.wState.Text = Trim(ds_Address.Tables(0).Rows(0).Item("State"))
                    Me.wZip.Text = Trim(ds_Address.Tables(0).Rows(0).Item("zip"))
                    Me.cb_Country.SelectedValue = Trim(ds_Address.Tables(0).Rows(0).Item("country"))
                    Me.wPhone.Text = Trim(ds_Address.Tables(0).Rows(0).Item("Phone"))
                    Me.rb_Actived.Checked = ds_Address.Tables(0).Rows(0).Item("Active")
                End If
                uid = Nothing
            End If
        End If
    End Sub

#Region "Commands"
    Private Sub bnt_New_Click(sender As Object, e As EventArgs) Handles bnt_New.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 3
            Me.Clear()
            Me.Clear_Address()
            Me.Modo_Edit_ADD_Read_Only(3)
            bnt_Add_Address.Enabled = True
            bnt_Clear_Address.Enabled = False
            Dim nLast As Integer = 1
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select top 1 Company_Number From CM_System Where Company_Number < 990000 Order By Company_number Desc", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                nLast = ds.Tables(0).Rows(0).Item("Company_Number") + 1
            End If
            Me.wCMS_Nro.Text = nLast
            Me.rb_Actived.Checked = True
            Me.wLoc.Text = "0"
            Me.wCMS_Name.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Len(Trim(Me.wLoc.Text)) = 0 Then
                MsgBox("You must select a CMS account,.... ")
                Me.cb_CMS.Focus()
                Exit Sub
            End If
            If Flag_Modo = 2 Then
                Flag_Modo = 1
                Modo_Edit_ADD_Read_Only(1)
                bnt_Clear_Address.Enabled = False
                bnt_Add_Address.Enabled = False
            Else
                Flag_Modo = 2
                Me.Modo_Edit_ADD_Read_Only(2)
                bnt_Clear_Address.Enabled = True
                bnt_Add_Address.Enabled = True
                Me.wCMS_Name.Focus()
            End If
            Flag_Modo = 2

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            ' ------- Validation
            If rb_Actived.Checked = False Then
                MsgBox("Actived is false,..")
                Me.rb_Actived.Focus()
                Exit Sub
            End If

            'If Len(Trim(Me.wEIN.Text)) = 0 Then
            '    MsgBox("EIN field is empty,..")
            '    Me.wEIN.Focus()
            '    Exit Sub
            'End If
            If Len(Trim(Me.wCMS_Name.Text)) = 0 Then
                MsgBox("Name field is empty,..")
                Me.wCMS_Name.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.wStreet.Text)) = 0 Then
                Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical
                Dim response = MsgBox("Street field is empty, Do you want to continue?",, "Warnning")
                If response = MsgBoxResult.No Then
                    Me.wStreet.Focus()
                    Exit Sub
                End If
            End If
            If Len(Trim(Me.wCity.Text)) = 0 Then
                MsgBox("City field is empty,..")
                Me.wCity.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.wPhone.Text)) = 0 Then
                If Len(Trim(Me.wStreet.Text)) = 0 Then
                    Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical
                    Dim response = MsgBox("Phone field is empty, Do you want to continue?",, "Warnning")
                    If response = MsgBoxResult.No Then
                        Me.wPhone.Focus()
                        Exit Sub
                    End If
                End If
            End If
            Dim strSql As String = ""
            Dim vResp As String = ""

            Dim ds As New DataSet
            If Flag_Modo = 3 Then
                ds = ws.GetDataset(md.strConnect, "Select Company_Number,Company_name, Location_Number From CM_System Where Company_Number = " & Me.wCMS_Nro.Text & " and Company_Name = '" & Trim(Replace(Me.wCMS_Name.Text, "'", "''")) & "' and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count = 0 Then
                    Dim Shipper As String = ""
                    Dim Consignee As String = ""
                    Dim FWDR As String = ""
                    Dim Notify As String = ""
                    Dim Trucker As String = ""
                    Dim Warehouse As String = ""
                    Dim Yard As String = ""
                    Dim Agent As String = ""
                    Dim ThirdParty As String = ""
                    Dim FMC As String = ""
                    Dim Terminal As String = ""
                    If Me.chk_Shipper.Checked = True Then
                        Shipper = "S"
                    End If
                    If Me.chk_Consignee.Checked = True Then
                        Consignee = "C"
                    End If
                    If Me.chk_FWDR.Checked = True Then
                        FWDR = "F"
                    End If
                    If Me.chk_Notify.Checked = True Then
                        Notify = "N"
                    End If
                    If Me.chk_trucker.Checked = True Then
                        Trucker = "T"
                    End If
                    If Me.chk_Warehouse.Checked = True Then
                        Warehouse = "W"
                    End If
                    If Me.chk_Yard.Checked = True Then
                        Yard = "Y"
                    End If
                    If Me.chk_Agent.Checked = True Then
                        Agent = "A"
                    End If
                    If Me.chk_ThirdParty.Checked = True Then
                        ThirdParty = "Y"
                    End If
                    If Me.chk_Terminal.Checked = True Then
                        Terminal = "Y"
                    End If
                    strSql = "Insert Into CM_System (Company_Number,Location_Number,Company_Name,
                                                 Street,Suite,City,
                                                 State,zip,
                                                 COUNTRY, 
                                                 phone,
                                                 Shipper,
                                                 Consignee,
                                                 FWDR,Notify,Trucker,Warehouse,Agent,Yard,
                                                 Created_By, Created_On,FMC,EIN,Description, ThirdParty, Port_Yard,Active) Values (" &
                    Me.wCMS_Nro.Text & ",0,'" & Trim(Replace(Me.wCMS_Name.Text, "'", "''")) & "','" &
                    Trim(Replace(Me.wStreet.Text, "'", "''")) & "','" & Trim(Replace(Me.wSuite.Text, "'", "''")) & "','" & Trim(Replace(Me.wCity.Text, "'", "''")) & "','" &
                    wState.Text & "','" & wZip.Text & "','" &
                    Trim(Me.cb_Country.SelectedValue) & "','" & Trim(Me.wPhone.Text) & "','" & Trim(Shipper) & "','" &
                    Trim(Consignee) & "','" & Trim(FWDR) & "','" & Trim(Notify) & "','" & Trim(Trucker) & "','" & Trim(Warehouse) & "','" & Trim(Agent) & "','" &
                    Trim(Yard) & "','" & Trim(System.Environment.UserName) & "','" & Format(System.DateTime.Today, "MM/dd/yyyy") & "','" & Trim(Me.wFMC.Text) & "','" & Trim(Me.wEIN.Text) & "','" & Trim(Me.wDesc.Text) & "','" & Trim(ThirdParty) & "','" & Trim(Terminal) & "',1)"
                    'MsgBox(strSql)
                    vResp = ws.ExecSQL(md.strConnect, strSql)

                    '' ------- Send eMail ------------------------------------------
                    'Dim Mail As New MailMessage("newinvoice@kingocean.us", "cdsoftdeveloper@gmail.com")
                    'Dim SMTP As New SmtpClient()
                    'SMTP.Host = "smtp.gmail.com"  '
                    'SMTP.Port = 587
                    'SMTP.EnableSsl = True

                    'SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

                    'Mail.Subject = "New CMS: " & Trim(Me.wEIN.Text)
                    'SMTP.UseDefaultCredentials = True
                    ''SMTP.Credentials = New System.Net.NetworkCredential("newinvoice@kingocean.us", "Ni12345*")

                    'SMTP.Credentials = New System.Net.NetworkCredential("cdsoftdeveloper@gmail.com", "Bravo24!*") '<-- Password Here
                    'md.eResp = "Name: " & Trim(Me.wCMS_Name.Text) & vbCrLf & "Address: " & Trim(Me.wStreet.Text) & ", " & Trim(Me.wCity.Text)
                    'Mail.Body = "Name: " & Trim(md.eResp) & vbCrLf
                    ''Message Here

                    'SMTP.Send(Mail)

                    If Me.dgv_eMails.RowCount > 0 Then
                        Dim j As Integer = 0
                        For j = 0 To Me.dgv_eMails.RowCount - 1
                            If Len(Trim(Me.dgv_eMails.Item(0, j).Value)) > 0 Then
                                strSql = "Insert Into CMSeMail (CMSNUM, [Document], Contact_Name, Phone, email, Created_By, Creation_Date) Values (" &
                                Trim(Me.wCMS_Nro.Text) & ",'" & Trim(Me.dgv_eMails.Item(3, j).Value) & "','" & Trim(Me.dgv_eMails.Item(1, j).Value) & "','" & Trim(Me.dgv_eMails.Item(2, j).Value) & "','" & Trim(Me.dgv_eMails.Item(0, j).Value) & "','" & Trim(System.Environment.UserName) & "','" & System.DateTime.Today.ToShortDateString & "')"
                                eResp = ws.ExecSQL(strConnect, strSql)
                            End If
                        Next
                        j = Nothing
                    End If

                    If Me.dgv_Notes.RowCount > 0 Then
                        Dim j As Integer = 0
                        For j = 0 To Me.dgv_Notes.RowCount - 1
                            If Len(Trim(Me.dgv_Notes.Item(0, j).Value)) > 0 Then
                                strSql = "Insert Into CMSNotes (Company_Number, Subject, text, Created_by, Created_on) Values (" &
                                Trim(Me.wCMS_Nro.Text) & ",'" & Trim(Me.dgv_Notes.Item(0, j).Value) & "','" & Trim(Me.dgv_Notes.Item(2, j).Value) & "','" & Trim(System.Environment.UserName) & "','" & System.DateTime.Today.ToShortDateString & "')"
                                eResp = ws.ExecSQL(strConnect, strSql)
                            End If
                        Next
                        j = Nothing
                    End If

                    'Me.CMS_Refresh()
                    Shipper = Nothing
                    Consignee = Nothing
                    FWDR = Nothing
                    Notify = Nothing
                    Trucker = Nothing
                    Warehouse = Nothing
                    Yard = Nothing

                Else
                    MsgBox("This Company alredy exists,...")
                    Exit Sub
                End If
                strSql = Nothing
                vResp = Nothing
            End If

            ds = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
            md.Insert_Error_msg("CMS", Trim(ex.Message), "")
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Clear_Address_Click(sender As Object, e As EventArgs) Handles bnt_Clear_Address.Click
        If Flag_Modo = 2 Then
            Me.Clear_Address()
            Dim nLast As Integer = 1
            If Len(Trim(Me.wCMS_Nro.Text)) > 0 Then
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select top 1 Location_Number From CM_System Where Company_Number = " & Me.wCMS_Nro.Text & "Order by Location_Number Desc", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    nLast = ds.Tables(0).Rows(0).Item("Location_Number") + 1
                End If
                Me.wLoc.Text = nLast
                ds = Nothing
            Else
                Me.wLoc.Text = "0"
            End If
            nLast = Nothing
            Me.wStreet.Focus()
        End If
    End Sub

    Private Sub bnt_Add_Address_Click(sender As Object, e As EventArgs) Handles bnt_Add_Address.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            ' ------- Validation
            If Len(Trim(Me.wCMS_Nro.Text)) = 0 Then
                MsgBox("Company number field is empty,..")
                Me.wCMS_Nro.Focus()
                Exit Sub
            End If
            If Len(Trim(Me.wStreet.Text)) = 0 Then
                MsgBox("Street field is empty,..")
                Me.wStreet.Focus()
                Exit Sub
            End If
            If Flag_Modo = 3 Or Flag_Modo = 2 Then
                Dim Shipper As String = ""
                Dim Consignee As String = ""
                Dim FWDR As String = ""
                Dim Notify As String = ""
                Dim Trucker As String = ""
                Dim Warehouse As String = ""
                Dim Yard As String = ""
                Dim ThirdParty As String = ""
                If Me.chk_Shipper.Checked = True Then
                    Shipper = "S"
                End If
                If Me.chk_Consignee.Checked = True Then
                    Consignee = "C"
                End If
                If Me.chk_FWDR.Checked = True Then
                    FWDR = "F"
                End If
                If Me.chk_Notify.Checked = True Then
                    Notify = "N"
                End If
                If Me.chk_trucker.Checked = True Then
                    Trucker = "T"
                End If
                If Me.chk_Warehouse.Checked = True Then
                    Warehouse = "W"
                End If
                If Me.chk_Yard.Checked = True Then
                    Yard = "Y"
                End If
                If Me.chk_ThirdParty.Checked = True Then
                    ThirdParty = "Y"
                End If
                Dim nLoc As Integer = 0
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "SELECT COUNT(*) AS nLoc FROM CM_System WHERE (COMPANY_NUMBER = " & Me.wCMS_Nro.Text & ")", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    nLoc = ds.Tables(0).Rows(0).Item("nLoc")
                End If
                ds = Nothing
                Dim strSql As String = "Insert Into CM_System (Company_Number,Company_Name,Location_Number,
                                                 Street,Suite,City,
                                                 State,zip,
                                                 COUNTRY,
                                                 phone,
                                                 Shipper,
                                                 Consignee,
                                                 FWDR,Notify,Trucker,Warehouse,Yard,
                                                 Created_By, Created_On, ThirdParty) Values (" &
                                   Me.wCMS_Nro.Text & ",'" & Trim(Replace(Me.wCMS_Name.Text, "'", "''")) & "'," & nLoc & ",'" &
                                   Trim(Replace(Me.wStreet.Text, "'", "''")) & "','" & Trim(Replace(Me.wSuite.Text, "'", "''")) & "','" & Trim(Me.wCity.Text) & "','" &
                                   wState.Text & "','" & wZip.Text & "','" &
                                   Trim(Me.cb_Country.SelectedValue) & "','" & Trim(Me.wPhone.Text) & "','" & Trim(Shipper) & "','" &
                                   Trim(Consignee) & "','" & Trim(FWDR) & "','" & Trim(Notify) & "','" & Trim(Trucker) & "','" & Trim(Warehouse) & "','" &
                                   Trim(Yard) & "','" & Trim(System.Environment.UserName) & "','" & Format(System.DateTime.Today, "MM/dd/yyyy") & "','" & Trim(ThirdParty) & "')"
                ' MsgBox(strSql)
                Dim vDesc As String = "Added address Loc: " & Me.wLoc.Text
                ' Me.Insert_Changes(Trim(vDesc), Me.wCMS_Nro.Text, Trim(Me.wCMS_Name.Text), Me.wLoc.Text, "")

                Dim vResp As String = ws.ExecSQL(md.strConnect, strSql)
                nLoc = Nothing
            End If  ' ------- Flag Modo

            Me.CMS_Address_Refresh()
            Modo_Edit_ADD_Read_Only(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

#End Region

    Private Sub Insert_Changes(ByVal Desc As String, ByVal Company_Number As Integer, ByVal Company_Name As String, ByVal loc As Integer, ByVal Old_value As String)
        Dim strSQL As String = "Insert Into CMS_Audit (
                                               Created_ON, 
                                               Created_By, 
                                               Description, 
                                               Company_Number,
                                               Company_name,
                                               loc, 
                                               Old_value
                                                     ) Values ('" &
                                  System.DateTime.Now & "','" &
                                  Trim(System.Environment.UserName) & "','" &
                                  Trim(Desc) & "'," & Company_Number & ",'" &
                                  Trim(Replace(Company_Name, "'", "''")) & "'," &
                                  loc & ",'" &
                                  Trim(Mid(Old_value, 1, 70)) & "')"
        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
        If md.eResp <> "Success" Then
            MsgBox(Trim(md.eResp))
        End If
        strSQL = Nothing
    End Sub
    Dim zDesc As String = ""
    Dim nPos As Integer = 1

    Private Sub chk_Yard_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Yard.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Yard.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Yard,'') as Yard From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Yard")) <> Trim(vType) Then
                    vDesc = "Changed Yard "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Yard = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Yard")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Agent_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Agent.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Agent.Checked = True Then
                vType = "A"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Agent,'') as Agent From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Agent")) <> Trim(vType) Then
                    vDesc = "Changed Agent "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Agent = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Agent")))

                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Consignee_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Consignee.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = ""
            If Me.chk_Consignee.Checked = True Then
                vType = "C"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Consignee,'') as Consignee From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Consignee")) <> Trim(vType) Then
                    vDesc = "Changed Consignee "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Consignee = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Consignee")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Sellers_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Sellers.SelectionChangeCommitted
        If Len(Trim(Me.nCust.Text)) > 0 And Len(Trim(Me.wCust.Text)) > 0 Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            md.eResp = "Update CM_System Set Sales = '" & Trim(Me.wCust.Text) & "' Where Company_Number = " & Me.nCust.Text
            md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Sales = '" & Trim(Me.cb_Sellers.SelectedValue) & "' Where Company_Number = " & Me.nCust.Text)
            Me.Refresh_Sales()
            Me.nCust.Clear()
            Me.wCust.Clear()
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub Refresh_Sales()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Distinct COMPANY_NAME as name,isnull(Sales,'') as Sales, COMPANY_NUMBER
                                                            FROM dbo.CM_System
                                                           WHERE isnull(Sales,'') <> ''
                                                            ORDER BY COMPANY_NAME", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.dgv_Sales.Rows.Clear()
            Dim j As Integer = 0
            For j = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Sales.Rows.Add(ds.Tables(0).Rows(j).Item("Name"), ds.Tables(0).Rows(j).Item("Sales"), ds.Tables(0).Rows(j).Item("Company_Number"))
            Next
        End If
        Me.nTotal.Text = ds.Tables(0).Rows.Count
        Me.nTotal.Refresh()
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CMS_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        md.Insert_User_Log("Closing CMS", Trim(md.UserName))
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_add_Sale_Click(sender As Object, e As EventArgs) Handles bnt_add_Sale.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Len(Trim(Me.wSeller.Text)) > 0 Then
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select Seller From Sellers Where Seller = '" & Trim(Me.wSeller.Text) & "'", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("This Seller already exists,....")
                Me.wSeller.Clear()
            Else
                Dim strSQL As String = "Insert Into Sellers (Seller) Values ('" & Trim(Me.wSeller.Text) & "')"
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                Me.wSeller.Clear()
                ds = Nothing
                strSQL = Nothing
                ds_Sales.Clear()
                ds_Sales = ws.GetDataset(md.strConnect, "Select Seller From Sellers Order by Seller", 1)
                If ds_Sales.Tables(0).Rows.Count > 0 Then
                    Me.cb_Sellers.DataSource = ds_Sales.Tables(0)
                    Me.cb_Sellers.DisplayMember = "Seller"
                    Me.cb_Sellers.ValueMember = "Seller"
                End If
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub wCust_TextChanged(sender As Object, e As EventArgs) Handles wCust.TextChanged
        If Me.wCust.Focused Then
            If Len(Trim(Me.wCust.Text)) > 0 Then
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Me.dgv_Cust.Rows.Clear()
                ds_CMS_USA_FL.Clear()
                ds_CMS_USA_FL = ws.GetDataset(md.strConnect, "SELECT Distinct Top 300 COMPANY_NAME as name,isnull(Sales,'') as Sales, COMPANY_NUMBER From dbo.CM_System where Company_Name Like '" & Trim(Me.wCust.Text) & "%' ORDER BY COMPANY_NAME", 1)
                If ds_CMS_USA_FL.Tables(0).Rows.Count > 0 Then
                    Dim j As Integer = 0
                    For j = 0 To ds_CMS_USA_FL.Tables(0).Rows.Count - 1
                        Me.dgv_Cust.Rows.Add(ds_CMS_USA_FL.Tables(0).Rows(j).Item("Name"), ds_CMS_USA_FL.Tables(0).Rows(j).Item("Company_Number"))
                    Next
                End If
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgv_Cust_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Cust.CellClick
        Me.wCust.Text = Trim(Me.dgv_Cust.Item(0, Me.dgv_Cust.CurrentCell.RowIndex).Value)
        Me.nCust.Text = Trim(Me.dgv_Cust.Item(1, Me.dgv_Cust.CurrentCell.RowIndex).Value)
        Me.dgv_Cust.Rows.Clear()
    End Sub

    Private Sub wPhone_KeyDown(sender As Object, e As KeyEventArgs) Handles wPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Phone,'') as Phone From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Phone")) <> Trim(Me.wPhone.Text) Then
                        vDesc = "Changed Phone "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Phone = '" & Trim(Me.wPhone.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Phone")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wCMS_Nro_Validating(sender As Object, e As CancelEventArgs) Handles wCMS_Nro.Validating
        If Flag_Modo = 3 Then
            If Me.wCMS_Nro.Focused Then
                If Len(Trim(Me.wCMS_Nro.Text)) > 0 Then
                    Dim ds As New DataSet
                    ds = ws.GetDataset(md.strConnect, "Select Company_Number From CM_System Where Company_Number = " & Me.wCMS_Nro.Text, 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        MsgBox("This CMS nro already exits,...")
                        Me.wCMS_Nro.Clear()
                        Me.wCMS_Nro.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub wStreet_KeyDown(sender As Object, e As KeyEventArgs) Handles wStreet.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Street,'') as Street From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Street")) <> Trim(Me.wStreet.Text) Then
                        vDesc = "Changed Street "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Street = '" & Trim(Me.wStreet.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Street")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wCMS_Name_KeyDown(sender As Object, e As KeyEventArgs) Handles wCMS_Name.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Company_Name,'') as Company_Name From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Company_Name")) <> Trim(Me.wCMS_Name.Text) Then
                        vDesc = "Changed Name "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Company_Name = '" & Trim(Replace(Me.wCMS_Name.Text, "'", "''")) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Company_Name")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wCity_KeyDown(sender As Object, e As KeyEventArgs) Handles wCity.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(City,'') as City From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("City")) <> Trim(Me.wCity.Text) Then
                        vDesc = "Changed City "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set City = '" & Trim(Me.wCity.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("City")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wState_KeyDown(sender As Object, e As KeyEventArgs) Handles wState.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(State,'') as State From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("State")) <> Trim(Me.wState.Text) Then
                        vDesc = "Changed State "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set State = '" & Trim(Me.wState.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("State")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wSuite_KeyDown(sender As Object, e As KeyEventArgs) Handles wSuite.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Suite,'') as Suite From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Suite")) <> Trim(Me.wSuite.Text) Then
                        vDesc = "Changed Suite "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Suite = '" & Trim(Me.wSuite.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Suite")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub wZip_KeyDown(sender As Object, e As KeyEventArgs) Handles wZip.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Zip,'') as Zip From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Zip")) <> Trim(Me.wZip.Text) Then
                        vDesc = "Changed Zip "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Zip = '" & Trim(Me.wZip.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Zip")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub cb_Country_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Country.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Country,'') as Country From CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Country")) <> Trim(Me.cb_Country.SelectedValue) Then
                    vDesc = "Changed Country "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Country = '" & Trim(Me.cb_Country.SelectedValue) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Country")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Contract_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Contract.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Contract.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Contract,'') as Contract From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Contract")) <> Trim(vType) Then
                    vDesc = "Changed Contract "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Contract = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Contract")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Shipper_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Shipper.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Shipper.Checked = True Then
                vType = "S"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Shipper,'') as Shipper From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Shipper")) <> Trim(vType) Then
                    vDesc = "Changed Shipper "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Shipper = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Shipper")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_FWDR_CheckedChanged(sender As Object, e As EventArgs) Handles chk_FWDR.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_FWDR.Checked = True Then
                vType = "F"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(FWDR,'') as FWDR From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("FWDR")) <> Trim(vType) Then
                    vDesc = "Changed Contract "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set FWDR = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("FWDR")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        'If Me.chk_FWDR.Checked = True Then
        'If Len(Trim(Me.wFMC.Text)) = 0 Then
        'MsgBox("FMC field is empty, you must fill the field,.. ")
        'Me.wFMC.Focus()
        'End If
        'End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Notify_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Notify.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = ""
            If Me.chk_Notify.Checked = True Then
                vType = "N"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Notify,'') as Notify From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Notify")) <> Trim(vType) Then
                    vDesc = "Changed Contract "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Notify = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Notify")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_trucker_CheckedChanged(sender As Object, e As EventArgs) Handles chk_trucker.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Me.chk_trucker.Checked = True Then
            Me.dtp_Trucker_Insure_Date.Enabled = True
        Else
            Me.dtp_Trucker_Insure_Date.Enabled = False
        End If
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_trucker.Checked = True Then
                vType = "T"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Trucker,'') as Trucker From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Trucker")) <> Trim(vType) Then
                    vDesc = "Changed Contract "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Trucker = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Trucker")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Warehouse_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Warehouse.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Warehouse.Checked = True Then
                vType = "W"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Contract,'') as Warehouse From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Warehouse")) <> Trim(vType) Then
                    vDesc = "Changed Contract "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Warehouse = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Warehouse")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub wFMC_KeyDown(sender As Object, e As KeyEventArgs) Handles wFMC.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(FMC,'') as FMC From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("FMC")) <> Trim(Me.wFMC.Text) Then
                        vDesc = "Changed FMC "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set FMC = '" & Trim(Me.wFMC.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("FMC")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

#Region "dgv Edit"

    Private Sub wEIN_KeyDown(sender As Object, e As KeyEventArgs) Handles wEIN.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(EIN,'') as EIN From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("EIN")) <> Trim(Me.wEIN.Text) Then
                        vDesc = "Changed EIN "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set EIN = '" & Trim(Me.wEIN.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("EIN")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub rb_Actived_CheckedChanged(sender As Object, e As EventArgs) Handles rb_Actived.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim nType As Integer = 0
            If Me.rb_Actived.Checked = True Then
                nType = 1
            Else
                nType = 0
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Active,0) as Active From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Active")) <> Trim(nType) Then
                    vDesc = "Changed Active in Location: " & Trim(Str(Me.wLoc.Text))
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Active = '" & Trim(Me.rb_Actived.Checked) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Active")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub wEIN_Validating(sender As Object, e As CancelEventArgs) Handles wEIN.Validating
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(EIN,'') as EIN From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("EIN")) <> Trim(Me.wEIN.Text) Then
                    vDesc = "Changed EIN "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set EIN = '" & Trim(Me.wEIN.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("EIN")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Dim ds_Cust As New DataSet
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Me.TabControl1.SelectedIndex = 1 Then
            Me.dgv_Customers.Rows.Clear()
            Me.dgv_Customers.Refresh()
            Me.dgv_Customers.Focus()
            Me.Refresh()
            Me.wCount_Customer.Clear()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER, rtrim(street) + ' ' + rtrim(Isnull(Suite,'')) + char(13) + char(10) + rtrim(Isnull(City,'')) + ' ' + rtrim(Isnull(State,'')) + rtrim(Isnull(Country,'')) as Address1 FROM CM_System WHERE (Customer = 'C') ORDER BY COMPANY_NAME"
            ds_Cust.Clear()
            ds_Cust = ws.GetDataset(strConnect, strSQL, 1)
            If ds_Cust.Tables(0).Rows.Count > 0 Then
                If ds_Cust.Tables(0).Rows.Count > 10000 Then
                    Dim style = MsgBoxStyle.YesNo
                    Dim response = MsgBox("Customers: " & Trim(Str(ds_Cust.Tables(0).Rows.Count - 1)) & ", Are you sure, Do you want to show them?", style, "Warning")
                    If response = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
                Me.dgv_Customers.DataSource = ds_Cust.Tables(0)
                Me.dgv_Customers.Columns("Company_Name").Width = 270
                Me.dgv_Customers.Columns("Company_Name").HeaderText = "Name"
                Me.dgv_Customers.Columns("Company_Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgv_Customers.Columns("Company_Number").Width = 70
                Me.dgv_Customers.Columns("Company_Number").HeaderText = "#"
                Me.dgv_Customers.Columns("Company_Number").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgv_Customers.Columns("Address1").Width = 370
                Me.dgv_Customers.Columns("Address1").HeaderText = "#"
                Me.dgv_Customers.Columns("Address1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgv_Customers.Columns("Address1").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                Me.dgv_Customers.Refresh()
                Me.wCount_Customer.Text = ds_Cust.Tables(0).Rows.Count - 1
            End If

        End If
        If TabControl1.SelectedIndex = 3 Then
            Me.dgv_Trucking_Ins.Rows.Clear()
            Me.wCount_Trucker_Ins.Clear()
            strSQL = "SELECT COMPANY_NUMBER, COMPANY_NAME, isnull(PHONE,'') as Phone, isnull(FAX,'') as Fax, ISNULL((SELECT TOP (1) isnull(email,'') as email FROM dbo.CMSeMail AS e WHERE (CMSNUM = n.COMPANY_NUMBER) AND ([Document] = 'TRUCKER')), '') AS eMail, 
                             ISNULL(Contact, '') AS Contact, ISNULL(Trucker_Ins_Date, '12/31/1999') AS Trucker_Ins_Date
                       FROM dbo.CM_System AS n
                      WHERE (TRUCKER = 'T') and Location_Number = 0 Order by Company_Name"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Trucking_Ins.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), Trim(ds.Tables(0).Rows(j).Item("Phone")), Trim(ds.Tables(0).Rows(j).Item("Fax")), Trim(ds.Tables(0).Rows(j).Item("eMail")), Trim(ds.Tables(0).Rows(j).Item("Contact")), Trim(ds.Tables(0).Rows(j).Item("Trucker_Ins_Date")))
                Next
                Me.wCount_Trucker_Ins.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If
        If Me.TabControl1.SelectedIndex = 4 Then
            Refresh_yards()
        End If
        If Me.TabControl1.SelectedIndex = 5 Then
            Me.dgv_Agents.Rows.Clear()
            Me.dgv_Agents.Refresh()
            Me.dgv_Agents.Focus()
            Me.Refresh()
            Me.wCount_Agents.Clear()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (Agent = 'A') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Agents.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Agents.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If
        If Me.TabControl1.SelectedIndex = 6 Then
            Me.dgv_Warehouse.Rows.Clear()
            Me.dgv_Warehouse.Refresh()
            Me.dgv_Warehouse.Focus()
            Me.wCount_Warehouse.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (Warehouse = 'W') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Warehouse.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Warehouse.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If

        If Me.TabControl1.SelectedIndex = 7 Then
            Me.dgv_ThirdParty.Rows.Clear()
            Me.dgv_ThirdParty.Refresh()
            Me.dgv_ThirdParty.Focus()
            Me.wCount_ThridParty.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (ThirdParty = 'Y') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds_ThirdParty.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_ThirdParty.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_ThridParty.Text = ds_ThirdParty.Tables(0).Rows.Count
            End If
        End If
        If Me.TabControl1.SelectedIndex = 8 Then
            Refresh_Trucker()
        End If
        If Me.TabControl1.SelectedIndex = 9 Then
            Me.dgv_ThirdParty.Rows.Clear()
            Me.dgv_ThirdParty.Refresh()
            Me.dgv_ThirdParty.Focus()
            Me.wCount_Vessel.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (isnull(Vessel_Owner,'') = 'Y') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Vessel_owner.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Vessel.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If
        If Me.TabControl1.SelectedIndex = 10 Then
            Me.dgv_Contract.Rows.Clear()
            Me.dgv_Contract.Refresh()
            Me.dgv_Contract.Focus()
            Me.wCount_Contract.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (isnull(Contract,'') = 'Y') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Contract.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Contract.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If
        If Me.TabControl1.SelectedIndex = 11 Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Me.Txt_Delivery_Terminal.Clear()
            Me.txt_Terminal_Notes_Comments.Clear()
            Me.dgv_Terminal.Rows.Clear()
            Me.dgv_Terminal.Refresh()
            Me.dgv_Terminal.Focus()
            Me.wCount_Terminal.Clear()
            Me.Refresh()
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (isnull(Port_Yard,'') = 'Y') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Terminal.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Contract.Text = ds.Tables(0).Rows.Count
                Me.dgv_Terminal.Rows(0).Selected = True
                Me.dgv_Terminal.Refresh()
            End If

            If Not IsNothing(Me.dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value) Then
                strSQL = "Select isnull(Notes_Comments,'') as Notes_Comments, isNull(Delivery_Notes,'') as Delivery_Notes From Booking_Terminal_Notes_Comments Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
                ds.Clear()
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.txt_Terminal_Notes_Comments.Text = Trim(ds.Tables(0).Rows(0).Item("Notes_Comments"))
                    Me.Txt_Delivery_Terminal.Text = Trim(ds.Tables(0).Rows(0).Item("Delivery_Notes"))
                Else
                    Me.txt_Terminal_Notes_Comments.Clear()
                    Me.Txt_Delivery_Terminal.Clear()
                End If
            End If
            ds = Nothing
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
            If Me.TabControl1.SelectedIndex = 13 Then
            Me.Audit_Search()
        End If
        If Me.TabControl1.SelectedIndex = 14 Then
            strSQL = "SELECT e.CMSNUM, ISNULL(n.COMPANY_NAME, '') AS Company_name, 
                             RTRIM(RTRIM(ISNULL(n.STREET, '') + ' ' + ISNULL(n.SUITE, ''))  + ', ' + ISNULL(n.City, '') + ' ' + ISNULL(n.STATE, '') + ' ' + ISNULL(n.ZIP, '')) AS Address, 
                             ISNULL(n.PHONE, '') AS Comp_Phone, ISNULL(e.Contact_Name, '') AS Contact_Name, ISNULL(e.Phone, '') AS Contact_Phone, ISNULL(e.email, '') AS email
                        FROM CMSeMail AS e INNER JOIN
                             CM_System AS n ON n.COMPANY_NUMBER = e.CMSNUM AND n.LOCATION_NUMBER = 0
                        ORDER BY n.COMPANY_NAME, Contact_Name, e.Phone, email"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            'ds = ws.SP_1_Param_int(strConnect, "BK_BreakBulk_with_Units_Rate_x_BKNro", "@BK", Me.wBooking.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim cr_CMS_List As New cr_CMS_List
                cr_CMS_List.SetDataSource(ds.Tables(0))
                Me.cv_CMS_List.ReportSource = cr_CMS_List
                Dim obTxt_cmp As CrystalDecisions.CrystalReports.Engine.TextObject = cr_CMS_List.ReportDefinition.Sections(1).ReportObjects("txt_cmp")
                obTxt_cmp.Text = Trim(md.GL_Company_Name)
                Dim obTxt_cmp_address As CrystalDecisions.CrystalReports.Engine.TextObject = cr_CMS_List.ReportDefinition.Sections(1).ReportObjects("txt_cmp_address")
                obTxt_cmp_address.Text = Trim(md.KOGA_Booking_Address)

                Me.cv_CMS_List.BringToFront()
                Me.cv_CMS_List.RefreshReport()
                Me.cv_CMS_List.DisplayToolbar = True
                Me.cv_CMS_List.DisplayStatusBar = True
                Me.cv_CMS_List.Zoom(85)
                Me.cv_CMS_List.Refresh()
                Me.cv_CMS_List.Show()
            End If
        End If
        If Me.TabControl1.SelectedIndex = 15 Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Me.Refresh()
            ds_CMS_SDN.Clear()
            'strSQL = "SELECT Company_Name as Name FROM CM_System where isnull(SDN_list,'') = '' ORDER BY Company_Name"
            strSQL = "SELECT Company_Name as Name FROM CM_System ORDER BY Company_Name"
            ds_CMS_SDN = ws.GetDataset(strConnect, strSQL, 1)
            If ds_CMS_SDN.Tables(0).Rows.Count > 0 Then
                Me.dgv_CMS.DataSource = ds_CMS_SDN.Tables(0)
                Me.dgv_CMS.Columns("Name").Width = 280
                Me.dgv_CMS.Columns("Name").HeaderText = "Company"
                Me.dgv_CMS.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Me.dgv_CMS.Refresh()
            End If

            strSQL = "Select * From
                        (SELECT ROW_NUMBER() OVER (ORDER BY name) AS row_number, Name, sdn_List From
                        (SELECT Company_Name as Name, isnull(SDN_List,'') as SDN_List FROM CM_System) as T1) as T2
                        Where ltrim(rtrim(SDN_List)) <> ''
                         ORDER BY Name"
            Dim ind As Integer = 0
            Dim jj As Integer = 0
            Dim ds_S As New DataSet
            ds_S = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_S.Tables(0).Rows.Count > 0 Then
                ind = 0
                For jj = 0 To ds_S.Tables(0).Rows.Count - 1
                    ind = ds_S.Tables(0).Rows(jj).Item("row_number") - 1
                    Me.dgv_CMS.Rows(ind).Cells(0).Style.BackColor = Color.Black
                    Me.dgv_CMS.Rows(ind).Cells(0).Style.ForeColor = Color.White
                Next
            End If
            ds_S.Clear()

            ds_SDN_List.Clear()
            ds_SDN_List = ws.GetDataset(strConnect, "SELECT Company_Name as Name FROM dbo.SDN_List ORDER BY Company_Name", 1)
            If ds_SDN_List.Tables(0).Rows.Count > 0 Then
                Me.dgv_SDN_List.DataSource = ds_SDN_List.Tables(0)
                Me.dgv_SDN_List.Columns("Name").Width = 280
                Me.dgv_SDN_List.Columns("Name").HeaderText = "Company"
                Me.dgv_SDN_List.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Me.dgv_SDN_List.Refresh()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        If Me.TabControl1.SelectedIndex = 16 Then
            Me.dgv_Eq_Owners.Rows.Clear()
            Me.dgv_Eq_Owners.Refresh()
            Me.dgv_Eq_Owners.Focus()
            Me.wCount_Eq_Owners.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE (isnull(Eq_Owner_Leased,'') = 'Y') ORDER BY COMPANY_NAME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Eq_Owners.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_Eq_Owners.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        End If


        If Me.TabControl1.SelectedIndex = 18 Then
            Me.dgv_Documents_GDZ.Rows.Clear()

            If Len(Trim(wCMS_Nro.Text)) = 0 Then
                MsgBox("CMS account field is empty.")
                Exit Sub
            End If
            strSQL = "SELECT CLAUSE, CREATION_DATE, CREATION_TIME, CREATED_BY, DESCRIPTION
                    FROM dbo.Clauses_GDZ
                    WHERE (REC_TYPE = 'X') AND (TRAN_TYPE = 'M') AND (DR_STEVEDORING = " & Me.wCMS_Nro.Text & ") ORDER BY Format(CREATION_DATE,'yyyy-MM-dd'), CREATION_TIME"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then

                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_Documents_GDZ.Rows.Add(ds.Tables(0).Rows(j).Item("DESCRIPTION"), ds.Tables(0).Rows(j).Item("CREATION_DATE"), ds.Tables(0).Rows(j).Item("CREATION_TIME"), ds.Tables(0).Rows(j).Item("CREATED_BY"), ds.Tables(0).Rows(j).Item("Clause"))
                    ', System.Drawing.Image.FromFile("C:\GDZ\Images\B000000000012284116.PDF")
                Next
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Dim ds_yards As New DataSet
    Private Sub Refresh_yards()
        strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER, RTRIM(ISNULL(STREET, '')) + ', ' + RTRIM(ISNULL(SUITE, '')) + ' ' + CHAR(13) + CHAR(10) + RTRIM(CITY) + ', ' + RTRIM(STATE) + ' ' + RTRIM(ZIP) AS Address FROM CM_System WHERE (YARD = 'Y') OR (YARD = 'P') ORDER BY COMPANY_NAME"
        Me.wCount_Yard.Clear()
        ds_yards.Clear()
        ds_yards = ws.GetDataset(strConnect, strSQL, 1)
        If ds_yards.Tables(0).Rows.Count > 0 Then
            Me.dgv_Yards.DataSource = ds_yards.Tables(0)
            Me.dgv_Yards.Columns(0).Width = 220
            Me.dgv_Yards.Columns(1).Width = 70
            Me.dgv_Yards.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Yards.Columns(2).Width = 350
            Me.dgv_Yards.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Me.dgv_Yards.Refresh()
            Me.wCount_Yard.Text = ds_yards.Tables(0).Rows.Count
        End If
    End Sub

    Dim ds_Trucker As New DataSet
    Private Sub Refresh_Trucker()
        If Me.rd_USA.Checked = True Then
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER, RTRIM(ISNULL(STREET, '')) + ', ' + RTRIM(ISNULL(SUITE, '')) + ' ' + CHAR(13) + CHAR(10) + RTRIM(CITY) + ', ' + RTRIM(STATE) + ' ' + RTRIM(ZIP) AS Address, ISNULL(CONVERT(nvarchar(MAX), Format(Trucker_Ins_Date,'yyyy-MM-dd'), 0), '') AS Exp_Ins_Date FROM CM_System WHERE (COUNTRY = 'USA') AND (Trucker = 'T') ORDER BY COMPANY_NAME"
        Else
            strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER, RTRIM(ISNULL(STREET, '')) + ', ' + RTRIM(ISNULL(SUITE, '')) + ' ' + CHAR(13) + CHAR(10) + RTRIM(CITY) + ', ' + RTRIM(STATE) + ' ' + RTRIM(ZIP) AS Address, ISNULL(CONVERT(nvarchar(MAX), Format(Trucker_Ins_Date,'yyyy-MM-dd'), 0), '') AS Exp_Ins_Date FROM CM_System WHERE (COUNTRY <> 'USA') AND (Trucker = 'T') ORDER BY COMPANY_NAME"
        End If
        Me.wCount_Truckers.Clear()
        ds_Trucker.Clear()
        ds_Trucker = ws.GetDataset(strConnect, strSQL, 1)
        If ds_Trucker.Tables(0).Rows.Count > 0 Then
            'Dim j As Integer = 0
            'For j = 0 To ds.Tables(0).Rows.Count - 1
            '    Me.dgv_Truckers.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
            'Next
            Me.dgv_Truckers.DataSource = ds_Trucker.Tables(0)

            Me.dgv_Truckers.Columns("Company_name").Width = 220
            Me.dgv_Truckers.Columns("Company_name").HeaderText = "Trucker"
            Me.dgv_Truckers.Columns("Company_name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Truckers.Columns("Company_number").Width = 70
            Me.dgv_Truckers.Columns("Company_number").HeaderText = "Account #"
            Me.dgv_Truckers.Columns("Company_number").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Truckers.Columns("Address").Width = 270
            Me.dgv_Truckers.Columns("Address").HeaderText = "Address"
            Me.dgv_Truckers.Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Truckers.Columns("Address").DefaultCellStyle.WrapMode = DataGridViewTriState.True

            Me.dgv_Truckers.Columns("Exp_Ins_Date").Width = 110
            Me.dgv_Truckers.Columns("Exp_Ins_Date").HeaderText = "Exp. Insp. Date"
            Me.dgv_Truckers.Columns("Exp_Ins_Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Me.dgv_Truckers.Refresh()
            Me.wCount_Truckers.Text = ds_Trucker.Tables(0).Rows.Count

            'Dim j As Integer = 0
            'For j = 0 To dgv_Truckers.Rows.Count - 1
            '    Me.dgv_Truckers.Rows(j).Cells(3).Style.BackColor = Color.White
            '    Me.dgv_Truckers.Rows(j).Cells(3).Style.ForeColor = Color.Black
            '    If md.Trucker_Insure_validation(Me.dgv_Truckers.Item(1, j).Value) = False Then
            '        Me.dgv_Truckers.Rows(j).Cells(3).Style.BackColor = Color.Red
            '        Me.dgv_Truckers.Rows(j).Cells(3).Style.ForeColor = Color.Yellow
            '    End If
            'Next
        End If
    End Sub

    Private Sub wDesc_Validating(sender As Object, e As CancelEventArgs) Handles wDesc.Validating
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Description,'') as Description From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = 0", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Description")) <> Trim(Me.wDesc.Text) Then
                    vDesc = "Changed Description "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Description = '" & Trim(Me.wDesc.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Mid(Trim(ds.Tables(0).Rows(0).Item("Description")), 1, 70))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub wContact_KeyDown(sender As Object, e As KeyEventArgs) Handles wContact.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Contact,'') as Contact From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Contact")) <> Trim(Me.wContact.Text) Then
                        vDesc = "Changed Contact "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Contact = '" & Trim(Me.wContact.Text) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                        Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Contact")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub


#End Region
    Private Sub chk_ThirdParty_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ThirdParty.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_ThirdParty.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(ThirdParty,'') as ThirdParty From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("ThirdParty")) <> Trim(vType) Then
                    vDesc = "Changed Third Party "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set ThirdParty = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Replace(Me.cb_CMS.SelectedItem("Company_Name"), "'", "''")), 0, Trim(ds.Tables(0).Rows(0).Item("ThirdParty")))
                End If
            End If
            ds = Nothing

            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Export_Click(sender As Object, e As EventArgs) Handles bnt_Export.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim nCount As Integer = 0
            Dim j As Integer = 0
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT COMPANY_NUMBER, isnull(LOCATION_NUMBER,0) as Location_Number, COMPANY_NAME, isnull(LOCATION_NAME,'') as Location_name, isnull(STREET,'') as Street, isnull(SUITE,'') as Suite, isnull(CITY,'') as City, isnull(STATE,'') as State, isnull(ZIP,'') as zip, isnull(PHONE,'') as Phone, isnull(FAX,'') as Fax, isnull(COUNTRY,'') Country, isnull(active,0) as active
                                                FROM dbo.CM_System
                                                ORDER BY COMPANY_NUMBER, LOCATION_NUMBER", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                nCount = ds.Tables(0).Rows.Count
                Me.txt_CMS_Count.Text = ds.Tables(0).Rows.Count
                Dim nro As Integer = 0
                ' ------ Header Template ----------------------------
                Dim Lin1 As String = "" & Chr(44) & "Company_Number" & Chr(44) & "Location_Number" & "Company_Name" & Chr(44) & "Street" & Chr(44) & "Suite" & Chr(44) & "City" & Chr(44) & "State" & Chr(44) & "Zip" & Chr(44) & "Phone" & Chr(44) & "Fax" & Chr(44) & "Country" & Chr(44) & "Actived"
                Dim LinDtl As String = ""
                If Not System.IO.Directory.Exists("c:\EDI\") Then
                    System.IO.Directory.CreateDirectory("C:\EDI")
                    System.IO.Directory.CreateDirectory("C:\EDI\CMS")
                Else
                    If Not System.IO.Directory.Exists("C:\EDI\CMS") Then
                        System.IO.Directory.CreateDirectory("C:\EDI\CMS")
                    End If
                End If
                nro = FreeFile()

                Dim FileName As String = "C:\EDI\CMS\CMS" & Trim(Format(System.DateTime.Today, "yyyyMMddhhss")) & ".csv"
                '------- Delete File of out ---------------------
                If (System.IO.File.Exists(FileName)) Then
                    System.IO.File.Delete("filename")
                End If
                FileOpen(nro, FileName, OpenMode.Binary)
                FilePut(nro, Lin1 & Chr(13) & Chr(10))

                Dim strSQL As String = ""
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    LinDtl = Trim(ds.Tables(0).Rows(j).Item("Company_Number")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Location_Number")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Company_Name")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Location_Name")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Street")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Suite")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("City")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("State")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("zip")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Phone")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Fax")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Country")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Active"))
                    FilePut(nro, LinDtl & Chr(13) & Chr(10))
                    nCount = nCount - 1
                    Me.txt_CMS_Count.Text = nCount
                    Me.txt_CMS_Count.Refresh()
                Next

                FileClose(nro)
            End If
            Me.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub chk_Ship_Owner_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Ship_Owner.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Ship_Owner.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Vessel_Owner,'') as Vessel_Owner From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Vessel_Owner")) <> Trim(vType) Then
                    vDesc = "Changed Vessel Owner"
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Vessel_Owner = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Replace(Me.cb_CMS.SelectedItem("Company_Name"), "'", "''")), 0, Trim(ds.Tables(0).Rows(0).Item("Vessel_Owner")))
                End If
            End If
            ds = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

#Region "Audit"
    Private Sub Audit_Search()
        If Len(Trim(Me.wCMS_Name.Text)) > 0 Then
            If Len(Trim(Me.wCMS_Nro.Text)) > 0 Then
                ' ------- Audit
                Dim ds As New DataSet
                strSQL = "SELECT Company_Number, Company_name, Loc, Description, Old_value, Created_By, Created_On FROM  dbo.CMS_Audit where Company_Number = " & Trim(Me.wCMS_Nro.Text) & " Order By uid Desc "
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.dgv_Audit.Rows.Clear()
                    Dim i As Integer = 0
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Me.dgv_Audit.Rows.Add(ds.Tables(0).Rows(i).Item("Created_On"), ds.Tables(0).Rows(i).Item("Created_By"), ds.Tables(0).Rows(i).Item("Description"), ds.Tables(0).Rows(i).Item("Old_Value"))
                    Next
                    ds = Nothing
                    i = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub dgv_Agents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Agents.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Agents.RowCount - 1 Then
            If Me.dgv_Agents.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Agents.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Agents.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub
#End Region

    Private Sub CMS_Search_Refresh(ByVal CMS_Name As String)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            ds_CMS.Clear()
            strSQL = "Select DISTINCT Top 57 Company_Name, Company_Number,isnull(shipper,'') as shipper,isnull(Consignee,'') as Consignee,isnull(FWDR,'') as FWDR,isnull(Notify,'') as Notify
                                                      ,isnull(Trucker,'') as Trucker,isnull(Warehouse,'') as Warehouse,isnull(Yard,'') as Yard,
                                                      isnull(Agent,'') as Agent,isnull(ThirdParty,'') as ThirdParty,isnull(Vessel_Owner,'') as Vessel_owner, isnull(Contract,'') as Contract, isnull(FMC,'') as FMC,
                                                      isnull(Port_Yard,'') as Terminal, isnull(Customer,'') as Customer, isnull(Vendor,'') as Vendor,
                                                      isnull(Eq_Owner_Leased,'') as Eq_Owner_Leased,
                                                      isnull(EIN,'') as EIN,
                                                      isnull(Contact,'') as Contact,
                                                      isnull(Format(Trucker_Ins_Date,'MM/dd/yyyy'),'01/01/1990') as Trucker_Ins_Date,
                                                      isNull(Active,0) as Active, isnull(Description,'') as Description, isnull(AR_Collectors,'') AR_Collector 
                                              From CM_System Where Company_Name Like '" & Trim(Replace(CMS_Name, "'", "''")) & "%' Order by Company_Name"
            ds_CMS = ws.GetDataset(md.strConnect, strSQL, 1) 'Me.CMS_MasterTableAdapter.Fill(Me.Ds_CMS_Master.CMS_Master, Trim(zDesc))
            If ds_CMS.Tables(0).Rows.Count > 0 Then
                Me.cb_CMS.DataSource = ds_CMS.Tables(0)
                Me.cb_CMS.DisplayMember = "Company_Name"
                Me.cb_CMS.ValueMember = "Company_Number"
                Me.cb_CMS.Refresh()

                Flag_Modo = 1
                Me.wCMS_Nro.Text = Me.cb_CMS.SelectedItem("Company_Number")
                Me.wCMS_Name.Text = Me.cb_CMS.SelectedItem("Company_Name")
                Me.cb_AR_Collectors.SelectedValue = Me.cb_CMS.SelectedItem("AR_Collector")
                Me.wEIN.Text = Me.cb_CMS.SelectedItem("EIN")
                Me.wFMC.Text = Me.cb_CMS.SelectedItem("FMC")
                Me.wContact.Text = Me.cb_CMS.SelectedItem("Contact")
                Me.wDesc.Text = Me.cb_CMS.SelectedItem("Description")
                Me.rb_Actived.Checked = Me.cb_CMS.SelectedItem("Active")

                Me.dtp_Trucker_Insure_Date.Value = Me.cb_CMS.SelectedItem("Trucker_Ins_Date")
                If Trim(Me.cb_CMS.SelectedItem("Agent")) = "A" Then
                    Me.chk_Agent.Checked = True
                Else
                    Me.chk_Agent.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Shipper")) = "S" Then
                    Me.chk_Shipper.Checked = True
                Else
                    Me.chk_Shipper.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Consignee")) = "C" Then
                    Me.chk_Consignee.Checked = True
                Else
                    Me.chk_Consignee.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("FWDR")) = "F" Then
                    Me.chk_FWDR.Checked = True
                Else
                    Me.chk_FWDR.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Notify")) = "N" Then
                    Me.chk_Notify.Checked = True
                Else
                    Me.chk_Notify.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Warehouse")) = "W" Then
                    Me.chk_Warehouse.Checked = True
                Else
                    Me.chk_Warehouse.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Yard")) = "Y" Then
                    Me.chk_Yard.Checked = True
                Else
                    Me.chk_Yard.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Trucker")) = "T" Then
                    Me.chk_trucker.Checked = True
                Else
                    Me.chk_trucker.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Contract")) = "Y" Then
                    Me.chk_Contract.Checked = True
                Else
                    Me.chk_Contract.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Vessel_Owner")) = "Y" Then
                    Me.chk_Ship_Owner.Checked = True
                Else
                    Me.chk_Ship_Owner.Checked = False
                End If

                If Trim(Me.cb_CMS.SelectedItem("Vendor")) = "V" Then
                    Me.chk_Vendor.Checked = True
                Else
                    Me.chk_Vendor.Checked = False
                End If
                If Trim(Me.cb_CMS.SelectedItem("Customer")) = "Y" Then
                    Me.chk_Customer.Checked = True
                Else
                    Me.chk_Customer.Checked = False
                End If

                If Trim(Me.cb_CMS.SelectedItem("ThirdParty")) = "Y" Then
                    Me.chk_ThirdParty.Checked = True
                Else
                    Me.chk_ThirdParty.Checked = False
                End If

                If Trim(Me.cb_CMS.SelectedItem("Eq_Owner_Leased")) = "Y" Then
                    Me.chk_Eq_Owner_Leased.Checked = True
                Else
                    Me.chk_Eq_Owner_Leased.Checked = False
                End If

                If ds_CMS.Tables(0).Rows.Count = 1 Then
                    Dim vSDN_list As String = md.CMS_Get_SDN_List(Me.cb_CMS.SelectedItem("Company_Number"))
                    If Len(Trim(vSDN_list)) > 0 Then
                        Me.txt_SDN_Black_List.Visible = True
                        Me.txt_SDN_List.Visible = True
                        Me.txt_SDN_List.Text = Trim(vSDN_list)
                    Else
                        Me.txt_SDN_Black_List.Visible = False
                        Me.txt_SDN_List.Visible = False
                        Me.txt_SDN_List.Clear()
                    End If
                    vSDN_list = Nothing
                End If
            End If
            Me.CMS_Address_Refresh()
            Me.CMS_eMail_Refresh()
            Me.CMS_Notes_refresh()
            Me.CMS_Contracts()

            Me.Modo_Edit_ADD_Read_Only(1)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub dgv_Contract_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Contract.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Contract.RowCount Then
            If Me.dgv_Contract.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Contract.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Contract.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_ThirdParty_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_ThirdParty.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_ThirdParty.RowCount Then
            If Me.dgv_ThirdParty.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_ThirdParty.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_ThirdParty.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_Truckers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Truckers.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Truckers.RowCount Then
            If Me.dgv_Truckers.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Truckers.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Truckers.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_Vessel_owner_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Vessel_owner.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Vessel_owner.RowCount Then
            If Me.dgv_Vessel_owner.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Vessel_owner.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Vessel_owner.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_Warehouse_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Warehouse.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Warehouse.RowCount Then
            If Me.dgv_Warehouse.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Warehouse.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Warehouse.Item(0, e.RowIndex).Value))

                Me.txt_Warehouse_Clauses.Clear()
                Dim ds As New DataSet
                strSQL = "Select isnull(Comment,'') as Comment From DRClauses Where Warehouse = " & Me.dgv_Warehouse.Item(1, e.RowIndex).Value
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.txt_Warehouse_Clauses.Text = Trim(ds.Tables(0).Rows(0).Item("Comment"))
                End If
                Me.txt_Warehouse_Clauses.Focus()

                'TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_Yards_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Yards.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Yards.RowCount Then
            If Me.dgv_Yards.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Yards.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Yards.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dgv_CMS_Address_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv_CMS_Address.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Flag_Modo = 2 Then
                If Me.dgv_CMS_Address.CurrentCell.RowIndex > -1 And Me.dgv_CMS_Address.CurrentCell.RowIndex < Me.dgv_CMS_Address.RowCount Then
                    If Len(Trim(Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value)) > 0 Then
                        Dim ds As New DataSet
                        strSQL = "SELECT COMPANY_NUMBER, LOCATION_NUMBER, COMPANY_NAME
                                    FROM dbo.CM_System
                                   WHERE Company_Number = " & Me.wCMS_Nro.Text & " and Location_Number = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & " and (SHIPPER IS NULL) AND (CONSIGNEE IS NULL) AND (FWDR IS NULL) AND (NOTIFY IS NULL) AND (TRUCKER IS NULL) AND (WAREHOUSE IS NULL) AND (YARD IS NULL) AND (Agent IS NULL) AND (Port_Yard IS NULL) AND 
                                                             (VENDOR IS NULL) AND (CUSTOMER IS NULL) AND (Contract IS NULL) AND (Support_Containers IS NULL) AND (Vessel_Owner IS NULL) AND (ThirdParty IS NULL)"
                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count = 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have transactions,...")
                            Exit Sub
                        End If
                        ' ------- BLs Validation --------------------------------------------
                        ds.Clear()
                        strSQL = "Select Top 1 BL_Number From BillOfLoadings Where (Shipper_Number = " & Me.wCMS_Nro.Text & " and Shipper_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (FWDR_Number = " & Me.wCMS_Nro.Text & " and FWDR_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Notify_Number = " & Me.wCMS_Nro.Text & " and Notify_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Cons_Number = " & Me.wCMS_Nro.Text & " and Cons_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")"
                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have BLs,...")
                            Exit Sub
                        End If
                        ' ------- BKs Validation --------------------------------------------
                        ds.Clear()
                        strSQL = "Select Top 1 Booking_Number From Bookings_Headline Where (Shipper_Number = " & Me.wCMS_Nro.Text & " and Shipper_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (FWDR_Number = " & Me.wCMS_Nro.Text & " and FWDR_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Notify_No = " & Me.wCMS_Nro.Text & " and Notify_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Consignee_No = " & Me.wCMS_Nro.Text & " and Consignee_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")"
                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have BKs,...")
                            Exit Sub
                        End If
                        ' ------- DRs Validation --------------------------------------------
                        ds.Clear()
                        strSQL = "Select Top 1 DR_Number From DockReceipts Where (From_Number = " & Me.wCMS_Nro.Text & " and From_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (To_Number = " & Me.wCMS_Nro.Text & " and To_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Notify_No = " & Me.wCMS_Nro.Text & " and Notify_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")" &
                                                                          " or (Consignee_No = " & Me.wCMS_Nro.Text & " and Consignee_Loc = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")"
                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have DRs,...")
                            Exit Sub
                        End If
                        ' ------- ARs Validation --------------------------------------------
                        ds.Clear()
                        strSQL = "Select Top 1 Invoice_Number From ARHDR Where (Customer_Number = " & Me.wCMS_Nro.Text & " and Location_Number = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")"

                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have ARs,...")
                            Exit Sub
                        End If
                        ' ------- APs Validation --------------------------------------------
                        ds.Clear()
                        strSQL = "Select Top 1 Invoice_Number From APHDR Where (Vendor_Number = " & Me.wCMS_Nro.Text & " and Location_Number = " & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value & ")"
                        ds = ws.GetDataset(strConnect, strSQL, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            MsgBox("This CMS Account can not be remove, becasue this account have APs,...")
                            Exit Sub
                        End If

                        Dim style = MsgBoxStyle.YesNo
                        Dim response = MsgBox("Are you sure, Do you want to remove this Address: " & Trim(Me.dgv_CMS_Address.Item(1, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                        If response = MsgBoxResult.Yes Then
                            eResp = ws.ExecSQL(strConnect, "Delete CM_System Where Company_Number = " & Me.wCMS_Nro.Text & " and Location_Number  = '" & Me.dgv_CMS_Address.Item(0, Me.dgv_CMS_Address.CurrentCell.RowIndex).Value) & "'"
                            Me.CMS_Contracts()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#Region "Notes"
    Dim TxtMedType_n As TextBox
    Private Sub dgv_Notes_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_Notes.EditingControlShowing
        Try
            If Me.dgv_Notes.CurrentCellAddress.X = 0 Or Me.dgv_Notes.CurrentCellAddress.X = 1 Then
                TxtMedType_n = CType(e.Control, TextBox)
                AddHandler TxtMedType_n.Validating, AddressOf TxtMedTypeN_Validation
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TxtMedTypeN_Validation(sender As Object, e As CancelEventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_Notes.CurrentCellAddress.X = 0 Then
                Dim vMove As String = ""
                vMove = Trim(TxtMedType_n.Text)
                If Flag_Modo = 2 Then
                    If Len(Trim(vMove)) > 0 Then
                        If Len(Trim(Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)) > 0 Then
                            If Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value > 0 Then
                                Dim Old_Value As String = ""
                                Dim New_Value As String = ""
                                New_Value = Trim(TxtMedType_n.Text)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                                Dim ds As New DataSet
                                ds = ws.GetDataset(md.strConnect, "Select isNull([subject],'') as [Subject],uid From CMSNotes Where uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value, 1)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Old_Value = Trim(ds.Tables(0).Rows(0).Item("Subject"))
                                    If Trim(Old_Value) <> Trim(New_Value) Then
                                        Dim vDesc As String = "Changed Subject"
                                        md.eResp = ws.ExecSQL(md.strConnect, "Update CMSNotes Set [Subject] = '" & Trim(New_Value) & "' Where  uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)
                                        If Len(Trim(Old_Value)) > 0 Then
                                            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Old_Value))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                vMove = Nothing
            End If
            If Me.dgv_Notes.CurrentCellAddress.X = 1 Then
                Dim vMove As String = ""
                vMove = Trim(TxtMedType_n.Text)
                If Flag_Modo = 2 Then
                    If Len(Trim(vMove)) > 0 Then
                        If Len(Trim(Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)) > 0 Then
                            If Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value > 0 Then
                                Dim Old_Value As String = ""
                                Dim New_Value As String = ""
                                New_Value = Trim(TxtMedType_n.Text)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                                Dim ds As New DataSet
                                ds = ws.GetDataset(md.strConnect, "Select isNull([Text],'') as Note,uid From CMSNotes Where uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value, 1)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Old_Value = Trim(ds.Tables(0).Rows(0).Item("Note"))
                                    If Trim(Old_Value) <> Trim(New_Value) Then
                                        Dim vDesc As String = "Changed Note"
                                        md.eResp = ws.ExecSQL(md.strConnect, "Update CMSNotes Set Text = '" & Trim(New_Value) & "' Where  uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)
                                        If Len(Trim(Old_Value)) > 0 Then
                                            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Old_Value))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                vMove = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub dgv_Notes_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgv_Notes.UserAddedRow
        If Flag_Modo = 2 Or Flag_Modo = 3 Then
            If Me.dgv_Notes.CurrentCellAddress.X = 0 Or Me.dgv_Notes.CurrentCellAddress.X = 1 Then
                Me.dgv_Notes.Item(0, Me.dgv_Notes.CurrentCell.RowIndex).Value = ""
                Me.dgv_Notes.Item(1, Me.dgv_Notes.CurrentCell.RowIndex).Value = ""
                Me.dgv_Notes.Item(2, Me.dgv_Notes.CurrentCell.RowIndex).Value = System.DateTime.Today.ToShortDateString
                Me.dgv_Notes.Item(3, Me.dgv_Notes.CurrentCell.RowIndex).Value = Trim(System.Environment.UserName)
                If Flag_Modo = 2 Then
                    strSQL = "Insert Into CMSNotes (Company_Number, Subject, text, Created_on, Created_by) Values (" &
                                  Me.wCMS_Nro.Text & ",'','','" & System.DateTime.Today.ToShortDateString & "','" & Trim(System.Environment.UserName) & "')"
                    md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                    Me.Insert_Changes("ADD New Note,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(TxtMedType_n.Text, 1, 70)))
                    Dim ds As New DataSet
                    ds = ws.GetDataset(strConnect, "Select Top 1 uid From CMSNotes Order By uid Desc", 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value = ds.Tables(0).Rows(0).Item("uid")
                    Else
                        Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value = 1
                    End If
                    'Me.CMS_Notes_refresh()
                End If
            End If
        End If
    End Sub
    Private Sub dgv_Notes_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv_Notes.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Flag_Modo = 2 Then
                If Me.dgv_Notes.CurrentCell.RowIndex > -1 And Me.dgv_Notes.CurrentCell.RowIndex < Me.dgv_Notes.RowCount Then
                    If Len(Trim(Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)) > 0 Then
                        Dim style = MsgBoxStyle.YesNo
                        Dim response = MsgBox("Are you sure, Do you want to remove this Note: " & Trim(Me.dgv_Notes.Item(1, Me.dgv_Notes.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                        If response = MsgBoxResult.Yes Then
                            eResp = ws.ExecSQL(strConnect, "Delete CMSNotes Where uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)
                            Me.Insert_Changes("Delete Note,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_Notes.Item(1, Me.dgv_Notes.CurrentCell.RowIndex).Value, 1, 70)))
                            Me.CMS_Notes_refresh()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dgv_Notes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Notes.CellContentClick
        If Flag_Modo = 2 Then
            If Me.dgv_Notes.CurrentCell.RowIndex > -1 And Me.dgv_Notes.CurrentCell.RowIndex < Me.dgv_Notes.RowCount Then
                If Len(Trim(Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)) > 0 Then
                    Dim style = MsgBoxStyle.YesNo
                    Dim response = MsgBox("Are you sure, Do you want to remove this Note: " & Trim(Me.dgv_Notes.Item(1, Me.dgv_Notes.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                    If response = MsgBoxResult.Yes Then
                        eResp = ws.ExecSQL(strConnect, "Delete CMSNotes Where uid = " & Me.dgv_Notes.Item(4, Me.dgv_Notes.CurrentCell.RowIndex).Value)
                        Me.Insert_Changes("Delete Note,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_Notes.Item(1, Me.dgv_Notes.CurrentCell.RowIndex).Value, 1, 70)))
                        Me.CMS_Notes_refresh()
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "emails"
    Dim TxtMedType As TextBox
    Dim ComboMedType As ComboBox
    Dim CheckMedType As CheckBox
    Private Sub dgv_eMails_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_eMails.EditingControlShowing
        Try
            If TypeOf e.Control Is TextBox Then
                DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
            End If
            If Me.dgv_eMails.CurrentCellAddress.X = 0 Or Me.dgv_eMails.CurrentCellAddress.X = 1 Or Me.dgv_eMails.CurrentCellAddress.X = 2 Then
                TxtMedType = CType(e.Control, TextBox)
                AddHandler TxtMedType.Validating, AddressOf TxtMedType_Validation
            End If

            If Me.dgv_eMails.CurrentCellAddress.X = 3 Then
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
    Private Sub TxtMedType_Validation(sender As Object, e As CancelEventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_eMails.CurrentCellAddress.X = 0 Then
                Dim vMove As String = ""
                vMove = Trim(TxtMedType.Text)
                Dim pos As Integer = 0
                pos = InStr(vMove, "@")
                If pos = 0 Then
                    MsgBox("Invalid eMail address,.... ")
                    Exit Sub
                End If
                pos = 0
                pos = InStr(vMove, ".")
                If pos = 0 Then
                    MsgBox("Invalid eMail address,.... ")
                End If
                If Flag_Modo = 2 Then
                    If Len(Trim(vMove)) > 0 Then
                        If Len(Trim(Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)) > 0 Then
                            If Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value > 0 Then
                                Dim Old_Value As String = ""
                                Dim New_Value As String = ""
                                New_Value = Trim(TxtMedType.Text)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                                Dim ds As New DataSet
                                ds = ws.GetDataset(md.strConnect, "Select isNull(eMail,'') as eMail,uid From CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Old_Value = Trim(ds.Tables(0).Rows(0).Item("eMail"))
                                    If Trim(Old_Value) <> Trim(New_Value) Then
                                        Dim vDesc As String = "Changed email"
                                        md.eResp = ws.ExecSQL(md.strConnect, "Update CMSeMail Set email = '" & New_Value & "' Where  uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                                        If Len(Trim(Old_Value)) > 0 Then
                                            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Old_Value))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                vMove = Nothing
            End If
            If Me.dgv_eMails.CurrentCellAddress.X = 1 Then
                Dim vMove As String = ""
                vMove = Trim(TxtMedType.Text)
                If Flag_Modo = 2 Then
                    If Len(Trim(vMove)) > 0 Then
                        If Len(Trim(Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)) > 0 Then
                            If Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value > 0 Then
                                Dim Old_Value As String = ""
                                Dim New_Value As String = ""
                                New_Value = Trim(TxtMedType.Text)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                                Dim ds As New DataSet
                                ds = ws.GetDataset(md.strConnect, "Select isNull(Contact_Name,'') as Contact_Name,uid From CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Old_Value = Trim(ds.Tables(0).Rows(0).Item("Contact_Name"))
                                    If Trim(Old_Value) <> Trim(New_Value) Then
                                        Dim vDesc As String = "Changed Name"
                                        md.eResp = ws.ExecSQL(md.strConnect, "Update CMSeMail Set Contact_name = '" & Trim(New_Value) & "' Where  uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                                        If Len(Trim(Old_Value)) > 0 Then
                                            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Old_Value))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                vMove = Nothing
            End If
            If Me.dgv_eMails.CurrentCellAddress.X = 2 Then
                Dim vMove As String = ""
                vMove = Trim(TxtMedType.Text)
                If Flag_Modo = 2 Then
                    If Len(Trim(vMove)) > 0 Then
                        If Len(Trim(Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)) > 0 Then
                            If Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value > 0 Then
                                Dim Old_Value As String = ""
                                Dim New_Value As String = ""
                                New_Value = Trim(TxtMedType.Text)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                                Dim ds As New DataSet
                                ds = ws.GetDataset(md.strConnect, "Select isNull(Phone,'') as Phone,uid From CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Old_Value = Trim(ds.Tables(0).Rows(0).Item("Phone"))
                                    If Trim(Old_Value) <> Trim(New_Value) Then
                                        Dim vDesc As String = "Changed Phone"
                                        md.eResp = ws.ExecSQL(md.strConnect, "Update CMSeMail Set Phone = '" & Trim(New_Value) & "' Where  uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                                        If Len(Trim(Old_Value)) > 0 Then
                                            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Old_Value))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                vMove = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ComboMedType_SelectedValueChanged(sender As Object, e As EventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_eMails.CurrentCellAddress.X = 3 Then
                If Flag_Modo = 2 Then
                    Dim vMove As String = ""
                    vMove = Trim(ComboMedType.SelectedItem)
                    If Len(Trim(vMove)) > 0 Then
                        Dim Old_Value As String = ""
                        Dim New_Value As String = ""
                        New_Value = Trim(ComboMedType.SelectedItem)   ' Me.dgw_BK_Dtl.Item(6, Me.dgw_BK_Dtl.CurrentCell.RowIndex).Value
                        Dim ds As New DataSet
                        ds = ws.GetDataset(md.strConnect, "Select [Document],isNull([Default],'') as [Default],uid From CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            Old_Value = Trim(ds.Tables(0).Rows(0).Item("Document"))
                            If Trim(Old_Value) <> Trim(New_Value) Then
                                Dim nLoc As Integer = 0
                                If Len(Trim(Me.wLoc.Text)) > 0 Then
                                    nLoc = Trim(Me.wLoc.Text)
                                End If
                                Dim vDesc As String = "Changed email Document"
                                md.eResp = ws.ExecSQL(md.strConnect, "Update CMSeMail Set [Document] = '" & Trim(New_Value) & "' Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                                'CMSNUM = " & Me.wCMS_Nro.Text & " and [Document] = '" & Trim(ds.Tables(0).Rows(0).Item("Document")) & "'")
                                If Len(Trim(Old_Value)) > 0 Then
                                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), nLoc, Trim(Old_Value))
                                End If
                                'Me.CMS_eMail_Refresh()
                            End If
                        End If
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub dgv_eMails_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgv_eMails.UserAddedRow
        ' MsgBox("User add new row")

        If Flag_Modo = 2 Then  'Or Flag_Modo = 3 
            ' ------- Change done 8/3/2018
            If Me.dgv_eMails.CurrentCellAddress.X = 0 Then
                If Me.dgv_eMails.CurrentCellAddress.X = 0 Or Me.dgv_eMails.CurrentCellAddress.X = 1 Or Me.dgv_eMails.CurrentCellAddress.X = 2 Or Me.dgv_eMails.CurrentCellAddress.X = 3 Then
                    Me.dgv_eMails.Item(0, Me.dgv_eMails.CurrentCell.RowIndex).Value = ""
                    Me.dgv_eMails.Item(1, Me.dgv_eMails.CurrentCell.RowIndex).Value = ""
                    Me.dgv_eMails.Item(2, Me.dgv_eMails.CurrentCell.RowIndex).Value = ""
                    Me.dgv_eMails.Item(3, Me.dgv_eMails.CurrentCell.RowIndex).Value = ""
                    Me.dgv_eMails.Item(4, Me.dgv_eMails.CurrentCell.RowIndex).Value = Trim(System.Environment.UserName)
                    Me.dgv_eMails.Item(5, Me.dgv_eMails.CurrentCell.RowIndex).Value = System.DateTime.Today.ToShortDateString
                    strSQL = "Insert Into CMSeMail (CMSNum, Contact_Name, Phone, [Document], email, Created_By, Creation_Date) Values (" &
                                  Me.wCMS_Nro.Text & ",'','','','','" & Trim(System.Environment.UserName) & "','" & System.DateTime.Today.ToShortDateString & "')"
                    md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                    Me.Insert_Changes("ADD New eMail,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(TxtMedType.Text, 1, 70)))
                    Dim ds As New DataSet
                    ds = ws.GetDataset(strConnect, "Select Top 1 uid From CMSemail Order By uid Desc", 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value = ds.Tables(0).Rows(0).Item("uid")
                    Else
                        Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value = 1
                    End If
                    Dim j As Integer = 0

                    For j = 0 To Me.dgv_eMails.RowCount - 1
                        Me.dgv_eMails.Rows(j).Cells(0).ReadOnly = False
                        Me.dgv_eMails.Rows(j).Cells(0).Style.BackColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(0).Style.SelectionBackColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(0).Style.SelectionForeColor = Color.Blue
                        Me.dgv_eMails.Rows(j).Cells(1).ReadOnly = False
                        Me.dgv_eMails.Rows(j).Cells(1).Style.BackColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(1).Style.SelectionForeColor = Color.Blue
                        Me.dgv_eMails.Rows(j).Cells(2).ReadOnly = False
                        Me.dgv_eMails.Rows(j).Cells(2).Style.BackColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(2).Style.SelectionForeColor = Color.Blue
                        Me.dgv_eMails.Rows(j).Cells(3).ReadOnly = False
                        Me.dgv_eMails.Rows(j).Cells(3).Style.BackColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(3).Style.SelectionForeColor = Color.Blue
                        Me.dgv_eMails.Rows(j).Cells(4).Style.BackColor = Color.MidnightBlue
                        Me.dgv_eMails.Rows(j).Cells(4).Style.ForeColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(4).ReadOnly = True
                        Me.dgv_eMails.Rows(j).Cells(5).Style.BackColor = Color.MidnightBlue
                        Me.dgv_eMails.Rows(j).Cells(5).Style.ForeColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(5).ReadOnly = True
                        Me.dgv_eMails.Rows(j).Cells(6).Style.BackColor = Color.MidnightBlue
                        Me.dgv_eMails.Rows(j).Cells(6).Style.ForeColor = Color.White
                        Me.dgv_eMails.Rows(j).Cells(6).ReadOnly = True
                    Next
                    Me.dgv_eMails.AllowUserToAddRows = True

                End If

            Else
                Me.dgv_eMails.Rows.RemoveAt(Me.dgv_eMails.CurrentCell.RowIndex)
                MsgBox("Line is empty, please enter eMail field,..")
                ' Me.dgw_DR_Dtl.AllowUserToAddRows = False
            End If
        Else
            ' Me.dgw_DR_Dtl.AllowUserToAddRows = False
        End If

    End Sub
    Private Sub dgv_eMails_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv_eMails.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Flag_Modo = 2 Then
                If Me.dgv_eMails.CurrentCell.RowIndex > -1 And Me.dgv_eMails.CurrentCell.RowIndex < Me.dgv_eMails.RowCount Then
                    If Len(Trim(Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)) > 0 Then
                        Dim style = MsgBoxStyle.YesNo
                        Dim response = MsgBox("Are you sure, Do you want to remove this eMail: " & Trim(Me.dgv_eMails.Item(1, Me.dgv_eMails.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                        If response = MsgBoxResult.Yes Then
                            eResp = ws.ExecSQL(strConnect, "Delete CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                            Me.Insert_Changes("Delete eMail,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_eMails.Item(1, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1, 70)))
                            Me.CMS_eMail_Refresh()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dgv_eMails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_eMails.CellContentClick
        If Flag_Modo = 2 Then
            If Me.dgv_eMails.CurrentCell.RowIndex > -1 And Me.dgv_eMails.CurrentCell.RowIndex < Me.dgv_eMails.RowCount Then
                If Len(Trim(Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)) > 0 Then
                    Dim style = MsgBoxStyle.YesNo
                    Dim response = MsgBox("Are you sure, Do you want to remove this eMail: " & Trim(Me.dgv_eMails.Item(1, Me.dgv_eMails.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                    If response = MsgBoxResult.Yes Then
                        eResp = ws.ExecSQL(strConnect, "Delete CMSeMail Where uid = " & Me.dgv_eMails.Item(6, Me.dgv_eMails.CurrentCell.RowIndex).Value)
                        Me.Insert_Changes("Delete eMail,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_eMails.Item(1, Me.dgv_eMails.CurrentCell.RowIndex).Value, 1, 70)))
                        Me.CMS_eMail_Refresh()
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "Contracts"
    Dim TxtMedType_c As TextBox
    Dim cbMedType_c As ComboBox
    Private Sub dgv_Contracts_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_Contracts.EditingControlShowing
        If Me.dgv_Contracts.CurrentCellAddress.X = 0 Then
            If TypeOf e.Control Is ComboBox Then
                DirectCast(e.Control, ComboBox).Text = CharacterCasing.Upper
            End If
            If Me.dgv_Contracts.CurrentCellAddress.X = 0 Then
                '            '------ ComboBox
                cbMedType_c = CType(e.Control, ComboBox)
                If (cbMedType_c IsNot Nothing) Then
                End If
                AddHandler cbMedType_c.SelectionChangeCommitted, AddressOf ComboMedTypec_SelectionChangeCommitted
            End If
        End If
    End Sub
    Private Sub ComboMedTypec_SelectionChangeCommitted(sender As Object, e As EventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Me.dgv_Contracts.Focus = True Then
                If Me.dgv_Contracts.CurrentCellAddress.X = 0 Then
                    If Flag_Modo > 1 Then
                        If Len(Trim(Me.wCMS_Nro.Text)) > 0 Then
                            Dim ds As New DataSet
                            strSQL = "SELECT isnull(CONTRACT_NUMBER,'') as Contract_Number FROM Contract_HDR Where isnull(Contract_number,'') = '" & Trim(cbMedType_c.SelectedValue) & "'"
                            ds = ws.GetDataset(strConnect, strSQL, 1)
                            If ds.Tables(0).Rows.Count = 0 Then
                                MsgBox("Contract #: " & Trim(TxtMedType_c.Text) & " not found,....")
                                Me.dgv_Contracts.BeginEdit(False)
                                Exit Sub
                            End If

                            strSQL = "SELECT CONTRACT_NUMBER, Customer_Number, Customer_Name FROM Contract_Cust WHERE (Customer_Number = " & Me.wCMS_Nro.Text & ") and isnull(Contract_number,'') = '" & Trim(cbMedType_c.SelectedValue) & "'"
                            ds.Clear()
                            ds = ws.GetDataset(strConnect, strSQL, 1)
                            If ds.Tables(0).Rows.Count = 0 Then
                                eResp = ws.ExecSQL(strConnect, "Update CM_System set Contract = 'Y' Where Company_Number = " & Me.wCMS_Nro.Text)

                                strSQL = "Insert Into Contract_Cust (CONTRACT_NUMBER, Customer_Number, Customer_Name) Values ('" & Trim(cbMedType_c.SelectedValue) & "','" & Me.wCMS_Nro.Text & "','" & Trim(Replace(Me.wCMS_Name.Text, "'", "''")) & "')"
                                eResp = ws.ExecSQL(strConnect, strSQL)

                                ' ------- Change #: 1000 ----------------------------------------------
                                eResp = ws.ExecSQL(strConnect, "Update Rate_Services Set Contract_Signed = 'Y' Where Contract_Number = '" & Trim(cbMedType_c.SelectedValue) & "'")
                                strSQL = "UPDATE dbo.Rate_Services SET Contract_Signed = 'Y' WHERE (CONTRACT_NUMBER = '" & Trim(cbMedType_c.SelectedValue) & "')"
                                eResp = ws.ExecSQL(strConnect, strSQL)
                                strSQL = "UPDATE Contract_Hdr SET Signed = 'Y' WHERE (CONTRACT_NUMBER = '" & Trim(cbMedType_c.SelectedValue) & "')"
                                eResp = ws.ExecSQL(strConnect, strSQL)

                                Me.Insert_Changes("Add Contract #: " & Trim(cbMedType_c.SelectedValue), Me.wCMS_Nro.Text, Trim(Me.wCMS_Name.Text), 0, "")
                                Me.Audit_Search()
                                Me.CMS_Contracts()
                            Else
                                'MsgBox("Contract already exists,....")
                            End If
                            Me.dgv_Contracts.BeginEdit(False)

                        Else
                            MsgBox("CMS # field is empty,...")
                            Me.wCMS_Name.Focus()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub dgv_Contracts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Contracts.CellDoubleClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Contracts.RowCount Then
            If Len(Trim(Me.wCMS_Nro.Text)) = 0 Then
                MsgBox("Customer field is empty,....")
                Exit Sub
            End If
            If Len(Trim(Me.dgv_Contracts.Item(0, e.RowIndex).Value)) = 0 Then
                MsgBox("Contract # is empty,....")
                Exit Sub
            End If
            If Me.dgv_Contracts.CurrentCellAddress.X = 0 Then
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Dim Contract_Document As New Contract_Document
                Contract_Document.Top = 0
                Contract_Document.Left = 0
                Contract_Document.txt_Customer.Text = Trim(Me.wCMS_Nro.Text)
                Contract_Document.txt_Contract_Number.Text = Trim(Me.dgv_Contracts.Item(0, e.RowIndex).Value)
                Contract_Document.Show()
                'EqMaster.ShowDialog(Me)
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
        End If
    End Sub
    Private Sub dgv_Contracts_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv_Contracts.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Flag_Modo = 2 Then
                If Me.dgv_Contracts.CurrentCell.RowIndex > -1 And Me.dgv_Contracts.CurrentCell.RowIndex < Me.dgv_Contracts.RowCount Then
                    If Len(Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value)) > 0 Then
                        Dim style = MsgBoxStyle.YesNo
                        Dim response = MsgBox("Are you sure, Do you want to remove this Contract: " & Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                        If response = MsgBoxResult.Yes Then
                            eResp = ws.ExecSQL(strConnect, "Delete Contract_Cust Where Customer_Number = " & Me.wCMS_Nro.Text & " and  Contract_Number  = '" & Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value) & "'")
                            Me.Insert_Changes("Delete Contract,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value, 1, 70)))
                            Me.CMS_Contracts()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dgv_Contracts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Contracts.CellContentClick
        If Flag_Modo = 2 Then
            If Me.dgv_Contracts.CurrentCell.RowIndex > -1 And Me.dgv_Contracts.CurrentCell.RowIndex < Me.dgv_Contracts.RowCount Then
                If Len(Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value)) > 0 Then
                    Dim style = MsgBoxStyle.YesNo
                    Dim response = MsgBox("Are you sure, Do you want to remove this Contract: " & Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value) & " ?", style, "Warning")
                    If response = MsgBoxResult.Yes Then
                        eResp = ws.ExecSQL(strConnect, "Delete Contract_Cust Where Customer_Number = " & Me.wCMS_Nro.Text & " and  Contract_Number  = '" & Trim(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value) & "'")
                        Me.Insert_Changes("Delete Contract,.", Me.cb_CMS.SelectedValue, Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Me.dgv_Contracts.Item(0, Me.dgv_Contracts.CurrentCell.RowIndex).Value, 1, 70)))
                        Me.CMS_Contracts()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dgv_Trucking_Ins_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Trucking_Ins.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_Trucking_Ins.RowCount Then
            If Me.dgv_Trucking_Ins.RowCount > 0 Then
                Me.cb_CMS.Text = Trim(Me.dgv_Trucking_Ins.Item(0, e.RowIndex).Value)
                Me.CMS_Search_Refresh(Trim(Me.dgv_Trucking_Ins.Item(0, e.RowIndex).Value))
                TabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub chk_Terminal_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Terminal.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Terminal.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Port_Yard,'') as Terminal From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Terminal")) <> Trim(vType) Then
                    vDesc = "Changed Terminal "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Port_Yard = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Terminal")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Del_Click(sender As Object, e As EventArgs) Handles bnt_Del.Click
        If Flag_Modo = 2 Then
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, "Select * From Contract_Cust Where Customer_Number = " & Me.wCMS_Nro.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("CMS account have contract, this can not be remove,..")
                Exit Sub
            End If
            ds.Clear()
            ds = ws.GetDataset(strConnect, "Select * From Bookings_Headline Where (Shipper_Number = " & Me.wCMS_Nro.Text & " or Consignee_no = " & Me.wCMS_Nro.Text & " or FWDR_Number = " & Me.wCMS_Nro.Text & " or Notify_No = " & Me.wCMS_Nro.Text & ")", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("CMS account have booking, this can not be remove, ..")
                Exit Sub
            End If
            ds.Clear()
            ds = ws.GetDataset(strConnect, "Select * From BillOfLoadings Where (Shipper_Number = " & Me.wCMS_Nro.Text & " or Cons_number = " & Me.wCMS_Nro.Text & " or FWDR_Number = " & Me.wCMS_Nro.Text & " or Notify_Number = " & Me.wCMS_Nro.Text & ")", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("CMS account have booking, this can not be remove, ..")
                Exit Sub
            End If

            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox("Are you sure, Do you want to remove this Customer?", style, "Warning")
            If response = MsgBoxResult.Yes Then
                Dim Old_value As String = "Customer: " & Trim(Me.wCMS_Name.Text)
                Dim uid As Integer = 0
                strSQL = "Delete CM_System Where Company_Number = " & Me.wCMS_Nro.Text & " and Company_Name = '" & Trim(Me.wCMS_Name.Text) & "'"
                Dim vDesc As String = ""
                vDesc = "Delete Customer: " & Trim(Me.wCMS_Name.Text)
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                Me.Insert_Changes(Trim(vDesc), Trim(Me.wCMS_Nro.Text), Trim(Me.wCMS_Name.Text), 0, Trim(Mid(Old_value, 1, 70)))

                strSQL = "Delete CMSeMail Where CMSNUM = " & Me.wCMS_Nro.Text
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)

                strSQL = "Delete CMSNotes Where  Company_Number = " & Me.wCMS_Nro.Text
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)

                Audit_Search()
                vDesc = Nothing
                Me.Clear()
                Me.Clear_Address()
                ds_CMS.Clear()
                ds_CMS = ws.GetDataset(md.strConnect, "Select DISTINCT Company_Name, Company_Number 
                                              From CM_System Order by Company_Name", 1) 'Me.CMS_MasterTableAdapter.Fill(Me.Ds_CMS_Master.CMS_Master, Trim(zDesc))
                If ds_CMS.Tables(0).Rows.Count > 0 Then
                    Me.cb_CMS.DataSource = ds_CMS.Tables(0)
                    Me.cb_CMS.DisplayMember = "Company_Name"
                    Me.cb_CMS.ValueMember = "Company_Number"
                    Me.cb_CMS.Refresh()

                    Me.cb_CMS.DroppedDown = True
                End If
            End If
        End If
    End Sub

    Private Sub bnt_Export_Contact_to_Excel_Click(sender As Object, e As EventArgs) Handles bnt_Export_Contact_to_Excel.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.SaveFileDialog1.Filter = "Text Files | *.csv"
        Me.SaveFileDialog1.DefaultExt = "csv"
        Me.SaveFileDialog1.ShowDialog()
        Dim wFileOpen As String = Me.SaveFileDialog1.FileName
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim nCount As Integer = 0
            Dim j As Integer = 0
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT e.CMSNUm,ISNULL(n.COMPANY_NAME, '') AS Company_name,
                                                      rtrim(rtrim(isnull(n.Street,'') + ' ' + Isnull(n.suite,'')) + ', ' + isnull(n.state,'') + ' ' + isnull(n.zip,'') + ' ' + isnull(n.PHONE,'') + ' ' + isnull(n.Country,'')) as Address, 
                                                       ISNULL(e.Contact_Name, '') AS Contact_Name, ISNULL(e.Phone, '') AS Phone, ISNULL(e.email, '') AS email
                                                  FROM dbo.CMSeMail AS e INNER JOIN
                                                       dbo.CM_System AS n ON n.COMPANY_NUMBER = e.CMSNUM AND n.LOCATION_NUMBER = 0
                                                ORDER BY Company_name, Contact_Name, Phone, email", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                nCount = ds.Tables(0).Rows.Count
                Me.txt_Contact_Count.Text = ds.Tables(0).Rows.Count
                Dim nro As Integer = 0
                ' ------ Header Template ----------------------------
                Dim Lin1 As String = "Company_Name" & Chr(44) & "Address" & Chr(44) & "Contact_Name" & Chr(44) & "Phone" & Chr(44) & "email"
                Dim LinDtl As String = ""
                nro = FreeFile()

                Dim FileName As String = wFileOpen
                '------- Delete File of out ---------------------
                If (System.IO.File.Exists(FileName)) Then
                    System.IO.File.Delete("filename")
                End If
                FileOpen(nro, FileName, OpenMode.Binary)
                FilePut(nro, Lin1 & Chr(13) & Chr(10))
                Dim vAddress As String = ""
                Dim strSQL As String = ""
                For j = 0 To ds.Tables(0).Rows.Count - 1

                    LinDtl = Trim(ds.Tables(0).Rows(j).Item("Company_Name")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Address")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Contact_Name")) & Chr(44) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("Phone")) & Chr(44) & Trim(ds.Tables(0).Rows(j).Item("email"))
                    FilePut(nro, LinDtl & Chr(13) & Chr(10))
                    nCount = nCount - 1
                    Me.txt_CMS_Count.Text = nCount
                    Me.txt_CMS_Count.Refresh()
                Next

                FileClose(nro)
            End If
            Me.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub chk_Vendor_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Vendor.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Vendor.Checked = True Then
                vType = "V"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Vendor,'') as Vendor From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Vendor")) <> Trim(vType) Then
                    vDesc = "Changed Vendor"
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Vendor = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Replace(Me.cb_CMS.SelectedItem("Company_Name"), "'", "''")), 0, Trim(ds.Tables(0).Rows(0).Item("Vendor")))
                End If
            End If
            ds = Nothing

        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chk_Customer_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Customer.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Customer.Checked = True Then
                vType = "C"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Customer,'') as Customer From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Customer")) <> Trim(vType) Then
                    vDesc = "Changed Customer"
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Customer = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Replace(Me.cb_CMS.SelectedItem("Company_Name"), "'", "''")), 0, Trim(ds.Tables(0).Rows(0).Item("Customer")))
                End If
            End If
            ds = Nothing

        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#Region "Import"
    Private Sub bnt_Import_SDN_List_Click(sender As Object, e As EventArgs) Handles bnt_Import_SDN_List.Click
        Me.Import_Eq_Excel_File()
    End Sub

    Private Sub Import_Eq_Excel_File()
        'If Flag_Modo = 2 Or Flag_Modo = 3 Then
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim range As Excel.Range
            Dim rCnt As Integer
            Dim cCnt As Integer
            Dim Obj As Object

            Dim nTot_rec As Integer = 0
            Dim vName As String = ""

            Dim vResp As String = ""
            xlApp = New Excel.Application


        Me.OpenFileDialog1.ShowDialog()
            Dim wFileOpen As String = Me.OpenFileDialog1.FileName
            xlWorkBook = xlApp.Workbooks.Open(Trim(wFileOpen))
        eResp = InputBox("Name:")
        xlWorkSheet = xlWorkBook.Worksheets(eResp)

        Dim vCont As String = ""
            Dim ds As New DataSet
            range = xlWorkSheet.UsedRange

        nTot_rec = Format(range.Rows.Count, "###,###,###")
        Me.nTot_SDN_Records.Text = Format(nTot_rec, "###,###,###.##")
        If nTot_rec > 1 Then
            For rCnt = 2 To range.Rows.Count
                Obj = CType(range.Cells(rCnt, 6), Excel.Range)
                vName = Obj.value

                strSQL = "Select Company_name From SDN_List Where  Company_name = '" & Trim(Replace(vName, "'", "''")) & "'"
                ds.Clear()
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count = 0 Then
                    'Me.dgv_SDN_List.Rows.Add(Trim(vName))
                    strSQL = "Insert Into SDN_List (Company_name, Created_By, Created_On) Values ('" & Trim(Mid(Replace(vName, "'", "''"), 1, 50)) & "','" & Trim(Mid(System.Environment.UserName, 1, 50)) & "','" & System.DateTime.Today.ToShortDateString & "')"
                    eResp = ws.ExecSQL(strConnect, strSQL)
                End If
                nTot_rec = nTot_rec - 1
                Me.nTot_SDN_Records.Text = Format(nTot_rec, "###,###,###.##")
                Me.nTot_SDN_Records.Refresh()
            Next

            MsgBox("The End,..")

        Else
            MsgBox("File is empty,...")
            End If
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            Cursor.Current = System.Windows.Forms.Cursors.Default
        'End If
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub dgv_CMS_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_CMS.CellClick
        If e.RowIndex > -1 And e.RowIndex < Me.dgv_CMS.RowCount Then
            Me.txt_SDN_List_Search.Text = Trim(Me.dgv_CMS.Item(0, e.RowIndex).Value)
            Me.txt_SDN_List_Search.Refresh()
            ds_SDN_List.Clear()
            strSQL = "SELECT Company_Name as Name FROM dbo.SDN_List Where Company_Name Like '%" & Trim(Mid(Me.txt_SDN_List_Search.Text, 1, 7)) & "%'  ORDER BY Company_Name"
            ds_SDN_List = ws.GetDataset(strConnect, strSQL, 1)
            If ds_SDN_List.Tables(0).Rows.Count > 0 Then
                Me.dgv_SDN_List.DataSource = ds_SDN_List.Tables(0)
                Me.dgv_SDN_List.Columns("Name").Width = 280
                Me.dgv_SDN_List.Columns("Name").HeaderText = "Company"
                Me.dgv_SDN_List.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Me.dgv_SDN_List.Refresh()
            End If
        End If
    End Sub

    Private Sub bnt_SDN_List_Search_Click(sender As Object, e As EventArgs) Handles bnt_SDN_List_Search.Click
        If Len(Trim(Me.txt_SDN_List_Search.Text)) > 0 Then
            ds_SDN_List.Clear()
            strSQL = "SELECT Company_Name as Name FROM dbo.SDN_List Where ltrim(rtrim(Company_name)) Like '%" & Trim(Me.txt_SDN_List_Search.Text) & "%'  ORDER BY Company_Name"
            ds_SDN_List = ws.GetDataset(strConnect, strSQL, 1)
            If ds_SDN_List.Tables(0).Rows.Count > 0 Then
                Me.dgv_SDN_List.DataSource = ds_SDN_List.Tables(0)
                Me.dgv_SDN_List.Columns("Name").Width = 280
                Me.dgv_SDN_List.Columns("Name").HeaderText = "Company"
                Me.dgv_SDN_List.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Me.dgv_SDN_List.Refresh()
            End If
        End If
    End Sub

    Private Sub dgv_CMS_SelectionChanged(sender As Object, e As EventArgs) Handles dgv_CMS.SelectionChanged
        If dgv_CMS.Focus = True Then
            If Me.dgv_CMS.CurrentCell.RowIndex > -1 And Me.dgv_CMS.CurrentCell.RowIndex < Me.dgv_CMS.RowCount Then
                Me.txt_SDN_List_Search.Text = Trim(Me.dgv_CMS.Item(0, Me.dgv_CMS.CurrentCell.RowIndex).Value)
                Me.txt_SDN_List_Search.Refresh()
                ds_SDN_List.Clear()
                strSQL = "SELECT Company_Name as Name FROM dbo.SDN_List Where Company_Name Like '%" & Trim(Mid(Me.txt_SDN_List_Search.Text, 1, 7)) & "%'  ORDER BY Company_Name"
                ds_SDN_List = ws.GetDataset(strConnect, strSQL, 1)
                If ds_SDN_List.Tables(0).Rows.Count > 0 Then
                    Me.dgv_SDN_List.DataSource = ds_SDN_List.Tables(0)
                    Me.dgv_SDN_List.Columns("Name").Width = 280
                    Me.dgv_SDN_List.Columns("Name").HeaderText = "Company"
                    Me.dgv_SDN_List.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Me.dgv_SDN_List.Refresh()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_SDN_List_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_SDN_List.CellDoubleClick
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim vmsg As String = "Do you want to link these companies " & Trim(Me.dgv_CMS.Item(0, dgv_CMS.CurrentCell.RowIndex).Value) & " with " & Trim(dgv_SDN_List.Item(0, e.RowIndex).Value) & "?"
        Dim response = MsgBox(vmsg, MsgBoxStyle.YesNo, "Warnning")
        If response = MsgBoxResult.No Then
            Exit Sub
        End If
        strSQL = "Update CM_System Set SDN_List = '" & Trim(dgv_SDN_List.Item(0, e.RowIndex).Value) & "' Where ltrim(rtrim(Company_Name)) = '" & Trim(Me.dgv_CMS.Item(0, dgv_CMS.CurrentCell.RowIndex).Value) & "'"
        eResp = ws.ExecSQL(strConnect, strSQL)

        strSQL = "Select * From
                        (SELECT ROW_NUMBER() OVER (ORDER BY name) AS row_number, Name, sdn_List From
                        (SELECT Company_Name as Name, isnull(SDN_List,'') as SDN_List FROM CM_System) as T1) as T2
                        Where ltrim(rtrim(SDN_List)) <> ''
                         ORDER BY Name"
        Dim ind As Integer = 0
        Dim jj As Integer = 0
        Dim ds_S As New DataSet
        ds_S = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_S.Tables(0).Rows.Count > 0 Then
            ind = 0
            For jj = 0 To ds_S.Tables(0).Rows.Count - 1
                ind = ds_S.Tables(0).Rows(jj).Item("row_number") - 1
                Me.dgv_CMS.Rows(ind).Cells(0).Style.BackColor = Color.Black
                Me.dgv_CMS.Rows(ind).Cells(0).Style.ForeColor = Color.White
            Next
        End If
        ds_S.Clear()
        Me.Refresh()
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txt_CMS_vs_SDN_TextChanged(sender As Object, e As EventArgs) Handles txt_CMS_vs_SDN.TextChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'If Len(Trim(Me.txt_CMS_vs_SDN.Text)) > 0 Then
        ds_CMS_SDN.Clear()
        'strSQL = "SELECT Company_Name as Name FROM CM_System where isnull(SDN_list,'') = '' ORDER BY Company_Name"
        strSQL = "SELECT Company_Name as Name FROM CM_System Where ltrim(rtrim(Company_name)) Like '" & Trim(Me.txt_CMS_vs_SDN.Text) & "%' ORDER BY Company_Name"
        ds_CMS_SDN = ws.GetDataset(strConnect, strSQL, 1)
        If ds_CMS_SDN.Tables(0).Rows.Count > 0 Then
            Me.dgv_CMS.DataSource = ds_CMS_SDN.Tables(0)
            Me.dgv_CMS.Columns("Name").Width = 280
            Me.dgv_CMS.Columns("Name").HeaderText = "Company"
            Me.dgv_CMS.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Me.dgv_CMS.Refresh()
        End If
        'End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txt_Terminal_Notes_Comments_Leave(sender As Object, e As EventArgs) Handles txt_Terminal_Notes_Comments.Leave
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not IsNothing(dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value) Then
            strSQL = "Select isnull(Notes_Comments,'') as Notes_Comments From Booking_Terminal_Notes_Comments Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                strSQL = "Update Booking_Terminal_Notes_Comments Set Notes_Comments = '" & Trim(Replace(Me.txt_Terminal_Notes_Comments.Text, "'", "''")) & "', Update_By = '" & System.Environment.UserName & "', Update_ON = '" & System.DateTime.Now & "'  Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
                eResp = ws.ExecSQL(strConnect, strSQL)
            Else
                If Len(Trim(Me.txt_Terminal_Notes_Comments.Text)) > 0 Then
                    strSQL = "Insert Into Booking_Terminal_Notes_Comments (Terminal, Notes_Comments, Delivery_Notes, Created_By, Created_ON) Values (" & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value & ",'" & Trim(Replace(Me.txt_Terminal_Notes_Comments.Text, "'", "''")) & "','','" & System.Environment.UserName & "','" & System.DateTime.Now & "')"
                    eResp = ws.ExecSQL(strConnect, strSQL)
                End If
            End If
            ds = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Txt_Delivery_Terminal_Leave(sender As Object, e As EventArgs) Handles Txt_Delivery_Terminal.Leave
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not IsNothing(dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value) Then
            strSQL = "Select isnull(Delivery_Notes,'') as Delivery_Notes From Booking_Terminal_Notes_Comments Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                strSQL = "Update Booking_Terminal_Notes_Comments Set Delivery_Notes = '" & Trim(Replace(Me.Txt_Delivery_Terminal.Text, "'", "''")) & "', Update_By = '" & System.Environment.UserName & "', Update_ON = '" & System.DateTime.Now & "' Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
                eResp = ws.ExecSQL(strConnect, strSQL)
            Else
                If Len(Trim(Me.Txt_Delivery_Terminal.Text)) > 0 Then
                    strSQL = "Insert Into Booking_Terminal_Notes_Comments (Terminal, Notes_Comments, Delivery_Notes,Created_By, Created_On) Values (" & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value & ",'','" & Trim(Replace(Me.Txt_Delivery_Terminal.Text, "'", "''")) & "','" & System.Environment.UserName & "','" & System.DateTime.Now & "')"
                    eResp = ws.ExecSQL(strConnect, strSQL)
                End If
            End If
            ds = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub dgv_Terminal_Click(sender As Object, e As EventArgs) Handles dgv_Terminal.Click
        Dim ds_Notes_Comments As New DataSet
        strSQL = "Select isnull(Notes_Comments,'') as Notes_Comments, isNull(Delivery_Notes,'') as Delivery_Notes From Booking_Terminal_Notes_Comments Where Terminal = " & dgv_Terminal.Item(1, Me.dgv_Terminal.CurrentCell.RowIndex).Value
        ds_Notes_Comments = ws.GetDataset(strConnect, strSQL, 1)
        If ds_Notes_Comments.Tables(0).Rows.Count > 0 Then
            Me.Txt_Delivery_Terminal.Text = md.FormatStrLine(Trim(ds_Notes_Comments.Tables(0).Rows(0).Item("Delivery_Notes")))
            Me.txt_Terminal_Notes_Comments.Text = md.FormatStrLine(Trim(ds_Notes_Comments.Tables(0).Rows(0).Item("Notes_Comments")))
        Else
            Me.txt_Terminal_Notes_Comments.Clear()
            Me.Txt_Delivery_Terminal.Clear()
        End If
    End Sub

    Private Sub chk_Eq_Owner_Leased_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Eq_Owner_Leased.CheckedChanged
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vType As String = "N"
            If Me.chk_Eq_Owner_Leased.Checked = True Then
                vType = "Y"
            End If
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(Eq_Owner_Leased,'') as Eq_Owner_Leased From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Eq_Owner_Leased")) <> Trim(vType) Then
                    vDesc = "Changed Equipment Owner/ Leased "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Eq_Owner_Leased = '" & Trim(vType) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue))
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Eq_Owner_Leased")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

#End Region

    Public Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without Autocompleting
        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                Return
        End Select

        'Get the Typed Text and Find it in the list
        sTypedText = cbo.Text
        If Len(Trim(sAppendText)) < 5 Then
            ds_CMS.Clear()
            strSQL = "Select DISTINCT top 50 Company_Name, Company_Number From CM_System where Company_Name Like '" & Trim(sTypedText) & "%' Order By Company_Name"
            ds_CMS = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_CMS.Tables(0).Rows.Count > 0 Then
                Me.cb_CMS.DataSource = ds_CMS.Tables(0)
                Me.cb_CMS.DisplayMember = "Company_Name"
                Me.cb_CMS.ValueMember = "Company_Number"
                Dim wText As String = Me.cb_CMS.Text
            End If
        End If
        cbo.Refresh()
        iFoundIndex = cbo.FindString(sTypedText)

        'If we found the Typed Text in the list then Autocomplete
        If iFoundIndex >= 0 Then

            'Get the Item from the list (Return Type depends if Datasource was bound 
            ' or List Created)
            oFoundItem = cbo.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo 
            ' was Data bound
            sFoundText = cbo.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case
            sAppendText = sFoundText.Substring(sTypedText.Length)
            cbo.Text = sTypedText & sAppendText

            'Select the Appended Text
            cbo.SelectionStart = sTypedText.Length
            cbo.SelectionLength = sAppendText.Length
        End If
    End Sub

    Public Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
        Dim iFoundIndex As Integer
        iFoundIndex = cbo.FindStringExact(cbo.Text)
        cbo.SelectedIndex = iFoundIndex
    End Sub

    Private Sub cb_CMS_LostFocus(sender As Object, e As EventArgs) Handles cb_CMS.LostFocus
        If Not Me.cb_CMS.SelectedValue = Nothing Then
            ' ------- Change done on 08/31/2022
            If Flag_Modo = 1 Then
                Me.CMS_Refresh()
            End If
        End If
    End Sub
    Private Sub cb_CMS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_CMS.KeyPress
        'e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
        'System.Threading.Thread.Sleep(100)
        'cb_CMS.DroppedDown = True
        If (Char.IsControl(e.KeyChar)) Then Return
        Dim Str As String = cb_CMS.Text.Substring(0, cb_CMS.SelectionStart) + e.KeyChar

        ds_CMS.Clear()
        strSQL = "Select DISTINCT top 50 Company_Name, Company_Number From CM_System where Company_Name Like '" & Trim(Replace(Str, "'", "''")) & "%' Order By Company_Name"
        ds_CMS = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_CMS.Tables(0).Rows.Count > 0 Then
            Me.cb_CMS.DataSource = ds_CMS.Tables(0)
            Me.cb_CMS.DisplayMember = "Company_Name"
            Me.cb_CMS.ValueMember = "Company_Number"
            Dim wText As String = Me.cb_CMS.Text
        Else
            strSQL = "Select DISTINCT top 50 Company_Name, Company_Number From CM_System where Company_Number = 999999 Order By Company_Name"
            ds_CMS = ws.GetDataset(md.strConnect, strSQL, 1)
            Me.cb_CMS.DataSource = ds_CMS.Tables(0)
            Me.cb_CMS.DisplayMember = "Company_Name"
            Me.cb_CMS.ValueMember = "Company_Number"
            Dim wText As String = Me.cb_CMS.Text
        End If

        Dim Index As Integer = cb_CMS.FindStringExact(Str)
        If Index = -1 Then
            Index = cb_CMS.FindString(Str)
        End If
        Me.cb_CMS.SelectedIndex = Index
        Me.cb_CMS.SelectionStart = Str.Length
        Me.cb_CMS.SelectionLength = Me.cb_CMS.Text.Length - Me.cb_CMS.SelectionStart
        e.Handled = True
    End Sub
    Private Sub cb_Country_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_Country.KeyPress
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
    End Sub

    Private Sub bnt_Contract_New_Click(sender As Object, e As EventArgs) Handles bnt_Contract_New.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim Contract_Services As New Contract_Services
        Contract_Services.Top = Me.Top + 50
        Contract_Services.Left = Me.Left + 20
        Contract_Services.cb_CMS.Text = Me.wCMS_Name.Text
        Contract_Services.txt_Cust.Text = Me.wCMS_Nro.Text
        Contract_Services.bnt_New.PerformClick()
        Contract_Services.bnt_New.Refresh()
        Contract_Services.ShowDialog(Me)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub dtp_Trucker_Insure_Date_Validating(sender As Object, e As CancelEventArgs) Handles dtp_Trucker_Insure_Date.Validating
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isnull(Trucker_Ins_Date,'') as Trucker_Ins_Date From CM_System CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Trucker_Ins_Date")) <> Trim(Me.dtp_Trucker_Insure_Date.Value) Then
                    vDesc = "Changed Trucker Ins. Exp. Date "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set Trucker_Ins_Date = '" & Trim(Me.dtp_Trucker_Insure_Date.Value.ToShortDateString) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Trucker_Ins_Date")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txt_Warehouse_Clauses_Leave(sender As Object, e As EventArgs) Handles txt_Warehouse_Clauses.Leave
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'If Flag_Modo = 2 Then
        Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isnull(Comment,'') as Comment From DRClauses Where Warehouse = " & Me.dgv_Warehouse.Item(1, Me.dgv_Warehouse.CurrentCell.RowIndex).Value, 1)
            If ds.Tables(0).Rows.Count > 0 Then
            vDesc = "Changed Warehouse Comments "
            strSQL = "Update DRClauses Set Comment = '" & Trim(Replace(Me.txt_Warehouse_Clauses.Text, "'", "''")) & "' Where Warehouse = " & Me.dgv_Warehouse.Item(1, Me.dgv_Warehouse.CurrentCell.RowIndex).Value
            md.eResp = ws.ExecSQL(md.strConnect, strSQL)
            Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, "")
            Else
                vDesc = "New Warehouse Comments "
                strSQL = "Insert Into DRClauses  (Warehouse, Comment, Created_By, Created_On) Values (" & Me.dgv_Warehouse.Item(1, Me.dgv_Warehouse.CurrentCell.RowIndex).Value & ",'" & Trim(Me.txt_Warehouse_Clauses.Text) & "','" & Trim(System.Environment.UserDomainName) & "','" & System.DateTime.Today.ToShortDateString & "')"
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, "")
            End If
            ds = Nothing
            'Audit_Search()
            'End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_AR_Collectors_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_AR_Collectors.KeyPress
        If (Char.IsControl(e.KeyChar)) Then Return
        Dim Str As String = cb_CMS.Text.Substring(0, cb_CMS.SelectionStart) + e.KeyChar

        Dim Index As Integer = cb_AR_Collectors.FindStringExact(Str)
        If Index = -1 Then
            Index = cb_AR_Collectors.FindString(Str)
        End If
        Me.cb_AR_Collectors.SelectedIndex = Index
        Me.cb_AR_Collectors.SelectionStart = Str.Length
        Me.cb_AR_Collectors.SelectionLength = Me.cb_AR_Collectors.Text.Length - Me.cb_AR_Collectors.SelectionStart
        e.Handled = True
    End Sub

    Private Sub cb_AR_Collectors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_AR_Collectors.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(AR_Collectors,'') as Collector From CM_System Where Company_Number = " & Me.cb_CMS.SelectedValue & " and Location_Number = " & Me.wLoc.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Collector")) <> Trim(Me.cb_AR_Collectors.SelectedValue) Then
                    vDesc = "Changed AR Collector "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update CM_System Set AR_Collectors = '" & Trim(Me.cb_AR_Collectors.SelectedValue) & "' Where Company_Number = " & Trim(Me.cb_CMS.SelectedValue) & " and Location_Number = " & Me.wLoc.Text)
                    Me.Insert_Changes(Trim(vDesc), Me.cb_CMS.SelectedValue, Trim(Me.cb_CMS.SelectedItem("Company_Name")), 0, Trim(ds.Tables(0).Rows(0).Item("Collector")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_AR_Collectors_Display_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_AR_Collectors_Display.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.dgv_AR_Collectors.Rows.Clear()
            Me.dgv_AR_Collectors.Refresh()
            Me.dgv_AR_Collectors.Focus()
            Me.wCount_AR_Collectors.Clear()
            Me.Refresh()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT DISTINCT COMPANY_NAME, COMPANY_NUMBER FROM CM_System WHERE ( IsNUll(AR_Collectors,'') = '" & Trim(Me.cb_AR_Collectors_Display.SelectedValue) & "') ORDER BY COMPANY_NAME"
        Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    Me.dgv_AR_Collectors.Rows.Add(Trim(ds.Tables(0).Rows(j).Item("Company_Name")), ds.Tables(0).Rows(j).Item("Company_Number"), Trim(md.CMS_Address(ds.Tables(0).Rows(j).Item("Company_Number"), 0)))
                Next
                Me.wCount_AR_Collectors.Text = ds.Tables(0).Rows.Count
            End If
            ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Sub

    Private Sub dgv_Documents_GDZ_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Documents_GDZ.CellClick
        Dim vPath As String = "c:\GDZ\Images\" & Trim(Me.dgv_Documents_GDZ.Item(0, e.RowIndex).Value)
        Process.Start("IExplore.exe", vPath)
    End Sub

#Region "Documents"

    Private Sub initEnvironment()
        oEnvironment = New Environment
        oEnvironment.FilesLocation = Application.StartupPath & Trim(System.Configuration.ConfigurationSettings.AppSettings("DocumentsLocation"))
        oEnvironment.Scanner = System.Configuration.ConfigurationSettings.AppSettings("Scanner")
        oEnvironment.TwainKey = System.Configuration.ConfigurationSettings.AppSettings("TwainKey")

        ' ------- Change done on 9/4/2018
        oEnvironment.PhotoCamera = System.Configuration.ConfigurationSettings.AppSettings("PhotoCamera")

    End Sub

    Private Sub initDocument()
        oDocument = New Document
    End Sub

    Private Sub takePicture()
        Try
            oDocument.Name = oEnvironment.FilesLocation + "Img_" + DateTime.Now.ToString("MMddyyyy_HHmmss") + ".jpg"
            oDocument.Image = ucPhotoCamera.cameraImage
            oDocument.Image.Save(oDocument.Name, Imaging.ImageFormat.Jpeg)  'Save picture in local folder
            '
            addItemToDocumentsDGV()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub scanDocument()
        Try
            oDocument.Name = oEnvironment.FilesLocation + "Img_" + DateTime.Now.ToString("MMddyyyy_HHmmss") + ".jpg"
            oDocument.Image = ucScanner.scannerImage
            oDocument.Image.Save(oDocument.Name, Imaging.ImageFormat.Jpeg)  'Save picture in local folder
            '
            addItemToDocumentsDGV()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub addItemToDocumentsDGV()
        If Not oDocument.imgExt.Contains(Path.GetExtension(oDocument.Name).ToUpper) Then
            oDocument.Image = (New Document).getExtensionIcon(Path.GetExtension(oDocument.Name).ToUpper)
        End If
        'dgvAttachments.Rows.Add(0, Path.GetFileName(oDocument.Name), Now(), System.Environment.UserName, oDocument.Image)
        'dgvAttachments.Rows(dgvAttachments.Rows.Count - 1).Cells(4).ToolTipText = "Dbl-Click for opening the document"
        'dgvAttachments.Rows(dgvAttachments.Rows.Count - 1).Cells(5).ToolTipText = "Remove document"

        dgvAttachments.Rows.Insert(0, 0, Path.GetFileName(oDocument.Name), Now(), System.Environment.UserName, oDocument.Image)
        dgvAttachments.Rows(0).Cells(4).ToolTipText = "Dbl-Click for opening the document"
        dgvAttachments.Rows(0).Cells(5).ToolTipText = "Remove document"
    End Sub

    ' Download/Create documents to/in local folder
    Private Sub downloadDocuments(ByVal dt As DataTable)
        Dim fileInLocal As FileInfo
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim ImgDoc As Byte() = DirectCast(row(2), Byte())
                Using ms As MemoryStream = New MemoryStream(ImgDoc, 0, ImgDoc.Length)
                    fileInLocal = New FileInfo(oEnvironment.FilesLocation + row(1))
                    If Not fileInLocal.Exists = True Then
                        Dim file As New FileStream(oEnvironment.FilesLocation + row(1), FileMode.Create, FileAccess.Write)
                        ms.WriteTo(file)
                        file.Close()
                        file.Dispose()
                        ms.Close()
                        ms.Dispose()
                    End If
                End Using
            Next
        End If
    End Sub

    ' Get Documents from database
    Private Sub getDocuments()
        Dim dt As DataTable = DocumentsManager.Eq_Documents.Get_Eq_Documents(oDocument.Number)
        If Not dt Is Nothing AndAlso dt.Rows.Count >= 1 Then

            ' Download documents in local folder...
            downloadDocuments(dt)

            ' Fill the dgv with documents...
            Me.dgvAttachments.Rows.Clear()
            For Each row As DataRow In dt.Rows
                dgvAttachments.Rows.Add(row("uid"), row("File Name"), row("Added On"), row("Added By")) '(row(0), row(1), row(3), row(4))
            Next

            ' Setting up Type column...
            For Each row As DataGridViewRow In dgvAttachments.Rows
                If Not oDocument.imgExt.Contains(Path.GetExtension(row.Cells(1).Value).ToUpper) Then
                    row.Cells(4).Value = (New Document).getExtensionIcon(Path.GetExtension(row.Cells(1).Value).ToUpper)
                Else
                    row.Cells(4).Value = Image.FromFile(oEnvironment.FilesLocation + row.Cells(1).Value)
                End If
                row.Cells(4).ToolTipText = "Dbl-Click for opening the document"
                row.Cells(5).ToolTipText = "Remove document"
            Next
        End If
    End Sub

    Private Sub RemoveDocument(ByVal uid As Integer)
        Dim dt As DataTable = DocumentsManager.Eq_Documents.Remove_Eq_Document(uid)
        If Not dt Is Nothing AndAlso dt.Rows.Count >= 1 Then
            MessageBox.Show(dt.Rows(0)(0), "Document", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub dgvAttachments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAttachments.CellClick
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case 5 'Click on Remove
                    If MessageBox.Show("Are you sure you want to delete this document?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                        If dgvAttachments.Rows(e.RowIndex).Cells(0).Value <> 0 Then
                            RemoveDocument(dgvAttachments.Rows(e.RowIndex).Cells(0).Value)
                        End If
                        dgvAttachments.Rows.RemoveAt(e.RowIndex)
                    End If
            End Select
        End If
    End Sub

    Private Sub dgvAttachments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAttachments.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                oDocument.Name = oEnvironment.FilesLocation + System.IO.Path.Combine(dgvAttachments.Rows(e.RowIndex).Cells(1).Value)
                Select Case e.ColumnIndex
                    Case 4
                        'Document type
                        System.Diagnostics.Process.Start(oDocument.Name)
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub dgvAttachments_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dgvAttachments.DragEnter
        If Flag_Modo = 1 Then
            Exit Sub
        End If
        '
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub dgvAttachments_DragDrop(sender As Object, e As DragEventArgs) Handles dgvAttachments.DragDrop

        If Flag_Modo = 1 Then
            Exit Sub
        End If

        Dim img As Image = Nothing
        Dim fileNames As String() = Nothing

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) = True Then

                ' ----- Drag&Drop from Windows Explorer...

                fileNames = CType(e.Data.GetData(DataFormats.FileDrop), String())
                For Each fileName As String In fileNames
                    oDocument.Name = fileName
                    If oDocument.imgExt.Contains(Path.GetExtension(oDocument.Name).ToUpper) Then
                        oDocument.Image = Image.FromFile(oDocument.Name)
                        oDocument.Image.Save(oEnvironment.FilesLocation + Path.GetFileName(oDocument.Name), Imaging.ImageFormat.Jpeg)
                    Else
                        System.IO.File.Copy(oDocument.Name, oEnvironment.FilesLocation + Path.GetFileName(oDocument.Name), True)
                    End If
                    addItemToDocumentsDGV()
                Next

            ElseIf e.Data.GetDataPresent("FileGroupDescriptor") Then

                ' ----- Drag&Drop from Outlook...

                Dim theStream As Stream = CType(e.Data.GetData("FileGroupDescriptor"), Stream)
                Dim fileGroupDescriptor As Byte() = New Byte(512) {}
                theStream.Read(fileGroupDescriptor, 0, 512)
                Dim i As Integer = 76
                Dim fileName As StringBuilder = New StringBuilder("")
                While fileGroupDescriptor(i) <> 0
                    fileName.Append(Convert.ToChar(fileGroupDescriptor(i)))
                    i += 1
                End While
                theStream.Close()

                oDocument.Name = oEnvironment.FilesLocation & fileName.ToString()

                Dim ms As MemoryStream = CType(e.Data.GetData("FileContents", True), MemoryStream)
                Dim fileBytes As Byte() = New Byte(ms.Length - 1) {}
                ms.Position = 0
                ms.Read(fileBytes, 0, CInt(ms.Length))
                Dim fs As FileStream = New FileStream(oDocument.Name, FileMode.Create)
                fs.Write(fileBytes, 0, CInt(fileBytes.Length))
                fs.Close()

                If oDocument.imgExt.Contains(Path.GetExtension(oDocument.Name).ToUpper) Then
                    oDocument.Image = Image.FromFile(oDocument.Name)
                End If

                Dim tempFile As FileInfo = New FileInfo(oDocument.Name)
                If tempFile.Exists = True Then
                    addItemToDocumentsDGV()
                Else
                    Trace.WriteLine("File was not created!")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Public Sub uploadDocuments()
        Dim xmlData As String = PrepareData()
        If xmlData.Length < 10 Then
            MessageBox.Show("Nothing to save.", "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim dt As DataTable = DocumentsManager.Eq_Documents.Set_Eq_Documents(xmlData)
            If Not dt Is Nothing AndAlso dt.Rows.Count >= 1 Then
                MessageBox.Show(dt.Rows(0)(0), "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information)
                getDocuments()
            End If
        End If
    End Sub

    Private Function PrepareData() As String
        Dim rootNodeName As String = "root"
        Dim itemNodeName As String = "node"
        Dim prepXML As XElement
        prepXML = New XElement(rootNodeName)
        If dgvAttachments.Rows.Count > 0 Then
            For Each row As DataGridViewRow In dgvAttachments.Rows
                If row.Cells(0).Value = 0 Then
                    Dim itemElement As XElement = New XElement(itemNodeName)
                    itemElement.SetAttributeValue("Eq_number", oDocument.Number)
                    itemElement.SetAttributeValue("FileName", row.Cells(1).Value)
                    itemElement.SetAttributeValue("Type_Attached", "I")
                    itemElement.SetAttributeValue("Created_By", System.Environment.UserName)
                    itemElement.SetAttributeValue("Created_On", Now())
                    itemElement.SetAttributeValue("Delete", "0")
                    Dim fileInBytes As Byte() = System.IO.File.ReadAllBytes(oEnvironment.FilesLocation & row.Cells(1).Value)
                    Dim fileInBase64 As String = Convert.ToBase64String(fileInBytes)
                    itemElement.SetAttributeValue("Doc_Img", fileInBase64)
                    prepXML.Add(itemElement)
                End If
            Next
        End If
        Return prepXML.ToString()
    End Function

    Private Sub initAttachmentsDGV()

        With dgvAttachments
            ' General...
            .AutoGenerateColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .AllowUserToAddRows = False
            .ColumnCount = 4
            .RowTemplate.Height = 50
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

            ' UID column (hidden)
            .Columns(0).DataPropertyName = "uid"
            .Columns(0).Visible = False

            ' File Name column...
            .Columns(1).DataPropertyName = "File Name"
            .Columns(1).HeaderText = "Document(s)"
            .Columns(1).DefaultCellStyle.Font = New Font("Arial", 12.0F, GraphicsUnit.Pixel)
            .Columns(1).DefaultCellStyle.Padding = New Padding(0, 5, 0, 0)

            ' Added On column...
            .Columns(2).DataPropertyName = "Added On"
            .Columns(2).HeaderText = "Added On"
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).DefaultCellStyle.Font = New Font("Arial", 12.0F, GraphicsUnit.Pixel)
            .Columns(2).DefaultCellStyle.Padding = New Padding(0, 5, 0, 0)
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' Added By column...
            .Columns(3).DataPropertyName = "Added By"
            .Columns(3).HeaderText = "Added By"
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).DefaultCellStyle.Font = New Font("Arial", 12.0F, GraphicsUnit.Pixel)
            .Columns(3).DefaultCellStyle.Padding = New Padding(0, 5, 0, 0)
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' Type column...
            Dim dgvIC As New DataGridViewImageColumn
            dgvIC.HeaderText = "Type"
            dgvIC.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvIC.ImageLayout = DataGridViewImageCellLayout.Zoom
            dgvIC.DefaultCellStyle.Padding = New Padding(0, 5, 0, 5)
            .Columns.Add(dgvIC)
            .Columns(4).Width = 70

            ' Remove button...
            Dim dgvIC2 As New DataGridViewImageColumn
            dgvIC2.HeaderText = ""
            dgvIC2.ImageLayout = DataGridViewImageCellLayout.Zoom
            dgvIC2.Image = My.Resources.rubbish_bin_green
            .Columns.Add(dgvIC2)
            .Columns(5).Width = 30
        End With

    End Sub

    Private Sub btnSaveDocuments_Click(sender As Object, e As EventArgs) Handles btnSaveDocuments.Click
        uploadDocuments()
    End Sub

    Private Sub setDocumentsViewMode()
        Me.ucPhotoCamera.setDisabledMode()
        Me.ucScanner.setDisabledMode()
        Me.dgvAttachments.Top = 7
        Me.dgvAttachments.Height = 700
        Me.dgvAttachments.Columns(5).Visible = False
        Me.btnSaveDocuments.Visible = False
    End Sub

    Private Sub setDocumentsReadyMode()

        If Me.ucPhotoCamera.cameraFound Then
            Me.ucPhotoCamera.setReadyMode()
            Me.dgvAttachments.Top = 439
            Me.dgvAttachments.Height = 281
        Else
            Me.ucPhotoCamera.setDisabledMode()
            Me.dgvAttachments.Top = 7
            Me.dgvAttachments.Height = 707
        End If

        If Me.ucScanner.scannerFound Then
            Me.ucScanner.setReadyMode()
        Else
            Me.ucScanner.setDisabledMode()
        End If
        Me.AllowDrop = True
        Me.dgvAttachments.Columns(5).Visible = True
        Me.btnSaveDocuments.Visible = True
    End Sub

#End Region
End Class