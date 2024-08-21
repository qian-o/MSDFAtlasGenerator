using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace MSDFAtlasGenerator.Contracts;

public abstract class View : Page
{
    protected View()
    {
        // Loaded
        {
            EventTrigger eventTrigger = new(nameof(Loaded));

            InvokeCommandAction action = new();
            BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, new Binding("ViewLoadedCommand"));

            eventTrigger.Actions.Add(action);

            Interaction.GetTriggers(this).Add(eventTrigger);
        }

        // Unloaded
        {
            EventTrigger eventTrigger = new(nameof(Unloaded));

            InvokeCommandAction action = new();
            BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, new Binding("ViewUnloadedCommand"));

            eventTrigger.Actions.Add(action);

            Interaction.GetTriggers(this).Add(eventTrigger);
        }
    }
}
