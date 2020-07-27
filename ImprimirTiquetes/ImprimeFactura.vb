﻿Imports System.Text.RegularExpressions

Module ImprimeFactura

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
    Public prn As New RawPrinterHelper
    Public _LongitudImpresion As Integer
    Public Const _Espacios As String = " "
    'Para imprimir con conexion local se necesita colocar
    '\\Desktop-feria\epson termica
    Public PrinterNameTermica As String

    Public Sub StartPrint()
        prn.OpenPrint(PrinterNameTermica)
    End Sub

    Public Sub StartPrintTiquete(ByVal p_longitudImpresion As Integer)
        prn.OpenPrint(PrinterNameTermica)
        _LongitudImpresion = p_longitudImpresion
    End Sub

    Public Sub PrintTiqueteBn(ByVal linea1 As TextBox)

        Print(eDrawer)
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _LongitudImpresion - 23

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

    Public Sub PrintTiqueteElectonico(ByVal p_tiquete As TextBox)

        Dim _LineaNumero As Integer = 0
        Dim _Productos As Boolean = False
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _LongitudImpresion - 23
        Dim _Cantidad As String = ""
        Dim _Descripcion As String = ""
        Dim _Subtotal As String = ""
        Dim _Codigo As String = ""

        'IMPRIME LOS ENCABEZADOS DE LA CANTIDAD DESCRIPCION Y PRECIOS
        If My.Settings.FontSize = 1 Then
            Println(eSmlText + "  CANT.  ") '9
            Println("COD/ DESCRIPCION") '16
            Println("".PadLeft((_LongitudImpresion - 37), _Espacios))
            Print("SUBTOTAL C/.") '12

            Println(eSmlText + "  -----  ")
            Println("".PadLeft((_TamannoDescripcion - 1), "-"))
            Print(" -------------")
        Else
            Println(eNmlText + "  CANT.  ") '9
            Println("COD/ DESCRIPCION") '16
            Println("".PadLeft((_LongitudImpresion - 37), _Espacios))
            Print("SUBTOTAL C/.") '12

            Println(eNmlText + "  -----  ")
            Println("".PadLeft((_TamannoDescripcion - 1), "-"))
            Print(" -------------")
        End If

        Dim _UltimaLinea As Boolean = False
        For i As Integer = 15 To p_tiquete.Lines().Length - 1

            If p_tiquete.Lines(i).ToString = "SubTotal" Then
                _UltimaLinea = True
            End If

            If p_tiquete.Lines(i).Contains("/") And _Productos Then
                ProductoDetallesCod(p_tiquete.Lines(i).ToString, _Cantidad, _Descripcion, _Codigo, _Subtotal)
                PrintTiqueteDetallesCod(_Cantidad, _Descripcion, _Subtotal, _Codigo)
            ElseIf p_tiquete.Lines(i).Contains("Desc: ") Then
                Println("".PadRight(9, _Espacios))
                Print(p_tiquete.Lines(i).ToString)
            End If

            If p_tiquete.Lines(i).Contains("Exentas") Or p_tiquete.Lines(i).Contains("Gravadas") Then
                _LineaNumero = i
                Print(" ")
                Exit For
            End If

            If p_tiquete.Lines(i).ToString = "Cant	Uni / Cod / Producto	Total" Then
                _Productos = True
            End If

        Next

        For i As Integer = _LineaNumero To p_tiquete.Lines().Length - 2

            If p_tiquete.Lines(i).Contains("Exentas") Or p_tiquete.Lines(i).Contains("Gravadas") Then
                Print(p_tiquete.Lines(i).ToString)
            ElseIf p_tiquete.Lines(i).Contains("Subtotal") Then
                Print(p_tiquete.Lines(i).ToString)
            ElseIf p_tiquete.Lines(i).Contains("Descuento:") Then
                Print(p_tiquete.Lines(i).ToString)
            ElseIf p_tiquete.Lines(i).Contains("Total IVA") Or p_tiquete.Lines(i).Contains("I.Ventas:") Then
                Print(p_tiquete.Lines(i).ToString)
            ElseIf p_tiquete.Lines(i).Contains("Otros Imp") Then
                Print(p_tiquete.Lines(i).ToString)
            ElseIf p_tiquete.Lines(i).Contains("Total:" & vbTab) Then
                PrintDashes()
                _ImprimirLinea = p_tiquete.Lines(i).ToString

                If My.Settings.FontSize = 1 Then
                    Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eSmlText)
                Else
                    Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eNmlText)
                End If

                PrintDashes()
            End If

            If p_tiquete.Lines(i).ToString.Contains("Comentarios") Then
                _LineaNumero = i
                _ImprimirLinea = p_tiquete.Lines(i).ToString.Replace("%", " ").ToUpper
                Print(eLeft + eNegritaOn + _ImprimirLinea + eNegritaOff)
            End If

            If p_tiquete.Lines(i).ToString.Contains("Documento emitido conforme lo establecido") Or p_tiquete.Lines(i).ToString.Contains("Documento electronico emitido mediante") Then
                _ImprimirLinea = p_tiquete.Lines(i).ToString & " " & p_tiquete.Lines(i + 1).ToString
                Print(eLeft + _ImprimirLinea)
                _ImprimirLinea = p_tiquete.Lines(i + 2).ToString
                Print(eLeft + _ImprimirLinea)
                Print(" ")
            End If

            If p_tiquete.Lines(i).ToString.Contains("Vuelto") Then
                _ImprimirLinea = p_tiquete.Lines(i).ToString
                Print(eLeft + _ImprimirLinea)
            End If

        Next

    End Sub

    Public Sub PrintTiqueteDetalles(ByVal p_cantidad As String, ByVal p_descripcion As String, ByVal p_subtotal As String)

        'EL NUMERO 23 CORRESPONDEN A 
        '    7 ESPACIOS RESERVADOS PARA LA CANTIDAD DE PRODUCTO
        '    1 ESPACIO ENTRE CANTIDAD Y PRODUCTO
        '    1 ESPACIO ENTRE PRODUCTO Y SUBTOTAL
        '    14 ESPACIOS ENTRE PRODUCTO Y SUBTOTAL
        'SUMADOS TODOS LOS ANTERIORES DAN 23, LO QUE SOBRE DE LA LONGITUD MAXIMA DE LETRAS SE UTILIZA PARA EL PRODUCTO

        Dim _EspaciosReservados As Integer = 23
        Dim _EspaciosCantidad As Integer = 7
        Dim _EspaciosSubtotal As Integer = 13

        Dim _TamannoDescripcion As Integer = _LongitudImpresion - _EspaciosReservados
        Dim _DescripcionTemporal As String = p_descripcion
        Dim _Cantidad As String = p_cantidad

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
            _DescripcionTemporal = p_descripcion.Substring(0, _TamannoDescripcion - 1) & "_"
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
            Print(p_descripcion.Substring((_TamannoDescripcion - 1), p_descripcion.Length - (_TamannoDescripcion)).TrimStart)

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
        End If

    End Sub

    Public Sub PrintTiqueteDetallesCod(ByVal p_cantidad As String, ByVal p_descripcion As String, ByVal p_subtotal As String, ByVal pCodigo As String)

        'EL NUMERO 23 CORRESPONDEN A 
        '    7 ESPACIOS RESERVADOS PARA LA CANTIDAD DE PRODUCTO
        '    1 ESPACIO ENTRE CANTIDAD Y PRODUCTO
        '    1 ESPACIO ENTRE PRODUCTO Y SUBTOTAL
        '    14 ESPACIOS ENTRE PRODUCTO Y SUBTOTAL
        'SUMADOS TODOS LOS ANTERIORES DAN 23, LO QUE SOBRE DE LA LONGITUD MAXIMA DE LETRAS SE UTILIZA PARA EL PRODUCTO

        Dim _EspaciosReservados As Integer = 23
        Dim _EspaciosCantidad As Integer = 7
        Dim _EspaciosSubtotal As Integer = 13

        Dim _TamannoDescripcion As Integer = _LongitudImpresion - _EspaciosReservados
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

    'Imprime la cabecera de la empresa, contiene los datos de la misma
    Public Sub PrintHeader(ByVal p_Empresa As String, ByVal p_Datos As Array)
        If My.Settings.FontSize = 1 Then
            Print(eInit + eSmlText + eCentre + "".PadLeft((_LongitudImpresion - 2), "="))
            Print(eBigText + eNegritaOn + eCentre + p_Empresa + eNegritaOff)
            Print(eSmlText + "".PadLeft((_LongitudImpresion - 6), "="))
        Else
            Print(eInit + eNmlText + eCentre + "".PadLeft((_LongitudImpresion - 2), "="))
            Print(eBigText + eNegritaOn + eCentre + p_Empresa + eNegritaOff)
            Print(eNmlText + "".PadLeft((_LongitudImpresion - 6), "="))
        End If

        For i = 0 To p_Datos.Length - 1
            Print(p_Datos(i))
        Next
        Print(" ")
    End Sub

    'Imprime los datos de la venta, el cliente y otros datos
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

    Public Sub PrintFooterTiquete(ByVal pTiquete As TextBox)

        If pTiquete.Text.Contains("Proforma") Then
            Print(eCentre + "*** LOS PRECIOS PUEDEN VARIAR SIN PREVIO AVISO ***")
        End If
        Print(eCentre + "Gracias Por Su Compra!")
        Print(vbLf + vbLf + vbLf + vbLf + vbLf + eCut + eDrawer)

    End Sub

    Public Sub Print(Line As String)
        prn.SendStringToPrinter(PrinterNameTermica, Line + vbLf)
    End Sub

    Public Sub Println(Line As String)
        prn.SendStringToPrinter(PrinterNameTermica, Line)
    End Sub
    Public Sub PrintDashes()
        If My.Settings.FontSize = 1 Then
            Print(eLeft + eSmlText + "-".PadRight(_LongitudImpresion, "-"))
        Else
            Print(eLeft + eNmlText + "-".PadRight(_LongitudImpresion, "-"))
        End If
    End Sub

    Public Sub EndPrint()
        prn.ClosePrint()
    End Sub

    'Divide los datos de los productos en Cantidad, Precio, Descripcion Y Subtotal
    Private Sub ProductoDetalles(ByVal pLinea As String, ByRef pCantidad As String, ByRef pDescripcion As String, ByRef pSubtotal As String)

        Dim _Distancia As Integer = 0
        Dim _Slash As Integer = 0

        Dim str As [String] = pLinea
        Dim _PrimerTab As Integer = 0
        Dim _SegundoTab As Integer = 0

        For i As Integer = 0 To str.Length - 1

            If str(i) = vbTab And _PrimerTab = 0 Then
                'Guarda la primera tabulacion, para poder seleccionar la cantidad
                _PrimerTab = i
            ElseIf str(i) = vbTab And _PrimerTab > 0 Then
                'Guarda la segunda tabulacion, entre la primera y la segunda se encuentra la descripcion
                _SegundoTab = i
            ElseIf str(i) = "/" And _Slash = 0 Then
                'El slash fija el punto de Inicio en el codigo, para seleccionar codigo y descripcion
                _Slash = i + 2
            End If

        Next

        pCantidad = pLinea.Substring(0, _PrimerTab)
        _Distancia = _SegundoTab - (_Slash)
        pDescripcion = pLinea.Substring(_Slash, _Distancia).ToUpper
        _Distancia = pLinea.Length - (_SegundoTab + 1)
        pSubtotal = pLinea.Substring(_SegundoTab + 1, _Distancia)

    End Sub


    'Divide los datos de los productos en Cantidad, Precio, Descripcion, Subtotal y Codigo
    Private Sub ProductoDetallesCod(ByVal pLinea As String, ByRef pCantidad As String, ByRef pDescripcion As String, ByRef pCodigo As String, ByRef pSubtotal As String)

        Dim _Distancia As Integer = 0
        Dim _Slash As Integer = 0
        Dim _Slash2 As Integer = 0

        Dim str As [String] = pLinea
        Dim _PrimerTab As Integer = 0
        Dim _SegundoTab As Integer = 0

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

        pCantidad = pLinea.Substring(0, _PrimerTab)

        _Distancia = _SegundoTab - (_Slash)
        pCodigo = pLinea.Substring(_Slash, (_Slash2 - _Slash)).ToUpper

        _Distancia = _SegundoTab - (_Slash2 + 2)
        pDescripcion = pLinea.Substring(_Slash2 + 2, _Distancia).ToUpper

        _Distancia = pLinea.Length - (_SegundoTab + 1)
        pSubtotal = pLinea.Substring(_SegundoTab + 1, _Distancia)

    End Sub
End Module
