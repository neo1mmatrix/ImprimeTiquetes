Public Class Ayuda
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub Ayuda_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Fr_Imprimir.timerCuentaRegresiva.Start()
    End Sub
End Class