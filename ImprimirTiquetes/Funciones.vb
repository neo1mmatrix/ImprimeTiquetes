Module Funciones

    'VERIFICA QUE EL TEXTO ESCRITO EN UN TEXTBOX CORRESPONDA SOLAMENTE A NUMERICO
    Function Fg_SoloNumeros(ByVal Digito As String, ByVal Texto As String) As Boolean
        Dim Dt_Entero As Integer = CInt(Asc(Digito))
        If Dt_Entero = 8 Then
            Fg_SoloNumeros = False
        Else
            If InStr("1234567890.", Digito) = 0 Then
                Fg_SoloNumeros = True
            ElseIf IsNumeric(Texto) = True Then
                Fg_SoloNumeros = False
            ElseIf IsNumeric(Texto) = False Then
                Fg_SoloNumeros = True
            End If
        End If
        Return Fg_SoloNumeros
    End Function

    'CALCULA EL PORCENTAJE DE AVANCE EN UN RECORRIDO DE UNA LISTA
    Function porcentaje(ByVal linea As Double, ByVal total_lineas As Double) As Integer

        Dim resultado As Integer = 0
        resultado = CInt(Math.Ceiling((linea * 100) / total_lineas))
        Return resultado
        Application.DoEvents()

    End Function

    'CALCULA EL PORCENTAJE DE GUARDADO EN UN ARCHIVO DE EXCEL
    Function porcentaje_guardado_excel(ByVal porcentaje As Double) As Integer

        Dim resultado As Integer = 0
        resultado = CInt(Math.Ceiling(porcentaje * 0.4))
        Return resultado
        Application.DoEvents()

    End Function

    'ESTABLECE EL VALOR DE UN PROGRESS BAR EN EL FORMULARIO Fr_Opciones_Cargar
    Public Sub progressBar_Opciones_Cargar(ByVal porcentaje As Integer)

        Fr_Opciones_cargar.PB_Opciones_cargar.Value = porcentaje
        Application.DoEvents()

    End Sub

    'ESTABLECE EL VALOR DE UN PROGRESS BAR EN EL FORMULARIO Fr_Opciones_Cargar
    Public Sub ProBar_reporte_fechas(ByVal porcentaje As Integer)

        Fr_Reporte_Fechas.PB_Opciones_cargar.Value = porcentaje
        Application.DoEvents()

    End Sub

    'VALIDACION PARA CAJAS DE TEXTO
    'INGRESO SOLO DE NUMEROS CON DECIMALES
    Public Sub NumerosyDecimal(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Char.IsDigit(e.KeyChar) Then
            If CajaTexto.Text.IndexOf(",") <> -1 Then
                If (CajaTexto.Text.Length >= CajaTexto.Text.IndexOf(",") + 3) Then
                    e.Handled = True
                End If
            Else
                e.Handled = False
            End If
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "," And Not CajaTexto.Text.IndexOf(",") Then
            e.Handled = True
        ElseIf e.KeyChar = "," Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    'CONVIERTE UN NUMERO A FORMATO DE MILLAR
    Public Function convertir_formato_miles_decimales(ByVal numero As Double) As String

        Dim conversion As String
        conversion = String.Format("{0:n}", numero)
        Return conversion

    End Function

    'CONVIERTE UN NUMERO A FORMATO MILLAR SIN DECIMALES
    Public Function convertir_formato_miles(ByVal numero As Double) As String

        Dim conversion As String
        conversion = String.Format("{0:n0}", numero)
        Return conversion

    End Function

    'CALCULA EL SUBTOTAL DE UN PRODUCTO MAS LOS IMPUESTOS
    Public Sub calcular_subtotal(ByVal precio As Double, ByVal cantidad As Double,
                                 ByVal impuesto As Double, ByRef subtotal As String)

        Dim resultado As Double
        impuesto = impuesto / 100
        resultado = precio * cantidad
        resultado = (resultado * impuesto) + resultado
        subtotal = convertir_formato_miles_decimales(CStr(resultado))

    End Sub

    Public Sub defineTamañoCeldaDataGrid(ByVal columnaTotales As Integer, ByVal columnaNoIncluir As Integer, ByRef Dgv As DataGridView, ByVal columnaOcultaTamano As Integer)

        Dim tamañoFinal As Integer = 0
        For i As Integer = 0 To columnaTotales - 1
            If i <> columnaNoIncluir Then
                tamañoFinal += Dgv.Columns(i).Width
            End If
        Next
        tamañoFinal += 60
        Dgv.Columns(columnaNoIncluir).Width = (Dgv.Width + columnaOcultaTamano) - tamañoFinal

    End Sub


End Module
