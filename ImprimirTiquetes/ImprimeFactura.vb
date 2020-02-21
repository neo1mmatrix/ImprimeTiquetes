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

    Public Sub PrintTiqueteElectonico(ByVal linea1 As TextBox)

        Print(eDrawer)
        Print(eInit + eSmlText + eCentre + "".PadLeft((_LongitudImpresion), "="))
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
                Print(eSmlText + "  " + "".PadLeft((_LongitudImpresion - 4), "="))
                'Imprime  Cedula
                temp = linea1.Lines(1).ToString + ": "
                temp += linea1.Lines(2).ToString
                Print(eSmlText + eNegritaOff + eCentre + temp + eNegritaOff)
                'Imprime  Telefono
                temp = linea1.Lines(3).ToString + ": "
                temp += linea1.Lines(4).ToString
                Print(eSmlText + eCentre + temp + eNegritaOff)
                'Imprime Correo
                _ImprimirLinea = linea1.Lines(5).ToString
                Print(eCentre + _ImprimirLinea)
                'Imprime Direccion
                _ImprimirLinea = linea1.Lines(6).ToString
                Print(eCentre + _ImprimirLinea)
                Print(" ")
                'Imprime el Numero de la factura electronica
                _ImprimirLinea = linea1.Lines(7).ToString
                Print(eSmlText + eNegritaOn + eLeft + _ImprimirLinea + eNegritaOff)
                'Imprime  el estado (Contado o Credito)
                _ImprimirLinea = linea1.Lines(8).ToString
                Print(eSmlText + eNegritaOn + eLeft + _ImprimirLinea + eNegritaOff)
                'Imprime  un espacio en blanco
                _ImprimirLinea = linea1.Lines(9).ToString
                Print(eSmlText + eNegritaOff + eLeft + _ImprimirLinea)
                'Imprime la Clave
                temp = linea1.Lines(10).ToString + " "
                temp += linea1.Lines(11).ToString
                Print(eSmlText + eLeft + temp + eNegritaOff)
                'Imprime el Numero de Consolidado
                temp = linea1.Lines(12).ToString + " "
                temp += linea1.Lines(13).ToString
                Print(eSmlText + temp + eNegritaOff)
                'Imprime  la fecha
                temp = linea1.Lines(14).ToString + " "
                temp += linea1.Lines(15).ToString
                Print(eSmlText + temp + eNegritaOff)
                'Imprime  la hora
                temp = linea1.Lines(16).ToString + " "
                temp += linea1.Lines(17).ToString
                Print(eSmlText + temp + eNegritaOff)
                'Imprime  el Usuario que creo la factura
                temp = linea1.Lines(18).ToString + " "
                temp += linea1.Lines(19).ToString
                Print(eSmlText + temp + eNegritaOff)
                'Imprime  el nombre del cliente
                temp = linea1.Lines(20).ToString + " "
                temp += linea1.Lines(21).ToString
                Print(eSmlText + temp + eNegritaOff)
                'Imprime la cedula del cliente
                temp = linea1.Lines(22).ToString + " "
                temp += linea1.Lines(23).ToString
                Print(eSmlText + temp)
                Print(" ")
            End If

            If i = 24 Then
                Println(eSmlText + "  CANT.  ") '9
                Println("DESCRIPCION") '11
                Println("".PadLeft((_LongitudImpresion - 32), _Espacios))
                Print("SUBTOTAL C/.") '12

                Println(eSmlText + "  -----  ")
                Println("".PadLeft((_TamannoDescripcion - 1), "-"))
                Print(" -------------")
            End If

            If i = 27 Then
                _LineaNumero = 27
                Dim _UltimaLinea As Boolean = False

                While Not _UltimaLinea

                    If linea1.Lines(_LineaNumero).ToString = "Sub Total" Then
                        _UltimaLinea = True
                    Else
                        For j As Integer = 0 To 2
                            _Cantidad = linea1.Lines(_LineaNumero).ToString
                            _Cantidad = _Cantidad.Replace(",", "")
                            _Cantidad = _Cantidad.Replace(".", ",")
                            _Descripcion = Regex.Replace(linea1.Lines(_LineaNumero + 1).ToString, "\s{2,}", " ")
                            _Subtotal = linea1.Lines(_LineaNumero + 2).ToString
                        Next

                        PrintTiqueteDetalles(_Cantidad, _Descripcion, _Subtotal)
                        _LineaNumero = _LineaNumero + 3
                    End If

                End While
            End If


            If linea1.Lines(i).ToString.Equals("Sub Total") Then
                Print(" ")
                _ImprimirLinea = linea1.Lines(i).ToString + ": " + linea1.Lines(i + 1).ToString
                Print(eRight + _ImprimirLinea)
            End If

            If linea1.Lines(i).ToString.Equals("I.V.") Then
                _ImprimirLinea = linea1.Lines(i).ToString + ": " + linea1.Lines(i + 1).ToString
                Print(eRight + _ImprimirLinea)
            End If

            If linea1.Lines(i).ToString.Contains("Imp. S") Then
                PrintDashes()
                _ImprimirLinea = linea1.Lines(i + 2).ToString + ": " + linea1.Lines(i + 3).ToString + " " + linea1.Lines(i + 4).ToString
                Print(eCentre + eBigText + eNegritaOn + _ImprimirLinea + eNegritaOff + eSmlText)
                PrintDashes()
            End If


            If linea1.Lines(i).ToString.Contains("Autorizada") Then
                _ImprimirLinea = linea1.Lines(i - 1).ToString
                Print(eLeft + _ImprimirLinea)
                _ImprimirLinea = linea1.Lines(i).ToString
                Print(eLeft + _ImprimirLinea)
                _ImprimirLinea = linea1.Lines(i + 1).ToString
                Print(eLeft + _ImprimirLinea)
                _ImprimirLinea = linea1.Lines(i + 2).ToString
                Print(eLeft + _ImprimirLinea)
                Print(" ")
            End If

            If linea1.Lines(i).ToString.Contains("Comentarios") Then
                _LineaNumero = i
                _ImprimirLinea = linea1.Lines(i).ToString + " " + linea1.Lines(_LineaNumero + 1).ToString
                Print(eLeft + _ImprimirLinea)
                _LineaNumero = i + 2
                While _LineaNumero <> (linea1.Lines().Length)
                    _ImprimirLinea = linea1.Lines(_LineaNumero).ToString
                    Print(eLeft + _ImprimirLinea)
                    _LineaNumero += 1
                End While
            End If

            If linea1.Lines(i).ToString.Contains("Vuelto") Then
                _ImprimirLinea = linea1.Lines(i).ToString
                Print(eLeft + _ImprimirLinea)
            End If
        Next
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

    Public Sub PrintHeader(ByVal linea1 As String, ByVal linea2 As Array)
        Print(eInit + eSmlText + eCentre + "==============================================")
        Print(eBigText + eNegritaOn + eCentre + linea1 + eNegritaOff)
        Print(eSmlText + "========================================")
        For Each Value In linea2
            If Value <> "" Then
                Print(Value)
            End If
        Next
    End Sub


    Public Sub PrintHeaderCot(ByVal linea1 As String, ByVal linea2 As Array)

        Print(eInit + eSmlText + eCentre + "==============================================")
        Print(eBigText + eNegritaOn + eCentre + linea1 + eNegritaOff)
        Print(eSmlText + "==========================================")

        Print(eSmlText + eCentre)
        For Each Value In linea2
            If Value <> "" Then
                Print(Value)
            End If
        Next

        Print(" ")
        Print(" ")

        Print(eSmlText + eCentre + "==============================================")
        Print(eBigText + eNegritaOn + eCentre + "* PROFORMA *" + eNegritaOff)
        Print(eSmlText + "============================================")

        Print(" ")
        Print(" ")

        Print(eLeft + " ")

    End Sub

    Public Sub PrintDetalles(ByVal fecha As DateTime, ByVal factura As String, ByVal cliente As String,
                             ByVal cod As String, ByVal coment1 As String, ByVal coment2 As String,
                             ByVal tipoFactura As String, ByVal dir As String)

        Println(eLeft + "Nº")
        Println("".PadRight(12 - factura.Length, " "))
        Println(factura)
        Println("".PadRight(24, " "))
        Print(fecha.ToString("dd/MM/yyyy"))

        Println("".PadRight(26, " "))
        Print(fecha.ToString("HH:mm"))

        Print(" ")

        Print(eLeft + "NOMBRE:")
        PrintDashes()
        Print(cliente)

        If coment1 <> "" And coment2 <> "" Then
            Print(coment1)
            Print(coment2)
        ElseIf coment1 <> "" And coment2 = "" Then
            Print(coment1)
        ElseIf coment1 = "" And coment2 <> "" Then
            Print(coment2)
        End If

        If dir <> "" Then
            Print("Dir: " & dir)
        End If
        PrintDashes()

    End Sub

    Public Sub PrintDetallesCot(ByVal fecha As DateTime, ByVal factura As String, ByVal cliente As String,
                            ByVal cod As String, ByVal coment1 As String, ByVal coment2 As String)

        Println(eLeft + "Nº")
        Println("".PadRight(12 - factura.Length, " "))
        Println(factura)
        Println("".PadRight(24, " "))
        Print(fecha.ToString("dd/MM/yyyy"))

        Print(" ")

        Print(eLeft + "CLIENTE:")
        PrintDashes()
        Print(cliente)
        If coment1 <> "" And coment2 <> "" Then
            Print(coment1)
            Print(coment2)
        ElseIf coment1 <> "" And coment2 = "" Then
            Print(coment1)
        ElseIf coment1 = "" And coment2 <> "" Then
            Print(coment2)
        End If
        Print("Codigo: " + cod)
        PrintDashes()
        Print(vbLf)

    End Sub

    Public Sub PrintBody(ByVal DGV As DataGridView, ByVal total As String, ByVal subt As String,
                         ByVal descu As String, ByVal efectivo As String, ByVal cambio As String)

        Dim impt As Double = Nothing
        Dim impSuma As Double = 0
        Dim printImp As String = Nothing
        Dim precioOriginal As Double = Nothing
        cambio = String.Format("{0:n}", CDbl(cambio))

        Try
            Dim cantpr, descrpr, subtpr, precpr, tempdesc As String

            Println(eSmlText + "  CANT.  ")
            Println("DESCRIPCION               ")
            Print("SUBTOTAL C/.")

            Println(eSmlText + "  -----  ")
            Println("-----------------------   ")
            Print("-------------")

            For Each item As DataGridViewRow In DGV.Rows
                cantpr = CStr(item.Cells(2).Value)
                descrpr = CStr(item.Cells(1).Value)
                tempdesc = CStr(item.Cells(1).Value)
                subtpr = CStr(item.Cells(5).Value)
                precpr = CStr(item.Cells(3).Value)
                impt = CDbl(item.Cells(4).Value)

                If descrpr <> "" Then
                    If descrpr.Length > 25 Then
                        tempdesc = descrpr.Substring(0, 24) & "_"
                    End If
                End If

                'REVISA SI EXISTEN DATOS A IMPRIMIR
                If cantpr <> "" Then
                    If cantpr.Contains(".") Then
                        cantpr = cantpr.Replace(".", "")
                    End If

                    'CHEQUEAR SI PRODUCTOS INCLUYEN IMPUESTOS
                    If impt > 0 Then
                        precioOriginal = (CDbl(subtpr) / ((impt / 100) + 1))
                        impSuma += (CDbl(precioOriginal) * (impt / 100))
                        subtpr = String.Format("{0:n}", (CDbl(subtpr) / ((impt / 100) + 1)))

                        'IMPRIME LA CANTIDAD E INCLUYE ASTERISCO A LA CANTIDAD
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("* ")
                        Else
                            Println("####.##")
                            Println("* ")
                        End If


                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 25 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                        'CALCULA LOS PRECIOS SIN IMPUESTOS
                    Else
                        'IMPRIME LA CANTIDAD DEL ARTICULO
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("  ")
                        Else
                            Println("####.##")
                            Println("  ")
                        End If

                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 18 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                    End If

                    'IMPRIME EL PRECIO INDIVIDUAL DEL ARTICULO
                    Println("".PadRight(9, " "))
                    Println("Precio   ")
                    If precpr.Length <= 12 Then
                        Println("".PadRight(12 - precpr.Length, " "))
                        Print(precpr)
                    Else
                        Print("#.###.###.##")
                    End If

                End If

            Next

            Print(" ")

            subt = String.Format("{0:n}", (CDbl(subt) - impSuma))
            'IMPRIME EL SUBTOTAL DE LA COMPRA
            Println(eLeft + "".PadRight(20, " "))
            Println(eSmlText + "SubTotal: ")
            Println("".PadRight(18 - subt.Length, " "))
            Print(subt)

            'IMPRIME EL IMPUESTO SI ES MAYOR A O
            If impSuma > 0 Then
                printImp = String.Format("{0:n}", CDbl(impSuma))
                Println(eLeft + " ".PadRight(20, " "))
                Println(eSmlText + "Impt: ")
                Println("".PadRight(22 - printImp.Length, " "))
                Print(printImp)
            End If

            'IMPRIME  EL DESCUENTO SI ES MAYOR A CERO
            If descu > 0 Then
                descu = String.Format("{0:n}", CDbl(descu))
                Println(eLeft + "".PadRight(20, " "))
                Println("Descuento: ")
                Println("".PadRight(17 - descu.Length, " "))
                Print(descu)
            End If

            'IMPRIME EL TOTAL DE LA FACTURA
            PrintDashes()
            Println(eLeft + "".PadRight(10, " "))
            Println(eBigText + eNegritaOn + "TOTAL: ")
            Println("".PadRight(20 - total.Length, " "))
            Print(total + eNegritaOff + eSmlText)
            PrintDashes()

        Catch ex As Exception
            MsgBox(ex.ToString)
            Logger.e("Error con excepción", ex, New StackFrame(True))
        End Try

    End Sub

    Public Sub PrintBody_wDate(ByVal DGV As DataGridView, ByVal total As String, ByVal subt As String,
                         ByVal descu As String, ByVal efectivo As String, ByVal cambio As String)

        Dim impt As Double = Nothing
        Dim impSuma As Double = 0
        Dim printImp As String = Nothing
        Dim precioOriginal As Double = Nothing
        Dim fecha As String = Nothing
        DGV.Sort(DGV.Columns(7), System.ComponentModel.ListSortDirection.Ascending)
        cambio = String.Format("{0:n}", CDbl(cambio))

        Try
            Dim cantpr, descrpr, subtpr, precpr, tempdesc As String

            Println(eSmlText + "  CANT.  ")
            Println("DESCRIPCION               ")
            Print("SUBTOTAL C/.")

            Println(eSmlText + "  -----  ")
            Println("-----------------------   ")
            Print("-------------")

            For Each item As DataGridViewRow In DGV.Rows
                cantpr = CStr(item.Cells(2).Value)
                descrpr = CStr(item.Cells(1).Value)
                tempdesc = CStr(item.Cells(1).Value)
                subtpr = CStr(item.Cells(5).Value)
                precpr = CStr(item.Cells(3).Value)
                impt = CDbl(item.Cells(4).Value)
                fecha = CStr(item.Cells(7).Value)

                If descrpr <> "" Then
                    If descrpr.Length > 25 Then
                        tempdesc = descrpr.Substring(0, 24) & "_"
                    End If
                End If

                'REVISA SI EXISTEN DATOS A IMPRIMIR
                If cantpr <> "" Then
                    If cantpr.Contains(".") Then
                        cantpr = cantpr.Replace(".", "")
                    End If

                    'CHEQUEAR SI PRODUCTOS INCLUYEN IMPUESTOS
                    If impt > 0 Then
                        precioOriginal = (CDbl(subtpr) / ((impt / 100) + 1))
                        impSuma += (CDbl(precioOriginal) * (impt / 100))
                        subtpr = String.Format("{0:n}", (CDbl(subtpr) / ((impt / 100) + 1)))

                        'IMPRIME LA CANTIDAD E INCLUYE ASTERISCO A LA CANTIDAD
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("* ")
                        Else
                            Println("####.##")
                            Println("* ")
                        End If


                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 25 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                        'CALCULA LOS PRECIOS SIN IMPUESTOS
                    Else
                        'IMPRIME LA CANTIDAD DEL ARTICULO
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("  ")
                        Else
                            Println("####.##")
                            Println("  ")
                        End If

                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 18 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                    End If

                    'IMPRIME EL PRECIO INDIVIDUAL DEL ARTICULO
                    Println("".PadRight(9, " "))
                    Println("Precio   ")
                    If precpr.Length <= 12 Then
                        Println("".PadRight(12 - precpr.Length, " "))
                        Print(precpr)
                    Else
                        Print("#.###.###.##")
                    End If

                    'Imprime la fecha y hora en la que se ingresaron los detalles
                    Println("".PadRight(9, " "))
                    Print(fecha)

                End If

            Next

            Print(" ")

            subt = String.Format("{0:n}", (CDbl(subt) - impSuma))
            'IMPRIME EL SUBTOTAL DE LA COMPRA
            Println(eLeft + "".PadRight(20, " "))
            Println(eSmlText + "SubTotal: ")
            Println("".PadRight(18 - subt.Length, " "))
            Print(subt)

            'IMPRIME EL IMPUESTO SI ES MAYOR A O
            If impSuma > 0 Then
                printImp = String.Format("{0:n}", CDbl(impSuma))
                Println(eLeft + " ".PadRight(20, " "))
                Println(eSmlText + "Impt: ")
                Println("".PadRight(22 - printImp.Length, " "))
                Print(printImp)
            End If

            'IMPRIME  EL DESCUENTO SI ES MAYOR A CERO
            If descu > 0 Then
                descu = String.Format("{0:n}", CDbl(descu))
                Println(eLeft + "".PadRight(20, " "))
                Println("Descuento: ")
                Println("".PadRight(17 - descu.Length, " "))
                Print(descu)
            End If

            'IMPRIME EL TOTAL DE LA FACTURA
            PrintDashes()
            Println(eLeft + "".PadRight(10, " "))
            Println(eBigText + eNegritaOn + "TOTAL: ")
            Println("".PadRight(20 - total.Length, " "))
            Print(total + eNegritaOff + eSmlText)
            PrintDashes()

        Catch ex As Exception
            MsgBox(ex.ToString)
            Logger.e("Error con excepción", ex, New StackFrame(True))
        End Try

    End Sub


    Public Sub PrintBodyCot(ByVal DGV As DataGridView, ByVal total As String, ByVal subt As String,
                        ByVal descu As String)

        Dim impt As Double = Nothing
        Dim impSuma As Double = Nothing
        Dim printImp As String = Nothing
        Dim precioOriginal As Double = Nothing

        Try

            Dim cantpr, descrpr, subtpr, precpr, tempdesc As String

            Println(eSmlText + "  CANT.  ")
            Println("DESCRIPCION               ")
            Print("SUBTOTAL C/.")

            Println(eSmlText + "  -----  ")
            Println("-----------------------   ")
            Print("-------------")
            Print("")

            For Each item As DataGridViewRow In DGV.Rows
                cantpr = CStr(item.Cells(2).Value)
                descrpr = CStr(item.Cells(1).Value)
                tempdesc = CStr(item.Cells(1).Value)
                subtpr = CStr(item.Cells(5).Value)
                precpr = CStr(item.Cells(3).Value)
                impt = CDbl(item.Cells(4).Value)

                If descrpr <> "" Then
                    If descrpr.Length > 25 Then
                        tempdesc = descrpr.Substring(0, 24) & "_"
                    End If
                End If

                'REVISA SI EXISTEN DATOS A IMPRIMIR
                If cantpr <> "" Then
                    If cantpr.Contains(".") Then
                        cantpr = cantpr.Replace(".", "")
                    End If

                    'CHEQUEAR SI PRODUCTOS INCLUYEN IMPUESTOS
                    If impt > 0 Then
                        precioOriginal = (CDbl(subtpr) / ((impt / 100) + 1))
                        impSuma += (CDbl(precioOriginal) * (impt / 100))
                        subtpr = String.Format("{0:n}", (CDbl(subtpr) / ((impt / 100) + 1)))

                        'IMPRIME LA CANTIDAD E INCLUYE ASTERISCO A LA CANTIDAD
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("* ")
                        Else
                            Println("####.##")
                            Println("* ")
                        End If


                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 25 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                        'CALCULA LOS PRECIOS SIN IMPUESTOS
                    Else
                        'IMPRIME LA CANTIDAD DEL ARTICULO
                        If cantpr.Length <= 7 Then
                            Println("".PadRight(7 - cantpr.Length, " "))
                            Println(cantpr)
                            Println("  ")
                        Else
                            Println("####.##")
                            Println("  ")
                        End If

                        'IMPRIME LA DESCRIPCION
                        If descrpr.Length > 25 Then

                            'SI LA DESCRIPCION SOBREPASA LOS 18 CARACTERES IMPRIME UNA PRIMERA PARTE
                            'CON EL TOTAL DEL ARTICULO Y PASA A ESCRIBIR LA CONTINUACION EN LA SIGUIENTE LINEA

                            Println(tempdesc)
                            Println("".PadRight(26 - tempdesc.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                            Println("".PadRight(9, " "))
                            Print(descrpr.Substring(24, descrpr.Length - 24))

                        Else

                            Println(descrpr)
                            Println("".PadRight(26 - descrpr.Length, " "))

                            'IMPRIME EL TOTAL POR ARTICULO
                            If subtpr.Length <= 13 Then
                                Println("".PadRight(13 - subtpr.Length, " "))
                                Print(subtpr)
                            Else
                                Print("##.###.###.##")
                            End If

                        End If

                    End If

                    'IMPRIME EL PRECIO INDIVIDUAL DEL ARTICULO
                    Println("".PadRight(9, " "))
                    Println("Precio   ")
                    If precpr.Length <= 12 Then
                        Println("".PadRight(12 - precpr.Length, " "))
                        Print(precpr)
                    Else
                        Print("#.###.###.##")
                    End If

                End If

            Next

            Print(" ")
            Print(" ")

            subt = String.Format("{0:n}", (CDbl(subt) - impSuma))
            'IMPRIME EL SUBTOTAL DE LA COMPRA
            Println(eLeft + "".PadRight(20, " "))
            Println(eSmlText + "SubTotal: ")
            Println("".PadRight(18 - subt.Length, " "))
            Print(subt)

            'IMPRIME EL IMPUESTO SI ES MAYOR A O
            If impSuma > 0 Then
                printImp = String.Format("{0:n}", CDbl(impSuma))
                Println(eLeft + " ".PadRight(20, " "))
                Println(eSmlText + "Impt: ")
                Println("".PadRight(22 - printImp.Length, " "))
                Print(printImp)
            End If

            'IMPRIME  EL DESCUENTO SI ES MAYOR A CERO
            If descu > 0 Then
                Println(eLeft + " ".PadRight(20, " "))
                Println("Descuento: ")
                Println("".PadRight(17 - descu.Length, " "))
                Print(descu)
            End If
            Print(eRight + "----------------------------")
            Println(eLeft + "".PadRight(20, " "))

            'IMPRIME EL TOTAL
            Println(eBigText + "TOTAL: ")
            Println("".PadRight(21 - total.Length, " "))
            Print(eNegritaOn + total + eNegritaOff + eSmlText)
            Print(" ")

            Print(eCentre + eNegritaOn + "* I.V.I. *")
            Print("* PRECIOS SUJETOS A CAMBIOS")
            Print("SIN PREVIO AVISO *" + eNegritaOff)
            Print(" ")
            Print(" ")
            Print(" ")
            Print(vbLf + vbLf + vbLf + vbLf + vbLf + vbLf + vbLf + eCut + eDrawer)

        Catch ex As Exception
            MsgBox(ex.ToString)
            Logger.e("Error con excepción", ex, New StackFrame(True))
        End Try
    End Sub

    Public Sub PrintBody_rapido(ByVal total As String, ByVal Det_art As String)
        Try

            'VARIABLES
            Dim cantpr, descrpr, cambio As String

            Println(eSmlText + "  CANT.  ")
            Println("DESCRIPCION               ")
            Print("SUBTOTAL C/.")

            Println(eSmlText + "  -----  ")
            Println("-----------------------   ")
            Print("-------------")
            Print("")

            'DESCRIPCION RAPIDA DEL ARTICULO QUE APARECERA EN LA FACTURA
            cantpr = "1"
            descrpr = Det_art
            cambio = "0,00"
            total = String.Format("{0:n}", CDbl(total))

            If cantpr <> "" Then
                If cantpr.Length <= 8 Then
                    Println("".PadRight(8 - cantpr.Length, " "))
                    Println(cantpr)
                    Println(" ")
                Else
                    Println("#.###.##")
                    Println(" ")
                End If

                'IMPRIME LA DESCRIPCION DEL ARTICULO
                Println(descrpr)
                Println("".PadRight(26 - descrpr.Length, " "))

                'IMPRIME EL TOTAL POR ARTICULO
                If total.Length <= 13 Then
                    Println("".PadRight(13 - total.Length, " "))
                    Print(total)
                Else
                    Println("##.###.###.##")
                End If

                Println("".PadRight(9, " "))
                Println("Precio")

                If total.Length <= 12 Then
                    Println("".PadRight(12 - total.Length, " "))
                    Print(total)
                Else
                    Print("#.###.###.##")
                End If

            End If

            Print(eClear)
            Print(" ")
            Print(" ")

            'IMPRIME EL SUBTOTAL DE LA COMPRA
            Println(eLeft + "".PadRight(20, " "))
            Println(eSmlText + "SubTotal: ")
            Println("".PadRight(18 - total.Length, " "))
            Print(total)

            Print(eRight + "----------------------------")
            Println(eLeft + "".PadRight(20, " "))

            'IMPRIME EL TOTAL
            Println(eBigText + "TOTAL: ")
            Println("".PadRight(21 - total.Length, " "))
            Print(eNegritaOn + total + eNegritaOff + eSmlText)

            'IMPRIME EL EFECTIVO RECIBIDO 
            Println(eLeft + "".PadRight(20, " "))
            Println("Efectivo: ")
            Println("".PadRight(18 - total.Length, " "))
            Print(total)

            'IMPRIME EL CAMBIO
            Println(eLeft + " ".PadRight(20, " "))
            Println("Cambio: ")
            Println("".PadRight(20 - cambio.Length, " "))
            Print(cambio)
            Print("")

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

    Public Sub abrir_caja_termica()
        Print(eDrawer)
    End Sub

    Public Sub PrintCierreCaja(ByVal p_montodinero As Array, ByVal p_totalDinero As String)

        Dim leyenda = "Dinero en Efectivo"
        Dim _Detalle As String = ""
        Dim _CharPorLinea As Integer = 48

        Dim _MonedasCinco As Integer = p_montodinero(0)
        Dim _MonedasDiez As Integer = p_montodinero(1)
        Dim _MonedasVeinticinco As Integer = p_montodinero(2)
        Dim _MonedasCincuenta As Integer = p_montodinero(3)
        Dim _MonedasCien As Integer = p_montodinero(4)
        Dim _MonedasQuinientos As Integer = p_montodinero(5)
        Dim _BilletesMil As Integer = p_montodinero(6)
        Dim _BilletesDosMil As Integer = p_montodinero(7)
        Dim _BilletesCincoMil As Integer = p_montodinero(8)
        Dim _BilletesDiezMil As Integer = p_montodinero(9)
        Dim _BilletesVeinteMil As Integer = p_montodinero(10)
        Dim _BilletesCincuentaMil As Integer = p_montodinero(11)

        Print(eCentre + leyenda)
        Print(eLeft + " ")

        If _MonedasCinco > 0 Then
            _Detalle = "Monedas 5"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasCinco.ToString.Length, " "))
            Print(_MonedasCinco)
        End If
        If _MonedasDiez > 0 Then
            _Detalle = "Monedas 10"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasDiez.ToString.Length, " "))
            Print(_MonedasDiez)
        End If
        If _MonedasVeinticinco > 0 Then
            _Detalle = "Monedas 25"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasVeinticinco.ToString.Length, " "))
            Print(_MonedasVeinticinco)
        End If
        If _MonedasCincuenta > 0 Then
            _Detalle = "Monedas 50"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasCincuenta.ToString.Length, " "))
            Print(_MonedasCincuenta)
        End If
        If _MonedasCien > 0 Then
            _Detalle = "Monedas 100"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasCien.ToString.Length, " "))
            Print(_MonedasCien)
        End If
        If _MonedasQuinientos > 0 Then
            _Detalle = "Monedas 500"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _MonedasQuinientos.ToString.Length, " "))
            Print(_MonedasQuinientos)
        End If
        If _BilletesMil > 0 Then
            _Detalle = "Billetes 1.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesMil.ToString.Length, " "))
            Print(_BilletesMil)
        End If
        If _BilletesDosMil > 0 Then
            _Detalle = "Billetes 2.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesDosMil.ToString.Length, " "))
            Print(_BilletesDosMil)
        End If
        If _BilletesCincoMil > 0 Then
            _Detalle = "Billetes 5.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesCincoMil.ToString.Length, " "))
            Print(_BilletesCincoMil)
        End If
        If _BilletesDiezMil > 0 Then
            _Detalle = "Billetes 10.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesDiezMil.ToString.Length, " "))
            Print(_BilletesDiezMil)
        End If
        If _BilletesVeinteMil > 0 Then
            _Detalle = "Billetes 20.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesVeinteMil.ToString.Length, " "))
            Print(_BilletesVeinteMil)
        End If
        If _BilletesCincuentaMil > 0 Then
            _Detalle = "Billetes 50.000"
            Println(_Detalle)
            Println("".PadRight((_CharPorLinea - _Detalle.Length) - _BilletesCincuentaMil.ToString.Length, " "))
            Print(_BilletesCincuentaMil)
        End If

        Print(" ")

        _Detalle = "Total "
        Println(_Detalle)
        Println("".PadRight((_CharPorLinea - _Detalle.Length) - p_totalDinero.Length, " "))
        Print(p_totalDinero)

        Print(vbLf + vbLf + vbLf + vbLf + eCut)

    End Sub

End Module
