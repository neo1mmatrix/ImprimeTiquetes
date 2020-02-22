Module Funciones

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

End Module
