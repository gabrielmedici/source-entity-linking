using Engine;

namespace Entities
{
    public class World : BaseEntity
    {

    } 

    public class HelloWorld : BaseEntity
    {
        public override void Use()
        {
            base.Use();
            Console.WriteLine("Hello World");
        }

        public event Action OnCustom;

        public void Custom() {
        }
    }
}