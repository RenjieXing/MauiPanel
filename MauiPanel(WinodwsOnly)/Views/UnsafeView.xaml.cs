using MauiPanel.ViewModels;
namespace MauiPanel.Views;

public partial class UnsafeView : ContentPage
{

    public static UnsafeViewModel mainViewModel;
    public UnsafeView()
	{
        mainViewModel = new UnsafeViewModel();
        BindingContext = mainViewModel;
        InitializeComponent();


    }

//    <DataTemplate x:DataType="x:String">
//    <ViewCell>
//        <Label Text = "{Binding}" />
//    </ ViewCell >
//</ DataTemplate >

    private void Entry_IsCompelet(object sender, EventArgs e)
    {

        try
        {
            mainViewModel.Input = int.Parse(inputEntry.Text).ToString();
            mainViewModel.inputEntery();
        }
        catch (Exception)
        {
            mainViewModel.Input = "";
        }
        
    }
    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var collectionView = sender as CollectionView;
        if (collectionView != null)
        {
            // 获取滚动的总距离
            var scrollDistance = e.VerticalOffset;

            // 获取 CollectionView 的高度
            var collectionViewHeight = collectionView.Height;

            // 计算滚动距离占屏幕的比例
            var ratio = scrollDistance / collectionViewHeight;

            // 计算当前屏幕可见的 CollectionViewItem 的数量
        
        }
    }



}