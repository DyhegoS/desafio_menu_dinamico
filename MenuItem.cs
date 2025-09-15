namespace MenuDinamico;

public class MenuItem
{
    public string Text { get; set; }
    public string Route { get; set; }

    public MenuItem(string text, string route)
    {
        Text = text;
        Route = route;
    }

    public override string ToString()
    {
        return Text + " (" + Route + ")";
    }
}