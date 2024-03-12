using Plugin.LocalNotification;
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

		LuckNumber01.Text = $"{listNumbers.ElementAt(0):D2}";
        await LuckNumber01.RotateTo(LuckNumber01.Rotation + 360, 500, Easing.BounceOut);

		LuckNumber02.Text = $"{listNumbers.ElementAt(1):D2}";
        await LuckNumber02.RotateTo(LuckNumber02.Rotation + 360, 500, Easing.BounceOut);

		LuckNumber03.Text = $"{listNumbers.ElementAt(2):D2}";
        await LuckNumber03.RotateTo(LuckNumber03.Rotation + 360, 500, Easing.BounceOut);

        _ = logoImage.ScaleTo(logoImage.Scale * 1.25, 1500, Easing.BounceOut);
        LuckNumber04.Text = $"{listNumbers.ElementAt(3):D2}";
        await LuckNumber04.RotateTo(LuckNumber04.Rotation + 360, 500, Easing.BounceOut);

		LuckNumber05.Text = $"{listNumbers.ElementAt(4):D2}";
        await LuckNumber05.RotateTo(LuckNumber05.Rotation + 360, 500, Easing.BounceOut);

		LuckNumber06.Text = $"{listNumbers.ElementAt(5):D2}";
        await LuckNumber06.RotateTo(LuckNumber06.Rotation + 360, 500, Easing.BounceOut);
        btSort.IsEnabled = true;
    }

	private SortedSet<int> getRandomListNumbers()
	{
		var listNumbers = new SortedSet<int>();

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
			Title = "Número da Sorte",
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