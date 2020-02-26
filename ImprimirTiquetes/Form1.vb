Imports System.Text.RegularExpressions
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtTiquete_Click(sender As Object, e As EventArgs) Handles txtTiquete.Click

        txtTiquete.Clear()
        CompruebaTipoFactura()

    End Sub

    Private Sub ImprimirFactura()

        'Elimina las lineas en blanco y las vacias
        txtTiquete.Lines = txtTiquete.Lines.Where(Function(line) line.Trim <> String.Empty).ToArray()
        ReemplazaAcentos()

        Dim _TiqueteElectronico As String = txtTiquete.Text
        Dim _Propietario As String = txtTiquete.Lines(0).ToString
        Dim _Cedula As String = txtTiquete.Lines(1).ToString
        Dim _Sucursal As String = txtTiquete.Lines(2).ToString
        Dim _Direccion As String = txtTiquete.Lines(4).ToString
        Dim _Telefono As String = txtTiquete.Lines(5).ToString
        Dim _Email As String = txtTiquete.Lines(6).ToString

        Dim _TipoDocumento As String = txtTiquete.Lines(7).ToString
        Dim _ClienteNombre As String = txtTiquete.Lines(8).ToString.Replace(vbTab, " ").ToUpper
        Dim _ClienteCedula As String = txtTiquete.Lines(9).ToString
        Dim _ClienteEmail As String = txtTiquete.Lines(10).ToString
        Dim _DocumentoNumero As String = txtTiquete.Lines(11).ToString
        Dim _DocumentoFecha As String = txtTiquete.Lines(12).ToString
        Dim _DocumentoClave As String = txtTiquete.Lines(14).ToString

        Dim _DatosSucursal() As String = {_Cedula, _Sucursal, _Direccion, _Telefono, _Email}
        Dim list As New List(Of String)

        If txtTiquete.Lines(11).ToString.Contains("E-mail") Then
            _ClienteEmail = txtTiquete.Lines(11).ToString
            _DocumentoNumero = txtTiquete.Lines(12).ToString
            _DocumentoFecha = txtTiquete.Lines(13).ToString
            _DocumentoClave = txtTiquete.Lines(15).ToString
        End If

        If txtTiquete.Lines(10).ToString.Contains("E-mail") Or txtTiquete.Lines(11).ToString.Contains("E-mail") Then
            list.Add(_ClienteCedula)
            list.Add(_ClienteEmail)
            list.Add(_DocumentoNumero)
            list.Add(_DocumentoFecha)
            list.Add(_DocumentoClave)
        Else
            list.Add(_ClienteCedula)
            list.Add(_ClienteEmail)
            list.Add(_DocumentoFecha)
        End If

        Dim _DatosCliente() As String = list.ToArray

        StartPrint()
        If prn.PrinterIsOpen = True Then
            PrintHeader(_Propietario, _DatosSucursal)
            PrintDetalles(_TipoDocumento, _ClienteNombre, _DatosCliente)
            PrintTiqueteElectonico(txtTiquete)
            PrintFooterTiquete()
            EndPrint()
        End If

    End Sub

    Private Sub ImprimeBn()

        FormateoBn()
        StartPrint()
        If prn.PrinterIsOpen = True Then
            PrintTiqueteBn(txtTiquete)
            PrintFooterBn()
            EndPrint()
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        CompruebaTipoFactura()

    End Sub

    Private Sub ReemplazaAcentos()

        Dim tempArray() As String
        tempArray = txtTiquete.Lines

        For i = 0 To tempArray.Length - 1
            tempArray(i) = tempArray(i).ToString.Replace("á", "a")
            tempArray(i) = tempArray(i).ToString.Replace("é", "e")
            tempArray(i) = tempArray(i).ToString.Replace("í", "i")
            tempArray(i) = tempArray(i).ToString.Replace("ó", "o")
            tempArray(i) = tempArray(i).ToString.Replace("ú", "u")
        Next

        txtTiquete.Lines = tempArray

    End Sub

    'Selecciona El tipo de Factura a Imprimir
    Private Sub CompruebaTipoFactura()

        If Clipboard.GetText().Contains("www.FacturaProfesional.com") Then
            txtTiquete.Text = Clipboard.GetText()
            ImprimirFactura()
            txtTiquete.Clear()
            Clipboard.Clear()
        ElseIf Clipboard.GetText().Contains("BN-SERVICIOS") Then
            txtTiquete.Text = Clipboard.GetText()
            ImprimeBn()
            txtTiquete.Clear()
            Clipboard.Clear()
        Else
            txtTiquete.Text = Clipboard.GetText()
            MsgBox("FORMATO NO RECONOCIDO")
        End If

    End Sub

    'Formatea el Texto del tiquete del BN para eleminar saltos de linea innecesarios
    Private Sub FormateoBn()

        Dim tempArray() As String = txtTiquete.Lines
        Dim _Productos As Boolean = False
        Dim list As New List(Of String)

        For i = 0 To tempArray.Length - 2
            If tempArray(i).Length > 0 And tempArray(i).Substring(tempArray(i).Length - 1, 1) = ":" Then
                list.Add(tempArray(i) & " " & tempArray(i + 1))
                i += 1
            Else
                list.Add(tempArray(i))
            End If

        Next

        txtTiquete.Clear()
        txtTiquete.Lines = list.ToArray
    End Sub

End Class
