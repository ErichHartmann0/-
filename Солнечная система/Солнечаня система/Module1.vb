Imports System.Data.OleDb
Module Module1
    Public conn As New OleDbConnection
    Public Sub Fill_grid(datagridview1 As DataGridView, cmd As String, tablename As String)
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = cmd
        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter(c)
        da.Fill(ds, tablename)
        datagridview1.DataSource = ds
        datagridview1.DataMember = tablename

    End Sub

End Module
