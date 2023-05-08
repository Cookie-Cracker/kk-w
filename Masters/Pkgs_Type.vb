Imports System.ComponentModel

Public Class Pkgs_Type
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1
    Private Sub Pkgs_Type_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, "Pkgs_Type")) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
        Dim ds As New DataSet
        strSQL = "SELECT uid,ABREVIATION, ENG_DESC, OceanACE_Code, Json_Desc, OTHER_DESC, VIN_FLAG FROM dbo.PKGDESC ORDER BY ABREVIATION"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim jj As Integer = 0
            For jj = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Pkgs.Rows.Add(Trim(ds.Tables(0).Rows(jj).Item("Abreviation")), Trim(ds.Tables(0).Rows(jj).Item("ENG_Desc")), ds.Tables(0).Rows(jj).Item("uid"))
            Next
            jj = Nothing
        End If
        ds = Nothing
        md.Insert_User_Log("Load Dpto", md.UserName)
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If Flag_Modo = 1 Then
            Me.bnt_Del.Enabled = False
            Me.bnt_Save.Enabled = False
            Me.bnt_Edit.Enabled = True
            Me.bnt_New.Enabled = True
            ' ------- Color
            Me.txt_Unit_Type.BackColor = Color.Navy
            Me.txt_Unit_Type.ForeColor = Color.White
            Me.txt_Desc.BackColor = Color.Navy
            Me.txt_Desc.ForeColor = Color.White

            Me.txt_Unit_Type.ReadOnly = True
            Me.txt_Desc.ReadOnly = True

            Me.Txt_Booking_Notes.BackColor = Color.Navy
            Me.Txt_Booking_Notes.ForeColor = Color.White
            Me.Txt_Booking_Notes.ReadOnly = True

        Else
            If Flag_Modo = 3 Then
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = True
            Else
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = False
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = False
            End If
            ' ------- Color
            Me.txt_Unit_Type.BackColor = Color.White
            Me.txt_Unit_Type.ForeColor = Color.Black
            Me.txt_Desc.BackColor = Color.White
            Me.txt_Desc.ForeColor = Color.Black

            Me.txt_Unit_Type.ReadOnly = False
            Me.txt_Desc.ReadOnly = False
            Me.Txt_Booking_Notes.ReadOnly = False
            Me.Txt_Booking_Notes.BackColor = Color.White
            Me.Txt_Booking_Notes.ForeColor = Color.Black
        End If
        Dim j As Integer = 0
        For j = 0 To Me.dgv_Pkgs.RowCount - 1
            Me.dgv_Pkgs.Rows(j).Cells(0).ReadOnly = True
            Me.dgv_Pkgs.Rows(j).Cells(1).ReadOnly = True
            Me.dgv_Pkgs.Rows(j).Cells(2).ReadOnly = True

            Me.dgv_Pkgs.Rows(j).Cells(0).Style.BackColor = Color.PaleTurquoise
            Me.dgv_Pkgs.Rows(j).Cells(0).Style.ForeColor = Color.Black
            Me.dgv_Pkgs.Rows(j).Cells(1).Style.BackColor = Color.PaleTurquoise
            Me.dgv_Pkgs.Rows(j).Cells(1).Style.ForeColor = Color.Black
            Me.dgv_Pkgs.Rows(j).Cells(2).Style.BackColor = Color.MidnightBlue
            Me.dgv_Pkgs.Rows(j).Cells(2).Style.ForeColor = Color.White
        Next
        j = Nothing
    End Sub

    Private Sub dgv_Pkgs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Pkgs.CellClick
        If e.RowIndex > -1 Then
            Me.dgv_Pkgs.Rows(e.RowIndex).Selected = True
            Me.dgv_Pkgs.Refresh()
            Me.txt_Unit_Type.Text = Me.dgv_Pkgs.Item(0, e.RowIndex).Value
            Me.txt_Desc.Text = Me.dgv_Pkgs.Item(1, e.RowIndex).Value

            Me.Txt_Booking_Notes.Text = FormatStrLine(md.Pkg_Get_Booking_Notes(Me.txt_Unit_Type.Text))

            Flag_Modo = 1
            Me.Modo_Edit_ADD_Read_Only(1)
        End If
    End Sub

    Private Sub bnt_Edit_Click(sender As Object, e As EventArgs) Handles bnt_Edit.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 2 Then
                Flag_Modo = 1
                Modo_Edit_ADD_Read_Only(1)
                bnt_Del.Enabled = False
                bnt_New.Enabled = True
                bnt_Edit.Enabled = True
                bnt_Save.Enabled = False
            Else
                Flag_Modo = 2
                Me.Modo_Edit_ADD_Read_Only(2)
                bnt_Del.Enabled = True
                bnt_Edit.Enabled = False
                bnt_Save.Enabled = False
                bnt_New.Enabled = False
                Me.txt_Unit_Type.Focus()
            End If
            Flag_Modo = 2
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub refrehs_Pkgs()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Clear()
        If Me.dgv_Pkgs.Rows.Count > 0 Then
            Me.dgv_Pkgs.Rows.Clear()
        End If
        Dim ds As New DataSet
        strSQL = "SELECT   uid, ABREVIATION, ENG_DESC, ISNULL(OceanACE_Code, '') AS OceanACE_Code, ISNULL(Json_Desc, '') AS Json_Desc, ISNULL(OTHER_DESC, '') AS Other_Desc, ISNULL(VIN_FLAG, '') AS VIN_Flag, ISNULL(Manzanillo, '') AS Manzanillo, ISNULL(Booking_Notes, '') AS Booking_Notes FROM PKGDESC ORDER BY ABREVIATION"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim jj As Integer = 0
            For jj = 0 To ds.Tables(0).Rows.Count - 1
                Me.dgv_Pkgs.Rows.Add(Trim(ds.Tables(0).Rows(jj).Item("Abreviation")), Trim(ds.Tables(0).Rows(jj).Item("ENG_Desc")), ds.Tables(0).Rows(jj).Item("uid"))
            Next
            jj = Nothing
        End If
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear()
        Me.txt_Unit_Type.Clear()
        Me.txt_Desc.Clear()
        Me.Txt_Booking_Notes.Clear()
        Me.Txt_Booking_Notes.Clear()
    End Sub

    Private Sub bnt_New_Click(sender As Object, e As EventArgs) Handles bnt_New.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Flag_Modo = 3
            Me.Clear()
            Me.Modo_Edit_ADD_Read_Only(3)
            bnt_Del.Enabled = False
            bnt_Edit.Enabled = False
            bnt_Save.Enabled = True
            bnt_New.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Save_Click(sender As Object, e As EventArgs) Handles bnt_Save.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Flag_Modo = 3 Then
                ' ------- Validation
                If Len(Trim(Me.txt_Unit_Type.Text)) = 0 Then
                    MsgBox("Unit field is empty,..")
                    Me.txt_Unit_Type.Focus()
                    Exit Sub
                End If
                If Len(Trim(Me.txt_Desc.Text)) = 0 Then
                    MsgBox("Description field is empty,..")
                    Me.txt_Desc.Focus()
                    Exit Sub
                End If
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select ABREVIATION From PkgDesc Where ABREVIATION = '" & Trim(Me.txt_Unit_Type.Text) & "'", 1)
                If ds.Tables(0).Rows.Count = 0 Then
                    strSQL = "Insert Into PkgDesc (ABREVIATION, ENG_DESC) Values ('" & Trim(Mid(Me.txt_Unit_Type.Text, 1, 2)) & "','" & Mid(Trim(Replace(Me.txt_Desc.Text, "'", "''")), 1, 20) & "')"
                    'MsgBox(strSql)
                    eResp = ws.ExecSQL(md.strConnect, strSQL)
                    Me.Clear()
                    Me.refrehs_Pkgs()
                Else
                    MsgBox("Unit found,...")
                End If
                ds = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txt_Unit_Type_Validating(sender As Object, e As CancelEventArgs) Handles txt_Unit_Type.Validating
        If Flag_Modo = 2 Then
            If Me.txt_Unit_Type.Focused Then
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Dim ds As New DataSet
                ds = ws.GetDataset(strConnect, "Select ABREVIATION From PkgDesc Where ABREVIATION = '" & Trim(Me.txt_Unit_Type.Text) & "'", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    MsgBox("Unit found,.....")
                    Me.txt_Unit_Type.Clear()
                    Me.txt_Unit_Type.Focus()
                End If
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
        End If
    End Sub

    Private Sub txt_Unit_Type_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Unit_Type.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim nID As Integer = Me.dgv_Pkgs.Item(2, Me.dgv_Pkgs.CurrentCell.RowIndex).Value
                Dim vDesc As String = ""
                Dim ds As New DataSet '
                strSQL = "Select isNull( ABREVIATION,'') as  ABREVIATION From PkgDesc Where uid = " & nID
                ds = ws.GetDataset(md.strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("ABREVIATION")) <> Trim(Me.txt_Unit_Type.Text) Then
                        vDesc = "Changed Unit "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update PkgDesc Set  ABREVIATION = '" & Trim(Replace(Me.txt_Unit_Type.Text, "'", "''")) & "' Where  uid = " & nID)
                        'Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.TextAlign), Trim(ds.Tables(0).Rows(0).Item("Dpto_Name")))
                        Me.refrehs_Pkgs()
                    End If
                Else
                    vDesc = "Changed Unit "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update PkgDesc Set  ABREVIATION = '" & Trim(Replace(Me.txt_Unit_Type.Text, "'", "''")) & "' Where  uid = " & nID)
                    'Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.TextAlign), Trim(ds.Tables(0).Rows(0).Item("Dpto_Name")))
                    Me.refrehs_Pkgs()
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub bnt_Del_Click(sender As Object, e As EventArgs) Handles bnt_Del.Click
        If Flag_Modo = 2 Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim style As MsgBoxStyle
            Dim response As MsgBoxResult
            style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
            response = MsgBox("Are you sure, you want remove this unit type?", style, "Warning")
            If response = MsgBoxResult.Yes Then   ' User chose Yes
                Dim nID As Integer = Me.dgv_Pkgs.Item(2, Me.dgv_Pkgs.CurrentCell.RowIndex).Value
                md.eResp = ws.ExecSQL(md.strConnect, "Delete PkgDesc Where  uid = " & nID)
                'Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.TextAlign), Trim(ds.Tables(0).Rows(0).Item("Dpto_Name")))
                Me.refrehs_Pkgs()
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub Txt_Booking_Notes_Leave(sender As Object, e As EventArgs) Handles Txt_Booking_Notes.Leave
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not IsNothing(dgv_Pkgs.Item(0, Me.dgv_Pkgs.CurrentCell.RowIndex).Value) Then
            strSQL = "Select isnull(Booking_Notes,'') as Booking_Notes From PkgDesc Where RTRIM(ABREVIATION) = '" & Trim(dgv_Pkgs.Item(0, Me.dgv_Pkgs.CurrentCell.RowIndex).Value) & "'"
            Dim ds As New DataSet
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                strSQL = "Update PkgDesc Set Booking_Notes = '" & Trim(Replace(Me.Txt_Booking_Notes.Text, "'", "''")) & "' Where ABREVIATION = '" & dgv_Pkgs.Item(0, Me.dgv_Pkgs.CurrentCell.RowIndex).Value & "'"
                eResp = ws.ExecSQL(strConnect, strSQL)
            End If
            ds = Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class