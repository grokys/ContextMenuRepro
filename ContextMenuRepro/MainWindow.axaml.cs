using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace ContextMenuRepro
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (!e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                e.Handled = true;
                return;
            }

            var cm = new ContextMenu();
            var items = new List<MenuItem>();

            for (var i = 0; i < 100; ++i)
            {
                items.Add(new MenuItem { Header = $"Item {i}" });
            }

            cm.Items = items;

            ContextMenu = cm;
            cm.PlacementAnchor = Avalonia.Controls.Primitives.PopupPositioning.PopupAnchor.TopLeft;
            cm.PlacementGravity = Avalonia.Controls.Primitives.PopupPositioning.PopupGravity.BottomRight;
            cm.PlacementMode = PlacementMode.AnchorAndGravity;
            cm.PlacementRect = new Avalonia.Rect(100, 100, 1, 1);
            cm.Open();
            e.Handled = true;
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);
            e.Handled = true;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
