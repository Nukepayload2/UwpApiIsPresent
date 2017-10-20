Public Class FeaturesViewModel
    Implements INotifyPropertyChanged

    Dim _ApiAvailableCount As Integer
    Public Property ApiAvailableCount As Integer
        Get
            Return _ApiAvailableCount
        End Get
        Set(value As Integer)
            _ApiAvailableCount = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ApiAvailableCount)))
        End Set
    End Property

    Dim _ApiUnavailableCount As Integer
    Public Property ApiUnavailableCount As Integer
        Get
            Return _ApiUnavailableCount
        End Get
        Set(value As Integer)
            _ApiUnavailableCount = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ApiUnavailableCount)))
        End Set
    End Property

    Dim _IsBusy As Boolean
    Public Property IsBusy As Boolean
        Get
            Return _IsBusy
        End Get
        Set(value As Boolean)
            _IsBusy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBusy)))
        End Set
    End Property

    Public ReadOnly Property Features As New ObservableCollection(Of FeatureItem)

    Public Property FeaturesCache As FeatureItem()

    Public ReadOnly Property DetectCommand As New DetectCommand(Me)

    Dim _FeatureNames As String()
    Public Property FeatureNames As String()
        Get
            Return _FeatureNames
        End Get
        Set(value As String())
            _FeatureNames = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FeatureNames)))
        End Set
    End Property

    Private _search As New SearchService

    Dim _SearchString As String
    Public Property SearchString As String
        Get
            Return _SearchString
        End Get
        Set(value As String)
            _SearchString = value
            Dim tsk = _search.SearchAsync(Me)
        End Set
    End Property

    Dim _Searched As Boolean
    Public Property Searched As Boolean
        Get
            Return _Searched
        End Get
        Set(value As Boolean)
            _Searched = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Searched)))
        End Set
    End Property

    Dim _SearchStarted As Boolean
    Public Property SearchStarted As Boolean
        Get
            Return _SearchStarted
        End Get
        Set(value As Boolean)
            _SearchStarted = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SearchStarted)))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
