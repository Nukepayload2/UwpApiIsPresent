Public Class DetectCommand
    Implements ICommand

    Private _vm As FeaturesViewModel

    Sub New(vm As FeaturesViewModel)
        _vm = vm
    End Sub

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Async Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
        CanExec = False
        _vm.SearchStarted = True
        _vm.IsBusy = True
        Const cacheCount = 256
        Dim _resultCache As New List(Of FeatureItem)(256)
        Dim apiSvc As New ApiEnumService
        Dim dispatcher = Window.Current.Dispatcher
        Await Task.Run(
            Async Function()
                For Each apiInfo In apiSvc.EnumApi
                    _resultCache.Add(apiInfo)
                    If _resultCache.Count >= cacheCount Then
                        Await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                                              Sub() UpdateResult(_resultCache))
                    End If
                Next
            End Function)
        UpdateResult(_resultCache)
        _vm.FeatureNames = Aggregate fest In _vm.Features Select fest.Name Into ToArray
        _vm.FeaturesCache = _vm.Features.ToArray
        _vm.IsBusy = False
        _vm.Searched = True
    End Sub

    Private Sub UpdateResult(resultCache As List(Of FeatureItem))
        If resultCache.Count = 0 Then
            Return
        End If
        Dim present = 0
        For i = 0 To resultCache.Count - 1
            Dim feat As FeatureItem = resultCache(i)
            If feat.IsPresent Then
                present += 1
            End If
            _vm.Features.Add(feat)
        Next
        _vm.ApiAvailableCount += present
        _vm.ApiUnavailableCount += resultCache.Count - present
        resultCache.Clear()
    End Sub

    Dim _CanExec As Boolean = True
    Private Property CanExec As Boolean
        Get
            Return _CanExec
        End Get
        Set(value As Boolean)
            _CanExec = value
            RaiseEvent CanExecuteChanged(Me, New PropertyChangedEventArgs(NameOf(CanExec)))
        End Set
    End Property

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return CanExec
    End Function
End Class
