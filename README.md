# SimpleMapper
Simple object mapper library

## General Map
It automatically maps the properties with same name and type.
i.e. 
```C#
  obj1.Name (string) -> obj2.Name (string)
  obj1.Name (string) -> obj2.Name (string)
```

## Usage:
```C#
Destination obj = Map.Initialize<Destination>(Source);
```

## Collection Map
Source property type should inherit from destination property type.
