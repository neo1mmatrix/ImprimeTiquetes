Imports System.Drawing.Printing

Public Class Fr_Configuracion
    Private Sub Configuración_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
            PrinterSettings.InstalledPrinters
            cbPrinterList.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        cbPrinterList.SelectedItem = My.Settings.PrinterName
        nudTiempo.Value = My.Settings.TiempoEspera
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

        If cbPrinterList.SelectedIndex = -1 Then
            MsgBox("Seleccione la Impresora")
        ElseIf My.Settings.FontSize = 0 Then
            MsgBox("Seleccione el tamaño de la letra")
        Else
            My.Settings.PrinterName = cbPrinterList.SelectedItem.ToString
            My.Settings.LongitudLinea = nudLineas.Value
            My.Settings.TiempoEspera = nudTiempo.Value
            If rbMedianaSize.Checked Then
                My.Settings.FontSize = 1
            ElseIf rbNormalSize.Checked Then
                My.Settings.FontSize = 2
            End If
            My.Settings.Save()
            Fr_Imprimir.CargarConfiguracion()
            MsgBox("Configuración Guardada")
        End If

    End Sub

    Private Sub rbImpresoraTermica_CheckedChanged(sender As Object, e As EventArgs) Handles rbNormalSize.CheckedChanged
        If rbNormalSize.Checked Then
            nudLineas.Value = 40
            My.Settings.FontSize = 40
        End If
    End Sub

    Private Sub rbImpresoraMatrix_CheckedChanged(sender As Object, e As EventArgs) Handles rbMedianaSize.CheckedChanged
        If rbMedianaSize.Checked Then
            nudLineas.Value = 48
            My.Settings.FontSize = 48
        End If
    End Sub

    Private Sub Configuracion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Fr_Imprimir.timerCuentaRegresiva.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnImprimirPrueba.Click

        If My.Settings.PrinterName = "" Then
            MsgBox("Guarde los cambios primero")
        ElseIf my.Settings.FontSize = 0 Then
            MsgBox("Seleccione el tamaño de la letra")
        Else
            Clipboard.SetText(TiquetePruebaImpresion)
            Fr_Imprimir.CompruebaTipoFactura()
            Threading.Thread.Sleep(500)
            Clipboard.SetText(TiqueteProformaPuebaImpresion)
            Fr_Imprimir.CompruebaTipoFactura()
        End If

    End Sub
End Class