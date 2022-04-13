using System.Threading.Tasks;

namespace JobsApi.AuthedGateway.Utils
{
    public interface IMessageQueueUtility
    {
        Task PushMessageToQueue(object data);
    }
}