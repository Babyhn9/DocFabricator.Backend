namespace ServerDocFabricator.BL.Mappers
{
    public interface IModelMapper <From, To>
    {
        To Map (From from);
    }
}
