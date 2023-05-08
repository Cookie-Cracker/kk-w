Public Class Movements_Type
    Public ds_Move As New DataSet
    ' ------- Flag Modo
    ' ------- 1 - Search (Read Only)
    ' ------- 2 - Edit
    ' ------- 3 - Add
    Dim Flag_Modo As Integer = 1
    Private Sub dgv_Move_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Move.CellClick
        If e.RowIndex > -1 Then
            Me.dgv_Move.Rows(e.RowIndex).Selected = True
            Me.dgv_Move.Refresh()
            Me.Clear()
            Me.txt_code.Text = Me.dgv_Move.Item(0, e.RowIndex).Value
            Me.txt_Desc.Text = Me.dgv_Move.Item(1, e.RowIndex).Value
            Me.cb_TIR.SelectedItem = Me.dgv_Move.Item(3, e.RowIndex).Value
            Me.cb_Seal.SelectedItem = Me.dgv_Move.Item(5, e.RowIndex).Value
            Me.cb_Weight.SelectedItem = Me.dgv_Move.Item(6, e.RowIndex).Value
            Me.txt_EDI_Tran.Text = Me.dgv_Move.Item(9, e.RowIndex).Value
            Me.txt_EDI_Code.Text = Me.dgv_Move.Item(10, e.RowIndex).Value
        End If
    End Sub
    Private Sub Clear()
        Me.txt_code.Clear()
        Me.txt_Desc.Clear()
        Me.txt_EDI_Code.Clear()
        Me.txt_EDI_Tran.Clear()
    End Sub
    Private Sub Movements_Type_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, "Movements_Type")) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        Me.Refresh_Move()
        Flag_Modo = 1
        Me.Modo_Edit_ADD_Read_Only(1)
        md.Insert_User_Log("Eq. Movement", md.UserName)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Sub

    Private Sub Refresh_Move()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT TRAN_CODE, DESCRIPTION, AFFECT_BOKDTL as BK, TIR_CODE as TIR, ALLOWED_FOR_BL as BL, SEAL_REQUIRED as Seal, WEIGHT_REQUIRED as Weight, EMPTY_STATUS as Empty, SENT_OUT as Sent, EDI_TRANS_01, EDI_CODE_01, Inv_Report
                    FROM dbo.Eq_Move_Master
                    ORDER BY TRAN_CODE"
        ds_Move = ws.GetDataset(strConnect, strSQL, 1)
        Me.dgv_Move.DataSource = ds_Move.Tables(0)
        Me.dgv_Move.Columns("TRAN_CODE").Width = 55
        Me.dgv_Move.Columns("TRAN_CODE").HeaderText = "Code"
        Me.dgv_Move.Columns("TRAN_CODE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("DESCRIPTION").Width = 180
        Me.dgv_Move.Columns("DESCRIPTION").HeaderText = "Description"
        Me.dgv_Move.Columns("DESCRIPTION").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("BK").Width = 50
        Me.dgv_Move.Columns("BK").HeaderText = "BK"
        Me.dgv_Move.Columns("BK").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("TIR").Width = 50
        Me.dgv_Move.Columns("TIR").HeaderText = "TIR"
        Me.dgv_Move.Columns("TIR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("BL").Width = 50
        Me.dgv_Move.Columns("BL").HeaderText = "BL"
        Me.dgv_Move.Columns("BL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("Seal").Width = 55
        Me.dgv_Move.Columns("Seal").HeaderText = "Seal"
        Me.dgv_Move.Columns("Seal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("Weight").Width = 55
        Me.dgv_Move.Columns("Weight").HeaderText = "Weight"
        Me.dgv_Move.Columns("Weight").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("Empty").Width = 55
        Me.dgv_Move.Columns("Empty").HeaderText = "Empty"
        Me.dgv_Move.Columns("Empty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("Sent").Width = 55
        Me.dgv_Move.Columns("Sent").HeaderText = "To Out"
        Me.dgv_Move.Columns("Sent").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("EDI_TRANS_01").Width = 65
        Me.dgv_Move.Columns("EDI_TRANS_01").HeaderText = "EDI Transaction"
        Me.dgv_Move.Columns("EDI_TRANS_01").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("EDI_CODE_01").Width = 55
        Me.dgv_Move.Columns("EDI_CODE_01").HeaderText = "EDI Code"
        Me.dgv_Move.Columns("EDI_CODE_01").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Columns("Inv_Report").Width = 55
        Me.dgv_Move.Columns("Inv_Report").HeaderText = "To Report"
        Me.dgv_Move.Columns("Inv_Report").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_Move.Refresh()
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Modo_Edit_ADD_Read_Only(ByVal Flag_Modo As Integer)
        Select Case Flag_Modo
            Case 1
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = False
                Me.bnt_New.Enabled = True
                Me.bnt_Edit.Enabled = True
                Me.cb_TIR.Enabled = False
                Me.cb_Seal.Enabled = False
                Me.cb_Weight.Enabled = False
                Me.txt_EDI_Tran.ReadOnly = True
                Me.txt_EDI_Tran.BackColor = Color.LightBlue
                Me.txt_EDI_Code.ReadOnly = True
                Me.txt_EDI_Code.BackColor = Color.LightBlue
            Case 2
                Me.bnt_Del.Enabled = True
                Me.bnt_Save.Enabled = True
                Me.cb_TIR.Enabled = True
                Me.cb_Seal.Enabled = True
                Me.cb_Weight.Enabled = True
                Me.txt_EDI_Tran.ReadOnly = False
                Me.txt_EDI_Tran.BackColor = Color.White
                Me.txt_EDI_Code.ReadOnly = False
                Me.txt_EDI_Code.BackColor = Color.White
            Case 3
                Me.bnt_Del.Enabled = False
                Me.bnt_Save.Enabled = True
                Me.bnt_Edit.Enabled = False
                Me.cb_TIR.Enabled = True
                Me.cb_Seal.Enabled = True
                Me.cb_Weight.Enabled = True
                Me.txt_EDI_Tran.ReadOnly = False
                Me.txt_EDI_Tran.BackColor = Color.White
                Me.txt_EDI_Code.ReadOnly = False
                Me.txt_EDI_Code.BackColor = Color.White
        End Select
    End Sub

End Class