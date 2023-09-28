namespace MonkeyFinder.ViewModel;

// Adding query property to handle the passing of monkey data 
[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    public MonkeyDetailsViewModel() {

    }

    [ObservableProperty]
    Monkey monkey; // Monkey object that will be passed from the parent view


}
