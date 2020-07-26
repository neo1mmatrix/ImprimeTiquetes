Public Class Fr_Configuracion
    Private Sub Configuración_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtPrinterName.Text = My.Settings.PrinterName
        If My.Settings.FontSize = 1 Then
            rbMedianaSize.Checked = True
        ElseIf My.Settings.FontSize = 2 Then
            rbNormalSize.Checked = True
        End If
        nudLineas.Value = My.Settings.LongitudLinea
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Me.Close()

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        My.Settings.PrinterName = txtPrinterName.Text
        My.Settings.LongitudLinea = nudLineas.Value
        If rbMedianaSize.Checked Then
            My.Settings.FontSize = 1
        ElseIf rbNormalSize.Checked Then
            My.Settings.FontSize = 2
        End If
        My.Settings.Save()
        Fr_Imprimir.CargarConfiguracion()
        MsgBox("Configuración Guardada")

    End Sub

    Private Sub rbImpresoraTermica_CheckedChanged(sender As Object, e As EventArgs) Handles rbNormalSize.CheckedChanged
        If rbNormalSize.Checked Then
            nudLineas.Value = 40
        End If
    End Sub

    Private Sub rbImpresoraMatrix_CheckedChanged(sender As Object, e As EventArgs) Handles rbMedianaSize.CheckedChanged
        If rbMedianaSize.Checked Then
            nudLineas.Value = 48
        End If
    End Sub

    Private Sub Configuracion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Fr_Imprimir.timerCuentaRegresiva.Start()
    End Sub
End Class