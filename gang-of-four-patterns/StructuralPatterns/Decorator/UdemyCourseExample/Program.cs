using Autofac;

var cb = new CodeBuilder();
cb.AppendLine("class Foo")
    .AppendLine("{")
    .AppendLine("}");
Console.WriteLine(cb);

DecoratorAndAdapter s = "hello ";
s += "world";
Console.WriteLine(s);

var d = new Dragon();
d.Weight = 123;
d.Fly();
d.Crawl();

Console.WriteLine();

var square = new Square(1.23f);
Console.WriteLine(square.AsString());
var redSquare = new ColoredShapeDecorator(square, "red");
Console.WriteLine(redSquare.AsString());
var transparentRedSquare = new TransparentShapeDecorator(redSquare, 0.5f);
Console.WriteLine(transparentRedSquare.AsString());

Console.WriteLine();

//var squareCycle = new SquareCycle(2.34f);
//var colored1 = new ColoredShapeDecoratorCycle(squareCycle, "red");
//var colored2 = new ColoredShapeDecoratorCycle(colored1, "blue");
//Console.WriteLine(colored2.AsString());

var redSquareStatic = new ColoredShapeStatic<SquareStatic>("red");
Console.WriteLine(redSquareStatic.AsString());
var circleStatic = new TransparentShapeStatic<ColoredShapeStatic<CircleStatic>>(0.4f);
Console.WriteLine(circleStatic.AsString());

var b = new ContainerBuilder();
b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
b.RegisterDecorator<IReportingService>(
    (context, service) => new ReportingServiceWithLoggging(service),
    "reporting"
);

using var c = b.Build();
var r = c.Resolve<IReportingService>();
r.Report();


