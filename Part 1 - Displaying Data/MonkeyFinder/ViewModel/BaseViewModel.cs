//namespace MonkeyFinder.ViewModel;

//// Allowing our base view model conform to the INotifyPropertyChanged wrapper to enable us observe changes 
//public class BaseViewModel : INotifyPropertyChanged
//{
//    public event PropertyChangedEventHandler PropertyChanged;

//    public void OnPropertyChanged([CallerMemberName] string name = null) =>
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

//    bool isBusy;
//    string title;

//    public bool IsBusy {
//        get => isBusy;
//        set {
//            if (isBusy == value) {
//                return;
//            } else {
//                // sets new value to isBusy variable 
//                isBusy = value;
//                OnPropertyChanged(); // Notify view of property change
//                // Also raise the IsNotBusy property changed
//                OnPropertyChanged(nameof(IsNotBusy));
//            }

//        }

//    }

//    public string Title {
//        get => title;
//        set {
//            if (title == value)
//            {
//                return;
//            }
//            else {
//                // sets new value to title variable 
//                title = value;
//                OnPropertyChanged(); // Notify view of property change
//            }

//        }
//    }


//    public bool IsNotBusy => !IsBusy;


//}
//IsBusy

// Using Community ToolKit from MVVM
// The ObservableObject base class already implemented the INotifyPropertyChanged behind the scenes and provides us a clean adoption of the same functiuonalities 

namespace MonkeyFinder.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;
}

