using System.Reflection;

string path = @"F:\Documents\Code\EntityLinker\Entities\bin\Debug\net6.0\Entities.dll";
Assembly assembly = Assembly.LoadFile(path);

Type baseEntity = typeof(Engine.BaseEntity);

foreach (Type type in assembly.GetTypes()
    .Where(myType =>
        myType.IsClass &&
        !myType.IsAbstract && 
        myType.IsSubclassOf(baseEntity)))
{
    Console.WriteLine("\n\n=========== " + type.Name + " ===========");

    Console.WriteLine("\n=========== Events ============");
    foreach (var @event in type.GetEvents())
        Console.WriteLine(@event.Name);

    Console.WriteLine("\n=========== Methods ============");
    foreach (MethodInfo method in type.GetMethods())
        Console.WriteLine(method.Name);
}

// Find a way to link Events to Methods just like hammer does on the source engine