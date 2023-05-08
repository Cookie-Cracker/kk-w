Imports System.Net.Mail
Public Class Dptos
    Public ds_Dpto As New DataSet
    Public ds_PortL As New DataSet
    Public ds_PortD As New DataSet

    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1
    Private Sub Dptos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If Trim(md.Progran_access(UserCode, "CMS")) = "N" Then
                Me.Close()
                Exit Sub
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
            Me.refrehs_Dptos()
            strSQL = "SELECT PORT_NUMBER, PORT_NAME FROM dbo.Ports WHERE (ISNULL(Actived, 'N') = 'Y') ORDER BY PORT_NAME"
            ds_PortL = ws.GetDataset(strConnect, strSQL, 1)
            If ds_PortL.Tables(0).Rows.Count > 0 Then
                Me.cb_PortL.DataSource = ds_PortL.Tables(0)
                Me.cb_PortL.DisplayMember = "Port_Name"
                Me.cb_PortL.ValueMember = "Port_Number"
            End If
            strSQL = "SELECT PORT_NUMBER, PORT_NAME FROM dbo.Ports WHERE (ISNULL(Actived, 'N') = 'Y') ORDER BY PORT_NAME"
            ds_PortD = ws.GetDataset(strConnect, strSQL, 1)
            If ds_PortD.Tables(0).Rows.Count > 0 Then
                Me.cb_PortD.DataSource = ds_PortD.Tables(0)
                Me.cb_PortD.DisplayMember = "Port_Name"
                Me.cb_PortD.ValueMember = "Port_Number"
            End If
            md.Insert_User_Log("Load Dpto", md.UserName)
            Flag_Modo = 1
            Me.Modo_Edit_ADD_Read_Only(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub refrehs_Dptos()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT d.Dpto_Number, d.Dpto_Name, d.PortL, pl.PORT_NAME AS PortL_name, d.PortD, pd.PORT_NAME AS PortD_Name
                        FROM dbo.GL_Dpto AS d INNER JOIN
                             dbo.Ports AS pl ON pl.PORT_NUMBER = d.PortL INNER JOIN
                             dbo.Ports AS pd ON pd.PORT_NUMBER = d.PortD
                      ORDER BY d.Dpto_Number"
        ds_Dpto = ws.GetDataset(strConnect, strSQL, 1)
        If ds_Dpto.Tables(0).Rows.Count > 0 Then
            Me.dgv_Dptos.DataSource = ds_Dpto.Tables(0)
            Me.dgv_Dptos.Columns("Dpto_Number").Width = 40
            Me.dgv_Dptos.Columns("Dpto_Number").HeaderText = "#"
            Me.dgv_Dptos.Columns("Dpto_Number").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Dptos.Columns("Dpto_Name").Width = 210
            Me.dgv_Dptos.Columns("Dpto_Name").HeaderText = "Dpto"
            Me.dgv_Dptos.Columns("Dpto_Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Me.dgv_Dptos.Columns("PortL").Width = 40
            Me.dgv_Dptos.Columns("PortL").HeaderText = "#"
            Me.dgv_Dptos.Columns("PortL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Dptos.Columns("PortL_name").Width = 180
            Me.dgv_Dptos.Columns("PortL_name").HeaderText = "Port of Loading"
            Me.dgv_Dptos.Columns("PortL_name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Me.dgv_Dptos.Columns("PortD").Width = 40
            Me.dgv_Dptos.Columns("PortD").HeaderText = "#"
            Me.dgv_Dptos.Columns("PortD").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgv_Dptos.Columns("PortD_name").Width = 180
            Me.dgv_Dptos.Columns("PortD_name").HeaderText = "Port of Discharge"
            Me.dgv_Dptos.Columns("PortD_name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Me.dgv_Dptos.Refresh()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear()
        Me.txt_Dpto_Number.Clear()
        Me.txt_Dpto_Name.Clear()
        Me.cb_PortL.SelectedValue = 1
        Me.cb_PortD.SelectedValue = 1
    End Sub
    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        If Flag_Modo = 1 Then
            Me.bnt_Del.Enabled = False
            Me.bnt_Save.Enabled = False
            Me.bnt_Edit.Enabled = True
            ' ------- Color
            'Me.TabPage1.BackColor = Color.LightBlue
            Me.txt_Dpto_Name.BackColor = Color.LightBlue
            Me.cb_PortL.BackColor = Color.LightBlue
            Me.cb_PortD.BackColor = Color.LightBlue

            Me.txt_Dpto_Name.ReadOnly = True
            Me.cb_PortL.Enabled = False
            Me.cb_PortD.Enabled = False
        Else
            If Flag_Modo = 3 Then
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = True
            Else
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = False
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = False
            End If

            ' ------- Color
            Me.txt_Dpto_Name.BackColor = Color.White
            Me.cb_PortL.BackColor = Color.White
            Me.cb_PortD.BackColor = Color.White

            Me.txt_Dpto_Name.ReadOnly = False
            Me.cb_PortL.Enabled = True
            Me.cb_PortD.Enabled = True
        End If
    End Sub

    Private Sub dgv_Dptos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Dptos.CellClick
        If e.RowIndex > -1 Then
            Me.dgv_Dptos.Rows(e.RowIndex).Selected = True
            Me.dgv_Dptos.Refresh()
            Me.txt_Dpto_Number.Text = Me.dgv_Dptos.Item(0, e.RowIndex).Value
            Me.txt_Dpto_Name.Text = Me.dgv_Dptos.Item(1, e.RowIndex).Value
            Me.cb_PortL.SelectedValue = Me.dgv_Dptos.Item(2, e.RowIndex).Value
            Me.cb_PortD.SelectedValue = Me.dgv_Dptos.Item(4, e.RowIndex).Value
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
                Me.txt_Dpto_Name.Focus()
            End If
            Flag_Modo = 2
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
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
            Dim nLast As Integer = 1
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select top 1 Company_Number From CM_System Where Company_Number < 990000 Order By Company_number Desc", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                nLast = ds.Tables(0).Rows(0).Item("Company_Number") + 1
            End If
            Me.txt_Dpto_Name.Focus()
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
                If Len(Trim(Me.txt_Dpto_Number.Text)) = 0 Then
                    MsgBox("Dpto account # field is empty,..")
                    Me.txt_Dpto_Number.Focus()
                    Exit Sub
                End If
                If Len(Trim(Me.txt_Dpto_Name.Text)) = 0 Then
                    MsgBox("Name field is empty,..")
                    Me.txt_Dpto_Name.Focus()
                    Exit Sub
                End If
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select Dpto_Number From GL_Dpto Where Dpto_Number = " & Me.txt_Dpto_Number.Text, 1)
                If ds.Tables(0).Rows.Count = 0 Then
                    strSQL = "Insert Into GL_Dpto (Dpto_Number, Dpto_Name,PortL,PortD,Created_By, Created_On) Values (" &
                            Me.txt_Dpto_Number.Text & ",0,'" & Trim(Replace(Me.txt_Dpto_Name.Text, "'", "''")) & "'," &
                                  cb_PortL.SelectedValue & "," & Me.cb_PortD.SelectedValue & ",'" & Trim(System.Environment.UserName) & "','" & Format(System.DateTime.Today, "MM/dd/yyyy") & "')"
                    'MsgBox(strSql)
                    eResp = ws.ExecSQL(md.strConnect, strSQL)

                    ' ------- Send eMail ------------------------------------------
                    Dim Mail As New MailMessage("newinvoice@kingocean.us", "cdsoftdeveloper@gmail.com")
                    Dim SMTP As New SmtpClient()
                    SMTP.Host = "smtp.gmail.com"
                    SMTP.Port = 587
                    SMTP.EnableSsl = True
                    SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

                    Mail.Subject = "New Dpto: " & Trim(Me.txt_Dpto_Name.Text)
                    SMTP.Credentials = New System.Net.NetworkCredential("newinvoice@kingocean.us", "Ko12345*")
                    'SMTP.Credentials = New System.Net.NetworkCredential("cdsoftdeveloper@gmail.com", "atb0n2zc") '<-- Password Here
                    md.eResp = "Name: " & Trim(Me.txt_Dpto_Name.Text) & vbCrLf
                    Mail.Body = "Name: " & Trim(md.eResp) & vbCrLf
                    'Message Here

                    SMTP.Send(Mail)

                    Me.Clear()
                    Me.refrehs_Dptos()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txt_Dpto_Name_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Dpto_Name.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Flag_Modo = 2 Then
                Dim vDesc As String = ""
                Dim ds As New DataSet
                ds = ws.GetDataset(md.strConnect, "Select isNull(Dpto_Name,'') as Dpto_Name From GL_Dpto Where Dpto_Number = " & Me.txt_Dpto_Number.Text, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Trim(ds.Tables(0).Rows(0).Item("Dpto_Name")) <> Trim(Me.txt_Dpto_Name.Text) Then
                        vDesc = "Changed Name "
                        md.eResp = ws.ExecSQL(md.strConnect, "Update GL_Dpto Set Dpto_Name = '" & Trim(Replace(Me.txt_Dpto_Name.Text, "'", "''")) & "' Where Dpto_Number = " & Trim(Me.txt_Dpto_Number.Text))
                        Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.TextAlign), Trim(ds.Tables(0).Rows(0).Item("Dpto_Name")))
                    End If
                End If
                ds = Nothing
                'Audit_Search()
            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub Insert_Changes(ByVal Desc As String, ByVal Company_Number As Integer, ByVal Company_Name As String, ByVal Old_value As String)
        Dim strSQL As String = "Insert Into Dpto_Audit (Created_ON,Created_By,Description,Dpto_Number,Dpto_Name,Old_value
                                                     ) Values ('" &
                                  System.DateTime.Now & "','" &
                                  Trim(System.Environment.UserName) & "','" &
                                  Trim(Desc) & "'," & Company_Number & ",'" &
                                  Trim(Replace(Company_Name, "'", "''")) & "','" &
                                  Trim(Old_value) & "')"
        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
        If md.eResp <> "Success" Then
            MsgBox(Trim(md.eResp))
        End If
        strSQL = Nothing
    End Sub

    Private Sub cb_PortL_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_PortL.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(PortL,1) as PortL From GL_Dpto Where Dpto_Number = " & Me.txt_Dpto_Number.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("PortL")) <> Trim(Me.cb_PortL.SelectedValue) Then
                    vDesc = "Changed Port Loading "
                    md.eResp = ws.ExecSQL(md.strConnect, "Update GL_Dpto Set PortL = " & Me.cb_PortL.SelectedValue & " Where Dpto_Number = " & Trim(Me.txt_Dpto_Number.Text))
                    Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.Text), Trim(ds.Tables(0).Rows(0).Item("PortL")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_PortD_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_PortD.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Flag_Modo = 2 Then
            Dim vDesc As String = ""
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "Select isNull(PortD,1) as PortD From GL_Dpto Where Dpto_Number = " & Me.txt_Dpto_Number.Text, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("PortD")) <> Trim(Me.cb_PortD.SelectedValue) Then
                    vDesc = "Changed Port Discharge"
                    md.eResp = ws.ExecSQL(md.strConnect, "Update GL_Dpto Set PortD = " & Me.cb_PortD.SelectedValue & " Where Dpto_Number = " & Trim(Me.txt_Dpto_Number.Text))
                    Me.Insert_Changes(Trim(vDesc), Me.txt_Dpto_Number.Text, Trim(Me.txt_Dpto_Name.Text), Trim(ds.Tables(0).Rows(0).Item("PortD")))
                End If
            End If
            ds = Nothing
            'Audit_Search()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bnt_Refresh_Click(sender As Object, e As EventArgs) Handles bnt_Refresh.Click
        Me.Clear()
        Me.refrehs_Dptos()
    End Sub
End Class