namespace JobsApi.JobsCore.Utils
{
    public static class ChannelGetterUtility
    {
        public static string GetHomeChannel(string username)
        {
            return $"HOME_LISTENER-{username}";
        }
    }

    public static class ChannelEventNames
    {
        public const string HomeReceiveOneJob = "HomeReceiveOneJob";
        public const string HomeReceiveJobList = "HomeReceiveJobList";
    }
}