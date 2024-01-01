using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components.Widgets
{
    public partial class InboxWidget
    {
        public int MessageCount { get; set; } = 0;

        [Inject]
        public ApplicationState? ApplicationState { get; set; }

        protected override void OnInitialized()
        {
            MessageCount = ApplicationState.NumberOfMessages;
        }
    }
}
