Imports System.Threading

Public Class SearchService
    Public Property Delay As Integer = 500
    Private _cancelSrc As New CancellationTokenSource
    Private _running As Boolean

    Public Async Function SearchAsync(vm As FeaturesViewModel) As Task
        If _running Then
            _cancelSrc.Cancel()
            _cancelSrc = New CancellationTokenSource
        Else
            _running = True
        End If
        Try
            Await Task.Delay(Delay, _cancelSrc.Token)
        Catch ex As TaskCanceledException
            Return
        End Try
        If String.IsNullOrWhiteSpace(vm.SearchString) Then
            vm.Features.Clear()
            For Each f In vm.FeaturesCache
                vm.Features.Add(f)
            Next
        Else
            Dim searchString = vm.SearchString.Trim.ToLowerInvariant
            vm.Features.Clear()
            For Each f In vm.FeaturesCache
                If f.Name.ToLower.Contains(searchString) Then
                    vm.Features.Add(f)
                End If
            Next
        End If
        _running = False
    End Function
End Class
