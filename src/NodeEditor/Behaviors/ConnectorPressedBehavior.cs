﻿using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using NodeEditor.ViewModels;

namespace NodeEditor.Behaviors
{
    public class ConnectorPressedBehavior : Behavior<Control>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is { })
            {
                AssociatedObject.AddHandler(InputElement.PointerPressedEvent, Pressed, RoutingStrategies.Tunnel);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject is { })
            {
                AssociatedObject.RemoveHandler(InputElement.PointerPressedEvent, Pressed);
            }
        }

        private void Pressed(object? sender, PointerPressedEventArgs e)
        {
            if (AssociatedObject is not { })
            {
                return;
            }

            if (AssociatedObject.DataContext is not PinViewModel pinViewModel)
            {
                return;
            }

            if (pinViewModel.Parent is not ConnectedNodeViewModel connectedNodeViewModel)
            {
                return;
            }

            if (connectedNodeViewModel.Parent is DrawingNodeViewModel drawingNodeViewModel)
            {
                drawingNodeViewModel.Pressed(pinViewModel);
            }
        }
    }
}
