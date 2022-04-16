namespace JobsApi.JobsCore.Utils
{
    public class TraceableQueuePayload<T>
    {
        public T Data { get; set; }
        
        public string SpanId { get; set; }
        
        public string RequestAddress { get; set; }
    }

    public static class TraceableQueueBuilder
    {
        public static TraceableQueuePayload<T> Build<T>(T data, string spanId, string requestAddress)
        {
            return new TraceableQueuePayload<T>
            {
                Data = data,
                SpanId = spanId,
                RequestAddress = requestAddress
            };
        }
    }
}