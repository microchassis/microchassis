namespace MicroChassis
{
    public interface IMicroModule<THost>
        where THost: class
    {
        void Setup(THost host);
    }
}