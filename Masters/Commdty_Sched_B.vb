Public Class Commdty_Sched_B
    Dim ds_SchedB As New DataSet
    Dim ds_SchedB_Master As New DataSet
    Dim ds_Chapters As New DataSet

    Private Sub Commdty_Sched_B_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Trim(md.Progran_access(UserCode, "Commodities")) = "N" Then
            Me.Close()
            Exit Sub
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
        strSQL = "SELECT ID, Chapter, Upper(Name) as Name FROM  Comdty_SchedB order by Chapter"

        ds_Chapters = ws.GetDataset(strConnect, strSQL, 1)
        If ds_Chapters.Tables(0).Rows.Count > 0 Then
            Me.cb_Chapters.DataSource = ds_Chapters.Tables(0)
            Me.cb_Chapters.DisplayMember = "Name"
            Me.cb_Chapters.ValueMember = "Chapter"
            Dim j As Integer = 0
            For j = 0 To ds_Chapters.Tables(0).Rows.Count - 1
                Me.dgv_Chapters.Rows.Add(Trim(ds_Chapters.Tables(0).Rows(j).Item("Chapter")), ds_Chapters.Tables(0).Rows(j).Item("ID"), Trim(ds_Chapters.Tables(0).Rows(j).Item("Name")))
            Next
        End If

        strSQL = "Select DISTINCT rtrim(Upper(SCHEDB_Name)), SCHEDB_NAME , SCHEDB_NUMBER From SCHEDB Order By SCHEDB_Name"

        ds_SchedB_Master.Clear()
        ds_SchedB_Master = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_SchedB_Master.Tables(0).Rows.Count > 0 Then
            Me.cb_Schud_B.DataSource = ds_SchedB_Master.Tables(0)
            Me.cb_Schud_B.DisplayMember = "SchedB_Name"
            Me.cb_Schud_B.ValueMember = "SCHEDB_Number"
            Me.cb_Schud_B.Refresh()
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Schud_B_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_Schud_B.KeyPress
        If (Char.IsControl(e.KeyChar)) Then Return
        Dim Str As String = cb_Schud_B.Text.Substring(0, cb_Schud_B.SelectionStart) + e.KeyChar
        Str = Str.ToUpper
        Dim Index As Integer = cb_Schud_B.FindStringExact(Str)
        If Index = -1 Then
            Index = cb_Schud_B.FindString(Str)
        End If
        Me.cb_Schud_B.SelectedIndex = Index
        Me.cb_Schud_B.SelectionStart = Str.Length
        Me.cb_Schud_B.SelectionLength = Me.cb_Schud_B.Text.Length - Me.cb_Schud_B.SelectionStart
        'Me.cb_Chapters.DroppedDown = True
        e.Handled = True
    End Sub

    Private Sub dgv_Chapters_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Chapters.CellClick
        Me.Search_x_Chapter(Trim(Me.dgv_Chapters.Item(0, e.RowIndex).Value))
    End Sub

    Private Sub cb_Schud_B_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Schud_B.SelectionChangeCommitted
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim vCode As String = ""
        vCode = Me.cb_Schud_B.SelectedValue
        strSQL = "SELECT SchedB_Number, SchedB_Name FROM  SchedB WHERE SchedB_Number = '" & Trim(vCode) & "' Order by SchedB_Number, SchedB_Name"
        ds_SchedB.Clear()
        ds_SchedB = GetDataset(strConnect, strSQL, 1)
        Me.dgv_SchedB.DataSource = ds_SchedB.Tables(0)

        Me.dgv_SchedB.Columns("SchedB_Number").Width = 90
        Me.dgv_SchedB.Columns("SchedB_Number").HeaderText = "Code"
        Me.dgv_SchedB.Columns("SchedB_Number").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_SchedB.Columns("SchedB_Name").Width = 870
        Me.dgv_SchedB.Columns("SchedB_Name").HeaderText = "Description"
        Me.dgv_SchedB.Columns("SchedB_Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.dgv_SchedB.Columns("SchedB_Name").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Me.dgv_SchedB.Refresh()
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Chapters_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cb_Chapters.KeyPress
        If (Char.IsControl(e.KeyChar)) Then Return
        Dim Str As String = cb_Chapters.Text.Substring(0, cb_Chapters.SelectionStart) + e.KeyChar

        Dim Index As Integer = cb_Chapters.FindStringExact(Str)
        If Index = -1 Then
            Index = cb_Chapters.FindString(Str)
        End If
        Me.cb_Chapters.SelectedIndex = Index
        Me.cb_Chapters.SelectionStart = Str.Length
        Me.cb_Chapters.SelectionLength = Me.cb_Chapters.Text.Length - Me.cb_Chapters.SelectionStart
        'Me.cb_Chapters.DroppedDown = True
        e.Handled = True
    End Sub

    Private Sub Search_x_Chapter(ByVal Chapter As String)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT SchedB_Number, SchedB_Name FROM  SchedB WHERE (SUBSTRING(SchedB_Number, 1, 2) = '" & Trim(Chapter) & "') Order by SchedB_Number"
        ds_SchedB.Clear()
        ds_SchedB = GetDataset(strConnect, strSQL, 1)
        Me.dgv_SchedB.DataSource = ds_SchedB.Tables(0)

        Me.dgv_SchedB.Columns("SchedB_Number").Width = 90
        Me.dgv_SchedB.Columns("SchedB_Number").HeaderText = "Code"
        Me.dgv_SchedB.Columns("SchedB_Number").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.dgv_SchedB.Columns("SchedB_Name").Width = 870
        Me.dgv_SchedB.Columns("SchedB_Name").HeaderText = "Description"
        Me.dgv_SchedB.Columns("SchedB_Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.dgv_SchedB.Columns("SchedB_Name").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Me.dgv_SchedB.Refresh()
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cb_Chapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cb_Chapters.SelectionChangeCommitted
        Me.Search_x_Chapter(Me.cb_Chapters.SelectedValue)
    End Sub

    Private Sub cb_Chapters_LostFocus(sender As Object, e As EventArgs) Handles cb_Chapters.LostFocus
        Me.Search_x_Chapter(Me.cb_Chapters.SelectedValue)
    End Sub
End Class