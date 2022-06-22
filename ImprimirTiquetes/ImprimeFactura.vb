Imports System.Text.RegularExpressions

Public Module ImprimeFactura

    Public Const eClear As String = Chr(27) + "@"
    Public Const eNegritaOn As String = Chr(27) + Chr(69) + "1"
    Public Const eNegritaOff As String = Chr(27) + Chr(69) + "0"
    Public Const eCentre As String = Chr(27) + Chr(97) + "1"
    Public Const eLeft As String = Chr(27) + Chr(97) + "0"
    Public Const eRight As String = Chr(27) + Chr(97) + "2"
    Public Const eDrawer As String = eClear + Chr(27) + "p" + Chr(0) + ".}"
    Public Const eCut As String = Chr(27) + "i" + vbCrLf
    Public Const eSmlText As String = Chr(27) + "!" + Chr(2)
    Public Const eBigText As String = Chr(27) + "!" + Chr(16)
    Public Const eNmlText As String = Chr(27) + "!" + Chr(1)

    Public Const eInit As String = eNmlText + Chr(13) + Chr(27) +
    "c6" + Chr(1) + Chr(27) + "R3" + vbCrLf

    Public Const eBigCharOn As String = Chr(27) + "!" + Chr(56)
    Public Const eBigCharOff As String = Chr(27) + "!" + Chr(0)
    Public Const eUltimaLinea As String = Chr(27) + Chr(100) + "1"
    Public Const eCol1 As String = Chr(27) + Chr(81) + "3"
    Public Const eCol2 As String = Chr(27) + Chr(92) + " 15" + " 0"
    Public Const eCol3 As String = Chr(27) + Chr(36) + "45L" + "0H"
    Public Const clear As String = Chr(27) + Chr(60)
    Public Const _Espacios As String = " "
    Public prn As New RawPrinterHelper
    Public _PrinterLong As Integer
    Public _PrinterName As String
    Public _PrinterFontSize As Integer

