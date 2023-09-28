using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    // Creating a local instance of the monkey service class 
    MonkeyService monkeyService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation) {
        Title = "Monkey Finder";

        // Assigning the passed monkey service pass through dependency injection
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    // Performing Network operation to fetch the closest Monkey
    [RelayCommand]
    async Task GetClosestMonkey() {
        if (IsBusy || Monkeys.Count == 0) { return;  }

        try {
            // Get the cached location else get the real location
            var location = await geolocation.GetLastKnownLocationAsync();

            // Checks to see if cached location is null
            if (location == null) {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            var first = Monkeys.OrderBy(m => location.CalculateDistance(
                new Location(m.Latitude, m.Longitude), DistanceUnits.Miles
                )).FirstOrDefault();

            await Shell.Current.DisplayAlert("", first.Name + " " + first.Location, "OK");
        } catch (Exception ex) {

            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    // Performing data task
    [RelayCommand]
    async Task GetMonkeysAsync() {
        if (IsBusy) {
            return;
        }

        try
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                Monkeys.Clear();

            foreach (var monkey in monkeys) {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Unexpected Error Occured", ex.Message, "OK");
        }
        finally {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    // Perform segue to detail view
    [RelayCommand]
    async Task GoToDetails(Monkey monkey) {

        // Checking to see if the monkey object is empty
        if (monkey == null) { return; }

        // We are passing a dictionary of key-value pairs to the details view.
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {
            {"Monkey", monkey }
        });
    }

    // Boolean property to check if the view is refreshing
    [ObservableProperty]
    bool isRefreshing;

}
