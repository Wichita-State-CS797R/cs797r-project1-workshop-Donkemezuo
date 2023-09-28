namespace MonkeyFinder.ViewModel;

// Adding query property to handle the passing of monkey data 
[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    // An instance of map
    IMap map;
    public MonkeyDetailsViewModel(IMap map) {

        // Initializing map
        this.map = map;
    }

    [ObservableProperty]
    Monkey monkey; // Monkey object that will be passed from the parent view

    [RelayCommand]
    async Task OpenMap() {
        try {
            await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
            {
                Name = Monkey.Name,
                NavigationMode = NavigationMode.None
            }) ;

        } catch(Exception ex) {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }

    }
}
