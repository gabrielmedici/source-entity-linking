using System.Reflection;
using System.Runtime.CompilerServices;

string path = @"F:\Documents\Code\EntityLinker\Entities\bin\Debug\net6.0\Entities.dll";
Assembly assembly = Assembly.LoadFile(path);

Type baseEntity = typeof(Engine.BaseEntity);

foreach (Type type in assembly.GetTypes()
    .Where(myType =>
        myType.IsClass &&
        !myType.IsAbstract && 
        myType.IsSubclassOf(baseEntity)))
{
    Console.WriteLine("\n\n=========== " + type.FullName + " ===========");

    Console.WriteLine("\n=========== Events ============");
    foreach (var @event in type.GetEvents())
        Console.WriteLine(@event.Name);

    Console.WriteLine("\n=========== Methods ============");
    foreach (MethodInfo method in type.GetMethods())
        Console.WriteLine(method.Name);
}

Console.WriteLine("\n\n=========== INPUT ============\n");

Type? selectedClass = null;

EventInfo? selectedEvent = null;
MethodInfo? selectedMethod = null;

while (true)
{
    Console.Write("> ");
    string input = Console.ReadLine() ?? "";

    if(input == "clear")
    {
        selectedClass = null;
        selectedEvent = null;
        selectedMethod = null;
    } else if (input.StartsWith("ent"))
    {
        var parts = input.Split(" ");
        if (parts.Length < 2)
        {
            Console.WriteLine("Missing entity name");
            continue;
        }

        var selection = parts[1];

        var type = assembly.GetType(selection);
        if(type == null || !type.IsClass || !type.IsSubclassOf(baseEntity))
        {
            Console.WriteLine($"{selection} is not an entity");
            continue;
        }

        selectedClass = type;
        Console.WriteLine($"Selected class {selectedClass.FullName}");
    } else if (input.StartsWith("ev"))
    {
        if(selectedClass == null)
        {
            Console.WriteLine("No class selected");
            continue;
        }

        var parts = input.Split(" ");
        if (parts.Length < 2)
        {
            Console.WriteLine("Missing entity name");
            continue;
        }

        var selection = parts[1];

        var ev = selectedClass.GetEvent(selection);
        if (ev == null) {
            Console.WriteLine("Invalid event");
            continue;
        }

        selectedEvent = ev;
        Console.WriteLine($"Selected event {selectedEvent.Name}");
    }
    else if (input.StartsWith("fn"))
    {
        if (selectedClass == null)
        {
            Console.WriteLine("No class selected");
            continue;
        }

        var parts = input.Split(" ");
        if (parts.Length < 2)
        {
            Console.WriteLine("Missing entity name");
            continue;
        }

        var selection = parts[1];

        var fn = selectedClass.GetMethod(selection);
        if (fn == null)
        {
            Console.WriteLine("Invalid method");
            continue;
        }

        selectedMethod = fn;
        Console.WriteLine($"Selected method {selectedMethod.Name}");
    } else if (input == "status")
    {
        //Console.WriteLine($"Current Selected class: {selectedClass?.FullName ?? "none"}");
        Console.WriteLine($"Selected event: {selectedEvent?.Name ?? "none"} of ent {selectedEvent?.DeclaringType?.FullName ?? "none"}");
        Console.WriteLine($"Selected method: {selectedMethod?.Name ?? "none"} of ent {selectedMethod?.DeclaringType?.FullName ?? "none"}");
    } else if (input == "link")
    {
        if(selectedEvent == null || selectedMethod == null) {
            Console.WriteLine("You need to select an event and a method");
            continue;
        }

        Console.WriteLine($"{selectedMethod.DeclaringType?.FullName ?? "none"} event {selectedEvent.Name} -> {selectedMethod.DeclaringType?.FullName ?? "none"} method {selectedMethod.Name}");
    }

    Console.WriteLine("");
}