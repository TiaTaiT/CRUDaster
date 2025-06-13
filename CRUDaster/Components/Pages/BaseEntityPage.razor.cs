using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Components.CommonDialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CRUDaster.Components.Pages
{
    public abstract class BaseEntityPage<TDto, TService> : ComponentBase
    where TDto : class
    where TService : IEntityService<TDto>
    {
        [Inject] protected TService EntityService { get; set; }
        [Inject] protected IDialogService DialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }

        protected IEnumerable<TDto> Items;
        protected bool Loading = true;
        protected string SearchString = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected virtual async Task LoadData()
        {
            Loading = true;
            try
            {
                Items = await EntityService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error loading items: {ex.Message}", Severity.Error);
            }
            finally
            {
                Loading = false;
            }
        }

        protected virtual bool FilterFunc(TDto dto) => true;

        protected async Task ShowCreateDialog<TDialog>(string title)
            where TDialog : ComponentBase
        {
            var dialog = await DialogService.ShowAsync<TDialog>(title, new DialogParameters());
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadData();
                Snackbar.Add("Item created successfully!", Severity.Success);
            }
        }

        protected async Task ShowEditDialog<TDialog>(string title, object itemId)
            where TDialog : ComponentBase
        {
            var parameters = new DialogParameters { ["ItemId"] = itemId };
            var dialog = await DialogService.ShowAsync<TDialog>(title, parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadData();
                Snackbar.Add("Item updated successfully!", Severity.Success);
            }
        }

        protected async Task ShowDeleteDialog(string name, object itemId, Func<Task> deleteAction)
        {
            var parameters = new DialogParameters
            {
                ["ContentText"] = $"Are you sure you want to delete '{name}'? This action cannot be undone.",
                ["ButtonText"] = "Delete",
                ["Color"] = Color.Error
            };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Delete Item", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                try
                {
                    await deleteAction();
                    await LoadData();
                    Snackbar.Add("Item deleted successfully!", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Error deleting item: {ex.Message}", Severity.Error);
                }
            }
        }
    }
}
