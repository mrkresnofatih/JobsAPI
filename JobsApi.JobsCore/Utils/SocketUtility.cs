using System.Threading.Tasks;
using PusherServer;

namespace JobsApi.JobsCore.Utils
{
    public class SocketUtility
    {
        public SocketUtility(Pusher pusher)
        {
            _pusher = pusher;
        }

        private readonly Pusher _pusher;

        public async Task DischargeAsync(string channelName, string eventName, object data)
        {
            await _pusher.TriggerAsync(channelName, eventName, data);
        }
    }
}