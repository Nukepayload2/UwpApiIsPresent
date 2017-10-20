Public Class FeatureItem
    Public Sub New(name As String, isPresent As Boolean)
        Me.Name = name
        LowerName = name.ToLowerInvariant
        Me.IsPresent = isPresent
    End Sub

    Public ReadOnly Property Name As String
    Public ReadOnly Property LowerName As String
    Public ReadOnly Property IsPresent As Boolean
End Class
