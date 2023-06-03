namespace FourTwenty.IoT.Server.ViewModels
{
    public class EntityVm<T> where T : new()
    {

        public T DbEntity { get; private set; } = new T();
        public void SetEntity(T entity) => DbEntity = entity;
    }
}
