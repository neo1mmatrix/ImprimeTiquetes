Imports System.Text.RegularExpressions
Public Class Fr_Imprimir

    Dim _Segundos As Integer = 0
    '1 = Letra Normal
    '2 = Letra Mediana
    Dim _TipoImpresora As Integer
    Dim _PrinterName As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CargarConfiguracion()
        _Segundos = 0
        lbSegundos.Visible = False
        timerCuentaRegresiva.Start()

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
        Dim _ClienteCedula As String = ""
        Dim _ClienteEmail As String = ""
        Dim _DocumentoNumero As String = ""
        Dim _DocumentoFecha As String = ""
        Dim _DocumentoClave As String = ""

        Dim _DatosSucursal() As String = {_Cedula, _Sucursal, _Direccion, _Telefono, _Email}
        Dim list As New List(Of String)

        Dim tempArray() As String
        tempArray = txtTiquete.Lines

        For i = 9 To tempArray.Length - 20
            If tempArray(i).Contains("Identificacion") Then
                _ClienteCedula = tempArray(i)
                list.Add(_ClienteCedula)
            End If
            If tempArray(i).Contains("E-mail") Then
                _ClienteEmail = tempArray(i)
                list.Add(_ClienteEmail)
            End If
            If tempArray(i).Contains("Documento") Then
                _DocumentoNumero = tempArray(i)
                list.Add(_DocumentoNumero)
            End If
            If tempArray(i).Contains("Fecha") Then
                _DocumentoFecha = tempArray(i)
                list.Add(_DocumentoFecha)
            End If
            If tempArray(i).Contains("Clave") Then
                _DocumentoClave = tempArray(i)
                list.Add(_DocumentoClave)
            End If
        Next

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

        txtTiquete.Lines = txtTiquete.Lines.Where(Function(line) line.Trim <> String.Empty).ToArray()
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

    Private Sub TimerCuentaRegresiva_Tick(sender As Object, e As EventArgs) Handles timerCuentaRegresiva.Tick
        _Segundos += 1
        lbSegundos.Text = CStr(15 - _Segundos)
        lbSegundos.Visible = True
        If _Segundos = 15 Then
            Me.Close()
        End If
    End Sub

    Private Sub Form1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick

        timerCuentaRegresiva.Stop()
        Fr_Configuracion.Show()

    End Sub

    Public Sub CargarConfiguracion()

        _TipoImpresora = My.Settings.FontSize
        _PrinterName = My.Settings.PrinterName
        _LongitudImpresion = My.Settings.LongitudLinea
        PrinterNameTermica = _PrinterName

    End Sub

    Private Sub btnAyuda_Click(sender As Object, e As EventArgs) Handles btnAyuda.Click

        timerCuentaRegresiva.Stop()
        Ayuda.Show()

    End Sub

    Private Sub Fr_Imprimir_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If _TipoImpresora = 0 Then
            MsgBox("Necesita Configurar la Impresión")
            timerCuentaRegresiva.Stop()
            Fr_Configuracion.Show()
        End If

    End Sub
End Class
