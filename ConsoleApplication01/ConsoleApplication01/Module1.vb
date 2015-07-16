Imports System.Reflection.Assembly

Class Test01
    'dynamic dll call test
    Public Function f()
        Dim sra As System.Reflection.Assembly
        Dim o As Object
        Dim arr As String()
        ReDim arr(1)
        arr(0) = "1"
        arr(1) = "2"
        For i = 0 To (arr.Length - 1)
            Try
                sra = System.Reflection.Assembly.LoadFile(getDllPathAndName(arr(i)))
            Catch ex As Exception
                Console.Write("Error load : " & getDllPathAndName(arr(i)))
                Return 1
            End Try
            Try
                o = sra.CreateInstance(getNamespaceAndClass(arr(i)))
            Catch ex As Exception
                Console.Write("Error create instance : " & getNamespaceAndClass(arr(i)))
                Return 2
            End Try
            Try
                o.getT()
            Catch e As Exception
                Console.Write("Error call function getT " & arr(i))
                Return 3
            End Try
        Next
        Return 0
    End Function

    Public Function getDllPathAndName(ByVal s As String) As String
        Dim strPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
        strPath = Right(strPath, strPath.Length - 6)
        strPath = strPath & "\Assembly0" & s & ".dll"
        Console.WriteLine()
        Console.WriteLine(strPath)
        Return strPath
    End Function

    Public Function getNamespaceAndClass(ByVal s As String)
        Return "ClassLibrary" & s & ".Class" & s
    End Function
End Class

Module Module1
    Private t As Test01
    Sub Main()
        t = New Test01
        t.f()
        Console.ReadLine()
    End Sub
End Module
