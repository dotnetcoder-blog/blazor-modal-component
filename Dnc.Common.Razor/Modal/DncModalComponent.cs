using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.Modal
{
    public class DncModalComponent<TItem> : ComponentBase
    {
        [Parameter] public RenderFragment<TItem> HeaderTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> BodyTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> FooterTemplate { get; set; }

        [Parameter] public EventCallback<TItem> OnSubmit { get; set; }
        [Parameter] public EventCallback<TItem> OnShow { get; set; }
        [Parameter] public string Size { get; set; }

        public EditContext EditContext { get; protected set; }
        protected TItem Item { get; set; }

        protected bool IsVisible { get; set; }
        protected string ModalSize { get; set; } = string.Empty;

        public void SetEditContext(EditContext editContext)
        {
            EditContext = editContext;
        }

        public async Task Show(TItem item = default)
        {
            ModalSize = Size switch
            {
                "Small" => "modal-sm",
                "Medium" => string.Empty,
                "Larg" => "modal-lg",
                "ExtraLarg" => "modal-xl",
                _ => string.Empty,
            };

            IsVisible = true;
            Item = item;

            var task = OnShow.InvokeAsync(Item);
            if (task != null && !task.IsCompleted)
            {
                StateHasChanged();
                await task;
            }

            EditContext ??= new EditContext(new { });

            StateHasChanged();
        }
        public async Task HandleSubmit()
        {
            await OnSubmit.InvokeAsync(Item);
        }
        public void Close()
        {
            IsVisible = false;
            Item = default;
            EditContext = null;

            StateHasChanged();
        }
    }
}
