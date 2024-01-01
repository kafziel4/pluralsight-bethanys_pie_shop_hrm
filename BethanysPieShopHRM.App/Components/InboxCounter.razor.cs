using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class InboxCounter
    {
        private int _messageCount;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        protected override void OnInitialized()
        {
            _messageCount = new Random().Next(10);
            ApplicationState.NumberOfMessages = _messageCount;
        }
    }
}
