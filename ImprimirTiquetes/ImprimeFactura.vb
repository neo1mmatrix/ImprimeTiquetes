Imports System.Text.RegularExpressions

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
    Public Const eNmlText As String = Chr(27) + "!" + Chr(0)
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
    Dim _LongitudImpresion As Integer = 40
    Public Const _Espacios As String = " "
    Public PrinterNameTermica As String = "EPSON TERMICA"

    Public Sub StartPrint()
        prn.OpenPrint(PrinterNameTermica)
    End Sub

    Public Sub StartPrintTiquete(ByVal p_longitudImpresion As Integer)
        prn.OpenPrint(PrinterNameTermica)
        _LongitudImpresion = p_longitudImpresion
    End Sub

    Public Sub PrintTiqueteBn(ByVal linea1 As TextBox)

        Print(eDrawer)
        Dim _LineaNumero As Integer = 0
        Dim temp As String = ""
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _LongitudImpresion - 23
        Dim _LineaDetalles As Integer = 0
        Dim _Cantidad As String = ""
        Dim _Descripcion As String = ""
        Dim _Subtotal As String = ""

        For i As Integer = 0 To linea1.Lines().Length - 1
            _ImprimirLinea = linea1.Lines(i).ToString

            If i = 0 Then
                'Imprime Nombre de la empresa
                Print(eSmlText + eNegritaOn + eCentre + _ImprimirLinea + eNegritaOff)
                Print(eSmlText + eCentre + linea1.Lines(1).ToString + eNegritaOff)
                Print(" ")
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
        Dim temp As String = ""
        Dim _ImprimirLinea As String = ""
        Dim _TamannoDescripcion As Integer = _LongitudImpresion - 23
        Dim _LineaDetalles As Integer = 0
        Dim _Cantidad As String = ""
        Dim _Descripcion As String = ""
        Dim _Subtotal As String = ""

        'IMPRIME LOS ENCABEZADOS DE LA CANTIDAD DESCRIPCION Y PRECIOS
        Println(eSmlText + "  CANT.  ") '9
        Println("COD/ DESCRIPCION") '16
        Println("".PadLeft((_LongitudImpresion - 37), _Espacios))
        Print("SUBTOTAL C/.") '12

        Println(eSmlText + "  -----  ")
        Println("".PadLeft((_TamannoDescripcion - 1), "-"))
        Print(" -------------")

        For i As Integer = 20 To p_tiquete.Lines().Length - 1
            _ImprimirLinea = p_tiquete.Lines(i).ToString

            _LineaNumero = 20
            Dim _UltimaLinea As Boolean = False

                While Not _UltimaLinea

                    If p_tiquete.Lines(_LineaNumero).ToString = "SubTotal" Then
                        _UltimaLinea = True
                    Else
                        For j As Integer = 0 To 2
                            _Cantidad = p_tiquete.Lines(_LineaNumero).ToString
                            _Cantidad = _Cantidad.Replace(",", "")
                            _Cantidad = _Cantidad.Replace(".", ",")
                            _Descripcion = Regex.Replace(p_tiquete.Lines(_LineaNumero + 1).ToString, "\s{2,}", " ")
                            _Subtotal = p_tiquete.Lines(_LineaNumero + 2).ToString
                        Next

                        PrintTiqueteDetalles(_Cantidad, _Descripcion, _Subtotal)
                        _LineaNumero = _LineaNumero + 3
                    End If

            End While

        Next
        '    If p_tiquete.Lines(i).ToString.Equals("Sub Total") Then
        '        Print(" ")
        '        _ImprimirLinea = p_tiquete.Lines(i).ToString + ": " + p_tiquete.Lines(i + 1).ToString
        '        Print(eRight + _ImprimirLinea)
        '    End If

        '    If p_tiquete.Lines(i).ToString.Equals("I.V.") Then
        '        _ImprimirLinea = p_tiquete.Lines(i).ToString + ": " + p_tiquete.Lines(i + 1).ToString
        '        Print(eRight + _ImprimirLinea)
        '    End If

        '    If p_tiquete.Lines(i).ToString.Contains("Imp. S") Then
        '        PrintDashes()
        '        _ImprimirLinea = p_tiquete.Lines(i + 2).ToString + ": " + p_tiquete.Lines(i + 3).ToString + " " + p_tiquete.Lines(i + 4).ToString
        '        Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eSmlText)
        '        PrintDashes()
        '    End If


        '    If p_tiquete.Lines(i).ToString.Contains("Autorizada") Then
        '        _ImprimirLinea = p_tiquete.Lines(i - 1).ToString
        '        Print(eLeft + _ImprimirLinea)
        '        _ImprimirLinea = p_tiquete.Lines(i).ToString
        '        Print(eLeft + _ImprimirLinea)
        '        _ImprimirLinea = p_tiquete.Lines(i + 1).ToString
        '        Print(eLeft + _ImprimirLinea)
        '        _ImprimirLinea = p_tiquete.Lines(i + 2).ToString
        '        Print(eLeft + _ImprimirLinea)
        '        Print(" ")
        '    End If

        '    If p_tiquete.Lines(i).ToString.Contains("Comentarios") Then
        '        _LineaNumero = i
        '        _ImprimirLinea = p_tiquete.Lines(i).ToString + " " + p_tiquete.Lines(_LineaNumero + 1).ToString
        '        Print(eLeft + _ImprimirLinea)
        '        _LineaNumero = i + 2
        '        While _LineaNumero <> (p_tiquete.Lines().Length)
        '            _ImprimirLinea = p_tiquete.Lines(_LineaNumero).ToString
        '            Print(eLeft + _ImprimirLinea)
        '            _LineaNumero += 1
        '        End While
        '    End If

        '    If p_tiquete.Lines(i).ToString.Contains("Vuelto") Then
        '        _ImprimirLinea = p_tiquete.Lines(i).ToString
        '        Print(eLeft + _ImprimirLinea)
        '    End If
        'Next
    End Sub

    Public Sub PrintTiqueteDetalles(ByVal p_cantidad As String, ByVal p_descripcion As String, ByVal p_subtotal As String)

        Dim _TamannoDescripcion As Integer = _LongitudImpresion - 23
        Dim _DescripcionTemporal As String = p_descripcion
        Dim _Cantidad As String = convertir_formato_miles_decimales(CDbl(p_cantidad))
        _Cantidad = _Cantidad.Replace(",", ".")
        'IMPRIME LA CANTIDAD DEL ARTICULO
        If _Cantidad.Length <= 7 Then
            Println("".PadRight(7 - _Cantidad.Length, _Espacios))
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
            If p_subtotal.Length <= 13 Then
                Println("".PadRight(13 - p_subtotal.Length, _Espacios))
                Print(p_subtotal)
            Else
                Print("##.###.###.##")
            End If

            Println("".PadRight(9, _Espacios))
            Print(p_descripcion.Substring((_TamannoDescripcion - 1), p_descripcion.Length - (_TamannoDescripcion - 1)))

        Else

            Println(p_descripcion)
            Println("".PadRight((_TamannoDescripcion + 1) - p_descripcion.Length, _Espacios))

            'IMPRIME EL TOTAL POR ARTICULO
            If p_subtotal.Length <= 13 Then
                Println("".PadRight(13 - p_subtotal.Length, _Espacios))
                Print(p_subtotal)
            Else
                Print("##.###.###.##")
            End If
        End If

    End Sub

    Public Sub PrintHeader(ByVal p_Empresa As String, ByVal p_Datos As Array)
        Print(eInit + eSmlText + eCentre + "==============================================")
        Print(eBigText + eNegritaOn + eCentre + p_Empresa + eNegritaOff)
        Print(eSmlText + "========================================")
        For i = 0 To p_Datos.Length - 1
            Print(p_Datos(i))
        Next
        Print(" ")
    End Sub

    Public Sub PrintDetalles(ByVal p_TipoDocumento As String, ByVal p_Cliente As String, ByVal p_Detalles As Array)

        Print(eCentre + eNegritaOn + p_TipoDocumento + eCentre)
        Print(eLeft + p_Cliente + eNegritaOff)

        For Each Value As String In p_Detalles
            Print(Value)
        Next
        Print(" ")

    End Sub


    Public Sub PrintBody(ByVal p_tiquete As TextBox)

        Dim impt As Double = Nothing
        Dim impSuma As Double = 0
        Dim printImp As String = Nothing
        Dim precioOriginal As Double = Nothing

        For i = 0 To Val(p_tiquete.Lines.Count) - 1

        Next

        Try
            Dim cantpr, descrpr, subtpr, precpr, tempdesc As String

            Println(eSmlText + "  CANT.  ")
            Println("COD/ DESCRIPCION          ")
            Print("SUBTOTAL C/.")

            Println(eSmlText + "  -----  ")
            Println("-----------------------   ")
            Print("-------------")

            'For Each item As DataGridViewRow In DGV.Rows
            '    cantpr = CStr(item.Cells(2).Value)
            '    descrpr = CStr(item.Cells(1).Value)
            '    tempdesc = CStr(item.Cells(1).Value)
            '    subtpr = CStr(item.Cells(5).Value)
            '    precpr = CStr(item.Cells(3).Value)
            '    impt = CDbl(item.Cells(4).Value)

            '    If descrpr <> "" Then
            '        If descrpr.Length > 25 Then
            '            tempdesc = descrpr.Substring(0, 24) & "_"
            '        End If
            '    End If

            '    'REVISA SI EXISTEN DATOS A IMPRIMIR
            '    If cantpr <> "" Then
            '        If cantpr.Contains(".") Then
            '            cantpr = cantpr.Replace(".", "")
            '        End If

            '        'CHEQUEAR SI PRODUCTOS INCLUYEN IMPUESTOS
            '        If impt > 0 Then
            '            precioOriginal = (CDbl(subtpr) / ((impt / 100) + 1))
            '            impSuma += (CDbl(precioOriginal) * (impt / 100))
            '            subtpr = String.Format("{0:n}", (CDbl(subtpr) / ((impt / 100) + 1)))

            '            'IMPRIME LA CANTIDAD E INCLUYE ASTERISCO A LA CANTIDAD
            '            If cantpr.Length <= 7 Then
            '                Println("".PadRight(7 - cantpr.Length, " "))
            '                Println(cantpr)
            '                Println("* ")
            '            Else
            '                Println("####.##")
            '                Println("* ")
            '            End If


            '            'IMPRIME LA DESCRIPCION
            '            If descrpr.Length > 25 Then

            '                'SI LA DESCRIPCION SOBREPASA LOS 25 CARACTERES IMPRIME UNA PRIMERA PARTE
            '                'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

            '                Println(tempdesc)
            '                Println("".PadRight(26 - tempdesc.Length, " "))

            '                'IMPRIME EL TOTAL POR ARTICULO
            '                If subtpr.Length <= 13 Then
            '                    Println("".PadRight(13 - subtpr.Length, " "))
            '                    Print(subtpr)
            '                Else
            '                    Print("##.###.###.##")
            '                End If

            '                Println("".PadRight(9, " "))
            '                Print(descrpr.Substring(24, descrpr.Length - 24))

            '            Else

            '                Println(descrpr)
            '                Println("".PadRight(26 - descrpr.Length, " "))

            '                'IMPRIME EL TOTAL POR ARTICULO
            '                If subtpr.Length <= 13 Then
            '                    Println("".PadRight(13 - subtpr.Length, " "))
            '                    Print(subtpr)
            '                Else
            '                    Print("##.###.###.##")
            '                End If

            '            End If

            '            'CALCULA LOS PRECIOS SIN IMPUESTOS
            '        Else
            '            'IMPRIME LA CANTIDAD DEL ARTICULO
            '            If cantpr.Length <= 7 Then
            '                Println("".PadRight(7 - cantpr.Length, " "))
            '                Println(cantpr)
            '                Println("  ")
            '            Else
            '                Println("####.##")
            '                Println("  ")
            '            End If

            '            'IMPRIME LA DESCRIPCION
            '            If descrpr.Length > 25 Then

            '                'SI LA DESCRIPCION SOBREPASA LOS 18 CARACTERES IMPRIME UNA PRIMERA PARTE
            '                'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

            '                Println(tempdesc)
            '                Println("".PadRight(26 - tempdesc.Length, " "))

            '                'IMPRIME EL TOTAL POR ARTICULO
            '                If subtpr.Length <= 13 Then
            '                    Println("".PadRight(13 - subtpr.Length, " "))
            '                    Print(subtpr)
            '                Else
            '                    Print("##.###.###.##")
            '                End If

            '                Println("".PadRight(9, " "))
            '                Print(descrpr.Substring(24, descrpr.Length - 24))

            '            Else

            '                Println(descrpr)
            '                Println("".PadRight(26 - descrpr.Length, " "))

            '                'IMPRIME EL TOTAL POR ARTICULO
            '                If subtpr.Length <= 13 Then
            '                    Println("".PadRight(13 - subtpr.Length, " "))
            '                    Print(subtpr)
            '                Else
            '                    Print("##.###.###.##")
            '                End If

            '            End If

            '        End If

            '        'IMPRIME EL PRECIO INDIVIDUAL DEL ARTICULO
            '        Println("".PadRight(9, " "))
            '        Println("Precio   ")
            '        If precpr.Length <= 12 Then
            '            Println("".PadRight(12 - precpr.Length, " "))
            '            Print(precpr)
            '        Else
            '            Print("#.###.###.##")
            '        End If

            '    End If

            'Next

            ''Print(" ")

            ''subt = String.Format("{0:n}", (CDbl(subt) - impSuma))
            ''IMPRIME EL SUBTOTAL DE LA COMPRA
            ''Println(eLeft + "".PadRight(20, " "))
            ''Println(eSmlText + "SubTotal: ")
            ''Println("".PadRight(18 - subt.Length, " "))
            ''Print(subt)

            ''IMPRIME EL IMPUESTO SI ES MAYOR A O
            ''If impSuma > 0 Then
            ''    printImp = String.Format("{0:n}", CDbl(impSuma))
            ''    Println(eLeft + " ".PadRight(20, " "))
            ''    Println(eSmlText + "Impt: ")
            ''    Println("".PadRight(22 - printImp.Length, " "))
            ''    Print(printImp)
            ''End If

            ''IMPRIME  EL DESCUENTO SI ES MAYOR A CERO
            ''If descu > 0 Then
            ''    descu = String.Format("{0:n}", CDbl(descu))
            ''    Println(eLeft + "".PadRight(20, " "))
            ''    Println("Descuento: ")
            ''    Println("".PadRight(17 - descu.Length, " "))
            ''    Print(descu)
            ''End If

            ''IMPRIME EL TOTAL DE LA FACTURA
            ''PrintDashes()
            ''Println(eLeft + "".PadRight(10, " "))
            ''Println(eBigText + eNegritaOn + "TOTAL: ")
            ''Println("".PadRight(20 - total.Length, " "))
            ''Print(total + eNegritaOff + eSmlText)
            ''PrintDashes()

        Catch ex As Exception
            MsgBox(ex.ToString)
            Logger.e("Error con excepción", ex, New StackFrame(True))
        End Try

    End Sub



    Public Sub PrintFooter(ByRef p_pie_de_factura)

        Print(vbLf + vbLf + vbLf + vbLf + vbLf + eCut + eDrawer)

    End Sub

    Public Sub PrintFooterTiquete()

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
        Print(eLeft + eSmlText + "-".PadRight(48, "-"))
    End Sub

    Public Sub EndPrint()
        prn.ClosePrint()
    End Sub

End Module