#Region "Imprime Una Factura Electronica"

    Public Sub StartPrint()
        prn.OpenPrint(_PrinterName)
    End Sub

    'Imprime la cabecera de la empresa, contiene los datos de la misma
    '   1) Nombre de la empresa
    '   2) Cedula
    '   3) Sucursal
    '   4) Direccion
    '   5) Telefono
    '   6) Email
    Public Sub PrintHeader(ByVal p_Empresa As String, ByVal p_Datos As Array)
        '   " ===================================   "
        '   "          NombreEmpresa                "
        '   "    =============================      "
        If My.Settings.FontSize = 1 Then
            Print(eInit + eSmlText + eCentre + "".PadLeft((_PrinterLong - 2), "="))
            Print(eBigText + eNegritaOn + eCentre + p_Empresa + eNegritaOff)
            Print(eSmlText + "".PadLeft((_PrinterLong - 8), "="))
        Else
            Print(eInit + eNmlText + eCentre + "".PadLeft((_PrinterLong - 2), "="))
            Print(eBigText + eNegritaOn + eCentre + p_Empresa + eNegritaOff)
            Print(eNmlText + "".PadLeft((_PrinterLong - 8), "="))
        End If

        For i = 0 To p_Datos.Length - 1
            Print(p_Datos(i))
        Next
        Print(" ")
    End Sub

    'Imprime los datos de la venta, el cliente y otros datos
    ' 1) Tipo Documento (Tiquete - Factura - Proforma)
    ' 2) Nombre del Cliente
    ' 3) Numero de Documento
    ' 4) Fecha
    ' 5) Clave Factura Electronica
    Public Sub PrintDetalles(ByVal p_TipoDocumento As String, ByVal p_Cliente As String, ByVal p_Detalles As Array)

        Print(eCentre + eNegritaOn + p_TipoDocumento)
        Print(eLeft + p_Cliente + eNegritaOff)

        For Each Value As String In p_Detalles
            If Value <> "" Then
                If My.Settings.FontSize = 1 Then
                    Print(eSmlText + Value)
                Else
                    Print(eNmlText + Value)
                End If

            End If
        Next
        Print(" ")

    End Sub

    Public Sub PrintTiqueteElectonico(ByVal p_tiquete As Array)

        Dim _LineaNumero As Integer = 0
        Dim _Productos As Boolean = False
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _PrinterLong - 23
        Dim _Cantidad As String = ""
        Dim _Descripcion As String = ""
        Dim _Subtotal As String = ""
        Dim _Codigo As String = ""

        'IMPRIME LOS ENCABEZADOS DE LA CANTIDAD DESCRIPCION Y PRECIOS
        '   " CANT. COD/ DESCRIPCION    SUBTOTAL C/. "
        If My.Settings.FontSize = 1 Then
            Println(eSmlText + "  CANT.  ") '9
            Println("COD/ DESCRIPCION") '16
            Println("".PadLeft((_PrinterLong - 37), _Espacios))
            Print("SUBTOTAL C/.") '12

            Println(eSmlText + "  -----  ")
            Println("".PadLeft((_TamannoDescripcion - 1), "-"))
            Print(" -------------")
        Else
            Println(eNmlText + "  CANT.  ") '9
            Println("COD/ DESCRIPCION") '16
            Println("".PadLeft((_PrinterLong - 37), _Espacios))
            Print("SUBTOTAL C/.") '12

            Println(eNmlText + "  -----  ")
            Println("".PadLeft((_TamannoDescripcion - 1), "-"))
            Print(" -------------")
        End If

        'A partir de la linea 15 casi siempre enpiezan los productos,
        '_UltimaLinea verifica si la linea corresponde a un producto
        Dim _UltimaLinea As Boolean = False
        For i As Integer = 15 To p_tiquete.Length - 1
            'Al llegar al Subtotal se da por hecho que no hay mas articulos que imprimir
            If p_tiquete(i).ToString = "SubTotal" Then
                _UltimaLinea = True
            End If

            'Envia la linea del producto para dividirlos en _Cantidad, _Descripcion, _Codigo, _Subtotal
            'Verifico por medio de _Productos, si aparece empieza a enviar los productos a imprimir
            If p_tiquete(i).Contains("/") And _Productos Then
                'Envia la linea del producto para dividirlos en _Cantidad, _Descripcion, _Codigo, _Subtotal
                DivideDetalleProductos(p_tiquete(i).ToString, _Cantidad, _Descripcion, _Codigo, _Subtotal)
                'Imprime los detalles
                ImprimeDetalleProductos(_Cantidad, _Descripcion, _Subtotal, _Codigo)
                'Imprimir descuentos si existen
            ElseIf p_tiquete(i).Contains("Desc: ") Then
                Println("".PadRight(9, _Espacios))
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("IVA:") Then
                Println("".PadRight(9, _Espacios))
                Print(p_tiquete(i).ToString)
            End If

            If p_tiquete(i).Contains("Exentas") Or p_tiquete(i).Contains("Gravadas") Then
                _LineaNumero = i
                Print(" ")
                Exit For
            End If

            If p_tiquete(i).ToString = "Cant	Uni / Cod / Producto	Total" Then
                _Productos = True
            End If

        Next

        For i As Integer = _LineaNumero To p_tiquete.Length - 2

            If p_tiquete(i).Contains("Exentas") Or p_tiquete(i).Contains("Gravadas") Then
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("Subtotal") Then
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("Descuento:") Then
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("Total IVA") Or p_tiquete(i).Contains("I.Ventas:") Then
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("Otros Imp") Then
                Print(p_tiquete(i).ToString)
            ElseIf p_tiquete(i).Contains("Total:" & vbTab) Then
                PrintDashes()
                _ImprimirLinea = p_tiquete(i).ToString

                If My.Settings.FontSize = 1 Then
                    Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eSmlText)
                Else
                    Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eNmlText)
                End If

                PrintDashes()
            End If

            If p_tiquete(i).ToString.Contains("Comentarios") Then
                _LineaNumero = i
                _ImprimirLinea = p_tiquete(i).ToString.Replace("%", " ").ToUpper
                Print(eLeft + eNegritaOn + _ImprimirLinea + eNegritaOff)
            End If

            If p_tiquete(i).ToString.Contains("Documento emitido conforme lo establecido") Or p_tiquete(i).ToString.Contains("Documento electronico emitido mediante") Then
                _ImprimirLinea = p_tiquete(i).ToString & " " & p_tiquete(i + 1).ToString
                Print(eLeft + _ImprimirLinea)
                _ImprimirLinea = p_tiquete(i + 2).ToString
                Print(eLeft + _ImprimirLinea)
                Print(" ")
            End If

            If p_tiquete(i).ToString.Contains("Vuelto") Then
                _ImprimirLinea = p_tiquete(i).ToString
                Print(eLeft + _ImprimirLinea)
            End If
        Next

    End Sub

    'Divide los datos de los productos en Cantidad, Precio, Descripcion, Subtotal y Codigo
    '20	kg / N3755 / BOLSA NEGRA 37X55	26,000.00
    Private Sub DivideDetalleProductos(ByVal pLinea As String, ByRef pCantidad As String, ByRef pDescripcion As String, ByRef pCodigo As String, ByRef pSubtotal As String)

        '_Distancia es la cantidad de letras que imprime en el Substring
        Dim _Distancia As Integer = 0
        Dim _Slash As Integer = 0
        Dim _Slash2 As Integer = 0

        Dim str As [String] = pLinea
        Dim _PrimerTab As Integer = 0
        Dim _SegundoTab As Integer = 0
        Dim _BarraIncl1 As Integer = 0
        Dim _BarraIncl2 As Integer = 0
        Dim _FinDescripcion As Integer = 0
        'Dim temp As String = str.Replace(" ", "+")
        'Clipboard.SetText(temp)

        For i As Integer = 0 To str.Length - 1

            If str(i) = vbTab And _PrimerTab = 0 Then
                'Guarda la primera tabulacion, para poder seleccionar la cantidad
                _PrimerTab = i
            ElseIf str(i) = vbTab And _PrimerTab > 0 Then
                'Guarda la segunda tabulacion, entre la primera y la segunda se encuentra la descripcion
                _SegundoTab = i
            ElseIf str(i) = "/" And _Slash = 0 Then
                'El slash fija el punto de Inicio en el codigo, para seleccionar codigo y descripcion
                _Slash = i + 1
            ElseIf str(i) = "/" And _Slash <> 0 Then
                _Slash2 = i
            End If

        Next

        '20	kg
        pCantidad = pLinea.Substring(0, _PrimerTab)

        _Distancia = _SegundoTab - (_Slash)
        '/ N3755 /
        pCodigo = pLinea.Substring(_Slash, (_Slash2 - _Slash)).ToUpper

        _Distancia = _SegundoTab - (_Slash2 + 2)
        'BOLSA NEGRA 37X55
        pDescripcion = pLinea.Substring(_Slash2 + 2, _Distancia).ToUpper

        _Distancia = pLinea.Length - (_SegundoTab + 1)
        '26,000.00
        pSubtotal = pLinea.Substring(_SegundoTab + 1, _Distancia)

    End Sub

    Public Sub ImprimeDetalleProductos(ByVal p_cantidad As String, ByVal p_descripcion As String, ByVal p_subtotal As String, ByVal pCodigo As String)

        'EL NUMERO 23 CORRESPONDEN A
        '    7 ESPACIOS RESERVADOS PARA LA CANTIDAD DE PRODUCTO
        '    1 ESPACIO ENTRE CANTIDAD Y PRODUCTO
        '    1 ESPACIO ENTRE PRODUCTO Y SUBTOTAL
        '    14 ESPACIOS ENTRE PRODUCTO Y SUBTOTAL
        'SUMADOS TODOS LOS ANTERIORES DAN 23,
        'LO QUE SOBRA DE LA LONGITUD MAXIMA DE LETRAS QUE SE PUEDE IMPRIMIR EN EL TIQUETE
        'SE UTILIZA PARA EL PRODUCTO

        Dim _EspaciosReservados As Integer = 23
        Dim _EspaciosCantidad As Integer = 7
        Dim _EspaciosSubtotal As Integer = 13

        Dim _TamannoDescripcion As Integer = _PrinterLong - _EspaciosReservados
        Dim _DescripcionTemporal As String = p_descripcion
        Dim _Cantidad As String = p_cantidad
        pCodigo = pCodigo & " /"

        'IMPRIME LA CANTIDAD DEL ARTICULO
        If _Cantidad.Length <= _EspaciosCantidad Then
            Println("".PadRight(_EspaciosCantidad - _Cantidad.Length, _Espacios))
            Println(_Cantidad)
            Println("  ")
        Else
            Println("####.##")
            Println("  ")
        End If

        If p_descripcion.Length > _TamannoDescripcion Then
            _DescripcionTemporal = p_descripcion.Substring(0, _TamannoDescripcion) & "_"
        End If

        'IMPRIME LA DESCRIPCION
        If p_descripcion.Length > _TamannoDescripcion Then

            'SI LA DESCRIPCION SOBREPASA LOS 18 CARACTERES IMPRIME UNA PRIMERA PARTE
            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

            Println(_DescripcionTemporal)
            Println("".PadRight((_TamannoDescripcion + 1) - _DescripcionTemporal.Length, _Espacios))

            'IMPRIME EL TOTAL POR ARTICULO
            If p_subtotal.Length <= _EspaciosSubtotal Then
                Println("".PadRight(_EspaciosSubtotal - p_subtotal.Length, _Espacios))
                Print(p_subtotal)
            Else
                Print("##.###.###.##")
            End If

            Println("".PadRight(9, _Espacios))
            Print(p_descripcion.Substring((_TamannoDescripcion), p_descripcion.Length - (_TamannoDescripcion)).TrimStart)
            Println("".PadRight(9, _Espacios))
            Println("Cod: / ")
            Print(pCodigo)
        Else

            Println(p_descripcion)
            Println("".PadRight((_TamannoDescripcion + 1) - p_descripcion.Length, _Espacios))

            'IMPRIME EL TOTAL POR ARTICULO
            If p_subtotal.Length <= _EspaciosSubtotal Then
                Println("".PadRight(_EspaciosSubtotal - p_subtotal.Length, _Espacios))
                Print(p_subtotal)
            Else
                Print("##.###.###.##")
            End If
            Println("".PadRight(9, _Espacios))
            Println("Cod: / ")
            Print(pCodigo)
        End If

    End Sub

    'Imprime el Pie de Pagina del Documento
    ' 1) Varios Avisos que se quieran imprimir al final del documento
    Public Sub PrintFooterTiquete(ByVal pTiquete As Array)

        If pTiquete.ToString().Contains("Proforma") Then
            Print(eCentre + "*** LOS PRECIOS PUEDEN VARIAR SIN PREVIO AVISO ***")
        End If
        Print(eCentre + "Gracias Por Su Compra!")
        Print(vbLf + vbLf + vbLf + vbLf + vbLf + eCut + eDrawer)

    End Sub

    'Imprime una serie de lineas esteticas en el documento
    'Para dar enfasis a elementos
    ' "------------------------------------------"
    ' "            TOTAL: CRC 123.000.45         "
    ' "------------------------------------------"
    Public Sub PrintDashes()
        If My.Settings.FontSize = 1 Then
            Print(eLeft + eSmlText + "-".PadRight(_PrinterLong, "-"))
        Else
            Print(eLeft + eNmlText + "-".PadRight(_PrinterLong, "-"))
        End If
    End Sub

    Public Sub EndPrint()
        prn.ClosePrint()
    End Sub

