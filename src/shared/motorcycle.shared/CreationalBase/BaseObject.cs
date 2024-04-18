namespace motorcycle.shared.CreationalBase
{
    public abstract class BaseObject<T> where T : class 
    {
        public void AddMessage(BaseError error)
        {
            throw error;
        }
    }
}
