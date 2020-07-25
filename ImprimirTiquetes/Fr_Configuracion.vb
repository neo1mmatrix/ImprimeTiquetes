Public Class Fr_Configuracion
    Private Sub Configuración_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtImpresoraMatrix.Text = My.Settings.Matrix
        txtImpresoraTermica.Text = My.Settings.Termica
        If My.Settings.TipoImpresora = 1 Then
            rbImpresoraMatrix.Checked = True
        ElseIf My.Settings.TipoImpresora = 2 Then
            rbImpresoraTermica.Checked = True
        End If
        nudLineas.Value = My.Settings.LongitudLinea
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Me.Close()

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        My.Settings.Matrix = txtImpresoraMatrix.Text
        My.Settings.Termica = txtImpresoraTermica.Text
        My.Settings.LongitudLinea = nudLineas.Value
        If rbImpresoraMatrix.Checked Then
            My.Settings.TipoImpresora = 1
        ElseIf rbImpresoraTermica.Checked Then
            My.Settings.TipoImpresora = 2
        End If
        My.Settings.Save()
        Fr_Imprimir.CargarConfiguracion()
        MsgBox("Configuración Guardada")

    End Sub

    Private Sub rbImpresoraTermica_CheckedChanged(sender As Object, e As EventArgs) Handles rbImpresoraTermica.CheckedChanged
        If rbImpresoraTermica.Checked Then
            nudLineas.Value = 48
        End If
    End Sub

    Private Sub rbImpresoraMatrix_CheckedChanged(sender As Object, e As EventArgs) Handles rbImpresoraMatrix.CheckedChanged
        If rbImpresoraMatrix.Checked Then
            nudLineas.Value = 40
        End If
    End Sub

    Private Sub Configuracion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Fr_Imprimir.timerCuentaRegresiva.Start()
    End Sub
End Class