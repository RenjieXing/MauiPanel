using MauiPanel.ViewModels;
namespace MauiPanel.Views;

public partial class UnsafeAccessorView : ContentPage
{
    public static UnsafeAccessViewModel mainViewModel;
    public UnsafeAccessorView()
	{
        mainViewModel = new UnsafeAccessViewModel();
        BindingContext = mainViewModel;
        InitializeComponent();
        refButton.IsEnabled = false;
        pointerButton.IsEnabled = false;
        reflexButton.IsEnabled = false;
        reflexMethodButton.IsEnabled = false;
      
	}
    public void Entry_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int.Parse(entry.Text);
            refButton.IsEnabled = true;
            pointerButton.IsEnabled = true;
            reflexButton.IsEnabled = true;
            reflexMethodButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            refButton.IsEnabled = false;
            pointerButton.IsEnabled = false;
            reflexButton.IsEnabled = false;
            reflexMethodButton.IsEnabled = false;
        }
    }
    public void ShowButton_Clicked(object sender, EventArgs e)
    {
        mainViewModel.Refresh();
        showButton.IsEnabled = false;
    }
    public void RefButton_Clicked(object sender, EventArgs e)
    {
        mainViewModel.ChangeAgeByField(int.Parse(entry.Text).ToString());

    }
    public void PointerButton_Clicked(object sender, EventArgs e)
    {
        mainViewModel.ChangeAgeByMethod(int.Parse(entry.Text).ToString());

    }
    public void reflexButton_Clicked(object sender, EventArgs e)
    {
        mainViewModel.ChangeAgeByReflex(int.Parse(entry.Text).ToString());

    }
    public void reflexMethodButton_Clicked(object sender, EventArgs e)
    {
        mainViewModel.ChangeAgeByReflexMethod(int.Parse(entry.Text).ToString());

    }
}