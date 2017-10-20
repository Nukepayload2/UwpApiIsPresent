Imports System.Reflection

Public Class ApiEnumService
    Public Iterator Function EnumApi() As IEnumerable(Of FeatureItem)
        Dim asm = GetType(UniversalApiContract).GetTypeInfo.Assembly
        For Each tp In asm.GetExportedTypes
            Dim fullName As String = tp.FullName
            Yield New FeatureItem(fullName, Windows.Foundation.Metadata.ApiInformation.IsTypePresent(fullName))
        Next
    End Function
End Class
