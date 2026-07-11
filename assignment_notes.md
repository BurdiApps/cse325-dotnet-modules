# Assignment Notes

## Quick background (my perspective)
I understand MVC patterns from other classes/tools, but this was my first time building it in .NET.  
The structure felt familiar:

- **Models** = data shape (`Pizza`)
- **Controller** = handles HTTP requests (`PizzaController`)
- **Service** = business/data logic (`PizzaService`)

So the pattern made sense, but I had to learn .NET conventions, attributes, and CLI workflow.

---

## 1) Web API module evidence + added pizza record

### Existing + additional pizza records
From `ContosoPizza/Services/PizzaService.cs`:

- `Id=1, Name="Classic Italian", IsGlutenFree=false`
- `Id=2, Name="Veggie", IsGlutenFree=true`
- `Id=3, Name="Pepperoni", IsGlutenFree=false` ✅ (my added record)

### CRUD verification (working request/response + status codes)

- **GET** `/pizza` → **200 OK**
- **POST** `/pizza` with body `{"name":"Hawaiian","isGlutenFree":false}` → **201 Created**
- **PUT** `/pizza/3` with body `{"id":3,"name":"Hawaiian Deluxe","isGlutenFree":false}` → **204 No Content**
- **DELETE** `/pizza/3` → **204 No Content**

> Note: I used id `3` because that was the id returned from POST in my test run.

---

## 2) Sales summary function (Part 2 text copy)

From `Directories/Program.cs`:

```csharp
void GenerateSalesSummaryReport(string outputPath, double totalSales, Dictionary<string, double> salesByFile)
{
    var sb = new StringBuilder();
    sb.AppendLine("Sales Summary");
    sb.AppendLine("----------------------------");
    sb.AppendLine($" Total Sales: {totalSales:C}");
    sb.AppendLine();
    sb.AppendLine(" Details:");

    foreach (var kvp in salesByFile.OrderBy(k => k.Key))
    {
        var relativeName = Path.GetRelativePath(currentDirectory, kvp.Key);
        sb.AppendLine($"  {relativeName}: {kvp.Value:C}");
    }

    File.WriteAllText(outputPath, sb.ToString());
}
```

---

## Short reflection
This assignment helped connect MVC concepts I already knew to actual .NET implementation.  
Biggest new things for me were:

- attribute routing (`[HttpGet]`, `[HttpPost]`, etc.)
- using `ActionResult/IActionResult`
- testing endpoints with `curl`
- organizing multiple projects in the same repo