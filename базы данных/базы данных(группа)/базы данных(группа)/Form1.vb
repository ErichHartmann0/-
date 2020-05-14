Imports System.Data.OleDb
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        refreshgrid()
    End Sub
    Private Sub refreshgrid()
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "select * from Группа"

        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter(c)
        da.Fill(ds, "Группа")
        Grid1.DataSource = ds
        Grid1.DataMember = "Группа"
        Grid1.Columns("Код").Visible = False

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New OleDbConnection
        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\DB1.accdb;Persist Security Info=False;"
        conn.Open()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim s1, s2, s3, s4 As String
        Dim r As DialogResult
        Form2.ShowDialog()
        s1 = Form2.TextBox1.Text
        s2 = Form2.TextBox2.Text
        s3 = Form2.TextBox3.Text
        s4 = Form2.TextBox4.Text
        r = Form2.DialogResult
        Form2.Close()

        If r = DialogResult.OK Then

            Exit Sub
        End If


        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = " insert into Группа(ФИО,Дата рождения,мобильный телефон,email) values('" & s1 & "',' " & s2 & "','" & s3 & "',' " & s4 & "')"
        c.ExecuteNonQuery()
        refreshgrid()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim k As Integer
        Dim c As New OleDbCommand
        c.Connection = conn
        k = Grid1.CurrentRow.Cells("Код").Value
        c.CommandText = "delete from Группа were Код =" & k
        c.ExecuteNonQuery()
    End Sub
End Class
