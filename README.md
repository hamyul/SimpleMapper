# SimpleMapper
Simple object mapper library  
This library is used to automatically map properties between two objects of different types.


The project is still on going and new features should be added soon. Please check the features section below:  

## General Map
It automatically maps the properties with same name and type.
i.e. 
```csharp
obj1.Name (string) -> obj2.Name (string)
obj1.Name (string) -> obj2.Name (string)
```

## Usage:
```csharp
Destination obj = Map.Initialize<Destination>(Source);
```

## Restriction
Source property type should inherit from destination property type.  
i.e.

```csharp
class A
{
    public int Id { get; set; }
    public IList<A> Items { get; set; }
}

class B
{
    public int Id { get; set; }
    public IEnumerable<A> Items { get; set; }
}

class C
{
    void ItWorks()
    {
        var source = new A();
        source.Add(new A());
        source.Add(new A());

        // Items property in the destination is correctly mapped.
        B destination = Map.Initialize<B>(source);
    }

    void ItDoesNotWork()
    {
        var source = new B();
        source.Add(new A());
        source.Add(new A());

        // Items property in the destination is not mapped.
        A destination = Map.Initialize<A>(source);
    }
}
```
  
## Features

### Implemented
- Map properties with same name and type.  
- Map collections.

### Coming soon
- Map properties without same name (using attributes)
