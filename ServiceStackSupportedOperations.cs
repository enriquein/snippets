    public class SupportedOperationsService : Service
    {
        public object Any(SupportedOperationsRequest request)
        {
            return ((ServiceRoutes) GetAppHost().Routes).RestPaths.Select<RestPath, object>(m => new
                {
                    RequestType = m.RequestType.Name,
                    m.Path,
                    AllowedVerbs = (m.AllowsAllVerbs) ? "Any" : m.AllowedVerbs
                });
        }
    }