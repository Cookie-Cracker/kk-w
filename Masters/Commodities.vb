Imports System.ComponentModel

Public Class Commodities
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1

    Private Sub Commodities_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, Me.Name)) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        If Trim(md.Progran_ReadOnly(UserCode, Me.Name)) = "Y" Then
            Me.bnt_New.Visible = False
            Me.bnt_Save.Visible = False
        End If
        Me.Refresh_Comdty()
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        md.Insert_User_Log("Load Commodities", md.UserName)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Commodity_Clear()
        Me.wID.Clear()
        Me.wCommodity.Clear()
        Me.dgv_Audit.Rows.Clear()
    End Sub

    Private Sub Refresh_Comdty()
        Me.Commodity_Clear()
        DsCommodity = ws.SP(md.strConnect, "Commodity_Master")
        If DsCommodity.Tables(0).Rows.Count > 0 Then
            cb_Commodity.DataSource = DsCommodity.Tables(0)
            cb_Commodity.DisplayMember = "Name"
            cb_Commodity.ValueMember = "ID"
            Me.wID.Text = cb_Commodity.SelectedValue
            Me.wCommodity.Text = cb_Commodity.SelectedText
            Me.cb_Commodity.SelectedValue = 0
            Me.cb_Commodity.Refresh()
            Me.wID.Text = Me.cb_Commodity.SelectedValue
            Me.wCommodity.Text = Trim(Me.cb_Commodity.SelectedItem("Name"))
        End If
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        Select Case Flag_Modo
            Case 1
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = False
                Me.bnt_New.Enabled = True
                Me.bnt_Edit.Enabled = True

                Me.wCommodity.ReadOnly = True
                Me.wCommodity.BackColor = Color.LightBlue
            Case 2
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = True
                Me.wCommodity.ReadOnly = False
                Me.wCommodity.BackColor = Color.White
            Case 3
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = True
                Me.bnt_Edit.Enabled = False
                Me.wCommodity.ReadOnly = False
                Me.wCommodity.BackColor = Color.White
        End Select
    End Sub

    Private Sub cb_Commodity_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Commodity.SelectionChangeCommitted
        Me.Commodity_Clear()
        Me.wID.Text = Me.cb_Commodity.SelectedValue
        Me.wCommodity.Text = cb_Commodity.SelectedItem("Name")
    End Sub

    Private Sub Commodities_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        md.Insert_User_Log("Closing Commodities", md.UserName)
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Len(Trim(Me.wCommodity.Text)) = 0 Then
            MsgBox("Commodity field is empty,....")
            Me.wCommodity.Focus()
            Exit Sub
        End If
        Dim nID As Integer = 1
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 ID From Comdty WHERE id < 999 Order by ID Desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nID = ds.Tables(0).Rows(0).Item("ID") + 1
        End If
        strSQL = "Insert Into Comdty (ID, Name) Values (" & nID & ",'" & Trim(Me.wCommodity.Text) & "')"
        eResp = ExecSQL(strConnect, strSQL)
        Me.Refresh_Comdty()
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Me.Modo_Edit_ADD_Read_Only(2)
        Flag_Modo = 2
        Me.wCommodity.Focus()
    End Sub

    Private Sub Insert_Changes(ByVal Desc As String, ByVal Comdty_ID As Integer, ByVal Old_value As String)
        Dim strSQL As String = "Insert Into Comdty_Journal (
                                               Created_Date, 
                                               Created_By, 
                                               Description, 
                                               Comdty_ID,
                                               Old_value
                                                     ) Values ('" &
                                  System.DateTime.Now & "','" &
                                  Trim(System.Environment.UserName) & "','" &
                                  Trim(Mid(Desc, 1, 100)) & "'," & Comdty_ID & ",'" &
                                  Trim(Mid(Old_value, 1, 100)) & "')"
        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
        If md.eResp <> "Success" Then
            MsgBox(Trim(md.eResp))
        End If
        strSQL = Nothing
    End Sub

    Private Sub wCommodity_KeyDown(sender As Object, e As KeyEventArgs) Handles wCommodity.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Flag_Modo = 2 Then
                    If Len(Trim(Me.wCommodity.Text)) > 0 Then
                        Dim ds As New DataSet
                        ds = ws.GetDataset(strConnect, "Select Name From Comdty Where id = " & Me.wID.Text, 1)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Trim(ds.Tables(0).Rows(0).Item("Name")) <> Trim(Me.wCommodity.Text) Then
                                eResp = ws.ExecSQL(md.strConnect, "Update Comdty Set Name = '" & Trim(Me.wCommodity.Text) & "' Where id = " & Me.wID.Text)
                                Me.Insert_Changes("Changed Name: " & Trim(Me.wCommodity.Text), wID.Text, Trim(ds.Tables(0).Rows(0).Item("Name")))
                                Me.Refresh_Comdty()
                            End If
                        End If
                        ds = Nothing
                    Else
                        MsgBox("Commodity Name field is empty,...")
                        Me.wCommodity.Focus()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub bnt_New_Click(sender As Object, e As EventArgs) Handles bnt_New.Click
        Me.Commodity_Clear()
        Me.Modo_Edit_ADD_Read_Only(3)
        Flag_Modo = 3
        Me.wCommodity.Focus()
    End Sub

    Private Sub Audit_Search()
        If Len(Trim(Me.wCommodity.Text)) > 0 Then
            If Len(Trim(Me.wID.Text)) > 0 Then
                ' ------- Audit
                Dim ds As New DataSet
                strSQL = "SELECT  Description, Comdty_ID, Old_value, Created_Date, Created_By FROM   dbo.Comdty_Journal where Comdty_ID =" & Trim(Me.wID.Text) & " Order By uid Desc "
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.dgv_Audit.Rows.Clear()
                    Dim i As Integer = 0
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Me.dgv_Audit.Rows.Add(ds.Tables(0).Rows(i).Item("Created_Date"), ds.Tables(0).Rows(i).Item("Created_By"), ds.Tables(0).Rows(i).Item("Description"), ds.Tables(0).Rows(i).Item("Old_Value"))
                    Next
                    ds = Nothing
                    i = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.TabControl1.SelectedIndex = 1 Then
            Me.TabControl1.SelectedIndex = 1
            Me.Refresh()
            Dim ds As New DataSet
            strSQL = "SELECT  ID,NAME  FROM COMDTY order by ID"
            ds = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim cr_comdty As New cr_Comdty
                cr_comdty.SetDataSource(ds.Tables(0))
                Me.cv_Comdty.CloseView(cr_comdty)
                Me.cv_Comdty.ReportSource = cr_comdty
                Me.cv_Comdty.BringToFront()
                Me.cv_Comdty.RefreshReport()
                Me.cv_Comdty.DisplayToolbar = True
                Me.cv_Comdty.DisplayStatusBar = True
                Me.cv_Comdty.Refresh()
                Me.cv_Comdty.Show()
            End If
        End If
        If Me.TabControl1.SelectedIndex = 2 Then
            Me.TabControl1.SelectedIndex = 2
            Me.Refresh()
            Me.Audit_Search()
        End If
    End Sub
End Class