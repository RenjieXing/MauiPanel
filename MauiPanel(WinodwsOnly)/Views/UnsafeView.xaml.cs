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
            // ��ȡ�������ܾ���
            var scrollDistance = e.VerticalOffset;

            // ��ȡ CollectionView �ĸ߶�
            var collectionViewHeight = collectionView.Height;

            // �����������ռ��Ļ�ı���
            var ratio = scrollDistance / collectionViewHeight;

            // ���㵱ǰ��Ļ�ɼ��� CollectionViewItem ������
        
        }
    }



}