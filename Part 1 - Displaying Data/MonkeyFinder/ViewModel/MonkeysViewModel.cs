using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    // Creating a local instance of the monkey service class 
    MonkeyService monkeyService; 

    public MonkeysViewModel(MonkeyService monkeyService) {
        Title = "Monkey Finder";

        // Assigning the passed monkey service pass through dependency injection
        this.monkeyService = monkeyService;
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
}
