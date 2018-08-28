using System.Windows;
using Engine.Models;
using Engine.ViewModels;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;

        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {

            if (((FrameworkElement)sender).DataContext is GroupedInventoryItem groupedInventoryItem)
            {
                Session.CurrentPlayer.ReceiveGold(groupedInventoryItem.Item.Price);
                Session.CurrentTrader.AddItemToInventory(groupedInventoryItem.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedInventoryItem.Item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {

            if (((FrameworkElement)sender).DataContext is GroupedInventoryItem groupedInventoryItem)
            {
                if (Session.CurrentPlayer.Gold >= groupedInventoryItem.Item.Price)
                {
                    Session.CurrentPlayer.SpendGold(groupedInventoryItem.Item.Price);
                    Session.CurrentTrader.RemoveItemFromInventory(groupedInventoryItem.Item);
                    Session.CurrentPlayer.AddItemToInventory(groupedInventoryItem.Item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}