namespace Engine
{
    public class BaseEntity
    {
        public event Action OnUse;
        public event Action OnCreate;

        public BaseEntity()
        {
            Console.WriteLine("BaseEntity Constructor");
            OnCreate();
        }

        virtual public void Use()
        {
            OnUse();
        }
    }
}
