Imports System.Text.RegularExpressions
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtTiquete_Click(sender As Object, e As EventArgs) Handles txtTiquete.Click

        txtTiquete.Clear()

        If Clipboard.GetText().Contains("www.FacturaProfesional.com") Then
            txtTiquete.Text = Clipboard.GetText()
            ImprimirFactura()
            txtTiquete.Clear()
            Clipboard.Clear()
        ElseIf Clipboard.GetText().Contains("www.FacturaProfesional.com") Then

        End If

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
        Dim _ClienteNombre As String = txtTiquete.Lines(8).ToString.Replace(vbTab, " ")
        Dim _ClienteCedula As String = txtTiquete.Lines(9).ToString
        Dim _ClienteEmail As String = txtTiquete.Lines(10).ToString
        Dim _DocumentoNumero As String = txtTiquete.Lines(11).ToString
        Dim _DocumentoFecha As String = txtTiquete.Lines(12).ToString
        Dim _DocumentoClave As String = txtTiquete.Lines(14).ToString

        Dim _DatosSucursal() As String = {_Cedula, _Sucursal, _Direccion, _Telefono, _Email}
        Dim list As New List(Of String)

        If txtTiquete.Lines(10).ToString.Contains("email") Then
            list.Add(_ClienteNombre)
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

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        ImprimirFactura()

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

    Private Sub lee()

        Dim tempArray() As String = txtTiquete.Lines
        Dim _Productos As Boolean = False
        For i = 0 To tempArray.Length - 1

            If tempArray(i).ToString.Contains("Exentas") Then
                _Productos = False
            End If
            If _Productos Then
                MsgBox(tempArray(i).ToString)
            End If
            If tempArray(i).ToString = "Cant	Uni / Cod / Producto	Total" Then
                _Productos = True
            End If
        Next

    End Sub

End Class
