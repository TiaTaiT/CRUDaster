using CRUDaster.Components.CommonDialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CRUDaster.Components.Pages
{
    public abstract class BaseEntityPage<TDto> : ComponentBase
        where TDto : class
    {
        [Inject] protected IDialogService? DialogService { get; set; }
        [Inject] protected ISnackbar? Snackbar { get; set; }
        [Inject] protected HttpClient? HttpClient { get; set; }

        protected IEnumerable<TDto> Items = [];
        protected bool Loading = true;
        protected string SearchString = "";

        /// <summary>
        /// Must be overridden to define the API endpoint (e.g. "api/brands")
        /// </summary>
        protected abstract string ApiEndpoint { get; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected virtual async Task LoadData()
        {
            Loading = true;
            try
            {
                Items = await HttpClient.GetFromJsonAsync<IEnumerable<TDto>>(ApiEndpoint)
                 ?? [];
            }
            catch (Exception ex)
            {
                var fullUrl = new Uri(HttpClient.BaseAddress!, ApiEndpoint).ToString();
                Snackbar.Add($"Error loading items: {ex.Message}. \"GET: {fullUrl}\"", Severity.Error);
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

            if (result != null && !result.Canceled)
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

            if (result != null && !result.Canceled)
            {
                await LoadData();
                Snackbar.Add("Item updated successfully!", Severity.Success);
            }
        }

        protected async Task ShowDeleteDialog(string name, object itemId)
        {
            var parameters = new DialogParameters
            {
                ["ContentText"] = $"Are you sure you want to delete '{name}'? This action cannot be undone.",
                ["ButtonText"] = "Delete",
                ["Color"] = Color.Error
            };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Delete Item", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                try
                {
                    var response = await HttpClient.DeleteAsync($"{ApiEndpoint}/{itemId}");
                    response.EnsureSuccessStatusCode();

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