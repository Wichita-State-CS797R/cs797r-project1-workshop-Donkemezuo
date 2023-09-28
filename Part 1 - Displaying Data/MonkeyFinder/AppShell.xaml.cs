namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		// Registering the details page route 
		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}