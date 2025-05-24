namespace MicroChassis
{
    public interface IMicroChassis<THost>
        where THost : class
    {
        void Setup(THost host);
    }
}