#End Region

    Public Sub Print(Line As String)
        prn.SendStringToPrinter(_PrinterName, Line + vbLf)
    End Sub

    Public Sub Println(Line As String)
        prn.SendStringToPrinter(_PrinterName, Line)
    End Sub

#Region "Tiquetes BN"

    'Imprime los tiquetes del Banco Nacional
    Public Sub PrintTiqueteBn(ByVal linea1 As TextBox)

        Print(eDrawer)
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _PrinterLong - 23

        For i As Integer = 0 To linea1.Lines().Length - 2
            _ImprimirLinea = linea1.Lines(i).ToString

            If i = 0 Then
                'Imprime Nombre de la empresa
                If My.Settings.FontSize = 1 Then
                    Print(eSmlText + eNegritaOn + eCentre + _ImprimirLinea + eNegritaOff)
                    Print(eSmlText + eCentre + linea1.Lines(1).ToString + eNegritaOff)
                    Print(" ")
                Else
                    Print(eNmlText + eNegritaOn + eCentre + _ImprimirLinea + eNegritaOff)
                    Print(eNmlText + eCentre + linea1.Lines(1).ToString + eNegritaOff)
                    Print(" ")
                End If

            End If
            If i > 1 Then
                Print(eLeft + linea1.Lines(i).ToString)
            End If
        Next

    End Sub

    Public Sub PrintFooterBn()
        Print(vbLf + vbLf + vbLf + vbLf + vbLf + vbLf + eCut)
    End Sub

#End Region

End Module