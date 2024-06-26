using Plugin.LocalNotification;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppNumeroDaSorte;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

	}

	private async void OnGenerateLuckNumbers()
	{
		btSort.IsEnabled = false;
        NameApp.IsVisible = false;
		ContainerLuckNumbers.IsVisible = true;
		var listNumbers = getRandomListNumbers();

		_ = logoImage.ScaleTo(logoImage.Scale / 1.25, 1500, Easing.BounceIn);

        await SortAnimate(LuckNumber01, 0);
        await SortAnimate(LuckNumber02, 1);
        await SortAnimate(LuckNumber03, 2);
        await SortAnimate(LuckNumber04, 3);
        await SortAnimate(LuckNumber05, 4);
        await SortAnimate(LuckNumber06, 5);

        btSort.IsEnabled = true;

        async Task SortAnimate (Label label, int order){
            label.Text = $"{listNumbers.ElementAt(order):D2}";
            await label.RotateTo(label.Rotation + 360, 500, Easing.BounceOut);
        }
    }

	private HashSet<int> getRandomListNumbers()
	{
		var listNumbers = new HashSet<int>();

		while(listNumbers.Count < 6)
		{
            listNumbers.Add(new Random().Next(1, 60));
		}

		return listNumbers;
	}

	private void CopyNumbers()
	{
		Clipboard.Default.SetTextAsync(getNumbers());
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    }

	private async Task ShareNumbers()
	{
		await Share.Default.RequestAsync(new ShareTextRequest()
		{
			Title = "N�mero da Sorte",
			Text = getNumbers(),
        });
	}

	private string getNumbers()
	{
		return $"{LuckNumber01.Text}-{LuckNumber02.Text}-{LuckNumber03.Text}-{LuckNumber04.Text}-{LuckNumber05.Text}-{LuckNumber06.Text}";

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		OnGenerateLuckNumbers();
		NotificationPush();
    }

    private void btCopy_Clicked(object sender, EventArgs e)
    {
		CopyNumbers();
    }

    private async void btShare_Clicked(object sender, EventArgs e)
    {
		await ShareNumbers();
    }

	private void NotificationPush()
	{
		var request = new NotificationRequest
		{
			NotificationId = 1337,
			Title = "Seu Omelete Senhor!",
			Subtitle = "R$ 23,00",
			Description = "OMELETE!!",
			BadgeNumber = 42,
		};

		LocalNotificationCenter.Current.Show(request);
	}
}