namespace MonkeyFinder.ViewModel;

// Allowing our base view model conform to the INotifyPropertyChanged wrapper to enable us observe changes 
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    bool isBusy;
    string title;

    public bool IsBusy {
        get => isBusy;
        set {
            if (isBusy == value) {
                return;
            } else {
                // sets new value to isBusy variable 
                isBusy = value;
                OnPropertyChanged(); // Notify view of property change
            }

        }

    }

    public string Title {
        get => title;
        set {
            if (title == value)
            {
                return;
            }
            else {
                // sets new value to title variable 
                title = value;
                OnPropertyChanged(); // Notify view of property change
            }

        }
    }

}
