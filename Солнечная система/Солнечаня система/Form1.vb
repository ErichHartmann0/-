Imports System.Data.OleDb
Imports System.Math

Public Class Form1
    Dim _id As Int16
    Dim _v1 As Double
    Dim _v2 As Double
    Dim _m As Double
    Dim _r As Double
    Dim _h As Double
    Dim G As Double = 0.00000000006672
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TODO: данная строка кода позволяет загрузить данные в таблицу "Солнечная_системаDataSet.Планеты". При необходимости она может быть перемещена или удалена.
        Me.ПланетыTableAdapter.Fill(Me.Солнечная_системаDataSet.Планеты)
        conn = New OleDbConnection
        conn.ConnectionString = "Provider=microsoft.Jet.OLEDB.4.0;Data Source=D:\Солнечная система.accdb; Persit Security Info=False;"
        conn.Open()
    End Sub
    Private Sub refresh_grid()
        Fill_grid(DGV1, "select * from Назавние планеты", "Название планеты")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        refresh_grid()
        DGV1.Columns("КОД").Visible = False
        GetPlanetInfo()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult
        Form2.ShowDialog()
        s1 = Form2.TextBox1.Text
        s2 = Form2.TextBox2.Text
        s3 = Form2.TextBox3.Text
        r = Form2.DialogResult
        Form2.Close()
        If r <> DialogResult.OK Then
            Exit Sub
        End If
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "insert into Планеты(Название_планеты,радиус_планеты,масса_планеты) values('" & s1 & "','" & s2 & "','" & s3 & "' )"
        c.ExecuteNonQuery()
        refresh_grid()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim k As Integer
        Dim c As New OleDbCommand
        c.Connection = conn
        k = DGV1.CurrentRow.Cells("КОД").Value
        c.CommandText = "delete from Планеты where KOД=" & k
        c.ExecuteNonQuery()
        refresh_grid()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult
        Dim k As Integer
        k = DGV1.CurrentRow.Cells("КОД").Value
        Form2.TextBox1.Text = DGV1.CurrentRow.Cells("Название_планеты").Value
        Form2.TextBox2.Text = DGV1.CurrentRow.Cells("радиус_планеты").Value
        Form2.TextBox3.Text = DGV1.CurrentRow.Cells("масса_планеты").Value
        Form2.ShowDialog()
        s1 = Form2.TextBox1.Text
        s2 = Form2.TextBox2.Text
        s3 = Form2.TextBox3.Text
        r = Form2.DialogResult
        Form2.Close()
        If r <> DialogResult.OK Then
            Exit Sub
        End If
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "update Планеты set Название_планеты'" & s1 & "',радиус_планеты '" & s2 & "',масса_планеты '" & s3 & "'"
        c.ExecuteNonQuery()
        refresh_grid()
    End Sub
    Private Sub GetPlanetInfo()
        Try
            _id = Int16.Parse(ComboBox1.SelectedValue)
            _h = Double.Parse(TextBox3.Text)
        Catch ex As Exception
            MsgBox("данные обновлены")
        End Try
        Dim row = ПланетыTableAdapter.GetData().Item(_id - 1)
        _m = row.масса_планеты
        _r = row.радиус_планеты
    End Sub
    Private Sub Calsvelocity()
        _v1 = Sqrt((G * _m) / (_r + _h))
        _v2 = -_v1 * Sqrt(2)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Calsvelocity()
        TextBox1.Text = _v1.ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Calsvelocity()
        TextBox2.Text = _v2.ToString()
    End Sub
End Class
