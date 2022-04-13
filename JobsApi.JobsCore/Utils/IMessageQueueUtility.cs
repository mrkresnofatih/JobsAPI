using System.Threading.Tasks;

namespace JobsApi.JobsCore.Utils
{
    public interface IMessageQueueUtility
    {
        Task PushMessageToQueue(object data);
    }
}