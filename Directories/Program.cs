using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

if (!Directory.Exists(storesDirectory))
{
    Console.WriteLine($"Missing folder: {storesDirectory}");
    return;
}

var salesFiles = FindFiles(storesDirectory).ToList();
var salesByFile = CalculateSalesByFile(salesFiles);
var salesTotal = salesByFile.Values.Sum();

// Keep original module behavior
File.AppendAllText(
    Path.Combine(salesTotalDir, "totals.txt"),
    $"{salesTotal}{Environment.NewLine}");

// New requirement: summary report
var summaryPath = Path.Combine(salesTotalDir, "sales-summary.txt");
GenerateSalesSummaryReport(summaryPath, salesTotal, salesByFile);

IEnumerable<string> FindFiles(string folderName)
{
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    return foundFiles.Where(file => Path.GetExtension(file).Equals(".json", StringComparison.OrdinalIgnoreCase));
}

Dictionary<string, double> CalculateSalesByFile(IEnumerable<string> salesFiles)
{
    var salesByFile = new Dictionary<string, double>();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesByFile[file] = data?.Total ?? 0;
    }

    return salesByFile;
}

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

record SalesData(double Total);