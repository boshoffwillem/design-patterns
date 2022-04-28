public class DecoratorWithDependencyInjection {}

public interface IReportingService
{
    void Report();
}

public class ReportingService : IReportingService
{
    public void Report()
    {
        Console.WriteLine("Here is your report.");
    }
}

public class ReportingServiceWithLoggging : IReportingService
{
    private IReportingService _decoratedReportingService;

    public ReportingServiceWithLoggging(IReportingService decoratedReportingService)
    {
        _decoratedReportingService = decoratedReportingService;
    }

    public void Report()
    {
        Console.WriteLine("Commencing log...");
        _decoratedReportingService.Report();
        Console.WriteLine("Ending log...");
    }
}

