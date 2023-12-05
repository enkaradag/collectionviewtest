using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CollectionViewTest
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        public class ROWDATA
        {
            public string FIELD1 { get; set; }
        }

        int CreatedRowCount = 0;
        ObservableCollection<ROWDATA> ROWS = new ObservableCollection<ROWDATA>();

        private void btn_fill_Clicked(object sender, EventArgs e)
        {

            /////AS SEEN DEBUG OUTPUT, 100 ROWS ARE CREATED IMMIDIATELY WITH view.ItemsSource=ROWS
            /////IF 10000 ROWS ARE ADDED TO ROWLIST, UI FREEZES SINCE CV TRIES TO LOAD ALL ROWS

            //            for (int i = 0; i < 10000; i++)
            for (int i = 0; i < 100; i++)
                ROWS.Add(new ROWDATA() { 
                    FIELD1 = Guid.NewGuid().ToString(),
                });

            view.ItemTemplate = new DataTemplate(() =>
            {

                CreatedRowCount++;
                Debug.WriteLine("CreatedRowCount=" + CreatedRowCount);

                Grid g = new Grid() { };
                g.RowDefinitions.Add(new RowDefinition(new GridLength(50)));
                g.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(400)));

                Label datalabel = new Label() { VerticalTextAlignment = TextAlignment.Center };
                datalabel.SetBinding(Label.TextProperty, "FIELD1");
                g.Children.Add(datalabel);


                //THIS STACKLAYOUT IS NEVER DRAWN
                StackLayout horizontalline = new StackLayout() { VerticalOptions = LayoutOptions.End, HeightRequest = 1, BackgroundColor = Colors.Silver };
                g.Children.Add(horizontalline);

                return g;
            });

            view.ItemsSource = ROWS;
        }

    }


}
