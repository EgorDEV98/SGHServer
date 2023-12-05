namespace SGHServer.Persistence;

public static class DataStoreInitializer
{
    public static void Init(DataStore context)
    {
        context.Database.EnsureCreated();
    }
}